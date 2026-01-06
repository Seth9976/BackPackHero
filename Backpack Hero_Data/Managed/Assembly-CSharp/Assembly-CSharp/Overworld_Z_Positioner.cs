using System;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class Overworld_Z_Positioner : MonoBehaviour
{
	// Token: 0x06000DED RID: 3565 RVA: 0x0008ACF9 File Offset: 0x00088EF9
	private void Start()
	{
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x0008ACFB File Offset: 0x00088EFB
	private void LateUpdate()
	{
		base.transform.position = this.GetZPosition(base.transform.position);
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x0008AD1C File Offset: 0x00088F1C
	public Vector3 GetZPosition(Vector3 position)
	{
		if (!this.baseOfObject)
		{
			return new Vector3(position.x, position.y, position.y);
		}
		return new Vector3(position.x, position.y, this.baseOfObject.position.y);
	}

	// Token: 0x04000B4B RID: 2891
	[SerializeField]
	public Transform baseOfObject;
}
