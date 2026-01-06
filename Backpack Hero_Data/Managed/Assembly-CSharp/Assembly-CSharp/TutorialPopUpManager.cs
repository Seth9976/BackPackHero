using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class TutorialPopUpManager : MonoBehaviour
{
	// Token: 0x060010B7 RID: 4279 RVA: 0x0009E9EA File Offset: 0x0009CBEA
	private void Awake()
	{
		if (TutorialPopUpManager.main == null)
		{
			TutorialPopUpManager.main = this;
		}
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0009E9FF File Offset: 0x0009CBFF
	private void OnDestroy()
	{
		if (TutorialPopUpManager.main == this)
		{
			TutorialPopUpManager.main = null;
		}
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0009EA14 File Offset: 0x0009CC14
	public void DisplayTutorial(string name)
	{
		List<string> completedTutorials = MetaProgressSaveManager.main.completedTutorials;
		foreach (TutorialPopUpManager.TutorialPopUp tutorialPopUp in this.tutorialPopUps)
		{
			if (tutorialPopUp.name == name)
			{
				if (!completedTutorials.Contains(name))
				{
					completedTutorials.Add(name);
					MetaProgressSaveManager.main.SaveCompletedTutorials(completedTutorials);
					TutorialManager.main.completedTutorials = completedTutorials;
				}
				GameObject prefab = tutorialPopUp.prefab;
				if (prefab == null)
				{
					prefab = this.tutorialPopUpPrefab;
				}
				GameObject gameObject = Object.Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").transform);
				gameObject.transform.localPosition = Vector3.zero;
				string text = name;
				if (tutorialPopUp.nameOverrideInLanguageManager.Length > 1)
				{
					text = tutorialPopUp.nameOverrideInLanguageManager;
				}
				gameObject.GetComponent<Tutorial>().SetTutorial(LangaugeManager.main.GetTextByKey("tut-" + text), LangaugeManager.main.GetTextByKey("tut-" + text + "1"), tutorialPopUp.image);
				return;
			}
		}
		Debug.Log("Invalid Tutorial Attempted to Load");
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x0009EB54 File Offset: 0x0009CD54
	public List<string> GetAllTutorials()
	{
		List<string> list = new List<string>();
		foreach (TutorialPopUpManager.TutorialPopUp tutorialPopUp in this.tutorialPopUps)
		{
			list.Add(tutorialPopUp.name);
		}
		return list;
	}

	// Token: 0x04000D95 RID: 3477
	public static TutorialPopUpManager main;

	// Token: 0x04000D96 RID: 3478
	[SerializeField]
	private GameObject tutorialPopUpPrefab;

	// Token: 0x04000D97 RID: 3479
	[SerializeField]
	public List<TutorialPopUpManager.TutorialPopUp> tutorialPopUps;

	// Token: 0x0200047A RID: 1146
	[Serializable]
	public class TutorialPopUp
	{
		// Token: 0x04001A5B RID: 6747
		public string name = "";

		// Token: 0x04001A5C RID: 6748
		public string nameOverrideInLanguageManager = "";

		// Token: 0x04001A5D RID: 6749
		public GameObject prefab;

		// Token: 0x04001A5E RID: 6750
		public Sprite image;
	}
}
