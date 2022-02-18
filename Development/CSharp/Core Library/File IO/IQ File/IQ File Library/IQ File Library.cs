using Newtonsoft.Json;
using SeeSharpTools.JXI.FileIO.VectorFile;
using SeeSharpTools.JXI.FileIO.WavFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns></returns>
        public static Complex[] ReadIQFile(string fileName, ref IQFileInfo IQInfo)
        {
            string ext = Path.GetExtension(fileName);
            if (ext == ".bin")
            {
                return ReadI16IQFile(fileName, ref IQInfo);
            }
            else
                if (ext == ".wav")
            {
                return ReadWavFile(fileName, ref IQInfo);
            }
            else
                return ReadVectorFile(fileName, ref IQInfo);
        }
        #region Vector文件读写
        /// <summary>
        /// 读取IQ矢量文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="IQInfo">采样信息</param>
        /// <returns>IQ波形</returns>
        public static Complex[] ReadVectorFile(string fileName, ref IQFileInfo IQInfo)
        {
            return ReadVectorFile(fileName, ref IQInfo, 0);
        }
        /// <summary>
        /// 限长度，读取IQ矢量文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="IQInfo">采样信息</param>
        /// <param name="lengthLimit">长度限制, 不大于零时缺省为1e6</param>
        /// <returns>IQ波形</returns>
        public static Complex[] ReadVectorFile(string fileName, ref IQFileInfo IQInfo, int lengthLimit)
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
                        _NumOfSamplesToRead = (int)streamFile.NumberOfSamples;
                        streamFile.Close();
                        break;
                    }
                default:
                    {
                        // 对于其他格式的数据文件，抛出异常。
                        throw new VectorFileException(ExceptionEnum.DataTypeConflict, "文件格式无效。");
                    }
            }
            if (lengthLimit > 0)
                _NumOfSamplesToRead = Math.Min(lengthLimit, _NumOfSamplesToRead);
            else
                _NumOfSamplesToRead = Math.Min((int)1e6, _NumOfSamplesToRead);
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
            double scale = (double)30000 / iqWav.Max().Magnitude;
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
        public static Complex[] ReadI16IQFile(string fileName, ref IQFileInfo IQInfo)
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
            binaryStream.ReadInt16(); //ignore heading 4 bytes
            binaryStream.ReadInt16();
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
