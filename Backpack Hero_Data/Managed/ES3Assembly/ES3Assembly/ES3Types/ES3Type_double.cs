using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000032 RID: 50
	[Preserve]
	public class ES3Type_double : ES3Type
	{
		// Token: 0x0600024E RID: 590 RVA: 0x00008BD4 File Offset: 0x00006DD4
		public ES3Type_double()
			: base(typeof(double))
		{
			this.isPrimitive = true;
			ES3Type_double.Instance = this;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008BF3 File Offset: 0x00006DF3
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((double)obj);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008C01 File Offset: 0x00006E01
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_double());
		}

		// Token: 0x04000069 RID: 105
		public static ES3Type Instance;
	}
}
