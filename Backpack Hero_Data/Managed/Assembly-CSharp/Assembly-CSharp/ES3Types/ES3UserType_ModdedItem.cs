using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000229 RID: 553
	[Preserve]
	[ES3Properties(new string[] { "id", "hash", "file", "textKeys", "sprites", "modpackInternal", "createEffectsRef", "valueChangerRef" })]
	public class ES3UserType_ModdedItem : ES3ComponentType
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x000ABF95 File Offset: 0x000AA195
		public ES3UserType_ModdedItem()
			: base(typeof(ModdedItem))
		{
			ES3UserType_ModdedItem.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000ABFB4 File Offset: 0x000AA1B4
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ModdedItem moddedItem = (ModdedItem)obj;
			writer.WriteProperty("id", moddedItem.id, ES3Type_int.Instance);
			writer.WriteProperty("hash", moddedItem.hash, ES3Type_string.Instance);
			writer.WriteProperty("file", moddedItem.file, ES3Type_string.Instance);
			writer.WriteProperty("spriteScale", moddedItem.spriteScale, ES3Type_int.Instance);
			writer.WriteProperty("modpackInternal", moddedItem.fromModpack.internalName, ES3Type_string.Instance);
			List<string> list = new List<string>();
			moddedItem.GatherExtraKeys();
			foreach (KeyValuePair<string, SerializedDictionary<string, string>> keyValuePair in moddedItem.textKeys)
			{
				foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair.Value)
				{
					list.Add(string.Concat(new string[] { keyValuePair.Key, "\u001e", keyValuePair2.Key, "\u001e", keyValuePair2.Value }));
				}
			}
			writer.WriteProperty("textKeys", list.ToArray(), ES3Type_StringArray.Instance);
			List<string> list2 = new List<string>();
			foreach (ValueTuple<string, byte[]> valueTuple in moddedItem.GetSpriteData())
			{
				list2.Add(valueTuple.Item1 + "\u001e" + Convert.ToBase64String(valueTuple.Item2));
			}
			writer.WriteProperty("sprites", list2.ToArray(), ES3Type_StringArray.Instance);
			List<string> list3 = new List<string>();
			int num = 0;
			foreach (ModdedItem.StringList stringList in moddedItem.createEffectRefs)
			{
				foreach (string text in stringList.list)
				{
					list3.Add(num.ToString() + "\u001e" + text);
				}
				num++;
			}
			writer.WriteProperty("createEffectsRef", list3.ToArray(), ES3Type_StringArray.Instance);
			List<string> list4 = new List<string>();
			int num2 = 0;
			foreach (ModdedItem.StringList stringList2 in moddedItem.valueChangerRefs)
			{
				foreach (string text2 in stringList2.list)
				{
					list4.Add(num2.ToString() + "\u001e" + text2);
				}
				num2++;
			}
			writer.WriteProperty("valueChangerRef", list4.ToArray(), ES3Type_StringArray.Instance);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000AC314 File Offset: 0x000AA514
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ModdedItem moddedItem = (ModdedItem)obj;
			moddedItem.reloaded = true;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2532921395U)
				{
					if (num <= 926444256U)
					{
						if (num != 48597689U)
						{
							if (num == 926444256U)
							{
								if (text == "id")
								{
									moddedItem.id = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "valueChangerRef")
						{
							string[] array = reader.Read<string[]>(ES3Type_StringArray.Instance);
							for (int i = 0; i < array.Length; i++)
							{
								string[] array2 = array[i].Split('\u001e', StringSplitOptions.None);
								int num2 = int.Parse(array2[0]);
								while (moddedItem.valueChangerRefs.Count - 1 < num2)
								{
									moddedItem.valueChangerRefs.Add(new ModdedItem.StringList());
								}
								moddedItem.valueChangerRefs[num2].list.Add(array2[1]);
							}
							continue;
						}
					}
					else if (num != 1744142202U)
					{
						if (num == 2532921395U)
						{
							if (text == "modpackInternal")
							{
								if (moddedItem.fromModpack == null)
								{
									moddedItem.fromModpack = new ModLoader.ModpackInfo();
								}
								moddedItem.fromModpack.internalName = reader.Read<string>(ES3Type_string.Instance);
								continue;
							}
						}
					}
					else if (text == "createEffectsRef")
					{
						string[] array = reader.Read<string[]>(ES3Type_StringArray.Instance);
						for (int i = 0; i < array.Length; i++)
						{
							string[] array3 = array[i].Split('\u001e', StringSplitOptions.None);
							int num3 = int.Parse(array3[0]);
							while (moddedItem.createEffectRefs.Count - 1 < num3)
							{
								moddedItem.createEffectRefs.Add(new ModdedItem.StringList());
							}
							moddedItem.createEffectRefs[num3].list.Add(array3[1]);
						}
						continue;
					}
				}
				else if (num <= 2934681110U)
				{
					if (num != 2867484483U)
					{
						if (num == 2934681110U)
						{
							if (text == "spriteScale")
							{
								moddedItem.spriteScale = reader.Read<int>(ES3Type_int.Instance);
								continue;
							}
						}
					}
					else if (text == "file")
					{
						moddedItem.file = reader.Read<string>(ES3Type_string.Instance);
						continue;
					}
				}
				else if (num != 3185882306U)
				{
					if (num != 3469047761U)
					{
						if (num == 3864082109U)
						{
							if (text == "sprites")
							{
								string[] array = reader.Read<string[]>(ES3Type_StringArray.Instance);
								for (int i = 0; i < array.Length; i++)
								{
									string[] array4 = array[i].Split('\u001e', StringSplitOptions.None);
									moddedItem.sprites.Add(new ValueTuple<string, byte[]>(array4[0], Convert.FromBase64String(array4[1])));
								}
								continue;
							}
						}
					}
					else if (text == "hash")
					{
						moddedItem.hash = reader.Read<string>(ES3Type_string.Instance);
						continue;
					}
				}
				else if (text == "textKeys")
				{
					string[] array = reader.Read<string[]>(ES3Type_StringArray.Instance);
					for (int i = 0; i < array.Length; i++)
					{
						string[] array5 = array[i].Split('\u001e', StringSplitOptions.None);
						SerializedDictionary<string, string> serializedDictionary;
						moddedItem.textKeys.TryGetValue(array5[0], out serializedDictionary);
						if (serializedDictionary == null)
						{
							serializedDictionary = new SerializedDictionary<string, string> { 
							{
								array5[1],
								array5[2]
							} };
							moddedItem.textKeys.Add(array5[0], serializedDictionary);
						}
						else
						{
							moddedItem.textKeys[array5[0]].Add(array5[1], array5[2]);
						}
					}
					moddedItem.AddKeysBack();
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E55 RID: 3669
		public static ES3Type Instance;
	}
}
