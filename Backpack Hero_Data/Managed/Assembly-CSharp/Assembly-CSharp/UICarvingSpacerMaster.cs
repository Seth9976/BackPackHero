using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class UICarvingSpacerMaster : MonoBehaviour
{
	// Token: 0x06000485 RID: 1157 RVA: 0x0002BB28 File Offset: 0x00029D28
	private void Start()
	{
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0002BB2C File Offset: 0x00029D2C
	private void Update()
	{
		if (!this.backpack)
		{
			this.backpack = GameObject.FindGameObjectWithTag("Inventory");
			if (this.backpack)
			{
				this.differenceY = base.transform.position.y - this.backpack.transform.position.y;
				return;
			}
		}
		else
		{
			base.transform.position = new Vector3(this.backpack.transform.position.x, this.backpack.transform.position.y + this.differenceY, base.transform.position.z);
		}
	}

	// Token: 0x04000357 RID: 855
	[SerializeField]
	private GameObject backpack;

	// Token: 0x04000358 RID: 856
	private float differenceY;
}
