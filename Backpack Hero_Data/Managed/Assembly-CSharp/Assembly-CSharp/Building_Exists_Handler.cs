using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000007 RID: 7
public class Building_Exists_Handler : MonoBehaviour
{
	// Token: 0x06000019 RID: 25 RVA: 0x00002963 File Offset: 0x00000B63
	private void Start()
	{
		base.StartCoroutine(this.CheckForBuilding());
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002972 File Offset: 0x00000B72
	private IEnumerator CheckForBuilding()
	{
		yield return null;
		yield return new WaitForFixedUpdate();
		Overworld_SaveManager saveManager = Object.FindObjectOfType<Overworld_SaveManager>();
		while (saveManager && saveManager.isSavingOrLoading)
		{
			yield return null;
		}
		this.CheckEvents();
		yield break;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002984 File Offset: 0x00000B84
	private void CheckEvents()
	{
		foreach (Building_Exists_Handler.BuildingEvent buildingEvent in this.events)
		{
			Overworld_Structure component = buildingEvent.building.GetComponent<Overworld_Structure>();
			if (!(component == null) && Overworld_Structure.StructuresOfType(component).Count > 0 == buildingEvent.buildingExists)
			{
				buildingEvent.eventToPerform.Invoke();
			}
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002A08 File Offset: 0x00000C08
	private void Update()
	{
	}

	// Token: 0x0400000C RID: 12
	[SerializeField]
	private List<Building_Exists_Handler.BuildingEvent> events = new List<Building_Exists_Handler.BuildingEvent>();

	// Token: 0x02000239 RID: 569
	[Serializable]
	private class BuildingEvent
	{
		// Token: 0x04000E8F RID: 3727
		public GameObject building;

		// Token: 0x04000E90 RID: 3728
		public bool buildingExists;

		// Token: 0x04000E91 RID: 3729
		public UnityEvent eventToPerform;
	}
}
