using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Pathfinding.Jobs
{
	// Token: 0x0200017B RID: 379
	public class DisposeArena
	{
		// Token: 0x06000A8F RID: 2703 RVA: 0x0003BF3D File Offset: 0x0003A13D
		public void Add<[IsUnmanaged] T>(NativeArray<T> data) where T : struct, ValueType
		{
			if (this.buffer == null)
			{
				this.buffer = ListPool<NativeArray<byte>>.Claim();
			}
			this.buffer.Add(data.Reinterpret<byte>(UnsafeUtility.SizeOf<T>()));
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0003BF6C File Offset: 0x0003A16C
		public unsafe void Add<[IsUnmanaged] T>(NativeList<T> data) where T : struct, ValueType
		{
			NativeList<byte> nativeList = *UnsafeUtility.As<NativeList<T>, NativeList<byte>>(ref data);
			if (this.buffer2 == null)
			{
				this.buffer2 = ListPool<NativeList<byte>>.Claim();
			}
			this.buffer2.Add(nativeList);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0003BFA8 File Offset: 0x0003A1A8
		public unsafe void Add<[IsUnmanaged] T>(NativeQueue<T> data) where T : struct, ValueType
		{
			NativeQueue<byte> nativeQueue = *UnsafeUtility.As<NativeQueue<T>, NativeQueue<byte>>(ref data);
			if (this.buffer3 == null)
			{
				this.buffer3 = ListPool<NativeQueue<byte>>.Claim();
			}
			this.buffer3.Add(nativeQueue);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0003BFE4 File Offset: 0x0003A1E4
		public unsafe void Remove<[IsUnmanaged] T>(NativeArray<T> data) where T : struct, ValueType
		{
			if (this.buffer == null)
			{
				return;
			}
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(data);
			for (int i = 0; i < this.buffer.Count; i++)
			{
				if (NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.buffer[i]) == unsafeBufferPointerWithoutChecks)
				{
					this.buffer.RemoveAtSwapBack(i);
					return;
				}
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0003C038 File Offset: 0x0003A238
		public void Add<T>(T data) where T : IArenaDisposable
		{
			data.DisposeWith(this);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0003C048 File Offset: 0x0003A248
		public void Add(GCHandle handle)
		{
			if (this.gcHandles == null)
			{
				this.gcHandles = ListPool<GCHandle>.Claim();
			}
			this.gcHandles.Add(handle);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0003C06C File Offset: 0x0003A26C
		public void DisposeAll()
		{
			if (this.buffer != null)
			{
				for (int i = 0; i < this.buffer.Count; i++)
				{
					this.buffer[i].Dispose();
				}
				ListPool<NativeArray<byte>>.Release(ref this.buffer);
			}
			if (this.buffer2 != null)
			{
				for (int j = 0; j < this.buffer2.Count; j++)
				{
					this.buffer2[j].Dispose();
				}
				ListPool<NativeList<byte>>.Release(ref this.buffer2);
			}
			if (this.buffer3 != null)
			{
				for (int k = 0; k < this.buffer3.Count; k++)
				{
					this.buffer3[k].Dispose();
				}
				ListPool<NativeQueue<byte>>.Release(ref this.buffer3);
			}
			if (this.gcHandles != null)
			{
				for (int l = 0; l < this.gcHandles.Count; l++)
				{
					this.gcHandles[l].Free();
				}
				ListPool<GCHandle>.Release(ref this.gcHandles);
			}
		}

		// Token: 0x0400072A RID: 1834
		private List<NativeArray<byte>> buffer;

		// Token: 0x0400072B RID: 1835
		private List<NativeList<byte>> buffer2;

		// Token: 0x0400072C RID: 1836
		private List<NativeQueue<byte>> buffer3;

		// Token: 0x0400072D RID: 1837
		private List<GCHandle> gcHandles;
	}
}
