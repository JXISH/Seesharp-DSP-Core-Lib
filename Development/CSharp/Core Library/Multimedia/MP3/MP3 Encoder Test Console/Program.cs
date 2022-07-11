using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Diagnostics;
using SeeSharpTools.JXI.Multimedia.MP3;

namespace JXIEncodeMP3_Test
{
    class Program
    {
        private static string testFilePath = @"D:\IQdata\wav\test111_1s.wav";
        private static   MP3EncoderLegacy mp3;
        private static EasyMp3Encoder mp3Encoder;
        private static ConcurrentQueue<byte[]> _q;
        private static double dataLength = 0.2;

        static void Main(string[] args)
        {
            //生成信号
            WaveFileWriter.CreateWaveFile16(testFilePath, new SignalGenerator(11025, 1)
            {
                Type = SignalGeneratorType.Sweep,
                Frequency = 500,
                FrequencyEnd = 2000,
                Gain = 0.2f,
                SweepLengthSecs = 20
            }.Take(TimeSpan.FromSeconds(dataLength)));

            // 读取数据
            FileStream _draFilewave = new FileStream(testFilePath, FileMode.Open);
            byte[] audioData = new byte[_draFilewave.Length-46];
            _draFilewave.Read(audioData, 46, audioData.Length-46);
            _draFilewave.Close();
            _draFilewave.Dispose();

            //var readWaveData = new WaveFileReader(testFilePath);
            //byte[] audioData = new byte[readWaveData.Length];
            //readWaveData.Read(audioData, 0, audioData.Length);

            // 数据转换
            short[] I16AudioData = new short[(int)(11025* dataLength)];
            Buffer.BlockCopy(audioData, 0, I16AudioData, 0, I16AudioData.Length*sizeof(short));

            //mp3 = new MP3EncoderLegacy(I16AudioData.Length, 44100, 1, 32);
            //mp3.Proccess(I16AudioData);
            //byte[] outData = mp3.OutPutEncodedData.ToArray();


            _q = new ConcurrentQueue<byte[]>();

        
            for (int i = 0; i < 10; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                mp3Encoder = new EasyMp3Encoder(11025, 1, 32000);              
                mp3Encoder.Enqueue(I16AudioData, (int)(1000 * dataLength));
                mp3Encoder.EndCurrentStream();
                var  outData = new byte[(int)(4000 * dataLength)];
                mp3Encoder.Dequeue(outData, (int)(100000 * dataLength));

                Debug.WriteLine("解调时间:" +sw.ElapsedMilliseconds.ToString());
                sw.Stop();
                _q.Enqueue(outData);
            
            }

            if (mp3Encoder!=null) { mp3Encoder.Dispose(); }

            Console.WriteLine("测试循环次数=" +_q.Count());



            #region ------单个解码测试------

            #endregion
            //mp3Encoder = new EasyMp3Encoder(44100, 1, 32000);
            //mp3Encoder.EndCurrentStream();
            //mp3Encoder.Enqueue(I16AudioData, (int)(1000 * dataLength));
            //outData = new byte[(int)(4000 * dataLength)];
            //mp3Encoder.Dequeue(outData, (int)(1000 * dataLength));
            //_q.Enqueue(outData);

            //FileStream _draFilemp3 = new FileStream(@"D:\IQdata\wav\test121.mp3", FileMode.Create);
            //_draFilemp3.Write(outData, 0, outData.Length);
            //_draFilemp3.Close();



            Console.WriteLine("解调成功！");

            Console.ReadLine();
        }
    }
}
