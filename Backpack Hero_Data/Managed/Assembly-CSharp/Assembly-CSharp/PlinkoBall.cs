using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class PlinkoBall : MonoBehaviour
{
	// Token: 0x060005EC RID: 1516 RVA: 0x0003A501 File Offset: 0x00038701
	private void Start()
	{
		this.randomEventMaster = base.GetComponentInParent<RandomEventMaster>();
		this.gameManager = GameManager.main;
		this.rigidbody2D = base.GetComponent<Rigidbody2D>();
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0003A528 File Offset: 0x00038728
	private void FixedUpdate()
	{
		if (this.gottenItem)
		{
			return;
		}
		if (this.rigidbody2D.velocity.magnitude <= 0.01f && this.dropped)
		{
			this.rigidbody2D.velocity = new Vector2(Random.Range(-3f, 3f), 6f);
		}
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(base.transform.position, Vector3.forward))
		{
			if (raycastHit2D.collider.gameObject.CompareTag("Dungeon Event"))
			{
				UICarvingIndicator componentInChildren = raycastHit2D.collider.GetComponentInChildren<UICarvingIndicator>();
				if (componentInChildren)
				{
					Item2 item = componentInChildren.GetItem();
					if (item)
					{
						if (Item2.ShareItemTypes(item.itemType, new List<Item2.ItemType> { Item2.ItemType.Curse }))
						{
							SoundManager.main.PlaySFX("miniGameBad");
						}
						else
						{
							SoundManager.main.PlaySFX("miniGameGood");
						}
						ItemSpawner.InstantiateItemFree(item);
						this.gottenItem = true;
						this.randomEventMaster.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.finished, 0);
					}
				}
			}
		}
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0003A660 File Offset: 0x00038860
	private void OnCollisionEnter2D(Collision2D col)
	{
		SoundManager.main.PlaySFX("menuBlip");
	}

	// Token: 0x040004B9 RID: 1209
	public bool gottenItem;

	// Token: 0x040004BA RID: 1210
	public bool dropped;

	// Token: 0x040004BB RID: 1211
	private GameManager gameManager;

	// Token: 0x040004BC RID: 1212
	private Rigidbody2D rigidbody2D;

	// Token: 0x040004BD RID: 1213
	private RandomEventMaster randomEventMaster;
}
