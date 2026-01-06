using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000067 RID: 103
public class ItemSpawnMenuButton : MonoBehaviour
{
	// Token: 0x06000207 RID: 519 RVA: 0x0000CA6E File Offset: 0x0000AC6E
	private void Start()
	{
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000CA70 File Offset: 0x0000AC70
	public void ReplaceWithButton()
	{
		GameObject button = Object.Instantiate<GameObject>(this.replacePrefab, base.transform.position, base.transform.rotation, base.transform.parent);
		button.transform.SetSiblingIndex(base.gameObject.transform.GetSiblingIndex());
		button.GetComponentInChildren<TextMeshProUGUI>().text = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(this.item.name));
		SpriteRenderer componentInChildren = this.item.GetComponentInChildren<SpriteRenderer>();
		button.GetComponent<ItemAtlasButton>().SetSprite(componentInChildren, Item2.GetDisplayName(this.item.name));
		button.GetComponent<Button>().onClick.AddListener(delegate
		{
			ItemSpawnMenu.main.SpawnItem(this.item, button.transform);
		});
		ItemSpawnMenuButton component = button.GetComponent<ItemSpawnMenuButton>();
		component.item = this.item;
		component.placeholder = false;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000CB7C File Offset: 0x0000AD7C
	private void Update()
	{
		if (base.transform.position.y < this.upperBound && base.transform.position.y > this.lowerBound)
		{
			if (this.placeholder)
			{
				this.ReplaceWithButton();
				return;
			}
		}
		else if (!this.placeholder)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.replacePrefab, base.transform.position, base.transform.rotation, base.transform.parent);
			gameObject.transform.SetSiblingIndex(base.gameObject.transform.GetSiblingIndex());
			ItemSpawnMenuButton component = gameObject.GetComponent<ItemSpawnMenuButton>();
			component.item = this.item;
			component.placeholder = true;
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000157 RID: 343
	public bool placeholder = true;

	// Token: 0x04000158 RID: 344
	[SerializeField]
	private GameObject replacePrefab;

	// Token: 0x04000159 RID: 345
	public Item2 item;

	// Token: 0x0400015A RID: 346
	private float upperBound = 2f;

	// Token: 0x0400015B RID: 347
	private float lowerBound = -7f;
}
