using System;
using UnityEngine;

// Token: 0x02000073 RID: 115
public class PolygonShaper : MonoBehaviour
{
	// Token: 0x06000342 RID: 834 RVA: 0x00010A28 File Offset: 0x0000EC28
	private void Start()
	{
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00010A2C File Offset: 0x0000EC2C
	public void SetRectangle(Vector2 origin, Vector2 destination, float width)
	{
		Vector2 vector = destination - origin;
		Vector2 vector2 = new Vector2(-vector.y, vector.x).normalized * width / 2f;
		Vector2[] array = new Vector2[]
		{
			origin + vector2,
			destination + vector2,
			destination - vector2,
			origin - vector2
		};
		this.polygonCollider2D.points = array;
	}

	// Token: 0x0400027D RID: 637
	[SerializeField]
	private PolygonCollider2D polygonCollider2D;
}
