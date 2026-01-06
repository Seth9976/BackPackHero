using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001BB RID: 443
	[Preserve]
	[ES3Properties(new string[] { "flavorTextKey", "descriptions" })]
	public class ES3UserType_CustomDescriptions : ES3ComponentType
	{
		// Token: 0x0600113D RID: 4413 RVA: 0x000A2505 File Offset: 0x000A0705
		public ES3UserType_CustomDescriptions()
			: base(typeof(CustomDescriptions))
		{
			ES3UserType_CustomDescriptions.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000A2524 File Offset: 0x000A0724
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			CustomDescriptions customDescriptions = (CustomDescriptions)obj;
			writer.WriteProperty("flavorTextKey", customDescriptions.flavorTextKey, ES3Type_string.Instance);
			writer.WriteProperty("descriptions", customDescriptions.descriptions, ES3TypeMgr.GetOrCreateES3Type(typeof(List<CustomDescriptions.Description>), true));
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000A2570 File Offset: 0x000A0770
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			CustomDescriptions customDescriptions = (CustomDescriptions)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "flavorTextKey"))
				{
					if (!(text == "descriptions"))
					{
						reader.Skip();
					}
					else
					{
						customDescriptions.descriptions = reader.Read<List<CustomDescriptions.Description>>();
					}
				}
				else
				{
					customDescriptions.flavorTextKey = reader.Read<string>(ES3Type_string.Instance);
				}
			}
		}

		// Token: 0x04000DE7 RID: 3559
		public static ES3Type Instance;
	}
}
