using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x0200012A RID: 298
[ExecuteInEditMode]
public class DocsGenerator : MonoBehaviour
{
	// Token: 0x06000B37 RID: 2871 RVA: 0x0007164C File Offset: 0x0006F84C
	public static JObject GenerateEnumSchema(Type enumType, string enumName, bool excludeDeprecated = false)
	{
		if (!enumType.IsEnum)
		{
			throw new ArgumentException("The specified type is not an enum.");
		}
		JObject jobject = new JObject();
		jobject["$comment"] = "Enum";
		JArray jarray = new JArray();
		foreach (object obj in Enum.GetValues(enumType))
		{
			string text = obj.ToString();
			string enumItemDescription = DocsGenerator.GetEnumItemDescription(obj);
			bool flag = DocsGenerator.IsEnumItemDeprecated(obj);
			if (!excludeDeprecated || !flag)
			{
				JObject jobject2 = new JObject();
				jobject2["const"] = text;
				if (!string.IsNullOrEmpty(enumItemDescription))
				{
					jobject2["description"] = enumItemDescription;
				}
				if (flag)
				{
					jobject2["deprecated"] = true;
				}
				jarray.Add(jobject2);
			}
		}
		jobject["anyOf"] = jarray;
		return jobject;
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x0007174C File Offset: 0x0006F94C
	private static string GetEnumItemDescription(object enumValue)
	{
		DescriptionAttribute descriptionAttribute = Attribute.GetCustomAttribute(enumValue.GetType().GetMember(enumValue.ToString())[0], typeof(DescriptionAttribute)) as DescriptionAttribute;
		if (descriptionAttribute == null)
		{
			return null;
		}
		return descriptionAttribute.Description;
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00071780 File Offset: 0x0006F980
	private static bool IsEnumItemDeprecated(object enumValue)
	{
		return Attribute.GetCustomAttribute(enumValue.GetType().GetMember(enumValue.ToString())[0], typeof(DocsGenerator.NoDocsAttribute)) is DocsGenerator.NoDocsAttribute;
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x000717AC File Offset: 0x0006F9AC
	private void Update()
	{
		if (this.generateEnumSchemas)
		{
			this.generateEnumSchemas = false;
			foreach (KeyValuePair<Type, ValueTuple<string, string>> keyValuePair in this.enumDict)
			{
				string text = "../BPHModDocs/schemas/Enum/";
				Debug.Log("Generating Enum Docs for " + keyValuePair.Key.ToString());
				JObject jobject = DocsGenerator.GenerateEnumSchema(keyValuePair.Key, keyValuePair.Value.Item1, true);
				jobject.Add("description", keyValuePair.Value.Item2);
				StreamWriter streamWriter = new StreamWriter(text + keyValuePair.Value.Item1 + ".schema.json", false);
				streamWriter.Write(jobject.ToString());
				streamWriter.Close();
			}
		}
	}

	// Token: 0x04000937 RID: 2359
	public Dictionary<Type, ValueTuple<string, string>> enumDict = new Dictionary<Type, ValueTuple<string, string>>
	{
		{
			typeof(Item2.ItemType),
			new ValueTuple<string, string>("ItemType", "The type of the item. Some values, such as `Pet` are currently not-functional and may lead to game-crashes. ")
		},
		{
			typeof(Item2.ItemGrouping),
			new ValueTuple<string, string>("ItemGrouping", "This influences the RNG in the game to favor giving items of the same groupings as items in your inventory")
		},
		{
			typeof(Item2.Rarity),
			new ValueTuple<string, string>("Rarity", "Rarity of the item.")
		},
		{
			typeof(Character.CharacterName),
			new ValueTuple<string, string>("Character", "A playable character")
		},
		{
			typeof(DungeonLevel.Zone),
			new ValueTuple<string, string>("DungeonZone", "A Zone (usually 3 Floors) of the dungeon")
		},
		{
			typeof(Item2.PlayerAnimation),
			new ValueTuple<string, string>("AnimationType", "Animation that is played when an item is used")
		},
		{
			typeof(Item2.SoundEffect),
			new ValueTuple<string, string>("ItemSoundEffect", "Sound Effect that is played when an item is used")
		},
		{
			typeof(Item2.CreateEffect.CreateType),
			new ValueTuple<string, string>("CreateType", "How an item will be created")
		},
		{
			typeof(Item2.Area),
			new ValueTuple<string, string>("Area", "Area to affect for trigger/modifier/effect")
		},
		{
			typeof(Item2.AreaDistance),
			new ValueTuple<string, string>("AreaDistance", "Distance for [Area](Area.md)")
		},
		{
			typeof(Item2.Effect.Type),
			new ValueTuple<string, string>("EffectType", "")
		},
		{
			typeof(Item2.Effect.Target),
			new ValueTuple<string, string>("EffectTarget", "")
		},
		{
			typeof(Item2.Trigger.ActionTrigger),
			new ValueTuple<string, string>("TriggerType", "")
		},
		{
			typeof(Item2.MovementEffect.MovementLength),
			new ValueTuple<string, string>("MovementLength", "")
		},
		{
			typeof(Item2.MovementEffect.MovementType),
			new ValueTuple<string, string>("MovementType", "")
		},
		{
			typeof(Item2.ItemStatusEffect.Length),
			new ValueTuple<string, string>("ItemStatusEffectLength", "")
		},
		{
			typeof(Item2.ItemStatusEffect.Type),
			new ValueTuple<string, string>("ItemStatusEffectType", "")
		},
		{
			typeof(Item2.Modifier.Length),
			new ValueTuple<string, string>("ModifierLength", "")
		},
		{
			typeof(ItemSpriteChanger.SpriteChangerMode),
			new ValueTuple<string, string>("SpriteChangeMode", "")
		},
		{
			typeof(ValueChangerEx.ReplaceWithValue),
			new ValueTuple<string, string>("ValueSource", "Source where the value for this ValueChanger is pulled from")
		},
		{
			typeof(ContextMenuButton.ContextMenuButtonAndCost.UseTime),
			new ValueTuple<string, string>("UseSituation", "Used with onAlternateUse to determine when an item can be used using the context menu")
		}
	};

	// Token: 0x04000938 RID: 2360
	public bool generateEnumSchemas;

	// Token: 0x020003CF RID: 975
	[AttributeUsage(AttributeTargets.Field)]
	public class NoDocsAttribute : Attribute
	{
	}
}
