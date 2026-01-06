using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class CharacterSelectionCharacter : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00002BED File Offset: 0x00000DED
	private void Start()
	{
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002BEF File Offset: 0x00000DEF
	private void Update()
	{
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002BF1 File Offset: 0x00000DF1
	private void MakeReferences()
	{
		if (!this.animator)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002C0C File Offset: 0x00000E0C
	public void SelectThis()
	{
		this.MakeReferences();
		base.gameObject.SetActive(true);
		this.animator.Play("characterIn");
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002C30 File Offset: 0x00000E30
	public void DeselectThis()
	{
		this.MakeReferences();
		this.animator.Play("characterOut");
	}

	// Token: 0x04000010 RID: 16
	[SerializeField]
	public Character.CharacterName characterName;

	// Token: 0x04000011 RID: 17
	private Animator animator;
}
