using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using SeeSharpTools.JXI.FileIO.VectorFile;

namespace SeeSharpTools.JXI.FileIO.VectorFile.Tests
{
    [TestClass()]
    public class FixFreqStreamDataPreviewerTests
    {

        FixFrequencyStreamDataPreviewer _previewer;

        /// <summary>
        /// 测试用的临时文件路径，各测试方法将生成所需的数据文件用于Preview，在测试完成后（析构），该临时文件会被删除。
        /// </summary>
        private static string _filePath;

        /// <summary>
        /// 用于存放异步线程出现的异常。
        /// </summary>
        Queue<Exception> _exceptionQueue;

        #region  -------------------- 构造和析构 -----------------------

        public FixFreqStreamDataPreviewerTests()
        {
            try
            {
                // 临时数据文件放在当前路径。
                _filePath = Environment.CurrentDirectory + "\\Temp.IQ";

                // Create preview task instance and subscribe events.
                _previewer = new FixFrequencyStreamDataPreviewer();
                _previewer.ProgressChanged += Previewer_ProgressChanged;
                _previewer.RunWorkerCompleted += Previewer_RunWorkerCompleted;

                _exceptionQueue = new Queue<Exception>();
            }
            catch (Exception exception) {Assert.Fail(exception.Message); }
        }

        [ClassCleanup]
        public static void AllTestsComplete()
        {
            // 删除测试中生成的临时数据文件。
            if (File.Exists(_filePath)) { File.Delete(_filePath); }
        }

        #endregion

        [TestMethod()]
        public void FixFrequencyStreamDataPreviewerTest()
        {
            try
            {

                #region  -------------------- 生成一个包含随机数据的FixFrequencyStream文件用于Preview -----------------------

                var vectorFile = new FixFrequencyStreamFile();

                vectorFile.Open(_filePath, FileMode.Create, FileAccess.Write);

                vectorFile.Storage.DataType = DataType.ComplexI16;
                vectorFile.Storage.NumberOfChannels = 1;

                vectorFile.Sampling.SampleRate = 1000000;
                vectorFile.Sampling.Channels.Add(new BaseChannelSamplingInfo());
                vectorFile.Sampling.Channels[0].RFFrequency = 566000000;
                vectorFile.Sampling.Channels[0].Bandwidth = vectorFile.Sampling.SampleRate * 0.8;
                vectorFile.Sampling.Channels[0].RFScaleFactor = 0.5;
                vectorFile.Sampling.Channels[0].DigitizerScaleFactor = 0.00001;

                vectorFile.WriteFileHeader();

                // Generate a random sequence.
                int numOfSamplesPerBlock = 65536 * 16;
                var rawData = new short[numOfSamplesPerBlock];
                var randomGen = new Random();
                for (int i = 0; i < rawData.Length; i++) { rawData[i] = (short)(32767 * (randomGen.NextDouble() * 2 - 1)); }

                // 重复写入上面生成的随机数据，直至写入的总长度超过预设值。
                int numOfBlocks = (int)(10E6) / numOfSamplesPerBlock;
                for (int i = 0; i < numOfBlocks; i++) { vectorFile.Write(rawData); }

                vectorFile.Close();

                #endregion

                _previewer.FilePath = _filePath;

                // Set selection to full range.
                long numberOfSamples = _previewer.NumberOfSamples;
                _previewer.SelectionStart = 0;
                _previewer.SelectionEnd = numberOfSamples - 1;
                _previewer.Unit = VectorDataAmplitudeUnit.uV;
                _previewer.PreviewLengthAuto = true;

                // Commit and get actual settings.
                _previewer.Commit();
                int previewLength = _previewer.PreviewLength;
                double decimation = _previewer.Decimation;
                double[] data = new double[previewLength];

                // Start task and wait until done.
                _previewer.Start();
                do { Thread.Sleep(10); } while (_previewer.IsBusy) ;
            }
            catch (Exception exception) {Assert.Fail(exception.Message); }

            // Wait for complete event handler done and check if exception occured.
            Thread.Sleep(1000);
            if (_exceptionQueue.Count > 0) { Assert.Fail(_exceptionQueue.Dequeue().Message); }
        }

        private void Previewer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check if exception occured during task running.
            if (e.Error != null) { _exceptionQueue.Enqueue(e.Error); return; }

            // Get final status.
            var taskResult = (VectorDataPreviewResult)e.Result;
            Debug.WriteLine("Preview result: " + taskResult.IsCompletePreview + taskResult.PreviewRatio);
        }

        private void Previewer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _previewer.WorkerReportsProgress = false;
            Debug.WriteLine("Progress:" + e.ProgressPercentage);
            _previewer.WorkerReportsProgress = true;
        }

    }
}