using System;
using System.Collections;
using UnityEngine.Scripting;

namespace Unity.VisualScripting
{
	// Token: 0x02000010 RID: 16
	public sealed class AotList : ArrayList
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002B3A File Offset: 0x00000D3A
		public AotList()
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B42 File Offset: 0x00000D42
		public AotList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B4B File Offset: 0x00000D4B
		public AotList(ICollection c)
			: base(c)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B54 File Offset: 0x00000D54
		[Preserve]
		public static void AotStubs()
		{
			AotList aotList = new AotList();
			aotList.Add(null);
			aotList.Remove(null);
			object obj = aotList[0];
			aotList[0] = null;
			aotList.Contains(null);
			aotList.Clear();
			int count = aotList.Count;
		}
	}
}
