using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class PickUpSpawner : MonoBehaviour
{
	// Token: 0x060002F5 RID: 757 RVA: 0x0000F299 File Offset: 0x0000D499
	private void OnEnable()
	{
		PickUpSpawner.instance = this;
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0000F2A1 File Offset: 0x0000D4A1
	private void OnDisable()
	{
		PickUpSpawner.instance = null;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0000F2A9 File Offset: 0x0000D4A9
	private void Update()
	{
		this.timeSinceLastMedkit += Time.deltaTime * TimeManager.instance.currentTimeScale;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
	public void CreatePickUp(Vector2 position)
	{
		if (!HordeRemainingDisplay.instance || HordeRemainingDisplay.instance.timeRemaining <= 4.25f)
		{
			return;
		}
		if (HealthBarMaster.instance.IsMostlyHealed())
		{
			return;
		}
		if (PickUp.pickups.Count >= 2)
		{
			return;
		}
		if ((float)Random.Range(0, 100) > Mathf.Lerp(0f, (float)this.chanceToSpawn, this.timeSinceLastMedkit / 10f))
		{
			return;
		}
		Object.Instantiate<GameObject>(this.pickUpPrefab, position, Quaternion.identity, RoomManager.instance.roomContents);
		this.timeSinceLastMedkit = 0f;
	}

	// Token: 0x04000242 RID: 578
	public static PickUpSpawner instance;

	// Token: 0x04000243 RID: 579
	[SerializeField]
	private GameObject pickUpPrefab;

	// Token: 0x04000244 RID: 580
	private float timeSinceLastMedkit;

	// Token: 0x04000245 RID: 581
	private int chanceToSpawn = 25;
}
