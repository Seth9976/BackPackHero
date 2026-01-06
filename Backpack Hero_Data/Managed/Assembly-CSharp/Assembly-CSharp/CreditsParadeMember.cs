using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class CreditsParadeMember : MonoBehaviour
{
	// Token: 0x060000CD RID: 205 RVA: 0x00006BF1 File Offset: 0x00004DF1
	private void Awake()
	{
		CreditsParadeMember.usedControllers = new List<RuntimeAnimatorController>();
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00006BFD File Offset: 0x00004DFD
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.SetController();
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00006C14 File Offset: 0x00004E14
	private void Update()
	{
		base.transform.position += Vector3.right * Time.deltaTime * this.speed;
		if (base.transform.localPosition.x > this.rightSide)
		{
			base.transform.position -= Vector3.right * this.rightSide * 2f;
			this.SetController();
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00006CA0 File Offset: 0x00004EA0
	private void SetController()
	{
		if (this.animator.runtimeAnimatorController != null)
		{
			CreditsParadeMember.usedControllers.Remove(this.animator.runtimeAnimatorController);
		}
		if (this.controllers.Count == 0 || CreditsParadeMember.usedControllers.Count == this.controllers.Count)
		{
			return;
		}
		int num = Random.Range(0, this.controllers.Count);
		RuntimeAnimatorController runtimeAnimatorController = this.controllers[num];
		while (CreditsParadeMember.usedControllers.Contains(runtimeAnimatorController))
		{
			num++;
			if (num >= this.controllers.Count)
			{
				num = 0;
			}
			runtimeAnimatorController = this.controllers[num];
		}
		this.animator.runtimeAnimatorController = runtimeAnimatorController;
		CreditsParadeMember.usedControllers.Add(runtimeAnimatorController);
		this.animator.Play("move_right");
	}

	// Token: 0x0400006F RID: 111
	private static List<RuntimeAnimatorController> usedControllers;

	// Token: 0x04000070 RID: 112
	[SerializeField]
	private List<RuntimeAnimatorController> controllers;

	// Token: 0x04000071 RID: 113
	[SerializeField]
	private float speed = 1f;

	// Token: 0x04000072 RID: 114
	[SerializeField]
	private float rightSide = 11f;

	// Token: 0x04000073 RID: 115
	private Animator animator;
}
