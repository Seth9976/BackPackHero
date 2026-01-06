using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000028 RID: 40
	[Preserve]
	public class ES3Type_bool : ES3Type
	{
		// Token: 0x06000238 RID: 568 RVA: 0x000089A9 File Offset: 0x00006BA9
		public ES3Type_bool()
			: base(typeof(bool))
		{
			this.isPrimitive = true;
			ES3Type_bool.Instance = this;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000089C8 File Offset: 0x00006BC8
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((bool)obj);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000089D6 File Offset: 0x00006BD6
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_bool());
		}

		// Token: 0x0400005F RID: 95
		public static ES3Type Instance;
	}
}
