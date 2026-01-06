using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class ModItemLoader : MonoBehaviour
{
	// Token: 0x06000B4F RID: 2895 RVA: 0x00073CF4 File Offset: 0x00071EF4
	public GameObject LoadItemFromFile(ModLoader.ModpackInfo modpack, string path)
	{
		GameObject gameObject = null;
		bool flag = false;
		string text = Path.GetDirectoryName(path) + "/";
		string text2;
		try
		{
			text2 = File.ReadAllText(path);
		}
		catch (Exception ex)
		{
			throw new ModUtils.LoadingException(path, ex);
		}
		string text3 = ModUtils.CalculateHash(text2);
		bool flag2 = false;
		foreach (Item2 item in this.modItems)
		{
			ModdedItem component = item.gameObject.GetComponent<ModdedItem>();
			if (component.file == path)
			{
				if (component.hash == text3)
				{
					flag2 = true;
					gameObject = item.gameObject;
					break;
				}
				this.modItems.Remove(item);
				Object.Destroy(item.gameObject);
				flag = true;
				break;
			}
		}
		if (flag2)
		{
			return gameObject;
		}
		JObject jobject;
		try
		{
			jobject = JObject.Parse(text2);
		}
		catch (Exception ex2)
		{
			throw new ModUtils.ParseException(path, ex2);
		}
		gameObject = Object.Instantiate<GameObject>(this.baseItem, this.modItemParent);
		ModdedItem component2 = gameObject.GetComponent<ModdedItem>();
		GameObject gameObject4;
		try
		{
			Item2 component3 = gameObject.GetComponent<Item2>();
			SpriteRenderer component4 = gameObject.GetComponent<SpriteRenderer>();
			string text4 = "";
			string internalName = modpack.internalName;
			try
			{
				if (jobject["name"] == null)
				{
					throw new ModUtils.SyntaxException("Item has no name.");
				}
				text4 = ModLoader.main.AddTextKey(jobject["name"], "", "item name", false, true, null, component2);
				gameObject.name = modpack.internalName + " " + text4 + " (Modded Item)";
				string displayName = Item2.GetDisplayName(gameObject.name);
				ModLoader.main.AddTextKey(jobject["name"], displayName, "item name", flag, false, modpack, component2);
			}
			catch (Exception ex3)
			{
				throw new ModUtils.SyntaxException("Cannot add item name", ex3);
			}
			ModLog.Log(internalName, text4, "Loading item " + text4);
			List<ValueTuple<Rect, bool>> list = new List<ValueTuple<Rect, bool>>();
			List<ValueTuple<Rect, bool>> list2 = new List<ValueTuple<Rect, bool>>();
			try
			{
				List<string> list3 = jobject["shape"].ToObject<List<string>>();
				char c = 'X';
				char c2 = 'A';
				char c3 = 'B';
				int num = 0;
				foreach (string text5 in list3)
				{
					int num2 = 0;
					foreach (char c4 in text5)
					{
						if (c4 == c || c4 == c3)
						{
							list.Add(new ValueTuple<Rect, bool>(new Rect((float)num2, (float)(num * -1), 1f, 1f), false));
						}
						if (c4 == c2 || c4 == c3)
						{
							list.Add(new ValueTuple<Rect, bool>(new Rect((float)num2, (float)(num * -1), 1f, 1f), true));
						}
						num2++;
					}
					num++;
				}
				list2 = list.FindAll(([TupleElementNames(new string[] { "rect", "isIEA" })] ValueTuple<Rect, bool> x) => !x.Item2);
				int num3 = Mathf.RoundToInt(list2.Min(([TupleElementNames(new string[] { "rect", "isIEA" })] ValueTuple<Rect, bool> r) => r.Item1.x));
				int num4 = Mathf.RoundToInt(list2.Min(([TupleElementNames(new string[] { "rect", "isIEA" })] ValueTuple<Rect, bool> r) => r.Item1.y));
				int num5 = Mathf.RoundToInt(list2.Max(([TupleElementNames(new string[] { "rect", "isIEA" })] ValueTuple<Rect, bool> r) => r.Item1.x));
				int num6 = Mathf.RoundToInt(list2.Max(([TupleElementNames(new string[] { "rect", "isIEA" })] ValueTuple<Rect, bool> r) => r.Item1.y));
				float num7 = (float)(num3 + num5) / 2f;
				float num8 = (float)(num4 + num6) / 2f;
				foreach (ValueTuple<Rect, bool> valueTuple in list)
				{
					if (!valueTuple.Item2)
					{
						GameObject gameObject2;
						if (gameObject.GetComponent<BoxCollider2D>() != null)
						{
							gameObject2 = new GameObject("Additional Collider");
							gameObject2.transform.parent = gameObject.transform;
							gameObject2.tag = "Item";
							gameObject2.layer = gameObject.layer;
						}
						else
						{
							gameObject2 = gameObject;
						}
						BoxCollider2D boxCollider2D = gameObject2.AddComponent<BoxCollider2D>();
						boxCollider2D.size = new Vector2(1f, 1f);
						Rect rect = valueTuple.Item1;
						float num9 = rect.x - num7;
						rect = valueTuple.Item1;
						boxCollider2D.offset = new Vector2(num9, rect.y - num8);
					}
					else
					{
						Transform transform = new GameObject("ItemEffectArea")
						{
							transform = 
							{
								parent = gameObject.transform
							},
							tag = "ItemEffectArea",
							layer = gameObject.layer
						}.transform;
						Rect rect = valueTuple.Item1;
						float num10 = rect.x - num7;
						rect = valueTuple.Item1;
						transform.position = new Vector2(num10, rect.y - num8);
					}
				}
			}
			catch (Exception ex4)
			{
				throw new ModUtils.SyntaxException("shape", ex4);
			}
			List<Rect> list4 = new List<Rect>();
			foreach (ValueTuple<Rect, bool> valueTuple2 in list2)
			{
				list4.Add(valueTuple2.Item1);
			}
			int num11 = ModUtils.CastValueOrDefault<int>(jobject["sprite_scale"], 16);
			component2.spriteScale = num11;
			if (ModUtils.IsNullEmpty(jobject["sprite"]))
			{
				component4.sprite = this.CreatePlaceholderSprite(list4, ModUtils.ColorFromTextHash(internalName + text4));
				ModLog.LogWarning(internalName, text4, "No sprite specified, adding placeholder. Please add one!");
			}
			else
			{
				List<Sprite> list5 = new List<Sprite>();
				component4.sprite = null;
				if (jobject["sprite"].Type == JTokenType.Array)
				{
					using (List<string>.Enumerator enumerator2 = jobject["sprite"].ToObject<List<string>>().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string text7 = enumerator2.Current;
							Sprite sprite;
							try
							{
								sprite = ModUtils.LoadNewSprite(text + text7, (float)num11);
							}
							catch (Exception ex5)
							{
								sprite = this.CreatePlaceholderSprite(list4, ModUtils.ColorFromTextHash(internalName + text4));
								string text8 = internalName;
								string text9 = text4;
								string text10 = "Could not load sprite -->";
								Exception ex6 = ex5;
								ModLog.LogError(text8, text9, text10 + ((ex6 != null) ? ex6.ToString() : null));
							}
							if (component4.sprite == null)
							{
								component4.sprite = sprite;
							}
							list5.Add(sprite);
						}
						goto IL_06FE;
					}
				}
				try
				{
					SpriteRenderer spriteRenderer = component4;
					string text11 = text;
					JToken jtoken = jobject["sprite"];
					spriteRenderer.sprite = ModUtils.LoadNewSprite(text11 + ((jtoken != null) ? jtoken.ToString() : null), (float)num11);
				}
				catch (Exception ex7)
				{
					component4.sprite = this.CreatePlaceholderSprite(list4, ModUtils.ColorFromTextHash(internalName + text4));
					string text12 = internalName;
					string text13 = text4;
					string text14 = "Could not load sprite -->";
					Exception ex8 = ex7;
					ModLog.LogError(text12, text13, text14 + ((ex8 != null) ? ex8.ToString() : null));
				}
				IL_06FE:
				if (list5.Count > 1)
				{
					ItemSpriteChanger itemSpriteChanger = gameObject.AddComponent<ItemSpriteChanger>();
					list5.Reverse();
					itemSpriteChanger.sprites = list5;
					itemSpriteChanger.mode = ModUtils.ParseEnumOrDefault<ItemSpriteChanger.SpriteChangerMode>((string)jobject["sprite_change_mode"], ItemSpriteChanger.SpriteChangerMode.Auto);
				}
			}
			try
			{
				if (jobject["flavor"] != null)
				{
					string text15 = Item2.GetDisplayName(gameObject.name) + "2";
					ModLoader.main.AddTextKey(jobject["flavor"], text15, "item flavor text", flag, false, modpack, component2);
				}
			}
			catch (Exception ex9)
			{
				throw new ModUtils.SyntaxException("Cannot add flavor text", ex9);
			}
			try
			{
				component3.rarity = ModUtils.ParseEnumOrDefault<Item2.Rarity>((string)jobject["rarity"], Item2.Rarity.Common);
			}
			catch (Exception ex10)
			{
				throw new ModUtils.SyntaxException("rarity", ex10);
			}
			try
			{
				foreach (JToken jtoken2 in ((IEnumerable<JToken>)jobject["type"]))
				{
					string text16 = (string)jtoken2;
					try
					{
						Item2.ItemType itemType = Enum.Parse<Item2.ItemType>(text16, true);
						component3.itemType.Add(itemType);
					}
					catch (Exception ex11)
					{
						throw new ModUtils.SyntaxException(text16, ex11);
					}
				}
			}
			catch (Exception ex12)
			{
				throw new ModUtils.SyntaxException("type", ex12);
			}
			if (!ModUtils.IsNullEmpty(jobject["group"]))
			{
				foreach (JToken jtoken3 in ((IEnumerable<JToken>)jobject["group"]))
				{
					string text17 = (string)jtoken3;
					try
					{
						Item2.ItemGrouping itemGrouping = Enum.Parse<Item2.ItemGrouping>(text17, true);
						component3.itemGroupings.Add(itemGrouping);
					}
					catch (Exception ex13)
					{
						throw new ModUtils.SyntaxException(text17, ex13);
					}
				}
			}
			bool flag3 = !component3.itemType.Contains(Item2.ItemType.Relic) && !component3.itemType.Contains(Item2.ItemType.Curse) && !component3.itemType.Contains(Item2.ItemType.Blessing) && !component3.itemType.Contains(Item2.ItemType.Hazard) && !component3.itemType.Contains(Item2.ItemType.Pet);
			component3.isStandard = ModUtils.CastValueOrDefault<bool>(jobject["findable"], flag3);
			if (component3.itemType.Contains(Item2.ItemType.Relic))
			{
				int num12 = Random.Range(1, 3);
				component3.spawnGroupings = new List<Item2.SpawnGrouping> { (Item2.SpawnGrouping)num12 };
			}
			try
			{
				if (ModUtils.IsNullEmpty(jobject["supported_characters"]) || jobject["supported_characters"].ToObject<List<string>>().Contains("any", StringComparer.OrdinalIgnoreCase))
				{
					component3.validForCharacters = Enum.GetValues(typeof(Character.CharacterName)).Cast<Character.CharacterName>().ToList<Character.CharacterName>();
				}
				else
				{
					foreach (JToken jtoken4 in ((IEnumerable<JToken>)jobject["supported_characters"]))
					{
						string text18 = (string)jtoken4;
						try
						{
							Character.CharacterName characterName = Enum.Parse<Character.CharacterName>(text18, true);
							component3.validForCharacters.Add(characterName);
						}
						catch (Exception ex14)
						{
							throw new ModUtils.SyntaxException(text18, ex14);
						}
					}
				}
			}
			catch (Exception ex15)
			{
				throw new ModUtils.SyntaxException("character", ex15);
			}
			try
			{
				if (ModUtils.IsNullEmpty(jobject["found_in"]))
				{
					component3.validForZones = Enum.GetValues(typeof(DungeonLevel.Zone)).Cast<DungeonLevel.Zone>().ToList<DungeonLevel.Zone>();
				}
				else
				{
					foreach (JToken jtoken5 in ((IEnumerable<JToken>)jobject["found_in"]))
					{
						string text19 = (string)jtoken5;
						try
						{
							DungeonLevel.Zone zone = Enum.Parse<DungeonLevel.Zone>(text19, true);
							component3.validForZones.Add(zone);
						}
						catch (Exception ex16)
						{
							throw new ModUtils.SyntaxException(text19, ex16);
						}
					}
				}
			}
			catch (Exception ex17)
			{
				throw new ModUtils.SyntaxException("found_in", ex17);
			}
			bool flag4 = true;
			if (!ModUtils.IsNullEmpty(jobject["use_costs"]))
			{
				using (IEnumerator<JToken> enumerator4 = ((IEnumerable<JToken>)jobject["use_costs"]).GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						JToken jtoken6 = enumerator4.Current;
						JProperty jproperty = (JProperty)jtoken6;
						Item2.Cost.Type type = Enum.Parse<Item2.Cost.Type>(jproperty.Name, true);
						Item2.Cost cost = new Item2.Cost();
						cost.type = type;
						cost.baseValue = this.CastValueOrChanger<int>(jproperty.Value, ValueChangerEx.ValueType.Cost, gameObject);
						component3.costs.Add(cost);
					}
					goto IL_0B5E;
				}
			}
			if (jobject["use_costs"] == null)
			{
				flag4 = false;
			}
			IL_0B5E:
			if (!ModUtils.IsNullEmpty(jobject["use_limits"]))
			{
				try
				{
					component3.usesLimits = new List<Item2.LimitedUses>();
					foreach (JToken jtoken7 in ((IEnumerable<JToken>)jobject["use_limits"]))
					{
						JProperty jproperty2 = (JProperty)jtoken7;
						Item2.LimitedUses limitedUses = new Item2.LimitedUses();
						limitedUses.type = Enum.Parse<Item2.LimitedUses.Type>(jproperty2.Name, true);
						limitedUses.value = this.CastValueOrChanger<float>(jproperty2.Value, ValueChangerEx.ValueType.UseLimit, gameObject);
						component3.usesLimits.Add(limitedUses);
					}
				}
				catch (Exception ex18)
				{
					throw new ModUtils.SyntaxException("Cannot add use limits", ex18);
				}
			}
			component3.playerAnimation = ModUtils.ParseEnumOrDefault<Item2.PlayerAnimation>((string)jobject["animation"], Item2.PlayerAnimation.UseItem);
			component3.soundEffect = ModUtils.ParseEnumOrDefault<Item2.SoundEffect>((string)jobject["soundeffect"], Item2.SoundEffect.None);
			component3.playType = ModUtils.ParseEnumOrDefault<Item2.PlayType>((string)jobject["playtype"], Item2.PlayType.Active);
			if (component3.playType == Item2.PlayType.Movable)
			{
				throw new ModUtils.ParseException("PlayType 'Movable' is deprecated.");
			}
			if (!ModUtils.IsNullEmpty(jobject["movable"]))
			{
				JObject jobject2 = (JObject)jobject["movable"];
				component3.moveArea = ModUtils.ParseEnumOrDefault<Item2.Area>((string)jobject2["area"], Item2.Area.self);
				component3.mustBePlacedOnItemType = ModUtils.ParseEnumOrDefault<Item2.ItemType>((string)jobject2["place_on_type"], Item2.ItemType.Grid);
				component3.mustBePlacedOnItemTypeInCombat = ModUtils.ParseEnumOrDefault<Item2.ItemType>((string)jobject2["place_on_type_combat"], Item2.ItemType.Grid);
				component3.moveDistance = ModUtils.ParseEnumOrDefault<Item2.AreaDistance>((string)jobject2["distance"], Item2.AreaDistance.all);
			}
			if (!ModUtils.IsNullEmpty(jobject["combat_effects"]))
			{
				try
				{
					component3.combatEffects = new List<Item2.CombattEffect>();
					foreach (JToken jtoken8 in ((IEnumerable<JToken>)jobject["combat_effects"]))
					{
						JObject jobject3 = (JObject)jtoken8;
						Item2.CombattEffect combattEffect = new Item2.CombattEffect();
						combattEffect.trigger = this.ProcessTrigger(jobject3, component3, modpack, "combat_" + component3.combatEffects.Count.ToString(), true, component2, flag4);
						combattEffect.effect = this.ProcessEffect(jobject3, component3, modpack, "combat_" + component3.combatEffects.Count.ToString(), component2);
						component3.combatEffects.Add(combattEffect);
					}
				}
				catch (Exception ex19)
				{
					throw new ModUtils.SyntaxException("Cannot add combat effects", ex19);
				}
			}
			if (!ModUtils.IsNullEmpty(jobject["create_effects"]))
			{
				try
				{
					component3.createEffects = new List<Item2.CreateEffect>();
					foreach (JToken jtoken9 in ((IEnumerable<JToken>)jobject["create_effects"]))
					{
						JObject jobject4 = (JObject)jtoken9;
						List<string> list6 = new List<string>();
						Item2.CreateEffect createEffect = new Item2.CreateEffect();
						createEffect.trigger = this.ProcessTrigger(jobject4, component3, modpack, "create_" + component3.createEffects.Count.ToString(), true, component2, flag4);
						createEffect.createType = ModUtils.ParseEnumOrDefault<Item2.CreateEffect.CreateType>((string)jobject4["type"], ModUtils.ParseEnumOrDefault<Item2.CreateEffect.CreateType>((string)jobject4["create_type"], Item2.CreateEffect.CreateType.set));
						if (ModUtils.IsNullEmpty(jobject4["create_areas"]))
						{
							createEffect.areasToCreateTheItem = new List<Item2.Area> { Item2.Area.self };
						}
						else
						{
							createEffect.areasToCreateTheItem = new List<Item2.Area>();
							foreach (JToken jtoken10 in ((IEnumerable<JToken>)jobject4["create_areas"]))
							{
								string text20 = (string)jtoken10;
								createEffect.areasToCreateTheItem.Add(Enum.Parse<Item2.Area>(text20, true));
							}
						}
						createEffect.areaDistance = ModUtils.ParseEnumOrDefault<Item2.AreaDistance>((string)jobject4["create_distance"], Item2.AreaDistance.all);
						if (ModUtils.IsNullEmpty(jobject4["create_items"]) && ModUtils.IsNullEmpty(jobject4["create_types"]) && createEffect.createType != Item2.CreateEffect.CreateType.createBlessing && createEffect.createType != Item2.CreateEffect.CreateType.createCurse)
						{
							throw new ModUtils.SyntaxException("Create Effect needs at least one item or type to create");
						}
						createEffect.itemsToCreate = new List<GameObject>();
						createEffect.typesToCreate = new List<Item2.ItemType>();
						createEffect.raritiesToCreate = new List<Item2.Rarity>();
						if (!ModUtils.IsNullEmpty(jobject4["create_items"]))
						{
							foreach (JToken jtoken11 in ((IEnumerable<JToken>)jobject4["create_items"]))
							{
								string text21 = "";
								if (jtoken11.Type == JTokenType.String)
								{
									string text22 = "BPH###";
									JToken jtoken12 = jtoken11;
									text21 = text22 + ((jtoken12 != null) ? jtoken12.ToString() : null);
								}
								else if (jtoken11.Type == JTokenType.Object)
								{
									JObject jobject5 = (JObject)jtoken11;
									if (!jobject5.HasValues)
									{
										continue;
									}
									if (jobject5.Count > 1)
									{
										throw new ModUtils.SyntaxException("MODPACK:ITEM Definition in Create Effect cannot have more than one item inside object. Make seperate objects for each item inside the array.");
									}
									JProperty jproperty3 = (JProperty)((JObject)jtoken11).First;
									if (jobject5[jproperty3.Name].Type != JTokenType.String)
									{
										throw new ModUtils.SyntaxException("MODPACK:ITEM Definition in Create Effect must be of type string");
									}
									string name = jproperty3.Name;
									string text23 = "###";
									JToken jtoken13 = jobject5[jproperty3.Name];
									text21 = name + text23 + ((jtoken13 != null) ? jtoken13.ToString() : null);
								}
								list6.Add(text21);
								GameObject gameObject3;
								this.gameItemLookup.TryGetValue(text21.ToLower(), out gameObject3);
								if (gameObject3 == null)
								{
									ModLog.LogWarning(internalName, text4, "Could not find " + text21 + ", adding Placeholder to be resolved after loading. This is not an Error. ");
									createEffect.itemsToCreate.Add(this.CreatePlaceholder(text21, component3.gameObject));
								}
								else
								{
									createEffect.itemsToCreate.Add(gameObject3);
								}
							}
						}
						ModdedItem.StringList stringList = new ModdedItem.StringList();
						stringList.list = list6;
						component2.createEffectRefs.Add(stringList);
						if (!ModUtils.IsNullEmpty(jobject4["create_types"]))
						{
							foreach (JToken jtoken14 in ((IEnumerable<JToken>)jobject4["create_types"]))
							{
								string text24 = (string)jtoken14;
								createEffect.typesToCreate.Add(Enum.Parse<Item2.ItemType>(text24, true));
							}
						}
						if (!ModUtils.IsNullEmpty(jobject4["create_rarities"]))
						{
							using (IEnumerator<JToken> enumerator5 = ((IEnumerable<JToken>)jobject4["create_rarities"]).GetEnumerator())
							{
								while (enumerator5.MoveNext())
								{
									JToken jtoken15 = enumerator5.Current;
									string text25 = (string)jtoken15;
									createEffect.raritiesToCreate.Add(Enum.Parse<Item2.Rarity>(text25, true));
								}
								goto IL_125A;
							}
							goto IL_123A;
						}
						goto IL_123A;
						IL_125A:
						if (jobject4["number_of_items"] != null)
						{
							createEffect.numberToCreate = this.CastValueOrChangerOrDefault<int>(jobject4["number_of_items"], 1, ValueChangerEx.ValueType.CreateNumOfItems, gameObject);
						}
						if (!ModUtils.IsNullEmpty(jobject4["description"]))
						{
							if (ModUtils.IsTextHidden(jobject4["description"]))
							{
								createEffect.descriptor = "hidden";
							}
							else
							{
								string text26 = "CREATE" + Guid.NewGuid().ToString();
								ModLoader.main.AddTextKey(jobject4["description"], text26, "create effect description", flag, false, modpack, component2);
								createEffect.descriptor = text26;
							}
						}
						component3.createEffects.Add(createEffect);
						continue;
						IL_123A:
						createEffect.raritiesToCreate = Enum.GetValues(typeof(Item2.Rarity)).Cast<Item2.Rarity>().ToList<Item2.Rarity>();
						goto IL_125A;
					}
				}
				catch (Exception ex20)
				{
					throw new ModUtils.SyntaxException("Cannot add create effects", ex20);
				}
			}
			if (!ModUtils.IsNullEmpty(jobject["modifiers"]))
			{
				try
				{
					component3.modifiers = new List<Item2.Modifier>();
					foreach (JToken jtoken16 in ((IEnumerable<JToken>)jobject["modifiers"]))
					{
						JObject jobject6 = (JObject)jtoken16;
						component3.modifiers.Add(this.ProcessModifier(jobject6, component3, modpack, component3.modifiers.Count.ToString(), component2, flag4));
					}
				}
				catch (Exception ex21)
				{
					throw new ModUtils.SyntaxException("Cannot add modifiers", ex21);
				}
			}
			if (!ModUtils.IsNullEmpty(jobject["add_modifiers"]))
			{
				try
				{
					component3.addModifiers = new List<Item2.AddModifier>();
					foreach (JToken jtoken17 in ((IEnumerable<JToken>)jobject["add_modifiers"]))
					{
						JObject jobject7 = (JObject)jtoken17;
						Item2.AddModifier addModifier = new Item2.AddModifier();
						if (ModUtils.IsNullEmpty(jobject7["addmod_area"]))
						{
							addModifier.areasToModify = new List<Item2.Area> { Item2.Area.self };
						}
						else
						{
							addModifier.areasToModify = new List<Item2.Area>();
							foreach (JToken jtoken18 in ((IEnumerable<JToken>)jobject7["addmod_area"]))
							{
								string text27 = (string)jtoken18;
								addModifier.areasToModify.Add(Enum.Parse<Item2.Area>(text27, true));
							}
						}
						addModifier.trigger = this.ProcessTrigger(jobject7, component3, modpack, "addmod_" + component3.addModifiers.Count.ToString(), addModifier.areasToModify.Contains(Item2.Area.self), component2, flag4);
						if (ModUtils.IsNullEmpty(jobject7["addmod_types"]))
						{
							addModifier.typesToModify = new List<Item2.ItemType> { Item2.ItemType.Any };
						}
						else
						{
							addModifier.typesToModify = new List<Item2.ItemType>();
							foreach (JToken jtoken19 in ((IEnumerable<JToken>)jobject7["addmod_types"]))
							{
								string text28 = (string)jtoken19;
								addModifier.typesToModify.Add(Enum.Parse<Item2.ItemType>(text28, true));
							}
						}
						addModifier.areaDistance = ModUtils.ParseEnumOrDefault<Item2.AreaDistance>((string)jobject7["addmod_distance"], Item2.AreaDistance.all);
						if (jobject7["modifier"] == null)
						{
							throw new ModUtils.SyntaxException("This add modifier has no modifier to add");
						}
						addModifier.modifier = this.ProcessModifier(jobject7["modifier"].ToObject<JObject>(), component3, modpack, "addmod_" + component3.addModifiers.Count.ToString(), component2, flag4);
						addModifier.lengthForThisModifier = ModUtils.ParseEnumOrDefault<Item2.Modifier.Length>((string)jobject7["addmod_length"], Item2.Modifier.Length.whileActive);
						if (!ModUtils.IsNullEmpty(jobject7["description"]))
						{
							if (ModUtils.IsTextHidden(jobject7["description"]))
							{
								addModifier.descriptionKey = "hidden";
							}
							else
							{
								addModifier.descriptionKey = "ADDMOD" + Guid.NewGuid().ToString();
								ModLoader.main.AddTextKey(jobject7["description"], addModifier.descriptionKey, "modifier description", false, false, modpack, component2);
							}
						}
						component3.addModifiers.Add(addModifier);
					}
				}
				catch (Exception ex22)
				{
					throw new ModUtils.SyntaxException("Cannot add add_modifiers", ex22);
				}
			}
			if (!ModUtils.IsNullEmpty(jobject["movement_effects"]))
			{
				try
				{
					component3.movementEffects = new List<Item2.MovementEffect>();
					foreach (JToken jtoken20 in ((IEnumerable<JToken>)jobject["movement_effects"]))
					{
						JObject jobject8 = (JObject)jtoken20;
						Item2.MovementEffect movementEffect = new Item2.MovementEffect();
						movementEffect.trigger = this.ProcessTrigger(jobject8, component3, modpack, "move" + component3.movementEffects.Count.ToString(), true, component2, flag4);
						if (ModUtils.IsNullEmpty(jobject8["affected_area"]))
						{
							movementEffect.itemsToMove = new List<Item2.Area> { Item2.Area.self };
						}
						else
						{
							movementEffect.itemsToMove = new List<Item2.Area>();
							foreach (JToken jtoken21 in ((IEnumerable<JToken>)jobject8["affected_area"]))
							{
								string text29 = (string)jtoken21;
								movementEffect.itemsToMove.Add(Enum.Parse<Item2.Area>(text29, true));
							}
						}
						movementEffect.areaDistance = ModUtils.ParseEnumOrDefault<Item2.AreaDistance>((string)jobject8["affected_area_distance"], Item2.AreaDistance.all);
						movementEffect.movementAmount = new Vector2(0f, 0f);
						movementEffect.movementAmount.x = (movementEffect.rotationAmount = (float)this.CastValueOrChangerOrDefault<int>(jobject8["x"], 0, ValueChangerEx.ValueType.MovementX, gameObject));
						movementEffect.movementAmount.y = (movementEffect.rotationAmount = (float)this.CastValueOrChangerOrDefault<int>(jobject8["y"], 0, ValueChangerEx.ValueType.MovementX, gameObject));
						try
						{
							ModUtils.CastValue<int>(jobject8["rotation"]);
							movementEffect.rotationAmount = (float)(ModUtils.CastValueOrDefault<int>(jobject8["rotation"], 0) * 90);
						}
						catch
						{
							movementEffect.rotationAmount = (float)this.CastValueOrChangerOrDefault<int>(jobject8["rotation"], 0, ValueChangerEx.ValueType.MovementRotation, gameObject);
						}
						if (movementEffect.movementAmount.x == 0f && movementEffect.movementAmount.y == 0f && movementEffect.rotationAmount == 0f)
						{
							ModLog.LogWarning(internalName, text4, "This movement effect has no movement and will do nothing!");
						}
						movementEffect.movementType = ModUtils.ParseEnumOrDefault<Item2.MovementEffect.MovementType>((string)jobject8["movement_type"], Item2.MovementEffect.MovementType.global);
						movementEffect.movementLength = ModUtils.ParseEnumOrDefault<Item2.MovementEffect.MovementLength>((string)jobject8["movement_length"], Item2.MovementEffect.MovementLength.oneSpace);
						if (!ModUtils.IsNullEmpty(jobject8["description"]))
						{
							if (ModUtils.IsTextHidden(jobject8["description"]))
							{
								movementEffect.descriptionKey = "hidden";
							}
							else
							{
								movementEffect.descriptionKey = "MOVE" + Guid.NewGuid().ToString();
								ModLoader.main.AddTextKey(jobject8["description"], movementEffect.descriptionKey, "movement description", false, false, modpack, component2);
							}
						}
						component3.movementEffects.Add(movementEffect);
					}
				}
				catch (Exception ex23)
				{
					throw new ModUtils.SyntaxException("Cannot add movement effects", ex23);
				}
			}
			component3.activeItemStatusEffects = this.ProcessItemStatusEffects(jobject["item_status_effects"], component3);
			if (component3.itemType.Contains(Item2.ItemType.Curse) || component3.itemType.Contains(Item2.ItemType.Hazard) || component3.itemType.Contains(Item2.ItemType.Blessing))
			{
				component3.activeItemStatusEffects.Add(new Item2.ItemStatusEffect
				{
					type = Item2.ItemStatusEffect.Type.cantBeSold,
					length = Item2.ItemStatusEffect.Length.permanent,
					num = 1
				});
			}
			if (ModUtils.IsNullEmpty(jobject["manastone"]))
			{
				if (component3.itemType.Contains(Item2.ItemType.ManaStone))
				{
					ModLog.LogError(internalName, text4, "Item has Manastone ItemType, but no manastone settings. Manastone will not work!");
				}
				Object.Destroy(component3.gameObject.GetComponent<ManaStone>());
			}
			else
			{
				if (!component3.itemType.Contains(Item2.ItemType.ManaStone))
				{
					component3.itemType.Add(Item2.ItemType.ManaStone);
				}
				ManaStone component5 = component3.gameObject.GetComponent<ManaStone>();
				component5.maxPower = this.CastValueOrChanger<int>(jobject["manastone"]["max_mana"], ValueChangerEx.ValueType.MaxMana, gameObject);
				component5.startingPower = this.CastValueOrChangerOrDefault<int>(jobject["manastone"]["mana"], this.CastValueOrChangerOrDefault<int>(jobject["manastone"]["starting_mana"], component5.maxPower, ValueChangerEx.ValueType.Mana, gameObject), ValueChangerEx.ValueType.Mana, gameObject);
				component5.currentPower = component5.startingPower;
				if (!ModUtils.IsNullEmpty(jobject["manastone"]["description"]))
				{
					component5.description = "MANA" + Guid.NewGuid().ToString();
					ModLoader.main.AddTextKey(jobject["manastone"]["description"], component5.description, "manastone description", false, false, modpack, component2);
				}
			}
			if (ModUtils.IsNullEmpty(jobject["carving"]))
			{
				if (component3.itemType.Contains(Item2.ItemType.Carving))
				{
					ModLog.LogWarning(internalName, text4, "Item has Carving ItemType, but no carving settings. Removing Carving type.");
					component3.itemType.RemoveAll((Item2.ItemType x) => x == Item2.ItemType.Carving);
				}
				Object.Destroy(component3.gameObject.GetComponent<Carving>());
			}
			else
			{
				component3.validForCharacters = new List<Character.CharacterName> { Character.CharacterName.Tote };
				if (component3.contextMenuOptions == null)
				{
					component3.contextMenuOptions = new List<ContextMenuButton.ContextMenuButtonAndCost>();
				}
				ContextMenuButton.ContextMenuButtonAndCost contextMenuButtonAndCost = new ContextMenuButton.ContextMenuButtonAndCost();
				contextMenuButtonAndCost.type = ContextMenuButton.Type.returnCarving;
				contextMenuButtonAndCost.costs = new List<Item2.Cost>();
				contextMenuButtonAndCost.timeToRemoveCost = ContextMenuButton.ContextMenuButtonAndCost.TimeToRemoveCost.onClick;
				contextMenuButtonAndCost.useTime = ContextMenuButton.ContextMenuButtonAndCost.UseTime.inOrOutOfBattle;
				contextMenuButtonAndCost.playerAnimation = Item2.PlayerAnimation.UseItem;
				ContextMenuButton.ContextMenuButtonAndCost contextMenuButtonAndCost2 = new ContextMenuButton.ContextMenuButtonAndCost();
				contextMenuButtonAndCost2.type = ContextMenuButton.Type.sellCarving;
				contextMenuButtonAndCost2.costs = new List<Item2.Cost>();
				contextMenuButtonAndCost2.timeToRemoveCost = ContextMenuButton.ContextMenuButtonAndCost.TimeToRemoveCost.onClick;
				contextMenuButtonAndCost2.useTime = ContextMenuButton.ContextMenuButtonAndCost.UseTime.inOrOutOfBattle;
				contextMenuButtonAndCost2.playerAnimation = Item2.PlayerAnimation.UseItem;
				gameObject.GetComponent<ItemMovement>().cardPrefab = ModTextGen.main.carvingCardPrefab;
				if (!component3.itemType.Contains(Item2.ItemType.Carving))
				{
					component3.itemType.Add(Item2.ItemType.Carving);
				}
				Carving component6 = component3.gameObject.GetComponent<Carving>();
				component6.summoningCosts = new List<Item2.Cost>();
				if (jobject["carving"]["summon_costs"] != null)
				{
					using (IEnumerator<JToken> enumerator4 = ((IEnumerable<JToken>)jobject["carving"]["summon_costs"]).GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							JToken jtoken22 = enumerator4.Current;
							JProperty jproperty4 = (JProperty)jtoken22;
							Item2.Cost.Type type2 = Enum.Parse<Item2.Cost.Type>(jproperty4.Name, true);
							Item2.Cost cost2 = new Item2.Cost();
							cost2.type = type2;
							cost2.baseValue = this.CastValueOrChanger<int>(jproperty4.Value, ValueChangerEx.ValueType.Cost, gameObject);
							component6.summoningCosts.Add(cost2);
						}
						goto IL_1DF5;
					}
				}
				Item2.Cost cost3 = new Item2.Cost();
				cost3.type = Item2.Cost.Type.energy;
				cost3.baseValue = 0;
				component6.summoningCosts.Add(cost3);
				IL_1DF5:
				if (!ModUtils.IsNullEmpty(jobject["carving"]["sell_costs"]))
				{
					using (IEnumerator<JToken> enumerator4 = ((IEnumerable<JToken>)jobject["carving"]["sell_costs"]).GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							JToken jtoken23 = enumerator4.Current;
							JProperty jproperty5 = (JProperty)jtoken23;
							Item2.Cost.Type type3 = Enum.Parse<Item2.Cost.Type>(jproperty5.Name, true);
							Item2.Cost cost4 = new Item2.Cost();
							cost4.type = type3;
							cost4.baseValue = this.CastValueOrChanger<int>(jproperty5.Value, ValueChangerEx.ValueType.Cost, gameObject);
							contextMenuButtonAndCost2.costs.Add(cost4);
						}
						goto IL_1EC1;
					}
				}
				Item2.Cost cost5 = new Item2.Cost();
				cost5.type = Item2.Cost.Type.gold;
				cost5.baseValue = 5;
				contextMenuButtonAndCost2.costs.Add(cost5);
				IL_1EC1:
				component3.contextMenuOptions.Add(contextMenuButtonAndCost);
				component3.contextMenuOptions.Add(contextMenuButtonAndCost2);
			}
			if (component3.itemType.Contains(Item2.ItemType.Treat))
			{
				gameObject.GetComponent<ItemMovement>().cardPrefab = ModTextGen.main.treatCardPrefab;
			}
			ContextMenuButton.ContextMenuButtonAndCost contextMenuButtonAndCost3 = component3.contextMenuOptions.Find((ContextMenuButton.ContextMenuButtonAndCost x) => x.type == ContextMenuButton.Type.alternateUse);
			if (contextMenuButtonAndCost3 == null)
			{
				contextMenuButtonAndCost3 = component3.contextMenuOptions.Find((ContextMenuButton.ContextMenuButtonAndCost x) => x.type == ContextMenuButton.Type.selectForComboUse);
				if (contextMenuButtonAndCost3 != null && contextMenuButtonAndCost3.type == ContextMenuButton.Type.selectForComboUse)
				{
					contextMenuButtonAndCost3.timeToRemoveCost = ContextMenuButton.ContextMenuButtonAndCost.TimeToRemoveCost.onCombine;
				}
			}
			if (contextMenuButtonAndCost3 != null && !ModUtils.IsNullEmpty(jobject["alternate_use"]))
			{
				contextMenuButtonAndCost3.costs = new List<Item2.Cost>();
				if (!ModUtils.IsNullEmpty(jobject["alternate_use"]["use_costs"]))
				{
					foreach (JToken jtoken24 in ((IEnumerable<JToken>)jobject["alternate_use"]["use_costs"]))
					{
						JProperty jproperty6 = (JProperty)jtoken24;
						Item2.Cost.Type type4 = Enum.Parse<Item2.Cost.Type>(jproperty6.Name, true);
						Item2.Cost cost6 = new Item2.Cost();
						cost6.type = type4;
						cost6.baseValue = this.CastValueOrChanger<int>(jproperty6.Value, ValueChangerEx.ValueType.Cost, gameObject);
						contextMenuButtonAndCost3.costs.Add(cost6);
					}
				}
				contextMenuButtonAndCost3.playerAnimation = ModUtils.ParseEnumOrDefault<Item2.PlayerAnimation>((string)jobject["alternate_use"]["animation"], Item2.PlayerAnimation.UseItem);
				contextMenuButtonAndCost3.useTime = ModUtils.ParseEnumOrDefault<ContextMenuButton.ContextMenuButtonAndCost.UseTime>((string)jobject["alternate_use"]["use_situation"], ContextMenuButton.ContextMenuButtonAndCost.UseTime.inOrOutOfBattle);
			}
			component2.hash = text3;
			component2.id = this.modItems.Count<Item2>();
			component2.file = path;
			component2.fromModpack = modpack;
			component2.thisObj = component2.gameObject;
			this.modItems.Add(component3);
			component2.lookupName = (internalName + "###" + text4).ToLower();
			this.modItemLookup.TryAdd(component2.lookupName, component3);
			gameObject4 = gameObject;
		}
		catch (Exception ex24)
		{
			Object.Destroy(gameObject);
			new StackTrace(1, true);
			new StackTrace(ex24, true);
			Debug.LogException(ex24);
			throw;
		}
		return gameObject4;
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x000762D0 File Offset: 0x000744D0
	public GameObject CreatePlaceholder(string itemStr, GameObject parent)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.placeholderItem, this.modItemParent);
		gameObject.GetComponent<ModdedItem>().placeholder = itemStr.ToLower();
		gameObject.GetComponent<ModdedItem>().thisObj = parent;
		return gameObject;
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x00076300 File Offset: 0x00074500
	public Item2.Trigger ProcessTrigger(JObject triggerJson, Item2 item, ModLoader.ModpackInfo modpack, string prefix, bool self = true, ModdedItem modded = null, bool itemHasCosts = true)
	{
		Item2.Trigger trigger = new Item2.Trigger();
		trigger.trigger = Enum.Parse<Item2.Trigger.ActionTrigger>((string)triggerJson["trigger"], true);
		if (!ModUtils.IsNullEmpty(triggerJson["trigger_on_type"]))
		{
			trigger.types = new List<Item2.ItemType>();
			using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)triggerJson["trigger_on_type"]).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JToken jtoken = enumerator.Current;
					string text = (string)jtoken;
					trigger.types.Add(Enum.Parse<Item2.ItemType>(text, true));
				}
				goto IL_0096;
			}
		}
		trigger.types = new List<Item2.ItemType> { Item2.ItemType.Any };
		IL_0096:
		trigger.areas = new List<Item2.Area>();
		if (!ModUtils.IsNullEmpty(triggerJson["trigger_area"]))
		{
			using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)triggerJson["trigger_area"]).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JToken jtoken2 = enumerator.Current;
					string text2 = (string)jtoken2;
					trigger.areas.Add(Enum.Parse<Item2.Area>(text2, true));
				}
				goto IL_0104;
			}
		}
		trigger.areas.Add(Item2.Area.self);
		IL_0104:
		if (self && trigger.trigger == Item2.Trigger.ActionTrigger.onUse && !itemHasCosts && (trigger.areas.Contains(Item2.Area.self) || trigger.areas.Count == 0))
		{
			Item2.Cost cost = new Item2.Cost();
			cost.type = Item2.Cost.Type.energy;
			cost.baseValue = 0;
			item.costs.Add(cost);
		}
		if (trigger.trigger == Item2.Trigger.ActionTrigger.onAlternateUse || trigger.trigger == Item2.Trigger.ActionTrigger.onComboUse)
		{
			Dictionary<Item2.Trigger.ActionTrigger, ContextMenuButton.Type> dictionary = new Dictionary<Item2.Trigger.ActionTrigger, ContextMenuButton.Type>
			{
				{
					Item2.Trigger.ActionTrigger.onAlternateUse,
					ContextMenuButton.Type.alternateUse
				},
				{
					Item2.Trigger.ActionTrigger.onComboUse,
					ContextMenuButton.Type.selectForComboUse
				}
			};
			ContextMenuButton.ContextMenuButtonAndCost contextMenuButtonAndCost = item.contextMenuOptions.Find((ContextMenuButton.ContextMenuButtonAndCost x) => x.type == ContextMenuButton.Type.alternateUse || x.type == ContextMenuButton.Type.selectForComboUse);
			if (contextMenuButtonAndCost != null)
			{
				if (trigger.trigger == Item2.Trigger.ActionTrigger.onComboUse)
				{
					contextMenuButtonAndCost.type = ContextMenuButton.Type.selectForComboUse;
				}
			}
			else
			{
				contextMenuButtonAndCost = new ContextMenuButton.ContextMenuButtonAndCost();
				contextMenuButtonAndCost.type = dictionary[trigger.trigger];
				item.contextMenuOptions.Add(contextMenuButtonAndCost);
			}
		}
		trigger.areaDistance = ModUtils.ParseEnumOrDefault<Item2.AreaDistance>((string)triggerJson["trigger_distance"], Item2.AreaDistance.all);
		trigger.requiresActivation = ModUtils.CastValueOrDefault<bool>((bool?)triggerJson["needs_activation"], false);
		if (!ModUtils.IsNullEmpty(triggerJson["trigger_description"]))
		{
			trigger.triggerOverrideKey = "TRIGGER" + Guid.NewGuid().ToString();
			ModLoader.main.AddTextKey(triggerJson["trigger_description"], trigger.triggerOverrideKey, "trigger description", false, false, modpack, modded);
		}
		return trigger;
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x000765BC File Offset: 0x000747BC
	public Item2.Effect ProcessEffect(JObject effectJson, Item2 item, ModLoader.ModpackInfo modpack, string prefix, ModdedItem modded = null)
	{
		Item2.Effect effect = new Item2.Effect();
		effect.type = Enum.Parse<Item2.Effect.Type>((string)effectJson["type"], true);
		effect.value = this.CastValueOrChangerOrDefault<float>(effectJson["value"], 0f, ValueChangerEx.ValueType.EffectValue, item.gameObject);
		effect.target = ModUtils.ParseEnumOrDefault<Item2.Effect.Target>((string)effectJson["target"], Item2.Effect.Target.unspecified);
		Item2.Effect effect2 = effect;
		string text = (string)effectJson["math"];
		Item2.Effect.MathematicalType mathematicalType;
		if (!(text == "mul"))
		{
			if (!(text == "sum") && (text == null || text.Length != 0) && text != null)
			{
				throw new ModUtils.ParseException("Invalid mathematical type.");
			}
			mathematicalType = Item2.Effect.MathematicalType.summative;
		}
		else
		{
			mathematicalType = Item2.Effect.MathematicalType.multiplicative;
		}
		effect2.mathematicalType = mathematicalType;
		effect.itemStatusEffect = this.ProcessItemStatusEffects(effectJson["item_status_effects"], item);
		if (!ModUtils.IsNullEmpty(effectJson["description"]))
		{
			if (ModUtils.IsTextHidden(effectJson["description"]))
			{
				effect.effectOverrideKey = "hidden";
			}
			else
			{
				effect.effectOverrideKey = "EFFECT" + Guid.NewGuid().ToString();
				ModLoader.main.AddTextKey(effectJson["description"], effect.effectOverrideKey, "effect description", false, false, modpack, modded);
			}
		}
		return effect;
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x0007671C File Offset: 0x0007491C
	public List<Item2.ItemStatusEffect> ProcessItemStatusEffects(JToken effectJsonArray, Item2 item)
	{
		if (effectJsonArray == null)
		{
			return new List<Item2.ItemStatusEffect>();
		}
		List<Item2.ItemStatusEffect> list = new List<Item2.ItemStatusEffect>();
		try
		{
			foreach (JToken jtoken in ((IEnumerable<JToken>)effectJsonArray))
			{
				JObject jobject = (JObject)jtoken;
				list.Add(new Item2.ItemStatusEffect
				{
					applyRightAway = ModUtils.CastValueOrDefault<bool>((bool?)jobject["apply_immediately"], false),
					type = Enum.Parse<Item2.ItemStatusEffect.Type>((string)jobject["type"], true),
					num = this.CastValueOrChangerOrDefault<int>(jobject["value"], 0, ValueChangerEx.ValueType.ItemStatusEffectValue, item.gameObject),
					length = ModUtils.ParseEnumOrDefault<Item2.ItemStatusEffect.Length>((string)jobject["length"], Item2.ItemStatusEffect.Length.permanent)
				});
			}
		}
		catch (Exception ex)
		{
			throw new ModUtils.SyntaxException("Error parsing item status effect", ex);
		}
		return list;
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00076824 File Offset: 0x00074A24
	public Item2.Modifier ProcessModifier(JObject modJson, Item2 item, ModLoader.ModpackInfo modpack, string prefix, ModdedItem modded = null, bool itemHasCosts = true)
	{
		Item2.Modifier modifier = new Item2.Modifier();
		if (ModUtils.IsNullEmpty(modJson["mod_area"]))
		{
			modifier.areasToModify = new List<Item2.Area> { Item2.Area.self };
		}
		else
		{
			modifier.areasToModify = new List<Item2.Area>();
			foreach (JToken jtoken in ((IEnumerable<JToken>)modJson["mod_area"]))
			{
				string text = (string)jtoken;
				modifier.areasToModify.Add(Enum.Parse<Item2.Area>(text, true));
			}
		}
		modifier.trigger = this.ProcessTrigger(modJson, item, modpack, prefix + "_mod", modifier.areasToModify.Contains(Item2.Area.self), modded, itemHasCosts);
		if (ModUtils.IsNullEmpty(modJson["effects"]))
		{
			throw new ModUtils.SyntaxException("Modifier needs at least one effect");
		}
		modifier.effects = new List<Item2.Effect>();
		foreach (JToken jtoken2 in ((IEnumerable<JToken>)modJson["effects"]))
		{
			JObject jobject = (JObject)jtoken2;
			modifier.effects.Add(this.ProcessEffect(jobject, item, modpack, prefix + "_mod", modded));
		}
		if (ModUtils.IsNullEmpty(modJson["mod_types"]))
		{
			modifier.typesToModify = new List<Item2.ItemType> { Item2.ItemType.Any };
		}
		else
		{
			modifier.typesToModify = new List<Item2.ItemType>();
			foreach (JToken jtoken3 in ((IEnumerable<JToken>)modJson["mod_types"]))
			{
				string text2 = (string)jtoken3;
				modifier.typesToModify.Add(Enum.Parse<Item2.ItemType>(text2, true));
			}
		}
		modifier.areaDistance = ModUtils.ParseEnumOrDefault<Item2.AreaDistance>((string)modJson["mod_distance"], Item2.AreaDistance.all);
		modifier.length = ModUtils.ParseEnumOrDefault<Item2.Modifier.Length>((string)modJson["length"], Item2.Modifier.Length.whileActive);
		modifier.stackable = ModUtils.CastValueOrDefault<bool>((bool?)modJson["stackable"], true);
		if (!ModUtils.IsNullEmpty(modJson["description"]))
		{
			if (ModUtils.IsTextHidden(modJson["description"]))
			{
				modifier.descriptionKey = "hidden";
			}
			else
			{
				modifier.descriptionKey = "MOD" + Guid.NewGuid().ToString();
				ModLoader.main.AddTextKey(modJson["description"], modifier.descriptionKey, "modifier description", false, false, modpack, modded);
			}
		}
		return modifier;
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x00076AD0 File Offset: 0x00074CD0
	public void ResolvePlaceholders(Item2 item)
	{
		if (item.createEffects != null)
		{
			foreach (Item2.CreateEffect createEffect in item.createEffects)
			{
				if (createEffect.itemsToCreate != null)
				{
					this.ResolvePlaceholders(ref createEffect.itemsToCreate);
				}
			}
		}
		foreach (ValueChangerEx valueChangerEx in item.gameObject.GetComponentsInChildren<ValueChangerEx>())
		{
			if (valueChangerEx.itemPrefabs != null)
			{
				this.ResolvePlaceholders(ref valueChangerEx.itemPrefabs);
			}
		}
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00076B70 File Offset: 0x00074D70
	public void ResolvePlaceholders(ref List<GameObject> itemList)
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			ModdedItem component = itemList[i].GetComponent<ModdedItem>();
			if (component != null && component.placeholder != null && component.placeholder != "")
			{
				bool flag = false;
				Item2 item = null;
				if (component.placeholder.ToLower().StartsWith("bph###"))
				{
					string text = component.placeholder.ToLower().Substring(3);
					flag = this.modItemLookup.TryGetValue((component.fromModpack.internalName + text).ToLower(), out item);
					using (List<ModLoader.ModpackInfo>.Enumerator enumerator = ModLoader.main.modpacks.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ModLoader.ModpackInfo modpackInfo = enumerator.Current;
							if (!flag)
							{
								flag = this.modItemLookup.TryGetValue((modpackInfo.internalName + text).ToLower(), out item);
							}
						}
						goto IL_0109;
					}
					goto IL_00F0;
				}
				goto IL_00F0;
				IL_0109:
				if (!flag)
				{
					throw new ModUtils.ParseException(component.thisObj.name + " will not work! " + component.placeholder.Split("###", StringSplitOptions.None)[1] + " is not a valid item");
				}
				ModLog.Log(component.fromModpack.internalName, component.thisObj.name, "Resolved " + component.thisObj.name + " placeholder: " + item.name);
				Object.Destroy(itemList[i]);
				itemList[i] = item.gameObject;
				goto IL_0195;
				IL_00F0:
				flag = this.modItemLookup.TryGetValue(component.placeholder.ToLower(), out item);
				goto IL_0109;
			}
			IL_0195:;
		}
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00076D34 File Offset: 0x00074F34
	public void ResolveCreateEffectDescriptions(bool reload = false)
	{
		foreach (Item2 item in this.modItems)
		{
			ModTextGen.GenerateCreateEffectDescriptions(item, reload);
		}
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00076D88 File Offset: 0x00074F88
	public void UnloadItem(GameObject obj)
	{
		Item2 component = obj.GetComponent<Item2>();
		ModdedItem component2 = obj.GetComponent<ModdedItem>();
		this.modItems.Remove(component);
		this.modItemLookup.Remove(component2.lookupName);
		Object.Destroy(obj);
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00076DC8 File Offset: 0x00074FC8
	public void ReplaceSpawnedItems()
	{
		if (this.itemParent == null)
		{
			return;
		}
		int num = 0;
		List<GameObject> list = new List<GameObject>();
		using (IEnumerator enumerator = this.itemParent.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Transform child = (Transform)enumerator.Current;
				if (!list.Contains(child.gameObject))
				{
					ModdedItem modded;
					if (child.gameObject.TryGetComponent<ModdedItem>(out modded))
					{
						Item2 item = this.modItems.Find((Item2 r) => r.gameObject.name == child.gameObject.name);
						if (item == null)
						{
							item = this.modItems.Find((Item2 r) => r.GetComponent<ModdedItem>().file == modded.file);
						}
						if (item == null)
						{
							item = this.modItems.Find((Item2 r) => r.GetComponent<ModdedItem>().id == modded.id);
						}
						if (item == null)
						{
							item = this.modItems.Find((Item2 r) => r.GetComponent<ModdedItem>().lookupName.Split("###", StringSplitOptions.None)[1] == modded.lookupName.Split("###", StringSplitOptions.None)[1]);
						}
						if (item != null)
						{
							GameObject gameObject = Object.Instantiate<GameObject>(item.gameObject, child.position, child.rotation, this.itemParent);
							list.Add(gameObject);
							gameObject.SetActive(child.gameObject.activeSelf);
							if (Tote.main != null)
							{
								Tote.main.ReplaceInLists(child.gameObject, gameObject);
							}
							foreach (object obj in this.itemParent)
							{
								ItemPouch component = ((Transform)obj).gameObject.GetComponent<ItemPouch>();
								if (component != null)
								{
									component.ReplaceInList(child.gameObject, gameObject, ref component.itemsInside);
								}
							}
							if (child.gameObject.GetComponent<ItemMovement>().inGrid)
							{
								gameObject.GetComponent<ItemMovement>().AddToGrid(false);
							}
							Object.Destroy(child.gameObject);
						}
						else
						{
							ModLog.LogWarning(modded.fromModpack.internalName, "itemReload", "Could not find " + child.gameObject.name);
						}
					}
					num++;
					if (num > 500)
					{
						ModLog.LogError("Too many items active!");
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00077090 File Offset: 0x00075290
	public Sprite CreatePlaceholderSprite(List<Rect> rects, Color blockColor)
	{
		int num = Mathf.RoundToInt(rects.Min((Rect r) => r.x));
		int num2 = Mathf.RoundToInt(rects.Min((Rect r) => r.y));
		int num3 = Mathf.RoundToInt(rects.Max((Rect r) => r.x));
		int num4 = Mathf.RoundToInt(rects.Max((Rect r) => r.y));
		for (int i = 0; i < rects.Count; i++)
		{
			Rect rect = rects[i];
			rect.x -= (float)num;
			rect.y -= (float)num2;
			rects[i] = rect;
		}
		num3 -= num;
		num4 -= num2;
		num3++;
		num4++;
		Texture2D texture2D = new Texture2D(num3 * 16, num4 * 16, TextureFormat.RGBA32, false);
		texture2D.filterMode = FilterMode.Point;
		Color[] array = Enumerable.Repeat<Color>(new Color(0f, 0f, 0f, 0f), texture2D.width * texture2D.height).ToArray<Color>();
		texture2D.SetPixels(array);
		texture2D.Apply();
		for (int j = 0; j < num4; j++)
		{
			for (int k = 0; k < num3; k++)
			{
				bool flag = false;
				foreach (Rect rect2 in rects)
				{
					flag = rect2.Contains(new Vector2((float)k, (float)j));
					if (flag)
					{
						break;
					}
				}
				if (flag)
				{
					ModUtils.CopyTexture(this.itemPlayerholderTile, texture2D, k * 16, j * 16);
				}
			}
		}
		ModUtils.ReplaceColorInTexture(texture2D, Color.white, blockColor);
		return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 16f);
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x000772E8 File Offset: 0x000754E8
	public T CastValueOrChangerOrDefault<T>(object value, object defaultValue, ValueChangerEx.ValueType type, GameObject item)
	{
		if (value is JObject)
		{
			return this.CastValueOrChanger<T>(value, type, item);
		}
		return ModUtils.CastValueOrDefault<T>(value, defaultValue);
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x00077304 File Offset: 0x00075504
	public T CastValueOrChanger<T>(object value, ValueChangerEx.ValueType type, GameObject item)
	{
		if (value is JObject)
		{
			JObject jobject = value as JObject;
			int num = -1337000 - item.GetComponentsInChildren<ValueChangerEx>().Count<ValueChangerEx>();
			ValueChangerEx valueChangerEx = item.AddComponent<ValueChangerEx>();
			valueChangerEx.valueToReplace = (float)num;
			valueChangerEx.replaceWithValue = Enum.Parse<ValueChangerEx.ReplaceWithValue>((string)jobject["replace_with"], true);
			valueChangerEx.baseValue = this.CastValueOrChangerOrDefault<float>(jobject["base_value"], 0f, ValueChangerEx.ValueType.ValueChangerBase, item);
			valueChangerEx.multiplier = this.CastValueOrChangerOrDefault<float>(jobject["multiplier"], 1f, ValueChangerEx.ValueType.ValueChangerMultiplier, item);
			valueChangerEx.itemPrefabs = new List<GameObject>();
			List<string> list = new List<string>();
			if (jobject["check_items"] != null)
			{
				foreach (JToken jtoken in ((IEnumerable<JToken>)jobject["check_items"]))
				{
					string text = "";
					if (jtoken.Type == JTokenType.String)
					{
						string text2 = "BPH###";
						JToken jtoken2 = jtoken;
						text = text2 + ((jtoken2 != null) ? jtoken2.ToString() : null);
					}
					else if (jtoken.Type == JTokenType.Object)
					{
						JObject jobject2 = (JObject)jtoken;
						if (!jobject2.HasValues)
						{
							continue;
						}
						if (jobject2.Count > 1)
						{
							throw new ModUtils.SyntaxException("MODPACK:ITEM Definition in Create Effect cannot have more than one item inside object. Make seperate objects for each item inside the array.");
						}
						JProperty jproperty = (JProperty)((JObject)jtoken).First;
						if (jobject2[jproperty.Name].Type != JTokenType.String)
						{
							throw new ModUtils.SyntaxException("MODPACK:ITEM Definition in Create Effect must be of type string");
						}
						string name = jproperty.Name;
						string text3 = "###";
						JToken jtoken3 = jobject2[jproperty.Name];
						text = name + text3 + ((jtoken3 != null) ? jtoken3.ToString() : null);
					}
					list.Add(text);
					GameObject gameObject;
					this.gameItemLookup.TryGetValue(text.ToLower(), out gameObject);
					if (gameObject == null)
					{
						ModLog.LogWarning(item.GetComponent<ModdedItem>().fromModpack.internalName, item.name, "Could not find " + text + ", adding Placeholder to be resolved after loading. This is not an Error. ");
						valueChangerEx.itemPrefabs.Add(this.CreatePlaceholder(text, item.gameObject));
						break;
					}
					valueChangerEx.itemPrefabs.Add(gameObject);
				}
			}
			ModdedItem.StringList stringList = new ModdedItem.StringList();
			stringList.list = list;
			item.GetComponent<ModdedItem>().valueChangerRefs.Add(stringList);
			valueChangerEx.areas = new List<Item2.Area>();
			if (!ModUtils.IsNullEmpty(jobject["check_area"]))
			{
				using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)jobject["check_area"]).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken jtoken4 = enumerator.Current;
						string text4 = (string)jtoken4;
						valueChangerEx.areas.Add(Enum.Parse<Item2.Area>(text4, true));
					}
					goto IL_02CC;
				}
			}
			valueChangerEx.areas.Add(Item2.Area.board);
			IL_02CC:
			valueChangerEx.types = new List<Item2.ItemType>();
			if (!ModUtils.IsNullEmpty(jobject["check_types"]))
			{
				valueChangerEx.types = new List<Item2.ItemType>();
				using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)jobject["check_types"]).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken jtoken5 = enumerator.Current;
						string text5 = (string)jtoken5;
						valueChangerEx.types.Add(Enum.Parse<Item2.ItemType>(text5, true));
					}
					goto IL_034C;
				}
			}
			valueChangerEx.types.Add(Item2.ItemType.Any);
			IL_034C:
			valueChangerEx.areaDistance = ModUtils.ParseEnumOrDefault<Item2.AreaDistance>((string)jobject["area_distance"], Item2.AreaDistance.all);
			return ModUtils.CastValue<T>(num);
		}
		return ModUtils.CastValue<T>(value);
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x000776D8 File Offset: 0x000758D8
	private void Start()
	{
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x000776DC File Offset: 0x000758DC
	private void Awake()
	{
		if (ModItemLoader.main != null && ModItemLoader.main != this)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			ModItemLoader.main = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		this.gameItemLookup = new Dictionary<string, GameObject>();
		foreach (GameObject gameObject in DebugItemManager.main.items)
		{
			this.gameItemLookup.TryAdd(("BPH###" + Item2.GetDisplayName(gameObject.name)).ToLower(), gameObject);
		}
		ModLog.Log("ItemLoader active");
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x000777A0 File Offset: 0x000759A0
	private void Update()
	{
		if (this.itemParent == null)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("ItemParent");
			if (gameObject == null)
			{
				return;
			}
			this.itemParent = gameObject.GetComponent<Transform>();
		}
	}

	// Token: 0x0400093D RID: 2365
	public static ModItemLoader main;

	// Token: 0x0400093E RID: 2366
	public Dictionary<string, GameObject> gameItemLookup;

	// Token: 0x0400093F RID: 2367
	public Dictionary<string, Item2> modItemLookup = new Dictionary<string, Item2>();

	// Token: 0x04000940 RID: 2368
	public List<Item2> modItems;

	// Token: 0x04000941 RID: 2369
	[SerializeField]
	private GameObject baseItem;

	// Token: 0x04000942 RID: 2370
	[SerializeField]
	private GameObject placeholderItem;

	// Token: 0x04000943 RID: 2371
	[SerializeField]
	private Transform modItemParent;

	// Token: 0x04000944 RID: 2372
	[SerializeField]
	private Transform itemParent;

	// Token: 0x04000945 RID: 2373
	[SerializeField]
	private Texture2D itemPlayerholderTile;

	// Token: 0x04000946 RID: 2374
	[SerializeField]
	private Material carvingShader;

	// Token: 0x04000947 RID: 2375
	[SerializeField]
	private Material treatShader;
}
