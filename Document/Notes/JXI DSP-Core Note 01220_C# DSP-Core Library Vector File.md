# C# DSP-Core Library Vector File

Feb-9-2022

# 功能

* 位置：Vector File Library
* 方法
  * Open
  * Seek
  * Read （6种重载）
  * Write （5种重载）
  * Close
* 类库公开方法

``` csharp
/// 打开或创建文件。
/// <param name="filePath"></param>
/// <param name="mode"></param>
/// <param name="fileAccess"></param>
/// <param name="disableBuffering">Specifies whether the file will read/write without buffering. 
/// Disable buffering willl speed up data transfers but require read/write block size to be integer multiple of disk sector size.</param>   
public virtual void Open(string filePath, FileMode mode, FileAccess fileAccess, bool disableBuffering = false)

/// Set the current read-write location of the file to a given value.     
/// <param name="offset">The number of frames relative to origin. 。</param>
/// <param name="origin">Using a value of type System.IO.SeekOrigin, the start, end, or current position is specified as a reference point for offset.</param>
/// <returns>The new location of the file read and write, the number of frames away from the starting point of the data, that is, the value of the attribute "Position". 。</returns>
public long Seek(long offset, SeekOrigin origin)

/// Write data information to file header, could be called after writing data.
/// After the operation, file pointer will be set to the beginning of data (value of property "Position" is 0 after operation).
/// In creating new file use case, it must be called once before writing first block data.      
public virtual void WriteFileHeader()

/// 写入I8类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public virtual void Write(sbyte[] data)

/// 写入I16类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public virtual void Write(short[] data)

/// 写入Float32类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public virtual void Write(float[] data)

/// 写入double64类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public virtual void Write(double[] data)

///  以IntPtr写入数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
/// <param name="lengthInBytes">数据长度，字节数。</param>
public virtual void Write(IntPtr data, int lengthInBytes)

/// 读出I8类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public virtual void Read(sbyte[] data)

/// 读出I16类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public virtual void Read(short[] data)

/// 读出Float32类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public virtual void Read(float[] data)

/// 读出Double64类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
public virtual void Read(double[] data)

///  以IntPtr读出数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
/// <param name="data"></param>
/// <param name="lengthInBytes">数据长度，字节数。</param>
public virtual void Read(IntPtr data, int lengthInBytes)

/// 关闭当前文件并释放与之关联的所有资源（如文件句柄）。
public void Close()
```



# Example 范例

## FixFreqFrame Read Complex

### 界面

![image-20220211001051909](C:\Users\alexs\AppData\Roaming\Typora\typora-user-images\image-20220211001051909.png)

### 核心代码

```csharp
/// 在文件的当前位置读取IQ数据，计算频谱并显示。
private void ReadDataAndDisplay()
{
    // 在当前位置读取IQ数据，若“当前位置 + 读取长度”已超出文件长度，则先将“当前读取位置”向前移动。
    if (_iqFrameFile.Position + 1 > _iqFrameFile.NumberOfFrames) { _iqFrameFile.Seek(-1, SeekOrigin.End); }

    // 读取数据。
    _iqFrameFile.Read(_shortIQAllChannels);
}
```



## FixFreqFrame Write Complex

### 界面

![image-20220213163911100](C:\Users\alexs\AppData\Roaming\Typora\typora-user-images\image-20220213163911100.png)

### 核心代码

```C#
// 实例化FixFrameFile对象，创建文件。
    vectorFile = new FixFrequencyFrameFile();
    vectorFile.Open(filePath, FileMode.Create, FileAccess.Write, false);

// 写入数据。
    for (int indexOfFrame = 0; indexOfFrame < numOfFrames; indexOfFrame++)
    {
        // 写入文件。
        vectorFile.Write(shortSineAllChannels);

        // 更新进度，并检查用户是否取消了操作。
        bgWorker.ReportProgress((int)((indexOfFrame + 1) / (float)numOfFrames * 100));
        if (bgWorker.CancellationPending == true) { e.Cancel = true; break; }
    }
finally
{
    // 总是关闭文件。
    vectorFile?.Close();
}
```



## FixFreqStream Read Complex

### 界面

![image-20220213164041622](C:\Users\alexs\AppData\Roaming\Typora\typora-user-images\image-20220213164041622.png)

### 核心代码

```csharp
/// 在文件的当前位置读取IQ数据，计算频谱并显示。
private void ReadDataAndDisplay()
{
    // 在当前位置读取IQ数据，若“当前位置 + 读取长度”已超出文件长度，则先将“当前读取位置”向前移动。
    if (_iqStreamFile.Position + NumOfSamplesPerRead > _iqStreamFile.NumberOfSamples) { _iqStreamFile.Seek(-NumOfSamplesPerRead, SeekOrigin.End); }

    // 读取数据。
    _iqStreamFile.Read(_shortIQAllChannels);

    // 获取数据采样信息。
    int numOfChannels = _iqStreamFile.Storage.NumberOfChannels;
    double sampleRate = _iqStreamFile.Sampling.SampleRate;
    // 获取信号带宽，用于计算频谱。若文件头中未保存带宽信息，则使用0.8倍采样率作为分析带宽。
    double bandwidth = _iqStreamFile.Sampling.Channels[0].Bandwidth;
    if(bandwidth <= 0) { bandwidth = sampleRate * 0.8; }

    // 将读取的数据按照各通道依次计算频谱。
    for (int channelIndex = 0; channelIndex < numOfChannels; channelIndex++)
    {
        // 获取当前通道数据的电压换算因子。
        double scaleFactor = _iqStreamFile.Sampling.Channels[channelIndex].GetScaleFactor();

        // 将当前通道的I16数据转换为Complex。
        int indexInRawArray;
        for(int i = 0; i < NumOfSamplesPerRead; i++)
        {
            // 计算当前sample在通道交织（Channel-Interleaved）数组中的位置。
            indexInRawArray = (i * numOfChannels + channelIndex) * 2;
            _complexIQOneChannel[i] = new Complex(_shortIQAllChannels[indexInRawArray] * scaleFactor, _shortIQAllChannels[indexInRawArray + 1] * scaleFactor);
        }

        // 计算频谱。简化实现：调用EasyPoweSpectrum。
        EasySpectrum.AutoPowerSpectrum(_complexIQOneChannel, sampleRate, bandwidth, _spectrumOneChannel);

        // 将当前通道的频谱填入多通道频谱的2维数组。
        ArrayManipulation.ReplaceArraySubset(_spectrumOneChannel, ref _spectrumAllChannels, channelIndex);
    }

    // 计算f0/df，并显示频谱。
    _guiSpectrumChart.Plot(_spectrumAllChannels, -bandwidth / 2, bandwidth / (NumOfSpectralLines - 1));

}
```



## FixFreqStream Write Complex

### 界面

![image-20220213164125354](C:\Users\alexs\AppData\Roaming\Typora\typora-user-images\image-20220213164125354.png)

### 核心代码

```csharp
// 获取BackgroudWorker对象，用于运行控制。
var bgWorker = sender as BackgroundWorker;

FixFrequencyStreamFile vectorFile = null;

try
{
    // 从GUI控件获取信号生成参数。
    int numOfChannels = (int)_guiNumberOfChannels.Value;
    double sampleRate = (double)_guiSampleRate.Value * 1E6;
    double level = (double)_guiLevel.Value;

    // 从GUI控件获取文件参数。
    string destinationFolder = _guiDestinationFolder.Text;
    double fileLengh = (double)_guiFileLength.Value;
    bool isDisableBuffering = _guiDisableBuffering.Checked;

    #region------------------------- 根据文件参数生成文件名，并计算要写入的数据次数。 -------------------------

    // 若存储目录目录不存在，则创建之，并自动生成文件名。
    if (!Directory.Exists(destinationFolder)) { Directory.CreateDirectory(destinationFolder); }
    string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_");
    fileName += string.Format("{0} Channel_Sample Rate {1} MHz.IQ", numOfChannels, (sampleRate / 1E6).ToString("f3").TrimEnd('0').TrimEnd('.'));
    string filePath = Path.Combine(destinationFolder, fileName);

    // 根据要写入文件的数据总长度和每次写入的块大小，计算总写入次数。简化实现：每次总是写入4 M个Sample。                
    int blockSizeInSamples = 0x400000;
    int numOfBlocks = (int)((sampleRate * fileLengh) / blockSizeInSamples);
    // 简化实现：总是多写入一个Block，以解决数据总长度不足一个Block，或者因数据总长度不能被Block大小整除应多写入一个Block的问题。
    numOfBlocks++;

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
    var complexSineOneChannel = new Complex[blockSizeInSamples];
    // 在写入文件时，所有通道的数据应合并组成交织格式的数组（Channel Interleaved）写入，故须分配空间用于存放多通道合并的数据。
    var shortSineAllChannels = new short[blockSizeInSamples * 2 * numOfChannels];

    #endregion

    // 实例化FixStreamFile对象，创建文件。
    vectorFile = new FixFrequencyStreamFile();
    vectorFile.Open(filePath, FileMode.Create, FileAccess.Write, isDisableBuffering);

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
    for (int blockIndex = 0; blockIndex < numOfBlocks; blockIndex++)
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
        bgWorker.ReportProgress((int)((blockIndex + 1) / (float)numOfBlocks * 100));
        if (bgWorker.CancellationPending == true) { e.Cancel = true; break; }
    }
}
finally
{
    // 总是关闭文件。
    vectorFile?.Close();
}
```



## INI File Read

### 界面

![image-20220222002556896](Resources\Note 01220\iniFileRead_example.png)

### 核心代码

* 读取全部键值

```csharp
if (_iniFile == null)
{
    if (_guiFileBrowseDialog.ShowDialog() == DialogResult.Cancel) { return; }

    // 获取文件名。
    _guiFilePath.Text = _guiFileBrowseDialog.FileName;
    // 实例化INI文件的对象。
    _iniFile = new IniFileHandler(_guiFilePath.Text);
}
// 获取section
string[] sectionName = _iniFile.GetSectionNames();
_iniPair = new List<KeyValuePair<string, string>>[sectionName.Length];
// 获取文件中的全部信息。
for (int i = 0; i < sectionName.Length; i++)
{
    _iniPair[i] = _iniFile.GetSectionAsListOfPairs(sectionName[i]);
    // 获取最大行数
    if (rowsMax < _iniPair[i].Count) { rowsMax = _iniPair[i].Count; }
}

// 先清空显示控件。
_guiDataGridView.Rows.Clear();
// 设置显示控件的行数和列数 。           
_guiDataGridView.ColumnCount = 2 * sectionName.Length;
_guiDataGridView.Rows.Add(rowsMax + 1);
// 显示全部信息          
for (int i = 0; i < _iniPair.Length; i++)
{
    // 由于各个section的长度不同，因此禁止重新排列
    _guiDataGridView.Columns[i * 2].SortMode = DataGridViewColumnSortMode.NotSortable;
    _guiDataGridView.Columns[i * 2 + 1].SortMode = DataGridViewColumnSortMode.NotSortable;
    _guiDataGridView.Columns[i * 2].HeaderText = sectionName[i];
    _guiDataGridView.Rows[0].Cells[i].Value = "Key";
    _guiDataGridView.Rows[0].Cells[i + 1].Value = "Value";
    for (int j = 0; j < _iniPair[i].Count; j++)
    {
        _guiDataGridView.Rows[j + 1].Cells[i * 2].Value = _iniPair[i][j].Key;
        _guiDataGridView.Rows[j + 1].Cells[i * 2 + 1].Value = _iniPair[i][j].Value;
    }
}
_guiDataGridView.Font = new Font("Cambria", 12, FontStyle.Regular);
_guiDataGridView.Refresh();
```

* 添加键值

```C#
_iniFile.WriteKey(_guiSection.Text, _guiKey.Text, _guiValue.Text);
```

* 删除键值

```C#
int sectionIndex = _guiDataGridView.CurrentCell.ColumnIndex / 2;
int keyIndex = _guiDataGridView.CurrentCell.RowIndex - 1;
if (keyIndex > _iniPair[sectionIndex].Count) { return; }
// 删除key
_iniFile.DeleteKey(_iniFile.GetSectionNames()[sectionIndex], _iniPair[sectionIndex][keyIndex].Key);
```



## INI File Write

### 界面

* 此范例为控制台程序，运行后将自动生成.ini文件至可执行文件目录。

### 核心代码

```C#
// 指定路径
string filePath = Environment.CurrentDirectory + "\\" + "Configure.ini";

// 实例化写INI文件的对象
IniFileHandler _iniFile = new IniFileHandler(filePath);

// 实例化一个类，用来写入到ini文件中
BaseSamplingInformation sampling = new BaseSamplingInformation();

// 在sampling中写入数据
sampling.SampleRate = 25000;
sampling.Channels.Add(new BaseChannelSamplingInfo());
sampling.Channels[0].Bandwidth = 1000;
sampling.Channels[0].IFFrequency = 156;
sampling.Channels[0].ReferenceLevel = -30;
sampling.Channels[0].RFScaleFactor = 1;
sampling.Channels[0].RFFrequency = 25600000;
sampling.Channels[0].DigitizerScaleFactor = 1.5;

// 实例化写入INI文件的对象。
StorageInformation storage = new StorageInformation();

// 写入数据
storage.DataType = DataType.RealI16;
storage.FileFormat = FileFormat.FrequencyScanIQ;
storage.NumberOfChannels = 1;
storage.ByteOrder = ByteOrder.LittleEndian;

try
{
    // 将数据写入文件中
    string section1 = "Sampling Information";
    _iniFile.WriteKey(section1, "SampleRate", sampling.SampleRate, "f2");
    _iniFile.WriteKey(section1, "NumberOfChannel", sampling.Channels.Count);
    _iniFile.WriteKey(section1, "Bandwidth", sampling.Channels[0].Bandwidth, "f2");
    _iniFile.WriteKey(section1, "IFFrequency", sampling.Channels[0].IFFrequency, "f2");
    _iniFile.WriteKey(section1, "ReferenceLevel", sampling.Channels[0].ReferenceLevel, "f2");
    _iniFile.WriteKey(section1, "RFScaleFactor", sampling.Channels[0].RFScaleFactor, "f2");
    _iniFile.WriteKey(section1, "RFFrequency", sampling.Channels[0].RFFrequency, "f2");
    _iniFile.WriteKey(section1, "DigitizerScaleFactor", sampling.Channels[0].DigitizerScaleFactor, "f2");

    string section2 = "Storage InFormation";
    _iniFile.WriteKey(section2, "DataType", storage.DataType.ToString());
    _iniFile.WriteKey(section2, "FileFormat", storage.FileFormat.ToString());
    _iniFile.WriteKey(section2, "NumberOfChannels", storage.NumberOfChannels);
    _iniFile.WriteKey(section2, "ByteOrder", storage.ByteOrder.ToString());
    _iniFile.WriteKey(section2, "FileHeaderSize", storage.FileHeaderSize);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine("写入INI文件完成！");
Console.ReadKey();
```

### 输出.ini文件

[Sampling Information]
SampleRate = 25000.00
NumberOfChannel = 1
Bandwidth = 1000.00
IFFrequency = 156.00
ReferenceLevel = -30.00
RFScaleFactor = 1.00
RFFrequency = 25600000.00
DigitizerScaleFactor = 1.50

[Storage InFormation]
DataType = RealI16
FileFormat = FrequencyScanIQ
NumberOfChannels = 1
ByteOrder = LittleEndian
FileHeaderSize = 65536



## Simple Vector File Data Viewer

### 界面

![image-20220213164308266](C:\Users\alexs\AppData\Roaming\Typora\typora-user-images\image-20220213164308266.png)

### 核心代码

```csharp
// Read raw data from file.
    short[] rawData = new short[numberOfRawSamplesToRead * arrayElementsPerSample];
    _vectorFile.Seek(_currentSelectionStart, SeekOrigin.Begin);
    _vectorFile.Read(rawData);
```



## Spectrum Read

### 界面

![image-20220213164340207](C:\Users\alexs\AppData\Roaming\Typora\typora-user-images\image-20220213164340207.png)

### 核心代码

```csharp
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
    ...
}
```



## Spectrum Write

### 界面

![image-20220213164424133](C:\Users\alexs\AppData\Roaming\Typora\typora-user-images\image-20220213164424133.png)

### 核心代码

```csharp
// 实例化SpectrumFile对象，创建文件。
    SpectrumFile spectrumFile = new SpectrumFile();
    spectrumFile.Open(filePath, FileMode.Create, FileAccess.Write);
    switch (_dataType)

// 写入数据。
if (spectrumFile.Storage.DataType == DataType.RealD64)
{
    //写入Double数据
    spectrumFile.Write(_spectrumDouble);
}
else if (spectrumFile.Storage.DataType == DataType.RealF32)
{
    //写入Float数据                   
    spectrumFile.Write(_spectrumFloat);
}

```

