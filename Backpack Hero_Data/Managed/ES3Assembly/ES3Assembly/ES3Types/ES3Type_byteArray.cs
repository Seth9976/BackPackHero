using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200002B RID: 43
	[Preserve]
	public class ES3Type_byteArray : ES3Type
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00008A4E File Offset: 0x00006C4E
		public ES3Type_byteArray()
			: base(typeof(byte[]))
		{
			this.isPrimitive = true;
			ES3Type_byteArray.Instance = this;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008A6D File Offset: 0x00006C6D
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((byte[])obj);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008A7B File Offset: 0x00006C7B
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_byteArray());
		}

		// Token: 0x04000062 RID: 98
		public static ES3Type Instance;
	}
}
