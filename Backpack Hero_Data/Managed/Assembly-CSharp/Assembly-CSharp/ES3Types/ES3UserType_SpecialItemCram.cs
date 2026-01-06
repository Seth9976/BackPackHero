using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200020B RID: 523
	[Preserve]
	[ES3Properties(new string[] { "type", "areas", "areaDistance", "directionalOverride", "overrideDirection", "trigger", "target", "description", "value" })]
	public class ES3UserType_SpecialItemCram : ES3ComponentType
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x000A853D File Offset: 0x000A673D
		public ES3UserType_SpecialItemCram()
			: base(typeof(SpecialItemCram))
		{
			ES3UserType_SpecialItemCram.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000A855C File Offset: 0x000A675C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpecialItemCram specialItemCram = (SpecialItemCram)obj;
			writer.WritePrivateField("type", specialItemCram);
			writer.WritePrivateField("areas", specialItemCram);
			writer.WritePrivateField("areaDistance", specialItemCram);
			writer.WritePrivateField("directionalOverride", specialItemCram);
			writer.WriteProperty("overrideDirection", specialItemCram.overrideDirection, ES3Type_bool.Instance);
			writer.WriteProperty("trigger", specialItemCram.trigger, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Trigger), true));
			writer.WriteProperty("target", specialItemCram.target, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Effect.Target), true));
			writer.WriteProperty("description", specialItemCram.description, ES3Type_string.Instance);
			writer.WriteProperty("value", specialItemCram.value, ES3Type_float.Instance);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000A8634 File Offset: 0x000A6834
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpecialItemCram specialItemCram = (SpecialItemCram)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 970568434U)
				{
					if (num <= 845187144U)
					{
						if (num != 691548271U)
						{
							if (num == 845187144U)
							{
								if (text == "target")
								{
									specialItemCram.target = reader.Read<Item2.Effect.Target>();
									continue;
								}
							}
						}
						else if (text == "areaDistance")
						{
							reader.SetPrivateField("areaDistance", reader.Read<Item2.AreaDistance>(), specialItemCram);
							continue;
						}
					}
					else if (num != 879704937U)
					{
						if (num == 970568434U)
						{
							if (text == "overrideDirection")
							{
								specialItemCram.overrideDirection = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "description")
					{
						specialItemCram.description = reader.Read<string>(ES3Type_string.Instance);
						continue;
					}
				}
				else if (num <= 1339115925U)
				{
					if (num != 1113510858U)
					{
						if (num == 1339115925U)
						{
							if (text == "areas")
							{
								reader.SetPrivateField("areas", reader.Read<List<Item2.Area>>(), specialItemCram);
								continue;
							}
						}
					}
					else if (text == "value")
					{
						specialItemCram.value = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 1361572173U)
				{
					if (num != 1967206915U)
					{
						if (num == 2619741367U)
						{
							if (text == "directionalOverride")
							{
								reader.SetPrivateField("directionalOverride", reader.Read<Item2.Area>(), specialItemCram);
								continue;
							}
						}
					}
					else if (text == "trigger")
					{
						specialItemCram.trigger = reader.Read<Item2.Trigger>();
						continue;
					}
				}
				else if (text == "type")
				{
					reader.SetPrivateField("type", reader.Read<SpecialItemCram.Type>(), specialItemCram);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E37 RID: 3639
		public static ES3Type Instance;
	}
}
