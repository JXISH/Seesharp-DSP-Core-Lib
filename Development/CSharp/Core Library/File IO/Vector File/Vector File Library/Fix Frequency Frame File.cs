using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace SeeSharpTools.JXI.FileIO.VectorFile
{
    /// <summary>
    ///  FixFrequencyFrameFile : VectorFile
    ///  从基类VectorFile类中派生过来的新类
    /// </summary>
    public class FixFrequencyFrameFile : VectorFile
    {

        #region ------------------- 构造函数 -------------------

        /// <summary>
        ///  Constructor
        /// </summary>
        public FixFrequencyFrameFile() : base()
        {
            _samplingInfo = new BaseSamplingInformation();
            _frameDataInfo = new FixFreqFrameDataInformation();
        }

        /// <summary>
        /// constructor, execute when first call any method
        /// 静态构造函数，当引用任何静态成员或者创建第一个实例时调用
        /// </summary>
        static FixFrequencyFrameFile() 
        {

        }
        #endregion

        #region ------------------- 公共属性 -------------------

        /// <summary>
        /// Current position,number of Frames form origion
        /// </summary>
        public long Position
        {
            get { return this.Seek(0, SeekOrigin.Current); }
            set { this.Seek(value, SeekOrigin.Begin); }
        }

        private long _numberOfFrames;
        /// <summary>
        /// Total data size of file, in number of frames, valid only when openning existed file.
        /// </summary>
        public long NumberOfFrames { get { return _numberOfFrames; } }

        private BaseSamplingInformation _samplingInfo;
        /// <summary>
        /// Sampling information
        /// 定频数据采集设置
        /// </summary>
        public BaseSamplingInformation Sampling { get { return _samplingInfo; } }

        private FixFreqFrameDataInformation _frameDataInfo;
        /// <summary>
        /// Frame data information
        /// 定频帧式Vector文件的存储信息。
        /// </summary>
        public FixFreqFrameDataInformation Frame { get { return _frameDataInfo; } }

        #endregion

        #region ------------------- 公共方法 -------------------

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="mode">File Mode</param>
        /// <param name="fileAccess">File Access</param>
        /// <param name="disableBuffering">Specifies whether the file will read/write without buffering. 
        /// Disable buffering willl speed up data transfers but require read/write block size to be integer multiple of disk sector size</param>
        public override void Open(string fileName, FileMode mode, FileAccess fileAccess, bool disableBuffering = false)
        {
            // Reset data information public properties.
            _samplingInfo = new BaseSamplingInformation();
            _frameDataInfo = new FixFreqFrameDataInformation();

            // Call base.Open to open file which will initialize fields/porperties of base class.
            base.Open(fileName, mode, fileAccess, disableBuffering);

            if (mode == FileMode.Create || mode == FileMode.CreateNew)
            {
                // Create new file.
                _numberOfFrames = 0;
                _storageInfo.FileFormat = FileFormat.FixFrequencyFrame;
                _frameDataInfo.SamplingInfoType = FixFreqFrameFileSamplingInfoType.Shared;
            }
            else
            {
                // Open existed file.
                if (_storageInfo.FileFormat != FileFormat.FixFrequencyFrame)
                {
                    this.Close();
                    throw new VectorFileException(ExceptionEnum.InvalidFile, "The vector file is not Fix Frequency Frame format.");
                }

                // Extract data information (e.g. samplilng information).
                ExtractDataInfoFromDictionary();

                // Calculate total number of frames in file.
                long bytesPerFrame = this.BytesPerSample * _frameDataInfo.Length + _frameDataInfo.HeaderSize;
                _numberOfFrames = (_fileLengthInBytes - _storageInfo.FileHeaderSize) / bytesPerFrame;
            }

        }

        /// <summary>
        /// Write data information to file header, could be called after writing data.
        /// After the operation, file pointer will be set to the beginning of data (value of property "Position" is 0 after operation).
        /// In creating new file use case, it must be called once before writing first block data.
        /// </summary>
        public override void WriteFileHeader()
        {
            UpdateDataInfoToDictionary();
            base.WriteFileHeader();
        }

        /// <summary>
        /// Set the current read-write location of the file to a given value.     
        /// </summary>
        /// <param name="offset">The number of frames relative to origin. 。</param>
        /// <param name="origin">Using a value of type System.IO.SeekOrigin, the start, end, or current position is specified as a reference point for offset.</param>
        /// <returns>The new location of the file read and write, the number of frames away from the starting point of the data, that is, the value of the attribute "Position". 。</returns>
        public long Seek(long offset, SeekOrigin origin)
        {
            long offsetInBytes = 0;

            // Convert input "offset" (in frames) to "offset in bytes", according to SeekOrigin.
            long bytesPerFrame = this.BytesPerSample * _frameDataInfo.Length + _frameDataInfo.HeaderSize;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    {
                        // For seeking from Begin, calculate offset in bytes must add Stogage.FileHeaderSize.
                        offsetInBytes = offset * bytesPerFrame + _storageInfo.FileHeaderSize;
                        break;
                    }
                case SeekOrigin.Current:
                case SeekOrigin.End:
                    {
                        // For seeking from Current/End, the offset is "relative", multiply offset by "bytesPerFrame" is ok.
                        offsetInBytes = offset * bytesPerFrame;
                        break;
                    }
            }

            // Call FileStream or KernelFileStream to seek file according to private field "_disableBuffering".
            long positionInBytes;
            if (_disableBuffering) { positionInBytes =  _kernelFileStream.Seek(offsetInBytes, origin); }
            else { positionInBytes = _nativeFileStream.Seek(offsetInBytes, origin); }

            // Convert "offset in bytes" from file start to "offset in frames" from data start.
            return (positionInBytes - _storageInfo.FileHeaderSize) / bytesPerFrame;
        }

        /// <summary>
        /// Write data of type I8. If it is multi-channel, the data   stored as Channel Interleave. 
        /// </summary>
        /// <param name="data"></param>
        public override void Write(sbyte[] data)//写入I8类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        {
            // 写入的数据长度必须与FrameLength一致。
            if (data.Length != (_frameDataInfo.Length * this.BytesPerSample))
            { throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame length"); }

            base.Write(data);
        }

        /// <summary>
        /// Write data of type I16. If it is multi-channel, the data stored as Channel Interleave. 
        /// </summary>
        /// <param name="data"></param>
        public override void Write(short[] data)
        {
            // 写入的数据长度必须与FrameLength一致。
            if (data.Length != (_frameDataInfo.Length * this.BytesPerSample / sizeof(short)))
            { throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame length"); }

            base.Write(data);
        }

        /// <summary>
        ///  Write data by Intptr. If it is multi-channel, the data  stored as Channel Interleave. 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lengthInBytes">Data length,in number of Byte</param>
        public override void Write(IntPtr data, int lengthInBytes)
        {
            // 写入的数据长度必须与FrameLength一致。
            if (lengthInBytes != (_frameDataInfo.Length * this.BytesPerSample))
            { throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame length"); }

            base.Write(data, lengthInBytes);
        }

        /// <summary>
        /// Read data of type I8. If it is multi-channel, the data  stored as Channel Interleave. 
        /// </summary>
        /// <param name="data"></param>
        public override void Read(sbyte[] data)
        {
            // 读取的数据长度必须与FrameLength一致。
            if (data.Length != (_frameDataInfo.Length * this.BytesPerSample))
            { throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame length"); }

            base.Read(data);
        }

        /// <summary>
        ///  Read data of type I16. If it is multi-channel, the data  stored as Channel Interleave. 
        /// </summary>
        /// <param name="data"></param>
        public override void Read(short[] data)
        {
            // 读取的数据长度必须与FrameLength一致。
            if (data.Length != (_frameDataInfo.Length * this.BytesPerSample / sizeof(short)))
            { throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame length"); }

            base.Read(data);

        }

        /// <summary>
        ///  Read data by IntPtr. If it is multi-channel, the data  stored as Channel Interleave.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lengthInBytes">Data length,in number of Byte</param>
        public override void Read(IntPtr data, int lengthInBytes)
        {
            // 读取的数据长度必须与FrameLength一致。
            if (lengthInBytes != (_frameDataInfo.Length * this.BytesPerSample))
            { throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame length"); }

            base.Read(data, lengthInBytes);

        }

        #endregion

        #region ------------------- 私有方法 -------------------

        /// <summary>
        /// 从字典中提取数据信息
        /// </summary>
        private void ExtractDataInfoFromDictionary()
        {
            int i;
            string rawCsvString;
            double[] numericArray;
            string separator = ",";

            #region --------Extract "Sampling" information from INI Dictionary -------------------

            // Extract "Sample Rate", this parameter is neccessory, missing it will throw an exception.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSampleRate))
            { _samplingInfo.SampleRate = Convert.ToDouble(_infoDictionary[HeaderKeyCostants.SamplingSampleRate]); }
            else
            { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Sample Rate"); }

            // Set size of "Channels" as "Stograge.NumberOfChannels".
            _samplingInfo.Channels.Clear();
            for (i = 0; i < _storageInfo.NumberOfChannels; i++) { _samplingInfo.Channels.Add(new BaseChannelSamplingInfo()); }

            // Extract "RF Frequency", this parameter is neccessory, missing it will throw an exception.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingRFFrequency))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingRFFrequency];
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separator);

                if (numericArray == null || numericArray.Length != _storageInfo.NumberOfChannels)
                { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, ": RF Frequency"); }

                for (i = 0; i < _storageInfo.NumberOfChannels; i++) { _samplingInfo.Channels[i].RFFrequency = numericArray[i]; }
            }

            // Extract "IF Frequency", this parameter is not neccessory and could be missing in dictionary.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingIFFrequency))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingIFFrequency];
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separator);

                if (numericArray != null && numericArray.Length == _storageInfo.NumberOfChannels)
                {
                    for (i = 0; i < _storageInfo.NumberOfChannels; i++) { _samplingInfo.Channels[i].IFFrequency = numericArray[i]; }
                }
            }

            // Extract "Bandwidth", this parameter is not neccessory and could be missing in dictionary.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingBandwidth))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingBandwidth];
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separator);

                if (numericArray != null && numericArray.Length == _storageInfo.NumberOfChannels)
                {
                    for (i = 0; i < _storageInfo.NumberOfChannels; i++) { _samplingInfo.Channels[i].Bandwidth = numericArray[i]; }
                }
            }

            // Extract "Reference Level", this parameter is not neccessory and could be missing in dictionary.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingReferenceLevel))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingReferenceLevel];
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separator);

                if (numericArray != null && numericArray.Length == _storageInfo.NumberOfChannels)
                {
                    for (i = 0; i < _storageInfo.NumberOfChannels; i++) { _samplingInfo.Channels[i].ReferenceLevel = numericArray[i]; }
                }
            }

            // Extract "RF Scale Factor", this parameter is neccessory, missing it will throw an exception.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingRFScaleFactor))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingRFScaleFactor];
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separator);

                if (numericArray == null || numericArray.Length != _storageInfo.NumberOfChannels)
                { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, ": RF Scale Factor"); }

                for (i = 0; i < _storageInfo.NumberOfChannels; i++) { _samplingInfo.Channels[i].RFScaleFactor = numericArray[i]; }
            }

            // Extract "Digitizer Scale Factor", this parameter is neccessory, missing it will throw an exception.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingDigitizerScaleFactor))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingDigitizerScaleFactor];
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separator);

                if (numericArray == null || numericArray.Length != _storageInfo.NumberOfChannels)
                { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, ": Digitizer Scale Factor"); }

                for (i = 0; i < _storageInfo.NumberOfChannels; i++) { _samplingInfo.Channels[i].DigitizerScaleFactor = numericArray[i]; }
            }

            #endregion

            #region --------Extract "Frame" information from INI Dictionary -------------------

            // Extract "Frame Length", this parameter is neccessory, missing it will throw an exception.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageFrameLength))
            { _frameDataInfo.Length = Convert.ToInt32(_infoDictionary[HeaderKeyCostants.StorageFrameLength]); }
            else
            { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Frame Length"); }

            // Extract "Frame Header size", and this will also determine sampling information type.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageFrameHeaderSize))
            {
                int frameHeaderSize = Convert.ToInt32(_infoDictionary[HeaderKeyCostants.StorageFrameHeaderSize]);
                if (frameHeaderSize > 0)
                {
                    _frameDataInfo.SamplingInfoType = FixFreqFrameFileSamplingInfoType.Independent;
                    _frameDataInfo.HeaderSize = frameHeaderSize;
                }
                else
                {
                    _frameDataInfo.SamplingInfoType = FixFreqFrameFileSamplingInfoType.Shared;
                    _frameDataInfo.HeaderSize = 0;
                }
            }
            else
            {
                _frameDataInfo.SamplingInfoType = FixFreqFrameFileSamplingInfoType.Shared;
                _frameDataInfo.HeaderSize = 0;
            }

            #endregion

        }
        /// <summary>
        /// 更新信息到字典
        /// </summary>

        private void UpdateDataInfoToDictionary()
        {
            int i;
            double[] numericArray;
            string resultString;
            string separator = ",";

            #region --------采样信息Update "Sampling" information to INI Dictionary -------------------

            // Update "Sample Rate".
            resultString = _samplingInfo.SampleRate.ToString("f3");//保留小数点后三位
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSampleRate))//是否包含SamplingSampleRate
            { _infoDictionary[HeaderKeyCostants.SamplingSampleRate] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingSampleRate, resultString); }

            // Check if sampling information valid.
            if (_samplingInfo.Channels == null || _samplingInfo.Channels.Count != _storageInfo.NumberOfChannels)
            { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Samplilng.Channel.Count"); }

            numericArray = new double[_samplingInfo.Channels.Count];//数组[通道数]

            // Update "RF Frequency".射频中心频率
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].RFFrequency; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f12", separator);//将数组转换为字符串
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingRFFrequency))
            { _infoDictionary[HeaderKeyCostants.SamplingRFFrequency] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingRFFrequency, resultString); }

            // Update "Bandwidth".带宽
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].Bandwidth; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f2", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingBandwidth))
            { _infoDictionary[HeaderKeyCostants.SamplingBandwidth] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingBandwidth, resultString); }

            // Update "Reference Level".参考电平
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].ReferenceLevel; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f0", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingReferenceLevel))
            { _infoDictionary[HeaderKeyCostants.SamplingReferenceLevel] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingReferenceLevel, resultString); }

            // Update "IF Frequency".//中频频率
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].IFFrequency; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f12", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingIFFrequency))
            { _infoDictionary[HeaderKeyCostants.SamplingIFFrequency] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingIFFrequency, resultString); }

            // Update "RF Scale Factor".RF链路补偿因子
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].RFScaleFactor; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f12", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingRFScaleFactor))
            { _infoDictionary[HeaderKeyCostants.SamplingRFScaleFactor] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingRFScaleFactor, resultString); }

            // Update "Digitizer Scale Factor".数字化仪补偿因子
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].DigitizerScaleFactor; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f12", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingDigitizerScaleFactor))
            { _infoDictionary[HeaderKeyCostants.SamplingDigitizerScaleFactor] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingDigitizerScaleFactor, resultString); }

            #endregion

            #region --------Update "Frame" information to INI Dictionary -------------------

            // Update "Frame Length".
            resultString = _frameDataInfo.Length.ToString();
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageFrameLength))
            { _infoDictionary[HeaderKeyCostants.StorageFrameLength] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.StorageFrameLength, resultString); }

            // Update "Frame Header Size".
            resultString = _frameDataInfo.HeaderSize.ToString();
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageFrameHeaderSize))
            { _infoDictionary[HeaderKeyCostants.StorageFrameHeaderSize] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.StorageFrameHeaderSize, resultString); }

            #endregion

        }

        #endregion

    }

    #region ---------- 数据类型定义 ----------

    /// <summary>
    /// Information of fixed-frequency frame Vector file. 
    /// </summary>
    public class FixFreqFrameDataInformation//定频帧式Vector文件的存储信息。
    {

        #region ------------------- 常量 -------------------

        internal static readonly int FrameHeaderSizeMin = 1024;

        #endregion

        /// <summary>
        /// Length of fram,uint is samples
        /// </summary>
        public int Length { get; set; }//每个Frame的数据长度，以Sample为单位。

      
        private FixFreqFrameFileSamplingInfoType _samplingInfoType;
        /// <summary>
        /// Information of Sampling
        /// 定频帧式Vector文件的采样信息类型。
        /// </summary>
        public FixFreqFrameFileSamplingInfoType SamplingInfoType//数据采样信息类型。
        {
            get { return _samplingInfoType; }
            set
            {
                _samplingInfoType = value;
                // 根据类型，重置私有成员FrameHeaderSize的值。
                if (value == FixFreqFrameFileSamplingInfoType.Shared) { _headerSize = 0; }
                else { _headerSize = Math.Max(_headerSize, FrameHeaderSizeMin); }
            }
        }

        private int _headerSize;
        /// <summary>
        ///  When the type of data sampling information is Independent, the size of the frame header and the number of bytes per frame of data.
        /// </summary>
        public int HeaderSize//当数据采样信息类型为Independent时，每帧数据的帧头大小，字节数。
        {
            get { return _headerSize; }
            set
            {
                if (_samplingInfoType == FixFreqFrameFileSamplingInfoType.Shared) { _headerSize = 0; }
                else { _headerSize = Math.Max(value, FrameHeaderSizeMin); }
            }
        }

        #region ------------------- 构造函数 -------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        public FixFreqFrameDataInformation():base()
        {
            _samplingInfoType = FixFreqFrameFileSamplingInfoType.Shared;//所有数据帧有相同的采样信息。
        }

        #endregion

    }

    /// <summary>
    /// Sampling information of fixed-frequency frame Vector file. 
    /// </summary>
    public enum FixFreqFrameFileSamplingInfoType//定频帧式Vector文件的采样信息类型。
    {
        /// <summary>
        /// 所有数据帧有相同的采样信息。
        /// </summary>
        Shared,

        /// <summary>
        /// 每帧数据都有独立的采样信息：时间和地理位置。
        /// </summary>
        Independent
    }

    #endregion
}
