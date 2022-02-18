using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Device;
using System.Runtime.InteropServices;
using System.Threading;

namespace SeeSharpTools.JXI.FileIO.VectorFile.Tests
{
    [TestClass]
    public class JXIVectorFileUnitTest
    {

        /// <summary>
        /// 测试用的临时文件路径，各测试方法将对同一文件进行读写，在测试完成后（析构），该临时文件会被删除。
        /// </summary>
        private static string _filePath;

        /// <summary>
        /// 用于存放多线程测试时，各线程出现的异常。
        /// </summary>
        private ConcurrentQueue<Exception> _exceptionQueue;

        #region  -------------------- 构造和析构函数 -----------------------

        public JXIVectorFileUnitTest()
        {
            // 初始化私有成员。
            _exceptionQueue = new ConcurrentQueue<Exception>();

            // 临时数据文件放在当前路径。
            _filePath = Environment.CurrentDirectory + "\\Temp.IQ";
        }

        
        [ClassCleanup]
        public static void AllTestsComplete()
        {
            // 删除测试中生成的临时数据文件。
            if (File.Exists(_filePath)) { File.Delete(_filePath); }
        }

        #endregion

        #region  -------------------- 私有方法 -----------------------

        /// <summary>
        /// 测试多个线程同时打开同一文件的行为，应无异常。
        /// </summary>
        private void MultiThreadOpenExistedVectorFileTest()
        {
            int i;
            int numberOfThreadInstances = 10;
            Thread[] bgThreads = new Thread[numberOfThreadInstances];

            // Vector File类库应支持多线程（多实例）同时打开同一文件进行数据读取。此处即测试该功能。
            try
            {
                // 创建多个线程，每个线程都执行打开同一个文件的操作。
                for (i = 0; i < numberOfThreadInstances; i++)
                {
                    bgThreads[i] = new Thread(() =>
                    {
                        try
                        {
                            // 暂时只测试打开文件操作，后续可根据需要添加数据正确性的测试代码。
                            var vectorFile = new VectorFile();
                            vectorFile.Open(_filePath, FileMode.Open, FileAccess.Read);
                            Debug.WriteLine(DateTime.Now + " File format:" + vectorFile.Storage.FileFormat);
                            vectorFile.Close();
                        }
                        catch (Exception exception) { _exceptionQueue.Enqueue(exception); }
                    });
                }

                // 清除异常队列中的数据，启动各线程。
                Exception threadException = null;
                for (i = 0; i < _exceptionQueue.Count; i++) { _exceptionQueue.TryDequeue(out threadException); }
                for (i = 0; i < numberOfThreadInstances; i++) { bgThreads[i].Start(); }

                // 等待所有的线程执行结束，若遇到异常，则认为测试失败。
                for (i = 0; i < numberOfThreadInstances; i++) { bgThreads[i].Join(1000); }
                if (_exceptionQueue.Count > 0)
                {
                    for (i = 0; i < _exceptionQueue.Count; i++) { _exceptionQueue.TryDequeue(out threadException); }
                    throw threadException;
                }
            }
            catch (Exception exception) { Assert.Fail(exception.Message); }

        }

        #endregion

        #region  -------------------- Vector File FixFrequencyStream Class Tests -----------------------

        /// <summary>
        /// 测试FixFrequencyStream的基础功能：创建文件->写入数据->关闭文件->打开文件->校验数据信息（文件头内容）-> 关闭文件等。
        /// </summary>
        [TestMethod]
        public void FixFreqStreamFileCreateNew_ValidateHeader()
        {
            var vectorFile = new FixFrequencyStreamFile();
            var channelSamplingInfo = new BaseChannelSamplingInfo();

            string dataTag = "JXI Vector File Unit Test";
            string producer = "JXI Vector File Unit Test";
            var fileFormat = FileFormat.FixFrequencyStream;
            var dataType = DataType.ComplexI16;
            var dateTime = DateTime.Now;
            int numberOfChannels = 1;
            double sampleRate = 1000000;
            double latitude = 23;

            channelSamplingInfo.RFFrequency = 566000000;
            channelSamplingInfo.Bandwidth = sampleRate * 0.8;
            channelSamplingInfo.RFScaleFactor = 0.5;
            channelSamplingInfo.DigitizerScaleFactor = 0.00001;

            #region--------------- 根据上述信息，生成文件并写入一组随机数据。 ------------------

            try
            {
                vectorFile.Open(_filePath, FileMode.Create, FileAccess.Write);

                vectorFile.Maker.Producer = producer;

                vectorFile.Archive.DataTag = dataTag;
                vectorFile.Archive.DateTime = dateTime;
                vectorFile.Archive.Coordinate.Latitude = latitude;

                vectorFile.Storage.FileFormat = fileFormat;
                vectorFile.Storage.DataType = dataType;
                vectorFile.Storage.NumberOfChannels = numberOfChannels;

                vectorFile.Sampling.SampleRate = sampleRate;
                for (int i = 0; i < numberOfChannels; i++) { vectorFile.Sampling.Channels.Add(channelSamplingInfo); }

                int dataLength = 65536;
                short[] rampData = new short[dataLength];
                for(int i = 0; i < dataLength; i ++) { rampData[i] = (short)(i % 32768); }

                vectorFile.WriteFileHeader();
                vectorFile.Write(rampData);
                vectorFile.Close();
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }

            #endregion

            #region--------------- 打开上述生成的文件，读取信息并校验。 ------------------

            vectorFile.Open(_filePath, FileMode.Open, FileAccess.Read);

            Assert.AreEqual(vectorFile.Maker.Producer, producer);

            Assert.AreEqual(vectorFile.Archive.DataTag, dataTag);
            Assert.AreEqual(vectorFile.Archive.DateTime, dateTime);
            Assert.AreEqual(vectorFile.Archive.Coordinate.Latitude, latitude);

            Assert.AreEqual(vectorFile.Storage.FileFormat, fileFormat);
            Assert.AreEqual(vectorFile.Storage.DataType, dataType);
            Assert.AreEqual(vectorFile.Storage.NumberOfChannels, numberOfChannels);

            Assert.AreEqual(vectorFile.Sampling.SampleRate, sampleRate);
            Assert.AreEqual(vectorFile.Sampling.Channels[0].RFFrequency, channelSamplingInfo.RFFrequency);
            Assert.AreEqual(vectorFile.Sampling.Channels[0].Bandwidth, channelSamplingInfo.Bandwidth);
            Assert.AreEqual(vectorFile.Sampling.Channels[0].RFScaleFactor, channelSamplingInfo.RFScaleFactor);
            Assert.AreEqual(vectorFile.Sampling.Channels[0].DigitizerScaleFactor, channelSamplingInfo.DigitizerScaleFactor);

            vectorFile.Close();

            #endregion

            #region--------------- 以VectorFile基类打开上述生成的文件（FixFrequencyStreamFile），读取信息并校验。 ------------------

            // 当不知道文件的具体格式时，可以先用基类（Vector File）来打开文件，获取文件的基础信息。此处即测试该功能。
            var baseVectorFile = new VectorFile();

            baseVectorFile.Open(_filePath, FileMode.Open, FileAccess.Read);

            Assert.AreEqual(baseVectorFile.Maker.Producer, producer);
            Assert.AreEqual(baseVectorFile.Archive.DataTag, dataTag);
            Assert.AreEqual(baseVectorFile.Storage.FileFormat, fileFormat);
            Assert.AreEqual(baseVectorFile.Storage.DataType, dataType);

            baseVectorFile.Close();

            #endregion

            #region---------------  测试多个线程同时打开同一文件的行为，应无异常。 ------------------

            MultiThreadOpenExistedVectorFileTest();

            #endregion

        }

        /// <summary>
        /// 测试FixFrequencyStream的数据写入和读取正确性，即测试Write(...)、Seek(...)、Read(...)等方法。
        /// 测试Buffered/NonBuffer不同模式的写入和读出，测试顺序读出和随机读出（Seek）。
        /// </summary>
        [TestMethod]
        public void FixFreqStreamFileWriteAndRead_ValidateData_I161Ch()
        {
            var vectorFile = new FixFrequencyStreamFile();

            // Try combination of buffer and non-buffered read/write.
            var isNoneBufferedWrite = new bool[] { true, true, false, false };
            var isNoneBufferedRead = new bool[] { true, false, true, false };

            // Generate a random sequence.
            int numberOfSamples = 65536 * 16;
            short[] rawData = new short[numberOfSamples];
            var randomGen = new Random();
            for (int i = 0; i < rawData.Length; i++) { rawData[i] = (short) (32767 * (randomGen.NextDouble() * 2 - 1)); }

            for (int j = 0; j < isNoneBufferedWrite.Length; j++)
            {

                #region---------------Create new file and write data------------------

                try
                {
                    vectorFile.Open(_filePath, FileMode.Create, FileAccess.Write, isNoneBufferedWrite[j]);

                    vectorFile.Storage.DataType = DataType.ComplexI16;
                    vectorFile.Storage.NumberOfChannels = 1;

                    vectorFile.Sampling.SampleRate = 1000000;
                    vectorFile.Sampling.Channels.Add(new BaseChannelSamplingInfo());
                    vectorFile.Sampling.Channels[0].RFFrequency = 566000000;
                    vectorFile.Sampling.Channels[0].Bandwidth = vectorFile.Sampling.SampleRate * 0.8;
                    vectorFile.Sampling.Channels[0].RFScaleFactor = 0.5;
                    vectorFile.Sampling.Channels[0].DigitizerScaleFactor = 0.00001;

                    vectorFile.WriteFileHeader();
                    vectorFile.Write(rawData);

                    vectorFile.Close();
                }
                catch (Exception ex) { Assert.Fail(ex.Message); }

                #endregion

                #region---------------Open file and check data ------------------

                int numberOfReads = 16;
                int readBlockSize = rawData.Length / numberOfReads;
                short[] correctData = new short[readBlockSize];
                short[] readData = new short[readBlockSize];

                #region---------------Sequencial data read test------------------
                try
                {
                    vectorFile.Open(_filePath, FileMode.Open, FileAccess.Read, isNoneBufferedRead[j]);
                    for (int i = 0; i < numberOfReads; i++)
                    {
                        Array.Copy(rawData, i * readBlockSize, correctData, 0, readBlockSize);
                        vectorFile.Read(readData);
                        CollectionAssert.AreEqual(correctData, readData);
                    }
                    vectorFile.Close();
                }
                catch (Exception ex) { Assert.Fail(ex.Message); }
                #endregion

                #region---------------Random data read test------------------
                try
                {
                    vectorFile.Open(_filePath, FileMode.Open, FileAccess.Read, isNoneBufferedRead[j]);
                    int[] randomBlockIndex = new int[] { 0, 2, 5, 0, 8, 1 };
                    int randomPositionInArray, randomPositionInSample;

                    int elementPerSample = (vectorFile.BytesPerSample / vectorFile.Storage.NumberOfChannels) / Marshal.SizeOf(rawData[0]);
                    for (int i = 0; i < randomBlockIndex.Length; i++)
                    {
                        randomPositionInArray = randomBlockIndex[i] * readBlockSize;
                        Array.Copy(rawData, randomPositionInArray, correctData, 0, readBlockSize);

                        randomPositionInSample = randomPositionInArray / elementPerSample;
                        vectorFile.Seek(randomPositionInSample, SeekOrigin.Begin);
                        vectorFile.Read(readData);
                        CollectionAssert.AreEqual(correctData, readData);
                    }
                    vectorFile.Close();
                }
                catch (Exception ex) { Assert.Fail(ex.Message); }
                #endregion

                #endregion

            }
        }

        #endregion

        #region  -------------------- Vector File FixFrequencyFrame Class Tests -----------------------

        /// <summary>
        /// 测试FixFrequencyFrame的基础功能：创建文件->写入数据->关闭文件->打开文件->校验数据信息（文件头内容）-> 关闭文件等。
        /// </summary>
        [TestMethod]
        public void FixFreqFrameFileCreateNew_ValidateHeader()
        {
            var vectorFile = new FixFrequencyFrameFile();
            var channelSamplingInfo = new BaseChannelSamplingInfo();

            string dataTag = "JXI Vector File Unit Test";
            string producer = "JXI Vector File Unit Test";
            var fileFormat = FileFormat.FixFrequencyFrame;
            var dataType = DataType.ComplexI16;
            var dateTime = DateTime.Now;
            int numberOfChannels = 1;
            double sampleRate = 1000000;
            double latitude = 23;

            int numOfDataFrames = 10;
            int frameLength = 65536;

            channelSamplingInfo.RFFrequency = 566000000;
            channelSamplingInfo.Bandwidth = sampleRate * 0.8;
            channelSamplingInfo.RFScaleFactor = 0.5;
            channelSamplingInfo.DigitizerScaleFactor = 0.00001;

            #region--------------- 根据上述信息，生成文件并写入一组随机数据。 ------------------

            try
            {
                vectorFile.Open(_filePath, FileMode.Create, FileAccess.Write);

                vectorFile.Maker.Producer = producer;

                vectorFile.Archive.DataTag = dataTag;
                vectorFile.Archive.DateTime = dateTime;
                vectorFile.Archive.Coordinate.Latitude = latitude;

                vectorFile.Storage.FileFormat = fileFormat;
                vectorFile.Storage.DataType = dataType;
                vectorFile.Storage.NumberOfChannels = numberOfChannels;

                vectorFile.Frame.SamplingInfoType = FixFreqFrameFileSamplingInfoType.Shared;
                vectorFile.Frame.Length = frameLength;

                vectorFile.Sampling.SampleRate = sampleRate;
                for (int i = 0; i < numberOfChannels; i++) { vectorFile.Sampling.Channels.Add(channelSamplingInfo); }

                short[] rampData = new short[frameLength * 2];
                for (int i = 0; i < rampData.Length; i++) { rampData[i] = (short)(i % 32768); }

                vectorFile.WriteFileHeader();
                for(int i =0; i < numOfDataFrames; i++) { vectorFile.Write(rampData); }
                vectorFile.Close();
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }

            #endregion

            #region--------------- 打开上述生成的文件，读取信息并校验。 ------------------

            vectorFile.Open(_filePath, FileMode.Open, FileAccess.Read);

            Assert.AreEqual(vectorFile.Maker.Producer, producer);

            Assert.AreEqual(vectorFile.Archive.DataTag, dataTag);
            Assert.AreEqual(vectorFile.Archive.DateTime, dateTime);
            Assert.AreEqual(vectorFile.Archive.Coordinate.Latitude, latitude);

            Assert.AreEqual(vectorFile.Storage.FileFormat, fileFormat);
            Assert.AreEqual(vectorFile.Storage.DataType, dataType);
            Assert.AreEqual(vectorFile.Storage.NumberOfChannels, numberOfChannels);

            Assert.AreEqual(vectorFile.Frame.Length, frameLength);
            Assert.AreEqual(vectorFile.NumberOfFrames, numOfDataFrames);
            Assert.AreEqual(vectorFile.Frame.SamplingInfoType, FixFreqFrameFileSamplingInfoType.Shared);

            Assert.AreEqual(vectorFile.Sampling.SampleRate, sampleRate);
            Assert.AreEqual(vectorFile.Sampling.Channels[0].RFFrequency, channelSamplingInfo.RFFrequency);
            Assert.AreEqual(vectorFile.Sampling.Channels[0].Bandwidth, channelSamplingInfo.Bandwidth);
            Assert.AreEqual(vectorFile.Sampling.Channels[0].RFScaleFactor, channelSamplingInfo.RFScaleFactor);
            Assert.AreEqual(vectorFile.Sampling.Channels[0].DigitizerScaleFactor, channelSamplingInfo.DigitizerScaleFactor);

            vectorFile.Close();

            #endregion

            #region--------------- 以VectorFile基类打开上述生成的文件（FixFrequencyStreamFile），读取信息并校验。 ------------------

            // 当不知道文件的具体格式时，可以先用基类（Vector File）来打开文件，获取文件的基础信息。此处即测试该功能。
            var baseVectorFile = new VectorFile();

            baseVectorFile.Open(_filePath, FileMode.Open, FileAccess.Read);

            Assert.AreEqual(baseVectorFile.Maker.Producer, producer);
            Assert.AreEqual(baseVectorFile.Archive.DataTag, dataTag);
            Assert.AreEqual(baseVectorFile.Storage.FileFormat, fileFormat);
            Assert.AreEqual(baseVectorFile.Storage.DataType, dataType);

            baseVectorFile.Close();

            #endregion

            #region---------------  测试多个线程同时打开同一文件的行为，应无异常。 ------------------

            MultiThreadOpenExistedVectorFileTest();

            #endregion

        }

        /// <summary>
        /// 测试FixFrequencyFrame的数据写入和读取正确性，即测试Write(...)、Seek(...)、Read(...)等方法。
        /// 测试Buffered/NonBuffer不同模式的写入和读出，测试顺序读出和随机读出（Seek）。
        /// </summary>
        [TestMethod]
        public void FixFreqFrameFileWriteAndRead_ValidateData_I161Ch()
        {
            var vectorFile = new FixFrequencyFrameFile();

            // Try combination of buffer and non-buffered read/write.
            var isNoneBufferedWrite = new bool[] { true, true, false, false };
            var isNoneBufferedRead = new bool[] { true, false, true, false };

            // Generate a random sequence.
            Random rn = new Random();
            int frameLength = 65536;
            int numOfFramesToWrite = 16;
            short[][] rawDataFrames = new short[numOfFramesToWrite][];
            for(int i = 0; i < numOfFramesToWrite; i++)
            {
                rawDataFrames[i] = new short[frameLength * 2];
                for (int j = 0; j < rawDataFrames[i].Length; j++) { rawDataFrames[i][j] = (short)(32767 * (rn.NextDouble() * 2 - 1)); }
            }

            for (int j = 0; j < isNoneBufferedWrite.Length; j++)
            {

                #region---------------Create new file and write data------------------

                try
                {
                    vectorFile.Open(_filePath, FileMode.Create, FileAccess.Write, isNoneBufferedWrite[j]);

                    vectorFile.Storage.DataType = DataType.ComplexI16;
                    vectorFile.Storage.NumberOfChannels = 1;

                    vectorFile.Sampling.SampleRate = 1000000;
                    vectorFile.Sampling.Channels.Add(new BaseChannelSamplingInfo());
                    vectorFile.Sampling.Channels[0].RFFrequency = 566000000;
                    vectorFile.Sampling.Channels[0].Bandwidth = vectorFile.Sampling.SampleRate * 0.8;
                    vectorFile.Sampling.Channels[0].RFScaleFactor = 0.5;
                    vectorFile.Sampling.Channels[0].DigitizerScaleFactor = 0.00001;

                    vectorFile.Frame.SamplingInfoType = FixFreqFrameFileSamplingInfoType.Shared;
                    vectorFile.Frame.Length = frameLength;

                    vectorFile.WriteFileHeader();
                    foreach(var frame in rawDataFrames) { vectorFile.Write(frame); }

                    vectorFile.Close();
                }
                catch (Exception ex) { Assert.Fail(ex.Message); }

                #endregion

                #region---------------Open file and check data ------------------

                #region---------------Sequencial data read test------------------
                try
                {
                    vectorFile.Open(_filePath, FileMode.Open, FileAccess.Read, isNoneBufferedRead[j]);

                    frameLength = vectorFile.Frame.Length;
                    short[] dataReadFromFile = new short[frameLength * 2];

                    for (int i = 0; i < vectorFile.NumberOfFrames; i++)
                    {
                        vectorFile.Read(dataReadFromFile);
                        CollectionAssert.AreEqual(rawDataFrames[i], dataReadFromFile);
                    }
                    vectorFile.Close();
                }
                catch (Exception ex) { Assert.Fail(ex.Message); }
                #endregion

                #region---------------Random data read test------------------
                try
                {
                    vectorFile.Open(_filePath, FileMode.Open, FileAccess.Read, isNoneBufferedRead[j]);

                    frameLength = vectorFile.Frame.Length;
                    short[] dataReadFromFile = new short[frameLength * 2];

                    int[] randomFrameIndex = new int[] { 0, 2, 5, 0, 8, 1 };

                    for (int i = 0; i < randomFrameIndex.Length; i++)
                    {
                        int frameIndex =randomFrameIndex[i];
                        vectorFile.Seek(frameIndex, SeekOrigin.Begin);
                        vectorFile.Read(dataReadFromFile);
                        CollectionAssert.AreEqual(rawDataFrames[frameIndex], dataReadFromFile);
                    }
                    vectorFile.Close();
                }
                catch (Exception ex) { Assert.Fail(ex.Message); }
                #endregion

                #endregion

            }


        }
      
        #endregion

    }
}

/*  不再使用的测试用例，暂不删除，留作参考。
 
        [TestMethod]
        public void VectorFileOpenExisted()
        {
            var vectorFile = new VectorFile();

            try
            {
                vectorFile.Open(_iqFilePath, FileMode.Open, FileAccess.Read);
                Console.WriteLine(vectorFile.Storage.FileFormat.ToString());
                Console.WriteLine(vectorFile.Storage.DataType.ToString());
                vectorFile.Close();
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }
    

            
        [TestMethod]
        public void FixFreqStreamFileRead_I16Data()
        {
            try
            {
                var vectorFile = new FixFrequencyStreamFile();
                vectorFile.Open(_iqFilePath, FileMode.Open, FileAccess.Read);

                Console.WriteLine("Data Type: " + vectorFile.Storage.DataType.ToString());
                Console.WriteLine("Sample Rate: " + vectorFile.Sampling.SampleRate.ToString("f3"));
                Console.WriteLine("RF Frequency: " + vectorFile.Sampling.Channels[0].RFFrequency.ToString("f3"));
                Console.WriteLine("Bandwidth: " + vectorFile.Sampling.Channels[0].Bandwidth.ToString("f3"));

                // Read all data into memory.
                int arraySize = (int)vectorFile.NumberOfSamples * vectorFile.BytesPerSample / sizeof(short);
                arraySize = Math.Min(arraySize, 1000000);
                short[] fullRawData = new short[arraySize];
                vectorFile.Seek(0, SeekOrigin.Begin);
                vectorFile.Read(fullRawData);

                vectorFile.Close();
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }


        [TestMethod]
        public void FixFreqFrameFileOpenExisted()
        {
            var vectorFile = new FixFrequencyFrameFile();

            try
            {
                vectorFile.Open(_iqFilePath, FileMode.Open, FileAccess.Read);

                Console.WriteLine(vectorFile.Storage.DataType.ToString());
                Console.WriteLine("Sample Rate:" + vectorFile.Sampling.SampleRate.ToString("f3"));
                Console.WriteLine("RF Frequency:" + vectorFile.Sampling.Channels[0].RFFrequency.ToString("f3"));
                Console.WriteLine("Bandwidth:" + vectorFile.Sampling.Channels[0].Bandwidth.ToString("f3"));

                Console.WriteLine("Frame Length:" + vectorFile.Frame.Length);

                vectorFile.Close();
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }

     
 */
