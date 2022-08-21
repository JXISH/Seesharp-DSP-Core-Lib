using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix;
using SeeSharpTools.JXI.Numerics;

namespace LinearEquationExample
{
    internal class Program
    {

        static void Main(string[] args)
        {

            void PrintMatrix(Matrix<double> m)
            {
                int i = 0;
                int j = 0;
                for (i = 0; i < m.Row; i++)
                {
                    for (j = 0; j < m.Colum; j++)
                    {
                        Console.Write("{0} ", m.MatrixArray[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            double[,] a = new double[3, 3] { { 1, 2, 3 }, { 2, 3, 4 }, { 3, 4, 5 } };
            double[,] b = new double[3, 1] { { 1 }, { 2 }, { 3 } };
            double[,] c = new double[3, 3] { { 1, 2, 3 }, { 2, 3, 4 }, { 3, 4, 5 } };
            Matrix<double> matrix = new Matrix<double>(a);
            Matrix<double> matrix2 = new Matrix<double>(b);
            Matrix<double> matrix3 = new Matrix<double>(c);


            Console.WriteLine("解矩阵方程：AX=B");
            Console.WriteLine("A = ");
            PrintMatrix(matrix);
            Console.WriteLine("B = ");
            PrintMatrix(matrix3);
            Matrix<double>.SolveLinearEquations(matrix, matrix3);
            Console.WriteLine("解为X = ");
            PrintMatrix(matrix3);
            Console.WriteLine();

            Console.WriteLine("解线性方程组：Ax=b");
            Console.WriteLine("A = ");
            PrintMatrix(matrix);
            Console.WriteLine("b = ");
            PrintMatrix(matrix2);
            Matrix<double>.SolveLinearEquations(matrix, matrix2);
            Console.WriteLine("解为x = ");
            PrintMatrix(matrix2);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
