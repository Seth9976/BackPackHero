using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000048 RID: 72
	[Preserve]
	public class ES3Type_UIntPtr : ES3Type
	{
		// Token: 0x0600027D RID: 637 RVA: 0x000094DA File Offset: 0x000076DA
		public ES3Type_UIntPtr()
			: base(typeof(UIntPtr))
		{
			this.isPrimitive = true;
			ES3Type_UIntPtr.Instance = this;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000094F9 File Offset: 0x000076F9
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((ulong)obj);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009507 File Offset: 0x00007707
		public override object Read<T>(ES3Reader reader)
		{
			return reader.Read_ulong();
		}

		// Token: 0x04000080 RID: 128
		public static ES3Type Instance;
	}
}
