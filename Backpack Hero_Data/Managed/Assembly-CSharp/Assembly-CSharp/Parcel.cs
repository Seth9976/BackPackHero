using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class Parcel : MonoBehaviour
{
	// Token: 0x060002DF RID: 735 RVA: 0x00010F34 File Offset: 0x0000F134
	private IEnumerator PlaySFX(string sfx)
	{
		for (;;)
		{
			SoundManager.main.PlaySFX(sfx);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00010F43 File Offset: 0x0000F143
	private void Awake()
	{
		if (!Parcel.main)
		{
			Parcel.main = this;
		}
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x00010F58 File Offset: 0x0000F158
	private void Start()
	{
		if (Parcel.main && Parcel.main != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		this.chestPouch.enabled = false;
		this.inputHandler.enabled = false;
		this.text.SetActive(false);
		MetaProgressSaveManager.main.AddRunEvent(MetaProgressSaveManager.LastRun.RunEvents.metParcel);
		this.sfxCoroutine = base.StartCoroutine(this.PlaySFX("footstep"));
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00010FD0 File Offset: 0x0000F1D0
	private void Update()
	{
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00010FD2 File Offset: 0x0000F1D2
	public void StopFootsteps()
	{
		base.StopCoroutine(this.sfxCoroutine);
		SoundManager.main.PlaySFX("dodge");
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00010FF0 File Offset: 0x0000F1F0
	public void ParcelActivate()
	{
		this.chestPouch.enabled = true;
		this.inputHandler.enabled = true;
		this.text.transform.position = new Vector3(-999f, -999f, 0f);
		this.text.SetActive(true);
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00011045 File Offset: 0x0000F245
	public void HideText(bool hide)
	{
		this.text.SetActive(false);
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00011054 File Offset: 0x0000F254
	public void Goodbye()
	{
		ChestPouch component = base.GetComponent<ChestPouch>();
		if (component)
		{
			component.GoodBye();
			component.enabled = false;
		}
		if (base.GetComponent<ItemPouch>().itemsInside.Count > 0 && this.dungeonEvent)
		{
			this.dungeonEvent.FinishEvent();
		}
		this.positionAnimator.Play("dungeonEventDespawn");
		this.inputHandler.enabled = false;
		this.HideText(true);
		this.spriteAnimator.Play("RunNoStop");
		this.sfxCoroutine = base.StartCoroutine(this.PlaySFX("footstep"));
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x000110F4 File Offset: 0x0000F2F4
	private void OnDestroy()
	{
		if (Parcel.main == this)
		{
			Parcel.main = null;
		}
		ItemPouch component = base.GetComponent<ItemPouch>();
		if (component && component.itemsInside.Count > 0)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.storedAnItemWithParcel);
			MetaProgressSaveManager.main.StoreItems(component.itemsInside.ConvertAll<Item2>((GameObject item) => item.GetComponent<Item2>()));
		}
	}

	// Token: 0x040001DB RID: 475
	[SerializeField]
	private InputHandler inputHandler;

	// Token: 0x040001DC RID: 476
	[SerializeField]
	private GameObject text;

	// Token: 0x040001DD RID: 477
	[SerializeField]
	private Animator positionAnimator;

	// Token: 0x040001DE RID: 478
	[SerializeField]
	private Animator spriteAnimator;

	// Token: 0x040001DF RID: 479
	[SerializeField]
	private ChestPouch chestPouch;

	// Token: 0x040001E0 RID: 480
	public DungeonEvent dungeonEvent;

	// Token: 0x040001E1 RID: 481
	private Coroutine sfxCoroutine;

	// Token: 0x040001E2 RID: 482
	public static Parcel main;
}
