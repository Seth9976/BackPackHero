using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000054 RID: 84
public class MapDescriptorManager : MonoBehaviour
{
	// Token: 0x0600026F RID: 623 RVA: 0x0000CD41 File Offset: 0x0000AF41
	private void OnEnable()
	{
		MapDescriptorManager.instance = this;
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000CD49 File Offset: 0x0000AF49
	private void OnDisable()
	{
		MapDescriptorManager.instance = null;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000CD54 File Offset: 0x0000AF54
	public void SetEvent(MapEvent mapEvent)
	{
		foreach (object obj in this.eventImagesParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		if (MapPlayer.instance && MapPlayer.instance.currentEvent == mapEvent)
		{
			this.eventName.SetKey("my position!");
			return;
		}
		MapDescription component = mapEvent.eventPrefab.GetComponent<MapDescription>();
		if (component)
		{
			this.eventName.SetKey(component.descriptorKey);
			foreach (Sprite sprite in component.images)
			{
				Object.Instantiate<GameObject>(this.eventImagePrefab, this.eventImagesParent).GetComponent<Image>().sprite = sprite;
			}
			return;
		}
		Debug.LogError("No MapDescription found for " + mapEvent.eventPrefab.name);
	}

	// Token: 0x040001D1 RID: 465
	public static MapDescriptorManager instance;

	// Token: 0x040001D2 RID: 466
	[SerializeField]
	private ReplacementText eventName;

	// Token: 0x040001D3 RID: 467
	[SerializeField]
	private ReplacementText eventDescription;

	// Token: 0x040001D4 RID: 468
	[SerializeField]
	private Transform eventImagesParent;

	// Token: 0x040001D5 RID: 469
	[SerializeField]
	private GameObject eventImagePrefab;
}
