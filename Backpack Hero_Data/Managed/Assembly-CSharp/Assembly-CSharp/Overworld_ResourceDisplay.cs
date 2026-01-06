using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000153 RID: 339
public class Overworld_ResourceDisplay : MonoBehaviour
{
	// Token: 0x06000D6F RID: 3439 RVA: 0x00086B00 File Offset: 0x00084D00
	private void Update()
	{
		if (this.amount != this.displayAmount)
		{
			if (this.amount > this.displayAmount)
			{
				SoundManager.main.PlaySFXPitched("blipDown", Random.Range(1f, 1.2f), false);
				if (Mathf.Abs(this.amount - this.displayAmount) > 10)
				{
					this.displayAmount += (int)((float)(this.amount - this.displayAmount) * 0.1f);
				}
				else
				{
					this.displayAmount++;
				}
			}
			else
			{
				SoundManager.main.PlaySFX("blipDown");
				if (Mathf.Abs(this.amount - this.displayAmount) > 10)
				{
					this.displayAmount -= (int)((float)(this.displayAmount - this.amount) * 0.1f);
				}
				else
				{
					this.displayAmount--;
				}
			}
			this.text.text = this.prefix + this.displayAmount.ToString();
		}
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x00086C10 File Offset: 0x00084E10
	public void Setup(Overworld_ResourceManager.Resource resource, bool showPlus, bool showRedIfCantAfford = false, Overworld_ResourceDisplayPanel panel = null)
	{
		this.type = resource.type;
		this.amount = resource.amount;
		this.displayAmount = resource.amount;
		if (showPlus && resource.amount > 0)
		{
			this.text.text = "+" + resource.amount.ToString();
		}
		else
		{
			this.text.text = resource.amount.ToString();
		}
		switch (resource.type)
		{
		case Overworld_ResourceManager.Resource.Type.Food:
			this.image.sprite = this.foodSprite;
			break;
		case Overworld_ResourceManager.Resource.Type.BuildingMaterial:
			this.image.sprite = this.buildingMaterialSprite;
			break;
		case Overworld_ResourceManager.Resource.Type.Treasure:
			this.image.sprite = this.treasureSprite;
			break;
		case Overworld_ResourceManager.Resource.Type.Population:
			this.image.sprite = this.populationSprite;
			break;
		}
		if (showRedIfCantAfford && Overworld_ResourceManager.main && !Overworld_ResourceManager.main.HasEnoughResources(resource.type, -resource.amount) && !panel.showPlus)
		{
			this.text.color = Color.red;
			if (panel)
			{
				panel.SetColor(Color.red);
				return;
			}
		}
		else
		{
			this.text.color = Color.white;
		}
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x00086D55 File Offset: 0x00084F55
	public void ChangeAmount(int amount)
	{
		if (amount < 0)
		{
			this.animator.Play("pulseDown");
		}
		else if (amount > 0)
		{
			this.animator.Play("pulseUp");
		}
		this.amount += amount;
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x00086D8F File Offset: 0x00084F8F
	public void SetAmountInstant(int amount)
	{
		this.amount = amount;
		this.displayAmount = amount;
		this.text.text = this.prefix + amount.ToString();
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x00086DBC File Offset: 0x00084FBC
	public void SetAmount(int amount)
	{
		if (amount < this.amount)
		{
			this.animator.Play("pulseDown");
		}
		else if (amount > this.amount)
		{
			this.animator.Play("pulseUp");
		}
		this.amount = amount;
	}

	// Token: 0x04000ADD RID: 2781
	[SerializeField]
	private Sprite foodSprite;

	// Token: 0x04000ADE RID: 2782
	[SerializeField]
	private Sprite buildingMaterialSprite;

	// Token: 0x04000ADF RID: 2783
	[SerializeField]
	private Sprite treasureSprite;

	// Token: 0x04000AE0 RID: 2784
	[SerializeField]
	private Sprite giftsSprite;

	// Token: 0x04000AE1 RID: 2785
	[SerializeField]
	private Sprite populationSprite;

	// Token: 0x04000AE2 RID: 2786
	public Overworld_ResourceManager.Resource.Type type;

	// Token: 0x04000AE3 RID: 2787
	public int amount;

	// Token: 0x04000AE4 RID: 2788
	public int displayAmount;

	// Token: 0x04000AE5 RID: 2789
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x04000AE6 RID: 2790
	[SerializeField]
	private Image image;

	// Token: 0x04000AE7 RID: 2791
	[SerializeField]
	private Animator animator;

	// Token: 0x04000AE8 RID: 2792
	[SerializeField]
	private string prefix = "";
}
