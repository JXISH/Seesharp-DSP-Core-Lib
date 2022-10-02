using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix;

namespace Matrix_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ///<summary>
            ///构造函数相关代码示例
            ///</summary>
            #region


            // 调用生成单位矩阵的静态方法
            Matrix<double> matIdent = Matrix<double>.Identity(3);
            Console.WriteLine("Identity Matrix Generated:");
            PrintMatrix(matIdent);

            //对角线生成
            double[] diag = new double[3] { 1, 2, 3 };
            Matrix<double> matDiag = new Matrix<double>(diag);
            Console.WriteLine("Diagnal Matrix Generated:");
            PrintMatrix(matDiag);


            //创建二维数组，并利用构造函数对Matrix类的mat进行初始化
            double[,] data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            //double[][] data2 = new double[3][] { new double[3], new double[3],                                                            new double[3]};
            Matrix<double> mat = new Matrix<double>(data);

            //利用构造函数，完成mat2对mat的拷贝
            Matrix<double> mat2 = new Matrix<double>(mat);

            //打印mat和mat2的数据以及哈希码
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat's HashCode:{0}", mat.GetHashCode());

            Console.WriteLine("mat2:");
            PrintMatrix(mat2);
            Console.WriteLine("mat2's HashCode:{0}", mat2.GetHashCode());

            #endregion



            ///<summary>
            ///属性相关代码示例
            ///</summary>
            #region

            //创建二维数组，并利用构造函数对Matrix类的mat进行初始化
            data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            mat = new Matrix<double>(data);

            //打印对象mat的相关属性值
            //行列信息
            Console.WriteLine("size of mat: row:{0}, colum:{1}", mat.Row, mat.Colum);
            //是否为方阵
            Console.WriteLine("mat is square: {0}", mat.IsSquare);
            //是否非空
            Console.WriteLine("mat is valid: {0}", mat.IsValid);

            #endregion



            ///<summary>
            ///GetColum & GetRow方法调用
            ///</summary>
            #region

            //创建二维数组和矩阵类对象mat，并利用构造函数对Matrix类的mat进行初始化
            data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            mat = new Matrix<double>(data);

            //打印mat第一行的元素，即mat.GetRow(0)的元素
            Console.Write("the elements of the first row: ");
            for (int i = 0; i < mat.Colum; i++)
            {
                Console.Write(mat.GetRow(0).GetValue(i));
                Console.Write(" ");
            }

            Console.WriteLine();

            Console.Write("the elements of the first colum: ");
            //打印第一列元素，即mat.GetColum(1)的元素
            for (int i = 0; i < mat.Row; i++)
            {
                Console.Write(mat.GetColum(0).GetValue(i));
                Console.Write(" ");
            }

            #endregion



            ///<summary>
            ///SetColum & SetRow方法调用
            ///</summary>
            #region

            //创建二维数组和矩阵类对象mat，并利用构造函数对Matrix类的mat进行初始化
            data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            mat = new Matrix<double>(data);
            //利用矩阵mat的行列属性，构造一个与之等大的空矩阵mat2
            mat2 = new Matrix<double>(mat.Row, mat.Colum);

            //打印两个矩阵数据
            Console.WriteLine("before assignment:");
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat2:");
            PrintMatrix(mat2);

            //使用GetRow和SetRow方法为mat2赋值
            for (int i = 0; i < mat.Row; i++)
            {
                mat2.SetRow(i, mat.GetRow(i));
            }

            Console.WriteLine();
            //再次打印两个矩阵数据
            Console.WriteLine("after assignment:");
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat2:");
            PrintMatrix(mat2);

            #endregion


            ///<summary>
            ///CopyFrom方法调用
            ///</summary>
            #region

            //创建二维数组data和data2作为矩阵的初始化数据    		
            data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            double[][] data2 = new double[3][] { new double[3], new double[3], new double[3] };
            //利用构造函数并输入data和data2对Matrix类的mat和mat2进行初始化
            mat = new Matrix<double>(data);
            mat2 = new Matrix<double>(data2);

            //打印复制前两矩阵元素
            Console.WriteLine("before copy:");
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat2:");
            PrintMatrix(mat2);

            //调用拷贝方法将mat拷贝到mat2
            mat2.CopyFrom(mat);
            Console.WriteLine();

            //打印复制后两矩阵的元素
            Console.WriteLine("after copy:");
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat2:");
            PrintMatrix(mat2);
            Console.WriteLine();

            #endregion

            Console.ReadKey();
        }

        static void PrintMatrix(Matrix<double> matrix)
        {
            int i = 0;
            int j = 0;
            for (i = 0; i < matrix.Row; i++)
            {
                for (j = 0; j < matrix.Colum; j++)
                {
                    Console.Write("{0} ", matrix.MatrixArray[i, j]);
                }
                Console.WriteLine();
            }
        }
    }

}
