using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class Slash : MonoBehaviour
{
	// Token: 0x060003EF RID: 1007 RVA: 0x000135E0 File Offset: 0x000117E0
	private void Update()
	{
		Sprite sprite = this.spriteRenderer.sprite;
		if (this.sprites.Contains(sprite))
		{
			int num = this.sprites.IndexOf(sprite);
			foreach (GameObject gameObject in this.colliderGameObjects)
			{
				gameObject.SetActive(false);
			}
			this.colliderGameObjects[num].SetActive(true);
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00013678 File Offset: 0x00011878
	private void OnTriggerEnter2D(Collider2D other)
	{
		Destructible component = other.GetComponent<Destructible>();
		if (component && !this.destructiblesHit.Contains(component))
		{
			this.damageDealer.ApplyEffects(other.gameObject);
			this.destructiblesHit.Add(component);
		}
	}

	// Token: 0x040002FC RID: 764
	[SerializeField]
	private DamageDealer damageDealer;

	// Token: 0x040002FD RID: 765
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x040002FE RID: 766
	[SerializeField]
	private List<Sprite> sprites;

	// Token: 0x040002FF RID: 767
	[SerializeField]
	private List<GameObject> colliderGameObjects;

	// Token: 0x04000300 RID: 768
	[SerializeField]
	private List<Destructible> destructiblesHit;
}
