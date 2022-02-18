using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JY.DSP.Fundamental;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JY.File;

namespace Harmonic_Measurement_Verification
{
    public partial class TestWaveformGeneration : Form
    {
        double[] signal;

        public TestWaveformGeneration()
        {
            InitializeComponent();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            int n = (int) numericSampleLength.Value;
            double sampleRate = (double)numericSampleRate.Value;
            signal = new double[n];
            double[] noise = new double[n];
            if(checkBoxSine.Checked)
            {
                Generation.SineWave(ref signal, 1, 0, (double)numericSignalFrequency.Value, sampleRate);
            }
            else
            {
                Generation.SquareWave(ref signal, 1, 50, (double)numericSignalFrequency.Value, sampleRate);
            }
            Generation.UniformWhiteNoise(ref noise, (double)numericNoiseLevel.Value);
            ArrayCalculation.Add(signal, noise, ref signal);
            easyChartX1.Plot(signal, 0, 1 / sampleRate);
        }

        private void buttonSaveCSV_Click(object sender, EventArgs e)
        {
            CsvHandler.WriteData(signal);
        }
    }
}
