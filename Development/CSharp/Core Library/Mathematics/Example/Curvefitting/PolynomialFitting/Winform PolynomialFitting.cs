/// overciew:polynomial fitting
/// 内容摘要: 多项式拟合
/// Date of completion: august 4th 2017
/// version1
/// author: xie xiao jiao, JXI

using SeeSharpTools.JXI.Mathematics;
using System;
using System.Windows.Forms;

namespace SeeSharpTools.JX.DSP.Math_PolynomialFittingForm
{
    public partial class PolynomialFittingForm : Form
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
        public PolynomialFittingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// generate array X and array Y
        /// 生成X数组以及Y数组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            double a0 = (double)numericUpDown_a0.Value;
            double a1 = (double)numericUpDown_a1.Value;
            double a2 = (double)numericUpDown_a2.Value;
            double a3 = (double)numericUpDown_a3.Value;

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

            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < (int)numericUpDownDataLength.Value; i++)
            {
                Noise[i] = rand.Next((int)(-numericUpDown_noise.Value * 100), (int)(numericUpDown_noise.Value * 100)) / 100.0;
                dataXOri[i] = i + 1;
                dataYOri[i] = a0 + a1 * dataXOri[i] + a2 * dataXOri[i] * dataXOri[i] + a3 * dataXOri[i] * dataXOri[i] * dataXOri[i] + Noise[i];
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
            string str =  "fitting formula: y=";
            int order = 3;
            int i = 0;
            double[] data = new double[dataXOri.Length];
            bool zeroFlag;
            double[] coefficient=new double [order+1];
            EasyCurveFitting.PolynomialFit(dataXOri, dataYOri, order, dataXOri, ref data, ref coefficient);
            if (data == null)
            {
                /* MessageBox.Show("参数设置错误，请重新设置！\n可能原因：\n"
                   + "1、未输入拟合参数\n2、多项式拟合阶数设置有误", "提示", MessageBoxButtons.OK); */
                MessageBox.Show("Parameter setting error, please reset!\n Possible cause: \n"
                     +"1, no fitting parameter \n2, polynomial fitting order setting error", "prompt", MessageBoxButtons.OK);
                return;
            }
            order += 1;
            for (int k = 0; k < data.Length; k++)
            {
                chart1.Series[1].Points.AddXY(dataXOri[k], data[k]);
            }

            zeroFlag = true;
            do
            {
                if (System.Math.Abs(coefficient[i]) < 0.00001)
                {
                    i++;
                    order--;
                    continue;
                }
                else
                {
                    zeroFlag = false;
                }

                if (i == 0)
                {
                    str += coefficient[i].ToString("f3");
                }
                else if (coefficient[i] < 0)
                {
                    if (i == 1)
                    {
                        str += coefficient[i].ToString("f3") + "x";
                    }
                    else
                    {
                        str += coefficient[i].ToString("f3") + "x^" + i.ToString();
                    }

                }
                else if (coefficient[i] > 0)
                {
                    if (i == 1)
                    {
                        str += "+" + coefficient[i].ToString("f3") + "x";
                    }
                    else
                    {
                        str += "+" + coefficient[i].ToString("f3") + "x^" + i.ToString();
                    }
                }
                i++;
                order--;

            } while (order != 0);

            if (zeroFlag)
            {
                str += "0";
            }
            Result.Text = str;
        }
        #endregion

        #region Methods
        #endregion
    }
}
