using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpTools.JXI.Numerics
{
    /// <summary>
    /// 数值常数
    /// 2022-02-01 来自VST\Digital Signal Processing\Math Utility
    /// </summary>
    internal class Const
    {
        /// <summary>
        /// 相位因子，将0-2pi 投影到I32范围中，满足相位自动回绕
        /// </summary>
        public static readonly double I32PhaseScale = Math.PI / Math.Pow(2, 31);
        /// <summary>
        /// 相位因子，将0-2pi 投影到 -32768 -- +32767 范围中，满足相位自动回绕
        /// </summary>
        public static readonly double I16PhaseScale = Math.PI / Math.Pow(2, 15);
    }
}
