using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using SeeSharpTools.LicenseManager;

namespace SeeSharpTools.JXI.FileIO.VectorFile
{
    /// <summary>
    /// FixFrequencyStreamFile : VectorFile
    /// </summary>
    public class FixFrequencyStreamFile : VectorFile
    {

        #region ------------------- Constructor and Destructor -------------------

        /// <summary>
        ///  Constructor//实例构造函数
        /// </summary>
        public FixFrequencyStreamFile() : base()
        {
            _samplingInfo = new BaseSamplingInformation();//定频数据采集设置
        }

        /// <summary>
        /// constructor, execute when first call any method
        /// //静态构造函数，创建第一个实例或者引用任何的静态成员时调用
        /// </summary>
        static FixFrequencyStreamFile()
        {
            //利用反射获取版本号
            Assembly fileAssembly = Assembly.GetExecutingAssembly();
            var fileVersion = fileAssembly.GetName().Version.ToString();//获取版本号
            var firstPointIndex = fileVersion.IndexOf('.');
            var secondPointIndex = fileVersion.Substring(firstPointIndex + 1, fileVersion.Length - firstPointIndex - 1).IndexOf('.');
            string strProduct = "SeeSharpTools.JXI.FileIO.VectorFile";
            string strVersion = fileVersion.Substring(0, firstPointIndex + 1 + secondPointIndex);//获取大版本号1.2.
#if LICENSEOFF
#else
            var status = LicenseManager.LicenseManager.GetActivationStatus(strProduct, strVersion);
            if (status < 0)
            {
                LicenseManager.ProductLicenseManager.GetActivationStatus(strProduct, strVersion);
            }
#endif

            // 默认支持打开FixFrequencyFrame格式。
            SupportFrameFileFormat = true;

        }
#endregion

#region ------------------- Public Properties (static)  -------------------

        /// <summary>
        /// 是否支持打开Fix Frequency Frame文件。
        /// 若为true，则将Fix Frequency Frame格式的文件也视为Fix Frequency Stream文件访问。
        /// 若为false，则在Open(...)方法中检查文件格式，若文件不是Fix Frequency Stream格式则抛出异常。
        /// </summary>
        public static bool SupportFrameFileFormat { get; set; }

#endregion

#region ------------------- Public Properties -------------------

        /// <summary>
        /// Current position,number of samples form origion
        /// </summary>
        public long Position//获取或设置文件的当前读写位置，即从数据起始的Sample数。
        {
            get { return this.Seek(0, SeekOrigin.Current); }
            set { this.Seek(value, SeekOrigin.Begin); }
        }

        private long _numberOfSamples;
        /// <summary>
        /// Total data size of file, in number of samples, valid only when openning existed file.
        /// </summary>
        public long NumberOfSamples
        {
            get { return _numberOfSamples; }
        }
        //定频数据采集设置
        private BaseSamplingInformation _samplingInfo;
        /// <summary>
        /// Sampling Information
        /// </summary>
        public BaseSamplingInformation Sampling { get { return _samplingInfo; } }

#endregion

#region ------------------- Public Methods -------------------

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="mode">File Mode</param>
        /// <param name="fileAccess">File Access</param>
        /// <param name="disableBuffering">Specifies whether the file will read/write without buffering.是否缓冲区读写
        /// Disable buffering willl speed up data transfers but require read/write block size to be integer multiple of disk sector size</param>
        public override void Open(string fileName, FileMode mode, FileAccess fileAccess, bool disableBuffering = false)
        {
            // Reset data information public properties.
            _samplingInfo = new BaseSamplingInformation();

            // Call base.Open to open file which will initialize fields/porperties of base class.
            base.Open(fileName, mode, fileAccess, disableBuffering);

            if (mode == FileMode.Create || mode == FileMode.CreateNew)
            {
                // Create new file.
                _numberOfSamples = 0;
                _storageInfo.FileFormat = FileFormat.FixFrequencyStream;
            }
            else
            {
                // 打开现有文件，则检查该文件的格式有效。
                if (_storageInfo.FileFormat == FileFormat.FixFrequencyStream || 
                    (SupportFrameFileFormat && _storageInfo.FileFormat == FileFormat.FixFrequencyFrame))
                {
                    // Extract data information (e.g. samplilng information).
                    ExtractDataInfoFromDictionary();
                    _numberOfSamples = (_fileLengthInBytes - _storageInfo.FileHeaderSize) / this.BytesPerSample;
                }
                else
                {
                    this.Close();
                    throw new VectorFileException(ExceptionEnum.InvalidFile, "File format doesn't match.");
                }
            }
        }

        /// <summary>
        /// 将数据信息写入文件头，在开始写入数据之前，必须调用一次WriteFileHeader()方法写入文件头。后续可以随时再次调用WriteFileHeader()方法来更新文件头。
        /// 每次调用WriteFileHeader()方法之后，数据的写入位置会被重置为起始位置，即等效于调用了一次Seek(0, SeekOrigin.Begin)。？？？重置为起始位置，数据不会乱？？
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
        /// 将文件读写位置设置为给定值
        /// </summary>
        /// <param name="offset">The number of samples relative to origin. 。</param>
        /// <param name="origin">Using a value of type System.IO.SeekOrigin, the start, end, or current position is specified as a reference point for offset.</param>
        /// <returns>The new location of the file read and write, the number of samples away from the starting point of the data, that is, the value of the attribute "Position". </returns>
        public long Seek(long offset, SeekOrigin origin)
        {
            long offsetInBytes = 0;

            // Convert input "offset" (in samples) to "offset in bytes", according to SeekOrigin.
            switch (origin)
            {
                case SeekOrigin.Begin:
                    {
                        // For seeking from Begin, calculate offset in bytes must add Stogage.FileHeaderSize.
                        offsetInBytes = offset * this.BytesPerSample + _storageInfo.FileHeaderSize;
                        break;
                    }
                case SeekOrigin.Current:
                case SeekOrigin.End:
                    {
                        // For seeking from Current/End, the offset is "relative", multiply offset by "BytesPerSample" is ok.
                        offsetInBytes = offset * this.BytesPerSample;
                        break;
                    }
            }

            // Call FileStream or KernelFileStream to seek file according to private field "_disableBuffering".
            long positionInBytes;
            if (_disableBuffering) { positionInBytes =  _kernelFileStream.Seek(offsetInBytes, origin); }
            else { positionInBytes = _nativeFileStream.Seek(offsetInBytes, origin); }

            // Convert "offset in bytes" from file start to "offset in samples" from data start.
            return (positionInBytes - _storageInfo.FileHeaderSize) / this.BytesPerSample;
        }


#endregion

#region ------------------- Private Methods -------------------

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

        }

        private void UpdateDataInfoToDictionary()
        {
            int i;
            double[] numericArray;
            string resultString;
            string separator = ",";


#region --------Update "Sampling" information to INI Dictionary -------------------

            // Update "Sample Rate".
            resultString = _samplingInfo.SampleRate.ToString("f3");
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSampleRate))
            { _infoDictionary[HeaderKeyCostants.SamplingSampleRate] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingSampleRate, resultString); }

            // Check if sampling information valid.
            if (_samplingInfo.Channels == null || _samplingInfo.Channels.Count != _storageInfo.NumberOfChannels)
            { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Samplilng.Channel.Count"); }

            numericArray = new double[_samplingInfo.Channels.Count];

            // Update "RF Frequency".
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].RFFrequency; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f12", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingRFFrequency))
            { _infoDictionary[HeaderKeyCostants.SamplingRFFrequency] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingRFFrequency, resultString); }

            // Update "Bandwidth".
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].Bandwidth; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f2", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingBandwidth))
            { _infoDictionary[HeaderKeyCostants.SamplingBandwidth] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingBandwidth, resultString); }

            // Update "Reference Level".
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].ReferenceLevel; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f0", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingReferenceLevel))
            { _infoDictionary[HeaderKeyCostants.SamplingReferenceLevel] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingReferenceLevel, resultString); }

            // Update "IF Frequency".
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].IFFrequency; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f12", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingIFFrequency))
            { _infoDictionary[HeaderKeyCostants.SamplingIFFrequency] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingIFFrequency, resultString); }

            // Update "RF Scale Factor".
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].RFScaleFactor; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f12", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingRFScaleFactor))
            { _infoDictionary[HeaderKeyCostants.SamplingRFScaleFactor] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingRFScaleFactor, resultString); }

            // Update "Digitizer Scale Factor".
            for (i = 0; i < _samplingInfo.Channels.Count; i++) { numericArray[i] = _samplingInfo.Channels[i].DigitizerScaleFactor; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f12", separator);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingDigitizerScaleFactor))
            { _infoDictionary[HeaderKeyCostants.SamplingDigitizerScaleFactor] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingDigitizerScaleFactor, resultString); }

#endregion
        }

#endregion

    }

#region ----------Data Types and Structures----------

    /// <summary>
    /// Sampling Information
    /// </summary>
    public class BaseSamplingInformation//定频数据采集设置。
    {
        private double _sampleRate;
        /// <summary>
        /// SampleRate，S/s
        /// </summary>
        public double SampleRate
        {
            get { return _sampleRate; }
            set
            {
                if (value > 0) { _sampleRate = value; }
                else { throw new ArgumentException("Sample rate must be greater than 0"); }
            }
        }

        private List<BaseChannelSamplingInfo> _channels;
        /// <summary>
        /// Sampling Infomaton of Channels
        /// </summary>
        public List<BaseChannelSamplingInfo> Channels//各通道的采样信息。
        {
            get { return _channels; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public BaseSamplingInformation()
        {
            _sampleRate = 1;
            _channels = new List<BaseChannelSamplingInfo>();
        }
    }

#endregion
}
