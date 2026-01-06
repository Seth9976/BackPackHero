using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000073 RID: 115
public class NewCharacterSelector : MonoBehaviour
{
	// Token: 0x0600023A RID: 570 RVA: 0x0000DE2C File Offset: 0x0000C02C
	private void Start()
	{
		if (!Singleton.Instance.storyMode)
		{
			ConditionalEffect[] componentsInChildren = base.GetComponentsInChildren<ConditionalEffect>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Object.Destroy(componentsInChildren[i]);
			}
		}
		Singleton.Instance.SetCharacter(Character.CharacterName.Purse);
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000DE70 File Offset: 0x0000C070
	public void LockCharacter(Transform t)
	{
		foreach (Image image in t.GetComponentsInChildren<Image>())
		{
			if (!(image.transform == t))
			{
				image.color = Color.black;
			}
		}
		Button componentInChildren = t.GetComponentInChildren<Button>();
		if (componentInChildren)
		{
			componentInChildren.interactable = false;
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
	private void OnEnable()
	{
		foreach (Animator animator in this.costumeParent.GetComponentsInChildren<Animator>())
		{
			if (this.chosenCostume && animator.transform.IsChildOf(this.chosenCostume))
			{
				animator.enabled = true;
			}
			else
			{
				base.StartCoroutine(this.DisableAnimation(animator));
			}
		}
		this.ChangeCharacterStuff(this.characterList[0]);
		if (this.chooseCharacterCoroutine != null)
		{
			base.StopCoroutine(this.chooseCharacterCoroutine);
		}
		this.chooseCharacterCoroutine = base.StartCoroutine(this.ChooseCharacterCoroutine(this.characterList[0]));
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0000DF70 File Offset: 0x0000C170
	private void Update()
	{
		if (this.selectedCharacterName == Character.CharacterName.Any)
		{
			this.ChooseCharacter(this.characterList[0]);
		}
		if (this.chosenCostume)
		{
			this.targetTransform.position = Vector3.Lerp(this.targetTransform.position, this.chosenCostume.position, 0.2f);
		}
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
	public void ChooseCharacter(Character character)
	{
		if (this.selectedCharacterName != Character.CharacterName.Any)
		{
			SoundManager.main.PlaySFX("menuBlip");
		}
		this.selectedCharacter = character;
		this.selectedCharacterName = character.characterName;
		this.characterSelectAnimator.gameObject.SetActive(true);
		this.characterSelectAnimator.Play("characterOut");
		Singleton.Instance.SetCharacter(character.characterName);
		if (this.chooseCharacterCoroutine != null)
		{
			base.StopCoroutine(this.chooseCharacterCoroutine);
		}
		this.chooseCharacterCoroutine = base.StartCoroutine(this.ChooseCharacterCoroutine(character));
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000E060 File Offset: 0x0000C260
	private void ChangeCharacterStuff(Character character)
	{
		this.selectedCharacter = character;
		this.selectedCharacterName = character.characterName;
		this.characterPortrait.sprite = character.portraitSprite;
		this.characterName.text = LangaugeManager.main.GetTextByKey(character.characterNameKey);
		this.characterDescription.text = LangaugeManager.main.GetTextByKey(character.characterDescriptionKey);
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000E0C7 File Offset: 0x0000C2C7
	private IEnumerator ChooseCharacterCoroutine(Character character)
	{
		yield return new WaitForSeconds(0.25f);
		this.ChangeCharacterStuff(character);
		foreach (object obj in this.costumeParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		int num = 0;
		foreach (RuntimeAnimatorController runtimeAnimatorController in character.animatorControllers)
		{
			if (num != 0 && Singleton.Instance.storyMode && !MetaProgressSaveManager.main.CostumeUnlocked(runtimeAnimatorController))
			{
				num++;
			}
			else
			{
				if (character.characterSelectorSizeRatio.Count > num && character.characterSelectorSizeRatio[num] != 0f)
				{
					this.costumePrefab.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(90f * character.characterSelectorSizeRatio[num], 90f * character.characterSelectorSizeRatio[num], 1f);
				}
				GameObject costume = Object.Instantiate<GameObject>(this.costumePrefab, this.costumeParent);
				Animator componentInChildren = costume.GetComponentInChildren<Animator>();
				componentInChildren.runtimeAnimatorController = runtimeAnimatorController;
				if (num == 0)
				{
					this.ChooseCostume(costume.transform, num);
				}
				else if (componentInChildren != null)
				{
					base.StartCoroutine(this.DisableAnimation(componentInChildren));
				}
				int u = num;
				costume.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate
				{
					this.ChooseCostumeWithBlip(costume.transform, u);
				});
				Singleton.Instance.costumeNumber = 0;
				num++;
			}
		}
		DigitalCursor.main.SelectFirstSelectableInElement(this.costumeParent);
		yield break;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000E0DD File Offset: 0x0000C2DD
	private IEnumerator DisableAnimation(Animator animator)
	{
		animator.enabled = true;
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		animator.enabled = false;
		yield break;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000E0EC File Offset: 0x0000C2EC
	private void ChooseCostumeWithBlip(Transform t, int num)
	{
		SoundManager.main.PlaySFX("menuBlip");
		this.ChooseCostume(t, num);
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000E108 File Offset: 0x0000C308
	private void ChooseCostume(Transform t, int num)
	{
		if (this.chosenCostume)
		{
			this.chosenCostume.GetComponentInChildren<Animator>().enabled = false;
		}
		this.chosenCostume = t;
		t.GetComponentInChildren<Animator>().enabled = true;
		Debug.Log("Choose costume " + num.ToString());
		Singleton.Instance.costumeNumber = num;
		DigitalCursor.main.SelectFirstSelectableInElement(this.goAdventuringButton.transform);
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0000E17C File Offset: 0x0000C37C
	public void CloseWindow()
	{
		Overworld_Manager.main.SetState(Overworld_Manager.State.MOVING);
		Animator component = base.GetComponent<Animator>();
		if (component)
		{
			component.Play("inventoryOut");
		}
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
	public void StartGame()
	{
		MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
		if (this.selectedCharacterName != Character.CharacterName.Purse && !Singleton.Instance.allowOtherCharacters)
		{
			string textByKey = LangaugeManager.main.GetTextByKey("demo1");
			menuManager.CreatePopUp(textByKey, Object.FindObjectOfType<Canvas>().transform.InverseTransformPoint(DigitalCursor.main.transform.position), 0.5f);
			return;
		}
		if (Singleton.Instance.storyMode)
		{
			Object.Instantiate<GameObject>(this.missionSelectorPrefab, base.transform.parent);
			return;
		}
		Object.Instantiate<GameObject>(this.runTypePrefab, base.transform.parent);
	}

	// Token: 0x0400017A RID: 378
	private Character.CharacterName selectedCharacterName;

	// Token: 0x0400017B RID: 379
	private Character selectedCharacter;

	// Token: 0x0400017C RID: 380
	private RunType selectedRunType;

	// Token: 0x0400017D RID: 381
	[SerializeField]
	private List<Character> characterList = new List<Character>();

	// Token: 0x0400017E RID: 382
	[SerializeField]
	private Animator characterSelectAnimator;

	// Token: 0x0400017F RID: 383
	[SerializeField]
	private Image characterPortrait;

	// Token: 0x04000180 RID: 384
	[SerializeField]
	private TextMeshProUGUI characterName;

	// Token: 0x04000181 RID: 385
	[SerializeField]
	private TextMeshProUGUI characterDescription;

	// Token: 0x04000182 RID: 386
	[SerializeField]
	private Transform costumeParent;

	// Token: 0x04000183 RID: 387
	[SerializeField]
	private GameObject costumePrefab;

	// Token: 0x04000184 RID: 388
	[SerializeField]
	private Transform targetTransform;

	// Token: 0x04000185 RID: 389
	[SerializeField]
	private Button goAdventuringButton;

	// Token: 0x04000186 RID: 390
	private Coroutine chooseCharacterCoroutine;

	// Token: 0x04000187 RID: 391
	private Transform chosenCostume;

	// Token: 0x04000188 RID: 392
	[SerializeField]
	private GameObject runTypePrefab;

	// Token: 0x04000189 RID: 393
	[SerializeField]
	private GameObject missionSelectorPrefab;
}
