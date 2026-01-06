using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class MapEventContentSpawner : MonoBehaviour
{
	// Token: 0x06000286 RID: 646 RVA: 0x0000D42C File Offset: 0x0000B62C
	public void SpawnEvent()
	{
		if (this.possibleEvents.Length == 0)
		{
			Debug.LogWarning("No possible events to spawn for " + base.gameObject.name);
			return;
		}
		GameObject gameObject = this.possibleEvents[Random.Range(0, this.possibleEvents.Length)];
		this.mapEvent.eventPrefab = gameObject;
	}

	// Token: 0x040001DE RID: 478
	[SerializeField]
	private MapEvent mapEvent;

	// Token: 0x040001DF RID: 479
	[SerializeField]
	private GameObject[] possibleEvents;
}
