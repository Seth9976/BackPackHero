using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class Carving : MonoBehaviour
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00043923 File Offset: 0x00041B23
	// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0004392B File Offset: 0x00041B2B
	public Item2 item { get; private set; }

	// Token: 0x060006F5 RID: 1781 RVA: 0x00043934 File Offset: 0x00041B34
	public void AddToDeckAfterDelay()
	{
		if (this.addCardToDeckCoroutine != null)
		{
			base.StopCoroutine(this.addCardToDeckCoroutine);
		}
		this.addCardToDeckCoroutine = base.StartCoroutine(this.AddCardToDeckAfterDelay());
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0004395C File Offset: 0x00041B5C
	private IEnumerator AddCardToDeckAfterDelay()
	{
		yield return new WaitForSeconds(0.5f);
		Tote.main.AddNewCarvingToUndrawn(base.gameObject);
		this.addCardToDeckCoroutine = null;
		yield break;
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0004396C File Offset: 0x00041B6C
	public void Start()
	{
		this.tote = Object.FindObjectOfType<Tote>();
		this.itemMovement = base.GetComponent<ItemMovement>();
		this.item = base.GetComponent<Item2>();
		this.inventoryTransform = GameObject.FindGameObjectWithTag("Inventory").transform;
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x000439C0 File Offset: 0x00041BC0
	public void ResetFirstFrames()
	{
		if (!this.itemMovement)
		{
			this.itemMovement = base.GetComponent<ItemMovement>();
		}
		base.GetComponent<SpriteRenderer>().enabled = true;
		this.firstFrames = 0.1f;
		this.itemMovement.StopAllCoroutines();
		this.itemMovement.inGrid = false;
		this.itemMovement.isAnimating = false;
		this.itemMovement.isTransiting = false;
		this.itemMovement.isPlayedCard = false;
		this.itemMovement.mousePreview.gameObject.SetActive(false);
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x00043A5E File Offset: 0x00041C5E
	private void OnDestroy()
	{
		this.RemoveUI();
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x00043A68 File Offset: 0x00041C68
	public void RemoveUI()
	{
		if (!this.myUIlocation)
		{
			return;
		}
		UICarvingSpacer component = this.myUIlocation.gameObject.GetComponent<UICarvingSpacer>();
		if (component)
		{
			component.Remove();
		}
		this.myUIlocation = null;
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00043AAC File Offset: 0x00041CAC
	private void Update()
	{
		if (!this.tote)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (!this.tote.IsDrawn(base.gameObject) && !this.moveToCardViewer)
		{
			this.spriteRenderer.transform.position = Vector3.one * 999f;
		}
		if (!this.tote.IsDrawn(base.gameObject) && !this.moveToCardViewer && this.item && this.itemMovement && this.item.setMyColor == null)
		{
			this.itemMovement.StopAllMovements();
			base.gameObject.SetActive(false);
		}
		if (this.item.destroyed)
		{
			this.RemoveUI();
		}
		if (!this.spriteRenderer.isVisible && !this.itemMovement.mousePreviewRenderer.isVisible && (!this.itemMovement || !this.itemMovement.inGrid))
		{
			base.transform.rotation = Quaternion.identity;
		}
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00043BC1 File Offset: 0x00041DC1
	public void MoveToCardViewer()
	{
		base.transform.position = new Vector3(-999f, -999f, 0f);
		this.ResetFirstFrames();
		this.moveToCardViewer = true;
		this.item.SetColor();
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x00043BFC File Offset: 0x00041DFC
	public void EndMoveToCardViewer()
	{
		this.ResetFirstFrames();
		base.transform.position = new Vector3(-999f, -999f, 0f);
		this.moveToCardViewer = false;
		this.spriteRenderer.sortingOrder = 0;
		this.spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00043C50 File Offset: 0x00041E50
	private void LateUpdate()
	{
		if (this.firstFrames > 0f || this.moveToCardViewer)
		{
			this.firstFrames -= Time.deltaTime;
			return;
		}
		if (this.itemMovement.isDragging || this.itemMovement.isPlayedCard || !this.myUIlocation)
		{
			return;
		}
		base.transform.position = new Vector3(this.myUIlocation.position.x, this.myUIlocation.position.y, -4f);
	}

	// Token: 0x04000596 RID: 1430
	private Carving.Status status;

	// Token: 0x04000597 RID: 1431
	public Transform myUIlocation;

	// Token: 0x04000598 RID: 1432
	private ItemMovement itemMovement;

	// Token: 0x0400059A RID: 1434
	private Transform inventoryTransform;

	// Token: 0x0400059B RID: 1435
	private float firstFrames = 0.1f;

	// Token: 0x0400059C RID: 1436
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400059D RID: 1437
	private Tote tote;

	// Token: 0x0400059E RID: 1438
	private CanvasGroup canvasGroup;

	// Token: 0x0400059F RID: 1439
	[SerializeField]
	public List<Item2.Cost> summoningCosts;

	// Token: 0x040005A0 RID: 1440
	private Coroutine addCardToDeckCoroutine;

	// Token: 0x040005A1 RID: 1441
	public bool moveToCardViewer;

	// Token: 0x0200031F RID: 799
	public enum Status
	{
		// Token: 0x04001271 RID: 4721
		drawnInCombat,
		// Token: 0x04001272 RID: 4722
		drawnOutofCombat
	}
}
