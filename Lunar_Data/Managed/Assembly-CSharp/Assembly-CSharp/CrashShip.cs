using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class CrashShip : MonoBehaviour
{
	// Token: 0x06000104 RID: 260 RVA: 0x00006730 File Offset: 0x00004930
	private void Update()
	{
		if (!SingleUI.IsViewingPopUp())
		{
			this.time += Time.deltaTime;
		}
		if (this.time > 7.5f && !this.goThisWayIndicator.activeSelf)
		{
			this.goThisWayIndicator.SetActive(true);
		}
		if (this.started)
		{
			return;
		}
		if (SingleUI.IsViewingPopUp())
		{
			this.animator.enabled = false;
			return;
		}
		this.started = true;
		if (!this.animator.enabled)
		{
			this.started = true;
			this.animator.enabled = true;
			this.animator.Play("ShipCrash", 0, 0f);
			this.animator.speed = 1f;
		}
	}

	// Token: 0x06000105 RID: 261 RVA: 0x000067E8 File Offset: 0x000049E8
	public void ActivatePlayer()
	{
		this.playerSpawn.SetActive(true);
		if (Singleton.instance.selectedCharacter.entranceAnimator)
		{
			this.playerSpawnAnimator.runtimeAnimatorController = Singleton.instance.selectedCharacter.entranceAnimator;
		}
		base.StartCoroutine(this.SetLerp());
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000683E File Offset: 0x00004A3E
	private IEnumerator SetLerp()
	{
		while (TimeManager.instance.currentTimeScale > 0.9f)
		{
			yield return null;
		}
		this.timeScaler.SetLerp();
		yield break;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000684D File Offset: 0x00004A4D
	public void ShakeScreen()
	{
		CameraShaker.instance.Shake(0.25f, 0.125f);
		SoundManager.instance.PlaySFX("wallCrashIntro", double.PositiveInfinity);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0000687B File Offset: 0x00004A7B
	public void RemoveTutorialPlayer()
	{
		if (!Player.instance)
		{
			return;
		}
		Object.Destroy(Player.instance.gameObject);
	}

	// Token: 0x040000C1 RID: 193
	[SerializeField]
	private GameObject playerSpawn;

	// Token: 0x040000C2 RID: 194
	[SerializeField]
	private Animator playerSpawnAnimator;

	// Token: 0x040000C3 RID: 195
	[SerializeField]
	private Animator animator;

	// Token: 0x040000C4 RID: 196
	[SerializeField]
	private GameObject goThisWayIndicator;

	// Token: 0x040000C5 RID: 197
	[SerializeField]
	private AnimatorTimeScaler timeScaler;

	// Token: 0x040000C6 RID: 198
	private bool setAnimationToZero;

	// Token: 0x040000C7 RID: 199
	private bool started;

	// Token: 0x040000C8 RID: 200
	private float time;
}
