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
        /// 计算特征值
        /// </summary>
        public U[] Eigenvalues<U>()
        {
            U[] eigenvalues = new U[Row];
            Eigenvalues(this, eigenvalues, null, false);

            return eigenvalues;
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public U[] Eigenvalues<U>(out U[][] eigenvectors)
        {
            U[] eigenvalues = new U[Row];
            eigenvectors = new U[Row][];
            for (int i = 0; i < Row; i++)
            {
                eigenvectors[i] = new U[Colum];
            }
            Eigenvalues(this, eigenvalues, eigenvectors, true);
            return eigenvalues;
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public void Eigenvalues<U>(U[] eigenvalues, U[][] eigenvectors)
        {
            Eigenvalues(this, eigenvalues, eigenvectors, true);
        }

        /// <summary>
        /// 计算特征值
        /// </summary>
        public U[] EigenvaluesSymmetric<U>()
        {
            U[] eigenvalues = new U[Row];
            EigenvaluesSymmetric(this, eigenvalues, null, false);

            return eigenvalues;
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public U[] EigenvaluesSymmetric<U>(out T[][] eigenvectors)
        {
            U[] eigenvalues = new U[Row];
            eigenvectors = new T[Row][];
            for (int i = 0; i < Row; i++)
            {
                eigenvectors[i] = new T[Colum];
            }
            EigenvaluesSymmetric(this, eigenvalues, eigenvectors, true);
            return eigenvalues;
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public void EigenvaluesSymmetric<U>(U[] eigenvalues, T[][] eigenvectors)
        {
            EigenvaluesSymmetric(this, eigenvalues, eigenvectors, true);
        }

        #region 静态调用

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public static void Eigenvalues(Matrix<float> matix, Complex32[] eigenValues, Complex32[][] eigenVectors, bool eigenvectorsOut)
        {
            Eigenvalues<float, Complex32>(matix, eigenValues, eigenVectors, eigenvectorsOut);
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public static void Eigenvalues(Matrix<double> matix, Complex[] eigenValues, Complex[][] eigenVectors, bool eigenvectorsOut)
        {
            Eigenvalues<double, Complex>(matix, eigenValues, eigenVectors, eigenvectorsOut);
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public static void Eigenvalues(Matrix<Complex32> matix, Complex32[] eigenValues, Complex32[][] eigenVectors, bool eigenvectorsOut)
        {
            Eigenvalues<Complex32, Complex32>(matix, eigenValues, eigenVectors, eigenvectorsOut);
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public static void Eigenvalues(Matrix<Complex> matix, Complex[] eigenValues, Complex[][] eigenVectors, bool eigenvectorsOut)
        {
            Eigenvalues<Complex, Complex>(matix, eigenValues, eigenVectors, eigenvectorsOut);
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public static void EigenvaluesSymmetric(Matrix<float> matix, float[] eigenValues, float[][] eigenVectors, bool eigenvectorsOut)
        {
            EigenvaluesSymmetric<float, float>(matix, eigenValues, eigenVectors, eigenvectorsOut);
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public static void EigenvaluesSymmetric(Matrix<double> matix, double[] eigenValues, double[][] eigenVectors, bool eigenvectorsOut)
        {
            EigenvaluesSymmetric<double, double>(matix, eigenValues, eigenVectors, eigenvectorsOut);
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public static void EigenvaluesSymmetric(Matrix<Complex32> matix, float[] eigenValues, Complex32[][] eigenVectors, bool eigenvectorsOut)
        {
            EigenvaluesSymmetric<Complex32, float>(matix, eigenValues, eigenVectors, eigenvectorsOut);
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public static void EigenvaluesSymmetric(Matrix<Complex> matix, double[] eigenValues, Complex[][] eigenVectors, bool eigenvectorsOut)
        {
            EigenvaluesSymmetric<Complex, double>(matix, eigenValues, eigenVectors, eigenvectorsOut);
        }

        #endregion

        #region 私有函数 - 接口封装

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"> 复数 </typeparam>
        /// <param name="matix"></param>
        /// <param name="eigenValues"> 特征值, 数据类型为复数 </param>
        /// <param name="eigenVectors"> 特征向量, 数据类型为复数 </param>
        /// <param name="eigenvectorsOut"></param>
        private static void Eigenvalues<T, U>(Matrix<T> matix, U[] eigenValues, U[][] eigenVectors, bool eigenvectorsOut)
        {
            #region ---- 判断数据类型 ----

            T[] datatype = new T[1];
            U[] returntype = new U[1];

            if (datatype is float[])
            {
                if (!(returntype is Complex32[])) { throw new System.Exception("Return Data type is error."); }
            }
            else if (datatype is double[])
            {
                if (!(returntype is Complex[])) { throw new System.Exception("Return Data type is error."); }
            }
            else if (datatype is Complex32[])
            {
                if (!(returntype is Complex32[])) { throw new System.Exception("Return Data type is error."); }
            }
            else if (datatype is Complex[])
            {
                if (!(returntype is Complex[])) { throw new System.Exception("Return Data type is error."); }
            }
            else { throw new System.Exception("Data type not supported"); }

            #endregion

            #region ---- 判断是否为方阵 ----

            int N = matix.Row;

            if (!matix.IsValid) { throw new System.Exception("Matrix is not valid."); }

            if (!matix.IsSquare) { throw new System.Exception("Data size is not valid."); }

            if (eigenValues.Length != N || eigenVectors.Length != N) { throw new System.Exception("Data size is not valid."); }

            for (int i = 0; i < eigenVectors.Length; i++)
            {
                if (eigenVectors[i].Length != N) { throw (new System.Exception("Data size is not valid.")); }
            }

            #endregion

            #region ---- 准备 ----

            int size = ElementSize<U>();

            GCHandle eigenValues_GC = GCHandle.Alloc(eigenValues, GCHandleType.Pinned);
            IntPtr eigenValues_address = eigenValues_GC.AddrOfPinnedObject();

            U[,] eigenVectorsColum = eigenvectorsOut ? new U[N, N] : null;
            GCHandle eigenVectorsColum_GC = GCHandle.Alloc(eigenVectorsColum, GCHandleType.Pinned);
            IntPtr eigenVectorsColum_address = eigenVectorsColum_GC.AddrOfPinnedObject();

            #endregion

            #region ---- 计算 ----
            if (matix.IsSymmetric)
            {
                EigenvaluesSymmetricG<T>(matix._dataAddress, matix.Row, eigenValues_address, eigenVectorsColum_address);
            }
            else
            {
                EigenvaluesGeneral<T>(matix._dataAddress, matix.Row, eigenValues_address, eigenVectorsColum_address);
            }

            #endregion

            #region ---- 收尾 ----

            if (eigenvectorsOut)
            {
                for (int i = 0; i < N; i++)
                {
                    MatrixCaller.ArrayCopy<U>(eigenVectorsColum_address + i * size, eigenVectors[i], N);
                }
            }

            eigenValues_GC.Free();
            eigenVectorsColum_GC.Free();

            #endregion
        }

        /// <summary>
        /// 计算对称阵特征值和特征向量
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="U"> 实数 float/double </typeparam>
        /// <param name="matix"></param>
        /// <param name="eigenValues"> 特征值, 数据类型为实数 </param>
        /// <param name="eigenVectors"> 特征向量, 数据类型和矩阵数据类型相同 </param>
        /// <param name="eigenvectorsOut"></param>
        private static void EigenvaluesSymmetric<T, U>(Matrix<T> matix, U[] eigenValues, T[][] eigenVectors, bool eigenvectorsOut)
        {
            #region ---- 判断数据类型 ----

            T[] datatype = new T[1];
            U[] returntype = new U[1];

            if (datatype is float[])
            {
                if (!(returntype is float[])) { throw new System.Exception("Return Data type is error."); }
            }
            else if (datatype is double[])
            {
                if (!(returntype is double[])) { throw new System.Exception("Return Data type is error."); }
            }
            else if (datatype is Complex32[])
            {
                if (!(returntype is float[])) { throw new System.Exception("Return Data type is error."); }
            }
            else if (datatype is Complex[])
            {
                if (!(returntype is double[])) { throw new System.Exception("Return Data type is error."); }
            }
            else { throw new System.Exception("Data type not supported"); }

            #endregion

            #region ---- 判断是否为对称阵 ----

            int N = matix.Row;

            if (!matix.IsValid) { throw new System.Exception("Matrix is not valid."); }

            if (!matix.IsSquare || !matix.IsSymmetric) { throw new System.Exception("Data size is not valid."); }

            if (eigenValues.Length != N || eigenVectors.Length != N) { throw new System.Exception("Data size is not valid."); }

            for (int i = 0; i < eigenVectors.Length; i++)
            {
                if (eigenVectors[i].Length != N) { throw (new System.Exception("Data size is not valid.")); }
            }

            #endregion

            #region ---- 准备 ----

            GCHandle eigenValues_GC = GCHandle.Alloc(eigenValues, GCHandleType.Pinned);
            IntPtr eigenValues_address = eigenValues_GC.AddrOfPinnedObject();

            int size = ElementSize<T>();
            T[,] eigenVectorsColum = eigenvectorsOut ? new T[N, N] : null;
            GCHandle eigenVectorsColum_GC = GCHandle.Alloc(eigenVectorsColum, GCHandleType.Pinned);
            IntPtr eigenVectorsColum_address = eigenVectorsColum_GC.AddrOfPinnedObject();

            #endregion

            #region ---- 计算 ----

            EigenvaluesSymmetric<T>(matix._dataAddress, matix.Row, eigenValues_address, eigenVectorsColum_address);

            #endregion

            #region ---- 收尾 ----

            if (eigenvectorsOut)
            {
                for (int i = 0; i < N; i++)
                {
                    MatrixCaller.ArrayCopy<T>(eigenVectorsColum_address + i * size, eigenVectors[i], N);
                }
            }

            eigenValues_GC.Free();
            eigenVectorsColum_GC.Free();

            #endregion
        }

        #endregion

        #region 私有函数 - mkl封装

        /// <summary>
        /// 计算一般阵特征值和特征向量. 特征向量为列向量, 即右乘特征向量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为N </param>
        /// <param name="N">matrixA行数 / matrixA列数 </param>
        /// <param name="eigenValues">特征值, 复数数组, 长度N</param>
        /// <param name="eigenVectors">特征向量, 二维复数数组, 长度N*N. 每一列代表一个特征向量</param>
        private static void EigenvaluesGeneral<T>(IntPtr matrixA, int N, IntPtr eigenValues, IntPtr eigenVectors)
        {
            int error = 0;

            #region ---- 准备 ----
            T[] datatype = new T[1];

            int rowA = N;
            int columA = rowA;
            int vectorInterval = N;
            int order = N;

            // 注： 特征向量是按列排列, 对应特征值
            bool eigenvectorsOut = (eigenVectors != IntPtr.Zero);
            JobCompute jobz = eigenvectorsOut ? JobCompute.Compute : JobCompute.NotCompute;

            // 创建临时内存存放数据.
            // 计算会覆盖矩阵内存空间, 需要保留原矩阵的数据
            T[,] tempMatrix = new T[N, N];
            Vector.ArrayCopy<T>(matrixA, tempMatrix);

            GCHandle tempMatrix_GC = GCHandle.Alloc(tempMatrix, GCHandleType.Pinned);
            IntPtr tempMatrix_address = tempMatrix_GC.AddrOfPinnedObject();

            #endregion

            #region ---- 调用API ----

            if (datatype is float[])
            {
                float[] eigenValues_f32_real = new float[order];
                float[] eigenValues_f32_image = new float[order];
                float[,] eigenVectors_f32 = new float[rowA, columA]; // 特征向量, 共轭排列
                error = MatrixCaller.LAPACKE_sgeev(MatrixLayoutEnum.RowMajor, JobCompute.NotCompute, jobz,
                                                   order,
                                                   tempMatrix_address, columA,
                                                   eigenValues_f32_real, eigenValues_f32_image,
                                                   IntPtr.Zero, vectorInterval,
                                                   eigenVectors_f32, vectorInterval);
                GetEigenValues(eigenValues_f32_real, eigenValues_f32_image, eigenValues);
                if (eigenvectorsOut)
                {
                    GetEigenVectors(eigenValues_f32_image, eigenVectors_f32, eigenVectors);
                }
            }
            else if (datatype is double[])
            {
                double[] eigenValues_f64_real = new double[order];
                double[] eigenValues_f64_image = new double[order];
                double[,] eigenVectors_f64 = new double[rowA, columA]; // 特征向量, 共轭排列
                error = MatrixCaller.LAPACKE_dgeev(MatrixLayoutEnum.RowMajor, JobCompute.NotCompute, jobz,
                                                   order,
                                                   tempMatrix_address, columA,
                                                   eigenValues_f64_real, eigenValues_f64_image,
                                                   IntPtr.Zero, vectorInterval,
                                                   eigenVectors_f64, vectorInterval);
                GetEigenValues(eigenValues_f64_real, eigenValues_f64_image, eigenValues);
                if (eigenvectorsOut)
                {
                    GetEigenVectors(eigenValues_f64_image, eigenVectors_f64, eigenVectors);
                }
            }
            else if (datatype is Complex32[])
            {
                error = MatrixCaller.LAPACKE_cgeev(MatrixLayoutEnum.RowMajor, JobCompute.NotCompute, jobz,
                                                   order,
                                                   tempMatrix_address, columA,
                                                   eigenValues,
                                                   IntPtr.Zero, vectorInterval,
                                                   eigenVectors, vectorInterval);
            }
            else if (datatype is Complex[])
            {
                error = MatrixCaller.LAPACKE_zgeev(MatrixLayoutEnum.RowMajor, JobCompute.NotCompute, jobz,
                                                   order,
                                                   tempMatrix_address, columA,
                                                   eigenValues,
                                                   IntPtr.Zero, vectorInterval,
                                                   eigenVectors, vectorInterval);
            }
            else { throw new System.Exception("Data type not supported"); }

            #endregion

            #region ---- 收尾 ----

            tempMatrix_GC.Free();

            #endregion

            if (error != 0) { throw new System.Exception(String.Format("Compute error. Error code = {0}", error)); }
        }

        /// <summary>
        /// 计算对称阵特征值和特征向量. 特征向量为列向量, 即右乘特征向量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为N </param>
        /// <param name="N">matrixA行数 / matrixA列数 </param>
        /// <param name="eigenValues">特征值, 复数数组, 长度N</param>
        /// <param name="eigenVectors">特征向量, 二维复数数组, 长度N*N. 每一列代表一个特征向量</param>
        private static void EigenvaluesSymmetricG<T>(IntPtr matrixA, int N, IntPtr eigenValues, IntPtr eigenVectors)
        {
            // 注： 特征向量是按列排列, 对应特征值
            int error = 0;

            #region ---- 准备 ----
            T[] datatype = new T[1];

            int rowA = N;
            int columA = rowA;
            int vectorInterval = N;
            int order = N;

            bool eigenvectorsOut = (eigenVectors != IntPtr.Zero);

            // 创建临时内存存放数据
            // 计算会覆盖矩阵内存空间, 需要保留原矩阵的数据
            bool inPlace = (matrixA == eigenVectors);

            #endregion

            #region ---- 调用API ----

            if (datatype is float[])
            {
                // 特征值实部
                float[] eigenValues_f32_real = new float[order];
                GCHandle eigenValues_f32_GC = GCHandle.Alloc(eigenValues_f32_real, GCHandleType.Pinned);
                IntPtr eigenValues_f32_Address = eigenValues_f32_GC.AddrOfPinnedObject();
                // 特征值虚部
                float[] eigenValues_f32_image = new float[order];
                // 实数特征向量
                float[,] eigenVectors_f32 = eigenvectorsOut ? new float[order, order] : null;
                GCHandle eigenVectors_f32_GC = GCHandle.Alloc(eigenVectors_f32, GCHandleType.Pinned);
                IntPtr eigenVectors_f32_Address = eigenVectors_f32_GC.AddrOfPinnedObject();
                // 计算对称矩阵特征值
                EigenvaluesSymmetric<float>(matrixA, N, eigenValues_f32_Address, eigenVectors_f32_Address);
                // 生成复数特征值
                GetEigenValues(eigenValues_f32_real, eigenValues_f32_image, eigenValues);
                // 生成复数特征向量
                if (eigenvectorsOut)
                {
                    GetEigenVectors(eigenValues_f32_image, eigenVectors_f32, eigenVectors);
                }

                eigenValues_f32_GC.Free();
                eigenVectors_f32_GC.Free();
            }
            else if (datatype is double[])
            {
                // 特征值实部
                double[] eigenValues_f64_real = new double[order];
                GCHandle eigenValues_f64_GC = GCHandle.Alloc(eigenValues_f64_real, GCHandleType.Pinned);
                IntPtr eigenValues_f64_Address = eigenValues_f64_GC.AddrOfPinnedObject();
                // 特征值虚部
                double[] eigenValues_f64_image = new double[order];
                // 实数特征向量
                double[,] eigenVectors_f64 = eigenvectorsOut ? new double[order, order] : null;
                GCHandle eigenVectors_f64_GC = GCHandle.Alloc(eigenVectors_f64, GCHandleType.Pinned);
                IntPtr eigenVectors_f64_Address = eigenVectors_f64_GC.AddrOfPinnedObject();
                // 计算对称矩阵特征值
                EigenvaluesSymmetric<double>(matrixA, N, eigenValues_f64_Address, eigenVectors_f64_Address);
                // 生成复数特征值
                GetEigenValues(eigenValues_f64_real, eigenValues_f64_image, eigenValues);
                // 生成复数特征向量
                if (eigenvectorsOut)
                {
                    GetEigenVectors(eigenValues_f64_image, eigenVectors_f64, eigenVectors);
                }

                eigenValues_f64_GC.Free();
                eigenVectors_f64_GC.Free();
            }
            else if (datatype is Complex32[])
            {
                // 特征值实部
                float[] eigenValues_f32_real = new float[order];
                GCHandle eigenValues_f32_GC = GCHandle.Alloc(eigenValues_f32_real, GCHandleType.Pinned);
                IntPtr eigenValues_f32_Address = eigenValues_f32_GC.AddrOfPinnedObject();
                // 特征值虚部
                float[] eigenValues_f32_image = new float[order];
                // 计算对称矩阵特征值
                EigenvaluesSymmetric<Complex32>(matrixA, N, eigenValues_f32_Address, eigenVectors);
                // 生成复数特征值
                GetEigenValues(eigenValues_f32_real, eigenValues_f32_image, eigenValues);

                eigenValues_f32_GC.Free();
            }
            else if (datatype is Complex[])
            {
                // 特征值实部
                double[] eigenValues_f64_real = new double[order];
                GCHandle eigenValues_f64_GC = GCHandle.Alloc(eigenValues_f64_real, GCHandleType.Pinned);
                IntPtr eigenValues_f64_Address = eigenValues_f64_GC.AddrOfPinnedObject();
                // 特征值虚部
                double[] eigenValues_f64_image = new double[order];
                // 计算对称矩阵特征值
                EigenvaluesSymmetric<Complex>(matrixA, N, eigenValues_f64_Address, eigenVectors);
                // 生成复数特征值
                GetEigenValues(eigenValues_f64_real, eigenValues_f64_image, eigenValues);

                eigenValues_f64_GC.Free();
            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            #region ---- 收尾 ----
            #endregion
        }

        /// <summary>
        /// 计算对称阵特征值和特征向量. 特征向量为列向量, 即右乘特征向量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为N </param>
        /// <param name="N">matrixA行数 / matrixA列数 </param>
        /// <param name="eigenValues">特征值, 实数类型数组, 长度N</param>
        /// <param name="eigenVectors">特征向量, 二维T类型数组, 长度N*N. 每一列代表一个特征向量 </param>
        private static void EigenvaluesSymmetric<T>(IntPtr matrixA, int N, IntPtr eigenValues, IntPtr eigenVectors)
        {
            // 注： 特征向量是按列排列, 对应特征值
            int error = 0;

            #region ---- 准备 ----
            T[] datatype = new T[1];

            int rowA = N;
            int columA = rowA;
            int vectorInterval = N;
            int order = N;

            bool eigenvectorsOut = (eigenVectors != IntPtr.Zero);
            JobCompute jobz = eigenvectorsOut ? JobCompute.Compute : JobCompute.NotCompute;

            // 创建临时内存存放数据
            // 计算会覆盖矩阵内存空间, 需要保留原矩阵的数据
            bool inPlace = (matrixA == eigenVectors);
            IntPtr tempMatrix;
            T[,] tempArray = null;

            if (inPlace)
            {
                tempMatrix = matrixA;
            }
            else
            {
                if (eigenvectorsOut)
                {
                    Vector.ArrayCopy<T>(matrixA, eigenVectors, N * N);
                    tempMatrix = eigenVectors;
                }
                else
                {
                    tempArray = new T[N, N];
                    Vector.ArrayCopy<T>(matrixA, tempArray);
                }
            }
            GCHandle tempArray_GC = GCHandle.Alloc(tempArray, GCHandleType.Pinned);
            IntPtr tempArray_GC_Address = tempArray_GC.AddrOfPinnedObject();

            tempMatrix = inPlace ? (matrixA) : (eigenvectorsOut ? eigenVectors : tempArray_GC_Address);

            #endregion

            #region ---- 调用API ----

            if (datatype is float[])
            {
                error = MatrixCaller.LAPACKE_ssyev(MatrixLayoutEnum.RowMajor, jobz, MatrixTriangularChar.UpTriangular,
                                                   order,
                                                   tempMatrix, columA,
                                                   eigenValues);
            }
            else if (datatype is double[])
            {
                error = MatrixCaller.LAPACKE_dsyev(MatrixLayoutEnum.RowMajor, jobz, MatrixTriangularChar.UpTriangular,
                                                   order,
                                                   tempMatrix, columA,
                                                   eigenValues);
            }
            else if (datatype is Complex32[])
            {
                error = MatrixCaller.LAPACKE_cheev(MatrixLayoutEnum.RowMajor, jobz, MatrixTriangularChar.UpTriangular,
                                                   order,
                                                   tempMatrix, columA,
                                                   eigenValues);
            }
            else if (datatype is Complex[])
            {
                error = MatrixCaller.LAPACKE_zheev(MatrixLayoutEnum.RowMajor, jobz, MatrixTriangularChar.UpTriangular,
                                                   order,
                                                   tempMatrix, columA,
                                                   eigenValues);
            }
            else { throw new System.Exception("Data type not supported"); }

            #endregion

            #region ---- 收尾 ----

            tempArray_GC.Free();

            #endregion

            if (error != 0) { throw new System.Exception(String.Format("Compute error. Error code = {0}", error)); }

        }

        #endregion

        #region 数据类型转换

        /// <summary>
        /// 复数特值
        /// </summary>
        private static Complex32[] GetEigenValues(int n, float[] wr, float[] wi)
        {
            int j;
            Complex32[] eigenValues = new Complex32[n];

            for (j = 0; j < n; j++)
            {
                if (wi[j] == 0.0f)
                {
                    eigenValues[j] = new Complex32(wr[j], 0.0f);
                }
                else
                {
                    eigenValues[j] = new Complex32(wr[j], wi[j]);
                }
            }



            return eigenValues;
        }

        /// <summary>
        /// 复数特值
        /// </summary>
        private static Complex[] GetEigenValues(int n, double[] wr, double[] wi)
        {
            int j;
            Complex[] eigenValues = new Complex[n];

            for (j = 0; j < n; j++)
            {
                if (wi[j] == 0.0)
                {
                    eigenValues[j] = new Complex(wr[j], 0.0);
                }
                else
                {
                    eigenValues[j] = new Complex(wr[j], wi[j]);
                }
            }



            return eigenValues;
        }

        /// <summary>
        /// 复数特征向量
        /// </summary>
        /// <returns>注： 特征向量是按列排列，对应特征值</returns>
        private static Complex32[,] GetEigenVectors(int n, float[] wi, float[,] v)
        {
            int i, j;
            Complex32[,] eigenVectors = new Complex32[n, n];
            for (i = 0; i < n; i++)
            {
                j = 0;
                while (j < n)
                {
                    if (wi[j] == 0.0f)
                    {
                        eigenVectors[i, j] = new Complex32(v[i * n, j], 0.0f);
                        j++;
                    }
                    else
                    {
                        eigenVectors[i, j] = new Complex32(v[i * n, j], v[i * n, (j + 1)]);
                        eigenVectors[i, j + 1] = new Complex32(v[i * n, j], -v[i * n, (j + 1)]);
                        j += 2;
                    }
                }
            }

            return eigenVectors;
        }

        /// <summary>
        /// 复数特征向量
        /// </summary>
        /// <returns>注： 特征向量是按列排列，对应特征值</returns>
        private static Complex[,] GetEigenVectors(int n, double[] wi, double[,] v)
        {
            int i, j;
            Complex[,] eigenVectors = new Complex[n, n];
            for (i = 0; i < n; i++)
            {
                j = 0;
                while (j < n)
                {
                    if (wi[j] == 0.0)
                    {
                        eigenVectors[i, j] = new Complex(v[i * n, j], 0.0);
                        j++;
                    }
                    else
                    {
                        eigenVectors[i, j] = new Complex(v[i * n, j], v[i * n, (j + 1)]);
                        eigenVectors[i, j + 1] = new Complex(v[i * n, j], -v[i * n, (j + 1)]);
                        j += 2;
                    }
                }
            }

            return eigenVectors;
        }


        /// <summary>
        /// 复数特值
        /// </summary>
        private static void GetEigenValues<T>(T[] wr, T[] wi, IntPtr eigenValues)
        {
            MatrixCaller.ArrayCopy<T>(wr, eigenValues, 2);
            MatrixCaller.ArrayCopy<T>(wi, eigenValues + ElementSize<T>(), 2);
        }

        /// <summary>
        /// 复数特征向量
        /// </summary>
        /// <returns>注： 特征向量是按列排列，对应特征值</returns>
        private static void GetEigenVectors(float[] wi, float[,] v, IntPtr eigenVectors)
        {
            int i, j;
            int n = wi.Length;
            Complex32[] vector = new Complex32[n];
            int offset = ElementSize<Complex32>();
            for (j = 0; j < n;)
            {
                if (wi[j] == 0.0f)
                {
                    // 实数特征向量
                    for (i = 0; i < n; i++)
                        vector[i] = new Complex32(v[i, j], 0.0f);
                    j++;

                    // 保存实数特征向量
                    MatrixCaller.ArrayCopy(vector, eigenVectors, n);
                    eigenVectors += offset;
                }
                else
                {
                    // 复数特征向量
                    for (i = 0; i < n; i++)
                        vector[i] = new Complex32(v[i, j], v[i, (j + 1)]);
                    j += 2;

                    // 保存复数特征向量
                    MatrixCaller.ArrayCopy(vector, eigenVectors, n);
                    eigenVectors += offset;

                    // 保存复数共轭特征向量
                    Vector.ComplexConjugate(vector);
                    MatrixCaller.ArrayCopy(vector, eigenVectors, n);
                    eigenVectors += offset;
                }
            }
        }

        /// <summary>
        /// 复数特征向量
        /// </summary>
        /// <returns>注： 特征向量是按列排列，对应特征值</returns>
        private static void GetEigenVectors(double[] wi, double[,] v, IntPtr eigenVectors)
        {
            int i, j;
            int n = wi.Length;
            Complex[] vector = new Complex[n];
            int offset = ElementSize<Complex>();
            for (j = 0; j < n;)
            {
                if (wi[j] == 0.0)
                {
                    // 实数特征向量
                    for (i = 0; i < n; i++)
                        vector[i] = new Complex(v[i, j], 0.0);
                    j++;

                    // 保存实数特征向量
                    MatrixCaller.ArrayCopy(vector, eigenVectors, n);
                    eigenVectors += offset;
                }
                else
                {
                    // 复数特征向量
                    for (i = 0; i < n; i++)
                        vector[i] = new Complex(v[i, j], v[i, (j + 1)]);
                    j += 2;

                    // 保存复数特征向量
                    MatrixCaller.ArrayCopy(vector, eigenVectors, n);
                    eigenVectors += offset;

                    // 保存复数共轭特征向量
                    Vector.ComplexConjugate(vector);
                    MatrixCaller.ArrayCopy(vector, eigenVectors, n);
                    eigenVectors += offset;
                }
            }
        }

        #endregion
    }

    internal partial class MatrixCaller
    {
        #region ---- 一般矩阵 (LAPACK Least Squares and Eigenvalue Problem -> Drive -> LAPACKE_?geev) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvLeft, JobCompute jobvRight,
                                                 int n,
                                                 float[,] a, int lda,
                                                 float[] wr, float[] wi,
                                                 float[,] vl, int ldvl,
                                                 float[,] vr, int ldvr);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvLeft, JobCompute jobvRight,
                                                 int n,
                                                 IntPtr a, int lda,
                                                 float[] wr, float[] wi,
                                                 IntPtr vl, int ldvl,
                                                 float[,] vr, int ldvr);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvl, JobCompute jobvr,
                                                 int n,
                                                 double[,] a, int lda,
                                                 double[] wr, double[] wi,
                                                 double[,] vl, int ldvl,
                                                 double[,] vr, int ldvr);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvl, JobCompute jobvr,
                                                 int n,
                                                 IntPtr a, int lda,
                                                 double[] wr, double[] wi,
                                                 IntPtr vl, int ldvl,
                                                 double[,] vr, int ldvr);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvl, JobCompute jobvr,
                                                 int n,
                                                 IntPtr a, int lda,
                                                 IntPtr w,
                                                 IntPtr vl, int ldvl,
                                                 IntPtr vr, int ldvr);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvl, JobCompute jobvr,
                                                 int n,
                                                 IntPtr a, int lda,
                                                 IntPtr w,
                                                 IntPtr vl, int ldvl,
                                                 IntPtr vr, int ldvr);
        #endregion

        #region ---- 对称矩阵 (LAPACK Least Squares and Eigenvalue Problem -> Drive -> LAPACKE_?syev/?heev) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_ssyev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n,
                                                 float[,] a, int lda,
                                                 float[] w);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_ssyev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n, IntPtr a, int lda, IntPtr w);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dsyev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n, double[,] a, int lda, double[] w);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dsyev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n, IntPtr a, int lda, IntPtr w);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cheev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n, IntPtr a, int lda, float[] w);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cheev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n, IntPtr a, int lda, IntPtr w);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zheev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n, IntPtr a, int lda, double[] w);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zheev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n, IntPtr a, int lda, IntPtr w);
        #endregion
    }
}
