using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000098 RID: 152
public class ResourceGainedPanel : MonoBehaviour
{
	// Token: 0x06000355 RID: 853 RVA: 0x00013840 File Offset: 0x00011A40
	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x00013849 File Offset: 0x00011A49
	public void SetResource(Overworld_ResourceManager.Resource.Type resourceType, ResourceGainedPanel.ResourceOrigin resourceOrigin)
	{
		this.SetResource(resourceType, new List<ResourceGainedPanel.ResourceOrigin> { resourceOrigin });
	}

	// Token: 0x06000357 RID: 855 RVA: 0x00013860 File Offset: 0x00011A60
	public void SetResource(Overworld_ResourceManager.Resource.Type resourceType, List<ResourceGainedPanel.ResourceOrigin> resourceOrigin)
	{
		this.resourceType = resourceType;
		this.resourceOrigins = resourceOrigin;
		this.resourceAmountFromBuildings = 0;
		this.resourceAmountFromEfficiencyBonus = 0;
		this.resourceAmountFromFloor = 0;
		this.SetSymbol();
		if (this.showingRoutine != null)
		{
			base.StopCoroutine(this.showingRoutine);
		}
		this.showingRoutine = base.StartCoroutine(this.ShowRoutine());
	}

	// Token: 0x06000358 RID: 856 RVA: 0x000138BC File Offset: 0x00011ABC
	public void SetResource(Overworld_ResourceManager.Resource.Type resourceType, int resourceAmountFromBuildings, int resourceAmountFromEfficiencyBonus)
	{
		this.resourceAmountFromBuildings = resourceAmountFromBuildings;
		this.resourceAmountFromEfficiencyBonus = resourceAmountFromEfficiencyBonus;
		this.resourceType = resourceType;
		this.SetSymbol();
		if (this.showingRoutine != null)
		{
			base.StopCoroutine(this.showingRoutine);
		}
		this.showingRoutine = base.StartCoroutine(this.ShowRoutine());
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0001390C File Offset: 0x00011B0C
	private void SetSymbol()
	{
		LangaugeManager.main.SetFont(base.transform);
		switch (this.resourceType)
		{
		case Overworld_ResourceManager.Resource.Type.Food:
			this.titleText.text = LangaugeManager.main.GetTextByKey("Food");
			this.resourceImage.sprite = this.foodSprite;
			return;
		case Overworld_ResourceManager.Resource.Type.BuildingMaterial:
			this.titleText.text = LangaugeManager.main.GetTextByKey("Material");
			this.resourceImage.sprite = this.buildingMaterialSprite;
			return;
		case Overworld_ResourceManager.Resource.Type.Treasure:
			this.titleText.text = LangaugeManager.main.GetTextByKey("Treasure");
			this.resourceImage.sprite = this.treasureSprite;
			return;
		default:
			return;
		}
	}

	// Token: 0x0600035A RID: 858 RVA: 0x000139C6 File Offset: 0x00011BC6
	private IEnumerator ShowRoutine()
	{
		yield return null;
		Animator animator = base.GetComponent<Animator>();
		animator.enabled = false;
		yield return new WaitForSeconds(0.1f * (float)(base.transform.GetSiblingIndex() + 1));
		animator.enabled = true;
		this.runningTotal = 0;
		this.floorNumber = GameManager.main.floor;
		this.resourceAmountFromFloor = 0;
		foreach (ResourceGainedPanel.Levels levels in this.levels)
		{
			this.resourceAmountFromFloor = Mathf.RoundToInt(levels.floorBonus * (float)(this.resourceAmountFromBuildings + this.resourceAmountFromEfficiencyBonus));
			if (this.floorNumber == levels.level)
			{
				break;
			}
		}
		this.startingNumber = MetaProgressSaveManager.main.GetResourceAmount(this.resourceType);
		int num = this.resourceAmountFromBuildings + this.resourceAmountFromEfficiencyBonus + this.resourceAmountFromFloor;
		Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, this.resourceType, num);
		int num2 = 0;
		foreach (ResourceGainedPanel.ResourceOrigin resourceOrigin2 in this.resourceOrigins)
		{
			num2 += resourceOrigin2.amount;
		}
		base.gameObject.SetActive(true);
		this.ShowTotalNumber(0);
		foreach (ResourceGainedPanel.ResourceOrigin resourceOrigin in this.resourceOrigins)
		{
			yield return this.ShowOverTime(resourceOrigin.name, (float)resourceOrigin.amount);
			this.ShowTotalNumber(resourceOrigin.amount);
			resourceOrigin = null;
		}
		List<ResourceGainedPanel.ResourceOrigin>.Enumerator enumerator3 = default(List<ResourceGainedPanel.ResourceOrigin>.Enumerator);
		if (this.resourceAmountFromBuildings > 0)
		{
			yield return this.ShowOverTime(LangaugeManager.main.GetTextByKey("Buildings"), (float)this.resourceAmountFromBuildings);
			this.ShowTotalNumber(this.resourceAmountFromBuildings);
		}
		if (this.resourceAmountFromEfficiencyBonus > 0)
		{
			yield return this.ShowOverTime(LangaugeManager.main.GetTextByKey("Efficiency"), (float)this.resourceAmountFromEfficiencyBonus);
			this.ShowTotalNumber(this.resourceAmountFromEfficiencyBonus);
		}
		if (this.resourceAmountFromFloor > 0)
		{
			yield return this.ShowOverTime(LangaugeManager.main.GetTextByKey("Floor Bonus"), (float)this.resourceAmountFromFloor);
			this.ShowTotalNumber(this.resourceAmountFromFloor);
		}
		yield return new WaitForSeconds(1.5f + 0.2f * (float)(base.transform.GetSiblingIndex() + 1));
		base.GetComponent<Animator>().Play("Close");
		yield break;
		yield break;
	}

	// Token: 0x0600035B RID: 859 RVA: 0x000139D5 File Offset: 0x00011BD5
	private void ShowTotalNumber(int amount)
	{
		this.runningTotal = amount;
		this.startingNumber += amount;
		this.totalHeldNumberText.text = this.startingNumber.ToString();
		this.totalHeldNumberTextAnimation.Play("numberBounce");
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00013A13 File Offset: 0x00011C13
	private IEnumerator ShowOverTime(string text, float amount)
	{
		foreach (object obj in this.explanationParent)
		{
			Transform transform = (Transform)obj;
			Animator component = transform.GetComponent<Animator>();
			if (component != null)
			{
				component.Play("Close");
			}
			else
			{
				Object.Destroy(transform.gameObject);
			}
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.explanationPrefab, this.explanationParent);
		LangaugeManager.main.SetFont(gameObject.transform);
		ExplanationGroup component2 = gameObject.GetComponent<ExplanationGroup>();
		this.explanationText = component2.explanationText;
		this.currentNumberText = component2.currentNumberText;
		this.currentNumberTextAnimation = component2.currentNumberTextAnimation;
		this.explanationText.text = text;
		float speed = 10f * this.speed;
		float startingAmount = amount;
		int lastDifference = 0;
		while (amount > 1f)
		{
			int num = Mathf.RoundToInt(startingAmount - amount);
			if (num != lastDifference)
			{
				lastDifference = num;
				this.currentNumberText.text = "+" + num.ToString();
				this.currentNumberTextAnimation.Play("numberBounce");
			}
			amount -= speed * Time.deltaTime;
			speed = Mathf.Lerp(Mathf.Clamp(0.2f * startingAmount, 2.5f, 50f), Mathf.Clamp(0.6f * startingAmount, 5f, 100f), amount / startingAmount) * this.speed;
			yield return null;
		}
		this.currentNumberText.text = "+" + startingAmount.ToString();
		yield return new WaitForSeconds(0.5f / this.speed);
		yield break;
	}

	// Token: 0x04000258 RID: 600
	[SerializeField]
	private List<ResourceGainedPanel.Levels> levels = new List<ResourceGainedPanel.Levels>();

	// Token: 0x04000259 RID: 601
	[SerializeField]
	private int floorNumber = 2;

	// Token: 0x0400025A RID: 602
	[SerializeField]
	private Overworld_ResourceManager.Resource.Type resourceType;

	// Token: 0x0400025B RID: 603
	private List<ResourceGainedPanel.ResourceOrigin> resourceOrigins = new List<ResourceGainedPanel.ResourceOrigin>();

	// Token: 0x0400025C RID: 604
	[SerializeField]
	private int resourceAmountFromBuildings = 50;

	// Token: 0x0400025D RID: 605
	[SerializeField]
	private int resourceAmountFromEfficiencyBonus = 10;

	// Token: 0x0400025E RID: 606
	[SerializeField]
	private int resourceAmountFromFloor;

	// Token: 0x0400025F RID: 607
	private Coroutine showingRoutine;

	// Token: 0x04000260 RID: 608
	[SerializeField]
	private TextMeshProUGUI titleText;

	// Token: 0x04000261 RID: 609
	[SerializeField]
	private TextMeshProUGUI totalNumberText;

	// Token: 0x04000262 RID: 610
	[SerializeField]
	private TextMeshProUGUI totalHeldNumberText;

	// Token: 0x04000263 RID: 611
	[SerializeField]
	private Animation totalNumberTextAnimation;

	// Token: 0x04000264 RID: 612
	[SerializeField]
	private Animation totalHeldNumberTextAnimation;

	// Token: 0x04000265 RID: 613
	[SerializeField]
	private TextMeshProUGUI currentNumberText;

	// Token: 0x04000266 RID: 614
	[SerializeField]
	private Animation currentNumberTextAnimation;

	// Token: 0x04000267 RID: 615
	[SerializeField]
	private TextMeshProUGUI explanationText;

	// Token: 0x04000268 RID: 616
	[SerializeField]
	private Transform explanationParent;

	// Token: 0x04000269 RID: 617
	[SerializeField]
	private GameObject explanationPrefab;

	// Token: 0x0400026A RID: 618
	[SerializeField]
	private Image resourceImage;

	// Token: 0x0400026B RID: 619
	[SerializeField]
	private Sprite foodSprite;

	// Token: 0x0400026C RID: 620
	[SerializeField]
	private Sprite buildingMaterialSprite;

	// Token: 0x0400026D RID: 621
	[SerializeField]
	private Sprite treasureSprite;

	// Token: 0x0400026E RID: 622
	private float speed = 1f;

	// Token: 0x0400026F RID: 623
	private int runningTotal;

	// Token: 0x04000270 RID: 624
	private int startingNumber;

	// Token: 0x0200029F RID: 671
	[Serializable]
	private class Levels
	{
		// Token: 0x04000FE5 RID: 4069
		public int level;

		// Token: 0x04000FE6 RID: 4070
		public float floorBonus;
	}

	// Token: 0x020002A0 RID: 672
	public class ResourceOrigin
	{
		// Token: 0x04000FE7 RID: 4071
		public string name;

		// Token: 0x04000FE8 RID: 4072
		public int amount;
	}
}
