using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000182 RID: 386
public class CostumeSelector : MonoBehaviour
{
	// Token: 0x06000F7F RID: 3967 RVA: 0x00097258 File Offset: 0x00095458
	private void Start()
	{
		this.maxCharacterNumber = this.characterScroller.childCount - 1;
		this.Show(false);
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x00097274 File Offset: 0x00095474
	public void Show(bool show)
	{
		this.characterSelectMaster.SetActive(show);
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x00097284 File Offset: 0x00095484
	public void Setup()
	{
		if (Singleton.Instance.doTutorial)
		{
			Singleton.Instance.runType = null;
		}
		this.runTypeMaster.SetActive(false);
		Singleton.Instance.SetCharacterFromNumber();
		this.ChooseNextCharacter(Singleton.Instance.characterNumber);
		GameObject[] array = GameObject.FindGameObjectsWithTag("CostumeButton");
		if (array.Length > Singleton.Instance.costumeNumber)
		{
			this.SelectCostumeQuietly(array[Singleton.Instance.costumeNumber].transform);
			return;
		}
		if (array.Length != 0)
		{
			this.SelectCostumeQuietly(array[0].transform);
		}
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x00097312 File Offset: 0x00095512
	private void Update()
	{
		if (this.targetDestination)
		{
			this.target.transform.position = this.targetDestination.position;
		}
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x0009733C File Offset: 0x0009553C
	public void StartGame()
	{
		if (Singleton.Instance.runType == null)
		{
			Singleton.Instance.runType = this.standardRun;
		}
		if (Singleton.Instance.runType != this.standardRun)
		{
			RunTypeButton runTypeButton = RunTypeButton.FindRunTypeButton(Singleton.Instance.runType);
			if (!runTypeButton)
			{
				return;
			}
			if (!runTypeButton.UnlockedRunType())
			{
				Object.FindObjectOfType<MenuManager>().CreatePopUp(LangaugeManager.main.GetTextByKey("rte3"), Object.FindObjectOfType<Canvas>().transform.InverseTransformPoint(DigitalCursor.main.transform.position), 0.2f);
				SoundManager.main.PlaySFX("negative");
				return;
			}
		}
		Singleton.Instance.characterNumber = this.characterNumber;
		Singleton.Instance.SetCharacterFromNumber();
		Singleton.Instance.StartGameScene();
		SceneLoader.main.LoadScene("Game", LoadSceneMode.Single, null, null);
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x0009742C File Offset: 0x0009562C
	public void CloseMenu()
	{
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (menuManager)
		{
			menuManager.ShowButtons();
		}
		SoundManager.main.PlaySFX("menuBlip");
		this.animator.Play("Out");
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x0009746C File Offset: 0x0009566C
	public void SelectCostume(Transform t)
	{
		Singleton.Instance.costumeNumber = 0;
		SoundManager.main.PlaySFX("menuBlip");
		this.target.gameObject.SetActive(true);
		this.targetDestination = t;
		Singleton.Instance.costumeNumber = t.GetSiblingIndex();
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x000974BB File Offset: 0x000956BB
	public void SelectCostumeQuietly(Transform t)
	{
		this.target.gameObject.SetActive(true);
		this.targetDestination = t;
		Singleton.Instance.costumeNumber = t.GetSiblingIndex();
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x000974E8 File Offset: 0x000956E8
	public void RunTypeButtonPressed()
	{
		SoundManager.main.PlaySFX("menuBlip");
		if (this.runTypeMaster.activeInHierarchy || Singleton.Instance.doTutorial)
		{
			this.StartGame();
			return;
		}
		this.characterMenu.SetActive(false);
		this.runTypeMaster.SetActive(true);
		this.runTypeMaster.GetComponent<RunTypeSelector>().LoadRunTypes();
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x0009754C File Offset: 0x0009574C
	public void BackFromRunTypeMenu()
	{
		SoundManager.main.PlaySFX("menuBlip");
		this.characterMenu.SetActive(true);
		this.runTypeMaster.SetActive(false);
		this.Setup();
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x0009757C File Offset: 0x0009577C
	private void SetCostume()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("CostumeButton");
		if (array.Length != 0)
		{
			this.SelectCostumeQuietly(array[0].transform);
		}
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x000975A8 File Offset: 0x000957A8
	public void ChooseNextCharacter(int change)
	{
		SoundManager.main.PlaySFX("menuBlip");
		this.runTypeMaster.SetActive(false);
		if (this.isChanging)
		{
			return;
		}
		this.characterNumber = change;
		this.isChanging = true;
		Singleton.Instance.characterNumber = this.characterNumber;
		Singleton.Instance.SetCharacterFromNumber();
		if (Singleton.Instance.runType && Singleton.Instance.runType.validForCharacters.Count > 0 && !Singleton.Instance.runType.validForCharacters.Contains(Singleton.Instance.character))
		{
			Singleton.Instance.runType = null;
		}
		base.StartCoroutine(this.MoveOverTime(this.characterScroller, this.characterScroller.anchoredPosition, new Vector2((float)(950 * this.characterNumber * -1), 0f), 0.2f));
		DigitalCursor.main.SelectUIElement(this.characterIcons.transform.GetChild(Singleton.Instance.characterNumber).gameObject);
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x000976B9 File Offset: 0x000958B9
	private IEnumerator MoveOverTime(RectTransform tran, Vector2 start, Vector2 end, float time)
	{
		this.targetDestination = null;
		this.target.gameObject.SetActive(false);
		this.characterScroller.GetChild(this.characterNumber).gameObject.SetActive(true);
		List<SpriteRenderer> spritesToFadeOut = new List<SpriteRenderer>();
		List<SpriteRenderer> spritesToFadeIn = new List<SpriteRenderer>();
		foreach (object obj in this.characterScroller.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.GetSiblingIndex() != this.characterNumber)
			{
				foreach (SpriteRenderer spriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
				{
					spritesToFadeOut.Add(spriteRenderer);
				}
			}
			else
			{
				foreach (SpriteRenderer spriteRenderer2 in transform.GetComponentsInChildren<SpriteRenderer>())
				{
					spritesToFadeIn.Add(spriteRenderer2);
				}
			}
		}
		float t = 0f;
		while (t < time)
		{
			t += Time.deltaTime;
			tran.anchoredPosition = Vector2.Lerp(start, end, t / time);
			foreach (SpriteRenderer spriteRenderer3 in spritesToFadeOut)
			{
				spriteRenderer3.color = Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), t / (time / 2f));
			}
			foreach (SpriteRenderer spriteRenderer4 in spritesToFadeIn)
			{
				spriteRenderer4.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), Color.white, t / time);
			}
			yield return null;
		}
		foreach (object obj2 in this.characterScroller.transform)
		{
			Transform transform2 = (Transform)obj2;
			if (transform2.GetSiblingIndex() != this.characterNumber)
			{
				transform2.gameObject.SetActive(false);
			}
		}
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("CostumeButton");
		if (array2.Length > Singleton.Instance.costumeNumber)
		{
			this.SelectCostumeQuietly(array2[Singleton.Instance.costumeNumber].transform);
		}
		else if (array2.Length != 0)
		{
			this.SelectCostumeQuietly(array2[0].transform);
		}
		this.isChanging = false;
		yield break;
	}

	// Token: 0x04000C9B RID: 3227
	[SerializeField]
	private RectTransform characterScroller;

	// Token: 0x04000C9C RID: 3228
	[SerializeField]
	private Transform target;

	// Token: 0x04000C9D RID: 3229
	[SerializeField]
	private GameObject leftArrow;

	// Token: 0x04000C9E RID: 3230
	[SerializeField]
	private GameObject rightArrow;

	// Token: 0x04000C9F RID: 3231
	[SerializeField]
	private Animator animator;

	// Token: 0x04000CA0 RID: 3232
	[SerializeField]
	private GameObject runTypeMaster;

	// Token: 0x04000CA1 RID: 3233
	[SerializeField]
	private GameObject characterSelectMaster;

	// Token: 0x04000CA2 RID: 3234
	[SerializeField]
	private GameObject startSelected;

	// Token: 0x04000CA3 RID: 3235
	[SerializeField]
	private GameObject characterIcons;

	// Token: 0x04000CA4 RID: 3236
	[SerializeField]
	private GameObject characterMenu;

	// Token: 0x04000CA5 RID: 3237
	private Transform targetDestination;

	// Token: 0x04000CA6 RID: 3238
	private int characterNumber;

	// Token: 0x04000CA7 RID: 3239
	private int maxCharacterNumber;

	// Token: 0x04000CA8 RID: 3240
	[SerializeField]
	private RunType standardRun;

	// Token: 0x04000CA9 RID: 3241
	private bool isChanging;
}
