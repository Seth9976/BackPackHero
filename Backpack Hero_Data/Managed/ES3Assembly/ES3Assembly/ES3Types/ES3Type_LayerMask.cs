using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200008C RID: 140
	[Preserve]
	[ES3Properties(new string[] { "colorKeys", "alphaKeys", "mode" })]
	public class ES3Type_LayerMask : ES3Type
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0001048D File Offset: 0x0000E68D
		public ES3Type_LayerMask()
			: base(typeof(LayerMask))
		{
			ES3Type_LayerMask.Instance = this;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000104A8 File Offset: 0x0000E6A8
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WriteProperty("value", ((LayerMask)obj).value, ES3Type_int.Instance);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000104D8 File Offset: 0x0000E6D8
		public override object Read<T>(ES3Reader reader)
		{
			LayerMask layerMask = default(LayerMask);
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (text == "value")
				{
					layerMask = reader.Read<int>(ES3Type_int.Instance);
				}
				else
				{
					reader.Skip();
				}
			}
			return layerMask;
		}

		// Token: 0x040000C2 RID: 194
		public static ES3Type Instance;
	}
}
