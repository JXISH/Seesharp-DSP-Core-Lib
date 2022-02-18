using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.IO;

namespace SeeSharpTools.JXI.FileIO.VectorFile
{
    /// <summary>
    /// KernelFileStream
    /// </summary>
    public class KernelFileStream
    {
        private bool _fileOpened = false;
        private string _fileFullPath = string.Empty;
        private SafeFileHandle _handle = null;

        #region ------------------- Constructor and Destructor -------------------

        /// <summary>
        /// 
        /// </summary>
        public KernelFileStream()
        {
            _fileOpened = false;
            _fileFullPath = string.Empty;
            _handle = null;
        }

        /// <summary>
        /// 
        /// </summary>
        ~KernelFileStream()
        { Close(); }

        #endregion

        #region ------------------- Public Properties -------------------

        /// <summary>
        /// 获取或设置文件的当前读写位置，即从起始的字节数。
        /// </summary>
        public long Position
        {
            get
            { return Seek(0, SeekOrigin.Current); }
            set
            { Seek(value, SeekOrigin.Begin); }
        }

        #endregion

        #region ------------------- Public Methods -------------------

        /// <summary>
        /// Open
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="mode"></param>
        /// <param name="fileAccess"></param>
        /// <param name="disableBuffering"></param>
        public void Open (string fileName, FileMode mode, FileAccess fileAccess, bool disableBuffering = false)
        {
            if (_fileOpened)
            { throw new InvalidOperationException("Call Close() before open another file."); }

            var creationDisposition = CreationDisposition.OpenExisting;
            var desiredAccess = DesiredAccess.GenericRead;

            #region ------- Convert input "FileMode" and "FileAccess" enum to correspoding kernal enum values---------

            switch (mode)
            {
                case FileMode.CreateNew:
                    {
                        creationDisposition = CreationDisposition.New;
                        break;
                    }
                case FileMode.Create:
                    {
                        creationDisposition = CreationDisposition.CreateAlways;
                        break;
                    }
                case FileMode.Open:
                    {
                        creationDisposition = CreationDisposition.OpenExisting;
                        break;
                    }
                case FileMode.OpenOrCreate:
                    {
                        creationDisposition = CreationDisposition.OpenAlways;
                        break;
                    }
                case FileMode.Truncate:
                    {
                        creationDisposition = CreationDisposition.TruncateExisting;
                        break;
                    }
                case FileMode.Append:
                    {
                        throw new NotSupportedException("Append mode is not directly supported by kernal.");
                    }
            }


            switch (fileAccess)
            {
                case FileAccess.Read:
                    {
                        desiredAccess = DesiredAccess.GenericRead;
                        break;
                    }
                case FileAccess.Write:
                    {
                        desiredAccess = DesiredAccess.GenericWrite;
                        break;
                    }
                case FileAccess.ReadWrite:
                    {
                        desiredAccess = DesiredAccess.GenericRead | DesiredAccess.GenericWrite;
                        break;
                    }
            }

            #endregion

            FlagsAndAttributes flags = 0;
            if (disableBuffering) { flags = FlagsAndAttributes.NoBuffering; }

            SafeFileHandle handle = CreateFile
                (fileName, desiredAccess, ShareMode.Read | ShareMode.Write, IntPtr.Zero, creationDisposition, flags, IntPtr.Zero);
            if (handle.IsInvalid)
                { Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error()); } 

            _handle = handle;
            _fileFullPath = fileName;
            _fileOpened = true;

        }

        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            if (_fileOpened)
            {
                // Calling SafeFileHandle.Close instead of Win32 CloseHandle (which is what SafeFileHandle.Close does)
                _handle.Close();

                _fileFullPath = string.Empty;
                _fileOpened = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public long Seek(long offset, SeekOrigin origin)
        {
            long newFilePointer = 0;
            SeekRefPosition refPosition = SeekRefPosition.Begin;

            #region ------- Convert input "SeekOrigin" enum to correspoding kernal enum values---------
            switch (origin)
            {
                case SeekOrigin.Begin:
                    {
                        refPosition = SeekRefPosition.Begin;
                        break;
                    }
                case SeekOrigin.Current:
                    {
                        refPosition = SeekRefPosition.Current;
                        break;
                    }
                case SeekOrigin.End:
                    {
                        refPosition = SeekRefPosition.End;
                        break;
                    }
            }
            #endregion

            if (_fileOpened)
            {
                bool success = SetFilePointerEx(_handle, offset, out newFilePointer, refPosition);
                if (!success)
                { Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error()); }
            }

            return newFilePointer;
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="data"></param>
        /// <param name="numberOfBytesToRead"></param>
        public void Read(IntPtr data, int numberOfBytesToRead)
        {

            #region -------------------- Validate input parameters---------------------

            if (numberOfBytesToRead <= 0)
            { throw new ArgumentException("numberOfBytesToRead must be greater than 0"); }

            if (data == IntPtr.Zero)
            { throw new ArgumentNullException("Empty data buffer."); }

            if(_fileOpened != true)
            { throw new InvalidOperationException("File is not open."); }

            #endregion

            uint numberOfBytesRead;
            bool success = ReadFile(_handle, data, (uint)numberOfBytesToRead, out numberOfBytesRead, IntPtr.Zero);

            if (!success)
            { Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error()); }

            if (numberOfBytesRead != numberOfBytesToRead)
            {
                string msg = "Number of bytes actually read does not match requested." + Environment.NewLine +
                    "Requested:" + numberOfBytesToRead.ToString() + Environment.NewLine +
                    "Actual Read:" + numberOfBytesRead.ToString() + Environment.NewLine +
                    "You might have reached the end of the file.";
                throw new NotImplementedException(msg);
            }
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="data"></param>
        /// <param name="numberOfBytesToWrite"></param>
        public void Write(IntPtr data, int numberOfBytesToWrite)
        {

            #region -------------------- Validate input parameters---------------------

            if (numberOfBytesToWrite <= 0)
            { throw new ArgumentException("numberOfBytesToWrite must be greater than 0."); }

            if (data == IntPtr.Zero)
            { throw new ArgumentNullException("Empty data buffer."); }

            if (_fileOpened != true)
            { throw new InvalidOperationException("File is not open."); }

            #endregion

            uint numberOfBytesWritten;
            bool success = WriteFile(_handle, data, (uint)numberOfBytesToWrite, out numberOfBytesWritten, IntPtr.Zero);

            if (!success)
            { Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error()); }

            if (numberOfBytesWritten != numberOfBytesToWrite)
            {
                string msg = "Number of bytes actually written does not match requested." + Environment.NewLine +
                    "Requested:" + numberOfBytesToWrite.ToString() + Environment.NewLine +
                    "Actual written:" + numberOfBytesWritten.ToString() + Environment.NewLine +
                    "You might have run out of disk space.";
                throw new NotImplementedException(msg);
            }
        }


        #endregion

        #region ------------------- Imported Kernel Methods and Data Types -------------------

        private const string DllName = "Kernel32.dll";

        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string lpFileName, DesiredAccess dwDesiredAccess, ShareMode dwShareMode, IntPtr lpSecurityAttributes,
            CreationDisposition dwCreationDisposition, FlagsAndAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool ReadFile(
            SafeFileHandle fileHandle, IntPtr buffer, uint numberOfBytesToRead, out uint numberOfBytesRead, IntPtr lpOverlapped);

        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool WriteFile(
            SafeFileHandle fileHandle, IntPtr buffer, uint numberOfBytesToWrite, out uint numberOfBytesWritten, IntPtr lpOverlapped);

        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SetFilePointerEx(
            SafeFileHandle handle, long distanceToMove, out long newFilePointer, SeekRefPosition dwMoveMethod);

        [Flags]
        enum DesiredAccess : uint
        {
            GenericRead = 0x80000000,
            GenericWrite = 0x40000000,
            GenericExecute = 0x20000000,
            GenericAll = 0x10000000
        }

        [Flags]
        enum ShareMode : uint
        {
            None = 0x00000000,
            Read = 0x00000001,
            Write = 0x00000002,
            Delete = 0x00000004
        }

        enum CreationDisposition : uint
        {
            New = 1,
            CreateAlways = 2,
            OpenExisting = 3,
            OpenAlways = 4,
            TruncateExisting = 5
        }

        [Flags]
        enum FlagsAndAttributes : uint
        {
            ReadOnly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,
            Temporary = 0x00000100,
            SparseFile = 0x00000200,
            ReparsePoint = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            NotContentIndexed = 0x00002000,
            Encrypted = 0x00004000,
            Write_Through = 0x80000000,
            Overlapped = 0x40000000,
            NoBuffering = 0x20000000,
            RandomAccess = 0x10000000,
            SequentialScan = 0x08000000,
            DeleteOnClose = 0x04000000,
            BackupSemantics = 0x02000000,
            PosixSemantics = 0x01000000,
            OpenReparsePoint = 0x00200000,
            OpenNoRecall = 0x00100000,
            FirstPipeInstance = 0x00080000
        }

        enum SeekRefPosition : uint
        {
            Begin = 0,
            Current = 1,
            End = 2
        }

        #endregion

    }
}
