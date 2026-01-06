using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200002C RID: 44
	[Preserve]
	public class ES3Type_char : ES3Type
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00008A8D File Offset: 0x00006C8D
		public ES3Type_char()
			: base(typeof(char))
		{
			this.isPrimitive = true;
			ES3Type_char.Instance = this;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008AAC File Offset: 0x00006CAC
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((char)obj);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008ABA File Offset: 0x00006CBA
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_char());
		}

		// Token: 0x04000063 RID: 99
		public static ES3Type Instance;
	}
}
