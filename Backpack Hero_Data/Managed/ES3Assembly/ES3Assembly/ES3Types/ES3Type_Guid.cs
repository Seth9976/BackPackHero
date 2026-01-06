using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000087 RID: 135
	[Preserve]
	[ES3Properties(new string[] { "value" })]
	public class ES3Type_Guid : ES3Type
	{
		// Token: 0x0600032A RID: 810 RVA: 0x000101A9 File Offset: 0x0000E3A9
		public ES3Type_Guid()
			: base(typeof(Guid))
		{
			ES3Type_Guid.Instance = this;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000101C4 File Offset: 0x0000E3C4
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WriteProperty("value", ((Guid)obj).ToString(), ES3Type_string.Instance);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000101F5 File Offset: 0x0000E3F5
		public override object Read<T>(ES3Reader reader)
		{
			return Guid.Parse(reader.ReadProperty<string>(ES3Type_string.Instance));
		}

		// Token: 0x040000BD RID: 189
		public static ES3Type Instance;
	}
}
