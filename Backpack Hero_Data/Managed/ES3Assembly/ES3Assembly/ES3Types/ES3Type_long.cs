using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200003E RID: 62
	[Preserve]
	public class ES3Type_long : ES3Type
	{
		// Token: 0x06000269 RID: 617 RVA: 0x00009304 File Offset: 0x00007504
		public ES3Type_long()
			: base(typeof(long))
		{
			this.isPrimitive = true;
			ES3Type_long.Instance = this;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009323 File Offset: 0x00007523
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((long)obj);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009331 File Offset: 0x00007531
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_long());
		}

		// Token: 0x04000076 RID: 118
		public static ES3Type Instance;
	}
}
