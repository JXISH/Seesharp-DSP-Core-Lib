using System;
using System.Diagnostics;
using System.ComponentModel;

namespace SeeSharpTools.JXI.FileIO.VectorFile
{
    /// <summary>
    /// Base class of data previewer for Vector File.
    /// </summary>
    public abstract class VectorDataPreviewerBase:BackgroundWorker
    {

        #region ------------------- Constants-------------------

        private static readonly double UnitConversionImpedance = 50;

        #endregion

        #region ------------------- Private Fields -------------------

        /// <summary>
        /// Stopwatch to keep elapsed time since Start();
        /// </summary>
        protected Stopwatch _timeoutStopwatch;

        #endregion

        #region ------------------- Constructor and Destructor -------------------

        /// <summary>
        /// Create instance of data previewer for Vector File.
        /// </summary>
        public VectorDataPreviewerBase()
        {
            // Initialize private fields.
            _timeout = 3000;
            _workerReportInterval = 100;

            _timeoutStopwatch = new Stopwatch();

            this.WorkerReportsProgress = true;
            this.WorkerSupportsCancellation = true;
            this.DoWork += ThreadProcessPreview;
        }

        #endregion

        #region ------------------- Public Properties -------------------

        /// <summary>
        /// Full path of file.
        /// </summary>
        protected string _filePath;
        /// <summary>
        /// Full path of file.
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        /// <summary>
        /// Max time to generate data preview, in milliseconds.
        /// </summary>
        protected int _timeout;
        /// <summary>
        /// Max time to generate data preview, in milliseconds.
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// Interval of the worker reports progress, in ms (milliseconds).
        /// </summary>
        protected int _workerReportInterval;
        /// <summary>
        /// Interval of the worker reports progress, in ms (milliseconds).
        /// </summary>
        public int WorkerReportInterval
        {
            get { return _workerReportInterval; }
            set { _workerReportInterval = value;}
        }

        #endregion

        #region ------------------- Public Methods -------------------

        /// <summary>
        /// Commit data preview parameters.
        /// </summary>
        public virtual void Commit()
        {
            // Re-entrant protection.
            if (this.IsBusy) { throw new InvalidOperationException("Previous preview task is still running."); }
        }

        /// <summary>
        /// Start background thread to process data for preview.
        /// </summary>
        public virtual void Start()
        {
            // Re-entrant protection.
            if (this.IsBusy) { throw new InvalidOperationException("Previous preview task is still running."); }

            // Start background worker.
            this.WorkerReportsProgress = true;
            this.RunWorkerAsync();

            // Start stopwatch.
            _timeoutStopwatch.Restart();
        }

        /// <summary>
        /// Stop backgournd thread which is processing data for preview.
        /// </summary>
        public virtual void Stop()
        {
            // Check if background thread is still running.
            if (! this.IsBusy) { return; }

            // Set flag to stop thread and wait for thread to stop.
            if (! this.CancellationPending) { this.CancelAsync(); }

            // Stop stopwatch.
            if (_timeoutStopwatch.IsRunning) { _timeoutStopwatch.Stop(); }
        }

        #endregion

        #region ------------------- Private Methods -------------------

        /// <summary>
        /// Implementation of processing data for preview.
        /// </summary>
        protected abstract void ThreadProcessPreview(object sender, DoWorkEventArgs e);

        /// <summary>
        /// Convert input unscaled raw amplitude to specified outputUnit.
        /// </summary>
        /// <param name="amplitude"> Raw amplitude (usually ADC value).</param>
        /// <param name="scaleFactor"> Scale factor used to convert unscaled amplitude to Volt. </param>
        /// <param name="outputUnit"></param>
        /// <returns></returns>
        protected double UnitConversion(double amplitude, double scaleFactor, VectorDataAmplitudeUnit outputUnit)
        {
            double convertedValue = amplitude;

            switch (outputUnit)
            {
                case VectorDataAmplitudeUnit.Unscaled:
                    convertedValue = amplitude;
                    break;
                case VectorDataAmplitudeUnit.V:
                    convertedValue = amplitude * scaleFactor;
                    break;
                case VectorDataAmplitudeUnit.uV:
                    convertedValue = amplitude * scaleFactor * 1E6;
                    break;
                case VectorDataAmplitudeUnit.dBuV:
                    // If rawAmplitude (I8/I16) is zero, force it to be 1, otherwise it could NOT be converted to power in dB unit due to invalid Log operation.
                    convertedValue = Math.Max(Math.Abs(amplitude), 1) * scaleFactor;
                    convertedValue = 20 * Math.Log10(convertedValue) + 120;
                    break;
                case VectorDataAmplitudeUnit.dBm:
                    convertedValue = Math.Max(Math.Abs(amplitude), 1) * scaleFactor;
                    convertedValue = 10 * Math.Log10(convertedValue * convertedValue / UnitConversionImpedance) + 30;
                    break;
            }

            return convertedValue;
        }

        #endregion

    }


    #region ----------Data Types and Structures----------

    /// <summary>
    /// Unit of preview data.
    /// </summary>
    public enum VectorDataAmplitudeUnit
    {
        /// <summary>
        /// Amplitude in Volt.
        /// </summary>
        V,

        /// <summary>
        /// Amplitude in MicroVolt
        /// </summary>
        uV,

        /// <summary>
        /// Power in dBuV.
        /// </summary>
        dBuV,

        /// <summary>
        /// Power in dBm.
        /// </summary>
        dBm,

        /// <summary>
        /// Raw unscaled amplitude.
        /// </summary>
        Unscaled
    }

    internal class DataPreviewSegmentBase
    {
        /// <summary>
        /// Start position in samples.
        /// </summary>
        public long PositionStart { get; set; }

        /// <summary>
        /// End position in samples.
        /// </summary>
        public long PositionEnd { get; set; }
    }

    internal class DataPreviewSegmentForMultiRead:DataPreviewSegmentBase
    {
        /// <summary>
        /// Samples to read from file, for each block.
        /// </summary>
        public int[] SamplesPerBlock { get; set; }
    }

    /// <summary>
    /// VectorDataPreviewResult 
    /// </summary>
    public class VectorDataPreviewResult
    {
        /// <summary>
        /// Indicates whether all selected data is processed (correspondingly "PreviewRatio" is 1).
        /// </summary>
        public bool IsCompletePreview { get; set; }

        /// <summary>
        /// Ratio (0 ~ 1) of processed data length to selected data length.
        /// </summary>
        public double PreviewRatio { get; set; }

        /// <summary>
        /// Progress in percentage (1 ~100).
        /// </summary>
        public double Progress { get; set; }

        /// <summary>
        /// VectorDataPreviewResult
        /// </summary>
        /// <param name="isCompletePreview"></param>
        /// <param name="previewRatio"></param>
        /// <param name="progress"></param>
        public VectorDataPreviewResult(bool isCompletePreview = false, double previewRatio = 0, double progress = 0)
        {
            this.IsCompletePreview = isCompletePreview;
            this.PreviewRatio = previewRatio;
            this.Progress = progress;
        }
    }

    #endregion

}
