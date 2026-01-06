using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200002E RID: 46
	[Preserve]
	public class ES3Type_DateTime : ES3Type
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00008AEE File Offset: 0x00006CEE
		public ES3Type_DateTime()
			: base(typeof(DateTime))
		{
			ES3Type_DateTime.Instance = this;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008B08 File Offset: 0x00006D08
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WriteProperty("ticks", ((DateTime)obj).Ticks, ES3Type_long.Instance);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008B38 File Offset: 0x00006D38
		public override object Read<T>(ES3Reader reader)
		{
			reader.ReadPropertyName();
			return new DateTime(reader.Read<long>(ES3Type_long.Instance));
		}

		// Token: 0x04000065 RID: 101
		public static ES3Type Instance;
	}
}
