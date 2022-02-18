using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace SeeSharpTools.JXI.Public.Struct
{
    /// <summary>
    /// 循环缓冲队列扩展类（非托管内存），主要用于实现循环缓冲链
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularBufferEx<T> : IDisposable
    {
        #region ------------------------- 私有成员 -------------------------

        /// <summary>
        /// 定义读指针
        /// </summary>
        private CircularBufferReader<T> _reader;

        /// <summary>
        /// 定义写指针
        /// </summary>
        private CircularBuffer<T> _writer;

        /// <summary>
        /// 要检测冗余调用
        /// </summary>
        private bool _isDisposed = false;

        #endregion

        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 当前队列中的元素个数。
        /// </summary>
        public int NumOfElements { get { return _reader.NumOfElements; } }

        /// <summary>
        /// 当前能容纳的元素个数，即BufferSize - NumOfElements。
        /// </summary>
        public int CurrentCapacity { get { return _writer.CurrentCapacity; } }


        /// <summary>
        /// 缓冲区的大小，元素个数，即实例化时设定的长度。
        /// </summary>
        public int BufferSize { get { return (int)_writer.BufferSize; } }

        public CircularBufferHandle<T> BufferHandle { get { return _writer.BufferHandle; } }

        #endregion

        #region------------------------- 构造和析构函数 -------------------------

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bufferSize"> 缓冲区大小 </param>
        /// <param name="overwriteReader"> 读指针类型 </param>
        public CircularBufferEx(int bufferSize, bool overwriteReader)
        {
            _writer = new CircularBuffer<T>(bufferSize);
            _reader = new CircularBufferReader<T>(_writer.BufferHandle, overwriteReader ? CircularBufReaderType.Lossy : CircularBufReaderType.Lossless);
        }

        /// <summary>
        /// 析构函数，避免非托管内存没有释放
        /// </summary>
        ~CircularBufferEx() { Dispose(); }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                try
                {
                    _reader.Dispose();
                    _writer.Dispose();
                }
                finally
                {
                    _reader = null;
                    _writer = null;
                    _isDisposed = true;
                }
            }
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 向缓冲队列中放入一组数据，若队列中的空间不足，则等待至超时。
        /// 若等待至超时后队列中的空间仍不足以放入所有数据，则根据forceEnqueue参数决定此时的行为。
        /// </summary>
        /// <param name="sourceArray"> 写入数据数据源 </param>
        /// <param name="sourceIndex"> sourceArray 中复制开始处的索引</param>
        /// <param name="length">要复制的元素数目。</param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>
        /// <returns>若成功放入则返回true，否则返回false（不放入任何数据）。</returns>
        public CircularBufEnqueueResult Enqueue(T[] sourceArray, int sourceIndex, int length, int timeout)
        {
            return _writer.Enqueue(sourceArray, sourceIndex, length, timeout);
        }

        /// <summary>
        /// 向缓冲队列中放入一组数据，若队列中的空间不足，则等待至超时。
        /// 若等待至超时后队列中的空间仍不足以放入所有数据，则根据forceEnqueue参数决定此时的行为。
        /// 若成功放入则返回true，否则返回false（不放入任何数据）。
        /// </summary>
        /// <param name="sourceArray"> 写入数据数据源 </param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>
        public CircularBufEnqueueResult Enqueue(T[] sourceArray, int timeout)
        {
            return this.Enqueue(sourceArray, 0, sourceArray.Length, timeout);
        }

        /// <summary>
        /// 向缓冲队列中取出指定长度的数据，若队列中的元素不足，则等待至超时。
        /// </summary>
        /// <param name="destinationArray">存储读出数据的数组。</param>
        /// <param name="destinationIndex">  destinationArray 中存储开始处的索引。</param>
        /// <param name="length">要读取的元素个数。</param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>
        /// <returns>若成功读取所需长度则返回true，否则返回false（不读取任何数据）。</returns>
        public bool Dequeue(ref T[] destinationArray, int destinationIndex, int length, int timeout)
        {
            return _reader.Dequeue(ref destinationArray, destinationIndex, length, timeout) != CircularBufDequeueResult.Abort;
        }

        /// <summary>
        /// 向缓冲队列中取出指定长度的数据，若队列中的元素不足，则等待至超时。
        /// </summary>
        /// <param name="destinationArray">存储读出数据的数组，其长度即为要读出的元素个数。</param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>
        /// <returns>若成功读取所需长度则返回true，否则返回false（不读取任何数据）。</returns>
        public bool Dequeue(ref T[] destinationArray, int timeout)
        {
            return this.Dequeue(ref destinationArray, 0, destinationArray.Length, timeout);
        }

        /// <summary>
        /// 清空循环缓冲区的数据
        /// </summary>
        public void Clear() { _reader.Clear(); }

        /// <summary>
        /// 获取循环缓冲区的原始顺序数据。
        /// </summary>
        /// <param name="destinationArray"></param>
#if DEBUG
        public
#else
        internal
#endif
            void GetRawData(ref T[] destinationArray)
        {
            _writer.BufferHandle.GetRawData(ref destinationArray);
        }

        #endregion
    }

    /// <summary>
    /// CircularBuffer的读函数封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularBufferReader<T>
    {
        #region ------------------------- 私有成员 -------------------------

        /// <summary>
        /// CircularBuffer 数据句柄。
        /// </summary>
        private CircularBufferHandle<T> _bufferHandle;

        /// <summary>
        /// 总共读取的元素个数。
        /// ReadPointsCount 对 _bufferSize 取余，就是读指针位置。
        /// 由于 Clear，或者 Enqueue Overwrite操作，数值会大于实际读数(Dequeue)
        /// </summary>
        internal long readPointsCount;

        /// <summary>
        /// 实际总共读取的元素个数。
        /// 所有Dequeue操作读取元素总和，数值小于等于 readPointsCount
        /// </summary>
        internal long actualReadCount;

        /// <summary>
        /// Reader 初始化的位置。
        /// Clear 时重置初始化位置。
        /// </summary>
        internal long initReadPosition;

        /// <summary>
        /// 读指针的类型
        /// </summary>
        internal CircularBufReaderType readerType;

        /// <summary>
        /// 读指针的锁
        /// readPointer 只有 Dequeue ，Clear 操作会进行写入，所有写入必须加锁，指针移动只能单次进行。
        /// readPointer 只有 Enqueue ，GetNumOfElement ，GetCapacity操作会进行读取。
        /// readPointer 是单调递增，所以 Enqueue ，GetCapacity ， 对 readPointer 不加锁产生的结果影响非负面。
        /// </summary>
        internal object readPointerLock = new object();

        /// <summary>
        /// 要检测冗余调用
        /// </summary>
        private bool _isDisposed = false;

        #endregion

        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 获取Circular Buffer 句柄，当前读指针归属的Circular Buffer，即由该Circular Buffer创建。
        /// </summary>
        public CircularBufferHandle<T> BufferHandle { get { return _bufferHandle; } }

        /// <summary>
        /// 当前读指针队列中的元素个数。
        /// </summary>
        public int NumOfElements
        {
            get
            {
                // 对读指针加锁，保证读指针只有一个操作 (read)。保证返回值不会大于真实值。
                lock (readPointerLock)
                {
                    return (int)Math.Min(GetNumOfElement(), _bufferHandle.bufferSize);
                }
            }
        }

        /// <summary>
        /// 当前能容纳的元素个数。
        /// </summary>
        public int CurrentCapacity
        {
            get
            {
                // 对写指针加锁，保证返回值不会大于真实值。
                lock (_bufferHandle.writePointerLock)
                {
                    return (int)Math.Max(GetCurrentCapacity(), 0);
                }
            }
        }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数，外部调用。
        /// 新增一个读指针
        /// </summary>
        public CircularBufferReader(CircularBufferHandle<T> source, CircularBufReaderType type = CircularBufReaderType.Lossy)
        {
            _bufferHandle = source;
            readerType = type;
            // 对齐写指针
            this.Clear();

            _bufferHandle.AddReader(this);

        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~CircularBufferReader() { Dispose(); }
        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 释放对读指针的引用
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                try
                {
                    if (_bufferHandle != null)
                        _bufferHandle.RemoveReader(this);
                }
                finally
                {
                    _bufferHandle = null;
                    _isDisposed = true;
                }
            }
        }

        /// <summary>
        /// 向缓冲队列中取出指定长度的数据，若队列中的元素不足，则等待至超时。
        /// </summary>
        /// <param name="outputPtr">存储读出数据的数组。</param>
        /// <param name="destinationIndex">  destinationArray 中存储开始处的索引。</param>
        /// <param name="length">要读取的元素个数。</param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>
        /// <param name="latestData">最新的数据。true表示从CircularBuffer 队尾进行Dequeue。</param>
        /// <returns>若成功读取所需长度则返回true，否则返回false（不读取任何数据）。</returns>
        [MethodImpl(MethodImplOptions.Synchronized)] // Dnqueue()操作是互斥的，即不允许多个Dnqueue()同时发生，所以这里需要一个锁。
        internal CircularBufDequeueResult Dequeue(IntPtr outputPtr, int destinationIndex, int length, int timeout, bool latestData = false)
        {
            #region------------------------- 检查输入参数有效 -------------------------

            if (_bufferHandle == null) { throw new ArgumentOutOfRangeException("Reader is not valid."); }
            if (_bufferHandle.bufferPtr == IntPtr.Zero || _bufferHandle.bufferPtr == null) { throw new ArgumentOutOfRangeException("Buffer is NULL."); }
            if (length <= 0) { throw new ArgumentOutOfRangeException("Length to dequeue must be greater than 0"); }
            if (length > (int)_bufferHandle.bufferSize) { throw new ArgumentOutOfRangeException("Length to dequeue is greater than Buffer Size."); }

            #endregion

            // 保护 readPointer 不被其它API干扰。
            lock (readPointerLock)
            {
                bool isOverWrirtten = false;    // 写覆盖操作时，定义写指针是否覆盖未读数据标志位
                bool isReadSkipped = false;     // 从队尾读书时，定义读指针是否丢弃未读数据标志位

                #region------------------------- 检查队列中是否有足够的元素可读出，若不足则等待至超时返回 -------------------------
                bool isBufferElementsEnough = false;            // 当前Buffer是否有数据可读。            
                Stopwatch dequeueStopwatch = new Stopwatch();   // Dequeue（读取）数据时的计时器，用于超时判断。

                // 开始计时。
                dequeueStopwatch.Restart();

                while (!isBufferElementsEnough)
                {
                    // 不能将GetNumOfElement转换成int， 会导致数据溢出。
                    isBufferElementsEnough = (this.GetNumOfElement() >= (long)length);

                    // 如果已经超时，则不再等待。
                    if ((timeout != Timeout.Infinite) && (dequeueStopwatch.ElapsedMilliseconds >= timeout)) { break; } //用 ">=" ，否则会额外多等1ms

                    // 若队列中尚未有足够空间，则Sleep(0)。
                    if (!isBufferElementsEnough) { Thread.Sleep(0); }
                }

                // 如果队列中始终没有足够的元素，则不做任何操作，返回false。
                if (!isBufferElementsEnough) { return CircularBufDequeueResult.Abort; }

                #endregion

                #region------------------------- 读取数据 -------------------------

                // 所有获取Buffer读写操作互斥锁。
                // 退出时要释放锁，使用Try Catch结构。
                _bufferHandle.bufferLock.EnterReadLock();
                try
                {
                    #region  ------------------------- 根据需求，赋值读指针 -------------------------
                    //进入了_bufferLock 锁，读写指针都固定不变，所以才能进行赋值操作。

                    // 判断 写指针 是否追上 读指针。
                    isOverWrirtten = GetNumOfElement() > _bufferHandle.bufferSize;
                    if (isOverWrirtten)
                    {
                        // 写指针 追上 读指针，Buffer长度为_bufferSize。
                        readPointsCount = _bufferHandle.writePointsCount - _bufferHandle.bufferSize;
                    }

                    // 从队尾进行读数设置
                    if (latestData)
                    {
                        long tempPosition = _bufferHandle.writePointsCount - length;
                        isReadSkipped = tempPosition > readPointsCount;
                        if (isReadSkipped)
                        {
                            readPointsCount = tempPosition;
                        }
                    }

                    #endregion

                    // 获取 读指针 在Buffer中的实际位置。
                    int readIndex = (int)(readPointsCount % _bufferHandle.bufferSize);

                    // 如果 “读指针Index” + “要写入的元素个数” > Buffer长度，则须分两次拷贝（Circular Read）。
                    if (readIndex + length > (int)_bufferHandle.bufferSize)
                    {

                        #region------------------------- 数据须分两次拷贝，即绕回读出（Circular Read）-------------------------

                        int numOfElementsAtEnd = (int)_bufferHandle.bufferSize - readIndex;
                        int numOfElementsAtHead = length - numOfElementsAtEnd;

                        // 获取CircularBuffer的读指针Ptr，即为读出位置。
                        var sourcePtr = IntPtr.Add(_bufferHandle.bufferPtr, readIndex * _bufferHandle.sizeOfT);

                        // 获取destinationArray数组对应非托管内存的destinationIndex地址Ptr。
                        var destinationPtr = IntPtr.Add(outputPtr, destinationIndex * _bufferHandle.sizeOfT);

                        // 拷贝（_buferSize - readIndex）个元素，即将读指针至Buffer末尾的数据读出。
                        CAPI.memcpy(destinationPtr, sourcePtr, (UIntPtr)(numOfElementsAtEnd * _bufferHandle.sizeOfT));

                        // 获取剩余读出元素对应的destinationArray的非托管内存的首地址Ptr。
                        destinationPtr = IntPtr.Add(outputPtr, (destinationIndex + numOfElementsAtEnd) * _bufferHandle.sizeOfT);

                        // 拷贝剩余的数据。
                        sourcePtr = _bufferHandle.bufferPtr;
                        CAPI.memcpy(destinationPtr, sourcePtr, (UIntPtr)(numOfElementsAtHead * _bufferHandle.sizeOfT));

                        #endregion
                    }
                    else
                    {
                        #region------------------------- 不需要绕回读取（Circular Read），直接拷贝即可-------------------------

                        // 获取destinationArray对应非托管内存的destinationIndex地址Ptr。
                        var destinationPtr = IntPtr.Add(outputPtr, destinationIndex * _bufferHandle.sizeOfT);

                        // 获取CircularBuffer的读指针Ptr，即为读出位置。
                        var sourcePtr = IntPtr.Add(_bufferHandle.bufferPtr, readIndex * _bufferHandle.sizeOfT);

                        // 拷贝数据。
                        CAPI.memcpy(destinationPtr, sourcePtr, (UIntPtr)(length * _bufferHandle.sizeOfT));

                        #endregion
                    }

                    // 更新读指针位置。
                    readPointsCount += (long)length;
                    actualReadCount += (long)length;
                }
                finally
                {
                    _bufferHandle.bufferLock.ExitReadLock();
                }
                #endregion

                #region ------------------------- 数据读取已完成，返回结果 -------------------------

                // 非 Latest Data 操作永远不能产生LossyData事件。
                if (isReadSkipped)
                    return CircularBufDequeueResult.LossyBySkipped;

                // 非 overwritten 的读指针永远不能产生LossyData事件。
                if (isOverWrirtten)
                    return CircularBufDequeueResult.LossyByOverwritten;

                return CircularBufDequeueResult.NormalDequeue;

                #endregion
            }
        }

        /// <summary>
        /// 向缓冲队列中取出指定长度的数据，若队列中的元素不足，则等待至超时。
        /// </summary>
        /// <param name="destinationArray">存储读出数据的数组。</param>
        /// <param name="destinationIndex">  destinationArray 中存储开始处的索引。</param>
        /// <param name="length">要读取的元素个数。</param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>        
        /// <param name="latestData">最新的数据。true表示从CircularBuffer 队尾进行Dequeue。</param>
        /// <returns>若成功读取所需长度则返回true，否则返回false（不读取任何数据）。</returns>
        public CircularBufDequeueResult Dequeue(ref T[] destinationArray, int destinationIndex, int length, int timeout, bool latestData = false)
        {
            // 检查输入参数有效。
            if (destinationArray == null) { throw new ArgumentNullException(); }

            GCHandle gch;

            // 获取destinationArray数组对应非托管内存的destinationIndex地址Ptr。
            gch = GCHandle.Alloc(destinationArray, GCHandleType.Pinned);

            try
            {
                IntPtr destinationPtr = gch.AddrOfPinnedObject();
                return Dequeue(destinationPtr, destinationIndex, length, timeout, latestData);
            }
            finally
            {
                gch.Free();
            }
        }

        /// <summary>
        /// 向缓冲队列中取出指定长度的数据，若队列中的元素不足，则等待至超时。
        /// </summary>
        /// <param name="destinationArray">存储读出数据的数组，其长度即为要读出的元素个数。</param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>    
        /// <param name="latestData">最新的数据。true表示从CircularBuffer 队尾进行Dequeue。</param>
        /// <returns>若成功读取所需长度则返回true，否则返回false（不读取任何数据）。</returns>
        public CircularBufDequeueResult Dequeue(ref T[] destinationArray, int timeout, bool latestData = false)
        {
            return this.Dequeue(ref destinationArray, 0, destinationArray.Length, timeout, latestData);
        }

        /// <summary>
        /// 清空循环缓冲区的数据
        /// </summary>
        public void Clear()
        {
            // 对读指针加锁，保证读指针只有一个操作 (write)。
            // 清空操作一定执行，不需要超时。
            lock (readPointerLock)
            {
                // 不需要对 writer 加锁，因为：
                // a. Clear 方法不对 写指针 操作；
                // b. Clear 方法 不需要 / 也不能 保证函数返回时，写指针没有其它操作(新写入操作)。
                readPointsCount = _bufferHandle.writePointsCount; // 读指针对齐写指针，清空Buffer。

                // 清空Buffer时，重新设置actual的个数。
                actualReadCount = 0;
                initReadPosition = readPointsCount;
            }
        }

        #endregion

        #region------------------------- 私有方法 -------------------------

        /// <summary>
        /// 当前读指针队列中的元素个数。
        /// 非保护方法，仅内部使用。
        /// 返回值大于bufferSize，表示数据被覆盖。返回值不会小于0。
        /// </summary>
        /// <returns></returns>
        internal long GetNumOfElement()
        {
            return (_bufferHandle.writePointsCount - readPointsCount);
        }

        /// <summary>
        /// 当前读指针队列中的元素个数。
        /// 非保护方法，仅内部使用。
        /// 返回值小于0，表示数据被覆盖。
        /// </summary>
        /// <returns></returns>
        internal long GetCurrentCapacity()
        {
            return (_bufferHandle.bufferSize + readPointsCount - _bufferHandle.writePointsCount);
        }

        #endregion
    }

    /// <summary>
    /// 循环缓冲队列扩展类（非托管内存），主要用于实现循环缓冲链，多读指针支持
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class CircularBuffer<T> : CircularBufferHandle<T>
    {

        #region------------------------- 私有成员 -------------------------

        #endregion

        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 获取Circular Buffer 句柄。既是 Circular Buffer 本身，也是唯一的 writer 。
        /// </summary>
        public CircularBufferHandle<T> BufferHandle { get { return this; } }

        /// <summary>
        /// 缓冲区的大小，元素个数，即实例化时设定的长度。
        /// </summary>
        public int BufferSize { get { return (int)bufferSize; } }

        /// <summary>
        /// 能容纳的元素个数。
        /// </summary>
        public int CurrentCapacity
        {
            get
            {
                // 对写指针加锁，保证返回值不会大于真实值。
                lock (writePointerLock)
                {
                    // 遍历 readPointers List 时，不能添加或者删除读指针。
                    lock (readPointersLock)
                    {
                        long capacity =  bufferSize;
                        foreach (var reader in readPointers)
                        {
                            capacity = Math.Min(reader.GetCurrentCapacity(), capacity);
                        }
                        return (int)Math.Max(capacity, 0);
                    }                    
                }
            }
        }

        #endregion

        #region------------------------- 构造和析构函数 -------------------------

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bufferSize">缓冲区大小</param>
        public CircularBuffer(int bufferSize) : base(bufferSize) { }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~CircularBuffer()
        {
            Dispose();
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 向缓冲队列中放入一组数据，若队列中的空间不足，则等待至超时。
        /// 若等待至超时后队列中的空间仍不足以放入所有数据，则根据forceEnqueue参数决定此时的行为。
        /// </summary>
        /// <param name="inputPtr"> 写入数据数据源 </param>
        /// <param name="sourceIndex"> sourceArray 中复制开始处的索引</param>
        /// <param name="length">要复制的元素数目。</param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>
        /// <returns>若成功放入则返回true，否则返回false（不放入任何数据）。</returns>        
        [MethodImpl(MethodImplOptions.Synchronized)] // Enqueue()操作是互斥的，即不允许多个Enqueue()同时发生，所以这里需要一个锁。
        internal CircularBufEnqueueResult Enqueue(IntPtr inputPtr, int sourceIndex, int length, int timeout)
        {
            #region------------------------- 检查输入参数有效 -------------------------

            if (bufferPtr == IntPtr.Zero || bufferPtr == null) { throw new ArgumentOutOfRangeException("Buffer is NULL."); }
            if (length <= 0) { throw new ArgumentOutOfRangeException("Length to enqueue must be greater than 0."); }
            if (length > (int)bufferSize) { throw new ArgumentOutOfRangeException("Length to enqueue is greater than Buffer Size."); }

            #endregion

            // 保护 writePointer 不被其它API干扰。
            lock (writePointerLock)
            {
                #region------------------------- 检查队列中是否有足够的空间来填入数据，若不足则等待至超时返回 -------------------------

                bool isBufferCapacityReady = false;                     // 当前Buffer是否有空间可写。 
                Stopwatch enqueueStopwatch = new Stopwatch();           // Enqueue（写入）数据时的计时器，用于超时判断。

                // 开始计时。 
                enqueueStopwatch.Restart();

                while (!isBufferCapacityReady)
                {
                    long availableLength = bufferSize;    // 当前Buffer最小的队列长度，初始长度为bufferSize。

                    // 遍历 readPointers List 时，不能添加或者删除读指针。
                    lock (readPointersLock)
                    {
                        foreach (var reader in readPointers)
                        {
                            // 对于 读指针 类型为 Lossy， 永远可以写入。
                            if (reader.readerType != CircularBufReaderType.Lossy)
                            {
                                availableLength = Math.Min(availableLength, reader.GetCurrentCapacity());
                            }
                        }
                    }
                    isBufferCapacityReady = (availableLength >= length); // 判断队列长度 

                    // 如果已经超时，则不再等待。
                    if ((timeout != Timeout.Infinite) && (enqueueStopwatch.ElapsedMilliseconds >= timeout)) { break; } // 用 ">=" ，否则会额外多等1ms

                    // 若队列中尚未有足够空间，则Sleep(0)。
                    if (!isBufferCapacityReady) { Thread.Sleep(1); }
                }

                // 如果队列中始终没有足够的空间，且并未开启强制写入，则不做任何操作，返回结果。
                if (!isBufferCapacityReady) { return CircularBufEnqueueResult.Abort; }

                #endregion

                #region------------------------- 写入数据 -------------------------

                // 所有获取Buffer读写操作互斥锁。
                // 退出时要释放锁，使用Try Catch结构。
                bufferLock.EnterWriteLock();
                try
                {
                    // 获取 写指针 在Buffer中的实际位置。
                    int writePointer = (int)(writePointsCount % bufferSize);

                    // 如果 “写指针Index” + “要写入的元素个数” > Buffer长度，则须分两次拷贝（Circular Write）。
                    if (writePointer + length > (int)bufferSize)
                    {
                        #region------------------------- 数据须分两次拷贝，即绕回写入（Circular Write）-------------------------

                        int numOfElementsAtEnd = (int)bufferSize - writePointer;
                        int numOfElementsAtHead = length - numOfElementsAtEnd;

                        // 获取输入数据对应非托管内存的sourceIndex地址Ptr。
                        var sourcePtr = IntPtr.Add(inputPtr, sourceIndex * sizeOfT);

                        // 获取CircularBuffer的写指针Ptr，即为写入位置。
                        var destinationPtr = IntPtr.Add(bufferPtr, writePointer * sizeOfT);

                        // 拷贝（_buferSize - writeIndex）个元素，即填满写指针至Buffer末尾。
                        CAPI.memcpy(destinationPtr, sourcePtr, (UIntPtr)(numOfElementsAtEnd * sizeOfT));

                        // 获取输入数据剩余元素对应的非托管内存的首地址Ptr。
                        sourcePtr = IntPtr.Add(inputPtr, (sourceIndex + numOfElementsAtEnd) * sizeOfT);

                        // 拷贝剩余的数据至Buffer的起始位置。
                        destinationPtr = bufferPtr;
                        CAPI.memcpy(destinationPtr, sourcePtr, (UIntPtr)(numOfElementsAtHead * sizeOfT));

                        #endregion
                    }
                    else
                    {
                        #region------------------------- 不需要绕回写入（Circular Write），直接拷贝即可-------------------------

                        // 获取输入数据对应非托管内存的sourceIndex地址Ptr。
                        var sourcePtr = IntPtr.Add(inputPtr, sourceIndex * sizeOfT);

                        // 获取CircularBuffer的写指针Ptr，即为写入位置。
                        var destinationPtr = IntPtr.Add(bufferPtr, writePointer * sizeOfT);

                        // 拷贝数据。
                        CAPI.memcpy(destinationPtr, sourcePtr, (UIntPtr)(length * sizeOfT));

                        #endregion
                    }

                    // 更新写指针位置
                    writePointsCount += (long)length;

                    // 更新读指针状态
                }
                finally
                {
                    bufferLock.ExitWriteLock();
                }

                #endregion

                #region ------------------------- 数据写入已完成，返回结果 -------------------------

                // 遍历 readPointers List 时，不能添加或者删除读指针。
                lock (readPointersLock)
                {
                    foreach (var reader in readPointers)
                    {
                        // 检查数据是否覆盖。只有overwrite的读指针才会产生LossyEnqueue事件，非overwrite的读指针在判断是否有空间时，会产生Timeout事件。
                        if (reader.GetCurrentCapacity() < 0)
                            return CircularBufEnqueueResult.LossyEnqueue;
                    }
                }

                // 数据正常写入完成。
                return CircularBufEnqueueResult.NormalEnqueue;

                #endregion
            }
        }

        /// <summary>
        /// 向缓冲队列中放入一组数据，若队列中的空间不足，则等待至超时。
        /// 若等待至超时后队列中的空间仍不足以放入所有数据，则根据forceEnqueue参数决定此时的行为。
        /// </summary>
        /// <param name="sourceArray"> 写入数据数据源 </param>
        /// <param name="sourceIndex"> sourceArray 中复制开始处的索引</param>
        /// <param name="length">要复制的元素数目。</param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>
        /// <returns>若成功放入则返回true，否则返回false（不放入任何数据）。</returns>
        public CircularBufEnqueueResult Enqueue(T[] sourceArray, int sourceIndex, int length, int timeout)
        {
            // 检查输入参数有效。
            if (sourceArray == null) { throw new ArgumentNullException(); }

            // 获取输入数据对应非托管内存的sourceIndex地址Ptr。
            var gch = GCHandle.Alloc(sourceArray, GCHandleType.Pinned);

            try
            {
                IntPtr sourcePtr = gch.AddrOfPinnedObject();
                return Enqueue(sourcePtr, sourceIndex, length, timeout);
            }
            finally
            {
                gch.Free();
            }

        }

        /// <summary>
        /// 向缓冲队列中放入一组数据，若队列中的空间不足，则等待至超时。
        /// 若等待至超时后队列中的空间仍不足以放入所有数据，则根据forceEnqueue参数决定此时的行为。
        /// 若成功放入则返回true，否则返回false（不放入任何数据）。
        /// </summary>
        /// <param name="sourceArray"> 写入数据数据源 </param>
        /// <param name="timeout">超时毫秒数。Timout.Infinite即-1为始终等待。</param>
        public CircularBufEnqueueResult Enqueue(T[] sourceArray, int timeout)
        {
            return this.Enqueue(sourceArray, 0, sourceArray.Length, timeout);
        }

        /// <summary>
        /// 清空循环缓冲区的数据
        /// </summary>
        public void ClearAll()
        {
            // 对 readPointers List 加锁，保证只有一个线程操作。
            // 防止在添加读指针列表时，有额外的添加(Add)，删除(Remove)读指针列表。
            lock (readPointersLock)
            {
                foreach (var reader in readPointers) // 遍历所有读指针
                {
                    reader.Clear();
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// CircularBuffer的句柄
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularBufferHandle<T> : IDisposable
    {
        #region------------------------- 公共成员 -------------------------

        /// <summary>
        /// T的Size，字节数，创建队列的时候初始化。
        /// </summary>
        internal readonly int sizeOfT;

        /// <summary>
        /// 缓冲区的大小，元素个数，即实例化时设定的长度。
        /// </summary>
        internal long bufferSize;

        /// <summary>
        /// 缓冲区的指针首地址，创建队列的时候初始化。
        /// </summary>
        internal IntPtr bufferPtr;

        /// <summary>
        /// 对Buffer访问的互斥锁。
        /// Buffer 只有 Enqueue ，Dispose 操作会进行写入。
        /// Buffer 只有 Dequeue ，GetRawData 操作会进行读取。
        /// </summary>
        internal readonly ReaderWriterLockSlim bufferLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 写入buffer的数据总数。
        /// 对_bufferSize取余就是写指针位置。
        /// </summary>
        internal long writePointsCount;

        /// <summary>
        /// 写指针锁。
        /// writePointer 只有 Enqueue 操作会进行写入，所有写入必须加锁，指针移动只能单次进行。
        /// writePointer 只有 Dequeue ，Clear ，GetNumOfElement ，GetCapacity操作会进行读取。
        /// writePointer 是单调递增，所以 Dequeue ，Clear ，GetNumOfElement ， 对 writePointer 不加锁产生的结果影响非负面。
        /// </summary>
        internal readonly object writePointerLock = new object();

        /// <summary>
        /// 队列读指针，相对于缓冲区指针首地址的Index值。
        /// </summary>
        internal List<CircularBufferReader<T>> readPointers;

        /// <summary>
        /// 对_readPointers 列表访问的锁。
        /// </summary>
        internal readonly object readPointersLock = new object();

        /// <summary>
        /// 要检测冗余调用
        /// </summary>
        protected bool _isDisposed = false;

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数，内部使用
        /// </summary>
        /// <param name="bufferSize">缓冲区大小</param>
        internal CircularBufferHandle(int bufferSize)
        {
            // 检查输入参数有效。
            if (bufferSize <= 0) { throw new ArgumentOutOfRangeException("Invalid buffer size " + bufferSize.ToString() + Environment.NewLine); }

            // 获取队列中每个元素的字节数。
            sizeOfT = Marshal.SizeOf(typeof(T));

            // 新建对应大小的缓冲区。
            this.bufferSize = (long)bufferSize;
            bufferPtr = Marshal.AllocHGlobal(bufferSize * sizeOfT);

            // 初始化 写指针。
            writePointsCount = 0;

            // 初始化读指针列表。
            readPointers = new List<CircularBufferReader<T>>();
        }

        /// <summary>
        /// 释放循环缓冲区
        /// </summary>
        public virtual void Dispose()
        {
            if (!_isDisposed)
            {
                // 清空读指针
                // 对 _readPointers List 加锁，保证只有一个线程操作(List Clear)。
                lock (readPointersLock)
                {
                    if (readPointers != null)
                    {
                        for (int i = 0; i < readPointers.Count; i++)
                        {
                            try
                            {
                                if (readPointers[i] != null)
                                {
                                    readPointers[i].Dispose();
                                    //readPointers[i] = null;
                                }
                            }
                            catch(Exception ex )
                            { }
                        }

                        readPointers.Clear();
                        readPointers = null;
                    }
                }

                // 清空Buffer
                bufferLock.EnterWriteLock();
                try
                {
                    // 释放非托管内存。
                    if (!bufferPtr.Equals(IntPtr.Zero)) { Marshal.FreeHGlobal(bufferPtr); }
                }
                finally
                {
                    bufferLock.ExitWriteLock();
                    // 清空内存。
                    bufferPtr = IntPtr.Zero;
                    _isDisposed = true;
                }
            }
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 添加读指针
        /// </summary>
        /// <param name="reader">  读指针 </param>
        /// <returns> 新添加的读指针 </returns>
        internal void AddReader(CircularBufferReader<T> reader)
        {
            // 检查输入参数有效。
            if (reader == null) { throw new ArgumentOutOfRangeException("Buffer Reader is NULL."); }

            // 对 _readPointers List 加锁，保证只有一个线程操作(List Add)。
            // 防止在添加读指针列表时，有额外的删除(Remove)读指针列表。
            lock (readPointersLock)
            {
                readPointers.Add(reader);
            }

            return;
        }

        /// <summary>
        /// 删除读指针
        /// </summary>
        /// <param name="reader"> 待删除的读指针 </param>
        /// <returns> 删除是否成功 </returns>
        internal bool RemoveReader(CircularBufferReader<T> reader)
        {
            // 检查输入参数有效。
            if (reader == null) { throw new ArgumentOutOfRangeException("Buffer Reader is NULL."); }

            // 对 _readPointers List 加锁，保证只有一个线程操作(List Remove)。
            // 防止在删除读指针列表时，有额外的添加(Add)读指针列表。
            lock (readPointersLock)
            {
                return readPointers.Remove(reader);
            }
        }

        /// <summary>
        /// 获取循环缓冲区的原始顺序数据。
        /// </summary>
        /// <param name="destinationArray"></param>
#if DEBUG
        public
#else
        internal
#endif
            void GetRawData(ref T[] destinationArray)
        {
            bufferLock.EnterWriteLock();
            try
            {
                // 获取destinationArray数组对应非托管内存的首地址Ptr。
                var gch = GCHandle.Alloc(destinationArray, GCHandleType.Pinned);

                // 拷贝所有元素。
                CAPI.memcpy(gch.AddrOfPinnedObject(), bufferPtr, (UIntPtr)((int)bufferSize * sizeOfT));

                gch.Free();
            }
            finally
            {
                bufferLock.ExitWriteLock();
            }
        }

        #endregion
    }

    internal static class CAPI
    {
        private static PlatformID _platformId;

        static CAPI()
        {  _platformId= Environment.OSVersion.Platform; }

        /// <summary>
        /// 内存拷贝
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="length"></param>
        public static void memcpy(IntPtr destination, IntPtr source, UIntPtr length)
        {
            if (_platformId == PlatformID.Unix)
            { LinuxAPI.memcpy(destination, source,length); }
            else
            { WinAPI.memcpy(destination, source, length); }
        }
    }

    /// <summary>
    /// windows api 的函数导入
    /// </summary>
    internal static class WinAPI
    {
        //[DllImport("libc", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern void memcpy(IntPtr destination, IntPtr source, UIntPtr length);
    }

    /// <summary>
    /// windows api 的函数导入
    /// </summary>
    internal static class LinuxAPI
    {
        [DllImport("libc", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        //[DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern void memcpy(IntPtr destination, IntPtr source, UIntPtr length);
    }

    #region------------------------- 公共数据类型 -------------------------

    /// <summary>
    /// CircularBuffer执行Enqueue操作的结果。
    /// </summary>
    public enum CircularBufEnqueueResult
    {
        /// <summary>
        /// 正常Enqueue成功。
        /// </summary>
        NormalEnqueue,

        /// <summary>
        /// 等待超时后，放弃Enqueue操作。
        /// </summary>
        Abort,

        /// <summary>
        /// 等待超时后强制Enqueue，已丢弃最早的数据。
        /// </summary>
        LossyEnqueue,
    }

    /// <summary>
    /// CircularBuffer执行Dequeue操作的结果。
    /// </summary>
    public enum CircularBufDequeueResult
    {
        /// <summary>
        /// 正常Dequeue成功。
        /// </summary>
        NormalDequeue,

        /// <summary>
        /// 等待超时后，放弃Dequeue操作。
        /// </summary>
        Abort,

        /// <summary>
        /// 读指针被覆盖，已丢弃最早的数据。
        /// </summary>
        LossyByOverwritten,

        /// <summary>
        /// 读指针偏移，已丢弃最早的数据。
        /// </summary>
        LossyBySkipped,
    }

    /// <summary>
    /// CircularBuffer读指针的类型。对应不同的Enqueue操作的表现。
    /// </summary>
    public enum CircularBufReaderType
    {
        /// <summary>
        ///  无损Reader，本Reader的未读取数据不能被Circular Buffer的Enqueue操作覆盖，即Enqueue操作时，若Buffer空间不足，须等待本Reader读走数据后才能写入。
        /// </summary>
        Lossless,

        /// <summary>
        /// 有损Reader，本Reader的未读取数据可以被Circular Buffer的Enqueue操作直接覆盖，即Enqueue操作时，若Buffer空间不足，则强制Enqueue无需等待。
        /// </summary>
        Lossy,
    }

    #endregion

}