
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JY.DSP.Fundamental;
using System;
using System.Threading;
using SeeSharpTools.JX.DataViewer;
using SeeSharpTools.JXI.SignalProcessing.Measurement;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters;

namespace SeeSharpTools.JXI.SignalProcessing.Measurement.Tests
{
    [TestClass()]
    public class FrequencyResponseFunctionTests
    {
        //[TestMethod()]
        //public void AnalyzeTest_001()
        //{
        //    //生成白噪音
        //    double[] writeValue = new double[10240];
        //    JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref writeValue);

        //    var aitask = new JYUSB61902AITask(0);
        //    var aotask = new JYUSB61902AOTask(0);
        //    //添加通道
        //    aotask.AddChannel(0, -10, 10);
        //    //基本参数配置
        //    aotask.Mode = AOMode.Finite;
        //    aotask.UpdateRate = 100000;
        //    aotask.Trigger.Type = AOTriggerType.Immediate;
        //    aitask.AddChannel(0, -10, 10, AITerminal.RSE);
        //    aitask.AddChannel(1, -10, 10, AITerminal.RSE);
        //    aitask.Mode = AIMode.Finite;

        //    aitask.SampleRate = 100000;

        //    //多发数据，防止由于驱动不同步的抖动
        //    aotask.SamplesToUpdate = writeValue.Length;
        //    aitask.SamplesToAcquire = writeValue.Length + 0;
        //    var readValue = new double[aitask.SamplesToAcquire, aitask.Channels.Count];

        //    aotask.WriteData(writeValue, -1);
        //    aotask.Start();
        //    Thread.Sleep(5);
        //    aitask.Start();
        //    aitask.ReadData(ref readValue, -1);

        //    double[] TempInput = new double[writeValue.Length];
        //    double[] TempOutput = new double[writeValue.Length];
        //    for (int k = 0; k < readValue.GetLength(0); k++)
        //    {
        //        if (k < writeValue.Length)
        //        {
        //            TempInput[k] = readValue[k, 0];
        //        }
        //        if (k > -1)
        //        {
        //            TempOutput[k] = readValue[k, 1];
        //        }
        //    }
        //    aotask.WaitUntilDone(-1);
        //    aotask.Stop();
        //    aotask.Channels.Clear();
        //    aitask.Stop();
        //    aitask.Channels.Clear();
        //    double Fs = 200000;
        //    int freqLines = 5000; //set frequency resolution here
        //    double df = Fs / 2 / freqLines;
        //    double[] analysisInWav = new double[freqLines * 2];
        //    double[] analysisOutWav = new double[freqLines * 2];
        //    double[] bodeI = new double[freqLines];
        //    double[] bodeQ = new double[freqLines];
        //    double[] bodeMag = new double[freqLines];
        //    double[] bodePhase = new double[freqLines];
        //    int numOfAverage = (int)Math.Floor((decimal)(TempInput.Length / 2 / freqLines));
        //    AnalogWaveformChart.Plot("input waveform", TempInput);
        //    AnalogWaveformChart.Plot("output waveform", TempOutput);
        //    FrequencyResponseFunction FRFAnalysis = new FrequencyResponseFunction();
        //    ArrayCalculation.InitializeArray(ref bodeI, 0);
        //    ArrayCalculation.InitializeArray(ref bodeQ, 0);

        //    FRFAnalysis.Average.Mode = AverageMode.RMS;
        //    FRFAnalysis.Average.Number = numOfAverage;
        //    FRFAnalysis.ResetAveraging = true;

        //    for (int i = 0; i < numOfAverage; i++)
        //    {
        //        ArrayManipulation.GetArraySubset(TempInput, i * 2 * freqLines, ref analysisInWav);
        //        ArrayManipulation.GetArraySubset(TempOutput, i * 2 * freqLines, ref analysisOutWav);
        //        if (i == 0) FRFAnalysis.Reset();
        //        FRFAnalysis.Analyze(analysisInWav, analysisOutWav);
        //        bodeMag = FRFAnalysis.GetMagenitude(true);
        //        bodePhase = FRFAnalysis.GetPhase(true);
        //    }
        //    bool averageDone = FRFAnalysis.AveragingDone;
        //    Console.WriteLine("averageDone:{0}", averageDone.ToString());
        //    AnalogWaveformChart.Plot("BodeMagnitude", 0, 1 / df, bodeMag);
        //    AnalogWaveformChart.Plot("BodePhase", 0, 1 / df, bodePhase);
        //}

        //[TestMethod()]
        //public void AnalyzeTest_002()
        //{
        //    /***********************
        //     * 带噪声的双频信号通过FIR滤波器后的得到单频信号
        //     * 通过单频信号和双频信号求滤波器的频谱响应
        //     * FIR LPF
        //     * passFreq=50Hz
        //     * stopFreq=100Hz
        //     * 带外衰减40db
        //     * 采样频率1000Hz
        //     * ************************/
        //    double Fs = 1000;
        //    int freqLines = 500; //set frequency resolution here
        //    double df = Fs / 2 / freqLines;
        //    double[] coe = new double[] { 0.00194150175769345, -0.00169793575520208, -0.00426956799325628, -0.00829350380976647, -0.0132303432212115, -0.0181544335557185, -0.0217585679131809, -0.0225134101549763, -0.0189478446411358, -0.00995128109977753, 0.00489205937905203, 0.0250974062726473, 0.0492006450550267, 0.0748863114260257, 0.0992878499995790, 0.119424371366942, 0.132695897659600, 0.137328720011526, 0.132695897659600, 0.119424371366942, 0.0992878499995790, 0.0748863114260257, 0.0492006450550267, 0.0250974062726473, 0.00489205937905203, -0.00995128109977753, -0.0189478446411358, -0.0225134101549763, -0.0217585679131809, -0.0181544335557185, -0.0132303432212115 - 0.00829350380976647, -0.00426956799325628, -0.00169793575520208, 0.00194150175769345 };
        //    double[] sinWave1 = new double[1000];
        //    double[] sinWave2 = new double[1000];
        //    double[] noise = new double[1000];
        //    JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, 10, Fs);
        //    JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, 0, 120, Fs);
        //    ArrayCalculation.Add(sinWave1, sinWave2, ref sinWave1);
        //    JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noise, 0.01);
        //    ArrayCalculation.Add(sinWave1, noise, ref sinWave1);
        //    double[] sinWaveFir = new double[1000];
        //    var fir = new FIRFilter();
        //    fir.Coefficients = coe;
        //    fir.Filter(sinWave1, ref sinWaveFir);
        //    //AnalogWaveformChart.Plot("input waveform", sinWave1);
        //    //AnalogWaveformChart.Plot("output waveform", sinWaveFir);

        //    double[] bodeMag = new double[freqLines];
        //    double[] bodePhase = new double[freqLines];
        //    double[] bodeCoherent = new double[freqLines];
        //    int numOfAverage = (int)Math.Floor((decimal)(sinWave1.Length / 2 / freqLines));
        //    //int numOfAverage = 1;
        //    AverageParam Average = new AverageParam();
        //    Average.Mode = AverageMode.RMS;
        //    Average.Number = numOfAverage;
        //    bodeMag = Measurement.FrequencyResponseFunction.GetMagenitude(sinWave1, sinWaveFir,true, Average);
        //    bodePhase = Measurement.FrequencyResponseFunction.GetPhase(sinWave1, sinWaveFir, true, Average);
        //    bodeCoherent = Measurement.FrequencyResponseFunction.GetCoherent(sinWave1, sinWaveFir, Average);


        //    AnalogWaveformChart.Plot("BodeMagnitude", 0, 1 / df, bodeMag);
        //    AnalogWaveformChart.Plot("BodePhase", 0, 1 / df, bodePhase);
        //    AnalogWaveformChart.Plot("bodeCoherent", 0, 1 / df, bodeCoherent);

        //    //var syncWaveform3 = new double[bodeMag.Length, 2];
        //    //var syncWaveform3T = new double[2, bodeMag.Length];
        //    //ArrayManipulation.Connected_2D_Array(bodeMag, bodeCoherent, ref syncWaveform3);
        //    //ArrayManipulation.Transpose(syncWaveform3, ref syncWaveform3T);
        //    //AnalogWaveformChart.Plot("syncWaveform3T", 0, 1 / df, syncWaveform3T);
        //}


        //[TestMethod()]
        //public void AnalyzeTest_003()
        //{
        //    /***********************
        //     * 带噪声的双频信号通过FIR滤波器后的得到单频信号
        //     * 通过单频信号和双频信号求滤波器的频谱响应
        //     * FIR LPF
        //     * passFreq=50Hz
        //     * stopFreq=100Hz
        //     * 带外衰减40db
        //     * 采样频率1000Hz
        //     * 测试相关性
        //     * ************************/
        //    double Fs = 1000;
        //    int freqLines = 500; //set frequency resolution here
        //    double df = Fs / 2 / freqLines;
        //    double[] coe = new double[] { 0.00194150175769345, -0.00169793575520208, -0.00426956799325628, -0.00829350380976647, -0.0132303432212115, -0.0181544335557185, -0.0217585679131809, -0.0225134101549763, -0.0189478446411358, -0.00995128109977753, 0.00489205937905203, 0.0250974062726473, 0.0492006450550267, 0.0748863114260257, 0.0992878499995790, 0.119424371366942, 0.132695897659600, 0.137328720011526, 0.132695897659600, 0.119424371366942, 0.0992878499995790, 0.0748863114260257, 0.0492006450550267, 0.0250974062726473, 0.00489205937905203, -0.00995128109977753, -0.0189478446411358, -0.0225134101549763, -0.0217585679131809, -0.0181544335557185, -0.0132303432212115 - 0.00829350380976647, -0.00426956799325628, -0.00169793575520208, 0.00194150175769345 };
        //    double[] sinWave1 = new double[1000];
        //    double[] sinWave2 = new double[1000];
        //    double[] noise = new double[1000];
        //    double[] sqre = new double[1000];
        //    JY.DSP.Fundamental.Generation.SquareWave(ref sqre, 1, 50, 50, Fs);
        //    JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, 10, Fs);
        //    JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, 90, 10, Fs);
        //    ArrayCalculation.Add(sinWave1, sinWave2, ref sinWave1);
        //    JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noise, 0.01);
        //    ArrayCalculation.Add(sinWave1, noise, ref sinWave1);
        //    double[] sinWaveFir = new double[1000];
        //    var fir = new FIRFilter();
        //    fir.Coefficients = coe;
        //    fir.Filter(sinWave1, ref sinWaveFir);
        //    AnalogWaveformChart.Plot("input waveform", sinWave1);
        //    AnalogWaveformChart.Plot("output waveform", sinWaveFir);

        //    double[] bodeMag = new double[freqLines];
        //    double[] bodePhase = new double[freqLines];
        //    double[] bodeCoherent = new double[freqLines];
        //    int numOfAverage = (int)Math.Floor((decimal)(sinWave1.Length / 2 / freqLines));
        //    AverageParam Average = new AverageParam();
        //    Average.Mode = AverageMode.RMS;
        //    Average.Number = numOfAverage;
        //    bodeMag = Measurement.FrequencyResponseFunction.GetMagenitude(sinWave1, sqre, true, Average);
        //    bodePhase = Measurement.FrequencyResponseFunction.GetPhase(sinWave1, sqre, true, Average);
        //    bodeCoherent = Measurement.FrequencyResponseFunction.GetCoherent(sinWave1, sinWave2, Average);
        //    AnalogWaveformChart.Plot("BodeMagnitude", 0, 1 / df, bodeMag);
        //    AnalogWaveformChart.Plot("BodePhase", 0, 1 / df, bodePhase);
        //    AnalogWaveformChart.Plot("bodeCoherent", 0, 1 / df, bodeCoherent);
        //}

        //[TestMethod()]
        //public void AnalyzeTest_004()
        //{
        //    /***********************
        //     * 带噪声的双频信号通过FIR滤波器后的得到单频信号
        //     * 通过单频信号和双频信号求滤波器的频谱响应
        //     * FIR LPF
        //     * passFreq=50Hz
        //     * stopFreq=100Hz
        //     * 带外衰减40db
        //     * 采样频率1000Hz
        //     * ************************/
        //    double Fs = 1000;
        //    int freqLines = 500; //set frequency resolution here
        //    double df = Fs / 2 / freqLines;
        //    double[] coe = new double[] { 0.00194150175769345, -0.00169793575520208, -0.00426956799325628, -0.00829350380976647, -0.0132303432212115, -0.0181544335557185, -0.0217585679131809, -0.0225134101549763, -0.0189478446411358, -0.00995128109977753, 0.00489205937905203, 0.0250974062726473, 0.0492006450550267, 0.0748863114260257, 0.0992878499995790, 0.119424371366942, 0.132695897659600, 0.137328720011526, 0.132695897659600, 0.119424371366942, 0.0992878499995790, 0.0748863114260257, 0.0492006450550267, 0.0250974062726473, 0.00489205937905203, -0.00995128109977753, -0.0189478446411358, -0.0225134101549763, -0.0217585679131809, -0.0181544335557185, -0.0132303432212115 - 0.00829350380976647, -0.00426956799325628, -0.00169793575520208, 0.00194150175769345 };
        //    double[] sinWave1 = new double[freqLines];
        //    double[] noise = new double[freqLines];
        //    //冲击信号
        //    sinWave1[freqLines/2] = 1;
        //    JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noise, 0.01);
        //    ArrayCalculation.Add(sinWave1, noise, ref sinWave1);
        //    //滤波
        //    double[] sinWaveFir = new double[freqLines];
        //    var fir = new FIRFilter();
        //    fir.Coefficients = coe;
        //    fir.Filter(sinWave1, ref sinWaveFir);
        //    AnalogWaveformChart.Plot("input waveform", sinWave1);
        //    AnalogWaveformChart.Plot("output waveform", sinWaveFir);
        //    //计算频响
        //    double[] bodeMag = new double[freqLines];
        //    double[] bodePhase = new double[freqLines];
        //    double[] bodeCoherent = new double[freqLines];
        //    int numOfAverage = (int)Math.Floor((decimal)(sinWave1.Length / 2 / freqLines));
        //    AverageParam Average = new AverageParam();
        //    Average.Mode = AverageMode.RMS;
        //    Average.Number = numOfAverage;
        //    bodeMag = Measurement.FrequencyResponseFunction.GetMagenitude(sinWave1, sinWaveFir, true, Average);
        //    bodePhase = Measurement.FrequencyResponseFunction.GetPhase(sinWave1, sinWaveFir, true, Average);
        //    bodeCoherent = Measurement.FrequencyResponseFunction.GetCoherent(sinWave1, sinWaveFir, Average);
        //    AnalogWaveformChart.Plot("BodeMagnitude", 0, 1 / df, bodeMag);
        //    AnalogWaveformChart.Plot("BodePhase", 0, 1 / df, bodePhase);
        //    AnalogWaveformChart.Plot("bodeCoherent", 0, 1 / df, bodeCoherent);

        //    //var syncWaveform3 = new double[freqLines, 2];
        //    //var syncWaveform3T = new double[2, freqLines];
        //    //ArrayManipulation.Connected_2D_Array(bodeMag, bodeCoherent, ref syncWaveform3);
        //    //ArrayManipulation.Transpose(syncWaveform3, ref syncWaveform3T);
        //    //AnalogWaveformChart.Plot("syncWaveform3T", 0, 1 / df, syncWaveform3T);
        //}

        [TestMethod()]
        public void AnalyzeTest_002()
        {
            /***********************
             * 带噪声的双频信号通过FIR滤波器后的得到单频信号
             * 通过单频信号和双频信号求滤波器的频谱响应
             * FIR LPF
             * passFreq=50Hz
             * stopFreq=100Hz
             * 带外衰减40db
             * 采样频率1000Hz
             * ************************/
            double Fs = 1000;
            int freqLines = 500; //set frequency resolution here
            double df = Fs / 2 / freqLines;
            double[] coe = new double[] { 0.00194150175769345, -0.00169793575520208, -0.00426956799325628, -0.00829350380976647, -0.0132303432212115, -0.0181544335557185, -0.0217585679131809, -0.0225134101549763, -0.0189478446411358, -0.00995128109977753, 0.00489205937905203, 0.0250974062726473, 0.0492006450550267, 0.0748863114260257, 0.0992878499995790, 0.119424371366942, 0.132695897659600, 0.137328720011526, 0.132695897659600, 0.119424371366942, 0.0992878499995790, 0.0748863114260257, 0.0492006450550267, 0.0250974062726473, 0.00489205937905203, -0.00995128109977753, -0.0189478446411358, -0.0225134101549763, -0.0217585679131809, -0.0181544335557185, -0.0132303432212115 - 0.00829350380976647, -0.00426956799325628, -0.00169793575520208, 0.00194150175769345 };
            double[] noiseInput = new double[1000];
            double[] noiseAddon = new double[1000];
            double[] FIROutput = new double[1000];
            var fir = new FIRFilter();
            fir.Coefficients = coe;

            double[] analysisInWav = new double[freqLines * 2];
            double[] analysisOutWav = new double[freqLines * 2];
            double[] bodeMag = new double[freqLines];
            double[] bodePhase = new double[freqLines];
            double[] coherent = new double[freqLines];
            int numOfAverage = 10;
            FrequencyResponseFunction FRFAnalysis = new FrequencyResponseFunction();
            FRFAnalysis.Average.Mode = AverageMode.RMS;
            FRFAnalysis.Average.Number = numOfAverage;
            FRFAnalysis.ResetAveraging = true;

            for (int i = 0; i < numOfAverage; i++)
            {
                JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noiseInput, 0.01);
                JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noiseAddon, 0.0001);
                fir.Filter(noiseInput, ref FIROutput);
                ArrayCalculation.Add(FIROutput, noiseAddon, ref FIROutput);
                if (i == 0) FRFAnalysis.Reset();
                FRFAnalysis.Analyze(noiseInput, FIROutput);
                bodeMag = FRFAnalysis.GetMagenitude(true);
                bodePhase = FRFAnalysis.GetPhase(true);
                coherent = FRFAnalysis.GetCoherente();
            }
            bool averageDone = FRFAnalysis.AveragingDone;
            Console.WriteLine("averageDone:{0}", averageDone.ToString());
            AnalogWaveformChart.Plot("BodeMagnitude", 0, 1 / df, bodeMag);
            AnalogWaveformChart.Plot("BodePhase", 0, 1 / df, bodePhase);
            AnalogWaveformChart.Plot("coherent", 0, 1 / df, coherent);

        }

        [TestMethod()]
        public void AnalyzeTest_003()
        {
            /***********************
             * 带噪声的双频信号通过FIR滤波器后的得到单频信号
             * 通过单频信号和双频信号求滤波器的频谱响应
             * FIR LPF
             * passFreq=50Hz
             * stopFreq=100Hz
             * 带外衰减40db
             * 采样频率1000Hz
             * ************************/
            double Fs = 1000;
            int freqLines = 500; //set frequency resolution here
            double df = Fs / 2 / freqLines;
            double[] coe = new double[] { 0.00194150175769345, -0.00169793575520208, -0.00426956799325628, -0.00829350380976647, -0.0132303432212115, -0.0181544335557185, -0.0217585679131809, -0.0225134101549763, -0.0189478446411358, -0.00995128109977753, 0.00489205937905203, 0.0250974062726473, 0.0492006450550267, 0.0748863114260257, 0.0992878499995790, 0.119424371366942, 0.132695897659600, 0.137328720011526, 0.132695897659600, 0.119424371366942, 0.0992878499995790, 0.0748863114260257, 0.0492006450550267, 0.0250974062726473, 0.00489205937905203, -0.00995128109977753, -0.0189478446411358, -0.0225134101549763, -0.0217585679131809, -0.0181544335557185, -0.0132303432212115 - 0.00829350380976647, -0.00426956799325628, -0.00169793575520208, 0.00194150175769345 };
            double[] noiseInput = new double[1000];
            double[] noiseAddon = new double[1000];
            double[] noiseAddon1 = new double[1000];
            double[] FIROutput = new double[1000];
            var fir = new FIRFilter();
            fir.Coefficients = coe;

            double[] analysisInWav = new double[freqLines * 2];
            double[] analysisOutWav = new double[freqLines * 2];
            double[] bodeMag = new double[freqLines];
            double[] bodePhase = new double[freqLines];
            double[] coherent = new double[freqLines];
            int numOfAverage = 10;
            FrequencyResponseFunction FRFAnalysis = new FrequencyResponseFunction();
            FRFAnalysis.Average.Mode = AverageMode.RMS;
            FRFAnalysis.Average.Number = numOfAverage;
            FRFAnalysis.ResetAveraging = true;
                JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noiseInput, 0.01);
            for (int i = 0; i < numOfAverage; i++)
            {
                JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noiseAddon1, 0.0001);
                JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noiseAddon, 0.0001);
                ArrayCalculation.Add(noiseInput, noiseAddon1, ref noiseInput);
                fir.Filter(noiseInput, ref FIROutput);
                ArrayCalculation.Add(FIROutput, noiseAddon, ref FIROutput);
                if (i == 0) FRFAnalysis.Reset();
                FRFAnalysis.Analyze(noiseInput, FIROutput);
                bodeMag = FRFAnalysis.GetMagenitude(true);
                //bodePhase = FRFAnalysis.GetPhase(true);
                coherent = FRFAnalysis.GetCoherente();
            }
            bool averageDone = FRFAnalysis.AveragingDone;
            Console.WriteLine("averageDone:{0}", averageDone.ToString());
            AnalogWaveformChart.Plot("BodeMagnitude", 0, 1 / df, bodeMag);
            //AnalogWaveformChart.Plot("BodePhase", 0, 1 / df, bodePhase);
            AnalogWaveformChart.Plot("coherent", 0, 1 / df, coherent);

        }
    }
}