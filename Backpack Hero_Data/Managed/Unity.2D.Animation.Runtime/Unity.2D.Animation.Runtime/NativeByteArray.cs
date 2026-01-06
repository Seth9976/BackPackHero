using System;
using Unity.Collections;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200002A RID: 42
	internal class NativeByteArray
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000045DC File Offset: 0x000027DC
		public int Length
		{
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000045F8 File Offset: 0x000027F8
		public bool IsCreated
		{
			get
			{
				return this.array.IsCreated;
			}
		}

		// Token: 0x1700002C RID: 44
		public byte this[int index]
		{
			get
			{
				return this.array[index];
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004630 File Offset: 0x00002830
		public NativeArray<byte> array { get; }

		// Token: 0x060000F6 RID: 246 RVA: 0x00004638 File Offset: 0x00002838
		public NativeByteArray(NativeArray<byte> array)
		{
			this.array = array;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004648 File Offset: 0x00002848
		public void Dispose()
		{
			this.array.Dispose();
		}
	}
}
