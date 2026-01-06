using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000004 RID: 4
	internal class BufferManager : ScriptableObject
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021F0 File Offset: 0x000003F0
		public int bufferCount
		{
			get
			{
				int num = 0;
				foreach (VertexBuffer vertexBuffer in this.m_Buffers.Values)
				{
					num += vertexBuffer.bufferCount;
				}
				return num;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002250 File Offset: 0x00000450
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002258 File Offset: 0x00000458
		public bool needDoubleBuffering { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002264 File Offset: 0x00000464
		public static BufferManager instance
		{
			get
			{
				if (BufferManager.s_Instance == null)
				{
					BufferManager[] array = Resources.FindObjectsOfTypeAll<BufferManager>();
					if (array.Length != 0)
					{
						BufferManager.s_Instance = array[0];
					}
					else
					{
						BufferManager.s_Instance = ScriptableObject.CreateInstance<BufferManager>();
					}
					BufferManager.s_Instance.hideFlags = HideFlags.HideAndDontSave;
				}
				return BufferManager.s_Instance;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022AD File Offset: 0x000004AD
		private void OnEnable()
		{
			if (BufferManager.s_Instance == null)
			{
				BufferManager.s_Instance = this;
			}
			this.needDoubleBuffering = SystemInfo.renderingThreadingMode > RenderingThreadingMode.Direct;
			Application.onBeforeRender += this.Update;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E1 File Offset: 0x000004E1
		private void OnDisable()
		{
			if (BufferManager.s_Instance == this)
			{
				BufferManager.s_Instance = null;
			}
			this.ForceClearBuffers();
			Application.onBeforeRender -= this.Update;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002310 File Offset: 0x00000510
		private void ForceClearBuffers()
		{
			foreach (VertexBuffer vertexBuffer in this.m_Buffers.Values)
			{
				vertexBuffer.Dispose();
			}
			foreach (VertexBuffer vertexBuffer2 in this.m_BuffersToDispose)
			{
				vertexBuffer2.Dispose();
			}
			this.m_Buffers.Clear();
			this.m_BuffersToDispose.Clear();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023BC File Offset: 0x000005BC
		public NativeByteArray GetBuffer(int id, int bufferSize)
		{
			VertexBuffer vertexBuffer;
			if (!this.m_Buffers.TryGetValue(id, out vertexBuffer))
			{
				vertexBuffer = this.CreateBuffer(id, bufferSize);
			}
			if (vertexBuffer == null)
			{
				return null;
			}
			return vertexBuffer.GetBuffer(bufferSize);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023F0 File Offset: 0x000005F0
		private VertexBuffer CreateBuffer(int id, int bufferSize)
		{
			if (bufferSize < 1)
			{
				Debug.LogError("Cannot create a buffer smaller than 1 byte.");
				return null;
			}
			VertexBuffer vertexBuffer = new VertexBuffer(id, bufferSize, this.needDoubleBuffering);
			this.m_Buffers.Add(id, vertexBuffer);
			return vertexBuffer;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000242C File Offset: 0x0000062C
		public void ReturnBuffer(int id)
		{
			VertexBuffer vertexBuffer;
			if (this.m_Buffers.TryGetValue(id, out vertexBuffer))
			{
				vertexBuffer.Deactivate();
				this.m_BuffersToDispose.Enqueue(vertexBuffer);
				this.m_Buffers.Remove(id);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002468 File Offset: 0x00000668
		private void Update()
		{
			while (this.m_BuffersToDispose.Count > 0 && this.m_BuffersToDispose.Peek().IsSafeToDispose())
			{
				this.m_BuffersToDispose.Dequeue().Dispose();
			}
		}

		// Token: 0x04000007 RID: 7
		private static BufferManager s_Instance;

		// Token: 0x04000008 RID: 8
		private Dictionary<int, VertexBuffer> m_Buffers = new Dictionary<int, VertexBuffer>();

		// Token: 0x04000009 RID: 9
		private Queue<VertexBuffer> m_BuffersToDispose = new Queue<VertexBuffer>();
	}
}
