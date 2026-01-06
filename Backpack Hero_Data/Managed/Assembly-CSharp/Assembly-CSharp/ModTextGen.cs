using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x02000133 RID: 307
public class ModTextGen : MonoBehaviour
{
	// Token: 0x06000B86 RID: 2950 RVA: 0x00078F14 File Offset: 0x00077114
	private void Awake()
	{
		ModTextGen.main = this;
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00078F1C File Offset: 0x0007711C
	public static void GenerateCreateEffectDescriptions(Item2 item, bool reload = false)
	{
		if (item.createEffects == null)
		{
			return;
		}
		ModdedItem component = item.gameObject.GetComponent<ModdedItem>();
		foreach (Item2.CreateEffect createEffect in item.createEffects)
		{
			if (createEffect.descriptor == null || !(createEffect.descriptor != ""))
			{
				string text = "/createType /itemnames /area";
				if (createEffect.createType == Item2.CreateEffect.CreateType.replace)
				{
					text = text.Replace("/createType", "Replaced with");
				}
				else
				{
					text = text.Replace("/createType", "Creates");
				}
				string text2 = "";
				int num = 0;
				List<string> list = new List<string>();
				foreach (GameObject gameObject in createEffect.itemsToCreate)
				{
					list.Add(LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(gameObject.GetComponent<Item2>().name)));
				}
				foreach (Item2.ItemType itemType in createEffect.typesToCreate)
				{
					list.Add(itemType.ToString() + " item");
				}
				foreach (string text3 in list)
				{
					text2 += text3;
					num++;
					if (num < list.Count - 1)
					{
						text2 += ", ";
					}
					else if (num == list.Count - 1)
					{
						text2 += " and ";
					}
				}
				text = text.Replace("/itemnames", text2);
				string text4 = "";
				int num2 = 0;
				foreach (Item2.Area area in createEffect.areasToCreateTheItem)
				{
					if (area == Item2.Area.self)
					{
						if (createEffect.areasToCreateTheItem.Count != 1)
						{
							text4 += "in this space";
						}
					}
					else
					{
						text4 = text4 + "in a " + area.ToString() + " space";
					}
					num2++;
					if (num2 < createEffect.areasToCreateTheItem.Count - 1)
					{
						text4 += ", ";
					}
					else if (num2 == createEffect.areasToCreateTheItem.Count - 1)
					{
						text4 += " or ";
					}
				}
				text = text.Replace("/area", text4);
				JValue jvalue = new JValue(text);
				string text5 = "CREATEAUTO" + Guid.NewGuid().ToString();
				ModLoader.main.AddTextKey(jvalue, text5, "create effect descriptor", reload, false, component.fromModpack, component);
				createEffect.descriptor = text5;
			}
		}
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0007929C File Offset: 0x0007749C
	public void SetupCards()
	{
		if (this.setup)
		{
			return;
		}
		this.carvingCard = Object.Instantiate<GameObject>(this.carvingCardPrefab, Vector3.zero, Quaternion.identity);
		this.petCard = Object.Instantiate<GameObject>(this.petCardPrefab, Vector3.zero, Quaternion.identity);
		this.treatCard = Object.Instantiate<GameObject>(this.treatCardPrefab, Vector3.zero, Quaternion.identity);
		this.card = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity);
		this.setup = true;
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00079325 File Offset: 0x00077525
	public void DestroyCards()
	{
		if (!this.setup)
		{
			return;
		}
		Object.Destroy(this.carvingCard);
		Object.Destroy(this.petCard);
		Object.Destroy(this.treatCard);
		Object.Destroy(this.card);
		this.setup = false;
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x00079364 File Offset: 0x00077564
	public void DetectEmptyDescriptions(Item2 item)
	{
		GameObject gameObject;
		if (item.itemType.Contains(Item2.ItemType.Carving))
		{
			gameObject = this.carvingCard;
		}
		else if (item.itemType.Contains(Item2.ItemType.Pet))
		{
			gameObject = this.petCard;
		}
		else if (item.itemType.Contains(Item2.ItemType.Treat))
		{
			gameObject = this.treatCard;
		}
		else
		{
			gameObject = this.card;
		}
		Card component = gameObject.GetComponent<Card>();
		component.SetParent(item.gameObject);
		ModdedItem component2 = item.gameObject.GetComponent<ModdedItem>();
		foreach (Item2.Modifier modifier in item.modifiers)
		{
			ModTextGen.HandleTrigger(item, modifier.trigger, component, false);
			if (modifier.descriptionKey == null || modifier.descriptionKey.Length <= 0)
			{
				string text = component.GetModifierDescription(modifier, true).Trim();
				if (text == null || text.Length <= 3)
				{
					ModLog.LogWarning(component2.fromModpack.internalName, item.name, "Game has no builtin description for this modifier, generating one.");
					modifier.descriptionKey = "AUTO" + Guid.NewGuid().ToString();
					ModLoader.main.AddTextKey(new JValue(ModTextGen.GenerateModifierDescription(item, modifier, false)), modifier.descriptionKey, "", false, false, component2.fromModpack, component2);
				}
			}
		}
		foreach (Item2.AddModifier addModifier in item.addModifiers)
		{
			ModTextGen.HandleTrigger(item, addModifier.modifier.trigger, component, false);
			ModTextGen.HandleTrigger(item, addModifier.trigger, component, false);
			if (addModifier.modifier.descriptionKey == null || addModifier.modifier.descriptionKey.Length <= 0)
			{
				string text2 = component.GetModifierDescription(addModifier.modifier, true).Trim();
				if (text2 == null || text2.Length <= 3)
				{
					ModLog.LogWarning(component2.fromModpack.internalName, item.name, "Game has no builtin description for this add_modifier, generating one.");
					addModifier.modifier.descriptionKey = "AUTO" + Guid.NewGuid().ToString();
					ModLoader.main.AddTextKey(new JValue(ModTextGen.GenerateModifierDescription(item, addModifier.modifier, false)), addModifier.modifier.descriptionKey, "", false, false, component2.fromModpack, component2);
				}
			}
			if (addModifier.descriptionKey == null || addModifier.descriptionKey.Length <= 0)
			{
				string text3 = component.GetAddModifierDescription(addModifier).Trim();
				if (text3 == null || text3.Split(":", StringSplitOptions.None)[0].Length <= 16)
				{
					addModifier.descriptionKey = "AUTO" + Guid.NewGuid().ToString();
					ModLoader.main.AddTextKey(new JValue(ModTextGen.GenerateAddModifierDescription(item, addModifier, false)), addModifier.descriptionKey, "", false, false, component2.fromModpack, component2);
				}
			}
		}
		foreach (Item2.CombattEffect combattEffect in item.combatEffects)
		{
			ModTextGen.HandleTrigger(item, combattEffect.trigger, component, false);
			if (combattEffect.effect.effectOverrideKey == null || combattEffect.effect.effectOverrideKey.Length <= 0)
			{
				string effectTotalDescription = component.GetEffectTotalDescription(Item2.GetEffectTotalFromCombatEffect(combattEffect));
				if (effectTotalDescription == null || effectTotalDescription.Length <= 3)
				{
					combattEffect.effect.effectOverrideKey = "AUTO" + Guid.NewGuid().ToString();
					ModLog.LogWarning(component2.fromModpack.internalName, item.name, "Game has no builtin description for this combat effect, generating one.");
					ModLoader.main.AddTextKey(new JValue(ModTextGen.GenerateCombatEffectDescription(item, combattEffect, false)), combattEffect.effect.effectOverrideKey, "", false, false, component2.fromModpack, component2);
				}
			}
		}
		foreach (Item2.MovementEffect movementEffect in item.movementEffects)
		{
			ModTextGen.HandleTrigger(item, movementEffect.trigger, component, false);
			if (movementEffect.descriptionKey == null || movementEffect.descriptionKey.Length <= 0)
			{
				string movementEffects = component.GetMovementEffects(movementEffect);
				if (movementEffects == null || movementEffects.Length <= 3)
				{
					movementEffect.descriptionKey = "AUTO" + Guid.NewGuid().ToString();
					ModLog.LogWarning(component2.fromModpack.internalName, item.name, "Game has no builtin description for this movement effect, generating one.");
					ModLoader.main.AddTextKey(new JValue(ModTextGen.GenerateMovementEffectDescription(item, movementEffect, false)), movementEffect.descriptionKey, "", false, false, component2.fromModpack, component2);
				}
			}
		}
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x000798CC File Offset: 0x00077ACC
	public static void HandleTrigger(Item2 item, Item2.Trigger trigger, Card cardComp, bool reload = false)
	{
		if (trigger.triggerOverrideKey != null && trigger.triggerOverrideKey.Length > 0)
		{
			return;
		}
		if (cardComp.GetTriggerDescription(trigger, false).Length > 2)
		{
			return;
		}
		if (Item2.Trigger.IsConstant(trigger.trigger) && trigger.areas.SequenceEqual(new List<Item2.Area> { Item2.Area.self }))
		{
			return;
		}
		trigger.triggerOverrideKey = "AUTO" + Guid.NewGuid().ToString();
		ModLoader.main.AddTextKey("<color=red>TRIGGER TEXT COULD NOT BE GENERATED. PLEASE ADD A DESCRIPTION MANUALLY</color>", trigger.triggerOverrideKey, "", false, false, item.gameObject.GetComponent<ModdedItem>().fromModpack, item.gameObject.GetComponent<ModdedItem>());
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x00079988 File Offset: 0x00077B88
	public static string GenerateMovementEffectDescription(Item2 item, Item2.MovementEffect cEffect, bool reload = false)
	{
		return "<color=red>ERROR: DESCRIPTION FOR MOVEMENT EFFECT COULD NOT BE GENERATED. PLEASE ADD A DESCRIPTION MANUALLY</color>";
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x00079990 File Offset: 0x00077B90
	public static string GenerateCombatEffectDescription(Item2 item, Item2.CombattEffect cEffect, bool reload = false)
	{
		string text2;
		try
		{
			bool flag = cEffect.effect.mathematicalType == Item2.Effect.MathematicalType.summative;
			bool flag2 = cEffect.effect.value < 0f;
			string text = "";
			if (flag || flag2)
			{
				text += ((!flag2) ? "Adds /x" : "Removes /x");
				if (!flag)
				{
					text += "%";
				}
				text = text + " " + cEffect.effect.type.ToString() + " ";
				if (cEffect.effect.target != Item2.Effect.Target.unspecified)
				{
					text += ((!flag2) ? "to " : "from ");
					text += ModTextGen.Target(cEffect.effect.target, true);
				}
			}
			else if (cEffect.effect.value == 0f)
			{
				text = string.Concat(new string[]
				{
					text,
					"Removes all ",
					cEffect.effect.type.ToString(),
					" ",
					(cEffect.effect.target != Item2.Effect.Target.unspecified) ? ("from " + ModTextGen.Target(cEffect.effect.target, true)) : ""
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					"Multiplies ",
					cEffect.effect.type.ToString(),
					" ",
					(cEffect.effect.target != Item2.Effect.Target.unspecified) ? ModTextGen.Target(cEffect.effect.target, false) : "",
					"by /x%"
				});
			}
			text2 = text;
		}
		catch
		{
			text2 = "<color=red>ERROR: DESCRIPTION FOR COMBAT EFFECT COULD NOT BE GENERATED. PLEASE ADD A DESCRIPTION MANUALLY</color>";
		}
		return text2;
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x00079B70 File Offset: 0x00077D70
	public static string GenerateAddModifierDescription(Item2 item, Item2.AddModifier addmod, bool reload = false)
	{
		string text;
		try
		{
			text = (ModTextGen.TypeAndArea(addmod.typesToModify, addmod.areasToModify, addmod.areaDistance) + "get this affect applied" + ModTextGen.TimeLimit(addmod.lengthForThisModifier)).Trim();
		}
		catch
		{
			text = "<color=red>ERROR: DESCRIPTION FOR ADDMODIFIER COULD NOT BE GENERATED. PLEASE ADD A DESCRIPTION MANUALLY</color>";
		}
		return text;
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x00079BCC File Offset: 0x00077DCC
	public static string TypeAndArea(List<Item2.ItemType> itemTypes, List<Item2.Area> areas, Item2.AreaDistance distance)
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		foreach (Item2.ItemType itemType in itemTypes)
		{
			bool flag = ModTextGen.typesSingular.Contains(itemType);
			if (text2 != "")
			{
				text2 += "and ";
			}
			text2 = text2 + itemType.ToString() + (flag ? " items " : "s ");
			switch (itemType)
			{
			case Item2.ItemType.Any:
				text2 = "items ";
				break;
			case Item2.ItemType.Grid:
				text2 = "spaces ";
				break;
			case Item2.ItemType.GridEmpty:
				text2 = "empty spaces ";
				break;
			default:
				if (itemType == Item2.ItemType.undefined)
				{
					throw new ModUtils.ParseException("Unsupported type: " + itemType.ToString());
				}
				break;
			}
			if (distance == Item2.AreaDistance.closest)
			{
				text = "Closest ";
				text2 = text2.Remove(text2.LastIndexOf("s"), 1);
			}
		}
		foreach (Item2.Area area in areas)
		{
			if (text3 != "")
			{
				text3 += "or ";
			}
			if (!ModTextGen.blankAreaKeys.Contains(area))
			{
				if (!ModTextGen.area2SKeys.ContainsKey(area))
				{
					throw new ModUtils.ParseException("Unsupported area: " + area.ToString());
				}
				text3 += ModTextGen.area2SKeys[area];
			}
		}
		return text + text2 + text3;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00079D8C File Offset: 0x00077F8C
	public static string Target(Item2.Effect.Target target, bool skipTo = false)
	{
		if (!ModTextGen.targetStrings.ContainsKey(target))
		{
			throw new ModUtils.ParseException("Unsupported target: " + target.ToString());
		}
		string text = ModTextGen.targetStrings[target];
		if (text != "" && !skipTo)
		{
			text = "to " + text;
		}
		return text;
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x00079DEC File Offset: 0x00077FEC
	public static string TimeLimit(Item2.Modifier.Length length)
	{
		if (!ModTextGen.lengthStrings.ContainsKey(length))
		{
			throw new ModUtils.ParseException("Unsupported length: " + length.ToString());
		}
		return ModTextGen.lengthStrings[length];
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x00079E24 File Offset: 0x00078024
	public static string GenerateModifierDescription(Item2 item, Item2.Modifier mod, bool reload = false)
	{
		string text3;
		try
		{
			bool flag = false;
			string text;
			if (mod.areasToModify.SequenceEqual(new List<Item2.Area> { Item2.Area.self }))
			{
				flag = true;
				text = "This item ";
			}
			else
			{
				text = ModTextGen.TypeAndArea(mod.typesToModify, mod.areasToModify, mod.areaDistance);
			}
			string text2;
			if (mod.effects[0].type == Item2.Effect.Type.Activate)
			{
				if (flag)
				{
					text2 = "is used ";
				}
				else
				{
					text2 = "are used ";
				}
			}
			else if (mod.effects[0].type == Item2.Effect.Type.Destroy)
			{
				if (flag)
				{
					text2 = "is destroyed ";
				}
				else
				{
					text2 = "are destroyed ";
				}
			}
			else
			{
				if (mod.effects[0].type == Item2.Effect.Type.ItemStatusEffect)
				{
					throw new Exception();
				}
				text2 = "get";
				if (flag)
				{
					text2 += "s";
				}
				if (mod.effects[0].mathematicalType == Item2.Effect.MathematicalType.multiplicative)
				{
					text2 += " /x% bonus ";
				}
				else
				{
					text2 += " /x ";
				}
				if (mod.effects[0].type == Item2.Effect.Type.AP)
				{
					text2 += "energy ";
				}
				else
				{
					text2 = text2 + mod.effects[0].type.ToString() + " ";
				}
			}
			text3 = (text + text2 + ModTextGen.Target(mod.effects[0].target, false) + ModTextGen.TimeLimit(mod.length)).Trim();
		}
		catch
		{
			text3 = "<color=red>ERROR: DESCRIPTION FOR MODIFIER COULD NOT BE GENERATED. PLEASE ADD A DESCRIPTION MANUALLY</color>";
		}
		return text3;
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x00079FD8 File Offset: 0x000781D8
	public static string GetModifierRotation(List<Item2.Area> areas)
	{
		foreach (KeyValuePair<Item2.Area, string> keyValuePair in ModTextGen.rotationalStrings)
		{
			if (areas.Contains(keyValuePair.Key))
			{
				return keyValuePair.Value;
			}
		}
		return "";
	}

	// Token: 0x04000964 RID: 2404
	public static ModTextGen main;

	// Token: 0x04000965 RID: 2405
	public GameObject carvingCardPrefab;

	// Token: 0x04000966 RID: 2406
	public GameObject petCardPrefab;

	// Token: 0x04000967 RID: 2407
	public GameObject cardPrefab;

	// Token: 0x04000968 RID: 2408
	public GameObject treatCardPrefab;

	// Token: 0x04000969 RID: 2409
	public GameObject card;

	// Token: 0x0400096A RID: 2410
	public GameObject petCard;

	// Token: 0x0400096B RID: 2411
	public GameObject treatCard;

	// Token: 0x0400096C RID: 2412
	public GameObject carvingCard;

	// Token: 0x0400096D RID: 2413
	public bool setup;

	// Token: 0x0400096E RID: 2414
	private static List<Item2.ItemType> typesSingular = new List<Item2.ItemType>
	{
		Item2.ItemType.Armor,
		Item2.ItemType.Accessory,
		Item2.ItemType.Mana,
		Item2.ItemType.Clothing,
		Item2.ItemType.Footwear,
		Item2.ItemType.Fish,
		Item2.ItemType.Magic,
		Item2.ItemType.Melee,
		Item2.ItemType.Scary,
		Item2.ItemType.Festive,
		Item2.ItemType.Kin,
		Item2.ItemType.Sap
	};

	// Token: 0x0400096F RID: 2415
	private static Dictionary<Item2.Area, string> area2SKeys = new Dictionary<Item2.Area, string>
	{
		{
			Item2.Area.adjacent,
			"adjacent to this "
		},
		{
			Item2.Area.diagonal,
			"diagonal to this "
		},
		{
			Item2.Area.connected,
			"Connected to this "
		},
		{
			Item2.Area.top,
			"above "
		},
		{
			Item2.Area.bottom,
			"below "
		},
		{
			Item2.Area.column,
			"in this column "
		},
		{
			Item2.Area.left,
			"to the left "
		},
		{
			Item2.Area.right,
			"to the right "
		},
		{
			Item2.Area.row,
			"in this row "
		},
		{
			Item2.Area.rowThenColumn,
			"[rotation] "
		},
		{
			Item2.Area.columnThenRow,
			"[rotation] "
		},
		{
			Item2.Area.oneSpaceOver,
			"two spaces away "
		},
		{
			Item2.Area.rightRotational,
			"[rotation] "
		},
		{
			Item2.Area.leftRotational,
			"[rotation] "
		},
		{
			Item2.Area.topRotational,
			"[rotation] "
		},
		{
			Item2.Area.bottomRotation,
			"[rotation] "
		},
		{
			Item2.Area.toteHand,
			"in your hand "
		},
		{
			Item2.Area.diagonalLine,
			"in a diagonal line "
		},
		{
			Item2.Area.inThisPocket,
			"in this pocket "
		},
		{
			Item2.Area.inAnotherPocket,
			"in another pocket "
		},
		{
			Item2.Area.inAUniquePocket,
			"in an unique pocket "
		}
	};

	// Token: 0x04000970 RID: 2416
	private static List<Item2.Area> blankAreaKeys = new List<Item2.Area>
	{
		Item2.Area.board,
		Item2.Area.self,
		Item2.Area.comboTarget,
		Item2.Area.undefined,
		Item2.Area.myPlaySpace,
		Item2.Area.AnythingEvenOutOfGrid
	};

	// Token: 0x04000971 RID: 2417
	private static Dictionary<Item2.Effect.Target, string> targetStrings = new Dictionary<Item2.Effect.Target, string>
	{
		{
			Item2.Effect.Target.unspecified,
			""
		},
		{
			Item2.Effect.Target.player,
			"self "
		},
		{
			Item2.Effect.Target.enemy,
			"enemy "
		},
		{
			Item2.Effect.Target.allEnemies,
			"all Enemies "
		},
		{
			Item2.Effect.Target.everyone,
			"everyone "
		},
		{
			Item2.Effect.Target.reactiveEnemy,
			"reactive enemy "
		},
		{
			Item2.Effect.Target.allFriendlies,
			"all friendlies "
		},
		{
			Item2.Effect.Target.adjacentFriendlies,
			"adjacent friendlies "
		},
		{
			Item2.Effect.Target.friendliesInFront,
			"friendlies in front "
		},
		{
			Item2.Effect.Target.friendliesBehind,
			"friendlies behind "
		},
		{
			Item2.Effect.Target.frontmostFriendly,
			"friend in front of everyone "
		},
		{
			Item2.Effect.Target.backmostFriendly,
			"friend behind everyone "
		},
		{
			Item2.Effect.Target.truePlayer,
			"player "
		},
		{
			Item2.Effect.Target.everyoneButPochette,
			"everyone but pochette "
		},
		{
			Item2.Effect.Target.statusFromItem,
			""
		}
	};

	// Token: 0x04000972 RID: 2418
	private static Dictionary<Item2.Modifier.Length, string> lengthStrings = new Dictionary<Item2.Modifier.Length, string>
	{
		{
			Item2.Modifier.Length.whileActive,
			""
		},
		{
			Item2.Modifier.Length.forTurn,
			"this turn "
		},
		{
			Item2.Modifier.Length.forCombat,
			"this combat "
		},
		{
			Item2.Modifier.Length.untilRotate,
			"until rotated "
		},
		{
			Item2.Modifier.Length.untilUse,
			"until used "
		},
		{
			Item2.Modifier.Length.permanent,
			""
		},
		{
			Item2.Modifier.Length.whileComboing,
			"while comboing "
		},
		{
			Item2.Modifier.Length.twoTurns,
			"for two turns "
		},
		{
			Item2.Modifier.Length.untilDiscard,
			"until discarded "
		},
		{
			Item2.Modifier.Length.whileItemIsInInventory,
			"while in inventory "
		},
		{
			Item2.Modifier.Length.untilUnzombied,
			"until no longer Zombiefied "
		}
	};

	// Token: 0x04000973 RID: 2419
	private static Dictionary<Item2.Area, string> rotationalStrings = new Dictionary<Item2.Area, string>
	{
		{
			Item2.Area.top,
			"[above] "
		},
		{
			Item2.Area.bottom,
			"[below] "
		},
		{
			Item2.Area.left,
			"[to the left] "
		},
		{
			Item2.Area.right,
			"[to the right] "
		},
		{
			Item2.Area.column,
			"[in this column] "
		},
		{
			Item2.Area.row,
			"[in this row] "
		}
	};
}
