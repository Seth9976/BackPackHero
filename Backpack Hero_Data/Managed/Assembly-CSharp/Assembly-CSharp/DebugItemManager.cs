using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x020000FD RID: 253
[ExecuteInEditMode]
public class DebugItemManager : MonoBehaviour
{
	// Token: 0x060008C4 RID: 2244 RVA: 0x0005C246 File Offset: 0x0005A446
	private void Awake()
	{
		if (DebugItemManager.main == null)
		{
			DebugItemManager.main = this;
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0005C25B File Offset: 0x0005A45B
	private void OnDestroy()
	{
		if (DebugItemManager.main == this)
		{
			DebugItemManager.main = null;
		}
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0005C270 File Offset: 0x0005A470
	public void FindResearch(Item2 item, out List<Overworld_BuildingInterface.Research> researchesSpecific, out List<Overworld_BuildingInterface.Research> researchesGeneral)
	{
		this.itemAtlas.FindResearch(item, out researchesSpecific, out researchesGeneral);
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0005C280 File Offset: 0x0005A480
	private void Start()
	{
		this.exportItemJSON = false;
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0005C28C File Offset: 0x0005A48C
	private void Update()
	{
		if (this.getItems)
		{
			this.getItems = false;
			this.items = this.LoadTo(this.items, "Items");
			this.GetItem2s();
			this.enemies = this.LoadTo(this.enemies, "Enemies");
			this.SetEnemyStats();
		}
		if (this.performAction)
		{
			this.performAction = false;
			this.PerformAction();
		}
		if (this.checkUnlocks)
		{
			this.checkUnlocks = false;
			this.CheckUnlocks();
		}
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0005C30C File Offset: 0x0005A50C
	private void SetEnemyStats()
	{
		foreach (GameObject gameObject in this.enemies)
		{
			Enemy component = gameObject.GetComponent<Enemy>();
			if (component)
			{
				component.SetStatsHealth();
			}
		}
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0005C36C File Offset: 0x0005A56C
	public void GetItem2s()
	{
		this.item2s = new List<Item2>();
		foreach (GameObject gameObject in this.items)
		{
			Item2 component = gameObject.GetComponent<Item2>();
			if (component)
			{
				ItemSwitcher component2 = gameObject.GetComponent<ItemSwitcher>();
				if (component2)
				{
					foreach (Item2 item in component2.GetItem2s())
					{
						item.StartProperties();
						item.displayName = Item2.GetDisplayName(component.name);
					}
				}
				component.StartProperties();
				this.item2s.Add(component);
				component.displayName = Item2.GetDisplayName(component.name);
			}
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0005C45C File Offset: 0x0005A65C
	private void CheckUnlocks()
	{
		foreach (MetaProgressSaveManager.Research research in this.researchList.researchList)
		{
			int num = 0;
			foreach (GameObject gameObject in research.objsToUnlock)
			{
				Item2 component = gameObject.GetComponent<Item2>();
				if (component)
				{
					component.storyModeAvailabilityType = Item2.AvailabilityType.UnlockDependent;
					if (component.rarity == Item2.Rarity.Common)
					{
						num += 30;
					}
					else if (component.rarity == Item2.Rarity.Uncommon)
					{
						num += 50;
					}
					else if (component.rarity == Item2.Rarity.Rare)
					{
						num += 80;
					}
					else if (component.rarity == Item2.Rarity.Legendary)
					{
						num += 150;
					}
				}
			}
			Overworld_ResourceManager.Resource resource = new Overworld_ResourceManager.Resource();
			if (research.item.GetComponent<Item2>().rarity == Item2.Rarity.Common)
			{
				num /= 3;
			}
			resource.amount = num;
			List<Overworld_ResourceManager.Resource.Type> list = new List<Overworld_ResourceManager.Resource.Type>
			{
				Overworld_ResourceManager.Resource.Type.BuildingMaterial,
				Overworld_ResourceManager.Resource.Type.Food,
				Overworld_ResourceManager.Resource.Type.Treasure
			};
			resource.type = list[Random.Range(0, list.Count)];
			research.resources = new List<Overworld_ResourceManager.Resource> { resource };
		}
		foreach (Item2 item in this.item2s)
		{
			if (item.itemType.Contains(Item2.ItemType.Magic) || item.itemType.Contains(Item2.ItemType.ManaStone))
			{
				if (item.storyModeAvailabilityType == Item2.AvailabilityType.Always)
				{
					item.storyModeAvailabilityType = Item2.AvailabilityType.MarkerDependent;
				}
				if (item.conditions.Count == 0)
				{
					MetaProgressSaveManager.AddBoolCondition(item.conditions, MetaProgressSaveManager.MetaProgressMarker.unlockedMagic);
				}
			}
			if (item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombat))
			{
				if (item.storyModeAvailabilityType == Item2.AvailabilityType.Always)
				{
					item.storyModeAvailabilityType = Item2.AvailabilityType.MarkerDependent;
				}
				if (item.conditions.Count == 0)
				{
					MetaProgressSaveManager.AddBoolCondition(item.conditions, MetaProgressSaveManager.MetaProgressMarker.firstRunStartedFromOverworld);
				}
			}
			if (item.itemType.Contains(Item2.ItemType.Arrow) || item.itemType.Contains(Item2.ItemType.Bow))
			{
				if (item.storyModeAvailabilityType == Item2.AvailabilityType.Always)
				{
					item.storyModeAvailabilityType = Item2.AvailabilityType.MarkerDependent;
				}
				if (item.conditions.Count == 0)
				{
					MetaProgressSaveManager.AddBoolCondition(item.conditions, MetaProgressSaveManager.MetaProgressMarker.unlockedArchery);
				}
			}
		}
		foreach (Item2 item2 in this.item2s)
		{
			if (item2.storyModeAvailabilityType == Item2.AvailabilityType.UnlockDependent)
			{
				bool flag = false;
				foreach (MetaProgressSaveManager.Research research2 in this.researchList.researchList)
				{
					using (List<GameObject>.Enumerator enumerator2 = research2.objsToUnlock.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current == item2.gameObject)
							{
								flag = true;
								break;
							}
						}
					}
				}
				if (!flag)
				{
					Debug.Log(item2.name + " is unlock dependent but not found in any research");
				}
			}
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0005C80C File Offset: 0x0005AA0C
	private void PerformAction()
	{
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0005C810 File Offset: 0x0005AA10
	private List<GameObject> LoadTo(List<GameObject> list, string folder)
	{
		list = new List<GameObject>();
		foreach (GameObject gameObject in Resources.LoadAll(folder))
		{
			if (gameObject)
			{
				list.Add(gameObject);
			}
		}
		list = list.OrderBy((GameObject go) => go.name).ToList<GameObject>();
		return list;
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0005C87E File Offset: 0x0005AA7E
	public IEnumerator ExportItemCards()
	{
		List<string> chList = Enum.GetNames(typeof(Character.CharacterName)).ToList<string>();
		chList.Remove(Character.CharacterName.Any.ToString());
		Debug.Log("Getting Item2s");
		string dir = "jsonExport/";
		Directory.CreateDirectory(dir + "/sprites/");
		int i = 0;
		this.GetItem2s();
		Debug.Log("Item Card Export Start");
		JArray itemArray = new JArray();
		foreach (Item2 item in this.item2s)
		{
			if (!this.itemsToSkip.Contains(item.gameObject))
			{
				JObject itemObj = new JObject();
				string textByKey = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(item.name));
				itemObj.Add("name", new JValue(new CultureInfo("en-US", false).TextInfo.ToTitleCase(textByKey.ToLower())));
				Sprite sprite = item.gameObject.GetComponent<SpriteRenderer>().sprite;
				IEnumerable<Color> pixels = SpriteExporter.duplicateTexture(sprite.texture).GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height);
				int num = 8;
				List<Color> list = new List<Color>();
				int b = 0;
				foreach (Color[] array2 in (from g in pixels.GroupBy(delegate(Color s)
					{
						int b2 = b;
						b = b2 + 1;
						return b2 / (int)sprite.rect.width;
					})
					select g.ToArray<Color>()).ToArray<Color[]>())
				{
					for (int k = 0; k < num; k++)
					{
						foreach (Color color in array2)
						{
							for (int m = 0; m < num; m++)
							{
								list.Add(color);
							}
						}
					}
				}
				Texture2D texture2D = new Texture2D((int)sprite.rect.width * num, (int)sprite.rect.height * num);
				texture2D.SetPixels(list.ToArray());
				byte[] array4 = texture2D.EncodeToPNG();
				string text = string.Concat(new string[]
				{
					"sprites/",
					itemObj["name"].ToString(),
					"_",
					i.ToString(),
					".png"
				});
				File.WriteAllBytes(dir + text, array4);
				itemObj.Add("sprite", new JValue(text));
				itemObj.Add("shape", ItemExporter.SerializeShape(item));
				if (item.validForCharacters.Count == 0 || item.validForCharacters.Contains(Character.CharacterName.Any))
				{
					itemObj.Add("validChar", new JArray(chList));
				}
				else
				{
					itemObj.Add("validChar", new JArray(item.validForCharacters.Select((Character.CharacterName t) => t.ToString()).ToList<string>()));
				}
				itemObj.Add("rarity", new JValue(item.rarity.ToString()));
				itemObj.Add("types", new JArray(item.itemType.Select(delegate(Item2.ItemType c)
				{
					Item2.ItemType itemType = c;
					return new JValue(itemType.ToString());
				})));
				itemObj.Add("useCost", new JObject(from c in Item2.GetCosts(item.costs)
					select new JProperty(c.type.ToString(), c.baseValue)));
				itemObj.Add("findable", new JValue(item.isStandard));
				Carving component = item.gameObject.GetComponent<Carving>();
				PetItem2 component2 = item.gameObject.GetComponent<PetItem2>();
				if (component)
				{
					itemObj.Add("summonCost", new JObject(from c in Item2.GetCosts(component.summoningCosts)
						select new JProperty(c.type.ToString(), c.baseValue)));
				}
				if (component2)
				{
					itemObj.Add("summonCost", new JObject(from c in Item2.GetCosts(component2.summoningCosts)
						select new JProperty(c.type.ToString(), c.baseValue)));
				}
				JArray jarray = new JArray();
				if (item.GetStatusEffectValue(Item2.ItemStatusEffect.Type.canBeMovedInCombat) != -1 || item.GetStatusEffectValue(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition) != -1)
				{
					jarray.Add(new JValue("Movable"));
				}
				itemObj.Add("special", jarray);
				GameObject itemGameObj = Object.Instantiate<GameObject>(item.gameObject, Vector3.zero, Quaternion.identity);
				yield return new WaitForEndOfFrame();
				GameObject gameObject;
				if (item.itemType.Contains(Item2.ItemType.Carving))
				{
					gameObject = Object.Instantiate<GameObject>(this.carvingCardPrefab, Vector3.zero, Quaternion.identity);
				}
				else if (item.itemType.Contains(Item2.ItemType.Pet))
				{
					gameObject = Object.Instantiate<GameObject>(this.petCardPrefab, Vector3.zero, Quaternion.identity);
				}
				else if (item.itemType.Contains(Item2.ItemType.Treat))
				{
					gameObject = Object.Instantiate<GameObject>(this.treatCardPrefab, Vector3.zero, Quaternion.identity);
				}
				else
				{
					gameObject = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity);
				}
				itemGameObj.GetComponent<ItemMovement>().ApplyCardToItem(gameObject, null, null, false);
				Card component3 = gameObject.GetComponent<Card>();
				List<Card.DescriptorTotal> descriptions = component3.GetDescriptions(itemGameObj.GetComponent<Item2>(), itemGameObj, false);
				try
				{
					JArray jarray2 = new JArray();
					foreach (Card.DescriptorTotal descriptorTotal in descriptions)
					{
						string text2 = ((descriptorTotal.trigger != null) ? component3.GetTriggerDescription(descriptorTotal.trigger, false) : "");
						text2 = Regex.Replace(text2, "<.*?>", "__");
						if (text2.EndsWith(","))
						{
							text2 = text2.Remove(text2.Length - 1, 1);
						}
						List<string> list2 = new List<string>();
						foreach (string text3 in descriptorTotal.texts)
						{
							Match match = Regex.Match(text3, "(?<=<color.*?>)(.*?)get[s]? this effect applied:(?=</color>)");
							if (match.Success)
							{
								text2 = match.Value;
								list2.Add(Regex.Match(text3, "(?<=</size>)(.*)").Value);
							}
							else
							{
								string text4 = Regex.Replace(text3, "<.*?>", "__");
								if (!(text4 == ""))
								{
									if (text4[0].ToString() == "/")
									{
										text4 = "*" + text4.Substring(1) + "*";
									}
									list2.Add(text4);
								}
							}
						}
						jarray2.Add(new JObject(new JProperty(text2, list2)));
					}
					itemObj.Add("descriptions", jarray2);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
					Debug.LogError(item.displayName);
					Debug.LogError(descriptions[0].trigger);
					Debug.LogError(descriptions[0].texts[0]);
				}
				Object.Destroy(gameObject);
				Object.Destroy(itemGameObj);
				itemArray.Add(itemObj);
				i++;
				if (i % 25 == 0)
				{
					Debug.Log(string.Concat(new string[]
					{
						"Processed ",
						i.ToString(),
						"/",
						this.item2s.Count.ToString(),
						" items. ",
						((int)((float)i / (float)this.item2s.Count * 100f)).ToString(),
						"%"
					}));
				}
				itemObj = null;
				itemGameObj = null;
				item = null;
			}
		}
		List<Item2>.Enumerator enumerator = default(List<Item2>.Enumerator);
		JObject jobject = new JObject();
		jobject.Add(new JProperty("version", "v" + File.ReadAllText(Application.streamingAssetsPath + "/build.txt")));
		jobject.Add(new JProperty("items", itemArray));
		Debug.Log("Writing JSON file");
		StreamWriter streamWriter = new StreamWriter(dir + "itemData.json", false);
		streamWriter.Write(jobject.ToString());
		streamWriter.Close();
		Debug.Log("Item Card Export End. " + i.ToString() + " items exported.");
		yield break;
		yield break;
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0005C890 File Offset: 0x0005AA90
	public GameObject GetPrefabOfItem(Item2 item)
	{
		if (!item)
		{
			return null;
		}
		string name = item.name;
		return this.GetItemByName(name);
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0005C8B8 File Offset: 0x0005AAB8
	public GameObject GetItemByName(string name)
	{
		foreach (GameObject gameObject in this.items)
		{
			if (Item2.GetDisplayName(gameObject.name) == Item2.GetDisplayName(name))
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0005C924 File Offset: 0x0005AB24
	public List<Item2> GetItem2sByNames(List<string> names)
	{
		List<Item2> list = new List<Item2>();
		foreach (string text in names)
		{
			Item2 item2ByName = this.GetItem2ByName(text);
			if (item2ByName)
			{
				list.Add(item2ByName);
			}
		}
		return list;
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0005C98C File Offset: 0x0005AB8C
	public Item2 GetItem2ByName(string name)
	{
		foreach (Item2 item in this.item2s)
		{
			if (Item2.GetDisplayName(item.name) == Item2.GetDisplayName(name))
			{
				return item;
			}
		}
		return null;
	}

	// Token: 0x040006EB RID: 1771
	public static DebugItemManager main;

	// Token: 0x040006EC RID: 1772
	[SerializeField]
	private ItemAtlas itemAtlas;

	// Token: 0x040006ED RID: 1773
	[SerializeField]
	private bool getItems;

	// Token: 0x040006EE RID: 1774
	[SerializeField]
	private bool exportItemJSON;

	// Token: 0x040006EF RID: 1775
	[SerializeField]
	private bool performAction;

	// Token: 0x040006F0 RID: 1776
	[SerializeField]
	private bool checkUnlocks;

	// Token: 0x040006F1 RID: 1777
	[SerializeField]
	private ResearchList researchList;

	// Token: 0x040006F2 RID: 1778
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x040006F3 RID: 1779
	[SerializeField]
	private GameObject carvingCardPrefab;

	// Token: 0x040006F4 RID: 1780
	[SerializeField]
	private GameObject petCardPrefab;

	// Token: 0x040006F5 RID: 1781
	[SerializeField]
	public List<GameObject> items;

	// Token: 0x040006F6 RID: 1782
	[SerializeField]
	private GameObject treatCardPrefab;

	// Token: 0x040006F7 RID: 1783
	[SerializeField]
	private List<GameObject> itemsToSkip;

	// Token: 0x040006F8 RID: 1784
	[SerializeField]
	public List<Item2> item2s;

	// Token: 0x040006F9 RID: 1785
	[SerializeField]
	public List<GameObject> enemies;
}
