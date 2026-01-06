using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001EF RID: 495
	[Preserve]
	[ES3Properties(new string[] { "shopAtlasPrefab", "shopItems" })]
	public class ES3UserType_Overworld_ShopKeeper : ES3ComponentType
	{
		// Token: 0x060011A5 RID: 4517 RVA: 0x000A6471 File Offset: 0x000A4671
		public ES3UserType_Overworld_ShopKeeper()
			: base(typeof(Overworld_ShopKeeper))
		{
			ES3UserType_Overworld_ShopKeeper.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000A6490 File Offset: 0x000A4690
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Overworld_ShopKeeper overworld_ShopKeeper = (Overworld_ShopKeeper)obj;
			writer.WritePrivateFieldByRef("shopAtlasPrefab", overworld_ShopKeeper);
			writer.WriteProperty("shopItems", overworld_ShopKeeper.shopItems, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Overworld_ShopKeeper.ShopItem>), true));
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000A64D4 File Offset: 0x000A46D4
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Overworld_ShopKeeper overworld_ShopKeeper = (Overworld_ShopKeeper)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "shopAtlasPrefab"))
				{
					if (!(text == "shopItems"))
					{
						reader.Skip();
					}
					else
					{
						overworld_ShopKeeper.shopItems = reader.Read<List<Overworld_ShopKeeper.ShopItem>>();
					}
				}
				else
				{
					reader.SetPrivateField("shopAtlasPrefab", reader.Read<GameObject>(), overworld_ShopKeeper);
				}
			}
		}

		// Token: 0x04000E1B RID: 3611
		public static ES3Type Instance;
	}
}
