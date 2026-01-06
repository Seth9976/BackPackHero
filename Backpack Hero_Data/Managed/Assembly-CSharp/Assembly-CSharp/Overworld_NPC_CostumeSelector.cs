using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class Overworld_NPC_CostumeSelector : MonoBehaviour
{
	// Token: 0x06000D3E RID: 3390 RVA: 0x00085320 File Offset: 0x00083520
	private void Start()
	{
		OVerworld_NPCManager.main.AssignOutFit(base.gameObject);
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x00085334 File Offset: 0x00083534
	private void Update()
	{
		this.currentAnimationSpeed -= Time.deltaTime;
		if (this.currentAnimationSpeed <= 0f)
		{
			if (this.animation == npcOutfit.Animation.Idle)
			{
				this.animationSpeed = 0.4f;
			}
			else
			{
				this.animationSpeed = 0.2f;
			}
			this.currentAnimationSpeed = this.animationSpeed;
			this.UpdateAnimationFrame();
		}
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x00085394 File Offset: 0x00083594
	public void SetOutfit(List<npcOutfit> newOutfits)
	{
		foreach (npcOutfit npcOutfit in newOutfits)
		{
			bool flag = false;
			foreach (npcOutfit npcOutfit2 in this.outfits)
			{
				if (npcOutfit.type == npcOutfit2.type)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.outfits.Add(npcOutfit);
			}
		}
		for (int i = 0; i < this.outfits.Count; i++)
		{
			for (int j = i; j < this.outfits.Count; j++)
			{
				npcOutfit npcOutfit3 = this.outfits[i];
				npcOutfit npcOutfit4 = this.outfits[j];
				if (npcOutfit3.type == npcOutfit4.type && npcOutfit3 != npcOutfit4)
				{
					if (Random.Range(0, 2) == 0)
					{
						this.outfits.RemoveAt(i);
						i--;
						break;
					}
					this.outfits.RemoveAt(j);
					j--;
				}
			}
		}
		this.UpdateOutfit();
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x000854E8 File Offset: 0x000836E8
	private void UpdateAnimationFrame()
	{
		this.animationFrame++;
		if (this.animationFrame >= 12)
		{
			this.animationFrame = 0;
		}
		this.UpdateOutfit();
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0008550F File Offset: 0x0008370F
	public void SetMovement(npcOutfit.Animation animation)
	{
		this.animation = animation;
		this.UpdateOutfit();
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0008551E File Offset: 0x0008371E
	public void SetMovement(npcOutfit.Direction direction)
	{
		this.direction = direction;
		this.UpdateOutfit();
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0008552D File Offset: 0x0008372D
	public void SetMovement(npcOutfit.Direction direction, npcOutfit.Animation animation)
	{
		this.direction = direction;
		this.animation = animation;
		this.UpdateOutfit();
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x00085544 File Offset: 0x00083744
	private void UpdateOutfit()
	{
		if (this.direction == npcOutfit.Direction.Left)
		{
			if (this.hat)
			{
				this.hat.flipX = true;
			}
			this.head.flipX = true;
			this.arms.flipX = true;
			this.body.flipX = true;
		}
		else
		{
			if (this.hat)
			{
				this.hat.flipX = false;
			}
			this.head.flipX = false;
			this.arms.flipX = false;
			this.body.flipX = false;
		}
		Vector2 vector = Vector2.zero;
		foreach (npcOutfit npcOutfit in this.outfits)
		{
			if (npcOutfit.type == npcOutfit.Type.Body)
			{
				int num = 0;
				foreach (npcOutfit.Outfit outfit in npcOutfit.outfits)
				{
					if (npcOutfit.MatchingDirection(outfit, this.animation, this.direction))
					{
						int count = npcOutfit.outfits[num].spriteFrames.Count;
						vector = outfit.spriteFrames[this.animationFrame % count].offset;
						break;
					}
				}
			}
		}
		foreach (npcOutfit npcOutfit2 in this.outfits)
		{
			int num2 = 0;
			foreach (npcOutfit.Outfit outfit2 in npcOutfit2.outfits)
			{
				if (npcOutfit.MatchingDirection(outfit2, this.animation, this.direction))
				{
					num2 = npcOutfit2.outfits.IndexOf(outfit2);
					break;
				}
			}
			int count2 = npcOutfit2.outfits[num2].spriteFrames.Count;
			switch (npcOutfit2.type)
			{
			case npcOutfit.Type.Head:
				this.head.transform.localPosition = new Vector3(0f, 0f, -0.1f) + vector * 0.65f;
				this.head.sprite = npcOutfit2.outfits[num2].spriteFrames[this.animationFrame % count2].sprite;
				break;
			case npcOutfit.Type.Arms:
				this.arms.transform.localPosition = new Vector3(0f, 0f, -0.3f) + vector * 0.65f;
				this.arms.sprite = npcOutfit2.outfits[num2].spriteFrames[this.animationFrame % count2].sprite;
				break;
			case npcOutfit.Type.Body:
				this.body.transform.localPosition = new Vector3(0f, 0f, 0f);
				this.body.sprite = npcOutfit2.outfits[num2].spriteFrames[this.animationFrame % count2].sprite;
				break;
			case npcOutfit.Type.Hat:
				this.hat.transform.localPosition = new Vector3(0f, 0f, -0.2f) + vector * 0.65f;
				this.hat.sprite = npcOutfit2.outfits[num2].spriteFrames[this.animationFrame % count2].sprite;
				break;
			}
		}
	}

	// Token: 0x04000AB6 RID: 2742
	[SerializeField]
	private List<npcOutfit> outfits;

	// Token: 0x04000AB7 RID: 2743
	[SerializeField]
	private SpriteRenderer hat;

	// Token: 0x04000AB8 RID: 2744
	[SerializeField]
	private SpriteRenderer head;

	// Token: 0x04000AB9 RID: 2745
	[SerializeField]
	private SpriteRenderer arms;

	// Token: 0x04000ABA RID: 2746
	[SerializeField]
	private SpriteRenderer body;

	// Token: 0x04000ABB RID: 2747
	private float animationSpeed = 0.2f;

	// Token: 0x04000ABC RID: 2748
	private float currentAnimationSpeed = 0.2f;

	// Token: 0x04000ABD RID: 2749
	private int animationFrame;

	// Token: 0x04000ABE RID: 2750
	public npcOutfit.Direction direction = npcOutfit.Direction.Front;

	// Token: 0x04000ABF RID: 2751
	public npcOutfit.Animation animation = npcOutfit.Animation.Idle;
}
