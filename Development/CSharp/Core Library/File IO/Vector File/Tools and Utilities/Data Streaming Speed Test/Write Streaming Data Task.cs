using SeeSharpTools.JXI.FileIO.VectorFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UtilityWin32WriteBlockSizeTest
{
    class WriteStreamingDataAndTestSpeedTask
    {
        #region ------------------- 私有成员 -------------------

        private FixFrequencyStreamFile _vectorFile;
        private int _fileIndex;
        private int _numberOfBlocks;
        private string _filePath;
        private sbyte[] _byteData;
        private IntPtr _byteDataInPtr;
        /// <summary>
        /// 用于停止Acquisition/Analysis Engine。
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;
        /// <summary>
        /// 流盘引擎。
        /// </summary>
        private Task _writeStreamingEngine;

        #endregion

        #region ------------------- 构造函数 -------------------

        public WriteStreamingDataAndTestSpeedTask(int fileIndex)
        {
            Length = 10;
            DisableBuffering = true;
            _writeStatus = new Status();
            _fileIndex = fileIndex;
            _fileParam = new SingleFileParam();
        }

        #endregion

        #region ------------------- 公共属性 -------------------

        /// <summary>
        /// 写入文件时长，s。
        /// </summary>
        public double Length { get; set; }

        public double NumberOfBlocks { get; set; }

        /// <summary>
        /// 禁用数据缓冲区。
        /// </summary>
        public bool DisableBuffering { get; set; }

        /// <summary>
        /// 文件名参数
        /// </summary>
        private SingleFileParam _fileParam;
        /// <summary>
        /// 文件名参数
        /// </summary>
        public SingleFileParam FileParam { get { return _fileParam; } }

        private Status _writeStatus;
        public Status WriteStatus { get { return _writeStatus; } }

        #endregion

        #region------------------------- 公共事件 -------------------------

        /// <summary>
        /// 任务完成时的委托。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void TaskCompletedEventHandler(object sender, TaskCompletedEventArgs e);
        /// <summary>
        /// 任务完成时触发本事件。
        /// </summary>
        public event TaskCompletedEventHandler TaskCompleted;

        #endregion

        #region ------------------- 公共方法 -------------------

        public void Start()
        {
            Commit();

            if (_cancellationTokenSource != null) { _cancellationTokenSource.Dispose(); }
            _cancellationTokenSource = new CancellationTokenSource();

            // 启动线程执行流盘操作。
            _writeStreamingEngine = new Task(WriteStreamingEngine);
            _writeStreamingEngine.Start();
        }

        public void Stop()
        {
            // 发送CacellationToken。
            _cancellationTokenSource.Cancel();

            // 等待完成。
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            bool isEngineStopped = false;
            while (!isEngineStopped && stopwatch.ElapsedMilliseconds < 100000)
            {
                // 查询_writeStreamingEngine是否都已完成。
                isEngineStopped = _writeStreamingEngine.Status != TaskStatus.Running;
                if (!isEngineStopped) { Thread.Sleep(0); }
            }

            // 确认线程已完成。
            if (isEngineStopped)
            {
                // 释放线程资源。
                _writeStreamingEngine.Dispose();
            }
            else
            {
                throw new InvalidOperationException("Failed to stop engine.");
            }
        }

        #endregion

        #region ------------------- 私有方法 -------------------

        private void Commit()
        {
            _filePath = _fileParam.RecordFolder + "\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +"_BlockSize_" + _fileParam.BlockSize.ToString("F0") + "_Length_" + Length.ToString("F0") + "_"+(_fileIndex+1).ToString()+".iq";
            _vectorFile = new FixFrequencyStreamFile();
            _vectorFile.Open(_filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, DisableBuffering);
            _vectorFile.Storage.FileFormat = FileFormat.FixFrequencyStream;
            _vectorFile.Storage.DataType = DataType.RealI8;
            _vectorFile.Storage.NumberOfChannels = 1;
            _vectorFile.Sampling.SampleRate = 1000000000;
            _vectorFile.Sampling.Channels.Add(new BaseChannelSamplingInfo());
            _vectorFile.Sampling.Channels[0].RFFrequency = 25E6; ;
            _vectorFile.Sampling.Channels[0].Bandwidth = 20E6;
            _vectorFile.WriteFileHeader();
            _byteData = new sbyte[(int)_fileParam.BlockSize];
            if(DisableBuffering)
            {
                byte[] byteData = (byte[])(Array)_byteData;
                _byteDataInPtr = Marshal.AllocHGlobal(byteData.Length);
                Marshal.Copy(byteData, 0, _byteDataInPtr, byteData.Length);
            }
        }

        private void WriteStreamingEngine()
        {
            _numberOfBlocks = 0;
            long totalBytesToWrites = 0;
            var stopwatchForWriteDiagnostics = new Stopwatch();
            stopwatchForWriteDiagnostics.Restart();
            try
            {
                while (!_cancellationTokenSource.IsCancellationRequested&&_numberOfBlocks < NumberOfBlocks)
                {
                    if (DisableBuffering) { _vectorFile.Write(_byteDataInPtr,(int)_fileParam.BlockSize); }
                    else { _vectorFile.Write(_byteData); }
                    _numberOfBlocks++;
                    totalBytesToWrites += (long)_fileParam.BlockSize;
                    _writeStatus.progress = (_numberOfBlocks * 1.0 / NumberOfBlocks*1.0) * 100;

                    // 如果调用者调用了Stop()方法，则退出循环。
                    if (_cancellationTokenSource.IsCancellationRequested) { break; }

                }//主While循环

                _writeStatus.WriteLength = stopwatchForWriteDiagnostics.Elapsed.TotalSeconds;
            }
            catch(Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                _vectorFile.Close();
                _writeStatus.WriteSpeed = totalBytesToWrites * 1.0 / _writeStatus.WriteLength;
                _writeStatus.progress = (_numberOfBlocks * 1.0 / NumberOfBlocks * 1.0) * 100;
                TaskCompleted?.Invoke(this, new TaskCompletedEventArgs(_fileIndex));
            }
        }

        #endregion
    }

    /// <summary>
    /// 单一文件的参数。
    /// </summary>
    public class SingleFileParam
    {
        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 块大小，samples。
        /// </summary>
        public double BlockSize { get; set; }

        /// <summary>
        /// 存储数据的文件目录，在此目录下自动生成文件名（根据通道号、用户设定的文件名前缀等）。
        /// </summary>
        public string RecordFolder { get; set; }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(SingleFileParam source)
        {
            this.BlockSize = source.BlockSize;
            this.RecordFolder = source.RecordFolder;
        }

        #endregion

    }

    /// <summary>
    /// 任务数据有效的事件参数。
    /// </summary>
    public class TaskCompletedEventArgs : EventArgs
    {
        private int _fileIndex;
        /// <summary>
        /// 数据计数。
        /// </summary>
        public int FileIndex { get { return _fileIndex; } }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="fileName"></param>
        public TaskCompletedEventArgs(int fileIndex ) { _fileIndex = fileIndex; }
    }

    public class Status
    {
        public double progress { get; set; }
        public double WriteSpeed { get; set; }
        public double WriteLength { get; set; }
    }

}
