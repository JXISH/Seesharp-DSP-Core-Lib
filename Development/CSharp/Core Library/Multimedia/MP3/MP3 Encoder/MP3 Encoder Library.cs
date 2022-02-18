using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SeeSharpTools.JXI.Public.Struct;
using static SeeSharpTools.JXI.Multimedia.MP3.MP3Core;

namespace SeeSharpTools.JXI.Multimedia.MP3
{
   public class MP3EncoderLegacy
    {
        #region ------------------- 常量 -------------------

        private const uint dwConfigID = 256;
        private const uint dwStrutVersion = 1;
        private const uint dwStructSize = 331;
        private const uint dwReSampleRate = 0;

        private const uint dwMaxBitrate = 0;
        private const int nPreset = 0;
        private const uint dwMpegVersion = 0;
        private const uint dwPsyModel = 0;
        private const uint dwEmphasis = 0;

        private const int bCopyright = 0;
        private const int bCRC = 0;
        private const int bOriginal = 0;
        private const int bPrivate = 0;
        private const int bWritVBRHeader = 0;
        private const int bEnableVBR = 0;
        private const int nVBRQuality = 0;
        private const uint dwVbrAbr_bps = 0;
        private const int nVbrMethod = 0;
        private const int bNoRes = 0;

        private const int bStrictlso = 0;
        private const short nQuality = 0;
        private const string reserver = "";

        #endregion

        #region -------------------私有成员----------------
        /// <summary>
        /// 解调初始化句柄参数
        /// </summary>
        private DecoderConfig _decoderConfig;

        /// <summary>
        /// 解调句柄
        /// </summary>
        private uint _streamHandle;
        /// <summary>
        /// 单次解调数据大小
        /// </summary>
        private uint _processBlockSize;
        /// <summary>
        /// 单次解调输出的最大空间
        /// </summary>
        private uint _decodedBlockSize;

        /// <summary>
        /// 解调状态
        /// </summary>
        private bool _encodeTaskStaus = false;

        /// <summary>
        /// 输入数据源
        /// </summary>
        private CircularBuffer<short> _inputDataBuffer;
        /// <summary>
        /// 输入数据源读取
        /// </summary>
        private CircularBufferReader<short> _readDataBuffer;
        /// <summary>
        /// 解调输出数据
        /// </summary>
        private List<byte> _outPutBufferDataList;

        /// <summary>
        /// 资源锁
        /// </summary>
        private Object _resourceLock;

        #endregion

        #region -----------------公共属性------------------
        private uint _dwSampleRate;
        /// <summary>
        /// sound sample rate (32000/44100/48000)
        /// </summary>
        public uint SoundSampleRate
        {
            get
            {
                return _dwSampleRate;
            }

            set
            {
                _dwSampleRate = value;
                _decoderConfig.dwSampleRate = SoundSampleRate;
            }
        }

        private int _nMode;
        /// <summary>
        /// sound mode (0:Stereo, 2:DualCh, 3:Mono)
        /// </summary>
        public int SoundMode
        {
            get
            {
                return _nMode;
            }

            set
            {
                _nMode = value;
                _decoderConfig.nMode = SoundMode;
            }
        }

        private uint _dwBitrate;
        /// <summary>
        /// encoded bit rate (32*n, n<=10)(kbps)
        /// </summary>
        public uint EncodeedBitrate
        {
            get
            {
                return _dwBitrate;
            }

            set
            {
                _dwBitrate = value;
                _decoderConfig.dwBitrate = EncodeedBitrate;
            }
        }

        /// <summary>
        ///  输出信号缓存
        /// </summary>
        public List<byte> OutPutEncodedData { get { return _outPutBufferDataList; } }

        #endregion

        #region -----------------构造函数------------------
        public MP3EncoderLegacy(int inputDataLength , uint sampleRate, int channels, uint encodeedBitrate)
        {

            _encodeTaskStaus = false;
            _dwSampleRate = sampleRate;
            _dwBitrate = encodeedBitrate;
            if (channels == 1) { _nMode = 3; } // 3:Mono
            else { _nMode = 0; }  // Stereo

            #region -----初始还参数-------          
            _decoderConfig.dwSampleRate = sampleRate;
            _decoderConfig.nMode = _nMode;
            _decoderConfig.dwBitrate = encodeedBitrate;

            _decoderConfig.dwConfigID = dwConfigID;
            _decoderConfig.dwStrutVersion = dwStrutVersion;
            _decoderConfig.dwStructSize = dwStructSize;
            _decoderConfig.dwReSampleRate = dwReSampleRate;

            _decoderConfig.dwMaxBitrate = dwMaxBitrate;
            _decoderConfig.nPreset = 0;
            _decoderConfig.dwMpegVersion = 0;
            _decoderConfig.dwPsyModel = 0;
            _decoderConfig.dwEmphasis = 0;

            _decoderConfig.bCopyright = 0;
            _decoderConfig.bCRC = 0;
            _decoderConfig.bOriginal = bOriginal;
            _decoderConfig.bPrivate = bPrivate;
            _decoderConfig.bWritVBRHeader = bWritVBRHeader;
            _decoderConfig.bEnableVBR = bEnableVBR;
            _decoderConfig.nVBRQuality = nVBRQuality;
            _decoderConfig.dwVbrAbr_bps = dwVbrAbr_bps;
            _decoderConfig.nVbrMethod = nVbrMethod;
            _decoderConfig.bNoRes = bNoRes;

            _decoderConfig.bStrictlso = bStrictlso;
            _decoderConfig.nQuality = nQuality;
            _decoderConfig.reserver = reserver;

            #endregion

            MP3Initialize();
            _resourceLock = new object();

            // 把原始数据写入队列中
            _inputDataBuffer = new CircularBuffer<short>(inputDataLength);
            // _inputDataBuffer.Enqueue(inputData, 10000);
            //初始化对队列
            _readDataBuffer = new CircularBufferReader<short>(_inputDataBuffer.BufferHandle);
            //初始化解调输出
            _outPutBufferDataList = new List<byte>();

        }
     
        ~MP3EncoderLegacy()
        {
            this.Dispose();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_encodeTaskStaus) { MP3Close(_streamHandle); }        
            if (_inputDataBuffer != null) { _inputDataBuffer.Dispose(); _inputDataBuffer = null; }      
            if (_readDataBuffer != null) { _readDataBuffer.Dispose(); _readDataBuffer = null; }

        }

        #endregion

        #region -----------------公共方法------------------

        /// <summary>
        /// 手动执行DRA解压缩
        /// </summary>
        public void Proccess(short[] inputData)
        {
            //把解调前数据写入队列中
            _inputDataBuffer.Enqueue(inputData, 5000);
            // 清空输出数据
            _outPutBufferDataList.Clear();

            lock (_resourceLock)
            {
                var tmpInput = new short[(int)_processBlockSize];
                var tmpOutput = new byte[(int)_decodedBlockSize];
                uint encodeDataSize = 0;

                // 按照计算的_processBlockSize大小，循环解码
                while (_readDataBuffer.NumOfElements >= _processBlockSize)
                {
                    _readDataBuffer.Dequeue(ref tmpInput, 500);
                    MP3Encode(_streamHandle, _processBlockSize, tmpInput, tmpOutput, out encodeDataSize);

                    if (encodeDataSize > 0)
                    {
                        var encodeData = new byte[encodeDataSize];
                        Array.Copy(tmpOutput, encodeData, encodeDataSize);
                        _outPutBufferDataList.AddRange(encodeData);
                        //_outPutBufferWriter.Enqueue(tmpOutput, 0, (int)encodeDataSize, 1000);
                    }
                }
                // 最后一帧数据解码
                if (_readDataBuffer.NumOfElements > 0 && _readDataBuffer.NumOfElements < _processBlockSize)
                {
                    MP3EndEncode(_streamHandle, tmpOutput, out encodeDataSize);
                    if (encodeDataSize > 0)
                    {
                        var encodeData = new byte[encodeDataSize];
                        Array.Copy(tmpOutput, encodeData, encodeDataSize);
                        _outPutBufferDataList.AddRange(encodeData);
                        // _outPutBufferWriter.Enqueue(tmpOutput, 0, (int)encodeDataSize, 200);
                    }
                }
                // 关闭解码句柄
                MP3Close(_streamHandle);
            }
        }
        #endregion

        #region ----------------私有方法-------------------       
        /// <summary>
        /// 初始化MP3编码
        /// </summary>
        private void MP3Initialize()
        {
            int status;
            status = MP3Core.Initialize(ref _decoderConfig, ref _processBlockSize, ref _decodedBlockSize, ref _streamHandle);
            if (status != 0) { throw new InvalidOperationException("Initial MP3 Encoding Failed." + status.ToString()); }
            _encodeTaskStaus = true;

        }
        /// <summary>
        /// MP3编码(数据大小必须是process block size)
        /// </summary>
        /// <param name="stramHandle"></param>
        /// <param name="dataSize"></param>
        /// <param name="inputData"></param>
        /// <param name="encodedData"></param>
        /// <param name="encodeDataSize"></param>
        private void MP3Encode(uint stramHandle, uint dataSize, short[] inputData, byte[] encodedData, out uint encodeDataSize)
        {
            int status;        
            status = MP3Core.MP3EncodeChunk(stramHandle, dataSize, inputData, encodedData, out encodeDataSize);         
            if (status != 0) { throw new InvalidOperationException("MP3 Encoding failed with error:" + status.ToString()); }
        }
        /// <summary>
        /// 最后一帧MP3编码
        /// </summary>
        /// <param name="stramHandle"></param>
        /// <param name="encodedData"></param>
        /// <param name="encodeDataSize"></param>
        private void MP3EndEncode(uint stramHandle, byte[] encodedData, out uint encodeDataSize)
        {
            int status;
            status = MP3Core.MP3DeinitEcodeing(stramHandle, encodedData, out encodeDataSize);
            if (status != 0) { throw new InvalidOperationException("MP3 End Encoding failed with error:" + status.ToString()); }
        }
        /// <summary>
        /// 关闭编码
        /// </summary>
        /// <param name="stramHandle"></param>
        private void MP3Close(uint stramHandle)
        {
            int status;
            status = MP3Core.MP3CloseStream(stramHandle);
            if (status != 0) { throw new InvalidOperationException("MP3 Encode  Close failed with error:" + status.ToString()); }
            _encodeTaskStaus = false;
        }

        #endregion

    }

    /// <summary>
    /// MP3编码类，将输入的PCM音频流持续进行MP3编码后输出码流。输入的PCM音频流和输出的MP3码流为Circular Buffer类。
    /// </summary>
    public class MP3Encoder : IDisposable
    {

        #region ----------------------- 常量 -------------------------------
        #region -------编码初始化相关常量     
        private const uint dwConfigID = 256;
        private const uint dwStrutVersion = 1;
        private const uint dwStructSize = 331;
        private const uint dwReSampleRate = 0;

        private const uint dwMaxBitrate = 0;
        private const int nPreset = 0;
        private const uint dwMpegVersion = 0;
        private const uint dwPsyModel = 0;
        private const uint dwEmphasis = 0;

        private const int bCopyright = 0;
        private const int bCRC = 0;
        private const int bOriginal = 0;
        private const int bPrivate = 0;
        private const int bWritVBRHeader = 0;
        private const int bEnableVBR = 0;
        private const int nVBRQuality = 0;
        private const uint dwVbrAbr_bps = 0;
        private const int nVbrMethod = 0;
        private const int bNoRes = 0;

        private const int bStrictlso = 0;
        private const short nQuality = 0;
        private const string reserver = "";
        #endregion

        #endregion

        #region------------------------- 私有成员 -------------------------

        /// <summary>
        /// 实现MP3编码的后台引擎对象，调用方法Start()时启动，调用方法Stop()时关闭。
        /// 功能：从InputBuffer获取原始PCM音频，进行MP3编码后输出至OutputBuffer。
        /// </summary>
        private Task _processEngine;

        /// <summary>
        /// 用于停止引擎的变量。
        /// </summary>
        private CancellationTokenSource _ctsForProcessEngine;

        /// <summary>
        /// 用于实现“同步处理”当前Stream最后一段的控制量。
        /// </summary>
        private bool _isStreamEnd;

        /// <summary>
        /// 用于实现“同步”处理当前Stream最后一段的信号量。
        /// </summary>
        private AutoResetEvent _streamEndEvent;

        /// <summary>
        /// 解调初始化句柄参数
        /// </summary>
        private DecoderConfig _decoderConfig;
        /// <summary>
        /// 解调句柄
        /// </summary>
        private uint _streamHandle;
        /// <summary>
        /// 单次解调数据大小
        /// </summary>
        private uint _processBlockSize;
        /// <summary>
        /// 单次解调输出的最大空间
        /// </summary>
        private uint _decodedBlockSize;
        /// <summary>
        /// 解调状态
        /// </summary>
        private bool _encodeTaskStaus = false;
        /// <summary>
        /// 读取输入数据源
        /// </summary>
        private CircularBufferReader<short> _inputBufferRead;     
        /// <summary>
        /// 资源锁
        /// </summary>
        private Object _resourceLock;

        /// <summary>
        /// 音频采样率(32000/44100/48000)
        /// </summary>
        private uint _dwSampleRate;
        /// <summary>
        /// 音频模式 (0:Stereo, 2:DualCh, 3:Mono)
        /// </summary>
        private int _nMode;
        /// <summary>
        /// MP3的比特率 (32*n, n<=10)(kbps)
        /// </summary>
        private uint _dwBitrate;
       
        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 实例化编码器对象。
        /// </summary>
        /// <param name="pcmSampleRate">PCM音频的采样率。</param>
        /// <param name="pcmNumOfChannels">PCM音频的通道数。</param>
        /// <param name="mp3Bitrate">编码后的MP3的码速率。</param>
        public MP3Encoder(double pcmSampleRate, int pcmNumOfChannels, double mp3Bitrate)
        {

            _encodeTaskStaus = false;
            _dwSampleRate = (uint)pcmSampleRate;
            _dwBitrate = (uint)(mp3Bitrate/1000);
            if (pcmNumOfChannels == 1) { _nMode = 3; } // 3:Mono
            else { _nMode = 0; }  // Stereo

            #region -------------初始化解码参数--------------          
            _decoderConfig.dwSampleRate = (uint)pcmSampleRate;
            _decoderConfig.nMode = _nMode;
            _decoderConfig.dwBitrate = (uint)(mp3Bitrate / 1000);

            _decoderConfig.dwConfigID = dwConfigID;
            _decoderConfig.dwStrutVersion = dwStrutVersion;
            _decoderConfig.dwStructSize = dwStructSize;
            _decoderConfig.dwReSampleRate = dwReSampleRate;

            _decoderConfig.dwMaxBitrate = dwMaxBitrate;
            _decoderConfig.nPreset = 0;
            _decoderConfig.dwMpegVersion = 0;
            _decoderConfig.dwPsyModel = 0;
            _decoderConfig.dwEmphasis = 0;

            _decoderConfig.bCopyright = 0;
            _decoderConfig.bCRC = 0;
            _decoderConfig.bOriginal = bOriginal;
            _decoderConfig.bPrivate = bPrivate;
            _decoderConfig.bWritVBRHeader = bWritVBRHeader;
            _decoderConfig.bEnableVBR = bEnableVBR;
            _decoderConfig.nVBRQuality = nVBRQuality;
            _decoderConfig.dwVbrAbr_bps = dwVbrAbr_bps;
            _decoderConfig.nVbrMethod = nVbrMethod;
            _decoderConfig.bNoRes = bNoRes;

            _decoderConfig.bStrictlso = bStrictlso;
            _decoderConfig.nQuality = nQuality;
            _decoderConfig.reserver = reserver;

            #endregion

            _resourceLock = new object();
           // _streamEndEvent = new AutoResetEvent(false);

               // 设置标志位。
              _isRunning = false;
             _isStreamEnd = false;
        }

        #endregion

        #region------------------------- 公共属性 -------------------------

        private bool _isRunning;
        /// <summary>
        /// 当前MP3编码引擎是否正在运行。
        /// </summary>
        public bool IsRunning { get { return _isRunning; } }

        private CircularBuffer<short> _inputBuffer;
        /// <summary>
        /// 用于输入PCM音频的Buffer对象。用户须自行创建Buffer对象后赋值给本属性。
        /// </summary>
        public CircularBuffer<short> InputBuffer
        {
            get { return _inputBuffer; }
            set { _inputBuffer = value; }
        }

        private CircularBuffer<byte> _outputBuffer;
        /// <summary>
        /// 用于输出MP3码流的Buffer对象。用户须自行创建Buffer对象后赋值给本属性。
        /// </summary>
        public CircularBuffer<byte> OutputBuffer
        {
            get { return _outputBuffer; }
            set { _outputBuffer = value; }
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 启动MP3编码引擎。
        /// </summary>
        public void Start()
        {
            //初始化对队列
            _inputBufferRead = new CircularBufferReader<short>(_inputBuffer.BufferHandle);
            // 初始化编码
            MP3Initialize();
           
            // 启动后台引擎。
            _ctsForProcessEngine = new CancellationTokenSource();
            _processEngine = new Task(ProcessEngine);
            _processEngine.Start();
                              
          _isRunning = true;
        }

        /// <summary>
        /// 停止MP3编码引擎。
        /// </summary>
        public void Stop()
        {
            // 发送CacellationToken以停止后台引擎。
            _ctsForProcessEngine.Cancel();

            // 等待引擎结束运行。
            TaskUtility.WaitForEngineComplete(_processEngine, "MP3 Encoder Engine");
        }

        /// <summary>
        /// 告知MP3Encoder“当前PCM数据流已提交所有数据”，让MP3Encoder将缓冲的数据全部输出到OutputBuffer。
        /// </summary>
        public void EndCurrentStream()
        {
            // 设置标志位，并等待引擎对当前Stream的最后一段数据处理完成。
            _isStreamEnd = true;
           // _streamEndEvent.WaitOne(1000);

        }

        #region------------------------- 公共事件 -------------------------

        /// <summary>
        /// 在运行中出现异步异常时触发的事件，比如任务调度（线程的自动分配和运行）中发生异常。
        /// </summary>
        public event AggregateExceptionOccuredEventHandler AggregateExceptionOccured;

        #endregion

        #endregion

        #region------------------------- 私有方法 -------------------------

        /// <summary>
        /// 实现MP3编码的后台引擎对象，调用方法Start()时启动，调用方法Stop()时关闭。
        /// 功能：从InputBuffer获取原始PCM音频，进行MP3编码后输出至OutputBuffer。
        /// </summary>
        private void ProcessEngine()
        {
            try
            {
                TaskStatus status = _processEngine.Status;

                var tmpInput = new short[(int)_processBlockSize];
                var tmpOutput = new byte[(int)_decodedBlockSize];
                uint encodeDataSize = 0;

                while (!_ctsForProcessEngine.IsCancellationRequested)
                {
                  
                    // 按照计算的_processBlockSize大小，循环解码
                    if (_inputBufferRead.NumOfElements >= _processBlockSize)
                    {
                        // 取数据并解码
                        _inputBufferRead.Dequeue(ref tmpInput, 200);
                        MP3Encode(_streamHandle, _processBlockSize, tmpInput, tmpOutput, out encodeDataSize);
                        // 存储解码数据
                        if (encodeDataSize > 0)
                        {                        
                            _outputBuffer.Enqueue(tmpOutput, 0, (int)encodeDataSize, 1000);
                        }
                    }

                    if (_isStreamEnd && _inputBufferRead.NumOfElements > 0 && _inputBufferRead.NumOfElements < _processBlockSize)
                    {
                        // 对最后一个Block进行处理。
                        MP3EndEncode(_streamHandle, tmpOutput, out encodeDataSize);
                        if (encodeDataSize > 0)
                        {
                            _outputBuffer.Enqueue(tmpOutput, 0, (int)encodeDataSize, 200);
                        }

                        // 已完成处理，清除标志位并触发事件。
                        _isStreamEnd = false;
                      //  _streamEndEvent.Set();

                        break;
                    }

                }
            }
            catch (Exception exception)
            {
                // 输出调试信息。
                Debug.WriteLine(DateTime.Now + " MP3 Encoder engine error : " + exception.Message);

                // 若调用者订阅了异常事件，则触发事件，用BeginInvoke异步触发，以避免与Stop()方法中WaitForEngineComplete()的互锁。
                AggregateExceptionOccured?.BeginInvoke(this, new AggregateExceptionOccuredEventArgs(DateTime.Now, "MP3 Encoder Engine", exception), null, null);
            }
            finally
            {
                // 总是情况IsRunning标志位。
                _isRunning = false;
                // 关闭MP3解码
                MP3Close(_streamHandle);
            }

        }

        /// <summary>
        /// 初始化MP3编码
        /// </summary>
        private void MP3Initialize()
        {
            int status;
            status = MP3Core.Initialize(ref _decoderConfig, ref _processBlockSize, ref _decodedBlockSize, ref _streamHandle);
            if (status != 0) { throw new InvalidOperationException("Initial MP3 Encoding Failed." + status.ToString()); }
            _encodeTaskStaus = true;

        }
        /// <summary>
        /// MP3编码(数据大小必须是process block size)
        /// </summary>
        /// <param name="stramHandle"></param>
        /// <param name="dataSize"></param>
        /// <param name="inputData"></param>
        /// <param name="encodedData"></param>
        /// <param name="encodeDataSize"></param>
        private void MP3Encode(uint stramHandle, uint dataSize, short[] inputData, byte[] encodedData, out uint encodeDataSize)
        {
            int status;
            status = MP3Core.MP3EncodeChunk(stramHandle, dataSize, inputData, encodedData, out encodeDataSize);
            if (status != 0) { throw new InvalidOperationException("MP3 Encoding failed with error:" + status.ToString()); }
        }
        /// <summary>
        /// 最后一帧MP3编码
        /// </summary>
        /// <param name="stramHandle"></param>
        /// <param name="encodedData"></param>
        /// <param name="encodeDataSize"></param>
        private void MP3EndEncode(uint stramHandle, byte[] encodedData, out uint encodeDataSize)
        {
            int status;
            status = MP3Core.MP3DeinitEcodeing(stramHandle, encodedData, out encodeDataSize);
            if (status != 0) { throw new InvalidOperationException("MP3 End Encoding failed with error:" + status.ToString()); }
        }
        /// <summary>
        /// 关闭编码
        /// </summary>
        /// <param name="stramHandle"></param>
        private void MP3Close(uint stramHandle)
        {
            int status;
            status = MP3Core.MP3CloseStream(stramHandle);
            if (status != 0) { throw new InvalidOperationException("MP3 Encode  Close failed with error:" + status.ToString()); }
            _encodeTaskStaus = false;
        }

        #endregion

        #region------------------------- IDisposable接口实现 -------------------------

        /// <summary>
        /// 标记位用于检测冗余调用，避免重复调用Dispose()和析构。
        /// </summary>
        private bool _isDisposed = false;

        /// <summary>
        /// 释放资源。
        /// </summary>
        /// <param name="disposing">当前的释放资源的操作是主动（true，即用户调用了Dispose()方法）还是被动（false，即析构调用）。</param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // disposing参数为true（即用户通过调用Dispose()方法主动释放资源），在此释放需要Dispose的托管对象。
                    // 注：若disposing参数为false（在析构中调用），则不应在此释放托管对象（即使这些托管对象实现了IDisposable接口），因为这些托管对象的状态未知（析构的不确定性）。
                    DisposeManaged();
                }

                // 在此释放本类中直接占用的非托管的资源（如Marshal.Alloc的内存)。（注：包含非托管资源的托管对象已在 if (dispoing) {...}中释放）。
                // TODO:  在以下内容中替代终结器。将大型字段设置为 null。
                DisposeUnmanaged();

                _isDisposed = true;
            }
        }

        /// <summary>
        /// 关闭对象并释放资源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // 已主动释放资源，故告知GC不再需要调用本对象的终结器（析构函数）。
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~MP3Encoder() { Dispose(false); }

        /// <summary>
        /// 释放托管资源。派生类应重写此方法释放派生类的托管资源。
        /// </summary>
        protected virtual void DisposeManaged()
        {
            // 如果引擎仍在运行，则停止引擎。
            if(_isRunning) { this.Stop(); }
            if (_inputBufferRead != null) { _inputBufferRead?.Dispose(); };
            _processEngine.Wait(1000);
        }

        /// <summary>
        /// 释放直接占用的非托管资源（如Marshal.Alloc的内存）。派生类若申请了非托管资源，则应重写此方法。
        /// </summary>
        protected virtual void DisposeUnmanaged()
        {
            // 释放MP3编码DLL的资源。

        }

        #endregion

    }

    /// <summary>
    /// MP3编码类，将输入的PCM音频流进行MP3编码后输出码流。
    /// </summary>
    public class EasyMp3Encoder : IDisposable
    {

        #region------------------------- 常量 -------------------------

        /// <summary>
        /// 内部缓冲区长度，以秒为单位。
        /// </summary>
        public static double BufferSizeInSec = 1;

        #endregion

        #region------------------------- 私有成员 -------------------------

        private MP3Encoder _mp3Encoder;

        private CircularBuffer<short> _inputBuffer;
        private CircularBuffer<byte> _outputBuffer;
        private CircularBufferReader<byte> _outputReader;

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 实例化编码器对象。
        /// </summary>
        /// <param name="pcmSampleRate">PCM音频的采样率。</param>
        /// <param name="pcmNumOfChannels">PCM音频的通道数。</param>
        /// <param name="mp3Bitrate">编码后的MP3的码速率。</param>
        public EasyMp3Encoder(double pcmSampleRate, int pcmNumOfChannels, double mp3Bitrate)
        {
            // 创建Input/Output Buffer对象。
            _inputBuffer = new CircularBuffer<short>((int)(pcmSampleRate * pcmNumOfChannels * BufferSizeInSec));
            _outputBuffer = new CircularBuffer<byte>((int)((mp3Bitrate / 8) * BufferSizeInSec));

            // 实例化OutputBuffer的Reader对象。
            _outputReader = new CircularBufferReader<byte>(_outputBuffer, CircularBufReaderType.Lossless);

            // 实例化MP3Encoder，创建Buffer并启动。        
            _mp3Encoder = new MP3Encoder(pcmSampleRate, pcmNumOfChannels, mp3Bitrate);
            _mp3Encoder.InputBuffer = _inputBuffer;
            _mp3Encoder.OutputBuffer = _outputBuffer;         
           _mp3Encoder.Start();           
        }

        #endregion

        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 当前可读取的MP3码流长度，字节数。
        /// </summary>
        public int AvailableMP3Bytes { get { return _outputReader.NumOfElements; } }

        #endregion

        #region------------------------- 公共方法 -------------------------

       
        /// <summary>
        /// 送入PCM音频数据。
        /// </summary>
        /// <param name="pcmData"></param>
        /// <param name="timeout"></param>
        public void Enqueue(short[] pcmData, int timeout) { _inputBuffer.Enqueue(pcmData, timeout); }

        /// <summary>
        /// 读取MP3数据。
        /// </summary>
        /// <param name="mp3Stream"></param>
        /// <param name="timeout"></param>
        public void Dequeue(byte[] mp3Stream, int timeout) { _outputReader.Dequeue(ref mp3Stream, timeout); }

        /// <summary>
        /// 告知MP3Encoder“当前PCM数据流已提交所有数据”，让MP3Encoder将缓冲的数据全部输出到OutputBuffer。
        /// </summary>
        public void EndCurrentStream() { _mp3Encoder.EndCurrentStream(); }

        #endregion

        #region------------------------- IDisposable接口实现 -------------------------

        /// <summary>
        /// 标记位用于检测冗余调用，避免重复调用Dispose()和析构。
        /// </summary>
        private bool _isDisposed = false;

        /// <summary>
        /// 释放资源。
        /// </summary>
        /// <param name="disposing">当前的释放资源的操作是主动（true，即用户调用了Dispose()方法）还是被动（false，即析构调用）。</param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // disposing参数为true（即用户通过调用Dispose()方法主动释放资源），在此释放需要Dispose的托管对象。
                    // 注：若disposing参数为false（在析构中调用），则不应在此释放托管对象（即使这些托管对象实现了IDisposable接口），因为这些托管对象的状态未知（析构的不确定性）。
                    DisposeManaged();
                }

                // 在此释放本类中直接占用的非托管的资源（如Marshal.Alloc的内存)。（注：包含非托管资源的托管对象已在 if (dispoing) {...}中释放）。
                // TODO:  在以下内容中替代终结器。将大型字段设置为 null。
                DisposeUnmanaged();

                _isDisposed = true;
            }
        }

        /// <summary>
        /// 关闭对象并释放资源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // 已主动释放资源，故告知GC不再需要调用本对象的终结器（析构函数）。
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~EasyMp3Encoder() { Dispose(false); }

        /// <summary>
        /// 释放托管资源。派生类应重写此方法释放派生类的托管资源。
        /// </summary>
        protected virtual void DisposeManaged()
        {
            // 释放MP3Encoder对象。
            _mp3Encoder?.Dispose();

            // 释放OutputBuffer的Reader对象。
            _outputReader?.Dispose();

            // 释放Input/Output Buffer对象。
            _inputBuffer?.Dispose();
            _outputBuffer?.Dispose();

        }

        /// <summary>
        /// 释放直接占用的非托管资源（如Marshal.Alloc的内存）。派生类若申请了非托管资源，则应重写此方法。
        /// </summary>
        protected virtual void DisposeUnmanaged() {; }

        #endregion

    }

}
