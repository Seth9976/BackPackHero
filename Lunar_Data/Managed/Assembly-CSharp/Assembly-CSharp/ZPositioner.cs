using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class ZPositioner : MonoBehaviour
{
	// Token: 0x060004A7 RID: 1191 RVA: 0x00016AD1 File Offset: 0x00014CD1
	private void Start()
	{
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00016AD4 File Offset: 0x00014CD4
	private void Update()
	{
		if (!RoomManager.instance.currentRoom)
		{
			return;
		}
		Bounds bounds = RoomManager.instance.currentRoom.bounds;
		float num = Mathf.Lerp(this.zPositionValues.x, this.zPositionValues.y, (base.transform.position.y - bounds.min.y) / bounds.size.y);
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, num);
	}

	// Token: 0x04000398 RID: 920
	[SerializeField]
	private Vector2 zPositionValues;
}
