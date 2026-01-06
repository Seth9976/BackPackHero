using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class BugReporter : MonoBehaviour
{
	// Token: 0x06000395 RID: 917 RVA: 0x00015064 File Offset: 0x00013264
	private void Start()
	{
		if (BugReporter.main != null)
		{
			Object.Destroy(this);
			return;
		}
		BugReporter.main = this;
		Object.DontDestroyOnLoad(this);
		string text = Application.persistentDataPath + "/Player-";
		try
		{
			File.Delete(text + "prev-prev-prev-prev-prev.log");
		}
		catch
		{
		}
		try
		{
			File.Move(text + "prev-prev-prev-prev.log", text + "prev-prev-prev-prev-prev.log");
		}
		catch
		{
		}
		try
		{
			File.Move(text + "prev-prev-prev.log", text + "prev-prev-prev-prev.log");
		}
		catch
		{
		}
		try
		{
			File.Move(text + "prev-prev.log", text + "prev-prev-prev.log");
		}
		catch
		{
		}
		try
		{
			File.Move(text + "prev.log", text + "prev-prev.log");
		}
		catch
		{
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0001517C File Offset: 0x0001337C
	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown("b"))
		{
			this.CreateBugReport();
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x000151A8 File Offset: 0x000133A8
	public static Dictionary<string, object> SerializeFields(object instance)
	{
		JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
		{
			PreserveReferencesHandling = PreserveReferencesHandling.Objects
		};
		FieldInfo[] fields = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		List<Type> list = new List<Type> { typeof(ES3Settings) };
		foreach (FieldInfo fieldInfo in fields)
		{
			object value = fieldInfo.GetValue(instance);
			try
			{
				if (!list.Contains(value.GetType()))
				{
					JsonConvert.SerializeObject(value, jsonSerializerSettings);
					dictionary[fieldInfo.Name] = value;
				}
			}
			catch
			{
			}
		}
		return dictionary;
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00015250 File Offset: 0x00013450
	private byte[] TakeScrenshot()
	{
		Camera component = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		int num = 1280;
		int num2 = 720;
		RenderTexture renderTexture = new RenderTexture(num, num2, 24);
		component.targetTexture = renderTexture;
		Texture2D texture2D = new Texture2D(num, num2, TextureFormat.RGB24, false);
		component.Render();
		RenderTexture.active = renderTexture;
		Debug.unityLogger.Log("read pixels");
		texture2D.ReadPixels(new Rect(0f, 0f, (float)num, (float)num2), 0, 0);
		component.targetTexture = null;
		RenderTexture.active = null;
		Object.Destroy(renderTexture);
		Debug.Log("Encoding");
		return texture2D.EncodeToPNG();
	}

	// Token: 0x06000399 RID: 921 RVA: 0x000152EC File Offset: 0x000134EC
	private void AddByteArrayToZip(ZipArchive archive, string filename, byte[] bytes)
	{
		using (Stream stream = archive.CreateEntry(filename).Open())
		{
			stream.Write(bytes, 0, bytes.Length);
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00015330 File Offset: 0x00013530
	private List<GameObject> GetRootObjects()
	{
		GameObject[] array = Object.FindObjectsOfType<GameObject>();
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in array)
		{
			if (gameObject.transform.parent == null)
			{
				list.Add(gameObject);
			}
		}
		return list;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00015378 File Offset: 0x00013578
	private void ListObjects(Transform parent, int depth)
	{
		string text = new string('-', depth * 4);
		foreach (object obj in parent)
		{
			Transform transform = (Transform)obj;
			this.objectHierarchy.AppendLine(text + transform.name);
			this.ListObjects(transform, depth + 1);
		}
	}

	// Token: 0x0600039C RID: 924 RVA: 0x000153F4 File Offset: 0x000135F4
	private void SerializeSceneToArchive(ZipArchive archive, string folder, List<Transform> objects)
	{
		try
		{
			archive.CreateEntry(folder, CompressionLevel.NoCompression);
			using (List<Transform>.Enumerator enumerator = objects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Transform root = enumerator.Current;
					List<Component> list = root.gameObject.GetComponents<Component>().ToList<Component>();
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					List<Type> list2 = new List<Type>
					{
						typeof(TwitchManager),
						typeof(LangaugeManager),
						typeof(BugReporter)
					};
					using (List<Component>.Enumerator enumerator2 = list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object comp = enumerator2.Current;
							if (!list2.Contains(comp.GetType()))
							{
								dictionary.Add(comp.GetType().Name + "_" + list.FindIndex((Component c) => c == comp).ToString(), BugReporter.SerializeFields(comp));
							}
						}
					}
					try
					{
						StringEnumConverter stringEnumConverter = new StringEnumConverter();
						this.AddByteArrayToZip(archive, string.Concat(new string[]
						{
							folder,
							root.gameObject.name,
							"_",
							objects.FindIndex((Transform c) => c == root).ToString(),
							".json"
						}), Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dictionary, Formatting.Indented, new JsonConverter[] { stringEnumConverter })));
					}
					catch
					{
					}
					List<Transform> list3 = new List<Transform>();
					foreach (object obj in root)
					{
						Transform transform = (Transform)obj;
						list3.Add(transform);
					}
					if (list3.Count > 0)
					{
						this.SerializeSceneToArchive(archive, string.Concat(new string[]
						{
							folder,
							root.gameObject.name,
							"_",
							objects.FindIndex((Transform c) => c == root).ToString(),
							"_Children/"
						}), list3);
					}
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x0600039D RID: 925 RVA: 0x000156E0 File Offset: 0x000138E0
	private void CreateBugReport()
	{
		Debug.Log("------BugReport Start------");
		if (!Directory.Exists(Application.persistentDataPath + "/BugReports/"))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/BugReports/");
		}
		string persistentDataPath = Application.persistentDataPath;
		string text = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
		string text2 = Application.persistentDataPath + "/BugReports/BugReport_" + text + ".zip";
		string[] array = new string[] { "Player.log", "Player-prev.log", "Player-prev-prev.log", "Player-prev-prev-prev.log", "Player-prev-prev-prev-prev.log", "Player-prev-prev-prev-prev-prev.log", "mods.log", "mods.old.log" };
		using (FileStream fileStream = new FileStream(text2, FileMode.Create))
		{
			using (ZipArchive zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
			{
				foreach (string text3 in array)
				{
					try
					{
						string text4 = Path.Combine(persistentDataPath, text3);
						if (File.Exists(text4))
						{
							string text5 = text3.Replace("\\", "/");
							using (FileStream fileStream2 = new FileStream(text4, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
							{
								byte[] array3 = new byte[fileStream2.Length];
								fileStream2.Read(array3, 0, (int)fileStream2.Length);
								this.AddByteArrayToZip(zipArchive, text5, array3);
							}
						}
					}
					catch (Exception ex)
					{
						Debug.Log(ex);
					}
				}
				try
				{
					this.AddByteArrayToZip(zipArchive, "Screenshot.png", this.TakeScrenshot());
				}
				catch (Exception)
				{
				}
				try
				{
					this.objectHierarchy = new StringBuilder();
					foreach (GameObject gameObject in this.GetRootObjects())
					{
						this.ListObjects(gameObject.transform, 0);
					}
					this.AddByteArrayToZip(zipArchive, "Scene.txt", Encoding.ASCII.GetBytes(this.objectHierarchy.ToString()));
				}
				catch (Exception)
				{
				}
				try
				{
					this.SerializeSceneToArchive(zipArchive, "Scene/", (from g in this.GetRootObjects()
						select g.transform).ToList<Transform>());
				}
				catch
				{
				}
				try
				{
					try
					{
						zipArchive.CreateEntryFromFile(Application.persistentDataPath + "/bphSettings.sav", "bphSettings.sav");
					}
					catch
					{
					}
					try
					{
						zipArchive.CreateEntryFromFile(Application.persistentDataPath + "/bphAchievementData.sav", "bphAchievementData.sav");
					}
					catch
					{
					}
					try
					{
						zipArchive.CreateEntryFromFile(Application.persistentDataPath + "/ModData.sav", "ModData.sav");
					}
					catch
					{
					}
					try
					{
						zipArchive.CreateEntryFromFile(Application.persistentDataPath + "/TwitchData.sav", "TwitchData.sav");
					}
					catch
					{
					}
					if (Object.FindAnyObjectByType(typeof(MenuManager)) != null)
					{
						try
						{
							foreach (string text6 in (from f in Directory.GetFiles(Application.persistentDataPath)
								where f.Contains(".sav")
								select f).ToList<string>())
							{
								try
								{
									zipArchive.CreateEntryFromFile(text6, Path.GetFileName(text6));
								}
								catch
								{
								}
							}
							goto IL_04EE;
						}
						catch
						{
							goto IL_04EE;
						}
					}
					if (Singleton.Instance.storyMode)
					{
						try
						{
							try
							{
								zipArchive.CreateEntryFromFile(Application.persistentDataPath + "/bphStoryModeRun" + Singleton.Instance.storyModeSlot.ToString() + ".sav", "bphStoryModeRun" + Singleton.Instance.storyModeSlot.ToString() + ".sav");
							}
							catch
							{
							}
							try
							{
								foreach (string text7 in SaveIncrementer.GetSaveFilesForSlot("bphStoryModeMetaData" + Singleton.Instance.storyModeSlot.ToString(), ".sav", true))
								{
									try
									{
										zipArchive.CreateEntryFromFile(text7, Path.GetFileName(text7));
									}
									catch
									{
									}
								}
							}
							catch
							{
							}
							try
							{
								foreach (string text8 in SaveIncrementer.GetSaveFilesForSlot("bphStoryModeOverworld" + Singleton.Instance.storyModeSlot.ToString(), ".sav", true))
								{
									try
									{
										zipArchive.CreateEntryFromFile(text8, Path.GetFileName(text8));
									}
									catch
									{
									}
								}
							}
							catch
							{
							}
							goto IL_04EE;
						}
						catch (Exception)
						{
							goto IL_04EE;
						}
					}
					try
					{
						zipArchive.CreateEntryFromFile(Application.persistentDataPath + "/bphRun" + Singleton.Instance.saveNumber.ToString() + ".sav", "bphRun" + Singleton.Instance.saveNumber.ToString() + ".sav");
					}
					catch
					{
					}
					try
					{
						zipArchive.CreateEntryFromFile(Application.persistentDataPath + "/bphMeta.sav", "bphMeta.sav");
					}
					catch
					{
					}
					IL_04EE:;
				}
				catch (Exception)
				{
				}
				Application.OpenURL(Application.persistentDataPath + "/BugReports/");
				Debug.Log("------BugReport End------");
			}
		}
	}

	// Token: 0x0400028F RID: 655
	private static BugReporter main;

	// Token: 0x04000290 RID: 656
	private StringBuilder objectHierarchy;
}
