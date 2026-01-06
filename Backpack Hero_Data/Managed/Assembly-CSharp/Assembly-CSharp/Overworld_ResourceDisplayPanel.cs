using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000154 RID: 340
public class Overworld_ResourceDisplayPanel : MonoBehaviour
{
	// Token: 0x06000D75 RID: 3445 RVA: 0x00086E0C File Offset: 0x0008500C
	private void Start()
	{
		this.SetupPanel();
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00086E14 File Offset: 0x00085014
	private void Update()
	{
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x00086E18 File Offset: 0x00085018
	public void SetupResources(List<Overworld_ResourceManager.Resource> resources, int populationRequirement)
	{
		resources = resources.Where((Overworld_ResourceManager.Resource x) => x.amount != 0).ToList<Overworld_ResourceManager.Resource>();
		this.resources = resources;
		this.popRequirementText.text = populationRequirement.ToString();
		this.SetupPanel();
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x00086E70 File Offset: 0x00085070
	public void SetupResources(List<Overworld_ResourceManager.Resource> resources)
	{
		if (resources == null || resources.Count == 0)
		{
			this.resources = new List<Overworld_ResourceManager.Resource>();
		}
		else
		{
			resources = resources.Where((Overworld_ResourceManager.Resource x) => x.amount != 0).ToList<Overworld_ResourceManager.Resource>();
			this.resources = resources;
		}
		this.SetupPanel();
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00086ED0 File Offset: 0x000850D0
	public void SetupPanel()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (!transform.CompareTag("Ignorable"))
			{
				Object.Destroy(transform.gameObject);
			}
		}
		if (this.resources.Count == 0)
		{
			base.gameObject.SetActive(false);
		}
		this.SetColor(Color.white);
		foreach (Overworld_ResourceManager.Resource resource in this.resources)
		{
			Object.Instantiate<GameObject>(this.resourcePrefab, new Vector3(-9999f, -9999f, 0f), Quaternion.identity, base.transform).GetComponent<Overworld_ResourceDisplay>().Setup(resource, this.showPlus, true, this);
		}
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x00086FD8 File Offset: 0x000851D8
	public void SetColor(Color color)
	{
		if (this.backgroundImage == null)
		{
			return;
		}
		this.backgroundImage.color = color;
	}

	// Token: 0x04000AE9 RID: 2793
	[SerializeField]
	private TextMeshProUGUI popRequirementText;

	// Token: 0x04000AEA RID: 2794
	[SerializeField]
	private Image backgroundImage;

	// Token: 0x04000AEB RID: 2795
	[SerializeField]
	public bool showPlus;

	// Token: 0x04000AEC RID: 2796
	[SerializeField]
	private GameObject resourcePrefab;

	// Token: 0x04000AED RID: 2797
	[SerializeField]
	private List<Overworld_ResourceManager.Resource> resources = new List<Overworld_ResourceManager.Resource>();
}
