using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JXI.MKL
{
    internal class Constants
    {
#if LINUX
        public const string MKLCoreFilePath = @"mkl_rt.so";
#else
        public const string MKLCoreFilePath = @"mkl_rt.dll";
#endif

    }
}
