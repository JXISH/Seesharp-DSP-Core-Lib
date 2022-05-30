using Newtonsoft.Json;
using SeeSharpTools.JXI.FileIO.VectorFile;
using SeeSharpTools.JXI.FileIO.WavFile;
using SeeSharpTools.JXI.Numerics;
using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace SeeSharpTools.JXI.FileIO.IQFile
{
    public class IQFile
    {
        /// <summary>
        /// 读取bin+json, Wav或者iq文件
        /// 其中bin为I16数据IQ交织，json包含采样信息关键字(格式见读取方法之源代码注释)
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="IQInfo">采样信息</param>
        /// <param name="maxLength">Max Samples to Read, default 各个子模块自行决定</param>
        /// <param name="startSample">Sample position to start read, default=begin</param>
        /// <returns></returns>
        public static Complex[] ReadIQFile(string fileName, ref IQFileInfo IQInfo, int maxLength = 0, long startSample = 0)
        {
            string ext = Path.GetExtension(fileName);
            if (ext == ".bin")
            {
                return ReadI16IQFile(fileName, ref IQInfo, maxLength, startSample);
            }
            else
                if (ext == ".wav")
            {
                return ReadWavFile(fileName, ref IQInfo); //尚不支持wav文件从任意位置读取
            }
            else
                return ReadVectorFile(fileName, ref IQInfo, maxLength, startSample);
        }
        #region Vector文件读写
        /// <summary>
        /// 限长度，读取IQ矢量文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="IQInfo">采样信息</param>
        /// <param name="seekSamples">读取第一个样本的位置</param>
        /// <param name="lengthLimit">长度限制, 不大于零时缺省为1e6</param>
        /// <returns>IQ波形</returns>
        public static Complex[] ReadVectorFile(string fileName, ref IQFileInfo IQInfo, int lengthLimit = 0, long seekSamples = 0)
        {

            // 打开文件，获取文件格式。
            var vectorFile = new VectorFile.VectorFile();
             
            vectorFile.Open(fileName, FileMode.Open, FileAccess.Read);
            FileFormat _fileFormat = vectorFile.Storage.FileFormat;
            vectorFile.Close();
            int _NumOfSamplesToRead = 0;
            // 根据不同的文件格式，设置默认读取的IQ点数。
            switch (_fileFormat)
            {
                case FileFormat.FixFrequencyFrame:
                    {
                        // 对于帧式数据文件，总是按帧读取固定点数。
                        var frameFile = new FixFrequencyFrameFile();
                        frameFile.Open(fileName, FileMode.Open, FileAccess.Read);
                        _NumOfSamplesToRead = frameFile.Frame.Length;
                        frameFile.Close();
                        break;
                    }
                case FileFormat.FixFrequencyStream:
                    {
                        // 对于流式数据文件，设置默认读取100k个点。
                        var streamFile = new FixFrequencyStreamFile();
                        streamFile.Open(fileName, FileMode.Open, FileAccess.Read);
                        long position = seekSamples;
                        if (seekSamples < 0 || seekSamples > streamFile.NumberOfSamples)
                        {
                            position = 0;
                        }
                        _NumOfSamplesToRead = (int)(streamFile.NumberOfSamples - position);
                        if (lengthLimit > 0)
                            _NumOfSamplesToRead = Math.Min(lengthLimit, _NumOfSamplesToRead); //长度限制有效
                        else
                            _NumOfSamplesToRead = Math.Min((int)1e6, _NumOfSamplesToRead); //长度限制无效，最多读取1e6点
                        streamFile.Close();
                        break;
                    }
                default:
                    {
                        // 对于其他格式的数据文件，抛出异常。
                        throw new VectorFileException(ExceptionEnum.DataTypeConflict, "文件格式无效。");
                    }
            }

            
            short[] _iqData = new short[(int)_NumOfSamplesToRead * 2];
            IQInfo = new IQFileInfo();
            IQInfo.Signal = new BasebandInfo();

            switch (_fileFormat)
            {
                case FileFormat.FixFrequencyFrame:
                    {
                        // 实例化帧式数据文件的对象。
                        var frameFile = new FixFrequencyFrameFile();
                        frameFile.Open(fileName, FileMode.Open, FileAccess.Read);

                        // 获取信号的参数。
                        IQInfo.Signal.RFGain = frameFile.Sampling.Channels[0].GetScaleFactor();
                        IQInfo.Signal.CenterFrequency = frameFile.Sampling.Channels[0].RFFrequency;
                        IQInfo.Signal.SampleRate = frameFile.Sampling.SampleRate;
                        IQInfo.NumOfSamples = frameFile.NumberOfFrames * frameFile.Frame.Length;

                        if (seekSamples > 0 && seekSamples < frameFile.NumberOfFrames)
                        {
                            frameFile.Seek(seekSamples, SeekOrigin.Begin);
                        }
                        // 读取IQ数据。 
                        frameFile.Read(_iqData);   //!此处有bug读文件报长度错

                        // 关闭文件
                        frameFile.Close();
                        break;
                    }
                case FileFormat.FixFrequencyStream:
                    {
                        // 实例化流式数据文件对象。
                        var streamFile = new FixFrequencyStreamFile();
                        streamFile.Open(fileName, FileMode.Open, FileAccess.Read);

                        IQInfo.Signal.RFGain = streamFile.Sampling.Channels[0].GetScaleFactor();
                        IQInfo.Signal.CenterFrequency = streamFile.Sampling.Channels[0].RFFrequency;
                        IQInfo.Signal.SampleRate = streamFile.Sampling.SampleRate;
                        IQInfo.NumOfSamples = streamFile.NumberOfSamples;

                        if (seekSamples > 0 && seekSamples < streamFile.NumberOfSamples)
                        {
                            streamFile.Seek(seekSamples, SeekOrigin.Begin);
                        }
                        // 读取IQ数据。             
                        streamFile.Read(_iqData);

                        //关闭文件。
                        streamFile.Close();
                        break;
                    }
                default:
                    {
                        // 对于其他格式的数据文件，抛出异常。
                        throw new VectorFileException(ExceptionEnum.DataTypeConflict, "文件格式无效。");
                    }
            }
            Complex[] iqWav = new Complex[_NumOfSamplesToRead];
            double gain = IQInfo.Signal.RFGain;
            for (int i = 0; i < _NumOfSamplesToRead; i++)
            {
                iqWav[i] = new Complex(_iqData[i * 2] * gain, _iqData[i * 2 + 1] * gain);
            }
            return iqWav;
        }
        /// <summary>
        /// 写入矢量文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="iqWav">IQ波形</param>
        /// <param name="IQInfo">采集信息</param>
        public static void WriteVectorFile(string fileName, Complex[] iqWav, IQFileInfo IQInfo)
        {
            // 实例化FixFrameFile对象，创建文件。
            FixFrequencyFrameFile vectorFile = new FixFrequencyFrameFile();
            vectorFile.Open(fileName, FileMode.Create, FileAccess.Write, false);

            // 设置数据长度，类型。
            vectorFile.Frame.Length = iqWav.Length;
            vectorFile.Frame.SamplingInfoType = FixFreqFrameFileSamplingInfoType.Shared;

            // 填写数据类型、通道数、采样率。
            vectorFile.Storage.DataType = DataType.ComplexI16;
            vectorFile.Storage.NumberOfChannels = 1;
            vectorFile.Sampling.SampleRate = IQInfo.Signal.SampleRate;

            // 根据通道数，填写各通道的中心频率、带宽、换算因子等。

            vectorFile.Sampling.Channels.Add(new BaseChannelSamplingInfo());
            vectorFile.Sampling.Channels[0].RFFrequency = IQInfo.Signal.CenterFrequency;
            vectorFile.Sampling.Channels[0].IFFrequency = IQInfo.Signal.IFCenterFrequency;
            // 简化实现，带宽取值为：采样率 x 0.8。在实际应用中应填入真实信号带宽。
            vectorFile.Sampling.Channels[0].Bandwidth = IQInfo.Signal.SampleRate * 0.8;
            vectorFile.Sampling.Channels[0].RFScaleFactor = IQInfo.Signal.RFGain;
            // 仿真实现：根据设定的信号电平，计算I16 -> 电压值的换算因子。在实际应用中应填入实际换算因子。
            // 先将电平（dBm）转换为电压值（dBm -> mW -> V），然后计算对应I16 (-32767 ~ 32767）满量程的换算因子。
            double scale = (double)30000 / iqWav.Max(value => value.Magnitude);
            vectorFile.Sampling.Channels[0].DigitizerScaleFactor = (double)1 / scale;

            // 写入文件头。
            vectorFile.WriteFileHeader();

            //转换数据到Short
            var shortSineAllChannels = new short[iqWav.Length * 2];
            for (int i = 0; i < iqWav.Length; i++)
            {
                shortSineAllChannels[i * 2] = (short)Math.Round(iqWav[i].Real * scale);
                shortSineAllChannels[i * 2 + 1] = (short)Math.Round(iqWav[i].Imaginary * scale);
            }

            // 写入文件。
            vectorFile.Write(shortSineAllChannels);

            vectorFile?.Close();
        }
        #endregion
        #region I16IQ文件读写
        /// <summary>
        /// 读取I16交替IQ文件bin及其信号说明json文件
        /// </summary>
        /// <param name="fileName">bin文件名</param>
        /// <param name="IQInfo">json文件包含的采样信息</param>
        /// <returns></returns>
        public static Complex[] ReadI16IQFile(string fileName, ref IQFileInfo IQInfo, int maxLength = 0, long seek = 0)
        {
            /******* json范例 *******
            {
                "Signal": {
                    "Format": "I16LittleEndianIQ",
                    "CenterFrequency": 1.0E9,
                    "IFCenterFrequency": 7E7,
                    "RFGain": 1.0E-5,
                    "SampleRate": 500000.00
                }
            }
             */
            //get info
            string jsonName = Path.ChangeExtension(fileName, ".json");
            string infoText = File.ReadAllText(jsonName);
            IQInfo = JsonConvert.DeserializeObject<IQFileInfo>(infoText);
            double gain = IQInfo.Signal.RFGain;

            //open file get info
            var filestream = File.Open(fileName, FileMode.Open);
            var binaryStream = new BinaryReader(filestream);
            FileInfo fInfo = new FileInfo(fileName);
            //calculate IQ length and init IQ array
            long sizeByte = fInfo.Length;
            int IQSize = (int)(sizeByte / 4) - 2; //reduce length working with ignoring heading bytes

            IQInfo.NumOfSamples = IQSize;
            int readLength = IQSize;
            //读取长度为文件长度和最大长度输入取小，最大长度=0表示忽略
            if (maxLength > 0 && readLength > maxLength)
                readLength = maxLength;
            //读取位置保证可以读到足够数量；
            long readPosition = Math.Min(seek, IQSize - readLength) * 4 + 4;
            filestream.Seek(readPosition, SeekOrigin.Begin);

            int tempI = 0;
            int tempQ = 0;
            Complex[] IQwave = new Complex[IQSize];

            for (int i = 0; i < IQSize; i++)
            {
                tempI = binaryStream.ReadInt16();
                tempQ = binaryStream.ReadInt16();
                IQwave[i] = new Complex(tempI * gain, tempQ * gain);
            }
            binaryStream.Close();
            filestream.Close();
            return IQwave;
        }

        /// <summary>
        /// 写入Bin文件(I16交织bin & 参数Json)
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="iqWav">IQ波形</param>
        /// <param name="IQInfo">采集信息</param>
        public static void WriteI16IQFile(string fileName, Complex[] iqWav, IQFileInfo IQInfo)
        {
            //复数换I16
            double maxReal = iqWav.Max(value => value.Real);
            double maxImage = iqWav.Max(value => value.Imaginary);
            double scale = 30000 / Math.Max(maxReal, maxImage); //按最大分量归一化到30000
            Vector.ArrayScale(iqWav, scale);
            //Complex[] => Complex32[] => short[]
            Complex32[] iqWav32 = new Complex32[iqWav.Length];
            Vector.ConvertToComplex32(iqWav, iqWav32);
            short[] iqI16 = new short[iqWav.Length*2];
            Vector.ConvertToShort(iqWav32, iqI16);
            byte[] buffer = new byte[iqI16.Length * 2];

            //写bin文件
            FileStream _binFileStream = new FileStream(fileName, FileMode.Create);
            BinaryWriter _binWriter = new BinaryWriter(_binFileStream);

            Buffer.BlockCopy(iqI16, 0, buffer, 0, buffer.Length);
            _binWriter.Write(buffer);

            _binWriter.Close();
            _binFileStream.Close();

            //生成json文件名
            string jName = Path.GetFileNameWithoutExtension(fileName);
            jName += ".json";
            jName = Path.Combine(Path.GetDirectoryName(fileName), jName);
            //生成json描述
            string jTxt = "{" + Environment.NewLine
                + "   \"Signal\": {" + Environment.NewLine
                + "      \"Format\": \"I16LittleEndianIQ\"," + Environment.NewLine
                + "      \"CenterFrequency\": " + IQInfo.Signal.CenterFrequency.ToString("F0") + "," + Environment.NewLine
                + "      \"IFCenterFrequency\": " + IQInfo.Signal.IFCenterFrequency.ToString("F0") + "," + Environment.NewLine
                + "      \"RFGain\": " + (IQInfo.Signal.RFGain/scale).ToString("F6") + "," + Environment.NewLine //复合射频增益和量化增益
                + "      \"SampleRate\": " + IQInfo.Signal.SampleRate.ToString("F0") + "," + Environment.NewLine
                + "      \"PulseShaping\": \"NA\"," + Environment.NewLine
                + "      \"BT\": 1.0," + Environment.NewLine
                + "      \"EbNo\": 15.0," + Environment.NewLine
                + "      \"FreqShift\": 0" + Environment.NewLine
                + "   }" + Environment.NewLine
                + @"}";
            //写json文件
            File.WriteAllText(jName, jTxt);
        }
        #endregion
        #region Wav文件读写
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
        {
            WavFileStream WavReader = new WavFileStream(fileName, FileMode.Open);
            //int 范围2,147,483,647，足够了
            int readSamples = (int) Math.Min((ulong)3e6, WavReader.Length);
            short[] wavData = new short[readSamples * 2];
            WavReader.Read(wavData);

            Complex[] iqWav = new Complex[readSamples];
            double waveformScale = (double)1 / (double)368809; // 数值大保证解调质量 (double)0.1 / (double)30000; //Scale 0.1
            for (int i = 0; i < readSamples; i++)
            {

                iqWav[i] = new Complex(wavData[i * 2 + 1] * waveformScale, wavData[i * 2] * waveformScale);
            }

            //find _*Hz_ pattern
            double fc = 1e9;
            int i2 = fileName.IndexOf("Hz_");
            if (i2 > 0)
            {
                int i1 = fileName.LastIndexOf("_", i2);
                string fcStr = fileName.Substring(i1 + 1, (i2 - i1 - 1));
                fc = Convert.ToDouble(fcStr);
            }
            IQInfo = new IQFileInfo();
            IQInfo.Signal = new BasebandInfo();

            IQInfo.Signal.RFGain = 1;
            IQInfo.Signal.CenterFrequency = fc;
            IQInfo.Signal.SampleRate = WavReader.SampleRate;

            return iqWav;
        }
        /// <summary>
        /// 浮点波形转换成I16数组(short[])，将峰值归一化到一个较大的I16数值
        /// </summary>
        /// <param name="data">原始浮点波形，如多通道，则通道交织</param>
        /// <param name="I16Range">归一化目标峰值, 不大于0：缺省30000</param>
        public static short[] ConvertDoubleArrayToShort(double[] data, double I16Range)
        {
            int numSamples = data.Length;
            short[] shortArray = new short[numSamples];
            double range = 30000;
            if (I16Range > 0)
                range = I16Range;
            double scale = (double)range / Math.Max(Math.Abs(data.Max()), Math.Abs(data.Min())); //ranging to +/-32000
            for (int i = 0; i < numSamples; i++)
            {
                shortArray[i] = (short)Math.Round((scale * data[i]));
            }
            return shortArray;
        }

        /// <summary>
        /// 浮点波形转换成I16，自动将峰值归一化到缺省的I16数值
        /// </summary>
        /// <param name="data"></param>
        public static short[] ConvertDoubleArrayToShort(double[] data)
        {
            return ConvertDoubleArrayToShort(data, -1); //峰值采用缺省值
        }
        #endregion

    }
    /// <summary>
    /// Class mapping to json files
    /// 暂时只有VectorFile的信号属性
    /// 也许可以在将来规定文件名包含的字段属性
    /// </summary>
    public class IQFileInfo
    {
        public BasebandInfo Signal;
        public long NumOfSamples { set; get; } //文件采样点数量
    }
    /// <summary>
    /// 基带采样信息，建议数字调制信号使用
    /// </summary>
    public class BasebandInfo
    {
        public String Format { get; set; }
        public double CenterFrequency { get; set; }
        public double IFCenterFrequency { get; set; }
        public double RFGain { get; set; }
        public double SampleRate { get; set; }
        public string PulseShaping { get; set; }
        public double BT { get; set; }
        public double EbNo { get; set; }
        public double FreqShift { get; set; }
    }

}
