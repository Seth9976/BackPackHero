using System;
using System.Runtime.InteropServices;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000077 RID: 119
	internal class DeferredShaderData : IDisposable
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x00017BE4 File Offset: 0x00015DE4
		private DeferredShaderData()
		{
			this.m_PreTiles = new NativeArray<PreTile>[3];
			this.m_Buffers = new ComputeBuffer[64];
			this.m_BufferInfos = new DeferredShaderData.ComputeBufferInfo[64];
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00017C12 File Offset: 0x00015E12
		internal static DeferredShaderData instance
		{
			get
			{
				if (DeferredShaderData.m_Instance == null)
				{
					DeferredShaderData.m_Instance = new DeferredShaderData();
				}
				return DeferredShaderData.m_Instance;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00017C2C File Offset: 0x00015E2C
		public void Dispose()
		{
			this.DisposeNativeArrays<PreTile>(ref this.m_PreTiles);
			for (int i = 0; i < this.m_Buffers.Length; i++)
			{
				if (this.m_Buffers[i] != null)
				{
					this.m_Buffers[i].Dispose();
					this.m_Buffers[i] = null;
				}
			}
			this.m_BufferCount = 0;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00017C7F File Offset: 0x00015E7F
		internal void ResetBuffers()
		{
			this.m_FrameIndex += 1U;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00017C8F File Offset: 0x00015E8F
		internal NativeArray<PreTile> GetPreTiles(int level, int count)
		{
			return this.GetOrUpdateNativeArray<PreTile>(ref this.m_PreTiles, level, count);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00017CA0 File Offset: 0x00015EA0
		internal ComputeBuffer ReserveBuffer<T>(int count, bool asCBuffer) where T : struct
		{
			int num = Marshal.SizeOf<T>();
			int num2 = (asCBuffer ? (DeferredShaderData.Align(num * count, 16) / num) : count);
			return this.GetOrUpdateBuffer(num2, num, asCBuffer);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00017CD0 File Offset: 0x00015ED0
		private NativeArray<T> GetOrUpdateNativeArray<T>(ref NativeArray<T>[] nativeArrays, int level, int count) where T : struct
		{
			if (!nativeArrays[level].IsCreated)
			{
				nativeArrays[level] = new NativeArray<T>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
			else if (count > nativeArrays[level].Length)
			{
				nativeArrays[level].Dispose();
				nativeArrays[level] = new NativeArray<T>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
			return nativeArrays[level];
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00017D34 File Offset: 0x00015F34
		private void DisposeNativeArrays<T>(ref NativeArray<T>[] nativeArrays) where T : struct
		{
			for (int i = 0; i < nativeArrays.Length; i++)
			{
				if (nativeArrays[i].IsCreated)
				{
					nativeArrays[i].Dispose();
				}
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00017D6C File Offset: 0x00015F6C
		private ComputeBuffer GetOrUpdateBuffer(int count, int stride, bool isConstantBuffer)
		{
			ComputeBufferType computeBufferType = (isConstantBuffer ? ComputeBufferType.Constant : ComputeBufferType.Structured);
			int maxQueuedFrames = QualitySettings.maxQueuedFrames;
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				int num = (this.m_CachedBufferIndex + i + 1) % this.m_BufferCount;
				if (DeferredShaderData.IsLessCircular(this.m_BufferInfos[num].frameUsed + (uint)maxQueuedFrames, this.m_FrameIndex) && this.m_BufferInfos[num].type == computeBufferType && this.m_Buffers[num].count == count && this.m_Buffers[num].stride == stride)
				{
					this.m_BufferInfos[num].frameUsed = this.m_FrameIndex;
					this.m_CachedBufferIndex = num;
					return this.m_Buffers[num];
				}
			}
			if (this.m_BufferCount == this.m_Buffers.Length)
			{
				ComputeBuffer[] array = new ComputeBuffer[this.m_BufferCount * 2];
				for (int j = 0; j < this.m_BufferCount; j++)
				{
					array[j] = this.m_Buffers[j];
				}
				this.m_Buffers = array;
				DeferredShaderData.ComputeBufferInfo[] array2 = new DeferredShaderData.ComputeBufferInfo[this.m_BufferCount * 2];
				for (int k = 0; k < this.m_BufferCount; k++)
				{
					array2[k] = this.m_BufferInfos[k];
				}
				this.m_BufferInfos = array2;
			}
			this.m_Buffers[this.m_BufferCount] = new ComputeBuffer(count, stride, computeBufferType, ComputeBufferMode.Immutable);
			this.m_BufferInfos[this.m_BufferCount].frameUsed = this.m_FrameIndex;
			this.m_BufferInfos[this.m_BufferCount].type = computeBufferType;
			this.m_CachedBufferIndex = this.m_BufferCount;
			ComputeBuffer[] buffers = this.m_Buffers;
			int bufferCount = this.m_BufferCount;
			this.m_BufferCount = bufferCount + 1;
			return buffers[bufferCount];
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00017F28 File Offset: 0x00016128
		private void DisposeBuffers(ComputeBuffer[,] buffers)
		{
			for (int i = 0; i < buffers.GetLength(0); i++)
			{
				for (int j = 0; j < buffers.GetLength(1); j++)
				{
					if (buffers[i, j] != null)
					{
						buffers[i, j].Dispose();
						buffers[i, j] = null;
					}
				}
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00017F79 File Offset: 0x00016179
		private static bool IsLessCircular(uint a, uint b)
		{
			return a != b && b - a < 2147483648U;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00017F8B File Offset: 0x0001618B
		private static int Align(int s, int alignment)
		{
			return (s + alignment - 1) / alignment * alignment;
		}

		// Token: 0x040002FB RID: 763
		private static DeferredShaderData m_Instance;

		// Token: 0x040002FC RID: 764
		private NativeArray<PreTile>[] m_PreTiles;

		// Token: 0x040002FD RID: 765
		private ComputeBuffer[] m_Buffers;

		// Token: 0x040002FE RID: 766
		private DeferredShaderData.ComputeBufferInfo[] m_BufferInfos;

		// Token: 0x040002FF RID: 767
		private int m_BufferCount;

		// Token: 0x04000300 RID: 768
		private int m_CachedBufferIndex;

		// Token: 0x04000301 RID: 769
		private uint m_FrameIndex;

		// Token: 0x0200016B RID: 363
		private struct ComputeBufferInfo
		{
			// Token: 0x04000951 RID: 2385
			public uint frameUsed;

			// Token: 0x04000952 RID: 2386
			public ComputeBufferType type;
		}
	}
}
