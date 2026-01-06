using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class ItemBorderBackground : MonoBehaviour
{
	// Token: 0x06000901 RID: 2305 RVA: 0x0005DE66 File Offset: 0x0005C066
	public static void SetAllColors()
	{
		ItemBorderBackground.SetAllColors(Object.FindObjectsOfType<ItemBorderBackground>().ToList<ItemBorderBackground>());
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x0005DE78 File Offset: 0x0005C078
	public static void SetAllColors(List<ItemBorderBackground> itemBorderBackgrounds)
	{
		foreach (ItemBorderBackground itemBorderBackground in itemBorderBackgrounds)
		{
			itemBorderBackground.SetColor();
		}
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0005DEC4 File Offset: 0x0005C0C4
	public void SetColor()
	{
		Item2 componentInParent = this.GetComponentInParent<Item2>();
		if (componentInParent && componentInParent.IsLocked())
		{
			this.storedColor = new Color(0.7f, 0.25f, 0.25f);
		}
		else
		{
			RaycastHit2D[] array = Physics2D.RaycastAll(this.transform.position, Vector2.zero);
			int i = 0;
			while (i < array.Length)
			{
				RaycastHit2D raycastHit2D = array[i];
				if (raycastHit2D.collider.gameObject.CompareTag("GridSquare"))
				{
					SpriteRenderer component = raycastHit2D.collider.GetComponent<SpriteRenderer>();
					if (component && this)
					{
						this.storedColor = component.color;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}
		SpriteRenderer component2 = this.GetComponent<SpriteRenderer>();
		if (component2)
		{
			component2.color = this.storedColor;
		}
	}

	// Token: 0x04000721 RID: 1825
	public Color storedColor = Color.white;
}
