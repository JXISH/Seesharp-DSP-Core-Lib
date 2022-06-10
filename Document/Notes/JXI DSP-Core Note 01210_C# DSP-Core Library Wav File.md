# C# DSP-Core Library Wav File

Feb-8-2022

# 功能

* 位置：File IO\Wav File

* 分类

  * Wav File Library //类库源代码
  * Wav File Generator Test Panel //写文件测试面板（范例）
  * Wav File Reader Test Panel //读文件测试面板（范例）

* 限制

  本类库支持16比特(short / I16)，1或多声道文件格式。

  测试面板给出1-2声道16比特文件生成和读取的范例

* 类库公开方法

```csharp
//*** 构造函数 ***
/// 新建或打开一个Wav文件。
/// <param name="filePath"></param>
/// <param name="mode"></param>
/// <param name="fileAccess"></param>
public WavFileStream(string filePath, FileMode mode = FileMode.OpenOrCreate, FileAccess fileAccess = FileAccess.ReadWrite)

/// 新建或打开一个Wav文件。
/// <param name="filePath"></param>
/// <param name="mode"></param>
/// <param name="fileAccess"></param>
/// <param name="fileShare">确定文件如何由进程共享。</param>
public WavFileStream(string filePath, FileMode mode, FileAccess fileAccess, FileShare fileShare)   

//*** 写入方法 ***
/// 写入I16类型PCM数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public void Write(short[] data)
            
/// 写入byte[]类型PCM数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public void Write(byte[] data)

//*** 读取方法 ***
/// 读出I16类型PCM数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public virtual void Read(short[] data)

//*** 定位 ***
/// 将文件的当前读写位置设置为给定值。
/// <param name="offset">相对于origin 的Sample数。</param>
/// <param name="origin">使用 System.IO.SeekOrigin 类型的值，将开始位置、结束位置或当前位置指定为 offset 的参考点。</param>
/// <returns>文件读写的新位置，距离数据起始点的Sample数，即属性"Position"的值。</returns>
public void Seek(long offset, SeekOrigin origin)

//*** 关闭 ***
/// 关闭当前文件并释放与之关联的所有资源（如文件句柄）。
public void Close()
```



# 写Wav文件测试面板

### 界面

![image-20220208223415736](.\Resources\Note 01210\WavFileGenTestPanel.PNG)

### 核心代码

* 生成模拟数据并写入文件

```csharp
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
```

### 技巧：弹出对话框选择目录

1. 编辑Winform时，从Toolbox选择FolderBrowserDialog添加到Form。本范例设置该对象名称为：_guiRecFolderDialog

   ![image-20220208224312750](.\Resources\Note 01210\Toolbox-FolderBrowserDialog.png)

2. 程序中调用_guiRecFolderDialog.ShowDialog方法，显示弹窗，获取路径

   ```csharp
   if (_guiRecFolderDialog.ShowDialog() == DialogResult.OK)
   {
       //如果用户点击目录弹窗OK，进入此条件分支，获取目录名称
       //名称属性为：_guiRecFolderDialog.SelectedPath    
   }
   ```

   

# 读Wav文件测试面板

### 界面

本面板可以读取已写入的wav文件，显示各声道波形，可在波形窗缩放浏览，可配置读取数据块长度，可拖动读取位置。界面还显示文件属性。

![image-20220208225200630](.\Resources\Note 01210\WavFileReadTestPanel.png)

### 核心代码

* 打开文件，读取属性，产生读位置修改事件

```csharp
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
```



* 读位置修改事件响应，读取一块数据

```csharp
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
```

