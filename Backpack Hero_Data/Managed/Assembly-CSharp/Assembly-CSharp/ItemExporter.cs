using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x0200012B RID: 299
public static class ItemExporter
{
	// Token: 0x06000B3C RID: 2876 RVA: 0x00071B40 File Offset: 0x0006FD40
	public static bool CheckIfItemSupported(GameObject gameObject)
	{
		bool flag = true;
		foreach (Component component in gameObject.GetComponents<Component>())
		{
			if (!ItemExporter.supported_components.Contains(component.GetType()))
			{
				flag = false;
				Debug.LogError(gameObject.name + ": Unsupported component found: " + component.GetType().Name);
			}
		}
		return flag;
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00071BA0 File Offset: 0x0006FDA0
	public static JObject SerializeItem(Item2 item, string dir)
	{
		JObject jobject8;
		try
		{
			if (!ItemExporter.CheckIfItemSupported(item.gameObject))
			{
				throw new Exception("Cannot export " + item.name + ".");
			}
			ItemExporter.currentItemToWorkOn = item.gameObject;
			ItemExporter.hasAlternateUse = false;
			JObject jobject = new JObject();
			string text = new CultureInfo("en-US", false).TextInfo.ToTitleCase(LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(item.name)).ToLower());
			jobject["name"] = text;
			ItemSpriteChanger component = item.GetComponent<ItemSpriteChanger>();
			if (component != null)
			{
				JArray jarray = new JArray();
				component.sprites.Reverse();
				foreach (Sprite sprite in component.sprites)
				{
					string text2 = text + "_" + jarray.Count.ToString() + ".png";
					SpriteExporter.ExportSprite(sprite, dir + text2);
					jarray.Add(text2);
				}
				component.sprites.Reverse();
				jobject["sprite"] = jarray;
				if (component.mode != ItemSpriteChanger.SpriteChangerMode.Auto)
				{
					jobject["sprite_change_mode"] = component.mode.ToString();
				}
			}
			else
			{
				string text3 = text + ".png";
				SpriteExporter.ExportSprite(item.GetComponent<SpriteRenderer>().sprite, dir + text3);
				jobject["sprite"] = text3;
			}
			jobject["shape"] = ItemExporter.SerializeShape(item);
			string text4 = Item2.GetDisplayName(item.name) + "2";
			if (LangaugeManager.main.KeyExists(text4))
			{
				jobject["flavor"] = LangaugeManager.main.GetTextByKey(text4);
			}
			jobject["rarity"] = item.rarity.ToString();
			JArray jarray2 = new JArray();
			foreach (Item2.ItemType itemType in item.itemType)
			{
				jarray2.Add(itemType.ToString());
			}
			jobject["type"] = jarray2;
			JArray jarray3 = new JArray();
			foreach (Item2.ItemGrouping itemGrouping in item.itemGroupings)
			{
				jarray3.Add(itemGrouping.ToString());
			}
			if (jarray3.Count > 0)
			{
				jobject["group"] = jarray3;
			}
			if (item.validForCharacters != null && item.validForCharacters.Count != 0 && !item.validForCharacters.Contains(Character.CharacterName.Any))
			{
				JArray jarray4 = new JArray();
				foreach (Character.CharacterName characterName in item.validForCharacters)
				{
					jarray4.Add(characterName.ToString());
				}
				if (jarray4.Count > 0)
				{
					jobject["supported_characters"] = jarray4;
				}
			}
			if (item.validForZones != null && item.validForZones.Count != 0)
			{
				JArray jarray5 = new JArray();
				foreach (DungeonLevel.Zone zone in item.validForZones)
				{
					jarray5.Add(zone.ToString());
				}
				if (jarray5.Count > 0)
				{
					jobject["found_in"] = jarray5;
				}
			}
			JObject jobject2 = new JObject();
			foreach (Item2.Cost cost in item.costs)
			{
				if (cost.type == Item2.Cost.Type.mustMeetASpecificCondition)
				{
					throw new Exception("Unsupported cost " + cost.type.ToString());
				}
				jobject2[cost.type.ToString()] = cost.baseValue;
			}
			if (jobject2.Children().Count<JToken>() > 0)
			{
				jobject["use_costs"] = jobject2;
			}
			JObject jobject3 = new JObject();
			foreach (Item2.LimitedUses limitedUses in item.usesLimits)
			{
				jobject3[limitedUses.type.ToString()] = (int)limitedUses.value;
			}
			if (jobject3.Children().Count<JToken>() > 0)
			{
				jobject["use_limits"] = jobject3;
			}
			if (item.playerAnimation != Item2.PlayerAnimation.UseItem)
			{
				jobject["animation"] = item.playerAnimation.ToString();
			}
			if (item.soundEffect != Item2.SoundEffect.None)
			{
				jobject["soundeffect"] = item.soundEffect.ToString();
			}
			if (item.playType != Item2.PlayType.Active)
			{
				jobject["playtype"] = item.playType.ToString();
			}
			JObject jobject4 = new JObject();
			if (item.moveArea != Item2.Area.self)
			{
				jobject4["area"] = item.moveArea.ToString();
			}
			if (item.mustBePlacedOnItemType != Item2.ItemType.Grid)
			{
				jobject4["place_on_type"] = item.mustBePlacedOnItemType.ToString();
			}
			if (item.mustBePlacedOnItemTypeInCombat != Item2.ItemType.Grid)
			{
				jobject4["place_on_type_combat"] = item.mustBePlacedOnItemTypeInCombat.ToString();
			}
			if (item.moveDistance != Item2.AreaDistance.all)
			{
				jobject4["distance"] = item.moveDistance.ToString();
			}
			if (jobject4.Children().Count<JToken>() > 0)
			{
				jobject["movable"] = jobject4;
			}
			JArray jarray6 = new JArray();
			foreach (Item2.CombattEffect combattEffect in item.combatEffects)
			{
				JObject jobject5 = ItemExporter.SerializeTrigger(combattEffect.trigger);
				jobject5.Merge(ItemExporter.SerializeEffect(combattEffect.effect));
				jarray6.Add(jobject5);
			}
			if (jarray6.Count > 0)
			{
				jobject["combat_effects"] = jarray6;
			}
			JArray jarray7 = new JArray();
			foreach (Item2.CreateEffect createEffect in item.createEffects)
			{
				jarray7.Add(ItemExporter.SerializeCreateEffect(createEffect));
			}
			if (jarray7.Count > 0)
			{
				jobject["create_effects"] = jarray7;
			}
			JArray jarray8 = new JArray();
			foreach (Item2.Modifier modifier in item.modifiers)
			{
				jarray8.Add(ItemExporter.SerializeModifier(modifier));
			}
			if (jarray8.Count > 0)
			{
				jobject["modifiers"] = jarray8;
			}
			JArray jarray9 = new JArray();
			foreach (Item2.AddModifier addModifier in item.addModifiers)
			{
				jarray9.Add(ItemExporter.SerializeAddModifier(addModifier));
			}
			if (jarray9.Count > 0)
			{
				jobject["add_modifiers"] = jarray9;
			}
			JArray jarray10 = new JArray();
			foreach (Item2.MovementEffect movementEffect in item.movementEffects)
			{
				jarray10.Add(ItemExporter.SerializeMovementEffect(movementEffect));
			}
			if (jarray10.Count > 0)
			{
				jobject["movement_effects"] = jarray10;
			}
			JArray jarray11 = ItemExporter.SerializeItemStatusEffects(item.activeItemStatusEffects);
			if (jarray11.Count > 0)
			{
				jobject["item_status_effects"] = jarray11;
			}
			ManaStone component2 = item.GetComponent<ManaStone>();
			if (component2 != null)
			{
				jobject["manastone"] = new JObject();
				jobject["manastone"]["max_mana"] = component2.maxPower;
				if (component2.startingPower != -1)
				{
					jobject["manastone"]["start_mana"] = component2.startingPower;
				}
			}
			Carving component3 = item.GetComponent<Carving>();
			if (component3 != null)
			{
				jobject["carving"] = new JObject();
				JObject jobject6 = new JObject();
				foreach (Item2.Cost cost2 in component3.summoningCosts)
				{
					jobject6[cost2.type.ToString()] = cost2.baseValue;
				}
				if (jobject6.Children().Count<JToken>() > 0)
				{
					jobject["carving"]["summon_costs"] = jobject6;
				}
			}
			ContextMenuButton.ContextMenuButtonAndCost contextMenuButtonAndCost = item.contextMenuOptions.Find((ContextMenuButton.ContextMenuButtonAndCost x) => x.type == ContextMenuButton.Type.alternateUse);
			if (contextMenuButtonAndCost == null)
			{
				contextMenuButtonAndCost = item.contextMenuOptions.Find((ContextMenuButton.ContextMenuButtonAndCost x) => x.type == ContextMenuButton.Type.selectForComboUse);
			}
			if (contextMenuButtonAndCost != null && ItemExporter.hasAlternateUse)
			{
				jobject["alternate_use"] = new JObject();
				JObject jobject7 = new JObject();
				foreach (Item2.Cost cost3 in contextMenuButtonAndCost.costs)
				{
					if (cost3.type == Item2.Cost.Type.mustMeetASpecificCondition)
					{
						throw new Exception("Unsupported cost " + cost3.type.ToString());
					}
					jobject7[cost3.type.ToString()] = cost3.baseValue;
				}
				if (jobject7.Children().Count<JToken>() > 0)
				{
					jobject["alternate_use"]["use_costs"] = jobject7;
				}
				if (contextMenuButtonAndCost.playerAnimation != Item2.PlayerAnimation.UseItem)
				{
					jobject["alternate_use"]["animation"] = contextMenuButtonAndCost.playerAnimation.ToString();
				}
				if (contextMenuButtonAndCost.useTime != ContextMenuButton.ContextMenuButtonAndCost.UseTime.inOrOutOfBattle)
				{
					jobject["alternate_use"]["use_situation"] = contextMenuButtonAndCost.useTime.ToString();
				}
			}
			jobject8 = jobject;
		}
		catch (Exception ex)
		{
			string text5 = "Cannot export ";
			string name = item.name;
			string text6 = " ";
			Exception ex2 = ex;
			throw new Exception(text5 + name + text6 + ((ex2 != null) ? ex2.ToString() : null));
		}
		return jobject8;
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00072858 File Offset: 0x00070A58
	public static float RoundToNearestHalf(float number)
	{
		return (float)(Math.Round((double)(number * 2f), MidpointRounding.AwayFromZero) / 2.0);
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00072874 File Offset: 0x00070A74
	public static List<Vector2> NormalizeVectors(List<Vector2> vectors)
	{
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		foreach (Vector2 vector in vectors)
		{
			num = Math.Min(num, vector.x);
			num2 = Math.Min(num2, vector.y);
		}
		List<Vector2> list = new List<Vector2>();
		foreach (Vector2 vector2 in vectors)
		{
			float num3 = vector2.x - num;
			float num4 = vector2.y - num2;
			list.Add(new Vector2(num3, num4));
		}
		return list;
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00072944 File Offset: 0x00070B44
	public static Vector2 FindMaxValues(List<Vector2> vectors)
	{
		float num = float.MinValue;
		float num2 = float.MinValue;
		foreach (Vector2 vector in vectors)
		{
			num = Math.Max(num, vector.x);
			num2 = Math.Max(num2, vector.y);
		}
		return new Vector2(num, num2);
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x000729B8 File Offset: 0x00070BB8
	public static JArray SerializeShape(Item2 item)
	{
		BoxCollider2D[] componentsInChildren = item.GetComponentsInChildren<BoxCollider2D>();
		List<Vector2> list = new List<Vector2>();
		List<bool> list2 = new List<bool>();
		foreach (BoxCollider2D boxCollider2D in componentsInChildren)
		{
			for (int j = 0; j < Mathf.RoundToInt(boxCollider2D.size.x); j++)
			{
				for (int k = 0; k < Mathf.RoundToInt(boxCollider2D.size.y); k++)
				{
					float num = boxCollider2D.size.x / -2f + (float)j + 0.5f + boxCollider2D.offset.x;
					float num2 = boxCollider2D.size.y / -2f + (float)k + 0.5f + boxCollider2D.offset.y;
					list.Add(new Vector2(num, num2));
					list2.Add(false);
				}
			}
		}
		foreach (Transform transform in item.GetComponentsInChildren<Transform>())
		{
			if (transform.gameObject.tag == "ItemEffectArea")
			{
				if (item.name.Contains("Wings"))
				{
					list.Add(new Vector2(transform.position.x * -1f, transform.position.y * -1f + 0.5f));
				}
				else
				{
					list.Add(new Vector2(transform.position.x * -1f - 0.5f, transform.position.y * -1f + 0.5f));
				}
				list2.Add(true);
			}
		}
		list = ItemExporter.NormalizeVectors(list);
		Vector2 vector = ItemExporter.FindMaxValues(list);
		List<string> list3 = new List<string>();
		for (int l = 0; l <= Mathf.RoundToInt(vector.y); l++)
		{
			list3.Add(new string('-', Mathf.RoundToInt(vector.x) + 1));
		}
		using (List<Vector2>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Vector2 vec = enumerator.Current;
				int num3 = Mathf.RoundToInt(vec.x);
				int num4 = Mathf.RoundToInt(vec.y);
				StringBuilder stringBuilder = new StringBuilder(list3[num4]);
				if (list2[list.FindIndex((Vector2 x) => x == vec)])
				{
					if (stringBuilder[num3] == 'X')
					{
						stringBuilder[num3] = 'B';
					}
					else
					{
						stringBuilder[num3] = 'A';
					}
				}
				else if (stringBuilder[num3] == 'A')
				{
					stringBuilder[num3] = 'B';
				}
				else
				{
					stringBuilder[num3] = 'X';
				}
				list3[num4] = stringBuilder.ToString();
			}
		}
		list3.Reverse();
		return new JArray(list3);
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x00072CCC File Offset: 0x00070ECC
	public static JObject SerializeTrigger(Item2.Trigger trigger)
	{
		JObject jobject = new JObject();
		if (trigger.trigger == Item2.Trigger.ActionTrigger.whenScripted)
		{
			throw new Exception("Unsupported trigger " + trigger.trigger.ToString());
		}
		if (trigger.trigger == Item2.Trigger.ActionTrigger.onAlternateUse || trigger.trigger == Item2.Trigger.ActionTrigger.onComboUse)
		{
			ItemExporter.hasAlternateUse = true;
		}
		jobject["trigger"] = trigger.trigger.ToString();
		if (trigger.types != null && trigger.types.Count > 0 && (trigger.types.Count != 1 || trigger.types[0] != Item2.ItemType.Any))
		{
			JArray jarray = new JArray();
			foreach (Item2.ItemType itemType in trigger.types)
			{
				jarray.Add(itemType.ToString());
			}
			jobject["trigger_on_type"] = jarray;
		}
		if (trigger.areas != null && trigger.areas.Count > 0 && (trigger.areas.Count != 1 || trigger.areas[0] != Item2.Area.self))
		{
			JArray jarray2 = new JArray();
			foreach (Item2.Area area in trigger.areas)
			{
				jarray2.Add(area.ToString());
			}
			jobject["trigger_area"] = jarray2;
		}
		if (trigger.areaDistance != Item2.AreaDistance.all)
		{
			jobject["trigger_distance"] = trigger.areaDistance.ToString();
		}
		if (trigger.requiresActivation)
		{
			jobject["needs_activation"] = trigger.requiresActivation;
		}
		if (!string.IsNullOrEmpty(trigger.triggerOverrideKey))
		{
			jobject["trigger_description"] = LangaugeManager.main.GetTextByKey(trigger.triggerOverrideKey);
		}
		return jobject;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00072EF8 File Offset: 0x000710F8
	public static JToken ConsiderValueChanger(float value)
	{
		foreach (ValueChanger valueChanger in ItemExporter.currentItemToWorkOn.GetComponentsInChildren<ValueChanger>())
		{
			if (value == valueChanger.valueToReplace)
			{
				JObject jobject = new JObject();
				Dictionary<ValueChanger.ReplaceWithValue, ValueChangerEx.ReplaceWithValue> dictionary = new Dictionary<ValueChanger.ReplaceWithValue, ValueChangerEx.ReplaceWithValue>
				{
					{
						ValueChanger.ReplaceWithValue.block,
						ValueChangerEx.ReplaceWithValue.PlayerBlock
					},
					{
						ValueChanger.ReplaceWithValue.numOfItem,
						ValueChangerEx.ReplaceWithValue.numOfItems
					},
					{
						ValueChanger.ReplaceWithValue.numOfItemType,
						ValueChangerEx.ReplaceWithValue.numOfItemTypes
					},
					{
						ValueChanger.ReplaceWithValue.deadPets,
						ValueChangerEx.ReplaceWithValue.numOfPetsDefeated
					},
					{
						ValueChanger.ReplaceWithValue.amountOfCharmOnTarget,
						ValueChangerEx.ReplaceWithValue.TargetCharm
					},
					{
						ValueChanger.ReplaceWithValue.amountOfPoison,
						ValueChangerEx.ReplaceWithValue.PlayerPoison
					}
				};
				if (dictionary.ContainsKey(valueChanger.replaceWithValue))
				{
					jobject["replace_with"] = dictionary[valueChanger.replaceWithValue].ToString();
				}
				else
				{
					jobject["replace_with"] = valueChanger.replaceWithValue.ToString();
				}
				if (valueChanger.multiplier != 1f)
				{
					jobject["multiplier"] = ItemExporter.ConsiderIntOrFloat(valueChanger.multiplier);
				}
				if (valueChanger.itemPrefabs != null && valueChanger.itemPrefabs.Count > 0)
				{
					JArray jarray = new JArray();
					foreach (GameObject gameObject in valueChanger.itemPrefabs)
					{
						string displayName = Item2.GetDisplayName(gameObject.name);
						string internalName = ItemExporter.exportedPackName;
						if (gameObject.GetComponent<ModdedItem>() != null)
						{
							internalName = gameObject.GetComponent<ModdedItem>().fromModpack.internalName;
						}
						jarray.Add(new JObject(new JProperty(internalName, displayName)));
					}
					jobject["check_items"] = jarray;
				}
				if (valueChanger.types != null && valueChanger.types.Count > 0)
				{
					JArray jarray2 = new JArray();
					foreach (Item2.ItemType itemType in valueChanger.types)
					{
						jarray2.Add(itemType.ToString());
					}
					jobject["check_types"] = jarray2;
				}
				if (valueChanger.areas != null && valueChanger.areas.Count > 0)
				{
					JArray jarray3 = new JArray();
					foreach (Item2.Area area in valueChanger.areas)
					{
						jarray3.Add(area.ToString());
					}
					jobject["check_areas"] = jarray3;
				}
				if (valueChanger.areaDistance != Item2.AreaDistance.all)
				{
					jobject["area_distance"] = valueChanger.areaDistance.ToString();
				}
				return jobject;
			}
		}
		return ItemExporter.ConsiderIntOrFloat(value);
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000731E8 File Offset: 0x000713E8
	public static JObject SerializeEffect(Item2.Effect effect)
	{
		JObject jobject = new JObject();
		jobject["type"] = effect.type.ToString();
		if (effect.value != 0f)
		{
			jobject["value"] = ItemExporter.ConsiderValueChanger(effect.value);
		}
		if (effect.target != Item2.Effect.Target.unspecified)
		{
			jobject["target"] = effect.target.ToString();
		}
		if (effect.mathematicalType == Item2.Effect.MathematicalType.multiplicative)
		{
			jobject["math"] = "mul";
		}
		JArray jarray = ItemExporter.SerializeItemStatusEffects(effect.itemStatusEffect);
		if (jarray.Count > 0)
		{
			jobject["item_status_effects"] = jarray;
		}
		if (!string.IsNullOrEmpty(effect.effectOverrideKey))
		{
			if (effect.effectOverrideKey == "hidden")
			{
				jobject["description"] = JValue.CreateNull();
			}
			else
			{
				string textByKey = LangaugeManager.main.GetTextByKey(effect.effectOverrideKey);
				jobject["description"] = textByKey;
			}
		}
		return jobject;
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x000732FC File Offset: 0x000714FC
	public static JArray SerializeItemStatusEffects(List<Item2.ItemStatusEffect> itemStatusEffects)
	{
		JArray jarray = new JArray();
		foreach (Item2.ItemStatusEffect itemStatusEffect in itemStatusEffects)
		{
			JObject jobject = new JObject();
			if (itemStatusEffect.applyRightAway)
			{
				jobject["apply_immediately"] = itemStatusEffect.applyRightAway;
			}
			jobject["type"] = itemStatusEffect.type.ToString();
			jobject["value"] = ItemExporter.ConsiderIntOrFloat((float)itemStatusEffect.num);
			jobject["length"] = itemStatusEffect.length.ToString();
			jarray.Add(jobject);
		}
		return jarray;
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x000733D4 File Offset: 0x000715D4
	public static JObject SerializeModifier(Item2.Modifier mod)
	{
		JObject jobject = ItemExporter.SerializeTrigger(mod.trigger);
		if (mod.areasToModify != null && mod.areasToModify.Count != 0 && (mod.areasToModify.Count != 1 || mod.areasToModify[0] != Item2.Area.self))
		{
			jobject["mod_area"] = new JArray(mod.areasToModify.Select((Item2.Area area) => area.ToString()));
		}
		jobject["effects"] = new JArray(mod.effects.Select((Item2.Effect effect) => ItemExporter.SerializeEffect(effect)));
		if (mod.typesToModify != null && mod.typesToModify.Count != 0 && (mod.typesToModify.Count != 1 || mod.typesToModify[0] != Item2.ItemType.Any))
		{
			jobject["mod_types"] = new JArray(mod.typesToModify.Select((Item2.ItemType type) => type.ToString()));
		}
		if (mod.areaDistance != Item2.AreaDistance.all)
		{
			jobject["mod_distance"] = mod.areaDistance.ToString();
		}
		if (mod.length != Item2.Modifier.Length.whileActive)
		{
			jobject["length"] = mod.length.ToString();
		}
		if (!mod.stackable)
		{
			jobject["stackable"] = mod.stackable;
		}
		if (!string.IsNullOrEmpty(mod.descriptionKey))
		{
			jobject["description"] = LangaugeManager.main.GetTextByKey(mod.descriptionKey);
		}
		return jobject;
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x0007359C File Offset: 0x0007179C
	public static JObject SerializeAddModifier(Item2.AddModifier addModifier)
	{
		JObject jobject = ItemExporter.SerializeTrigger(addModifier.trigger);
		if (addModifier.areasToModify.Count != 1 || addModifier.areasToModify[0] != Item2.Area.self)
		{
			jobject["addmod_area"] = new JArray(addModifier.areasToModify.Select((Item2.Area area) => area.ToString()));
		}
		if (addModifier.typesToModify.Count != 1 || addModifier.typesToModify[0] != Item2.ItemType.Any)
		{
			jobject["addmod_types"] = new JArray(addModifier.typesToModify.Select((Item2.ItemType type) => type.ToString()));
		}
		if (addModifier.areaDistance != Item2.AreaDistance.all)
		{
			jobject["addmod_distance"] = addModifier.areaDistance.ToString();
		}
		jobject["modifier"] = ItemExporter.SerializeModifier(addModifier.modifier);
		jobject["addmod_length"] = addModifier.lengthForThisModifier.ToString();
		if (!string.IsNullOrEmpty(addModifier.descriptionKey))
		{
			jobject["description"] = LangaugeManager.main.GetTextByKey(addModifier.descriptionKey);
		}
		return jobject;
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x000736F0 File Offset: 0x000718F0
	public static JObject SerializeMovementEffect(Item2.MovementEffect movementEffect)
	{
		JObject jobject = ItemExporter.SerializeTrigger(movementEffect.trigger);
		if (movementEffect.itemsToMove.Count != 1 || movementEffect.itemsToMove[0] != Item2.Area.self)
		{
			jobject["affected_area"] = new JArray(movementEffect.itemsToMove.Select((Item2.Area area) => area.ToString()));
		}
		if (movementEffect.areaDistance != Item2.AreaDistance.all)
		{
			jobject["affected_area_distance"] = movementEffect.areaDistance.ToString();
		}
		if (movementEffect.movementAmount.x != 0f)
		{
			jobject["x"] = movementEffect.movementAmount.x;
		}
		if (movementEffect.movementAmount.y != 0f)
		{
			jobject["y"] = movementEffect.movementAmount.y;
		}
		if (movementEffect.rotationAmount != 0f)
		{
			jobject["rotation"] = movementEffect.rotationAmount / 90f;
		}
		jobject["movement_type"] = movementEffect.movementType.ToString();
		jobject["movement_length"] = movementEffect.movementLength.ToString();
		if (!string.IsNullOrEmpty(movementEffect.descriptionKey))
		{
			jobject["description"] = LangaugeManager.main.GetTextByKey(movementEffect.descriptionKey);
		}
		return jobject;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x0007387C File Offset: 0x00071A7C
	public static JObject SerializeCreateEffect(Item2.CreateEffect createEffect)
	{
		JObject jobject = ItemExporter.SerializeTrigger(createEffect.trigger);
		jobject["create_type"] = createEffect.createType.ToString();
		if (createEffect.areasToCreateTheItem.Count != 1 || createEffect.areasToCreateTheItem[0] != Item2.Area.self)
		{
			jobject["create_areas"] = new JArray(createEffect.areasToCreateTheItem.Select((Item2.Area area) => area.ToString()));
		}
		if (createEffect.areaDistance != Item2.AreaDistance.all)
		{
			jobject["create_distance"] = createEffect.areaDistance.ToString();
		}
		if (createEffect.itemsToCreate.Count > 0)
		{
			JArray jarray = new JArray();
			foreach (GameObject gameObject in createEffect.itemsToCreate)
			{
				string displayName = Item2.GetDisplayName(gameObject.name);
				string internalName = ItemExporter.exportedPackName;
				if (gameObject.GetComponent<ModdedItem>() != null)
				{
					internalName = gameObject.GetComponent<ModdedItem>().fromModpack.internalName;
				}
				jarray.Add(new JObject(new JProperty(internalName, displayName)));
			}
			jobject["create_items"] = jarray;
		}
		if (createEffect.typesToCreate.Count > 0)
		{
			jobject["create_types"] = new JArray(createEffect.typesToCreate.Select((Item2.ItemType type) => type.ToString()));
		}
		if (createEffect.raritiesToCreate.Count > 0)
		{
			jobject["create_rarities"] = new JArray(createEffect.raritiesToCreate.Select((Item2.Rarity rarity) => rarity.ToString()));
		}
		if (createEffect.numberToCreate > 0)
		{
			jobject["number_of_items"] = createEffect.numberToCreate;
		}
		if (!string.IsNullOrEmpty(createEffect.descriptor))
		{
			jobject["description"] = LangaugeManager.main.GetTextByKey(createEffect.descriptor);
		}
		return jobject;
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x00073ABC File Offset: 0x00071CBC
	public static JValue ConsiderIntOrFloat(float value)
	{
		if (Mathf.Floor(value) != value)
		{
			return new JValue(value);
		}
		return new JValue((long)Mathf.FloorToInt(value));
	}

	// Token: 0x04000939 RID: 2361
	public static GameObject currentItemToWorkOn;

	// Token: 0x0400093A RID: 2362
	public static bool hasAlternateUse = false;

	// Token: 0x0400093B RID: 2363
	public static List<Type> supported_components = new List<Type>
	{
		typeof(Transform),
		typeof(BoxCollider2D),
		typeof(Rigidbody2D),
		typeof(Animator),
		typeof(global::AnimationEvent),
		typeof(GridObject),
		typeof(SpriteRenderer),
		typeof(Item2),
		typeof(ItemMovement),
		typeof(ManaStone),
		typeof(Carving),
		typeof(ItemSpriteChanger),
		typeof(ValueChanger)
	};

	// Token: 0x0400093C RID: 2364
	public static string exportedPackName = "BaseGameExport";
}
