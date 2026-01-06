using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class DisableWaterTile : MonoBehaviour
{
	// Token: 0x0600015A RID: 346 RVA: 0x00009698 File Offset: 0x00007898
	public static void DisableAllWaterTiles()
	{
		DisableWaterTile[] array = Object.FindObjectsOfType<DisableWaterTile>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].DisableTile();
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000096C1 File Offset: 0x000078C1
	private void Update()
	{
	}

	// Token: 0x0600015C RID: 348 RVA: 0x000096C3 File Offset: 0x000078C3
	private void Start()
	{
	}

	// Token: 0x0600015D RID: 349 RVA: 0x000096C8 File Offset: 0x000078C8
	public void DisableTile()
	{
		foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(base.transform.position + this.bridgeCollider.offset, this.bridgeCollider.size, 0f, Vector2.zero))
		{
			if (raycastHit2D.collider.gameObject.CompareTag("Overworld_Water"))
			{
				raycastHit2D.collider.enabled = false;
				this.waterTiles.Add(raycastHit2D.collider);
			}
		}
		Overworld_Manager.main.UpdateMap();
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000976C File Offset: 0x0000796C
	public void ReEnableTile()
	{
		foreach (Collider2D collider2D in this.waterTiles)
		{
			collider2D.enabled = true;
			BoxCollider2D component = collider2D.GetComponent<BoxCollider2D>();
			if (component)
			{
				foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(component.transform.position + component.offset, component.size, 0f, Vector2.zero))
				{
					if (raycastHit2D.collider.gameObject.layer == 8)
					{
						raycastHit2D.collider.gameObject.transform.position = new Vector3(-30f, 0f, raycastHit2D.collider.gameObject.transform.position.z);
					}
				}
			}
		}
	}

	// Token: 0x040000E5 RID: 229
	private List<Collider2D> waterTiles = new List<Collider2D>();

	// Token: 0x040000E6 RID: 230
	[SerializeField]
	private BoxCollider2D bridgeCollider;
}
