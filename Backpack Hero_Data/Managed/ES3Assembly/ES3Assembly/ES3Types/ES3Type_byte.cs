using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200002A RID: 42
	[Preserve]
	public class ES3Type_byte : ES3Type
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00008A0A File Offset: 0x00006C0A
		public ES3Type_byte()
			: base(typeof(byte))
		{
			this.isPrimitive = true;
			ES3Type_byte.Instance = this;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008A29 File Offset: 0x00006C29
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((byte)obj);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008A37 File Offset: 0x00006C37
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_byte());
		}

		// Token: 0x04000061 RID: 97
		public static ES3Type Instance;
	}
}
