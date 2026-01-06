using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class CurseManager : MonoBehaviour
{
	// Token: 0x06000510 RID: 1296 RVA: 0x00031D27 File Offset: 0x0002FF27
	private void Awake()
	{
		if (CurseManager.Instance == null)
		{
			CurseManager.Instance = this;
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00031D48 File Offset: 0x0002FF48
	private void OnDestroy()
	{
		if (CurseManager.Instance == this)
		{
			CurseManager.Instance = null;
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00031D60 File Offset: 0x0002FF60
	public void RemoveAllCurses()
	{
		foreach (Item2 item in Item2.GetItemsOfTypes(Item2.ItemType.Curse, Item2.allItems))
		{
			item.Cleanse();
		}
		if (Tote.main)
		{
			foreach (Item2 item2 in Item2.GetItemsOfTypes(Item2.ItemType.Curse, Tote.main.GetAllCarvings()))
			{
				item2.Cleanse();
			}
		}
		int num = this.CursesStored();
		while (base.transform.childCount > 0)
		{
			GameObject gameObject = base.transform.GetChild(0).gameObject;
			gameObject.transform.SetParent(null);
			Object.Destroy(gameObject);
		}
		if (GameManager.main)
		{
			GameManager.main.ChangeCurse(num * -1);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00031E60 File Offset: 0x00030060
	public int CursesStored()
	{
		return base.transform.childCount;
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00031E6D File Offset: 0x0003006D
	public void AddCurse(GameObject curse)
	{
		curse.transform.SetParent(base.transform);
		curse.gameObject.SetActive(false);
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00031E8C File Offset: 0x0003008C
	public void RemoveACurse()
	{
		if (base.transform.childCount == 0)
		{
			return;
		}
		GameObject gameObject = base.transform.GetChild(0).gameObject;
		gameObject.transform.SetParent(null);
		Object.Destroy(gameObject);
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00031EC0 File Offset: 0x000300C0
	public GameObject GetCurse()
	{
		if (base.transform.childCount == 0)
		{
			return null;
		}
		GameObject gameObject = base.transform.GetChild(0).gameObject;
		Transform itemsParent = GameManager.main.itemsParent;
		gameObject.SetActive(true);
		gameObject.transform.SetParent(itemsParent);
		SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
		component.enabled = true;
		component.color = Color.white;
		Item2 component2 = gameObject.GetComponent<Item2>();
		gameObject.transform.position = new Vector3(-99f, -99f, 0f);
		component2.SetColor();
		return gameObject;
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00031F50 File Offset: 0x00030150
	public List<GameObject> GetAllCurses()
	{
		List<GameObject> list = new List<GameObject>();
		while (this.CursesStored() > 0)
		{
			GameObject curse = this.GetCurse();
			if (curse)
			{
				list.Add(curse);
			}
		}
		GameManager.main.ChangeCurse(-1 * list.Count);
		return list;
	}

	// Token: 0x040003D7 RID: 983
	public static CurseManager Instance;
}
