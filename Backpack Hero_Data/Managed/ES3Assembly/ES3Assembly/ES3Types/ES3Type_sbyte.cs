using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000040 RID: 64
	[Preserve]
	public class ES3Type_sbyte : ES3Type
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00009365 File Offset: 0x00007565
		public ES3Type_sbyte()
			: base(typeof(sbyte))
		{
			this.isPrimitive = true;
			ES3Type_sbyte.Instance = this;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009384 File Offset: 0x00007584
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((sbyte)obj);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009392 File Offset: 0x00007592
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_sbyte());
		}

		// Token: 0x04000078 RID: 120
		public static ES3Type Instance;
	}
}
