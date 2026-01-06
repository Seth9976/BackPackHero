using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class Follow : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
		if (!this.follow || !this.follow.gameObject.activeInHierarchy)
		{
			this.FindFollowFromTag();
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
	private void Update()
	{
		if (!this.follow || !this.follow.gameObject.activeInHierarchy)
		{
			if (base.transform)
			{
				this.time += Time.deltaTime;
				if (this.time >= 1f)
				{
					this.time = 0f;
					this.FindFollowFromTag();
				}
			}
			return;
		}
		if (this.doLateUpdate)
		{
			return;
		}
		base.transform.position = this.follow.position + this.offset;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002114 File Offset: 0x00000314
	private void LateUpdate()
	{
		if (!this.doLateUpdate || !this.follow || !this.follow.gameObject.activeInHierarchy)
		{
			return;
		}
		base.transform.position = this.follow.position + this.offset;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002170 File Offset: 0x00000370
	public void FindFollowFromTag()
	{
		if (this.backupTag == null || this.backupTag.Length < 1)
		{
			return;
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag(this.backupTag);
		if (gameObject)
		{
			this.follow = gameObject.transform;
		}
	}

	// Token: 0x04000001 RID: 1
	public Transform follow;

	// Token: 0x04000002 RID: 2
	[SerializeField]
	private string backupTag;

	// Token: 0x04000003 RID: 3
	[SerializeField]
	private Vector2 offset = Vector2.zero;

	// Token: 0x04000004 RID: 4
	private float time = 1f;

	// Token: 0x04000005 RID: 5
	[SerializeField]
	private bool doLateUpdate;
}
