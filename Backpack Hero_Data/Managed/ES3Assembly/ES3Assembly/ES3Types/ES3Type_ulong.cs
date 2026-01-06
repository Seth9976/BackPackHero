using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200004A RID: 74
	[Preserve]
	public class ES3Type_ulong : ES3Type
	{
		// Token: 0x06000281 RID: 641 RVA: 0x00009531 File Offset: 0x00007731
		public ES3Type_ulong()
			: base(typeof(ulong))
		{
			this.isPrimitive = true;
			ES3Type_ulong.Instance = this;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009550 File Offset: 0x00007750
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((ulong)obj);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000955E File Offset: 0x0000775E
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_ulong());
		}

		// Token: 0x04000082 RID: 130
		public static ES3Type Instance;
	}
}
