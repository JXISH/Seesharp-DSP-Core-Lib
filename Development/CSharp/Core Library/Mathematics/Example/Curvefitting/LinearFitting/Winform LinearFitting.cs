/// overview:Linear fitting
/// 内容摘要: 线性拟合
/// Date of completion: August 4th 2017
/// 完成日期: 2017年8月4日
///  version1
/// author: xie xiao jiao, JXI

using System;
using System.Windows.Forms;
using SeeSharpTools.JXI.Mathematics;

namespace SeeSharpTools.JX.DSP.Math_LinearFittingExample
{
    public partial class LinearFittingFrom : Form
    {
        #region Private Fields
        /// <summary>
        /// input array X
        /// 输入数组X
        /// </summary>
        double[] dataXOri;

        /// <summary>
        /// input array Y
        /// 输入数组Y
        /// </summary>
        double[] dataYOri;
        #endregion

        #region Constructor
        public LinearFittingFrom()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// generate array X and arrray Y
        /// 生成X数组以及Y数组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            dataXOri = new double[(int)numericUpDownDataLength.Value];
            dataYOri = new double[(int)numericUpDownDataLength.Value];
            double[] Noise = new double[(int)numericUpDownDataLength.Value];

            while (chart1.ChartAreas[0].AxisX.ScaleView.IsZoomed)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
            }
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            Result.Visible = false;
            btnFit.Enabled = true;

            double a = (double)numericUpDownA.Value;
            double b = (double)numericUpDownB.Value;

            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < dataXOri.Length; i++)
            {
                Noise[i] = rand.Next((int)(-numericUpDownNoise.Value * 100), (int)(numericUpDownNoise.Value * 100)) / 100.0;
                dataXOri[i] = i + 1;
                dataYOri[i] = a * dataXOri[i] + b + Noise[i];
                chart1.Series[0].Points.AddXY(dataXOri[i], dataYOri[i]);
            }
        }

        /// <summary>
        /// fit
        /// 拟合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFit_Click(object sender, EventArgs e)
        {
            chart1.Series[1].Points.Clear();
            Result.Visible = true;
            btnFit.Enabled = false;

            double[] data=new double[dataXOri.Length];
            double slop;
            double inte;
            EasyCurveFitting.LinearFit(dataXOri, dataYOri, dataXOri, ref data, out slop, out inte);
            if (data == null)
            {
                //MessageBox.Show("参数设置错误，请重新设置！\n可能原因：未输入拟合参数", "提示", MessageBoxButtons.OK);
                MessageBox.Show(" Parameters error, pls set again!\nPossible reason: fitting parameters not input","Note",MessageBoxButtons.OK);
                return;
            }
            for (int i = 0; i < dataXOri.Length; i++)
            {
                chart1.Series[1].Points.AddXY(dataXOri[i], data[i]);
            }

            bool zeroFlag = true;
            //Result.Text = "拟合公式:y = ";
            Result.Text = "fitting formula: y=";
            if (System.Math.Abs(slop) > 0.00001)
            {
                zeroFlag = false;
                Result.Text += slop.ToString("f3") + "x";
            }
            if (System.Math.Abs(inte) > 0.00001)
            {
                zeroFlag = false;
                if (inte < 0)
                {
                    Result.Text += inte.ToString("f3");
                }
                else
                {
                    Result.Text += "+" + inte.ToString("f3");
                }
            }

            if (zeroFlag)
            {
                Result.Text += "0";
            }
        }
        #endregion;

        #region Methods
        #endregion
    }
}

