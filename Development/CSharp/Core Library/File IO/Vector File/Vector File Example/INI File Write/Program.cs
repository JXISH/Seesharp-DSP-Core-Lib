using SeeSharpTools.JXI.FileIO.VectorFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IniFileWriteExample
{
    class Program
    {
        static void Main(string[] args)
        {
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
            

        }
    }
}
