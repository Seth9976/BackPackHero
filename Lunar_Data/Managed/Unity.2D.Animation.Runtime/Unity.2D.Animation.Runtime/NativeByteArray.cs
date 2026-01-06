using System;
using Unity.Collections;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000034 RID: 52
	internal class NativeByteArray
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006184 File Offset: 0x00004384
		public int Length
		{
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000061A0 File Offset: 0x000043A0
		public bool IsCreated
		{
			get
			{
				return this.array.IsCreated;
			}
		}

		// Token: 0x1700002E RID: 46
		public byte this[int index]
		{
			get
			{
				return this.array[index];
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000061D8 File Offset: 0x000043D8
		public NativeArray<byte> array { get; }

		// Token: 0x0600011C RID: 284 RVA: 0x000061E0 File Offset: 0x000043E0
		public NativeByteArray(NativeArray<byte> array)
		{
			this.array = array;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000061F0 File Offset: 0x000043F0
		public void Dispose()
		{
			this.array.Dispose();
		}
	}
}
