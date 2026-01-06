using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class Overworld_ShopKeeper : MonoBehaviour
{
	// Token: 0x06000D9A RID: 3482 RVA: 0x00088298 File Offset: 0x00086498
	private void Start()
	{
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x0008829A File Offset: 0x0008649A
	private void Update()
	{
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0008829C File Offset: 0x0008649C
	public void OpenShopDirect()
	{
		this.OpenShop(false);
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x000882A8 File Offset: 0x000864A8
	public StoreAtlas OpenShop(bool fromConversation = false)
	{
		Canvas component = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>();
		GameObject gameObject = Object.Instantiate<GameObject>(this.shopAtlasPrefab, component.transform);
		gameObject.transform.localPosition = Vector3.zero;
		StoreAtlas component2 = gameObject.GetComponent<StoreAtlas>();
		component2.SetupStoreContents(this.shopItems);
		component2.fromConversation = fromConversation;
		return component2;
	}

	// Token: 0x04000B0D RID: 2829
	[SerializeField]
	private GameObject shopAtlasPrefab;

	// Token: 0x04000B0E RID: 2830
	[SerializeField]
	public List<Overworld_ShopKeeper.ShopItem> shopItems = new List<Overworld_ShopKeeper.ShopItem>();

	// Token: 0x02000414 RID: 1044
	[Serializable]
	public class ShopItem
	{
		// Token: 0x040017E3 RID: 6115
		public GameObject item;

		// Token: 0x040017E4 RID: 6116
		public Missions mission;

		// Token: 0x040017E5 RID: 6117
		public List<Overworld_ResourceManager.Resource> cost = new List<Overworld_ResourceManager.Resource>();

		// Token: 0x040017E6 RID: 6118
		public MetaProgressSaveManager.MetaProgressMarker unlockMarker;
	}
}
