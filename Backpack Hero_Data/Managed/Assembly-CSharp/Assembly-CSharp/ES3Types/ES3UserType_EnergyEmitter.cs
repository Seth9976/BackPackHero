using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001CB RID: 459
	[Preserve]
	[ES3Properties(new string[]
	{
		"type", "heatInformation", "maxHeat", "energyBallPrefab", "filteredItemTypes", "value", "energyValueType", "acceptableEntraces", "sprites", "heldCharge",
		"playable", "toggle", "requiredDistance", "usesPerCharge"
	})]
	public class ES3UserType_EnergyEmitter : ES3ComponentType
	{
		// Token: 0x0600115D RID: 4445 RVA: 0x000A3465 File Offset: 0x000A1665
		public ES3UserType_EnergyEmitter()
			: base(typeof(EnergyEmitter))
		{
			ES3UserType_EnergyEmitter.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000A3484 File Offset: 0x000A1684
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			EnergyEmitter energyEmitter = (EnergyEmitter)obj;
			writer.WriteProperty("type", energyEmitter.type, ES3TypeMgr.GetOrCreateES3Type(typeof(EnergyEmitter.Type), true));
			writer.WritePrivateFieldByRef("heatInformation", energyEmitter);
			writer.WriteProperty("maxHeat", energyEmitter.maxHeat, ES3Type_int.Instance);
			writer.WritePrivateFieldByRef("energyBallPrefab", energyEmitter);
			writer.WritePrivateField("filteredItemTypes", energyEmitter);
			writer.WritePrivateField("value", energyEmitter);
			writer.WriteProperty("energyValueType", energyEmitter.energyValueType, ES3TypeMgr.GetOrCreateES3Type(typeof(EnergyEmitter.EnergyValueType), true));
			writer.WritePrivateField("acceptableEntraces", energyEmitter);
			writer.WritePrivateField("sprites", energyEmitter);
			writer.WritePrivateField("heldCharge", energyEmitter);
			writer.WriteProperty("playable", energyEmitter.playable, ES3Type_bool.Instance);
			writer.WriteProperty("toggle", energyEmitter.toggle, ES3Type_int.Instance);
			writer.WriteProperty("requiredDistance", energyEmitter.requiredDistance, ES3Type_int.Instance);
			writer.WriteProperty("usesPerCharge", energyEmitter.usesPerCharge, ES3Type_int.Instance);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000A35C0 File Offset: 0x000A17C0
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			EnergyEmitter energyEmitter = (EnergyEmitter)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2788247338U)
				{
					if (num <= 1113510858U)
					{
						if (num != 902884759U)
						{
							if (num != 1076453893U)
							{
								if (num == 1113510858U)
								{
									if (text == "value")
									{
										reader.SetPrivateField("value", reader.Read<int>(), energyEmitter);
										continue;
									}
								}
							}
							else if (text == "toggle")
							{
								energyEmitter.toggle = reader.Read<int>(ES3Type_int.Instance);
								continue;
							}
						}
						else if (text == "heatInformation")
						{
							reader.SetPrivateField("heatInformation", reader.Read<GameObject>(), energyEmitter);
							continue;
						}
					}
					else if (num <= 1361572173U)
					{
						if (num != 1170191104U)
						{
							if (num == 1361572173U)
							{
								if (text == "type")
								{
									energyEmitter.type = reader.Read<EnergyEmitter.Type>();
									continue;
								}
							}
						}
						else if (text == "acceptableEntraces")
						{
							reader.SetPrivateField("acceptableEntraces", reader.Read<List<Vector2>>(), energyEmitter);
							continue;
						}
					}
					else if (num != 1440059454U)
					{
						if (num == 2788247338U)
						{
							if (text == "filteredItemTypes")
							{
								reader.SetPrivateField("filteredItemTypes", reader.Read<List<Item2.ItemType>>(), energyEmitter);
								continue;
							}
						}
					}
					else if (text == "heldCharge")
					{
						reader.SetPrivateField("heldCharge", reader.Read<int>(), energyEmitter);
						continue;
					}
				}
				else if (num <= 3605216671U)
				{
					if (num != 2998660816U)
					{
						if (num != 3433282523U)
						{
							if (num == 3605216671U)
							{
								if (text == "playable")
								{
									energyEmitter.playable = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "requiredDistance")
						{
							energyEmitter.requiredDistance = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (text == "usesPerCharge")
					{
						energyEmitter.usesPerCharge = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num <= 3864082109U)
				{
					if (num != 3649976694U)
					{
						if (num == 3864082109U)
						{
							if (text == "sprites")
							{
								reader.SetPrivateField("sprites", reader.Read<List<Sprite>>(), energyEmitter);
								continue;
							}
						}
					}
					else if (text == "energyBallPrefab")
					{
						reader.SetPrivateField("energyBallPrefab", reader.Read<GameObject>(), energyEmitter);
						continue;
					}
				}
				else if (num != 4141084090U)
				{
					if (num == 4158027207U)
					{
						if (text == "maxHeat")
						{
							energyEmitter.maxHeat = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
				}
				else if (text == "energyValueType")
				{
					energyEmitter.energyValueType = reader.Read<EnergyEmitter.EnergyValueType>();
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000DF7 RID: 3575
		public static ES3Type Instance;
	}
}
