using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class PlayerShooter : MonoBehaviour
{
	// Token: 0x0600033A RID: 826 RVA: 0x0001052E File Offset: 0x0000E72E
	private void Start()
	{
		this.timeForNextShot = this.timeBetweenShots;
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0001053C File Offset: 0x0000E73C
	private void Update()
	{
		if (!this.automaticallyShoots)
		{
			return;
		}
		this.timeForNextShot += Time.deltaTime * TimeManager.instance.currentTimeScale;
		if (this.timeForNextShot >= this.timeBetweenShots)
		{
			this.timeForNextShot = 0f;
			this.Shoot();
		}
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00010590 File Offset: 0x0000E790
	private int GetNumberOfShots()
	{
		int num = this.numberOfShots;
		if (this.modifierTypes.Contains(Modifier.ModifierEffect.Type.NumberOfStakes))
		{
			num += (int)ModifierManager.instance.GetModifierValue(Modifier.ModifierEffect.Type.NumberOfStakes);
		}
		return num;
	}

	// Token: 0x0600033D RID: 829 RVA: 0x000105C4 File Offset: 0x0000E7C4
	public void Shoot()
	{
		Vector3 vector = base.transform.position;
		switch (this.origin)
		{
		case PlayerShooter.Origin.Player:
			vector = Player.instance.transform.position;
			break;
		case PlayerShooter.Origin.ThisTransform:
			vector = base.transform.position;
			break;
		case PlayerShooter.Origin.RandomEnemy:
		{
			Enemy random = Enemy.GetRandom(base.transform.position, this.maxDistance);
			if (!random)
			{
				return;
			}
			vector = random.transform.position;
			break;
		}
		case PlayerShooter.Origin.RandomPositionInRoom:
			vector = RoomManager.instance.currentRoom.GetRandomPosition();
			break;
		case PlayerShooter.Origin.RandomPositionNearPlayer:
			vector = Player.instance.transform.position + Random.insideUnitCircle * this.maxDistance;
			vector = RoomManager.instance.currentRoom.ClampPositionToComfortablyInSideRoom(vector);
			break;
		}
		Transform transform = RoomManager.instance.roomContents;
		if (this.parent == PlayerShooter.Parent.Player)
		{
			transform = Player.instance.transform;
		}
		switch (this.aimAt)
		{
		case PlayerShooter.AimAt.None:
			Object.Instantiate<GameObject>(this.bulletPrefab, vector, Quaternion.identity, transform);
			return;
		case PlayerShooter.AimAt.ClosestEnemy:
		{
			Enemy closest = Enemy.GetClosest(vector, this.maxDistance);
			if (!closest)
			{
				return;
			}
			int num = this.GetNumberOfShots();
			for (int i = 0; i < num; i++)
			{
				Vector2 vector2 = closest.transform.position - vector;
				vector2.Normalize();
				SoundManager.instance.PlaySFX("shoot", double.PositiveInfinity);
				GameObject gameObject = Object.Instantiate<GameObject>(this.bulletPrefab, vector, Quaternion.identity, transform);
				Vector2 vector3 = Quaternion.Euler(0f, 0f, (float)num * this.angleBetweenShots / 2f - (float)i * this.angleBetweenShots) * vector2;
				Bullet component = gameObject.GetComponent<Bullet>();
				if (component)
				{
					component.SetVelocity(vector3);
				}
				else
				{
					gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(vector2.y, vector2.x) * 57.29578f);
					gameObject.transform.localPosition += gameObject.transform.rotation * this.spawnOffset;
				}
			}
			return;
		}
		case PlayerShooter.AimAt.RandomDirection:
		{
			Vector2 insideUnitCircle = Random.insideUnitCircle;
			SoundManager.instance.PlaySFX("shoot", double.PositiveInfinity);
			Object.Instantiate<GameObject>(this.bulletPrefab, vector, Quaternion.identity, transform).GetComponent<Bullet>().SetVelocity(insideUnitCircle);
			return;
		}
		case PlayerShooter.AimAt.FacingDirection:
		{
			int num2 = this.GetNumberOfShots();
			for (int j = 0; j < num2; j++)
			{
				Vector2 vector4 = ((this.origin == PlayerShooter.Origin.Player) ? Player.instance.GetFacingDirection() : base.transform.right);
				SoundManager.instance.PlaySFX("shoot", double.PositiveInfinity);
				Bullet component2 = Object.Instantiate<GameObject>(this.bulletPrefab, vector, Quaternion.identity, transform).GetComponent<Bullet>();
				Vector2 vector5 = Quaternion.Euler(0f, 0f, (float)num2 * this.angleBetweenShots / 2f - (float)j * this.angleBetweenShots) * vector4;
				component2.SetVelocity(vector5);
			}
			return;
		}
		case PlayerShooter.AimAt.GrenadeGroundBelow:
			Object.Instantiate<GameObject>(this.bulletPrefab, vector, Quaternion.identity, transform).GetComponent<Grenade>().destination = vector + Vector3.down * 1f;
			return;
		default:
			return;
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0001098E File Offset: 0x0000EB8E
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(base.transform.position, this.maxDistance);
	}

	// Token: 0x04000270 RID: 624
	public List<Modifier.ModifierEffect.Type> modifierTypes = new List<Modifier.ModifierEffect.Type>();

	// Token: 0x04000271 RID: 625
	[SerializeField]
	private PlayerShooter.Origin origin;

	// Token: 0x04000272 RID: 626
	[SerializeField]
	private PlayerShooter.Parent parent;

	// Token: 0x04000273 RID: 627
	[SerializeField]
	private GameObject bulletPrefab;

	// Token: 0x04000274 RID: 628
	[SerializeField]
	private Vector3 spawnOffset;

	// Token: 0x04000275 RID: 629
	[SerializeField]
	private bool automaticallyShoots = true;

	// Token: 0x04000276 RID: 630
	[SerializeField]
	private float timeBetweenShots = 0.5f;

	// Token: 0x04000277 RID: 631
	[SerializeField]
	private int numberOfShots = 1;

	// Token: 0x04000278 RID: 632
	[SerializeField]
	private float angleBetweenShots;

	// Token: 0x04000279 RID: 633
	[SerializeField]
	public float maxDistance = float.PositiveInfinity;

	// Token: 0x0400027A RID: 634
	[SerializeField]
	public PlayerShooter.AimAt aimAt = PlayerShooter.AimAt.ClosestEnemy;

	// Token: 0x0400027B RID: 635
	private float timeForNextShot;

	// Token: 0x020000FB RID: 251
	public enum Origin
	{
		// Token: 0x040004A3 RID: 1187
		Player,
		// Token: 0x040004A4 RID: 1188
		ThisTransform,
		// Token: 0x040004A5 RID: 1189
		RandomEnemy,
		// Token: 0x040004A6 RID: 1190
		RandomPositionInRoom,
		// Token: 0x040004A7 RID: 1191
		RandomPositionNearPlayer
	}

	// Token: 0x020000FC RID: 252
	public enum Parent
	{
		// Token: 0x040004A9 RID: 1193
		None,
		// Token: 0x040004AA RID: 1194
		Player
	}

	// Token: 0x020000FD RID: 253
	public enum AimAt
	{
		// Token: 0x040004AC RID: 1196
		None,
		// Token: 0x040004AD RID: 1197
		ClosestEnemy,
		// Token: 0x040004AE RID: 1198
		RandomDirection,
		// Token: 0x040004AF RID: 1199
		FacingDirection,
		// Token: 0x040004B0 RID: 1200
		GrenadeGroundBelow
	}
}
