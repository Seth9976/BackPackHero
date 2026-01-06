using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001D1 RID: 465
	[Preserve]
	[ES3Properties(new string[]
	{
		"resourcesToGet", "markerIfBroughtHome", "oneOfAKindType", "parentInventoryGrid", "lastParentInventoryGrid", "itemType", "rarity", "originalCost", "playType", "costs",
		"playerAnimation", "soundEffect", "descriptions", "moveArea", "mustBePlacedOnItemType", "mustBePlacedOnItemTypeInCombat", "combatEffects", "usesLimits", "createEffects", "modifiers",
		"movementEffects", "addModifiers", "energyEffects", "activeItemStatusEffects", "contextMenuOptions", "appliedModifierModifiers", "appliedModifiers", "isDiscounted", "isForSale", "destroyed",
		"enabled", "name"
	})]
	public class ES3UserType_Item2 : ES3ComponentType
	{
		// Token: 0x06001169 RID: 4457 RVA: 0x000A3C39 File Offset: 0x000A1E39
		public ES3UserType_Item2()
			: base(typeof(Item2))
		{
			ES3UserType_Item2.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000A3C58 File Offset: 0x000A1E58
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Item2 item = (Item2)obj;
			writer.WriteProperty("resourcesToGet", item.resourcesToGet, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Overworld_ResourceManager.Resource>), true));
			writer.WriteProperty("markerIfBroughtHome", item.markerIfBroughtHome, ES3TypeMgr.GetOrCreateES3Type(typeof(MetaProgressSaveManager.MetaProgressMarker), true));
			writer.WriteProperty("oneOfAKindType", item.oneOfAKindType, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.OneOfAKindType), true));
			writer.WritePropertyByRef("parentInventoryGrid", item.parentInventoryGrid);
			writer.WritePropertyByRef("lastParentInventoryGrid", item.lastParentInventoryGrid);
			writer.WriteProperty("itemType", item.itemType, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.ItemType>), true));
			writer.WriteProperty("rarity", item.rarity, ES3Type_enum.Instance);
			writer.WriteProperty("originalCost", item.originalCost, ES3Type_int.Instance);
			writer.WriteProperty("playType", item.playType, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.PlayType), true));
			writer.WriteProperty("costs", item.costs, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.Cost>), true));
			writer.WriteProperty("playerAnimation", item.playerAnimation, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.PlayerAnimation), true));
			writer.WriteProperty("soundEffect", item.soundEffect, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.SoundEffect), true));
			writer.WriteProperty("descriptions", item.descriptions, ES3TypeMgr.GetOrCreateES3Type(typeof(List<string>), true));
			writer.WriteProperty("moveArea", item.moveArea, ES3Type_enum.Instance);
			writer.WriteProperty("mustBePlacedOnItemType", item.mustBePlacedOnItemType, ES3Type_enum.Instance);
			writer.WriteProperty("mustBePlacedOnItemTypeInCombat", item.mustBePlacedOnItemTypeInCombat, ES3Type_enum.Instance);
			writer.WriteProperty("combatEffects", item.combatEffects, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.CombattEffect>), true));
			writer.WriteProperty("usesLimits", item.usesLimits, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.LimitedUses>), true));
			writer.WriteProperty("createEffects", item.createEffects, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.CreateEffect>), true));
			writer.WriteProperty("modifiers", item.modifiers, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.Modifier>), true));
			writer.WriteProperty("movementEffects", item.movementEffects, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.MovementEffect>), true));
			writer.WriteProperty("addModifiers", item.addModifiers, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.AddModifier>), true));
			writer.WriteProperty("energyEffects", item.energyEffects, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.EnergyEffect>), true));
			writer.WriteProperty("activeItemStatusEffects", item.activeItemStatusEffects, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.ItemStatusEffect>), true));
			writer.WriteProperty("contextMenuOptions", item.contextMenuOptions, ES3TypeMgr.GetOrCreateES3Type(typeof(List<ContextMenuButton.ContextMenuButtonAndCost>), true));
			writer.WriteProperty("appliedModifierModifiers", item.appliedModifierModifiers, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.Modifier>), true));
			writer.WriteProperty("appliedModifiers", item.appliedModifiers, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.Modifier>), true));
			writer.WriteProperty("isDiscounted", item.isDiscounted, ES3Type_bool.Instance);
			writer.WriteProperty("isForSale", item.isForSale, ES3Type_bool.Instance);
			writer.WriteProperty("destroyed", item.destroyed, ES3Type_bool.Instance);
			writer.WriteProperty("enabled", item.enabled, ES3Type_bool.Instance);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000A4030 File Offset: 0x000A2230
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Item2 item = (Item2)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2454629613U)
				{
					if (num <= 1092292633U)
					{
						if (num <= 337952529U)
						{
							if (num != 49525662U)
							{
								if (num != 52160763U)
								{
									if (num == 337952529U)
									{
										if (text == "costs")
										{
											item.costs = reader.Read<List<Item2.Cost>>();
											continue;
										}
									}
								}
								else if (text == "isForSale")
								{
									item.isForSale = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
							else if (text == "enabled")
							{
								item.enabled = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num <= 465301137U)
						{
							if (num != 403228622U)
							{
								if (num == 465301137U)
								{
									if (text == "resourcesToGet")
									{
										item.resourcesToGet = reader.Read<List<Overworld_ResourceManager.Resource>>();
										continue;
									}
								}
							}
							else if (text == "playerAnimation")
							{
								item.playerAnimation = reader.Read<Item2.PlayerAnimation>();
								continue;
							}
						}
						else if (num != 764483983U)
						{
							if (num == 1092292633U)
							{
								if (text == "modifiers")
								{
									item.modifiers = reader.Read<List<Item2.Modifier>>();
									continue;
								}
							}
						}
						else if (text == "lastParentInventoryGrid")
						{
							item.lastParentInventoryGrid = reader.Read<Transform>(ES3Type_Transform.Instance);
							continue;
						}
					}
					else if (num <= 1508417205U)
					{
						if (num <= 1266615690U)
						{
							if (num != 1256323916U)
							{
								if (num == 1266615690U)
								{
									if (text == "rarity")
									{
										item.rarity = reader.Read<Item2.Rarity>(ES3Type_enum.Instance);
										continue;
									}
								}
							}
							else if (text == "destroyed")
							{
								item.destroyed = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 1446166370U)
						{
							if (num == 1508417205U)
							{
								if (text == "energyEffects")
								{
									item.energyEffects = reader.Read<List<Item2.EnergyEffect>>();
									continue;
								}
							}
						}
						else if (text == "addModifiers")
						{
							item.addModifiers = reader.Read<List<Item2.AddModifier>>();
							continue;
						}
					}
					else if (num <= 1894908305U)
					{
						if (num != 1627500691U)
						{
							if (num == 1894908305U)
							{
								if (text == "combatEffects")
								{
									item.combatEffects = reader.Read<List<Item2.CombattEffect>>();
									continue;
								}
							}
						}
						else if (text == "originalCost")
						{
							item.originalCost = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (num != 2029135143U)
					{
						if (num == 2454629613U)
						{
							if (text == "mustBePlacedOnItemTypeInCombat")
							{
								item.mustBePlacedOnItemTypeInCombat = reader.Read<Item2.ItemType>(ES3Type_enum.Instance);
								continue;
							}
						}
					}
					else if (text == "contextMenuOptions")
					{
						item.contextMenuOptions = reader.Read<List<ContextMenuButton.ContextMenuButtonAndCost>>();
						continue;
					}
				}
				else if (num <= 3517969275U)
				{
					if (num <= 2826197612U)
					{
						if (num <= 2769947118U)
						{
							if (num != 2529758562U)
							{
								if (num == 2769947118U)
								{
									if (text == "descriptions")
									{
										item.descriptions = reader.Read<List<string>>();
										continue;
									}
								}
							}
							else if (text == "markerIfBroughtHome")
							{
								item.markerIfBroughtHome = reader.Read<MetaProgressSaveManager.MetaProgressMarker>();
								continue;
							}
						}
						else if (num != 2824120293U)
						{
							if (num == 2826197612U)
							{
								if (text == "activeItemStatusEffects")
								{
									item.activeItemStatusEffects = reader.Read<List<Item2.ItemStatusEffect>>();
									continue;
								}
							}
						}
						else if (text == "moveArea")
						{
							item.moveArea = reader.Read<Item2.Area>(ES3Type_enum.Instance);
							continue;
						}
					}
					else if (num <= 3016455821U)
					{
						if (num != 2839117148U)
						{
							if (num == 3016455821U)
							{
								if (text == "createEffects")
								{
									item.createEffects = reader.Read<List<Item2.CreateEffect>>();
									continue;
								}
							}
						}
						else if (text == "appliedModifiers")
						{
							item.appliedModifiers = reader.Read<List<Item2.Modifier>>();
							continue;
						}
					}
					else if (num != 3374092598U)
					{
						if (num == 3517969275U)
						{
							if (text == "playType")
							{
								item.playType = reader.Read<Item2.PlayType>();
								continue;
							}
						}
					}
					else if (text == "itemType")
					{
						item.itemType = reader.Read<List<Item2.ItemType>>();
						continue;
					}
				}
				else if (num <= 3948328871U)
				{
					if (num <= 3876851621U)
					{
						if (num != 3792356059U)
						{
							if (num == 3876851621U)
							{
								if (text == "usesLimits")
								{
									item.usesLimits = reader.Read<List<Item2.LimitedUses>>();
									continue;
								}
							}
						}
						else if (text == "parentInventoryGrid")
						{
							item.parentInventoryGrid = reader.Read<Transform>(ES3Type_Transform.Instance);
							continue;
						}
					}
					else if (num != 3931712157U)
					{
						if (num == 3948328871U)
						{
							if (text == "appliedModifierModifiers")
							{
								item.appliedModifierModifiers = reader.Read<List<Item2.Modifier>>();
								continue;
							}
						}
					}
					else if (text == "soundEffect")
					{
						item.soundEffect = reader.Read<Item2.SoundEffect>();
						continue;
					}
				}
				else if (num <= 4118648430U)
				{
					if (num != 4062308406U)
					{
						if (num == 4118648430U)
						{
							if (text == "movementEffects")
							{
								item.movementEffects = reader.Read<List<Item2.MovementEffect>>();
								continue;
							}
						}
					}
					else if (text == "mustBePlacedOnItemType")
					{
						item.mustBePlacedOnItemType = reader.Read<Item2.ItemType>(ES3Type_enum.Instance);
						continue;
					}
				}
				else if (num != 4241925999U)
				{
					if (num == 4270091695U)
					{
						if (text == "oneOfAKindType")
						{
							item.oneOfAKindType = reader.Read<Item2.OneOfAKindType>();
							continue;
						}
					}
				}
				else if (text == "isDiscounted")
				{
					item.isDiscounted = reader.Read<bool>(ES3Type_bool.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000DFD RID: 3581
		public static ES3Type Instance;
	}
}
