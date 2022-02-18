using System;
using System.Windows.Forms;
using SeeSharpTools.JXI.FileIO.WavFile;

namespace WavFileReaderTestPanel
{

    public partial class MainForm : Form
    {

        #region --------------------------私有字段------------------------

        private string _fileFullPath;
        private string[] _SampleProperties;
        private ushort _bitsPerSample;
        private ushort _numberOfChannels;
        private ulong _numberOfTotalSamples;
        private double _dataSampleRate;
        private WavFileStream _wavFileStream;

        #endregion

        #region --------------------------构造函数------------------------
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region --------------------------GUI事件处理函数-----------------

        private void MainForm_Load(object sender, EventArgs e)
        {
            _guiSampleInfo.Text = null;
            _guiAudioDisplaySamples.Value = 4096;
            _guiReadPosition.Value = 0;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_wavFileStream!=null)
            {
                _wavFileStream.Close();
                _wavFileStream = null;
            }

        }

        private void _guiOpenFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_wavFileStream != null)
                {
                    _wavFileStream.Close();
                    _wavFileStream = null;
                }

                // Show dialog for user to select IQ file.
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.RestoreDirectory = false;
                fileDialog.Filter = "Wav Files (*.wav)|*.wav";
                fileDialog.Title = "Select wav file to open";
                if (fileDialog.ShowDialog() == DialogResult.Cancel) { return; }

                _SampleProperties = new string[4];
                _wavFileStream = new WavFileStream(fileDialog.FileName);
                _numberOfChannels = _wavFileStream.NumberOfChannels;
                _numberOfTotalSamples = _wavFileStream.Length / _numberOfChannels;
                _dataSampleRate = _wavFileStream.SampleRate;
                _bitsPerSample = _wavFileStream.BitsPerSample;
                _fileFullPath = fileDialog.FileName;
                _guiReadPosition.Max = (int)_numberOfTotalSamples;
                _guiSampleInfo.Text = string.Format("totalSamples: {0}bitsPerSample: {1}dataSampleRate: {2}numberOfChannels: {3}", _numberOfTotalSamples.ToString() + Environment.NewLine , _bitsPerSample.ToString()+ Environment.NewLine, (_dataSampleRate / 1000).ToString()+"kHz"+Environment.NewLine,  _numberOfChannels.ToString());
   
                // Set file path and set selection to full range.
                _guiFilePath.Text = fileDialog.FileName;
                _guiReadPosition.Value = 0;
                _guiReadPosition_ValueChanged(null, 0);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void _guiReadPosition_ValueChanged(object sender, double value)
        {
            //创建缓存
            short[] data = new short[(int)_guiAudioDisplaySamples.Value * _numberOfChannels];
            var displayData = new short[_numberOfChannels, data.Length / _numberOfChannels];
            //设置读指针位置
            if (value + (ulong)_guiAudioDisplaySamples.Value > _numberOfTotalSamples)
            {
                long position = (long)_numberOfTotalSamples - (long)_guiAudioDisplaySamples.Value;
                _wavFileStream.Seek(position, System.IO.SeekOrigin.Begin);

            }
            else
            {
                _wavFileStream.Seek((long)_guiReadPosition.Value, System.IO.SeekOrigin.Begin);
            }
            //读取数据
            _wavFileStream.Read(data);
            //如果是单声道文件，直接显示
            if(_numberOfChannels==1)
            {
                _guieasyChartX.Plot(data);
            }
            //如果是双声道文件，分配到一个二维数组显示
            if(_numberOfChannels == 2)
            {
                for (int j = 0; j < data.Length / _numberOfChannels; j++)
                {
                    displayData[0, j] = data[j * 2];
                    displayData[1, j] = data[j * 2 + 1];
                }
                _guieasyChartX.Plot(displayData);
            }
        }

        #endregion
    }

}