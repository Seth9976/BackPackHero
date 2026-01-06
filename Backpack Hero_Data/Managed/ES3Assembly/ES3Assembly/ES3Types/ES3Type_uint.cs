using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000046 RID: 70
	[Preserve]
	public class ES3Type_uint : ES3Type
	{
		// Token: 0x06000279 RID: 633 RVA: 0x00009479 File Offset: 0x00007679
		public ES3Type_uint()
			: base(typeof(uint))
		{
			this.isPrimitive = true;
			ES3Type_uint.Instance = this;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009498 File Offset: 0x00007698
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((uint)obj);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000094A6 File Offset: 0x000076A6
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_uint());
		}

		// Token: 0x0400007E RID: 126
		public static ES3Type Instance;
	}
}
