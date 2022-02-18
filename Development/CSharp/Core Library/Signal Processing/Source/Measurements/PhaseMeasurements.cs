using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
//using MathNet.Numerics.IntegralTransforms;

namespace SeeSharpTools.JXI.SignalProcessing.Measurement
{
    /// <summary>
    /// <para>PhaseMeasurements Class</para>
    /// <para>Chinese Simplified：相位测量类</para>
    /// </summary>
    public static class PhaseMeasurements
    {
        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// 构造函数 调License Manager
        /// </summary>
        static PhaseMeasurements()
        {
            //利用反射获取版本号
            //Assembly fileAssembly = Assembly.GetExecutingAssembly();
            //var fileVersion = fileAssembly.GetName().Version.ToString();//获取版本号
            //var firstPointIndex = fileVersion.IndexOf('.');
            //var secondPointIndex = fileVersion.Substring(firstPointIndex + 1, fileVersion.Length - firstPointIndex - 1).IndexOf('.');
            //string strProduct = "SeeSharpTools.JXI.DSP";
            //string strVersion = fileVersion.Substring(0, firstPointIndex + 1 + secondPointIndex);//获取大版本号1.0
            //                                                                                     //LicenseManager.LicenseManager.CheckActivationStatus(strProduct, strVersion);
            //var status = LicenseManager.LicenseManager.GetActivationStatus(strProduct, strVersion);
            //if (status < 0)
            //{
            //    LicenseManager.ProductLicenseManager.GetActivationStatus(strProduct, strVersion);
            //}
            //LicenseManager.ProductLicenseManager.GetSeeSharpDeviceStatus();
        }
        #endregion

        #region---------------------------Public Methods----------------------------------------
        /// <summary>
        /// <para>Calculate the phase shift between two input waveform arrays, return value will be between -180° and 180°.</para>
        /// <para>Chinese Simplified：计算输入信号之间的相位差，返回值在-180°到180之间</para>
        /// </summary>
        /// <param name="inputWaveform1">
        /// <para>waveform array1 </para>
        /// <para> Chinese Simplified：输入信号1</para>
        /// </param>
        /// <param name="inputWaveform2">
        /// <para>waveform array1 </para>
        /// <para> Chinese Simplified：输入信号2</para>
        /// </param>
        /// <returns></returns>
        public static double CalPhaseShift(double[] inputWaveform1, double[] inputWaveform2)
        {
            var inputWaveform1Hilbert = MathDotNetHilbert(inputWaveform1);
            var inputWaveform2Hilbert = MathDotNetHilbert(inputWaveform2);
            int dataLength = inputWaveform1.Length > inputWaveform2.Length ? inputWaveform2.Length : inputWaveform1.Length;
            double phaseShiftSum = 0;
            for (int i = 0; i < dataLength; i++)
            {
                phaseShiftSum += (inputWaveform1Hilbert[i] / inputWaveform2Hilbert[i]).Phase;
            }
            return phaseShiftSum * 180 / (Math.PI * dataLength);
        }

        /// <summary>
        /// <para>Calculate the phase shift between two input waveform arrays, return value will be between -180° and 180°.</para>
        /// <para>Chinese Simplified：计算输入信号之间的相位差，返回值在-180°到180之间</para>
        /// </summary>
        /// <param name="inputWaveform1">
        /// <para>waveform array1 </para>
        /// <para> Chinese Simplified：输入信号1</para>
        /// </param>
        /// <param name="inputWaveform2">
        /// <para>waveform array1 </para>
        /// <para> Chinese Simplified：输入信号2</para>
        /// </param>
        /// <returns>phase shift in unit of degree</returns>
        public static double CalPhaseShift(float[] inputWaveform1, float[] inputWaveform2)
        {
            var inputWaveform1Hilbert = MathDotNetHilbert(inputWaveform1);
            var inputWaveform2Hilbert = MathDotNetHilbert(inputWaveform2);
            int dataLength = inputWaveform1.Length > inputWaveform2.Length ? inputWaveform2.Length : inputWaveform1.Length;
            double phaseShiftSum = 0;
            for (int i = 0; i < dataLength; i++)
            {
                phaseShiftSum += (inputWaveform1Hilbert[i] / inputWaveform2Hilbert[i]).Phase;
            }
            return phaseShiftSum * 180 / (Math.PI * dataLength);
        }

        #endregion

        #region ---------------------------Private Methods----------------------------------------
        /// <summary>
        /// Hilbert transform by Math.Net
        /// </summary>
        /// <param name="xreal"></param>
        /// <returns></returns>
        private static Complex[] MathDotNetHilbert(double[] xreal)
        {
            var x = (from sample in xreal select new Complex(sample, 0)).ToArray();
            //Fourier.Forward(x, FourierOptions.Default);
            //var h = new double[x.Length];
            //var fftLengthIsOdd = (x.Length | 1) == 1;
            //if (fftLengthIsOdd)
            //{
            //    h[0] = 1;
            //    for (var i = 1; i < xreal.Length / 2; i++) h[i] = 2;
            //}
            //else
            //{
            //    h[0] = 1;
            //    h[(xreal.Length / 2)] = 1;
            //    for (var i = 1; i < xreal.Length / 2; i++) h[i] = 2;
            //}
            //for (var i = 0; i < x.Length; i++) x[i] *= h[i];
            //Fourier.Inverse(x, FourierOptions.Default);
            return x;
        }

        /// <summary>
        /// Hilbert transform by Math.Net
        /// </summary>
        /// <param name="xreal"></param>
        /// <returns></returns>
        private static Complex[] MathDotNetHilbert(float[] xreal)
        {
            var x = (from sample in xreal select new Complex(sample, 0)).ToArray();
            //Fourier.Forward(x, FourierOptions.Default);
            //var h = new double[x.Length];
            //var fftLengthIsOdd = (x.Length | 1) == 1;
            //if (fftLengthIsOdd)
            //{
            //    h[0] = 1;
            //    for (var i = 1; i < xreal.Length / 2; i++) h[i] = 2;
            //}
            //else
            //{
            //    h[0] = 1;
            //    h[(xreal.Length / 2)] = 1;
            //    for (var i = 1; i < xreal.Length / 2; i++) h[i] = 2;
            //}
            //for (var i = 0; i < x.Length; i++) x[i] *= h[i];
            //Fourier.Inverse(x, FourierOptions.Default);
            return x;
        }
        #endregion
    }
}
