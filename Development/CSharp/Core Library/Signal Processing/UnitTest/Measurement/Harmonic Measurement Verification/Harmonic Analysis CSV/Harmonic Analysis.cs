using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JY.File;
using SeeSharpTools.JXI.DSP.Measurement;

namespace Harmonic_Analysis_CSV
{
    public partial class FormHarmonicAnalysisTest : Form
    {
        public FormHarmonicAnalysisTest()
        {
            InitializeComponent();
        }

        private void buttonAnalysis_Click(object sender, EventArgs e)
        {
            //load waveform
            double[,] rawData = CsvHandler.Read<double>(textBoxCSVFilePath.Text);
            int size = rawData.GetLength(0);
            double[] signal = new double[size];
            double dt = 1 / (double)numericSampleRate.Value;
            for (int i = 0; i < size; i++)
            {
                signal[i] = rawData[i, 0];
            }
            easyChartXWaveform.Plot(signal, 0, dt);

            //SINAD analysis
            double fundamentalFreq;
            double THD;
            double[] componentsLevel = new double[0];
            HarmonicAnalysis.THDAnalysis(signal, dt, out fundamentalFreq, out THD, ref componentsLevel);
            textBoxFundamentalFreq.Text = fundamentalFreq.ToString("N3");
            textBoxTHD.Text = THD.ToString("N6");
            string harmonicsText = "";
            for (int i = 0; i < componentsLevel.Length; i++)
            {
                harmonicsText += componentsLevel[i].ToString("N6") + "    ";
            }
            textBoxHarmonics.Text = harmonicsText;

            double SINAD;
            HarmonicAnalysis.SINADAnalysis(signal, dt, out fundamentalFreq, out SINAD, ref componentsLevel);
            double SINAD_dB = 10 * Math.Log10(SINAD);
            textBoxSINAD.Text = SINAD_dB.ToString("N6");
            textBoxTHDPlusNoise.Text = (1 / SINAD).ToString("N6");
        }
    }
}
