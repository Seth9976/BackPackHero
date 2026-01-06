using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000096 RID: 150
public class SpecialLevelProgressComputer : MonoBehaviour
{
	// Token: 0x0600040D RID: 1037 RVA: 0x00014248 File Offset: 0x00012448
	private void Start()
	{
		this.lineRenderer.positionCount = 100;
		for (int i = 0; i < 100; i++)
		{
			float num = (float)i * 3.1415927f * 2f / 100f;
			float num2 = Mathf.Cos(num) * this.radius;
			float num3 = Mathf.Sin(num) * this.radius;
			this.lineRenderer.SetPosition(i, new Vector3(num2, num3, 0f));
		}
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x000142B8 File Offset: 0x000124B8
	private void Update()
	{
		if (!Player.instance)
		{
			return;
		}
		if (this.powerRemaining <= 0f || RoomManager.instance.currentRoom.specialObjective == Room.SpecialObjective.None)
		{
			if (SpecialLevelProgressComputer.activeComputers.Contains(this))
			{
				SpecialLevelProgressComputer.activeComputers.Remove(this);
			}
			if (!this.laserInstance)
			{
				this.laserInstance = Object.Instantiate<GameObject>(this.laserPrefab, base.transform.position, Quaternion.identity);
				this.laserInstance.transform.parent = base.transform;
				Laser component = this.laserInstance.GetComponent<Laser>();
				component.target = Boss.instance.transform;
				component.origin = this.laserOriginPoint;
			}
			this.lineRenderer.startColor = new Color(1f, 1f, 1f, 0f);
			this.lineRenderer.endColor = new Color(1f, 1f, 1f, 0f);
			this.simpleAnimator.PlayAnimation("complete");
			if (!SpecialLevelProgressComputer.fullyChargedComputers.Contains(this))
			{
				SpecialLevelProgressComputer.fullyChargedComputers.Add(this);
			}
			return;
		}
		Vector2 vector = Player.instance.transform.position;
		float num = this.radius - Vector2.Distance(vector, base.transform.position);
		num = Mathf.Clamp(num, 0f, 1f);
		this.lineRenderer.startColor = new Color(1f, 1f, 1f, num);
		this.lineRenderer.endColor = new Color(1f, 1f, 1f, num);
		if (num > 0f)
		{
			this.simpleAnimator.PlayAnimation("download");
			if (!SpecialLevelProgressComputer.activeComputers.Contains(this))
			{
				SpecialLevelProgressComputer.activeComputers.Add(this);
			}
			float num2 = Time.deltaTime * TimeManager.instance.currentTimeScale;
			this.powerRemaining -= num2;
			return;
		}
		this.simpleAnimator.PlayAnimation("off");
		if (SpecialLevelProgressComputer.activeComputers.Contains(this))
		{
			SpecialLevelProgressComputer.activeComputers.Remove(this);
		}
	}

	// Token: 0x04000314 RID: 788
	private static List<SpecialLevelProgressComputer> activeComputers = new List<SpecialLevelProgressComputer>();

	// Token: 0x04000315 RID: 789
	public static List<SpecialLevelProgressComputer> fullyChargedComputers = new List<SpecialLevelProgressComputer>();

	// Token: 0x04000316 RID: 790
	[SerializeField]
	private LineRenderer lineRenderer;

	// Token: 0x04000317 RID: 791
	[SerializeField]
	private float radius = 3f;

	// Token: 0x04000318 RID: 792
	[SerializeField]
	private float powerRemaining = 0.25f;

	// Token: 0x04000319 RID: 793
	[SerializeField]
	private GameObject laserPrefab;

	// Token: 0x0400031A RID: 794
	[SerializeField]
	private GameObject laserInstance;

	// Token: 0x0400031B RID: 795
	[SerializeField]
	private Transform laserOriginPoint;

	// Token: 0x0400031C RID: 796
	[SerializeField]
	private SimpleAnimator simpleAnimator;
}
