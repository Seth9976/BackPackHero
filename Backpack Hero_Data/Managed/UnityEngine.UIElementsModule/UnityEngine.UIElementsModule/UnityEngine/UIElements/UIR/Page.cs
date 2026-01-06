using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200033E RID: 830
	internal class Page : IDisposable
	{
		// Token: 0x06001A83 RID: 6787 RVA: 0x000734F6 File Offset: 0x000716F6
		public Page(uint vertexMaxCount, uint indexMaxCount, uint maxQueuedFrameCount, bool mockPage)
		{
			vertexMaxCount = Math.Min(vertexMaxCount, 65536U);
			this.vertices = new Page.DataSet<Vertex>(Utility.GPUBufferType.Vertex, vertexMaxCount, maxQueuedFrameCount, 32U, mockPage);
			this.indices = new Page.DataSet<ushort>(Utility.GPUBufferType.Index, indexMaxCount, maxQueuedFrameCount, 32U, mockPage);
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00073531 File Offset: 0x00071731
		// (set) Token: 0x06001A85 RID: 6789 RVA: 0x00073539 File Offset: 0x00071739
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001A86 RID: 6790 RVA: 0x00073542 File Offset: 0x00071742
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00073554 File Offset: 0x00071754
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.indices.Dispose();
					this.vertices.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00073598 File Offset: 0x00071798
		public bool isEmpty
		{
			get
			{
				return this.vertices.allocator.isEmpty && this.indices.allocator.isEmpty;
			}
		}

		// Token: 0x04000C90 RID: 3216
		public Page.DataSet<Vertex> vertices;

		// Token: 0x04000C91 RID: 3217
		public Page.DataSet<ushort> indices;

		// Token: 0x04000C92 RID: 3218
		public Page next;

		// Token: 0x04000C93 RID: 3219
		public int framesEmpty;

		// Token: 0x0200033F RID: 831
		public class DataSet<T> : IDisposable where T : struct
		{
			// Token: 0x06001A89 RID: 6793 RVA: 0x000735D0 File Offset: 0x000717D0
			public DataSet(Utility.GPUBufferType bufferType, uint totalCount, uint maxQueuedFrameCount, uint updateRangePoolSize, bool mockBuffer)
			{
				bool flag = !mockBuffer;
				if (flag)
				{
					this.gpuData = new Utility.GPUBuffer<T>((int)totalCount, bufferType);
				}
				this.cpuData = new NativeArray<T>((int)totalCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.allocator = new GPUBufferAllocator(totalCount);
				bool flag2 = !mockBuffer;
				if (flag2)
				{
					this.m_ElemStride = (uint)this.gpuData.ElementStride;
				}
				this.m_UpdateRangePoolSize = updateRangePoolSize;
				uint num = this.m_UpdateRangePoolSize * maxQueuedFrameCount;
				this.updateRanges = new NativeArray<GfxUpdateBufferRange>((int)num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_UpdateRangeMin = uint.MaxValue;
				this.m_UpdateRangeMax = 0U;
				this.m_UpdateRangesEnqueued = 0U;
				this.m_UpdateRangesBatchStart = 0U;
			}

			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0007366A File Offset: 0x0007186A
			// (set) Token: 0x06001A8B RID: 6795 RVA: 0x00073672 File Offset: 0x00071872
			private protected bool disposed { protected get; private set; }

			// Token: 0x06001A8C RID: 6796 RVA: 0x0007367B File Offset: 0x0007187B
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06001A8D RID: 6797 RVA: 0x00073690 File Offset: 0x00071890
			public void Dispose(bool disposing)
			{
				bool disposed = this.disposed;
				if (!disposed)
				{
					if (disposing)
					{
						Utility.GPUBuffer<T> gpubuffer = this.gpuData;
						if (gpubuffer != null)
						{
							gpubuffer.Dispose();
						}
						this.cpuData.Dispose();
						this.updateRanges.Dispose();
					}
					this.disposed = true;
				}
			}

			// Token: 0x06001A8E RID: 6798 RVA: 0x000736E8 File Offset: 0x000718E8
			public void RegisterUpdate(uint start, uint size)
			{
				Debug.Assert((ulong)(start + size) <= (ulong)((long)this.cpuData.Length));
				int num = (int)(this.m_UpdateRangesBatchStart + this.m_UpdateRangesEnqueued);
				bool flag = this.m_UpdateRangesEnqueued > 0U;
				if (flag)
				{
					int num2 = num - 1;
					GfxUpdateBufferRange gfxUpdateBufferRange = this.updateRanges[num2];
					uint num3 = start * this.m_ElemStride;
					bool flag2 = gfxUpdateBufferRange.offsetFromWriteStart + gfxUpdateBufferRange.size == num3;
					if (flag2)
					{
						this.updateRanges[num2] = new GfxUpdateBufferRange
						{
							source = gfxUpdateBufferRange.source,
							offsetFromWriteStart = gfxUpdateBufferRange.offsetFromWriteStart,
							size = gfxUpdateBufferRange.size + size * this.m_ElemStride
						};
						this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
						return;
					}
				}
				this.m_UpdateRangeMin = Math.Min(this.m_UpdateRangeMin, start);
				this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
				bool flag3 = this.m_UpdateRangesEnqueued == this.m_UpdateRangePoolSize;
				if (flag3)
				{
					this.m_UpdateRangesSaturated = true;
				}
				else
				{
					UIntPtr uintPtr;
					uintPtr..ctor(this.cpuData.Slice((int)start, (int)size).GetUnsafeReadOnlyPtr<T>());
					this.updateRanges[num] = new GfxUpdateBufferRange
					{
						source = uintPtr,
						offsetFromWriteStart = start * this.m_ElemStride,
						size = size * this.m_ElemStride
					};
					this.m_UpdateRangesEnqueued += 1U;
				}
			}

			// Token: 0x06001A8F RID: 6799 RVA: 0x00073874 File Offset: 0x00071A74
			private bool HasMappedBufferRange()
			{
				return Utility.HasMappedBufferRange();
			}

			// Token: 0x06001A90 RID: 6800 RVA: 0x0007388C File Offset: 0x00071A8C
			public void SendUpdates()
			{
				bool flag = this.HasMappedBufferRange();
				if (flag)
				{
					this.SendPartialRanges();
				}
				else
				{
					this.SendFullRange();
				}
			}

			// Token: 0x06001A91 RID: 6801 RVA: 0x000738B4 File Offset: 0x00071AB4
			public void SendFullRange()
			{
				uint num = (uint)((long)this.cpuData.Length * (long)((ulong)this.m_ElemStride));
				this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
				{
					source = new UIntPtr(this.cpuData.GetUnsafeReadOnlyPtr<T>()),
					offsetFromWriteStart = 0U,
					size = num
				};
				Utility.GPUBuffer<T> gpubuffer = this.gpuData;
				if (gpubuffer != null)
				{
					gpubuffer.UpdateRanges(this.updateRanges.Slice((int)this.m_UpdateRangesBatchStart, 1), 0, (int)num);
				}
				this.ResetUpdateState();
			}

			// Token: 0x06001A92 RID: 6802 RVA: 0x00073948 File Offset: 0x00071B48
			public void SendPartialRanges()
			{
				bool flag = this.m_UpdateRangesEnqueued == 0U;
				if (!flag)
				{
					bool updateRangesSaturated = this.m_UpdateRangesSaturated;
					if (updateRangesSaturated)
					{
						uint num = this.m_UpdateRangeMax - this.m_UpdateRangeMin;
						this.m_UpdateRangesEnqueued = 1U;
						this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
						{
							source = new UIntPtr(this.cpuData.Slice((int)this.m_UpdateRangeMin, (int)num).GetUnsafeReadOnlyPtr<T>()),
							offsetFromWriteStart = this.m_UpdateRangeMin * this.m_ElemStride,
							size = num * this.m_ElemStride
						};
					}
					uint num2 = this.m_UpdateRangeMin * this.m_ElemStride;
					uint num3 = this.m_UpdateRangeMax * this.m_ElemStride;
					bool flag2 = num2 > 0U;
					if (flag2)
					{
						for (uint num4 = 0U; num4 < this.m_UpdateRangesEnqueued; num4 += 1U)
						{
							int num5 = (int)(num4 + this.m_UpdateRangesBatchStart);
							this.updateRanges[num5] = new GfxUpdateBufferRange
							{
								source = this.updateRanges[num5].source,
								offsetFromWriteStart = this.updateRanges[num5].offsetFromWriteStart - num2,
								size = this.updateRanges[num5].size
							};
						}
					}
					Utility.GPUBuffer<T> gpubuffer = this.gpuData;
					if (gpubuffer != null)
					{
						gpubuffer.UpdateRanges(this.updateRanges.Slice((int)this.m_UpdateRangesBatchStart, (int)this.m_UpdateRangesEnqueued), (int)num2, (int)num3);
					}
					this.ResetUpdateState();
				}
			}

			// Token: 0x06001A93 RID: 6803 RVA: 0x00073ADC File Offset: 0x00071CDC
			private void ResetUpdateState()
			{
				this.m_UpdateRangeMin = uint.MaxValue;
				this.m_UpdateRangeMax = 0U;
				this.m_UpdateRangesEnqueued = 0U;
				this.m_UpdateRangesBatchStart += this.m_UpdateRangePoolSize;
				bool flag = (ulong)this.m_UpdateRangesBatchStart >= (ulong)((long)this.updateRanges.Length);
				if (flag)
				{
					this.m_UpdateRangesBatchStart = 0U;
				}
				this.m_UpdateRangesSaturated = false;
			}

			// Token: 0x04000C95 RID: 3221
			public Utility.GPUBuffer<T> gpuData;

			// Token: 0x04000C96 RID: 3222
			public NativeArray<T> cpuData;

			// Token: 0x04000C97 RID: 3223
			public NativeArray<GfxUpdateBufferRange> updateRanges;

			// Token: 0x04000C98 RID: 3224
			public GPUBufferAllocator allocator;

			// Token: 0x04000C99 RID: 3225
			private readonly uint m_UpdateRangePoolSize;

			// Token: 0x04000C9A RID: 3226
			private uint m_ElemStride;

			// Token: 0x04000C9B RID: 3227
			private uint m_UpdateRangeMin;

			// Token: 0x04000C9C RID: 3228
			private uint m_UpdateRangeMax;

			// Token: 0x04000C9D RID: 3229
			private uint m_UpdateRangesEnqueued;

			// Token: 0x04000C9E RID: 3230
			private uint m_UpdateRangesBatchStart;

			// Token: 0x04000C9F RID: 3231
			private bool m_UpdateRangesSaturated;
		}
	}
}
