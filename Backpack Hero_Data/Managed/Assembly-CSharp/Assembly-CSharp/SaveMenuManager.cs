using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000169 RID: 361
public class SaveMenuManager : MonoBehaviour
{
	// Token: 0x06000EB1 RID: 3761 RVA: 0x00092864 File Offset: 0x00090A64
	private void Start()
	{
		string text = Application.persistentDataPath + "/";
		foreach (string text2 in Directory.GetFiles(text, "bphRun*.sav"))
		{
			Debug.Log("File found!" + text2);
			this.ShowSaveSlot(int.Parse(text2.Replace(text + "bphRun", "").Replace(".sav", "")));
		}
		if (this.saveSlotsParent.childCount <= 1)
		{
			this.noSavesText.SetActive(true);
			this.saveCountText.gameObject.SetActive(false);
			return;
		}
		this.noSavesText.SetActive(false);
		this.saveCountText.gameObject.SetActive(false);
		EventSystem.current.SetSelectedGameObject(this.saveSlotsParent.GetChild(1).gameObject);
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x00092944 File Offset: 0x00090B44
	public void SetSliderMenu(Transform transform)
	{
		RectTransform component = transform.GetComponent<RectTransform>();
		RectTransform component2 = transform.parent.GetComponent<RectTransform>();
		RectTransform component3 = transform.parent.parent.parent.GetComponent<RectTransform>();
		component2.anchoredPosition = new Vector2(0f, component2.rect.height - (float)(transform.parent.childCount - transform.GetSiblingIndex()) * component.rect.height - component3.rect.height / 2f);
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x000929D0 File Offset: 0x00090BD0
	private void Update()
	{
		if (!this.eventBoxAnimator.gameObject.activeInHierarchy)
		{
			Object.FindObjectOfType<OptionsSaveManager>().SaveOptions();
			Object.Destroy(base.gameObject);
		}
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller && EventSystem.current.currentSelectedGameObject == null && this.saveSlotsParent.childCount > this.nextSaveSlot)
		{
			string text = "Selecting ";
			Transform child = this.saveSlotsParent.GetChild(this.nextSaveSlot);
			Debug.Log(text + ((child != null) ? child.ToString() : null) + " " + this.nextSaveSlot.ToString());
			EventSystem.current.SetSelectedGameObject(this.saveSlotsParent.GetChild(this.nextSaveSlot).gameObject);
		}
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x00092A98 File Offset: 0x00090C98
	private void ShowSaveSlot(int num)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.saveSlotPrefab, Vector3.zero, Quaternion.identity, this.saveSlotsParent);
		gameObject.SetActive(true);
		SaveSlot componentInChildren = gameObject.GetComponentInChildren<SaveSlot>();
		Sprite sprite = SaveManager.LoadImage(num);
		componentInChildren.SetStuff(sprite, num);
		for (int i = 0; i < this.saveSlotsParent.childCount; i++)
		{
			SaveSlot component = this.saveSlotsParent.GetChild(i).GetComponent<SaveSlot>();
			if (component && component.dateTime < componentInChildren.dateTime)
			{
				componentInChildren.transform.SetSiblingIndex(i);
				return;
			}
		}
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x00092B2C File Offset: 0x00090D2C
	public void EndEvent()
	{
		if (this.finished)
		{
			return;
		}
		SoundManager.main.PlaySFX("menuBlip");
		this.finished = true;
		this.eventBoxAnimator.Play("Out");
		Singleton.Instance.showingOptions = false;
		GameManager main = GameManager.main;
		if (main)
		{
			main.SetAllItemColliders(true);
		}
		MenuManager main2 = MenuManager.main;
		if (main2)
		{
			main2.ShowButtons();
		}
	}

	// Token: 0x04000BE7 RID: 3047
	[SerializeField]
	private GameObject saveSlotPrefab;

	// Token: 0x04000BE8 RID: 3048
	[SerializeField]
	private Transform saveSlotsParent;

	// Token: 0x04000BE9 RID: 3049
	[SerializeField]
	private Animator eventBoxAnimator;

	// Token: 0x04000BEA RID: 3050
	[SerializeField]
	private GameObject noSavesText;

	// Token: 0x04000BEB RID: 3051
	[SerializeField]
	private TMP_Text saveCountText;

	// Token: 0x04000BEC RID: 3052
	[SerializeField]
	private GameObject confirm;

	// Token: 0x04000BED RID: 3053
	private bool finished;

	// Token: 0x04000BEE RID: 3054
	public static int maxSaves = 50;

	// Token: 0x04000BEF RID: 3055
	public int nextSaveSlot;

	// Token: 0x04000BF0 RID: 3056
	private SaveSlot slotToDelete;
}
