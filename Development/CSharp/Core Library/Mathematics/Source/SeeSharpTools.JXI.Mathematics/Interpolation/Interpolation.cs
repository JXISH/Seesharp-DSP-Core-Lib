using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JXI.Mathematics.Interpolation
{
    /// <summary>
    /// 内插方法，包括线性
    /// 可扩展多项式内插
    /// </summary>
    public class Interpolation
    {
        /// <summary>
        /// 计算在点[xStart, yStart]与点[xEnd, yEnd]所连成的直线上给定的x所对应的y值。
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="xEnd"></param>
        /// <param name="yEnd"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double LinearInterpolate(double xStart, double yStart, double xEnd, double yEnd, double x)
        {
            // 如果输入的x与xStart或xEnd相同，则直接返回对应的yStart/yEnd。
            if (x == xStart) { return yStart; }
            if (x == xEnd) { return yEnd; }

            // 如果xStart与xEnd相同，则抛出异常。
            if (xStart == xEnd) { throw new ArgumentException("xStart and xEnd are the same."); }

            // 如果yStart与yEnd相同，则直接返回yStart/yEnd。
            if (yStart == yEnd) { return yStart; }

            // 并非上述边界条件，则插值计算x对应的y。
            double ratio = (x - xStart) / (xEnd - xStart);
            return yStart + ratio * (yEnd - yStart);
        }
    }
}
