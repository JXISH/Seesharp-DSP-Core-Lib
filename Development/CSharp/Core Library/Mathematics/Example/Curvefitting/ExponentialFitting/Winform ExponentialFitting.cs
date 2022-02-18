/// overview:exponential fitting
/// 内容摘要: 指数拟合
/// Date of completion: august 4th 2017
/// version1
/// author: xie xiao jiao, JXI
/// 
using System;
using System.Windows.Forms;
using SeeSharpTools.JXI.Mathematics;

namespace SeeSharpTools.JX.DSP.Math_ExponentialFittingExample
{
    public partial class ExponentialFittingForm : Form
    {
        #region Private Fields
        /// <summary>
        /// input array X
        /// 输入数组X
        /// </summary>
        double[] dataXOri;

        /// <summary>
        /// input array Y
        ///  输入数组Y
        /// </summary>
        double[] dataYOri;

        /// <summary>
        /// array Y after exponential fitting
        /// 拟合后数组Y
        /// </summary>
        double[] dataYFit;
        #endregion

        #region Constructor
        public ExponentialFittingForm()
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
            dataXOri = new double[(int)numericUpDownDataLength.Value];
            dataYOri = new double[(int)numericUpDownDataLength.Value];
            dataYFit = new double[(int)numericUpDownDataLength.Value];

            while (chart1.ChartAreas[0].AxisX.ScaleView.IsZoomed)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
            }
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            Result.Visible = false;
            btnFit.Enabled = true;

            double[] Noise = new double[dataXOri.Length];
            Random noise = new Random(DateTime.Now.Millisecond);
            //Generate an array to be fitted based on parameters
            //根据参数生成待拟合数组
            for (int i = 0; i < dataXOri.Length; i++)
            {            
                Noise[i] = noise.Next((int)(-numericUpDown_noise.Value * 100), (int)(numericUpDown_noise.Value * 100)) / 100.0;
                dataXOri[i] = i + 1;
                dataYOri[i] = (double)numericUpDown_a.Value * System.Math.Exp((double)(numericUpDown_b.Value) * dataXOri[i])+Noise[i];
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


            double amplitude;
            double damping;
            double offset;
            EasyCurveFitting.ExponentialFit(dataXOri, dataYOri, dataXOri, ref dataYFit, out amplitude, out damping, out offset);
            if (dataYFit == null)
            {
                //MessageBox.Show("参数设置错误，请重新设置！\n可能原因:未输入拟合参数", "提示", MessageBoxButtons.OK);
                MessageBox.Show(" Parameters error, pls set again!\nPossible reason: fitting parameters not input","Note",MessageBoxButtons.OK);
                return;
            }
            for (int i = 0; i < dataYFit.Length; i++)
            {
                chart1.Series[1].Points.AddXY(dataXOri[i], dataYFit[i]);
            }

            if (System.Math.Abs(amplitude) < 0.00001 && System.Math.Abs(damping) < 0.00001)
            {
                //Result.Text = "拟合公式:y = 0";
                Result.Text = "fitting formula: y=0";
            }
            else if (System.Math.Abs(damping) < 0.00001)
            {
                //Result.Text = "拟合公式:y = " + amplitude.ToString("f3");
                Result.Text = "fitting formula: y="+ amplitude.ToString("f3");
            }
            else
            {
                //Result.Text = "拟合公式:y = " + amplitude.ToString("f3") + "exp" + "(" + damping.ToString("f3") + "x)";
                Result.Text = "fitting formula: y="+ amplitude.ToString("f3")+ "exp" + "(" + damping.ToString("f3") + "x)";
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
