using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class LevelUpManager : MonoBehaviour
{
	// Token: 0x06000667 RID: 1639 RVA: 0x0003E500 File Offset: 0x0003C700
	private void Awake()
	{
		if (LevelUpManager.main == null)
		{
			LevelUpManager.main = this;
			return;
		}
		if (LevelUpManager.main != this)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0003E52E File Offset: 0x0003C72E
	private void OnDestroy()
	{
		if (LevelUpManager.main == this)
		{
			LevelUpManager.main = null;
		}
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0003E543 File Offset: 0x0003C743
	private void Start()
	{
		this.time = 0f;
		this.gameManager = GameManager.main;
		this.player = Player.main;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0003E568 File Offset: 0x0003C768
	public bool CanLevelUp()
	{
		return ((this.player.chosenCharacter.levelUps.Count > this.player.level + 1 && this.player.experience >= this.player.chosenCharacter.levelUps[this.player.level + 1].xpRequired) || this.spacesSaved > 0) && !this.levelingUp;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0003E5E4 File Offset: 0x0003C7E4
	private void Update()
	{
		bool flag = this.levelingUp;
		if (this.CanLevelUp() && this.gameManager.InEmptyRoom(false))
		{
			this.levelUpPopUpButton.gameObject.SetActive(true);
		}
		else
		{
			this.levelUpPopUpButton.gameObject.SetActive(false);
		}
		if (!this.levelingUp)
		{
			return;
		}
		this.time += Time.deltaTime;
		if (this.time > 2f)
		{
			this.time = 0f;
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0003E668 File Offset: 0x0003C868
	public void LevelUpButton()
	{
		if (this.levelingUp)
		{
			return;
		}
		SoundManager.main.MuteAllSongs();
		SoundManager.main.PlaySongSudden("bph_game_level_up_loop", 0f, 0f, true);
		this.levelingUp = true;
		this.player.IncrementLevel();
		base.StartCoroutine(this.LevelUpOutside());
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0003E6C1 File Offset: 0x0003C8C1
	public IEnumerator LevelUpOutside()
	{
		this.gameManager.ShowInventory();
		yield return new WaitForSeconds(0.5f);
		yield return this.LevelUp(0);
		while (this.levelingUp)
		{
			yield return null;
		}
		this.gameManager.ClearLevelUp();
		yield break;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0003E6D0 File Offset: 0x0003C8D0
	private IEnumerator ReSizeAfterDelay()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.ResizeAllBackpacks();
		yield break;
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0003E6E0 File Offset: 0x0003C8E0
	public void EndLevelUp()
	{
		if (this.gameManager.dungeonLevel.currentFloor == DungeonLevel.Floor.boss && !this.gameManager.EnemiesInDungeon())
		{
			SoundManager.main.PlayOrContinueSong("bph_game_roaming_after_boss", 0f);
		}
		else
		{
			SoundManager.main.PlayOrContinueSong(this.gameManager.dungeonLevel.levelSong);
		}
		this.particles.Stop();
		bool flag = false;
		if (!this.stuffToHideDuringLevelUp[0].activeInHierarchy)
		{
			flag = true;
		}
		foreach (GameObject gameObject in this.stuffToHideDuringLevelUp)
		{
			gameObject.SetActive(true);
		}
		if (flag)
		{
			this.stuffToHideDuringLevelUp[0].GetComponentInChildren<Animator>().Play("bigAnnouncement", 0, 0f);
		}
		base.StartCoroutine(this.ReSizeAfterDelay());
		this.spacesSaved = this.numberToUnlock;
		foreach (GridBlock gridBlock in Object.FindObjectsOfType<GridBlock>())
		{
			if (gridBlock.isAnchored)
			{
				this.chosenBlockPrefabs = new List<GameObject>();
			}
			foreach (object obj in gridBlock.transform)
			{
				SpriteRenderer component = ((Transform)obj).GetComponent<SpriteRenderer>();
				if (component)
				{
					this.gameManager.AddParticles(component.transform.position + Vector3.back, component, null);
				}
			}
			Object.Destroy(gridBlock.gameObject);
		}
		for (int j = 0; j < this.gridParent.transform.childCount; j++)
		{
			Transform child = this.gridParent.GetChild(j);
			if (child.CompareTag("GridSquare"))
			{
				SpriteRenderer component2 = child.GetComponent<SpriteRenderer>();
				if (component2.color == Color.red)
				{
					this.gameManager.AddParticles(child.transform.position + Vector3.back, component2, null);
					Object.Destroy(child.gameObject);
				}
			}
		}
		if (this.rewardNumber < this.player.chosenCharacter.levelUps[this.player.level].rewards.Count - 1)
		{
			base.StartCoroutine(this.LevelUp(this.rewardNumber + 1));
			return;
		}
		this.levelUpText.GetComponent<Animator>().SetBool("ToggleOff", true);
		this.levelingUp = false;
		this.button.GetComponent<Animator>().Play("Out");
		this.gameManager.StartCoroutine(this.gameManager.HidePromptText(null, -105f));
		foreach (GridExtension gridExtension in Object.FindObjectsOfType<GridExtension>())
		{
			if (gridExtension.selected)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.gridSquarePrefab, gridExtension.transform.position, Quaternion.identity, this.gridParent);
				gameObject2.transform.localPosition = Vector3Int.RoundToInt(gameObject2.transform.localPosition);
				gameObject2.transform.localPosition = new Vector3(gameObject2.transform.localPosition.x, gameObject2.transform.localPosition.y, 0f);
				GridSquare component3 = gameObject2.GetComponent<GridSquare>();
				component3.isRunic = false;
				component3.SetColor();
			}
			Object.Destroy(gridExtension.gameObject);
		}
		this.fadeOutUnnecessary.GetComponent<Animator>().Play("fadeOutPartial", 0, 0f);
		SpriteRenderer[] componentsInChildren = this.inventoryParent.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].sortingOrder = 0;
		}
		ItemBorderBackground.SetAllColors();
		this.gameManager.StartCoroutine(this.gameManager.MoveOverTime(this.inventoryParent, this.gameManager.inventoryStartPosition, 0.3f));
		Object.FindObjectOfType<ItemMovementManager>().CheckForMovementPublic();
		if (this.player.characterName == Character.CharacterName.Satchel && PocketManager.main)
		{
			PocketManager.main.DeterminePockets();
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0003EB2C File Offset: 0x0003CD2C
	private void HideAnnouncements()
	{
		foreach (GameObject gameObject in this.stuffToHideDuringLevelUp)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0003EB80 File Offset: 0x0003CD80
	public void PlayParticles()
	{
		this.particles.Play();
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0003EB8D File Offset: 0x0003CD8D
	public IEnumerator LevelUp(int num)
	{
		this.particles.Play();
		DigitalCursor.main.FollowGameElement(GameObject.FindGameObjectWithTag("Inventory").transform, true);
		this.rewardNumber = num;
		if (this.player.characterName == Character.CharacterName.Satchel)
		{
			Object.FindObjectOfType<TutorialManager>().ConsiderTutorial("satchelTutorial");
		}
		if (Singleton.Instance.isDemo)
		{
			int num2 = Mathf.RoundToInt(this.player.chosenCharacter.endingBagSizeDemo.x);
			int num3 = Mathf.RoundToInt(this.player.chosenCharacter.endingBagSizeDemo.y);
			this.maxSizeX = num2;
			this.maxSizeY = num3;
		}
		else
		{
			int num4 = Mathf.RoundToInt(this.player.chosenCharacter.endingBagSize.x);
			int num5 = Mathf.RoundToInt(this.player.chosenCharacter.endingBagSize.y);
			RunType.RunProperty runProperty = RunTypeManager.CheckForRunProperty(RunType.RunProperty.Type.limitBackpackWidth);
			if (runProperty != null)
			{
				num4 = (runProperty.value - 3) / 2;
			}
			this.maxSizeX = num4;
			this.maxSizeY = num5;
		}
		this.levelUpText.SetActive(true);
		this.levelUpText.GetComponent<Animator>().SetBool("ToggleOff", false);
		this.levelingUp = true;
		this.gameManager.inventoryPhase = GameManager.InventoryPhase.locked;
		yield return new WaitForFixedUpdate();
		Character.LevelUp.Reward reward = this.player.chosenCharacter.levelUps[this.player.level].rewards[this.rewardNumber];
		if (reward.rewardType == Character.LevelUp.Reward.RewardType.NewSpace)
		{
			this.HideAnnouncements();
			this.button.SetActive(true);
			this.SetupSpaces();
			Object.FindObjectsOfType<GridExtension>();
			this.numberToUnlock = this.spacesSaved;
			SpriteRenderer[] array = this.inventoryParent.GetComponentsInChildren<SpriteRenderer>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sortingOrder = 1;
			}
			this.gameManager.StartCoroutine(this.gameManager.MoveOverTime(this.inventoryParent, new Vector3(0f, 0.75f, this.inventoryParent.position.z), 0.3f));
			this.fadeOutUnnecessary.SetActive(true);
			this.fadeOutUnnecessary.GetComponent<Animator>().Play("fadeInPartial", 0, 0f);
			this.gameManager.promptText.text = LangaugeManager.main.GetTextByKey("gm42").Replace("/x", this.numberToUnlock.ToString() ?? "");
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(null, 95f));
		}
		else if (reward.rewardType == Character.LevelUp.Reward.RewardType.Pets)
		{
			yield return Object.FindObjectOfType<PetManager>().StartSelection();
			Object.FindObjectOfType<LevelUpManager>().ResizeAllBackpacks();
			this.EndLevelUp();
			this.gameManager.StartSimpleLimitedItemGetPeriod(1);
			this.gameManager.ClearLevelUp();
			this.gameManager.SpawnGoldAndMana();
		}
		else if (reward.rewardType == Character.LevelUp.Reward.RewardType.SpaceBlock)
		{
			this.HideAnnouncements();
			this.button.SetActive(true);
			this.SetupBlocks();
			GridExtension[] array2 = Object.FindObjectsOfType<GridExtension>();
			this.numberToUnlock = Mathf.Min(reward.rewardValue, array2.Length);
			this.numberToUnlock = this.spacesSaved;
			SpriteRenderer[] array = this.inventoryParent.GetComponentsInChildren<SpriteRenderer>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sortingOrder = 1;
			}
			this.gameManager.StartCoroutine(this.gameManager.MoveOverTime(this.inventoryParent, new Vector3(0f, 0.75f, this.inventoryParent.position.z), 0.3f));
			this.fadeOutUnnecessary.SetActive(true);
			this.fadeOutUnnecessary.GetComponent<Animator>().Play("fadeInPartial", 0, 0f);
			this.gameManager.promptText.text = LangaugeManager.main.GetTextByKey("gm42").Replace("/x", this.numberToUnlock.ToString() ?? "");
			this.gameManager.StartCoroutine(this.gameManager.ShowPromptText(null, 95f));
		}
		else if (reward.rewardType == Character.LevelUp.Reward.RewardType.Component)
		{
			this.EndLevelUp();
			Object.FindObjectOfType<CR8Manager>().SpawnComponenets(5);
			this.gameManager.StartSimpleLimitedItemGetPeriod(1);
			this.gameManager.ClearLevelUp();
		}
		yield break;
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0003EBA4 File Offset: 0x0003CDA4
	public void SetupBlocks()
	{
		List<GameObject> list = new List<GameObject>();
		if (this.chosenBlockPrefabs.Count > 0)
		{
			list = new List<GameObject>(this.chosenBlockPrefabs);
			for (int i = 0; i < 2; i++)
			{
				int num = Random.Range(0, list.Count);
				Object.Instantiate<GameObject>(list[num], new Vector3(-5.5f + (float)(11 * i), -2.5f, 0f), Quaternion.identity, GameObject.FindGameObjectWithTag("GridParent").transform);
				list.RemoveAt(num);
			}
		}
		else
		{
			list = new List<GameObject>(this.gridBlockPrefabs);
			for (int j = 0; j < 2; j++)
			{
				int num2 = Random.Range(0, list.Count);
				Object.Instantiate<GameObject>(list[num2], new Vector3(-5.5f + (float)(11 * j), -2.5f, 0f), Quaternion.identity, GameObject.FindGameObjectWithTag("GridParent").transform);
				this.chosenBlockPrefabs.Add(list[num2]);
				list.RemoveAt(num2);
			}
		}
		this.ResizeAllBackpacks();
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0003ECB4 File Offset: 0x0003CEB4
	public void ChangeSpaces(int num)
	{
		this.numberToUnlock += num;
		this.gameManager.promptText.text = LangaugeManager.main.GetTextByKey("gm42").Replace("/x", this.numberToUnlock.ToString() ?? "");
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0003ED0C File Offset: 0x0003CF0C
	public void Select()
	{
		this.ChangeSpaces(-1);
		this.SetupSpaces();
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0003ED1B File Offset: 0x0003CF1B
	public void Deselect()
	{
		this.ChangeSpaces(1);
		this.SetupSpaces();
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0003ED2C File Offset: 0x0003CF2C
	private bool isValid(PathFinding.Location location, bool isDest)
	{
		foreach (Vector2 vector in new Vector2[]
		{
			new Vector2(-1f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, -1f),
			new Vector2(0f, 1f)
		})
		{
			foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(location.position + vector, Vector2.zero))
			{
				GridExtension component = raycastHit2D.collider.GetComponent<GridExtension>();
				if (raycastHit2D.collider.GetComponent<GridSquare>() || (component && component.selected && component.isConnected))
				{
					foreach (RaycastHit2D raycastHit2D2 in Physics2D.RaycastAll(location.position, Vector2.zero))
					{
						GridExtension component2 = raycastHit2D2.collider.GetComponent<GridExtension>();
						if (component2)
						{
							component2.isConnected = true;
						}
					}
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0003EE7C File Offset: 0x0003D07C
	private void SetupSpaces()
	{
		foreach (GridExtension gridExtension in GridExtension.gridExtensions.ToArray())
		{
			if (!gridExtension.selected)
			{
				gridExtension.transform.SetParent(null);
				Object.Destroy(gridExtension.gameObject);
			}
			else
			{
				gridExtension.isConnected = false;
			}
		}
		Transform child = GameObject.FindGameObjectWithTag("GridParent").transform.GetChild(0);
		List<PathFinding.Location> list = new List<PathFinding.Location>();
		PathFinding.FindAvailableSpaces(child.position, 24, new Func<PathFinding.Location, bool, bool>(this.isValid), out list, null);
		PathFinding.FindAvailableSpaces(child.position, 24, new Func<PathFinding.Location, bool, bool>(this.isValid), out list, null);
		foreach (GridExtension gridExtension2 in GameObject.FindGameObjectWithTag("GridParent").GetComponentsInChildren<GridExtension>())
		{
			if (!gridExtension2.isConnected)
			{
				gridExtension2.transform.SetParent(null);
				Object.Destroy(gridExtension2.gameObject);
				if (gridExtension2.runicType == GridExtension.RunicType.isRunicNotOnGrid)
				{
					this.ChangeSpaces(2);
				}
				else
				{
					this.ChangeSpaces(1);
				}
			}
		}
		foreach (PathFinding.Location location in list)
		{
			Vector2 vector = GameObject.FindGameObjectWithTag("GridParent").transform.InverseTransformPoint(location.position);
			if (vector.x >= (float)((this.maxSizeX + 1) * -1) && vector.x <= (float)(this.maxSizeX + 1) && vector.y >= (float)(this.maxSizeY * -1) && vector.y <= (float)(this.maxSizeY + 1))
			{
				bool flag = false;
				bool flag2 = false;
				foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(location.position, Vector2.zero))
				{
					GridSquare component = raycastHit2D.collider.GetComponent<GridSquare>();
					if (component)
					{
						flag2 = true;
						if (!component.isRunic || this.player.characterName != Character.CharacterName.Tote)
						{
							flag = true;
							break;
						}
					}
					if (raycastHit2D.collider.GetComponent<GridExtension>())
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.gridExtensionPrefab, location.position, Quaternion.identity, this.gridParent);
					gameObject.transform.localPosition = Vector3Int.RoundToInt(gameObject.transform.localPosition);
					if (flag2)
					{
						gameObject.GetComponent<GridExtension>().MakeRunic();
					}
				}
			}
		}
		this.ResizeAllBackpacks();
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0003F134 File Offset: 0x0003D334
	public void ResizeAllBackpacks()
	{
		foreach (ModularBackpack modularBackpack in Object.FindObjectsOfType<ModularBackpack>())
		{
			this.ResizeBackpack(modularBackpack);
		}
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0003F160 File Offset: 0x0003D360
	public void ResizeBackpack(ModularBackpack modBack)
	{
		if (!this.player || !this.player.chosenCharacter)
		{
			return;
		}
		Transform transform = modBack.gridParent;
		this.topBackpackSprite = modBack.topRenderer;
		this.leftBackpackSprite = modBack.leftRenderer;
		this.rightBackpackSprite = modBack.rightRenderer;
		this.bottomBackpackSprite = modBack.bottomRenderer;
		this.backgroundBackpackSprite = modBack.backgroundRenderer;
		float num = 999f;
		float num2 = -999f;
		for (int i = this.maxSizeY * -1; i <= this.maxSizeY; i++)
		{
			List<Transform> rowOfGrid = this.GetRowOfGrid((float)i, true, transform);
			Transform leftMostGrid = this.GetLeftMostGrid(rowOfGrid, transform);
			if (leftMostGrid && leftMostGrid.localPosition.x < num)
			{
				num = leftMostGrid.localPosition.x;
			}
			Transform rightMostGrid = this.GetRightMostGrid(rowOfGrid, transform);
			if (rightMostGrid && rightMostGrid.localPosition.x > num2)
			{
				num2 = rightMostGrid.localPosition.x;
			}
		}
		if (this.player.characterName == Character.CharacterName.Satchel)
		{
			num2 = 5f;
			num = -5f;
		}
		float num3 = num2 + Mathf.Abs(num);
		float num4 = (num + num2) / 2f;
		this.topBackpackSprite.transform.localPosition = new Vector2(num4, this.topBackpackSprite.transform.localPosition.y);
		this.bottomBackpackSprite.transform.localPosition = new Vector3(num4, this.bottomBackpackSprite.transform.localPosition.y, -1f);
		this.topBackpackSprite.size = new Vector2(num3 + 1f, this.topBackpackSprite.size.y);
		this.bottomBackpackSprite.size = new Vector2(num3 + 1.25f, this.bottomBackpackSprite.size.y);
		this.leftBackpackSprite.transform.localPosition = new Vector2(num - 1.5f, this.leftBackpackSprite.transform.localPosition.y);
		this.rightBackpackSprite.transform.localPosition = new Vector2(num2 + 1.5f, this.rightBackpackSprite.transform.localPosition.y);
		float num5 = 999f;
		float num6 = -999f;
		for (int j = this.maxSizeX * -1; j <= this.maxSizeX; j++)
		{
			List<Transform> columnOfGrid = this.GetColumnOfGrid((float)j, true, transform);
			Transform bottomMostGrid = this.GetBottomMostGrid(columnOfGrid, transform);
			if (bottomMostGrid && bottomMostGrid.localPosition.y < num5)
			{
				num5 = bottomMostGrid.localPosition.y;
			}
			Transform topMostGrid = this.GetTopMostGrid(columnOfGrid, transform);
			if (topMostGrid && topMostGrid.localPosition.y > num6)
			{
				num6 = topMostGrid.localPosition.y;
			}
		}
		if (this.player.characterName == Character.CharacterName.Satchel)
		{
			num6 = 2f;
			num5 = -2f;
		}
		float num7 = num6 + Mathf.Abs(num5);
		float num8 = (num5 + num6) / 2f;
		this.rightBackpackSprite.transform.localPosition = new Vector2(this.rightBackpackSprite.transform.localPosition.x, num8 + 0.5f);
		this.leftBackpackSprite.transform.localPosition = new Vector2(this.leftBackpackSprite.transform.localPosition.x, num8 + 0.5f);
		this.rightBackpackSprite.size = new Vector2(this.rightBackpackSprite.size.x, num7 + 3f);
		this.leftBackpackSprite.size = new Vector2(this.leftBackpackSprite.size.x, num7 + 3f);
		this.bottomBackpackSprite.transform.localPosition = new Vector3(this.bottomBackpackSprite.transform.localPosition.x, num5 - 1f, -1f);
		this.topBackpackSprite.transform.localPosition = new Vector2(this.topBackpackSprite.transform.localPosition.x, num6 + 1.25f);
		this.backgroundBackpackSprite.size = new Vector2(num3 + 1f, num7 + 1f);
		this.backgroundBackpackSprite.transform.localPosition = new Vector3(num4, num8, 1f);
		if (this.player.characterName == Character.CharacterName.Satchel && PocketManager.main)
		{
			PocketManager.main.DeterminePockets();
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0003F610 File Offset: 0x0003D810
	private List<Transform> GetRowOfGrid(float y, bool acceptExtension, Transform gridParent)
	{
		List<Transform> list = new List<Transform>();
		foreach (object obj in gridParent)
		{
			Transform transform = (Transform)obj;
			GridExtension component = transform.GetComponent<GridExtension>();
			if ((transform.CompareTag("GridSquare") || (component && component.selected && acceptExtension)) && transform.localPosition.y == y)
			{
				list.Add(transform);
			}
		}
		return list;
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0003F6A8 File Offset: 0x0003D8A8
	private List<Transform> GetColumnOfGrid(float x, bool acceptExtension, Transform gridParent)
	{
		List<Transform> list = new List<Transform>();
		foreach (object obj in gridParent)
		{
			Transform transform = (Transform)obj;
			GridExtension component = transform.GetComponent<GridExtension>();
			if ((transform.CompareTag("GridSquare") || (component && component.selected && acceptExtension)) && transform.localPosition.x == x)
			{
				list.Add(transform);
			}
		}
		return list;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0003F740 File Offset: 0x0003D940
	private Transform GetLeftMostGrid(List<Transform> gridSpaces, Transform gridParent)
	{
		float num = 999f;
		Transform transform = null;
		foreach (Transform transform2 in gridSpaces)
		{
			if (transform2.localPosition.x < num)
			{
				transform = transform2;
				num = transform2.localPosition.x;
			}
		}
		return transform;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0003F7AC File Offset: 0x0003D9AC
	private Transform GetRightMostGrid(List<Transform> gridSpaces, Transform gridParent)
	{
		float num = -999f;
		Transform transform = null;
		foreach (Transform transform2 in gridSpaces)
		{
			if (transform2.localPosition.x > num)
			{
				transform = transform2;
				num = transform2.localPosition.x;
			}
		}
		return transform;
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0003F818 File Offset: 0x0003DA18
	private Transform GetTopMostGrid(List<Transform> gridSpaces, Transform gridParent)
	{
		float num = -999f;
		Transform transform = null;
		foreach (Transform transform2 in gridSpaces)
		{
			if (transform2.localPosition.y > num)
			{
				transform = transform2;
				num = transform2.localPosition.y;
			}
		}
		return transform;
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0003F884 File Offset: 0x0003DA84
	private Transform GetBottomMostGrid(List<Transform> gridSpaces, Transform gridParent)
	{
		float num = 999f;
		Transform transform = null;
		foreach (Transform transform2 in gridSpaces)
		{
			if (transform2.localPosition.y < num)
			{
				transform = transform2;
				num = transform2.localPosition.y;
			}
		}
		return transform;
	}

	// Token: 0x04000521 RID: 1313
	public static LevelUpManager main;

	// Token: 0x04000522 RID: 1314
	[SerializeField]
	private ParticleSystem particles;

	// Token: 0x04000523 RID: 1315
	[SerializeField]
	private List<GameObject> stuffToHideDuringLevelUp;

	// Token: 0x04000524 RID: 1316
	[SerializeField]
	private GameObject fadeOutUnnecessary;

	// Token: 0x04000525 RID: 1317
	[SerializeField]
	private GameObject button;

	// Token: 0x04000526 RID: 1318
	[SerializeField]
	private GameObject levelUpText;

	// Token: 0x04000527 RID: 1319
	[SerializeField]
	public Transform inventoryParent;

	// Token: 0x04000528 RID: 1320
	[SerializeField]
	public Transform gridParent;

	// Token: 0x04000529 RID: 1321
	[SerializeField]
	private GameObject gridExtensionPrefab;

	// Token: 0x0400052A RID: 1322
	[SerializeField]
	private GameObject gridSquarePrefab;

	// Token: 0x0400052B RID: 1323
	[SerializeField]
	public List<GameObject> gridBlockPrefabs;

	// Token: 0x0400052C RID: 1324
	[SerializeField]
	public List<GameObject> chosenBlockPrefabs;

	// Token: 0x0400052D RID: 1325
	[SerializeField]
	public Transform cramButtons;

	// Token: 0x0400052E RID: 1326
	public SpriteRenderer topBackpackSprite;

	// Token: 0x0400052F RID: 1327
	public SpriteRenderer rightBackpackSprite;

	// Token: 0x04000530 RID: 1328
	public SpriteRenderer leftBackpackSprite;

	// Token: 0x04000531 RID: 1329
	public SpriteRenderer bottomBackpackSprite;

	// Token: 0x04000532 RID: 1330
	public SpriteRenderer backgroundBackpackSprite;

	// Token: 0x04000533 RID: 1331
	[HideInInspector]
	public float time;

	// Token: 0x04000534 RID: 1332
	[HideInInspector]
	public int numberToUnlock;

	// Token: 0x04000535 RID: 1333
	[HideInInspector]
	public bool levelingUp;

	// Token: 0x04000536 RID: 1334
	[SerializeField]
	private Transform levelUpPopUpButton;

	// Token: 0x04000537 RID: 1335
	public int maxSizeX = 4;

	// Token: 0x04000538 RID: 1336
	public int maxSizeY = 3;

	// Token: 0x04000539 RID: 1337
	public int spacesSaved;

	// Token: 0x0400053A RID: 1338
	private GameManager gameManager;

	// Token: 0x0400053B RID: 1339
	private Player player;

	// Token: 0x0400053C RID: 1340
	private int rewardNumber;
}
