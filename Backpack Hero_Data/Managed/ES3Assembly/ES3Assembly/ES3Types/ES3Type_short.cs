using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000042 RID: 66
	[Preserve]
	public class ES3Type_short : ES3Type
	{
		// Token: 0x06000271 RID: 625 RVA: 0x000093C6 File Offset: 0x000075C6
		public ES3Type_short()
			: base(typeof(short))
		{
			this.isPrimitive = true;
			ES3Type_short.Instance = this;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000093E5 File Offset: 0x000075E5
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((short)obj);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000093F3 File Offset: 0x000075F3
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_short());
		}

		// Token: 0x0400007A RID: 122
		public static ES3Type Instance;
	}
}
