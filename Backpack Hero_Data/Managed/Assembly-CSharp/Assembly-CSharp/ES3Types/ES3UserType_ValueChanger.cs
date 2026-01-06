using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000223 RID: 547
	[Preserve]
	[ES3Properties(new string[]
	{
		"triggerOverrideKey", "triggerDisplayValue", "descriptionOverrideKey", "descriptionDisplayValue", "replaceWithValue", "valueToReplace", "multiplier", "itemPrefabs", "areas", "types",
		"areaDistance", "storedEffect"
	})]
	public class ES3UserType_ValueChanger : ES3ComponentType
	{
		// Token: 0x0600120D RID: 4621 RVA: 0x000AB60D File Offset: 0x000A980D
		public ES3UserType_ValueChanger()
			: base(typeof(ValueChanger))
		{
			ES3UserType_ValueChanger.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000AB62C File Offset: 0x000A982C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ValueChanger valueChanger = (ValueChanger)obj;
			writer.WriteProperty("triggerOverrideKey", valueChanger.triggerOverrideKey, ES3Type_string.Instance);
			writer.WriteProperty("triggerDisplayValue", valueChanger.triggerDisplayValue, ES3Type_float.Instance);
			writer.WriteProperty("descriptionOverrideKey", valueChanger.descriptionOverrideKey, ES3Type_string.Instance);
			writer.WriteProperty("descriptionDisplayValue", valueChanger.descriptionDisplayValue, ES3Type_float.Instance);
			writer.WriteProperty("replaceWithValue", valueChanger.replaceWithValue, ES3TypeMgr.GetOrCreateES3Type(typeof(ValueChanger.ReplaceWithValue), true));
			writer.WritePrivateField("valueToReplace", valueChanger);
			writer.WriteProperty("multiplier", valueChanger.multiplier, ES3Type_float.Instance);
			writer.WritePrivateField("itemPrefabs", valueChanger);
			writer.WritePrivateField("areas", valueChanger);
			writer.WritePrivateField("types", valueChanger);
			writer.WritePrivateField("areaDistance", valueChanger);
			writer.WriteProperty("storedEffect", valueChanger.storedEffect, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Effect), true));
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000AB740 File Offset: 0x000A9940
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ValueChanger valueChanger = (ValueChanger)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2153350995U)
				{
					if (num <= 1339115925U)
					{
						if (num != 691548271U)
						{
							if (num != 972631634U)
							{
								if (num == 1339115925U)
								{
									if (text == "areas")
									{
										reader.SetPrivateField("areas", reader.Read<List<Item2.Area>>(), valueChanger);
										continue;
									}
								}
							}
							else if (text == "triggerOverrideKey")
							{
								valueChanger.triggerOverrideKey = reader.Read<string>(ES3Type_string.Instance);
								continue;
							}
						}
						else if (text == "areaDistance")
						{
							reader.SetPrivateField("areaDistance", reader.Read<Item2.AreaDistance>(), valueChanger);
							continue;
						}
					}
					else if (num != 1549735536U)
					{
						if (num != 2055847824U)
						{
							if (num == 2153350995U)
							{
								if (text == "storedEffect")
								{
									valueChanger.storedEffect = reader.Read<Item2.Effect>();
									continue;
								}
							}
						}
						else if (text == "descriptionOverrideKey")
						{
							valueChanger.descriptionOverrideKey = reader.Read<string>(ES3Type_string.Instance);
							continue;
						}
					}
					else if (text == "triggerDisplayValue")
					{
						valueChanger.triggerDisplayValue = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 2542433161U)
				{
					if (num != 2302575176U)
					{
						if (num != 2476303950U)
						{
							if (num == 2542433161U)
							{
								if (text == "valueToReplace")
								{
									reader.SetPrivateField("valueToReplace", reader.Read<float>(), valueChanger);
									continue;
								}
							}
						}
						else if (text == "descriptionDisplayValue")
						{
							valueChanger.descriptionDisplayValue = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "replaceWithValue")
					{
						valueChanger.replaceWithValue = reader.Read<ValueChanger.ReplaceWithValue>();
						continue;
					}
				}
				else if (num != 3720762021U)
				{
					if (num != 4110862310U)
					{
						if (num == 4292920474U)
						{
							if (text == "types")
							{
								reader.SetPrivateField("types", reader.Read<List<Item2.ItemType>>(), valueChanger);
								continue;
							}
						}
					}
					else if (text == "multiplier")
					{
						valueChanger.multiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (text == "itemPrefabs")
				{
					reader.SetPrivateField("itemPrefabs", reader.Read<List<GameObject>>(), valueChanger);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E4F RID: 3663
		public static ES3Type Instance;
	}
}
