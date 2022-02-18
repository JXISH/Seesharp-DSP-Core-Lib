using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeeSharpTools.JXI.Public.Struct
{
    /// <summary>
    /// Task对象的公共方法。
    /// </summary>
    public static class TaskUtility
    {

        /// <summary>
        /// 等待指定的Task结束运行，若超时则抛出异常。
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="engineName"></param>
        /// <param name="timeout"></param>
        public static void WaitForEngineComplete(Task engine, string engineName, int timeout = 30000)
        {
            try
            {
                // 等待Task结束或超时。
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                while (engine.Status == TaskStatus.Running && stopwatch.ElapsedMilliseconds < timeout) { Thread.Sleep(0); }

                // 若超时，则抛出超时异常。
                if (engine.Status == TaskStatus.Running) { throw new TimeoutException(string.Format(" Stopping {0} timeout.", engineName)); }
            }
            catch (Exception exception)
            {
                // 输出调试信息以协助调试。
                Debug.WriteLine(DateTime.Now + " " + exception.Message + Environment.NewLine + exception.StackTrace + exception.InnerException?.Message);

                // 仍然抛出异常。
                throw;
            }
        }

    }
}
