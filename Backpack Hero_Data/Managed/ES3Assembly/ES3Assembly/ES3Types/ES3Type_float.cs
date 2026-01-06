using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000038 RID: 56
	[Preserve]
	public class ES3Type_float : ES3Type
	{
		// Token: 0x0600025D RID: 605 RVA: 0x000091D7 File Offset: 0x000073D7
		public ES3Type_float()
			: base(typeof(float))
		{
			this.isPrimitive = true;
			ES3Type_float.Instance = this;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000091F6 File Offset: 0x000073F6
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((float)obj);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009204 File Offset: 0x00007404
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_float());
		}

		// Token: 0x04000070 RID: 112
		public static ES3Type Instance;
	}
}
