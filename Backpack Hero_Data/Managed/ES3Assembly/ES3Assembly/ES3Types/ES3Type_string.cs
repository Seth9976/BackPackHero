using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000044 RID: 68
	[Preserve]
	public class ES3Type_string : ES3Type
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00009427 File Offset: 0x00007627
		public ES3Type_string()
			: base(typeof(string))
		{
			this.isPrimitive = true;
			ES3Type_string.Instance = this;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009446 File Offset: 0x00007646
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((string)obj);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009454 File Offset: 0x00007654
		public override object Read<T>(ES3Reader reader)
		{
			return reader.Read_string();
		}

		// Token: 0x0400007C RID: 124
		public static ES3Type Instance;
	}
}
