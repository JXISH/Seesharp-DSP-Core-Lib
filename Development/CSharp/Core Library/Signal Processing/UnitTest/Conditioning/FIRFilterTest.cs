using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters;

using SeeSharpTools.JX.DataViewer;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JY.DSP.Fundamental;



namespace SeeSharpTools.JX.SignalProcessing.ConditioningTest
{
    [TestClass]
    public class FIRFilterTest
    {
        [TestMethod]
        public void FIR_LPF_Test_001()
        {
            /*************************
             * LPF
             * passFreq=50Hz
             * stopFreq=100Hz
             * 带外衰减40db
             * 采样频率1000Hz
             * *************************/
            double[] coe = new double[] { 0.00194150175769345,- 0.00169793575520208,- 0.00426956799325628,- 0.00829350380976647,- 0.0132303432212115,- 0.0181544335557185,- 0.0217585679131809,- 0.0225134101549763,- 0.0189478446411358,- 0.00995128109977753,0.00489205937905203,0.0250974062726473, 0.0492006450550267, 0.0748863114260257, 0.0992878499995790, 0.119424371366942,  0.132695897659600,  0.137328720011526,  0.132695897659600 ,0.119424371366942 , 0.0992878499995790,0.0748863114260257,0.0492006450550267,0.0250974062726473, 0.00489205937905203,- 0.00995128109977753,- 0.0189478446411358,- 0.0225134101549763,- 0.0217585679131809,- 0.0181544335557185,- 0.0132303432212115 - 0.00829350380976647,- 0.00426956799325628,- 0.00169793575520208,0.00194150175769345 };
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            Generation.SineWave(ref sinWave2, 1, 0, 120, 1000);
            ArrayCalculation.Add(sinWave1, sinWave2, ref sinWave1);
            double[] sinWaveFir = new double[1000];
            var fir = new FIRFilter();
            fir.Coefficients = coe;
            fir.Filter(sinWave1, ref sinWaveFir);
            //VISN_Plot.PlotData("FIR_LPF1", sinWaveFir);
            //VISN_Plot.PlotData("FIR_LPF2", sinWave1);
            AnalogWaveformChart.Plot("FIR_LPF1", sinWaveFir);
            AnalogWaveformChart.Plot("FIR_LPF2", sinWave1);
        }

        //[TestMethod]
        //public void FIR_LPF_MAT_Test()
        //{
        //    /*************************
        //     * LPF
        //     * passFreq=50Hz
        //     * stopFreq=100Hz
        //     * 带外衰减40db
        //     * 采样频率1000Hz
        //     * *************************/
        //    //从collection.mat中读取一个名称为 vd 的特定矩阵
        //    Matrix<double> m = MatlabReader.Read<double>("fir_lpf_50_100_40.mat", "Num");
        //    var stream = File.OpenRead("fir_lpf_50_100_40.mat");
        //    var reader = new BinaryReader(stream);
        //    reader.BaseStream.Position = 128;
        //    var length = stream.Length;
        //    var rows = reader.ReadInt32();
        //    var columns = reader.ReadInt32();
        //    var count = rows * columns;
        //    var data = new double [count];
        //    // small size (2 bytes)
        //    int size = reader.ReadInt16();
        //    Buffer.BlockCopy(reader.ReadBytes(size), 0, data, 0, size);
        //    double[] sinWave1 = new double[1000];
        //    double[] sinWave2 = new double[1000];
        //    Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
        //    Generation.SineWave(ref sinWave2, 1, 0, 120, 1000);
        //    ArrayCalculation.Add(sinWave1, sinWave2, ref sinWave1);
        //    double[] sinWaveFir = new double[1000];
        //    var fir = new FIRFilter();
        //    //fir.Coefficients = coe;
        //    fir.Filter(sinWave1, ref sinWaveFir);
        //    VISN_Plot.PlotData("FIR_LPF1", sinWaveFir);
        //    VISN_Plot.PlotData("FIR_LPF2", sinWave1);
        //}
    }
}
