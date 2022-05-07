using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using JXI.RF.DSP.Generation;
using JXI.RF.DSP.Spectrum;
using SeeSharpTools.JXI.FileIO.VectorFile;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SpectrumFileReadExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region------------------------- 私有成员 -------------------------

        /// <summary>
        /// 频谱数据，依次按照通道、频段索引(Double)。
        /// </summary>
        private double[][][] _spectrumDouble;

        /// <summary>
        /// 频谱数据，依次按照通道、频段索引(Float)。
        /// </summary>
        private float[][][] _spectrumFloat;

        /// <summary>
        /// 用于存放用户通过GUI控件所设定的信号参数。
        /// </summary>
        private SpectrumSamplingInformation _samplingInfo;

        /// <summary>
        /// 数据类型
        /// </summary>
        private string _dataType;

        //计算存储频谱数据的时间间隔。
        Stopwatch _stopwatchForSpectrum = new Stopwatch();

        #endregion

        #region------------------------ GUI事件处理 -----------------------

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 设置控件默认值。
            _guiWriteDataType.SelectedIndex = 0;

            // 触发一次“参数已修改”事件，完成初始化时的仿真波形显示。
            GuiSignalParam_ValueChanged(this, null);
        }

        private void GuiSignalParam_ValueChanged(object sender, EventArgs e)
        {
            // 获取通道数。
            int numberOfChannels = (int)_guiNumberOfChannels.Value;

            // 获取每个通道的频段数（简化实现，只允许配置1或2个频段）。
            int numberOfBands = _guiBand2Enabled.Checked ? 2 : 1;

            // 从GUI控件获取各通道、频段的信号参数，保存与私有成员_samplingInfo中。
            _samplingInfo = new SpectrumSamplingInformation();
            _samplingInfo.Unit = "dBm";
            for (int bandIndex = 0; bandIndex < numberOfBands; bandIndex++)
            {
                // 获取当前频段的信息，计算频谱线数。
                double freqStart = (double)(((bandIndex == 0) ? _guiBand1FreqStart : _guiBand2FreqStart).Value) * 1E6;
                double freqStep = (double)(((bandIndex == 0) ? _guiBand1FreqStep : _guiBand2FreqStep).Value) * 1E6;
                double freqStop = (double)(((bandIndex == 0) ? _guiBand1FreqStop : _guiBand2FreqStop).Value) * 1E6;
                int numOfSpectralLines = (int)((freqStop - freqStart) / freqStep + 1);

                // 更新频谱线数显示。
                ((bandIndex == 0) ? _guiBand1NumOfLines : _guiBand2NumOfLines).Value = numOfSpectralLines;

                // 将当前频段的参数存入私有成员_samplingInfo，每个频段中所有通道都有相同的频率参数，但可以有不同的通道参数（如参考电平）。
                _samplingInfo.Bands.Add(new BandSpectrumSamplingInformation(freqStart, freqStop, freqStep, numOfSpectralLines));

                for (int channelIndex = 0; channelIndex < numberOfChannels; channelIndex++)
                {
                    _samplingInfo.Bands[bandIndex].Channels.Add(new BandSpectrumChannelInformation());
                    // 仿真：在当前Band中，不同的通道有不同的参考电平，依次为0，-10，-20等等。
                    _samplingInfo.Bands[bandIndex].Channels[channelIndex].ReferenceLevel = 0 - (channelIndex * 10);
                }
            }
            GenerateSpectrum();
            DisplaySpectrum();
        }

        private void GuiBand2Enabled_CheckedChanged(object sender, EventArgs e)
        {
            // 若开启Band2则允许修改参数。
            _guiSignalBand2Info.Enabled = _guiBand2Enabled.Checked;

            // 参数已修改。
            GuiSignalParam_ValueChanged(this, null);
        }

        /// <summary>
        /// 获取写入的路径。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuiBrowseFolder_Click(object sender, EventArgs e)
        {
            if (_guiFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                _guiDestinationFolder.Text = _guiFolderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        ///  Button Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuiStart_Click(object sender, EventArgs e)
        {
            // 启动BackgroundWorker。
            _bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Button Stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuiStop_Click(object sender, EventArgs e)
        {
            // 停止BackgroundWorker。
            _bgWorker.CancelAsync();
        }

        /// <summary>
        /// 数据类型选择。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuiWriteDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取数据类型。
            _dataType = _guiWriteDataType.SelectedItem.ToString();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_bgWorker.IsBusy)
            {
                // 简化实现：如果当前正在连续读取，不允许关闭窗口。
                MessageBox.Show("当前正在写入数据，请先停止任务。");
                e.Cancel = true;
            }
        }
        #endregion

        #region------------------------- 私有方法 -------------------------

        /// <summary>
        /// 根据GUI控件所设定的信号参数（已存放于私有成员_samplingInfo中）生成频谱数据，存放于私有成员_spectrum中。
        /// </summary>
        private void GenerateSpectrum()
        {
            // 获取通道数。
            int numberOfChannels = _samplingInfo.Bands.First().Channels.Count;
            _spectrumDouble = new double[numberOfChannels][][];

            // 获取频段数。
            int numberOfBands = _samplingInfo.Bands.Count;
            for (int i = 0; i < numberOfChannels; i++)
            {
                _spectrumDouble[i] = new double[numberOfBands][];
            }

            // 依次生成各通道、各频段的频谱数据。
            for (int channelIndex = 0; channelIndex < numberOfChannels; channelIndex++)
            {
                for (int bandIndex = 0; bandIndex < numberOfBands; bandIndex++)
                {
                    _spectrumDouble[channelIndex][bandIndex] = new double[_samplingInfo.Bands[bandIndex].NumOfSpectralLines];

                    // 先生成单正弦IQ数据，简化实现：IQ数据点数为频谱线数的2倍，采样率为带宽的1.25倍，正弦波频率为“通道 * 0.1倍带宽”。
                    var iqSignal = new Complex[_samplingInfo.Bands[bandIndex].NumOfSpectralLines * 2];
                    double bandwidth = _samplingInfo.Bands[bandIndex].FrequencyStop - _samplingInfo.Bands[bandIndex].FrequencyStart;
                    double iqSampleRate = bandwidth * 1.25;
                    EasyGeneration.ComplexSine(iqSignal, bandwidth * (channelIndex + 1) * 0.1, iqSampleRate, 1, 80);

                    // 计算信号的频谱。
                    EasySpectrum.AutoPowerSpectrum(iqSignal, iqSampleRate, bandwidth, _spectrumDouble[channelIndex][bandIndex]);
                }
            }
        }

        /// <summary>
        /// 将私有成员_spectrum中存放的频谱数据在Chart中显示。
        /// </summary>
        private void DisplaySpectrum()
        {
            #region--------------将一个通道中的多个频段合并在一起显示。--------------------

            // 获取所有频段的谱线数的总和（即各通道要显示的频谱线数）。
            int numberOfBands = _samplingInfo.Bands.Count;
            int totalSpectralLines = 0;
            for (int i = 0; i < numberOfBands; i++) { totalSpectralLines += _samplingInfo.Bands[i].NumOfSpectralLines; }

            // 分配数组用于存放各频段合并后的数据。
            int numberOfChannels = _samplingInfo.Bands.First().Channels.Count;
            double[][] xData = new double[numberOfChannels][];
            double[][] yData = new double[numberOfChannels][];
            for (int i = 0; i < numberOfChannels; i++)
            {
                xData[i] = new double[totalSpectralLines];
                yData[i] = new double[totalSpectralLines];
            }

            // 合并多个频段的数据。
            for (int channelIndex = 0; channelIndex < numberOfChannels; channelIndex++)
            {
                int startIndexOfCurrentBand = 0;
                for (int bandIndex = 0; bandIndex < numberOfBands; bandIndex++)
                {
                    int length = _samplingInfo.Bands[bandIndex].NumOfSpectralLines;
                    double f0 = _samplingInfo.Bands[bandIndex].FrequencyStart;
                    double df = _samplingInfo.Bands[bandIndex].FrequencyStep;

                    // x轴数据需生成。
                    for (int i = 0; i < length; i++) { xData[channelIndex][startIndexOfCurrentBand + i] = (f0 + df * i) / 1E6; }

                    // y轴数据从_spectrum中拷贝。
                    Array.Copy(_spectrumDouble[channelIndex][bandIndex], 0, yData[channelIndex], startIndexOfCurrentBand, length);

                    // 继续合并下一频段。
                    startIndexOfCurrentBand += length;
                }
            }
            #endregion

            // 显示频谱。
            _guiSpectrumChart.Plot(xData, yData);
        }

        #endregion

        #region------------------------- BackgroundWorker实现文件生成 -------------------------

        /// <summary>
        /// 完成或异常提示。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // BackgroundWorker的运行中出现异常，则提示用户。
                MessageBox.Show(e.Error.Message + Environment.NewLine + e.Error.StackTrace);
            }
            else
            {
                if (!e.Cancelled)
                {
                    // BackgroundWorker未发生异常，用户也未取消，说明任务已正常结束。                   
                    MessageBox.Show("文件已生成。");
                    _guiProgressBar.Value = 100;
                    _guiProgressValue.Text = "100 %";
                }
                else
                {
                    // 用户主动取消了操作，则不作任何提示。
                }
            }
            // 任务已经结束，设置GUI控件状态。
            _guiSignalParam.Enabled = true;
            _guiFileParam.Enabled = true;
            _guiStart.Enabled = true;
            _guiStop.Enabled = false;
        }

        /// <summary>
        /// 更新进度条。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _guiProgressBar.Value = e.ProgressPercentage;
            _guiProgressValue.Text = string.Format("{0} %", e.ProgressPercentage);
        }

        /// <summary>
        /// 创建文件并进行写操作。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var bgWorker = sender as BackgroundWorker;

            //存储路径。
            string destinationFolder = _guiDestinationFolder.Text;
            int fileLengthInFrames = (int)_guiFileLength.Value;

            // 若存储目录目录不存在，则创建之，并自动生成文件名。
            if (!Directory.Exists(destinationFolder)) { Directory.CreateDirectory(destinationFolder); }
            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_");
            fileName += string.Format("{0}Channels_{1}Bands_{2}Frames_{3}_Spectrum.spt", (int)_guiNumberOfChannels.Value, _guiBand2Enabled.Checked ? 2 : 1, _guiFileLength.Value, _dataType);
            string filePath = Path.Combine(destinationFolder, fileName);

            // 实例化SpectrumFile对象，创建文件。
            SpectrumFile spectrumFile = new SpectrumFile();
            spectrumFile.Open(filePath, FileMode.Create, FileAccess.Write);
            switch (_dataType)
            {
                case "Double": { spectrumFile.Storage.DataType = DataType.RealD64; break; }
                case "Float": { spectrumFile.Storage.DataType = DataType.RealF32; break; }
            }
            spectrumFile.Storage.NumberOfChannels = (int)_guiNumberOfChannels.Value;
            spectrumFile.Sampling.Unit = _samplingInfo.Unit;

            for (int i = 0; i < _samplingInfo.Bands.Count; i++)
            {
                var bandInfo = _samplingInfo.Bands[i];
                spectrumFile.Sampling.Bands.Add(new BandSpectrumSamplingInformation
               (bandInfo.FrequencyStart, bandInfo.FrequencyStop, bandInfo.FrequencyStep, bandInfo.NumOfSpectralLines));
                for (int j = 0; j < bandInfo.Channels.Count; j++)
                {
                    spectrumFile.Sampling.Bands[i].Channels.Add(new BandSpectrumChannelInformation());
                    spectrumFile.Sampling.Bands[i].Channels[j].ReferenceLevel = bandInfo.Channels[j].ReferenceLevel;
                    spectrumFile.Sampling.Bands[i].Channels[j].FrequencyShift = bandInfo.Channels[j].FrequencyShift;
                }
            }

            // 数据存储时间间隔。
            spectrumFile.Sampling.Interval = TimeSpan.FromTicks((long)((double)_guiFrameIntervalInMilliSec.Value * TimeSpan.TicksPerMillisecond));

            //更新完文件头信息之后，写文件头。
            spectrumFile.WriteFileHeader();

            // 开启定时器。
            _stopwatchForSpectrum.Start();

            //写入文件。
            for (int i = 0; i < fileLengthInFrames; i++)
            {
                // 仿真生成数据。
                GenerateSpectrum();

                //显示数据。
                this.Invoke(new Action(() => { DisplaySpectrum(); }));

                // 如果写入数据的时间间隔满足大于等于Interval。
                if (_stopwatchForSpectrum.ElapsedMilliseconds >= spectrumFile.Sampling.Interval.TotalMilliseconds|| i == 0)
                {
                    // 写入数据。
                    if (spectrumFile.Storage.DataType == DataType.RealD64)
                    {
                        //写入Double数据
                        spectrumFile.Write(_spectrumDouble);
                    }
                    else if (spectrumFile.Storage.DataType == DataType.RealF32)
                    {

                        #region------------------------------转换数据类型Double→Float----------------------------

                        _spectrumFloat = new float[spectrumFile.Storage.NumberOfChannels][][];
                        for (int channelIndex = 0; channelIndex < spectrumFile.Storage.NumberOfChannels; channelIndex++)
                        {
                            _spectrumFloat[channelIndex] = new float[_samplingInfo.Bands.Count][];
                            for (int bandIndex = 0; bandIndex < _samplingInfo.Bands.Count; bandIndex++)
                            {
                                _spectrumFloat[channelIndex][bandIndex] = new float[_samplingInfo.Bands[bandIndex].NumOfSpectralLines];
                                for (int lines = 0; lines < _samplingInfo.Bands[bandIndex].NumOfSpectralLines; lines++)
                                {
                                    _spectrumFloat[channelIndex][bandIndex][lines] = (float)_spectrumDouble[channelIndex][bandIndex][lines];
                                }
                            }
                        }

                        #endregion

                        //写入Float数据                   
                        spectrumFile.Write(_spectrumFloat);
                    }

                    //更新进度。
                    bgWorker.ReportProgress((int)((i + 1) / (float)fileLengthInFrames * 100));
                    if (bgWorker.CancellationPending == true) { e.Cancel = true; break; }

                    // 重置定时器。
                    _stopwatchForSpectrum.Restart();
                }
                else
                {
                    // 如果不满足存储时间间隔，则存储次数i此时不应该加1。即：自减1.
                    i--;
                }
            }
        }
        #endregion     
    }
}
