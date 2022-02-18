using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JX.DataViewer;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.Synchronization;

namespace SeeSharpTools.JXI.SignalProcessing.SynchronizationTests
{
    [TestClass()]
    public class SynchronizationTests
    {
        //[TestMethod()]
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

        [TestMethod()]
        public void SyncWaveformTest002()
        {
            /***************************          
             * 输入两个差10个点（在此程序中就是36度）的信号
             * 通过同步后，两个信号达到同步
             * ********************************/
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, 36, 10, 1000);
            double[,] rawWaveform = new double[1000, 2];
            ArrayManipulation.Connected_2D_Array(sinWave1, sinWave2, ref rawWaveform);
            double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
            ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
            var syncWaveform = Synchronization.SyncWaveform(rawWaveformT, 10);
            AnalogWaveformChart.Plot("RawWaveform", rawWaveformT);
            AnalogWaveformChart.Plot("SyncWaveform", syncWaveform);
        }


        [TestMethod()]
        public void SyncWaveformTest003()
        {
            /***************************          
             * 输入两个差0.5个点的信号
             * 通过同步后，两个信号达到同步
             * ********************************/
            double[] sinWave1 = new double[2000];
            double[] sinWave2 = new double[2000];
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, 360.0/200, 10, 1000);
            double[,] rawWaveform = new double[2000, 2];
            ArrayManipulation.Connected_2D_Array(sinWave1, sinWave2, ref rawWaveform);
            double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
            ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
            var syncWaveform = Synchronization.SyncWaveform(rawWaveformT);
            AnalogWaveformChart.Plot("RawWaveform", rawWaveformT);
            AnalogWaveformChart.Plot("SyncWaveform", syncWaveform);
        }

    }

}