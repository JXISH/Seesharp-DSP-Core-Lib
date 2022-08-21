using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeeSharpTools.JXI.Mathematics.Geometry;

namespace GeometryExapmple
{
    internal class Program
    {
        static public void PrintArray(double[] array)
        {
            for(int i = 0; i < array.Length; i++)
                Console.Write("{0} ", Math.Round(array[i], 3));
            Console.WriteLine();
        }


        static void Main(string[] args)
        {
            // ----------------------------笛卡尔坐标系操作测试-------------------------
            Console.WriteLine("----------------笛卡尔坐标系操作测试---------------");
            Cartesian cartesian = new Cartesian(1,2,3);
            Cartesian cartesian2 = new Cartesian(2,3,4);
            double scale = 5;
            Console.WriteLine();
            Console.WriteLine("第一个笛卡尔坐标：c1 = ({0},{1},{2})", cartesian.x, cartesian.y, cartesian.z);
            Console.WriteLine("第二个笛卡尔坐标：c2 = ({0},{1},{2})", cartesian2.x, cartesian2.y, cartesian2.z);
            Console.WriteLine("标量s = {0}", scale);


            // 基本运算
            Console.WriteLine("----------------------基本运算---------------------");
            Console.WriteLine("---------------------运算符重载--------------------");
            // 1，坐标加法
            var res = cartesian + cartesian2;
            Console.WriteLine("坐标加法结果：c1 + c2 = ({0},{1},{2})", res.x, res.y, res.z);
            // 2，坐标减法
            var res2 = cartesian - cartesian2;
            Console.WriteLine("坐标减法结果：c1 - c2 = ({0},{1},{2})", res2.x, res2.y, res2.z);
            // 3，三维矢量内积
            var res3 = cartesian * cartesian2;
            Console.WriteLine("三维矢量内积结果：c1 * c2 = {0}", res3);
            // 4，三维矢量标积
            var res4 = cartesian * scale;
            Console.WriteLine("三维矢量标积：c1 * scale = ({0},{1},{2})", res4.x, res4.y, res4.z);
            // 调用CrossProduct计算外积
            var res5 = Cartesian.CrossProduct(cartesian, cartesian2);
            Console.WriteLine();
            Console.WriteLine("--------------------CrossProduct-------------------");
            Console.WriteLine("调用CrossProduct计算外积结果：c1 x c2 = ({0},{1},{2})", res5.x, res5.y, res5.z);

            // 调用DotProduct计算内积
            var res6 = Cartesian.DotProduct(cartesian, cartesian2);
            Console.WriteLine();
            Console.WriteLine("---------------------DotProduct--------------------");
            Console.WriteLine("调用DotProduct计算点积结果：c1 * c2 = {0}", res6);


            // 调用ConverterToSpherical计算将笛卡尔坐标转换为球坐标
            var res7 = Cartesian.ConverterToSpherical(cartesian);
            Console.WriteLine();
            Console.WriteLine("-----------------ConverterToSpherical--------------");
            Console.WriteLine("调用ConverterToSpherical计算将c1转换为球坐标之后为：({0},{1},{2})", res7.r, res7.fai, res7.theta);



            // -----------------------------球坐标系操作测试----------------------------
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("------------------球坐标系操作测试-----------------");
            Spherical spherical = new Spherical(2,Math.PI / 4,Math.PI / 2);
            Console.WriteLine("球坐标：s = ({0},{1},{2})", Math.Round(spherical.r,3), Math.Round(spherical.fai,3), Math.Round(spherical.theta,3));


            // 调用ConvertToCartesian计算将球坐标转换为直角坐标
            Console.WriteLine();
            Console.WriteLine("-----------------ConvertToCartesian----------------");
            var res8 = Spherical.ConvertToCartesian(spherical);
            Console.WriteLine("调用ConvertToCartesian计算将s转换为直角坐标之后为：({0},{1},{2})", Math.Round(res8.x,3), Math.Round(res8.y,3), Math.Round(res8.z, 3));




            //  -------------------------------Scaling测试-----------------------------
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("--------------------Scaling测试--------------------");
            double input = Math.PI / 3;
            double[] input2 = new double[5] { Math.PI / 2, Math.PI / 3, Math.PI / 4, Math.PI / 6, Math.PI };
            double[] inout = new double[5] { Math.PI / 2, Math.PI / 3, Math.PI / 4, Math.PI / 6, Math.PI};
            Console.WriteLine("--------------------弧度制输入---------------------");
            Console.WriteLine("input = {0}", Math.Round(input, 3));
            Console.Write("input2 = ");
            PrintArray(input2);
            Console.Write("inout = ");
            PrintArray(inout);


            // 调用ClockwiseExchange与GetClockwiseExchange求余角（弧度制）
            Console.WriteLine();
            var res9 = Scaling.ClockwiseExchange(input);
            Console.WriteLine("调用ClockwiseExchange计算input的余角 = {0}", Math.Round(res9, 3));

            Scaling.ClockwiseExchange(inout);
            Console.Write("调用ClockwiseExchange计算inout的余角 = ");
            PrintArray(inout);

            var res10 = Scaling.GetClockwiseExchange(input2);
            Console.Write("调用GetClockwiseExchange计算input2的余角 = ");
            PrintArray(res10);

            // 调用ClockwiseExchangeDegree与GetClockwiseExchangeDegree求余角（角度制）
            Console.WriteLine();


            double input_degree = 45;
            double[] input2_degree = new double[5] { 15, 25, 45.5, 75, 85 };
            double[] inout_degree = new double[5] { 15, 25, 45.5, 75, 85 };
            Console.WriteLine("--------------------角度制输入---------------------");
            Console.WriteLine("input_degree = {0}", Math.Round(input_degree, 3));
            Console.Write("input2_degree = ");
            PrintArray(input2_degree);
            Console.Write("inout_degree = ");
            PrintArray(inout_degree);

            // 调用ClockwiseExchangeDegree与GetClockwiseExchangeDegree求余角（角度制）
            Console.WriteLine();
            var res11 = Scaling.ClockwiseExchangeDegree(input_degree);
            Console.WriteLine("调用ClockwiseExchangeDegree计算input_degree的余角 = {0}", Math.Round(res11, 3));

            Scaling.ClockwiseExchangeDegree(inout_degree);
            Console.Write("调用ClockwiseExchangeDegree计算inout_degree的余角 = ");
            PrintArray(inout_degree);

            var res12 = Scaling.GetClockwiseExchangeDegree(input2_degree);
            Console.Write("调用GetClockwiseExchangeDegree计算input2_degree的余角 = ");
            PrintArray(res12);



            
            Console.ReadKey();
        }
        /*
         *  ----------------笛卡尔坐标系操作测试---------------

            第一个笛卡尔坐标：c1 = (1,2,3)
            第二个笛卡尔坐标：c2 = (2,3,4)
            标量s = 5
            ----------------------基本运算---------------------
            ---------------------运算符重载--------------------
            坐标加法结果：c1 + c2 = (3,5,7)
            坐标减法结果：c1 - c2 = (-1,-1,-1)
            三维矢量内积结果：c1 * c2 = 20
            三维矢量标积：c1 * scale = (5,10,15)

            --------------------CrossProduct-------------------
            调用CrossProduct计算外积结果：c1 x c2 = (-1,2,-1)

            ---------------------DotProduct--------------------
            调用DotProduct计算点积结果：c1 * c2 = 20

            -----------------ConverterToSpherical--------------
            调用ConverterToSpherical计算将c1转换为球坐标之后为：(3.74165738677394,0.930274014115472,1.10714871779409)


            ------------------球坐标系操作测试-----------------
            球坐标：s = (2,1.571,0.785)

            -----------------ConvertToCartesian----------------
            调用ConvertToCartesian计算将s转换为直角坐标之后为：(0,0,2)


            --------------------Scaling测试--------------------
            --------------------弧度制输入---------------------
            input = 1.047
            input2 = 1.571 1.047 0.785 0.524 3.142
            inout = 1.571 1.047 0.785 0.524 3.142

            调用ClockwiseExchange计算input的余角 = 0.524
            调用ClockwiseExchange计算inout的余角 = 0 0.524 0.785 1.047 -1.571
            调用GetClockwiseExchange计算input2的余角 = 0 0.524 0.785 1.047 -1.571

            --------------------角度制输入---------------------
            input_degree = 45
            input2_degree = 15 25 45.5 75 85
            inout_degree = 15 25 45.5 75 85

            调用ClockwiseExchangeDegree计算input_degree的余角 = 45
            调用ClockwiseExchangeDegree计算inout_degree的余角 = 75 65 44.5 15 5
            调用GetClockwiseExchangeDegree计算input2_degree的余角 = 75 65 44.5 15 5

         */
    }
}
