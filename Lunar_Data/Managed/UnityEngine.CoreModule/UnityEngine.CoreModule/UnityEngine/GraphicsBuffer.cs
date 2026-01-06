using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014B RID: 331
	[NativeHeader("Runtime/Export/Graphics/GraphicsBuffer.bindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/GraphicsBuffer.h")]
	public sealed class GraphicsBuffer : IDisposable
	{
		// Token: 0x06000DDC RID: 3548 RVA: 0x0001237C File Offset: 0x0001057C
		~GraphicsBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000123B0 File Offset: 0x000105B0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000123C4 File Offset: 0x000105C4
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				GraphicsBuffer.DestroyBuffer(this);
			}
			else
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Debug.LogWarning("GarbageCollector disposing of GraphicsBuffer. Please use GraphicsBuffer.Release() or .Dispose() to manually release the buffer.");
				}
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00012410 File Offset: 0x00010610
		private static bool RequiresCompute(GraphicsBuffer.Target target)
		{
			GraphicsBuffer.Target target2 = GraphicsBuffer.Target.Structured | GraphicsBuffer.Target.Raw | GraphicsBuffer.Target.Append | GraphicsBuffer.Target.Counter | GraphicsBuffer.Target.IndirectArguments;
			return (target & target2) > (GraphicsBuffer.Target)0;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00012430 File Offset: 0x00010630
		private static bool IsVertexIndexOrCopyOnly(GraphicsBuffer.Target target)
		{
			GraphicsBuffer.Target target2 = GraphicsBuffer.Target.Vertex | GraphicsBuffer.Target.Index | GraphicsBuffer.Target.CopySource | GraphicsBuffer.Target.CopyDestination;
			return (target & target2) == target;
		}

		// Token: 0x06000DE1 RID: 3553
		[FreeFunction("GraphicsBuffer_Bindings::InitBuffer")]
		[MethodImpl(4096)]
		private static extern IntPtr InitBuffer(GraphicsBuffer.Target target, int count, int stride);

		// Token: 0x06000DE2 RID: 3554
		[FreeFunction("GraphicsBuffer_Bindings::DestroyBuffer")]
		[MethodImpl(4096)]
		private static extern void DestroyBuffer(GraphicsBuffer buf);

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0001244C File Offset: 0x0001064C
		public GraphicsBuffer(GraphicsBuffer.Target target, int count, int stride)
		{
			bool flag = GraphicsBuffer.RequiresCompute(target) && !SystemInfo.supportsComputeShaders;
			if (flag)
			{
				throw new ArgumentException("Attempting to create a graphics buffer that requires compute shader support, but compute shaders are not supported on this platform. Target: " + target.ToString());
			}
			bool flag2 = count <= 0;
			if (flag2)
			{
				throw new ArgumentException("Attempting to create a zero length graphics buffer", "count");
			}
			bool flag3 = stride <= 0;
			if (flag3)
			{
				throw new ArgumentException("Attempting to create a graphics buffer with a negative or null stride", "stride");
			}
			bool flag4 = (target & GraphicsBuffer.Target.Index) != (GraphicsBuffer.Target)0 && stride != 2 && stride != 4;
			if (flag4)
			{
				throw new ArgumentException("Attempting to create an index buffer with an invalid stride: " + stride.ToString(), "stride");
			}
			bool flag5 = !GraphicsBuffer.IsVertexIndexOrCopyOnly(target) && stride % 4 != 0;
			if (flag5)
			{
				throw new ArgumentException("Stride must be a multiple of 4 unless the buffer is only used as a vertex buffer and/or index buffer ", "stride");
			}
			long num = (long)count * (long)stride;
			long maxGraphicsBufferSize = SystemInfo.maxGraphicsBufferSize;
			bool flag6 = num > maxGraphicsBufferSize;
			if (flag6)
			{
				throw new ArgumentException(string.Format("The total size of the graphics buffer ({0} bytes) exceeds the maximum buffer size. Maximum supported buffer size: {1} bytes.", num, maxGraphicsBufferSize));
			}
			this.m_Ptr = GraphicsBuffer.InitBuffer(target, count, stride);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0001256F File Offset: 0x0001076F
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06000DE5 RID: 3557
		[FreeFunction("GraphicsBuffer_Bindings::IsValidBuffer")]
		[MethodImpl(4096)]
		private static extern bool IsValidBuffer(GraphicsBuffer buf);

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0001257C File Offset: 0x0001077C
		public bool IsValid()
		{
			return this.m_Ptr != IntPtr.Zero && GraphicsBuffer.IsValidBuffer(this);
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000DE7 RID: 3559
		public extern int count
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000DE8 RID: 3560
		public extern int stride
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000DE9 RID: 3561
		public extern GraphicsBuffer.Target target
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x000125AC File Offset: 0x000107AC
		[SecuritySafeCritical]
		public void SetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetData(data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00012614 File Offset: 0x00010814
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to GraphicsBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00012685 File Offset: 0x00010885
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data) where T : struct
		{
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000126A8 File Offset: 0x000108A8
		[SecuritySafeCritical]
		public void SetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetData(data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0001274C File Offset: 0x0001094C
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to GraphicsBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000127FC File Offset: 0x000109FC
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06000DF0 RID: 3568
		[SecurityCritical]
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06000DF1 RID: 3569
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		[MethodImpl(4096)]
		private extern void InternalSetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0001286C File Offset: 0x00010A6C
		[SecurityCritical]
		public void GetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalGetData(data, 0, 0, data.Length, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000128D4 File Offset: 0x00010AD4
		[SecurityCritical]
		public void GetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count argument (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalGetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000DF4 RID: 3572
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalGetData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		[MethodImpl(4096)]
		private extern void InternalGetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x06000DF5 RID: 3573
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalGetNativeBufferPtr", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern IntPtr GetNativeBufferPtr();

		// Token: 0x170002CB RID: 715
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x00012978 File Offset: 0x00010B78
		public string name
		{
			set
			{
				this.SetName(value);
			}
		}

		// Token: 0x06000DF7 RID: 3575
		[FreeFunction(Name = "GraphicsBuffer_Bindings::SetName", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetName(string name);

		// Token: 0x06000DF8 RID: 3576
		[MethodImpl(4096)]
		public extern void SetCounterValue(uint counterValue);

		// Token: 0x06000DF9 RID: 3577
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(4096)]
		private static extern void CopyCountCC(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes);

		// Token: 0x06000DFA RID: 3578
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(4096)]
		private static extern void CopyCountGC(GraphicsBuffer src, ComputeBuffer dst, int dstOffsetBytes);

		// Token: 0x06000DFB RID: 3579
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(4096)]
		private static extern void CopyCountCG(ComputeBuffer src, GraphicsBuffer dst, int dstOffsetBytes);

		// Token: 0x06000DFC RID: 3580
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		[MethodImpl(4096)]
		private static extern void CopyCountGG(GraphicsBuffer src, GraphicsBuffer dst, int dstOffsetBytes);

		// Token: 0x06000DFD RID: 3581 RVA: 0x00012982 File Offset: 0x00010B82
		public static void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0001298E File Offset: 0x00010B8E
		public static void CopyCount(GraphicsBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0001299A File Offset: 0x00010B9A
		public static void CopyCount(ComputeBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCG(src, dst, dstOffsetBytes);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000129A6 File Offset: 0x00010BA6
		public static void CopyCount(GraphicsBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGG(src, dst, dstOffsetBytes);
		}

		// Token: 0x04000413 RID: 1043
		internal IntPtr m_Ptr;

		// Token: 0x0200014C RID: 332
		[Flags]
		public enum Target
		{
			// Token: 0x04000415 RID: 1045
			Vertex = 1,
			// Token: 0x04000416 RID: 1046
			Index = 2,
			// Token: 0x04000417 RID: 1047
			CopySource = 4,
			// Token: 0x04000418 RID: 1048
			CopyDestination = 8,
			// Token: 0x04000419 RID: 1049
			Structured = 16,
			// Token: 0x0400041A RID: 1050
			Raw = 32,
			// Token: 0x0400041B RID: 1051
			Append = 64,
			// Token: 0x0400041C RID: 1052
			Counter = 128,
			// Token: 0x0400041D RID: 1053
			IndirectArguments = 256,
			// Token: 0x0400041E RID: 1054
			Constant = 512
		}

		// Token: 0x0200014D RID: 333
		public struct IndirectDrawArgs
		{
			// Token: 0x170002CC RID: 716
			// (get) Token: 0x06000E01 RID: 3585 RVA: 0x000129B2 File Offset: 0x00010BB2
			// (set) Token: 0x06000E02 RID: 3586 RVA: 0x000129BA File Offset: 0x00010BBA
			public uint vertexCountPerInstance { readonly get; set; }

			// Token: 0x170002CD RID: 717
			// (get) Token: 0x06000E03 RID: 3587 RVA: 0x000129C3 File Offset: 0x00010BC3
			// (set) Token: 0x06000E04 RID: 3588 RVA: 0x000129CB File Offset: 0x00010BCB
			public uint instanceCount { readonly get; set; }

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06000E05 RID: 3589 RVA: 0x000129D4 File Offset: 0x00010BD4
			// (set) Token: 0x06000E06 RID: 3590 RVA: 0x000129DC File Offset: 0x00010BDC
			public uint startVertex { readonly get; set; }

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06000E07 RID: 3591 RVA: 0x000129E5 File Offset: 0x00010BE5
			// (set) Token: 0x06000E08 RID: 3592 RVA: 0x000129ED File Offset: 0x00010BED
			public uint startInstance { readonly get; set; }

			// Token: 0x0400041F RID: 1055
			public const int size = 16;
		}

		// Token: 0x0200014E RID: 334
		public struct IndirectDrawIndexedArgs
		{
			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06000E09 RID: 3593 RVA: 0x000129F6 File Offset: 0x00010BF6
			// (set) Token: 0x06000E0A RID: 3594 RVA: 0x000129FE File Offset: 0x00010BFE
			public uint indexCountPerInstance { readonly get; set; }

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00012A07 File Offset: 0x00010C07
			// (set) Token: 0x06000E0C RID: 3596 RVA: 0x00012A0F File Offset: 0x00010C0F
			public uint instanceCount { readonly get; set; }

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00012A18 File Offset: 0x00010C18
			// (set) Token: 0x06000E0E RID: 3598 RVA: 0x00012A20 File Offset: 0x00010C20
			public uint startIndex { readonly get; set; }

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00012A29 File Offset: 0x00010C29
			// (set) Token: 0x06000E10 RID: 3600 RVA: 0x00012A31 File Offset: 0x00010C31
			public uint baseVertexIndex { readonly get; set; }

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00012A3A File Offset: 0x00010C3A
			// (set) Token: 0x06000E12 RID: 3602 RVA: 0x00012A42 File Offset: 0x00010C42
			public uint startInstance { readonly get; set; }

			// Token: 0x04000424 RID: 1060
			public const int size = 20;
		}
	}
}
