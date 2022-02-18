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
        /// 方阵求逆
        /// </summary>
        public void Inverse()
        {
            Matrix<T> output =  Inverse(this);
            Vector.ArrayCopy(output._dataRef, this._dataRef);
        }

        /// <summary>
        /// 方阵求逆
        /// </summary>
        public static Matrix<T> Inverse(Matrix<T> input)
        {
            Matrix<T> output = Matrix<T>.Identity(input.Colum) ;
            SolveLinearEquations(input, output);
            return output;
        }
    }
}
