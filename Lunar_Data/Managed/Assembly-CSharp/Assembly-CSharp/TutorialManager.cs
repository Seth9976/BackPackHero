using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class TutorialManager : MonoBehaviour
{
	// Token: 0x06000440 RID: 1088 RVA: 0x000152A7 File Offset: 0x000134A7
	private void OnEnable()
	{
		TutorialManager.instance = this;
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x000152AF File Offset: 0x000134AF
	private void OnDisable()
	{
		if (TutorialManager.instance == this)
		{
			TutorialManager.instance = null;
		}
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x000152C4 File Offset: 0x000134C4
	private void Start()
	{
		if (Singleton.instance.selectedRun && Singleton.instance.selectedRun.forceTutorial)
		{
			this.loadTutorial = true;
		}
		if (!this.loadTutorial)
		{
			return;
		}
		if (RoomManager.instance.currentRoom)
		{
			Object.Destroy(RoomManager.instance.currentRoom.gameObject);
		}
		GameManager.instance.isTutorial = true;
		CardManager.instance.isAllowedToDraw = false;
		HordeRemainingDisplay.instance.ShowTimer(false);
		GameObject gameObject = Object.Instantiate<GameObject>(this.operatorPanel, CanvasManager.instance.masterContentScaler);
		GameObject gameObject2 = Object.Instantiate<GameObject>(this.tutorialLevel);
		RoomManager.instance.SetRoom(gameObject2);
		Object.Instantiate<GameObject>(Singleton.instance.selectedCharacter.characterPrefab);
		SpawnPoint[] componentsInChildren = this.tutorialLevel.GetComponentsInChildren<SpawnPoint>();
		OperatorPanel component = gameObject.GetComponent<OperatorPanel>();
		component.spawnPointA = componentsInChildren[0];
		component.spawnPointB = componentsInChildren[1];
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x000153AC File Offset: 0x000135AC
	public void LoadStoryPanel()
	{
		Singleton.instance.selectedRun = this.standardRun;
		Object.Instantiate<GameObject>(this.storyPanelPrefab, CanvasManager.instance.masterContentScaler);
		Object.Destroy(RoomManager.instance.currentRoom.gameObject);
		GameObject gameObject = Object.Instantiate<GameObject>(this.entranceRoom);
		this.entranceManager.startingRoom = gameObject;
		RoomManager.instance.SetRoom(gameObject);
	}

	// Token: 0x04000345 RID: 837
	public static TutorialManager instance;

	// Token: 0x04000346 RID: 838
	[SerializeField]
	private RunType standardRun;

	// Token: 0x04000347 RID: 839
	[SerializeField]
	private bool loadTutorial;

	// Token: 0x04000348 RID: 840
	[SerializeField]
	private GameObject operatorPanel;

	// Token: 0x04000349 RID: 841
	[SerializeField]
	private GameObject tutorialLevel;

	// Token: 0x0400034A RID: 842
	[SerializeField]
	private GameObject storyPanelPrefab;

	// Token: 0x0400034B RID: 843
	[SerializeField]
	private GameObject entranceRoom;

	// Token: 0x0400034C RID: 844
	[SerializeField]
	private EntranceManager entranceManager;
}
