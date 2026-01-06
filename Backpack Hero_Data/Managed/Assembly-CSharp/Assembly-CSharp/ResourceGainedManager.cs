using System;
using UnityEngine;

// Token: 0x02000097 RID: 151
public class ResourceGainedManager : MonoBehaviour
{
	// Token: 0x06000350 RID: 848 RVA: 0x00013746 File Offset: 0x00011946
	private void Awake()
	{
		ResourceGainedManager.main = this;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0001374E File Offset: 0x0001194E
	private void OnDestroy()
	{
		if (ResourceGainedManager.main == this)
		{
			ResourceGainedManager.main = null;
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00013763 File Offset: 0x00011963
	public void ShowResourcePanel(Overworld_ResourceManager.Resource.Type type, int amount, float speed = 1f)
	{
		ResourceGainedPanel component = Object.Instantiate<GameObject>(this.resourceGainedPanelPrefab, this.resourceGainedPanelParent).GetComponent<ResourceGainedPanel>();
		component.SetSpeed(speed);
		component.SetResource(type, new ResourceGainedPanel.ResourceOrigin
		{
			name = "Gold",
			amount = amount
		});
	}

	// Token: 0x06000353 RID: 851 RVA: 0x000137A0 File Offset: 0x000119A0
	public void ShowResourcePanels()
	{
		if (!Singleton.Instance.storyMode)
		{
			return;
		}
		foreach (MetaProgressSaveManager.ResourceToAdd resourceToAdd in MetaProgressSaveManager.main.resourcesToAdd)
		{
			if (resourceToAdd.amount != 0)
			{
				Object.Instantiate<GameObject>(this.resourceGainedPanelPrefab, this.resourceGainedPanelParent).GetComponent<ResourceGainedPanel>().SetResource(resourceToAdd.type, resourceToAdd.amount, Mathf.RoundToInt(resourceToAdd.currentEfficiencyBonus));
			}
		}
	}

	// Token: 0x04000255 RID: 597
	public static ResourceGainedManager main;

	// Token: 0x04000256 RID: 598
	[SerializeField]
	private GameObject resourceGainedPanelPrefab;

	// Token: 0x04000257 RID: 599
	[SerializeField]
	private Transform resourceGainedPanelParent;
}
