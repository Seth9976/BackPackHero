using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001DB RID: 475
	[Preserve]
	[ES3Properties(new string[] { "changeCondition", "item2Changes", "currentReference" })]
	public class ES3UserType_ItemSwitcher : ES3ComponentType
	{
		// Token: 0x0600117D RID: 4477 RVA: 0x000A5409 File Offset: 0x000A3609
		public ES3UserType_ItemSwitcher()
			: base(typeof(ItemSwitcher))
		{
			ES3UserType_ItemSwitcher.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000A5428 File Offset: 0x000A3628
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ItemSwitcher itemSwitcher = (ItemSwitcher)obj;
			writer.WriteProperty("changeCondition", itemSwitcher.changeCondition, ES3Type_enum.Instance);
			writer.WritePrivateField("item2Changes", itemSwitcher);
			writer.WritePrivateFieldByRef("currentReference", itemSwitcher);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000A5470 File Offset: 0x000A3670
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ItemSwitcher itemSwitcher = (ItemSwitcher)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "changeCondition"))
				{
					if (!(text == "item2Changes"))
					{
						if (!(text == "currentReference"))
						{
							reader.Skip();
						}
						else
						{
							reader.SetPrivateField("currentReference", reader.Read<Item2>(), itemSwitcher);
						}
					}
					else
					{
						reader.SetPrivateField("item2Changes", reader.Read<List<ItemSwitcher.Item2Change>>(), itemSwitcher);
					}
				}
				else
				{
					itemSwitcher.changeCondition = reader.Read<ItemSwitcher.ChangeCondition>(ES3Type_enum.Instance);
				}
			}
		}

		// Token: 0x04000E07 RID: 3591
		public static ES3Type Instance;
	}
}
