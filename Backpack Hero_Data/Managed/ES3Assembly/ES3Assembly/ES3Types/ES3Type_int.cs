using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200003A RID: 58
	[Preserve]
	public class ES3Type_int : ES3Type
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00009238 File Offset: 0x00007438
		public ES3Type_int()
			: base(typeof(int))
		{
			this.isPrimitive = true;
			ES3Type_int.Instance = this;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009257 File Offset: 0x00007457
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((int)obj);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009265 File Offset: 0x00007465
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_int());
		}

		// Token: 0x04000072 RID: 114
		public static ES3Type Instance;
	}
}
