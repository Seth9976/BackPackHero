using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013C RID: 316
public class Overworld_Button : CustomInputHandler
{
	// Token: 0x06000BE2 RID: 3042 RVA: 0x0007C3E2 File Offset: 0x0007A5E2
	private void Awake()
	{
		Overworld_Button.buttons.Add(this);
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x0007C3EF File Offset: 0x0007A5EF
	private void OnDestroy()
	{
		if (this.isDragging)
		{
			Overworld_Button.isDraggingAnything = false;
		}
		Overworld_Button.buttons.Remove(this);
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x0007C40B File Offset: 0x0007A60B
	public string GetName()
	{
		if (this.objectToSpawn)
		{
			return this.objectToSpawn.name;
		}
		if (this.tileToSpawn)
		{
			return this.tileToSpawn.name;
		}
		return "";
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x0007C444 File Offset: 0x0007A644
	private void Start()
	{
		this.parentInMenu = base.gameObject.transform.parent;
		if (this.parentInMenu != null)
		{
			this.parentButtonGroup = this.parentInMenu.GetComponent<Overworld_ButtonGroup>();
			if (this.parentButtonGroup != null)
			{
				this.isInGroup = true;
			}
		}
		if (this.tileToSpawn)
		{
			this.image.sprite = this.tileToSpawn.m_DefaultSprite;
			this.image.color = Color.white;
			base.gameObject.AddComponent<ReplacementText>().key = Item2.GetDisplayName(this.tileToSpawn.name);
		}
		if (this.objectToSpawn)
		{
			SpriteRenderer componentInChildren = this.objectToSpawn.GetComponentInChildren<SpriteRenderer>();
			this.structure = this.objectToSpawn.GetComponent<Overworld_Structure>();
			this.image.sprite = componentInChildren.sprite;
			float num = componentInChildren.sprite.rect.width;
			float num2 = componentInChildren.sprite.rect.height;
			float num3 = num / num2;
			if (num > num2)
			{
				num = 120f;
				num2 = num / num3;
			}
			else
			{
				num2 = 120f;
				num = num2 * num3;
			}
			this.image.rectTransform.sizeDelta = new Vector2(num, num2);
			this.image.color = componentInChildren.color;
			base.gameObject.AddComponent<ReplacementText>().key = Item2.GetDisplayName(this.objectToSpawn.name);
		}
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x0007C5BC File Offset: 0x0007A7BC
	public override void OnCursorHold()
	{
		if (this.isInGroup && base.gameObject.transform.parent == this.parentInMenu && this.parentButtonGroup.position != Overworld_ButtonGroup.Position.open)
		{
			return;
		}
		if (Overworld_Button.isDraggingAnything)
		{
			return;
		}
		this.timeHovering += Time.deltaTime;
		if (!this.previewCard && this.objectToSpawn && this.timeHovering > 0.25f && this.structure)
		{
			this.previewCard = Overworld_CardManager.main.DisplayCard(this.structure, null);
			Card component = this.previewCard.GetComponent<Card>();
			component.SetParent(base.gameObject);
			List<Overworld_ResourceManager.Resource> list = this.GetResourceCosts();
			component.GetResourceCosts(list);
		}
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x0007C685 File Offset: 0x0007A885
	public override void OnCursorEnd()
	{
		this.timeHovering = 0f;
		if (this.previewCard)
		{
			Object.Destroy(this.previewCard);
		}
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x0007C6AA File Offset: 0x0007A8AA
	public void SetBuilding(Overworld_BuildingManager.Building building)
	{
		this.objectToSpawn = building.prefab;
		this.tileToSpawn = building.tile;
		this.resourceCosts = building.costs;
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x0007C6D0 File Offset: 0x0007A8D0
	public List<Overworld_ResourceManager.Resource> GetResourceCosts()
	{
		if (this.resourceCosts.Count <= 0)
		{
			return null;
		}
		if (!this.objectToSpawn || !this.structure)
		{
			return null;
		}
		int num = Overworld_Structure.StructuresOfType(this.structure).Count + 1;
		if (!this.structure.increasesCost)
		{
			num = 1;
		}
		return Overworld_ResourceManager.main.MultiplyResourceCosts(this.structure.costs, num);
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x0007C744 File Offset: 0x0007A944
	private void Update()
	{
		if (this.existedFor < 0.25f)
		{
			this.existedFor += Time.deltaTime;
		}
		else
		{
			this.canConvertBack = true;
		}
		if (this.isDragging)
		{
			this.timeDragging += Time.deltaTime;
			base.transform.localPosition = base.transform.parent.InverseTransformPoint(DigitalCursor.main.transform.position);
			if (!this.isBuildingFromSolidClick && this.canConvertBack && ((!Input.GetMouseButton(0) && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor) || DigitalCursor.main.GetInputDown("cancel") || (!DigitalCursor.main.GetInputHold("confirm") && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)))
			{
				this.EndDrag();
				return;
			}
			if (this.isBuildingFromSolidClick && (DigitalCursor.main.GetInputDown("confirm") || DigitalCursor.main.GetInputDown("interact") || Input.GetMouseButtonDown(0)) && this.canConvertBack)
			{
				this.EndDrag();
				return;
			}
			if (this.verticalLayoutGroup && this.canConvertBack && this.timeDragging > 0.25f)
			{
				Vector3[] array = new Vector3[4];
				this.verticalLayoutGroup.GetWorldCorners(array);
				if (base.transform.position.x > array[2].x || base.transform.position.x < array[0].x || base.transform.position.y > array[2].y || base.transform.position.y < array[0].y)
				{
					this.timeToConvert += Time.deltaTime;
					if (this.timeToConvert >= this.timeToConvertMax)
					{
						this.ConvertToBuilding(true);
					}
				}
			}
		}
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x0007C938 File Offset: 0x0007AB38
	private void ConvertToBuilding(bool isDragged = true)
	{
		if (this.objectToSpawn == null)
		{
			return;
		}
		if (!this.canConvertBack)
		{
			return;
		}
		this.canSwitchMode = false;
		Overworld_Structure component = this.objectToSpawn.GetComponent<Overworld_Structure>();
		if (!component)
		{
			return;
		}
		if (component.oneOfAKind)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			this.EndDrag();
		}
		Overworld_Structure component2 = Object.Instantiate<GameObject>(this.objectToSpawn, DigitalCursor.main.transform.position, Quaternion.identity).GetComponent<Overworld_Structure>();
		component2.StartDragFromButton();
		if (!isDragged)
		{
			Overworld_Manager.main.SetState(Overworld_Manager.State.BUILDING);
			Overworld_Manager.main.SetText(LangaugeManager.main.GetTextByKey("townState4"));
		}
		component2.isBuildingFromSolidClick = this.isBuildingFromSolidClick;
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x0007C9F0 File Offset: 0x0007ABF0
	public void StartDragIfCanAfford()
	{
		Overworld_Structure component = this.objectToSpawn.GetComponent<Overworld_Structure>();
		if (!component)
		{
			return;
		}
		int num = Overworld_Structure.StructuresOfType(component).Count + 1;
		if (!component.increasesCost)
		{
			num = 1;
		}
		List<Overworld_ResourceManager.Resource> list = Overworld_ResourceManager.main.MultiplyResourceCosts(component.costs, num);
		if (!Overworld_ResourceManager.main.HasEnoughResources(list, -1))
		{
			SoundManager.main.PlaySFX("negative");
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
			return;
		}
		Overworld_Manager.main.SetState(Overworld_Manager.State.NewBuildMode);
		this.StartDrag();
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x0007CA84 File Offset: 0x0007AC84
	public void StartDrag()
	{
		if (this.objectToSpawn == null)
		{
			return;
		}
		if (Overworld_Button.isDraggingAnything)
		{
			return;
		}
		this.canSwitchMode = true;
		VerticalLayoutGroup componentInParent = base.GetComponentInParent<VerticalLayoutGroup>();
		if (componentInParent)
		{
			this.verticalLayoutGroup = componentInParent.GetComponent<RectTransform>();
		}
		if (this.previewCard)
		{
			Object.Destroy(this.previewCard);
		}
		base.transform.SetAsLastSibling();
		this.isDragging = true;
		Overworld_Button.isDraggingAnything = true;
		this.isBuildingFromSolidClick = false;
		this.layoutElement.ignoreLayout = true;
		this.timeDragging = 0f;
		base.GetComponent<Button>().interactable = false;
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x0007CB24 File Offset: 0x0007AD24
	public void ForceRemoval()
	{
		foreach (Overworld_Button overworld_Button in Object.FindObjectsOfType<Overworld_Button>())
		{
			if (overworld_Button != this && overworld_Button.objectToSpawn == this.objectToSpawn)
			{
				Object.Destroy(base.gameObject);
				return;
			}
		}
		Overworld_Button.isDraggingAnything = false;
		this.isDragging = false;
		this.timeDragging = 0f;
		this.layoutElement.ignoreLayout = false;
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x0007CB98 File Offset: 0x0007AD98
	public static void EndAllButtonDrags()
	{
		Overworld_Button.isDraggingAnything = false;
		foreach (Overworld_Button overworld_Button in Overworld_Button.buttons)
		{
			overworld_Button.EndDrag();
		}
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x0007CBF0 File Offset: 0x0007ADF0
	private void EndDrag()
	{
		if (!this.isDragging)
		{
			return;
		}
		if (this.timeDragging < 0.2f && this.canSwitchMode)
		{
			this.PressSpawnFromSolidClick();
			return;
		}
		this.SimpleEndDrag();
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x0007CC20 File Offset: 0x0007AE20
	private void SimpleEndDrag()
	{
		this.timeDragging = 0f;
		foreach (Overworld_Button overworld_Button in Object.FindObjectsOfType<Overworld_Button>())
		{
			if (overworld_Button != this && overworld_Button.objectToSpawn == this.objectToSpawn)
			{
				Object.Destroy(base.gameObject);
				return;
			}
		}
		Overworld_Button.isDraggingAnything = false;
		this.isDragging = false;
		this.timeDragging = 0f;
		this.layoutElement.ignoreLayout = false;
		base.GetComponent<Button>().interactable = true;
		Overworld_ButtonGroup componentInParent = base.GetComponentInParent<Overworld_ButtonGroup>();
		if (componentInParent)
		{
			componentInParent.Setup();
		}
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x0007CCC0 File Offset: 0x0007AEC0
	public void SpawnBuilding()
	{
		if (!Overworld_ResourceManager.main.HasEnoughResources(this.resourceCosts, 1))
		{
			SoundManager.main.PlaySFX("negative");
			MessageManager.main.CreatePopUp("Not enough resources");
			return;
		}
		SoundManager.main.PlaySFX("menuBlip");
		base.GetComponentInParent<Overworld_ButtonGroup>().DeselectThisGroup();
		Overworld_Manager.main.RememberOverworldButton(this);
		DigitalCursor.main.ClearUIElement();
		if (this.objectToSpawn)
		{
			this.ConvertToBuilding(false);
		}
		if (this.tileToSpawn)
		{
			DigitalCursorInterface componentInParent = base.GetComponentInParent<DigitalCursorInterface>();
			if (componentInParent)
			{
				componentInParent.enabled = false;
				DigitalCursor.main.SetGameWorldDestinationTransform(Overworld_Purse.main.transform);
			}
			Overworld_Manager.main.GetButton(this.tileToSpawn);
		}
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x0007CD8C File Offset: 0x0007AF8C
	public void EnterBuildMode()
	{
		Debug.Log("Enter build mode");
		if (!Overworld_Purse.main.IsFreeToMove())
		{
			return;
		}
		DigitalCursor.main.Show();
		Overworld_BuildingManager.main.ResetBuildingButtons();
		Overworld_Manager.main.SetState(Overworld_Manager.State.NewBuildMode);
		MetaProgressSaveManager.main.CalculatePassiveGenerationRate();
		if (!MetaProgressSaveManager.main.HasMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.destroyedOldShop))
		{
			ArrowTutorialManager.instance.PointArrow(Overworld_BuildingManager.main.destroyButton.GetComponent<RectTransform>());
		}
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x0007CE00 File Offset: 0x0007B000
	public void ExitBuildMode()
	{
		Overworld_Button.EndAllButtonDrags();
		DigitalCursor.main.Hide();
		Overworld_Manager.main.SetState(Overworld_Manager.State.MOVING);
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x0007CE1C File Offset: 0x0007B01C
	public override void OnPressStart(string keyName, bool overrideBool)
	{
		if (keyName == "confirm")
		{
			if (this.tileToSpawn)
			{
				this.SpawnBuilding();
				return;
			}
			this.ControllerClick();
		}
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x0007CE48 File Offset: 0x0007B048
	private void ControllerClick()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.controller)
		{
			return;
		}
		if (!DigitalCursor.main.digitalCursorInterface || !DigitalCursor.main.digitalCursorInterface.enabled || DigitalCursor.main.digitalCursorInterface.IsEnabling())
		{
			return;
		}
		this.SimpleEndDrag();
		this.isBuildingFromSolidClick = true;
		this.ConvertToBuilding(true);
		if (this.tileToSpawn)
		{
			Overworld_Manager.main.GetButton(this.tileToSpawn);
			return;
		}
		Overworld_Manager.main.SetState(Overworld_Manager.State.NewBuildMode);
		Overworld_ButtonGroup.DeselectAllGroups();
		DigitalCursorInterface componentInParent = base.GetComponentInParent<DigitalCursorInterface>();
		if (componentInParent)
		{
			componentInParent.enabled = false;
			DigitalCursor.main.SetGameWorldDestinationTransform(Overworld_Purse.main.transform);
		}
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x0007CF03 File Offset: 0x0007B103
	public void PressSpawn()
	{
		if (this.isDragging)
		{
			return;
		}
		if (!this.objectToSpawn)
		{
			return;
		}
		this.StartDragIfCanAfford();
		if (this.isDragging)
		{
			this.isBuildingFromSolidClick = true;
		}
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x0007CF31 File Offset: 0x0007B131
	public void PressSpawnFromSolidClick()
	{
		if (!this.objectToSpawn)
		{
			return;
		}
		this.StartDragIfCanAfford();
		if (this.isDragging)
		{
			this.isBuildingFromSolidClick = true;
		}
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0007CF56 File Offset: 0x0007B156
	public void InstantSpawn()
	{
		if (this.isDragging)
		{
			return;
		}
		if (this.objectToSpawn)
		{
			this.StartDragIfCanAfford();
			if (this.isDragging)
			{
				this.isBuildingFromSolidClick = false;
			}
		}
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x0007CF83 File Offset: 0x0007B183
	public void OpenInventory()
	{
		if (!Overworld_Purse.main.IsFreeToMove())
		{
			return;
		}
		Overworld_InventoryManager.main.ToggleInventory(Overworld_InventoryManager.ClickAction.NONE);
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0007CFA0 File Offset: 0x0007B1A0
	public void RemovePulse()
	{
		PulseImage componentInParent = base.GetComponentInParent<PulseImage>();
		if (componentInParent)
		{
			Object.Destroy(componentInParent);
		}
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x0007CFC4 File Offset: 0x0007B1C4
	public void DestroyMode()
	{
		if (!Overworld_Purse.main.IsFreeToMove())
		{
			return;
		}
		SoundManager.main.PlaySFX("menuBlip");
		if (DigitalCursor.main.digitalCursorInterface)
		{
			DigitalCursor.main.digitalCursorInterface.enabled = false;
		}
		Overworld_Manager.main.DisablePreview();
		Overworld_Manager.main.DestroyMode();
	}

	// Token: 0x040009A1 RID: 2465
	private static List<Overworld_Button> buttons = new List<Overworld_Button>();

	// Token: 0x040009A2 RID: 2466
	private static bool isDraggingAnything = false;

	// Token: 0x040009A3 RID: 2467
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x040009A4 RID: 2468
	[SerializeField]
	private LayoutElement layoutElement;

	// Token: 0x040009A5 RID: 2469
	[SerializeField]
	private GameObject resourceCounterPrefab;

	// Token: 0x040009A6 RID: 2470
	[SerializeField]
	public GameObject objectToSpawn;

	// Token: 0x040009A7 RID: 2471
	[SerializeField]
	private RuleTile tileToSpawn;

	// Token: 0x040009A8 RID: 2472
	[SerializeField]
	private Image image;

	// Token: 0x040009A9 RID: 2473
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x040009AA RID: 2474
	[SerializeField]
	public List<Overworld_ResourceManager.Resource> resourceCosts = new List<Overworld_ResourceManager.Resource>();

	// Token: 0x040009AB RID: 2475
	public bool isBuildingFromSolidClick;

	// Token: 0x040009AC RID: 2476
	public Transform parentInMenu;

	// Token: 0x040009AD RID: 2477
	public Overworld_ButtonGroup parentButtonGroup;

	// Token: 0x040009AE RID: 2478
	public bool isInGroup;

	// Token: 0x040009AF RID: 2479
	public bool isDragging;

	// Token: 0x040009B0 RID: 2480
	private float timeToConvert;

	// Token: 0x040009B1 RID: 2481
	private float timeToConvertMax = 0.06f;

	// Token: 0x040009B2 RID: 2482
	public bool canSwitchMode = true;

	// Token: 0x040009B3 RID: 2483
	private Overworld_Structure structure;

	// Token: 0x040009B4 RID: 2484
	private GameObject previewCard;

	// Token: 0x040009B5 RID: 2485
	private float timeDragging;

	// Token: 0x040009B6 RID: 2486
	private float timeHovering;

	// Token: 0x040009B7 RID: 2487
	private float existedFor;

	// Token: 0x040009B8 RID: 2488
	private bool canConvertBack;

	// Token: 0x040009B9 RID: 2489
	private RectTransform verticalLayoutGroup;
}
