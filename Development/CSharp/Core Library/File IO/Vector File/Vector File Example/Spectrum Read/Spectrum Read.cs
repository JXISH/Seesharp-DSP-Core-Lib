using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using SeeSharpTools.JXI.FileIO.VectorFile;
using System.Threading;

namespace SpectrumFileReadExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region------------------------------------------私有成员----------------------------------------

        private SpectrumFile _spectrumFile;

        /// <summary>
        /// 用于存放读取出来的频谱数据（多通道+多频段,Double）。
        /// </summary>
        private double[][][] _spectrumDataDouble;

        /// <summary>
        /// 用于存放读取出来的频谱数据（多通道+多频段,Float）。
        /// </summary>
        private float[][][] _spectrumDataFloat;

        /// <summary>
        /// 一次读一帧，一帧的频谱线数。
        /// </summary>
        private int _numOfLinesPerFrame;

        /// <summary>
        /// 当前是否已启动连续读取。
        /// </summary>
        private bool _isContinuousReading;

        #endregion

        #region--------------------------------------GUI事件处理-----------------------------------------

        private void MainForm_Load(object sender, EventArgs e)
        {
            //设置Stop控件默认状态。
            _guiStop.Enabled = false;
        }

        private void GuiBrowseFile_Click(object sender, EventArgs e)
        {
            //如果用户取消了操作，则直接返回。
            if (_guiFileBrowseDialog.ShowDialog() == DialogResult.Cancel) { return; }

            //关闭当前已经打开的文件。
            _spectrumFile?.Close();
            _spectrumFile = null;

            #region-------------------------------------------检查选中的文件格式。--------------------------------------

            _guiFilePath.Text = _guiFileBrowseDialog.FileName;
            //以Vector File基类打开文件，检查文件格式。
            var vectorFile = new VectorFile();
            try
            {
                vectorFile.Open(_guiFilePath.Text, FileMode.Open, FileAccess.Read);
                if (vectorFile.Storage.FileFormat != FileFormat.Spectrum)
                {
                    throw new VectorFileException(ExceptionEnum.DataLengthConflict, "文件格式不是Spectrum。");
                }
            }
            catch (Exception exception)
            {
                //文件格式异常，提示用户并直接返回。
                MessageBox.Show(exception.Message + Environment.NewLine + exception.StackTrace);
                _guiReadControlSetting.Enabled = false;
                return;
            }
            finally
            {
                //总是关闭文件。
                vectorFile?.Close();
            }

            #endregion

            _spectrumFile = new SpectrumFile();
            try
            {
                #region-------------------------------------------显示频谱数据信息。----------------------------------------

                //打开文件，获取数据信息：通道数，频带数以及各个频带的频谱信息。
                _spectrumFile.Open(_guiFilePath.Text, FileMode.Open, FileAccess.Read);
                int numOfChannels = _spectrumFile.Storage.NumberOfChannels;
                int numOfBands = _spectrumFile.Sampling.Bands.Count;

                _guiFileInfoView.Rows.Clear();
                _guiFileInfoView.Rows.Add(12);
                _guiFileInfoView.Rows[0].Cells[0].Value = "Channel"; _guiFileInfoView.Rows[0].Cells[1].Value = numOfChannels;
                _guiFileInfoView.Rows[1].Cells[0].Value = "Band"; _guiFileInfoView.Rows[1].Cells[1].Value = numOfBands;
                _guiFileInfoView.Rows[2].Cells[0].Value = "数据起始时间"; _guiFileInfoView.Rows[2].Cells[1].Value = _spectrumFile.Archive.DateTime.ToString("yyy-MM-dd HH:mm:ss:");
                _guiFileInfoView.Rows[3].Cells[0].Value = "存储时间间隔";_guiFileInfoView.Rows[3].Cells[1].Value = _spectrumFile.Sampling.Interval.ToString();

                int indexOfCurrentRow = 4;
                for (int i = 0; i < numOfBands; i++)
                {
                    _guiFileInfoView.Rows[indexOfCurrentRow].Cells[0].Value = "FStart";
                    _guiFileInfoView.Rows[indexOfCurrentRow].Cells[1].Value = _spectrumFile.Sampling.Bands[i].FrequencyStart;
                    indexOfCurrentRow++;
                    _guiFileInfoView.Rows[indexOfCurrentRow].Cells[0].Value = "FStop";
                    _guiFileInfoView.Rows[indexOfCurrentRow].Cells[1].Value = _spectrumFile.Sampling.Bands[i].FrequencyStop;
                    indexOfCurrentRow++;
                    _guiFileInfoView.Rows[indexOfCurrentRow].Cells[0].Value = "FStep";
                    _guiFileInfoView.Rows[indexOfCurrentRow].Cells[1].Value = _spectrumFile.Sampling.Bands[i].FrequencyStep;
                    indexOfCurrentRow++;
                    _guiFileInfoView.Rows[indexOfCurrentRow].Cells[0].Value = "Lines";
                    _guiFileInfoView.Rows[indexOfCurrentRow].Cells[1].Value = _spectrumFile.Sampling.Bands[i].NumOfSpectralLines;
                    indexOfCurrentRow++;
                }

                #endregion

                #region--------------------------------------分配数据空间，用于存储每次读取的数据。--------------------------

                _numOfLinesPerFrame = 0;

                //Double数据。
                if (vectorFile.Storage.DataType == DataType.RealD64)
                {
                    _spectrumDataDouble = new double[numOfChannels][][];
                    for (int i = 0; i < numOfChannels; i++)
                    {
                        _spectrumDataDouble[i] = new double[numOfBands][];
                        for (int j = 0; j < numOfBands; j++)
                        {
                            _spectrumDataDouble[i][j] = new double[_spectrumFile.Sampling.Bands[j].NumOfSpectralLines];
                            _numOfLinesPerFrame += _spectrumFile.Sampling.Bands[j].NumOfSpectralLines;
                        }
                    }
                }

                //Float数据。
                else if (vectorFile.Storage.DataType == DataType.RealF32)
                {
                    _spectrumDataFloat = new float[numOfChannels][][];
                    for (int i = 0; i < numOfChannels; i++)
                    {
                        _spectrumDataFloat[i] = new float[numOfBands][];
                        for (int j = 0; j < numOfBands; j++)
                        {
                            _spectrumDataFloat[i][j] = new float[_spectrumFile.Sampling.Bands[j].NumOfSpectralLines];
                            _numOfLinesPerFrame += _spectrumFile.Sampling.Bands[j].NumOfSpectralLines;
                        }
                    }
                }
                else { throw new VectorFileException(ExceptionEnum.DataLengthConflict, "文件数据不是Double或Float类型。"); }

                //每次从文件中读取一帧数据，若文件长度（线数）不足以读取一次，则直接抛出异常。
                if (_spectrumFile.NumberOfFrames * _spectrumFile.Storage.NumberOfChannels < _numOfLinesPerFrame)
                {
                    throw new VectorFileException(ExceptionEnum.InvalidFile, "文件包含的数点数太少。");
                }
                #endregion

                #region--------------------------------------根据数据总长度设置TrackBar控件属性。----------------------------

                //最大值为整个文件包含的总帧数*1000。
                _guiReadPositionBar.Maximum = ((int)_spectrumFile.NumberOfFrames * numOfChannels / _numOfLinesPerFrame - 1) * 1000;

                //滚动框长距离移动时，每次增加一帧数据，即多通道多频段的线数。
                _guiReadPositionBar.LargeChange = 1000;

                //滚动框短距离移动时，每次增加1。
                _guiReadPositionBar.SmallChange = 1;

                //显示文件中频谱线数。
                _guiReadPosValue.Text = "1";

                #endregion

                if (_guiReadPositionBar.Value != 0) { _guiReadPositionBar.Value = 0; }
                else { ReadDataAndDisplay(); }

                // 读取数据成功，允许用户的后续操作。
                _guiReadControlSetting.Enabled = true;
            }

            catch (Exception exception)
            {
                //出现异常，提示用户，关闭文件并禁止后续操作。
                MessageBox.Show(exception.Message + Environment.NewLine + exception.StackTrace);
                _spectrumFile?.Close();
                _spectrumFile = null;
                _guiReadControlSetting.Enabled = false;
            }
        }

        private void GuiReadPositionBar_ValueChanged(object sender, EventArgs e)
        {
            //如果当前已启动连续读取，则TrackerBar的控件值改变是因BackgroundWorker更新显示而触发，无需任何处理，直接返回。
            if (_isContinuousReading) { return; }

            // 根据TrackBar控件的当前值（相对于数据起始的线数），计算读取位置并显示。
            long readPosition = _guiReadPositionBar.Value / 1000;
            _guiReadPosValue.Text = Convert.ToString(readPosition + 1);

            // 设置读取位置，读取数据并显示。
            _spectrumFile.Seek(readPosition, SeekOrigin.Begin);
            ReadDataAndDisplay();
        }

        private void GuiStart_Click(object sender, EventArgs e)
        {
            // 启动BackgroundWorker。
            _bgWorker.RunWorkerAsync();

            // 设置标志位。
            _isContinuousReading = true;

            // 设置控件状态。简化实现：开启连续读取之后，不允许拖动TrackerBar。
            _guiReadPositionBar.Enabled = false;
            _guiFileSelectionParam.Enabled = false;
            _guiStart.Enabled = false;
            _guiStop.Enabled = true;
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
                MessageBox.Show("当前正在连续读取，请先停止读取任务。");
                e.Cancel = true;
            }
            else
            {
                // 可以正常关闭窗口，先关闭已经打开的文件。
                _spectrumFile?.Close();
            }
        }

        #endregion

        #region---------------------------BackGroundWorker实现文件连续读取---------------------------------

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //获取BackgroundWorker对象，用于运行控制。
            var byWorker = sender as BackgroundWorker;
            while (true)
            {
                //从文件当前位置开始，持续读取数据并显示，直至文件末尾。
                this.Invoke(new Action(() => { ReadDataAndDisplay(); }));

                //向GUI更新进度，不使用ProgressPercentage，而是发送当前读取位置相当于文件起始的线数。
                byWorker.ReportProgress(0, (int)(_spectrumFile.Position - 1));

                //若已到文件末尾，或用户取消了操作，则退出循环。
                if (_spectrumFile.Position + 1 > _spectrumFile.NumberOfFrames * _spectrumFile.Storage.NumberOfChannels / _numOfLinesPerFrame) { break; }
                if (byWorker.CancellationPending) { e.Cancel = true; break; }

                Thread.Sleep(10);
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //trackBar总长为帧数*1000，连续读取时，trackBar实时位置为当前帧数*1000。
            _guiReadPositionBar.Value = (int)e.UserState * 1000;
            _guiReadPosValue.Text = Convert.ToString(_guiReadPositionBar.Value / 1000 + 1);
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region-----------------------------------显示BackgroundWorker的运行结果。-------------------------------------、

            if (e.Error != null)
            {
                //BackgroundWorker的运行中出现异常，则提示用户。
                MessageBox.Show(e.Error.Message + Environment.NewLine + e.Error.StackTrace);
            }
            else
            {
                if (!e.Cancelled)
                {
                    // BackgroundWorker未发生异常，用户也未取消，说明任务已正常结束。
                    MessageBox.Show("已到到文件末尾");
                }
                else
                {
                    //用户主动取消了任务，不作任何操作。
                }
            }
            #endregion

            // 任务已经结束，设置GUI控件状态。
            _guiFileSelectionParam.Enabled = true;
            _guiReadPositionBar.Enabled = true;
            _guiStart.Enabled = true;
            _guiStop.Enabled = false;

            // 设置标志位。
            _isContinuousReading = false;
        }
        
        #endregion

        #region---------------------------------------私有方法---------------------------------------------

        /// <summary>
        /// 在文件的当前位置读取IQ数据并显示。
        /// </summary>
        private void ReadDataAndDisplay()
        {
            // 在文件的当前位置读取IQ数据，若“当前位置 + 读取长度”已超出文件总的帧数，则先将“当前读取位置”向前移动1帧。            
            if (_spectrumFile.Position + 1 > _spectrumFile.NumberOfFrames*_spectrumFile.Storage.NumberOfChannels /_numOfLinesPerFrame)
            {
                _spectrumFile.Seek(-1, SeekOrigin.End);
            }

            //读取数据。    
            if (_spectrumFile.Storage.DataType == DataType.RealD64) { _spectrumFile.Read(_spectrumDataDouble); }
            else { _spectrumFile.Read(_spectrumDataFloat); }

            //获取数据信息。
            int numOfChannels = _spectrumFile.Storage.NumberOfChannels;
            int numOfBands = _spectrumFile.Sampling.Bands.Count;                      

            double[][] xData = new double[numOfChannels][];
            double[][] yData = new double[numOfChannels][];
            for (int i = 0; i < numOfChannels; i++)
            {
                for (int j = 0; j < numOfBands; j++)
                {
                    xData[i] = new double[_numOfLinesPerFrame / numOfChannels];
                    yData[i] = new double[_numOfLinesPerFrame / numOfChannels];
                }
            }

            //合并多个频段的数据。
            for (int channelIndex = 0; channelIndex < numOfChannels; channelIndex++)
            {
                int startIndexOfCurrentBand = 0;
                for (int bandIndex = 0; bandIndex < numOfBands; bandIndex++)
                {
                    double f0 = _spectrumFile.Sampling.Bands[bandIndex].FrequencyStart;
                    double df = _spectrumFile.Sampling.Bands[bandIndex].FrequencyStep;
                    //生成x轴数据。
                    for (int i = 0; i < _spectrumFile.Sampling.Bands[bandIndex].NumOfSpectralLines; i++)
                    {
                        xData[channelIndex][startIndexOfCurrentBand + i] = (f0 + df * i) / 1E6;
                    }

                    //y轴数据从_spectrum中拷贝。
                    if (_spectrumFile.Storage.DataType == DataType.RealD64)
                    {
                        //Double数据
                        Array.Copy(_spectrumDataDouble[channelIndex][bandIndex], 0, yData[channelIndex], 
                            startIndexOfCurrentBand, _spectrumFile.Sampling.Bands[bandIndex].NumOfSpectralLines);
                    }
                    else
                    {
                        //Float数据
                        Array.Copy(_spectrumDataFloat[channelIndex][bandIndex], 0, yData[channelIndex], 
                            startIndexOfCurrentBand, _spectrumFile.Sampling.Bands[bandIndex].NumOfSpectralLines);
                    }

                    //继续合并下一频段。
                    startIndexOfCurrentBand += _spectrumFile.Sampling.Bands[bandIndex].NumOfSpectralLines;
                }               
            }

            //显示频谱。
            _guiSpectrumChart.Plot(xData, yData);
        }
        #endregion     
    }
}
