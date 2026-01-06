using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000072 RID: 114
public class MetaProgressHandler : MonoBehaviour
{
	// Token: 0x06000235 RID: 565 RVA: 0x0000DD34 File Offset: 0x0000BF34
	private void Start()
	{
		this.Perform();
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		foreach (MetaProgressSaveManager.MetaProgressMarker metaProgressMarker in this.markersToAdd)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(metaProgressMarker);
		}
		base.StartCoroutine(this.WaitforBuilding());
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000DDAC File Offset: 0x0000BFAC
	private void Update()
	{
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
	private void Perform()
	{
		foreach (MetaProgressHandler.MetaProgressEvent metaProgressEvent in this.events)
		{
			if (MetaProgressSaveManager.ConditionMet(metaProgressEvent.condition))
			{
				metaProgressEvent.eventToPerform.Invoke();
			}
		}
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000DE14 File Offset: 0x0000C014
	private IEnumerator WaitforBuilding()
	{
		yield return new WaitForSeconds(0.25f);
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForSeconds(0.25f);
		using (List<MetaProgressHandler.BuildingEvent>.Enumerator enumerator = this.buildingEvents.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MetaProgressHandler.BuildingEvent buildingEvent = enumerator.Current;
				if (Overworld_Structure.StructuresOfType(buildingEvent.building.GetComponent<Overworld_Structure>()).Count > 0 == buildingEvent.buildingBuilt)
				{
					buildingEvent.eventToPerform.Invoke();
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x04000177 RID: 375
	[SerializeField]
	private List<MetaProgressSaveManager.MetaProgressMarker> markersToAdd;

	// Token: 0x04000178 RID: 376
	[SerializeField]
	private List<MetaProgressHandler.MetaProgressEvent> events;

	// Token: 0x04000179 RID: 377
	[SerializeField]
	private List<MetaProgressHandler.BuildingEvent> buildingEvents;

	// Token: 0x02000282 RID: 642
	[Serializable]
	private class MetaProgressEvent
	{
		// Token: 0x04000F73 RID: 3955
		public MetaProgressSaveManager.MetaProgressCondition condition;

		// Token: 0x04000F74 RID: 3956
		public UnityEvent eventToPerform;
	}

	// Token: 0x02000283 RID: 643
	[Serializable]
	private class BuildingEvent
	{
		// Token: 0x04000F75 RID: 3957
		public GameObject building;

		// Token: 0x04000F76 RID: 3958
		public bool buildingBuilt;

		// Token: 0x04000F77 RID: 3959
		public UnityEvent eventToPerform;
	}
}
