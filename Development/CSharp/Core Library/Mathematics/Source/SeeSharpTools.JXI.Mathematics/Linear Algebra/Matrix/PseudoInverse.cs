using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Numerics;

using SeeSharpTools.JXI.Numerics;

namespace SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix
{
    public partial class Matrix<T>
    {
        /// <summary>
        /// 方阵伪逆
        /// </summary>
        public void PseudoInverse()
        {
            Matrix<T> output = PseudoInverse(this);
            Vector.ArrayCopy(output._dataRef, this._dataRef);
        }

        /// <summary>
        /// 方阵伪逆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">伪逆运算的目标矩阵</param>
        /// <returns>伪逆矩阵</returns>
        public static Matrix<T> PseudoInverse(Matrix<T> input)
        {
            Matrix<T> identity = Matrix<T>.Identity(input.Row);
            Matrix<T> output = new Matrix<T>(input.Colum, input.Row);
            SolveLinearEquationsLMS(input, identity, output);
            return output;
        }
    }
}
