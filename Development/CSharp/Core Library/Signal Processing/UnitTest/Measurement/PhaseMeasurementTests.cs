using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SeeSharpTools.JXI.SignalProcessing.Measurement;

namespace SeeSharpTools.JXI.SignalProcessing.Measurement.Tests
{
    /// <summary>
    /// PhaseMeasurementTests 的摘要说明
    /// </summary>
    [TestClass]
    public class PhaseMeasurementTests
    {
        public PhaseMeasurementTests()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod_real_001()
        {
            /**************************
            * 产生相位不同的正弦波
            * 计算相位差
            * ***************************/
            int length = 1000;
            double amp = 1.0;
            double frequency = 100;
            double sampleRate = 10000;
            double phase1 = 133;
            double phase2 = 100;
            double[] waveform1 = new double[length];
            double[] waveform2 = new double[length];
            JY.DSP.Fundamental.Generation.SineWave(ref waveform1, amp, phase1, frequency, sampleRate);
            JY.DSP.Fundamental.Generation.SineWave(ref waveform2, amp, phase2, frequency, sampleRate);

            double phaseErr=PhaseMeasurements.CalPhaseShift(waveform1, waveform2);


        }
    }
}
