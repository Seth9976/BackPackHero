using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x02000155 RID: 341
public class Overworld_ResourceManager : MonoBehaviour
{
	// Token: 0x06000D7C RID: 3452 RVA: 0x00087008 File Offset: 0x00085208
	private void Awake()
	{
		Overworld_ResourceManager.main = this;
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x00087010 File Offset: 0x00085210
	private void OnDestroy()
	{
		Overworld_ResourceManager.main = null;
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x00087018 File Offset: 0x00085218
	private void Start()
	{
		this.foodCounterText = this.foodCounter.GetComponentInChildren<TextMeshProUGUI>();
		this.buildingMaterialCounterText = this.buildingMaterialCounter.GetComponentInChildren<TextMeshProUGUI>();
		this.treasureCounterText = this.treasureCounter.GetComponentInChildren<TextMeshProUGUI>();
		this.populationCounterText = this.populationCounter.GetComponentInChildren<TextMeshProUGUI>();
		this.foodToGainCounterText = this.foodToGainCounter.GetComponentInChildren<TextMeshProUGUI>();
		this.buildingMaterialToGainCounterText = this.buildingMaterialToGainCounter.GetComponentInChildren<TextMeshProUGUI>();
		this.treasureToGainCounterText = this.treasureToGainCounter.GetComponentInChildren<TextMeshProUGUI>();
		this.foodCounterText.text = "0";
		this.buildingMaterialCounterText.text = "0";
		this.treasureCounterText.text = "0";
		this.populationCounterText.text = "0";
		this.foodToGainCounterText.text = "0";
		this.buildingMaterialToGainCounterText.text = "0";
		this.treasureToGainCounterText.text = "0";
		this.resources = MetaProgressSaveManager.main.resources;
		foreach (Overworld_ResourceManager.Resource resource in this.resources)
		{
			switch (resource.type)
			{
			case Overworld_ResourceManager.Resource.Type.Food:
				this.foodCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmountInstant(resource.amount);
				break;
			case Overworld_ResourceManager.Resource.Type.BuildingMaterial:
				this.buildingMaterialCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmountInstant(resource.amount);
				break;
			case Overworld_ResourceManager.Resource.Type.Treasure:
				this.treasureCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmountInstant(resource.amount);
				break;
			case Overworld_ResourceManager.Resource.Type.Population:
				this.populationCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmountInstant(resource.amount);
				break;
			}
		}
		this.UpdateResourcesToGain();
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x000871E4 File Offset: 0x000853E4
	public void HideAllGainPanels()
	{
		this.foodToGainCounter.transform.gameObject.SetActive(false);
		this.buildingMaterialToGainCounter.transform.gameObject.SetActive(false);
		this.treasureToGainCounter.transform.gameObject.SetActive(false);
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x00087234 File Offset: 0x00085434
	public void ShowAllGainPanels()
	{
		List<Overworld_ResourceManager.Resource> resoucesToGain = MetaProgressSaveManager.main.GetResoucesToGain();
		if (resoucesToGain.Where((Overworld_ResourceManager.Resource r) => r.type == Overworld_ResourceManager.Resource.Type.Food && r.amount > 0).Count<Overworld_ResourceManager.Resource>() > 0)
		{
			this.foodToGainCounter.transform.gameObject.SetActive(true);
		}
		if (resoucesToGain.Where((Overworld_ResourceManager.Resource r) => r.type == Overworld_ResourceManager.Resource.Type.BuildingMaterial && r.amount > 0).Count<Overworld_ResourceManager.Resource>() > 0)
		{
			this.buildingMaterialToGainCounter.transform.gameObject.SetActive(true);
		}
		if (resoucesToGain.Where((Overworld_ResourceManager.Resource r) => r.type == Overworld_ResourceManager.Resource.Type.Treasure && r.amount > 0).Count<Overworld_ResourceManager.Resource>() > 0)
		{
			this.treasureToGainCounter.transform.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0008731C File Offset: 0x0008551C
	public void UpdateResourcesToGain()
	{
		if (!Overworld_Manager.main.IsBuildingMode())
		{
			this.foodToGainCounter.transform.gameObject.SetActive(false);
			this.buildingMaterialToGainCounter.transform.gameObject.SetActive(false);
			this.treasureToGainCounter.transform.gameObject.SetActive(false);
			return;
		}
		List<Overworld_ResourceManager.Resource> resoucesToGain = MetaProgressSaveManager.main.GetResoucesToGain();
		foreach (Overworld_ResourceManager.Resource resource in resoucesToGain)
		{
			switch (resource.type)
			{
			case Overworld_ResourceManager.Resource.Type.Food:
				this.foodToGainCounter.transform.gameObject.SetActive(true);
				this.foodToGainCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmount(resource.amount);
				break;
			case Overworld_ResourceManager.Resource.Type.BuildingMaterial:
				this.buildingMaterialToGainCounter.transform.gameObject.SetActive(true);
				this.buildingMaterialToGainCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmount(resource.amount);
				break;
			case Overworld_ResourceManager.Resource.Type.Treasure:
				this.treasureToGainCounter.transform.gameObject.SetActive(true);
				this.treasureToGainCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmount(resource.amount);
				break;
			}
		}
		if (resoucesToGain.Where((Overworld_ResourceManager.Resource r) => r.type == Overworld_ResourceManager.Resource.Type.Food && r.amount > 0).Count<Overworld_ResourceManager.Resource>() <= 0)
		{
			this.foodToGainCounter.transform.gameObject.SetActive(false);
		}
		if (resoucesToGain.Where((Overworld_ResourceManager.Resource r) => r.type == Overworld_ResourceManager.Resource.Type.BuildingMaterial && r.amount > 0).Count<Overworld_ResourceManager.Resource>() <= 0)
		{
			this.buildingMaterialToGainCounter.transform.gameObject.SetActive(false);
		}
		if (resoucesToGain.Where((Overworld_ResourceManager.Resource r) => r.type == Overworld_ResourceManager.Resource.Type.Treasure && r.amount > 0).Count<Overworld_ResourceManager.Resource>() <= 0)
		{
			this.treasureToGainCounter.transform.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x00087538 File Offset: 0x00085738
	private void Update()
	{
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0008753C File Offset: 0x0008573C
	public static List<Overworld_ResourceManager.Resource> AddResources(List<Overworld_ResourceManager.Resource> resources1, List<Overworld_ResourceManager.Resource> resources2)
	{
		List<Overworld_ResourceManager.Resource> list = new List<Overworld_ResourceManager.Resource>();
		foreach (Overworld_ResourceManager.Resource resource in resources1)
		{
			bool flag = false;
			foreach (Overworld_ResourceManager.Resource resource2 in list)
			{
				if (resource2.type == resource.type)
				{
					resource2.amount += resource.amount;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(resource.Clone());
			}
		}
		foreach (Overworld_ResourceManager.Resource resource3 in resources2)
		{
			bool flag2 = false;
			foreach (Overworld_ResourceManager.Resource resource4 in list)
			{
				if (resource4.type == resource3.type)
				{
					resource4.amount += resource3.amount;
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				list.Add(resource3.Clone());
			}
		}
		return list;
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x000876A8 File Offset: 0x000858A8
	public void UpdateCounters()
	{
		this.resources = MetaProgressSaveManager.main.resources;
		if (!this.foodCounterText || !this.buildingMaterialCounterText || !this.treasureCounterText || !this.populationCounterText)
		{
			return;
		}
		foreach (Overworld_ResourceManager.Resource resource in this.resources)
		{
			switch (resource.type)
			{
			case Overworld_ResourceManager.Resource.Type.Food:
				this.foodCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmount(resource.amount);
				break;
			case Overworld_ResourceManager.Resource.Type.BuildingMaterial:
				this.buildingMaterialCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmount(resource.amount);
				break;
			case Overworld_ResourceManager.Resource.Type.Treasure:
				this.treasureCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmount(resource.amount);
				break;
			case Overworld_ResourceManager.Resource.Type.Population:
			{
				this.populationCounterText.GetComponentInParent<Overworld_ResourceDisplay>().SetAmount(resource.amount);
				LayeredSong componentInChildren = SoundManager.main.GetComponentInChildren<LayeredSong>();
				if (componentInChildren)
				{
					componentInChildren.progress = (float)resource.amount;
				}
				break;
			}
			}
		}
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x000877DC File Offset: 0x000859DC
	public static void ChangeResourceAmountBy(List<Overworld_ResourceManager.Resource> resources, List<Overworld_ResourceManager.Resource> resourceCosts, int factor)
	{
		foreach (Overworld_ResourceManager.Resource resource in resourceCosts)
		{
			Overworld_ResourceManager.ChangeResourceAmountBy(resources, resource.type, resource.amount * factor);
		}
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x00087838 File Offset: 0x00085A38
	public static void ChangeResourceAmountBy(List<Overworld_ResourceManager.Resource> resources, Overworld_ResourceManager.Resource.Type type, int amount)
	{
		foreach (Overworld_ResourceManager.Resource resource in resources)
		{
			if (resource.type == type)
			{
				resource.amount += amount;
				if (Overworld_ResourceManager.main)
				{
					Overworld_ResourceManager.main.UpdateCounters();
				}
				return;
			}
		}
		resources.Add(new Overworld_ResourceManager.Resource
		{
			type = type,
			amount = amount
		});
		if (Overworld_ResourceManager.main)
		{
			Overworld_ResourceManager.main.UpdateCounters();
		}
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x000878E0 File Offset: 0x00085AE0
	public List<Overworld_ResourceManager.Resource> MultiplyResourceCosts(List<Overworld_ResourceManager.Resource> resources, int factor)
	{
		List<Overworld_ResourceManager.Resource> list = new List<Overworld_ResourceManager.Resource>();
		foreach (Overworld_ResourceManager.Resource resource in resources)
		{
			Overworld_ResourceManager.Resource resource2 = new Overworld_ResourceManager.Resource();
			resource2.type = resource.type;
			if (factor > 1)
			{
				resource2.amount = Mathf.RoundToInt((float)resource.amount * ((float)factor * ((float)factor * 0.5f)));
			}
			else
			{
				resource2.amount = resource.amount;
			}
			list.Add(resource2);
		}
		return list;
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x00087978 File Offset: 0x00085B78
	public bool HasEnoughResources(List<Overworld_ResourceManager.Resource> resourceCosts, int factor = 1)
	{
		foreach (Overworld_ResourceManager.Resource resource in resourceCosts)
		{
			if (!this.HasEnoughResources(resource.type, resource.amount * factor))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x000879DC File Offset: 0x00085BDC
	public bool HasEnoughResources(Overworld_ResourceManager.Resource.Type type, int amount)
	{
		if (amount >= 0)
		{
			return true;
		}
		foreach (Overworld_ResourceManager.Resource resource in MetaProgressSaveManager.main.resources)
		{
			if (resource.type == type)
			{
				if (resource.amount + amount >= 0 && amount < 0)
				{
					return true;
				}
				return false;
			}
		}
		return false;
	}

	// Token: 0x04000AEE RID: 2798
	public static Overworld_ResourceManager main;

	// Token: 0x04000AEF RID: 2799
	[Header("Resource Counter References")]
	[SerializeField]
	private GameObject foodCounter;

	// Token: 0x04000AF0 RID: 2800
	[SerializeField]
	private GameObject buildingMaterialCounter;

	// Token: 0x04000AF1 RID: 2801
	[SerializeField]
	private GameObject treasureCounter;

	// Token: 0x04000AF2 RID: 2802
	[SerializeField]
	private GameObject populationCounter;

	// Token: 0x04000AF3 RID: 2803
	[Header("Resource Counter Text References")]
	private TextMeshProUGUI foodCounterText;

	// Token: 0x04000AF4 RID: 2804
	private TextMeshProUGUI buildingMaterialCounterText;

	// Token: 0x04000AF5 RID: 2805
	private TextMeshProUGUI treasureCounterText;

	// Token: 0x04000AF6 RID: 2806
	private TextMeshProUGUI populationCounterText;

	// Token: 0x04000AF7 RID: 2807
	[Header("Resource To Gain Passively Refereces")]
	[SerializeField]
	private GameObject foodToGainCounter;

	// Token: 0x04000AF8 RID: 2808
	[SerializeField]
	private GameObject buildingMaterialToGainCounter;

	// Token: 0x04000AF9 RID: 2809
	[SerializeField]
	private GameObject treasureToGainCounter;

	// Token: 0x04000AFA RID: 2810
	[Header("Resource To Gain Passively Text References")]
	private TextMeshProUGUI foodToGainCounterText;

	// Token: 0x04000AFB RID: 2811
	private TextMeshProUGUI buildingMaterialToGainCounterText;

	// Token: 0x04000AFC RID: 2812
	private TextMeshProUGUI treasureToGainCounterText;

	// Token: 0x04000AFD RID: 2813
	[SerializeField]
	public List<Overworld_ResourceManager.Resource> resources = new List<Overworld_ResourceManager.Resource>();

	// Token: 0x0200040F RID: 1039
	[Serializable]
	public class Resource
	{
		// Token: 0x0600195F RID: 6495 RVA: 0x000CFA51 File Offset: 0x000CDC51
		public Overworld_ResourceManager.Resource Clone()
		{
			return new Overworld_ResourceManager.Resource
			{
				type = this.type,
				amount = this.amount
			};
		}

		// Token: 0x040017D2 RID: 6098
		public Overworld_ResourceManager.Resource.Type type;

		// Token: 0x040017D3 RID: 6099
		public int amount;

		// Token: 0x020004BC RID: 1212
		public enum Type
		{
			// Token: 0x04001C5F RID: 7263
			Food,
			// Token: 0x04001C60 RID: 7264
			BuildingMaterial,
			// Token: 0x04001C61 RID: 7265
			Treasure,
			// Token: 0x04001C62 RID: 7266
			Population
		}
	}
}
