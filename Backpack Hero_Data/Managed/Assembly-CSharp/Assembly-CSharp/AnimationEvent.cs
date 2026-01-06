using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class AnimationEvent : MonoBehaviour
{
	// Token: 0x060008A0 RID: 2208 RVA: 0x0005B5EE File Offset: 0x000597EE
	private void Start()
	{
		this.gameFlowManager = GameFlowManager.main;
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0005B5FB File Offset: 0x000597FB
	private void Update()
	{
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x0005B5FD File Offset: 0x000597FD
	public void PerformActionEffect()
	{
		this.gameFlowManager.PerformActionEffect();
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0005B60A File Offset: 0x0005980A
	public void CompleteAction()
	{
		this.gameFlowManager.CompleteAction();
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0005B617 File Offset: 0x00059817
	public void DestroyMe()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x0005B624 File Offset: 0x00059824
	public void DestroyMyParent()
	{
		Object.Destroy(base.transform.parent.gameObject);
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0005B63B File Offset: 0x0005983B
	public void DisableAnimator()
	{
		base.GetComponent<Animator>().enabled = false;
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0005B649 File Offset: 0x00059849
	public void Disable()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0005B657 File Offset: 0x00059857
	public void DontFindMe()
	{
		this.dontFindMe = true;
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0005B660 File Offset: 0x00059860
	public void MoveAway()
	{
		base.transform.position = new Vector3(-999f, -999f);
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0005B67C File Offset: 0x0005987C
	public void DisableAndNestUnderDungeon()
	{
		base.gameObject.SetActive(false);
		if (!base.GetComponentInChildren<EventNPC>())
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0005B6A4 File Offset: 0x000598A4
	public void PlayAnimation(string x)
	{
		Animator component = base.GetComponent<Animator>();
		component.enabled = true;
		if (component.GetCurrentAnimatorStateInfo(0).IsName(x))
		{
			component.Play(x, 0, 0f);
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0005B6DE File Offset: 0x000598DE
	public void PlaySFX(string x)
	{
		SoundManager.main.PlaySFX(x);
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0005B6EB File Offset: 0x000598EB
	public void HideCursor()
	{
		DigitalCursor.main.Hide();
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0005B6F7 File Offset: 0x000598F7
	public void ShowCursor()
	{
		DigitalCursor.main.Show();
	}

	// Token: 0x040006CF RID: 1743
	[HideInInspector]
	public bool dontFindMe;

	// Token: 0x040006D0 RID: 1744
	private GameFlowManager gameFlowManager;
}
