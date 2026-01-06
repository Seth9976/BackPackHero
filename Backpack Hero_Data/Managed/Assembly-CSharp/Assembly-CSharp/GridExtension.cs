using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class GridExtension : CustomInputHandler
{
	// Token: 0x06000641 RID: 1601 RVA: 0x0003D8D7 File Offset: 0x0003BAD7
	private void Awake()
	{
		if (GridExtension.gridExtensions == null)
		{
			GridExtension.gridExtensions = new List<GridExtension>();
		}
		GridExtension.gridExtensions.Add(this);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0003D8F5 File Offset: 0x0003BAF5
	private void OnDestroy()
	{
		GridExtension.gridExtensions.Remove(this);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0003D903 File Offset: 0x0003BB03
	private void Start()
	{
		this.levelUpManager = Object.FindObjectOfType<LevelUpManager>();
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.player = Player.main;
		this.gridSprite = this.player.chosenCharacter.standardGridSprite;
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0003D940 File Offset: 0x0003BB40
	private void Update()
	{
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -7f);
		if (this.selected)
		{
			base.transform.localScale = Vector3.one * Mathf.Lerp(1f, 1.2f, Mathf.Abs(1f - this.levelUpManager.time) / 1f);
			return;
		}
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0003D9DB File Offset: 0x0003BBDB
	public void MakeRunic()
	{
		base.GetComponentInChildren<SpriteRenderer>().color = new Color(0.46f, 1f, 0.71f, 1f);
		this.runicType = GridExtension.RunicType.onGridSpace;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0003DA08 File Offset: 0x0003BC08
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.Click();
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0003DA1D File Offset: 0x0003BC1D
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "confirm" || overrideKeyName)
		{
			this.Click();
		}
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0003DA34 File Offset: 0x0003BC34
	private void Click()
	{
		if (this.selected)
		{
			this.spriteRenderer.color = Color.white;
			this.spriteRenderer.sprite = this.selectSprite;
			this.selected = false;
			this.levelUpManager.Deselect();
			SoundManager.main.PlaySFX("weakHit");
			return;
		}
		if (this.levelUpManager.numberToUnlock <= 0)
		{
			SoundManager.main.PlaySFX("negative");
			return;
		}
		this.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
		this.spriteRenderer.sprite = this.gridSprite;
		this.selected = true;
		this.levelUpManager.Select();
		if (Random.Range(0, 2) == 0)
		{
			SoundManager.main.PlaySFX("levelUp1");
			return;
		}
		SoundManager.main.PlaySFX("levelUp2");
	}

	// Token: 0x0400050C RID: 1292
	public static List<GridExtension> gridExtensions = new List<GridExtension>();

	// Token: 0x0400050D RID: 1293
	[SerializeField]
	private Sprite gridSprite;

	// Token: 0x0400050E RID: 1294
	[SerializeField]
	private Sprite selectSprite;

	// Token: 0x0400050F RID: 1295
	[SerializeField]
	private Sprite arrowSprite;

	// Token: 0x04000510 RID: 1296
	[SerializeField]
	private Sprite runeSprite;

	// Token: 0x04000511 RID: 1297
	public bool isConnected;

	// Token: 0x04000512 RID: 1298
	public bool selected;

	// Token: 0x04000513 RID: 1299
	public GridExtension.RunicType runicType;

	// Token: 0x04000514 RID: 1300
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000515 RID: 1301
	private LevelUpManager levelUpManager;

	// Token: 0x04000516 RID: 1302
	private Player player;

	// Token: 0x0200030F RID: 783
	public enum RunicType
	{
		// Token: 0x0400123D RID: 4669
		noSpaceHere,
		// Token: 0x0400123E RID: 4670
		selectedOnce,
		// Token: 0x0400123F RID: 4671
		isRunicNotOnGrid,
		// Token: 0x04001240 RID: 4672
		onGridSpace,
		// Token: 0x04001241 RID: 4673
		isRunicOnGridSpace
	}
}
