# C# DSP-Core Library IQ File

Feb-20-2022

# 功能

* 位置：File IO\IQ File
* 分类
  * IQ File Library //类库源代码
  * IQ File Read Example //读文件测试面板（范例）
* 类库公开方法

```c#
/// <summary>
/// 读取bin+json, Wav或者iq文件
/// 其中bin为I16数据IQ交织，json包含采样信息关键字(格式见读取方法之源代码注释)
/// </summary>
/// <param name="fileName">文件路径</param>
/// <param name="IQInfo">采样信息</param>
/// <returns></returns>
public static Complex[] ReadIQFile(string fileName, ref IQFileInfo IQInfo)

/// <summary>
/// 读取IQ矢量文件
/// </summary>
/// <param name="fileName">文件路径</param>
/// <param name="IQInfo">采样信息</param>
/// <returns>IQ波形</returns>
public static Complex[] ReadVectorFile(string fileName, ref IQFileInfo IQInfo)

/// <summary>
/// 限长度，读取IQ矢量文件
/// </summary>
/// <param name="fileName">文件路径</param>
/// <param name="IQInfo">采样信息</param>
/// <param name="lengthLimit">长度限制, 不大于零时缺省为1e6</param>
/// <returns>IQ波形</returns>
public static Complex[] ReadVectorFile(string fileName, ref IQFileInfo IQInfo, int lengthLimit)

/// <summary>
/// 读取I16交替IQ文件bin及其信号说明json文件
/// </summary>
/// <param name="fileName">bin文件名</param>
/// <param name="IQInfo">json文件包含的采样信息</param>
/// <returns></returns>
public static Complex[] ReadI16IQFile(string fileName, ref IQFileInfo IQInfo)

/// <summary>
/// 读取WAV格式IQ数据，要求Wav文件为双通道I16模式
/// 强制归一化到0.1
/// 采样率 = Wav采样率
/// 中心频率默认1GHz，如果文件名包含"_*Hz_"模式，中心频率=*  (兼容SDR软件输出)
/// 限制长度3M samples
/// </summary>
/// <param name="fileName"></param>
/// <param name="IQInfo"></param>
/// <returns></returns>
public static Complex[] ReadWavFile(string fileName, ref IQFileInfo IQInfo)

/// <summary>
/// 写入矢量文件
/// </summary>
/// <param name="fileName">文件路径</param>
/// <param name="iqWav">IQ波形</param>
/// <param name="IQInfo">采集信息</param>
public static void WriteVectorFile(string fileName, Complex[] iqWav, IQFileInfo IQInfo)

/// <summary>
/// 浮点波形转换成I16数组(short[])，将峰值归一化到一个较大的I16数值
/// </summary>
/// <param name="data">原始浮点波形，如多通道，则通道交织</param>
/// <param name="I16Range">归一化目标峰值, 不大于0：缺省30000</param>
public static short[] ConvertDoubleArrayToShort(double[] data, double I16Range)

/// <summary>
/// 浮点波形转换成I16，自动将峰值归一化到缺省的I16数值
/// </summary>
/// <param name="data"></param>
public static short[] ConvertDoubleArrayToShort(double[] data)
```



# 读IQ文件测试面板

### 界面

![example_interface](Resources\Note 01230\interface_example.png)

### 核心代码

* 打开文件并读取

```c#
// Show dialog for user to select IQ file.
OpenFileDialog fileDialog = new OpenFileDialog();
fileDialog.Multiselect = false;
fileDialog.RestoreDirectory = false;
fileDialog.Filter = "Vector Files (*.iq)|*.iq|Bin Files(with Json) (*.bin)|*.bin|Wav Files (*.wav)|*.wav";
fileDialog.Title = "Select IQ file to open";
if (fileDialog.ShowDialog() == DialogResult.Cancel) { return; }

Complex[] iqData;
IQFileInfo iqInfo= new IQFileInfo();

iqData = IQFile.ReadIQFile(fileDialog.FileName, ref iqInfo);

```

* 显示不超过4000个样本的IQ波形

```C#
int displayLength = Math.Min(iqData.Length, 4000);
double[,] displayData = new double[2, displayLength];
for (int i = 0; i < displayLength; i++)
{
    displayData[0, i] = iqData[i].Real;
    displayData[1, i] = iqData[i].Imaginary;
}
easyChartXIQPlot.Plot(displayData);


textBoxFileInfo.Text = fileDialog.FileName + Environment.NewLine
    + "Center Frequency: " + iqInfo.Signal.CenterFrequency.ToString("G3")
    + "  Sample Rate: " + iqInfo.Signal.SampleRate.ToString("N2") + Environment.NewLine
    + "Samples Read: " + iqData.Length.ToString("N0")
    + "  Samples Plotted: " + displayLength.ToString("N0");
```

