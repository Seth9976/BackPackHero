using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class ItemSpriteChanger : MonoBehaviour
{
	// Token: 0x06000845 RID: 2117 RVA: 0x00056D14 File Offset: 0x00054F14
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.myItem = base.GetComponent<Item2>();
		this.itemMovement = base.GetComponent<ItemMovement>();
		this.manaStone = null;
		this.usesLimit = null;
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00056D54 File Offset: 0x00054F54
	private void MakeReferences()
	{
		bool flag = this.mode == ItemSpriteChanger.SpriteChangerMode.Auto;
		if (this.usesLimit == null && !this.manaStone)
		{
			this.manaStone = base.GetComponent<ManaStone>();
			if (this.manaStone && (flag || this.mode == ItemSpriteChanger.SpriteChangerMode.ManaStone))
			{
				this.referencesFound = true;
				return;
			}
		}
		if (this.myItem)
		{
			foreach (Item2.LimitedUses limitedUses in this.myItem.usesLimits)
			{
				if ((limitedUses.type == Item2.LimitedUses.Type.total && (flag || this.mode == ItemSpriteChanger.SpriteChangerMode.UseLimitTotal)) || (limitedUses.type == Item2.LimitedUses.Type.perTurn && (flag || this.mode == ItemSpriteChanger.SpriteChangerMode.UseLimitTurn)) || (limitedUses.type == Item2.LimitedUses.Type.perCombat && (flag || this.mode == ItemSpriteChanger.SpriteChangerMode.UseLimitCombat)))
				{
					this.usesLimit = limitedUses;
					this.referencesFound = true;
					break;
				}
			}
		}
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00056E50 File Offset: 0x00055050
	private void Update()
	{
		if (!this.referencesFound || (this.usesLimit == null && this.manaStone == null))
		{
			this.MakeReferences();
			return;
		}
		int num = 0;
		if (this.usesLimit != null)
		{
			num = Mathf.RoundToInt(this.usesLimit.currentValue - 1f);
		}
		else if (this.manaStone)
		{
			num = this.manaStone.currentPower;
		}
		if (this.plusOne)
		{
			num++;
		}
		num = Mathf.Clamp(num, 0, this.sprites.Count - 1);
		if (this.spriteRenderer && this.spriteRenderer.sprite != this.sprites[num])
		{
			if (this.animator)
			{
				this.animator.enabled = false;
			}
			if (this.itemMovement && this.itemMovement.mousePreviewRenderer)
			{
				this.itemMovement.mousePreviewRenderer.sprite = this.sprites[num];
			}
			this.spriteRenderer.enabled = true;
			this.spriteRenderer.sprite = this.sprites[num];
		}
	}

	// Token: 0x04000662 RID: 1634
	[SerializeField]
	public List<Sprite> sprites;

	// Token: 0x04000663 RID: 1635
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000664 RID: 1636
	[SerializeField]
	private Animator animator;

	// Token: 0x04000665 RID: 1637
	[HideInInspector]
	private Item2 myItem;

	// Token: 0x04000666 RID: 1638
	[HideInInspector]
	private ItemMovement itemMovement;

	// Token: 0x04000667 RID: 1639
	[HideInInspector]
	private Item2.LimitedUses usesLimit;

	// Token: 0x04000668 RID: 1640
	[SerializeField]
	private bool plusOne;

	// Token: 0x04000669 RID: 1641
	[SerializeField]
	private ManaStone manaStone;

	// Token: 0x0400066A RID: 1642
	[SerializeField]
	public ItemSpriteChanger.SpriteChangerMode mode;

	// Token: 0x0400066B RID: 1643
	[SerializeField]
	private bool referencesFound;

	// Token: 0x02000366 RID: 870
	public enum SpriteChangerMode
	{
		// Token: 0x04001452 RID: 5202
		[Description("Automatically determines the mode")]
		Auto,
		// Token: 0x04001453 RID: 5203
		[Description("Change Sprites based on Use Limit Total")]
		UseLimitTotal,
		// Token: 0x04001454 RID: 5204
		[Description("Change Sprites based on Use Limit Per Turn")]
		UseLimitTurn,
		// Token: 0x04001455 RID: 5205
		[Description("Change Sprites based on Use Limit Per Combat")]
		UseLimitCombat,
		// Token: 0x04001456 RID: 5206
		[Description("Change Sprites based on Manastone Power")]
		ManaStone
	}
}
