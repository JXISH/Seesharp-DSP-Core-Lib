using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeeSharpTools.JX.DataViewer;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.Synchronization;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.AdvancedFilters;
using SeeSharpTools.JXI.Exception;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using System.Windows.Forms;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyResample;
using System.Numerics;
using System.IO;



namespace SyncrhonizationExample
{
    public class ResampleUnitTest
    {

        //public void SyncWaveformTest001()
        //{
        //    JYUSB61902AITask aitask = new JYUSB61902AITask(0);
        //    aitask.AddChannel(0, -10, 10, AITerminal.RSE);
        //    aitask.AddChannel(1, -10, 10, AITerminal.RSE);
        //    aitask.SampleRate = 500;
        //    aitask.Mode = AIMode.Finite;
        //    double[,] rawWaveform = new double[1024, 2];
        //    aitask.ReadData(ref rawWaveform);
        //    double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
        //    ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
        //    var syncWaveform = Synchronization.SyncWaveform(rawWaveformT);
        //    AnalogWaveformChart.Plot("RawWaveform", rawWaveformT);
        //    AnalogWaveformChart.Plot("SyncWaveform", syncWaveform);
        //}


        public static void SyncWaveformTest002()
        {
            /***************************          
             * 输入两个差10个点（在此程序中就是36度）的信号
             * 通过同步后，两个信号达到同步
             * ********************************/
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            Generation.SineWave(ref sinWave2, 1, 36, 10, 1000);
            double[,] rawWaveform = new double[1000, 2];
            ArrayManipulation.Connected_2D_Array(sinWave1, sinWave2, ref rawWaveform);
            double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
            ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
            var syncWaveform = Synchronization.SyncWaveform(rawWaveformT, 10);
            AnalogWaveformChart.Plot("RawWaveform", rawWaveformT);
            AnalogWaveformChart.Plot("SyncWaveform", syncWaveform);
            string[] temp = new string[1000];
            using (StreamWriter sw = new StreamWriter("sinWaveRaw.txt"))
            {
                for (int i = 0; i < 1000; i++)
                    temp[i] = Convert.ToString(rawWaveformT[0, i]);
                var readablePhrase = string.Join(" ", temp);
                sw.WriteLine(readablePhrase);
                for (int i = 0; i < 1000; i++)
                    temp[i] = Convert.ToString(rawWaveformT[1, i]);
                readablePhrase = string.Join(" ", temp);
                sw.WriteLine(readablePhrase);
            }

            using (StreamWriter sw = new StreamWriter("sinWaveSync.txt"))
            {
                for (int i = 0; i < 830; i++)
                    temp[i] = Convert.ToString(syncWaveform[0, i]);
                var readablePhrase = string.Join(" ", temp);
                sw.WriteLine(readablePhrase);
                for (int i = 0; i < 830; i++)
                    temp[i] = Convert.ToString(syncWaveform[1, i]);
                readablePhrase = string.Join(" ", temp);
                sw.WriteLine(readablePhrase);
            }
        }



        public static void SyncWaveformTest003()
        {
            /***************************          
             * 输入两个差0.5个点的信号
             * 通过同步后，两个信号达到同步
             * ********************************/
            double[] sinWave1 = new double[2000];
            double[] sinWave2 = new double[2000];
            Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            Generation.SineWave(ref sinWave2, 1, 360.0 / 200, 10, 1000);
            double[,] rawWaveform = new double[2000, 2];
            ArrayManipulation.Connected_2D_Array(sinWave1, sinWave2, ref rawWaveform);
            double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
            ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
            var syncWaveform = Synchronization.SyncWaveform(rawWaveformT);
            AnalogWaveformChart.Plot("RawWaveform", rawWaveformT);
            AnalogWaveformChart.Plot("SyncWaveform", syncWaveform);
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                //double[] b = new double[5] { 1, 2, 3, 4, 5 };
                //Console.WriteLine();


                //double[] sin1 = new double[30];
                //double[] sin2 = new double[30];
                //double[,] sin = new double[30, 2];
                //double[,] sinT = new double[2, 30];
                //Generation.SineWave(ref sin1, 1, 0, 10, 1000);
                //Generation.SineWave(ref sin2, 1, 1.57, 10, 1000);
                //for (int i = 0; i < 30; i++)
                //    sin[i,0] = sin1[i];
                //for (int i = 0; i < 30; i++)
                //    sin[i,1] = sin2[i];
                //var z = Synchronization.SyncWaveform(sin);
                //Console.WriteLine("");





                //Resample_Test_001();
                //SyncWaveformTest002();
                //Resample_Test_003();
                //Resample_Test_004();
                //Resample_Test_005();
                double[] wave = new double[1000];
                double[] wave2 = new double[1000];
                Generation.SineWave(ref wave, 1, 0, 10, 1000);
                Generation.SineWave(ref wave2, 1, 90, 10, 1000);
                Complex[] sinwave = new Complex[1000];
                for (int i = 0; i < 1000; i++)
                    sinwave[i] = new Complex(wave[i], wave2[i]);

                AnalogWaveformChart.Plot("Sinwave", wave);
                FrequencyShift<double> shifter = new FrequencyShift<double>(0.5, 0);
                shifter.Process(wave);
                AnalogWaveformChart.Plot("Sinwave_tran", wave);


                //var bCofficient = Properties.Resource.SynchronizerCoefficient;//内插滤波器系数
                Console.ReadKey();

            }
        }
    }
}
