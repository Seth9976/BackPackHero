using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200004C RID: 76
	[Preserve]
	public class ES3Type_ushort : ES3Type
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00009592 File Offset: 0x00007792
		public ES3Type_ushort()
			: base(typeof(ushort))
		{
			this.isPrimitive = true;
			ES3Type_ushort.Instance = this;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000095B1 File Offset: 0x000077B1
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((ushort)obj);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000095BF File Offset: 0x000077BF
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_ushort());
		}

		// Token: 0x04000084 RID: 132
		public static ES3Type Instance;
	}
}
