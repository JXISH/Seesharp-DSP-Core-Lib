using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SeeSharpTools.JXI.FileIO.WavFile
{
    /// <summary>
    /// 音频文件Wav读写类，仅支持Pcm格式。
    /// </summary>
    public class WavFileStream
    {

        #region------------------------- 常量 -------------------------

        internal static readonly ushort AudioRawSampleRate = 48000;
        internal const int BYTES_OF_REAL_I16 = 2;
        internal static readonly ushort AudioBitsPerSample = 16;
        internal static readonly ushort AudioNumberOfChannels = 2;
        internal static readonly int WAVEFILE_HEADER_QUANTUM = 44;

        #endregion

        #region------------------------- 私有成员 -------------------------

        private FileStream _fileStream;
        private bool _fileOpened = false;
        private bool _bNewFile = false;
        private ushort _blockAlign;
        private long _headerLength;
        internal FlexByteArray _nativeFileInteropArray;
        #endregion

        #region------------------------- 公共属性 -------------------------

        private string _fileFullPath;
        /// <summary>
        /// 文件的全路径名，即传递给构造函数的文件名称。
        /// </summary>
        public string Name { get { return _fileFullPath; } }

        private ulong _totalSamples = 0;
        /// <summary>
        /// 文件的总数据长度，in Samples（Pcm数据的总样点数），即各声道（通道）的数据总点数。
        /// </summary>
        public ulong Length { get { return _totalSamples; } }

        private ulong _filePosition = 0;
        /// <summary>
        /// 文件读写指针位置，以采样点数为单位
        /// </summary>
        public ulong Position { get { return _filePosition; } }

        private ushort _bitsPerSamples;
        /// <summary>
        /// PCM数据的采样深度，8/16/24/32 bit等。
        /// </summary>
        /// <returns></returns>
        public ushort BitsPerSample
        {
            get { return _bitsPerSamples; }
            set
            {
                // 必须取值为8的倍数。
                if ((value % 8) != 0) { throw new ArgumentException("Bits per sample must be multiple of 8"); }
                _bitsPerSamples = value;
                _blockAlign = (ushort)(_numOfChannels * _bitsPerSamples / 8);
            }
        }

        private ushort _numOfChannels;
        /// <summary>
        /// PCM数据的通道数。
        /// </summary>
        /// <returns></returns>
        public ushort NumberOfChannels
        {
            get { return _numOfChannels; }
            set
            {
                if (value <= 0) { throw new ArgumentOutOfRangeException(); }
                _numOfChannels = value;
                _blockAlign = (ushort)(_numOfChannels * _bitsPerSamples / 8);
            }
        }

        private double _sampleRate;
        /// <summary>
        /// PCM数据的采样率。
        /// </summary>
        public double SampleRate
        {
            get { return _sampleRate; }
            set
            {
                if (value <= 0) { throw new ArgumentOutOfRangeException(); }
                _sampleRate = value;
            }
        }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 新建或打开一个Wav文件。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mode"></param>
        /// <param name="fileAccess"></param>
        public WavFileStream(string filePath, FileMode mode = FileMode.OpenOrCreate, FileAccess fileAccess = FileAccess.ReadWrite)
            : this(filePath, mode, fileAccess, FileShare.Read){; }

        /// <summary>
        /// 新建或打开一个Wav文件。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mode"></param>
        /// <param name="fileAccess"></param>
        /// <param name="fileShare">确定文件如何由进程共享。</param>
        public WavFileStream(string filePath, FileMode mode, FileAccess fileAccess, FileShare fileShare)
        {
            WaveHeader header;
            _bNewFile = (mode == FileMode.Create || mode == FileMode.CreateNew || mode == FileMode.Truncate
                || (mode == FileMode.OpenOrCreate && !File.Exists(filePath)));
            _fileStream = new FileStream(filePath, mode, fileAccess, fileShare);

            if (_bNewFile)
            {
                header = new WaveHeader(0, AudioNumberOfChannels, AudioRawSampleRate, AudioBitsPerSample);
                // 文件头占位符
                WriteDataInfoToFileHeader(header);
                _totalSamples = 0;
            }
            else
            {
                _totalSamples = (ulong)LoadDataInfoFromFile(_fileStream, out header);
            }

            // 设值默认参数
            _bitsPerSamples = header.WaveFormat.BitsPerSample;
            _numOfChannels = header.WaveFormat.Channels;
            _sampleRate = header.WaveFormat.SamplesPerSec;
            _blockAlign = header.WaveFormat.BlockAlign;
            _headerLength = _fileStream.Position;

            _filePosition = 0;
            _fileOpened = true;
            _filePosition = 0;
            _fileFullPath = filePath;
            _nativeFileInteropArray = new FlexByteArray();

        }

        /// <summary>
        /// 析构函数，关闭已打开的文件。
        /// </summary>
        ~WavFileStream()
        {
            this.Close();
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 写入I16类型PCM数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public void Write(short[] data)
        {
            _nativeFileInteropArray.Copy(data, 0, data.Length);
            _fileStream.Write(_nativeFileInteropArray.FlexArray, 0, _nativeFileInteropArray.FlexArray.Length);
            _filePosition += (ulong)(data.Length / _numOfChannels);
            _totalSamples = Math.Max(_totalSamples, _filePosition);
        }

        /// <summary>
        /// 写入byte[]类型PCM数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public void Write(byte[] data)
        {
            _fileStream.Write(data, 0, data.Length);
            _filePosition += (ulong)(data.Length / (_numOfChannels * _bitsPerSamples / 8));
            _totalSamples = Math.Max(_totalSamples, _filePosition);
        }

        /// <summary>
        /// 读出I16类型PCM数据，若为多通道，则data中的数据为Channel Interleave(通道交织)存放。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Read(short[] data)
        {
            int lengthInBytes = data.Length * sizeof(short);
            _nativeFileInteropArray.LeastLength = lengthInBytes;
            _fileStream.Read(_nativeFileInteropArray.FlexArray, 0, lengthInBytes);
            Buffer.BlockCopy(_nativeFileInteropArray.FlexArray, 0, data, 0, lengthInBytes);
            UpdatePosition();
        }

        /// <summary>
        /// 将文件的当前读写位置设置为给定值。
        /// </summary>
        /// <param name="offset">相对于origin 的Sample数。</param>
        /// <param name="origin">使用 System.IO.SeekOrigin 类型的值，将开始位置、结束位置或当前位置指定为 offset 的参考点。</param>
        /// <returns>文件读写的新位置，距离数据起始点的Sample数，即属性"Position"的值。</returns>
        public void Seek(long offset, SeekOrigin origin)
        {
            long offsetInBytes = 0;

            // Convert input "offset" (in sampls) to "offset in bytes", according to SeekOrigin.
            switch (origin)
            {
                case SeekOrigin.Begin:
                    {
                        // For seeking from Begin, calculate offset in bytes must add Stogage.FileHeaderSize.
                        offsetInBytes = offset * _blockAlign + _headerLength;
                        break;
                    }
                case SeekOrigin.Current:
                case SeekOrigin.End:
                    {
                        // For seeking from Current/End, the offset is "relative", multiply offset by "BytesPerSample" is ok.
                        offsetInBytes = offset * _blockAlign;
                        break;
                    }
            }
            // Call FileStream to seek file .
            _fileStream.Seek(offsetInBytes, origin);
            UpdatePosition();
        }

        /// <summary>
        /// 关闭当前文件并释放与之关联的所有资源（如文件句柄）。
        /// </summary>
        public void Close()
        {
            if (_fileOpened)
            {
                if (_bNewFile)
                    WriteDataInfoToFileHeader(new WaveHeader((uint)_totalSamples, _numOfChannels, (uint)_sampleRate, _bitsPerSamples));
                _fileStream.Close();
                _fileOpened = false;
            }
        }

        #endregion

        #region------------------------- 私有方法 -------------------------
        private void UpdatePosition()
        {
            _filePosition = (ulong)((_fileStream.Position - _headerLength) / _blockAlign);
        }

        private int LoadDataInfoFromFile(FileStream fileHandle, out WaveHeader header)
        {
            header = new WaveHeader(0);
            int startIndex;
            int readSize;
            byte[] bufferForBytes = new byte[128];

            startIndex = 0;
            readSize = fileHandle.Read(bufferForBytes, 0, 12);
            if (readSize != 12)
                return -1;
            // Chunk ID: RIFF
            header.WaveFile.FileID = Encoding.UTF8.GetString(bufferForBytes, startIndex, 4);
            if (header.WaveFile.FileID != "RIFF")
                return -1;
            startIndex += 4;
            // Chunk size: 4+n
            header.WaveFile.FileSize = BitConverter.ToUInt32(bufferForBytes, startIndex);
            // file size abnormal
            //if ((header.WaveFile.FileSize + 8) != fileHandle.Length)
            //    return -1;
            startIndex += 4;
            // WAVE ID: WAVE
            header.WaveFile.WaveID = Encoding.UTF8.GetString(bufferForBytes, startIndex, 4);
            startIndex += 4;


            startIndex = 0;
            readSize = fileHandle.Read(bufferForBytes, 0, 24);
            if (readSize != 24)
                return -1;
            // 	Chunk ID: fmt 
            header.WaveFormat.FormatID = Encoding.UTF8.GetString(bufferForBytes, 0, 4);
            if (header.WaveFormat.FormatID != "fmt ")
                return -1;
            startIndex += 4;
            //  Chunk size: 16, 18 or 40
            header.WaveFormat.FormatSize = BitConverter.ToUInt32(bufferForBytes, startIndex);
            // fmt Chunk size uncorrect
            if (!(header.WaveFormat.FormatSize == 16 || header.WaveFormat.FormatSize == 18 || header.WaveFormat.FormatSize == 40))
                return -1;
            startIndex += 4;
            if (header.WaveFormat.FormatSize == 18 || header.WaveFormat.FormatSize == 40)
                fileHandle.Read(bufferForBytes, 16, (int)header.WaveFormat.FormatSize - 16);
            //  Format code: 1-PCM            
            header.WaveFormat.FormatTag = BitConverter.ToUInt16(bufferForBytes, startIndex);
            startIndex += 2;
            // Number of interleaved channels            
            header.WaveFormat.Channels = BitConverter.ToUInt16(bufferForBytes, startIndex);
            startIndex += 2;
            // Sampling rate (blocks per second)
            header.WaveFormat.SamplesPerSec = BitConverter.ToUInt32(bufferForBytes, startIndex);
            startIndex += 4;
            // Data rate
            header.WaveFormat.AvgBytesPerSec = BitConverter.ToUInt32(bufferForBytes, startIndex);
            startIndex += 4;
            // Data block size (bytes)
            header.WaveFormat.BlockAlign = BitConverter.ToUInt16(bufferForBytes, startIndex);
            startIndex += 2;
            // Bits per sample
            header.WaveFormat.BitsPerSample = BitConverter.ToUInt16(bufferForBytes, startIndex);
            startIndex += 2;
            // Size of the extension (0 or 22)
            if (header.WaveFormat.FormatSize > 16)
            {
                header.WaveFormat.ExtSize = BitConverter.ToUInt16(bufferForBytes, startIndex);
                startIndex += 2;
            }
            else
                header.WaveFormat.ExtSize = 0;
            if (header.WaveFormat.ExtSize != 0)
            {
                // Number of valid bits
                header.WaveFormat.ValidBitsPerSample = BitConverter.ToUInt16(bufferForBytes, startIndex);
                startIndex += 2;
                // Speaker position mask
                header.WaveFormat.ChannelMask = BitConverter.ToUInt32(bufferForBytes, startIndex);
                startIndex += 4;
                // GUID, including the data format code
                header.WaveFormat.SubFormat = Encoding.UTF8.GetString(bufferForBytes, startIndex, 16);
                startIndex += 16;
            }

            while (true)
            {
                fileHandle.Read(bufferForBytes, 0, 8);
                string checkID = Encoding.UTF8.GetString(bufferForBytes, 0, 4);

                if (checkID == "data")
                {
                    startIndex = 0;
                    header.WaveData.DataID = Encoding.UTF8.GetString(bufferForBytes, startIndex, 4);
                    startIndex += 4;
                    header.WaveData.DataSize = BitConverter.ToUInt32(bufferForBytes, startIndex);
                    startIndex += 4;
                    break;
                }
                else
                {
                    header.WaveInfo.Valid = true;
                    startIndex = 0;
                    header.WaveInfo.ChunkIDs += Encoding.UTF8.GetString(bufferForBytes, startIndex, 4) + ",";
                    startIndex += 4;
                    uint chunkSize = BitConverter.ToUInt32(bufferForBytes, startIndex);
                    // other chunk size > 120, file invalid
                    if (chunkSize > (128 - 8))
                        return -1;
                    header.WaveInfo.TotalSize += chunkSize + 8;
                    startIndex += 4;

                    fileHandle.Read(bufferForBytes, 8, (int)chunkSize);
                }

                // File End, no "data" chunk
                if (fileHandle.Position + 8 > fileHandle.Length)
                    return -1;
            }
            return (int)header.WaveData.DataSize / header.WaveFormat.BlockAlign;
        }


        /// <summary>
        /// Formating data information dictionary to Vector File Header Format and write to file.
        /// </summary>
        private void WriteDataInfoToFileHeader(WaveHeader header)
        {
            byte[] fullFileHeadData;
            int buffUpdatePosition = 0;
            byte[] tempArray;


            // Pre-Allocated byte[] with size of Storage.HeaderSize, fill with space.
            fullFileHeadData = new byte[WAVEFILE_HEADER_QUANTUM];
            for (int i = 0; i < fullFileHeadData.Length; i++) { fullFileHeadData[i] = 0x20; }

            tempArray = Encoding.UTF8.GetBytes(header.WaveFile.FileID);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 4);
            buffUpdatePosition += 4;

            tempArray = BitConverter.GetBytes(header.WaveFile.FileSize);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 4);
            buffUpdatePosition += 4;

            tempArray = Encoding.UTF8.GetBytes(header.WaveFile.WaveID);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 4);
            buffUpdatePosition += 4;

            tempArray = Encoding.UTF8.GetBytes(header.WaveFormat.FormatID);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 4);
            buffUpdatePosition += 4;

            tempArray = BitConverter.GetBytes(header.WaveFormat.FormatSize);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 4);
            buffUpdatePosition += 4;

            tempArray = BitConverter.GetBytes(header.WaveFormat.FormatTag);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 2);
            buffUpdatePosition += 2;

            tempArray = BitConverter.GetBytes(header.WaveFormat.Channels);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 2);
            buffUpdatePosition += 2;

            tempArray = BitConverter.GetBytes(header.WaveFormat.SamplesPerSec);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 4);
            buffUpdatePosition += 4;

            tempArray = BitConverter.GetBytes(header.WaveFormat.AvgBytesPerSec);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 4);
            buffUpdatePosition += 4;

            tempArray = BitConverter.GetBytes(header.WaveFormat.BlockAlign);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 2);
            buffUpdatePosition += 2;

            tempArray = BitConverter.GetBytes(header.WaveFormat.BitsPerSample);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 2);
            buffUpdatePosition += 2;

            tempArray = Encoding.UTF8.GetBytes(header.WaveData.DataID);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 4);
            buffUpdatePosition += 4;

            tempArray = BitConverter.GetBytes(header.WaveData.DataSize);
            Buffer.BlockCopy(tempArray, 0, fullFileHeadData, buffUpdatePosition, 4);
            buffUpdatePosition += 4;

            _fileStream.Seek(0, SeekOrigin.Begin);
            _fileStream.Write(fullFileHeadData, 0, fullFileHeadData.Length);

        }
        #endregion


        internal class FlexByteArray
        {
            /// <summary>
            /// Actual buffer length in Bytes.
            /// </summary>
            private int _length;

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

        internal class WaveHeader
        {
            /// <summary>
            /// Initializes a WaveHeader object with the default values.
            /// </summary>
            public WaveHeader(uint numberOfSamples, ushort channeles = 2, uint sampleRate = 48000, ushort bits = 16)
            {
                // Chunk data
                WaveData.DataID = "data";
                WaveData.DataSize = numberOfSamples * channeles * bits / 8;

                // Chunk others
                WaveInfo.Valid = false;
                WaveInfo.ChunkIDs = "";
                WaveInfo.TotalSize = 0;

                // Chunk fmt
                WaveFormat.FormatID = "fmt ";
                WaveFormat.FormatSize = 16;
                WaveFormat.FormatTag = 1;
                WaveFormat.Channels = channeles;
                WaveFormat.SamplesPerSec = sampleRate;
                WaveFormat.AvgBytesPerSec = sampleRate * channeles * bits / 8;
                WaveFormat.BlockAlign = (ushort)(channeles * bits / 8);
                WaveFormat.BitsPerSample = bits;
                WaveFormat.ExtSize = 0;
                WaveFormat.ValidBitsPerSample = bits;
                WaveFormat.ChannelMask = 0;
                WaveFormat.SubFormat = "";

                // File Header
                WaveFile.FileID = "RIFF";
                WaveFile.FileSize = WaveData.DataSize + 4 + 4;  // total data chunk size
                WaveFile.FileSize += WaveInfo.TotalSize;        // total other chunks size
                WaveFile.FileSize += (WaveFormat.FormatSize + 4 + 4); // total format chunk size
                WaveFile.FileSize += 4;  // waveID size
                WaveFile.WaveID = "WAVE";
            }

            public FileInfo WaveFile;
            public FormatInfo WaveFormat;
            public TagInfo WaveInfo;
            public DataInfo WaveData;
            public struct FileInfo
            {
                public string FileID;   // Chunk ID: RIFF
                public uint FileSize;    // Chunk size: bytes after this field = file size - 8
                public string WaveID;   // WAVE ID: WAVE
            }
            public struct FormatInfo
            {
                public string FormatID;         // Chunk ID: fmt 
                public uint FormatSize;          // Chunk size: 16, 18 or 40
                public ushort FormatTag;        // Format code: 1--PCM
                public ushort Channels;         // Number of interleaved channels
                public uint SamplesPerSec;      // Sampling rate (blocks per second),eg: 44100
                public uint AvgBytesPerSec;     // Data rate
                public ushort BlockAlign;       // Data block size (bytes)
                public ushort BitsPerSample;    // bits per sample
                public ushort ExtSize;          // Size of the extension (0 or 22)
                public ushort ValidBitsPerSample;   // Number of valid bits
                public uint ChannelMask;             // Speaker position mask
                public string SubFormat;            // GUID, including the data format code
            }
            public struct TagInfo
            {
                public bool Valid;
                public string ChunkIDs;          // All other Chunk IDs, example: fact, LIST, ...
                public uint TotalSize;            // total size
            }
            public struct DataInfo
            {
                public string DataID;   // Chunk ID: data
                public uint DataSize;    // Chunk size: n
            }
        }
    }
}

