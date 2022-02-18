using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using SeeSharpTools.JXI.FileIO.VectorFile;
using SeeSharpTools.JXI.DSP.Spectrum;

namespace SimpleVectorDataViewer
{
    public partial class MainForm : Form
    {
        #region------------------Constants----------------------------

        const double SecondsPerDay = 86400; // 3600 * 24;
        static readonly int NumberOfSpectralLines = 2000;
        static readonly double UnitConversionImpedance = 50;

        #endregion

        FixFrequencyStreamFile _vectorFile;
        FixFrequencyStreamDataPreviewer _vectorDataPreviewer;
        SpectrumTask _spectrumTask;

        string _filePath;
        double[] _xAxisDataOfSelection, _previewDataOfSelection;
        double[] _xAxisDataForDisplay, _yAxisDataForDisplay;

        // Data information.
        double _dataSampleRate;
        long _numberOfTotalSamples;

        // Current selection.
        bool _isFullSelection;
        long _currentSelectionStart, _currentSelectionEnd;

        // Remember full selection preview data.
        bool _isFullSelectionPreviewReady;
        double[] _fullSelectionPreviewX, _fullSelectionPreviewY;
        double _fullSelectionDecimation;

        // Flags
        bool _isFormLoaded;

        public MainForm()
        {
            InitializeComponent();
            SeeSharpTools.JXI.DSP.LicenseBase.LicenseBase.Validate("Z24S7-BNVLB-AE33P-AJCZX-CDUMC", "YEVUB-B7VKV-LQKRR-7FHUZ-5UHPQ", "");
            SeeSharpTools.JXI.FileIO.VectorFile.LicenseBase.LicenseBase.Validate("Z24S7-BNVLB-AE33P-AJCZX-CDUMC", "WFYDR-V32OC-26W5H-S6VST-LHIR2", "");
            // New VectorDataPreviewer instance and subscribe events.
            _vectorDataPreviewer = new FixFrequencyStreamDataPreviewer();
            _vectorDataPreviewer.Unit = VectorDataAmplitudeUnit.Unscaled;
            _vectorDataPreviewer.PreviewLengthAuto = true;
            _vectorDataPreviewer.Timeout = Timeout.Infinite;
            _vectorDataPreviewer.WorkerReportInterval = 200;
            _vectorDataPreviewer.RunWorkerCompleted += VectorDataPreviewer_RunWorkerCompleted;
            _vectorDataPreviewer.ProgressChanged += VectorDataPreviewer_ProgressChanged;

            // New VectorFile instance for reading raw data for display.
            _vectorFile = new FixFrequencyStreamFile();

            // New SpectrumTask for spectrum.
            _spectrumTask = new SpectrumTask();
            _spectrumTask.WindowType = FFTWindowType.Seven_Term_Flat_Top;
            _spectrumTask.Average.Mode = SpectrumAverageMode.PeakHold;
            _spectrumTask.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _spectrumTask.Average.Size = 100;
            _spectrumTask.Unit.Impedance = 50;
            _spectrumTask.Unit.Type = SpectrumOutputUnit.dBm;
            _spectrumTask.Unit.IsPSD = false;

            _isFormLoaded = false;
        }

        #region------------------Event Handlers----------------------------

        private void MainForm_Load(object sender, EventArgs e)
        {
            var iqDataUnits = Enum.GetNames(typeof(IQDisplayUnitType));//IQ单位
            foreach (var item in iqDataUnits) { _guiIQDisplayUnit.Items.Add(item); }
            _guiIQDisplayUnit.SelectedIndex = 0;

            var iqDisplayDataTypes = Enum.GetNames(typeof(IQDisplayDataType));//数据类型
            foreach (var item in iqDisplayDataTypes) { _guiIQDisplayDataType.Items.Add(item); }
            _guiIQDisplayDataType.SelectedIndex = 0;

            var averageModes = Enum.GetNames(typeof(SpectrumAverageMode));//平均模式
            foreach (var item in averageModes) { _guiSpectrumAvgMode.Items.Add(item); }
            _guiSpectrumAvgMode.SelectedIndex = 2;

            _isFormLoaded = true;
        }

        private void GuiOpenFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Show dialog for user to select IQ file.
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.RestoreDirectory = false;
                fileDialog.Filter = "IQ Files (*.IQ)|*.IQ";
                fileDialog.Title = "Select vector file to open";
                if (fileDialog.ShowDialog() == DialogResult.Cancel) { return; }

                // Reset overview chart zoom.
                if(_guiOverviewChart.ChartAreas[0].AxisX.ScaleView.IsZoomed) { _guiOverviewChart.ChartAreas[0].AxisX.ScaleView.ZoomReset(100); }

                // Open file to get data information.
                var vectorFile = new FixFrequencyStreamFile();
                vectorFile.Open(fileDialog.FileName, FileMode.Open, FileAccess.Read);
                _numberOfTotalSamples = vectorFile.NumberOfSamples;
                _dataSampleRate = vectorFile.Sampling.SampleRate;
                vectorFile.Close();

                // Set file path and set selection to full range.
                _guiFilePath.Text = fileDialog.FileName;
                _filePath = fileDialog.FileName;
                _currentSelectionStart = 0;
                _currentSelectionEnd = _numberOfTotalSamples - 1;

                // Clear flag.
                _isFullSelectionPreviewReady = false;

                // Do preview.
                StartPreviewOfSelection();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void VectorDataPreviewer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                // Pause pending report from preview task.
                _vectorDataPreviewer.WorkerReportsProgress = false;

                _guiStatusStripProgressBar.Value = e.ProgressPercentage;
                _vectorDataPreviewer.GetData(ref _previewDataOfSelection);
                RefreshOverviewChart();

                // Resume report from preview task.
                _vectorDataPreviewer.WorkerReportsProgress = true;
            }
            catch(Exception exception) { Debug.WriteLine(exception.Message); }

        }

        private void VectorDataPreviewer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Prompt user if exception occured during preview processing.
            if (e.Error != null) { MessageBox.Show(e.Error.Message); }

            // Refresh final data.
            _vectorDataPreviewer.GetData(ref _previewDataOfSelection);
            RefreshOverviewChart();

            // Get preview result and display.
            var previewResult = (VectorDataPreviewResult)e.Result;
            _guiStatusStripProgressBar.Value = (int)previewResult.Progress;

            // If it is the first preview (full selection preview), remember it.
            if (_isFullSelection)
            {
                _fullSelectionPreviewX = new double[_xAxisDataOfSelection.Length];
                Array.Copy(_xAxisDataOfSelection, _fullSelectionPreviewX, _xAxisDataOfSelection.Length);

                _fullSelectionPreviewY = new double[_previewDataOfSelection.Length];
                Array.Copy(_previewDataOfSelection, _fullSelectionPreviewY, _previewDataOfSelection.Length);

                _isFullSelectionPreviewReady = true;
            }

            // Enable scrollbar.
            _guiOverviewChart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;

            // Display raw IQ and spectrum.
            ReadRawDataFromFileAndDisplay();
        }

        private void GuiOverviewChart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            // Get selection.
            _currentSelectionStart = (long)(OADateToSeconds(e.Axis.ScaleView.ViewMinimum) * _dataSampleRate);
            if (_currentSelectionStart < 0) { _currentSelectionStart = 0; }
            _currentSelectionEnd = (long)(OADateToSeconds(e.Axis.ScaleView.ViewMaximum) * _dataSampleRate);
            if (_currentSelectionEnd > _numberOfTotalSamples - 1) { _currentSelectionEnd = _numberOfTotalSamples - 1; }

            // Disable scrollbar to avoid re-entrant.
            //_guiOverviewChart.ChartAreas[0].AxisX.ScrollBar.Enabled = false;

            StartPreviewOfSelection();
        }

        private void IQAndSpectrumSettings_ValueChanged(object sender, EventArgs e)
        {
            if (! _isFormLoaded) { return; }
            ReadRawDataFromFileAndDisplay();
        }

        #endregion

        #region------------------Private Methods----------------------------

        private void StartPreviewOfSelection()
        {
            double decimation;

            // Set "_isFullSelection" flag.
            _isFullSelection = (_currentSelectionStart == 0 && _currentSelectionEnd == (_numberOfTotalSamples - 1));

            // If current selection is full selection (top view) and it is already generated before, use history data.
            if ( _isFullSelection && _isFullSelectionPreviewReady)
            {
                decimation = _fullSelectionDecimation;
                _guiOverviewChart.Series[0].Points.DataBindXY(_fullSelectionPreviewX, _fullSelectionPreviewY);
            }
            else // Apply selection and start preview task.
            {
                // Commit and get actual settings.
                _vectorDataPreviewer.FilePath = _filePath;
                _vectorDataPreviewer.SelectionStart = _currentSelectionStart;
                _vectorDataPreviewer.SelectionEnd = _currentSelectionEnd;
                _vectorDataPreviewer.Commit();
                int previewLength = _vectorDataPreviewer.PreviewLength;
                decimation = _vectorDataPreviewer.Decimation;

                // Remember decimation if currently is full selection.
                if (_isFullSelection) { _fullSelectionDecimation = decimation; }

                // New array and generate x axis data.
                _previewDataOfSelection = new double[previewLength];
                _xAxisDataOfSelection = new double[previewLength];
                for (int i = 0; i < previewLength; i++)
                { _xAxisDataOfSelection[i] = SecondsToOADate(_currentSelectionStart / _dataSampleRate + i * decimation / _dataSampleRate); }

                // Set X axis scroll step to 1/10 ot current selection.
                _guiOverviewChart.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = SecondsToOADate(decimation / _dataSampleRate) * 0.1 * previewLength;

                // Start and get data until done.
                _vectorDataPreviewer.Start();
            }

            // Set cursor moving step to dt.
            _guiOverviewChart.ChartAreas[0].CursorX.Interval = SecondsToOADate(decimation / _dataSampleRate);
        }

        private void RefreshOverviewChart()
        {
            if(_isFullSelection)
            {
                _guiOverviewChart.Series[0].Points.DataBindXY(_xAxisDataOfSelection, _previewDataOfSelection);
            }
            else
            {
                int previewResultLength = _xAxisDataOfSelection.Length;
                int displayLength = _xAxisDataOfSelection.Length + 2;

                if(_xAxisDataForDisplay == null || _xAxisDataForDisplay.Length != displayLength)
                {
                    _xAxisDataForDisplay = new double[displayLength];
                    _yAxisDataForDisplay = new double[displayLength];
                }

                _xAxisDataForDisplay[0] = _fullSelectionPreviewX[0];
                _xAxisDataForDisplay[displayLength - 1] = _fullSelectionPreviewX[previewResultLength - 1];
                _yAxisDataForDisplay[0] = _fullSelectionPreviewY[0];
                _yAxisDataForDisplay[displayLength - 1] = _fullSelectionPreviewY[previewResultLength - 1];

                Array.Copy(_xAxisDataOfSelection, 0, _xAxisDataForDisplay, 1, previewResultLength);
                Array.Copy(_previewDataOfSelection, 0, _yAxisDataForDisplay, 1, previewResultLength);

                _guiOverviewChart.Series[0].Points.DataBindXY(_xAxisDataForDisplay, _yAxisDataForDisplay);
            }
        }

        private void ReadRawDataFromFileAndDisplay()
        {
            int i, j;

            // Open file.
            _vectorFile.Open(_filePath, FileMode.Open, FileAccess.Read);

            try
            {
                // Get data sampling information.
                int numberOfChannels = _vectorFile.Storage.NumberOfChannels;
                int arrayElementsPerSample = _vectorFile.BytesPerSample / sizeof(short);
                double[] scaleFactor = new double[numberOfChannels];
                for (i = 0; i < numberOfChannels; i++){ scaleFactor[i] = _vectorFile.Sampling.Channels[i].GetScaleFactor(); }

                // Get IQ display number of samples from GUI, coerce to selected number of samples in overview chart.
                long numberOfRawSamplesToRead = (long) _guiIQDisplaySamples.Value;
                long numberOfRawSamplesSelected = _currentSelectionEnd - _currentSelectionStart + 1;
                if (numberOfRawSamplesToRead > numberOfRawSamplesSelected) { numberOfRawSamplesToRead = numberOfRawSamplesSelected; }

                // Get IQ display data type from GUI.
                var iqDisplayDataType = (IQDisplayDataType)Enum.Parse(typeof(IQDisplayDataType), _guiIQDisplayDataType.Text);

                // Get IQ display unit from GUI, if IQ data type is I/Q, always use amplitude in V.
                var iqDisplayUnit = (IQDisplayUnitType)Enum.Parse(typeof(IQDisplayUnitType), _guiIQDisplayUnit.Text);
                if (iqDisplayDataType == IQDisplayDataType.IQWaveform) { iqDisplayUnit = IQDisplayUnitType.V; }

                // Read raw data from file.
                short[] rawData = new short[numberOfRawSamplesToRead * arrayElementsPerSample];
                _vectorFile.Seek(_currentSelectionStart, SeekOrigin.Begin);
                _vectorFile.Read(rawData);

                // Convert raw data to complex array with engineering unit (V)
                double real, imaginary, scale;
                Complex[,] complexData = new Complex[numberOfChannels, numberOfRawSamplesToRead];
                for (i = 0; i < numberOfChannels; i++)
                {
                    scale = scaleFactor[i];
                    for (j = 0; j < numberOfRawSamplesToRead; j++)
                    {
                        real = rawData[j * arrayElementsPerSample + i * 2] * scale;
                        imaginary = rawData[j * arrayElementsPerSample + i * 2 + 1] * scale;
                        complexData[i, j] = new Complex(real, imaginary);
                    }
                }

                // Get I/Q or amplitude from Complex array for display.
                double[,] waveformForDisplay = null;
                switch (iqDisplayDataType)
                {
                    case IQDisplayDataType.IQWaveform:
                        {
                            waveformForDisplay = new double[numberOfChannels * 2, numberOfRawSamplesToRead];
                            for (i = 0; i < numberOfChannels; i++)
                            {
                                for (j = 0; j < numberOfRawSamplesToRead; j++)
                                {
                                    waveformForDisplay[i * 2, j] = complexData[i, j].Real;
                                    waveformForDisplay[i * 2 + 1, j] = complexData[i, j].Imaginary;
                                }
                            }
                            break;
                        }
                    case IQDisplayDataType.Amplitude:
                        {
                            waveformForDisplay = new double[numberOfChannels, numberOfRawSamplesToRead];
                            for (i = 0; i < numberOfChannels; i++)
                            {
                                for (j = 0; j < numberOfRawSamplesToRead; j++)
                                {
                                    waveformForDisplay[i, j] = complexData[i, j].Magnitude;
                                    // If value is 0, Math.Log10() will return "-Infinity" which will result in EasyChart.Plot() error, so use quuntum 1 in this case.
                                    if (iqDisplayUnit == IQDisplayUnitType.dBm && waveformForDisplay[i, j] == 0) { waveformForDisplay[i, j] = scaleFactor[i]; }
                                }
                            }
                            break;
                        }
                }

                // IQ waveform unit conversion.
                for (i = 0; i < waveformForDisplay.GetLength(0); i++)
                {
                    for (j = 0; j < waveformForDisplay.GetLength(1); j++)
                    {
                        switch (iqDisplayUnit)
                        {
                            case IQDisplayUnitType.V: { break; }
                            case IQDisplayUnitType.W:
                                {
                                    waveformForDisplay[i, j] = Math.Pow(waveformForDisplay[i, j], 2) / UnitConversionImpedance;
                                    break;
                                }
                            case IQDisplayUnitType.dBm:
                                {
                                    waveformForDisplay[i, j] = 10 * Math.Log10(Math.Pow(waveformForDisplay[i, j], 2) / UnitConversionImpedance) + 30;
                                    break;
                                }
                        }
                    }
                }

                // Plot I/Q or amplitude/Power.
                _guiIQChart.Plot(waveformForDisplay);

                // New array for spectrum.
                Complex[] complexDataOf1Channel = new Complex[numberOfRawSamplesToRead];
                double[] spectrumDataOf1Channel = new double[NumberOfSpectralLines];
                double[,] spectrumData = new double[numberOfChannels, NumberOfSpectralLines];

                // Get spectrum settings from GUI.
                _spectrumTask.Average.Mode = (SpectrumAverageMode)Enum.Parse(typeof(SpectrumAverageMode), _guiSpectrumAvgMode.Text);
                _spectrumTask.Average.Size = (double)_guiSpectrumAvgSize.Value;

                // Compute spectrum and display.
                double powerInBand;
                for (i = 0; i < numberOfChannels; i++)
                {
                    for (j = 0; j < numberOfRawSamplesToRead; j++) { complexDataOf1Channel[j] = complexData[i, j]; }
                    AutoPowerSpectrum(complexDataOf1Channel, _dataSampleRate, ref spectrumDataOf1Channel, out powerInBand);
                    for (j = 0; j < NumberOfSpectralLines; j++) { spectrumData[i, j] = spectrumDataOf1Channel[j]; }
                }

                // Plot spectrum.
                _guiSpectrumChart.Plot(spectrumData, _spectrumTask.SpectralInfomation.FreqStart, _spectrumTask.SpectralInfomation.FreqDelta);
            }
            finally { _vectorFile.Close(); }

        }

        private void AutoPowerSpectrum(Complex[] data, double sampleRate, ref double[] spectrum, out double powerInBand)
        {
            // Set spectrum parameters according to input.
            _spectrumTask.InputDataType = InputDataType.Complex;
            _spectrumTask.SampleRate = sampleRate;
            _spectrumTask.Output.NumberOfLines = spectrum.Length;

            // Calculate actual average size, which should between [1, DefaultAverageSize].
            int actualAverageSize, processBlockSize;
            if (data.Length >= _spectrumTask.SpectralInfomation.FFTSize)
            {
                actualAverageSize = data.Length / _spectrumTask.SpectralInfomation.FFTSize;
                processBlockSize = _spectrumTask.SpectralInfomation.FFTSize;
            }
            else
            {
                actualAverageSize = 1;
                processBlockSize = data.Length;
            }
            actualAverageSize = Math.Min(actualAverageSize, (int)_spectrumTask.Average.Size);

            // New array to keep data for 1 FFT.
            Complex[] blockData = new Complex[_spectrumTask.SpectralInfomation.FFTSize];

            // Compute power spectrum.
            _spectrumTask.Reset();
            for (int i = 0; i < actualAverageSize; i++)
            {
                Array.Copy(data, i * processBlockSize, blockData, 0, processBlockSize);
                _spectrumTask.GetSpectrum(blockData, ref spectrum);
            }

            // Measure power in band.
            powerInBand = _spectrumTask.MeasurePowerInBand(spectrum, 0, sampleRate / 2);
        }

        /// <summary>
        /// Convert duration in seconds to OADate format, which is floating value in "days", relative to 1900-00-00, 00:00:00.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private double SecondsToOADate(double seconds) { return seconds / SecondsPerDay; }

        /// <summary>
        /// Convert OADate value to duration in seconds , relative to 1900-00-00, 00:00:00.
        /// </summary>
        /// <param name="oaDate"></param>
        /// <returns></returns>
        private double OADateToSeconds(double oaDate) { return oaDate * SecondsPerDay; }

        #endregion

        #region------------------Private Methods----------------------------

        /// <summary>
        /// IQ显示格式
        /// </summary>
        public enum IQDisplayDataType
        {
            /// <summary>
            /// I and Q waveform. 
            /// </summary>
            IQWaveform,

            /// <summary>
            /// Amplitude or power. 
            /// </summary>
            Amplitude,
        }

        /// <summary>
        /// IQ显示幅度/功率单位
        /// </summary>
        public enum IQDisplayUnitType
        {
            /// <summary>
            /// Amplitude in V. 
            /// </summary>
            V,

            /// <summary>
            /// Power in W. 
            /// </summary>
            W,

            /// <summary>
            /// Power in dBmW. 
            /// </summary>
            dBm,
        }

        #endregion

    }

}