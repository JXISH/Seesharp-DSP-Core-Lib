using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Device.Location;
using System.IO;
using System.Runtime.InteropServices;

namespace SeeSharpTools.JXI.FileIO.VectorFile
{
    /// <summary>
    /// 
    /// </summary>
    public class VectorFile
    {

        #region ------------------- 常量 -------------------

        internal const int BYTES_OF_REAL_I8 = 1;
        internal const int BYTES_OF_REAL_I16 = 2;
        internal const int BYTES_OF_REAL_F32 = 4;
        internal const int BYTES_OF_REAL_D64 = 8;
        internal const int BYTES_OF_COMPLEX_I8 = 2;
        internal const int BYTES_OF_COMPLEX_I16 = 4;

        internal static readonly string VECTORFILE_VERSION = "3.2";
        internal static readonly int VECTORFILE_HEADER_QUANTUM = 65536;
        internal static readonly int HEADER_TAG_SIZE_MAXIMUM = 256;

        internal static readonly string VECTORFILE_HEADER_TAG = "JXI  Vector File";
        internal static readonly string HEADER_SECTION_NAME = "JXI  Vector File";
        internal static readonly string[] HEADER_TAG_TOKENS = new string[] { "VISN", "JXI", "Receiver", "Vector" };


        #endregion

        #region ------------------- 私有成员 -------------------

        /// <summary>
        /// Flag indicates whether file is opened.
        /// </summary>
        protected bool _fileOpened = false;

        /// <summary>
        /// Indicates whether header size is changeable. In opening existing file use case, it is false.
        /// In creating new file use case, it is true until a "CommitDataInformation()" is called.
        /// </summary>
        protected bool _fileHeaderSizeChangeable;

        /// <summary>
        /// Whether the file will read/write without buffering. If true, KernelFileStream is used.
        /// </summary>
        protected bool _disableBuffering = false;

        /// <summary>
        /// 
        /// </summary>
        protected FileStream _nativeFileStream;
        /// <summary>
        /// Internal byte[] for converting short[]/IntPtr data input/output for FileStream read/write.
        /// The Read()/Write() of C# FileStream class only accept byte[] data type, 
        /// so we need to perform data type convert when reading/writing other data type (e.g. short[]/Intptr)
        /// </summary>
        internal FlexByteArray _nativeFileInteropArray;

        /// <summary>
        /// 
        /// </summary>
        protected KernelFileStream _kernelFileStream;
        /// <summary>
        /// Internal IntPtr for converting sbyte[]/short[] data input/output for KernelFileStream read/write.
        /// The Read()/Write() of KernelFileStream class only accept IntPtr data type, 
        /// so we need to perform data type convert when reading/writing other data type (e.g. sbyte[]/short[]).
        /// </summary>
        internal FlexInteropPtr _kernelFileInteropPtr;

        /// <summary>
        /// Dictionary contains all keys/values of Vector File header information.
        /// </summary>
        protected Dictionary<string, string> _infoDictionary;

        #endregion

        #region ------------------- 公共属性 -------------------

        /// <summary>
        /// 
        /// </summary>
        protected string _fileFullPath;
        /// <summary>
        /// 获取文件的全路径名，即传递给构造函数的名称。
        /// </summary>
        public string Name { get { return _fileFullPath; } }

        /// <summary>
        /// 
        /// </summary>
        protected long _fileLengthInBytes = 0;
        /// <summary>
        /// 获取文件长度，字节数。
        /// </summary>
        public long Length { get { return _fileLengthInBytes; } }

        /// <summary>
        /// BytesPerSample is determined by "Storage.DataType" and "Storage.NumberOfChannels", 
        /// </summary>
        /// <returns></returns>
        public int BytesPerSample
        {
            get
            {
                int bytesPerPoint;

                switch (_storageInfo.DataType)
                {
                    case DataType.RealI8: { bytesPerPoint = BYTES_OF_REAL_I8; break; }
                    case DataType.RealI16: { bytesPerPoint = BYTES_OF_REAL_I16; break; }
                    case DataType.RealF32: { bytesPerPoint = BYTES_OF_REAL_F32; break; }
                    case DataType.RealD64: { bytesPerPoint = BYTES_OF_REAL_D64; break; }
                    case DataType.ComplexI8: { bytesPerPoint = BYTES_OF_COMPLEX_I8; break; }
                    case DataType.ComplexI16: { bytesPerPoint = BYTES_OF_COMPLEX_I16; break; }
                    default: { throw new NotImplementedException("Data type : " + _storageInfo.DataType.ToString()); }
                }

                return bytesPerPoint * this.Storage.NumberOfChannels;
            }
        }

        /// <summary>
        /// Storage Information
        /// </summary>
        protected StorageInformation _storageInfo;
        /// <summary>
        /// 存储格式。
        /// </summary>
        public StorageInformation Storage { get { return _storageInfo; } }

        /// <summary>
        /// 
        /// </summary>
        protected MakerInformation _makerInfo;
        /// <summary>
        /// 制造商信息。
        /// </summary>
        public MakerInformation Maker { get { return _makerInfo; } }

        /// <summary>
        /// 
        /// </summary>
        protected ArchiveInformation _archiveInfo;
        /// <summary>
        /// 归档信息。
        /// </summary>
        public ArchiveInformation Archive { get { return _archiveInfo; } }

        /// <summary>
        /// 
        /// </summary>
        protected DeviceInformation _deviceInfo;
        /// <summary>
        /// 设备信息。
        /// </summary>
        public DeviceInformation Device { get { return _deviceInfo; } }

        /// <summary>
        /// 
        /// </summary>
        protected ReservedInformation _reservedInfo;
        /// <summary>
        /// 预留信息。
        /// </summary>
        public ReservedInformation Reserved { get { return _reservedInfo; } }

        #endregion

        #region ------------------- 构造和析构函数 -------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public VectorFile()
        {
            _fileOpened = false;
            _disableBuffering = false;

            _infoDictionary = new Dictionary<string, string>();
            _kernelFileStream = new KernelFileStream();
            _kernelFileInteropPtr = new FlexInteropPtr();
            _nativeFileInteropArray = new FlexByteArray();
        }

        /// <summary>
        ///  析构函数，关闭已打开的文件。
        /// </summary>
        ~VectorFile()
        {
            this.Close();
        }

        #endregion

        #region ------------------- 公共方法 -------------------

        /// <summary>
        /// 打开或创建文件。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mode"></param>
        /// <param name="fileAccess"></param>
        /// <param name="disableBuffering">Specifies whether the file will read/write without buffering. 
        /// Disable buffering willl speed up data transfers but require read/write block size to be integer multiple of disk sector size.</param>   
        public virtual void Open(string filePath, FileMode mode, FileAccess fileAccess, bool disableBuffering = false)
        {

            #region ------------------- Check if input parameters are valid. -------------------

            if (filePath == string.Empty)
            { throw new ArgumentNullException("Empty file path."); }

            if (mode == FileMode.OpenOrCreate || mode == FileMode.Truncate || mode == FileMode.Append)
            { throw new NotSupportedException("Must create new file or open existed file."); }

            if (_fileOpened)
            { throw new InvalidOperationException("Call Close() before open another file."); }

            #endregion

            #region --------------- Reset data information public properties. ---------------          

            _storageInfo = new StorageInformation();
            _archiveInfo = new ArchiveInformation();
            _makerInfo = new MakerInformation();
            _deviceInfo = new DeviceInformation();
            _reservedInfo = new ReservedInformation();

            #endregion

            // Clear data information dictionary.
            _infoDictionary.Clear();

            if (mode == FileMode.Create || mode == FileMode.CreateNew)
            {
                // Call FileStream or KernelFileStream to create file according to input "disableBuffering".
                if (disableBuffering)
                { _kernelFileStream.Open(filePath, mode, fileAccess, disableBuffering); }
                else
                { _nativeFileStream = new FileStream(filePath, mode, fileAccess); }

                _fileLengthInBytes = 0;
            }
            else
            {
                // Load data information from file, stored in private field "_infoDictionary".
                int fullHeaderSize = LoadDataInfoFromFile(filePath, mode, fileAccess);

                // Extract data information to fill in Public Properties.
                ExtractDataInfoFromDictionary();

                // Update the "Storage.FileHeaderSize" public property, which is not included in "_infoDictionary".
                _storageInfo.FileHeaderSize = fullHeaderSize;

                // Get file length in Bytes.
                _fileLengthInBytes = new FileInfo(filePath).Length;

                // Call FileStream or KernelFileStream (according to input "disableBuffering") to open file ,
                //  and seek to the first sample of data (or the end of header).
                if (disableBuffering)
                {
                    _kernelFileStream.Open(filePath, mode, fileAccess, disableBuffering);
                    _kernelFileStream.Seek(fullHeaderSize, SeekOrigin.Begin);
                }
                else
                {
                    _nativeFileStream = new FileStream(filePath, mode, fileAccess);
                    _nativeFileStream.Seek(fullHeaderSize, SeekOrigin.Begin);
                }

            }

            _disableBuffering = disableBuffering;
            _fileOpened = true;
            _fileFullPath = filePath;

        }

        /// <summary>
        /// 关闭当前文件并释放与之关联的所有资源（如文件句柄）。
        /// </summary>
        public void Close()
        {
            if (_fileOpened == false) { return; }

            try
            {
                if(_disableBuffering) { _kernelFileStream.Close(); }
                else { _nativeFileStream.Close(); }
            }
            finally
            {
                _fileOpened = false;
            }
        }

        /// <summary>
        /// Write data information to file header, could be called after writing data.
        /// After the operation, file pointer will be set to the beginning of data (value of property "Position" is 0 after operation).
        /// In creating new file use case, it must be called once before writing first block data.      
        /// </summary>
        public virtual void WriteFileHeader()
        {
            UpdateDataInfoToDictionary();
            WriteDataInfoToFileHeader();
        }

        /// <summary>
        /// 写入I8类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Write(sbyte[] data)
        {
            if (Storage.DataType != DataType.RealI8 && Storage.DataType != DataType.ComplexI8)
            { throw new VectorFileException(ExceptionEnum.DataTypeConflict); }

            byte[] byteData = (byte[])(Array)data;

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to write file.
            if (_disableBuffering)
            {
                _kernelFileInteropPtr.Copy(byteData, 0, data.Length);
                _kernelFileStream.Write(_kernelFileInteropPtr.FlexIntPtr, data.Length);
            }
            else
            {
                _nativeFileStream.Write(byteData, 0, data.Length);
            }

        }

        /// <summary>
        /// 写入I16类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Write(short[] data)
        {
            if (Storage.DataType != DataType.RealI16 && Storage.DataType != DataType.ComplexI16)
            { throw new VectorFileException(ExceptionEnum.DataTypeConflict); }

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to write file.
            if (_disableBuffering)
            {
                _kernelFileInteropPtr.Copy(data, 0, data.Length);
                _kernelFileStream.Write(_kernelFileInteropPtr.FlexIntPtr, data.Length * sizeof(short));
            }
            else
            {
                _nativeFileInteropArray.Copy(data, 0, data.Length);
                _nativeFileStream.Write(_nativeFileInteropArray.FlexArray, 0, data.Length * sizeof(short));
            }
        }

        /// <summary>
        /// 写入Float32类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Write(float[] data)
        {
            if (Storage.DataType != DataType.RealF32) { throw new VectorFileException(ExceptionEnum.DataTypeConflict); }

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to write file.
            if (_disableBuffering)
            {
                _kernelFileInteropPtr.Copy(data, 0, data.Length);
                _kernelFileStream.Write(_kernelFileInteropPtr.FlexIntPtr, data.Length * sizeof(float));
                
            }
            else
            {
                _nativeFileInteropArray.Copy(data, 0, data.Length);
                _nativeFileStream.Write(_nativeFileInteropArray.FlexArray, 0, data.Length * sizeof(float));              
            }
        }

        /// <summary>
        /// 写入double64类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Write(double[] data)
        {
            if (Storage.DataType != DataType.RealD64) { throw new VectorFileException(ExceptionEnum.DataTypeConflict); }

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to write file.
            if (_disableBuffering)
            {
                _kernelFileInteropPtr.Copy(data, 0, data.Length);
                _kernelFileStream.Write(_kernelFileInteropPtr.FlexIntPtr, data.Length * sizeof(double));
            }
            else
            {
                _nativeFileInteropArray.Copy(data, 0, data.Length);
                _nativeFileStream.Write(_nativeFileInteropArray.FlexArray, 0, data.Length * sizeof(double));
            }
        }

        /// <summary>
        ///  以IntPtr写入数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lengthInBytes">数据长度，字节数。</param>
        public virtual void Write(IntPtr data, int lengthInBytes)
        {

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to write file.
            if (_disableBuffering)
            {
                _kernelFileStream.Write(data, lengthInBytes);
            }
            else
            {
                _nativeFileInteropArray.Copy(data, 0, lengthInBytes);
                _nativeFileStream.Write(_nativeFileInteropArray.FlexArray, 0, lengthInBytes);
            }

        }

        /// <summary>
        /// 读出I8类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Read(sbyte[] data)
        {
            if (Storage.DataType != DataType.RealI8 && Storage.DataType != DataType.ComplexI8)
            { throw new VectorFileException(ExceptionEnum.DataTypeConflict); }

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to read file.
            if (_disableBuffering)
            {
                _kernelFileInteropPtr.LeastLength = data.Length;
                _kernelFileStream.Read(_kernelFileInteropPtr.FlexIntPtr, data.Length);
                Marshal.Copy(_kernelFileInteropPtr.FlexIntPtr, (byte[])(Array)data, 0, data.Length);
            }
            else
            {
                int readLength = _nativeFileStream.Read((byte[])(Array)data, 0, data.Length);
            }
        }

        /// <summary>
        /// 读出I16类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Read(short[] data)
        {
            if (Storage.DataType != DataType.RealI16 && Storage.DataType != DataType.ComplexI16)
            { throw new VectorFileException(ExceptionEnum.DataTypeConflict); }

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to read file.
            if (_disableBuffering)
            {
                _kernelFileInteropPtr.LeastLength = data.Length * sizeof(short);
                _kernelFileStream.Read(_kernelFileInteropPtr.FlexIntPtr, data.Length * sizeof(short));
                Marshal.Copy(_kernelFileInteropPtr.FlexIntPtr, data, 0, data.Length);
            }
            else
            {
                int lengthInBytes = data.Length * sizeof(short);
                _nativeFileInteropArray.LeastLength = lengthInBytes;
                int readLength = _nativeFileStream.Read(_nativeFileInteropArray.FlexArray, 0, lengthInBytes);
                Buffer.BlockCopy(_nativeFileInteropArray.FlexArray, 0, data, 0, lengthInBytes);
            }

        }

        /// <summary>
        /// 读出Float32类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Read(float[] data)
        {
            if (Storage.DataType != DataType.RealF32) { throw new VectorFileException(ExceptionEnum.DataTypeConflict); }

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to read file.
            if (_disableBuffering)
            {
                _kernelFileInteropPtr.LeastLength = data.Length * sizeof(float);
                _kernelFileStream.Read(_kernelFileInteropPtr.FlexIntPtr, data.Length * sizeof(float));
                Marshal.Copy(_kernelFileInteropPtr.FlexIntPtr, data, 0, data.Length);
            }
            else
            {
                int lengthInBytes = data.Length * sizeof(float);
                _nativeFileInteropArray.LeastLength = lengthInBytes;
                int readLength = _nativeFileStream.Read(_nativeFileInteropArray.FlexArray, 0, lengthInBytes);
                Buffer.BlockCopy(_nativeFileInteropArray.FlexArray, 0, data, 0, lengthInBytes);
            }

        }

        /// <summary>
        /// 读出Double64类型数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Read(double[] data)
        {
            if (Storage.DataType != DataType.RealD64) { throw new VectorFileException(ExceptionEnum.DataTypeConflict); }

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to read file.
            if (_disableBuffering)
            {
                _kernelFileInteropPtr.LeastLength = data.Length * sizeof(double);
                _kernelFileStream.Read(_kernelFileInteropPtr.FlexIntPtr, data.Length * sizeof(double));
                Marshal.Copy(_kernelFileInteropPtr.FlexIntPtr, data, 0, data.Length);
            }
            else
            {
                int lengthInBytes = data.Length * sizeof(double);
                _nativeFileInteropArray.LeastLength = lengthInBytes;
                int readLength = _nativeFileStream.Read(_nativeFileInteropArray.FlexArray, 0, lengthInBytes);
                Buffer.BlockCopy(_nativeFileInteropArray.FlexArray, 0, data, 0, lengthInBytes);
            }

        }

        /// <summary>
        ///  以IntPtr读出数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lengthInBytes">数据长度，字节数。</param>
        public virtual void Read(IntPtr data, int lengthInBytes)
        {
            // Call FileStream or KernelFileStream (according to input "disableBuffering") to read file.
            if (_disableBuffering)
            {
                _kernelFileStream.Read(data, lengthInBytes);
            }
            else
            {
                _nativeFileInteropArray.LeastLength = lengthInBytes;
                int readLength = _nativeFileStream.Read(_nativeFileInteropArray.FlexArray, 0, lengthInBytes);
                Marshal.Copy(_nativeFileInteropArray.FlexArray, 0, data, lengthInBytes);
            }

        }

        #endregion

        #region ------------------- 私有方法 -------------------

        /// <summary>
        /// Open existed vector file, read data information from header and store in "_infoDictionary" private field.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mode"></param>
        /// <param name="fileAccess"></param>
        /// <returns>Full header size in bytes, or data start position in file.</returns>
        private int LoadDataInfoFromFile(string filePath, FileMode mode, FileAccess fileAccess)
        {
            int fullHeaderSize = 0;
            string headerInfoString;

            #region --------Read header information string from file ---------------

            const int SIZE_OF_I32 = 4;
            byte[] bufferForI32 = new byte[SIZE_OF_I32];

            // Open file
            _nativeFileStream = new FileStream(filePath, mode, fileAccess);

            // Parse file header to read raw string containing data information.
            try
            {
                // Read the first 4 bytes which indicates size of the following Tag.
                _nativeFileStream.Read(bufferForI32, 0, bufferForI32.Length);
                int tagLength = BitConverter.ToInt32(bufferForI32, 0);
                if (tagLength <= 0 || tagLength > HEADER_TAG_SIZE_MAXIMUM) { throw new VectorFileException(ExceptionEnum.InvalidFile); }

                // Read tag string
                byte[] bufferForTag = new byte[tagLength];
                _nativeFileStream.Read(bufferForTag, 0, bufferForTag.Length);
                string tag = Encoding.UTF8.GetString(bufferForTag);

                //  Check if read tag contains one of predefined tokens.
                bool tagContainsToken = false;
                for (int i = 0; i < HEADER_TAG_TOKENS.Length; i++)
                {
                    if (tag.Contains(HEADER_TAG_TOKENS[i])) { tagContainsToken = true; }
                }
                if (tagContainsToken != true) { throw new VectorFileException(ExceptionEnum.InvalidFile); }

                // Read the next 4 bytes which indicates size of remained header string.
                _nativeFileStream.Read(bufferForI32, 0, bufferForI32.Length);
                int remainedLength = BitConverter.ToInt32(bufferForI32, 0);
                if (remainedLength <= 0) { throw new VectorFileException(ExceptionEnum.InvalidFile); }

                // Read remained header string.
                byte[] bufferForRemained = new byte[remainedLength];
                _nativeFileStream.Read(bufferForRemained, 0, bufferForRemained.Length);
                headerInfoString = Encoding.UTF8.GetString(bufferForRemained);

                // Current file position after reading header string is the total size of header (in Bytes).
                fullHeaderSize = (int) _nativeFileStream.Position;
            }
            finally
            {
                // Close opened file anyway.
                _nativeFileStream.Close();
            }

            #endregion

            #region --------Convert header information string to INI Dictionary ---------------

            // Generate a random number used for generating temp file name.
            //string tempIniFilePath = Path.GetTempPath() + Path.GetFileNameWithoutExtension(filePath) + this.GetHashCode().ToString() + ".ini";

            // Write header string as a temperory INI file.
            //File.WriteAllText(tempIniFilePath, headerInfoString, Encoding.ASCII);

            // Read all keys and values from INI file as a Dictionary, stored in private field "_infoDictionary".
            var iniFile = new IniStreamHandler(headerInfoString);
            // The INI file should have only one section.
            string sectionName = iniFile.GetSectionNames()[0];
            _infoDictionary = iniFile.GetSectionAsDictionary(sectionName);

            // Remove the temperory INI file.
            //File.Delete(tempIniFilePath);

            #endregion

            return fullHeaderSize;

        }

        /// <summary>
        /// Extract base data information from dictionary and update this.Public Properties ((Maker/Archive/Device/Storage/Reserved).
        /// </summary>
        private void ExtractDataInfoFromDictionary()
        {

            #region --------Extract "Maker" information from INI Dictionary -------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.MakerVersion))
            { _makerInfo.Version = _infoDictionary[HeaderKeyCostants.MakerVersion]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.MakerSoftware))
            { _makerInfo.Software = _infoDictionary[HeaderKeyCostants.MakerSoftware]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.MakerProducer))
            { _makerInfo.Producer = _infoDictionary[HeaderKeyCostants.MakerProducer]; }

            #endregion

            #region --------Extract "Archive" information from INI Dictionary -------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveStationID))
            { _archiveInfo.StationID = _infoDictionary[HeaderKeyCostants.ArchiveStationID]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveOperator))
            { _archiveInfo.Operator = _infoDictionary[HeaderKeyCostants.ArchiveOperator]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveDataTag))
            { _archiveInfo.DataTag = _infoDictionary[HeaderKeyCostants.ArchiveDataTag]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveLocation))
            { _archiveInfo.Location = _infoDictionary[HeaderKeyCostants.ArchiveLocation]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveComment))
            { _archiveInfo.Comment = _infoDictionary[HeaderKeyCostants.ArchiveComment]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveDateTime))
            {
                DateTime parsedDateTimeValue;
                if (DateTime.TryParse(_infoDictionary[HeaderKeyCostants.ArchiveDateTime], out parsedDateTimeValue))
                { _archiveInfo.DateTime = parsedDateTimeValue; }
            }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveLatitude))
            {
                try
                {
                    double latitude = Convert.ToDouble(_infoDictionary[HeaderKeyCostants.ArchiveLatitude]);
                    double longitude = Convert.ToDouble(_infoDictionary[HeaderKeyCostants.ArchiveLongitude]);
                    double altitude = Convert.ToDouble(_infoDictionary[HeaderKeyCostants.ArchiveAltitude]);
                    _archiveInfo.Coordinate = new GeoCoordinate(latitude, longitude, altitude);
                }
                catch
                {
                    // If the coordinate values are invalid (e.g. empty or null string).
                    _archiveInfo.Coordinate = new GeoCoordinate(0, 0, 0);
                }

            }

            #endregion

            #region --------Extract "Device" information from INI Dictionary -------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceReceiverModel))
            { _deviceInfo.ReceiverModel = _infoDictionary[HeaderKeyCostants.DeviceReceiverModel]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceReceiverSerialNumber))
            { _deviceInfo.ReceiverSerialNumber = _infoDictionary[HeaderKeyCostants.DeviceReceiverSerialNumber]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceAntennaModel))
            { _deviceInfo.AntennalModel = _infoDictionary[HeaderKeyCostants.DeviceAntennaModel]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceAntennaSerialNumber))
            { _deviceInfo.AntennalSerialNumber = _infoDictionary[HeaderKeyCostants.DeviceAntennaSerialNumber]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceFrontendModel))
            { _deviceInfo.FrontendModel = _infoDictionary[HeaderKeyCostants.DeviceFrontendModel]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceFrontendSerialNumber))
            { _deviceInfo.FrontendSerialNumber = _infoDictionary[HeaderKeyCostants.DeviceFrontendSerialNumber]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceComment))
            { _deviceInfo.Comment = _infoDictionary[HeaderKeyCostants.DeviceComment]; }

            #endregion

            #region --------Extract "Storage" information from INI Dictionary -------------------

            // NOTE:  "Storage.FileHeaderSize" is not included in file header INI string,
            // actually the "File Header Size" will be known and returned during the "LoadDataInfoFromFile()" operation.
            // So the "Storage.FileHeaderSize" is not updated here.

            // Read "number of channels".
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageNumberOfChannels))
            { _storageInfo.NumberOfChannels = (int)(Convert.ToDouble(_infoDictionary[HeaderKeyCostants.StorageNumberOfChannels])); }

            // Read "file format" string and parse string to enum.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageFileFormat))
            {
                string fileFormatRawStr = _infoDictionary[HeaderKeyCostants.StorageFileFormat];
                var fileFormatEnum = FileFormat.FixFrequencyStream;

                if (fileFormatRawStr == HeaderValueConstants.FileFormatFixFreqFrame) { fileFormatEnum = FileFormat.FixFrequencyFrame; }
                else if (fileFormatRawStr == HeaderValueConstants.FileFormatFreqScanIQ) { fileFormatEnum = FileFormat.FrequencyScanIQ; }
                else if (fileFormatRawStr == HeaderValueConstants.FileFormatFixFreqStream) { fileFormatEnum = FileFormat.FixFrequencyStream; }
                else if (fileFormatRawStr == HeaderValueConstants.FileFormatSpectrum) { fileFormatEnum = FileFormat.Spectrum; }
                else { throw new NotImplementedException("File Format : " + fileFormatRawStr); }

                _storageInfo.FileFormat = fileFormatEnum;
            }
            else
            {
                // "File Format" key not found, this is an old version vector file. Use "Fix Block Size" to determine file format.
                if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageFrameLength))
                {
                    int frameLength = (int)(Convert.ToDouble(_infoDictionary[HeaderKeyCostants.StorageFrameLength]));
                    if (frameLength > 0) { _storageInfo.FileFormat = FileFormat.FixFrequencyFrame; }
                    else { _storageInfo.FileFormat = FileFormat.FixFrequencyStream; }
                }
            }

            // Read "byte order" string and parse string to enum.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageByteOrder))
            {
                string byteOrderRawStr = _infoDictionary[HeaderKeyCostants.StorageByteOrder];
                var byteOrderEnum = ByteOrder.LittleEndian;

                if (byteOrderRawStr == HeaderValueConstants.ByteOrderBigEndian) { byteOrderEnum = ByteOrder.BigEndian; }
                else { byteOrderEnum = ByteOrder.LittleEndian; }

                _storageInfo.ByteOrder = byteOrderEnum;
            }

            // Read "data type" and "bytes per sample" to determine data type in C#.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageDataType)
                && _infoDictionary.ContainsKey(HeaderKeyCostants.StorageBytesPerSample))
            {

                // Read "bytes per sample" and convert to "bytes per sample" of one channel.
                int bytesPerSample = (int)(Convert.ToDouble(_infoDictionary[HeaderKeyCostants.StorageBytesPerSample]));
                int bytesPerPoint = bytesPerSample / _storageInfo.NumberOfChannels;

                string dataTypeRawStr = _infoDictionary[HeaderKeyCostants.StorageDataType];
                var dataTypeEnum = DataType.ComplexI16;

                #region --------Parse data type raw string to Storage.DataType enum-------------------

                if (dataTypeRawStr == HeaderValueConstants.DataTypeReal)
                {
                    // Real data type.
                    switch (bytesPerPoint)
                    {
                        case BYTES_OF_REAL_I8: { dataTypeEnum = DataType.RealI8; break; }
                        case BYTES_OF_REAL_I16: { dataTypeEnum = DataType.RealI16; break; }
                        case BYTES_OF_REAL_F32: { dataTypeEnum = DataType.RealF32; break; }
                        case BYTES_OF_REAL_D64: { dataTypeEnum = DataType.RealD64; break; }
                        default: { throw new NotImplementedException("Bytes per data point : " + bytesPerPoint.ToString()); }
                    }
                }
                else
                {
                    // Complex data type, IQ interleaved.
                    switch (bytesPerPoint)
                    {
                        case BYTES_OF_COMPLEX_I8: { dataTypeEnum = DataType.ComplexI8; break; }
                        case BYTES_OF_COMPLEX_I16: { dataTypeEnum = DataType.ComplexI16; break; }
                        default: { throw new NotImplementedException("Bytes per data point : " + bytesPerPoint.ToString()); }
                    }
                }

                #endregion

                _storageInfo.DataType = dataTypeEnum;
            }

            #endregion

            #region --------Extract "Reserved" information from INI Dictionary -------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedDouble1))
            { _reservedInfo.Double1 = Convert.ToDouble(_infoDictionary[HeaderKeyCostants.ReservedDouble1]); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedDouble2))
            { _reservedInfo.Double2 = Convert.ToDouble(_infoDictionary[HeaderKeyCostants.ReservedDouble2]); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedDouble3))
            { _reservedInfo.Double3 = Convert.ToDouble(_infoDictionary[HeaderKeyCostants.ReservedDouble3]); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedDouble4))
            { _reservedInfo.Double4 = Convert.ToDouble(_infoDictionary[HeaderKeyCostants.ReservedDouble4]); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedString1))
            { _reservedInfo.String1 = _infoDictionary[HeaderKeyCostants.ReservedString1]; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedString2))
            { _reservedInfo.String2 = _infoDictionary[HeaderKeyCostants.ReservedString2]; }

            #endregion

        }

        /// <summary>
        /// Update data information dictionary according to this.Public Properties (Maker/Archive/Device/Storage/Reserved).
        /// </summary>
        private void UpdateDataInfoToDictionary()
        {

            #region --------Update "Maker" information to INI Dictionary -------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.MakerVersion))
            { _infoDictionary[HeaderKeyCostants.MakerVersion] = _makerInfo.Version; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.MakerVersion, _makerInfo.Version); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.MakerSoftware))
            { _infoDictionary[HeaderKeyCostants.MakerSoftware] = _makerInfo.Software; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.MakerSoftware, _makerInfo.Software); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.MakerProducer))
            { _infoDictionary[HeaderKeyCostants.MakerProducer] = _makerInfo.Producer; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.MakerProducer, _makerInfo.Producer); }

            #endregion

            #region --------Update "Archive" information to INI Dictionary -------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveStationID))
            { _infoDictionary[HeaderKeyCostants.ArchiveStationID] = _archiveInfo.StationID; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ArchiveStationID, _archiveInfo.StationID); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveOperator))
            { _infoDictionary[HeaderKeyCostants.ArchiveOperator] = _archiveInfo.Operator; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ArchiveOperator, _archiveInfo.Operator); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveDataTag))
            { _infoDictionary[HeaderKeyCostants.ArchiveDataTag] = _archiveInfo.DataTag; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ArchiveDataTag, _archiveInfo.DataTag); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveLocation))
            { _infoDictionary[HeaderKeyCostants.ArchiveLocation] = _archiveInfo.Location; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ArchiveLocation, _archiveInfo.Location); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveComment))
            { _infoDictionary[HeaderKeyCostants.ArchiveComment] = _archiveInfo.Comment; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ArchiveComment, _archiveInfo.Comment); }

            // Format date time to string and update information dictionary.
            string dateTimeString = _archiveInfo.DateTime.ToString(HeaderValueConstants.ArchiveDateTimeFormatStr);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveDateTime))
            { _infoDictionary[HeaderKeyCostants.ArchiveDateTime] = dateTimeString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ArchiveDateTime, dateTimeString); }

            // Format coordinate to string and update information dictionary.
            string coordFormatStr = "f12";
            // If coordinate is invalid, use (0, 0, 0).
            if (_archiveInfo.Coordinate.IsUnknown) { _archiveInfo.Coordinate = new GeoCoordinate(0, 0, 0); }

            string latitudeString = _archiveInfo.Coordinate.Latitude.ToString(coordFormatStr);
            string longitudeString = _archiveInfo.Coordinate.Longitude.ToString(coordFormatStr);
            string altitudeString = _archiveInfo.Coordinate.Altitude.ToString(coordFormatStr);

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveLatitude))
            { _infoDictionary[HeaderKeyCostants.ArchiveLatitude] = latitudeString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ArchiveLatitude, latitudeString); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveLongitude))
            { _infoDictionary[HeaderKeyCostants.ArchiveLongitude] = longitudeString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ArchiveLongitude, longitudeString); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ArchiveAltitude))
            { _infoDictionary[HeaderKeyCostants.ArchiveAltitude] = altitudeString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ArchiveAltitude, altitudeString); }

            #endregion

            #region --------Update "Device" information to INI Dictionary -------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceReceiverModel))
            { _infoDictionary[HeaderKeyCostants.DeviceReceiverModel] = _deviceInfo.ReceiverModel; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.DeviceReceiverModel, _deviceInfo.ReceiverModel); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceReceiverSerialNumber))
            { _infoDictionary[HeaderKeyCostants.DeviceReceiverSerialNumber] = _deviceInfo.ReceiverSerialNumber; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.DeviceReceiverSerialNumber, _deviceInfo.ReceiverSerialNumber); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceAntennaModel))
            { _infoDictionary[HeaderKeyCostants.DeviceAntennaModel] = _deviceInfo.AntennalModel; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.DeviceAntennaModel, _deviceInfo.AntennalModel); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceAntennaSerialNumber))
            { _infoDictionary[HeaderKeyCostants.DeviceAntennaSerialNumber] = _deviceInfo.AntennalSerialNumber; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.DeviceAntennaSerialNumber, _deviceInfo.AntennalSerialNumber); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceFrontendModel))
            { _infoDictionary[HeaderKeyCostants.DeviceFrontendModel] = _deviceInfo.FrontendModel; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.DeviceFrontendModel, _deviceInfo.FrontendModel); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceFrontendSerialNumber))
            { _infoDictionary[HeaderKeyCostants.DeviceFrontendSerialNumber] = _deviceInfo.FrontendSerialNumber; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.DeviceFrontendSerialNumber, _deviceInfo.FrontendSerialNumber); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.DeviceComment))
            { _infoDictionary[HeaderKeyCostants.DeviceComment] = _deviceInfo.Comment; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.DeviceComment, _deviceInfo.Comment); }


            #endregion

            #region --------Update "Storage" information to INI Dictionary -------------------

            // Format "Storage.FileFormat" enum to string and update information dictionary.
            string fileFormatInStr = HeaderValueConstants.FileFormatFixFreqStream;

            #region --------Format Storage.FileFormat enum to string -------------------

            switch (_storageInfo.FileFormat)
            {
                case FileFormat.FixFrequencyStream: { fileFormatInStr = HeaderValueConstants.FileFormatFixFreqStream; break; }
                case FileFormat.FixFrequencyFrame: { fileFormatInStr = HeaderValueConstants.FileFormatFixFreqFrame; break; }
                case FileFormat.FrequencyScanIQ: { fileFormatInStr = HeaderValueConstants.FileFormatFreqScanIQ; break; }
                case FileFormat.Spectrum: { fileFormatInStr = HeaderValueConstants.FileFormatSpectrum; break; }
            }

            #endregion

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageFileFormat))
            { _infoDictionary[HeaderKeyCostants.StorageFileFormat] = fileFormatInStr; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.StorageFileFormat, fileFormatInStr); }

            // Format "Storage.NumberOfChannels" to string and update information dictionary.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageNumberOfChannels))
            { _infoDictionary[HeaderKeyCostants.StorageNumberOfChannels] = _storageInfo.NumberOfChannels.ToString(); }
            else
            { _infoDictionary.Add(HeaderKeyCostants.StorageNumberOfChannels, _storageInfo.NumberOfChannels.ToString()); }

            // Format "Storage.DataType" enum to string and update information dictionary.
            string dataTypeInStr = HeaderValueConstants.DataTypeComplex;

            #region --------Format Storage.DataType enum to string -------------------

            switch (_storageInfo.DataType)
            {
                case DataType.RealI8:
                case DataType.RealI16:
                case DataType.RealF32:
                case DataType.RealD64:
                    {
                        dataTypeInStr = HeaderValueConstants.DataTypeReal;
                        break;
                    }
                case DataType.ComplexI8:
                case DataType.ComplexI16:
                    {
                        dataTypeInStr = HeaderValueConstants.DataTypeComplex;
                        break;
                    }
                default:
                    {
                        // 若为未处理的数据类型，说明实现时有遗漏，抛出异常。
                        throw new NotImplementedException(_storageInfo.DataType.ToString());
                    }
            }

            #endregion

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageDataType))
            { _infoDictionary[HeaderKeyCostants.StorageDataType] = dataTypeInStr; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.StorageDataType, dataTypeInStr); }

            // Calculate "BytesPerSample" according to "Storage.DataType" enum and "Storage.NumberOfChannels",
            // then format to string and update information dictionary.
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageBytesPerSample))
            { _infoDictionary[HeaderKeyCostants.StorageBytesPerSample] = this.BytesPerSample.ToString(); }
            else
            { _infoDictionary.Add(HeaderKeyCostants.StorageBytesPerSample, this.BytesPerSample.ToString()); }

            // Format "Storage.ByteOrder" enum to string and update information dictionary.
            string byteOrderInStr;
            if (_storageInfo.ByteOrder == ByteOrder.BigEndian)
            { byteOrderInStr = HeaderValueConstants.ByteOrderBigEndian; }
            else
            { byteOrderInStr = HeaderValueConstants.ByteOrderLittleEndian; }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.StorageByteOrder))
            { _infoDictionary[HeaderKeyCostants.StorageByteOrder] = byteOrderInStr; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.StorageByteOrder, byteOrderInStr); }

            #endregion

            #region --------Update "Reserved" information to INI Dictionary -------------------

            string reservedDblFormat = "f12";

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedDouble1))
            { _infoDictionary[HeaderKeyCostants.ReservedDouble1] = _reservedInfo.Double1.ToString(reservedDblFormat); }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ReservedDouble1, _reservedInfo.Double1.ToString(reservedDblFormat)); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedDouble2))
            { _infoDictionary[HeaderKeyCostants.ReservedDouble2] = _reservedInfo.Double2.ToString(reservedDblFormat); }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ReservedDouble2, _reservedInfo.Double2.ToString(reservedDblFormat)); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedDouble3))
            { _infoDictionary[HeaderKeyCostants.ReservedDouble3] = _reservedInfo.Double3.ToString(reservedDblFormat); }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ReservedDouble3, _reservedInfo.Double3.ToString(reservedDblFormat)); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedDouble4))
            { _infoDictionary[HeaderKeyCostants.ReservedDouble4] = _reservedInfo.Double4.ToString(reservedDblFormat); }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ReservedDouble4, _reservedInfo.Double4.ToString(reservedDblFormat)); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedString1))
            { _infoDictionary[HeaderKeyCostants.ReservedString1] = _reservedInfo.String1; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ReservedString1, _reservedInfo.String1); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.ReservedString2))
            { _infoDictionary[HeaderKeyCostants.ReservedString2] = _reservedInfo.String2; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.ReservedString2, _reservedInfo.String2); }

            #endregion

        }

        /// <summary>
        /// Formating data information dictionary to Vector File Header Format and write to file.
        /// </summary>
        private void WriteDataInfoToFileHeader()
        {
            int i;
            string headerInfoString;
            byte[] fullFileHeadData;

            #region --------Convert INI Dictionary to flattened INI string -------------------

            // Generate a random number used for generating temp file name.
            string randomString = Environment.TickCount.ToString() + "_" + (new Random()).Next().ToString();
            //string tempIniFilePath = Path.GetTempPath() + "VectorFile" + randomString + ".ini";

            // Remove the temperory INI file to create new INI file.
            //File.Delete(tempIniFilePath);

            // Write INI dictionary to INI file.
            var iniFile = new IniStreamHandler("");
            foreach (var item in _infoDictionary) { iniFile.WriteKey(HEADER_SECTION_NAME, item.Key, item.Value); }

            // Read the INI file as raw ASCII string, and append a new line starting with ";" which means a comment line.
            headerInfoString = iniFile.GetAllText();

            // Remove the temperory INI file.
            //File.Delete(tempIniFilePath);

            #endregion

            #region ----------Formating header string into Byte[] for writing to file-----------

            int buffUpdatePosition = 0;
            byte[] bufferForI32;

            // Pre-Allocated byte[] with size of Storage.HeaderSize, fill with space.
            fullFileHeadData = new byte[_storageInfo.FileHeaderSize];
            for (i = 0; i < fullFileHeadData.Length; i++) { fullFileHeadData[i] = 0x20; }

            // Update the first 4 bytes indicating Tag size.
            bufferForI32 = BitConverter.GetBytes((int)VECTORFILE_HEADER_TAG.Length);
            Buffer.BlockCopy(bufferForI32, 0, fullFileHeadData, 0, bufferForI32.Length);
            buffUpdatePosition = bufferForI32.Length;

            // Update Tag content.
            byte[] tagInByteArray = Encoding.UTF8.GetBytes(VECTORFILE_HEADER_TAG);
            Buffer.BlockCopy(tagInByteArray, 0, fullFileHeadData, buffUpdatePosition, tagInByteArray.Length);
            buffUpdatePosition += tagInByteArray.Length;

            // Update the next 4 bytes indicating size of rest header.
            bufferForI32 = BitConverter.GetBytes((int)(fullFileHeadData.Length - buffUpdatePosition - sizeof(int)));
            Buffer.BlockCopy(bufferForI32, 0, fullFileHeadData, buffUpdatePosition, bufferForI32.Length);
            buffUpdatePosition += bufferForI32.Length;

            // Update information INI string, if size of information is larger than header size, throw an exception.
            byte[] infoInByteArray = Encoding.UTF8.GetBytes(headerInfoString);
            if (fullFileHeadData.Length < buffUpdatePosition + infoInByteArray.Length)
            { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Header Out of Size."); }
            Buffer.BlockCopy(infoInByteArray, 0, fullFileHeadData, buffUpdatePosition, infoInByteArray.Length);

            #endregion

            // Call FileStream or KernelFileStream (according to input "disableBuffering") to write file.
            if (_disableBuffering)
            {
                _kernelFileStream.Seek(0, SeekOrigin.Begin);

                IntPtr buffer = Marshal.AllocHGlobal(fullFileHeadData.Length);
                try
                {
                    Marshal.Copy(fullFileHeadData, 0, buffer, fullFileHeadData.Length);
                    _kernelFileStream.Write(buffer, fullFileHeadData.Length);
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }

            }
            else
            {
                _nativeFileStream.Seek(0, SeekOrigin.Begin);
                _nativeFileStream.Write(fullFileHeadData, 0, fullFileHeadData.Length);
            }

        }

        /// <summary> 
        /// Converts spreadsheet string with specified separator into a double array. 
        /// </summary> 
        /// <param name="spreadsheetString">String data to convert.</param> 
        /// <param name="separator">Separator used to separate fields in the spreadsheet string.</param> 
        internal double[] ConvertSpreadsheetStringToArrayDbl(string spreadsheetString, string separator)
        {

            if (string.IsNullOrEmpty(spreadsheetString)) { return null; }

            if (separator == null)
            { throw new ArgumentNullException("Delimiter could not be empty"); }

            // Split spreadsheet string to string array.
            string[] stringArray = spreadsheetString.Split(new string[] { separator }, StringSplitOptions.None);

            if (stringArray == null || stringArray.Length == 0) { return null; }

            // Convert string array to numeric array.
            double[] data = new double[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++) { data[i] = Convert.ToDouble(stringArray[i]); }

            return data;
        }

        /// <summary>
        /// Converts a double array into spreadsheet string with specified separator. 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="format"></param>
        /// <param name="separator">Separator used to separate fields in the spreadsheet string.</param>
        /// <returns></returns>
        internal string ConvertArrayToSpreadsheetString(double[] data, string format, string separator)
        {

            if (data == null || data.Length == 0) { return null; }

            if (separator == null)
            { throw new ArgumentNullException("Delimiter could not be empty"); }

            // Convert double array to string array.
            string[] stringArray = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            { stringArray[i] = data[i].ToString(format); }

            return string.Join(separator, stringArray);
        }

        #endregion

    }

    #region ----------Data Types and Structures----------

    /// <summary>
    /// 文件格式和数据格式信息
    /// </summary>
    public class StorageInformation
    {
        /// <summary>
        /// 文件格式，FixFreqStream/FixFreRecord/ScanRecord
        /// </summary>
        public FileFormat FileFormat { get; set; }

        /// <summary>
        /// 通道数
        /// </summary>
        public int NumberOfChannels { get; set; }       

        /// <summary>
        /// 数据类型，Complex/Real，I8/I16/DBL
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// 字节序，Little Endian/Big Endian
        /// </summary>
        public ByteOrder ByteOrder { get; set; }

        private int _fileHeaderSize;
        /// <summary>
        /// 文件头长度，字节数
        /// </summary>
        public int FileHeaderSize
        {
            get { return _fileHeaderSize; }
            set
            {
                if (value > 0)
                {
                    // Set file header size to multiple of VECTORFILE_HEADER_QUANTUM constant.
                    int multiplier = value / VectorFile.VECTORFILE_HEADER_QUANTUM;
                    multiplier += ((value % VectorFile.VECTORFILE_HEADER_QUANTUM) == 0) ? 0 : 1;
                    _fileHeaderSize = multiplier * VectorFile.VECTORFILE_HEADER_QUANTUM;
                }
                else
                { throw new ArgumentOutOfRangeException("File header size must greater than 0."); }
            }
        }

        #region ------------------- Constructor -------------------

        /// <summary>
        /// Constructor, initializing default values.
        /// </summary>
        public StorageInformation()
        {
            this.FileFormat = FileFormat.FixFrequencyStream;
            this.NumberOfChannels = 1;
            this.DataType = DataType.ComplexI16;
            this.ByteOrder = ByteOrder.LittleEndian;
            this._fileHeaderSize = VectorFile.VECTORFILE_HEADER_QUANTUM;
        }

        #endregion

    }    
    
    /// <summary>
    /// Maker information includes file version, software and producer.
    /// </summary>
    public class MakerInformation
    {
        /// <summary>
        /// VectorFile版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 生成该数据文件的软件
        /// </summary>
        public string Software { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Producer { get; set; }

        #region ------------------- Constructor -------------------

        /// <summary>
        /// Constructor will initialize "Version".
        /// </summary>
        public MakerInformation()
        {
            this.Version = VectorFile.VECTORFILE_VERSION;
        }

        #endregion

    }

    /// <summary>
    /// Archive information including date time, location, data source, tag and comment.
    /// </summary>
    public class ArchiveInformation
    {
        /// <summary>
        /// 数据来源的设备或站点ID
        /// </summary>
        public string StationID { get; set; }

        /// <summary>
        /// 数据来源的设备操作员ID
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// DataTag
        /// </summary>
        public string DataTag { get; set; }

        /// <summary>
        /// 数据起始点的时间
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Location description.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Geographical coordinate of data source.
        /// </summary>
        public GeoCoordinate Coordinate { get; set; }

        /// <summary>
        /// 可用于描述数据备注
        /// </summary>
        public string Comment { get; set; }

        #region ------------------- Constructor -------------------

        /// <summary>
        /// 归档信息，默认构造函数，will initialize "DataStartTime" and "Coordinate"。
        /// </summary>
        public ArchiveInformation()
        {
            this.DateTime = DateTime.Now;
            this.Coordinate = new GeoCoordinate(0, 0, 0);
        }

        #endregion

    }

    /// <summary>
    /// Information of acquisition device, used when the data is acquired by device.
    /// </summary>
    public class DeviceInformation
    {
        /// <summary>
        /// 接收设备model
        /// </summary>
        public string ReceiverModel { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public string ReceiverSerialNumber { get; set; }

        /// <summary>
        /// 接收天线model
        /// </summary>
        public string AntennalModel { get; set; }

        /// <summary>
        /// 接收天线序列号
        /// </summary>
        public string AntennalSerialNumber { get; set; }

        /// <summary>
        /// 接收前端model
        /// </summary>
        public string FrontendModel { get; set; }

        /// <summary>
        /// 接收前端序列号
        /// </summary>
        public string FrontendSerialNumber { get; set; }

        /// <summary>
        /// 可用于描述设备备注
        /// </summary>
        public string Comment { get; set; }

    }

    /// <summary>
    /// 地理信息（经度，维度和高度）
    /// </summary>
    public class GeoCoordinate
    {
        private double _latitude;
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private double _longititude;
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude
        {
            get { return _longititude; }
            set { _longititude = value; }
        }

        private double _altitude;
        /// <summary>
        /// 高度
        /// </summary>
        public double Altitude
        {
            get { return _altitude; }
            set { _altitude = value; }
        }

        /// <summary>
        /// 含有未知经度，纬度和高度的数据
        /// </summary>
        public bool IsUnknown
        {
            get
            {
                // 判断经纬度是否在范围内。
                if (_latitude < 0 || _latitude > 90 || _longititude < 0 || _longititude > 180) { return true; }
                else { return false; }
            }
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="altitude"></param>
        public GeoCoordinate(double latitude, double longitude, double altitude)
        {
            _latitude = latitude;
            _longititude = longitude;
            _altitude = altitude;
        }
    }

    /// <summary>
    /// 预留
    /// </summary>
    public class ReservedInformation
    {

        /// <summary>
        /// 浮点数1
        /// </summary>
        public double Double1 { get; set; }

        /// <summary>
        /// 浮点数2
        /// </summary>
        public double Double2 { get; set; }

        /// <summary>
        /// 浮点数3
        /// </summary>
        public double Double3 { get; set; }

        /// <summary>
        /// 浮点数4
        /// </summary>
        public double Double4 { get; set; }

        /// <summary>
        /// 字符串1
        /// </summary>
        public string String1 { get; set; }

        /// <summary>
        /// 字符串2
        /// </summary>
        public string String2 { get; set; }

    }

    /// <summary>
    /// 文件格式
    /// </summary>
    public enum FileFormat
    {
        /// <summary>
        /// Fix Frequency Stream, data is continuous.
        /// </summary>
        FixFrequencyStream,

        /// <summary>
        /// Fix Frequency Records, data is by frame.
        /// </summary>
        FixFrequencyFrame,

        /// <summary>
        /// Scan Records, data is by frame and each frame corresponds to index of a frequency sequence.
        /// </summary>
        FrequencyScanIQ,

        /// <summary>
        /// Multi-band Spectrum.
        /// </summary>
        Spectrum

    }

    /// <summary>
    /// 数据格式
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// Raw complex data, interleaved I/Q, 8-bit. 
        /// </summary>
        ComplexI8,

        /// <summary>
        /// Raw complex data, interleaved I/Q, 16-bit. 
        /// </summary>
        ComplexI16,

        /// <summary>
        /// Raw ADC data, 8-bit
        /// </summary>
        RealI8,

        /// <summary>
        /// Raw ADC data, 16-bit
        /// </summary>
        RealI16,

        /// <summary>
        /// 实数单精度浮点数，32-bit
        /// </summary>
        RealF32,

        /// <summary>
        /// 实数双精度浮点数，64-bit
        /// </summary>
        RealD64,

    }

    /// <summary>
    /// 字节序
    /// </summary>
    public enum ByteOrder
    {
        /// <summary>
        /// 将低序字节存储在起始地址
        /// </summary>
        LittleEndian,

        /// <summary>
        /// 将高序字节存储在起始地址
        /// </summary>
        BigEndian
    }

    /// <summary>
    /// 基础采样信息。
    /// </summary>
    public class BaseChannelSamplingInfo
    {

        /// <summary>
        /// 射频中心频率，以Hz为单位
        /// </summary>
        public double RFFrequency { get; set; }

        /// <summary>
        /// 带宽，以Hz为单位
        /// </summary>
        public double Bandwidth { get; set; }

        /// <summary>
        /// 参考电平，以dBm为单位
        /// </summary>
        public double ReferenceLevel { get; set; }

        /// <summary>
        /// 中频频率，以Hz为单位
        /// </summary>
        public double IFFrequency { get; set; }

        private double _rfScaleFactor;
        /// <summary>
        /// RF链路补偿因子，线性（非dB），用于与raw数据相乘得到电压值。
        /// </summary>
        public double RFScaleFactor
        {
            get { return _rfScaleFactor; }
            set
            {
                if (value != 0) { _rfScaleFactor = value; }
                else { throw new ArgumentException("Scale factor could NOT be 0."); }
            }

        }

        private double _digitizerScaleFactor;
        /// <summary>
        /// 数字化仪补偿因子，线性（非dB），用于与raw数据相乘得到电压值。
        /// </summary>
        public double DigitizerScaleFactor
        {
            get { return _digitizerScaleFactor; }
            set
            {
                if (value != 0) { _digitizerScaleFactor = value; }
                else { throw new ArgumentException("Scale factor could NOT be 0."); }
            }

        }

        #region ------------------- Constructor -------------------

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseChannelSamplingInfo()
        {
            _rfScaleFactor = 1;
            _digitizerScaleFactor = 1;
        }

        #endregion

        #region ------------------- Public Method -------------------

        /// <summary>
        /// Return total scale factor, which is calculated by "RFScaleFactor * DigitizerScaleFactor".
        /// </summary>
        /// <returns></returns>
        public double GetScaleFactor () { return _rfScaleFactor * _digitizerScaleFactor; }

        #endregion
    }

    /// <summary>
    ///  A flexible IntPtr whose size will automatically increase when property "LeastLength" is set, 
    ///  or when Copy() is called and input data size is larger the current length.
    /// </summary>
    internal class FlexInteropPtr
    {
        #region --------Private Fields ---------------

        /// <summary>
        /// Actual buffer length in Bytes.
        /// </summary>
        private int _bufferLength;

        #endregion

        #region --------Public Property ---------------

        /// <summary>
        /// Least buffer length in Bytes.
        /// </summary>
        public int LeastLength
        {
            set
            {
                if (value <= 0) { throw new ArgumentException("Buffer length must be greater than 0."); }
                ReAllocFlexIntPtr(value);
            }
        }

        private IntPtr _flexIntPtr;
        /// <summary>
        /// The unmanaged memory handle.
        /// </summary>
        public IntPtr FlexIntPtr { get { return _flexIntPtr; } }

        #endregion

        #region --------Constructor and Destructor ---------------

        /// <summary>
        ///  Constructor to create a IntPtr with default length.
        /// </summary>
        public FlexInteropPtr()
        {
            _bufferLength = 1024;
            _flexIntPtr = Marshal.AllocHGlobal(_bufferLength);
        }

        /// <summary>
        /// Destructor to free allocated unmanaged memory.
        /// </summary>
        ~FlexInteropPtr()
        {
            if (_flexIntPtr != IntPtr.Zero) { Marshal.FreeHGlobal(_flexIntPtr); }
        }

        #endregion

        #region --------Public Methods ---------------

        /// <summary>
        /// 将一维的托管 8 位无符号整数数组中的数据复制到内部的非托管内存指针FlexInptr。
        /// </summary>
        /// <param name="source">从中进行复制的一维数组。</param>
        /// <param name="startIndex">源数组中复制起始位置的索引（从零开始）。</param>
        /// <param name="length">要复制的数组元素的数目。</param>
        public void Copy(byte[] source, int startIndex, int length)
        {
            // Re-Alloc unmanaged memory if neccessory and perform copy operation.
            ReAllocFlexIntPtr(length);
            Marshal.Copy(source, startIndex, _flexIntPtr, length);
        }

        /// <summary>
        /// 将一维的托管 16 位有符号整数数组中的数据复制到内部的非托管内存指针FlexInptr。
        /// </summary>
        /// <param name="source">从中进行复制的一维数组。</param>
        /// <param name="startIndex">源数组中复制起始位置的索引（从零开始）。</param>
        /// <param name="length">要复制的数组元素的数目。</param>
        public void Copy(short[] source, int startIndex, int length)
        {
            // Re-Alloc unmanaged memory if neccessory and perform copy operation.
            ReAllocFlexIntPtr(length * sizeof(short));
            Marshal.Copy(source, startIndex, _flexIntPtr, length);
        }

        /// <summary>
        /// 将一维的托管 32 位单精度浮点数组中的数据复制到内部的非托管内存指针FlexInptr。
        /// </summary>
        /// <param name="source">从中进行复制的一维数组。</param>
        /// <param name="startIndex">源数组中复制起始位置的索引（从零开始）。</param>
        /// <param name="length">要复制的数组元素的数目。</param>
        public void Copy(float[] source, int startIndex, int length)
        {
            // Re-Alloc unmanaged memory if neccessory and perform copy operation.
            ReAllocFlexIntPtr(length * sizeof(float));
            Marshal.Copy(source, startIndex, _flexIntPtr, length);
        }

        /// <summary>
        /// 将一维的托管 64 位双精度浮点数组中的数据复制到内部的非托管内存指针FlexInptr。
        /// </summary>
        /// <param name="source">从中进行复制的一维数组。</param>
        /// <param name="startIndex">源数组中复制起始位置的索引（从零开始）。</param>
        /// <param name="length">要复制的数组元素的数目。</param>
        public void Copy(double[] source, int startIndex, int length)
        {
            // Re-Alloc unmanaged memory if neccessory and perform copy operation.
            ReAllocFlexIntPtr(length * sizeof(double));
            Marshal.Copy(source, startIndex, _flexIntPtr, length);
        }

        #endregion

        #region --------Private Methods ---------------

        private void ReAllocFlexIntPtr(int leastLengthInBytes)
        {
            // Re-allocate unmanaged memory if required size if larger than current length.
            if (_bufferLength < leastLengthInBytes)
            {
                Marshal.FreeHGlobal(_flexIntPtr);
                _bufferLength = leastLengthInBytes;
                _flexIntPtr = Marshal.AllocHGlobal(_bufferLength);
            }
        }

        #endregion
    }

    /// <summary>
    ///  A flexible byte[] whose size will automatically increase when property "LeastLength" is set, 
    ///  or when Copy() is called and input data size is larger the current length.
    /// </summary>
    internal class FlexByteArray
    {
        #region --------Private Fields ---------------

        /// <summary>
        /// Actual buffer length in Bytes.
        /// </summary>
        private int _length;

        #endregion

        #region --------Public Property ---------------

        /// <summary>
        /// Least buffer length in Bytes.
        /// </summary>
        public int LeastLength
        {
            set
            {
                if (value <= 0) { throw new ArgumentException("Buffer length must be greater than 0."); }
                ResizeFlexArray(value);
            }
        }

        private byte[] _flexByteArray;
        /// <summary>
        /// The flex array.
        /// </summary>
        public byte[] FlexArray { get { return _flexByteArray; } }

        #endregion

        #region --------Constructor and Destructor ---------------

        /// <summary>
        ///  Constructor to create an array with default length.
        /// </summary>
        public FlexByteArray()
        {
            _length = 1024;
            _flexByteArray = new byte[_length];
        }

        #endregion

        #region --------Public Methods ---------------

        /// <summary>
        /// 将指定元素数目的从起始于特定偏移量的源数组复制到内部托管 8 位无符号整数数组"FlexArray"。
        /// </summary>
        /// <param name="source">从中进行复制的一维数组。</param>
        /// <param name="startIndex">源数组中复制起始位置的索引（从零开始）。</param>
        /// <param name="length">要复制的数组元素的数目。</param>
        public void Copy<T>(T[] source, int startIndex, int length)
        {
            int bytesPerElement = Marshal.SizeOf(source[0]);

            // Resize "_flexByteArray" if neccessory and perform copy operation.
            ResizeFlexArray(length * bytesPerElement);
            Buffer.BlockCopy(source, startIndex * bytesPerElement, _flexByteArray, 0, length * bytesPerElement);
        }

        /// <summary>
        /// 将数据从非托管内存指针复制到内部托管 8 位无符号整数数组"FlexArray"。
        /// </summary>
        /// <param name="source">从中进行复制的数组。</param>
        /// <param name="startIndex">源数组中复制起始位置的索引（从零开始）。</param>
        /// <param name="length">要复制的字节数。</param>
        public void Copy(IntPtr source, int startIndex, int length)
        {
            // Resize "_flexByteArray" if neccessory and perform copy operation.
            ResizeFlexArray(length);
            Marshal.Copy(source, _flexByteArray, startIndex, length);
        }

        #endregion

        #region --------Private Methods ---------------

        private void ResizeFlexArray(int leastLengthInBytes)
        {
            // Resize array if required size if larger than current length.
            if (_length < leastLengthInBytes)
            {
                _length = leastLengthInBytes;
                Array.Resize(ref _flexByteArray, _length);
            }
        }

        #endregion
    }

    #endregion

    #region ----------- Vector File Header INI Keys and Values -----------

    internal sealed class HeaderKeyCostants
    {
        private HeaderKeyCostants() { }

        public static readonly string MakerVersion = "Version";
        public static readonly string MakerSoftware = "Software";
        public static readonly string MakerProducer = "Producer";

        public static readonly string ArchiveStationID = "Station ID";
        public static readonly string ArchiveOperator = "Operator";
        public static readonly string ArchiveDataTag = "Data Tag";
        public static readonly string ArchiveDateTime = "Date Time";
        public static readonly string ArchiveLocation = "Location";
        public static readonly string ArchiveLatitude = "Latitude";
        public static readonly string ArchiveLongitude = "Longitude";
        public static readonly string ArchiveAltitude = "Altitude";
        public static readonly string ArchiveComment = "Comment";

        public static readonly string DeviceReceiverModel = "Receiver Model";
        public static readonly string DeviceReceiverSerialNumber = "Receiver SN";
        public static readonly string DeviceAntennaModel = "Antenna Model";
        public static readonly string DeviceAntennaSerialNumber = "Antenna SN";
        public static readonly string DeviceFrontendModel = "Frontend Model";
        public static readonly string DeviceFrontendSerialNumber = "Frontend SN";
        public static readonly string DeviceComment = "Device Comment";

        public static readonly string StorageFileFormat = "File Format";
        public static readonly string StorageDataType = "Data Type";
        public static readonly string StorageByteOrder = "Byte Order";
        public static readonly string StorageNumberOfChannels = "Number of Channels";
        public static readonly string StorageBytesPerSample = "Bytes per Sample";
        public static readonly string StorageFrameLength = "Fix Block Size";
        public static readonly string StorageFrameHeaderSize = "Record Header Size";

        public static readonly string SamplingSampleRate = "Sample Rate";
        public static readonly string SamplingRFFrequency = "Center Freq";
        public static readonly string SamplingIFFrequency = "Intermediate Freq";
        public static readonly string SamplingBandwidth = "Span";
        public static readonly string SamplingReferenceLevel = "Reference Level";
        public static readonly string SamplingRFScaleFactor = "Tuner Factor";
        public static readonly string SamplingDigitizerScaleFactor = "Digitizer Factor";
        public static readonly string SamplingRecordInterval = "Record Interval";
        public static readonly string SamplingScanIFBandwidth = "Scan IF Bandwidth";
        public static readonly string SamplingScanFrequencyStep = "Scan Fc Step";
        public static readonly string SamplingScanFrameInterval = "Scan Frame Interval";
        public static readonly string SamplingScanBandsInfo = "Scan Bands Info";

        public static readonly string SamplingSpectrumUnit = "Spectrum Unit";
        public static readonly string SamplingSpectrumFrequencyStart = "Spectrum FrequencyStart";
        public static readonly string SamplingSpectrumFrequencyStop = "Spectrum FrequencyStop";
        public static readonly string SamplingSpectrumFrequencyStep = "Spectrum FrequencyStep";
        public static readonly string SamplingSpectrumNumOfSpectralLines = "Spectrum NumOfSpectralLines";
        public static readonly string SamplingSpectrumFrequencyShift = "Spectrum FrequencyShift";
        public static readonly string SamplingSpectrumReferenceLevel = "Spectrum ReferenceLevel";
        public static readonly string SamplingSpectrumNumOfBand = "Spectrum Bands";



        public static readonly string ReservedDouble1 = "Reserved DBL 1";
        public static readonly string ReservedDouble2 = "Reserved DBL 2";
        public static readonly string ReservedDouble3 = "Reserved DBL 3";
        public static readonly string ReservedDouble4 = "Reserved DBL 4";
        public static readonly string ReservedString1 = "Reserved Str 1";
        public static readonly string ReservedString2 = "Reserved Str 2";

    }

    internal sealed class HeaderValueConstants
    {
        private HeaderValueConstants() { }

        public static readonly string ArchiveDateTimeFormatStr = "yyyy-MM-dd HH:mm:ss.fffffff";

        public static readonly string FileFormatFixFreqStream = "FixFreq Stream";
        public static readonly string FileFormatFixFreqFrame = "FixFreq Record";
        public static readonly string FileFormatFreqScanIQ = "Scan Record";
        public static readonly string FileFormatSpectrum = "Spectrum";

        public static readonly string ByteOrderLittleEndian = "Little-Endian";
        public static readonly string ByteOrderBigEndian = "Big-Endian";

        public static readonly string DataTypeComplex = "Interleaved IQ";
        public static readonly string DataTypeReal = "Real";

    }

    #endregion

    #region ----------Exception Definition----------

    /// <summary>
    /// 异常类
    /// </summary>
    public class VectorFileException : ApplicationException
    {
        /// <summary>
        /// 异常错误代码
        /// </summary>
        public ExceptionEnum ErrorCode;

        /// <summary>
        /// 构造函数，指定异常错误代码
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="msg"></param>
        public VectorFileException(ExceptionEnum exceptionType, string msg = null)
            : base(exceptionType.ToString() + ":" + msg, null) 
        {
            this.ErrorCode = exceptionType;
        }
    }

    /// <summary>
    /// 错误代码
    /// </summary>
    public enum ExceptionEnum
    {
        /// <summary>
        /// 无效的文件
        /// </summary>
        InvalidFile,

        /// <summary>
        /// 数据信息冲突
        /// </summary>
        InconsistantFileHeaderInfo,
        
        /// <summary>
        /// 写入或读取的数据类型与文件格式不符
        /// </summary>
        DataTypeConflict,

        /// <summary>
        /// 写入或读取的数据长度与文件格式不符
        /// </summary>
        DataLengthConflict

    }

    #endregion

}
