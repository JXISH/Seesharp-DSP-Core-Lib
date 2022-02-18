using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using SeeSharpTools.JXI.FileIO.IQFile;

namespace IQ_File_Read_Example
{
    public partial class IQFileReadExample : Form
    {
        public IQFileReadExample()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 打开对话框，选择wav、iq或bin文件
        /// 这些文件必须符合IQ File Library格式要求
        /// 可以选择范例文件试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRead_Click(object sender, EventArgs e)
        {
            try
            {
                // Show dialog for user to select IQ file.
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.RestoreDirectory = false;
                fileDialog.Filter = "Vector Files (*.iq)|*.iq|Bin Files(with Json) (*.bin)|*.bin|Wav Files (*.wav)|*.wav";
                fileDialog.Title = "Select IQ file to open";
                if (fileDialog.ShowDialog() == DialogResult.Cancel) { return; }

                Complex[] iqData;
                IQFileInfo iqInfo= new IQFileInfo();

                iqData = IQFile.ReadIQFile(fileDialog.FileName, ref iqInfo);

                //显示不超过4000个样本的IQ波形
                int displayLength = Math.Min(iqData.Length, 4000);
                double[,] displayData = new double[2, displayLength];
                for (int i = 0; i < displayLength; i++)
                {
                    displayData[0, i] = iqData[i].Real;
                    displayData[1, i] = iqData[i].Imaginary;
                }
                easyChartXIQPlot.Plot(displayData);


                textBoxFileInfo.Text = fileDialog.FileName + Environment.NewLine
                    + "Center Frequency: " + iqInfo.Signal.CenterFrequency.ToString("G3")
                    + "  Sample Rate: " + iqInfo.Signal.SampleRate.ToString("N2") + Environment.NewLine
                    + "Samples Read: " + iqData.Length.ToString("N0")
                    + "  Samples Plotted: " + displayLength.ToString("N0");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
