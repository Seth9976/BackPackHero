using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001F9 RID: 505
	[Preserve]
	[ES3Properties(new string[] { "maxHealth", "health", "APperTurn", "summoningCosts", "defaultCombatEffects", "petProxyPrefab", "statusPrefab", "combatPetPrefab" })]
	public class ES3UserType_PetItem2 : ES3ComponentType
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x000A6ED5 File Offset: 0x000A50D5
		public ES3UserType_PetItem2()
			: base(typeof(PetItem2))
		{
			ES3UserType_PetItem2.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000A6EF4 File Offset: 0x000A50F4
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			PetItem2 petItem = (PetItem2)obj;
			writer.WriteProperty("maxHealth", petItem.maxHealth, ES3Type_int.Instance);
			writer.WriteProperty("health", petItem.health, ES3Type_int.Instance);
			writer.WriteProperty("APperTurn", petItem.APperTurn, ES3Type_int.Instance);
			writer.WriteProperty("summoningCosts", petItem.summoningCosts, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.Cost>), true));
			writer.WriteProperty("defaultCombatEffects", petItem.defaultCombatEffects, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.CombattEffect>), true));
			writer.WritePropertyByRef("petProxyPrefab", petItem.petProxyPrefab);
			writer.WritePropertyByRef("statusPrefab", petItem.statusPrefab);
			writer.WritePropertyByRef("combatPetPrefab", petItem.combatPetPrefab);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000A6FD0 File Offset: 0x000A51D0
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			PetItem2 petItem = (PetItem2)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1832387084U)
				{
					if (num <= 1255207024U)
					{
						if (num != 434419771U)
						{
							if (num == 1255207024U)
							{
								if (text == "APperTurn")
								{
									petItem.APperTurn = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "maxHealth")
						{
							petItem.maxHealth = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (num != 1805184399U)
					{
						if (num == 1832387084U)
						{
							if (text == "summoningCosts")
							{
								petItem.summoningCosts = reader.Read<List<Item2.Cost>>();
								continue;
							}
						}
					}
					else if (text == "health")
					{
						petItem.health = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num <= 3021339481U)
				{
					if (num != 1881111572U)
					{
						if (num == 3021339481U)
						{
							if (text == "statusPrefab")
							{
								petItem.statusPrefab = reader.Read<GameObject>(ES3Type_GameObject.Instance);
								continue;
							}
						}
					}
					else if (text == "defaultCombatEffects")
					{
						petItem.defaultCombatEffects = reader.Read<List<Item2.CombattEffect>>();
						continue;
					}
				}
				else if (num != 3754506736U)
				{
					if (num == 4190216984U)
					{
						if (text == "petProxyPrefab")
						{
							petItem.petProxyPrefab = reader.Read<GameObject>(ES3Type_GameObject.Instance);
							continue;
						}
					}
				}
				else if (text == "combatPetPrefab")
				{
					petItem.combatPetPrefab = reader.Read<GameObject>(ES3Type_GameObject.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E25 RID: 3621
		public static ES3Type Instance;
	}
}
