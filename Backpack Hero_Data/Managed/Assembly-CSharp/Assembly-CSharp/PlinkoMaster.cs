using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class PlinkoMaster : MonoBehaviour
{
	// Token: 0x060005F0 RID: 1520 RVA: 0x0003A67C File Offset: 0x0003887C
	private void Start()
	{
		this.randomEventMaster = base.GetComponent<RandomEventMaster>();
		this.gameManager = GameManager.main;
		base.StartCoroutine(this.Setup());
		if (this.randomEventMaster.npc)
		{
			BoxCollider2D[] componentsInChildren = this.randomEventMaster.npc.GetComponentsInChildren<BoxCollider2D>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = false;
			}
		}
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0003A6E7 File Offset: 0x000388E7
	private IEnumerator Setup()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		new List<GameObject>();
		this.preSpawnedITems = new List<GameObject>();
		List<Item2> item2s = this.randomEventMaster.dungeonEvent.GetItem2s();
		if (item2s.Count == 0)
		{
			item2s.AddRange(ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Curse }, true, true));
			item2s.AddRange(ItemSpawner.GetItems(6 - item2s.Count, new List<Item2.ItemType> { Item2.ItemType.Any }, true, false));
			this.randomEventMaster.dungeonEvent.StoreItem2s(item2s);
		}
		UICarvingIndicator[] componentsInChildren = base.GetComponentsInChildren<UICarvingIndicator>();
		int num = 0;
		foreach (UICarvingIndicator uicarvingIndicator in componentsInChildren)
		{
			if (num >= item2s.Count)
			{
				break;
			}
			uicarvingIndicator.Setup(item2s[num]);
			num++;
		}
		DigitalCursor.main.FollowGameElement(this.plinkoBall.transform, true);
		yield break;
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0003A6F8 File Offset: 0x000388F8
	private void Update()
	{
		float y = this.plinkoBall.transform.position.y;
		if (!this.dropped)
		{
			if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
			{
				Vector3 position = DigitalCursor.main.transform.position;
				position.z = 10f;
				this.plinkoBall.transform.position = position;
			}
			else if (Vector2.Distance(this.plinkoBall.transform.position, DigitalCursor.main.transform.position) < 1f && (DigitalCursor.main.moveFreeVector.magnitude > 0.1f || DigitalCursor.main.moveLockVector.magnitude > 0.1f))
			{
				this.plinkoBall.transform.position += DigitalCursor.main.moveFreeVector * Time.deltaTime * 3.5f;
				this.plinkoBall.transform.position += DigitalCursor.main.moveLockVector * Time.deltaTime * 3.5f;
			}
			this.plinkoBall.transform.position = new Vector3(Mathf.Clamp(this.plinkoBall.transform.position.x, -4.4f, 4.4f), y, this.plinkoBall.transform.position.z);
			if ((Input.GetMouseButtonDown(0) || DigitalCursor.main.GetInputDown("confirm")) && Vector2.Distance(this.plinkoBall.transform.position, DigitalCursor.main.transform.position) < 1f)
			{
				DigitalCursorInterface componentInParent = base.GetComponentInParent<DigitalCursorInterface>();
				if (componentInParent)
				{
					componentInParent.SetCursorVisiblity(false);
				}
				this.dropped = true;
				this.plinkoBallScript.dropped = true;
				Rigidbody2D component = this.plinkoBall.GetComponent<Rigidbody2D>();
				component.velocity = new Vector2(0f, -2f);
				component.gravityScale = 0.9f;
			}
		}
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0003A940 File Offset: 0x00038B40
	public void EndEvent()
	{
		SoundManager.main.PlaySFX("menuBlip");
		if (this.dropped && !this.plinkoBallScript.gottenItem)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("mini3"));
			return;
		}
		if (this.randomEventMaster.finished)
		{
			return;
		}
		this.randomEventMaster.dungeonEvent.StoreItems(this.preSpawnedITems);
		this.gameManager.SetAllSpritesToLayer0();
		this.gameManager.viewingEvent = false;
		this.randomEventMaster.finished = true;
		this.eventBoxAnimator.Play("Out");
		base.GetComponent<RandomEventMaster>().npc.isOpen = false;
		if (this.randomEventMaster.npc)
		{
			BoxCollider2D[] componentsInChildren = this.randomEventMaster.npc.GetComponentsInChildren<BoxCollider2D>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = true;
			}
		}
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		this.gameManager.SetAllItemColliders(true);
	}

	// Token: 0x040004BE RID: 1214
	[SerializeField]
	private Transform itemsPositionParent;

	// Token: 0x040004BF RID: 1215
	[SerializeField]
	private GameObject plinkoBall;

	// Token: 0x040004C0 RID: 1216
	[SerializeField]
	private PlinkoBall plinkoBallScript;

	// Token: 0x040004C1 RID: 1217
	[SerializeField]
	private Animator eventBoxAnimator;

	// Token: 0x040004C2 RID: 1218
	private GameManager gameManager;

	// Token: 0x040004C3 RID: 1219
	private RandomEventMaster randomEventMaster;

	// Token: 0x040004C4 RID: 1220
	private List<GameObject> preSpawnedITems;

	// Token: 0x040004C5 RID: 1221
	private bool dropped;
}
