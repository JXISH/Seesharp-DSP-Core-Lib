using SeeSharpTools.JXI.SignalProcessing.SpectrumAnalysis.RFSASpectrum;
using SeeSharpTools.JXI.SignalProcessing.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpTools.JXI.SignalProcessing.JTFA
{
    internal class Spectrogram
    {
        /// <summary>
        /// 短时傅里叶变换
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="bins"></param>
        /// <param name="stepLength"></param>
        /// <param name="spectrogram"></param>
        void STFT(Complex[] iq, int bins, int stepLength, FFTWindowType window, out Complex[][] spectrogram)
        {
            int numOfSteps = (iq.Length - bins) / stepLength + 1;

            spectrogram = new Complex[numOfSteps][];
            for (int i = 0; i < numOfSteps; i++)
            {
                //临时将短时时域波形放到输出频谱
                int i0 = i * stepLength;
                int rotateL = i0 % bins; //步进引起相位偏移需要移位补偿
                spectrogram[i] = new Complex[bins];
                if (rotateL == 0)
                {
                    Array.Copy(iq, i0, spectrogram[i], 0, bins);
                }
                else
                {
                    Array.Copy(iq, i0 + bins - rotateL, spectrogram[i], 0, rotateL);
                    Array.Copy(iq, i0, spectrogram[i], rotateL, bins - rotateL);
                }
                FFTWindow.ProcessWindow(spectrogram[i], window);
                //原位输出频谱计算不用加窗
                DFT.ComputeForwardShifted(spectrogram[i]);
            }
        }
    }
}
