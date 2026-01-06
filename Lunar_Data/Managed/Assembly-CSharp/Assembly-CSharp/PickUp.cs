using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class PickUp : MonoBehaviour
{
	// Token: 0x060002EA RID: 746 RVA: 0x0000EE4A File Offset: 0x0000D04A
	private void OnEnable()
	{
		PickUp.pickups.Add(this);
		this.pickUpDelay = 0.5f;
		this.canBePickedUp = true;
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0000EE69 File Offset: 0x0000D069
	private void OnDisable()
	{
		PickUp.pickups.Remove(this);
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0000EE77 File Offset: 0x0000D077
	private void Start()
	{
		base.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0000EE98 File Offset: 0x0000D098
	private void Update()
	{
		if (this.isBeingPickedUp)
		{
			if (this.simpleAnimator && this.simpleAnimator.enabled)
			{
				this.simpleAnimator.enabled = false;
				this.spriteRenderer.sprite = this.pickupSprite;
			}
			this.col.enabled = false;
			this.pickUpTime += Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, Player.instance.transform.position, -14f * Time.deltaTime);
			base.transform.position = Vector2.MoveTowards(base.transform.position, Player.instance.transform.position, this.pickUpTime * 24f * Time.deltaTime);
			if (Vector2.Distance(base.transform.position, Player.instance.transform.position) < 0.1f)
			{
				this.Interact();
			}
		}
		if (this.pickUpDelay > 0f)
		{
			this.pickUpDelay -= Time.deltaTime;
			if (this.pickUpDelay <= 0f)
			{
				base.gameObject.layer = LayerMask.NameToLayer("PickUp");
			}
		}
		base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, Vector3.one, 15f * Time.deltaTime);
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0000F028 File Offset: 0x0000D228
	public static void RemoveAllPickups()
	{
		foreach (PickUp pickUp in new List<PickUp>(PickUp.pickups))
		{
			Object.Destroy(pickUp.gameObject);
		}
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0000F084 File Offset: 0x0000D284
	public void Get()
	{
		if (!this.canBePickedUp)
		{
			return;
		}
		if (this.pickUpDelay > 0f)
		{
			return;
		}
		using (List<PickUp.Condition>.Enumerator enumerator = this.conditions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == PickUp.Condition.LessThanFullHealth && HealthBarMaster.instance.IsFullHealth())
				{
					return;
				}
			}
		}
		this.saleForAmount = 0;
		this.canBePickedUp = false;
		this.isBeingPickedUp = true;
		this.pickUpDelay = 0.5f;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000F118 File Offset: 0x0000D318
	private void Interact()
	{
		this.canBePickedUp = true;
		this.isBeingPickedUp = false;
		this.pickUpTime = 0f;
		switch (this.pickupType)
		{
		case PickUp.PickupType.NewCard:
			GameManager.instance.StartChooseCard();
			break;
		case PickUp.PickupType.NewRelic:
			GameManager.instance.StartChooseRelic();
			break;
		case PickUp.PickupType.XP:
			XPManager.instance.AddXP(1);
			break;
		case PickUp.PickupType.Health:
			HealthBarMaster.instance.Heal(15f);
			break;
		}
		this.Explode();
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000F197 File Offset: 0x0000D397
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			this.Get();
		}
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
	public void Explode()
	{
		if (!this.destroyParticles || !this.spriteRenderer)
		{
			return;
		}
		this.destroyParticles.main.startColor = new ParticleSystem.MinMaxGradient(this.spriteRenderer.color);
		ParticleSystem.ShapeModule shape = this.destroyParticles.shape;
		shape.sprite = this.spriteRenderer.sprite;
		shape.texture = this.spriteRenderer.sprite.texture;
		this.destroyParticles.gameObject.SetActive(true);
		this.destroyParticles.transform.SetParent(null);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000232 RID: 562
	public List<PickUp.Condition> conditions = new List<PickUp.Condition>();

	// Token: 0x04000233 RID: 563
	public PickUp.PickupType pickupType;

	// Token: 0x04000234 RID: 564
	public int commonality = 1;

	// Token: 0x04000235 RID: 565
	public static List<PickUp> pickups = new List<PickUp>();

	// Token: 0x04000236 RID: 566
	[SerializeField]
	public string itemName;

	// Token: 0x04000237 RID: 567
	[SerializeField]
	public string description;

	// Token: 0x04000238 RID: 568
	[Header("------------References------------")]
	public int saleForAmount;

	// Token: 0x04000239 RID: 569
	[SerializeField]
	private Sprite pickupSprite;

	// Token: 0x0400023A RID: 570
	[SerializeField]
	private SimpleAnimator simpleAnimator;

	// Token: 0x0400023B RID: 571
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400023C RID: 572
	[SerializeField]
	private Collider2D col;

	// Token: 0x0400023D RID: 573
	[SerializeField]
	private ParticleSystem destroyParticles;

	// Token: 0x0400023E RID: 574
	[SerializeField]
	private bool isBeingPickedUp;

	// Token: 0x0400023F RID: 575
	[SerializeField]
	private float pickUpTime;

	// Token: 0x04000240 RID: 576
	private float pickUpDelay = 0.5f;

	// Token: 0x04000241 RID: 577
	private bool canBePickedUp = true;

	// Token: 0x020000F3 RID: 243
	public enum Condition
	{
		// Token: 0x04000480 RID: 1152
		LessThanFullHealth
	}

	// Token: 0x020000F4 RID: 244
	public enum PickupType
	{
		// Token: 0x04000482 RID: 1154
		NewCard,
		// Token: 0x04000483 RID: 1155
		NewRelic,
		// Token: 0x04000484 RID: 1156
		XP,
		// Token: 0x04000485 RID: 1157
		Health
	}
}
