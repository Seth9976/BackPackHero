using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200007B RID: 123
	[Preserve]
	[ES3Properties(new string[] { "hideFlags" })]
	public class ES3Type_Flare : ES3Type
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0000F2D6 File Offset: 0x0000D4D6
		public ES3Type_Flare()
			: base(typeof(Flare))
		{
			ES3Type_Flare.Instance = this;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000F2F0 File Offset: 0x0000D4F0
		public override void Write(object obj, ES3Writer writer)
		{
			Flare flare = (Flare)obj;
			writer.WriteProperty("hideFlags", flare.hideFlags);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000F31C File Offset: 0x0000D51C
		public override object Read<T>(ES3Reader reader)
		{
			Flare flare = new Flare();
			this.ReadInto<T>(reader, flare);
			return flare;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000F338 File Offset: 0x0000D538
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			Flare flare = (Flare)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (text == "hideFlags")
				{
					flare.hideFlags = reader.Read<HideFlags>();
				}
				else
				{
					reader.Skip();
				}
			}
		}

		// Token: 0x040000AE RID: 174
		public static ES3Type Instance;
	}
}
