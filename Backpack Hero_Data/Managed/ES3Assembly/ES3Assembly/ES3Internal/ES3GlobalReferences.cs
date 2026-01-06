using System;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000D0 RID: 208
	public class ES3GlobalReferences : ScriptableObject
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00021986 File Offset: 0x0001FB86
		public static ES3GlobalReferences Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00021989 File Offset: 0x0001FB89
		public Object Get(long id)
		{
			return null;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0002198C File Offset: 0x0001FB8C
		public long GetOrAdd(Object obj)
		{
			return -1L;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00021990 File Offset: 0x0001FB90
		public void RemoveInvalidKeys()
		{
		}
	}
}
