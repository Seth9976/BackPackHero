using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001ED RID: 493
	[Preserve]
	[ES3Properties(new string[] { "itemSprite", "itemSpriteName", "interactionRadius" })]
	public class ES3UserType_Overworld_Podium : ES3ComponentType
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x000A6329 File Offset: 0x000A4529
		public ES3UserType_Overworld_Podium()
			: base(typeof(Overworld_Podium))
		{
			ES3UserType_Overworld_Podium.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000A6348 File Offset: 0x000A4548
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Overworld_Podium overworld_Podium = (Overworld_Podium)obj;
			writer.WritePrivateFieldByRef("itemSprite", overworld_Podium);
			writer.WritePrivateField("itemSpriteName", overworld_Podium);
			writer.WriteProperty("interactionRadius", overworld_Podium.interactionRadius, ES3Type_float.Instance);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000A6390 File Offset: 0x000A4590
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Overworld_Podium overworld_Podium = (Overworld_Podium)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "itemSprite"))
				{
					if (!(text == "itemSpriteName"))
					{
						if (!(text == "interactionRadius"))
						{
							reader.Skip();
						}
						else
						{
							overworld_Podium.interactionRadius = reader.Read<float>(ES3Type_float.Instance);
						}
					}
					else
					{
						reader.SetPrivateField("itemSpriteName", reader.Read<string>(), overworld_Podium);
					}
				}
				else
				{
					reader.SetPrivateField("itemSprite", reader.Read<SpriteRenderer>(), overworld_Podium);
				}
			}
		}

		// Token: 0x04000E19 RID: 3609
		public static ES3Type Instance;
	}
}
