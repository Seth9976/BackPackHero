using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x0200014B RID: 331
public class Overworld_Manager : MonoBehaviour
{
	// Token: 0x06000CC6 RID: 3270 RVA: 0x00081935 File Offset: 0x0007FB35
	public void EnterNewBuildMode()
	{
		if (this.IsState(Overworld_Manager.State.NewBuildMode))
		{
			this.DisablePreview();
			this.SetState(Overworld_Manager.State.MOVING);
			return;
		}
		this.SetState(Overworld_Manager.State.NewBuildMode);
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x00081958 File Offset: 0x0007FB58
	public static bool IsFreeToMove()
	{
		return Overworld_Manager.main && !SingleUI.IsViewingPopUp() && (!Overworld_Manager.main || Overworld_Manager.main.timeOutToMove <= 0f) && (!Overworld_ConversationManager.main || !Overworld_ConversationManager.main.interactiveObject || !Overworld_ConversationManager.main.InLockedConversation()) && !Overworld_ConversationManager.main.ViewingOptions();
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x000819D8 File Offset: 0x0007FBD8
	private void Awake()
	{
		Overworld_Manager.main = this;
		MetaProgressSaveManager.main.ConsiderAdditionalMarkers();
		if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.readyForRaid) && !MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.returnedToDungeonAfterRaid))
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.townRaided);
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedElites);
			this.raidManager.enabled = true;
			this.newRunButton.SetActive(false);
		}
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00081A41 File Offset: 0x0007FC41
	private void OnDestroy()
	{
		Overworld_Manager.main = null;
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00081A4C File Offset: 0x0007FC4C
	private void Start()
	{
		this.previewObjectGridObject = this.previewObject.GetComponent<GridObject>();
		this.SetButtonsForBuildMode(false);
		this.previewSpriteRenderer = this.previewObject.GetComponent<SpriteRenderer>();
		float num = 100f;
		if (this.raidManager.enabled)
		{
			SoundManager.main.PlayOrContinueSong("bph_game_town_raid", 0f);
		}
		else
		{
			SoundManager.main.PlayOrContinueSong("TownMusic", num);
		}
		MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.backpackCollected);
		if (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.firstBuildingBuilt))
		{
			this.newRunButton.SetActive(false);
		}
		this.MakeReferences();
		this.ConsiderUnlocks();
		MetaProgressSaveManager.main.RemoveTemporaryMarkers();
		this.UpdateMap();
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x00081AFB File Offset: 0x0007FCFB
	private void ConsiderUnlocks()
	{
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x00081AFD File Offset: 0x0007FCFD
	public void EnableRunButton()
	{
		if (this.newRunButton.activeInHierarchy)
		{
			return;
		}
		this.newRunButton.SetActive(true);
		ArrowTutorialManager.instance.PointArrow(this.newRunButton.GetComponent<RectTransform>());
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x00081B2E File Offset: 0x0007FD2E
	public void MakeReferences()
	{
		if (!this.buildingsParent)
		{
			this.buildingsParent = GameObject.FindGameObjectWithTag("Buildings").transform;
		}
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x00081B54 File Offset: 0x0007FD54
	private void OnDrawGizmos()
	{
		if (this.overworld_Structure_currentlyBuilding)
		{
			Vector2 vector = Vector2.one;
			if (this.objectToCreateCollider)
			{
				vector = this.objectToCreateCollider.size;
			}
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(this.previewObject.transform.position, vector);
		}
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x00081BB4 File Offset: 0x0007FDB4
	private void SetupResourceDisplayPanel(List<Overworld_ResourceManager.Resource> costs, GameObject currentResourceDisplayPanelSource, int necessaryPopulationToDestroy = 0)
	{
		if (this.currentResourceDisplayPanelSource != currentResourceDisplayPanelSource)
		{
			this.ClearResourceDisplayPanel();
		}
		this.currentResourceDisplayPanelSource = currentResourceDisplayPanelSource;
		if (this.currentResourceDisplayPanel)
		{
			this.currentResourceDisplayPanel.transform.position = DigitalCursor.main.transform.position + Vector3.up * 1.5f;
			return;
		}
		if (costs.Count == 0)
		{
			return;
		}
		if (necessaryPopulationToDestroy > 0)
		{
			this.currentResourceDisplayPanel = Object.Instantiate<GameObject>(this.resourceDisplayPanelPrefabWithPopulationRequirement, new Vector3(-9999f, -9999f, 0f), Quaternion.identity);
			Overworld_ResourceDisplayPanel component = this.currentResourceDisplayPanel.GetComponent<Overworld_ResourceDisplayPanel>();
			component.SetupResources(costs, necessaryPopulationToDestroy);
			component.showPlus = true;
		}
		else
		{
			this.currentResourceDisplayPanel = Object.Instantiate<GameObject>(this.resourceDisplayPanelPrefab, new Vector3(-9999f, -9999f, 0f), Quaternion.identity);
			this.currentResourceDisplayPanel.GetComponent<Overworld_ResourceDisplayPanel>().SetupResources(costs);
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("UI Canvas");
		this.currentResourceDisplayPanel.transform.SetParent(gameObject.transform);
		this.currentResourceDisplayPanel.transform.position = Vector3.zero;
		this.currentResourceDisplayPanel.transform.localScale = Vector3.one;
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x00081CF5 File Offset: 0x0007FEF5
	private void ClearResourceDisplayPanel()
	{
		if (this.currentResourceDisplayPanel)
		{
			Object.Destroy(this.currentResourceDisplayPanel);
			this.currentResourceDisplayPanel = null;
			this.currentResourceDisplayPanelSource = null;
		}
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x00081D1D File Offset: 0x0007FF1D
	public void ClearAllDraggingBuildings()
	{
		Overworld_Structure.RemoveDraggingBuilding();
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x00081D24 File Offset: 0x0007FF24
	private bool AttemptBuild(Vector2 currentRoundedPosition)
	{
		return (Input.GetMouseButtonDown(0) || DigitalCursor.main.GetInputDown("confirm") || ((Input.GetMouseButton(0) || DigitalCursor.main.GetInputHold("confirm")) && currentRoundedPosition != this.lastRoundedPosition)) && DigitalCursor.main.NoUIElements();
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x00081D80 File Offset: 0x0007FF80
	public void ReturnToBuildMode()
	{
		this.ClearResourceDisplayPanel();
		this.DisablePreview();
		if (!this.IsState(Overworld_Manager.State.NewBuildMode))
		{
			this.SetState(Overworld_Manager.State.NewBuildMode);
		}
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x00081DA0 File Offset: 0x0007FFA0
	private void Update()
	{
		if (SingleUI.IsViewingPopUp())
		{
			this.timeOutToMove = 0.2f;
		}
		else
		{
			if (this.timeOutToMove > 0f)
			{
				this.timeOutToMove -= Time.deltaTime;
				return;
			}
			this.EndInteraction();
		}
		if (this.IsState(Overworld_Manager.State.NewBuildMode) || this.IsState(Overworld_Manager.State.DESTROYING) || this.IsState(Overworld_Manager.State.BUILDING))
		{
			if (DigitalCursor.main.GetInputDown("cancel"))
			{
				this.ClearResourceDisplayPanel();
				this.DisablePreview();
				if (!this.IsState(Overworld_Manager.State.NewBuildMode))
				{
					this.SetState(Overworld_Manager.State.NewBuildMode);
				}
			}
			if (Input.GetMouseButtonDown(1))
			{
				this.ClearResourceDisplayPanel();
				this.DisablePreview();
				if (!this.IsState(Overworld_Manager.State.NewBuildMode))
				{
					this.SetState(Overworld_Manager.State.NewBuildMode);
				}
				else if (!Overworld_Structure.draggingStructure)
				{
					this.SetState(Overworld_Manager.State.MOVING);
				}
			}
			Vector2 vector = Vector2.zero;
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				vector += Vector2.up;
			}
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				vector += Vector2.down;
			}
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				vector += Vector2.left;
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				vector += Vector2.right;
			}
			if (vector != Vector2.zero)
			{
				vector.Normalize();
				vector *= 12f;
				Camera.main.transform.position += vector * Time.deltaTime;
			}
		}
		Vector3 position = DigitalCursor.main.transform.position;
		this.previewObject.transform.position = position;
		Vector2 vector2 = Overworld_Manager.AlignToGrid(new Vector3(position.x, position.y, 0f));
		Vector2 vector3 = Overworld_Manager.AlignToGridHalf(new Vector3(position.x, position.y, 0f));
		if (this.lastRoundPosition != vector3)
		{
			if (this.overworld_Structure_currentlyBuilding)
			{
				base.StartCoroutine(this.ShowCircle(this.overworld_Structure_currentlyBuilding.gameObject));
			}
			this.lastRoundPosition = vector3;
		}
		if (this.changingOverworldState)
		{
			this.changingOverworldState = false;
			return;
		}
		if (this.overworldState == Overworld_Manager.State.INMENU)
		{
			this.followObjectCameraPurse.enabled = true;
			this.followObjectCameraCursor.enabled = false;
			if (!DigitalCursor.main.UIObjectSelected)
			{
				this.DisablePreview();
				this.SetState(Overworld_Manager.State.MOVING);
			}
		}
		if (this.overworldState == Overworld_Manager.State.DESTROYING)
		{
			this.followObjectCameraPurse.enabled = false;
			this.followObjectCameraCursor.enabled = true;
			if (DigitalCursor.main.OverUI())
			{
				this.DisablePreview();
				return;
			}
			if (this.CheckForBoundry())
			{
				this.previewObject.transform.position = new Vector3(-999f, -999f, this.previewObject.transform.position.z);
				if (this.AttemptBuild(vector2))
				{
					SoundManager.main.PlaySFX("negative");
					PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm70"));
				}
				return;
			}
			List<GridObject> itemsAtPosition = GridObject.GetItemsAtPosition(vector2);
			List<GridObject> list = GridObject.FilterByType(itemsAtPosition, GridObject.Type.item);
			List<GridObject> list2 = GridObject.FilterByType(itemsAtPosition, GridObject.Type.grid);
			list.RemoveAll((GridObject x) => x.tagType == GridObject.Tag.entrance);
			GridObject gridObject = null;
			foreach (GridObject gridObject2 in list)
			{
				if (gridObject2.tagType == GridObject.Tag.blocksPath || gridObject2.tagType == GridObject.Tag.decoration || list2.Count == 0)
				{
					gridObject = gridObject2;
					break;
				}
			}
			if (gridObject)
			{
				Overworld_Structure componentInParent = gridObject.GetComponentInParent<Overworld_Structure>();
				if (componentInParent)
				{
					int num;
					if (componentInParent.name.ToLower().Contains("rubble") || !componentInParent.increasesCost)
					{
						num = 1;
					}
					else
					{
						num = Overworld_Structure.StructuresOfType(componentInParent).Count;
					}
					List<Overworld_ResourceManager.Resource> list3 = Overworld_ResourceManager.main.MultiplyResourceCosts(componentInParent.costs, num);
					this.SetupResourceDisplayPanel(list3, componentInParent.gameObject, componentInParent.necessaryPopulationToDestroy);
					this.previewObject.SetActive(true);
					this.previewObject.transform.position = componentInParent.transform.position;
					this.SetPreviewToGameObject(this.previewObject, componentInParent.gameObject);
					this.previewSpriteRenderer.color = Color.red;
					if (this.AttemptBuild(vector2))
					{
						if (!Overworld_ResourceManager.main.HasEnoughResources(Overworld_ResourceManager.Resource.Type.Population, -componentInParent.necessaryPopulationToDestroy))
						{
							SoundManager.main.PlaySFX("negative");
							PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm92"));
							return;
						}
						componentInParent.NaturalDelete();
					}
				}
			}
			else if (list2.Count > 0 && list2[0].tagType != GridObject.Tag.water)
			{
				this.ClearResourceDisplayPanel();
				this.previewObject.SetActive(true);
				this.previewObject.transform.position = Overworld_Manager.AlignToGrid(new Vector3(position.x, position.y, 0f));
				this.previewSpriteRenderer.sprite = this.destroyingSprite;
				if (this.AttemptBuild(vector2))
				{
					SpriteRenderer component = list2[0].GetComponent<SpriteRenderer>();
					Sprite sprite = null;
					if (component)
					{
						sprite = component.sprite;
					}
					TileBase tileAtPosition = Overworld_TileManager.main.GetTileAtPosition(this.previewObject.transform.position, out sprite);
					Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, Overworld_ResourceManager.Resource.Type.BuildingMaterial, 3);
					SplashParticles.CopySpriteAndMoveToPosition(tileAtPosition, sprite, this.previewObject.transform.position, 300f, SplashParticles.Type.Create);
					Overworld_TileManager.main.RemoveTileAtPosition(this.previewObject.transform.position);
					this.UpdateMap();
				}
			}
			else
			{
				this.ClearResourceDisplayPanel();
				this.previewObject.SetActive(false);
			}
			this.lastRoundedPosition = Overworld_Manager.AlignToGrid(new Vector3(position.x, position.y, 0f));
			return;
		}
		else
		{
			if (this.overworldState == Overworld_Manager.State.NewBuildMode)
			{
				this.followObjectCameraPurse.enabled = false;
				this.followObjectCameraCursor.enabled = true;
				return;
			}
			if (this.overworldState != Overworld_Manager.State.BUILDING)
			{
				this.followObjectCameraPurse.enabled = true;
				this.followObjectCameraCursor.enabled = false;
				return;
			}
			this.followObjectCameraPurse.enabled = false;
			this.followObjectCameraCursor.enabled = true;
			Transform transform = null;
			if (this.overworld_Structure_currentlyBuilding)
			{
				this.previewObject.transform.position;
				if (this.overworld_Structure_currentlyBuilding.gridSize == Overworld_Structure.GridSize.HalfTile)
				{
					this.previewObject.transform.position = Overworld_Manager.AlignToGridHalf(this.previewObject.transform.position) + this.previewSpriteOffset;
				}
				else
				{
					this.previewObject.transform.position = Overworld_Manager.AlignToGrid(this.previewObject.transform.position) + this.previewSpriteOffset;
				}
				if (this.tile_currentlyBuilding)
				{
					this.previewObject.transform.position = Overworld_Manager.AlignToGrid(this.previewObject.transform.position);
				}
				if (this.overworld_Structure_currentlyBuilding.structureLayer == Overworld_Structure.StructureLayer.Bridge)
				{
					bool flag = false;
					float num2 = 2.5f;
					foreach (object obj in this.bridgeSpotsParent)
					{
						Transform transform2 = (Transform)obj;
						float num3 = Vector2.Distance(transform2.position, DigitalCursor.main.transform.position);
						if (num3 < num2)
						{
							this.previewObject.transform.position = transform2.position;
							transform = transform2;
							flag = true;
							num2 = num3;
						}
					}
					if (!flag)
					{
						this.SetToRed();
					}
				}
			}
			if (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.unlockedBridges) && (this.previewObject.transform.position.x < this.areaLeftOfRiver1.position.x || this.previewObject.transform.position.x > this.areaLeftOfRiver2.position.x || this.previewObject.transform.position.y > this.areaLeftOfRiver1.position.y || this.previewObject.transform.position.y < this.areaLeftOfRiver2.position.y) && (this.previewObject.transform.position.x < this.areaLeftOfRiver3.position.x || this.previewObject.transform.position.x > this.areaLeftOfRiver4.position.x || this.previewObject.transform.position.y > this.areaLeftOfRiver3.position.y || this.previewObject.transform.position.y < this.areaLeftOfRiver4.position.y))
			{
				if (this.AttemptBuild(vector2))
				{
					SoundManager.main.PlaySFX("negative");
					PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36"));
				}
				this.SetToRed();
				return;
			}
			this.SetToNormal();
			if (this.CheckForBoundry())
			{
				if (this.AttemptBuild(vector2))
				{
					PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36"));
					SoundManager.main.PlaySFX("negative");
				}
				this.SetToRed();
				return;
			}
			if (this.overworld_Structure_currentlyBuilding)
			{
				this.previewStructureGridObject.SnapToGrid();
				if (this.overworld_Structure_currentlyBuilding.IsValidPlacement(false, transform))
				{
					this.SetToNormal();
					if (this.AttemptBuild(vector2))
					{
						int num4 = Overworld_Structure.StructuresOfType(this.overworld_Structure_currentlyBuilding).Count + 1;
						if (!this.overworld_Structure_currentlyBuilding.increasesCost)
						{
							num4 = 1;
						}
						List<Overworld_ResourceManager.Resource> list4 = Overworld_ResourceManager.main.MultiplyResourceCosts(this.overworld_Structure_currentlyBuilding.costs, num4);
						if (!Overworld_ResourceManager.main.HasEnoughResources(list4, -1))
						{
							SoundManager.main.PlaySFX("negative");
							PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
							this.DisablePreview();
							this.SetState(Overworld_Manager.State.NewBuildMode);
							return;
						}
						MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.firstBuildingBuilt);
						GameObject gameObject = Object.Instantiate<GameObject>(this.objectToCreate, this.buildingsParent);
						gameObject.transform.position = this.previewObject.transform.position - this.previewSpriteOffset;
						gameObject.GetComponent<Overworld_Structure>().FirstBuild(1);
						Vector2 one = Vector2.one;
						if (this.objectToCreateCollider)
						{
							Vector2 size = this.objectToCreateCollider.size;
						}
						DisableWaterTile component2 = gameObject.GetComponent<DisableWaterTile>();
						if (component2)
						{
							component2.DisableTile();
						}
						this.UpdateMap();
						if (this.overworld_Structure_currentlyBuilding.oneOfAKind)
						{
							this.DisablePreview();
							this.SetState(Overworld_Manager.State.NewBuildMode);
						}
					}
				}
				else
				{
					if (this.AttemptBuild(vector2))
					{
						PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36"));
						SoundManager.main.PlaySFX("negative");
					}
					this.SetToRed();
				}
			}
			if (this.tile_currentlyBuilding)
			{
				this.previewObject.transform.position = vector2;
				if (this.tilemap.GetTile(this.tilemap.WorldToCell(vector2)) == this.waterTile)
				{
					this.SetToRed();
					return;
				}
				if (this.AttemptBuild(vector2))
				{
					if (!Overworld_ResourceManager.main.HasEnoughResources(Overworld_ResourceManager.Resource.Type.BuildingMaterial, -3))
					{
						SoundManager.main.PlaySFX("negative");
						PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
						this.SetState(Overworld_Manager.State.NewBuildMode);
						return;
					}
					Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, this.button_currentlyBuilding.resourceCosts, -1);
					Sprite sprite2;
					if (Overworld_TileManager.main.GetTileAtPosition(this.previewObject.transform.position, out sprite2))
					{
						Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, Overworld_ResourceManager.Resource.Type.BuildingMaterial, 3);
					}
					Overworld_TileManager.main.AddTileAtPosition(this.previewObject.transform.position, this.tile_currentlyBuilding);
					SoundManager.main.PlaySFX("buildPath");
					SplashParticles.CopySpriteAndMoveToPosition(this.previewObject.GetComponent<SpriteRenderer>(), 100f, SplashParticles.Type.Create);
					this.UpdateMap();
					if (!Overworld_ResourceManager.main.HasEnoughResources(this.button_currentlyBuilding.resourceCosts, 1))
					{
						this.DisablePreview();
						this.SetState(Overworld_Manager.State.MOVING);
						return;
					}
				}
			}
			this.lastRoundedPosition = Overworld_Manager.AlignToGrid(new Vector3(position.x, position.y, 0f));
			return;
		}
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x00082AB8 File Offset: 0x00080CB8
	public Transform GetClosestBridgeSpot(Vector2 pos)
	{
		float num = 1f;
		Transform transform = null;
		foreach (object obj in this.bridgeSpotsParent)
		{
			Transform transform2 = (Transform)obj;
			float num2 = Vector2.Distance(transform2.position, DigitalCursor.main.transform.position);
			if (num2 < num)
			{
				transform = transform2;
				num = num2;
			}
		}
		return transform;
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x00082B48 File Offset: 0x00080D48
	public bool IsBuildingMode()
	{
		return this.overworldState == Overworld_Manager.State.BUILDING || this.overworldState == Overworld_Manager.State.DESTROYING || this.overworldState == Overworld_Manager.State.NewBuildMode || this.overworldState == Overworld_Manager.State.INMENU;
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x00082B6F File Offset: 0x00080D6F
	public bool IsState(Overworld_Manager.State state)
	{
		return !this.changingOverworldState && this.overworldState == state;
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x00082B84 File Offset: 0x00080D84
	public Overworld_Manager.State GetState()
	{
		return this.overworldState;
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x00082B8C File Offset: 0x00080D8C
	public void UpdateText()
	{
		this.UpdateText(this.overworldState);
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x00082B9C File Offset: 0x00080D9C
	public void UpdateText(Overworld_Manager.State newState)
	{
		if (newState == Overworld_Manager.State.NewBuildMode && !Overworld_Structure.draggingStructure)
		{
			this.SetText(LangaugeManager.main.GetTextByKey("townState1"));
			this.previewObject.SetActive(false);
			return;
		}
		if (newState == Overworld_Manager.State.NewBuildMode && Overworld_Structure.draggingStructure)
		{
			this.previewObject.SetActive(false);
			this.SetText(LangaugeManager.main.GetTextByKey("townState4"));
			return;
		}
		if (newState == Overworld_Manager.State.DESTROYING)
		{
			this.SetText(LangaugeManager.main.GetTextByKey("townState2"));
			return;
		}
		if (newState == Overworld_Manager.State.BUILDING)
		{
			if (this.tile_currentlyBuilding)
			{
				this.SetText(LangaugeManager.main.GetTextByKey("townState3"));
				return;
			}
			if (this.overworld_Structure_currentlyBuilding)
			{
				this.SetText(LangaugeManager.main.GetTextByKey("townState4"));
			}
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x00082C70 File Offset: 0x00080E70
	public void SetState(Overworld_Manager.State newState)
	{
		Overworld_Purse.main.StopMoving();
		if (newState != Overworld_Manager.State.NewBuildMode && newState != Overworld_Manager.State.DESTROYING && newState != Overworld_Manager.State.BUILDING)
		{
			this.SetButtonsForBuildMode(false);
			if (!Overworld_ConversationManager.main.InLockedConversation())
			{
				PixelZoomer.main.SetResolution(1f);
			}
			Overworld_ResourceManager.main.HideAllGainPanels();
		}
		else
		{
			this.SetButtonsForBuildMode(true);
			PixelZoomer.main.SetResolution(0.75f);
			this.UpdateText(newState);
			Overworld_ResourceManager.main.ShowAllGainPanels();
		}
		this.timeOutToMove = 0.1f;
		this.overworldState = newState;
		this.changingOverworldState = true;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00082D00 File Offset: 0x00080F00
	public void SetText(string text)
	{
		if (this.textCoroutine != null)
		{
			base.StopCoroutine(this.textCoroutine);
		}
		this.textCoroutine = base.StartCoroutine(this.SetTextCoroutine(text));
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x00082D29 File Offset: 0x00080F29
	private IEnumerator SetTextCoroutine(string text)
	{
		LangaugeManager.main.SetFont(this.constructionIndicatorText.transform);
		this.constructionIndicatorText.text = "";
		while (this.constructionIndicatorText.text != text)
		{
			do
			{
				this.constructionIndicatorText.text = text.Substring(0, this.constructionIndicatorText.text.Length + 1);
			}
			while (this.constructionIndicatorText.text[this.constructionIndicatorText.text.Length - 1] == ' ' && this.constructionIndicatorText.text != text);
			yield return new WaitForSeconds(0.05f);
		}
		yield break;
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x00082D40 File Offset: 0x00080F40
	private void SetButtonsForBuildMode(bool b)
	{
		if (!b)
		{
			if (this.constructionIndicatorBars.gameObject.activeInHierarchy)
			{
				this.constructionIndicatorBars.SetTrigger("Exit");
			}
			this.buildModePanel.gameObject.SetActive(false);
			foreach (GameObject gameObject in this.objectsToShowDuringBuildMode)
			{
				gameObject.SetActive(false);
			}
			foreach (GameObject gameObject2 in this.objectsToShowDuringWalkMode)
			{
				gameObject2.SetActive(true);
			}
			if (MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.talkedToZaarAndUnlockedMissionMenu))
			{
				this.newRunButton.SetActive(true);
			}
			this.previewObject.SetActive(false);
			return;
		}
		Overworld_BuildingManager.main.ClearPulse(this.buildModeButton);
		this.constructionIndicatorBars.gameObject.SetActive(true);
		this.constructionIndicatorBars.SetTrigger("Enter");
		if (Overworld_BuildingManager.main.AnythingToBuild())
		{
			this.buildModePanel.gameObject.SetActive(true);
		}
		foreach (GameObject gameObject3 in this.objectsToShowDuringBuildMode)
		{
			gameObject3.SetActive(true);
		}
		foreach (GameObject gameObject4 in this.objectsToShowDuringWalkMode)
		{
			gameObject4.SetActive(false);
		}
		this.newRunButton.SetActive(false);
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x00082F0C File Offset: 0x0008110C
	private bool CheckForSpace()
	{
		return GridObject.GetGridObjectsAtCollider(this.overworld_Structure_currentlyBuilding.GetComponentInChildren<BoxCollider2D>(), this.previewObject.transform.position).Count == 0;
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x00082F38 File Offset: 0x00081138
	private bool CheckForBoundry()
	{
		if (this.objectToCreate && this.overworldState != Overworld_Manager.State.DESTROYING)
		{
			foreach (BoxCollider2D boxCollider2D in this.objectToCreate.GetComponentsInChildren<BoxCollider2D>())
			{
				Vector2 vector = this.previewObject.transform.position - this.previewSpriteOffset + boxCollider2D.offset;
				if (boxCollider2D.transform != this.objectToCreate.transform)
				{
					vector += boxCollider2D.transform.position;
				}
				Vector2 size = boxCollider2D.size;
				foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(vector, size, 0f, Vector2.zero))
				{
					if (raycastHit2D.collider.gameObject.CompareTag("Boundry"))
					{
						return true;
					}
				}
			}
		}
		else
		{
			foreach (RaycastHit2D raycastHit2D2 in Physics2D.BoxCastAll(this.previewObject.transform.position, Vector2.one, 0f, Vector2.zero))
			{
				if (raycastHit2D2.collider.gameObject.CompareTag("Boundry"))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0008309C File Offset: 0x0008129C
	public bool CheckForBoundry(List<Collider2D> collider2Ds)
	{
		foreach (Collider2D collider2D in collider2Ds)
		{
			BoxCollider2D boxCollider2D = collider2D as BoxCollider2D;
			if (boxCollider2D)
			{
				Vector2 vector = boxCollider2D.transform.position + boxCollider2D.offset;
				Vector2 size = boxCollider2D.size;
				foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(vector, size, 0f, Vector2.zero))
				{
					if (raycastHit2D.collider.gameObject.CompareTag("Boundry"))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0008316C File Offset: 0x0008136C
	private bool CheckForWaterHere()
	{
		if (this.objectToCreate && this.overworldState != Overworld_Manager.State.DESTROYING)
		{
			foreach (BoxCollider2D boxCollider2D in this.objectToCreate.GetComponentsInChildren<BoxCollider2D>())
			{
				Vector2 vector = this.previewObject.transform.position - this.previewSpriteOffset + boxCollider2D.offset;
				if (boxCollider2D.transform != this.objectToCreate.transform)
				{
					vector += boxCollider2D.transform.position;
				}
				Vector2 size = boxCollider2D.size;
				foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(vector, size, 0f, Vector2.zero))
				{
					if (raycastHit2D.collider.gameObject.CompareTag("Overworld_Water"))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x00083270 File Offset: 0x00081470
	public bool CheckForWaterHere(List<Collider2D> collider2Ds)
	{
		foreach (Collider2D collider2D in collider2Ds)
		{
			BoxCollider2D boxCollider2D = collider2D as BoxCollider2D;
			if (boxCollider2D)
			{
				Vector2 vector = boxCollider2D.transform.position + boxCollider2D.offset;
				Vector2 size = boxCollider2D.size;
				foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(vector, size, 0f, Vector2.zero))
				{
					if (raycastHit2D.collider.gameObject.CompareTag("Overworld_Water"))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x00083340 File Offset: 0x00081540
	private GameObject CheckForObjectHere()
	{
		if (this.objectToCreate && this.overworldState != Overworld_Manager.State.DESTROYING)
		{
			foreach (BoxCollider2D boxCollider2D in this.objectToCreate.GetComponentsInChildren<BoxCollider2D>())
			{
				Vector2 vector = this.previewObject.transform.position - this.previewSpriteOffset + boxCollider2D.offset;
				if (boxCollider2D.transform != this.objectToCreate.transform)
				{
					vector += boxCollider2D.transform.position;
				}
				Vector2 size = boxCollider2D.size;
				foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(vector, size, 0f, Vector2.zero))
				{
					if (!raycastHit2D.collider.gameObject.CompareTag("BuildingEntrance"))
					{
						Overworld_Structure componentInParent = raycastHit2D.collider.gameObject.GetComponentInParent<Overworld_Structure>();
						if (raycastHit2D.collider.gameObject != this.previewObject && componentInParent && componentInParent.structureLayer != Overworld_Structure.StructureLayer.Tilemap)
						{
							return raycastHit2D.collider.gameObject;
						}
					}
				}
			}
		}
		else
		{
			foreach (RaycastHit2D raycastHit2D2 in Physics2D.BoxCastAll(this.previewObject.transform.position, Vector2.one, 0f, Vector2.zero))
			{
				if (!raycastHit2D2.collider.gameObject.CompareTag("BuildingEntrance"))
				{
					Overworld_Structure componentInParent2 = raycastHit2D2.collider.gameObject.GetComponentInParent<Overworld_Structure>();
					if (raycastHit2D2.collider.gameObject != this.previewObject && componentInParent2 && componentInParent2.structureLayer != Overworld_Structure.StructureLayer.Tilemap)
					{
						return componentInParent2.gameObject;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x00083534 File Offset: 0x00081734
	public void UpdateMap()
	{
		if (this.UpdateMapRoutine != null)
		{
			return;
		}
		this.UpdateMapRoutine = base.StartCoroutine(this.UpdateMap2());
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x00083551 File Offset: 0x00081751
	private IEnumerator UpdateMap2()
	{
		yield return new WaitForFixedUpdate();
		yield return null;
		GridGraph gridGraph = AstarPath.active.data.gridGraph;
		foreach (Progress progress in AstarPath.active.ScanAsync(gridGraph))
		{
			yield return null;
		}
		IEnumerator<Progress> enumerator = null;
		while (this.astarPath.isScanning)
		{
			yield return null;
		}
		yield return null;
		yield return new WaitForFixedUpdate();
		MetaProgressSaveManager.main.CalculatePassiveGenerationRate();
		MetaProgressSaveManager.main.CalculatePopulation();
		this.UpdateMapRoutine = null;
		yield break;
		yield break;
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x00083560 File Offset: 0x00081760
	public static Vector3 AlignToGrid(Vector3 position)
	{
		return new Vector3(Mathf.Round(position.x - 0.5f) + 0.5f, Mathf.Round(position.y - 0.5f) + 0.5f, Mathf.Round(position.z));
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x000835A0 File Offset: 0x000817A0
	public static Vector3 AlignToGridHalf(Vector3 position)
	{
		return Overworld_Manager.AlignToGrid(position);
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x000835A8 File Offset: 0x000817A8
	public void RememberOverworldButton(Overworld_Button b)
	{
		this.button_currentlyBuilding = b;
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x000835B1 File Offset: 0x000817B1
	public void GetButton(GameObject objectToSpawn)
	{
		this.SetPreviewToGameObject(this.previewObject, objectToSpawn);
		this.SetState(Overworld_Manager.State.BUILDING);
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x000835C7 File Offset: 0x000817C7
	public void GetButton(RuleTile objectToSpawn)
	{
		this.previewSpriteOffset = Vector3.zero;
		this.SetPreviewToGameObject(this.previewObject, objectToSpawn);
		this.SetState(Overworld_Manager.State.BUILDING);
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x000835E8 File Offset: 0x000817E8
	public void DestroyMode()
	{
		this.DisablePreview();
		this.previewObject.SetActive(true);
		this.SetState(Overworld_Manager.State.DESTROYING);
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x00083603 File Offset: 0x00081803
	public void DisablePreview()
	{
		Overworld_Structure.EndCursorBuilding();
		this.circleRenderer.HideCircle();
		this.objectToCreate = null;
		this.tile_currentlyBuilding = null;
		this.overworld_Structure_currentlyBuilding = null;
		this.previewSpriteRenderer.sprite = null;
		this.previewObject.SetActive(false);
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x00083644 File Offset: 0x00081844
	private void SetToNormal()
	{
		SpriteRenderer componentInChildren = this.previewObject.GetComponentInChildren<SpriteRenderer>();
		if (this.objectToCreateSpriteRenderer)
		{
			componentInChildren.color = new Color(this.objectToCreateSpriteRenderer.color.r, this.objectToCreateSpriteRenderer.color.g, this.objectToCreateSpriteRenderer.color.b, 0.5f);
			return;
		}
		componentInChildren.color = new Color(1f, 1f, 1f, 0.5f);
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x000836CA File Offset: 0x000818CA
	private void SetToRed()
	{
		this.previewObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x000836F8 File Offset: 0x000818F8
	private void SetPreviewToGameObject(GameObject previewObject, GameObject buildingObject)
	{
		base.StartCoroutine(this.ShowCircle(buildingObject));
		this.overworld_Structure_currentlyBuilding = buildingObject.GetComponent<Overworld_Structure>();
		this.objectToCreate = buildingObject;
		this.previewStructureCollider.size = this.overworld_Structure_currentlyBuilding.gridCollider.size;
		this.previewStructureCollider.offset = this.overworld_Structure_currentlyBuilding.gridCollider.offset;
		this.previewStructureCollider.transform.localPosition = this.overworld_Structure_currentlyBuilding.gridCollider.transform.localPosition;
		this.previewStructureCollider.transform.localScale = this.overworld_Structure_currentlyBuilding.gridCollider.transform.localScale;
		previewObject.SetActive(true);
		this.objectToCreateSpriteRenderer = buildingObject.GetComponentInChildren<SpriteRenderer>();
		this.previewSpriteOffset = Vector3.zero;
		this.previewSpriteRenderer.sprite = this.objectToCreateSpriteRenderer.sprite;
		this.previewSpriteRenderer.drawMode = this.objectToCreateSpriteRenderer.drawMode;
		this.previewSpriteRenderer.size = this.objectToCreateSpriteRenderer.size;
		this.previewSpriteRenderer.color = new Color(this.objectToCreateSpriteRenderer.color.r, this.objectToCreateSpriteRenderer.color.g, this.objectToCreateSpriteRenderer.color.b, 0.5f);
		this.previewSpriteRenderer.transform.localScale = this.objectToCreateSpriteRenderer.transform.lossyScale;
		this.previewSpriteRenderer.transform.localPosition += this.objectToCreateSpriteRenderer.transform.localPosition;
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x00083896 File Offset: 0x00081A96
	private IEnumerator ShowCircle(GameObject buildingObject)
	{
		yield break;
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x000838AC File Offset: 0x00081AAC
	private void SetPreviewToGameObject(GameObject previewObject, RuleTile tile)
	{
		Overworld_Structure.EndCursorBuilding();
		this.tile_currentlyBuilding = tile;
		previewObject.SetActive(true);
		this.previewSpriteRenderer.sprite = tile.m_DefaultSprite;
		this.previewSpriteRenderer.drawMode = SpriteDrawMode.Sliced;
		this.previewSpriteRenderer.size = this.tilemap.cellSize;
		this.previewSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
		this.previewSpriteRenderer.transform.localScale = Vector3.one;
		this.previewSpriteRenderer.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x00083954 File Offset: 0x00081B54
	public GameObject OpenNewConstructionWindow(SellingTile tile)
	{
		Transform transform = GameObject.FindGameObjectWithTag("UI Canvas").transform;
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetBuilding(tile);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x00083994 File Offset: 0x00081B94
	public GameObject OpenNewSimpleImageWindow(Sprite s, string unlockName, string cardName, string text)
	{
		Transform transform = GameObject.FindGameObjectWithTag("UI Canvas").transform;
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		gameObject.GetComponent<NewItemWindow>().SetSimpleImageCard(s, unlockName, cardName, text);
		return gameObject;
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x000839D0 File Offset: 0x00081BD0
	public GameObject OpenNewLoreWindow(string lore)
	{
		Transform transform = GameObject.FindGameObjectWithTag("UI Canvas").transform;
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetLore(lore);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x00083A10 File Offset: 0x00081C10
	public GameObject OpenNewConstructionWindow(Overworld_Structure structure)
	{
		Transform transform = GameObject.FindGameObjectWithTag("UI Canvas").transform;
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetBuilding(structure);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x00083A50 File Offset: 0x00081C50
	public GameObject OpenNewMissionWindow(Missions m)
	{
		Transform transform = GameObject.FindGameObjectWithTag("UI Canvas").transform;
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetMission(m);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x00083A90 File Offset: 0x00081C90
	public GameObject OpenNewCharacterWindow(Character character)
	{
		Transform transform = GameObject.FindGameObjectWithTag("UI Canvas").transform;
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetCharacter(character);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x00083AD0 File Offset: 0x00081CD0
	public GameObject OpenNewCostumeWindow(RuntimeAnimatorController controller)
	{
		Transform transform = GameObject.FindGameObjectWithTag("UI Canvas").transform;
		GameObject gameObject = Object.Instantiate<GameObject>(this.newCostumeWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetCostume(controller);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x00083B10 File Offset: 0x00081D10
	public GameObject OpenNewItemsWindow(List<Item2> item)
	{
		Transform transform = GameObject.FindGameObjectWithTag("UI Canvas").transform;
		GameObject gameObject = Object.Instantiate<GameObject>(this.newItemWindowPrefab, transform);
		NewItemWindow component = gameObject.GetComponent<NewItemWindow>();
		component.SetItems(item);
		component.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x00083B50 File Offset: 0x00081D50
	public void OpenNewItemWindow(Item2 item)
	{
		this.OpenNewItemsWindow(new List<Item2> { item });
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x00083B65 File Offset: 0x00081D65
	public void StartInteraction(Overworld_InteractiveObject target)
	{
		this.currentTargetForInteraction = target;
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x00083B6E File Offset: 0x00081D6E
	public void EndInteraction()
	{
		if (!this.currentTargetForInteraction)
		{
			return;
		}
		this.currentTargetForInteraction.EndInteraction();
		this.currentTargetForInteraction = null;
		this.SetState(Overworld_Manager.State.MOVING);
		this.DisablePreview();
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x00083BA0 File Offset: 0x00081DA0
	public bool OverBuildModePanel(Vector2 pos)
	{
		Vector3[] array = new Vector3[4];
		this.buildModePanel.GetWorldCorners(array);
		return pos.x <= array[2].x && pos.x >= array[0].x && pos.y <= array[2].y && pos.y >= array[0].y;
	}

	// Token: 0x04000A48 RID: 2632
	[SerializeField]
	public RectTransform buildModePanel;

	// Token: 0x04000A49 RID: 2633
	[SerializeField]
	private List<GameObject> objectsToShowDuringBuildMode;

	// Token: 0x04000A4A RID: 2634
	[SerializeField]
	private List<GameObject> objectsToShowDuringWalkMode;

	// Token: 0x04000A4B RID: 2635
	[SerializeField]
	private GameObject buildModeButton;

	// Token: 0x04000A4C RID: 2636
	[SerializeField]
	private Animator constructionIndicatorBars;

	// Token: 0x04000A4D RID: 2637
	[SerializeField]
	private TextMeshProUGUI constructionIndicatorText;

	// Token: 0x04000A4E RID: 2638
	[SerializeField]
	private CircleRenderer circleRenderer;

	// Token: 0x04000A4F RID: 2639
	[SerializeField]
	private Transform areaLeftOfRiver1;

	// Token: 0x04000A50 RID: 2640
	[SerializeField]
	private Transform areaLeftOfRiver2;

	// Token: 0x04000A51 RID: 2641
	[SerializeField]
	private Transform areaLeftOfRiver3;

	// Token: 0x04000A52 RID: 2642
	[SerializeField]
	private Transform areaLeftOfRiver4;

	// Token: 0x04000A53 RID: 2643
	public static Overworld_Manager main;

	// Token: 0x04000A54 RID: 2644
	[Header("References")]
	[SerializeField]
	public Transform cardPosition;

	// Token: 0x04000A55 RID: 2645
	[SerializeField]
	private Overworld_Structure previewStructure;

	// Token: 0x04000A56 RID: 2646
	[SerializeField]
	private BoxCollider2D previewStructureCollider;

	// Token: 0x04000A57 RID: 2647
	[SerializeField]
	private GridObject previewStructureGridObject;

	// Token: 0x04000A58 RID: 2648
	[SerializeField]
	private FollowObjectCamera followObjectCameraPurse;

	// Token: 0x04000A59 RID: 2649
	[SerializeField]
	private FollowObjectCamera followObjectCameraCursor;

	// Token: 0x04000A5A RID: 2650
	[SerializeField]
	private AstarPath astarPath;

	// Token: 0x04000A5B RID: 2651
	[SerializeField]
	private Tilemap topLayerTilemap;

	// Token: 0x04000A5C RID: 2652
	[SerializeField]
	public Tilemap tilemap;

	// Token: 0x04000A5D RID: 2653
	[SerializeField]
	public TileBase waterTile;

	// Token: 0x04000A5E RID: 2654
	[SerializeField]
	private GameObject previewObject;

	// Token: 0x04000A5F RID: 2655
	[SerializeField]
	private GridObject previewObjectGridObject;

	// Token: 0x04000A60 RID: 2656
	private SpriteRenderer previewSpriteRenderer;

	// Token: 0x04000A61 RID: 2657
	[SerializeField]
	public Animator buttonsAnimator;

	// Token: 0x04000A62 RID: 2658
	[SerializeField]
	private Sprite blankSquare;

	// Token: 0x04000A63 RID: 2659
	[SerializeField]
	private Sprite destroyingSprite;

	// Token: 0x04000A64 RID: 2660
	[SerializeField]
	private GameObject shopAtlasPrefab;

	// Token: 0x04000A65 RID: 2661
	[SerializeField]
	private Transform buildingsParent;

	// Token: 0x04000A66 RID: 2662
	[SerializeField]
	public GameObject newRunButton;

	// Token: 0x04000A67 RID: 2663
	private Overworld_Button button_currentlyBuilding;

	// Token: 0x04000A68 RID: 2664
	public RuntimeAnimatorController animatorController;

	// Token: 0x04000A69 RID: 2665
	[SerializeField]
	private GameObject newItemWindowPrefab;

	// Token: 0x04000A6A RID: 2666
	[SerializeField]
	private GameObject newCostumeWindowPrefab;

	// Token: 0x04000A6B RID: 2667
	[SerializeField]
	private GameObject currentResourceDisplayPanelSource;

	// Token: 0x04000A6C RID: 2668
	[SerializeField]
	private GameObject resourceDisplayPanelPrefab;

	// Token: 0x04000A6D RID: 2669
	[SerializeField]
	private GameObject resourceDisplayPanelPrefabWithPopulationRequirement;

	// Token: 0x04000A6E RID: 2670
	[SerializeField]
	private GameObject currentResourceDisplayPanel;

	// Token: 0x04000A6F RID: 2671
	[SerializeField]
	private Transform bridgeSpotsParent;

	// Token: 0x04000A70 RID: 2672
	[Header("Overworld State")]
	private bool changingOverworldState;

	// Token: 0x04000A71 RID: 2673
	private float pauseTime;

	// Token: 0x04000A72 RID: 2674
	[SerializeField]
	private Overworld_Manager.State overworldState = Overworld_Manager.State.MOVING;

	// Token: 0x04000A73 RID: 2675
	private Overworld_InteractiveObject currentTargetForInteraction;

	// Token: 0x04000A74 RID: 2676
	private GameObject objectToCreate;

	// Token: 0x04000A75 RID: 2677
	private SpriteRenderer objectToCreateSpriteRenderer;

	// Token: 0x04000A76 RID: 2678
	private BoxCollider2D objectToCreateCollider;

	// Token: 0x04000A77 RID: 2679
	private Overworld_Structure overworld_Structure_currentlyBuilding;

	// Token: 0x04000A78 RID: 2680
	private RuleTile tile_currentlyBuilding;

	// Token: 0x04000A79 RID: 2681
	private Vector3 previewSpriteOffset;

	// Token: 0x04000A7A RID: 2682
	private float timeOutToMove;

	// Token: 0x04000A7B RID: 2683
	[SerializeField]
	private RaidManager raidManager;

	// Token: 0x04000A7C RID: 2684
	[SerializeField]
	private List<Item2> items = new List<Item2>();

	// Token: 0x04000A7D RID: 2685
	[SerializeField]
	private Character character1;

	// Token: 0x04000A7E RID: 2686
	private Vector2 lastRoundedPosition;

	// Token: 0x04000A7F RID: 2687
	private Vector2 lastRoundPosition;

	// Token: 0x04000A80 RID: 2688
	private Coroutine textCoroutine;

	// Token: 0x04000A81 RID: 2689
	private Coroutine UpdateMapRoutine;

	// Token: 0x020003FA RID: 1018
	public enum State
	{
		// Token: 0x0400177A RID: 6010
		BUILDING,
		// Token: 0x0400177B RID: 6011
		MOVING,
		// Token: 0x0400177C RID: 6012
		DESTROYING,
		// Token: 0x0400177D RID: 6013
		INMENU,
		// Token: 0x0400177E RID: 6014
		WAITING,
		// Token: 0x0400177F RID: 6015
		NewBuildMode
	}
}
