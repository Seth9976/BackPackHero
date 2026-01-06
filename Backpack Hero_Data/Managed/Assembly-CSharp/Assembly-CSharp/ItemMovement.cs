using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020000E6 RID: 230
public class ItemMovement : CustomInputHandler
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060007BA RID: 1978 RVA: 0x0004E863 File Offset: 0x0004CA63
	// (set) Token: 0x060007BB RID: 1979 RVA: 0x0004E86B File Offset: 0x0004CA6B
	public float timeViewingCard { get; private set; }

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060007BC RID: 1980 RVA: 0x0004E874 File Offset: 0x0004CA74
	// (set) Token: 0x060007BD RID: 1981 RVA: 0x0004E87C File Offset: 0x0004CA7C
	public GameObject previewCard { get; private set; }

	// Token: 0x060007BE RID: 1982 RVA: 0x0004E888 File Offset: 0x0004CA88
	private void OnEnable()
	{
		if (!ItemMovement.allItems.Contains(this))
		{
			ItemMovement.allItems.Add(this);
		}
		this.ConsiderChangingShader();
		this.isMovingItem = false;
		this.isTransiting = false;
		this.isAnimating = false;
		this.isHovering = false;
		this.isDragging = false;
		this.isStoppingDrag = false;
		this.wasRotated = false;
		this.isShowingHighlights = false;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0004E8EB File Offset: 0x0004CAEB
	private void OnDestroy()
	{
		ItemMovement.allItems.Remove(this);
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0004E8FC File Offset: 0x0004CAFC
	public void Start()
	{
		if (CR8Manager.instance && CR8Manager.instance.isTesting)
		{
			return;
		}
		this.gridObject = null;
		this.MakeReferences();
		this.bounceTime = Random.Range(0f, 1f);
		this.SetupAfterSavingAndLoading();
		this.SetupPositionAndParent();
		if (this.myItem && this.myItem.itemType.Contains(Item2.ItemType.Treat))
		{
			TutorialManager.main.ConsiderTutorial("PetTreats");
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0004E980 File Offset: 0x0004CB80
	private void MakeReferences()
	{
		this.saveManager = SaveManager.GetSaveManager();
		if (!this.player)
		{
			this.player = Player.main;
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0004E9A8 File Offset: 0x0004CBA8
	private void Update()
	{
		if (this.logPosition)
		{
			Debug.Log(base.name + " position: " + base.transform.position.ToString());
		}
		if (!this.myItem)
		{
			this.myItem = base.GetComponent<Item2>();
			if (!this.myItem)
			{
				return;
			}
		}
		if (!this.started && this.myItem.itemType.Contains(Item2.ItemType.Curse) && this.moveToItemTransform)
		{
			TutorialManager.main.ConsiderTutorial("curseUpdate");
			MetaProgressSaveManager.main.AddRunEvent(MetaProgressSaveManager.LastRun.RunEvents.gotACurse);
		}
		if (!this.started && this.myItem && this.myItem.itemType.Contains(Item2.ItemType.Blessing) && this.moveToItemTransform)
		{
			TutorialManager.main.ConsiderTutorial("blessings");
			MetaProgressSaveManager.main.AddRunEvent(MetaProgressSaveManager.LastRun.RunEvents.gotABlessing);
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.hasBeenBlessed);
		}
		this.started = true;
		if (this.gameFlowManager && this.gameFlowManager.selectedItem == this.myItem)
		{
			this.HoveringAnimation();
		}
		if ((!this.isDragging || this.isStoppingDrag) && this.gameManager && this.gameManager.draggingItem == base.gameObject)
		{
			this.gameManager.draggingItem = null;
		}
		if (this.previewCard)
		{
			this.timeViewingCard += Time.deltaTime;
			if (this.timeViewingCard > 0.1f && !this.updatedControls)
			{
				this.updatedControls = true;
				DigitalCursor.main.UpdateContextControls();
			}
		}
		else
		{
			this.updatedControls = false;
			this.timeViewingCard = 0f;
		}
		if (this.animator)
		{
			this.animator.SetBool("hovering", this.isHovering);
		}
		this.isHovering = false;
		if (this.myItem && this.myItem.destroyed)
		{
			if (this.spriteRenderer)
			{
				this.spriteRenderer.enabled = false;
			}
			if (this.mousePreviewRenderer)
			{
				this.mousePreviewRenderer.enabled = false;
			}
			if (this.curseSymbol)
			{
				this.curseSymbol.enabled = false;
			}
			if (this.mouseCurseSymbol)
			{
				this.mouseCurseSymbol.enabled = false;
			}
			if (this.animator)
			{
				this.animator.enabled = false;
			}
			this.RemoveCard();
			this.isDragging = false;
		}
		if (this.curseSymbol)
		{
			if (this.spriteRenderer.enabled)
			{
				this.curseSymbol.enabled = true;
				this.curseSymbol.color = this.spriteRenderer.color;
				this.curseSymbol.sortingOrder = this.spriteRenderer.sortingOrder;
			}
			else
			{
				this.curseSymbol.enabled = false;
			}
		}
		if (this.mouseCurseSymbol)
		{
			if (this.mousePreviewRenderer.enabled)
			{
				this.mouseCurseSymbol.enabled = true;
				this.mouseCurseSymbol.color = this.mousePreviewRenderer.color;
				this.mouseCurseSymbol.sortingOrder = this.mousePreviewRenderer.sortingOrder;
				this.mouseCurseSymbol.sprite = this.curseSymbol.sprite;
			}
			else
			{
				this.mouseCurseSymbol.enabled = false;
			}
		}
		if (this.isTransiting)
		{
			this.RemoveCard();
			this.isDragging = false;
			return;
		}
		bool flag = false;
		if (this.wasRotated)
		{
			flag = true;
			this.wasRotated = false;
		}
		if (this.isStoppingDrag)
		{
			if (this.isStoppingDragTimer == -1f)
			{
				this.isStoppingDragTimer = 0.15f;
			}
			else
			{
				this.isStoppingDragTimer -= Time.deltaTime;
				if (this.isStoppingDragTimer < 0f)
				{
					this.isStoppingDrag = false;
				}
			}
		}
		else
		{
			this.isStoppingDragTimer = -1f;
		}
		if (this.isDragging && !this.isStoppingDrag)
		{
			this.SetupParent();
			Vector2 vector;
			this.MoveToMouse(out flag, out vector);
			if (this.spriteRenderer)
			{
				this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 0.6f);
			}
			if (!flag)
			{
				this.timeOnThisSpace += Time.deltaTime;
				if (this.timeOnThisSpace > 0.3f && !this.considererPouchHere)
				{
					this.considererPouchHere = true;
					foreach (GameObject gameObject in PathFinding.GetObjectsAtVector(DigitalCursor.main.transform.position))
					{
						if (!(gameObject == base.gameObject))
						{
							ItemPouch componentInParent = gameObject.GetComponentInParent<ItemPouch>();
							if (componentInParent && componentInParent.gameObject != base.gameObject && !ItemPouch.AnyPouchesHovered() && this.gameManager.inventoryPhase != GameManager.InventoryPhase.inCombatMove)
							{
								componentInParent.OpenPouch();
								break;
							}
						}
					}
				}
			}
			if (flag)
			{
				this.everMovedDuringThisDrag = true;
				ItemMovement.RemoveAllHighlights();
				if (this.myItem && this.myItem.moveArea != Item2.Area.board)
				{
					this.ShowSquaresForMovement(this.myItem.moveArea);
				}
				this.timeOnThisSpace = 0f;
				this.considererPouchHere = false;
				if (this.gameManager.draggingCard)
				{
					this.gameManager.draggingCard.GetComponentInChildren<Card>().GetDescriptions(this.myItem, base.gameObject, false);
				}
				List<GameObject> list = new List<GameObject>();
				List<GameObject> list2 = new List<GameObject>();
				this.TestAtPosition(base.transform.position, out list, out list2, 1f, false, true, false);
				this.inPouch = false;
				this.myItem.parentInventoryGrid = null;
				this.myItem.lastParentInventoryGrid = null;
				foreach (GameObject gameObject2 in list)
				{
					if (gameObject2.GetComponent<GridSquare>().isPouch)
					{
						this.wasInPouch = true;
						this.inPouch = true;
					}
					this.myItem.parentInventoryGrid = gameObject2.transform.parent;
					this.myItem.lastParentInventoryGrid = gameObject2.transform.parent;
				}
				bool flag2 = false;
				if (list2.Count > 0)
				{
					flag2 = true;
				}
				if (list.Count == 0)
				{
					this.spriteRenderer.enabled = false;
				}
				else
				{
					this.spriteRenderer.enabled = true;
					if (!flag2 && list.Count == this.GetSpacesNeeded())
					{
						this.spriteRenderer.color = new Color(1f, 1f, 1f, 0.6f);
						this.AddHighlights();
					}
					else
					{
						this.spriteRenderer.color = new Color(1f, 0f, 0f, 0.6f);
						if ((this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems) && this.gameManager.inventoryPhase == GameManager.InventoryPhase.inCombatMove) || this.myItem.itemType.Contains(Item2.ItemType.Carving) || this.myItem.itemType.Contains(Item2.ItemType.Curse))
						{
							this.AddHighlights();
						}
					}
				}
			}
			if ((!Input.GetMouseButton(0) && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor && !Singleton.Instance.clickOnceToPickupAndAgainToDrop) || (Input.GetMouseButtonDown(0) && this.dragginForTime > 0.1f && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor && Singleton.Instance.clickOnceToPickupAndAgainToDrop) || (DigitalCursor.main.GetInputDown("confirm") && this.dragginForTime > 0.1f))
			{
				base.StartCoroutine(this.StopDrag());
			}
			else if (DigitalCursor.main.GetInputDown("cancel") && GameManager.main.inventoryPhase == GameManager.InventoryPhase.inCombatMove)
			{
				base.transform.position = this.startPosition;
				base.transform.rotation = this.startRotation;
				base.StartCoroutine(this.StopDrag());
			}
			else if (DigitalCursor.main.GetInputDown("cancel"))
			{
				this.isDragging = false;
				this.gameManager.draggingItem = null;
				this.isStoppingDrag = false;
				this.moveOutOfGridRoutine = base.StartCoroutine(this.Move(base.transform.position, 0));
				Item2.SetAllItemColors();
				ItemMovement.RemoveAllHighlights();
				this.myItem.SetColor();
				ContextControlsDisplay.main.ClearAllControls();
				DigitalCursor.main.UpdateContextControls();
			}
			this.dragginForTime += Time.deltaTime;
			return;
		}
		this.dragginForTime = 0f;
		if (this.myItem && this.spriteRenderer && !this.isDragging && !this.myItem.destroyed && !this.isAnimating && !this.isHovering && !this.isStoppingDrag && !this.isMovingItem && !this.isShowingHighlights && !this.isTransiting)
		{
			this.spriteRenderer.enabled = true;
		}
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0004F2E4 File Offset: 0x0004D4E4
	private bool BlockHere(Vector2 pos, Transform parent)
	{
		foreach (object obj in parent)
		{
			Transform transform = (Transform)obj;
			if (Mathf.Abs(transform.localPosition.x - pos.x) < 0.3f && Mathf.Abs(transform.localPosition.y - pos.y) < 0.3f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0004F374 File Offset: 0x0004D574
	public int ChooseSprite(Transform t, Transform parent)
	{
		bool flag = this.BlockHere(t.localPosition + Vector3.left, parent);
		bool flag2 = this.BlockHere(t.localPosition + Vector3.right, parent);
		bool flag3 = this.BlockHere(t.localPosition + Vector3.up, parent);
		bool flag4 = this.BlockHere(t.localPosition + Vector3.down, parent);
		return ItemMovement.GetTileNumber(flag3, flag4, flag, flag2);
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0004F404 File Offset: 0x0004D604
	private static int GetTileNumber(bool up, bool down, bool left, bool right)
	{
		int num = 15;
		if (up && down && left && right)
		{
			num = 14;
		}
		else if (up && left && right)
		{
			num = 13;
		}
		else if (up && down && left)
		{
			num = 12;
		}
		else if (down && left && right)
		{
			num = 11;
		}
		else if (up && down && right)
		{
			num = 10;
		}
		else if (left && up)
		{
			num = 9;
		}
		else if (left && down)
		{
			num = 8;
		}
		else if (right && down)
		{
			num = 7;
		}
		else if (right && up)
		{
			num = 6;
		}
		else if (right && left)
		{
			num = 5;
		}
		else if (up && down)
		{
			num = 4;
		}
		else if (left)
		{
			num = 3;
		}
		else if (right)
		{
			num = 2;
		}
		else if (down)
		{
			num = 1;
		}
		else if (up)
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0004F4A8 File Offset: 0x0004D6A8
	public void SetCharges(int amount)
	{
		if (this.chargesParent == null)
		{
			return;
		}
		int num = 0;
		foreach (object obj in this.chargesParent)
		{
			Transform transform = (Transform)obj;
			num++;
			if (amount < num)
			{
				transform.GetComponent<SpriteRenderer>().sprite = this.chargesSprites[0];
			}
			else
			{
				transform.GetComponent<SpriteRenderer>().sprite = this.chargesSprites[1];
			}
		}
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x0004F53C File Offset: 0x0004D73C
	private void SetSize()
	{
		float num = 999f;
		float num2 = -999f;
		float num3 = 999f;
		float num4 = -999f;
		foreach (BoxCollider2D boxCollider2D in this.boxCollider2Ds)
		{
			if (boxCollider2D.offset.x - boxCollider2D.size.x / 2f < num)
			{
				num = boxCollider2D.offset.x - boxCollider2D.size.x / 2f;
			}
			if (boxCollider2D.offset.x + boxCollider2D.size.x / 2f > num2)
			{
				num2 = boxCollider2D.offset.x + boxCollider2D.size.x / 2f;
			}
			if (boxCollider2D.offset.y - boxCollider2D.size.y / 2f < num3)
			{
				num3 = boxCollider2D.offset.y - boxCollider2D.size.y / 2f;
			}
			if (boxCollider2D.offset.y + boxCollider2D.size.y / 2f > num4)
			{
				num4 = boxCollider2D.offset.y + boxCollider2D.size.y / 2f;
			}
		}
		this.size = new Vector2(num2 + Mathf.Abs(num), num4 + Mathf.Abs(num3));
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x0004F6AC File Offset: 0x0004D8AC
	public void SetupParent()
	{
		if (!base.transform.parent || !base.transform.parent.CompareTag("ItemParent"))
		{
			Transform itemsParent = GameManager.main.itemsParent;
			if (this.moveToItemTransform)
			{
				base.transform.SetParent(itemsParent);
				return;
			}
			this.canvasGroup = base.GetComponentInParent<CanvasGroup>();
		}
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0004F710 File Offset: 0x0004D910
	public void SetupPositionAndParent()
	{
		this.SetupParent();
		Transform itemsParent = GameManager.main.itemsParent;
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 0f);
		if (this.myItem && (this.myItem.itemType.Contains(Item2.ItemType.Hazard) || this.myItem.itemType.Contains(Item2.ItemType.Blessing) || this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems)))
		{
			base.transform.localPosition += Vector3.back * 0.3f;
		}
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x0004F7CC File Offset: 0x0004D9CC
	public void SetupParticles()
	{
		if (!this.spriteRenderer || !this.spriteRenderer.sprite || !this.spriteRenderer.sprite.texture)
		{
			return;
		}
		this.mousePreview.gameObject.SetActive(true);
		if (this.mousePreview.childCount > 0)
		{
			this.mousePreview.transform.GetChild(0).gameObject.SetActive(true);
		}
		this.mouseParticleSystem = this.mousePreview.GetComponentInChildren<ParticleSystem>().transform;
		ParticleSystem.ShapeModule shape = this.mouseParticleSystem.GetComponent<ParticleSystem>().shape;
		shape.shapeType = ParticleSystemShapeType.Sprite;
		shape.sprite = this.spriteRenderer.sprite;
		shape.texture = this.spriteRenderer.sprite.texture;
		if (this.myParticles)
		{
			ParticleSystem.ShapeModule shape2 = this.myParticles.GetComponent<ParticleSystem>().shape;
			shape2.sprite = this.spriteRenderer.sprite;
			shape2.texture = this.spriteRenderer.sprite.texture;
			if (this.myItem.destroyed)
			{
				shape2.enabled = false;
			}
		}
		ParticleSystem.MainModule main = this.mouseParticleSystem.GetComponent<ParticleSystem>().main;
		this.mousePreview.gameObject.SetActive(false);
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0004F924 File Offset: 0x0004DB24
	public static void ConsiderChangingAllShaders()
	{
		foreach (ItemMovement itemMovement in ItemMovement.allItems)
		{
			itemMovement.ConsiderChangingShader();
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x0004F974 File Offset: 0x0004DB74
	private void ConsiderChangingShader()
	{
		if (CR8Manager.instance && CR8Manager.instance.isTesting)
		{
			return;
		}
		if (!ItemShaderManager.main)
		{
			return;
		}
		if (!this.myItem)
		{
			return;
		}
		if (this.myItem.itemType.Contains(Item2.ItemType.Carving) && Singleton.Instance.showOutlineOnCarvings)
		{
			ItemShaderManager.SetMaterial(base.gameObject, ItemShaderManager.MaterialType.defaultCarving);
			if (base.GetComponent<ModdedItem>() != null)
			{
				SpriteRenderer[] array = base.GetComponentsInChildren<SpriteRenderer>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].material.SetFloat("_Thick", ItemShaderManager.main.defaultCarvingMaterial.GetFloat("_Thick") * 14f);
				}
				return;
			}
		}
		else if (this.myItem.itemType.Contains(Item2.ItemType.Treat) && Singleton.Instance.showOutlineOnCarvings)
		{
			ItemShaderManager.SetMaterial(base.gameObject, ItemShaderManager.MaterialType.defaultTreat);
			if (base.GetComponent<ModdedItem>() != null)
			{
				SpriteRenderer[] array = base.GetComponentsInChildren<SpriteRenderer>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].material.SetFloat("_Thick", ItemShaderManager.main.defaultTreatMaterial.GetFloat("_Thick") * 14f);
				}
				return;
			}
		}
		else if (this.myItem.itemType.Contains(Item2.ItemType.Blessing) && Singleton.Instance.showOutlineOnCarvings)
		{
			ItemShaderManager.SetMaterial(base.gameObject, ItemShaderManager.MaterialType.defaultBlessing);
			if (base.GetComponent<ModdedItem>() != null)
			{
				SpriteRenderer[] array = base.GetComponentsInChildren<SpriteRenderer>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].material.SetFloat("_Thick", ItemShaderManager.main.defaultTreatMaterial.GetFloat("_Thick") * 14f);
				}
				return;
			}
		}
		else
		{
			ItemShaderManager.SetMaterial(base.gameObject, ItemShaderManager.MaterialType.defaultItem);
		}
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x0004FB3C File Offset: 0x0004DD3C
	public void SetupAfterSavingAndLoading()
	{
		if (CR8Manager.instance && CR8Manager.instance.isTesting)
		{
			return;
		}
		this.saveManager = SaveManager.GetSaveManager();
		if (!this.seenBefore && PlayerStatTracking.main)
		{
			this.seenBefore = true;
			PlayerStatTracking.main.AddStat("Items seen", 1);
		}
		Animator animator = base.GetComponent<Animator>();
		if (!animator)
		{
			animator = base.gameObject.AddComponent<Animator>();
		}
		animator.runtimeAnimatorController = this.saveManager.itemController;
		if (!base.GetComponent<global::AnimationEvent>())
		{
			base.gameObject.AddComponent<global::AnimationEvent>();
		}
		this.animator = base.GetComponent<Animator>();
		this.itemsReplacedByThisCurse = new List<ItemMovement>();
		this.gameFlowManager = GameFlowManager.main;
		this.levelUpManager = LevelUpManager.main;
		this.myItem = base.GetComponent<Item2>();
		this.gameManager = GameManager.main;
		this.itemMovementManager = ItemMovementManager.main;
		Object component = base.GetComponent<Rigidbody2D>();
		this.ConsiderChangingShader();
		if (!component)
		{
			base.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		}
		if (this.mousePreview)
		{
			Object.Destroy(this.mousePreview.gameObject);
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.saveManager.mousePreviewToCopy.gameObject, base.transform.position, base.transform.rotation, base.transform);
		gameObject.SetActive(true);
		gameObject.transform.SetAsFirstSibling();
		this.mousePreview = gameObject.transform;
		this.mousePreview.name = "MousePreview";
		this.animator.Rebind();
		if (this.myParticles)
		{
			if (this.myItem.itemType.Contains(Item2.ItemType.Curse))
			{
				Object.Destroy(this.myParticles);
				this.myParticles = Object.Instantiate<GameObject>(this.saveManager.curseParticles.gameObject, base.transform.position, base.transform.rotation, base.transform);
				Follow component2 = this.myParticles.GetComponent<Follow>();
				if (component2)
				{
					component2.follow = this.mousePreview;
				}
				this.myParticles.SetActive(true);
				ParticleSystem component3 = this.myParticles.GetComponent<ParticleSystem>();
				if (component3)
				{
					ParticleSystem.MainModule main = component3.main;
					GameObject gameObject2 = GameObject.FindGameObjectWithTag("Inventory");
					if (gameObject2)
					{
						main.customSimulationSpace = gameObject2.transform;
					}
				}
			}
			else if (this.myItem.itemType.Contains(Item2.ItemType.Blessing))
			{
				Object.Destroy(this.myParticles);
				this.myParticles = Object.Instantiate<GameObject>(this.saveManager.blessingParticles.gameObject, base.transform.position, base.transform.rotation, base.transform);
				Follow component4 = this.myParticles.GetComponent<Follow>();
				if (component4)
				{
					component4.follow = this.mousePreview;
				}
				this.myParticles.SetActive(true);
				ParticleSystem component5 = this.myParticles.GetComponent<ParticleSystem>();
				if (component5)
				{
					ParticleSystem.MainModule main2 = component5.main;
					GameObject gameObject3 = GameObject.FindGameObjectWithTag("Inventory");
					if (gameObject3)
					{
						main2.customSimulationSpace = gameObject3.transform;
					}
				}
			}
			else if (this.myItem.itemType.Contains(Item2.ItemType.Relic) || this.myItem.itemType.Contains(Item2.ItemType.Fragment))
			{
				Object.Destroy(this.myParticles);
				this.myParticles = Object.Instantiate<GameObject>(this.saveManager.relicParticles.gameObject, base.transform.position, base.transform.rotation, base.transform);
				Follow component6 = this.myParticles.GetComponent<Follow>();
				if (component6)
				{
					component6.follow = this.mousePreview;
				}
				this.myParticles.SetActive(true);
				ParticleSystem component7 = this.myParticles.GetComponent<ParticleSystem>();
				if (component7)
				{
					ParticleSystem.MainModule main3 = component7.main;
					GameObject gameObject4 = GameObject.FindGameObjectWithTag("Inventory");
					if (gameObject4)
					{
						main3.customSimulationSpace = gameObject4.transform;
					}
				}
			}
		}
		this.mousePreviewRenderer = this.mousePreview.GetComponent<SpriteRenderer>();
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.boxCollider2Ds = base.GetComponentsInChildren<BoxCollider2D>();
		foreach (BoxCollider2D boxCollider2D in this.boxCollider2Ds)
		{
			boxCollider2D.size = new Vector2(Mathf.Max(Mathf.Round(boxCollider2D.size.x), 1f), Mathf.Max(Mathf.Round(boxCollider2D.size.y), 1f));
		}
		this.SetSize();
		this.SetupParticles();
		if (this.inGrid)
		{
			this.CreateBorder();
		}
		this.mousePreview.transform.localScale = Vector3.one;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00050028 File Offset: 0x0004E228
	public void SpawnParticles()
	{
		if (!this.spriteRenderer)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.itemCreationParticlesPrefab, base.transform.position + Vector3.back, base.transform.rotation);
		gameObject.GetComponent<Follow>().follow = this.mousePreview.transform;
		ParticleSystem.ShapeModule shape = gameObject.GetComponent<ParticleSystem>().shape;
		shape.sprite = this.spriteRenderer.sprite;
		shape.texture = this.spriteRenderer.sprite.texture;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x000500B8 File Offset: 0x0004E2B8
	public void MoveOut(Vector2 position)
	{
		this.moveOutOfGridRoutine = base.StartCoroutine(this.Move(position, 0));
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x000500CE File Offset: 0x0004E2CE
	public IEnumerator Move(Vector2 position, int moves = 0)
	{
		if (!this.myItem || this.myItem.destroyed)
		{
			yield break;
		}
		this.isTransiting = true;
		yield return new WaitForFixedUpdate();
		this.mousePreview.gameObject.SetActive(true);
		this.mousePreviewRenderer.sortingOrder = this.spriteRenderer.sortingOrder;
		this.spriteRenderer.enabled = false;
		Vector3 position2 = base.transform.position;
		if (!this.returnsToOutOfInventoryPosition)
		{
			this.LookForSpace(new Vector3(position.x, -5f, -1f), 0);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -1f);
		}
		else
		{
			base.transform.localPosition = this.outOfInventoryPosition;
		}
		this.mousePreviewRenderer.sortingOrder = this.spriteRenderer.sortingOrder;
		this.spriteRenderer.enabled = false;
		this.spriteRenderer.color = Color.white;
		this.mousePreview.position = position2;
		yield return this.MoveOverTime(16f);
		yield return new WaitForFixedUpdate();
		List<GameObject> list;
		List<GameObject> list2;
		this.TestAtPosition(base.transform.position, out list, out list2, 1f, true, true, false);
		if (list2.Count > 0 && moves < 1)
		{
			int num = moves;
			moves = num + 1;
			yield return this.Move(base.transform.position, moves);
		}
		this.isTransiting = false;
		yield break;
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x000500EB File Offset: 0x0004E2EB
	public IEnumerator Move(Vector2 position, Quaternion rotation, int moves = 0)
	{
		if (!this.myItem || this.myItem.destroyed)
		{
			yield break;
		}
		this.isTransiting = true;
		yield return new WaitForFixedUpdate();
		this.mousePreview.gameObject.SetActive(true);
		this.mousePreviewRenderer.sortingOrder = this.spriteRenderer.sortingOrder;
		this.spriteRenderer.enabled = false;
		Vector3 position2 = base.transform.position;
		Quaternion rotation2 = base.transform.rotation;
		if (!this.returnsToOutOfInventoryPosition)
		{
			this.LookForSpace(new Vector3(position.x, -5f, -1f), 0);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -1f);
		}
		else
		{
			base.transform.localPosition = this.outOfInventoryPosition;
			base.transform.rotation = this.outOfInventoryRotation;
		}
		this.mousePreview.position = position2;
		this.mousePreview.rotation = rotation2;
		yield return this.MoveOverTime(16f);
		yield return new WaitForFixedUpdate();
		List<GameObject> list;
		List<GameObject> list2;
		this.TestAtPosition(base.transform.position, out list, out list2, 1f, true, true, false);
		if (list2.Count > 0 && moves < 1)
		{
			int num = moves;
			moves = num + 1;
			yield return this.Move(base.transform.position, moves);
		}
		this.isTransiting = false;
		yield break;
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x00050108 File Offset: 0x0004E308
	private Vector2 LimitPosition(Vector2 pos)
	{
		Vector2 vector = this.gameManager.upperLeft.transform.position;
		Vector2 vector2 = this.size * 0.9f;
		if (base.transform.rotation.eulerAngles.z == 90f || base.transform.rotation.eulerAngles.z == -90f || base.transform.rotation.eulerAngles.z == 270f)
		{
			vector2 = new Vector2(vector2.y, vector2.x);
		}
		float num = Mathf.Clamp(pos.x, vector.x + vector2.x / 2f + base.transform.parent.position.x, vector.x * -1f - vector2.x / 2f + base.transform.parent.position.x);
		float num2 = Mathf.Clamp(pos.y, -0.5f + vector2.y / 2f + base.transform.parent.position.y - 7.65f, vector.y - vector2.y / 2f + base.transform.parent.position.y - 2.3f);
		return new Vector2(num, num2);
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x00050284 File Offset: 0x0004E484
	public void Rotate(float angle)
	{
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		if (!this.inGrid && (!this.gameManager.reorgnizeItem || this.gameManager.reorgnizeItem != base.gameObject) && (!this.gameManager.draggingItem || this.gameManager.draggingItem == base.gameObject))
		{
			Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (this.myItem && !this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.cannotBeRotated))
			{
				this.wasRotated = true;
				if (this.gameManager.draggingItem == base.gameObject)
				{
					base.transform.RotateAround(vector, Vector3.forward, angle);
					return;
				}
				base.transform.Rotate(new Vector3(0f, 0f, angle));
			}
		}
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00050394 File Offset: 0x0004E594
	public void MoveToMouse(out bool wasMoved, out Vector2 roundedPos)
	{
		wasMoved = false;
		Vector2 vector = DigitalCursor.main.transform.position + this.RoundPosition(this.difference);
		vector = this.LimitPosition(vector);
		Vector2 vector2 = vector;
		vector = base.transform.parent.InverseTransformPoint(vector);
		roundedPos = this.RoundPosition(vector);
		if (roundedPos.x != base.transform.localPosition.x || roundedPos.y != base.transform.localPosition.y)
		{
			wasMoved = true;
		}
		base.transform.localPosition = new Vector3(roundedPos.x, roundedPos.y, -4.5f);
		this.mousePreview.position = new Vector3(vector2.x, vector2.y, this.mousePreview.position.z);
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x0005047D File Offset: 0x0004E67D
	public static void CopyBounceTime(ItemMovement from, ItemMovement to)
	{
		to.bounceTime = from.bounceTime;
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x0005048C File Offset: 0x0004E68C
	private void LateUpdate()
	{
		if (this.canvasGroup)
		{
			this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, this.canvasGroup.alpha);
		}
		if ((!this.myItem || this.myItem.destroyed) && this.mousePreview)
		{
			this.mousePreview.gameObject.SetActive(false);
		}
		if (this.pauseTime > 0f)
		{
			this.pauseTime -= Time.deltaTime;
			return;
		}
		if (!this.isDragging && !this.inGrid && !this.isStoppingDrag && !this.isTransiting)
		{
			if (this.returnsToOutOfInventoryPosition)
			{
				this.spriteRenderer.sortingOrder = 0;
				this.bounceTime += Time.deltaTime / 3f;
				if (this.bounceTime > 1f)
				{
					this.bounceTime = 0f;
				}
				Vector3 vector = this.outOfInventoryPosition + Vector3.up * Mathf.Abs(0.5f - this.bounceTime) * 0.25f;
				base.transform.localPosition = vector;
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -2f);
				return;
			}
			if (this.parentAltar)
			{
				this.spriteRenderer.color = Color.white;
				base.transform.position = this.parentAltar.transform.position + Vector3.back;
				return;
			}
		}
		else if (this.isDragging && this.mousePreviewRenderer)
		{
			this.animator.enabled = false;
			this.mousePreviewRenderer.transform.localScale = Vector3.one;
			this.mousePreviewRenderer.enabled = true;
			this.mousePreviewRenderer.gameObject.SetActive(true);
			this.mousePreviewRenderer.color = Color.white;
			this.mousePreviewRenderer.transform.localPosition = new Vector3(this.mousePreviewRenderer.transform.localPosition.x, this.mousePreviewRenderer.transform.localPosition.y, -1f);
		}
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00050722 File Offset: 0x0004E922
	public IEnumerator MoveOverTime(float speed)
	{
		if (!this.myItem || this.myItem.destroyed)
		{
			yield break;
		}
		ItemPouch component = base.GetComponent<ItemPouch>();
		if (component)
		{
			component.ClosePouch();
		}
		this.isMovingItem = true;
		this.mousePreview.gameObject.SetActive(true);
		SpriteRenderer component2 = this.mousePreview.GetComponent<SpriteRenderer>();
		component2.sprite = this.spriteRenderer.sprite;
		component2.color = new Color(1f, 1f, 1f, 1f);
		Quaternion startingRotation = this.mousePreview.localRotation;
		this.mousePreview.localPosition = new Vector3(this.mousePreview.localPosition.x, this.mousePreview.localPosition.y, -3f);
		float startingDistance = Vector2.Distance(this.mousePreview.localPosition, Vector3.zero);
		float t = 0f;
		while (Vector2.Distance(this.mousePreview.localPosition, Vector3.zero) > 0.1f)
		{
			t += Time.deltaTime;
			this.mousePreview.localPosition = Vector3.MoveTowards(this.mousePreview.localPosition, new Vector3(0f, 0f, -2f), speed * Time.deltaTime);
			this.mousePreview.localRotation = Quaternion.Lerp(Quaternion.identity, startingRotation, Vector2.Distance(this.mousePreview.localPosition, Vector3.zero) / startingDistance);
			yield return null;
		}
		this.mousePreview.localPosition = Vector3.zero;
		this.mousePreview.localRotation = Quaternion.identity;
		this.mousePreview.gameObject.SetActive(false);
		this.spriteRenderer.enabled = true;
		this.spriteRenderer.color = Color.white;
		this.isMovingItem = false;
		yield break;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x00050738 File Offset: 0x0004E938
	public IEnumerator RotateOverTime(float time)
	{
		if (!this.myItem || this.myItem.destroyed)
		{
			yield break;
		}
		this.isMovingItem = true;
		this.mousePreview.gameObject.SetActive(true);
		SpriteRenderer component = this.mousePreview.GetComponent<SpriteRenderer>();
		component.sprite = this.spriteRenderer.sprite;
		component.color = new Color(1f, 1f, 1f, 1f);
		this.mousePreview.localPosition = Vector3.zero;
		Quaternion startingRotation = this.mousePreview.localRotation;
		float t = 0f;
		while (t < time)
		{
			t += Time.deltaTime;
			this.mousePreview.localRotation = Quaternion.Lerp(startingRotation, Quaternion.identity, t / time);
			yield return null;
		}
		this.mousePreview.localPosition = Vector3.zero;
		this.mousePreview.localRotation = Quaternion.identity;
		this.mousePreview.gameObject.SetActive(false);
		this.spriteRenderer.enabled = true;
		this.isMovingItem = false;
		yield break;
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00050750 File Offset: 0x0004E950
	public Vector2 RoundPosition(Vector2 newPos)
	{
		base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Round(base.transform.rotation.eulerAngles.z / 90f) * 90f);
		float num;
		float num2;
		if (base.transform.rotation.eulerAngles.z != 90f && base.transform.rotation.eulerAngles.z != -90f && base.transform.rotation.eulerAngles.z != 270f)
		{
			if (Mathf.Round(this.size.y) % 2f == 0f)
			{
				num = Mathf.Round(newPos.y - 0.5f) + 0.5f;
			}
			else
			{
				num = Mathf.Round(newPos.y);
			}
			if (Mathf.Round(this.size.x) % 2f == 0f)
			{
				num2 = Mathf.Round(newPos.x - 0.5f) + 0.5f;
			}
			else
			{
				num2 = Mathf.Round(newPos.x);
			}
		}
		else
		{
			if (Mathf.Round(this.size.x) % 2f == 0f)
			{
				num = Mathf.Round(newPos.y - 0.5f) + 0.5f;
			}
			else
			{
				num = Mathf.Round(newPos.y);
			}
			if (Mathf.Round(this.size.y) % 2f == 0f)
			{
				num2 = Mathf.Round(newPos.x - 0.5f) + 0.5f;
			}
			else
			{
				num2 = Mathf.Round(newPos.x);
			}
		}
		return new Vector2(num2, num);
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0005092D File Offset: 0x0004EB2D
	public bool TestForHazard()
	{
		return false;
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00050930 File Offset: 0x0004EB30
	public void TestAtPosition(Vector2 position, out List<GameObject> grids, out List<GameObject> items, float scale = 1f, bool findOutOfGrid = false, bool considerPouches = true, bool ignorePouchSpaces = false)
	{
		this.GetGridObject();
		this.boxCollider2Ds = base.GetComponentsInChildren<BoxCollider2D>();
		grids = new List<GameObject>();
		items = new List<GameObject>();
		foreach (GridObject gridObject in GridObject.GetItemsAtPosition(this.gridObject.GetGridPositionsAtPosition(position)))
		{
			if (!(this.gridObject == gridObject))
			{
				if (gridObject.type == GridObject.Type.item)
				{
					Item2 component = gridObject.GetComponent<Item2>();
					if (component)
					{
						items.Add(component.gameObject);
					}
				}
				else if (gridObject.type == GridObject.Type.grid)
				{
					GridObject component2 = gridObject.GetComponent<GridObject>();
					GridSquare component3 = gridObject.GetComponent<GridSquare>();
					if (component2 && (considerPouches || !component3 || !component3.isPouch))
					{
						grids.Add(component2.gameObject);
					}
				}
			}
		}
		if (considerPouches)
		{
			bool flag = false;
			grids = GridSquare.ConsiderIfHoveringInPouch(grids, out flag);
			if (flag)
			{
				items = ItemPouch.FindItemsInPouches(items);
			}
		}
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00050A40 File Offset: 0x0004EC40
	public void TestAtPositionOutOfGrid(Vector2 position, out List<GameObject> grids, out List<GameObject> items, float scale = 1f, bool findOutOfGrid = false, bool considerPouches = true, bool ignorePouchSpaces = false)
	{
		this.boxCollider2Ds = base.GetComponentsInChildren<BoxCollider2D>();
		grids = new List<GameObject>();
		items = new List<GameObject>();
		List<RaycastHit2D> list = new List<RaycastHit2D>();
		Vector3 position2 = base.transform.position;
		base.transform.position = position;
		foreach (BoxCollider2D boxCollider2D in this.boxCollider2Ds)
		{
			foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(base.transform.TransformPoint(boxCollider2D.offset), boxCollider2D.size * 0.98f * scale * base.transform.lossyScale, base.transform.rotation.eulerAngles.z, base.transform.forward))
			{
				if (!list.Contains(raycastHit2D))
				{
					list.Add(raycastHit2D);
				}
			}
		}
		base.transform.position = position2;
		foreach (RaycastHit2D raycastHit2D2 in list)
		{
			if (!(raycastHit2D2.collider.GetType() == typeof(CircleCollider2D)) && !(raycastHit2D2.collider.gameObject == base.gameObject))
			{
				if (raycastHit2D2.collider.gameObject.CompareTag("GridSquare"))
				{
					GridSquare component = raycastHit2D2.collider.GetComponent<GridSquare>();
					if (!component || !component.isPouch || !ignorePouchSpaces)
					{
						grids.Add(raycastHit2D2.collider.gameObject);
					}
				}
				else if (raycastHit2D2.collider.gameObject.CompareTag("Item"))
				{
					Item2 componentInParent = raycastHit2D2.collider.GetComponentInParent<Item2>();
					if (componentInParent && componentInParent.gameObject && !items.Contains(componentInParent.gameObject) && componentInParent.itemMovement && (findOutOfGrid || componentInParent.itemMovement.inGrid))
					{
						items.Add(raycastHit2D2.collider.GetComponentInParent<Item2>().gameObject);
					}
				}
			}
		}
		if (considerPouches)
		{
			bool flag = false;
			grids = GridSquare.ConsiderIfHoveringInPouch(grids, out flag);
			if (flag)
			{
				items = ItemPouch.FindItemsInPouches(items);
			}
		}
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00050CE8 File Offset: 0x0004EEE8
	public static void AddAllToGrid()
	{
		foreach (ItemMovement itemMovement in ItemMovement.allItems)
		{
			itemMovement.ConsiderAddToGrid();
		}
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00050D38 File Offset: 0x0004EF38
	public static void AllGetGridObject()
	{
		foreach (ItemMovement itemMovement in ItemMovement.allItems)
		{
			itemMovement.GetGridObject();
		}
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00050D88 File Offset: 0x0004EF88
	public void GetGridObject()
	{
		if (this.gridObject)
		{
			return;
		}
		this.gridObject = base.GetComponent<GridObject>();
		if (!this.gridObject)
		{
			this.gridObject = base.gameObject.AddComponent<GridObject>();
		}
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00050DC2 File Offset: 0x0004EFC2
	public void RemoveGridObjectPositions()
	{
		this.GetGridObject();
		this.gridObject.ClearGridPositions();
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00050DD5 File Offset: 0x0004EFD5
	private IEnumerator RemoveAtEnd()
	{
		while (GameFlowManager.main.isCheckingEffects)
		{
			yield return null;
		}
		if (!this.gridObject)
		{
			this.gridObject = base.GetComponent<GridObject>();
			if (!this.gridObject)
			{
				this.gridObject = base.gameObject.AddComponent<GridObject>();
			}
		}
		this.gridObject.ClearGridPositions();
		yield break;
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00050DE4 File Offset: 0x0004EFE4
	public void RemoveFromGrid()
	{
		if (!this.gridObject)
		{
			this.gridObject = base.GetComponent<GridObject>();
			if (!this.gridObject)
			{
				this.gridObject = base.gameObject.AddComponent<GridObject>();
			}
		}
		if (!GameFlowManager.main.isCheckingEffects)
		{
			this.gridObject.ClearGridPositions();
		}
		else
		{
			if (this.removeAtEndCoroutine != null)
			{
				base.StopCoroutine(this.removeAtEndCoroutine);
			}
			this.removeAtEndCoroutine = base.StartCoroutine(this.RemoveAtEnd());
		}
		this.hoverTime = 0f;
		ItemMovement.RemoveAllHighlights();
		this.RemoveCard();
		this.wasInPouch = false;
		ItemPouch.RemoveItemFromPouches(base.gameObject, out this.wasInPouch);
		PetMaster.RemoveFromPetInventory(this.myItem);
		this.myItem.parentInventoryGrid = null;
		if (this.itemsReplacedByThisCurse.Count > 0)
		{
			this.inGrid = false;
			foreach (ItemMovement itemMovement in this.itemsReplacedByThisCurse)
			{
				if (itemMovement)
				{
					itemMovement.MoveBackAfterCursePlacement();
				}
			}
			this.itemsReplacedByThisCurse = new List<ItemMovement>();
			this.inGrid = true;
		}
		Store store = Object.FindObjectOfType<Store>();
		if (this.myItem.isForSale && !this.myItem.isOwned && !this.myItem.destroyed && this.inGrid)
		{
			this.gameManager.ChangeGold(this.myItem.cost);
			if (store && (this.returnsToOutOfInventoryPosition || !this.myItem.isOwned))
			{
				store.RemoveCost(this.myItem.cost, this.myItem);
			}
		}
		if (!this.inPouch && this.inGrid)
		{
			this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onRemove, this.myItem, null, true, false);
		}
		this.inPouch = false;
		this.inGrid = false;
		this.gameManager.SetItemsAllowToTake();
		this.myItem.RemoveModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.whileActive }, -1);
		this.gameManager.objsInGrid--;
		List<GameObject> list;
		List<GameObject> list2;
		this.TestAtPosition(base.transform.position, out list, out list2, 1f, false, this.wasInPouch, false);
		this.ReenableSpritesOfSlotsBelow(list);
		foreach (object obj in this.itemBackgroundBordersParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		ConnectionManager.main.FindManaNetworks();
		this.gameFlowManager.CheckConstants();
		ItemMovementManager.main.CheckForMovementPublic();
		ItemMovementManager.main.CheckAllForValidPlacementPublic();
		this.gameManager.UpdateFinishButton();
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x000510C0 File Offset: 0x0004F2C0
	public bool CanBeTakenFromLimitedItemGet()
	{
		if (!this.myItem || !this || !GameManager.main || (this.returnsToOutOfInventoryPosition && !this.myItem.isOwned && !this.inGrid && GameManager.main.numOfItemsAllowedToTake <= 0))
		{
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm9"));
			return false;
		}
		return true;
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00051134 File Offset: 0x0004F334
	private void ReenableSpritesOfSlotsBelow(List<GameObject> grids)
	{
		foreach (GameObject gameObject in grids)
		{
			List<Item2> itemsFromGameObjects = Item2.GetItemsFromGameObjects(PathFinding.GetObjectsAtVector(gameObject.transform.position));
			itemsFromGameObjects.Remove(this.myItem);
			bool flag = false;
			foreach (Item2 item in itemsFromGameObjects)
			{
				if (item.itemMovement && item.itemMovement.inGrid)
				{
					flag = true;
				}
			}
			GridSquare component = gameObject.GetComponent<GridSquare>();
			if (!flag || itemsFromGameObjects.Count == 0 || (component && component.isPouch))
			{
				component.containsItem = false;
				component.itemCountsAsEmpty = false;
				SpriteRenderer component2 = component.GetComponent<SpriteRenderer>();
				if (component2)
				{
					component2.enabled = true;
				}
			}
		}
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00051250 File Offset: 0x0004F450
	public IEnumerator ChangeItemAfterDelay()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.myItem.isForSale = false;
		this.returnsToOutOfInventoryPosition = false;
		this.myItem.isOwned = true;
		yield break;
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0005125F File Offset: 0x0004F45F
	public IEnumerator RemoveCostAfterDelay()
	{
		if (!this.returnsToOutOfInventoryPosition)
		{
			yield break;
		}
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		Store store = Object.FindObjectOfType<Store>();
		if (store)
		{
			this.gameManager.ChangeGold(this.myItem.cost * -1);
			store.AddFakeGold();
			store.AddCost();
		}
		yield break;
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00051270 File Offset: 0x0004F470
	public void AddToGrid(bool ignorePouchSpaces = false)
	{
		this.GetGridObject();
		if (this.removeAtEndCoroutine != null)
		{
			base.StopCoroutine(this.removeAtEndCoroutine);
		}
		this.gridObject.SnapToGrid();
		this.isStoppingDrag = true;
		this.isDragging = false;
		if (!this.myItem)
		{
			this.myItem = base.GetComponent<Item2>();
			this.gameFlowManager = GameFlowManager.main;
			this.gameManager = GameManager.main;
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
		if (this.mousePreview)
		{
			this.mousePreview.gameObject.SetActive(false);
		}
		if (this.myItem.isForSale)
		{
			base.StartCoroutine(this.RemoveCostAfterDelay());
		}
		SoundManager.main.PlaySFX("putdown");
		if (this.returnsToOutOfInventoryPosition)
		{
			this.myItem.ConsiderTakingAsLimitedItemGet();
			this.spriteRenderer.sortingOrder = 0;
		}
		this.inGrid = true;
		this.gameManager.SetItemsAllowToTake();
		this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onAdd, this.myItem, null, true, false);
		this.gameManager.objsInGrid++;
		List<GameObject> list;
		List<GameObject> list2;
		this.TestAtPosition(base.transform.position, out list, out list2, 1f, false, !ignorePouchSpaces, ignorePouchSpaces);
		if (list.Count != this.GetSpacesNeeded())
		{
			this.TestAtPosition(base.transform.position, out list, out list2, 1f, false, false, true);
			if (list.Count != this.GetSpacesNeeded())
			{
				Debug.LogError("grids.Count != GetSpacesNeeded() " + list.Count.ToString() + " " + this.GetSpacesNeeded().ToString());
			}
		}
		foreach (GameObject gameObject in list)
		{
			SpriteRenderer component = gameObject.gameObject.GetComponent<SpriteRenderer>();
			this.spriteRenderer.sortingOrder = component.sortingOrder;
			GridSquare component2 = gameObject.GetComponent<GridSquare>();
			component2.containsItem = true;
			if (this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.countAsEmpty) || this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.ghostly))
			{
				component2.itemCountsAsEmpty = true;
			}
			if (component2 && component2.isPouch && component2.itemPouch)
			{
				component2.itemPouch.AddItem(base.gameObject);
				this.inPouch = true;
			}
			this.myItem.parentInventoryGrid = gameObject.transform.parent;
			this.myItem.lastParentInventoryGrid = gameObject.transform.parent;
		}
		PetMaster.AddToPetInventory(this.myItem);
		AchievementAbstractor.instance.ConsiderBagAchievements();
		this.CreateBorder();
		ConnectionManager.main.FindManaNetworks();
		this.gameFlowManager.CheckConstants();
		ItemMovementManager.main.CheckForMovementPublic();
		this.gameManager.UpdateFinishButton();
		ItemMovementManager.main.CheckAllForValidPlacementPublic();
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0005156C File Offset: 0x0004F76C
	public static int GetSpacesNeededStatic(GameObject x)
	{
		BoxCollider2D[] componentsInChildren = x.GetComponentsInChildren<BoxCollider2D>();
		int num = 0;
		foreach (BoxCollider2D boxCollider2D in componentsInChildren)
		{
			num += Mathf.RoundToInt(boxCollider2D.size.x) * Mathf.RoundToInt(boxCollider2D.size.y);
		}
		return num;
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x000515BC File Offset: 0x0004F7BC
	public int GetSpacesNeeded()
	{
		if (this.boxCollider2Ds.Length == 0)
		{
			this.boxCollider2Ds = base.GetComponentsInChildren<BoxCollider2D>();
		}
		int num = 0;
		foreach (BoxCollider2D boxCollider2D in this.boxCollider2Ds)
		{
			num += Mathf.RoundToInt(boxCollider2D.size.x) * Mathf.RoundToInt(boxCollider2D.size.y);
		}
		return num;
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x00051620 File Offset: 0x0004F820
	public void SimpleMove(Vector2 direction)
	{
		Vector3 position = base.transform.position;
		this.RemoveFromGrid();
		base.transform.position += direction;
		this.AddToGrid(false);
		this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onMoveFinish, this.myItem, null, true, false);
		this.mousePreview.position = position;
		this.spriteRenderer.enabled = false;
		base.StartCoroutine(this.MoveOverTime(12f));
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x000516A4 File Offset: 0x0004F8A4
	public void ConsiderCramDestroy()
	{
		int num = 0;
		int num2 = 0;
		foreach (BoxCollider2D boxCollider2D in base.GetComponentsInChildren<BoxCollider2D>())
		{
			int num3 = 0;
			while ((float)num3 < boxCollider2D.size.x)
			{
				int num4 = 0;
				while ((float)num4 < boxCollider2D.size.y)
				{
					float num5 = boxCollider2D.size.x / -2f + (float)num3 + 0.5f + boxCollider2D.offset.x;
					float num6 = boxCollider2D.size.y / -2f + (float)num4 + 0.5f + boxCollider2D.offset.y;
					Vector2 vector = new Vector2(num5, num6);
					RaycastHit2D[] array = Physics2D.RaycastAll(base.transform.TransformPoint(vector), Vector3.forward);
					bool flag = false;
					foreach (RaycastHit2D raycastHit2D in array)
					{
						if (raycastHit2D.collider.GetComponent<GridSquare>())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						num++;
					}
					else
					{
						num2++;
					}
					num4++;
				}
				num3++;
			}
		}
		if (num2 > 0)
		{
			base.gameObject.SetActive(false);
			GameManager.main.objectsToRecover.Add(base.gameObject);
			List<GameObject> list = new List<GameObject>();
			List<GameObject> list2 = new List<GameObject>();
			this.TestAtPosition(base.transform.position, out list, out list2, 1f, false, true, false);
			if (this.inGrid)
			{
				foreach (GameObject gameObject in list)
				{
					gameObject.gameObject.GetComponent<SpriteRenderer>().enabled = true;
				}
			}
			if (this.gameFlowManager.selectedItem && this.myItem && this.gameFlowManager.selectedItem == this.myItem)
			{
				this.gameFlowManager.DeselectItem();
			}
			if (this.myParticles)
			{
				this.myParticles.SetActive(false);
			}
			this.gameManager.draggingItem = null;
			this.isDragging = false;
			this.RemoveCard();
			if (this.inGrid)
			{
				this.RemoveFromGrid();
			}
			if (this.myItem.itemType.Contains(Item2.ItemType.Core))
			{
				this.DelayDestroy();
			}
		}
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x00051930 File Offset: 0x0004FB30
	public int MoveItem(Vector2 direction, int recursion, Vector3 startPos, out bool didMove, bool anyObject = false)
	{
		didMove = false;
		int spacesNeeded = this.GetSpacesNeeded();
		ItemPouch itemPouch = ItemPouch.FindPouchForItem(base.gameObject);
		List<GameObject> list;
		List<GameObject> list2;
		if (itemPouch)
		{
			this.TestAtPositionOutOfGrid(base.transform.position + direction, out list, out list2, 1f, false, itemPouch, false);
		}
		else
		{
			this.TestAtPositionOutOfGrid(base.transform.position + direction, out list, out list2, 1f, false, false, true);
		}
		bool flag = true;
		list2.RemoveAll((GameObject x) => !x.GetComponent<ItemMovement>().inGrid);
		if (this.myItem.itemType.Contains(Item2.ItemType.Hazard))
		{
			list2.RemoveAll((GameObject x) => !x.GetComponent<Item2>().itemType.Contains(Item2.ItemType.Hazard));
		}
		else
		{
			list2.RemoveAll((GameObject x) => x.GetComponent<Item2>().itemType.Contains(Item2.ItemType.Hazard));
		}
		list2.RemoveAll((GameObject x) => x == this.myItem.gameObject);
		if (!anyObject)
		{
			using (List<GameObject>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GameObject gameObject = enumerator.Current;
					Item2 component = gameObject.GetComponent<Item2>();
					if (component.CheckForStatusEffect(Item2.ItemStatusEffect.Type.heavy) || component.CheckForStatusEffect(Item2.ItemStatusEffect.Type.locked) || component.CheckForStatusEffect(Item2.ItemStatusEffect.Type.buoyant))
					{
						flag = false;
					}
				}
				goto IL_0180;
			}
		}
		if (list2.Count > 0)
		{
			flag = false;
		}
		IL_0180:
		if (itemPouch)
		{
			using (List<GameObject>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.GetComponent<GridSquare>().isPouch)
					{
						flag = false;
					}
				}
			}
		}
		if ((GridSquare.IsRunicSquareInList(list) > 0 && !Item2.GetItemStatusEffectBool(Item2.ItemStatusEffect.Type.allowsItemsInIllusorySpaces)) || GridSquare.IsRunicSquareInList(list) == list.Count)
		{
			flag = false;
		}
		int num = -2;
		if (flag && list.Count == spacesNeeded)
		{
			foreach (GameObject gameObject2 in list2)
			{
				ItemMovement component2 = gameObject2.GetComponent<ItemMovement>();
				Carving component3 = gameObject2.GetComponent<Carving>();
				if (component2 && component3 && component2.inGrid)
				{
					Tote.main.DiscardCarving(gameObject2);
				}
			}
			num = this.MoveItem(direction + direction.normalized, recursion++, startPos, out didMove, false);
		}
		if (num == recursion - 1)
		{
			this.RemoveFromGrid();
			base.transform.position += direction;
			foreach (GameObject gameObject3 in list2)
			{
				ItemMovement component4 = gameObject3.GetComponent<ItemMovement>();
				if (this.gameManager.inSpecialReorg && this.gameManager.inventoryPhase != GameManager.InventoryPhase.open)
				{
					component4.MoveAwayFromCurse(this);
					this.itemsReplacedByThisCurse.Add(component4);
				}
				else
				{
					component4.RemoveFromGrid();
					this.moveOutOfGridRoutine = component4.StartCoroutine(component4.Move(base.transform.position, 0));
				}
			}
			if (!itemPouch)
			{
				this.AddToGrid(true);
			}
			else
			{
				this.AddToGrid(false);
			}
			this.mousePreview.position = startPos;
			this.spriteRenderer.enabled = false;
			base.StartCoroutine(this.MoveOverTime(12f));
			if (direction.y < 0f)
			{
				SoundManager.main.PlaySFX("thud", direction.magnitude / 12f);
			}
			else if (direction.y > 0f)
			{
				SoundManager.main.PlaySFX("float", direction.magnitude / 12f);
			}
			didMove = true;
		}
		return recursion;
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x00051D48 File Offset: 0x0004FF48
	public static List<ItemMovement> SortByHighestLeftGridPosition(List<ItemMovement> itemMovements)
	{
		itemMovements = (from x in itemMovements
			orderby x.transform.TransformPoint(x.highestLeftGridPosition).y * -1f, x.transform.TransformPoint(x.highestLeftGridPosition).x
			select x).ToList<ItemMovement>();
		return itemMovements;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x00051DA8 File Offset: 0x0004FFA8
	private void CreateBorder()
	{
		if (this.myItem && this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems))
		{
			return;
		}
		if (!this.spriteRenderer)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
		this.GetGridObject();
		List<ItemBorderBackground> list = new List<ItemBorderBackground>();
		foreach (Vector2Int vector2Int in this.gridObject.gridPositions)
		{
			Vector2 vector = new Vector2((float)vector2Int.x, (float)vector2Int.y);
			base.transform.TransformPoint(vector);
			GameObject gameObject = Object.Instantiate<GameObject>(this.itemBackgroundBorderPrefab, Vector3.zero, Quaternion.identity, this.itemBackgroundBordersParent);
			list.Add(gameObject.GetComponent<ItemBorderBackground>());
			gameObject.transform.position = GridObject.CellToWorld(vector2Int);
			gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0f);
			gameObject.transform.localRotation = Quaternion.identity;
			SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
			component.sortingOrder = this.spriteRenderer.sortingOrder;
			if (this.myItem.IsLocked())
			{
				component.color = Color.red;
			}
			else
			{
				component.color = Color.white;
			}
		}
		if (list.Count > 0)
		{
			this.highestLeftGridPosition = new Vector2(999f, 999f);
			this.highestLeftGridPosition = (from x in list
				orderby x.transform.localPosition.y * -1f, x.transform.localPosition.x
				select x).First<ItemBorderBackground>().transform.localPosition;
			this.highestLeftGridPosition = new Vector2(Mathf.Round(this.highestLeftGridPosition.x * 100f) / 100f, Mathf.Round(this.highestLeftGridPosition.y * 100f) / 100f);
		}
		ItemBorderBackground.SetAllColors(list);
		this.itemBackgroundBordersParent.gameObject.SetActive(true);
		foreach (object obj in this.itemBackgroundBordersParent)
		{
			Transform transform = (Transform)obj;
			transform.GetComponent<SpriteRenderer>().sprite = this.player.chosenCharacter.itemBorderBackgroundSprites[this.ChooseSprite(transform, this.itemBackgroundBordersParent)];
		}
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x000520A8 File Offset: 0x000502A8
	private void ConsiderAddToGrid()
	{
		if (!this || !base.gameObject)
		{
			return;
		}
		if (this.inGrid)
		{
			this.GetGridObject();
			this.gridObject.ClearGridPositions();
			this.gridObject.SnapToGrid();
		}
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x000520E4 File Offset: 0x000502E4
	public static void RemoveAllHighlights()
	{
		ConnectionManager.main.RemoveAllLines();
		Tiler.FadeAllTilers(Tiler.Type.MovementSpace);
		foreach (ItemMovement itemMovement in ItemMovement.allItems)
		{
			itemMovement.RemoveHighlight();
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00052144 File Offset: 0x00050344
	private void RemoveHighlight()
	{
		this.isShowingHighlights = false;
		foreach (object obj in this.itemHighlightBordersParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		if (!LevelUpManager.main.levelingUp)
		{
			foreach (GridSquare gridSquare in GridSquare.allGrids)
			{
				gridSquare.GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x000521FC File Offset: 0x000503FC
	private void ChangeBorderColor(Color color)
	{
		foreach (object obj in this.itemBackgroundBordersParent)
		{
			((Transform)obj).GetComponent<SpriteRenderer>().color = color;
		}
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00052258 File Offset: 0x00050458
	public static void ShowSquares(List<Vector2> vecs, GameObject gameObjectToSpawn, Transform gridParents = null)
	{
		Transform transform = gridParents;
		if (gridParents == null)
		{
			transform = GameObject.FindGameObjectWithTag("GridParent").transform;
		}
		foreach (Vector2 vector in vecs)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(gameObjectToSpawn, transform.TransformPoint(new Vector3(vector.x, vector.y, -3f)), Quaternion.identity, transform);
			bool flag = false;
			if (vecs.Contains(vector + Vector2.left))
			{
				flag = true;
			}
			bool flag2 = false;
			if (vecs.Contains(vector + Vector2.right))
			{
				flag2 = true;
			}
			bool flag3 = false;
			if (vecs.Contains(vector + Vector2.up))
			{
				flag3 = true;
			}
			bool flag4 = false;
			if (vecs.Contains(vector + Vector2.down))
			{
				flag4 = true;
			}
			gameObject.GetComponent<Tiler>().SetTile(ItemMovement.GetTileNumber(flag3, flag4, flag, flag2));
		}
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00052360 File Offset: 0x00050560
	private void ShowSquaresForMovement()
	{
		SpecificConditionToUse[] componentsInChildren = base.GetComponentsInChildren<SpecificConditionToUse>();
		if (!this.myItem || this.myItem.moveArea == Item2.Area.board)
		{
			return;
		}
		List<Item2> list = new List<Item2>();
		List<GridSquare> list2 = new List<GridSquare>();
		this.myItem.FindItemsAndGridsinArea(list, list2, new List<Item2.Area> { this.myItem.moveArea }, this.myItem.moveDistance, null, null, null, true, false, true);
		List<Vector2> list3 = new List<Vector2>();
		foreach (GridSquare gridSquare in list2)
		{
			if (!gridSquare.containsItem || this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems) || this.myItem.mustBePlacedOnItemTypeInCombat != Item2.ItemType.Grid)
			{
				list3.Add(gridSquare.transform.localPosition);
			}
			if (SpecificConditionToUse.CanBePlayedOnSpaceWithTheseItems(componentsInChildren, list))
			{
				list3.Add(gridSquare.transform.localPosition);
			}
		}
		ItemMovement.ShowSquares(list3, this.gameManager.highlightAreaForValidMovement, this.myItem.lastParentInventoryGrid);
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0005248C File Offset: 0x0005068C
	public void ShowSquaresForMovement(Item2.Area area)
	{
		ItemMovement.RemoveAllHighlights();
		SpecificConditionToUse[] componentsInChildren = base.GetComponentsInChildren<SpecificConditionToUse>();
		if (!this.myItem || area == Item2.Area.board)
		{
			return;
		}
		List<Item2> list = new List<Item2>();
		List<GridSquare> list2 = new List<GridSquare>();
		if (area == Item2.Area.board)
		{
			this.myItem.FindItemsAndGridsinArea(list, list2, new List<Item2.Area>
			{
				area,
				Item2.Area.myPlaySpace
			}, this.myItem.moveDistance, null, null, null, true, false, true);
		}
		else
		{
			this.myItem.FindItemsAndGridsinArea(list, list2, new List<Item2.Area> { area }, this.myItem.moveDistance, null, null, null, true, false, true);
		}
		List<Vector2> list3 = new List<Vector2>();
		foreach (GridSquare gridSquare in list2)
		{
			if (!gridSquare.containsItem || this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems) || this.myItem.mustBePlacedOnItemTypeInCombat != Item2.ItemType.Grid)
			{
				list3.Add(gridSquare.transform.localPosition);
			}
			if (SpecificConditionToUse.CanBePlayedOnSpaceWithTheseItems(componentsInChildren, list))
			{
				list3.Add(gridSquare.transform.localPosition);
			}
		}
		Item2.SetAllItemColors();
		ItemMovement.ShowSquares(list3, this.gameManager.highlightAreaForValidMovement, this.myItem.lastParentInventoryGrid);
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x000525E8 File Offset: 0x000507E8
	public void ResetHighlights()
	{
		this.RemoveHighlight();
		this.AddHighlights();
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x000525F8 File Offset: 0x000507F8
	private void AddHighlights()
	{
		this.isShowingHighlights = true;
		if (!this.myItem || this.myItem.itemType.Contains(Item2.ItemType.Treat))
		{
			return;
		}
		foreach (Item2.CombattEffect combattEffect in this.myItem.combatEffects)
		{
			List<Item2> list = new List<Item2>();
			List<GridSquare> list2 = new List<GridSquare>();
			this.myItem.FindItemsAndGridsinArea(list, list2, combattEffect.trigger.areas, combattEffect.trigger.areaDistance, null, combattEffect.trigger.types, null, true, false, true);
			foreach (Item2 item in list)
			{
				if (!(item == this.myItem) && Item2.ShareItemTypes(item.itemType, combattEffect.trigger.types, combattEffect.trigger.excludedTypes))
				{
					item.GetComponent<ItemMovement>().CreateHighlight(Color.yellow, null);
				}
			}
			if (combattEffect.effect.type == Item2.Effect.Type.Mana)
			{
				foreach (ManaStone manaStone in ConnectionManager.main.FindManaStonesForItem(this.myItem, Mathf.RoundToInt(combattEffect.effect.value)))
				{
					List<Vector2> list3 = GameFlowManager.main.FindManaPath(this.myItem, manaStone.transform);
					if (list3 != null)
					{
						ConnectionManager.main.DrawLine(list3, combattEffect.effect.value >= 0f, this);
					}
				}
			}
		}
		foreach (Item2.Modifier modifier in this.myItem.modifiers)
		{
			List<Item2> list4 = new List<Item2>();
			List<GridSquare> list5 = new List<GridSquare>();
			this.myItem.FindItemsAndGridsinArea(list4, list5, modifier.areasToModify, modifier.areaDistance, null, modifier.typesToModify, null, true, false, true);
			foreach (Item2 item2 in list4)
			{
				if (!(item2 == this.myItem) && Item2.ShareItemTypes(item2.itemType, modifier.typesToModify, modifier.excludedTypes))
				{
					item2.GetComponent<ItemMovement>().CreateHighlight(Color.white, null);
				}
			}
			list4 = new List<Item2>();
			list5 = new List<GridSquare>();
			this.myItem.FindItemsAndGridsinArea(list4, list5, modifier.trigger.areas, modifier.trigger.areaDistance, null, modifier.trigger.types, null, true, false, true);
			foreach (Item2 item3 in list4)
			{
				if (!(item3 == this.myItem) && Item2.ShareItemTypes(item3.itemType, modifier.trigger.types, modifier.trigger.excludedTypes))
				{
					item3.GetComponent<ItemMovement>().CreateHighlight(Color.yellow, null);
				}
			}
		}
		foreach (Item2.MovementEffect movementEffect in this.myItem.movementEffects)
		{
			List<Item2> list6 = new List<Item2>();
			List<GridSquare> list7 = new List<GridSquare>();
			this.myItem.FindItemsAndGridsinArea(list6, list7, movementEffect.itemsToMove, movementEffect.areaDistance, null, new List<Item2.ItemType> { Item2.ItemType.Any }, null, true, false, true);
			foreach (Item2 item4 in list6)
			{
				if (!(item4 == this.myItem))
				{
					item4.GetComponent<ItemMovement>().CreateHighlight(Color.white, null);
				}
			}
		}
		int currentCost = Item2.GetCurrentCost(Item2.Cost.Type.mana, this.myItem.costs);
		if (currentCost > 0 && currentCost != -999)
		{
			foreach (ManaStone manaStone2 in ConnectionManager.main.FindManaStonesForItem(this.myItem, currentCost * -1))
			{
				List<Vector2> list8 = GameFlowManager.main.FindManaPath(this.myItem, manaStone2.transform);
				if (list8 != null)
				{
					ConnectionManager.main.DrawLine(list8, false, this);
				}
			}
		}
		SpecialItem component = base.GetComponent<SpecialItem>();
		if (component)
		{
			component.ShowHighlights();
		}
		ScriptedTrigger component2 = base.GetComponent<ScriptedTrigger>();
		if (component2)
		{
			component2.ShowHighlights();
		}
		ValueChanger[] componentsInChildren = base.GetComponentsInChildren<ValueChanger>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].AddHighlights();
		}
		ValueChangerEx[] componentsInChildren2 = base.GetComponentsInChildren<ValueChangerEx>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].AddHighlights();
		}
		if (this.myItem.moveArea != Item2.Area.board)
		{
			this.ShowSquaresForMovement();
		}
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x00052C0C File Offset: 0x00050E0C
	private void AddHighlightsToParent(Transform newHighlightParent, Color color, Item2 item)
	{
		foreach (BoxCollider2D boxCollider2D in item.GetComponentsInChildren<BoxCollider2D>())
		{
			int num = 0;
			while ((float)num < boxCollider2D.size.x)
			{
				int num2 = 0;
				while ((float)num2 < boxCollider2D.size.y)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.itemHighlightPrefab, Vector3.zero, Quaternion.identity, newHighlightParent);
					float num3 = boxCollider2D.size.x / -2f + (float)num + 0.5f + boxCollider2D.offset.x;
					float num4 = boxCollider2D.size.y / -2f + (float)num2 + 0.5f + boxCollider2D.offset.y;
					gameObject.transform.localPosition = new Vector3(num3, num4, -1f);
					gameObject.transform.localRotation = Quaternion.identity;
					SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
					component.sortingOrder = this.spriteRenderer.sortingOrder;
					component.color = color;
					num2++;
				}
				num++;
			}
		}
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00052D24 File Offset: 0x00050F24
	public void AddHighlights(Transform newHighlightParent, Color color, Item2 item, List<Vector2> vecs)
	{
		foreach (Vector2 vector in vecs)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.itemHighlightPrefab, Vector3.zero, Quaternion.identity, newHighlightParent);
			gameObject.transform.localPosition = new Vector3(vector.x, vector.y, -1f);
			gameObject.transform.localRotation = Quaternion.identity;
			SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
			component.sortingOrder = this.spriteRenderer.sortingOrder;
			component.color = color;
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00052DD0 File Offset: 0x00050FD0
	public void CreateHighlight(Color color, List<Vector2> vecs = null)
	{
		this.RemoveHighlight();
		if (!this.itemHighlightBordersParent)
		{
			return;
		}
		this.itemHighlightBordersParent.gameObject.SetActive(true);
		GameObject gameObject = Object.Instantiate<GameObject>(this.itemHighlightParentPrefab, Vector3.zero, Quaternion.identity, this.itemHighlightBordersParent);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		if (vecs != null)
		{
			this.AddHighlights(gameObject.transform, color, this.myItem, vecs);
		}
		else
		{
			this.AddHighlightsToParent(gameObject.transform, color, this.myItem);
		}
		foreach (object obj in gameObject.transform)
		{
			Transform transform = (Transform)obj;
			transform.GetComponent<AnimatedHighlight>().highlightNumber = this.ChooseSprite(transform, gameObject.transform);
		}
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00052EC8 File Offset: 0x000510C8
	public void LookForSpace(Vector2 searchPosition, int recursiveIterations)
	{
		if (recursiveIterations > 60)
		{
			base.transform.position = searchPosition + Vector3.right * 0.2f;
			return;
		}
		if (searchPosition.y < -3f)
		{
			searchPosition = new Vector2(-5.5f, 5.35f - this.size.y);
		}
		List<GameObject> list;
		List<GameObject> list2;
		this.TestAtPositionOutOfGrid(searchPosition, out list, out list2, 1f, true, true, false);
		if (list2.Count != 0 || list.Count != 0)
		{
			searchPosition += Vector2.right;
			if (searchPosition.x + this.size.x / 2f > 6f)
			{
				searchPosition = new Vector2(-5.5f + this.size.x / 2f, searchPosition.y - 1f);
			}
			this.LookForSpace(searchPosition, recursiveIterations + 1);
			return;
		}
		base.transform.position = searchPosition;
		base.transform.position = this.LimitPosition(base.transform.position);
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x00052FE8 File Offset: 0x000511E8
	public void MoveAwayFromCurse(ItemMovement curseThatMovedThisAway)
	{
		curseThatMovedThisAway.itemsReplacedByThisCurse.Add(this);
		this.curseThatMovedThisAway = curseThatMovedThisAway;
		this.mySpot = base.transform.localPosition;
		this.myStoredRotation = base.transform.rotation;
		this.RemoveFromGrid();
		this.moveOutOfGridRoutine = base.StartCoroutine(this.Move(base.transform.position, 0));
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00053054 File Offset: 0x00051254
	public void MoveBackAfterCursePlacement()
	{
		base.StopAllCoroutines();
		Debug.Log("MoveBackAfterCursePlacement");
		List<GameObject> list = new List<GameObject>();
		List<GameObject> list2 = new List<GameObject>();
		this.TestAtPosition(base.transform.TransformPoint(this.mySpot), out list, out list2, 1f, false, true, false);
		foreach (GameObject gameObject in list2)
		{
			if (!(gameObject == base.gameObject) && !(gameObject == this.curseThatMovedThisAway) && gameObject.GetComponent<ItemMovement>().inGrid)
			{
				Debug.Log("MoveBackAfterCursePlacement item is in grid here " + gameObject.name);
				return;
			}
		}
		Debug.Log("MoveBackAfterCursePlacement item is NOT in grid here ");
		base.transform.localPosition = this.mySpot;
		base.transform.rotation = this.myStoredRotation;
		this.curseThatMovedThisAway = null;
		this.isTransiting = false;
		this.mousePreview.localPosition = Vector3.zero;
		this.mousePreview.gameObject.SetActive(false);
		this.spriteRenderer.enabled = true;
		this.AddToGrid(false);
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x00053190 File Offset: 0x00051390
	public Vector2 FindCenterCollider()
	{
		Vector2 vector = base.transform.position;
		float num = 99f;
		foreach (object obj in this.itemBackgroundBordersParent)
		{
			Transform transform = (Transform)obj;
			if (Vector2.Distance(transform.position, base.transform.position) < num)
			{
				num = Vector2.Distance(transform.position, base.transform.position);
				vector = transform.position;
			}
		}
		return vector;
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00053250 File Offset: 0x00051450
	public List<Vector2> FindAllCollidersFromColliders()
	{
		List<Vector2> list = new List<Vector2>();
		foreach (BoxCollider2D boxCollider2D in base.GetComponentsInChildren<BoxCollider2D>())
		{
			int num = 0;
			while ((float)num < boxCollider2D.size.x)
			{
				int num2 = 0;
				while ((float)num2 < boxCollider2D.size.y)
				{
					float num3 = boxCollider2D.size.x / -2f + (float)num + 0.5f + boxCollider2D.offset.x;
					float num4 = boxCollider2D.size.y / -2f + (float)num2 + 0.5f + boxCollider2D.offset.y;
					Vector2 vector = new Vector2(num3, num4);
					Vector2 vector2 = base.transform.TransformPoint(vector);
					list.Add(vector2);
					num2++;
				}
				num++;
			}
		}
		return (from x in list
			orderby x.y * -1f, x.x
			select x).ToList<Vector2>();
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x00053392 File Offset: 0x00051592
	public List<Vector2> FindAllColliders()
	{
		return this.FindAllCollidersFromColliders();
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0005339A File Offset: 0x0005159A
	public IEnumerator StopDragInCombat()
	{
		if (!this.myItem || this.myItem.destroyed)
		{
			yield break;
		}
		new List<RaycastHit2D>();
		int spacesNeeded = this.GetSpacesNeeded();
		List<GameObject> list;
		List<GameObject> list2;
		this.TestAtPosition(base.transform.position, out list, out list2, 1f, false, true, false);
		bool acceptable = true;
		if (!this.gameFlowManager.IsComplete())
		{
			acceptable = false;
		}
		if (list.Count != spacesNeeded)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm34"));
			acceptable = false;
		}
		if (Vector2.Distance(this.startPosition, base.transform.position) < 0.1f)
		{
			acceptable = false;
		}
		if (acceptable && this.myItem.mustBePlacedOnItemTypeInCombat != Item2.ItemType.Grid && this.myItem.mustBePlacedOnItemTypeInCombat != Item2.ItemType.Any)
		{
			acceptable = false;
			foreach (GameObject gameObject in list2)
			{
				Item2 component = gameObject.GetComponent<Item2>();
				if (component && component.itemType.Contains(this.myItem.mustBePlacedOnItemTypeInCombat))
				{
					acceptable = true;
					break;
				}
			}
			if (!acceptable)
			{
				string text = LangaugeManager.main.GetTextByKey("isCurseMBPO");
				text = text + " " + LangaugeManager.main.GetTextByKey(this.myItem.mustBePlacedOnItemTypeInCombat.ToString());
				this.gameManager.CreatePopUp(text);
			}
		}
		else if (acceptable && list2.Count > 0 && !this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems) && this.myItem.mustBePlacedOnItemTypeInCombat != Item2.ItemType.Any)
		{
			acceptable = false;
			if (SpecificConditionToUse.CanBePlayedOnSpaceWithTheseItems(base.GetComponentsInChildren<SpecificConditionToUse>(), list2))
			{
				acceptable = true;
			}
			if (!acceptable)
			{
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm34"));
			}
		}
		if (acceptable && this.myItem.moveArea != Item2.Area.board)
		{
			foreach (GameObject gameObject2 in list)
			{
				if (!this.gameManager.AreasForSpecialReorganizationIncludes(gameObject2.transform.localPosition, this.myItem.lastParentInventoryGrid))
				{
					this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm34"));
					acceptable = false;
				}
			}
		}
		Carving carving = base.GetComponent<Carving>();
		if (carving)
		{
			if (!this.gridObject)
			{
				this.gridObject = base.GetComponent<GridObject>();
				if (!this.gridObject)
				{
					this.gridObject = base.gameObject.AddComponent<GridObject>();
				}
			}
			if (!this.gridObject)
			{
				Debug.LogError("carving doesn't have grid object");
				yield break;
			}
			bool wasInGrid = this.inGrid;
			this.inGrid = true;
			List<Vector2Int> vecs = this.gridObject.gridPositions;
			this.gridObject.SnapToGrid();
			ConnectionManager.main.FindManaNetworks();
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			yield return new WaitForFixedUpdate();
			this.gridObject.gridPositions = vecs;
			this.inGrid = wasInGrid;
			vecs = null;
		}
		if (acceptable && ((!carving && !this.myItem.CanBeUsedActive(true, this.myItem.costs, true, false, new List<SpecificConditionToUse.ConditionTime> { SpecificConditionToUse.ConditionTime.onDragEnd }, false)) || (carving && !this.myItem.CanBeUsedActive(true, carving.summoningCosts, false, false, new List<SpecificConditionToUse.ConditionTime> { SpecificConditionToUse.ConditionTime.onDragEnd }, false))))
		{
			acceptable = false;
		}
		this.gameManager.ClearAreasForSpecialReorganization();
		if (acceptable)
		{
			SoundManager.main.PlaySFX("carvingPlay");
			if (carving)
			{
				carving.RemoveUI();
			}
			this.isStoppingDrag = false;
			this.isDragging = false;
			this.isPlayedCard = true;
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -0.05f);
			this.mousePreview.transform.localPosition = Vector3.zero;
			this.mousePreview.gameObject.SetActive(true);
			this.AddToGrid(false);
			this.gameFlowManager.CheckConstants();
			this.gameManager.AddParticles(base.transform.position + Vector3.forward, this.spriteRenderer, this.gameManager.carvingSummonParticles);
			if (carving)
			{
				this.gameFlowManager.AddCombatStat(GameFlowManager.CombatStat.Type.carvingsUsed, 1, null);
				Item2.DetractCosts(this.myItem, carving.summoningCosts, null);
				this.player.itemToInteractWith.sprite = this.spriteRenderer.sprite;
				this.player.GetComponentInChildren<Animator>().Play("Player_UseItem", 0, 0f);
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onSummonCarvingEarly, this.myItem, null, true, false);
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onSummonCarving, this.myItem, null, true, false);
				this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onSummonCarvingLate, this.myItem, null, true, false);
				Tote main = Tote.main;
				if (main)
				{
					main.ShowDamageValue();
				}
				if (main && main.GetCarvingsInHandNotPlayed() == 0 && Item2.GetItemStatusEffectBool(Item2.ItemStatusEffect.Type.reverseHourglass) && Item2.UseAllItemsIndirectWithStatusEffect(Item2.ItemStatusEffect.Type.reverseHourglass, null))
				{
					main.DrawCarvingsFromUndrawn(1);
				}
			}
			else
			{
				this.gameFlowManager.CheckConstants();
				yield return this.gameFlowManager.UseItem(this.myItem, false, false, Item2.PlayerAnimation.UseDefault, false, false);
				this.gameManager.inSpecialReorg = false;
				this.gameManager.inventoryPhase = GameManager.InventoryPhase.locked;
				while (this.gameFlowManager.isCheckingEffects)
				{
					yield return null;
				}
				if (this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombat))
				{
					yield return new WaitForFixedUpdate();
					yield return new WaitForFixedUpdate();
					if (!this.myItem || this.myItem.destroyed)
					{
						yield break;
					}
					this.AddToGrid(false);
				}
				else if (this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition))
				{
					this.RemoveFromGrid();
					base.transform.position = this.startPosition;
					base.transform.rotation = this.startRotation;
					yield return new WaitForFixedUpdate();
					if (!this.myItem || this.myItem.destroyed)
					{
						yield break;
					}
					this.AddToGrid(false);
				}
			}
		}
		else
		{
			this.EndDrag();
		}
		yield break;
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x000533AC File Offset: 0x000515AC
	public void StartDrag()
	{
		if (this.isStoppingDrag)
		{
			return;
		}
		if (GameManager.main && GameManager.main.dead)
		{
			return;
		}
		if (!this.gameFlowManager || (!this.gameFlowManager.IsComplete() && this.gameManager.inventoryPhase != GameManager.InventoryPhase.specialReorganization) || this.itemMovementManager.isRunning || this.itemMovementManager.isCheckingForPuzzle || this.gameManager.draggingItem)
		{
			return;
		}
		if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.specialReorganization && !this.gameManager.itemsForSpecialReorganization.Contains(base.gameObject))
		{
			return;
		}
		if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.choose)
		{
			return;
		}
		if (!this.myItem || this.myItem.destroyed)
		{
			return;
		}
		if (this.myItem.itemType.Contains(Item2.ItemType.Carving) && this.gameManager.inventoryPhase == GameManager.InventoryPhase.open && this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.outOfBattle)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm43"));
			return;
		}
		this.StartDragWithoutChecks();
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x000534D4 File Offset: 0x000516D4
	private void StartDragWithoutChecks()
	{
		if (!this.myItem)
		{
			this.myItem = base.GetComponent<Item2>();
		}
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		if (!this.gameFlowManager)
		{
			this.gameFlowManager = GameFlowManager.main;
		}
		if (!this.myItem || !this.gameManager || !this.gameFlowManager)
		{
			return;
		}
		if ((this.myItem.IsLocked() && this.inGrid) || this.TestForHazard())
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm32"));
			base.StartCoroutine(this.Shake());
			return;
		}
		if (this.satchelLocked)
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gmSatchel2"));
			base.StartCoroutine(this.Shake());
			return;
		}
		if (!this.CanBeTakenFromLimitedItemGet())
		{
			this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm9"));
			base.StartCoroutine(this.Shake());
			return;
		}
		if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.specialReorganization)
		{
			foreach (ItemMovement itemMovement in this.gameManager.itemsReplacedBySpecialReorgItem)
			{
				if (itemMovement)
				{
					itemMovement.transform.position = itemMovement.startPosition;
					itemMovement.transform.rotation = itemMovement.startRotation;
					itemMovement.AddToGrid(false);
				}
			}
			this.gameManager.itemsReplacedBySpecialReorgItem.Clear();
		}
		ItemTogglerForController.main.ResetItem();
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn && this.gameManager.inventoryPhase == GameManager.InventoryPhase.locked)
		{
			this.gameManager.inventoryPhase = GameManager.InventoryPhase.inCombatMove;
		}
		if (this.myItem.isOwned)
		{
			Store store = Object.FindObjectOfType<Store>();
			if (store)
			{
				store.ShowSellText(this.myItem);
			}
		}
		this.gameManager.draggingItem = base.gameObject;
		SoundManager.main.PlaySFX("pickup");
		ContextControlsDisplay.main.ClearAllControls();
		DigitalCursor.main.UpdateContextControls();
		Vector3 eulerAngles = base.transform.eulerAngles;
		eulerAngles.x = 0f;
		eulerAngles.y = 0f;
		eulerAngles.z = Mathf.Round(eulerAngles.z / 90f) * 90f;
		base.transform.eulerAngles = eulerAngles;
		if (this.mousePreview)
		{
			this.mousePreview.gameObject.SetActive(true);
			SpriteRenderer component = this.mousePreview.GetComponent<SpriteRenderer>();
			component.sprite = this.spriteRenderer.sprite;
			component.sharedMaterial = this.spriteRenderer.sharedMaterial;
			component.sortingOrder = this.spriteRenderer.sortingOrder;
			component.color = new Color(1f, 1f, 1f, 1f);
			this.spriteRenderer.color = new Color(1f, 1f, 1f, 0.6f);
		}
		this.pouchWasOpenAtStart = false;
		ItemPouch component2 = base.GetComponent<ItemPouch>();
		if (component2 && component2.open)
		{
			this.pouchWasOpenAtStart = true;
		}
		this.everMovedDuringThisDrag = false;
		this.startRotation = base.transform.rotation;
		this.startPosition = base.transform.position;
		Vector2 vector = DigitalCursor.main.transform.position;
		this.difference = base.transform.position - vector;
		if (this.difference.magnitude > this.size.magnitude)
		{
			this.difference = Vector2.zero;
		}
		this.isDragging = true;
		this.RemoveCard();
		if (this.inGrid)
		{
			this.RemoveFromGrid();
			this.liftedFromGrid = true;
		}
		else
		{
			this.liftedFromGrid = false;
		}
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller && !this.liftedFromGrid)
		{
			bool flag = false;
			foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(base.transform.position, (base.transform.parent.position - base.transform.position).normalized, 1000f))
			{
				if (raycastHit2D.collider.CompareTag("GridSquare"))
				{
					flag = true;
					DigitalCursor.main.SetGameWorldDestinationTransform(raycastHit2D.collider.transform);
					break;
				}
			}
			if (!flag)
			{
				DigitalCursor.main.SetGameWorldDestinationTransform(base.transform.parent);
			}
		}
		ItemMovement.RemoveAllHighlights();
		this.AddHighlights();
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x000539B4 File Offset: 0x00051BB4
	private static void IsAcceptablePlacement(ItemMovement itemMovement, Vector3 startPosition, Vector3 endPosition, bool displayReasonWhyNot, int recursionNumber)
	{
		if (!itemMovement || !itemMovement.myItem || !itemMovement.gridObject)
		{
			return;
		}
		if (recursionNumber == 0)
		{
			ItemMovement.itemsThatCannotBeSwappedAgain = new List<ItemMovement>();
		}
		if (!ItemMovement.itemsThatCannotBeSwappedAgain.Contains(itemMovement))
		{
			ItemMovement.itemsThatCannotBeSwappedAgain.Add(itemMovement);
		}
		Vector2 vector = endPosition - startPosition;
		GameManager main = GameManager.main;
		ItemMovementManager main2 = ItemMovementManager.main;
		int spacesNeeded = itemMovement.GetSpacesNeeded();
		List<Item2> list = new List<Item2>();
		List<GridSquare> list2 = new List<GridSquare>();
		itemMovement.transform.position = endPosition;
		itemMovement.gridObject.SnapToGrid();
		itemMovement.myItem.FindItemsAndGridsinArea(list, list2, new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, false, false, false);
		itemMovement.gridObject.ClearGridPositions();
		if (recursionNumber != 0)
		{
			itemMovement.transform.position = startPosition;
		}
		list.Remove(itemMovement.myItem);
		if (list2.Count == 0)
		{
			itemMovement.MoveOutOfGrid(false, true);
			return;
		}
		if (list.Count > 0 && recursionNumber > 0)
		{
			return;
		}
		if (list2.Count != spacesNeeded)
		{
			if (displayReasonWhyNot)
			{
				itemMovement.RemoveFromGrid();
				itemMovement.MoveOutOfGrid(true, true);
			}
			return;
		}
		SpecialItem component = itemMovement.GetComponent<SpecialItem>();
		if (component && !component.ConsiderPlacement(endPosition))
		{
			if (displayReasonWhyNot)
			{
				itemMovement.RemoveFromGrid();
				itemMovement.MoveOutOfGrid(true, true);
			}
			return;
		}
		foreach (Item2 item in list)
		{
			if (item.IsLocked())
			{
				if (displayReasonWhyNot)
				{
					main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm32"));
					ItemMovement component2 = item.GetComponent<ItemMovement>();
					component2.StartCoroutine(component2.Shake());
					itemMovement.RemoveFromGrid();
					itemMovement.MoveOutOfGrid(true, true);
				}
				return;
			}
		}
		if (itemMovement.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems))
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GetComponent<ItemMovement>().myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems))
				{
					if (displayReasonWhyNot)
					{
						main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36"));
						itemMovement.RemoveFromGrid();
						itemMovement.MoveOutOfGrid(true, true);
					}
					return;
				}
				list.RemoveAt(i);
				i--;
			}
		}
		bool flag = true;
		if (main.inventoryPhase == GameManager.InventoryPhase.specialReorganization && !main.itemsForSpecialReorganization.Contains(itemMovement.gameObject) && list2.Count > 0)
		{
			flag = false;
		}
		else if (main.inventoryPhase == GameManager.InventoryPhase.specialReorganization && list2.Count > 0)
		{
			flag = false;
			foreach (GridSquare gridSquare in list2)
			{
				foreach (GameManager.AreasForSpecialReorganization areasForSpecialReorganization in main.areasForSpecialReorganizations)
				{
					if ((areasForSpecialReorganization.gridParent == itemMovement.myItem.parentInventoryGrid || areasForSpecialReorganization.gridParent == itemMovement.myItem.lastParentInventoryGrid) && areasForSpecialReorganization.localPositions.Contains(gridSquare.transform.localPosition))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				if (displayReasonWhyNot)
				{
					main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm34"));
					itemMovement.RemoveFromGrid();
					itemMovement.MoveOutOfGrid(true, true);
				}
				return;
			}
		}
		ItemPouch itemPouch;
		int num = GridSquare.IsPouchSquareInList(list2.ConvertAll<GameObject>((GridSquare x) => x.gameObject), out itemPouch);
		if (itemPouch && itemPouch.petItem && !itemMovement.myItem.itemType.Contains(Item2.ItemType.Treat))
		{
			if (displayReasonWhyNot)
			{
				main.CreatePopUp(LangaugeManager.main.GetTextByKey("gmPetBags"));
				itemMovement.RemoveFromGrid();
				itemMovement.MoveOutOfGrid(true, true);
			}
			return;
		}
		RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.mustKeep);
		if ((itemMovement.GetComponent<ItemPouch>() && num > 0) || (itemPouch && itemPouch.allowedTypes.Count > 0 && !Item2.ShareItemTypes(itemPouch.allowedTypes, itemMovement.myItem.itemType)) || (itemPouch && itemMovement.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBePlayedOverOtherItems)) || (itemPouch && runProperty && runProperty.assignedGameObject.Contains(itemMovement.gameObject)))
		{
			if (displayReasonWhyNot)
			{
				main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm34"));
				itemMovement.RemoveFromGrid();
				itemMovement.MoveOutOfGrid(true, true);
			}
			return;
		}
		if (list2.Count > 0 && list2[0].transform.parent.CompareTag("PetGridParent") && (itemMovement.myItem.itemType.Contains(Item2.ItemType.Pet) || itemMovement.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canOnlyBeHeldByPochette)))
		{
			if (displayReasonWhyNot)
			{
				main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm54"));
				itemMovement.RemoveFromGrid();
				itemMovement.MoveOutOfGrid(true, true);
			}
			return;
		}
		if (itemMovement.myItem.itemType.Contains(Item2.ItemType.Carving) && list2.Count > 0 && main.inventoryPhase != GameManager.InventoryPhase.locked)
		{
			if (displayReasonWhyNot)
			{
				main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm36"));
				itemMovement.RemoveFromGrid();
				itemMovement.MoveOutOfGrid(true, true);
			}
			return;
		}
		for (int j = 0; j < list.Count; j++)
		{
			Item2 item2 = list[j];
			if (item2 && item2.itemMovement)
			{
				ItemMovement itemMovement2 = item2.itemMovement;
				if (GameManager.main.inSpecialReorg && main.inventoryPhase != GameManager.InventoryPhase.open)
				{
					itemMovement2.MoveAwayFromCurse(itemMovement);
					list.RemoveAt(j);
					j--;
				}
				else if (ItemMovement.itemsThatCannotBeSwappedAgain.Contains(itemMovement2))
				{
					itemMovement.RemoveFromGrid();
					itemMovement.MoveOutOfGrid(true, true);
					return;
				}
			}
		}
		itemMovement.StopAllCoroutines();
		itemMovement.isTransiting = false;
		itemMovement.isDragging = false;
		itemMovement.pauseTime = 0.3f;
		itemMovement.transform.position = endPosition;
		itemMovement.transform.localPosition = new Vector3(itemMovement.transform.localPosition.x, itemMovement.transform.localPosition.y, 0f);
		if (itemPouch)
		{
			itemMovement.AddToGrid(false);
		}
		else
		{
			itemMovement.AddToGrid(true);
		}
		itemMovement.mousePreview.position = startPosition;
		if (recursionNumber > 0)
		{
			itemMovement.StartCoroutine(itemMovement.MoveOverTime(12f));
		}
		main.itemsReplacedBySpecialReorgItem.Clear();
		if (list.Count > 0)
		{
			foreach (Item2 item3 in list)
			{
				if (item3 && item3.itemMovement)
				{
					ItemMovement itemMovement3 = item3.itemMovement;
					itemMovement3.RemoveFromGrid();
					itemMovement3.MoveOutOfGrid(true, false);
					vector = new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
					if (recursionNumber <= 3 && vector.magnitude > 0f && main.inventoryPhase != GameManager.InventoryPhase.specialReorganization && !itemMovement3.inPouch && !itemMovement.inPouch)
					{
						Vector3 position = itemMovement3.transform.position;
						ItemMovement.IsAcceptablePlacement(itemMovement3, position, position - vector, true, recursionNumber + 1);
						if (!itemMovement3.inGrid)
						{
							Vector2 vector2 = position - vector / 2f;
							vector2 = new Vector2(Mathf.Round(vector2.x), Mathf.Round(vector2.y));
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, vector2, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							Vector2 vector3 = position - vector * 2f;
							vector3 = new Vector2(Mathf.Round(vector3.x), Mathf.Round(vector3.y));
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, vector3, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, position - vector + Vector3.down, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, position - vector + Vector3.up, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, position - vector + Vector3.left, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, position - vector + Vector3.right, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, position + Vector3.down, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, position + Vector3.up, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, position + Vector3.left, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							ItemMovement.IsAcceptablePlacement(itemMovement3, position, position + Vector3.right, true, recursionNumber + 1);
						}
						if (!itemMovement3.inGrid)
						{
							itemMovement3.RemoveFromGrid();
							itemMovement3.MoveOutOfGrid(true, true);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x000543B0 File Offset: 0x000525B0
	public void StopAllMovements()
	{
		this.pauseTime = 0.3f;
		this.isDragging = false;
		this.isMovingItem = false;
		this.isAnimating = false;
		this.isHovering = false;
		this.isStoppingDrag = false;
		this.isTransiting = false;
		this.animator.enabled = false;
		base.StopAllCoroutines();
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00054404 File Offset: 0x00052604
	public void MoveOutOfGrid(bool findNewPosition = false, bool moveToMouse = true)
	{
		this.spriteRenderer.enabled = false;
		if (moveToMouse)
		{
			base.transform.position = this.mousePreview.position;
			this.mousePreview.localPosition = Vector3.zero;
		}
		else
		{
			this.mousePreview.localPosition = Vector3.zero;
		}
		if (!this.returnsToOutOfInventoryPosition)
		{
			if (findNewPosition)
			{
				this.moveOutOfGridRoutine = base.StartCoroutine(this.Move(base.transform.parent.TransformPoint(base.transform.localPosition), 0));
				return;
			}
			if (base.transform.localPosition.y < -3f)
			{
				this.moveOutOfGridRoutine = base.StartCoroutine(this.Move(base.transform.parent.TransformPoint(new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z)), base.transform.rotation, 0));
				return;
			}
		}
		else
		{
			this.moveOutOfGridRoutine = base.StartCoroutine(this.Move(base.transform.parent.TransformPoint(this.outOfInventoryPosition), this.outOfInventoryRotation, 0));
		}
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x00054554 File Offset: 0x00052754
	private void EndDrag()
	{
		if (!this.myItem.itemType.Contains(Item2.ItemType.Carving))
		{
			base.transform.position = this.startPosition;
			base.transform.rotation = this.startRotation;
			this.AddToGrid(false);
		}
		if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.inCombatMove)
		{
			this.gameManager.inventoryPhase = GameManager.InventoryPhase.locked;
		}
		this.gameManager.inSpecialReorg = false;
		ItemMovement.RemoveAllHighlights();
		this.mousePreviewRenderer.enabled = false;
		this.spriteRenderer.enabled = true;
		this.isPlayedCard = false;
		this.isDragging = false;
		if (this.gameManager.draggingItem == this.myItem)
		{
			this.gameManager.draggingItem = null;
		}
		this.myItem.SetColor();
		if (base.GetComponent<Carving>())
		{
			base.StartCoroutine(this.MoveOverTime(12f));
		}
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x0005463D File Offset: 0x0005283D
	public IEnumerator StopDrag()
	{
		if (this.isStoppingDrag)
		{
			yield break;
		}
		this.isStoppingDrag = true;
		ItemTogglerForController.main.ResetItem();
		ItemMovement.RemoveAllHighlights();
		Player main = Player.main;
		Item2.SetAllItemColors();
		this.gameManager.draggingItem = null;
		ContextControlsDisplay.main.ClearAllControls();
		DigitalCursor.main.UpdateContextControls();
		if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.inCombatMove)
		{
			if (this.gameFlowManager.isCheckingEffects)
			{
				this.EndDrag();
				yield break;
			}
			if (this.myItem.itemType.Contains(Item2.ItemType.Carving) || this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombat) || this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition))
			{
				yield return this.StopDragInCombat();
			}
			else
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 0f);
				this.myItem.SetColor();
				this.mousePreview.gameObject.SetActive(false);
				this.spriteRenderer.enabled = true;
				this.isDragging = false;
			}
			this.isDragging = false;
			yield break;
		}
		else
		{
			if (!this.myItem || this.myItem.destroyed)
			{
				yield break;
			}
			Store store = Object.FindObjectOfType<Store>();
			Tote main2 = Tote.main;
			Store store2 = Object.FindObjectOfType<Store>();
			RaycastHit2D[] array = Physics2D.RaycastAll(base.transform.position, Vector2.zero);
			if (store2 && this.myItem.isOwned && !this.inPouch)
			{
				store2.HideSellText();
				foreach (RaycastHit2D raycastHit2D in array)
				{
					Store component = raycastHit2D.collider.gameObject.GetComponent<Store>();
					if (component && (!component.dungeonEvent || component.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.boughtItems) < 3) && store2.BuyPlayerItem(this.myItem))
					{
						this.isDragging = false;
						this.isStoppingDrag = false;
						yield break;
					}
				}
			}
			if (this.myItem.itemType.Contains(Item2.ItemType.Key))
			{
				foreach (RaycastHit2D raycastHit2D2 in array)
				{
					Chest component2 = raycastHit2D2.collider.gameObject.GetComponent<Chest>();
					if (component2)
					{
						component2.UnlockAndOpen(this.myItem);
					}
				}
			}
			if (store && this.myItem.isForSale && this.myItem.cost > this.gameManager.GetCurrentGold())
			{
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
				store.CannotAfford();
				this.moveOutOfGridRoutine = base.StartCoroutine(this.Move(base.transform.position, 0));
				yield break;
			}
			Vector3 localPosition = this.mousePreviewRenderer.transform.localPosition;
			Vector3 position = base.transform.position;
			ItemMovement.IsAcceptablePlacement(this, this.startPosition, base.transform.position, true, 0);
			if (!this.inGrid)
			{
				this.myItem.parentInventoryGrid = null;
			}
			ItemMovement.RemoveAllHighlights();
			this.isDragging = false;
			this.isStoppingDrag = false;
			this.SetupPositionAndParent();
			this.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
			this.mousePreview.gameObject.SetActive(false);
			this.spriteRenderer.enabled = true;
			ItemPouch component3 = base.GetComponent<ItemPouch>();
			if (component3 && this.startPosition == base.transform.position && !this.everMovedDuringThisDrag && !this.pouchWasOpenAtStart && this.gameManager.inventoryPhase != GameManager.InventoryPhase.locked)
			{
				component3.Toggle();
				component3.OpenedViaClick();
			}
			yield break;
		}
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0005464C File Offset: 0x0005284C
	public void OnMouseDown()
	{
		this.ClickOrPress();
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00054654 File Offset: 0x00052854
	public void ClickOrPress()
	{
		if (!this.myItem)
		{
			this.myItem = base.GetComponent<Item2>();
		}
		DigitalCursor.main.UpdateToMousePosition();
		if (!this.saveManager || this.saveManager.isSavingOrLoading)
		{
			return;
		}
		if (ItemMovementManager.main.isRunning)
		{
			return;
		}
		if (this.gameManager.dead && this.myItem)
		{
			SaveAnItemOnLoss saveAnItemOnLoss = Object.FindObjectOfType<SaveAnItemOnLoss>();
			if (saveAnItemOnLoss)
			{
				saveAnItemOnLoss.GetItem(this.myItem);
			}
			return;
		}
		if (SingleUI.IsViewingPopUp() && !base.transform.IsChildOf(SingleUI.GetPopUp().transform))
		{
			return;
		}
		if (!this.moveToItemTransform || (this.gameManager.viewingEvent && !this.gameManager.inSpecialReorg) || ContextMenuManager.main.currentState != ContextMenuManager.CurrentState.noMenu)
		{
			return;
		}
		if (this.myItem.itemType.Contains(Item2.ItemType.Carving) && !this.inGrid && this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.outOfBattle)
		{
			Carving component = base.GetComponent<Carving>();
			if (!component || component.myUIlocation != null)
			{
				return;
			}
			if (this.gameManager.limitedItemReorganize && this.gameManager.numOfItemsAllowedToTake <= 0 && !this.inGrid && this.returnsToOutOfInventoryPosition)
			{
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm9"));
				base.StartCoroutine(this.Shake());
				return;
			}
			Store store = Object.FindObjectOfType<Store>();
			if (store && this.myItem.cost > this.gameManager.GetCurrentGold())
			{
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
				return;
			}
			if (store && this.myItem.isForSale)
			{
				this.gameManager.ChangeGold(-this.myItem.cost);
			}
			Tote main = Tote.main;
			if (main && !GameManager.main.IsConsideringCurseReplacement())
			{
				this.returnsToOutOfInventoryPosition = false;
				main.AddNewCarvingToUndrawn(base.gameObject);
				this.StopAllMovements();
			}
			return;
		}
		else if (this.myItem.itemType.Contains(Item2.ItemType.Carving) && !this.inGrid)
		{
			Carving component2 = base.GetComponent<Carving>();
			if (this.player.AP < Item2.GetCurrentCost(Item2.Cost.Type.energy, component2.summoningCosts))
			{
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
				return;
			}
			if (this.gameManager.GetCurrentGold() < Item2.GetCurrentCost(Item2.Cost.Type.gold, component2.summoningCosts))
			{
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm27"));
				return;
			}
			this.StartDrag();
			return;
		}
		else
		{
			if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.open || (this.gameManager.inventoryPhase == GameManager.InventoryPhase.specialReorganization && (this.gameManager.itemsForSpecialReorganization.Contains(base.gameObject) || !this.inGrid)))
			{
				if (this.parentAltar)
				{
					this.parentAltar.GetComponent<SimpleClickItem>().Click();
					this.parentAltar = null;
				}
				this.StartDrag();
				return;
			}
			if (this.myItem && this.inGrid && (this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombat) || this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition)) && this.myItem.CanBeUsedActive(true, this.myItem.costs, true, false, null, false) && this.gameFlowManager && this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn)
			{
				if (this.gameManager && this.gameManager.reorgnizeItem && this.gameManager.reorgnizeItem == base.gameObject)
				{
					return;
				}
				this.StartDrag();
				this.ShowSquaresForMovement(this.myItem.moveArea);
			}
			return;
		}
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00054A38 File Offset: 0x00052C38
	private void SpecialDestroy()
	{
		Item2.Modifier modifier = new Item2.Modifier();
		modifier.trigger = new Item2.Trigger();
		modifier.trigger.trigger = Item2.Trigger.ActionTrigger.onRemove;
		modifier.trigger.areas = new List<Item2.Area> { Item2.Area.self };
		modifier.trigger.types = new List<Item2.ItemType> { Item2.ItemType.Any };
		modifier.areasToModify = new List<Item2.Area> { Item2.Area.self };
		modifier.typesToModify = new List<Item2.ItemType> { Item2.ItemType.Any };
		Item2.Effect effect = new Item2.Effect();
		effect.type = Item2.Effect.Type.Destroy;
		modifier.effects = new List<Item2.Effect> { effect };
		this.myItem.modifiers.Add(modifier);
		this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onRemove, this.myItem, null, true, false);
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00054AFC File Offset: 0x00052CFC
	public void PretendDestroyCurse()
	{
		if (this.myItem && this.myItem.destroyed)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.myItem && this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.Cleansed))
		{
			this.DelayDestroy();
			return;
		}
		this.gameManager.AddParticles(base.transform.position + Vector3.back, this.spriteRenderer, null);
		CurseManager.Instance.AddCurse(base.gameObject);
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00054B8C File Offset: 0x00052D8C
	public void DelayDestroy()
	{
		if (!this.myItem || this.myItem.destroyed)
		{
			return;
		}
		EnergyEmitter component = base.GetComponent<EnergyEmitter>();
		if (component)
		{
			component.HideHeatParticles();
		}
		if (this.myItem.petItem && this.myItem.petItem.combatPet)
		{
			this.myItem.petItem.RemoveCombatPet();
		}
		ItemPouch component2 = base.GetComponent<ItemPouch>();
		if (component2)
		{
			component2.ClosePouch();
		}
		if (this.myItem.itemType.Contains(Item2.ItemType.Core))
		{
			this.player.stats.SetHealth(-999);
		}
		if (RunTypeManager.CheckIfAssignedItemIsInProperty(RunType.RunProperty.Type.mustKeep, base.gameObject))
		{
			this.player.stats.SetHealth(-999);
		}
		if (this.myItem.itemType.Contains(Item2.ItemType.Carving) && !this.returnsToOutOfInventoryPosition)
		{
			Tote.main.RemoveCarvingFromLists(base.gameObject);
		}
		List<GameObject> list = new List<GameObject>();
		List<GameObject> list2 = new List<GameObject>();
		this.TestAtPosition(base.transform.position, out list, out list2, 1f, false, true, false);
		this.ReenableSpritesOfSlotsBelow(list);
		if (this.gameFlowManager.selectedItem && this.myItem && this.gameFlowManager.selectedItem == this.myItem)
		{
			GameManager.main.EndChooseItem();
		}
		if (this.myParticles)
		{
			this.myParticles.SetActive(false);
		}
		this.gameManager.draggingItem = null;
		this.isDragging = false;
		this.myItem.destroyed = true;
		base.DisableInterface();
		this.gameFlowManager.itemsDestroyedInThisEffectSequence.Add(this.myItem);
		this.RemoveCard();
		if (!this.gameManager.inSpecialReorg && this.myItem && this.inGrid)
		{
			this.gameFlowManager.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onDestroy, this.myItem, null, true, false);
		}
		if (this.inGrid)
		{
			this.RemoveFromGrid();
		}
		this.mousePreview.gameObject.SetActive(false);
		BoxCollider2D[] componentsInChildren = base.GetComponentsInChildren<BoxCollider2D>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		SpriteRenderer[] componentsInChildren2 = base.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].enabled = false;
		}
		this.gameManager.AddParticles(base.transform.position + Vector3.back, this.spriteRenderer, null);
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00054E28 File Offset: 0x00053028
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (!ContextMenuManager.main.CanInteractOutsideMenu())
		{
			return;
		}
		if ((keyName == "confirm" && !DigitalCursor.main.GetInputHold("leftBumper") && !DigitalCursor.main.GetInputHold("rightBumper")) || overrideKeyName)
		{
			this.ClickOrPress();
			this.ConsiderClick();
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00054E88 File Offset: 0x00053088
	public override void OnPressEnd(string keyName)
	{
		if (!ContextMenuManager.main.CanInteractOutsideMenu())
		{
			return;
		}
		if (!Singleton.Instance.clickOnceToPickupAndAgainToDrop)
		{
			return;
		}
		if (keyName == "confirm" && !DigitalCursor.main.GetInputHold("leftBumper") && !DigitalCursor.main.GetInputHold("rightBumper"))
		{
			base.StartCoroutine(this.StopDrag());
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00054EEB File Offset: 0x000530EB
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.ConsiderClick();
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00054F00 File Offset: 0x00053100
	public void ConsiderClick()
	{
		if (!this.saveManager || this.saveManager.isSavingOrLoading)
		{
			return;
		}
		Debug.Log(string.Format("EventSystem.current = {0}\nEventSystem.current.IsPointerOverGameObject() = {1}\nspriteRenderer = {2}\nspriteRenderer.sortingOrder = {3}", new object[]
		{
			EventSystem.current,
			EventSystem.current.IsPointerOverGameObject(),
			this.spriteRenderer,
			this.spriteRenderer.sortingOrder
		}));
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.controller && EventSystem.current && EventSystem.current.IsPointerOverGameObject() && this.spriteRenderer && this.spriteRenderer.sortingOrder < 2)
		{
			return;
		}
		Debug.Log(string.Format("ContextMenuManager.main.currentState = {0}", ContextMenuManager.main.currentState));
		if (ContextMenuManager.main.currentState != ContextMenuManager.CurrentState.noMenu)
		{
			return;
		}
		PetItem component = base.GetComponent<PetItem>();
		if (component && !component.petMaster.showingInventory)
		{
			component.petMaster.OpenInventory();
			return;
		}
		this.ClickItem(false);
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00055010 File Offset: 0x00053210
	public void ChooseItemForEvent()
	{
		if (!this.gameManager.eventButton || this.gameManager.eventButton.requirement != EventButton.Requirements.specificItemSacrifice)
		{
			if (this.gameFlowManager.selectedItem && this.myItem.canBeComboed)
			{
				if (this.myItem)
				{
					GameFlowManager.main.ConsiderUse(this.myItem, false);
					return;
				}
			}
			else if (this.gameManager.eventButton && Item2.ShareItemTypes(this.gameManager.eventButton.requiredItemType, this.myItem.itemType) && !this.myItem.itemType.Contains(Item2.ItemType.Pet) && (this.gameManager.eventButton.requiredRarities == null || this.gameManager.eventButton.requiredRarities.Count == 0 || this.gameManager.eventButton.requiredRarities.Contains(this.myItem.rarity)))
			{
				if (this.gameManager.destroySelectedItem)
				{
					this.DelayDestroy();
				}
				RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.unlimitedForges);
				if ((this.gameManager.eventButton.requirement == EventButton.Requirements.itemSelectNonForged || this.gameManager.eventButton.requirement == EventButton.Requirements.carvingSelectNonForged) && !runProperty)
				{
					if (this.myItem.GetStatusEffectValue(Item2.ItemStatusEffect.Type.canBeForged) <= 0)
					{
						this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm48"));
						base.StartCoroutine(this.Shake());
						return;
					}
					this.myItem.ChangeStatusEffectValue(Item2.ItemStatusEffect.Type.canBeForged, Item2.ItemStatusEffect.Length.permanent, -1);
				}
				if (this.gameManager.eventButton.requirement == EventButton.Requirements.carvingSelectNonForged && !this.myItem.itemType.Contains(Item2.ItemType.Carving))
				{
					base.StartCoroutine(this.Shake());
					return;
				}
				RandomEventMaster[] array = Object.FindObjectsOfType<RandomEventMaster>();
				for (int i = 0; i < array.Length; i++)
				{
					Object.Destroy(array[i].gameObject);
				}
				this.gameManager.eventButton.sacrificedItem = this.myItem;
				this.gameManager.eventButton.sacrificeAccepted = true;
				this.gameManager.FinishReorganizeButton();
				if (this.gameManager.eventButton && this.gameManager.eventButton.randomEventMaster)
				{
					this.gameManager.eventButton.randomEventMaster.gameObject.SetActive(true);
					return;
				}
			}
			else
			{
				base.StartCoroutine(this.Shake());
			}
			return;
		}
		if (Item2.GetDisplayName(this.gameManager.eventButton.requiredItem.name) != Item2.GetDisplayName(base.gameObject.name))
		{
			base.StartCoroutine(this.Shake());
			return;
		}
		if (this.gameManager.destroySelectedItem)
		{
			this.DelayDestroy();
		}
		this.gameManager.eventButton.sacrificedItem = this.myItem;
		this.gameManager.eventButton.sacrificeAccepted = true;
		this.gameManager.FinishReorganizeButton();
		this.gameManager.eventButton.randomEventMaster.gameObject.SetActive(true);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00055334 File Offset: 0x00053534
	public void ClickItem(bool dontShowNegative = false)
	{
		if (!this.myItem || !this.gameManager || this.gameManager.inSpecialReorg || this.gameManager.dead || !this.moveToItemTransform || (this.gameManager.viewingEvent && !this.gameManager.inSpecialReorg && this.gameManager.inventoryPhase != GameManager.InventoryPhase.choose))
		{
			return;
		}
		if (!SingleUI.FreeToInteractWith(base.transform))
		{
			return;
		}
		if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.choose)
		{
			this.ChooseItemForEvent();
			return;
		}
		if (!this.inGrid)
		{
			return;
		}
		PetItem2 component = base.GetComponent<PetItem2>();
		if (component && !component.combatPet)
		{
			this.gameFlowManager.ConsiderSummon(component);
			return;
		}
		CR8Manager instance = CR8Manager.instance;
		if (this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombat) || this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition))
		{
			return;
		}
		if (this.myItem.CanBeUsedActive(false, this.myItem.costs, true, false, null, false) && this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn && base.GetComponent<EnergyEmitter>())
		{
			if (this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.runsAutomaticallyOnCoreUse) && !instance.isRunning)
			{
				instance.exit = false;
				GameFlowManager.main.StartCoroutine(instance.StartTurn(false, base.gameObject));
				return;
			}
			if (this.myItem.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeUsedByCR8Directly) && !instance.isRunning)
			{
				instance.exit = false;
				GameFlowManager.main.StartCoroutine(instance.StartTurn(false, base.gameObject));
				return;
			}
		}
		Item2 component2 = base.GetComponent<Item2>();
		if ((this.player.characterName != Character.CharacterName.CR8 && !this.inPouch) || (component2.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeUsedByCR8Directly) && (!instance || !instance.isRunning)))
		{
			GameFlowManager.main.ConsiderUse(this.myItem, dontShowNegative);
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00055514 File Offset: 0x00053714
	public override void OnCursorStart()
	{
		if (!this.saveManager || this.saveManager.isSavingOrLoading)
		{
			return;
		}
		if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		if (this.isTransiting)
		{
			return;
		}
		if (this.animator)
		{
			this.animator.enabled = false;
		}
		PetItem component = base.GetComponent<PetItem>();
		if (component)
		{
			component.petMaster.ShowOutline();
		}
		ItemTogglerForController.main.SetItem(this.myItem);
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x000555A4 File Offset: 0x000537A4
	private void HoveringAnimation()
	{
		if (!this.mousePreviewRenderer)
		{
			return;
		}
		this.mousePreviewRenderer.sortingOrder = this.spriteRenderer.sortingOrder;
		this.mousePreviewRenderer.sharedMaterial = this.spriteRenderer.sharedMaterial;
		this.mousePreview.localPosition = new Vector3(0f, 0f, -1f);
		this.mousePreview.gameObject.SetActive(true);
		this.mousePreviewRenderer.enabled = true;
		this.mousePreviewRenderer.sprite = this.spriteRenderer.sprite;
		this.mousePreviewRenderer.flipX = this.spriteRenderer.flipX;
		this.mousePreviewRenderer.color = new Color(0.6f, 0.6f, 1f, 0.6f);
		this.mousePreview.localScale = Vector3.one * (1f + Mathf.Abs(this.hoverTime));
		this.hoverTime += Time.deltaTime * 0.25f;
		if (this.hoverTime > 0.1f)
		{
			this.hoverTime = -0.1f;
		}
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x000556D0 File Offset: 0x000538D0
	public override void OnCursorHold()
	{
		if (!this.gameManager || !this.saveManager || this.saveManager.isSavingOrLoading || !this.myItem || this.myItem.destroyed || this.isTransiting || this.isStoppingDrag)
		{
			return;
		}
		float num;
		if (ItemPouchMaster.IsBlocked(DigitalCursor.main.transform.position, out num) && num < base.transform.position.z)
		{
			return;
		}
		if (EventSystem.current && EventSystem.current.IsPointerOverGameObject() && this.spriteRenderer.sortingOrder < 2 && !this.myItem.itemType.Contains(Item2.ItemType.Carving))
		{
			return;
		}
		if (this.isMovingItem || this.isTransiting || (this.gameManager && this.gameManager.viewingEvent && this.moveToItemTransform && this.spriteRenderer.sortingOrder < 2 && !this.gameManager.inSpecialReorg && this.gameManager.inventoryPhase != GameManager.InventoryPhase.choose))
		{
			return;
		}
		if (this.gameManager.inventoryPhase == GameManager.InventoryPhase.notInteractable)
		{
			return;
		}
		if (SingleUI.IsViewingPopUp() && !base.transform.IsChildOf(SingleUI.GetSingleUI().transform))
		{
			return;
		}
		ItemTogglerForController.main.SetItemIfUnset(this.myItem);
		if (Input.GetMouseButton(0) || DigitalCursor.main.GetInputDown("confirm"))
		{
			if (!this.isDragging)
			{
				this.RemoveCard();
				return;
			}
		}
		else
		{
			if (this.inGrid && !this.isDragging && (!this.gameManager || !this.gameManager.draggingItem) && !this.isMovingItem && !this.isShowingHighlights)
			{
				this.AddHighlights();
			}
			this.HoveringAnimation();
			this.timeToDisplayCard += Time.deltaTime;
			if (this.timeToDisplayCard > 0.25f && !this.previewCard && !this.levelUpManager.levelingUp && ContextMenuManager.main.currentState == ContextMenuManager.CurrentState.noMenu && (!this.gameFlowManager.isCheckingEffects || this.gameManager.inSpecialReorg) && (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor || (!this.everShownCard && !this.isDragging)))
			{
				this.RemoveCard();
				this.ShowCard(null);
				this.everShownCard = true;
				return;
			}
			if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller && DigitalCursor.main.GetInputDown("contextMenu"))
			{
				this.RemoveCard();
				this.previewCard = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
				this.ApplyCardToItem(this.previewCard, null, null, false);
				this.everShownCard = true;
			}
		}
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x000559A8 File Offset: 0x00053BA8
	public GameObject ShowCard(GameObject pretendParent = null)
	{
		this.RemoveCard();
		this.previewCard = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
		this.ApplyCardToItem(this.previewCard, null, null, false);
		if (pretendParent)
		{
			this.previewCard.GetComponent<Card>().SetParent(pretendParent);
		}
		return this.previewCard;
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00055A18 File Offset: 0x00053C18
	public GameObject ShowCardDirect(Item2 itemOverride, Sprite spriteOverride)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
		this.ApplyCardToItem(gameObject, itemOverride, spriteOverride, false);
		return gameObject;
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00055A5C File Offset: 0x00053C5C
	public void ApplyCardToItem(GameObject card, Item2 itemOverride = null, Sprite spriteOverride = null, bool headless = false)
	{
		if (!this.spriteRenderer)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
		if (!this.myItem)
		{
			this.myItem = base.GetComponent<Item2>();
		}
		card.transform.SetAsFirstSibling();
		Card componentInChildren = card.GetComponentInChildren<Card>();
		if (this.curseSymbol && this.curseSymbol.sprite)
		{
			componentInChildren.iconSprite.sprite = this.curseSymbol.sprite;
			componentInChildren.iconSprite.color = new Color(0.6f, 0f, 0f);
		}
		else if (spriteOverride)
		{
			componentInChildren.iconSprite.sprite = spriteOverride;
		}
		else
		{
			componentInChildren.iconSprite.sprite = this.spriteRenderer.sprite;
		}
		componentInChildren.iconSprite.SetNativeSize();
		componentInChildren.iconSprite.rectTransform.sizeDelta = componentInChildren.iconSprite.rectTransform.sizeDelta.normalized * 170f;
		if (this.size.y > this.size.x)
		{
			componentInChildren.iconSprite.rectTransform.rotation = Quaternion.Euler(0f, 0f, -90f);
			componentInChildren.iconSprite.rectTransform.sizeDelta *= 1.2f;
		}
		Item2.GetAllEffectTotals();
		if (itemOverride)
		{
			componentInChildren.GetDescriptions(itemOverride, base.gameObject, headless);
			return;
		}
		componentInChildren.GetDescriptions(this.myItem, base.gameObject, headless);
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00055BFC File Offset: 0x00053DFC
	public override void OnCursorEnd()
	{
		this.everShownCard = false;
		ItemTogglerForController.main.ResetItem();
		PetItem component = base.GetComponent<PetItem>();
		if (component)
		{
			component.petMaster.HideOutline();
		}
		if (!this.saveManager || this.saveManager.isSavingOrLoading)
		{
			return;
		}
		if (this.isMovingItem)
		{
			return;
		}
		if (!this.isDragging && (!this.gameManager || !this.gameManager.draggingItem))
		{
			ItemMovement.RemoveAllHighlights();
		}
		this.RemoveCard();
		if (this.isTransiting || this.isAnimating)
		{
			return;
		}
		if (!this.isDragging)
		{
			this.mousePreview.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00055CB4 File Offset: 0x00053EB4
	public static void RemoveAnyItemCard()
	{
		foreach (ItemMovement itemMovement in ItemMovement.allItems)
		{
			itemMovement.RemoveCard();
		}
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00055D04 File Offset: 0x00053F04
	public static void RemoveAllCards()
	{
		foreach (ItemMovement itemMovement in ItemMovement.allItems)
		{
			itemMovement.RemoveCard();
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00055D54 File Offset: 0x00053F54
	public void RemoveCard()
	{
		this.timeToDisplayCard = 0f;
		if (!this.previewCard)
		{
			return;
		}
		this.previewCard.GetComponentInChildren<Card>().EndHover();
		DigitalCursor.main.UpdateContextControls();
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00055D89 File Offset: 0x00053F89
	public IEnumerator EndItemAnimation(float time)
	{
		yield return new WaitForSeconds(time);
		this.animator.enabled = false;
		this.isAnimating = false;
		float timeT = 0f;
		Color startColor = this.mousePreviewRenderer.color;
		Color spriteRendererColor = this.spriteRenderer.color;
		while (timeT < 0.25f)
		{
			this.mousePreviewRenderer.color = Color.Lerp(startColor, spriteRendererColor, timeT / 0.25f);
			timeT += Time.deltaTime;
			yield return null;
		}
		this.mousePreview.gameObject.SetActive(false);
		if (this.myItem && !this.myItem.destroyed)
		{
			this.spriteRenderer.enabled = true;
		}
		yield break;
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00055D9F File Offset: 0x00053F9F
	public IEnumerator PlayAnimation()
	{
		if (this.isDragging || this.isStoppingDrag || !this.myItem || this.myItem.destroyed)
		{
			yield break;
		}
		this.mousePreview.gameObject.SetActive(true);
		this.mousePreview.localPosition = new Vector3(0f, 0f, -1f);
		this.mousePreviewRenderer.color = new Color(1f, 1f, 1f, 1f);
		this.animator.enabled = true;
		this.animator.Play("itemUsed", 0, 0f);
		yield break;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00055DAE File Offset: 0x00053FAE
	public IEnumerator ModifiedAnimation()
	{
		if (this.isDragging || this.isStoppingDrag || !this.myItem || this.myItem.destroyed)
		{
			yield break;
		}
		this.mousePreview.gameObject.SetActive(true);
		this.mousePreview.localPosition = new Vector3(0f, 0f, -1f);
		this.mousePreviewRenderer.color = this.spriteRenderer.color;
		this.animator.enabled = true;
		this.animator.gameObject.SetActive(true);
		this.animator.Play("itemModified", 0, 0f);
		bool flag = this.isDragging;
		yield break;
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x00055DBD File Offset: 0x00053FBD
	public IEnumerator Shake()
	{
		if (this.animator)
		{
			this.animator.enabled = true;
			this.mousePreviewRenderer.enabled = true;
			this.mousePreview.localScale = Vector3.one;
			this.animator.Play("itemShake", 0, 0f);
		}
		yield break;
	}

	// Token: 0x04000602 RID: 1538
	private bool wasInPouch;

	// Token: 0x04000603 RID: 1539
	private static List<ItemMovement> allItems = new List<ItemMovement>();

	// Token: 0x04000604 RID: 1540
	private float isStoppingDragTimer = -1f;

	// Token: 0x04000605 RID: 1541
	private float pauseTime;

	// Token: 0x04000606 RID: 1542
	private bool isStoppingDrag;

	// Token: 0x04000607 RID: 1543
	public GameObject parentAltar;

	// Token: 0x04000608 RID: 1544
	private bool isShowingHighlights;

	// Token: 0x04000609 RID: 1545
	[HideInInspector]
	public bool satchelLocked;

	// Token: 0x0400060A RID: 1546
	public bool isDragging;

	// Token: 0x0400060B RID: 1547
	private Vector3 startPosition;

	// Token: 0x0400060C RID: 1548
	private Quaternion startRotation;

	// Token: 0x0400060D RID: 1549
	[HideInInspector]
	public Vector2 difference;

	// Token: 0x0400060E RID: 1550
	[SerializeField]
	public Transform mouseParticleSystem;

	// Token: 0x0400060F RID: 1551
	[SerializeField]
	public Transform itemBackgroundBordersParent;

	// Token: 0x04000610 RID: 1552
	[SerializeField]
	public Transform itemHighlightBordersParent;

	// Token: 0x04000611 RID: 1553
	[SerializeField]
	public Transform mousePreview;

	// Token: 0x04000612 RID: 1554
	public SpriteRenderer mousePreviewRenderer;

	// Token: 0x04000613 RID: 1555
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000614 RID: 1556
	private BoxCollider2D[] boxCollider2Ds = new BoxCollider2D[0];

	// Token: 0x04000615 RID: 1557
	[SerializeField]
	public GameObject cardPrefab;

	// Token: 0x04000616 RID: 1558
	[SerializeField]
	private GameObject itemBackgroundBorderPrefab;

	// Token: 0x04000617 RID: 1559
	[SerializeField]
	private GameObject itemHighlightParentPrefab;

	// Token: 0x04000618 RID: 1560
	[SerializeField]
	private GameObject itemHighlightPrefab;

	// Token: 0x04000619 RID: 1561
	private float timeToDisplayCard;

	// Token: 0x0400061B RID: 1563
	private GameManager gameManager;

	// Token: 0x0400061C RID: 1564
	private GameFlowManager gameFlowManager;

	// Token: 0x0400061D RID: 1565
	public Item2 myItem;

	// Token: 0x0400061E RID: 1566
	public bool inGrid;

	// Token: 0x0400061F RID: 1567
	public bool inPouch;

	// Token: 0x04000620 RID: 1568
	private bool isHovering;

	// Token: 0x04000621 RID: 1569
	private float hoverTime;

	// Token: 0x04000623 RID: 1571
	public bool isAnimating;

	// Token: 0x04000624 RID: 1572
	public bool isTransiting;

	// Token: 0x04000625 RID: 1573
	private LevelUpManager levelUpManager;

	// Token: 0x04000626 RID: 1574
	[SerializeField]
	public Vector2 size;

	// Token: 0x04000627 RID: 1575
	[SerializeField]
	private GameObject itemCreationParticlesPrefab;

	// Token: 0x04000628 RID: 1576
	[SerializeField]
	public SpriteRenderer curseSymbol;

	// Token: 0x04000629 RID: 1577
	[SerializeField]
	public SpriteRenderer mouseCurseSymbol;

	// Token: 0x0400062A RID: 1578
	[NonSerialized]
	public bool moveToItemTransform = true;

	// Token: 0x0400062B RID: 1579
	public bool returnsToOutOfInventoryPosition;

	// Token: 0x0400062C RID: 1580
	public Vector3 outOfInventoryPosition;

	// Token: 0x0400062D RID: 1581
	public Quaternion outOfInventoryRotation;

	// Token: 0x0400062E RID: 1582
	[NonSerialized]
	private float bounceTime;

	// Token: 0x0400062F RID: 1583
	[SerializeField]
	private Transform chargesParent;

	// Token: 0x04000630 RID: 1584
	[SerializeField]
	private Sprite[] chargesSprites;

	// Token: 0x04000631 RID: 1585
	private SaveManager saveManager;

	// Token: 0x04000632 RID: 1586
	public bool seenBefore;

	// Token: 0x04000633 RID: 1587
	private Player player;

	// Token: 0x04000634 RID: 1588
	private ItemMovementManager itemMovementManager;

	// Token: 0x04000635 RID: 1589
	public bool pouchWasOpenAtStart;

	// Token: 0x04000636 RID: 1590
	private bool everMovedDuringThisDrag;

	// Token: 0x04000637 RID: 1591
	[SerializeField]
	public Vector2 highestLeftGridPosition;

	// Token: 0x04000638 RID: 1592
	private float timeOnThisSpace;

	// Token: 0x04000639 RID: 1593
	private bool considererPouchHere;

	// Token: 0x0400063A RID: 1594
	private float dragginForTime;

	// Token: 0x0400063B RID: 1595
	private bool updatedControls;

	// Token: 0x0400063C RID: 1596
	[SerializeField]
	private bool logPosition;

	// Token: 0x0400063D RID: 1597
	private bool started;

	// Token: 0x0400063E RID: 1598
	private CanvasGroup canvasGroup;

	// Token: 0x0400063F RID: 1599
	private bool wasRotated;

	// Token: 0x04000640 RID: 1600
	private GridObject gridObject;

	// Token: 0x04000641 RID: 1601
	private Coroutine removeAtEndCoroutine;

	// Token: 0x04000642 RID: 1602
	private Vector3 mySpot;

	// Token: 0x04000643 RID: 1603
	private Quaternion myStoredRotation;

	// Token: 0x04000644 RID: 1604
	public List<ItemMovement> itemsReplacedByThisCurse;

	// Token: 0x04000645 RID: 1605
	private ItemMovement curseThatMovedThisAway;

	// Token: 0x04000646 RID: 1606
	public bool isPlayedCard;

	// Token: 0x04000647 RID: 1607
	private static List<ItemMovement> itemsThatCannotBeSwappedAgain = new List<ItemMovement>();

	// Token: 0x04000648 RID: 1608
	private Coroutine moveOutOfGridRoutine;

	// Token: 0x04000649 RID: 1609
	private bool liftedFromGrid;

	// Token: 0x0400064A RID: 1610
	[SerializeField]
	public GameObject myParticles;

	// Token: 0x0400064B RID: 1611
	private bool isMovingItem;

	// Token: 0x0400064C RID: 1612
	private bool everShownCard;

	// Token: 0x0400064D RID: 1613
	[SerializeField]
	public Animator animator;
}
