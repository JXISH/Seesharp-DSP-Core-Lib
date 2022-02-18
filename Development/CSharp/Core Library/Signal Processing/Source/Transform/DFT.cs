#define dftIPP
//#define dftMKL
//#define dftFFTW

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using SeeSharpTools.JXI.Numerics;

namespace SeeSharpTools.JXI.SignalProcessing.Transform
{
	/// <summary>
	/// 离散Fourier变换
	/// </summary>
	public class DFT : IDisposable
	{
		#region ---- 构造和引用管理 ----

		/// <summary>
		/// DFTI 唯一实例，用非null表示已经初始化过了。
		/// </summary>
		private static DFT _onlyInstance = null;

		/// <summary>
		/// Descriptor Config Settings, Descriptor Key
		/// </summary>
		private struct DFTConfig
		{
			public int Samples;         // FFT点数
			public int Precision;       // 数据类型
			public int Placement;       // FFT变换结果存储位置
			public int Domain;          // FFT变换作用域
			public int Direction;       // FFT / Inverse FFT

			public DFTHandle Handle;    // 执行句柄
		}

		/// <summary>
		/// DFT Working Parameters
		/// </summary>
		private struct DFTHandle
		{
			public IntPtr pDFTSpec;         // DFT Config Buffer
			public IntPtr pDFTWorkBuf;      // DFT Computing Temp Buffer
			public IntPtr pDFTWorkBuf2;     // DFT Computing Temp Buffer
		}

		/// <summary>
		/// All initialzied DFTI Descriptor
		/// </summary>
		private readonly List<DFTConfig> _dftDescriptors;

		/// <summary>
		/// Construct, Creat the descriptor table
		/// </summary>
		private DFT()
		{
			_dftDescriptors = new List<DFTConfig>();
		}

		/// <summary>
		/// Release all the descriptors
		/// </summary>
		public void Dispose()
		{
			int error;
			foreach (DFTConfig descriptor in _dftDescriptors)
			{
				DFTHandle handle = descriptor.Handle;
#if dftMKL
                error = DftiFreeDescriptor(ref handle.pDFTSpec);
#endif
#if dftIPP
				ippsFree(handle.pDFTWorkBuf);
				ippsFree(handle.pDFTSpec);
#endif
#if dftFFTW
				if (descriptor.Precision == DOUBLE)
				{
					fftwDestroyPlan_d(handle.pDFTSpec);
					fftwFree_d(handle.pDFTWorkBuf);
					if (handle.pDFTWorkBuf2 != IntPtr.Zero) { fftwFree_d(handle.pDFTWorkBuf2); }
				}
				else if (descriptor.Precision == SINGLE)
				{
					fftwDestroyPlan_f(handle.pDFTSpec);
					fftwFree_f(handle.pDFTWorkBuf);
					if (handle.pDFTWorkBuf2 != IntPtr.Zero) { fftwFree_f(handle.pDFTWorkBuf2); }
				}
#endif
			}
			_dftDescriptors.Clear();
		}

		/// <summary>
		/// 析构
		/// </summary>
		~DFT()
		{
			Dispose();
		}

		/// <summary>
		/// Get DFT Descriptor
		/// </summary>
		private int GetDFTDescriptor(int fftSampleCount, int precision, int placement, int direction, out DFTHandle handle)
		{
			int error = 0;

			lock (_dftDescriptors)
			{
				//存在该Key则直接返回
				foreach (DFTConfig config in _dftDescriptors)
				{
					if (DFTExist(config, fftSampleCount, precision, placement, direction, DFT.COMPLEX))
					{
						handle = config.Handle;
						return (int)error;
					}
				}

				//不存在，则创建并配置，保存后返回                
				DFTConfig descriptor;
				descriptor.Samples = fftSampleCount;
				descriptor.Precision = precision;
				descriptor.Placement = placement;
				descriptor.Direction = direction;
				descriptor.Domain = DFT.COMPLEX;
				descriptor.Handle.pDFTSpec = IntPtr.Zero;
				descriptor.Handle.pDFTWorkBuf = IntPtr.Zero;
				descriptor.Handle.pDFTWorkBuf2 = IntPtr.Zero;

#if dftMKL
				IntPtr dftSpecHandle = IntPtr.Zero;
                double forwardscale = 1.0 / descriptor.Samples;
                double backwordscale = 1.0;
                error = DftiCreateDescriptor(ref dftSpecHandle, descriptor.Precision, descriptor.Domain, 1, descriptor.Samples);
                error = DftiSetValue(dftSpecHandle, DFT.PLACEMENT, descriptor.Placement);
                // Set SCALE Error on x64
                //error = DftiSetValue(dftSpecHandle, JXIDFT.FORWARD_SCALE, forwardscale);
                //error = DftiSetValue(dftSpecHandle, JXIDFT.BACKWARD_SCALE, backwordscale);

                error = DftiCommitDescriptor(dftSpecHandle);

                if (descriptor.Precision == DFT.DOUBLE)
                {
                    double getForwardscale = 0;
                    double getBackwordscale = 0;
                    DftiGetValue(dftSpecHandle, DFT.FORWARD_SCALE, ref getForwardscale);
                    DftiGetValue(dftSpecHandle, DFT.BACKWARD_SCALE, ref getBackwordscale);
                }
                else if (descriptor.Precision == DFT.SINGLE)
                {
                    float getForwardscale = 0;
                    float getBackwordscale = 0;
                    DftiGetValue(dftSpecHandle, DFT.FORWARD_SCALE, ref getForwardscale);
                    DftiGetValue(dftSpecHandle, DFT.BACKWARD_SCALE, ref getBackwordscale);
                }

                descriptor.Handle.pDFTSpec = dftSpecHandle;
                descriptor.Handle.pDFTWorkBuf = IntPtr.Zero;
#endif
#if dftIPP
				IntPtr pDFTInitBuf;
				int sizeDFTSpec = 0, sizeDFTInitBuf = 0, sizeDFTWorkBuf = 0;

				// Query to get buffer sizes
				if (precision == DOUBLE)
				{
					error = ippsDFTGetSize_C_64fc(descriptor.Samples, Flag.IPP_FFT_DIV_FWD_BY_N, IppHintAlgorithm.ippAlgHintNone, out sizeDFTSpec, out sizeDFTInitBuf, out sizeDFTWorkBuf);
				}
				else if (precision == SINGLE)
				{
					error = ippsDFTGetSize_C_32fc(descriptor.Samples, Flag.IPP_FFT_DIV_FWD_BY_N, IppHintAlgorithm.ippAlgHintNone, out sizeDFTSpec, out sizeDFTInitBuf, out sizeDFTWorkBuf);
				}

				// Alloc DFT buffers
				pDFTInitBuf = ippsMalloc_8u(sizeDFTInitBuf);
				descriptor.Handle.pDFTSpec = ippsMalloc_8u(sizeDFTSpec);
				descriptor.Handle.pDFTWorkBuf = ippsMalloc_8u(sizeDFTWorkBuf);

				// Initialize DFT
				if (precision == DOUBLE)
				{
					error = ippsDFTInit_C_64fc(descriptor.Samples, Flag.IPP_FFT_DIV_FWD_BY_N, IppHintAlgorithm.ippAlgHintNone, descriptor.Handle.pDFTSpec, pDFTInitBuf);
				}
				else if (precision == SINGLE)
				{
					error = ippsDFTInit_C_32fc(descriptor.Samples, Flag.IPP_FFT_DIV_FWD_BY_N, IppHintAlgorithm.ippAlgHintNone, descriptor.Handle.pDFTSpec, pDFTInitBuf);
				}

				// Free Init buffer
				ippsFree(pDFTInitBuf);
#endif
#if dftFFTW
				IntPtr _fftIn = IntPtr.Zero;      // fft workspace, input
				IntPtr _fftOut = IntPtr.Zero;      // fft workspace, input
				IntPtr _fftPlan = IntPtr.Zero;       // fft configuration

				// Query to get buffer sizes
				if (precision == DOUBLE)
				{
					_fftIn = fftwAllocComplex_d(descriptor.Samples);
					if (descriptor.Placement == DFT.NOT_INPLACE)
					{
						_fftOut = fftwAllocComplex_d(descriptor.Samples);
					}
					else if (descriptor.Placement == DFT.INPLACE)
					{
						_fftOut = _fftIn;
					}
					_fftPlan = fftwCreatePlan_C2C_d(descriptor.Samples, _fftIn, _fftOut, (fftwDirection)descriptor.Direction, fftwFlags.Measure);
				}
				else if (precision == SINGLE)
				{
					_fftIn = fftwAllocComplex_f(descriptor.Samples);
					if (descriptor.Placement == DFT.NOT_INPLACE)
					{
						_fftOut = fftwAllocComplex_f(descriptor.Samples);
					}
					else if (descriptor.Placement == DFT.INPLACE)
					{
						_fftOut = _fftIn;
					}
					_fftPlan = fftwCreatePlan_C2C_f(descriptor.Samples, _fftIn, _fftOut, (fftwDirection)descriptor.Direction, fftwFlags.Measure);
				}

				descriptor.Handle.pDFTSpec = _fftPlan;
				descriptor.Handle.pDFTWorkBuf = _fftIn;
				if (descriptor.Placement == DFT.NOT_INPLACE)
				{
					descriptor.Handle.pDFTWorkBuf = _fftOut;
				}
#endif

				//保存描述符并返回
				if (error == 0) { _dftDescriptors.Add(descriptor); }

				handle = descriptor.Handle;
				return error;
			}
		}

		/// <summary>
		/// Get the only DFTI Instance。
		/// </summary>
		internal static DFT GetInstance()
		{
			return _onlyInstance ?? (_onlyInstance = new DFT());
		}

		/// <summary>
		/// 判断 是否有相同的配置
		/// </summary>
		private static bool DFTExist(DFTConfig config, int samples, int precision, int placement, int direction, int domain)
		{
			return (config.Samples == samples) && (config.Precision == precision) && (config.Placement == placement) && (config.Direction == direction) && (config.Domain == domain);
		}

		#endregion

		/// <summary>
		/// Complex DFT
		/// </summary>
		public static int ComputeForwardShifted(Complex[] x_in, Complex[] x_out)
		{
			Complex[] fftTemp = new Complex[x_in.Length];
			int error = ComputeForward(x_in, fftTemp);
			int k = x_in.Length / 2;

			Vector.ArrayCopy(fftTemp, (x_in.Length - k), x_out, 0,k);
			Vector.ArrayCopy(fftTemp, 0, x_out, k, (x_in.Length - k));

			return error;
		}

		/// <summary>
		/// Complex DFT
		/// </summary>
		public static int ComputeForward(Complex[] x_in, Complex[] x_out)
		{
			GCHandle inputGC = GCHandle.Alloc(x_in, GCHandleType.Pinned);
			IntPtr inputaddress = inputGC.AddrOfPinnedObject();

			GCHandle outputGC = GCHandle.Alloc(x_out, GCHandleType.Pinned);
			IntPtr outputaddress = outputGC.AddrOfPinnedObject();

			int error;
			DFTHandle descriptor;
			error = GetInstance().GetDFTDescriptor(x_in.Length, DFT.DOUBLE, DFT.NOT_INPLACE, DFT.FORWARD, out descriptor);
#if dftMKL
            error = DftiComputeForward(descriptor.pDFTSpec, inputaddress, outputaddress);
			int length = x_in.Length;
			double scale = 1.0 / length;
			cblas_zdscal(length, scale, outputaddress, 1);
#endif
#if dftIPP
			error = ippsDFTFwd_CToC_64fc(inputaddress, outputaddress, descriptor.pDFTSpec, descriptor.pDFTWorkBuf);
#endif
#if dftFFTW
			if ((fftwAlignmentOf_d(inputaddress) == fftwAlignmentOf_d(descriptor.pDFTWorkBuf))
				&& (fftwAlignmentOf_d(outputaddress) == fftwAlignmentOf_d(descriptor.pDFTWorkBuf2)))
			{
				fftwExecute_C2C_d(descriptor.pDFTSpec, inputaddress, outputaddress);
			}
			else
			{
				memcpy(descriptor.pDFTWorkBuf, inputaddress, (UIntPtr)(x_in.Length * 2 * sizeof(double)));
				fftwExecute_d(descriptor.pDFTSpec);
				memcpy(outputaddress, descriptor.pDFTWorkBuf, (UIntPtr)(x_out.Length * 2 * sizeof(double)));
			}
			Vector.ArrayScale(x_out, 1.0f / x_in.Length);
#endif

			inputGC.Free();
			outputGC.Free();

			return error;
		}

		/// <summary>
		/// Complex DFT
		/// </summary>
		public static int ComputeForwardShifted(Complex[] x_inout)
		{
			Complex[] fftTemp = new Complex[x_inout.Length];
			int error = ComputeForward(x_inout, fftTemp);
			int k = x_inout.Length / 2;

			Vector.ArrayCopy(fftTemp, (x_inout.Length - k), x_inout, 0, k);
			Vector.ArrayCopy(fftTemp, 0, x_inout, k, (x_inout.Length - k));

			return error;
		}

		/// <summary>
		/// Complex DFT
		/// </summary>
		public static int ComputeForward(Complex[] x_inout)
		{
			GCHandle inoutGC = GCHandle.Alloc(x_inout, GCHandleType.Pinned);
			IntPtr inoutaddress = inoutGC.AddrOfPinnedObject();

			int error;
			DFTHandle descriptor;
			error = GetInstance().GetDFTDescriptor(x_inout.Length, DFT.DOUBLE, DFT.INPLACE, DFT.FORWARD, out descriptor);
#if dftMKL
            error = DftiComputeForward(descriptor.pDFTSpec, inoutaddress);
			int length = x_inout.Length;
			double scale = 1.0 / length;
			cblas_zdscal(length, scale, inoutaddress, 1);
#endif
#if dftIPP
			error = ippsDFTFwd_CToC_64fc(inoutaddress, inoutaddress, descriptor.pDFTSpec, descriptor.pDFTWorkBuf);
#endif
#if dftFFTW
			if (fftwAlignmentOf_d(inoutaddress) == fftwAlignmentOf_d(descriptor.pDFTWorkBuf))
			{
				fftwExecute_C2C_d(descriptor.pDFTSpec,inoutaddress,inoutaddress);
			}
			else
			{
				memcpy(descriptor.pDFTWorkBuf, inoutaddress, (UIntPtr)(x_inout.Length * 2 * sizeof(double)));
				fftwExecute_d(descriptor.pDFTSpec);
				memcpy(inoutaddress, descriptor.pDFTWorkBuf, (UIntPtr)(x_inout.Length * 2 * sizeof(double)));
			}
			Vector.ArrayScale(x_inout, x_inout.Length);
#endif
			inoutGC.Free();

			return error;
		}

		/// <summary>
		/// Complex32 DFT
		/// </summary>
		public static int ComputeForwardShifted(Complex32[] x_in, Complex32[] x_out)
		{
			Complex32[] fftTemp = new Complex32[x_in.Length];
			int error = ComputeForward(x_in, fftTemp);
			int k = x_in.Length / 2;

			Vector.ArrayCopy(fftTemp, (x_in.Length - k), x_out, 0, k);
			Vector.ArrayCopy(fftTemp, 0, x_out, k, (x_in.Length - k));

			return error;
		}

		/// <summary>
		/// Complex32 DFT
		/// </summary>
		public static int ComputeForward(Complex32[] x_in, Complex32[] x_out)
		{
			GCHandle inputGC = GCHandle.Alloc(x_in, GCHandleType.Pinned);
			IntPtr inputaddress = inputGC.AddrOfPinnedObject();

			GCHandle outputGC = GCHandle.Alloc(x_out, GCHandleType.Pinned);
			IntPtr outputaddress = outputGC.AddrOfPinnedObject();

			int error;
			DFTHandle descriptor;
			error = GetInstance().GetDFTDescriptor(x_in.Length, DFT.SINGLE, DFT.NOT_INPLACE, DFT.FORWARD, out descriptor);
#if dftMKL
            error = DftiComputeForward(descriptor.pDFTSpec, inputaddress, outputaddress);
			int length = x_in.Length;
			float scale = 1.0f / length;
			cblas_csscal(length, scale, outputaddress, 1);
#endif
#if dftIPP
			error = ippsDFTFwd_CToC_32fc(inputaddress, outputaddress, descriptor.pDFTSpec, descriptor.pDFTWorkBuf);
#endif
#if dftFFTW
			if ((fftwAlignmentOf_f(inputaddress) == fftwAlignmentOf_f(descriptor.pDFTWorkBuf))
				&& (fftwAlignmentOf_f(outputaddress) == fftwAlignmentOf_f(descriptor.pDFTWorkBuf2)))
			{
				fftwExecute_C2C_f(descriptor.pDFTSpec, inputaddress, outputaddress);
			}
			else
			{
				memcpy(descriptor.pDFTWorkBuf, inputaddress, (UIntPtr)(x_in.Length * 2 * sizeof(float)));
				fftwExecute_f(descriptor.pDFTSpec);
				memcpy(outputaddress, descriptor.pDFTWorkBuf, (UIntPtr)(x_out.Length * 2 * sizeof(float)));
			}
			Vector.ArrayScale(x_out, 1.0 / x_in.Length);			
#endif

			inputGC.Free();
			outputGC.Free();

			return error;
		}

		/// <summary>
		/// Complex32 DFT
		/// </summary>
		public static int ComputeForwardShifted(Complex32[] x_inout)
		{
			Complex32[] fftTemp = new Complex32[x_inout.Length];
			int error = ComputeForward(x_inout, fftTemp);
			int k = x_inout.Length / 2;

			Vector.ArrayCopy(fftTemp, (x_inout.Length - k), x_inout, 0, k);
			Vector.ArrayCopy(fftTemp, 0, x_inout, k, (x_inout.Length - k));

			return error;
		}

		/// <summary>
		/// Complex32 DFT
		/// </summary>
		public static int ComputeForward(Complex32[] x_inout)
		{
			GCHandle inoutGC = GCHandle.Alloc(x_inout, GCHandleType.Pinned);
			IntPtr inoutaddress = inoutGC.AddrOfPinnedObject();

			int error;
			DFTHandle descriptor;
			error = GetInstance().GetDFTDescriptor(x_inout.Length, DFT.SINGLE, DFT.INPLACE, DFT.FORWARD, out descriptor);
#if dftMKL
            error = DftiComputeForward(descriptor.pDFTSpec, inoutaddress);
			int length = x_inout.Length;
			float scale = 1.0f / length;
			cblas_csscal(length, scale, inoutaddress, 1);
#endif
#if dftIPP
			error = ippsDFTFwd_CToC_32fc(inoutaddress, inoutaddress, descriptor.pDFTSpec, descriptor.pDFTWorkBuf);
#endif
#if dftFFTW
			if (fftwAlignmentOf_f(inoutaddress) == fftwAlignmentOf_f(descriptor.pDFTWorkBuf))
			{
				fftwExecute_C2C_f(descriptor.pDFTSpec,inoutaddress,inoutaddress);
			}
			else
			{
				memcpy(descriptor.pDFTWorkBuf, inoutaddress, (UIntPtr)(x_inout.Length * 2 * sizeof(float)));
				fftwExecute_f(descriptor.pDFTSpec);
				memcpy(inoutaddress, descriptor.pDFTWorkBuf, (UIntPtr)(x_inout.Length * 2 * sizeof(float)));
			}
			Vector.ArrayScale(x_inout, 1.0f / x_inout.Length);
#endif

			inoutGC.Free();

			return error;
		}

		/// <summary>
		/// Complex IDFT
		/// </summary>
		public static int ComputeBackwardShifted(Complex[] x_in, Complex[] x_out)
		{
			Complex[] fftTemp = new Complex[x_in.Length];
			int k = x_in.Length / 2;

			Vector.ArrayCopy(x_in, (x_in.Length - k), fftTemp, 0, k);
			Vector.ArrayCopy(x_in, 0, fftTemp, k, (x_in.Length - k));

			int error = ComputeBackward(fftTemp, x_out);
			return error;
		}

		/// <summary>
		/// Complex IDFT
		/// </summary>
		public static int ComputeBackward(Complex[] x_in, Complex[] x_out)
		{
			GCHandle inputGC = GCHandle.Alloc(x_in, GCHandleType.Pinned);
			IntPtr inputaddress = inputGC.AddrOfPinnedObject();

			GCHandle outputGC = GCHandle.Alloc(x_out, GCHandleType.Pinned);
			IntPtr outputaddress = outputGC.AddrOfPinnedObject();

			int error;
			DFTHandle descriptor;
			error = GetInstance().GetDFTDescriptor(x_in.Length, DFT.DOUBLE, DFT.NOT_INPLACE, DFT.BACKWARD, out descriptor);
#if dftMKL
            error = DftiComputeBackward(descriptor.pDFTSpec, inputaddress, outputaddress);
#endif
#if dftIPP
			error = ippsDFTInv_CToC_64fc(inputaddress, outputaddress, descriptor.pDFTSpec, descriptor.pDFTWorkBuf);
#endif
#if dftFFTW
			if ((fftwAlignmentOf_d(inputaddress) == fftwAlignmentOf_d(descriptor.pDFTWorkBuf))
				&& (fftwAlignmentOf_d(outputaddress) == fftwAlignmentOf_d(descriptor.pDFTWorkBuf2)))
			{
				fftwExecute_C2C_d(descriptor.pDFTSpec, inputaddress, outputaddress);
			}
			else
			{
				memcpy(descriptor.pDFTWorkBuf, inputaddress, (UIntPtr)(x_in.Length * 2 * sizeof(double)));
				fftwExecute_d(descriptor.pDFTSpec);
				memcpy(outputaddress, descriptor.pDFTWorkBuf, (UIntPtr)(x_out.Length * 2 * sizeof(double)));
			}
#endif

			inputGC.Free();
			outputGC.Free();

			return error;
		}

		/// <summary>
		/// Complex32 IDFT
		/// </summary>
		public static int ComputeBackwardShifted(Complex[] x_inout)
		{
			Complex[] fftTemp = new Complex[x_inout.Length];
			int k = x_inout.Length / 2;

			Vector.ArrayCopy(x_inout, (x_inout.Length - k), fftTemp, 0, k);
			Vector.ArrayCopy(x_inout, 0, fftTemp, k, (x_inout.Length - k));

			int error = ComputeBackward(fftTemp, x_inout);
			return error;
		}

		/// <summary>
		/// Complex IDFT
		/// </summary>
		public static int ComputeBackward(Complex[] x_inout)
		{
			GCHandle inoutGC = GCHandle.Alloc(x_inout, GCHandleType.Pinned);
			IntPtr inoutaddress = inoutGC.AddrOfPinnedObject();

			int error;
			DFTHandle descriptor;
			error = GetInstance().GetDFTDescriptor(x_inout.Length, DFT.DOUBLE, DFT.INPLACE, DFT.BACKWARD, out descriptor);
#if dftMKL
            error = DftiComputeBackward(descriptor.pDFTSpec, inoutaddress);
#endif
#if dftIPP
			error = ippsDFTInv_CToC_64fc(inoutaddress, inoutaddress, descriptor.pDFTSpec, descriptor.pDFTWorkBuf);
#endif
#if dftFFTW
			if (fftwAlignmentOf_d(inoutaddress) == fftwAlignmentOf_d(descriptor.pDFTWorkBuf))
			{
				fftwExecute_C2C_d(descriptor.pDFTSpec,inoutaddress,inoutaddress);
			}
			else
			{
				memcpy(descriptor.pDFTWorkBuf, inoutaddress, (UIntPtr)(x_inout.Length * 2 * sizeof(double)));
				fftwExecute_d(descriptor.pDFTSpec);
				memcpy(inoutaddress, descriptor.pDFTWorkBuf, (UIntPtr)(x_inout.Length * 2 * sizeof(double)));
			}
#endif

			inoutGC.Free();

			return error;
		}

		/// <summary>
		/// Complex32 IDFT
		/// </summary>
		public static int ComputeBackwardShifted(Complex32[] x_in, Complex32[] x_out)
		{
			Complex32[] fftTemp = new Complex32[x_in.Length];
			int k = x_in.Length / 2;

			Vector.ArrayCopy(x_in, (x_in.Length - k), fftTemp, 0, k);
			Vector.ArrayCopy(x_in, 0, fftTemp, k, (x_in.Length - k));

			int error = ComputeBackward(fftTemp, x_out);

			return error;
		}

		/// <summary>
		/// Complex32 IDFT
		/// </summary>
		public static int ComputeBackward(Complex32[] x_in, Complex32[] x_out)
		{
			GCHandle inputGC = GCHandle.Alloc(x_in, GCHandleType.Pinned);
			IntPtr inputaddress = inputGC.AddrOfPinnedObject();

			GCHandle outputGC = GCHandle.Alloc(x_out, GCHandleType.Pinned);
			IntPtr outputaddress = outputGC.AddrOfPinnedObject();

			int error;
			DFTHandle descriptor;
			error = GetInstance().GetDFTDescriptor(x_in.Length, DFT.SINGLE, DFT.NOT_INPLACE, DFT.BACKWARD, out descriptor);
#if dftMKL
            error = DftiComputeBackward(descriptor.pDFTSpec, inputaddress, outputaddress);
#endif
#if dftIPP
			error = ippsDFTInv_CToC_32fc(inputaddress, outputaddress, descriptor.pDFTSpec, descriptor.pDFTWorkBuf);
#endif
#if dftFFTW
			if ((fftwAlignmentOf_f(inputaddress) == fftwAlignmentOf_f(descriptor.pDFTWorkBuf)) 
				&& (fftwAlignmentOf_f(outputaddress) == fftwAlignmentOf_f(descriptor.pDFTWorkBuf2)))
			{
				fftwExecute_C2C_f(descriptor.pDFTSpec, inputaddress, outputaddress);
			}
			else
			{
				memcpy(descriptor.pDFTWorkBuf, inputaddress, (UIntPtr)(x_in.Length * 2 * sizeof(float)));
				fftwExecute_f(descriptor.pDFTSpec);
				memcpy(outputaddress, descriptor.pDFTWorkBuf, (UIntPtr)(x_out.Length * 2 * sizeof(float)));
			}
#endif

			inputGC.Free();
			outputGC.Free();

			return error;
		}

		/// <summary>
		/// Complex32 IDFT
		/// </summary>
		public static int ComputeBackwardShifted(Complex32[] x_inout)
		{
			Complex32[] fftTemp = new Complex32[x_inout.Length];
			int k = x_inout.Length / 2;

			Vector.ArrayCopy(x_inout, (x_inout.Length - k), fftTemp, 0, k);
			Vector.ArrayCopy(x_inout, 0, fftTemp, k, (x_inout.Length - k));

			int error = ComputeBackward(fftTemp, x_inout);

			return error;
		}

		/// <summary>
		/// Complex32 IDFT
		/// </summary>
		public static int ComputeBackward(Complex32[] x_inout)
		{
			GCHandle inoutGC = GCHandle.Alloc(x_inout, GCHandleType.Pinned);
			IntPtr inoutaddress = inoutGC.AddrOfPinnedObject();

			int error;
			DFTHandle descriptor;
			error = GetInstance().GetDFTDescriptor(x_inout.Length, DFT.SINGLE, DFT.INPLACE, DFT.BACKWARD, out descriptor);
#if dftMKL
            error = DftiComputeBackward(descriptor.pDFTSpec, inoutaddress);
#endif
#if dftIPP
			error = ippsDFTInv_CToC_32fc(inoutaddress, inoutaddress, descriptor.pDFTSpec, descriptor.pDFTWorkBuf);
#endif
#if dftFFTW
			if (fftwAlignmentOf_f(inoutaddress) == fftwAlignmentOf_f(descriptor.pDFTWorkBuf))
			{
				fftwExecute_C2C_f(descriptor.pDFTSpec,inoutaddress,inoutaddress);
			}
			else
			{
				memcpy(descriptor.pDFTWorkBuf, inoutaddress, (UIntPtr)(x_inout.Length * 2 * sizeof(float)));
				fftwExecute_f(descriptor.pDFTSpec);
				memcpy(inoutaddress, descriptor.pDFTWorkBuf, (UIntPtr)(x_inout.Length * 2 * sizeof(float)));
			}
#endif

			inoutGC.Free();

			return error;
		}

		private static int FORWARD = -1;
		private static int BACKWARD = 1;

		#region ----  Constants for DFTI, file "mkl_dfti.h" ----

		/** DFTI configuration parameters */
		private static int PRECISION = 3;
		private static int FORWARD_DOMAIN = 0;
		private static int DIMENSION = 1;
		private static int LENGTHS = 2;
		private static int NUMBER_OF_TRANSFORMS = 7;
		private static int FORWARD_SCALE = 4;
		private static int BACKWARD_SCALE = 5;
		private static int PLACEMENT = 11;
		private static int COMPLEX_STORAGE = 8;
		private static int REAL_STORAGE = 9;
		private static int CONJUGATE_EVEN_STORAGE = 10;
		private static int DESCRIPTOR_NAME = 20;
		private static int PACKED_FORMAT = 21;
		private static int NUMBER_OF_USER_THREADS = 26;
		private static int INPUT_DISTANCE = 14;
		private static int OUTPUT_DISTANCE = 15;
		private static int INPUT_STRIDES = 12;
		private static int OUTPUT_STRIDES = 13;
		private static int ORDERING = 18;
		private static int TRANSPOSE = 19;
		private static int COMMIT_STATUS = 22;
		private static int VERSION = 23;
		/** DFTI configuration values */
		private static int SINGLE = 35;
		private static int DOUBLE = 36;
		private static int COMPLEX = 32;
		private static int REAL = 33;
		private static int INPLACE = 43;
		private static int NOT_INPLACE = 44;
		private static int COMPLEX_COMPLEX = 39;
		private static int REAL_REAL = 42;
		private static int COMPLEX_REAL = 40;
		private static int REAL_COMPLEX = 41;
		private static int COMMITTED = 30;
		private static int UNCOMMITTED = 31;
		private static int ORDERED = 48;
		private static int BACKWARD_SCRAMBLED = 49;
		private static int NONE = 53;
		private static int CCS_FORMAT = 54;
		private static int PACK_FORMAT = 55;
		private static int PERM_FORMAT = 56;
		private static int CCE_FORMAT = 57;
		private static int VERSION_LENGTH = 198;
		private static int MAX_NAME_LENGTH = 10;
		private static int MAX_MESSAGE_LENGTH = 40;
		/** DFTI predefined error classes */
		private static int NO_ERROR = 0;
		private static int MEMORY_ERROR = 1;
		private static int INVALID_CONFIGURATION = 2;
		private static int INCONSISTENT_CONFIGURATION = 3;
		private static int NUMBER_OF_THREADS_ERROR = 8;
		private static int MULTITHREADED_ERROR = 4;
		private static int BAD_DESCRIPTOR = 5;
		private static int UNIMPLEMENTED = 6;
		private static int MKL_INTERNAL_ERROR = 7;
		private static int LENGTH_EXCEEDS_INT32 = 9;

		#endregion

#if dftMKL
		#region ---- MKL DLL Caller ----
#if dspLinux
		private const string dllName = @"mkl_rt.dll";
#else
		//private const string dllName = @"jxiMKLcdecl_s.dll";
		private const string dllName = @"jxiMKLcdecl_p.dll";
#endif

        private const CallingConvention MKLCallingConvertion = CallingConvention.Cdecl;

        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_csscal(int n, float a, IntPtr x, int incx);

        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_zdscal(int n, double a, IntPtr x, int incx);

        /** DFTI native DftiCreateDescriptor declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiCreateDescriptor(ref IntPtr descroptorHandle, int precision, int domain, int dimention, int length);

        /** DFTI native DftiCommitDescriptor declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiCommitDescriptor(IntPtr descroptorHandle);

        /** DFTI native DftiFreeDescriptor declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiFreeDescriptor(ref IntPtr descroptorHandle);

        /** DFTI native DftiSetValue declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiSetValue(IntPtr descroptorHandle, int config_param, int config_val);

        /** DFTI native DftiSetValue declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiSetValue(IntPtr descroptorHandle, int config_param, float config_val);

        /** DFTI native DftiSetValue declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiSetValue(IntPtr descroptorHandle, int config_param, double config_val);

        /** DFTI native DftiSetValue declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiSetValue(IntPtr descroptorHandle, int config_param, decimal config_val);

        /** DFTI native DftiGetValue declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiGetValue(IntPtr descroptorHandle, int config_param, ref int config_val);

        /** DFTI native DftiGetValue declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiGetValue(IntPtr descroptorHandle, int config_param, ref float config_val);

        /** DFTI native DftiGetValue declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiGetValue(IntPtr descroptorHandle, int config_param, ref double config_val);

        /** DFTI native DftiComputeForward declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiComputeForward(IntPtr descroptorHandle, IntPtr x_in, IntPtr x_out);

        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiComputeForward(IntPtr descroptorHandle, IntPtr x_inout);

        /** DFTI native DftiComputeBackward declaration */
        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiComputeBackward(IntPtr descroptorHandle, IntPtr x_in, IntPtr x_out);

        [DllImport(dllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int DftiComputeBackward(IntPtr descroptorHandle, IntPtr x_inout);

		#endregion
#endif

#if dftIPP
		#region---- IPP DLL Caller ----
#if dspLinux
		private const string IPPCoreFilePath = @"ipps.dll";
#else
		private const string IPPCoreFilePath = @"jxiIPP.dll";
#endif
		const CallingConvention IPPCallingConvertion = CallingConvention.Winapi;

		// float complex
		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern int ippsDFTGetSize_C_32fc(int length, Flag flag, IppHintAlgorithm hint, out int pSizeSpec, out int pSizeInit, out int pSizeBuf);

		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern int ippsDFTInit_C_32fc(int length, Flag flag, IppHintAlgorithm hint, IntPtr pDFTSpec, IntPtr pMemInit);

		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern int ippsDFTFwd_CToC_32fc(IntPtr pSrc, IntPtr pDst, IntPtr pDFTSpec, IntPtr pBuffer);

		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern int ippsDFTInv_CToC_32fc(IntPtr pSrc, IntPtr pDst, IntPtr pDFTSpec, IntPtr pBuffer);

		// double complex
		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern int ippsDFTGetSize_C_64fc(int length, Flag flag, IppHintAlgorithm hint, out int pSizeSpec, out int pSizeInit, out int pSizeBuf);

		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern int ippsDFTInit_C_64fc(int length, Flag flag, IppHintAlgorithm hint, IntPtr pDFTSpec, IntPtr pMemInit);

		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern int ippsDFTFwd_CToC_64fc(IntPtr pSrc, IntPtr pDst, IntPtr pDFTSpec, IntPtr pBuffer);

		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern int ippsDFTInv_CToC_64fc(IntPtr pSrc, IntPtr pDst, IntPtr pDFTSpec, IntPtr pBuffer);


		#region DFT Const
		internal enum Flag
		{
			IPP_FFT_DIV_FWD_BY_N = 1,
			IPP_FFT_DIV_INV_BY_N = 2,
			IPP_FFT_DIV_BY_SQRTN = 4,
			IPP_FFT_NODIV_BY_ANY = 8,
		}

		internal enum IppHintAlgorithm
		{
			ippAlgHintNone,
			ippAlgHintFast,
			ippAlgHintAccurate,
		}
		#endregion

		// Malloc

		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern IntPtr ippsMalloc_8u(int len);

		// Free
		[DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
		private static extern void ippsFree(IntPtr ptr);

		#endregion
#endif

#if dftFFTW
		#region---- FFTW DLL Caller ----

		private const string FFTWSinglePrecisionFilePath = @"libfftw3f-3.dll";
		private const string FFTWDoublePrecisionFilePath = @"libfftw3-3.dll";
		const CallingConvention FFTWCallingConvertion = CallingConvention.Cdecl;

		// Memory Copy
		[DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
		public static extern void memcpy(IntPtr destination, IntPtr source, UIntPtr length);

		// Various Flags used by FFTW
		#region Enums
		/// <summary>
		/// FFTW planner flags
		/// </summary>
		[Flags]
		internal enum fftwFlags : uint
		{
			/// <summary>
			/// Tells FFTW to find an optimized plan by actually computing several FFTs and measuring their execution time. 
			/// Depending on your machine, this can take some time (often a few seconds). Default (0x0). 
			/// </summary>
			Measure = 0,
			/// <summary>
			/// Specifies that an out-of-place transform is allowed to overwrite its 
			/// input array with arbitrary data; this can sometimes allow more efficient algorithms to be employed.
			/// </summary>
			DestroyInput = 1,
			/// <summary>
			/// Rarely used. Specifies that the algorithm may not impose any unusual alignment requirements on the input/output 
			/// arrays (i.e. no SIMD). This flag is normally not necessary, since the planner automatically detects 
			/// misaligned arrays. The only use for this flag is if you want to use the guru interface to execute a given 
			/// plan on a different array that may not be aligned like the original. 
			/// </summary>
			Unaligned = 2,
			/// <summary>
			/// Not used.
			/// </summary>
			ConserveMemory = 4,
			/// <summary>
			/// Like Patient, but considers an even wider range of algorithms, including many that we think are 
			/// unlikely to be fast, to produce the most optimal plan but with a substantially increased planning time. 
			/// </summary>
			Exhaustive = 8,
			/// <summary>
			/// Specifies that an out-of-place transform must not change its input array. 
			/// </summary>
			/// <remarks>
			/// This is ordinarily the default, 
			/// except for c2r and hc2r (i.e. complex-to-real) transforms for which DestroyInput is the default. 
			/// In the latter cases, passing PreserveInput will attempt to use algorithms that do not destroy the 
			/// input, at the expense of worse performance; for multi-dimensional c2r transforms, however, no 
			/// input-preserving algorithms are implemented and the planner will return null if one is requested.
			/// </remarks>
			PreserveInput = 16,
			/// <summary>
			/// Like Measure, but considers a wider range of algorithms and often produces a 搈ore optimal?plan 
			/// (especially for large transforms), but at the expense of several times longer planning time 
			/// (especially for large transforms).
			/// </summary>
			Patient = 32,
			/// <summary>
			/// Specifies that, instead of actual measurements of different algorithms, a simple heuristic is 
			/// used to pick a (probably sub-optimal) plan quickly. With this flag, the input/output arrays 
			/// are not overwritten during planning. 
			/// </summary>
			Estimate = 64
		}

		/// <summary>
		/// Defines direction of operation
		/// </summary>
		internal enum fftwDirection : int
		{
			/// <summary>
			/// Computes a regular DFT
			/// </summary>
			Forward = -1,
			/// <summary>
			/// Computes the inverse DFT
			/// </summary>
			Backward = 1
		}

		/// <summary>
		/// Kinds of real-to-real transforms
		/// </summary>
		internal enum fftwRealType : uint
		{
			R2HC = 0,
			HC2R = 1,
			DHT = 2,
			REDFT00 = 3,
			REDFT01 = 4,
			REDFT10 = 5,
			REDFT11 = 6,
			RODFT00 = 7,
			RODFT01 = 8,
			RODFT10 = 9,
			RODFT11 = 10
		}
		#endregion

		// FFTW API
		/// <summary>
		/// Allocates FFTW-optimized unmanaged memory
		/// </summary>
		/// <param name="length">Amount to allocate, in bytes</param>
		/// <returns>Pointer to allocated memory</returns>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_malloc", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwMalloc_f(int length);

		/// <summary>
		/// Allocates FFTW-optimized unmanaged memory
		/// </summary>
		/// <param name="length">Amount to allocate, in bytes</param>
		/// <returns>Pointer to allocated memory</returns>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_malloc", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwMalloc_d(int length);

		/// <summary>
		/// Allocates FFTW-optimized unmanaged memory
		/// </summary>
		/// <param name="length">Amount to allocate, in samples</param>
		/// <returns>Pointer to allocated memory</returns>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_alloc_real", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwAllocReal_f(int length);

		/// <summary>
		/// Allocates FFTW-optimized unmanaged memory
		/// </summary>
		/// <param name="length">Amount to allocate, in samples</param>
		/// <returns>Pointer to allocated memory</returns>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_alloc_real", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwAllocReal_d(int length);

		/// <summary>
		/// Allocates FFTW-optimized unmanaged memory
		/// </summary>
		/// <param name="length">Amount to allocate, in samples</param>
		/// <returns>Pointer to allocated memory</returns>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_alloc_complex", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwAllocComplex_f(int length);

		/// <summary>
		/// Allocates FFTW-optimized unmanaged memory
		/// </summary>
		/// <param name="length">Amount to allocate, in samples</param>
		/// <returns>Pointer to allocated memory</returns>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_alloc_complex", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwAllocComplex_d(int length);

		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_alignment_of", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern int fftwAlignmentOf_f(IntPtr mem);

		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_alignment_of", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern int fftwAlignmentOf_d(IntPtr mem);

		/// <summary>
		/// Deallocates memory allocated by FFTW malloc
		/// </summary>
		/// <param name="mem">Pointer to memory to release</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_free", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwFree_f(IntPtr mem);

		/// <summary>
		/// Deallocates memory allocated by FFTW malloc
		/// </summary>
		/// <param name="mem">Pointer to memory to release</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_free", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwFree_d(IntPtr mem);

		/// <summary>
		/// Deallocates an FFTW plan and all associated resources
		/// </summary>
		/// <param name="plan">Pointer to the plan to release</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_destroy_plan", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwDestroyPlan_f(IntPtr plan);

		/// <summary>
		/// Deallocates an FFTW plan and all associated resources
		/// </summary>
		/// <param name="plan">Pointer to the plan to release</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_destroy_plan", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwDestroyPlan_d(IntPtr plan);

		/// <summary>
		/// Clears all memory used by FFTW, resets it to initial state. Does not replace destroy_plan and free
		/// </summary>
		/// <remarks>After calling fftw_cleanup, all existing plans become undefined, and you should not 
		/// attempt to execute them nor to destroy them. You can however create and execute/destroy new plans, 
		/// in which case FFTW starts accumulating wisdom information again. 
		/// fftw_cleanup does not deallocate your plans; you should still call fftw_destroy_plan for this purpose.</remarks>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_cleanup", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwCleanup_f();

		/// <summary>
		/// Clears all memory used by FFTW, resets it to initial state. Does not replace destroy_plan and free
		/// </summary>
		/// <remarks>After calling fftw_cleanup, all existing plans become undefined, and you should not 
		/// attempt to execute them nor to destroy them. You can however create and execute/destroy new plans, 
		/// in which case FFTW starts accumulating wisdom information again. 
		/// fftw_cleanup does not deallocate your plans; you should still call fftw_destroy_plan for this purpose.</remarks>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_cleanup", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwCleanup_d();

		/// <summary>
		/// Sets the maximum time that can be used by the planner.
		/// </summary>
		/// <param name="seconds">Maximum time, in seconds.</param>
		/// <remarks>This function instructs FFTW to spend at most seconds seconds (approximately) in the planner. 
		/// If seconds == -1.0 (the default value), then planning time is unbounded. 
		/// Otherwise, FFTW plans with a progressively wider range of algorithms until the the given time limit is 
		/// reached or the given range of algorithms is explored, returning the best available plan. For example, 
		/// specifying fftw_flags.Patient first plans in Estimate mode, then in Measure mode, then finally (time 
		/// permitting) in Patient. If fftw_flags.Exhaustive is specified instead, the planner will further progress to 
		/// Exhaustive mode. 
		/// </remarks>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_set_timelimit", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwSetTimeLimit_f(double seconds);

		/// <summary>
		/// Sets the maximum time that can be used by the planner.
		/// </summary>
		/// <param name="seconds">Maximum time, in seconds.</param>
		/// <remarks>This function instructs FFTW to spend at most seconds seconds (approximately) in the planner. 
		/// If seconds == -1.0 (the default value), then planning time is unbounded. 
		/// Otherwise, FFTW plans with a progressively wider range of algorithms until the the given time limit is 
		/// reached or the given range of algorithms is explored, returning the best available plan. For example, 
		/// specifying fftw_flags.Patient first plans in Estimate mode, then in Measure mode, then finally (time 
		/// permitting) in Patient. If fftw_flags.Exhaustive is specified instead, the planner will further progress to 
		/// Exhaustive mode. 
		/// </remarks>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_set_timelimit", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwSetTimeLimit_d(double seconds);



		/// <summary>
		/// Executes an FFTW plan, provided that the input and output arrays still exist
		/// </summary>
		/// <param name="plan">Pointer to the plan to execute</param>
		/// <remarks>execute (and equivalents) is the only function in FFTW guaranteed to be thread-safe.</remarks>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_execute", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_f(IntPtr plan);

		/// <summary>
		/// Executes an FFTW plan, provided that the input and output arrays still exist
		/// </summary>
		/// <param name="plan">Pointer to the plan to execute</param>
		/// <remarks>execute (and equivalents) is the only function in FFTW guaranteed to be thread-safe.</remarks>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_execute", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_d(IntPtr plan);

		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_execute_dft", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_C2C_f(IntPtr plan, IntPtr input, IntPtr output);

		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_execute_dft", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_C2C_d(IntPtr plan, IntPtr input, IntPtr output);

		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_execute_split_dft", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_C2C_f(IntPtr plan, IntPtr inputReal, IntPtr inputImage, IntPtr outputReal, IntPtr outputImage);

		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_execute_split_dft", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_C2C_d(IntPtr plan, IntPtr inputReal, IntPtr inputImage, IntPtr outputReal, IntPtr outputImage);

		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_execute_dft_r2c", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_R2C_f(IntPtr plan, IntPtr input, IntPtr output);

		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_execute_dft_r2c", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_R2C_d(IntPtr plan, IntPtr input, IntPtr output);

		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_execute_split_dft_r2c", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_R2C_f(IntPtr plan, IntPtr input, IntPtr outputReal, IntPtr outputImage);

		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_execute_split_dft_r2c", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_R2C_d(IntPtr plan, IntPtr input, IntPtr outputReal, IntPtr outputImage);

		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_execute_dft_c2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_C2R_f(IntPtr plan, IntPtr input, IntPtr output);

		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_execute_dft_c2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_C2R_d(IntPtr plan, IntPtr input, IntPtr output);

		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_execute_split_dft_c2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_C2R_f(IntPtr plan, IntPtr inputReal, IntPtr inputImage, IntPtr output);

		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_execute_split_dft_c2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_C2R_d(IntPtr plan, IntPtr inputReal, IntPtr inputImage, IntPtr output);

		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_execute_r2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_R2R_f(IntPtr plan, IntPtr input, IntPtr output);

		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_execute_r2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExecute_R2R_d(IntPtr plan, IntPtr input, IntPtr output);


	
		/// <summary>
		/// Creates a plan for a 1-dimensional complex-to-complex DFT
		/// </summary>
		/// <param name="n">The logical size of the transform</param>
		/// <param name="direction">Specifies the direction of the transform</param>
		/// <param name="input">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_1d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2C_f(int n, IntPtr input, IntPtr output, fftwDirection direction, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 1-dimensional complex-to-complex DFT
		/// </summary>
		/// <param name="n">The logical size of the transform</param>
		/// <param name="direction">Specifies the direction of the transform</param>
		/// <param name="input">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_1d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2C_d(int n, IntPtr input, IntPtr output, fftwDirection direction, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 2-dimensional complex-to-complex DFT
		/// </summary>
		/// <param name="nx">The logical size of the transform along the first dimension</param>
		/// <param name="ny">The logical size of the transform along the second dimension</param>
		/// <param name="direction">Specifies the direction of the transform</param>
		/// <param name="input">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_2d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2C_f(int nx, int ny, IntPtr input, IntPtr output, fftwDirection direction, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 2-dimensional complex-to-complex DFT
		/// </summary>
		/// <param name="nx">The logical size of the transform along the first dimension</param>
		/// <param name="ny">The logical size of the transform along the second dimension</param>
		/// <param name="direction">Specifies the direction of the transform</param>
		/// <param name="input">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_2d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2C_d(int nx, int ny, IntPtr input, IntPtr output, fftwDirection direction, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 3-dimensional complex-to-complex DFT
		/// </summary>
		/// <param name="nx">The logical size of the transform along the first dimension</param>
		/// <param name="ny">The logical size of the transform along the second dimension</param>
		/// <param name="nz">The logical size of the transform along the third dimension</param>
		/// <param name="direction">Specifies the direction of the transform</param>
		/// <param name="input">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_3d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2C_f(int nx, int ny, int nz, IntPtr input, IntPtr output, fftwDirection direction, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 3-dimensional complex-to-complex DFT
		/// </summary>
		/// <param name="nx">The logical size of the transform along the first dimension</param>
		/// <param name="ny">The logical size of the transform along the second dimension</param>
		/// <param name="nz">The logical size of the transform along the third dimension</param>
		/// <param name="direction">Specifies the direction of the transform</param>
		/// <param name="input">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_3d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2C_d(int nx, int ny, int nz, IntPtr input, IntPtr output, fftwDirection direction, fftwFlags flags);

		/// <summary>
		/// Creates a plan for an n-dimensional complex-to-complex DFT
		/// </summary>
		/// <param name="rank">Number of dimensions</param>
		/// <param name="n">Array containing the logical size along each dimension</param>
		/// <param name="direction">Specifies the direction of the transform</param>
		/// <param name="input">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2C_f(int rank, int[] n, IntPtr input, IntPtr output, fftwDirection direction, fftwFlags flags);

		/// <summary>
		/// Creates a plan for an n-dimensional complex-to-complex DFT
		/// </summary>
		/// <param name="rank">Number of dimensions</param>
		/// <param name="n">Array containing the logical size along each dimension</param>
		/// <param name="direction">Specifies the direction of the transform</param>
		/// <param name="input">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2C_d(int rank, int[] n, IntPtr input, IntPtr output, fftwDirection direction, fftwFlags flags);



		/// <summary>
		/// Creates a plan for a 1-dimensional real-to-complex DFT
		/// </summary>
		/// <param name="n">Number of REAL (input) elements in the transform</param>
		/// <param name="input">Pointer to an array of 4-byte real numbers</param>
		/// <param name="output">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_r2c_1d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2C_f(int n, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 1-dimensional real-to-complex DFT
		/// </summary>
		/// <param name="n">Number of REAL (input) elements in the transform</param>
		/// <param name="input">Pointer to an array of 8-byte real numbers</param>
		/// <param name="output">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_r2c_1d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2C_d(int n, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 2-dimensional real-to-complex DFT
		/// </summary>
		/// <param name="nx">Number of REAL (input) elements in the transform along the first dimension</param>
		/// <param name="ny">Number of REAL (input) elements in the transform along the second dimension</param>
		/// <param name="input">Pointer to an array of 4-byte real numbers</param>
		/// <param name="output">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_r2c_2d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2C_f(int nx, int ny, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 2-dimensional real-to-complex DFT
		/// </summary>
		/// <param name="nx">Number of REAL (input) elements in the transform along the first dimension</param>
		/// <param name="ny">Number of REAL (input) elements in the transform along the second dimension</param>
		/// <param name="input">Pointer to an array of 8-byte real numbers</param>
		/// <param name="output">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_r2c_2d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2C_d(int nx, int ny, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 3-dimensional real-to-complex DFT
		/// </summary>
		/// <param name="nx">Number of REAL (input) elements in the transform along the first dimension</param>
		/// <param name="ny">Number of REAL (input) elements in the transform along the second dimension</param>
		/// <param name="nz">Number of REAL (input) elements in the transform along the third dimension</param>
		/// <param name="input">Pointer to an array of 4-byte real numbers</param>
		/// <param name="output">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_r2c_3d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2C_f(int nx, int ny, int nz, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 3-dimensional real-to-complex DFT
		/// </summary>
		/// <param name="nx">Number of REAL (input) elements in the transform along the first dimension</param>
		/// <param name="ny">Number of REAL (input) elements in the transform along the second dimension</param>
		/// <param name="nz">Number of REAL (input) elements in the transform along the third dimension</param>
		/// <param name="input">Pointer to an array of 8-byte real numbers</param>
		/// <param name="output">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_r2c_3d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2C_d(int nx, int ny, int nz, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for an n-dimensional real-to-complex DFT
		/// </summary>
		/// <param name="rank">Number of dimensions</param>
		/// <param name="n">Array containing the number of REAL (input) elements along each dimension</param>
		/// <param name="input">Pointer to an array of 4-byte real numbers</param>
		/// <param name="output">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_r2c", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2C_f(int rank, int[] n, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for an n-dimensional real-to-complex DFT
		/// </summary>
		/// <param name="rank">Number of dimensions</param>
		/// <param name="n">Array containing the number of REAL (input) elements along each dimension</param>
		/// <param name="input">Pointer to an array of 8-byte real numbers</param>
		/// <param name="output">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_r2c", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2C_d(int rank, int[] n, IntPtr input, IntPtr output, fftwFlags flags);



		/// <summary>
		/// Creates a plan for a 1-dimensional complex-to-real DFT
		/// </summary>
		/// <param name="n">Number of REAL (output) elements in the transform</param>
		/// <param name="input">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 4-byte real numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_c2r_1d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2R_f(int n, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 1-dimensional complex-to-real DFT
		/// </summary>
		/// <param name="n">Number of REAL (output) elements in the transform</param>
		/// <param name="input">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 8-byte real numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_c2r_1d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2R_d(int n, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 2-dimensional complex-to-real DFT
		/// </summary>
		/// <param name="nx">Number of REAL (output) elements in the transform along the first dimension</param>
		/// <param name="ny">Number of REAL (output) elements in the transform along the second dimension</param>
		/// <param name="input">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 4-byte real numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_c2r_2d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2R_f(int nx, int ny, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 2-dimensional complex-to-real DFT
		/// </summary>
		/// <param name="nx">Number of REAL (output) elements in the transform along the first dimension</param>
		/// <param name="ny">Number of REAL (output) elements in the transform along the second dimension</param>
		/// <param name="input">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 8-byte real numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_c2r_2d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2R_d(int nx, int ny, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 3-dimensional complex-to-real DFT
		/// </summary>
		/// <param name="nx">Number of REAL (output) elements in the transform along the first dimension</param>
		/// <param name="ny">Number of REAL (output) elements in the transform along the second dimension</param>
		/// <param name="nz">Number of REAL (output) elements in the transform along the third dimension</param>
		/// <param name="input">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 4-byte real numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_c2r_3d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2R_f(int nx, int ny, int nz, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 3-dimensional complex-to-real DFT
		/// </summary>
		/// <param name="nx">Number of REAL (output) elements in the transform along the first dimension</param>
		/// <param name="ny">Number of REAL (output) elements in the transform along the second dimension</param>
		/// <param name="nz">Number of REAL (output) elements in the transform along the third dimension</param>
		/// <param name="input">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 8-byte real numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_c2r_3d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2R_d(int nx, int ny, int nz, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for an n-dimensional complex-to-real DFT
		/// </summary>
		/// <param name="rank">Number of dimensions</param>
		/// <param name="n">Array containing the number of REAL (output) elements along each dimension</param>
		/// <param name="input">Pointer to an array of 8-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 4-byte real numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_dft_c2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2R_f(int rank, int[] n, IntPtr input, IntPtr output, fftwFlags flags);

		/// <summary>
		/// Creates a plan for an n-dimensional complex-to-real DFT
		/// </summary>
		/// <param name="rank">Number of dimensions</param>
		/// <param name="n">Array containing the number of REAL (output) elements along each dimension</param>
		/// <param name="input">Pointer to an array of 16-byte complex numbers</param>
		/// <param name="output">Pointer to an array of 8-byte real numbers</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_dft_c2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_C2R_d(int rank, int[] n, IntPtr input, IntPtr output, fftwFlags flags);



		/// <summary>
		/// Creates a plan for a 1-dimensional real-to-real DFT
		/// </summary>
		/// <param name="n">Number of elements in the transform</param>
		/// <param name="input">Pointer to an array of 4-byte real numbers</param>
		/// <param name="output">Pointer to an array of 4-byte real numbers</param>
		/// <param name="kind">The kind of real-to-real transform to compute</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_r2r_1d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2R_f(int n, IntPtr input, IntPtr output, fftwRealType kind, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 1-dimensional real-to-real DFT
		/// </summary>
		/// <param name="n">Number of elements in the transform</param>
		/// <param name="input">Pointer to an array of 8-byte real numbers</param>
		/// <param name="output">Pointer to an array of 8-byte real numbers</param>
		/// <param name="kind">The kind of real-to-real transform to compute</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_r2r_1d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2R_d(int n, IntPtr input, IntPtr output, fftwRealType kind, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 2-dimensional real-to-real DFT
		/// </summary>
		/// <param name="nx">Number of elements in the transform along the first dimension</param>
		/// <param name="ny">Number of elements in the transform along the second dimension</param>
		/// <param name="input">Pointer to an array of 4-byte real numbers</param>
		/// <param name="output">Pointer to an array of 4-byte real numbers</param>
		/// <param name="kindx">The kind of real-to-real transform to compute along the first dimension</param>
		/// <param name="kindy">The kind of real-to-real transform to compute along the second dimension</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_r2r_2d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2R_f(int nx, int ny, IntPtr input, IntPtr output, fftwRealType kindx, fftwRealType kindy, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 2-dimensional real-to-real DFT
		/// </summary>
		/// <param name="nx">Number of elements in the transform along the first dimension</param>
		/// <param name="ny">Number of elements in the transform along the second dimension</param>
		/// <param name="input">Pointer to an array of 8-byte real numbers</param>
		/// <param name="output">Pointer to an array of 8-byte real numbers</param>
		/// <param name="kindx">The kind of real-to-real transform to compute along the first dimension</param>
		/// <param name="kindy">The kind of real-to-real transform to compute along the second dimension</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_r2r_2d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2R_d(int nx, int ny, IntPtr input, IntPtr output, fftwRealType kindx, fftwRealType kindy, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 3-dimensional real-to-real DFT
		/// </summary>
		/// <param name="nx">Number of elements in the transform along the first dimension</param>
		/// <param name="ny">Number of elements in the transform along the second dimension</param>
		/// <param name="nz">Number of elements in the transform along the third dimension</param>
		/// <param name="input">Pointer to an array of 4-byte real numbers</param>
		/// <param name="output">Pointer to an array of 4-byte real numbers</param>
		/// <param name="kindx">The kind of real-to-real transform to compute along the first dimension</param>
		/// <param name="kindy">The kind of real-to-real transform to compute along the second dimension</param>
		/// <param name="kindz">The kind of real-to-real transform to compute along the third dimension</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_r2r_3d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2R_f(int nx, int ny, int nz, IntPtr input, IntPtr output, fftwRealType kindx, fftwRealType kindy, fftwRealType kindz, fftwFlags flags);

		/// <summary>
		/// Creates a plan for a 3-dimensional real-to-real DFT
		/// </summary>
		/// <param name="nx">Number of elements in the transform along the first dimension</param>
		/// <param name="ny">Number of elements in the transform along the second dimension</param>
		/// <param name="nz">Number of elements in the transform along the third dimension</param>
		/// <param name="input">Pointer to an array of 8-byte real numbers</param>
		/// <param name="output">Pointer to an array of 8-byte real numbers</param>
		/// <param name="kindx">The kind of real-to-real transform to compute along the first dimension</param>
		/// <param name="kindy">The kind of real-to-real transform to compute along the second dimension</param>
		/// <param name="kindz">The kind of real-to-real transform to compute along the third dimension</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_r2r_3d", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2R_d(int nx, int ny, int nz, IntPtr input, IntPtr output, fftwRealType kindx, fftwRealType kindy, fftwRealType kindz, fftwFlags flags);

		/// <summary>
		/// Creates a plan for an n-dimensional real-to-real DFT
		/// </summary>
		/// <param name="rank">Number of dimensions</param>
		/// <param name="n">Array containing the number of elements in the transform along each dimension</param>
		/// <param name="input">Pointer to an array of 4-byte real numbers</param>
		/// <param name="output">Pointer to an array of 4-byte real numbers</param>
		/// <param name="kind">An array containing the kind of real-to-real transform to compute along each dimension</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_plan_r2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2R_f(int rank, int[] n, IntPtr input, IntPtr output, fftwRealType[] kind, fftwFlags flags);

		/// <summary>
		/// Creates a plan for an n-dimensional real-to-real DFT
		/// </summary>
		/// <param name="rank">Number of dimensions</param>
		/// <param name="n">Array containing the number of elements in the transform along each dimension</param>
		/// <param name="input">Pointer to an array of 8-byte real numbers</param>
		/// <param name="output">Pointer to an array of 8-byte real numbers</param>
		/// <param name="kind">An array containing the kind of real-to-real transform to compute along each dimension</param>
		/// <param name="flags">Flags that specify the behavior of the planner</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_plan_r2r", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern IntPtr fftwCreatePlan_R2R_d(int rank, int[] n, IntPtr input, IntPtr output, fftwRealType[] kind, fftwFlags flags);



		/// <summary>
		/// Returns (approximately) the number of flops used by a certain plan
		/// </summary>
		/// <param name="plan">The plan to measure</param>
		/// <param name="add">Reference to double to hold number of adds</param>
		/// <param name="mul">Reference to double to hold number of muls</param>
		/// <param name="fma">Reference to double to hold number of fmas (fused multiply-add)</param>
		/// <remarks>Total flops ~= add+mul+2*fma or add+mul+fma if fma is supported</remarks>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_flops", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwFlops_f(IntPtr plan, ref double add, ref double mul, ref double fma);

		/// <summary>
		/// Returns (approximately) the number of flops used by a certain plan
		/// </summary>
		/// <param name="plan">The plan to measure</param>
		/// <param name="add">Reference to double to hold number of adds</param>
		/// <param name="mul">Reference to double to hold number of muls</param>
		/// <param name="fma">Reference to double to hold number of fmas (fused multiply-add)</param>
		/// <remarks>Total flops ~= add+mul+2*fma or add+mul+fma if fma is supported</remarks>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_flops", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwFlops_d(IntPtr plan, ref double add, ref double mul, ref double fma);

		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_cost", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern double fftwCost_f(IntPtr plan);

		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_cost", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern double fftwCost_d(IntPtr plan);


		/// <summary>
		/// Outputs a "nerd-readable" version of the specified plan to stdout
		/// </summary>
		/// <param name="plan">The plan to output</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_print_plan", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwPrintPlan_f(IntPtr plan);

		/// <summary>
		/// Outputs a "nerd-readable" version of the specified plan to stdout
		/// </summary>
		/// <param name="plan">The plan to output</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_print_plan", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwPrintPlan_d(IntPtr plan);

		/// <summary>
		/// Exports the accumulated Wisdom to the provided filename
		/// </summary>
		/// <param name="filename">The target filename</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_export_wisdom_to_filename", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExportWisdomToFilename_f(string filename);

		/// <summary>
		/// Exports the accumulated Wisdom to the provided filename
		/// </summary>
		/// <param name="filename">The target filename</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_export_wisdom_to_filename", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwExportWisdomToFilename_d(string filename);
		
		/// <summary>
		/// Imports Wisdom from provided filename
		/// </summary>
		/// <param name="filename">The filename to read from</param>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_import_wisdom_from_filename", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwImportWisdomFromFilename_f(string filename);

		/// <summary>
		/// Imports Wisdom from provided filename
		/// </summary>
		/// <param name="filename">The filename to read from</param>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_import_wisdom_from_filename", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwImportWisdomFromFilename_d(string filename);

		/// <summary>
		/// Forgets the Wisdom
		/// </summary>
		[DllImport(FFTWSinglePrecisionFilePath, EntryPoint = "fftwf_forget_wisdom", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwForgetWisdom_f();

		/// <summary>
		/// Forgets the Wisdom
		/// </summary>
		[DllImport(FFTWDoublePrecisionFilePath, EntryPoint = "fftw_forget_wisdom", ExactSpelling = true, CallingConvention = FFTWCallingConvertion)]
		private static extern void fftwForgetWisdom_d();

		#endregion
#endif
	}

}

