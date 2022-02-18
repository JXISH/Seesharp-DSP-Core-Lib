using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace SeeSharpTools.JXI.FileIO.VectorFile
{
    /// <summary>
    /// Data priviewer for Vector File Fix Frequency Stream Format.
    /// </summary>
    public class FixFrequencyStreamDataPreviewer : VectorDataPreviewerBase
    {

        #region ------------------- Constants -------------------

        private readonly static int PreviewLenggthMin = 10;
        private readonly static int PreviewLengthDefault = 4000;
        private readonly static int PreviewLengthMax = 10000;

        private readonly static int QuickPreviewSamplersPerSegment = 10;
        private readonly static int MemoryBufferModeMaxBytes = (int) (10 * 1E6);
        private readonly static int FileBufferModeBytesPerRead = (int)1E6;

        #endregion

        #region ------------------- Private Fields -------------------

        private FixFrequencyStreamFile _file;

        /// <summary>
        /// Scale factor of channels, updated when Commit() is called.
        /// </summary>
        private double[] _scaleFactor;

        private int _numberOfChannels;
        private DataType _dataType;

        private bool _isDecimationOn;
        private double[,] _nonDecimatedData;
        private DataPreviewResultOf1Ch1Segment[,] _decimatedPreviewResult;

        #endregion

        #region--------------Delegate and Events----------------------------

        //public delegate void RunWorkerCompletedEventHandler(object sender, EventArgs e);
        //public event RunWorkerCompletedEventHandler RunWorkerCompleted;

        #endregion

        #region ------------------- Public Properties -------------------

        /// <summary>
        /// Total data size of file, in number of samples.
        /// </summary>
        public long NumberOfSamples
        {
            get
            {
                _file.Open(_filePath, FileMode.Open, FileAccess.Read);
                long numberOfSamples = _file.NumberOfSamples;
                _file.Close();
                return numberOfSamples;
            }
        }

        private long _selectionStart;
        /// <summary>
        /// Selected data start position, in Samples.
        /// </summary>
        public long  SelectionStart
        {
            get { return _selectionStart; }
            set
            {
                if (value >= 0) { _selectionStart = value; }
                else { throw new ArgumentOutOfRangeException("Selection start position must be greater than or equal to 0."); }
            }
        }

        private long _selectionEnd;
        /// <summary>
        /// Selected data end position, in Samples.
        /// </summary>
        public long SelectionEnd
        {
            get { return _selectionEnd; }
            set
            {
                if (value > 0) { _selectionEnd = value; }
                else { throw new ArgumentOutOfRangeException("Selection end position must be greater than 0."); }
            }
        }

        private int _previewLength;
        /// <summary>
        /// Result preview waveform length, in Samples.
        /// </summary>
        public int PreviewLength
        {
            get { return _previewLength; }
            set
            {
                _previewLength = Math.Min(value, PreviewLengthMax);
                _previewLength = Math.Max(_previewLength, PreviewLenggthMin);
            }
        }

        private bool _previewLengthAuto;
        /// <summary>
        /// If set to true, preview waveform length is automatically selected.
        /// </summary>
        public bool PreviewLengthAuto
        {
            get { return _previewLengthAuto; }
            set { _previewLengthAuto = value; }
        }

        private VectorDataAmplitudeUnit _dataUnit;
        /// <summary>
        /// Unit of preview data.
        /// </summary>
        public VectorDataAmplitudeUnit Unit
        {
            get { return _dataUnit; }
            set { _dataUnit = value; }
        }

        private double _decimation;
        /// <summary>
        /// Decimation ratio of preview data, calculated by "(SelectionEnd - SelectionStart + 1) /PreviewLength".
        /// </summary>
        public double Decimation
        { get { return _decimation; } }

        #endregion

        #region ------------------- Constructor and Destructor -------------------

        /// <summary>
        /// Create instance of data previewer for Vector File Fix Frequency Stream format.
        /// </summary>
        public FixFrequencyStreamDataPreviewer()
        {
            _file = new FixFrequencyStreamFile();
            _dataUnit = VectorDataAmplitudeUnit.V;
            _previewLengthAuto = true;
        }

        #endregion

        #region ------------------- Public Methods -------------------

        /// <summary>
        /// Commit data selection to get output preview data(waveform) length.
        /// </summary>
        public override void Commit()
        {
            // Call base method.
            base.Commit();

            // Check if selection valid.
            if (_selectionEnd <= _selectionStart) { throw new ArgumentException("Invliad selection: start must be smaller than end."); }

            // Open file to get data information.
            _file.Open(_filePath, FileMode.Open, FileAccess.Read);
            _numberOfChannels = _file.Storage.NumberOfChannels;
            _dataType = _file.Storage.DataType;
            _scaleFactor = new double[_numberOfChannels];
            for (int i = 0; i < _numberOfChannels; i++)
            {
                _scaleFactor[i] = _file.Sampling.Channels[i].RFScaleFactor * _file.Sampling.Channels[i].DigitizerScaleFactor ;
            }

            try
            {
                // Check if selection is within file data range.
                if (_selectionEnd > _file.NumberOfSamples - 1) { throw new ArgumentOutOfRangeException("Selection End."); }

                // Set _previewLength to default value if Auto is enabled.
                if (_previewLengthAuto) { _previewLength = PreviewLengthDefault; }

                // Actual preview data(waveform) length should not be greater than selected length, and must be even.
                _previewLength = (int) Math.Min(_previewLength, _selectionEnd - _selectionStart + 1);
                if ((_previewLength % 2) != 0) { _previewLength--; }

                _isDecimationOn = (_previewLength == (int)Math.Min(_previewLength, _selectionEnd - _selectionStart + 1));
                _decimation = (_selectionEnd - _selectionStart + 1) / (double)_previewLength;

                // New array to hold processed data.
                if (_isDecimationOn)
                {
                    _decimatedPreviewResult = new DataPreviewResultOf1Ch1Segment[_numberOfChannels, _previewLength / 2];
                }
                else
                {
                    _nonDecimatedData = new double[_numberOfChannels, _previewLength];
                }
            }
            finally
            {
                // Close file anyway.
                _file.Close();
            }
        }

        /// <summary>
        /// Get one channel preview data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="channelIndex"></param>
        public void GetData(ref double[] data, int channelIndex = 0)
        {
            int i;

            // Parameter validation.
            if (channelIndex > _numberOfChannels - 1) { throw new ArgumentOutOfRangeException("channelIndex invalid."); }
            if (data.Length < _previewLength) { throw new ArgumentException("Array length must be at least PreviewLength"); }

            // Copy data from preview result.
            if(_isDecimationOn)
            {
                // Copy data from raw _decimatedPreviewResult, by segments.
                DataPreviewResultOf1Ch1Segment segmentPreviewResult;
                for (i = 0; i < data.Length / 2; i++)
                {
                    segmentPreviewResult = _decimatedPreviewResult[channelIndex, i];
                    if(segmentPreviewResult.IndexOfMin <= segmentPreviewResult.IndexOfMax)
                    {
                        // Minimum value comes first.
                        data[i * 2] = segmentPreviewResult.MinValue;
                        data[i * 2 + 1] = segmentPreviewResult.MaxValue;
                    }
                    else
                    {
                        // Maximum value comes first.
                        data[i * 2] = segmentPreviewResult.MaxValue;
                        data[i * 2 + 1] = segmentPreviewResult.MinValue;
                    }
                }
            }
            else
            {
                // Copy data from raw _nonDecimatedData.
                for (i = 0; i < data.Length; i++) { data[i] = _nonDecimatedData[channelIndex, i]; }
            }

            // Perform unit conversion from rawAmplitude (unscaled) to specified engineering unit.
            double scaleFactor = _scaleFactor[channelIndex];
            for (i = 0; i < data.Length; i++) { data[i] = UnitConversion(data[i], scaleFactor, _dataUnit); }

        }

        #endregion

        #region ------------------- Private Methods -------------------
        /// <summary>
        /// ThreadProcessPreview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ThreadProcessPreview(object sender, DoWorkEventArgs e)
        {
            int i, j, k;
            double processingProgress = 0, previewRatio = 0;
            bool isCompletePreview = false;
            short[] fullRawData;

            // local variables for data segment management..
            int numberOfSegments;
            double samplesPerSegment;
            int samplesOfCurrentSegment, numberOfBlocksOfCurrentSeg;

            // Calculate number of selected samples.
            long numberOfSelectedSamples = _selectionEnd - _selectionStart + 1;

            // Stopwatch used to check progress reporting interval.
            var reportStopwatch = new Stopwatch();

            try
            {
                // Open file to get data information.
                _file.Open(_filePath, FileMode.Open, FileAccess.Read);
                int bytesPerSample = _file.BytesPerSample;
                int numberOfChannels = _file.Storage.NumberOfChannels;
                int arrayElementsPerSample = bytesPerSample / sizeof(short);

                // Data format determines how to generate preview result: use r (amplitude) for complex IQ, and use direct value for real waveform.
                bool isInterleavedIQ = false;
                if (_file.Storage.DataType == DataType.ComplexI16 || _file.Storage.DataType == DataType.ComplexI8) { isInterleavedIQ = true; }

                // New array to store 1 segment/block processing result of all channels.
                var previewResultOfAllCh1Seg = new DataPreviewResultOf1Ch1Segment[numberOfChannels];

                #region ---- If no processing is needed, just read selected data from file and return.----

                // If no processing is needed, just read selected data from file and return.
                if (! _isDecimationOn)
                {
                    // Read all data from file. 
                    fullRawData = new short[numberOfSelectedSamples * arrayElementsPerSample];
                    _file.Seek(_selectionStart, SeekOrigin.Begin);
                    _file.Read(fullRawData);

                    #region ------------- Copy read data (in channel interleaved format) to result array (by rows) ------------

                    if (isInterleavedIQ)
                    {
                        // complex format, 2 array elements per point, calculate r (amplitude) from I/Q.
                        for (i = 0; i < numberOfChannels; i++)
                        {
                            for (j = 0; j < numberOfSelectedSamples; j++)
                            {
                                k = arrayElementsPerSample * j + 2 * i;
                                _nonDecimatedData[i, j] = Math.Sqrt(Math.Pow(fullRawData[k], 2) + Math.Pow(fullRawData[k + 1], 2));
                            }
                        }
                    }
                    else
                    {
                        // real format, 1 array element per point, just re-organize channel interleaved 1D array to 2D array.
                        for (i = 0; i < numberOfChannels; i++)
                        {
                            for (j = 0; j < numberOfSelectedSamples; j++)
                            {
                                _nonDecimatedData[i, j] = fullRawData[arrayElementsPerSample * j + i];
                            }
                        }
                    }

                    #endregion

                    // Set flags and return.
                    this.ReportProgress(100);
                    isCompletePreview = true;
                    previewRatio = 1;
                    return;
                }

                #endregion

                #region ---- If number of selected samples is small, use memory buffer mode.----

                // If number of selected samples is less than predefined size, 
                // use memory buffer mode: read all selected data into memory for processing.
                if (numberOfSelectedSamples * bytesPerSample <= MemoryBufferModeMaxBytes)
                {
                    // Read all selected data into memory.
                    fullRawData = new short[numberOfSelectedSamples * arrayElementsPerSample];
                    _file.Seek(_selectionStart, SeekOrigin.Begin);
                    _file.Read(fullRawData);

                    // Calculate parameters used to split data into segments.
                    numberOfSegments = _previewLength / 2;
                    samplesPerSegment = numberOfSelectedSamples /  (double)numberOfSegments;
                    var segments = new DataPreviewSegmentBase[numberOfSegments];
                    for (i = 0; i < numberOfSegments; i++) { segments[i] = new DataPreviewSegmentBase(); }

                    segments[0].PositionStart = 0;
                    segments[0].PositionEnd = (long)Math.Floor(samplesPerSegment) - 1;
                    for (i = 1; i < numberOfSegments; i++)
                    {
                        segments[i].PositionStart = segments[i - 1].PositionEnd + 1;
                        segments[i].PositionEnd = (long) Math.Floor(samplesPerSegment * (i + 1)) - 1;
                    }
                    // Set end position of last segment to make sure all selected data is covered.
                    segments[numberOfSegments - 1].PositionEnd = numberOfSelectedSamples - 1;

                    short[] segmentRawData = null;

                    #region ------------- Go through data quickly by proccessing a few points per segment to generate a quick preview -----------
                    for (i = 0; i < numberOfSegments; i++)
                    {
                        //Copy a few points of current segment from full raw data.
                        samplesOfCurrentSegment = (int)(segments[i].PositionEnd - segments[i].PositionStart + 1);
                        samplesOfCurrentSegment = Math.Min(samplesOfCurrentSegment, QuickPreviewSamplersPerSegment);
                        if (segmentRawData == null || segmentRawData.Length != samplesOfCurrentSegment * arrayElementsPerSample)
                        {
                            segmentRawData = new short[samplesOfCurrentSegment * arrayElementsPerSample];
                        }
                        Array.Copy(fullRawData, segments[i].PositionStart * arrayElementsPerSample,
                                            segmentRawData, 0, samplesOfCurrentSegment * arrayElementsPerSample);

                        // Process current segment.
                        ProcessDataForPreview(segmentRawData, ref previewResultOfAllCh1Seg, isInterleavedIQ);

                        // Copy preview result of segment to full preview result.
                        for (j = 0; j < numberOfChannels; j++)
                        {
                            _decimatedPreviewResult[j, i].IndexOfMax = previewResultOfAllCh1Seg[j].IndexOfMax + segments[i].PositionStart;
                            _decimatedPreviewResult[j, i].MaxValue = previewResultOfAllCh1Seg[j].MaxValue;
                            _decimatedPreviewResult[j, i].IndexOfMin = previewResultOfAllCh1Seg[j].IndexOfMin + segments[i].PositionStart;
                            _decimatedPreviewResult[j, i].MinValue = previewResultOfAllCh1Seg[j].MinValue;
                        }

                        // Continue next segment.
                    }
#if DEBUG
                    Debug.WriteLine("Quick preview done, elapsed time: {0} ms.", _timeoutStopwatch.ElapsedMilliseconds);
#endif
                    #endregion

                    // Report progress to caller(GUI).
                    this.ReportProgress(1);
                    reportStopwatch.Restart();

                    // Process data by segments and update to _previewResult.
                    for (i = 0;  i < numberOfSegments; i++)
                    {
                        // Copy 1 segment data from full raw data.
                        samplesOfCurrentSegment = (int) (segments[i].PositionEnd - segments[i].PositionStart + 1);
                        if (segmentRawData == null || segmentRawData.Length != samplesOfCurrentSegment * arrayElementsPerSample)
                        {
                            segmentRawData = new short[samplesOfCurrentSegment * arrayElementsPerSample];
                        }
                        Array.Copy(fullRawData, segments[i].PositionStart * arrayElementsPerSample,
                                            segmentRawData, 0, samplesOfCurrentSegment * arrayElementsPerSample);

                        // Process 1 segment data.
                        ProcessDataForPreview(segmentRawData, ref previewResultOfAllCh1Seg, isInterleavedIQ);

                        // Copy preview result of segment to full preview result.
                        for (j = 0; j < numberOfChannels; j++)
                        {
                            // The index of max returned from 1 block processing is relative, add offset to convert to absolute index.
                            _decimatedPreviewResult[j, i].IndexOfMax = previewResultOfAllCh1Seg[j].IndexOfMax + segments[i].PositionStart;
                            _decimatedPreviewResult[j, i].MaxValue = previewResultOfAllCh1Seg[j].MaxValue;
                            _decimatedPreviewResult[j, i].IndexOfMin = previewResultOfAllCh1Seg[j].IndexOfMin + segments[i].PositionStart;
                            _decimatedPreviewResult[j, i].MinValue = previewResultOfAllCh1Seg[j].MinValue;
                        }

                        // Update progress.
                        previewRatio = (i + 1) / (double) numberOfSegments;
                        if (_timeout == System.Threading.Timeout.Infinite)
                            { processingProgress = previewRatio; }
                        else
                            { processingProgress = Math.Min(1, Math.Max((double)_timeoutStopwatch.ElapsedMilliseconds / _timeout, previewRatio)); }

                        // Report progress to caller (GUI) if configured interval reached and report enabled.
                        if (this.WorkerReportsProgress && (reportStopwatch.ElapsedMilliseconds > Math.Max(0, _workerReportInterval)))
                        {
                            this.ReportProgress((int)(processingProgress * 100));
                            reportStopwatch.Restart();
                            // Disable report progress to avoid entering event handler frequently, if "event producer" (report progress) is running faster than
                            // "event consumer" (event handler), it will cause "GUI freezing" due to too many pending events, so we turn off progress reporting
                            // after each reporting, and expect caller (GUI) to re-enable progress reporting when finishing event handler.
                            this.WorkerReportsProgress = false;
                        }

                        // Check if user stopped processing or configured timeout reached.
                        if (this.CancellationPending) { break; }
                        if (_timeout != System.Threading.Timeout.Infinite && _timeoutStopwatch.ElapsedMilliseconds >= _timeout) { break; }

                        // Continue next segment.
                    }

#if DEBUG
                    Debug.WriteLine("Processed done, elapsed time: {0} ms.", _timeoutStopwatch.ElapsedMilliseconds);
#endif

                    // If all segments are processed, it is a complete preview.
                    if (i == numberOfSegments) { isCompletePreview = true; }
                    return;
                }

                #endregion

                #region ---- Number of selected samples is large, use file buffer mode.----
                // Number of selected samples is large, read from file by multiple times.
                else
                {

                    #region ---- Calculate parameters used to split selected data into segements and split each segment into blocks.------

                    numberOfSegments = _previewLength / 2;

                    // Calculate segment information to split full selection into segments.
                    samplesPerSegment = numberOfSelectedSamples / (double)numberOfSegments;
                    var segments = new DataPreviewSegmentForMultiRead[numberOfSegments];
                    for (i = 0; i < numberOfSegments; i++) { segments[i] = new DataPreviewSegmentForMultiRead(); }

                    segments[0].PositionStart = _selectionStart;
                    segments[0].PositionEnd = _selectionStart + (long)Math.Floor(samplesPerSegment) - 1;
                    for (i = 1; i < numberOfSegments; i++)
                    {
                        segments[i].PositionStart = segments[i - 1].PositionEnd + 1;
                        segments[i].PositionEnd = _selectionStart + (long)Math.Floor(samplesPerSegment * (i + 1)) - 1;
                    }
                    // Set end position of last segment to make sure all selected data is covered.
                    segments[numberOfSegments - 1].PositionEnd = _selectionEnd;

                    // For each segment, split it into blocks, each block corresponds to once file reading and processing.
                    int numberOfBlocksOfLargestSeg = 0;
                    int samplesPerRead = FileBufferModeBytesPerRead / bytesPerSample;
                    for (i = 0; i < segments.Length; i++)
                    {
                        // Calculate number of blocks of current segment.
                        samplesOfCurrentSegment = (int)(segments[i].PositionEnd - segments[i].PositionStart + 1);
                        numberOfBlocksOfCurrentSeg = samplesOfCurrentSegment / samplesPerRead;
                        if (samplesOfCurrentSegment % samplesPerRead != 0) { numberOfBlocksOfCurrentSeg++; }

                        // Remember the max number of blocks.
                        if (numberOfBlocksOfCurrentSeg > numberOfBlocksOfLargestSeg) { numberOfBlocksOfLargestSeg = numberOfBlocksOfCurrentSeg; }

                        // Set samples per block.
                        segments[i].SamplesPerBlock = new int[numberOfBlocksOfCurrentSeg];
                        for (j = 0; j < numberOfBlocksOfCurrentSeg; j ++) { segments[i].SamplesPerBlock[j] = samplesPerRead; }

                        // The last block might be special.
                        if (samplesOfCurrentSegment % samplesPerRead != 0)
                        { segments[i].SamplesPerBlock[numberOfBlocksOfCurrentSeg - 1] = samplesOfCurrentSegment % samplesPerRead; }
                    }
                    #endregion

                    int arrayLengthOfCurrentBlock;
                    long currentReadPosition;
                    short[] rawDataOfCurrentBlock = null;

                    #region ------------- Go through file quickly by proccessing a few points per segment to generate a quick preview -----------

                    for (i = 0; i < numberOfSegments; i++)
                    {
                        // Get file read position and length of current block of current segment.
                        currentReadPosition = segments[i].PositionStart;
                        samplesOfCurrentSegment = (int)(segments[i].PositionEnd - segments[i].PositionStart + 1);
                        samplesOfCurrentSegment = Math.Min(samplesOfCurrentSegment, QuickPreviewSamplersPerSegment);
                        if (rawDataOfCurrentBlock == null || rawDataOfCurrentBlock.Length != samplesOfCurrentSegment * arrayElementsPerSample)
                        {
                            rawDataOfCurrentBlock = new short[samplesOfCurrentSegment * arrayElementsPerSample];
                        }

                        // Read a few points of current segment from file.
                        _file.Seek(currentReadPosition, SeekOrigin.Begin);
                        _file.Read(rawDataOfCurrentBlock);

                        // Process current segment.
                        ProcessDataForPreview(rawDataOfCurrentBlock, ref previewResultOfAllCh1Seg, isInterleavedIQ);

                        // Copy preview result of segment to full preview result.
                        for (j = 0; j < numberOfChannels; j++)
                        {
                            // The index of max/min returned from 1 block processing is relative, add offset to convert to absolute index.
                            _decimatedPreviewResult[j, i].IndexOfMax = previewResultOfAllCh1Seg[j].IndexOfMax + currentReadPosition;
                            _decimatedPreviewResult[j, i].MaxValue = previewResultOfAllCh1Seg[j].MaxValue;
                            _decimatedPreviewResult[j, i].IndexOfMin = previewResultOfAllCh1Seg[j].IndexOfMin + currentReadPosition;
                            _decimatedPreviewResult[j, i].MinValue = previewResultOfAllCh1Seg[j].MinValue;
                        }

                        // Continue next segment.
                    }
#if DEBUG
                    Debug.WriteLine("Quick preview done, elapsed time: {0} ms.", _timeoutStopwatch.ElapsedMilliseconds);
#endif
                    #endregion

                    // Report progress to caller(GUI).
                    this.ReportProgress(1);
                    reportStopwatch.Restart();

                    // Process file by blocks and segments.
                    for (i = 0; i < numberOfBlocksOfLargestSeg; i++)
                    {
                        for (j = 0; j < segments.Length; j++)
                        {
                            // If all blocks of current segment are processed, continue to process next segment.
                            // Since number of blocks of each segment might be different, so we need to loop with "numberOfBlocksOfLargestSeg".
                            if (segments[j].SamplesPerBlock.Length <= i) { continue; }

                            // Get file read position and length of current block of current segment.
                            currentReadPosition = segments[j].PositionStart;
                            for (k = 0; k < i; k++) { currentReadPosition += segments[j].SamplesPerBlock[k]; }
                            arrayLengthOfCurrentBlock = segments[j].SamplesPerBlock[i] * arrayElementsPerSample;

                            // New array and read data from file.
                            if (rawDataOfCurrentBlock == null || rawDataOfCurrentBlock.Length != arrayLengthOfCurrentBlock)
                            {
                                rawDataOfCurrentBlock = new short[arrayLengthOfCurrentBlock];
                            }
                            _file.Seek(currentReadPosition, SeekOrigin.Begin);
                            _file.Read(rawDataOfCurrentBlock);

                            // Process 1 segment data.
                            ProcessDataForPreview(rawDataOfCurrentBlock, ref previewResultOfAllCh1Seg, isInterleavedIQ);

                            // Update preview result of current block to full preview result, if necessory.
                            for (k = 0; k < numberOfChannels; k++)
                            {
                                if (previewResultOfAllCh1Seg[k].MaxValue > _decimatedPreviewResult[k, j].MaxValue)
                                {
                                    _decimatedPreviewResult[k, j].MaxValue = previewResultOfAllCh1Seg[k].MaxValue;
                                    // The index of max returned from 1 block processing is relative, add offset to convert to absolute index.
                                    _decimatedPreviewResult[k, j].IndexOfMax = previewResultOfAllCh1Seg[k].IndexOfMax + currentReadPosition;
                                }
                                if (previewResultOfAllCh1Seg[k].MinValue < _decimatedPreviewResult[k, j].MinValue)
                                {
                                    _decimatedPreviewResult[k, j].MinValue = previewResultOfAllCh1Seg[k].MinValue;
                                    // The index of max returned from 1 block processing is relative, add offset to convert to absolute index.
                                    _decimatedPreviewResult[k, j].IndexOfMin = previewResultOfAllCh1Seg[k].IndexOfMin + currentReadPosition;
                                }
                            }

                            // Calculate preview ratio by "number of processed blocks / number of total blocks".
                            previewRatio = (i * numberOfSegments + (j + 1)) / (double) (numberOfSegments * numberOfBlocksOfLargestSeg);

                            // Update progress.
                            if (_timeout == System.Threading.Timeout.Infinite)
                            { processingProgress = previewRatio; }
                            else
                            { processingProgress = Math.Min(1, Math.Max((double)_timeoutStopwatch.ElapsedMilliseconds / _timeout, previewRatio)); }

                            // Report progress to caller (GUI) if configured interval reached and report enabled.
                            if (this.WorkerReportsProgress && (reportStopwatch.ElapsedMilliseconds > Math.Max(0, _workerReportInterval)))
                            {
                                this.ReportProgress((int)(processingProgress * 100));
                                reportStopwatch.Restart();
                                // Disable report progress to avoid entering event handler frequently, if "event producer" (report progress) is running faster than
                                // "event consumer" (event handler), it will cause "GUI freezing" due to too many pending events, so we turn off progress reporting
                                // after each reporting, and expect caller (GUI) to re-enable progress reporting when finishing event handler.
                                this.WorkerReportsProgress = false;
                            }

                            // Check if user stopped processing or configured timeout reached.
                            if (this.CancellationPending) { break; }
                            if (_timeout != System.Threading.Timeout.Infinite && _timeoutStopwatch.ElapsedMilliseconds >= _timeout) { break; }

                            // Continue next segment.
                        }

                        // Check if user stopped processing or configured timeout reached.
                        if (this.CancellationPending) { break; }
                        if (_timeout != System.Threading.Timeout.Infinite && _timeoutStopwatch.ElapsedMilliseconds >= _timeout) { break; }

                        // Continue next block.
                    }

#if DEBUG
                    Debug.WriteLine("Processed done, elapsed time: {0} ms.", _timeoutStopwatch.ElapsedMilliseconds);
#endif

                    if (i == numberOfBlocksOfLargestSeg) { isCompletePreview = true; }
                    return;
                }
                #endregion
            }

            finally
            {
                // Close file anyway.
                _file.Close();

                // Set result.
                e.Result = new VectorDataPreviewResult(isCompletePreview, previewRatio, processingProgress * 100);
            }
        }

        /// <summary>
        /// Find the Max/Min pair of input "rawData" and return corresponding amplitude in "previewResult".
        /// </summary>
        /// <param name="rawData">Channel interleaved data, and I/Q interleaved if data is complex format.</param>
        /// <param name="previewResult">Returned preview data, its length must be the same as number of channels.</param>
        /// <param name="isInterleavedIQ">If true, input "rawData" is interleaved IQ format, if false, "rawData" is real format.</param>
        private void ProcessDataForPreview(short[] rawData, ref DataPreviewResultOf1Ch1Segment[] previewResult, bool isInterleavedIQ)
        {
            int i,j;
            int arrayElementsPerSample, numberOfSamples;
            int indexOfCurrent, indexOfMax, indexOfMin;
            double amplitudeCurrent, amplitudeMax, amplitudeMin;

            int numberOfChannels = previewResult.Length;

            // If data type is Complex, input data is I/Q interleaved, 1 data point includes 2 consequent array elements.
            if (isInterleavedIQ)
            {
                arrayElementsPerSample = numberOfChannels * 2;
                numberOfSamples = rawData.Length / arrayElementsPerSample;
                for (i = 0; i < numberOfChannels; i++)
                {
#region ---------- Go through data of channel[i] to find power Max/Min position ----------

                    amplitudeMax = double.MinValue;
                    indexOfMax = 0;
                    amplitudeMin = double.MaxValue;
                    indexOfMin = 0;
                    for (j = 0; j < numberOfSamples; j++)
                    {
                        // Calculate amplitude of current sample.
                        indexOfCurrent = j * arrayElementsPerSample + i * 2;
                        amplitudeCurrent = Math.Sqrt(Math.Pow(rawData[indexOfCurrent], 2) + Math.Pow(rawData[indexOfCurrent + 1], 2));

                        // Remember current index if amplitude of current sample is greater than history Max value.
                        if (amplitudeCurrent > amplitudeMax)
                        {
                            amplitudeMax = amplitudeCurrent;
                            indexOfMax = indexOfCurrent;
                        }

                        // Remember current index if amplitude of current sample is less than history Min value.
                        if (amplitudeCurrent < amplitudeMin)
                        {
                            amplitudeMin = amplitudeCurrent;
                            indexOfMin = indexOfCurrent;
                        }
                    }
#endregion

                    // Copy data at Max/Min index to preview result.
                    previewResult[i].IndexOfMax = indexOfMax;
                    previewResult[i].IndexOfMin = indexOfMin;
                    previewResult[i].MaxValue = amplitudeMax;
                    previewResult[i].MinValue = amplitudeMin;
                }
            }
            else // data type is Real
            {
                numberOfSamples = rawData.Length / numberOfChannels;
                for (i = 0; i < numberOfChannels; i++)
                {
#region ---------- Go through data of channel[i] to find power Max/Min position ----------

                    amplitudeMax = double.MinValue;
                    indexOfMax = 0;
                    amplitudeMin = double.MaxValue;
                    indexOfMin = 0;
                    for (j = 0; j < numberOfSamples; j++)
                    {
                        // Get amplitude of current sample.
                        indexOfCurrent = j * numberOfChannels + i;
                        amplitudeCurrent = rawData[indexOfCurrent];

                        // Remember current index if amplitude of current sample is greater than history Max value.
                        if (amplitudeCurrent > amplitudeMax)
                        {
                            amplitudeMax = amplitudeCurrent;
                            indexOfMax = indexOfCurrent;
                        }

                        // Remember current index if amplitude of current sample is less than history Min value.
                        if (amplitudeCurrent < amplitudeMin)
                        {
                            amplitudeMin = amplitudeCurrent;
                            indexOfMin = indexOfCurrent;
                        }
                    }
                    #endregion

                    // Copy data at Max/Min index to preview result.
                    previewResult[i].IndexOfMax = indexOfMax;
                    previewResult[i].IndexOfMin = indexOfMin;
                    previewResult[i].MaxValue = amplitudeMax;
                    previewResult[i].MinValue = amplitudeMin;
                }
            }

        }

        #endregion

        #region ------------Data Types and Structures----------

        internal struct DataPreviewResultOf1Ch1Segment
        {
            /// <summary>
            /// Minimum value of the Segment
            /// </summary>
            public double MinValue;

            /// <summary>
            /// Maximum value of the Segment
            /// </summary>
            public double MaxValue;

            /// <summary>
            /// Index of minimum value.
            /// </summary>
            public long IndexOfMin;

            /// <summary>
            /// Index of maximumvalue.
            /// </summary>
            public long IndexOfMax;
        }

        #endregion

    }
}
