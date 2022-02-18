using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JXI.FileIO.VectorFile;
using JXI.RF.DSP.Generation;

namespace FixFreqFrameFileExampleWriteComplex
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region------------------------- GUI事件处理 -------------------------

        private void GuiBrowseFolder_Click(object sender, EventArgs e)
        {
            if (_guiFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                _guiDestinationFolder.Text = _guiFolderBrowserDialog.SelectedPath;
            }
        }

        private void GuiStart_Click(object sender, EventArgs e)
        {
            // 设置控件状态。
            _guiSignalParam.Enabled = false;
            _guiFileParam.Enabled = false;
            _guiStart.Enabled = false;
            _guiStop.Enabled = true;

            // 启动BackgroundWorker。
            _bgWorker.RunWorkerAsync();
        }

        private void GuiStop_Click(object sender, EventArgs e)
        {
            // 停止BackgroundWorker。
            _bgWorker.CancelAsync();
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

        #region------------------------- BackgroundWorker实现文件生成 -------------------------

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 获取BackgroudWorker对象，用于运行控制。
            var bgWorker = sender as BackgroundWorker;

            FixFrequencyFrameFile vectorFile = null;

            try
            {
                // 从GUI控件获取信号生成参数。
                int numOfChannels = (int)_guiNumberOfChannels.Value;
                double sampleRate = (double)_guiSampleRate.Value * 1E6;
                double level = (double)_guiLevel.Value;

                // 从GUI控件获取文件参数。
                string destinationFolder = _guiDestinationFolder.Text;
                int numOfSamplesPerFrame = (int)_guiFrameLength.Value; 
                int numOfFrames = (int)_guiNumberOfFrame.Value;             

                #region------------------------- 根据文件参数生成文件名。 -------------------------

                // 若存储目录目录不存在，则创建之，并自动生成文件名。
                if (!Directory.Exists(destinationFolder)) { Directory.CreateDirectory(destinationFolder); }
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_");
                fileName += string.Format("{0} Channel_Sample Rate {1} MHz {2} Frames.IQ", numOfChannels, (sampleRate / 1E6).ToString("f3").TrimEnd('0').TrimEnd('.'), numOfFrames);
                string filePath = Path.Combine(destinationFolder, fileName);
               
                #endregion

                #region------------------------- 实例化信号生成对象，并分配数组空间用于存储生成的数据。 -------------------------

                // 为每个通道各自实例化正弦波发生器对象。
                var signalGenerators = new ComplexSineGenerator[numOfChannels];
                var randomGen = new Random();
                for (int i = 0; i < numOfChannels; i++)
                {
                    // 正弦波的频率为随机生成，幅度为1，并添加噪声。
                    signalGenerators[i] = new ComplexSineGenerator(sampleRate, sampleRate * 0.25 * randomGen.NextDouble(), 1);
                    signalGenerators[i].NoiseType = NoiseType.UniformWhiteNoise;
                    signalGenerators[i].SNR = 60;
                }

                // 分配数组空间，用于存放每次生成和写入的数据。
                var complexSineOneChannel = new Complex[numOfSamplesPerFrame];
                // 在写入文件时，所有通道的数据应合并组成交织格式的数组（Channel Interleaved）写入，故须分配空间用于存放多通道合并的数据。
                var shortSineAllChannels = new short[numOfSamplesPerFrame * 2 * numOfChannels];

                #endregion

                // 实例化FixFrameFile对象，创建文件。
                vectorFile = new FixFrequencyFrameFile();
                vectorFile.Open(filePath, FileMode.Create, FileAccess.Write, false);

                // 设置数据长度，类型。
                vectorFile.Frame.Length = numOfSamplesPerFrame;
                vectorFile.Frame.SamplingInfoType = FixFreqFrameFileSamplingInfoType.Shared;

                // 填写数据类型、通道数、采样率。
                vectorFile.Storage.DataType = DataType.ComplexI16;
                vectorFile.Storage.NumberOfChannels = numOfChannels;
                vectorFile.Sampling.SampleRate = sampleRate;

                // 根据通道数，填写各通道的中心频率、带宽、换算因子等。
                for (int i = 0; i < numOfChannels; i++)
                {
                    vectorFile.Sampling.Channels.Add(new BaseChannelSamplingInfo());
                    vectorFile.Sampling.Channels[i].RFFrequency = 1E9;
                    // 简化实现，带宽取值为：采样率 x 0.8。在实际应用中应填入真实信号带宽。
                    vectorFile.Sampling.Channels[i].Bandwidth = sampleRate * 0.8;
                    vectorFile.Sampling.Channels[i].RFScaleFactor = 1;
                    // 仿真实现：根据设定的信号电平，计算I16 -> 电压值的换算因子。在实际应用中应填入实际换算因子。
                    // 先将电平（dBm）转换为电压值（dBm -> mW -> V），然后计算对应I16 (-32767 ~ 32767）满量程的换算因子。
                    vectorFile.Sampling.Channels[i].DigitizerScaleFactor = Math.Sqrt(Math.Pow(10, level / 10) * 1E-3 * 50) / 32767;
                }

                // 写入文件头。
                vectorFile.WriteFileHeader();

                // 写入数据。
                for (int indexOfFrame = 0; indexOfFrame < numOfFrames; indexOfFrame++)
                {
                    #region------------------------- 生成各通道的数据，合并到一个数组中。 -------------------------

                    for (int channelIndex = 0; channelIndex < numOfChannels; channelIndex++)
                    {
                        // 生成正弦波。
                        signalGenerators[channelIndex].Generate(complexSineOneChannel);

                        // 将Complex转换为short，并填入通道交织（Channel-Interleaved）数组中相应的位置。
                        for (int i = 0; i < complexSineOneChannel.Length; i++)
                        {
                            // 归一化到30000（不用32767是为了避免运算溢出风险，因正弦波加入噪声之后最大幅度会超过1）。
                            shortSineAllChannels[(i * numOfChannels + channelIndex) * 2] = (short)(complexSineOneChannel[i].Real * 30000);
                            shortSineAllChannels[(i * numOfChannels + channelIndex) * 2 + 1] = (short)(complexSineOneChannel[i].Imaginary * 30000);
                        }
                    }

                    #endregion

                    // 写入文件。
                    vectorFile.Write(shortSineAllChannels);

                    // 更新进度，并检查用户是否取消了操作。
                    bgWorker.ReportProgress((int)((indexOfFrame + 1) / (float)numOfFrames * 100));
                    if (bgWorker.CancellationPending == true) { e.Cancel = true; break; }
                }
            }
            finally
            {
                // 总是关闭文件。
                vectorFile?.Close();
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _guiProgressBar.Value = e.ProgressPercentage;
            _guiProgressValue.Text = string.Format("{0} %", e.ProgressPercentage);
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            #region------------------------- 显示BackgroundWorker的运行结果。 -------------------------

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

            #endregion

            // 任务已经结束，设置GUI控件状态。
            _guiSignalParam.Enabled = true;
            _guiFileParam.Enabled = true;
            _guiStart.Enabled = true;
            _guiStop.Enabled = false;

        }

        #endregion

    }
}
