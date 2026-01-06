using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000135 RID: 309
public class ModdedItem : MonoBehaviour
{
	// Token: 0x06000BA8 RID: 2984 RVA: 0x0007AA7C File Offset: 0x00078C7C
	private void OnEnable()
	{
		if (this.gotEnabled)
		{
			return;
		}
		this.gotEnabled = true;
		base.gameObject.GetComponent<ItemMovement>().Start();
		this.Start();
		if (base.gameObject.GetComponent<Carving>() != null)
		{
			base.gameObject.GetComponent<Carving>().Start();
		}
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x0007AAD4 File Offset: 0x00078CD4
	public void GatherExtraKeys()
	{
		SerializedDictionary<string, string> serializedDictionary = new SerializedDictionary<string, string>();
		foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in ModLoader.main.languageTerms)
		{
			string text;
			keyValuePair.Value.TryGetValue(this.fromModpack.displayName.ToLower().Trim(), out text);
			if (text != null)
			{
				serializedDictionary.Add(keyValuePair.Key, text);
			}
		}
		this.textKeys.TryAdd("modpackName", serializedDictionary);
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x0007AB74 File Offset: 0x00078D74
	public void AddKeysBack()
	{
		ModLoader main = ModLoader.main;
		if (main == null)
		{
			return;
		}
		foreach (KeyValuePair<string, SerializedDictionary<string, string>> keyValuePair in this.textKeys)
		{
			string text = keyValuePair.Key;
			if (text == "modpackName")
			{
				if (this.fromModpack == null)
				{
					this.fromModpack = new ModLoader.ModpackInfo();
				}
				text = "PACKNAME" + Guid.NewGuid().ToString();
				this.fromModpack.displayName = text;
			}
			main.AddTextKey(JObject.FromObject(keyValuePair.Value), text, "reloaded item keys ", true, false, null, null);
		}
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x0007AC44 File Offset: 0x00078E44
	private void Start()
	{
		if (this.reloaded)
		{
			this.reloaded = false;
			this.ReloadSprites();
			this.AddKeysBack();
			base.StartCoroutine(this.ResolvePlaceholders());
		}
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0007AC6E File Offset: 0x00078E6E
	private IEnumerator ResolvePlaceholders()
	{
		yield return new WaitForSeconds(2f);
		this.RefsToPlaceholder();
		try
		{
			ModItemLoader.main.ResolvePlaceholders(base.GetComponent<Item2>());
			yield break;
		}
		catch (Exception ex)
		{
			string internalName = this.fromModpack.internalName;
			string text = "item";
			string text2 = "Cannot resolve item placeholder --> ";
			Exception ex2 = ex;
			ModLog.LogError(internalName, text, text2 + ((ex2 != null) ? ex2.ToString() : null));
			yield break;
		}
		yield break;
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x0007AC80 File Offset: 0x00078E80
	private void RefsToPlaceholder()
	{
		List<Item2.CreateEffect> createEffects = base.GetComponent<Item2>().createEffects;
		int num = 0;
		foreach (Item2.CreateEffect createEffect in createEffects)
		{
			for (int i = 0; i < createEffect.itemsToCreate.Count; i++)
			{
				if (createEffect.itemsToCreate[i] == null)
				{
					createEffect.itemsToCreate[i] = ModItemLoader.main.CreatePlaceholder(this.createEffectRefs[num].list[i], base.gameObject);
				}
			}
			num++;
		}
		ValueChangerEx[] componentsInChildren = base.GetComponentsInChildren<ValueChangerEx>();
		int num2 = 0;
		foreach (ValueChangerEx valueChangerEx in componentsInChildren)
		{
			for (int k = 0; k < valueChangerEx.itemPrefabs.Count; k++)
			{
				if (valueChangerEx.itemPrefabs[k] == null)
				{
					valueChangerEx.itemPrefabs[k] = ModItemLoader.main.CreatePlaceholder(this.valueChangerRefs[num2].list[k], base.gameObject);
				}
			}
			num2++;
		}
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x0007ADCC File Offset: 0x00078FCC
	public List<ValueTuple<string, byte[]>> GetSpriteData()
	{
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		List<ValueTuple<string, byte[]>> list = new List<ValueTuple<string, byte[]>>();
		list.Add(new ValueTuple<string, byte[]>("main", component.sprite.texture.EncodeToPNG()));
		ItemSpriteChanger component2 = base.GetComponent<ItemSpriteChanger>();
		if (component2 != null)
		{
			foreach (Sprite sprite in component2.sprites)
			{
				list.Add(new ValueTuple<string, byte[]>("changer", sprite.texture.EncodeToPNG()));
			}
		}
		this.sprites = list;
		return list;
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x0007AE7C File Offset: 0x0007907C
	public void ReloadSprites()
	{
		ItemSpriteChanger component = base.GetComponent<ItemSpriteChanger>();
		if (component != null)
		{
			component.sprites = new List<Sprite>();
		}
		foreach (ValueTuple<string, byte[]> valueTuple in this.sprites)
		{
			string item = valueTuple.Item1;
			if (!(item == "main"))
			{
				if (item == "changer")
				{
					if (!(component == null))
					{
						component.sprites.Add(ModUtils.LoadNewSprite(valueTuple.Item2, (float)this.spriteScale));
					}
				}
			}
			else
			{
				base.GetComponent<SpriteRenderer>().sprite = ModUtils.LoadNewSprite(valueTuple.Item2, (float)this.spriteScale);
			}
		}
	}

	// Token: 0x04000974 RID: 2420
	public int id;

	// Token: 0x04000975 RID: 2421
	public string hash;

	// Token: 0x04000976 RID: 2422
	public string file;

	// Token: 0x04000977 RID: 2423
	public int spriteScale = 16;

	// Token: 0x04000978 RID: 2424
	public string placeholder;

	// Token: 0x04000979 RID: 2425
	public string lookupName;

	// Token: 0x0400097A RID: 2426
	public GameObject thisObj;

	// Token: 0x0400097B RID: 2427
	public bool reloaded;

	// Token: 0x0400097C RID: 2428
	[NonSerialized]
	private bool gotEnabled;

	// Token: 0x0400097D RID: 2429
	public List<ValueTuple<string, byte[]>> sprites = new List<ValueTuple<string, byte[]>>();

	// Token: 0x0400097E RID: 2430
	public ModLoader.ModpackInfo fromModpack;

	// Token: 0x0400097F RID: 2431
	public List<ModdedItem.StringList> createEffectRefs = new List<ModdedItem.StringList>();

	// Token: 0x04000980 RID: 2432
	public List<ModdedItem.StringList> valueChangerRefs = new List<ModdedItem.StringList>();

	// Token: 0x04000981 RID: 2433
	public SerializedDictionary<string, SerializedDictionary<string, string>> textKeys = new SerializedDictionary<string, SerializedDictionary<string, string>>();

	// Token: 0x020003E1 RID: 993
	[Serializable]
	public class StringList
	{
		// Token: 0x04001713 RID: 5907
		public List<string> list = new List<string>();
	}
}
