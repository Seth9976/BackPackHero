using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class ModularBackpack : MonoBehaviour
{
	// Token: 0x06000682 RID: 1666 RVA: 0x0003F906 File Offset: 0x0003DB06
	private void Start()
	{
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0003F908 File Offset: 0x0003DB08
	private void Update()
	{
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0003F90C File Offset: 0x0003DB0C
	public static void SetAllBackpackSprites()
	{
		Player main = Player.main;
		ModularBackpack[] array = Object.FindObjectsOfType<ModularBackpack>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetBackpackSprites();
		}
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0003F93C File Offset: 0x0003DB3C
	public void SetBackpackSprites()
	{
		Character chosenCharacter = Player.main.chosenCharacter;
		if (!chosenCharacter)
		{
			return;
		}
		GridSquare[] componentsInChildren = base.transform.parent.GetComponentsInChildren<GridSquare>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetSprite(chosenCharacter.standardGridSprite);
		}
		ModularBackpack.BackpackPieces backpackPieces = chosenCharacter.backpackPieces;
		this.topRenderer.sprite = backpackPieces.topBackpackSprite;
		this.leftRenderer.sprite = backpackPieces.sideBackpackSprite;
		this.rightRenderer.sprite = backpackPieces.sideBackpackSprite;
		this.bottomRenderer.sprite = backpackPieces.bottomBackpackSprite;
		this.leftDecalRenderer.sprite = backpackPieces.leftDecalSprite;
		this.rightDecalRenderer.sprite = backpackPieces.rightDecalSprite;
		if (backpackPieces.bottomDecalSprite != null)
		{
			this.bottomDecalRenderer.sprite = backpackPieces.bottomDecalSprite;
		}
		this.backgroundRenderer.sprite = backpackPieces.backgroundSprite;
		if (chosenCharacter.decalPositions != null)
		{
			if (chosenCharacter.decalPositions.Length != 0)
			{
				this.leftDecalRenderer.transform.localPosition = chosenCharacter.decalPositions[0];
			}
			if (chosenCharacter.decalPositions.Length > 1)
			{
				this.rightDecalRenderer.transform.localPosition = chosenCharacter.decalPositions[1];
			}
			if (chosenCharacter.decalPositions.Length > 2)
			{
				this.bottomDecalRenderer.transform.localPosition = chosenCharacter.decalPositions[2];
			}
		}
	}

	// Token: 0x0400053D RID: 1341
	[SerializeField]
	public Transform gridParent;

	// Token: 0x0400053E RID: 1342
	[SerializeField]
	public SpriteRenderer topRenderer;

	// Token: 0x0400053F RID: 1343
	[SerializeField]
	public SpriteRenderer leftRenderer;

	// Token: 0x04000540 RID: 1344
	[SerializeField]
	public SpriteRenderer rightRenderer;

	// Token: 0x04000541 RID: 1345
	[SerializeField]
	public SpriteRenderer bottomRenderer;

	// Token: 0x04000542 RID: 1346
	[SerializeField]
	public SpriteRenderer leftDecalRenderer;

	// Token: 0x04000543 RID: 1347
	[SerializeField]
	public SpriteRenderer rightDecalRenderer;

	// Token: 0x04000544 RID: 1348
	[SerializeField]
	public SpriteRenderer bottomDecalRenderer;

	// Token: 0x04000545 RID: 1349
	[SerializeField]
	public SpriteRenderer backgroundRenderer;

	// Token: 0x02000313 RID: 787
	[Serializable]
	public class BackpackPieces
	{
		// Token: 0x0400124C RID: 4684
		[SerializeField]
		public Sprite topBackpackSprite;

		// Token: 0x0400124D RID: 4685
		[SerializeField]
		public Sprite sideBackpackSprite;

		// Token: 0x0400124E RID: 4686
		[SerializeField]
		public Sprite leftDecalSprite;

		// Token: 0x0400124F RID: 4687
		[SerializeField]
		public Sprite bottomBackpackSprite;

		// Token: 0x04001250 RID: 4688
		[SerializeField]
		public Sprite rightDecalSprite;

		// Token: 0x04001251 RID: 4689
		[SerializeField]
		public Sprite backgroundSprite;

		// Token: 0x04001252 RID: 4690
		[SerializeField]
		public Sprite bottomDecalSprite;
	}
}
