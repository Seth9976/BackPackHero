using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class GridTester : MonoBehaviour
{
	// Token: 0x06000191 RID: 401 RVA: 0x0000A205 File Offset: 0x00008405
	private void Start()
	{
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000A207 File Offset: 0x00008407
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(base.transform.position, new Vector3(1f, 1f, 1f));
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000A238 File Offset: 0x00008438
	private void Update()
	{
		base.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 vector = this.gridObject.CalculateDifferenceToGrid(default(Vector3));
		base.transform.position = new Vector3(base.transform.position.x + vector.x, base.transform.position.y + vector.y, base.transform.position.z);
		if (Input.GetKeyDown(KeyCode.U))
		{
			foreach (GridObject gridObject in GridObject.GetItemsAtPosition(base.transform.position))
			{
				Debug.Log(gridObject.name);
			}
		}
	}

	// Token: 0x04000104 RID: 260
	[SerializeField]
	private GridObject gridObject;
}
