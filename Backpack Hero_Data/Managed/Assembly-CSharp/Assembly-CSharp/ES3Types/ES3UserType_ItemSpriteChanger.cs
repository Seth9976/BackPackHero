using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001D9 RID: 473
	[Preserve]
	[ES3Properties(new string[] { "sprites", "plusOne", "mode" })]
	public class ES3UserType_ItemSpriteChanger : ES3ComponentType
	{
		// Token: 0x06001179 RID: 4473 RVA: 0x000A52B9 File Offset: 0x000A34B9
		public ES3UserType_ItemSpriteChanger()
			: base(typeof(ItemSpriteChanger))
		{
			ES3UserType_ItemSpriteChanger.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000A52D8 File Offset: 0x000A34D8
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ItemSpriteChanger itemSpriteChanger = (ItemSpriteChanger)obj;
			writer.WritePrivateField("sprites", itemSpriteChanger);
			writer.WritePrivateField("plusOne", itemSpriteChanger);
			writer.WriteProperty("mode", itemSpriteChanger.mode, ES3Type_enum.Instance);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000A5320 File Offset: 0x000A3520
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ItemSpriteChanger itemSpriteChanger = (ItemSpriteChanger)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "sprites"))
				{
					if (!(text == "plusOne"))
					{
						if (!(text == "mode"))
						{
							reader.Skip();
						}
						else
						{
							itemSpriteChanger.mode = reader.Read<ItemSpriteChanger.SpriteChangerMode>(ES3Type_enum.Instance);
						}
					}
					else
					{
						reader.SetPrivateField("plusOne", reader.Read<bool>(), itemSpriteChanger);
					}
				}
				else
				{
					reader.SetPrivateField("sprites", reader.Read<List<Sprite>>(), itemSpriteChanger);
				}
			}
		}

		// Token: 0x04000E05 RID: 3589
		public static ES3Type Instance;
	}
}
