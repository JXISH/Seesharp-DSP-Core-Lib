using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeeSharpTools.JXI.SignalProcessing.Window;


namespace Window_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region --- Windows ---
            Console.WriteLine("*** Windows ***");
            double CG;
            double ENBW;
            double[] origin_data = new double[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double[] origin_winData = origin_data.Skip(2).Take(6).ToArray();

            Console.WriteLine("* Rectangle window *");
            Window.GetWindow(WindowType.None, ref origin_winData, out CG, out ENBW);
            Console.Write("Rectangle window data: ");
            foreach (var item in origin_winData) { Console.Write("{0} ", item); }
            Console.WriteLine();
            Console.WriteLine("Rectangle window Coherent Gain: {0}", CG);
            Console.WriteLine("Rectangle window ENBW: {0}", ENBW);
            Console.WriteLine();

            Console.WriteLine("* Hanning window *");
            Window.GetWindow(WindowType.Hanning, ref origin_winData, out CG, out ENBW);
            Console.Write("Hanning window data: ");
            foreach (var item in origin_winData) { Console.Write("{0} ", item); }
            Console.WriteLine();
            Console.WriteLine("Hanning window Coherent Gain: {0}", CG);
            Console.WriteLine("Hanning window ENBW: {0}", ENBW);
            Console.WriteLine();
            Console.ReadKey();
            #endregion
        }
    }
}
