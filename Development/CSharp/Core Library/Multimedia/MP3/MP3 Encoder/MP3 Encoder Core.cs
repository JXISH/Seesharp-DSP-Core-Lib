using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SeeSharpTools.JXI.Multimedia.MP3
{
    internal class MP3Core
    {

        [StructLayout(LayoutKind.Sequential)]
        internal struct DecoderConfig
        {

            [MarshalAs(UnmanagedType.U4)]
            public uint dwConfigID;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwStrutVersion;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwStructSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwSampleRate;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwReSampleRate;
            [MarshalAs(UnmanagedType.I4)]
            public int nMode;

            [MarshalAs(UnmanagedType.U4)]
            public uint dwBitrate;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwMaxBitrate;
            [MarshalAs(UnmanagedType.I4)]
            public int nPreset;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwMpegVersion;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwPsyModel;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwEmphasis;

            [MarshalAs(UnmanagedType.I4)]
            public int bCopyright;
            [MarshalAs(UnmanagedType.I4)]
            public int bCRC;
            [MarshalAs(UnmanagedType.I4)]
            public int bOriginal;
            [MarshalAs(UnmanagedType.I4)]
            public int bPrivate;

            [MarshalAs(UnmanagedType.I4)]
            public int bWritVBRHeader;
            [MarshalAs(UnmanagedType.I4)]
            public int bEnableVBR;
            [MarshalAs(UnmanagedType.I4)]
            public int nVBRQuality;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwVbrAbr_bps;
            [MarshalAs(UnmanagedType.I4)]
            public int nVbrMethod;
            [MarshalAs(UnmanagedType.I4)]
            public int bNoRes;

            [MarshalAs(UnmanagedType.I4)]
            public int bStrictlso;
            [MarshalAs(UnmanagedType.I2)]
            public short nQuality;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 237)]
            public string reserver;
        }

        #region -------- DLL函数定义 --------

        private const string DllName = "lame_enc.dll";

        private const CallingConvention DllCallingConvention = CallingConvention.Cdecl;

        //beInitStream(void *beConfig, uint32_t *rawBlockSize, uint32_t *decodedBlockSize, uint32_t *streamHandle);
        // This function is the first to call before starting an encoding stream.
        // 初始化
        [DllImport(DllName, EntryPoint = "beInitStream", CallingConvention = DllCallingConvention)]
        public static extern int Initialize(ref DecoderConfig decoderConfig, ref uint rawBlockSize, ref uint decodedBlockSize, ref uint streamHandle);

        //uint32_t beEncodeChunk(uint32_t stream handle, uint32_t data size, const int16_t *data in, uint8_t *encoded data out, uint32_t *encoded data size);
        // Encodes a chunk of samples. Please note that if you have set the output to generate mono MP3 files you must feed beEncodeChunk() with mono samples!
        // 数据进行编码
        [DllImport(DllName, EntryPoint = "beEncodeChunk", CallingConvention = DllCallingConvention)]
        public static extern int MP3EncodeChunk(uint stramHandle, uint dataSize, short[] inputData, byte[] encodedData, out uint encodeDataSize);

        // uint32_t beDeinitStream(uint32_t stream handle, uint8_t *encoded data out, uint32_t *encoded data size);
        //This function should be called after encoding the last chunk in order to flush the encoder. It writes any encoded data that still might be left inside the encoder to the output buffer. This function should NOT be called unless you have encoded all of the chunks in your stream.
        // 对最后一块非整数倍的数据进行处理
        [DllImport(DllName, EntryPoint = "beDeinitStream", CallingConvention = DllCallingConvention)]
        public static extern int MP3DeinitEcodeing(uint stramHandle, byte[] encodedData, out uint encodeDataSize);

        // uint32_t beCloseStream(uint32_t stream handle);
        // Last function to be called when finished encoding a stream. Should unlike beDeinitStream() also be called if the encoding is canceled.
        // 关闭数据流
        [DllImport(DllName, EntryPoint = "beCloseStream", CallingConvention = DllCallingConvention)]
        public static extern int MP3CloseStream(uint stramHandle);

        #endregion

    }
}
