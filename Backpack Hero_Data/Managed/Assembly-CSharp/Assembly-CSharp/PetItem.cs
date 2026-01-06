using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class PetItem : MonoBehaviour
{
	// Token: 0x06000849 RID: 2121 RVA: 0x00056F8C File Offset: 0x0005518C
	private void Start()
	{
		this.itemMovement = base.GetComponent<ItemMovement>();
		this.myItem = base.GetComponent<Item2>();
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00056FA6 File Offset: 0x000551A6
	private void Update()
	{
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00056FA8 File Offset: 0x000551A8
	private void LateUpdate()
	{
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00056FAA File Offset: 0x000551AA
	private void OnMouseUpAsButton()
	{
		DigitalCursor.ControlStyle controlStyle = DigitalCursor.main.controlStyle;
	}

	// Token: 0x0400066C RID: 1644
	[SerializeField]
	public PetMaster petMaster;

	// Token: 0x0400066D RID: 1645
	[SerializeField]
	public ItemMovement itemMovement;

	// Token: 0x0400066E RID: 1646
	[SerializeField]
	public Item2 myItem;
}
