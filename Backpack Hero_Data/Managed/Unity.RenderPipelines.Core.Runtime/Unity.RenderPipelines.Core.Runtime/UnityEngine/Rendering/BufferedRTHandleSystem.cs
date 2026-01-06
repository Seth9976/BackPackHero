using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008C RID: 140
	public class BufferedRTHandleSystem : IDisposable
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00014B5A File Offset: 0x00012D5A
		public int maxWidth
		{
			get
			{
				return this.m_RTHandleSystem.GetMaxWidth();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00014B67 File Offset: 0x00012D67
		public int maxHeight
		{
			get
			{
				return this.m_RTHandleSystem.GetMaxHeight();
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00014B74 File Offset: 0x00012D74
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_RTHandleSystem.rtHandleProperties;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00014B81 File Offset: 0x00012D81
		public RTHandle GetFrameRT(int bufferId, int frameIndex)
		{
			if (!this.m_RTHandles.ContainsKey(bufferId))
			{
				return null;
			}
			return this.m_RTHandles[bufferId][frameIndex];
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00014BA4 File Offset: 0x00012DA4
		public void AllocBuffer(int bufferId, Func<RTHandleSystem, int, RTHandle> allocator, int bufferCount)
		{
			RTHandle[] array = new RTHandle[bufferCount];
			this.m_RTHandles.Add(bufferId, array);
			array[0] = allocator(this.m_RTHandleSystem, 0);
			int i = 1;
			int num = array.Length;
			while (i < num)
			{
				array[i] = allocator(this.m_RTHandleSystem, i);
				this.m_RTHandleSystem.SwitchResizeMode(array[i], RTHandleSystem.ResizeMode.OnDemand);
				i++;
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00014C04 File Offset: 0x00012E04
		public void ReleaseBuffer(int bufferId)
		{
			RTHandle[] array;
			if (this.m_RTHandles.TryGetValue(bufferId, out array))
			{
				foreach (RTHandle rthandle in array)
				{
					this.m_RTHandleSystem.Release(rthandle);
				}
			}
			this.m_RTHandles.Remove(bufferId);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00014C4E File Offset: 0x00012E4E
		public void SwapAndSetReferenceSize(int width, int height)
		{
			this.Swap();
			this.m_RTHandleSystem.SetReferenceSize(width, height);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00014C63 File Offset: 0x00012E63
		public void ResetReferenceSize(int width, int height)
		{
			this.m_RTHandleSystem.ResetReferenceSize(width, height);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00014C72 File Offset: 0x00012E72
		public int GetNumFramesAllocated(int bufferId)
		{
			if (!this.m_RTHandles.ContainsKey(bufferId))
			{
				return 0;
			}
			return this.m_RTHandles[bufferId].Length;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00014C94 File Offset: 0x00012E94
		public Vector2 CalculateRatioAgainstMaxSize(int width, int height)
		{
			RTHandleSystem rthandleSystem = this.m_RTHandleSystem;
			Vector2Int vector2Int = new Vector2Int(width, height);
			return rthandleSystem.CalculateRatioAgainstMaxSize(in vector2Int);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00014CB8 File Offset: 0x00012EB8
		private void Swap()
		{
			foreach (KeyValuePair<int, RTHandle[]> keyValuePair in this.m_RTHandles)
			{
				if (keyValuePair.Value.Length > 1)
				{
					RTHandle rthandle = keyValuePair.Value[keyValuePair.Value.Length - 1];
					int i = 0;
					int num = keyValuePair.Value.Length - 1;
					while (i < num)
					{
						keyValuePair.Value[i + 1] = keyValuePair.Value[i];
						i++;
					}
					keyValuePair.Value[0] = rthandle;
					this.m_RTHandleSystem.SwitchResizeMode(keyValuePair.Value[0], RTHandleSystem.ResizeMode.Auto);
					this.m_RTHandleSystem.SwitchResizeMode(keyValuePair.Value[1], RTHandleSystem.ResizeMode.OnDemand);
				}
				else
				{
					this.m_RTHandleSystem.SwitchResizeMode(keyValuePair.Value[0], RTHandleSystem.ResizeMode.Auto);
				}
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00014DA4 File Offset: 0x00012FA4
		private void Dispose(bool disposing)
		{
			if (!this.m_DisposedValue)
			{
				if (disposing)
				{
					this.ReleaseAll();
					this.m_RTHandleSystem.Dispose();
					this.m_RTHandleSystem = null;
				}
				this.m_DisposedValue = true;
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00014DD0 File Offset: 0x00012FD0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00014DDC File Offset: 0x00012FDC
		public void ReleaseAll()
		{
			foreach (KeyValuePair<int, RTHandle[]> keyValuePair in this.m_RTHandles)
			{
				int i = 0;
				int num = keyValuePair.Value.Length;
				while (i < num)
				{
					this.m_RTHandleSystem.Release(keyValuePair.Value[i]);
					i++;
				}
			}
			this.m_RTHandles.Clear();
		}

		// Token: 0x040002DD RID: 733
		private Dictionary<int, RTHandle[]> m_RTHandles = new Dictionary<int, RTHandle[]>();

		// Token: 0x040002DE RID: 734
		private RTHandleSystem m_RTHandleSystem = new RTHandleSystem();

		// Token: 0x040002DF RID: 735
		private bool m_DisposedValue;
	}
}
