using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class BasicPopUp : MonoBehaviour
{
	// Token: 0x0600039F RID: 927 RVA: 0x00015E94 File Offset: 0x00014094
	private void Start()
	{
		this.animator = base.GetComponentInChildren<Animator>();
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00015EA2 File Offset: 0x000140A2
	private void Update()
	{
		if (!this.animator.gameObject.activeInHierarchy)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00015EC1 File Offset: 0x000140C1
	public void Close()
	{
		this.animator.Play("Out");
	}

	// Token: 0x04000291 RID: 657
	private Animator animator;
}
