using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JXI.SignalProcessing.Generation
{
    /// <summary>
    /// 噪声生成器
    /// </summary>
    public static class NoiseGenerator  //2022-02-01 来自VST\Digital Signal Processing\Math Utility
    {
        /// <summary>
        /// 产设种子，seed =-1 随机生成种子；seed != -1 返回seed
        /// </summary>
        /// <param name="seed">种子</param>
        /// <returns></returns>
        public static int GetRandomSeed(int seed = -1)
        {
            if (seed != -1)
                return seed;

            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            rng.Dispose();
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// 用box muller的方法产生高斯噪声
        /// https://en.wikipedia.org/wiki/Box-Muller_transform
        /// </summary>
        /// <param name="gaussNoise">输出噪声数组</param>
        /// <param name="sigma">方差</param>
        /// <param name="mean">均值</param>
        /// <param name="seed">种子</param>
        public static void GaussNiose(double[] gaussNoise, double sigma = 1, double mean = 0, int seed = -1)
        {
            if (gaussNoise == null)
                return;
            Random ran = new Random(GetRandomSeed(seed));
            for (int i = 0; i < gaussNoise.Length; i++)
            {
                double r1 = ran.NextDouble();
                double r2 = ran.NextDouble();
                gaussNoise[i] = Math.Sqrt((-2) * Math.Log(r2)) * Math.Sin(2 * Math.PI * r1);
                gaussNoise[i] = gaussNoise[i] * sigma + mean;
            }
        }
        /// <summary>
        /// 产生白噪声
        /// </summary>
        /// <param name="whiteNoise">输出噪声数组</param>
        /// <param name="amplitude">幅度</param>
        /// <param name="mean">均值</param>
        /// <param name="seed">种子</param>
        public static void WhiteNoise(double[] whiteNoise, double amplitude = 1, double mean = 0, int seed = -1)
        {
            if (whiteNoise == null)
                return;
            Random ran = new Random(GetRandomSeed(seed));
            for (int i = 0; i < whiteNoise.Length; i++)
            {
                whiteNoise[i] = ran.NextDouble() * 2 - 1; // 输出为正负1 之间
                whiteNoise[i] = whiteNoise[i] * amplitude + mean;
            }
        }
    }

}
