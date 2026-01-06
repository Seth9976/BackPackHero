using System;
using Unity.Collections;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000003 RID: 3
	internal class VertexBuffer
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public int bufferCount
		{
			get
			{
				return this.m_Buffers.Length;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002074 File Offset: 0x00000274
		public VertexBuffer(int id, int size, bool needDoubleBuffering)
		{
			this.m_Id = id;
			int num = (needDoubleBuffering ? 2 : 1);
			this.m_Buffers = new NativeByteArray[num];
			for (int i = 0; i < num; i++)
			{
				this.m_Buffers[i] = new NativeByteArray(new NativeArray<byte>(size, Allocator.Persistent, NativeArrayOptions.UninitializedMemory));
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020D1 File Offset: 0x000002D1
		public override int GetHashCode()
		{
			return this.m_Id;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D9 File Offset: 0x000002D9
		private static int GetCurrentFrame()
		{
			return Time.frameCount;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E0 File Offset: 0x000002E0
		public NativeByteArray GetBuffer(int size)
		{
			if (!this.m_IsActive)
			{
				Debug.LogError(string.Format("Cannot request deactivated buffer. ID: {0}", this.m_Id));
				return null;
			}
			this.m_ActiveIndex = (this.m_ActiveIndex + 1) % this.m_Buffers.Length;
			if (this.m_Buffers[this.m_ActiveIndex].Length != size)
			{
				this.ResizeBuffer(this.m_ActiveIndex, size);
			}
			return this.m_Buffers[this.m_ActiveIndex];
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002157 File Offset: 0x00000357
		private void ResizeBuffer(int bufferId, int newSize)
		{
			this.m_Buffers[bufferId].Dispose();
			this.m_Buffers[bufferId] = new NativeByteArray(new NativeArray<byte>(newSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217B File Offset: 0x0000037B
		public void Deactivate()
		{
			if (!this.m_IsActive)
			{
				return;
			}
			this.m_IsActive = false;
			this.m_DeactivateFrame = VertexBuffer.GetCurrentFrame();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002198 File Offset: 0x00000398
		public void Dispose()
		{
			for (int i = 0; i < this.m_Buffers.Length; i++)
			{
				if (this.m_Buffers[i].IsCreated)
				{
					this.m_Buffers[i].Dispose();
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D4 File Offset: 0x000003D4
		public bool IsSafeToDispose()
		{
			return !this.m_IsActive && VertexBuffer.GetCurrentFrame() > this.m_DeactivateFrame;
		}

		// Token: 0x04000002 RID: 2
		private readonly int m_Id;

		// Token: 0x04000003 RID: 3
		private bool m_IsActive = true;

		// Token: 0x04000004 RID: 4
		private int m_DeactivateFrame = -1;

		// Token: 0x04000005 RID: 5
		private NativeByteArray[] m_Buffers;

		// Token: 0x04000006 RID: 6
		private int m_ActiveIndex;
	}
}
