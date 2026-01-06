using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class BuildNPCsUIList : MonoBehaviour
{
	// Token: 0x0600001E RID: 30 RVA: 0x00002A1D File Offset: 0x00000C1D
	private void Start()
	{
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002A1F File Offset: 0x00000C1F
	private void Update()
	{
		if (Input.GetMouseButtonDown(1) || DigitalCursor.main.GetInputDown("cancel"))
		{
			this.DisableList();
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002A40 File Offset: 0x00000C40
	public void DisableList()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002A50 File Offset: 0x00000C50
	public void BuildList()
	{
		Transform transform = null;
		foreach (object obj in this.parent)
		{
			Transform transform2 = (Transform)obj;
			if (transform2.CompareTag("Ignorable"))
			{
				transform = transform2;
			}
			else
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		List<Overworld_NPC> list = new List<Overworld_NPC>(Overworld_NPC.npcs);
		list.Sort((Overworld_NPC x, Overworld_NPC y) => x.name.CompareTo(y.name));
		foreach (Overworld_NPC overworld_NPC in list)
		{
			if (overworld_NPC.portraitSprite)
			{
				Object.Instantiate<GameObject>(this.npcUIStandInPrefab, this.parent).GetComponent<Overworld_NPC_UI_Button>().Setup(overworld_NPC);
			}
		}
		if (transform != null)
		{
			transform.SetAsLastSibling();
		}
	}

	// Token: 0x0400000D RID: 13
	[SerializeField]
	private Transform parent;

	// Token: 0x0400000E RID: 14
	[SerializeField]
	private GameObject npcUIStandInPrefab;
}
