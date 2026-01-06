using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200003C RID: 60
	[Preserve]
	public class ES3Type_IntPtr : ES3Type
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00009299 File Offset: 0x00007499
		public ES3Type_IntPtr()
			: base(typeof(IntPtr))
		{
			this.isPrimitive = true;
			ES3Type_IntPtr.Instance = this;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000092B8 File Offset: 0x000074B8
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((long)((IntPtr)obj));
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000092CB File Offset: 0x000074CB
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)(IntPtr)reader.Read_long());
		}

		// Token: 0x04000074 RID: 116
		public static ES3Type Instance;
	}
}
