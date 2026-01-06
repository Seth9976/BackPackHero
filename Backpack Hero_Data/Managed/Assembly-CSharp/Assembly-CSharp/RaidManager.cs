using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class RaidManager : MonoBehaviour
{
	// Token: 0x06000328 RID: 808 RVA: 0x00012597 File Offset: 0x00010797
	private void Start()
	{
		this.SetupRaid();
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0001259F File Offset: 0x0001079F
	private void Update()
	{
	}

	// Token: 0x0600032A RID: 810 RVA: 0x000125A4 File Offset: 0x000107A4
	private void SetupRaid()
	{
		foreach (object obj in this.npcsParent.transform)
		{
			((Transform)obj).gameObject.SetActive(false);
		}
		this.npcRaidParent.gameObject.SetActive(true);
		this.redArea.SetActive(true);
		foreach (ParticleSystem particleSystem in this.particleSystemParent.GetComponentsInChildren<ParticleSystem>())
		{
			particleSystem.main.startColor = Color.red;
			particleSystem.Clear();
			particleSystem.Play();
		}
		this.SpawnObjectsInPositions();
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0001266C File Offset: 0x0001086C
	private void SpawnObjectsInPositions()
	{
		List<Vector2> list = new List<Vector2>();
		int num = 0;
		int num2 = 0;
		while (num2 < 15 && num < 50)
		{
			num++;
			int num3 = Random.Range(0, 3);
			Vector2 zero = Vector2.zero;
			if (num3 != 2)
			{
				zero = new Vector2(Random.Range(this.upperLeft.position.x, this.lowerRight.position.x), Random.Range(this.upperLeft.position.y, this.lowerRight.position.y));
			}
			else
			{
				zero = new Vector2(Random.Range(this.upperLeft2.position.x, this.lowerRight2.position.x), Random.Range(this.upperLeft2.position.y, this.lowerRight2.position.y));
			}
			if (Physics2D.OverlapCircle(zero, 0.25f) == null)
			{
				list.Add(zero);
			}
			else
			{
				num2--;
			}
			num2++;
		}
		foreach (Vector2 vector in list)
		{
			if (this.objectsToSpawOnce.Count > 0)
			{
				int num4 = Random.Range(0, this.objectsToSpawOnce.Count);
				Object.Instantiate<GameObject>(this.objectsToSpawOnce[num4], vector, Quaternion.identity, this.npcsParent);
				this.objectsToSpawOnce.RemoveAt(num4);
			}
			else
			{
				Object.Instantiate<GameObject>(this.objectsToSpawn[Random.Range(0, this.objectsToSpawn.Count)], vector, Quaternion.identity, this.npcRaidParent);
			}
		}
	}

	// Token: 0x0400022D RID: 557
	[SerializeField]
	private Transform particleSystemParent;

	// Token: 0x0400022E RID: 558
	[SerializeField]
	private Transform npcsParent;

	// Token: 0x0400022F RID: 559
	[SerializeField]
	private Transform npcRaidParent;

	// Token: 0x04000230 RID: 560
	[SerializeField]
	private List<GameObject> objectsToSpawn;

	// Token: 0x04000231 RID: 561
	[SerializeField]
	private List<GameObject> objectsToSpawOnce;

	// Token: 0x04000232 RID: 562
	[SerializeField]
	private Transform upperLeft;

	// Token: 0x04000233 RID: 563
	[SerializeField]
	private Transform upperLeft2;

	// Token: 0x04000234 RID: 564
	[SerializeField]
	private Transform lowerRight;

	// Token: 0x04000235 RID: 565
	[SerializeField]
	private Transform lowerRight2;

	// Token: 0x04000236 RID: 566
	[SerializeField]
	private GameObject redArea;
}
