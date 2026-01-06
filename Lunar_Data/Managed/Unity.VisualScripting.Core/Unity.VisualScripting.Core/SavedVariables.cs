using System;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200016B RID: 363
	public static class SavedVariables
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x000290A5 File Offset: 0x000272A5
		public static VariablesAsset asset
		{
			get
			{
				if (SavedVariables._asset == null)
				{
					SavedVariables.Load();
				}
				return SavedVariables._asset;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000290BE File Offset: 0x000272BE
		public static void Load()
		{
			SavedVariables._asset = Resources.Load<VariablesAsset>("SavedVariables") ?? ScriptableObject.CreateInstance<VariablesAsset>();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000290D8 File Offset: 0x000272D8
		public static void OnEnterEditMode()
		{
			SavedVariables.FetchSavedDeclarations();
			SavedVariables.DestroyMergedDeclarations();
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000290E4 File Offset: 0x000272E4
		public static void OnExitEditMode()
		{
			SavedVariables.SaveDeclarations(SavedVariables.saved);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000290F0 File Offset: 0x000272F0
		internal static void OnEnterPlayMode()
		{
			SavedVariables.FetchSavedDeclarations();
			SavedVariables.MergeInitialAndSavedDeclarations();
			VariableDeclarations merged = SavedVariables.merged;
			merged.OnVariableChanged = (Action)Delegate.Combine(merged.OnVariableChanged, new Action(delegate
			{
				if (VariablesSaver.instance == null)
				{
					VariablesSaver.Instantiate();
				}
			}));
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00029140 File Offset: 0x00027340
		internal static void OnExitPlayMode()
		{
			SavedVariables.SaveDeclarations(SavedVariables.merged);
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002914C File Offset: 0x0002734C
		public static VariableDeclarations initial
		{
			get
			{
				return SavedVariables.asset.declarations;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00029158 File Offset: 0x00027358
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0002915F File Offset: 0x0002735F
		public static VariableDeclarations saved { get; private set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00029167 File Offset: 0x00027367
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0002916E File Offset: 0x0002736E
		public static VariableDeclarations merged { get; private set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00029176 File Offset: 0x00027376
		public static VariableDeclarations current
		{
			get
			{
				if (!Application.isPlaying)
				{
					return SavedVariables.initial;
				}
				return SavedVariables.merged;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002918C File Offset: 0x0002738C
		public static void SaveDeclarations(VariableDeclarations declarations)
		{
			SavedVariables.WarnAndNullifyUnityObjectReferences(declarations);
			try
			{
				SerializationData serializationData = declarations.Serialize(false);
				if (serializationData.objectReferences.Length != 0)
				{
					throw new InvalidOperationException("Cannot use Unity object variable references in saved variables.");
				}
				PlayerPrefs.SetString("LudiqSavedVariables", serializationData.json);
				PlayerPrefs.Save();
			}
			catch (Exception ex)
			{
				Debug.LogWarning(string.Format("Failed to save variables to player prefs: \n{0}", ex));
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000291F8 File Offset: 0x000273F8
		public static void FetchSavedDeclarations()
		{
			if (PlayerPrefs.HasKey("LudiqSavedVariables"))
			{
				try
				{
					SavedVariables.saved = (VariableDeclarations)new SerializationData(PlayerPrefs.GetString("LudiqSavedVariables"), Array.Empty<Object>()).Deserialize(false);
					return;
				}
				catch (Exception ex)
				{
					Debug.LogWarning(string.Format("Failed to fetch saved variables from player prefs: \n{0}", ex));
					SavedVariables.saved = new VariableDeclarations();
					return;
				}
			}
			SavedVariables.saved = new VariableDeclarations();
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00029270 File Offset: 0x00027470
		private static void MergeInitialAndSavedDeclarations()
		{
			SavedVariables.merged = SavedVariables.initial.CloneViaFakeSerialization<VariableDeclarations>();
			SavedVariables.WarnAndNullifyUnityObjectReferences(SavedVariables.merged);
			foreach (string text in SavedVariables.saved.Select((VariableDeclaration vd) => vd.name))
			{
				if (!SavedVariables.merged.IsDefined(text))
				{
					SavedVariables.merged[text] = SavedVariables.saved[text];
				}
				else if (SavedVariables.merged[text] == null)
				{
					if (SavedVariables.saved[text] == null || SavedVariables.saved[text].GetType().IsNullable())
					{
						SavedVariables.merged[text] = SavedVariables.saved[text];
					}
					else
					{
						Debug.LogWarning("Cannot convert saved player pref '" + text + "' to null.\n");
					}
				}
				else if (SavedVariables.saved[text].IsConvertibleTo(SavedVariables.merged[text].GetType(), true))
				{
					SavedVariables.merged[text] = SavedVariables.saved[text];
				}
				else
				{
					Debug.LogWarning(string.Format("Cannot convert saved player pref '{0}' to expected type ({1}).\nReverting to initial value.", text, SavedVariables.merged[text].GetType()));
				}
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000293DC File Offset: 0x000275DC
		private static void DestroyMergedDeclarations()
		{
			SavedVariables.merged = null;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000293E4 File Offset: 0x000275E4
		private static void WarnAndNullifyUnityObjectReferences(VariableDeclarations declarations)
		{
			Ensure.That("declarations").IsNotNull<VariableDeclarations>(declarations);
			foreach (VariableDeclaration variableDeclaration in declarations)
			{
				if (variableDeclaration.value is Object)
				{
					Debug.LogWarning("Saved variable '" + variableDeclaration.name + "' refers to a Unity object. This is not supported. Its value will be null.");
					declarations[variableDeclaration.name] = null;
				}
			}
		}

		// Token: 0x04000243 RID: 579
		public const string assetPath = "SavedVariables";

		// Token: 0x04000244 RID: 580
		public const string playerPrefsKey = "LudiqSavedVariables";

		// Token: 0x04000245 RID: 581
		private static VariablesAsset _asset;
	}
}
