using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class MapSpawner : MonoBehaviour
{
	// Token: 0x06000295 RID: 661 RVA: 0x0000D61D File Offset: 0x0000B81D
	private void Start()
	{
		this.SpawnMap();
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0000D628 File Offset: 0x0000B828
	private void SpawnMap()
	{
		for (int i = 0; i < this.mapSectionSpots.Length; i++)
		{
			switch (i)
			{
			case 0:
				Object.Instantiate<GameObject>(this.mapSections1[Random.Range(0, this.mapSections1.Length)], this.mapSectionSpots[i].transform.position, Quaternion.identity, this.mapSectionSpots[i].transform);
				break;
			case 1:
				Object.Instantiate<GameObject>(this.mapSections2[Random.Range(0, this.mapSections2.Length)], this.mapSectionSpots[i].transform.position, Quaternion.identity, this.mapSectionSpots[i].transform);
				break;
			case 2:
				Object.Instantiate<GameObject>(this.mapSections3[Random.Range(0, this.mapSections3.Length)], this.mapSectionSpots[i].transform.position, Quaternion.identity, this.mapSectionSpots[i].transform);
				break;
			}
		}
		MapEvent.SetupEvents();
	}

	// Token: 0x040001E6 RID: 486
	[SerializeField]
	private GameObject[] mapSectionSpots;

	// Token: 0x040001E7 RID: 487
	[SerializeField]
	private GameObject[] mapSections1;

	// Token: 0x040001E8 RID: 488
	[SerializeField]
	private GameObject[] mapSections2;

	// Token: 0x040001E9 RID: 489
	[SerializeField]
	private GameObject[] mapSections3;
}
