using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200015E RID: 350
public class PetMaster : MonoBehaviour
{
	// Token: 0x06000DF9 RID: 3577 RVA: 0x0008AEBC File Offset: 0x000890BC
	private void Start()
	{
		this.player = Player.main;
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		PetMaster.petMasters.Add(this);
		if (!this.petInventory)
		{
			this.petInventory = Object.Instantiate<GameObject>(this.petInventoryPrefab, new Vector3(4f, 9f, 0f), Quaternion.identity, this.gameManager.inventoryTransform).transform;
			this.petInventory.SetAsFirstSibling();
			ModularBackpack componentInChildren = this.petInventory.GetComponentInChildren<ModularBackpack>();
			componentInChildren.bottomDecalRenderer.sprite = this.petItemPrefab.GetComponent<SpriteRenderer>().sprite;
			componentInChildren.bottomDecalRenderer.material = this.wigglyMaterial;
			componentInChildren.SetBackpackSprites();
		}
		if (!this.petItem)
		{
			this.petItem = Object.Instantiate<GameObject>(this.petItemPrefab, new Vector3(0f, 10f, 0f), Quaternion.identity).GetComponent<PetItem>();
		}
		this.petItem.petMaster = this;
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x0008AFD0 File Offset: 0x000891D0
	private void OnDestroy()
	{
		if (PetMaster.petMasters.Contains(this))
		{
			PetMaster.petMasters.Remove(this);
		}
		if (this.combatPet)
		{
			Object.Destroy(this.combatPet);
		}
		if (this.petInventory)
		{
			Object.Destroy(this.petInventory.gameObject);
		}
		if (this.petItem)
		{
			Object.Destroy(this.petItem);
		}
		for (int i = 0; i < this.itemsInside.Count; i++)
		{
			if (this.itemsInside[i])
			{
				GameObject gameObject = this.itemsInside[i];
				if (gameObject)
				{
					Object.Destroy(gameObject);
				}
			}
		}
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x0008B087 File Offset: 0x00089287
	private void Update()
	{
		if (!this.petItem || (this.petItem.myItem && this.petItem.myItem.destroyed))
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x0008B0C5 File Offset: 0x000892C5
	public void ShowOutline()
	{
		if (this.combatPetVisible)
		{
			this.combatPet;
		}
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x0008B0DB File Offset: 0x000892DB
	public void HideOutline()
	{
		if (this.combatPetVisible)
		{
			this.combatPet;
		}
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x0008B0F4 File Offset: 0x000892F4
	public void SetHp(int num)
	{
		this.health = Mathf.Clamp(num, 0, this.maxHealth);
		if (this.combatPetCom)
		{
			this.combatPetCom.stats.SetHealth(this.health);
			this.combatPetCom.stats.SetMaxHP(this.maxHealth);
		}
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x0008B150 File Offset: 0x00089350
	public static PetMaster GetPetMasterFromInventory(Transform myGridTransform)
	{
		foreach (PetMaster petMaster in PetMaster.petMasters)
		{
			if (myGridTransform && petMaster.petInventory == myGridTransform.transform.parent)
			{
				return petMaster;
			}
		}
		return null;
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x0008B1C4 File Offset: 0x000893C4
	public static Status GetStatusFromInventory(Transform mygridTransform, Player player)
	{
		foreach (PetMaster petMaster in PetMaster.petMasters)
		{
			if (mygridTransform && petMaster.petInventory == mygridTransform.transform.parent)
			{
				if (petMaster.combatPet)
				{
					return petMaster.combatPetCom.stats;
				}
				return null;
			}
		}
		return player.stats;
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x0008B258 File Offset: 0x00089458
	public static Transform GetInventoryFromStats(Status stats)
	{
		if (stats.playerScript)
		{
			return GameManager.main.mainGridParent;
		}
		foreach (PetMaster petMaster in PetMaster.petMasters)
		{
			if (petMaster.combatPetCom && petMaster.combatPetCom.stats == stats)
			{
				return petMaster.petInventory.GetChild(0);
			}
		}
		return null;
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x0008B2F0 File Offset: 0x000894F0
	public static Status GetRightMostCombatPetStats()
	{
		Player main = Player.main;
		Status status = main.stats;
		float num = -999f;
		List<Status> list = new List<Status>();
		foreach (CombatPet combatPet in Object.FindObjectsOfType<CombatPet>().ToList<CombatPet>())
		{
			list.Add(combatPet.stats);
		}
		list.Add(main.stats);
		foreach (Status status2 in list)
		{
			if (status2.transform.position.x > num && status2.health > 0)
			{
				num = status2.transform.position.x;
				status = status2;
			}
		}
		return status;
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x0008B3E0 File Offset: 0x000895E0
	public static void StartCombat()
	{
		foreach (PetMaster petMaster in PetMaster.petMasters)
		{
			petMaster.hasBeenSummoned = false;
		}
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x0008B430 File Offset: 0x00089630
	public void CreateCombatPet(int num)
	{
		if (this.combatPetVisible)
		{
			return;
		}
		this.combatPet = Object.Instantiate<GameObject>(this.combatPetPrefab, new Vector3(this.player.transform.position.x, this.combatPetPrefab.transform.position.y, this.player.transform.position.z), Quaternion.identity, this.player.transform.parent);
		this.combatPetCom = this.combatPet.GetComponent<CombatPet>();
		GameObject gameObject = Object.Instantiate<GameObject>(this.statusPrefab, new Vector3(this.player.transform.position.x, this.combatPetPrefab.transform.position.y, this.player.transform.position.z), Quaternion.identity);
		Object.Instantiate<GameObject>(this.energyPrefab, new Vector3(this.player.transform.position.x, this.combatPetPrefab.transform.position.y, this.player.transform.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("PrimaryCanvas").transform);
		Status component = gameObject.GetComponent<Status>();
		component.parent = this.combatPet;
		component.SetMaxHP(this.maxHealth);
		component.SetHealth(this.health);
		List<Item2> list = new List<Item2>();
		foreach (GameObject gameObject2 in this.itemsInside)
		{
			Item2 component2 = gameObject2.GetComponent<Item2>();
			if (component2)
			{
				list.Add(component2);
			}
		}
		this.gameFlowManager.CheckConstants();
		if (!this.hasBeenSummoned)
		{
			this.gameFlowManager.ConsiderAllEffectsPublicList(Item2.Trigger.ActionTrigger.onCombatStart, list, null, new List<PetMaster> { this }, false, true);
			this.hasBeenSummoned = true;
		}
		this.gameFlowManager.ConsiderAllEffectsPublicList(Item2.Trigger.ActionTrigger.onTurnStart, list, null, new List<PetMaster> { this }, false, true);
		GameObject gameObject3 = Object.Instantiate<GameObject>(this.petProxyPrefab, this.player.transform.position + new Vector3(-0.5f, 0.2f, 0f), Quaternion.identity);
		ItemAnimationProxy component3 = gameObject3.GetComponent<ItemAnimationProxy>();
		component3.ChangeSettings(Vector3.zero, Vector3.one, Quaternion.Euler(0f, 0f, 180f), 0.4f);
		component3.CopySprite(this.combatPet.GetComponentInChildren<SpriteRenderer>());
		component3.FlyTo(gameObject3.transform.position, this.combatPet.transform, Vector3.zero);
		this.combatPetVisible = true;
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x0008B6F4 File Offset: 0x000898F4
	public static void RemoveAllCombatPets()
	{
		PetMaster[] array = Object.FindObjectsOfType<PetMaster>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].RemoveCombatPet();
		}
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x0008B720 File Offset: 0x00089920
	public void RemoveCombatPet()
	{
		if (!this.combatPetVisible || !this.combatPet)
		{
			return;
		}
		this.combatPetCom = null;
		GameObject gameObject = Object.Instantiate<GameObject>(this.petProxyPrefab, this.combatPet.transform.position, Quaternion.identity);
		ItemAnimationProxy component = gameObject.GetComponent<ItemAnimationProxy>();
		component.CopySprite(this.combatPet.GetComponentInChildren<SpriteRenderer>());
		component.ChangeSettings(Vector3.one, Vector3.zero, Quaternion.identity, 0.4f);
		component.FlyTo(gameObject.transform.position, this.player.transform.GetChild(0), new Vector3(-0.5f, 0.2f, 0f));
		Object.Destroy(this.combatPet);
		this.combatPet = null;
		this.combatPetVisible = false;
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x0008B7F0 File Offset: 0x000899F0
	public static void AddAllToPetInventorys()
	{
		Item2[] array = Object.FindObjectsOfType<Item2>();
		for (int i = 0; i < array.Length; i++)
		{
			PetMaster.AddToPetInventory(array[i]);
		}
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0008B81C File Offset: 0x00089A1C
	public static void HealAllPets()
	{
		foreach (PetMaster petMaster in PetMaster.petMasters)
		{
			petMaster.health = petMaster.maxHealth;
		}
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0008B874 File Offset: 0x00089A74
	public static void AddToPetInventory(Item2 item)
	{
		foreach (PetMaster petMaster in PetMaster.petMasters)
		{
			if (item.parentInventoryGrid && petMaster.petInventory == item.parentInventoryGrid.parent && !petMaster.itemsInside.Contains(item.gameObject))
			{
				petMaster.itemsInside.Add(item.gameObject);
			}
		}
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x0008B908 File Offset: 0x00089B08
	public static void RemoveFromPetInventory(Item2 item)
	{
		foreach (PetMaster petMaster in PetMaster.petMasters)
		{
			if (petMaster.itemsInside.Contains(item.gameObject))
			{
				petMaster.itemsInside.Remove(item.gameObject);
				break;
			}
		}
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x0008B97C File Offset: 0x00089B7C
	public static void CloseAllPetInventories(PetMaster exclude)
	{
		foreach (PetMaster petMaster in PetMaster.petMasters)
		{
			if (!(petMaster == exclude))
			{
				petMaster.CloseInventory();
			}
		}
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x0008B9D8 File Offset: 0x00089BD8
	private void CloseInventory()
	{
		if (this.movingInventory != null)
		{
			base.StopCoroutine(this.movingInventory);
		}
		this.showingInventory = false;
		this.movingInventory = base.StartCoroutine(this.HideInventory());
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x0008BA07 File Offset: 0x00089C07
	public void OpenInventory()
	{
		PetMaster.CloseAllPetInventories(this);
		if (this.movingInventory != null)
		{
			base.StopCoroutine(this.movingInventory);
		}
		this.movingInventory = base.StartCoroutine(this.ShowInventory());
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x0008BA35 File Offset: 0x00089C35
	private IEnumerator HideInventory()
	{
		if (!this.petInventory.gameObject.activeInHierarchy)
		{
			yield break;
		}
		foreach (GameObject gameObject in this.itemsInside)
		{
			gameObject.transform.SetParent(this.petInventory.transform, true);
		}
		Vector3 end = new Vector3(this.petInventory.transform.localPosition.x, 8f, this.petInventory.transform.localPosition.z);
		if (this.petInventory.transform.position.y < 7f)
		{
			while (Vector3.Distance(this.petInventory.transform.localPosition, end) > 0.1f)
			{
				this.petInventory.transform.localPosition = Vector3.MoveTowards(this.petInventory.transform.localPosition, end, 25f * Time.deltaTime);
				this.petInventory.transform.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one, (Vector3.Distance(this.petInventory.transform.localPosition, end) - 4f) / 4f);
				yield return null;
			}
		}
		this.petInventory.transform.localPosition = end + Vector3.up * 8f * (float)(base.transform.GetSiblingIndex() + 1);
		this.petInventory.transform.localScale = Vector3.one;
		yield break;
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x0008BA44 File Offset: 0x00089C44
	private IEnumerator ShowInventory()
	{
		foreach (GameObject gameObject in this.itemsInside)
		{
			gameObject.transform.SetParent(this.petInventory.transform, true);
		}
		if (this.petInventory.localPosition.y > 8f)
		{
			this.petInventory.localPosition = new Vector3(this.petInventory.localPosition.x, 8f, this.petInventory.localPosition.z);
		}
		Vector3 end = new Vector3(this.petInventory.transform.localPosition.x, 0f, this.petInventory.transform.localPosition.z);
		while (Vector3.Distance(this.petInventory.transform.localPosition, end) > 0.01f)
		{
			this.petInventory.transform.localPosition = Vector3.MoveTowards(this.petInventory.transform.localPosition, end, 25f * Time.deltaTime);
			this.petInventory.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, Vector3.Distance(this.petInventory.transform.localPosition, end) / 8f);
			yield return null;
		}
		this.petInventory.transform.localPosition = end;
		this.petInventory.transform.localScale = Vector3.one;
		foreach (GameObject gameObject2 in this.itemsInside)
		{
			gameObject2.transform.SetParent(GameObject.FindGameObjectWithTag("ItemParent").transform, true);
		}
		this.showingInventory = true;
		yield break;
	}

	// Token: 0x04000B51 RID: 2897
	public static List<PetMaster> petMasters = new List<PetMaster>();

	// Token: 0x04000B52 RID: 2898
	[SerializeField]
	private Material outlineMaterial;

	// Token: 0x04000B53 RID: 2899
	[SerializeField]
	private Material standardMaterial;

	// Token: 0x04000B54 RID: 2900
	[SerializeField]
	private Material wigglyMaterial;

	// Token: 0x04000B55 RID: 2901
	[SerializeField]
	private GameObject petProxyPrefab;

	// Token: 0x04000B56 RID: 2902
	[Header("Combat Pet")]
	[SerializeField]
	private GameObject statusPrefab;

	// Token: 0x04000B57 RID: 2903
	[SerializeField]
	private GameObject energyPrefab;

	// Token: 0x04000B58 RID: 2904
	[SerializeField]
	public GameObject combatPetPrefab;

	// Token: 0x04000B59 RID: 2905
	public GameObject combatPet;

	// Token: 0x04000B5A RID: 2906
	public CombatPet combatPetCom;

	// Token: 0x04000B5B RID: 2907
	[Header("Pet Item")]
	[SerializeField]
	public GameObject petItemPrefab;

	// Token: 0x04000B5C RID: 2908
	[SerializeField]
	public PetItem petItem;

	// Token: 0x04000B5D RID: 2909
	[Header("Pet Inventory")]
	[SerializeField]
	public GameObject petInventoryPrefab;

	// Token: 0x04000B5E RID: 2910
	[SerializeField]
	public Transform petInventory;

	// Token: 0x04000B5F RID: 2911
	[SerializeField]
	public List<GameObject> itemsInside = new List<GameObject>();

	// Token: 0x04000B60 RID: 2912
	private const float speed = 25f;

	// Token: 0x04000B61 RID: 2913
	private Coroutine movingInventory;

	// Token: 0x04000B62 RID: 2914
	private Player player;

	// Token: 0x04000B63 RID: 2915
	private GameManager gameManager;

	// Token: 0x04000B64 RID: 2916
	private GameFlowManager gameFlowManager;

	// Token: 0x04000B65 RID: 2917
	private bool combatPetVisible;

	// Token: 0x04000B66 RID: 2918
	public bool showingInventory;

	// Token: 0x04000B67 RID: 2919
	[Header("Pet Stats")]
	[SerializeField]
	public int health = 12;

	// Token: 0x04000B68 RID: 2920
	[SerializeField]
	public int maxHealth = 12;

	// Token: 0x04000B69 RID: 2921
	[SerializeField]
	public int currentAP;

	// Token: 0x04000B6A RID: 2922
	[SerializeField]
	public int APperTurn;

	// Token: 0x04000B6B RID: 2923
	[SerializeField]
	public int APonSummon;

	// Token: 0x04000B6C RID: 2924
	[SerializeField]
	public List<Item2.CombattEffect> petEffects;

	// Token: 0x04000B6D RID: 2925
	public bool hasBeenSummoned;
}
