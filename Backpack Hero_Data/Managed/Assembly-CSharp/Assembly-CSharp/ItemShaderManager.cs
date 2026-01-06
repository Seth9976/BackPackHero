using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class ItemShaderManager : MonoBehaviour
{
	// Token: 0x060001D5 RID: 469 RVA: 0x0000B94F File Offset: 0x00009B4F
	private void Awake()
	{
		if (ItemShaderManager.main != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		ItemShaderManager.main = this;
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000B970 File Offset: 0x00009B70
	private void OnDestroy()
	{
		if (ItemShaderManager.main == this)
		{
			ItemShaderManager.main = null;
		}
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000B988 File Offset: 0x00009B88
	public static void SetMaterial(GameObject ob, ItemShaderManager.MaterialType materialType)
	{
		if (!ob)
		{
			return;
		}
		Material material = null;
		if (materialType == ItemShaderManager.MaterialType.defaultItem)
		{
			material = ItemShaderManager.main.defaultItemMaterial;
		}
		else if (materialType == ItemShaderManager.MaterialType.defaultCarving)
		{
			material = ItemShaderManager.main.defaultCarvingMaterial;
		}
		else if (materialType == ItemShaderManager.MaterialType.defaultTreat)
		{
			material = ItemShaderManager.main.defaultTreatMaterial;
		}
		else if (materialType == ItemShaderManager.MaterialType.defaultBlessing)
		{
			material = ItemShaderManager.main.defaultBlessingMaterial;
		}
		SpriteRenderer[] componentsInChildren = ob.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].material = material;
		}
	}

	// Token: 0x04000141 RID: 321
	public static ItemShaderManager main;

	// Token: 0x04000142 RID: 322
	public Material defaultItemMaterial;

	// Token: 0x04000143 RID: 323
	public Material defaultCarvingMaterial;

	// Token: 0x04000144 RID: 324
	public Material defaultBlessingMaterial;

	// Token: 0x04000145 RID: 325
	public Material defaultTreatMaterial;

	// Token: 0x02000276 RID: 630
	public enum MaterialType
	{
		// Token: 0x04000F4D RID: 3917
		defaultItem,
		// Token: 0x04000F4E RID: 3918
		defaultCarving,
		// Token: 0x04000F4F RID: 3919
		defaultTreat,
		// Token: 0x04000F50 RID: 3920
		defaultBlessing
	}
}
