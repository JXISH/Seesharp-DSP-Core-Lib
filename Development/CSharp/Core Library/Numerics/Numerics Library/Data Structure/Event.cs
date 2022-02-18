using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JXI.Public.Struct
{

    #region------------------------- 公共委托和事件参数定义 -------------------------

    /// <summary>
    /// 参数值改变事件的参数。
    /// </summary>
    public class ValueChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Old value.
        /// </summary>
        public object OldValue { get; set; }

        /// <summary>
        /// New value.
        /// </summary>
        public object NewValue { get; set; }

        #region ------------------- 构造函数 -------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newVaue"></param>
        public ValueChangedEventArgs(object oldValue, object newVaue)
        {
            this.OldValue = oldValue;
            this.NewValue = newVaue;
        }

        #endregion
    }

    /// <summary>
    /// 参数改变时的委托。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ParamValueChangedEventHandler(object sender, ValueChangedEventArgs e);

    /// <summary>
    /// 任务数据有效的事件参数。
    /// </summary>
    public class TaskDataAvailableEventArgs : EventArgs
    {
        private int _count;
        /// <summary>
        /// 数据计数。
        /// </summary>
        public int Count { get { return _count; } }

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="count"></param>
        public TaskDataAvailableEventArgs(int count = 0) { _count = count; }

        #endregion
    }

    /// <summary>
    /// 任务数据更新的委托。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TaskDataAvailableEventHandler(object sender, TaskDataAvailableEventArgs e);

    /// <summary>
    /// 任务心跳事件的参数。
    /// </summary>
    public class TaskHeartbeatTriggeredEventArgs : EventArgs
    {
        private int _count;
        /// <summary>
        /// 心跳计数器。
        /// </summary>
        public int Count { get { return _count; } }

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public TaskHeartbeatTriggeredEventArgs(int count) { _count = count; }

        #endregion
    }

    /// <summary>
    /// 任务心跳时的委托。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TaskHeartbeatTriggeredEventHandler(object sender, TaskHeartbeatTriggeredEventArgs e);

    /// <summary>
    /// 任务完成的事件参数。
    /// </summary>
    public class TaskCompletedEventArgs : EventArgs
    {
        private Exception _error;
        /// <summary>
        /// 任务执行的错误信息。如果任务执行期间发生错误（异常终止），则为实例化的异常类对象，否则为null。
        /// </summary>
        public Exception Error { get { return _error; } }

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public TaskCompletedEventArgs() {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TaskCompletedEventArgs(string message, Exception innerException = null)
        {
            _error = new Exception(message, innerException);
        }

        #endregion
    }

    /// <summary>
    /// 任务完成的委托。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TaskCompletedEventHandler(object sender, TaskCompletedEventArgs e);

    /// <summary>
    /// 异步任务（如基于Thread/Task的引擎）执行出现异常的事件参数。
    /// </summary>
    public class AggregateExceptionOccuredEventArgs : EventArgs
    {

        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 出现异常的时间。
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 出现异常的任务名称。
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 异常信息。
        /// </summary>
        public Exception Exception { get; set; }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="time"></param>
        /// <param name="taskName"></param>
        /// <param name="exception"></param>
        public AggregateExceptionOccuredEventArgs(DateTime time, string taskName, Exception exception)
        {
            this.Time = time;
            this.TaskName = taskName;
            this.Exception = exception;
        }
        #endregion

    }

    /// <summary>
    /// 异步任务（如基于Thread/Task的引擎）执行出现异常时的委托。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void AggregateExceptionOccuredEventHandler(object sender, AggregateExceptionOccuredEventArgs e);


    #endregion

}
