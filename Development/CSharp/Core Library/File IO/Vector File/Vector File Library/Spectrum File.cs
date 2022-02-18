using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SeeSharpTools.JXI.FileIO.VectorFile
{
    /// <summary>
    /// 存储频谱数据的文件。
    /// </summary>
    public class SpectrumFile : VectorFile
    {

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        ///  Constructor
        /// </summary>
        public SpectrumFile()
        {
            _samplingInfo = new SpectrumSamplingInformation();//频谱采样信息
        }

        /// <summary>
        /// constructor, execute when first call any method
        /// </summary>
        static SpectrumFile()
        {
        }

        #endregion

        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 获取或设置文件的当前读写位置，即从数据起始的频谱帧数（Frame）。
        /// Current position,number of framess form origion.
        /// </summary>
        public long Position
        {
            get { return this.Seek(0, SeekOrigin.Current); }
            set { this.Seek(value, SeekOrigin.Begin); }
        }
        private int numOfBands;

        private long _numberOfFrames;
        /// <summary>
        /// 文件中包含的频谱总帧数。
        /// Total data size of file, in frames, valid only when openning existed file.
        /// </summary>
        public long NumberOfFrames { get { return _numberOfFrames; } }

        private SpectrumSamplingInformation _samplingInfo;
        /// <summary>
        /// 频谱数据的采样信息。
        /// Sampling Information.
        /// </summary>
        public SpectrumSamplingInformation Sampling { get { return _samplingInfo; } }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 打开或创建文件。
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="mode">File Mode</param>
        /// <param name="fileAccess">File Access</param>
        public void Open(string fileName, FileMode mode, FileAccess fileAccess)
        {
            // Reset data information public properties.
            _samplingInfo = new SpectrumSamplingInformation();

            // Call base.Open to open file which will initialize fields/porperties of base class.
            // 频谱数据不需要高速存取，所以总是enable buffering（即使用.NET FileStream读写即可）。
            base.Open(fileName, mode, fileAccess, false);

            if (mode == FileMode.Create || mode == FileMode.CreateNew)
            {
                // Create new file.
                _numberOfFrames = 0;
                _storageInfo.FileFormat = FileFormat.Spectrum;
            }
            else
            {
                // 打开现有文件，则检查该文件的格式有效。
                if (_storageInfo.FileFormat == FileFormat.Spectrum)
                {
                    // Extract data information (e.g. samplilng information).
                    ExtractDataInfoFromDictionary();
                    _numberOfFrames = (_fileLengthInBytes - _storageInfo.FileHeaderSize) / this.BytesPerSample;
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
        /// 每次调用WriteFileHeader()方法之后，数据的写入位置会被重置为起始位置，即等效于调用了一次Seek(0, SeekOrigin.Begin)。
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
        /// <param name="offset">The number of samples relative to origin. 。</param>
        /// <param name="origin">Using a value of type System.IO.SeekOrigin, the start, end, or current position is specified as a reference point for offset.</param>
        /// <returns>The new location of the file read and write, the number of samples away from the starting point of the data, that is, the value of the attribute "Position". </returns>
        public long Seek(long offset, SeekOrigin origin)
        {
            long offsetInBytes = 0;

            // Number of lines for single channel per Frame.
            int linesTotalOneChnPerFrame = 0;
            for (int j = 0; j < _samplingInfo.Bands.Count; j++)
            {
                linesTotalOneChnPerFrame += _samplingInfo.Bands[j].NumOfSpectralLines;
            }

            // Convert input "offset" (in samples) to "offset in bytes", according to SeekOrigin.
            switch (origin)
            {
                case SeekOrigin.Begin:
                    {
                        // For seeking from Begin, calculate offset in bytes must add Stogage.FileHeaderSize.
                        offsetInBytes = offset * linesTotalOneChnPerFrame * this.BytesPerSample + _storageInfo.FileHeaderSize;
                        break;
                    }
                case SeekOrigin.Current:
                case SeekOrigin.End:
                    {
                        // For seeking from Current/End, the offset is "relative", multiply offset by "BytesPerSample" is ok.
                        offsetInBytes = offset * linesTotalOneChnPerFrame * this.BytesPerSample;
                        break;
                    }
            }

            // Call FileStream or KernelFileStream to seek file according to private field "_disableBuffering".
            long positionInBytes;
            if (_disableBuffering) { positionInBytes = _kernelFileStream.Seek(offsetInBytes, origin); }
            else { positionInBytes = _nativeFileStream.Seek(offsetInBytes, origin); }

            // Convert "offset in bytes" from file start to "offset in samples" from data start.
            return (positionInBytes - _storageInfo.FileHeaderSize) / (this.BytesPerSample * linesTotalOneChnPerFrame);
        }

        /// <summary>
        /// 写入单通道单频段频谱数据，float数据类型。
        /// </summary>
        /// <param name="data"></param>
        public override void Write(float[] data)
        {
            this.Write(new float[][] { data });
        }

        /// <summary>
        /// 写入单通道单频段频谱数据，double数据类型。
        /// </summary>
        /// <param name="data"></param>
        public override void Write(double[] data)
        {
            this.Write(new double[][] { data });
        }

        /// <summary>
        /// 写入单通道多频段频谱数据，或多通道单频段数据，float数据类型。
        /// 当为单通道多频段数据时，按频段索引，即data[0]为第一个频段的频谱数据，以此类推。
        /// 当为多通道单频段数据时，按通道索引，即data[0]为第一个通道的频谱数据，以此类推。
        /// 判断data[0]的个数与通道还是频段数相符。若与通道相符，则按通道索引，判断数据长度与线数是否相符，相符则分别写入各通道的数据。
        /// </summary>
        /// <param name="data"></param>
        public void Write(float[][] data)
        {
            if (data.GetLength(0) == Storage.NumberOfChannels && data.GetLength(0) == Sampling.Bands.Count && data.GetLength(0) != 1)
            {
                throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length: Channel=Band");
            }

            //多(单)通道单频段
            if (data.GetLength(0) == _storageInfo.NumberOfChannels)
            {
                #region--------------------------数组转换----------------------

                float[][][] dataToWrite;
                dataToWrite = new float[data.GetLength(0)][][];
                for (int channelIndex = 0; channelIndex < _storageInfo.NumberOfChannels; channelIndex++)
                {
                    dataToWrite[channelIndex] = new float[1][];
                    dataToWrite[channelIndex][0] = new float[data[0].GetLength(0)];
                    for (int lines = 0; lines < data[0].GetLength(0); lines++)
                    {
                        dataToWrite[channelIndex][0][lines] = data[channelIndex][lines];
                    }
                }
                #endregion

                this.Write(dataToWrite);
            }

            //单通道多频段      
            else { this.Write(new float[][][] { data }); }
        }

        /// <summary>
        /// 写入单通道多频段频谱数据，或多通道单频段数据，double数据类型。
        /// 当为单通道多频段数据时，按频段索引，即data[0]为第一个频段的频谱数据，以此类推。
        /// 当为多通道单频段数据时，按通道索引，即data[0]为第一个通道的频谱数据，以此类推。
        /// </summary>
        /// <param name="data"></param>
        public void Write(double[][] data)
        {
            if (data.GetLength(0) == Storage.NumberOfChannels && data.GetLength(0) == Sampling.Bands.Count && data.GetLength(0) != 1)
            {
                throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length: Channel=Band");
            }

            //多(单)通道单频段
            if (data.GetLength(0) == _storageInfo.NumberOfChannels)
            {
                #region--------------------------数组转换----------------------

                double[][][] dataToWrite;
                dataToWrite = new double[data.GetLength(0)][][];
                for (int channelIndex = 0; channelIndex < _storageInfo.NumberOfChannels; channelIndex++)
                {
                    dataToWrite[channelIndex] = new double[1][];
                    dataToWrite[channelIndex][0] = new double[data[0].GetLength(0)];
                    for (int lines = 0; lines < data[0].GetLength(0); lines++)
                    {
                        dataToWrite[channelIndex][0][lines] = data[channelIndex][lines];
                    }
                }
                #endregion

                this.Write(dataToWrite);
            }

            //单通道多频段      
            else { this.Write(new double[][][] { data }); }
        }

        /// <summary>
        /// 写入多通道多频段频谱数据，double数据类型。依次按通道、频段索引，即data[0][1]为第一个通道的第二频段的频谱数据，以此类推。      
        /// </summary>
        /// <param name="data"></param>
        public void Write(double[][][] data)
        {
            #region------------------------- 检查输入参数有效。 -------------------------

            //检查通道数。
            if (data.GetLength(0) != _storageInfo.NumberOfChannels)
            {
                int a = data.GetLength(0);
                throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Channel");
            }

            for (int i = 0; i < _storageInfo.NumberOfChannels; i++)
            {
                //检查频带数。
                if (data[i].GetLength(0) != _samplingInfo.Bands.Count)
                {
                    throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Band");
                }
                for (int j = 0; j < _samplingInfo.Bands.Count; j++)
                {
                    //检查频谱线数。
                    if (data[i][j].Length != _samplingInfo.Bands[j].NumOfSpectralLines)
                    {
                        throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Line");
                    }
                }
            }
            #endregion

            // 按照通道 -> 频段的次序写入。
            for (int i = 0; i < _storageInfo.NumberOfChannels; i++)
            {
                for (int j = 0; j < _samplingInfo.Bands.Count; j++)
                {
                    base.Write(data[i][j]);
                }
            }
        }

        /// <summary>
        /// 写入多通道多频段频谱数据，float数据类型。依次按通道、频段索引，即data[0][1]为第一个通道的第二频段的频谱数据，以此类推。
        /// </summary>
        /// <param name="data"></param>
        public void Write(float[][][] data)
        {
            #region------------------------- 检查输入参数有效。 -------------------------

            //检查通道数。
            if (data.GetLength(0) != _storageInfo.NumberOfChannels)
            {
                int a = data.GetLength(0);
                throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Channel");
            }

            for (int i = 0; i < _storageInfo.NumberOfChannels; i++)
            {
                //检查频带数。
                if (data[i].GetLength(0) != _samplingInfo.Bands.Count)
                {
                    throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Band");
                }
                for (int j = 0; j < _samplingInfo.Bands.Count; j++)
                {
                    //检查频谱线数。
                    if (data[i][j].Length != _samplingInfo.Bands[j].NumOfSpectralLines)
                    {
                        throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Line");
                    }
                }
            }
            #endregion

            // 按照通道 -> 频段的次序写入。
            for (int i = 0; i < _storageInfo.NumberOfChannels; i++)
            {
                for (int j = 0; j < _samplingInfo.Bands.Count; j++)
                {
                    base.Write(data[i][j]);
                }
            }
        }

        /// <summary>
        /// 读取单通道单频段频谱数据，float数据类型。
        /// 参数data由调用者分配空间，长度为Sampling.Bands[0].NumOfSpectrualLines。
        /// </summary>
        /// <param name="data"></param>
        public override void Read(float[] data)
        {
            this.Read(new float[][] { data });
        }

        /// <summary>
        /// 读取单通道单频段频谱数据，double数据类型。
        /// 参数data由调用者分配空间，长度为Sampling.Bands[0].NumOfSpectrualLines。
        /// </summary>
        /// <param name="data"></param>
        public override void Read(double[] data)
        {
            this.Read(new double[][] { data });
        }

        /// <summary>
        /// 读取单通道多频段频谱数据，或多通道单频段数据，float数据类型。参数data由调用者分配空间：
        /// 当为单通道多频段数据时，按频段索引，即第一维长度为[Sampling.Bands.Count]，第二维长度为各Band的谱线数[Sampling.Band[i].NumOfSpectrualLines]。
        /// 当为多通道单频段数据时，按通道索引，即第一维长度为[Storage.NumberOfChannels]，第二维长度为谱线数[Sampling.Band[0].NumOfSpectrualLines]。
        /// </summary>
        /// <param name="data"></param>
        public void Read(float[][] data)
        {
            if (data.GetLength(0) == Storage.NumberOfChannels && data.GetLength(0) == Sampling.Bands.Count && data.GetLength(0) != 1)
            {
                throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length: Channel=Band");
            }

            //多(单)通道单频段
            if (data.GetLength(0) == _storageInfo.NumberOfChannels)
            {
                #region--------------------------数组转换----------------------

                float[][][] dataToRead;
                dataToRead = new float[data.GetLength(0)][][];
                for (int channelIndex = 0; channelIndex < _storageInfo.NumberOfChannels; channelIndex++)
                {
                    dataToRead[channelIndex] = new float[1][];
                    dataToRead[channelIndex][0] = new float[data[channelIndex].Length];
                }

                #endregion

                // 读取数据到三维数组中。
                this.Read(dataToRead);

                // 将三维数组中的频谱数据赋值给二维数组。
                for (int i = 0; i < dataToRead.Length; i++)
                {
                    for (int j = 0; j < dataToRead[i].Length; j++)
                    {
                        for (int k = 0; k < dataToRead[i][j].Length; k++)
                        {
                            data[i][k] = dataToRead[i][j][k];
                        }
                    }
                }
            }

            //单通道多频段      
            else { this.Read(new float[][][] { data }); }
        }

        /// <summary>
        /// 读取单通道多频段频谱数据，或多通道单频段数据，double数据类型。参数data由调用者分配空间：
        /// 当为单通道多频段数据时，按频段索引，即第一维长度为[Sampling.Bands.Count]，第二维长度为各Band的谱线数[Sampling.Band[i].NumOfSpectrualLines]。
        /// 当为多通道单频段数据时，按通道索引，即第一维长度为[Storage.NumberOfChannels]，第二维长度为谱线数[Sampling.Band[0].NumOfSpectrualLines]。
        /// </summary>
        /// <param name="data"></param>
        public void Read(double[][] data)
        {
            if (data.GetLength(0) == Storage.NumberOfChannels && data.GetLength(0) == Sampling.Bands.Count && data.GetLength(0) != 1)
            {
                throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length: Channel=Band");
            }

            //多(单)通道单频段
            if (data.GetLength(0) == _storageInfo.NumberOfChannels)
            {
                #region--------------------------数组转换----------------------

                double[][][] dataToRead;
                dataToRead = new double[data.GetLength(0)][][];
                for (int channelIndex = 0; channelIndex < _storageInfo.NumberOfChannels; channelIndex++)
                {
                    dataToRead[channelIndex] = new double[1][];
                    dataToRead[channelIndex][0] = new double[data[channelIndex].Length];
                }

                #endregion

                // 读取数据到三维数组中。
                this.Read(dataToRead);

                // 将三维数组中的频谱数据赋值给二维数组。
                for (int i = 0; i < dataToRead.Length; i++)
                {
                    for (int j = 0; j < dataToRead[i].Length; j++)
                    {
                        for (int k = 0; k < dataToRead[i][j].Length; k++)
                        {
                            data[i][k] = dataToRead[i][j][k];
                        }
                    }
                }
            }

            //单通道多频段      
            else { this.Read(new double[][][] { data }); }
        }

        /// <summary>
        /// 读取多通道多频段频谱数据，float数据类型。参数data由调用者分配空间，依次按通道、频段索引：
        /// 长度为[Storage.NumberOfChannels][Sampling.Bands.Count][Sampling.Band[i].NumOfSpectrualLines]。
        /// </summary>
        /// <param name="data"></param>
        public void Read(float[][][] data)
        {
            #region------------------------检查输入参数的有效性。----------------------------

            //检查通道数。
            if (data.GetLength(0) != _storageInfo.NumberOfChannels)
            {
                throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Fram Length：Channel");
            }

            for (int i = 0; i < _storageInfo.NumberOfChannels; i++)
            {
                //检查频带数。
                if (data[i].GetLength(0) != _samplingInfo.Bands.Count)
                {
                    throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Band");
                }
                for (int j = 0; j < data[i].GetLength(0); j++)
                {
                    //检查频谱线数。
                    if (data[i][j].Length != _samplingInfo.Bands[j].NumOfSpectralLines)
                    {
                        throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Line");
                    }
                }
            }
            #endregion
            // 按照通道 -> 频段的次序读取。
            for (int i = 0; i < _storageInfo.NumberOfChannels; i++)
            {
                for (int j = 0; j < _samplingInfo.Bands.Count; j++)
                {
                    base.Read(data[i][j]);
                }
            }
        }

        /// <summary>
        /// 读取多通道多频段频谱数据，double数据类型。参数data由调用者分配空间，依次按通道、频段索引：
        /// 长度为[Storage.NumberOfChannels][Sampling.Bands.Count][Sampling.Band[i].NumOfSpectrualLines]。
        /// </summary>
        /// <param name="data"></param>
        public void Read(double[][][] data)
        {
            #region------------------------检查输入参数的有效性。----------------------------

            //检查通道数。
            if (data.GetLength(0) != _storageInfo.NumberOfChannels)
            {
                throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Fram Length：Channel");
            }
            for (int i = 0; i < _storageInfo.NumberOfChannels; i++)
            {
                //检查频带数。
                if (data[i].GetLength(0) != _samplingInfo.Bands.Count)
                {
                    throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Band");
                }
                for (int j = 0; j < _samplingInfo.Bands.Count; j++)
                {
                    //检查数据线数。
                    if (data[i][j].Length != _samplingInfo.Bands[j].NumOfSpectralLines)
                    {
                        throw new VectorFileException(ExceptionEnum.DataLengthConflict, "Frame Length：Line");
                    }
                }
            }
            #endregion

            // 按照通道 -> 频段的次序依次读取。
            for (int i = 0; i < _storageInfo.NumberOfChannels; i++)
            {
                for (int j = 0; j < _samplingInfo.Bands.Count; j++)
                {
                    base.Read(data[i][j]);
                }
            }
        }

        #endregion

        #region------------------------- 私有方法 -------------------------
        /// <summary>
        /// 从字典中提取数据信息
        /// </summary>
        private void ExtractDataInfoFromDictionary()
        {
            string rawCsvString;
            string[] bandRawCsvString;
            double[] numericArray;
            string[] strTime;
            string separatorFirst = ",";
            char separatorSecond = ':';

            #region -------------------------Extract"Sampling"infomation from INI Dictionary---------------------

            #region-----------------------------------------Extract "Unit"--------------------------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumUnit))
            { _samplingInfo.Unit = Convert.ToString(_infoDictionary[HeaderKeyCostants.SamplingSpectrumUnit]); }
            else
            { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Unit"); }

            #endregion

            #region-----------------------------------------Extract "Interval"--------------------------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingScanFrameInterval))
            {
                rawCsvString = Convert.ToString(_infoDictionary[HeaderKeyCostants.SamplingScanFrameInterval]);
                strTime = rawCsvString.Split(separatorSecond);
                _samplingInfo.Interval = new TimeSpan(Convert.ToInt32(strTime[0]), Convert.ToInt32(strTime[1]), Convert.ToInt32(strTime[2]));
            }
            else
            { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Interval"); }

            #endregion

            #region--------------------------------------Extract "numOfBands"-----------------------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumNumOfBand))
            {
                numOfBands = Convert.ToInt32(_infoDictionary[HeaderKeyCostants.SamplingSpectrumNumOfBand]);
            }
            else
            {
                throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Bands");
            }
            #endregion

            #region--------------------------------------Extract "FrequencyStart"-------------------------------       

            //Set size of "Band" as "Stroage.NumberofBands".
            _samplingInfo.Bands.Clear();
            for (int i = 0; i < numOfBands; i++) { _samplingInfo.Bands.Add(new BandSpectrumSamplingInformation()); }
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumFrequencyStart))
            {
                //将键FrequencyStart的内容(以逗号隔开的形式)赋给rawCsvString
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingSpectrumFrequencyStart];

                //将以separator隔开的字符串rawCsvString转换成double的数组
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separatorFirst);

                //数组大小应与频带数相等
                if (numericArray == null || numericArray.Length != numOfBands)
                {
                    throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Frequency Start");
                }
                for (int i = 0; i < numOfBands; i++)
                {
                    _samplingInfo.Bands[i].FrequencyStart = numericArray[i];
                }
            }
            #endregion

            #region--------------------------------------Extract "FrequencyStop"--------------------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumFrequencyStop))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingSpectrumFrequencyStop];
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separatorFirst);
                if (numericArray == null || numericArray.Length != numOfBands)
                {
                    throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Frequency Stop");
                }
                for (int i = 0; i < numOfBands; i++)
                {
                    _samplingInfo.Bands[i].FrequencyStop = numericArray[i];
                }
            }
            #endregion

            #region--------------------------------------Extract "FrequencyStep"--------------------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumFrequencyStep))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingSpectrumFrequencyStep];
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separatorFirst);
                if (numericArray == null || numericArray.Length != numOfBands)
                {
                    throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Frequency Step");
                }
                for (int i = 0; i < numOfBands; i++)
                {
                    _samplingInfo.Bands[i].FrequencyStep = numericArray[i];
                }
            }
            #endregion

            #region------------------------------------Extract "NumOfSpectralLines"-----------------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumNumOfSpectralLines))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingSpectrumNumOfSpectralLines];
                numericArray = ConvertSpreadsheetStringToArrayDbl(rawCsvString, separatorFirst);
                if (numericArray == null || numericArray.Length != numOfBands)
                {
                    throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "NumOfSpectralLines");
                }
                for (int i = 0; i < numOfBands; i++)
                {
                    _samplingInfo.Bands[i].NumOfSpectralLines = Convert.ToInt32(numericArray[i]);
                }
            }

            for (int i = 0; i < numOfBands; i++)
            {
                for (int j = 0; j < _storageInfo.NumberOfChannels; j++) { _samplingInfo.Bands[i].Channels.Add(new BandSpectrumChannelInformation()); }
            }
            #endregion

            #region-------------------------------------Extract "FrequencyShift"--------------------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumFrequencyShift))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingSpectrumFrequencyShift];
                bandRawCsvString = rawCsvString.Split(separatorSecond);
                for (int i = 0; i < numOfBands; i++)
                {
                    numericArray = ConvertSpreadsheetStringToArrayDbl(bandRawCsvString[i], separatorFirst);
                    if (numericArray == null || numericArray.Length != _storageInfo.NumberOfChannels)
                    {
                        throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Frequency Shift");
                    }
                    for (int j = 0; j < _storageInfo.NumberOfChannels; j++)
                    {
                        _samplingInfo.Bands[i].Channels[j].FrequencyShift = numericArray[j];
                    }
                }
            }

            #endregion

            #region------------------------------------Extract "ReferenceLevel"---------------------------------

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumReferenceLevel))
            {
                rawCsvString = _infoDictionary[HeaderKeyCostants.SamplingSpectrumReferenceLevel];
                bandRawCsvString = rawCsvString.Split(separatorSecond);
                for (int i = 0; i < numOfBands; i++)
                {
                    numericArray = ConvertSpreadsheetStringToArrayDbl(bandRawCsvString[i], separatorFirst);
                    if (numericArray == null || numericArray.Length != _storageInfo.NumberOfChannels)
                    {
                        throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Reference Level");
                    }
                    for (int j = 0; j < _storageInfo.NumberOfChannels; j++)
                    {
                        _samplingInfo.Bands[i].Channels[j].ReferenceLevel = numericArray[j];
                    }
                }
            }

            #endregion

            #endregion
        }

        /// <summary>
        /// 将数据信息更新到字典
        /// </summary>
        private void UpdateDataInfoToDictionary()
        {
            double[] numericArray;
            string resultString;
            string[] bandInfoString = new string[_samplingInfo.Bands.Count];
            string separatorFirst = ",";
            string separatorSecond = ":";

            #region-----------------------------更新采样信息_samplingInfo---------------------------------------

            #region------------------------------------------Update "Unit"--------------------------------

            resultString = _samplingInfo.Unit;
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumUnit))
            { _infoDictionary[HeaderKeyCostants.SamplingSpectrumUnit] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingSpectrumUnit, resultString); }

            #endregion

            #region------------------------------------------Update "Interval"--------------------------------

            resultString = _samplingInfo.Interval.ToString();
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingRecordInterval))
            { _infoDictionary[HeaderKeyCostants.SamplingRecordInterval] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingScanFrameInterval, resultString); }

            #endregion

            #region------------------------------------------Update "NumOfBands"--------------------------------

            resultString = Convert.ToString(_samplingInfo.Bands.Count);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumNumOfBand))
            {
                _infoDictionary[HeaderKeyCostants.SamplingSpectrumNumOfBand] = resultString;
            }
            else
            {
                _infoDictionary.Add(HeaderKeyCostants.SamplingSpectrumNumOfBand, resultString);
            }
            #endregion

            #region------------------------------------------Update "FrequencyStart"--------------------------------

            //Check if sample infomation valid.
            if (_samplingInfo.Bands == null)
            { throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Sampling.Band.Count"); }
            numericArray = new double[_samplingInfo.Bands.Count];
            double[][] multiChannelnumericArray = new double[_samplingInfo.Bands.Count][];


            for (int i = 0; i < _samplingInfo.Bands.Count; i++)
            { numericArray[i] = _samplingInfo.Bands[i].FrequencyStart; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f2", separatorFirst);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumFrequencyStart))
            { _infoDictionary[HeaderKeyCostants.SamplingSpectrumFrequencyStart] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingSpectrumFrequencyStart, resultString); }

            #endregion

            #region------------------------------------------Update "FrequencyStop"--------------------------------

            for (int i = 0; i < _samplingInfo.Bands.Count; i++)
            { numericArray[i] = _samplingInfo.Bands[i].FrequencyStop; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f2", separatorFirst);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumFrequencyStop))
            { _infoDictionary[HeaderKeyCostants.SamplingSpectrumFrequencyStop] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingSpectrumFrequencyStop, resultString); }

            #endregion

            #region------------------------------------------Update "FrequencyStep"--------------------------------

            for (int i = 0; i < _samplingInfo.Bands.Count; i++)
            { numericArray[i] = _samplingInfo.Bands[i].FrequencyStep; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f2", separatorFirst);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumFrequencyStep))
            { _infoDictionary[HeaderKeyCostants.SamplingSpectrumFrequencyStep] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingSpectrumFrequencyStep, resultString); }

            #endregion

            #region------------------------------------------Update "NumOfSpectralLines"----------------------------

            for (int i = 0; i < _samplingInfo.Bands.Count; i++)
            { numericArray[i] = _samplingInfo.Bands[i].NumOfSpectralLines; }
            resultString = ConvertArrayToSpreadsheetString(numericArray, "f2", separatorFirst);
            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumNumOfSpectralLines))
            { _infoDictionary[HeaderKeyCostants.SamplingSpectrumNumOfSpectralLines] = resultString; }
            else
            { _infoDictionary.Add(HeaderKeyCostants.SamplingSpectrumNumOfSpectralLines, resultString); }

            #endregion

            #region------------------------------------------Update "FrequencyShift"-------------------------------

            resultString = null;
            for (int i = 0; i < _samplingInfo.Bands.Count; i++)
            {
                multiChannelnumericArray[i] = new double[_samplingInfo.Bands[i].Channels.Count];
                if (_samplingInfo.Bands[i].Channels == null || _samplingInfo.Bands[i].Channels.Count != _storageInfo.NumberOfChannels)
                {
                    throw new VectorFileException(ExceptionEnum.InconsistantFileHeaderInfo, "Sampling.Bands.Channel.Count");
                }
                for (int j = 0; j < _samplingInfo.Bands[i].Channels.Count; j++)
                {
                    multiChannelnumericArray[i][j] = _samplingInfo.Bands[i].Channels[j].FrequencyShift;
                }
                bandInfoString[i] = ConvertArrayToSpreadsheetString(multiChannelnumericArray[i], "f2", separatorFirst);
            }

            if (_samplingInfo.Bands.Count == 1) { resultString = bandInfoString[0]; }
            else { resultString = string.Join(separatorSecond, bandInfoString); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumFrequencyShift))
            {
                _infoDictionary[HeaderKeyCostants.SamplingSpectrumFrequencyShift] = resultString;
            }
            else
            {
                _infoDictionary.Add(HeaderKeyCostants.SamplingSpectrumFrequencyShift, resultString);
            }
            #endregion

            #region------------------------------------------Update "ReferenceLevel"--------------------------------

            resultString = null;
            for (int i = 0; i < _samplingInfo.Bands.Count; i++)
            {
                multiChannelnumericArray[i] = new double[_samplingInfo.Bands[i].Channels.Count];
                for (int j = 0; j < _samplingInfo.Bands[i].Channels.Count; j++)
                {
                    multiChannelnumericArray[i][j] = _samplingInfo.Bands[i].Channels[j].ReferenceLevel;
                }
                bandInfoString[i] = ConvertArrayToSpreadsheetString(multiChannelnumericArray[i], "f2", separatorFirst);
            }
            if (_samplingInfo.Bands.Count == 1) { resultString = bandInfoString[0]; }
            else { resultString = string.Join(separatorSecond, bandInfoString); }

            if (_infoDictionary.ContainsKey(HeaderKeyCostants.SamplingSpectrumReferenceLevel))
            {
                _infoDictionary[HeaderKeyCostants.SamplingSpectrumReferenceLevel] = resultString;
            }
            else
            {
                _infoDictionary.Add(HeaderKeyCostants.SamplingSpectrumReferenceLevel, resultString);
            }

            #endregion

            #endregion
        }

        #endregion

    }

    #region------------------------- 公共数据类型 -------------------------

    /// <summary>
    /// 频谱的完整采样信息。
    /// </summary>
    public class SpectrumSamplingInformation
    {

        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 频谱数据的工程单位。
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 数据记录的时间间隔。
        /// </summary>
        public TimeSpan Interval { get; set; }

        private List<BandSpectrumSamplingInformation> _bands;
        /// <summary>
        /// 所有频段的频谱采样信息。列表中的每个元素对应频谱的一个频段。
        /// </summary>
        public List<BandSpectrumSamplingInformation> Bands { get { return _bands; } }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public SpectrumSamplingInformation()
        {
            this.Interval = TimeSpan.FromSeconds(1);
            _bands = new List<BandSpectrumSamplingInformation>();
        }

        #endregion
    }

    /// <summary>
    /// 一个频段的频谱采样信息。
    /// </summary>
    public class BandSpectrumSamplingInformation
    {
        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 频谱起始频率,以Hz为单位。
        /// Start frequency of spectrum, in Hz.
        /// </summary>
        public double FrequencyStart { get; set; }

        /// <summary>
        /// 频谱终止频率,以Hz为单位。
        /// Stop frequency of spectrum, in Hz.
        /// </summary>
        public double FrequencyStop { get; set; }

        /// <summary>
        /// 频率步进，以Hz为单位。
        /// FrequencyStep, in Hz.
        /// </summary>
        public double FrequencyStep { get; set; }

        /// <summary>
        /// 频谱线数。
        /// </summary>
        public int NumOfSpectralLines { get; set; }

        private List<BandSpectrumChannelInformation> _channels;
        /// <summary>
        /// 在当前频段下，各通道的采样信息。
        /// </summary>
        public List<BandSpectrumChannelInformation> Channels { get { return _channels; } }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数
        /// </summary>
        public BandSpectrumSamplingInformation() : this(0, 0, 0, 0) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="frequencyStart">频谱起始频率,以Hz为单位。</param>
        /// <param name="frequencyStop">频谱终止频率,以Hz为单位。</param>
        /// <param name="frequencyStep">频率步进，以Hz为单位。</param>
        /// <param name="numOfSpectralLines">频谱线数。</param>
        public BandSpectrumSamplingInformation(double frequencyStart, double frequencyStop, double frequencyStep, int numOfSpectralLines)
        {
            this.FrequencyStart = frequencyStart;
            this.FrequencyStop = frequencyStop;
            this.FrequencyStep = frequencyStep;
            this.NumOfSpectralLines = numOfSpectralLines;

            _channels = new List<BandSpectrumChannelInformation>();
        }

        #endregion
    }

    /// <summary>
    /// 在频谱的一个频段上，一个通道的采样信息。
    /// </summary>
    public class BandSpectrumChannelInformation
    {
        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 频谱的偏置频率，以Hz为单位。
        /// Offset frequency of spectrum, in Hz.
        /// </summary>
        public double FrequencyShift { get; set; }

        /// <summary>
        /// 参考电平。
        /// </summary>
        public double ReferenceLevel { get; set; }

        #endregion
    }

    #endregion

}
