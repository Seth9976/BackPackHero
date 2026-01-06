using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class MenuPhysicsObjects : MonoBehaviour
{
	// Token: 0x0600093C RID: 2364 RVA: 0x0005F8EF File Offset: 0x0005DAEF
	private void Start()
	{
		this.menuDragger = Object.FindObjectOfType<MenuDragger>();
		this.rigidbody2D = base.GetComponent<Rigidbody2D>();
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x0005F908 File Offset: 0x0005DB08
	private void Update()
	{
		if (this.time > 0f)
		{
			this.time -= Time.deltaTime;
			return;
		}
		if (!this.spriteRenderer.isVisible)
		{
			Object.Destroy(base.gameObject);
		}
		if (!Input.GetMouseButton(0) && this.isDragging)
		{
			if (this.menuDragger.GetComponent<Joint2D>().connectedBody == this.rigidbody2D)
			{
				this.rigidbody2D.mass = 1f;
				this.rigidbody2D.angularDrag = 0f;
				this.menuDragger.GetComponent<Joint2D>().connectedBody = null;
				this.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.None;
			}
			this.isDragging = false;
		}
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0005F9BE File Offset: 0x0005DBBE
	private void FixedUpdate()
	{
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0005F9C0 File Offset: 0x0005DBC0
	private void OnMouseDown()
	{
		this.rigidbody2D.mass = 30f;
		this.rigidbody2D.angularDrag = 1f;
		this.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		this.menuDragger.GetComponent<Joint2D>().connectedBody = this.rigidbody2D;
		this.menuDragger.GetComponent<DistanceJoint2D>().connectedAnchor = base.transform.InverseTransformPoint(this.menuDragger.transform.position);
		this.isDragging = true;
	}

	// Token: 0x04000750 RID: 1872
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000751 RID: 1873
	private Rigidbody2D rigidbody2D;

	// Token: 0x04000752 RID: 1874
	private float time = 1f;

	// Token: 0x04000753 RID: 1875
	[SerializeField]
	private float speed = 100f;

	// Token: 0x04000754 RID: 1876
	private bool isDragging;

	// Token: 0x04000755 RID: 1877
	private MenuDragger menuDragger;
}
