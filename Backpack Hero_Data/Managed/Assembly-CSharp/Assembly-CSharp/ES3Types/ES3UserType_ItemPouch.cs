using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001D7 RID: 471
	[Preserve]
	[ES3Properties(new string[] { "backgroundSprite", "pouchPrefabs", "pouchContents", "lastOpeningPosition", "itemsInside", "openSprite", "closedSprite", "allowedTypes" })]
	public class ES3UserType_ItemPouch : ES3ComponentType
	{
		// Token: 0x06001175 RID: 4469 RVA: 0x000A4FA1 File Offset: 0x000A31A1
		public ES3UserType_ItemPouch()
			: base(typeof(ItemPouch))
		{
			ES3UserType_ItemPouch.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000A4FC0 File Offset: 0x000A31C0
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ItemPouch itemPouch = (ItemPouch)obj;
			writer.WritePropertyByRef("backgroundSprite", itemPouch.backgroundSprite);
			writer.WritePrivateField("pouchPrefabs", itemPouch);
			writer.WritePropertyByRef("pouchContents", itemPouch.pouchContents);
			writer.WritePrivateField("lastOpeningPosition", itemPouch);
			writer.WriteProperty("itemsInside", itemPouch.itemsInside, ES3TypeMgr.GetOrCreateES3Type(typeof(List<GameObject>), true));
			writer.WritePrivateFieldByRef("openSprite", itemPouch);
			writer.WritePrivateFieldByRef("closedSprite", itemPouch);
			writer.WriteProperty("allowedTypes", itemPouch.allowedTypes, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.ItemType>), true));
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000A5068 File Offset: 0x000A3268
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ItemPouch itemPouch = (ItemPouch)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2512436792U)
				{
					if (num <= 1415841478U)
					{
						if (num != 387223167U)
						{
							if (num == 1415841478U)
							{
								if (text == "pouchContents")
								{
									itemPouch.pouchContents = reader.Read<GameObject>(ES3Type_GameObject.Instance);
									continue;
								}
							}
						}
						else if (text == "itemsInside")
						{
							itemPouch.itemsInside = reader.Read<List<GameObject>>();
							continue;
						}
					}
					else if (num != 1781533092U)
					{
						if (num == 2512436792U)
						{
							if (text == "allowedTypes")
							{
								itemPouch.allowedTypes = reader.Read<List<Item2.ItemType>>();
								continue;
							}
						}
					}
					else if (text == "backgroundSprite")
					{
						itemPouch.backgroundSprite = reader.Read<Sprite>(ES3Type_Sprite.Instance);
						continue;
					}
				}
				else if (num <= 3662131439U)
				{
					if (num != 2631990844U)
					{
						if (num == 3662131439U)
						{
							if (text == "pouchPrefabs")
							{
								reader.SetPrivateField("pouchPrefabs", reader.Read<List<GameObject>>(), itemPouch);
								continue;
							}
						}
					}
					else if (text == "closedSprite")
					{
						reader.SetPrivateField("closedSprite", reader.Read<Sprite>(), itemPouch);
						continue;
					}
				}
				else if (num != 3944883320U)
				{
					if (num == 4068459440U)
					{
						if (text == "openSprite")
						{
							reader.SetPrivateField("openSprite", reader.Read<Sprite>(), itemPouch);
							continue;
						}
					}
				}
				else if (text == "lastOpeningPosition")
				{
					reader.SetPrivateField("lastOpeningPosition", reader.Read<Vector3>(), itemPouch);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E03 RID: 3587
		public static ES3Type Instance;
	}
}
