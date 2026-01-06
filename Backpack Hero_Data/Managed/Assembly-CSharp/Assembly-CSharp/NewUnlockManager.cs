using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class NewUnlockManager : MonoBehaviour
{
	// Token: 0x06000258 RID: 600 RVA: 0x0000E917 File Offset: 0x0000CB17
	private void Awake()
	{
		if (!NewUnlockManager.main)
		{
			NewUnlockManager.main = this;
			return;
		}
		Object.Destroy(this);
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000E932 File Offset: 0x0000CB32
	private void OnDestroy()
	{
		if (NewUnlockManager.main == this)
		{
			NewUnlockManager.main = null;
		}
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000E947 File Offset: 0x0000CB47
	private void Start()
	{
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000E94C File Offset: 0x0000CB4C
	public GameObject OpenNewSimpleImageWindow(Sprite s, string unlockName, string cardName, string text)
	{
		Transform transform = this.FindCanvas();
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		gameObject.GetComponent<NewItemWindow>().SetSimpleImageCard(s, unlockName, cardName, text);
		return gameObject;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000E97C File Offset: 0x0000CB7C
	public GameObject OpenNewLoreWindow(string lore)
	{
		Transform transform = this.FindCanvas();
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetLore(lore);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
	public GameObject OpenNewConstructionWindow(Overworld_Structure structure)
	{
		Transform transform = this.FindCanvas();
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetBuilding(structure);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000E9EC File Offset: 0x0000CBEC
	public GameObject OpenNewMissionWindow(Missions m)
	{
		Transform transform = this.FindCanvas();
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetMission(m);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000EA24 File Offset: 0x0000CC24
	public GameObject OpenNewCharacterWindow(Character character)
	{
		Transform transform = this.FindCanvas();
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetCharacter(character);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000EA5C File Offset: 0x0000CC5C
	public GameObject OpenNewCostumeWindow(RuntimeAnimatorController controller)
	{
		Transform transform = this.FindCanvas();
		GameObject gameObject = Object.Instantiate<GameObject>(this.newCostumeWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetCostume(controller);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000EA94 File Offset: 0x0000CC94
	public GameObject OpenNewItemsWindow(List<Item2> item)
	{
		Transform transform = this.FindCanvas();
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetItems(item);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000EACB File Offset: 0x0000CCCB
	public void OpenNewItemWindow(Item2 item)
	{
		this.OpenNewItemsWindow(new List<Item2> { item });
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
	private Transform FindCanvas()
	{
		Transform transform = GameObject.FindGameObjectWithTag("SecondaryCanvas").transform;
		if (transform)
		{
			return transform;
		}
		transform = GameObject.FindGameObjectWithTag("UI Canvas").transform;
		if (transform)
		{
			return transform;
		}
		Canvas canvas = Object.FindObjectOfType<Canvas>();
		if (canvas)
		{
			return canvas.transform;
		}
		return null;
	}

	// Token: 0x04000197 RID: 407
	public static NewUnlockManager main;

	// Token: 0x04000198 RID: 408
	[SerializeField]
	private GameObject newItemWindowPrefab;

	// Token: 0x04000199 RID: 409
	[SerializeField]
	private GameObject newCostumeWindowPrefab;
}
