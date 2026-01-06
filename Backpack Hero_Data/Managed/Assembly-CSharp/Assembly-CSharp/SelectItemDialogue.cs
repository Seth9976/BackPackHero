using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class SelectItemDialogue : MonoBehaviour
{
	// Token: 0x0600102F RID: 4143 RVA: 0x0009C053 File Offset: 0x0009A253
	private void Start()
	{
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x0009C055 File Offset: 0x0009A255
	private void Update()
	{
		if (this.eventBoxAnimator && !this.eventBoxAnimator.gameObject.activeInHierarchy && this.finished)
		{
			Object.Destroy(base.gameObject);
			return;
		}
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x0009C08A File Offset: 0x0009A28A
	public void SetupType(SelectItemDialogue.Type type)
	{
		this.type = type;
	}

	// Token: 0x06001032 RID: 4146 RVA: 0x0009C093 File Offset: 0x0009A293
	public void SetupName(string name)
	{
		this.titleText.text = name;
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x0009C0A4 File Offset: 0x0009A2A4
	public void SetupItems(List<Item2> items, bool skippable = false)
	{
		if (skippable)
		{
			this.doneButton.SetActive(true);
		}
		else
		{
			this.doneButton.SetActive(false);
		}
		foreach (Item2 item in items)
		{
			Object.Instantiate<GameObject>(this.itemUIStandIn, this.itemParent).GetComponent<UICarvingIndicator>().Setup(item);
		}
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x0009C124 File Offset: 0x0009A324
	public void ChooseItem(GameObject item)
	{
		if (this.type == SelectItemDialogue.Type.AddItemToCurses)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(item, new Vector3(-999f, -999f, 0f), Quaternion.identity);
			CurseManager.Instance.AddCurse(gameObject);
			GameManager.main.ChangeCurse(1);
		}
		this.EndEvent();
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0009C178 File Offset: 0x0009A378
	public void EndEvent()
	{
		if (this.finished)
		{
			return;
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("PopUpSpriteMask");
		if (gameObject)
		{
			gameObject.GetComponent<SpriteMask>().enabled = false;
		}
		GameManager.main.viewingEvent = false;
		this.finished = true;
		if (this.eventBoxAnimator)
		{
			this.eventBoxAnimator.Play("Out");
		}
	}

	// Token: 0x04000D48 RID: 3400
	[SerializeField]
	private SelectItemDialogue.Type type;

	// Token: 0x04000D49 RID: 3401
	[SerializeField]
	private TextMeshProUGUI titleText;

	// Token: 0x04000D4A RID: 3402
	[SerializeField]
	private Transform itemParent;

	// Token: 0x04000D4B RID: 3403
	[SerializeField]
	private GameObject itemUIStandIn;

	// Token: 0x04000D4C RID: 3404
	[SerializeField]
	private GameObject doneButton;

	// Token: 0x04000D4D RID: 3405
	[SerializeField]
	private Animator eventBoxAnimator;

	// Token: 0x04000D4E RID: 3406
	private bool finished;

	// Token: 0x0200046C RID: 1132
	public enum Type
	{
		// Token: 0x04001A27 RID: 6695
		AddItemToCurses
	}
}
