using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001E5 RID: 485
	[Preserve]
	[ES3Properties(new string[] { "openSprite", "closedSprite", "storedItems" })]
	public class ES3UserType_Overworld_Chest : ES3ComponentType
	{
		// Token: 0x06001191 RID: 4497 RVA: 0x000A5E45 File Offset: 0x000A4045
		public ES3UserType_Overworld_Chest()
			: base(typeof(Overworld_Chest))
		{
			ES3UserType_Overworld_Chest.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x000A5E64 File Offset: 0x000A4064
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Overworld_Chest overworld_Chest = (Overworld_Chest)obj;
			writer.WritePrivateFieldByRef("openSprite", overworld_Chest);
			writer.WritePrivateFieldByRef("closedSprite", overworld_Chest);
			writer.WritePrivateField("storedItems", overworld_Chest);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x000A5E9C File Offset: 0x000A409C
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Overworld_Chest overworld_Chest = (Overworld_Chest)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "openSprite"))
				{
					if (!(text == "closedSprite"))
					{
						if (!(text == "storedItems"))
						{
							reader.Skip();
						}
						else
						{
							reader.SetPrivateField("storedItems", reader.Read<List<string>>(), overworld_Chest);
						}
					}
					else
					{
						reader.SetPrivateField("closedSprite", reader.Read<Sprite>(), overworld_Chest);
					}
				}
				else
				{
					reader.SetPrivateField("openSprite", reader.Read<Sprite>(), overworld_Chest);
				}
			}
		}

		// Token: 0x04000E11 RID: 3601
		public static ES3Type Instance;
	}
}
