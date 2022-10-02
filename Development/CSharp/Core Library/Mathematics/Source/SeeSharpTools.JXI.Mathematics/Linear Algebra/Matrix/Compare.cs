using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Numerics;
using SeeSharpTools.JXI.Mathematics;
using SeeSharpTools.JXI.Numerics;

namespace SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix
{
    public partial class Matrix<T> : IDisposable
    {
        public static bool Equal(Matrix<T> A, Matrix<T> B, double resolution = 1.0e-9, bool absolution = false)
        {
            if (A.Row != B.Row || A.Colum != B.Colum) { return false; }

            T[] arrayA = new T[A.RefSize];
            T[] arrayB = new T[B.RefSize];
            Vector.ArrayCopy(A._dataAddress, arrayA);
            Vector.ArrayCopy(B._dataAddress, arrayB);

            return Equal(arrayA, arrayB, resolution, absolution);
        }

        public static bool Equal(T[] A, T[] B, double resolution = 1.0e-9, bool absolution = false)
        {
            if (A.Length != B.Length) { return false; }

            T[] differ = Vector.GetArraySub(A, B);
            if (absolution)
            {

            }
            else
            {
                Vector.ArrayDivision(differ, A);
            }
            Vector.ArrayAbs(differ);


            if (differ is float[] differ_f32)
            {
                float max_f32 = Vector.ArrayFindMax(differ_f32);
                if (max_f32 > (float)resolution) return false;
            }
            else if (differ is double[] differ_f64)
            {
                double max_f64 = Vector.ArrayFindMax(differ_f64);
                if (max_f64 > resolution) return false;
            }
            else if (differ is Complex32[] differ_fc32)
            {
                float[] real_fc32 = Vector.GetComplexReal(differ_fc32);
                float max_fc32 = Vector.ArrayFindMax(real_fc32);
                if (max_fc32 > (float)resolution) return false;
            }
            else if (differ is Complex[] differ_fc64)
            {
                double[] real_fc64 = Vector.GetComplexReal(differ_fc64);
                double max_fc64 = Vector.ArrayFindMax(real_fc64);
                if (max_fc64 > resolution) return false;
            }
            else { throw new System.Exception("Data type not supported"); }

            return true;
        }
    }
}
