using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000209 RID: 521
	[Preserve]
	[ES3Properties(new string[] { "tilesUsed", "trigger", "target", "description", "value", "item", "itemMovement" })]
	public class ES3UserType_SpecialItemCleansingFlame : ES3ComponentType
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x000A8269 File Offset: 0x000A6469
		public ES3UserType_SpecialItemCleansingFlame()
			: base(typeof(SpecialItemCleansingFlame))
		{
			ES3UserType_SpecialItemCleansingFlame.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000A8288 File Offset: 0x000A6488
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpecialItemCleansingFlame specialItemCleansingFlame = (SpecialItemCleansingFlame)obj;
			writer.WriteProperty("tilesUsed", specialItemCleansingFlame.tilesUsed, ES3TypeMgr.GetOrCreateES3Type(typeof(List<GameObject>), true));
			writer.WriteProperty("trigger", specialItemCleansingFlame.trigger, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Trigger), true));
			writer.WriteProperty("target", specialItemCleansingFlame.target, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Effect.Target), true));
			writer.WriteProperty("description", specialItemCleansingFlame.description, ES3Type_string.Instance);
			writer.WriteProperty("value", specialItemCleansingFlame.value, ES3Type_float.Instance);
			writer.WritePropertyByRef("item", specialItemCleansingFlame.item);
			writer.WritePropertyByRef("itemMovement", specialItemCleansingFlame.itemMovement);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000A8358 File Offset: 0x000A6558
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpecialItemCleansingFlame specialItemCleansingFlame = (SpecialItemCleansingFlame)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1073051383U)
				{
					if (num != 845187144U)
					{
						if (num != 879704937U)
						{
							if (num == 1073051383U)
							{
								if (text == "itemMovement")
								{
									specialItemCleansingFlame.itemMovement = reader.Read<ItemMovement>(ES3UserType_ItemMovement.Instance);
									continue;
								}
							}
						}
						else if (text == "description")
						{
							specialItemCleansingFlame.description = reader.Read<string>(ES3Type_string.Instance);
							continue;
						}
					}
					else if (text == "target")
					{
						specialItemCleansingFlame.target = reader.Read<Item2.Effect.Target>();
						continue;
					}
				}
				else if (num <= 1837712375U)
				{
					if (num != 1113510858U)
					{
						if (num == 1837712375U)
						{
							if (text == "tilesUsed")
							{
								specialItemCleansingFlame.tilesUsed = reader.Read<List<GameObject>>();
								continue;
							}
						}
					}
					else if (text == "value")
					{
						specialItemCleansingFlame.value = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 1967206915U)
				{
					if (num == 2671260646U)
					{
						if (text == "item")
						{
							specialItemCleansingFlame.item = reader.Read<Item2>(ES3UserType_Item2.Instance);
							continue;
						}
					}
				}
				else if (text == "trigger")
				{
					specialItemCleansingFlame.trigger = reader.Read<Item2.Trigger>();
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E35 RID: 3637
		public static ES3Type Instance;
	}
}
