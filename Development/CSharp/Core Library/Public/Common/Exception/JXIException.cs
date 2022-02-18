using System;

namespace SeeSharpTools.JXI.Exception
{
    /// <summary>
    /// JXI DSP Inner Exception
    /// </summary>
    public class JXIInnerException:ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public JXIInnerException(string msg):base(msg)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public JXIInnerException()
        {

        }
    }

    /// <summary>
    /// JXI DSP User Buffer Exception
    /// </summary>
    public class JXIUserBufferException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public JXIUserBufferException(string msg) : base(msg)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public JXIUserBufferException()
        {

        }

    }

    /// <summary>
    /// JXI DSP Param Exception
    /// </summary>
    public class JXIParamException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public JXIParamException(string msg) : base(msg)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public JXIParamException()
        {

        }

    }
}
