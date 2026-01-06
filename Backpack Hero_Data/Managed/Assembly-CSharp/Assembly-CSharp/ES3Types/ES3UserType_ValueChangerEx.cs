using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000225 RID: 549
	[Preserve]
	[ES3Properties(new string[] { "triggerOverrideKey", "descriptionOverrideKey", "replaceWithValue", "valueToReplace", "multiplier", "itemPrefab", "areas", "types", "areaDistance" })]
	public class ES3UserType_ValueChangerEx : ES3ComponentType
	{
		// Token: 0x06001211 RID: 4625 RVA: 0x000ABA79 File Offset: 0x000A9C79
		public ES3UserType_ValueChangerEx()
			: base(typeof(ValueChangerEx))
		{
			ES3UserType_ValueChangerEx.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000ABA98 File Offset: 0x000A9C98
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ValueChangerEx valueChangerEx = (ValueChangerEx)obj;
			writer.WriteProperty("triggerOverrideKey", valueChangerEx.triggerOverrideKey, ES3Type_string.Instance);
			writer.WriteProperty("descriptionOverrideKey", valueChangerEx.descriptionOverrideKey, ES3Type_string.Instance);
			writer.WriteProperty("replaceWithValue", valueChangerEx.replaceWithValue, ES3Type_enum.Instance);
			writer.WritePrivateField("valueToReplace", valueChangerEx);
			writer.WriteProperty("multiplier", valueChangerEx.multiplier, ES3Type_float.Instance);
			writer.WriteProperty("baseValue", valueChangerEx.baseValue, ES3Type_float.Instance);
			writer.WritePrivateField("itemPrefabs", valueChangerEx);
			writer.WritePrivateField("areas", valueChangerEx);
			writer.WritePrivateField("types", valueChangerEx);
			writer.WritePrivateField("areaDistance", valueChangerEx);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000ABB68 File Offset: 0x000A9D68
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ValueChangerEx valueChangerEx = (ValueChangerEx)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2237054217U)
				{
					if (num <= 972631634U)
					{
						if (num != 691548271U)
						{
							if (num == 972631634U)
							{
								if (text == "triggerOverrideKey")
								{
									valueChangerEx.triggerOverrideKey = reader.Read<string>(ES3Type_string.Instance);
									continue;
								}
							}
						}
						else if (text == "areaDistance")
						{
							reader.SetPrivateField("areaDistance", reader.Read<Item2.AreaDistance>(), valueChangerEx);
							continue;
						}
					}
					else if (num != 1339115925U)
					{
						if (num != 2055847824U)
						{
							if (num == 2237054217U)
							{
								if (text == "baseValue")
								{
									valueChangerEx.baseValue = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "descriptionOverrideKey")
						{
							valueChangerEx.descriptionOverrideKey = reader.Read<string>(ES3Type_string.Instance);
							continue;
						}
					}
					else if (text == "areas")
					{
						reader.SetPrivateField("areas", reader.Read<List<Item2.Area>>(), valueChangerEx);
						continue;
					}
				}
				else if (num <= 2542433161U)
				{
					if (num != 2302575176U)
					{
						if (num == 2542433161U)
						{
							if (text == "valueToReplace")
							{
								reader.SetPrivateField("valueToReplace", reader.Read<float>(), valueChangerEx);
								continue;
							}
						}
					}
					else if (text == "replaceWithValue")
					{
						valueChangerEx.replaceWithValue = reader.Read<ValueChangerEx.ReplaceWithValue>(ES3Type_enum.Instance);
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
								reader.SetPrivateField("types", reader.Read<List<Item2.ItemType>>(), valueChangerEx);
								continue;
							}
						}
					}
					else if (text == "multiplier")
					{
						valueChangerEx.multiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (text == "itemPrefabs")
				{
					reader.SetPrivateField("itemPrefabs", reader.Read<List<GameObject>>(), valueChangerEx);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E51 RID: 3665
		public static ES3Type Instance;
	}
}
