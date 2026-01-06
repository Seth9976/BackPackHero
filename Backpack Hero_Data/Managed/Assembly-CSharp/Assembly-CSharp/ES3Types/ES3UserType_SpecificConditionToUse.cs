using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000219 RID: 537
	[Preserve]
	[ES3Properties(new string[] { "canAlsoBeOnEmptySpace", "conditionType", "conditionTime", "areaDistance", "itemAreas", "itemTypes", "value", "explanationKey", "cardKey" })]
	public class ES3UserType_SpecificConditionToUse : ES3ComponentType
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x000A92DD File Offset: 0x000A74DD
		public ES3UserType_SpecificConditionToUse()
			: base(typeof(SpecificConditionToUse))
		{
			ES3UserType_SpecificConditionToUse.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000A92FC File Offset: 0x000A74FC
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpecificConditionToUse specificConditionToUse = (SpecificConditionToUse)obj;
			writer.WriteProperty("canAlsoBeOnEmptySpace", specificConditionToUse.canAlsoBeOnEmptySpace, ES3Type_bool.Instance);
			writer.WriteProperty("conditionType", specificConditionToUse.conditionType, ES3Type_enum.Instance);
			writer.WritePrivateField("conditionTime", specificConditionToUse);
			writer.WritePrivateField("areaDistance", specificConditionToUse);
			writer.WritePrivateField("itemAreas", specificConditionToUse);
			writer.WritePrivateField("itemTypes", specificConditionToUse);
			writer.WriteProperty("value", specificConditionToUse.value, ES3Type_float.Instance);
			writer.WriteProperty("explanationKey", specificConditionToUse.explanationKey, ES3Type_string.Instance);
			writer.WriteProperty("cardKey", specificConditionToUse.cardKey, ES3Type_string.Instance);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000A93C0 File Offset: 0x000A75C0
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpecificConditionToUse specificConditionToUse = (SpecificConditionToUse)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1113510858U)
				{
					if (num <= 691548271U)
					{
						if (num != 302144939U)
						{
							if (num == 691548271U)
							{
								if (text == "areaDistance")
								{
									reader.SetPrivateField("areaDistance", reader.Read<Item2.AreaDistance>(), specificConditionToUse);
									continue;
								}
							}
						}
						else if (text == "canAlsoBeOnEmptySpace")
						{
							specificConditionToUse.canAlsoBeOnEmptySpace = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 1090637840U)
					{
						if (num == 1113510858U)
						{
							if (text == "value")
							{
								specificConditionToUse.value = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "itemAreas")
					{
						reader.SetPrivateField("itemAreas", reader.Read<List<Item2.Area>>(), specificConditionToUse);
						continue;
					}
				}
				else if (num <= 1719205583U)
				{
					if (num != 1523737734U)
					{
						if (num == 1719205583U)
						{
							if (text == "explanationKey")
							{
								specificConditionToUse.explanationKey = reader.Read<string>(ES3Type_string.Instance);
								continue;
							}
						}
					}
					else if (text == "cardKey")
					{
						specificConditionToUse.cardKey = reader.Read<string>(ES3Type_string.Instance);
						continue;
					}
				}
				else if (num != 2718545527U)
				{
					if (num != 3588491486U)
					{
						if (num == 3707285407U)
						{
							if (text == "itemTypes")
							{
								reader.SetPrivateField("itemTypes", reader.Read<List<Item2.ItemType>>(), specificConditionToUse);
								continue;
							}
						}
					}
					else if (text == "conditionType")
					{
						specificConditionToUse.conditionType = reader.Read<SpecificConditionToUse.ConditionType>(ES3Type_enum.Instance);
						continue;
					}
				}
				else if (text == "conditionTime")
				{
					reader.SetPrivateField("conditionTime", reader.Read<SpecificConditionToUse.ConditionTime>(), specificConditionToUse);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E45 RID: 3653
		public static ES3Type Instance;
	}
}
