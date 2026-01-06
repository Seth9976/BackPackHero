using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000015 RID: 21
	[Preserve]
	public class ES3Type_ES3Prefab : ES3Type
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00006697 File Offset: 0x00004897
		public ES3Type_ES3Prefab()
			: base(typeof(ES3Prefab))
		{
			ES3Type_ES3Prefab.Instance = this;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000066AF File Offset: 0x000048AF
		public override void Write(object obj, ES3Writer writer)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000066B1 File Offset: 0x000048B1
		public override object Read<T>(ES3Reader reader)
		{
			return null;
		}

		// Token: 0x04000048 RID: 72
		public static ES3Type Instance;
	}
}
