using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000035 RID: 53
	[Preserve]
	public class ES3Type_ES3Ref : ES3Type
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000911C File Offset: 0x0000731C
		public ES3Type_ES3Ref()
			: base(typeof(long))
		{
			this.isPrimitive = true;
			ES3Type_ES3Ref.Instance = this;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000913C File Offset: 0x0000733C
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive(((long)obj).ToString());
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000915D File Offset: 0x0000735D
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)new ES3Ref(reader.Read_ref()));
		}

		// Token: 0x0400006D RID: 109
		public static ES3Type Instance = new ES3Type_ES3Ref();
	}
}
