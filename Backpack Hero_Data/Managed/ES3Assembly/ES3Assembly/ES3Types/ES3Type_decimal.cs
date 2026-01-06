using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000030 RID: 48
	[Preserve]
	public class ES3Type_decimal : ES3Type
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00008B73 File Offset: 0x00006D73
		public ES3Type_decimal()
			: base(typeof(decimal))
		{
			this.isPrimitive = true;
			ES3Type_decimal.Instance = this;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008B92 File Offset: 0x00006D92
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((decimal)obj);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008BA0 File Offset: 0x00006DA0
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_decimal());
		}

		// Token: 0x04000067 RID: 103
		public static ES3Type Instance;
	}
}
