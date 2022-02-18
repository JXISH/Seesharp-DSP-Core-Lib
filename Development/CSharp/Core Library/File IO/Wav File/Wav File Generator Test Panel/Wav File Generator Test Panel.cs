using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JXI.FileIO.WavFile;

namespace WavFileGeneratorTestPanel
{
    public partial class MainForm : Form
    {

        #region---------------------------常量----------------------------
        //每次生成的波形长度，考虑到范例简洁，生成频率是这个长度倒数的整数倍，保证整周期复制
        private static readonly double StreamDataGenInterval = 0.2;

        private static readonly double FullScaleI16Amplitude = 32767;
        #endregion

        #region --------------------------私有字段------------------------

        private double _sampleRate;
        private ushort _numOfChannels;
   
        private string _filePath;
        private short[] _audioData;

        private short[][] _pendingAudioWaveform;        

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
            _guiChannelSelection.SelectedIndex = 0;
            _guiChannelOneFrequency.Enabled = true;
            _guiChannelTwoFrequency.Enabled = false;
            _guiAudioSampleRate.SelectedIndex = 7;
            _numOfChannels = 1;
        }

        private void _guiSelectFolder_Click(object sender, EventArgs e)
        {
            if (_guiRecFolderDialog.ShowDialog() == DialogResult.OK)
            {
                _guiRecorderFolder.Text = _guiRecFolderDialog.SelectedPath;
                _filePath = _guiRecorderFolder.Text;
            }
        }

        private void _guiChannelSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_guiChannelSelection.SelectedIndex == 0)
            {
                _guiChannelOneFrequency.Enabled = true;
                _guiChannelTwoFrequency.Enabled = false;
            }

            if (_guiChannelSelection.SelectedIndex == 1)
            {
                _guiChannelOneFrequency.Enabled = true;
                _guiChannelTwoFrequency.Enabled = true;
            }
        }

        private void GuiGenerate_Click(object sender, EventArgs e)
        {
            _numOfChannels = Convert.ToUInt16(_guiChannelSelection.Text);
            _sampleRate = Convert.ToDouble(_guiAudioSampleRate.Text);
            string folderPath = _guiRecorderFolder.Text;
            if (!Directory.Exists(folderPath)) { Directory.CreateDirectory(folderPath); }
            _filePath = folderPath + "\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".wav";

            _wavFileStream = new WavFileStream(_filePath);
            _wavFileStream.NumberOfChannels = _numOfChannels;
            _wavFileStream.SampleRate = _sampleRate;
            _wavFileStream.BitsPerSample = 16;
            _pendingAudioWaveform = new short[_numOfChannels][];

            Random rand = new Random(); //随机数发生器，用来发生噪声
            for (int i = 0; i < _numOfChannels; i++)
            {
                // 实例化音频数据生成对象，从Receiver对象获取采样率，默认音频数据为1 kHz正弦波，噪声幅度100。
                _pendingAudioWaveform[i] = new short[(int)(_sampleRate * StreamDataGenInterval)];
                double frequency = 0;
                if (i == 0)
                {
                    frequency = Convert.ToDouble(_guiChannelOneFrequency.Value) * 1e3;
                }
                else
                {
                    frequency = Convert.ToDouble(_guiChannelTwoFrequency.Value) * 1e3;
                }
                //取整数周期
                double periodNum = Math.Max(1, Math.Round(frequency * StreamDataGenInterval));
                double amplitude = FullScaleI16Amplitude * 0.9; //90%满幅度
                double noiseLevel = amplitude * 0.001; //千分之一的噪声幅度
                double phaseStep = Math.PI * 2 * periodNum / _pendingAudioWaveform[i].Length;

                for (int j = 0; j < _pendingAudioWaveform[i].Length; j++)
                {
                    _pendingAudioWaveform[i][j] = (short) Math.Round(amplitude * Math.Sin(phaseStep * j) 
                        + noiseLevel * 2 * (rand.NextDouble() - 0.5));
                }
            }
            //循环若干次，每次一段波形，总长度 _guiFileTimeLength.Value 每次长度 StreamDataGenInterval
            int loop = (int)((double)_guiFileTimeLength.Value / StreamDataGenInterval);
            for (int i = 0; i < loop; i++)
            {
                if (_numOfChannels == 1)
                {
                    _wavFileStream.Write(_pendingAudioWaveform[0]);
                }
                if (_numOfChannels == 2)
                {
                    //双通道音频通过_audioData合并写文件
                    _audioData = new short[(int)(_sampleRate * StreamDataGenInterval) * 2];
                    int m = 0;
                    for (int j = 0; j < _pendingAudioWaveform[0].Length; j++)
                    {
                        _audioData[m] = _pendingAudioWaveform[0][j];
                        _audioData[m + 1] = _pendingAudioWaveform[1][j];
                        m += 2;
                    }
                    _wavFileStream.Write(_audioData);
                    _audioData = null;
                }
            }
            MessageBox.Show("File is Done!");
            _wavFileStream.Close();

        }
        #endregion
    }

}
