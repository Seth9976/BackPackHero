using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

// Token: 0x02000159 RID: 345
public class Overworld_Structure : CustomInputHandler
{
	// Token: 0x06000D9F RID: 3487 RVA: 0x00088314 File Offset: 0x00086514
	public void SetMetaProgressMarkerBoolean(string x)
	{
		MetaProgressSaveManager.MetaProgressMarker metaProgressMarker = MetaProgressSaveManager.TranslateStringToMarker(x);
		MetaProgressSaveManager.main.AddMetaProgressMarker(metaProgressMarker);
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x00088333 File Offset: 0x00086533
	private void OnDestroy()
	{
		if (Overworld_CardManager.main)
		{
			Overworld_CardManager.main.RemoveCard();
		}
		Overworld_Structure.structures.Remove(this);
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x00088358 File Offset: 0x00086558
	public static bool ShareStructureTypes(List<Overworld_Structure.StructureType> structureTypes1, List<Overworld_Structure.StructureType> structureTypes2)
	{
		if (structureTypes1.Contains(Overworld_Structure.StructureType.Any) || structureTypes2.Contains(Overworld_Structure.StructureType.Any))
		{
			return true;
		}
		if (structureTypes1.Count == 0 || structureTypes2.Count == 0)
		{
			return true;
		}
		foreach (Overworld_Structure.StructureType structureType in structureTypes1)
		{
			if (structureTypes2.Contains(structureType))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x000883D8 File Offset: 0x000865D8
	public void GetGridObject()
	{
		if (this.gridObjects.Count != 0)
		{
			if (this.gridObjects.Where((GridObject x) => x == null).Count<GridObject>() <= 0)
			{
				goto IL_008D;
			}
		}
		this.gridObjects = base.GetComponentsInChildren<GridObject>().ToList<GridObject>();
		for (int i = 0; i < this.gridObjects.Count; i++)
		{
			if (this.gridObjects[i] == this.doorGridObject)
			{
				this.gridObjects.RemoveAt(i);
				break;
			}
		}
		IL_008D:
		this.doorGridObject;
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x00088480 File Offset: 0x00086680
	private bool ShareOverworldTypes(List<Overworld_Structure.StructureType> structures, List<Overworld_Structure.StructureType> otherStructures)
	{
		if (structures.Contains(Overworld_Structure.StructureType.Any) || otherStructures.Contains(Overworld_Structure.StructureType.Any))
		{
			return true;
		}
		foreach (Overworld_Structure.StructureType structureType in structures)
		{
			if (otherStructures.Contains(structureType))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x000884EC File Offset: 0x000866EC
	public static List<Overworld_Structure> GetStructuresInRadius(Vector2 position, float radius)
	{
		List<Overworld_Structure> list = new List<Overworld_Structure>();
		foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
		{
			if (Vector2.Distance(position, overworld_Structure.transform.position) <= radius)
			{
				list.Add(overworld_Structure);
			}
		}
		return list;
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x00088560 File Offset: 0x00086760
	public static void AllStructuresApplyAllModifiers()
	{
		foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
		{
			overworld_Structure.appliedModifiers = new List<Overworld_Structure.Modifier>();
		}
		foreach (Overworld_Structure overworld_Structure2 in Overworld_Structure.structures)
		{
			overworld_Structure2.connectedStructuresViaPath.Clear();
		}
		Overworld_Structure.structures.Sort((Overworld_Structure x, Overworld_Structure y) => x.searchingPriority.CompareTo(y.searchingPriority));
		foreach (Overworld_Structure overworld_Structure3 in Overworld_Structure.structures)
		{
			bool flag = false;
			foreach (Overworld_Structure overworld_Structure4 in Overworld_Structure.structures)
			{
				if (overworld_Structure4 != overworld_Structure3 && overworld_Structure4.connectedStructuresViaPath.Contains(overworld_Structure3))
				{
					overworld_Structure3.connectedStructuresViaPath = new HashSet<Overworld_Structure>(overworld_Structure4.connectedStructuresViaPath);
					overworld_Structure3.connectedStructuresViaPath.Remove(overworld_Structure3);
					overworld_Structure3.connectedStructuresViaPath.Add(overworld_Structure4);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				overworld_Structure3.FindAllStructuresConnectedViaPath();
			}
		}
		foreach (Overworld_Structure overworld_Structure5 in Overworld_Structure.structures)
		{
			overworld_Structure5.ApplyAllModifiers();
		}
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x00088730 File Offset: 0x00086930
	public static List<Overworld_Structure> StructuresOfType(Overworld_Structure structure)
	{
		return Overworld_Structure.StructuresOfType(structure.name);
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x00088740 File Offset: 0x00086940
	public static List<Overworld_Structure> StructuresOfType(string x)
	{
		List<Overworld_Structure> list = new List<Overworld_Structure>();
		foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
		{
			if (!(Item2.GetDisplayName(overworld_Structure.name) != Item2.GetDisplayName(x)) && overworld_Structure.removeCoroutine == null)
			{
				list.Add(overworld_Structure);
			}
		}
		return list;
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x000887BC File Offset: 0x000869BC
	public void ShowArea()
	{
		BoxCollider2D boxCollider2D = null;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.CompareTag("Ignorable"))
			{
				boxCollider2D = transform.GetComponent<BoxCollider2D>();
				break;
			}
		}
		boxCollider2D;
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x00088830 File Offset: 0x00086A30
	private List<GridObject> GetAdjacentGridObjects(GridObject.Type type, List<GridObject.Tag> tags)
	{
		return GridObject.FilterByType(GridObject.GetAdjacentItems(GridObject.GetGridPositionsFromMultiple(this.gridObjects), this.spacesForExpansion, tags, this.spacesNearby), type);
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x00088858 File Offset: 0x00086A58
	private List<Overworld_Structure> GetAdjacentStructures(int extendWithPath = 1)
	{
		this.GetGridObject();
		List<GridObject> adjacentItems = GridObject.GetAdjacentItems(GridObject.GetGridPositionsFromMultiple(this.gridObjects), extendWithPath, this.tagsForExpansion, this.spacesNearby);
		List<Overworld_Structure> list = new List<Overworld_Structure>();
		foreach (GridObject gridObject in adjacentItems)
		{
			Overworld_Structure component = gridObject.GetComponent<Overworld_Structure>();
			if (component)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x000888DC File Offset: 0x00086ADC
	private void ApplyAllModifiers()
	{
		foreach (Overworld_Structure.Modifier modifier in this.modifiers)
		{
			if (modifier.originName == null || modifier.originName == "")
			{
				modifier.originName = Item2.GetDisplayName(base.name);
			}
			if (modifier.connectionType == Overworld_Structure.Modifier.ConnectionType.Nearby || modifier.connectionType == Overworld_Structure.Modifier.ConnectionType.NearbyConnected)
			{
				if (modifier.structureTypes.Contains(Overworld_Structure.StructureType.BridgeAndWater))
				{
					int num = 1;
					for (int i = 0; i < num; i++)
					{
						this.appliedModifiers.Add(modifier);
					}
					continue;
				}
				if (modifier.tagTypes.Count > 0)
				{
					using (List<GridObject>.Enumerator enumerator2 = this.GetAdjacentGridObjects(GridObject.Type.grid, modifier.tagTypes).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							GridObject gridObject = enumerator2.Current;
							if (modifier.tagTypes.Contains(gridObject.tagType) && modifier.applyToSelf)
							{
								this.appliedModifiers.Add(modifier);
							}
						}
						continue;
					}
				}
				using (List<Overworld_Structure>.Enumerator enumerator3 = this.GetAdjacentStructures(this.spacesForExpansion).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Overworld_Structure overworld_Structure = enumerator3.Current;
						if (this.ShareOverworldTypes(overworld_Structure.structureTypes, modifier.structureTypes) && !(overworld_Structure == this))
						{
							if (modifier.applyToSelf)
							{
								this.appliedModifiers.Add(modifier);
							}
							else
							{
								overworld_Structure.appliedModifiers.Add(modifier);
							}
						}
					}
					continue;
				}
			}
			if (modifier.connectionType == Overworld_Structure.Modifier.ConnectionType.connectedViaPath)
			{
				foreach (Overworld_Structure overworld_Structure2 in this.connectedStructuresViaPath)
				{
					if (this.ShareOverworldTypes(overworld_Structure2.structureTypes, modifier.structureTypes))
					{
						if (modifier.applyToSelf)
						{
							this.appliedModifiers.Add(modifier);
						}
						else
						{
							overworld_Structure2.appliedModifiers.Add(modifier);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x00088B50 File Offset: 0x00086D50
	public void GetEffectTotals()
	{
		this.currentEfficiencyBonus = this.startingEfficiency;
		foreach (Overworld_Structure.Modifier modifier in this.appliedModifiers)
		{
			this.currentEfficiencyBonus += modifier.efficiencyBonus;
		}
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x00088BBC File Offset: 0x00086DBC
	public static void RemoveDraggingBuilding()
	{
		foreach (Overworld_Structure overworld_Structure in new List<Overworld_Structure>(Overworld_Structure.structures))
		{
			if (overworld_Structure.isDragging)
			{
				overworld_Structure.ConvertToBuildingButton(false);
				if (!overworld_Structure.isBuildingFromSolidClick)
				{
					overworld_Structure.Remove();
				}
				Object.Destroy(overworld_Structure.gameObject);
				break;
			}
		}
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x00088C38 File Offset: 0x00086E38
	public void IfFirstShowTut()
	{
		if (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.firstBuildingBuilt))
		{
			Object.FindObjectOfType<OverworldTutorials>().ShowTellZaar2Tutorial(true);
		}
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00088C54 File Offset: 0x00086E54
	public void FirstBuild(int additional = 0)
	{
		UnityEvent unityEvent = this.onCreateEvent;
		if (unityEvent != null)
		{
			unityEvent.Invoke();
		}
		MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.firstBuildingBuilt);
		this.canConvertBack = true;
		List<Overworld_ResourceManager.Resource> list = this.CalculateCosts(additional);
		Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, list, -1);
		if (!Overworld_Structure.structures.Contains(this))
		{
			Overworld_Structure.structures.Add(this);
		}
		if (this.populationAdd != 0)
		{
			MetaProgressSaveManager.main.ChangePopTicket(this.populationPrefab, this.populationAdd);
		}
		Overworld_NPC_Spawner componentInChildren = base.GetComponentInChildren<Overworld_NPC_Spawner>();
		if (componentInChildren)
		{
			componentInChildren.Delay();
		}
		foreach (Overworld_NPC overworld_NPC in Overworld_NPC.npcs)
		{
			overworld_NPC.FindHome();
		}
		this.startFromButton = false;
		this.firstBuildParticles = true;
		this.AddToGrid();
		this.CreatePlacementParticles();
		this.PlayPlacementEffects();
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x00088D4C File Offset: 0x00086F4C
	private void OnDrawGizmos()
	{
		foreach (Overworld_Structure.Modifier modifier in this.modifiers)
		{
			foreach (Overworld_Structure overworld_Structure in this.GetAdjacentStructures(this.spacesForExpansion))
			{
				if (!(overworld_Structure == this) && this.ShareOverworldTypes(modifier.structureTypes, overworld_Structure.structureTypes))
				{
					Gizmos.color = Color.green;
					Gizmos.DrawWireSphere(overworld_Structure.transform.position, 3f);
				}
			}
		}
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x00088E18 File Offset: 0x00087018
	private IEnumerator SetupParticleSystem()
	{
		yield return null;
		yield return null;
		if (this.firstBuildParticles)
		{
			yield break;
		}
		foreach (ParticleSystem particleSystem in base.GetComponentsInChildren<ParticleSystem>())
		{
			if (!(particleSystem.gameObject == base.gameObject))
			{
				Object.Destroy(particleSystem.gameObject);
			}
		}
		this.CreatePlacementParticles();
		yield break;
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x00088E28 File Offset: 0x00087028
	private void CreatePlacementParticles()
	{
		GameObject building = Overworld_BuildingManager.main.GetBuilding(base.name);
		if (!building)
		{
			return;
		}
		ParticleSystem componentInChildren = building.GetComponentInChildren<ParticleSystem>();
		if (!componentInChildren)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(componentInChildren.gameObject, base.transform);
		this.placementParticles = gameObject.GetComponent<ParticleSystem>();
		this.placementParticles.main.stopAction = ParticleSystemStopAction.Destroy;
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x00088E91 File Offset: 0x00087091
	private IEnumerator SetupAnimator()
	{
		Animator component = base.GetComponent<Animator>();
		if (component && component.runtimeAnimatorController)
		{
			yield break;
		}
		if (component)
		{
			Object.Destroy(component);
		}
		GameObject building = Overworld_BuildingManager.main.GetBuilding(base.name);
		if (!building)
		{
			yield break;
		}
		Animator animator2 = building.GetComponentInChildren<Animator>();
		if (!animator2)
		{
			yield break;
		}
		yield return null;
		Animator animator3 = base.gameObject.AddComponent<Animator>();
		animator3.applyRootMotion = animator2.applyRootMotion;
		animator3.runtimeAnimatorController = animator2.runtimeAnimatorController;
		yield break;
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x00088EA0 File Offset: 0x000870A0
	private IEnumerator SetupExclamation()
	{
		Overworld_BuildingInterfaceLauncher component = base.GetComponent<Overworld_BuildingInterfaceLauncher>();
		if (!component)
		{
			yield break;
		}
		GameObject exclamation = component.exclamation;
		if (exclamation)
		{
			Object.Destroy(exclamation);
		}
		yield return null;
		this.SetupExclamationPoint();
		yield break;
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x00088EB0 File Offset: 0x000870B0
	public static void SetupAllExclamationPoints()
	{
		foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
		{
			overworld_Structure.SetupExclamationPoint();
		}
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00088F00 File Offset: 0x00087100
	private void SetupExclamationPoint()
	{
		Overworld_BuildingInterfaceLauncher component = base.GetComponent<Overworld_BuildingInterfaceLauncher>();
		if (!component)
		{
			return;
		}
		GameObject exclamation = component.exclamation;
		if (exclamation)
		{
			Object.Destroy(exclamation);
		}
		GameObject building = Overworld_BuildingManager.main.GetBuilding(base.name);
		if (!building)
		{
			return;
		}
		Overworld_BuildingInterfaceLauncher componentInChildren = building.GetComponentInChildren<Overworld_BuildingInterfaceLauncher>();
		if (!componentInChildren)
		{
			return;
		}
		if (!componentInChildren.exclamation)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(componentInChildren.exclamation, component.transform);
		gameObject.transform.localPosition = componentInChildren.exclamation.transform.localPosition;
		component.exclamation = gameObject;
		component.DetermineIfResearchIsAvailable();
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00088FA7 File Offset: 0x000871A7
	private void CopyStructure()
	{
		if (!Overworld_BuildingManager.main)
		{
			return;
		}
		Overworld_BuildingManager.main.GetBuilding(base.name);
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00088FCC File Offset: 0x000871CC
	private void Start()
	{
		this.renderers = new List<SpriteRenderer>(base.GetComponentsInChildren<SpriteRenderer>());
		if (this.structureLayer == Overworld_Structure.StructureLayer.Bridge && !base.GetComponent<Rigidbody2D>())
		{
			base.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		}
		this.GetGridObject();
		this.collider2Ds = new List<Collider2D>(base.GetComponentsInChildren<Collider2D>());
		Transform transform = GameObject.FindGameObjectWithTag("Buildings").transform;
		base.transform.SetParent(transform);
		base.StartCoroutine(this.SetupParticleSystem());
		base.StartCoroutine(this.SetupAnimator());
		base.StartCoroutine(this.SetupExclamation());
		this.zPositioner = base.GetComponent<Overworld_Z_Positioner>();
		if (!Overworld_Structure.structures.Contains(this))
		{
			Overworld_Structure.structures.Add(this);
		}
		this.interactable = base.GetComponent<Overworld_Interactable>();
		SpriteRenderer componentInChildren = base.GetComponentInChildren<SpriteRenderer>();
		if (componentInChildren && this.shadowSprite)
		{
			this.shadowSprite.sprite = componentInChildren.sprite;
		}
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x000890C8 File Offset: 0x000872C8
	public void AddParticles(Vector3 position, SpriteRenderer spriteRenderer, GameObject particles = null)
	{
		if (!spriteRenderer || !spriteRenderer.sprite || !spriteRenderer.sprite.texture)
		{
			return;
		}
		if (!particles)
		{
			particles = this.itemDestroyParticlePrefab;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(particles, position, spriteRenderer.transform.rotation, base.transform.parent);
		ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
		ParticleSystem.MainModule main = component.main;
		gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder = Mathf.Max(spriteRenderer.sortingOrder, 2);
		ParticleSystem.ShapeModule shape = component.shape;
		shape.sprite = spriteRenderer.sprite;
		shape.texture = spriteRenderer.sprite.texture;
		main.startColor = spriteRenderer.color;
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x00089188 File Offset: 0x00087388
	public bool IsTileSpace(PathFinding.Location loc, bool ending)
	{
		if (ending)
		{
			return true;
		}
		List<GridObject> itemsAtPosition = GridObject.GetItemsAtPosition(loc.position);
		bool flag = false;
		foreach (GridObject gridObject in itemsAtPosition)
		{
			if (gridObject.type == GridObject.Type.item && gridObject.tagType == GridObject.Tag.bridge)
			{
				flag = true;
			}
			if (gridObject.type == GridObject.Type.item && gridObject.tagType == GridObject.Tag.blocksPath)
			{
				return false;
			}
			if (gridObject.type == GridObject.Type.grid && gridObject.tagType != GridObject.Tag.water)
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x00089220 File Offset: 0x00087420
	private void OnDrawGizmosSelected()
	{
		if (this.storedLocationsTried == null)
		{
			return;
		}
		foreach (Vector2 vector in this.storedLocationsTried)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(vector + Vector2.one * 0.5f, 0.25f);
		}
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x000892A4 File Offset: 0x000874A4
	private void FindAllStructuresConnectedViaPath()
	{
		if (!this.isConnectable)
		{
			return;
		}
		this.connectedStructuresViaPath.Clear();
		List<Vector2Int> list = new List<Vector2Int>(GridObject.GetGridPositionsFromMultiple(this.gridObjects));
		if (this.doorGridObject)
		{
			list.AddRange(this.doorGridObject.gridPositions);
		}
		PathFinding.FindConnectedStructures(list, new Func<PathFinding.Location, bool, bool>(this.IsTileSpace), out this.connectedStructuresViaPath, out this.storedLocationsTried);
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x00089314 File Offset: 0x00087514
	private bool PathIsAcceptable(Path p, Overworld_Structure target)
	{
		return !p.error && p.CompleteState == PathCompleteState.Complete && Vector2.Distance(p.vectorPath[0], base.transform.position) <= this.pathRadius && Vector2.Distance(p.vectorPath[p.vectorPath.Count - 1], target.transform.position) <= target.pathRadius;
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x000893A3 File Offset: 0x000875A3
	public void OnPathComplete(Path p)
	{
		this.path = p;
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x000893AC File Offset: 0x000875AC
	private void AddAnimator()
	{
		if (base.GetComponent<Animator>())
		{
			return;
		}
		base.gameObject.AddComponent<Animator>().runtimeAnimatorController = Overworld_Manager.main.animatorController;
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x000893D8 File Offset: 0x000875D8
	public static void RemoveAllAnimators()
	{
		Overworld_Structure[] array = Object.FindObjectsOfType<Overworld_Structure>();
		for (int i = 0; i < array.Length; i++)
		{
			Animator component = array[i].GetComponent<Animator>();
			if (component)
			{
				Object.Destroy(component);
			}
		}
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x00089410 File Offset: 0x00087610
	public void Remove()
	{
		if (this.removeCoroutine != null)
		{
			return;
		}
		Overworld_BuildingInterfaceLauncher component = base.GetComponent<Overworld_BuildingInterfaceLauncher>();
		if (component && component.exclamation)
		{
			Object.Destroy(component.exclamation);
		}
		foreach (GridObject gridObject in this.gridObjects)
		{
			gridObject.ClearGridPositions();
		}
		if (this.doorGridObject)
		{
			this.doorGridObject.ClearGridPositions();
		}
		Overworld_BuildingManager.main.RefundCosts(base.gameObject);
		if (this.populationAdd != 0)
		{
			Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, Overworld_ResourceManager.Resource.Type.Population, -this.populationAdd);
			MetaProgressSaveManager.main.ChangePopTicket(this.populationPrefab, -this.populationAdd);
		}
		this.onDestroyEvent.Invoke();
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x000894FC File Offset: 0x000876FC
	public void NaturalDelete()
	{
		this.Remove();
		this.removeCoroutine = base.StartCoroutine(this.RemoveCoroutine());
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x00089518 File Offset: 0x00087718
	public bool IsOutOfBOunds()
	{
		return base.transform.position.x < -26f || (double)base.transform.position.x > 39.5 || base.transform.position.y < -27f || base.transform.position.y > 26f;
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x00089589 File Offset: 0x00087789
	private IEnumerator RemoveCoroutine()
	{
		Collider2D[] componentsInChildren = base.GetComponentsInChildren<Collider2D>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		DisableWaterTile component = base.GetComponent<DisableWaterTile>();
		if (component)
		{
			component.ReEnableTile();
		}
		this.SaveItems();
		SpriteRenderer spriteRenderer = null;
		if (base.transform.childCount > 0 && base.transform.GetChild(0).childCount > 0)
		{
			spriteRenderer = base.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
		}
		if (!spriteRenderer)
		{
			spriteRenderer = base.GetComponentInChildren<SpriteRenderer>();
		}
		if (spriteRenderer)
		{
			this.AddParticles(spriteRenderer.transform.position, spriteRenderer, null);
		}
		SoundManager.main.PlaySFX("destroy" + Random.Range(1, 3).ToString());
		yield return null;
		yield return new WaitForFixedUpdate();
		Overworld_Structure.structures.Remove(this);
		Overworld_Manager.main.UpdateMap();
		this.AddAnimator();
		Animator component2 = base.GetComponent<Animator>();
		component2.enabled = true;
		component2.SetTrigger("remove");
		Overworld_BuildingManager.main.ResetBuildingButtons();
		yield return new WaitForSeconds(0.5f);
		yield return new WaitForSeconds(0.5f);
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00089598 File Offset: 0x00087798
	public void Interact()
	{
		if (!Overworld_Manager.IsFreeToMove())
		{
			return;
		}
		if (this.isShowingCard)
		{
			return;
		}
		this.ShowCard();
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x000895B4 File Offset: 0x000877B4
	public override void OnCursorHold()
	{
		if (!Overworld_Manager.IsFreeToMove())
		{
			return;
		}
		if (this.isShowingCard)
		{
			return;
		}
		if (DigitalCursor.main.OverUI())
		{
			return;
		}
		this.timeToDisplayCard += Time.deltaTime;
		if (this.timeToDisplayCard > 0.25f && !this.isShowingCard)
		{
			this.ShowCard();
		}
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0008960C File Offset: 0x0008780C
	private void ShowAsColor(Color color)
	{
		for (int i = 0; i < this.renderers.Count; i++)
		{
			if (!this.renderers[i])
			{
				this.renderers.RemoveAt(i);
				i--;
			}
		}
		if (this.renderers.Count == 0)
		{
			this.renderers = new List<SpriteRenderer>(base.GetComponentsInChildren<SpriteRenderer>());
		}
		foreach (SpriteRenderer spriteRenderer in this.renderers)
		{
			if (!spriteRenderer.gameObject.CompareTag("Ignorable"))
			{
				spriteRenderer.color = color;
			}
		}
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x000896C8 File Offset: 0x000878C8
	private static void ShowAllAsColor(Color color)
	{
		foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
		{
			overworld_Structure.ShowAsColor(color);
		}
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x00089718 File Offset: 0x00087918
	public void ShowCard()
	{
		if (this.isDragging)
		{
			return;
		}
		if (Overworld_Manager.main.IsState(Overworld_Manager.State.DESTROYING) || Overworld_Manager.main.IsState(Overworld_Manager.State.BUILDING))
		{
			return;
		}
		this.isShowingCard = true;
		Overworld_CardManager.main.DisplayCard(this, null);
		if (!Overworld_Manager.main.IsState(Overworld_Manager.State.NewBuildMode))
		{
			return;
		}
		foreach (Overworld_Structure.Modifier modifier in this.modifiers)
		{
			this.ShowGrids();
			foreach (Overworld_Structure overworld_Structure in this.GetAdjacentStructures(this.spacesForExpansion))
			{
				if (!(overworld_Structure == this) && this.ShareOverworldTypes(modifier.structureTypes, overworld_Structure.structureTypes))
				{
					overworld_Structure.ShowAsColor(Color.green);
				}
			}
		}
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x0008981C File Offset: 0x00087A1C
	private void SetupBlock(Vector2 position)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.overworldBlankSquarePrefab, position, Quaternion.identity);
		gameObject.transform.SetParent(base.transform);
		gameObject.transform.localScale = new Vector3(0.99f, 0.99f, 1f);
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x00089870 File Offset: 0x00087A70
	private void ShowGrids()
	{
		List<Vector2Int> list = GridObject.AdjacentVecs(GridObject.GetGridPositionsFromMultiple(this.gridObjects), this.spacesForExpansion, this.tagsForExpansion, this.spacesNearby);
		list.AddRange(GridObject.GetGridPositionsFromMultiple(this.gridObjects));
		foreach (Vector2Int vector2Int in list)
		{
			this.SetupBlock(GridObject.CellToWorld(vector2Int));
		}
		Tiler[] componentsInChildren = base.GetComponentsInChildren<Tiler>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetTile();
		}
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x00089914 File Offset: 0x00087B14
	private void HideGrids()
	{
		Tiler[] componentsInChildren = base.GetComponentsInChildren<Tiler>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.Destroy(componentsInChildren[i].gameObject);
		}
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x00089944 File Offset: 0x00087B44
	private void HideCard()
	{
		this.timeToDisplayCard = 0f;
		if (!this.isShowingCard)
		{
			return;
		}
		this.HideGrids();
		Overworld_Structure.ShowAllAsColor(Color.white);
		Overworld_CardManager.main.RemoveCard();
		this.isShowingCard = false;
		CircleRenderer componentInChildren = base.GetComponentInChildren<CircleRenderer>();
		if (componentInChildren)
		{
			componentInChildren.HideCircle();
		}
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x0008999B File Offset: 0x00087B9B
	public override void OnCursorEnd()
	{
		this.HideCard();
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x000899A3 File Offset: 0x00087BA3
	public override void OnPressHold(string keyName)
	{
		if (keyName == "Interact")
		{
			this.Interact();
		}
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x000899B8 File Offset: 0x00087BB8
	public void StartDragFromButton()
	{
		Animator component = base.GetComponent<Animator>();
		if (component)
		{
			component.enabled = true;
			component.Play("structureSpawnIn", 0, 0.2f);
		}
		this.collider2Ds = new List<Collider2D>(base.GetComponentsInChildren<Collider2D>());
		this.StartDrag(true);
		this.startFromButton = true;
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00089A0C File Offset: 0x00087C0C
	public void StartDrag(bool ignoreMode = false)
	{
		if (Overworld_Structure.draggingStructure)
		{
			return;
		}
		if (!Overworld_Manager.main.IsState(Overworld_Manager.State.NewBuildMode) && !ignoreMode)
		{
			return;
		}
		if (this.isDragging)
		{
			return;
		}
		if (!this.isDraggable)
		{
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm81"));
			return;
		}
		this.HideCard();
		DisableWaterTile component = base.GetComponent<DisableWaterTile>();
		if (component)
		{
			component.ReEnableTile();
		}
		foreach (Collider2D collider2D in this.collider2Ds)
		{
			collider2D.enabled = false;
		}
		SpriteRenderer[] componentsInChildren = base.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].sortingOrder += 3;
		}
		this.ShowAsColor(Color.gray);
		this.startPosition = base.transform.position;
		this.offset = DigitalCursor.main.transform.position - this.startPosition;
		if (this.offset.magnitude > 4f)
		{
			this.offset = this.offset.normalized * 4f;
		}
		SoundManager.main.PlaySFX("pickup");
		this.isDragging = true;
		Overworld_Structure.draggingStructure = this;
		Overworld_Manager.main.UpdateText();
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x00089B80 File Offset: 0x00087D80
	private void RemoveFromGrid()
	{
		foreach (GridObject gridObject in this.gridObjects)
		{
			gridObject.ClearGridPositions();
		}
		if (this.doorGridObject)
		{
			this.doorGridObject.ClearGridPositions();
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00089BE8 File Offset: 0x00087DE8
	private void AddToGrid()
	{
		foreach (GridObject gridObject in this.gridObjects)
		{
			gridObject.SnapToGrid();
		}
		if (this.doorGridObject)
		{
			this.doorGridObject.SnapToGrid();
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x00089C50 File Offset: 0x00087E50
	private void EndDrag()
	{
		if (!this.isDragging)
		{
			return;
		}
		List<Overworld_ResourceManager.Resource> list = this.CalculateCosts(0);
		if (this.startFromButton && !Overworld_ResourceManager.main.HasEnoughResources(list, -1))
		{
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
			if (!this.isBuildingFromSolidClick)
			{
				this.ConvertToBuildingButton(false);
				Overworld_Structure.draggingStructure = null;
				this.isDragging = false;
				Overworld_Manager.main.UpdateText();
				return;
			}
			if (this.isBuildingFromSolidClick)
			{
				return;
			}
		}
		Transform closestBridgeSpot = Overworld_Manager.main.GetClosestBridgeSpot(base.transform.position);
		if (!this.IsValidPlacement(true, closestBridgeSpot))
		{
			if (this.startFromButton && !this.isBuildingFromSolidClick)
			{
				this.ConvertToBuildingButton(false);
				Overworld_Structure.draggingStructure = null;
				this.isDragging = false;
				Overworld_Manager.main.UpdateText();
				return;
			}
			if (this.isBuildingFromSolidClick)
			{
				return;
			}
			if (!this.startFromButton)
			{
				base.transform.position = this.startPosition;
			}
		}
		SpriteRenderer[] componentsInChildren = base.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].sortingOrder -= 3;
		}
		foreach (Collider2D collider2D in this.collider2Ds)
		{
			if (!(collider2D as CircleCollider2D))
			{
				collider2D.enabled = true;
			}
		}
		this.isDragging = false;
		Overworld_Structure.draggingStructure = null;
		Overworld_Manager.main.UpdateText();
		this.AddToGrid();
		this.ShowAsColor(Color.white);
		if (this.isBuildingFromSolidClick)
		{
			this.isBuildingFromSolidClick = false;
			if (this.oneOfAKind)
			{
				Overworld_Manager.main.ReturnToBuildMode();
			}
			else
			{
				this.MakeDuplicate();
			}
		}
		DisableWaterTile component = base.GetComponent<DisableWaterTile>();
		if (component)
		{
			component.DisableTile();
		}
		this.CreatePlacementParticles();
		this.PlayPlacementEffects();
		if (this.startFromButton)
		{
			this.FirstBuild(0);
		}
		Overworld_Manager.main.UpdateMap();
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x00089E54 File Offset: 0x00088054
	public bool IsValidPlacement(bool showMessage, Transform bridgeSpot = null)
	{
		if (this.structureLayer == Overworld_Structure.StructureLayer.Bridge && !bridgeSpot)
		{
			if (showMessage)
			{
				PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm83"));
				SoundManager.main.PlaySFX("negative");
			}
			return false;
		}
		return this.OpenSpace(showMessage);
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x00089EAC File Offset: 0x000880AC
	private void PlayPlacementEffects()
	{
		SoundManager.main.PlaySFX(this.sfxNameOnPlacement);
		Animator component = base.GetComponent<Animator>();
		if (component)
		{
			component.Play("structureSpawnIn", 0, 0.5f);
		}
		if (this.placementParticles)
		{
			this.placementParticles.Clear();
			this.placementParticles.Play();
		}
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x00089F0C File Offset: 0x0008810C
	private bool OpenSpace(bool showMessage)
	{
		if (this.IsOutOfBOunds())
		{
			if (showMessage)
			{
				PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36e"));
				SoundManager.main.PlaySFX("negative");
			}
			return false;
		}
		List<GridObject> itemsAtPosition = GridObject.GetItemsAtPosition(GridObject.GetGridPositionsFromMultiple(this.gridObjects));
		if (this.gridObjects.Count == 0)
		{
			return true;
		}
		foreach (GridObject gridObject in itemsAtPosition)
		{
			if (gridObject && !this.gridObjects.Contains(gridObject) && !(gridObject == this.doorGridObject) && !gridObject.transform.IsChildOf(base.transform))
			{
				if (gridObject.tagType == GridObject.Tag.Boundry)
				{
					if (showMessage)
					{
						PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36e"));
						SoundManager.main.PlaySFX("negative");
					}
					return false;
				}
				if (gridObject.type == GridObject.Type.item && gridObject.gameObject.CompareTag("BuildingEntrance"))
				{
					if (showMessage)
					{
						PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36c"));
						SoundManager.main.PlaySFX("negative");
					}
					return false;
				}
				if (gridObject.type == GridObject.Type.item)
				{
					if (showMessage)
					{
						PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36a"));
						SoundManager.main.PlaySFX("negative");
					}
					return false;
				}
				if (gridObject.type == GridObject.Type.grid && gridObject.tagType == GridObject.Tag.water && this.structureLayer != Overworld_Structure.StructureLayer.Bridge)
				{
					if (showMessage)
					{
						PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36b"));
						SoundManager.main.PlaySFX("negative");
					}
					return false;
				}
			}
		}
		if (this.doorGridObject)
		{
			foreach (GridObject gridObject2 in GridObject.GetItemsAtPosition(this.doorGridObject.gridPositions))
			{
				if (gridObject2 && !this.gridObjects.Contains(gridObject2) && !(gridObject2 == this.doorGridObject) && !gridObject2.transform.IsChildOf(base.transform))
				{
					if (gridObject2.type == GridObject.Type.item)
					{
						if (showMessage)
						{
							PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36c"));
							SoundManager.main.PlaySFX("negative");
						}
						return false;
					}
					if (gridObject2.type == GridObject.Type.grid && gridObject2.tagType == GridObject.Tag.water && this.structureLayer != Overworld_Structure.StructureLayer.Bridge)
					{
						if (showMessage)
						{
							PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36c"));
							SoundManager.main.PlaySFX("negative");
						}
						return false;
					}
				}
			}
		}
		if (this.placementConditions.Contains(Overworld_Structure.PlacementCondition.MustBeNearEdge))
		{
			bool flag = false;
			foreach (RaycastHit2D raycastHit2D in Physics2D.CircleCastAll(base.transform.position, 2.5f, Vector2.zero, 0f))
			{
				if (raycastHit2D.collider.CompareTag("Boundry") && raycastHit2D.collider.gameObject.name != "Path Boundry")
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (showMessage)
				{
					PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36d"));
					SoundManager.main.PlaySFX("negative");
				}
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0008A308 File Offset: 0x00088508
	private void OnMouseDown()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.ClickedOn();
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0008A31D File Offset: 0x0008851D
	public override void OnPressStart(string x, bool xx)
	{
		if (x == "confirm")
		{
			this.ClickedOn();
		}
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0008A332 File Offset: 0x00088532
	private void ClickedOn()
	{
		if (DigitalCursor.main.OverUI())
		{
			return;
		}
		if (!Overworld_Manager.main.IsState(Overworld_Manager.State.NewBuildMode))
		{
			return;
		}
		this.StartDrag(false);
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0008A358 File Offset: 0x00088558
	public List<Overworld_ResourceManager.Resource> CalculateCosts(int additional = 0)
	{
		int num = ((Overworld_Structure.StructuresOfType(this).Count >= 1) ? (Overworld_Structure.StructuresOfType(this).Count + additional) : 1);
		if (base.name.ToLower().Contains("rubble") || !this.increasesCost)
		{
			num = 1;
		}
		return Overworld_ResourceManager.main.MultiplyResourceCosts(this.costs, num);
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x0008A3B8 File Offset: 0x000885B8
	private void SaveItems()
	{
		Overworld_Chest component = base.GetComponent<Overworld_Chest>();
		if (component)
		{
			component.AddAllItemsToInventory();
		}
		Overworld_BuildingInterfaceLauncher component2 = base.GetComponent<Overworld_BuildingInterfaceLauncher>();
		if (component2)
		{
			component2.AddAllItemsToInventory();
		}
		Overworld_Podium component3 = base.GetComponent<Overworld_Podium>();
		if (component3)
		{
			component3.Remove();
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x0008A404 File Offset: 0x00088604
	public void ForceRemoval()
	{
		if (!this.startFromButton)
		{
			this.Remove();
		}
		Debug.Log("Force removal");
		this.HideCard();
		this.isDragging = false;
		this.isBuildingFromSolidClick = false;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x0008A440 File Offset: 0x00088640
	private GameObject ConvertToBuildingButton(bool startDrag)
	{
		if (this.removeCoroutine != null)
		{
			return null;
		}
		this.SaveItems();
		GameObject building = Overworld_BuildingManager.main.GetBuilding(Item2.GetDisplayName(base.name));
		if (building)
		{
			Overworld_BuildingManager.main.RemoveBuildingButtonIfItExists(building);
			GameObject gameObject = Overworld_BuildingManager.main.CreateNewBuildingButton(building);
			Overworld_Button component = gameObject.GetComponent<Overworld_Button>();
			if (startDrag && (this.isDragging || this.isBuildingFromSolidClick))
			{
				component.StartDrag();
				component.canSwitchMode = false;
			}
			component.isBuildingFromSolidClick = this.isBuildingFromSolidClick;
			if (!this.startFromButton)
			{
				this.Remove();
			}
			Overworld_BuildingManager.main.EnableBuildingBox();
			Object.Destroy(base.gameObject);
			MetaProgressSaveManager.main.CalculatePassiveGenerationRate();
			return gameObject;
		}
		return null;
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0008A4F4 File Offset: 0x000886F4
	private void MakeDuplicate()
	{
		if (this.oneOfAKind)
		{
			return;
		}
		GameObject building = Overworld_BuildingManager.main.GetBuilding(Item2.GetDisplayName(base.name));
		if (building)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(building, base.transform.position, base.transform.rotation, base.transform.parent);
			gameObject.transform.localScale = base.transform.localScale;
			Overworld_Structure component = gameObject.GetComponent<Overworld_Structure>();
			component.StartDragFromButton();
			component.offset = Vector2.zero;
			component.isBuildingFromSolidClick = true;
		}
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x0008A584 File Offset: 0x00088784
	private void Update()
	{
		if ((double)this.existedFor < 0.25)
		{
			this.existedFor += Time.deltaTime;
		}
		else
		{
			this.canConvertBack = true;
		}
		if (this.isDragging)
		{
			if (!Overworld_Manager.main.IsBuildingMode() || SingleUI.IsViewingPopUp())
			{
				this.ForceRemoval();
				return;
			}
			if (!this.isBuildingFromSolidClick && this.existedFor >= 0.25f && this.canConvertBack && (!Overworld_Manager.main.IsState(Overworld_Manager.State.NewBuildMode) || (!Input.GetMouseButton(0) && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor) || DigitalCursor.main.GetInputDown("cancel") || (!DigitalCursor.main.GetInputHold("confirm") && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)))
			{
				this.EndDrag();
				return;
			}
			if (this.isBuildingFromSolidClick && this.existedFor >= 0.25f && (DigitalCursor.main.GetInputDown("confirm") || DigitalCursor.main.GetInputDown("interact") || (Input.GetMouseButtonDown(0) && this.canConvertBack)))
			{
				this.EndDrag();
				return;
			}
			if (this.isBuildingFromSolidClick && (DigitalCursor.main.GetInputDown("cancel") || Input.GetMouseButtonDown(1)))
			{
				this.ConvertToBuildingButton(false);
				this.isDragging = false;
				Overworld_Structure.draggingStructure = null;
				Overworld_Manager.main.UpdateText();
			}
			if (Overworld_Manager.main.OverBuildModePanel(DigitalCursor.main.transform.position) && this.canConvertBack)
			{
				this.ConvertToBuildingButton(true);
				return;
			}
			if (this.canBeRotated && (Input.GetKeyDown("r") || DigitalCursor.main.GetInputDown("rotateLeft") || DigitalCursor.main.GetInputDown("rotateRight")))
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x * -1f, base.transform.localScale.y, base.transform.localScale.z);
			}
			Vector2 vector = DigitalCursor.main.transform.position - this.offset;
			if (this.zPositioner)
			{
				base.transform.position = this.zPositioner.GetZPosition(vector);
			}
			else
			{
				base.transform.position = new Vector3(vector.x, vector.y, vector.y);
			}
			if (this.zPositioner)
			{
				base.transform.position = this.zPositioner.GetZPosition(vector);
			}
			else
			{
				base.transform.position = new Vector3(vector.x, vector.y, vector.y);
			}
			Vector3 position = base.transform.position;
			Transform transform = null;
			if (this.structureLayer == Overworld_Structure.StructureLayer.Bridge)
			{
				foreach (GridObject gridObject in this.gridObjects)
				{
					gridObject.SnapToGrid();
				}
				transform = Overworld_Manager.main.GetClosestBridgeSpot(base.transform.position);
				if (transform)
				{
					base.transform.position = transform.position;
				}
			}
			else
			{
				foreach (GridObject gridObject2 in this.gridObjects)
				{
					gridObject2.SnapToGrid();
				}
				if (this.doorGridObject)
				{
					this.doorGridObject.SnapToGrid();
				}
			}
			if (!Singleton.Instance.snapToGrid)
			{
				base.transform.position = position;
			}
			if (!this.IsValidPlacement(false, transform))
			{
				this.ShowAsColor(Color.red);
				return;
			}
			this.ShowAsColor(Color.gray);
		}
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x0008A96C File Offset: 0x00088B6C
	public static void EndCursorBuilding()
	{
		foreach (Overworld_Structure overworld_Structure in new List<Overworld_Structure>(Overworld_Structure.structures))
		{
			if (overworld_Structure && overworld_Structure.isDragging)
			{
				overworld_Structure.EndBuildingFromSolidClick();
			}
		}
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x0008A9D4 File Offset: 0x00088BD4
	private void EndBuildingFromSolidClick()
	{
		this.ConvertToBuildingButton(false);
		this.isDragging = false;
		Overworld_Structure.draggingStructure = null;
		Overworld_Manager.main.UpdateText();
	}

	// Token: 0x04000B0F RID: 2831
	public int searchingPriority = -1;

	// Token: 0x04000B10 RID: 2832
	public List<Overworld_Structure.PlacementCondition> placementConditions = new List<Overworld_Structure.PlacementCondition>();

	// Token: 0x04000B11 RID: 2833
	private List<SpriteRenderer> renderers = new List<SpriteRenderer>();

	// Token: 0x04000B12 RID: 2834
	public bool isConnectable = true;

	// Token: 0x04000B13 RID: 2835
	public bool increasesCost = true;

	// Token: 0x04000B14 RID: 2836
	public static Overworld_Structure draggingStructure = null;

	// Token: 0x04000B15 RID: 2837
	public int spacesForExpansion;

	// Token: 0x04000B16 RID: 2838
	public int spacesNearby;

	// Token: 0x04000B17 RID: 2839
	public List<GridObject.Tag> tagsForExpansion;

	// Token: 0x04000B18 RID: 2840
	[SerializeField]
	public TileBase expansionTile;

	// Token: 0x04000B19 RID: 2841
	public BoxCollider2D gridCollider;

	// Token: 0x04000B1A RID: 2842
	public BoxCollider2D entranceCollider;

	// Token: 0x04000B1B RID: 2843
	[SerializeField]
	private List<GridObject> gridObjects;

	// Token: 0x04000B1C RID: 2844
	[SerializeField]
	private GridObject doorGridObject;

	// Token: 0x04000B1D RID: 2845
	[SerializeField]
	private GameObject overworldBlankSquarePrefab;

	// Token: 0x04000B1E RID: 2846
	[SerializeField]
	public int necessaryPopulationToDestroy;

	// Token: 0x04000B1F RID: 2847
	[SerializeField]
	private bool canBeRotated;

	// Token: 0x04000B20 RID: 2848
	[SerializeField]
	private string sfxNameOnPlacement;

	// Token: 0x04000B21 RID: 2849
	[SerializeField]
	private ParticleSystem placementParticles;

	// Token: 0x04000B22 RID: 2850
	public static List<Overworld_Structure> structures = new List<Overworld_Structure>();

	// Token: 0x04000B23 RID: 2851
	[SerializeField]
	private UnityEvent onCreateEvent = new UnityEvent();

	// Token: 0x04000B24 RID: 2852
	[SerializeField]
	private UnityEvent onDestroyEvent = new UnityEvent();

	// Token: 0x04000B25 RID: 2853
	public bool isDragging;

	// Token: 0x04000B26 RID: 2854
	private Vector3 startPosition;

	// Token: 0x04000B27 RID: 2855
	private bool startFromButton;

	// Token: 0x04000B28 RID: 2856
	private Vector2 offset;

	// Token: 0x04000B29 RID: 2857
	[SerializeField]
	public List<Overworld_ResourceManager.Resource> resourcesToAddEachRun = new List<Overworld_ResourceManager.Resource>();

	// Token: 0x04000B2A RID: 2858
	[SerializeField]
	private SpriteRenderer shadowSprite;

	// Token: 0x04000B2B RID: 2859
	[SerializeField]
	private Overworld_Z_Positioner zPositioner;

	// Token: 0x04000B2C RID: 2860
	[SerializeField]
	private bool isDraggable;

	// Token: 0x04000B2D RID: 2861
	[SerializeField]
	private List<Collider2D> collider2Ds;

	// Token: 0x04000B2E RID: 2862
	public float startingEfficiency = 100f;

	// Token: 0x04000B2F RID: 2863
	public float currentEfficiencyBonus = 1f;

	// Token: 0x04000B30 RID: 2864
	public bool oneOfAKind;

	// Token: 0x04000B31 RID: 2865
	public List<Overworld_ResourceManager.Resource> costs = new List<Overworld_ResourceManager.Resource>();

	// Token: 0x04000B32 RID: 2866
	public Overworld_BuildingManager.BuildingCategory category;

	// Token: 0x04000B33 RID: 2867
	private Coroutine findConnectedStructuresCoroutine;

	// Token: 0x04000B34 RID: 2868
	[Header("---------------------------Universal Stats---------------------------")]
	[SerializeField]
	public List<Overworld_Structure.StructureType> structureTypes;

	// Token: 0x04000B35 RID: 2869
	[Header("---------------------------Properties---------------------------")]
	[SerializeField]
	public List<Overworld_Structure.Modifier> modifiers = new List<Overworld_Structure.Modifier>();

	// Token: 0x04000B36 RID: 2870
	[Header("---------------------------------------Structure Details---------------------------------------")]
	public List<Overworld_Structure.Modifier> appliedModifiers = new List<Overworld_Structure.Modifier>();

	// Token: 0x04000B37 RID: 2871
	public List<Overworld_Structure> connectedStructuresViaRadius = new List<Overworld_Structure>();

	// Token: 0x04000B38 RID: 2872
	public HashSet<Overworld_Structure> connectedStructuresViaPath = new HashSet<Overworld_Structure>();

	// Token: 0x04000B39 RID: 2873
	public Overworld_Structure.StructureLayer structureLayer;

	// Token: 0x04000B3A RID: 2874
	public Overworld_Structure.GridSize gridSize = Overworld_Structure.GridSize.HalfTile;

	// Token: 0x04000B3B RID: 2875
	private float timeToDisplayCard;

	// Token: 0x04000B3C RID: 2876
	private bool isShowingCard;

	// Token: 0x04000B3D RID: 2877
	private Overworld_Interactable interactable;

	// Token: 0x04000B3E RID: 2878
	[SerializeField]
	private GameObject populationPrefab;

	// Token: 0x04000B3F RID: 2879
	public int populationAdd;

	// Token: 0x04000B40 RID: 2880
	private float pathRadius = 4f;

	// Token: 0x04000B41 RID: 2881
	private float existedFor;

	// Token: 0x04000B42 RID: 2882
	private bool canConvertBack;

	// Token: 0x04000B43 RID: 2883
	private bool firstBuildParticles;

	// Token: 0x04000B44 RID: 2884
	[SerializeField]
	private GameObject itemDestroyParticlePrefab;

	// Token: 0x04000B45 RID: 2885
	private Path path;

	// Token: 0x04000B46 RID: 2886
	private List<Vector2> storedLocationsTried = new List<Vector2>();

	// Token: 0x04000B47 RID: 2887
	private Coroutine removeCoroutine;

	// Token: 0x04000B48 RID: 2888
	public bool isBuildingFromSolidClick;

	// Token: 0x02000415 RID: 1045
	public enum PlacementCondition
	{
		// Token: 0x040017E8 RID: 6120
		None,
		// Token: 0x040017E9 RID: 6121
		MustBeNearEdge
	}

	// Token: 0x02000416 RID: 1046
	public enum StructureType
	{
		// Token: 0x040017EB RID: 6123
		Any,
		// Token: 0x040017EC RID: 6124
		Undefined,
		// Token: 0x040017ED RID: 6125
		Building,
		// Token: 0x040017EE RID: 6126
		Residential,
		// Token: 0x040017EF RID: 6127
		Commercial,
		// Token: 0x040017F0 RID: 6128
		Workshop,
		// Token: 0x040017F1 RID: 6129
		Religious,
		// Token: 0x040017F2 RID: 6130
		Decoration,
		// Token: 0x040017F3 RID: 6131
		Natural,
		// Token: 0x040017F4 RID: 6132
		Military,
		// Token: 0x040017F5 RID: 6133
		BridgeAndWater,
		// Token: 0x040017F6 RID: 6134
		Agrarian,
		// Token: 0x040017F7 RID: 6135
		Researcher
	}

	// Token: 0x02000417 RID: 1047
	public enum Area
	{
		// Token: 0x040017F9 RID: 6137
		withinAreaOfEffect,
		// Token: 0x040017FA RID: 6138
		connectedViaPath
	}

	// Token: 0x02000418 RID: 1048
	[Serializable]
	public class Modifier
	{
		// Token: 0x040017FB RID: 6139
		public string originName;

		// Token: 0x040017FC RID: 6140
		public bool applyToSelf;

		// Token: 0x040017FD RID: 6141
		public Overworld_Structure.Modifier.ConnectionType connectionType;

		// Token: 0x040017FE RID: 6142
		public List<Overworld_Structure.StructureType> structureTypes;

		// Token: 0x040017FF RID: 6143
		public List<GridObject.Tag> tagTypes;

		// Token: 0x04001800 RID: 6144
		public float efficiencyBonus;

		// Token: 0x020004BD RID: 1213
		public enum ConnectionType
		{
			// Token: 0x04001C64 RID: 7268
			Nearby,
			// Token: 0x04001C65 RID: 7269
			NearbyConnected,
			// Token: 0x04001C66 RID: 7270
			connectedViaPath
		}
	}

	// Token: 0x02000419 RID: 1049
	public enum StructureLayer
	{
		// Token: 0x04001802 RID: 6146
		Ground,
		// Token: 0x04001803 RID: 6147
		Tilemap,
		// Token: 0x04001804 RID: 6148
		Bridge
	}

	// Token: 0x0200041A RID: 1050
	public enum GridSize
	{
		// Token: 0x04001806 RID: 6150
		FullTile,
		// Token: 0x04001807 RID: 6151
		HalfTile
	}
}
