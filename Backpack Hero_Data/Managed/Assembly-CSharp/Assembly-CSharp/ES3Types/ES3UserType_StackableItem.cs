using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200021F RID: 543
	[Preserve]
	[ES3Properties(new string[] { "amountTextParent", "fakePrefab", "amount", "combining", "real" })]
	public class ES3UserType_StackableItem : ES3ComponentType
	{
		// Token: 0x06001205 RID: 4613 RVA: 0x000A9FD5 File Offset: 0x000A81D5
		public ES3UserType_StackableItem()
			: base(typeof(StackableItem))
		{
			ES3UserType_StackableItem.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000A9FF4 File Offset: 0x000A81F4
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			StackableItem stackableItem = (StackableItem)obj;
			writer.WritePrivateFieldByRef("amountTextParent", stackableItem);
			writer.WritePrivateFieldByRef("fakePrefab", stackableItem);
			writer.WriteProperty("amount", stackableItem.amount, ES3Type_int.Instance);
			writer.WriteProperty("combining", stackableItem.combining, ES3Type_bool.Instance);
			writer.WriteProperty("real", stackableItem.real, ES3Type_bool.Instance);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000AA074 File Offset: 0x000A8274
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			StackableItem stackableItem = (StackableItem)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "amountTextParent"))
				{
					if (!(text == "fakePrefab"))
					{
						if (!(text == "amount"))
						{
							if (!(text == "combining"))
							{
								if (!(text == "real"))
								{
									reader.Skip();
								}
								else
								{
									stackableItem.real = reader.Read<bool>(ES3Type_bool.Instance);
								}
							}
							else
							{
								stackableItem.combining = reader.Read<bool>(ES3Type_bool.Instance);
							}
						}
						else
						{
							stackableItem.amount = reader.Read<int>(ES3Type_int.Instance);
						}
					}
					else
					{
						reader.SetPrivateField("fakePrefab", reader.Read<GameObject>(), stackableItem);
					}
				}
				else
				{
					reader.SetPrivateField("amountTextParent", reader.Read<GameObject>(), stackableItem);
				}
			}
		}

		// Token: 0x04000E4B RID: 3659
		public static ES3Type Instance;
	}
}
