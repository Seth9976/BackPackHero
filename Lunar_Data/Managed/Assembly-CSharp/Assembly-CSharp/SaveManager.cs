using System;
using System.Text;
using SaveSystem.States;
using UnityEngine;

namespace SaveSystem
{
	// Token: 0x020000B1 RID: 177
	public class SaveManager
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x00016C94 File Offset: 0x00014E94
		public static void SaveOptions()
		{
			Debug.Log("Saving Options");
			try
			{
				string text = JSONStateSerializer.Serialize<OptionsState>(OptionsState.Capture());
				PlatformWrapper.SaveFileDefaultDir(SaveManager.filename_options, Encoding.ASCII.GetBytes(text));
			}
			catch (Exception ex)
			{
				string text2 = "Cannot Save Options: ";
				Exception ex2 = ex;
				Debug.LogError(text2 + ((ex2 != null) ? ex2.ToString() : null) + "\n" + ex.StackTrace);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00016D08 File Offset: 0x00014F08
		public static void LoadOptions()
		{
			Debug.Log("Loading Options");
			try
			{
				byte[] array = PlatformWrapper.LoadFileDefaultDir(SaveManager.filename_options);
				OptionsState optionsState;
				if (array != null)
				{
					optionsState = JSONStateSerializer.Deserialize<OptionsState>(Encoding.ASCII.GetString(array));
				}
				else
				{
					Debug.Log("No Options Data found. Restoring default state");
					optionsState = new OptionsState();
					optionsState.chosenLanguage = LanguageManager.main.GetAutoDetectLanguage();
				}
				optionsState.Restore();
			}
			catch (Exception ex)
			{
				string text = "Cannot Load Options: ";
				Exception ex2 = ex;
				Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null) + "\n" + ex.StackTrace);
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00016DA4 File Offset: 0x00014FA4
		public static void SaveProgress()
		{
			Debug.Log("Saving Progress");
			try
			{
				string text = JSONStateSerializer.Serialize<ProgressState>(ProgressState.Capture());
				PlatformWrapper.SaveFileDefaultDir(SaveManager.filename_progress, Encoding.ASCII.GetBytes(text));
			}
			catch (Exception ex)
			{
				string text2 = "Cannot Save Progress: ";
				Exception ex2 = ex;
				Debug.LogError(text2 + ((ex2 != null) ? ex2.ToString() : null) + "\n" + ex.StackTrace);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00016E18 File Offset: 0x00015018
		public static void LoadProgress()
		{
			Debug.Log("Loading Progress");
			try
			{
				byte[] array = PlatformWrapper.LoadFileDefaultDir(SaveManager.filename_progress);
				ProgressState progressState;
				if (array != null)
				{
					progressState = JSONStateSerializer.Deserialize<ProgressState>(Encoding.ASCII.GetString(array));
				}
				else
				{
					Debug.Log("No Progress Data found. Restoring default state");
					progressState = new ProgressState();
				}
				progressState.Restore();
			}
			catch (Exception ex)
			{
				string text = "Cannot Load Progress: ";
				Exception ex2 = ex;
				Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null) + "\n" + ex.StackTrace);
			}
		}

		// Token: 0x04000399 RID: 921
		private static string filename_options = "options.json";

		// Token: 0x0400039A RID: 922
		private static string filename_progress = "progress.json";
	}
}
