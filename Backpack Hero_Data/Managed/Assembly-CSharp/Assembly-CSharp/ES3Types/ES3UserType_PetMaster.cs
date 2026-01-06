using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001FB RID: 507
	[Preserve]
	[ES3Properties(new string[]
	{
		"outlineMaterial", "standardMaterial", "wigglyMaterial", "petProxyPrefab", "statusPrefab", "energyPrefab", "combatPetPrefab", "petItemPrefab", "petItem", "petInventoryPrefab",
		"petInventory", "health", "maxHealth", "currentAP", "APperTurn", "APonSummon", "petEffects"
	})]
	public class ES3UserType_PetMaster : ES3ComponentType
	{
		// Token: 0x060011BD RID: 4541 RVA: 0x000A7215 File Offset: 0x000A5415
		public ES3UserType_PetMaster()
			: base(typeof(PetMaster))
		{
			ES3UserType_PetMaster.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000A7234 File Offset: 0x000A5434
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			PetMaster petMaster = (PetMaster)obj;
			writer.WritePrivateFieldByRef("outlineMaterial", petMaster);
			writer.WritePrivateFieldByRef("standardMaterial", petMaster);
			writer.WritePrivateFieldByRef("wigglyMaterial", petMaster);
			writer.WritePrivateFieldByRef("petProxyPrefab", petMaster);
			writer.WritePrivateFieldByRef("statusPrefab", petMaster);
			writer.WritePrivateFieldByRef("energyPrefab", petMaster);
			writer.WritePropertyByRef("combatPetPrefab", petMaster.combatPetPrefab);
			writer.WritePropertyByRef("petItemPrefab", petMaster.petItemPrefab);
			writer.WritePropertyByRef("petItem", petMaster.petItem);
			writer.WritePropertyByRef("petInventoryPrefab", petMaster.petInventoryPrefab);
			writer.WritePropertyByRef("petInventory", petMaster.petInventory);
			writer.WriteProperty("health", petMaster.health, ES3Type_int.Instance);
			writer.WriteProperty("maxHealth", petMaster.maxHealth, ES3Type_int.Instance);
			writer.WriteProperty("currentAP", petMaster.currentAP, ES3Type_int.Instance);
			writer.WriteProperty("APperTurn", petMaster.APperTurn, ES3Type_int.Instance);
			writer.WriteProperty("APonSummon", petMaster.APonSummon, ES3Type_int.Instance);
			writer.WriteProperty("petEffects", petMaster.petEffects, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.CombattEffect>), true));
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000A7390 File Offset: 0x000A5590
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			PetMaster petMaster = (PetMaster)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1255207024U)
				{
					if (num <= 465667368U)
					{
						if (num <= 369606673U)
						{
							if (num != 307178141U)
							{
								if (num == 369606673U)
								{
									if (text == "standardMaterial")
									{
										reader.SetPrivateField("standardMaterial", reader.Read<Material>(), petMaster);
										continue;
									}
								}
							}
							else if (text == "petItem")
							{
								petMaster.petItem = reader.Read<PetItem>(ES3UserType_PetItem.Instance);
								continue;
							}
						}
						else if (num != 434419771U)
						{
							if (num == 465667368U)
							{
								if (text == "petInventoryPrefab")
								{
									petMaster.petInventoryPrefab = reader.Read<GameObject>(ES3Type_GameObject.Instance);
									continue;
								}
							}
						}
						else if (text == "maxHealth")
						{
							petMaster.maxHealth = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (num <= 654507658U)
					{
						if (num != 634588512U)
						{
							if (num == 654507658U)
							{
								if (text == "petInventory")
								{
									petMaster.petInventory = reader.Read<Transform>(ES3Type_Transform.Instance);
									continue;
								}
							}
						}
						else if (text == "petEffects")
						{
							petMaster.petEffects = reader.Read<List<Item2.CombattEffect>>();
							continue;
						}
					}
					else if (num != 839242317U)
					{
						if (num == 1255207024U)
						{
							if (text == "APperTurn")
							{
								petMaster.APperTurn = reader.Read<int>(ES3Type_int.Instance);
								continue;
							}
						}
					}
					else if (text == "wigglyMaterial")
					{
						reader.SetPrivateField("wigglyMaterial", reader.Read<Material>(), petMaster);
						continue;
					}
				}
				else if (num <= 3021339481U)
				{
					if (num <= 1868058243U)
					{
						if (num != 1805184399U)
						{
							if (num == 1868058243U)
							{
								if (text == "currentAP")
								{
									petMaster.currentAP = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "health")
						{
							petMaster.health = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (num != 2549324786U)
					{
						if (num == 3021339481U)
						{
							if (text == "statusPrefab")
							{
								reader.SetPrivateField("statusPrefab", reader.Read<GameObject>(), petMaster);
								continue;
							}
						}
					}
					else if (text == "APonSummon")
					{
						petMaster.APonSummon = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num <= 3752393096U)
				{
					if (num != 3257587379U)
					{
						if (num == 3752393096U)
						{
							if (text == "outlineMaterial")
							{
								reader.SetPrivateField("outlineMaterial", reader.Read<Material>(), petMaster);
								continue;
							}
						}
					}
					else if (text == "energyPrefab")
					{
						reader.SetPrivateField("energyPrefab", reader.Read<GameObject>(), petMaster);
						continue;
					}
				}
				else if (num != 3754506736U)
				{
					if (num != 4054525947U)
					{
						if (num == 4190216984U)
						{
							if (text == "petProxyPrefab")
							{
								reader.SetPrivateField("petProxyPrefab", reader.Read<GameObject>(), petMaster);
								continue;
							}
						}
					}
					else if (text == "petItemPrefab")
					{
						petMaster.petItemPrefab = reader.Read<GameObject>(ES3Type_GameObject.Instance);
						continue;
					}
				}
				else if (text == "combatPetPrefab")
				{
					petMaster.combatPetPrefab = reader.Read<GameObject>(ES3Type_GameObject.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E27 RID: 3623
		public static ES3Type Instance;
	}
}
