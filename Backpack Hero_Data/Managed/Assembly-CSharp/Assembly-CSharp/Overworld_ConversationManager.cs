using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Graphs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000140 RID: 320
public class Overworld_ConversationManager : MonoBehaviour
{
	// Token: 0x06000C1C RID: 3100 RVA: 0x0007D503 File Offset: 0x0007B703
	public bool InLockedConversation()
	{
		return this.ShowingConversation() && !(this.conversationPanel == this.littleConversationPanel);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0007D525 File Offset: 0x0007B725
	public bool ShowingConversation()
	{
		return this.conversationPanel.activeInHierarchy;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0007D534 File Offset: 0x0007B734
	private void Awake()
	{
		this.conversationPanel = this.bigConversationPanel;
		Overworld_ConversationManager.main = this;
		DatabaseInstanceExtended databaseInstanceExtended = new DatabaseInstanceExtended();
		this.dialogueController = new DialogueController(databaseInstanceExtended);
		this.dialogueController.Events.Speak.AddListener(delegate(IActor actor, string text)
		{
			string text2;
			if (text.StartsWith("EXT#"))
			{
				text2 = text.Remove(0, 4);
			}
			else
			{
				text2 = text;
			}
			if (this.currentActor != actor)
			{
				this.currentActor = actor;
				this.HandleActorChange();
			}
			this.HandleActorTasks();
			if (this.currentNPCSpeaker && this.currentNPCSpeaker.isRandomNPC)
			{
				this.currentNPCSpeaker.textChosenForRandomNPC = text2;
			}
			this.ShowConversationSmall(this.debugDisableKeys ? text : LangaugeManager.main.GetTextByKey(text2), this.interactiveObject);
			this.currentReplaceRules = new List<ReplaceTextAction.Rule>();
			if (Overworld_Animations_Manager.main)
			{
				Overworld_Manager.main.SetState(Overworld_Manager.State.WAITING);
			}
		});
		this.dialogueController.Events.Choice.AddListener(delegate(IActor actor, string text, List<IChoice> choices)
		{
			string text3;
			if (text.StartsWith("EXT#"))
			{
				text3 = text.Remove(0, 4);
			}
			else
			{
				text3 = text;
			}
			if (this.currentActor != actor)
			{
				this.currentActor = actor;
				this.HandleActorChange();
			}
			this.choicesList = choices;
			this.HandleActorTasks();
			this.ShowConversationSmall(this.debugDisableKeys ? text : LangaugeManager.main.GetTextByKey(text3), this.interactiveObject);
			this.currentReplaceRules = new List<ReplaceTextAction.Rule>();
			Overworld_Manager.main.SetState(Overworld_Manager.State.WAITING);
		});
		this.dialogueController.Events.End.AddListener(delegate
		{
			if (!this.ConsiderNextConversation())
			{
				this.EndConversation();
			}
		});
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0007D5C8 File Offset: 0x0007B7C8
	private bool ConsiderNextConversation()
	{
		if (this.currentNPCSpeaker)
		{
			DialogueGraph dialogueGraph = this.currentNPCSpeaker.FindAlternateDialogueGraph();
			if (dialogueGraph && this.currentNPCSpeaker.lastPlayedDialogueGraph != dialogueGraph)
			{
				if (!MetaProgressSaveManager.main.dialoguesPerformed.Contains(dialogueGraph.name))
				{
					MetaProgressSaveManager.main.dialoguesPerformed.Add(dialogueGraph.name);
				}
				this.ShowConversation(dialogueGraph, this.currentSpeaker.transform);
				this.currentNPCSpeaker.lastPlayedDialogueGraph = dialogueGraph;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x0007D658 File Offset: 0x0007B858
	private bool ConsiderNextConversationHarmless()
	{
		if (this.currentNPCSpeaker)
		{
			DialogueGraph dialogueGraph = this.currentNPCSpeaker.FindAlternateDialogueGraph();
			if (dialogueGraph && this.currentNPCSpeaker.lastPlayedDialogueGraph != dialogueGraph)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x0007D69C File Offset: 0x0007B89C
	private void EndConversation()
	{
		if (PixelZoomer.main)
		{
			PixelZoomer.main.SetResolution(1f);
		}
		if (Overworld_Purse.main)
		{
			Overworld_Purse.main.isLocked = false;
			Camera.main.GetComponent<FollowObjectCamera>().FollowObject(Overworld_Purse.main.transform);
		}
		this.dialogueGraph.RestoreKeyPrefix();
		this.ToggleInputIcon(false);
		this.bigConversationPanelAnimator.enabled = true;
		this.bigConversationPanelAnimator.Play("CloseBig", 0, 0f);
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x0007D728 File Offset: 0x0007B928
	private void OnDestroy()
	{
		Overworld_ConversationManager.main = null;
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x0007D730 File Offset: 0x0007B930
	public void GetNextDialogue()
	{
		if (this.conversationOptionsPanel.activeInHierarchy)
		{
			return;
		}
		if (this.choicesList != null)
		{
			this.conversationPanel.SetActive(false);
			this.ShowOptions(this.choicesList);
			this.choicesList = null;
			return;
		}
		if (this.ShowingConversation() && this.dialogueGraph != null && this.dialogueController != null)
		{
			this.dialogueController.Next();
		}
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x0007D79C File Offset: 0x0007B99C
	private void Start()
	{
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x0007D79E File Offset: 0x0007B99E
	private void ToggleInputIcon(bool onOrOff)
	{
		this.inputHandlerBig.enabled = onOrOff;
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x0007D7AC File Offset: 0x0007B9AC
	public void MaxOutCharacters()
	{
		this.conversationText.maxVisibleCharacters = this.conversationText.text.Length;
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x0007D7CC File Offset: 0x0007B9CC
	private void Update()
	{
		if (this.timeSinceOptionsShown < 1f && (!this.conversationOptionsPanel || !this.conversationOptionsPanel.activeInHierarchy))
		{
			this.timeSinceOptionsShown += Time.deltaTime;
		}
		if (this.littleConversationPanel && !this.littleConversationPanel.activeSelf)
		{
			this.showConversationCoroutine = null;
			if (this.conversationPanel == this.littleConversationPanel)
			{
				this.showingTextRoutine = null;
			}
			this.conversationText.maxVisibleCharacters = this.conversationText.text.Length;
		}
		if (this.littleConversationPanel && this.littleConversationPanel.activeInHierarchy && this.conversationText.maxVisibleCharacters >= this.conversationText.text.Length)
		{
			this.time += Time.deltaTime;
			if (this.time > 2.5f)
			{
				this.time = 0f;
				if (this.showConversationCoroutine != null)
				{
					base.StopCoroutine(this.showConversationCoroutine);
				}
				this.showConversationCoroutine = base.StartCoroutine(this.ShrinkAndDiableAfterDelay(2f));
			}
		}
		else
		{
			this.time = 0f;
		}
		if (this.littleConversationPanel && this.littleConversationPanel.activeInHierarchy && this.showConversationCoroutine == null)
		{
			if (this.showConversationCoroutine != null)
			{
				base.StopCoroutine(this.showConversationCoroutine);
			}
			this.showConversationCoroutine = base.StartCoroutine(this.ShrinkAndDiableAfterDelay(2f));
		}
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || Input.GetKeyDown("return") || DigitalCursor.main.GetInputDown("cancel") || DigitalCursor.main.GetInputDown("confirm")) && this.timeSinceOptionsShown > 0.2f)
		{
			if (this.showingTextRoutine != null && this.bigConversationPanel == this.conversationPanel)
			{
				base.StopCoroutine(this.showingTextRoutine);
				this.showingTextRoutine = null;
				if (this.conversationText)
				{
					this.conversationText.maxVisibleCharacters = this.conversationText.text.Length;
				}
				this.bigConversationText.maxVisibleCharacters = this.bigConversationText.text.Length;
				return;
			}
			if (this.inputHandlerBig && this.inputHandlerBig.enabled)
			{
				this.GetNextDialogue();
			}
		}
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0007DA38 File Offset: 0x0007BC38
	public void SelectLastConversationOption()
	{
		Button[] componentsInChildren = this.conversationOptionsPanel.GetComponentsInChildren<Button>();
		if (componentsInChildren.Length != 0)
		{
			componentsInChildren[componentsInChildren.Length - 1].onClick.Invoke();
		}
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0007DA68 File Offset: 0x0007BC68
	private void LateUpdate()
	{
		if (this.dialogueController != null)
		{
			this.dialogueController.Tick();
		}
		if (this.conversationPanelCanvasGroup.alpha != 1f)
		{
			if (this.conversationText)
			{
				this.conversationText.maxVisibleCharacters = 0;
			}
			this.bigConversationText.maxVisibleCharacters = 0;
		}
		if (this.currentSpeaker)
		{
			this.SetPosition(this.currentSpeaker.position);
		}
		if (this.currentNPCSpeaker && this.currentNPCSpeaker.phase != Overworld_NPC.Phase.Idle && this.currentNPCSpeaker.phase != Overworld_NPC.Phase.Talking)
		{
			this.currentNPCSpeaker = null;
			this.DisableConversation();
		}
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0007DB17 File Offset: 0x0007BD17
	public void SetActorInGameScene(Sprite sprite, string name, float pitch)
	{
		this.gameSceneSprite = sprite;
		this.gameSceneName = name;
		this.gameScenePitch = pitch;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0007DB30 File Offset: 0x0007BD30
	private void HandleActorChange()
	{
		this.conversationOptionsPanel.gameObject.SetActive(false);
		if (this.currentActor == null)
		{
			this.currentActor = this.defaultActor;
		}
		IActor.ActorType actorType = this.currentActor.actorType;
		if (actorType != IActor.ActorType.NPC)
		{
			if (actorType == IActor.ActorType.Player)
			{
				if (Overworld_Purse.main)
				{
					this.currentSpeaker = Overworld_Purse.main.transform;
				}
				this.leftPortait.SetActive(true);
				this.rightPortait.SetActive(false);
				return;
			}
			this.currentSpeaker = this.interactiveObject;
		}
		else
		{
			this.currentSpeaker = this.interactiveObject;
			this.leftPortait.SetActive(false);
			this.rightPortait.SetActive(true);
			if (GameManager.main)
			{
				this.rightPortaitImage.sprite = this.gameSceneSprite;
				this.rightPortaitName.text = LangaugeManager.main.GetTextByKey(this.gameSceneName);
				if (this.rightPortaitName)
				{
					LangaugeManager.main.SetFont(this.rightPortaitName.transform);
				}
				this.rightPortaitImage.transform.localScale = Vector3.one;
			}
			if (this.currentSpeaker)
			{
				Overworld_NPC component = this.currentSpeaker.GetComponent<Overworld_NPC>();
				if (component)
				{
					this.rightPortaitName.text = LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(component.name).Replace("OVERWORLD", "").Trim());
					if (this.rightPortaitName)
					{
						LangaugeManager.main.SetFont(this.rightPortaitName.transform);
					}
					if (component.portraitSprite)
					{
						this.rightPortaitImage.sprite = component.portraitSprite;
						this.rightPortaitImage.transform.localScale = Vector3.one;
						return;
					}
					this.rightPortaitImage.sprite = component.defaultSprite;
					this.rightPortaitImage.transform.localScale = Vector3.one * 0.5f;
					return;
				}
			}
		}
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x0007DD34 File Offset: 0x0007BF34
	private void ShowBigPanel()
	{
		if (this.shrinkAndDisableCoroutine != null)
		{
			base.StopCoroutine(this.shrinkAndDisableCoroutine);
		}
		this.conversationPanelCanvasGroup.gameObject.SetActive(true);
		this.conversationPanelCanvasGroup.alpha = 1f;
		this.bigConversationText.maxVisibleCharacters = 0;
		this.bigConversationPanel.SetActive(true);
		if (PixelZoomer.main)
		{
			PixelZoomer.main.SetResolution(1.25f);
		}
		if (this.littleConversationPanel)
		{
			this.littleConversationPanel.SetActive(false);
		}
		this.conversationPanel = this.bigConversationPanel;
		this.ToggleInputIcon(true);
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0007DDD8 File Offset: 0x0007BFD8
	private void ShowLittlePanel()
	{
		this.conversationPanelCanvasGroup.gameObject.SetActive(true);
		this.conversationPanelCanvasGroup.alpha = 1f;
		this.bigConversationPanel.SetActive(false);
		this.littleConversationPanel.SetActive(true);
		this.conversationText.maxVisibleCharacters = 0;
		this.conversationPanel = this.littleConversationPanel;
		this.SetAnchor(this.currentNPCSpeaker.transform.position);
		this.ToggleInputIcon(false);
		if (this.expandOverTime != null)
		{
			base.StopCoroutine(this.expandOverTime);
		}
		this.expandOverTime = base.StartCoroutine(this.ExpandScaleOverTime(0.15f));
		if (this.shrinkAndDisableCoroutine != null)
		{
			base.StopCoroutine(this.shrinkAndDisableCoroutine);
		}
		this.shrinkAndDisableCoroutine = base.StartCoroutine(this.ShrinkAndDiableAfterDelay(2.75f));
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x0007DEB0 File Offset: 0x0007C0B0
	private void HandleActorTasks()
	{
		if (this.currentActor == null || this.currentActor.Tasks == null)
		{
			return;
		}
		bool flag = this.ConsiderNextConversationHarmless();
		if (!flag)
		{
			if (this.currentActor.Tasks.Where((string x) => x.ToLower().Trim() == "endearly").ToList<string>().Count != 0)
			{
				this.ShowLittlePanel();
				goto IL_00E4;
			}
		}
		this.ShowBigPanel();
		if (Overworld_Manager.main)
		{
			Camera.main.GetComponent<FollowObjectCamera>().FollowObject(this.currentSpeaker);
			Overworld_Purse.main.overworld_FollowAStarPath.EndMove();
			Overworld_Purse.main.overworld_FollowAStarPath.FaceTowards(this.currentNPCSpeaker.transform.position);
			Overworld_FollowAStarPath component = this.currentNPCSpeaker.GetComponent<Overworld_FollowAStarPath>();
			if (component)
			{
				component.FaceTowards(Overworld_Purse.main.transform.position);
			}
		}
		IL_00E4:
		foreach (string text in this.currentActor.Tasks)
		{
			if (!(text == ""))
			{
				string text2 = text.ToLower().Trim();
				if (!(text2 == "stopmusic"))
				{
					if (!(text2 == "endearly"))
					{
						Debug.LogError(string.Concat(new string[]
						{
							"Unhandled Actor task ",
							text,
							" from ",
							this.currentActor.DisplayName,
							" (",
							((ActorDefinition)this.currentActor).name,
							")"
						}));
					}
					else if (!flag)
					{
						this.EndConversation();
					}
				}
				else
				{
					SoundManager.main.FadeOutAllSongs(0f);
				}
			}
		}
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0007E098 File Offset: 0x0007C298
	public void ShowConversationSmall(string text, Transform speaker)
	{
		if (this.showConversationCoroutine != null)
		{
			base.StopCoroutine(this.showConversationCoroutine);
		}
		if (this.shrinkAndDisableCoroutine != null)
		{
			base.StopCoroutine(this.shrinkAndDisableCoroutine);
		}
		if (this.currentNPCSpeaker)
		{
			this.currentNPCSpeaker = speaker.GetComponent<Overworld_NPC>();
		}
		text = ReplaceTextAction.Replace(text, this.currentReplaceRules);
		if (this.littleConversationPanel)
		{
			LangaugeManager.main.SetFont(this.littleConversationPanel.transform);
		}
		if (this.showingTextRoutine != null)
		{
			base.StopCoroutine(this.showingTextRoutine);
		}
		this.showingTextRoutine = base.StartCoroutine(this.ShowConversationOverTime(text, 0.043f));
		if (this.conversationPanel.transform.localScale.x == 1f)
		{
			return;
		}
		if (this.expandOverTime != null)
		{
			base.StopCoroutine(this.expandOverTime);
		}
		this.expandOverTime = base.StartCoroutine(this.ExpandScaleOverTime(0.15f));
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x0007E18A File Offset: 0x0007C38A
	public void ShowConversation(string text)
	{
		if (this.shrinkAndDisableCoroutine != null)
		{
			base.StopCoroutine(this.shrinkAndDisableCoroutine);
		}
		if (this.showConversationCoroutine != null)
		{
			base.StopCoroutine(this.showConversationCoroutine);
		}
		this.ShowConversation(text, 0.03f);
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0007E1C0 File Offset: 0x0007C3C0
	public static void SpaceRectTransform(RectTransform rectTransform, Canvas canvas, Vector2 localPoint)
	{
		localPoint = rectTransform.anchoredPosition;
		if (rectTransform.pivot == new Vector2(1f, 1f))
		{
			localPoint -= new Vector2(50f, 50f) - new Vector2(220f, -60f);
		}
		else if (rectTransform.pivot == new Vector2(0f, 1f))
		{
			localPoint -= new Vector2(-30f, 40f) - new Vector2(-220f, -90f);
		}
		else if (rectTransform.pivot == new Vector2(1f, 0f))
		{
			localPoint -= new Vector2(20f, -20f) - new Vector2(220f, 110f);
		}
		if (rectTransform.pivot == new Vector2(0f, 0f))
		{
			localPoint -= new Vector2(-20f, -20f) - new Vector2(-220f, 90f);
		}
		rectTransform.anchoredPosition = localPoint;
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x0007E300 File Offset: 0x0007C500
	private void SetAnchor(Vector2 position)
	{
		if (this.conversationPanel == this.bigConversationPanel)
		{
			return;
		}
		RectTransform component = this.conversationPanel.GetComponent<RectTransform>();
		if (position.x > Camera.main.transform.position.x)
		{
			component.pivot = new Vector2(1f, 0f);
		}
		else
		{
			component.pivot = new Vector2(0f, 0f);
		}
		if (position.y > Camera.main.transform.position.y)
		{
			component.pivot = new Vector2(component.pivot.x, 1f);
			return;
		}
		component.pivot = new Vector2(component.pivot.x, 0f);
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x0007E3C8 File Offset: 0x0007C5C8
	public void SetPosition(Vector2 position)
	{
		if (this.conversationPanel == this.bigConversationPanel)
		{
			return;
		}
		Canvas componentInParent = this.conversationPanel.GetComponentInParent<Canvas>();
		RectTransform component = this.conversationPanel.GetComponent<RectTransform>();
		component.position = position;
		Overworld_ConversationManager.SpaceRectTransform(component, componentInParent, position);
		if (position.y > this.conversationPanel.transform.position.y)
		{
			this.conversationPanelDingBottom.gameObject.SetActive(false);
			this.conversationPanelDingTop.gameObject.SetActive(true);
		}
		else
		{
			this.conversationPanelDingBottom.gameObject.SetActive(true);
			this.conversationPanelDingTop.gameObject.SetActive(false);
		}
		this.conversationPanelDingTop.position = new Vector3(position.x, this.conversationPanelDingTop.position.y, 0f);
		this.conversationPanelDingBottom.position = new Vector3(position.x, this.conversationPanelDingBottom.position.y, 0f);
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x0007E4CC File Offset: 0x0007C6CC
	public void ShowConversation(string text, float time)
	{
		if (this.showingTextRoutine != null)
		{
			base.StopCoroutine(this.showingTextRoutine);
		}
		this.showingTextRoutine = base.StartCoroutine(this.ShowConversationOverTime(text, time));
		if (this.conversationPanel.transform.localScale.x == 1f)
		{
			return;
		}
		if (this.showConversationCoroutine != null)
		{
			base.StopCoroutine(this.showConversationCoroutine);
		}
		if (this.expandOverTime != null)
		{
			base.StopCoroutine(this.expandOverTime);
		}
		this.expandOverTime = base.StartCoroutine(this.ExpandScaleOverTime(0.25f));
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0007E55D File Offset: 0x0007C75D
	private IEnumerator ExpandScaleOverTime(float time)
	{
		if (this.conversationPanel == this.bigConversationPanel)
		{
			yield break;
		}
		float currentTime = 0f;
		Vector3 startScale = Vector3.one * 0.1f;
		Vector3 endScale = Vector3.one;
		while (currentTime < time)
		{
			currentTime += Time.deltaTime;
			this.conversationPanel.transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / time);
			yield return null;
		}
		this.conversationPanel.transform.localScale = endScale;
		yield break;
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x0007E574 File Offset: 0x0007C774
	private void DisableConversation()
	{
		if (this.showConversationCoroutine != null)
		{
			base.StopCoroutine(this.showConversationCoroutine);
		}
		this.showConversationCoroutine = base.StartCoroutine(this.ShrinkAndDisableOverTime(0.15f));
		if (Overworld_Purse.main.targetForInteraction)
		{
			Overworld_Purse.main.targetForInteraction = null;
		}
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0007E5C8 File Offset: 0x0007C7C8
	private IEnumerator ShrinkAndDiableAfterDelay(float time)
	{
		float timeWAiting = 0f;
		while (this.showingTextRoutine != null)
		{
			timeWAiting += Time.deltaTime;
			if (timeWAiting > 2.5f)
			{
				break;
			}
			yield return null;
		}
		yield return new WaitForSeconds(time);
		yield return this.ShrinkAndDisableOverTime(0.15f);
		this.showConversationCoroutine = null;
		yield break;
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x0007E5DE File Offset: 0x0007C7DE
	public IEnumerator ShrinkAndDisableOverTime(float time)
	{
		if (this.conversationPanel == this.bigConversationPanel)
		{
			this.bigConversationPanel.SetActive(false);
			this.showConversationCoroutine = null;
			yield break;
		}
		float currentTime = 0f;
		Vector3 startScale = Vector3.one;
		Vector3 endScale = Vector3.one * 0.1f;
		while (currentTime < time)
		{
			currentTime += Time.deltaTime;
			this.littleConversationPanel.transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / time);
			yield return null;
		}
		this.littleConversationPanel.SetActive(false);
		this.showConversationCoroutine = null;
		yield break;
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x0007E5F4 File Offset: 0x0007C7F4
	public void ShowConversation(string x, Transform speaker)
	{
		Card.RemoveAllCursorCards();
		if (Overworld_Manager.main)
		{
			Overworld_CardManager.main.RemoveCard();
		}
		if (Overworld_Purse.main)
		{
			Overworld_Purse.main.EndMovement();
		}
		if (Overworld_Manager.main)
		{
			Overworld_Manager.main.SetState(Overworld_Manager.State.WAITING);
		}
		this.SetInteractiveObject(speaker);
		this.dialogueGraph = null;
		this.currentSpeaker = speaker;
		this.currentNPCSpeaker = speaker.GetComponent<Overworld_NPC>();
		this.ShowLittlePanel();
		new GameObjectOverride[0];
		this.currentReplaceRules = new List<ReplaceTextAction.Rule>();
		this.currentActor = this.defaultActor;
		if (this.debugDisableKeys)
		{
			this.dialogueGraph.DebugDisableKeys();
		}
		this.ShowConversationSmall(LangaugeManager.main.GetTextByKey(x), speaker);
		this.currentReplaceRules = new List<ReplaceTextAction.Rule>();
		if (Overworld_Animations_Manager.main)
		{
			Overworld_Manager.main.SetState(Overworld_Manager.State.WAITING);
		}
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x0007E6D8 File Offset: 0x0007C8D8
	public void ShowConversation(DialogueGraph dialogueGraph, Transform speaker)
	{
		Card.RemoveAllCursorCards();
		if (Overworld_Manager.main)
		{
			Overworld_CardManager.main.RemoveCard();
		}
		if (Overworld_Purse.main)
		{
			Overworld_Purse.main.EndMovement();
		}
		if (Overworld_Manager.main)
		{
			Overworld_Manager.main.SetState(Overworld_Manager.State.WAITING);
		}
		this.SetInteractiveObject(speaker);
		this.dialogueGraph = dialogueGraph;
		this.currentNPCSpeaker = speaker.GetComponent<Overworld_NPC>();
		GameObjectOverride[] array = new GameObjectOverride[0];
		this.currentReplaceRules = new List<ReplaceTextAction.Rule>();
		this.currentActor = this.defaultActor;
		if (this.debugDisableKeys)
		{
			dialogueGraph.DebugDisableKeys();
		}
		if (dialogueGraph)
		{
			this.dialogueController.Play(dialogueGraph, array.ToArray<IGameObjectOverride>());
		}
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x0007E78D File Offset: 0x0007C98D
	public void SetInteractiveObject(Transform t)
	{
		this.interactiveObject = t;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x0007E796 File Offset: 0x0007C996
	private IEnumerator ShowConversationOverTime(string text, float time)
	{
		if (this.conversationText)
		{
			this.conversationText.text = text;
		}
		if (this.conversationText)
		{
			this.conversationText.maxVisibleCharacters = 0;
		}
		this.bigConversationText.text = text;
		this.bigConversationText.gameObject.SetActive(true);
		if (this.conversationText)
		{
			this.conversationText.maxVisibleCharacters = 0;
		}
		this.bigConversationText.maxVisibleCharacters = 0;
		if (this.conversationText)
		{
			LangaugeManager.main.SetFont(this.conversationText.transform);
		}
		if (this.bigConversationText)
		{
			LangaugeManager.main.SetFont(this.bigConversationText.transform);
		}
		while (this.conversationPanelCanvasGroup.alpha != 1f)
		{
			yield return null;
		}
		string text2 = "(?<=[.!?]|[.,]|(?<=[.])\\.\\.\\.)(?=\\s|$)";
		string[] sentences = Regex.Split(text, text2);
		sentences = sentences.Where((string s) => !string.IsNullOrWhiteSpace(s)).ToArray<string>();
		int totalTextLength = 0;
		string[] array = sentences;
		int j = 0;
		while (j < array.Length)
		{
			Overworld_ConversationManager.<>c__DisplayClass66_0 CS$<>8__locals1 = new Overworld_ConversationManager.<>c__DisplayClass66_0();
			CS$<>8__locals1.sentence = array[j];
			char c2 = CS$<>8__locals1.sentence.Last<char>();
			List<float> melody;
			int num;
			if (c2 <= ',')
			{
				if (c2 != '!')
				{
					if (c2 != ',')
					{
						goto IL_0363;
					}
					melody = new List<float> { 45f, 42f, 50f };
				}
				else
				{
					melody = new List<float> { 50f, 48f, 45f, 44f, 50f, 60f, 45f };
				}
			}
			else if (c2 != '.')
			{
				if (c2 != '?')
				{
					goto IL_0363;
				}
				melody = new List<float> { 45f, 42f, 45f, 55f, 60f };
			}
			else
			{
				string sentence = CS$<>8__locals1.sentence;
				num = sentence.Length - 2;
				if (sentence[num] == '.')
				{
					string sentence2 = CS$<>8__locals1.sentence;
					num = sentence2.Length - 3;
					if (sentence2[num] == '.')
					{
						melody = new List<float> { 50f, 50f, 48f, 42f, 41f };
						goto IL_0384;
					}
				}
				melody = new List<float> { 50f, 45f };
			}
			IL_0384:
			bool nonEnglishMode = !new string[] { "a", "e", "i", "o", "u", "y" }.Any((string c) => CS$<>8__locals1.sentence.Contains(c));
			for (int i = 0; i <= CS$<>8__locals1.sentence.Length; i = num + 1)
			{
				totalTextLength++;
				if (this.conversationText)
				{
					this.conversationText.maxVisibleCharacters = totalTextLength;
				}
				this.bigConversationText.maxVisibleCharacters = totalTextLength;
				if ((i < CS$<>8__locals1.sentence.Length && char.IsLetterOrDigit(CS$<>8__locals1.sentence[i])) || nonEnglishMode)
				{
					string text3;
					if (!nonEnglishMode)
					{
						text3 = CS$<>8__locals1.sentence[i].ToString().ToLower();
					}
					else if (i % 2 == 0)
					{
						text3 = new List<string> { "a", "e", "i", "o", "u" }[Random.Range(0, 5)];
					}
					else
					{
						text3 = "-";
					}
					if (text3[0] == 'a' || text3[0] == 'e' || text3[0] == 'i' || text3[0] == 'o' || text3[0] == 'u' || text3[0] == 'y')
					{
						if (text3[0] == 'y')
						{
							text3 = "i";
						}
						float num2 = 0.9f;
						if (this.currentActor.DisplayName == "Purse")
						{
							num2 = this.currentActor.TalkPitch + Random.Range(-0.07f, 0.07f) * this.currentActor.TalkPitch;
						}
						else if (GameManager.main)
						{
							num2 = this.gameScenePitch + Random.Range(-0.07f, 0.07f);
						}
						else if (this.currentNPCSpeaker)
						{
							num2 = this.currentNPCSpeaker.voicePitch + Random.Range(-0.07f, 0.07f) * this.currentNPCSpeaker.voicePitch;
						}
						num2 += (Overworld_ConversationManager.LinearListInterpolation(melody, (float)i / (float)CS$<>8__locals1.sentence.Length) - 50f) / 25f;
						SoundManager.main.PlaySFXPitched("speech_" + Random.Range(1, 8).ToString() + "_" + text3, num2, true);
						yield return new WaitForSeconds(time);
					}
				}
				else
				{
					yield return new WaitForSeconds(time * 1.1f);
				}
				num = i;
			}
			if (CS$<>8__locals1.sentence != sentences[sentences.Length - 1])
			{
				yield return new WaitForSeconds(time * 1.2f);
			}
			melody = null;
			CS$<>8__locals1 = null;
			j++;
			continue;
			IL_0363:
			melody = new List<float> { 50f, 45f };
			goto IL_0384;
		}
		array = null;
		if (this.conversationText)
		{
			this.conversationText.maxVisibleCharacters = text.Length;
		}
		this.bigConversationText.maxVisibleCharacters = text.Length;
		this.showingTextRoutine = null;
		yield break;
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x0007E7B4 File Offset: 0x0007C9B4
	public static float LinearListInterpolation(List<float> values, float pos)
	{
		if (values.Count < 2)
		{
			throw new ArgumentException("The list must contain at least two values for interpolation.");
		}
		pos = Mathf.Clamp(pos, 0f, 1f);
		int num = Mathf.RoundToInt(pos * (float)(values.Count - 1));
		if (num >= values.Count - 1)
		{
			return values[values.Count - 1];
		}
		if (num <= 0)
		{
			return values[0];
		}
		float num2 = pos * (float)(values.Count - 1) - (float)num;
		return Mathf.Lerp(values[num], values[num + 1], num2);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x0007E843 File Offset: 0x0007CA43
	public bool ViewingOptions()
	{
		return this.conversationOptionsPanel.activeInHierarchy;
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x0007E850 File Offset: 0x0007CA50
	public void ShowOptions(List<IChoice> options)
	{
		this.ShowBigPanel();
		this.ShowConversationOptions();
		this.leftPortait.SetActive(true);
		this.rightPortait.SetActive(false);
		this.bigConversationText.gameObject.SetActive(false);
		foreach (object obj in this.conversationOptionsPanel.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		int num = 0;
		List<Button> list = new List<Button>();
		foreach (IChoice choice in options)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.conversationOptionButtonPrefab, this.conversationOptionsPanel.transform);
			string text;
			if (choice.externalKey)
			{
				text = choice.key;
			}
			else
			{
				text = choice.prefix + choice.key;
			}
			string text2 = ((choice.prefix == "nokey") ? choice.Text : LangaugeManager.main.GetTextByKey(text));
			text2 = ReplaceTextAction.Replace(text2, this.currentReplaceRules);
			Overworld_ConversationButton component = gameObject.GetComponent<Overworld_ConversationButton>();
			component.text = text2;
			component.isDialogueButton = true;
			component.dialogueButtonIndex = num;
			gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text2;
			LangaugeManager.main.SetFont(gameObject.transform);
			Button component2 = gameObject.GetComponent<Button>();
			list.Add(component2);
			num++;
		}
		for (int i = 0; i < list.Count; i++)
		{
			Button button = list[i];
			if (i == 0 && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
			{
				EventSystem.current.SetSelectedGameObject(button.gameObject);
				DigitalCursor.main.ToggleSprite(true);
			}
			if (i > 0 && i < list.Count - 1)
			{
				button.navigation = new Navigation
				{
					mode = Navigation.Mode.Explicit,
					selectOnUp = list[i - 1],
					selectOnDown = list[i + 1]
				};
			}
			else if (i > 0)
			{
				button.navigation = new Navigation
				{
					mode = Navigation.Mode.Explicit,
					selectOnUp = list[i - 1]
				};
			}
			else if (i < list.Count - 1)
			{
				button.navigation = new Navigation
				{
					mode = Navigation.Mode.Explicit,
					selectOnDown = list[i + 1]
				};
			}
		}
		if (this.overworldConversationOptionsCanvasGroup)
		{
			this.overworldConversationOptionsCanvasGroup.alpha = 1f;
			this.overworldConversationOptionsCanvasGroup.gameObject.SetActive(true);
			this.overworldConversationOptionsCanvasGroup.interactable = true;
			this.overworldConversationOptionsCanvasGroup.blocksRaycasts = true;
		}
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x0007EB4C File Offset: 0x0007CD4C
	private void ShowConversationOptions()
	{
		DigitalCursor.main.Show();
		this.conversationOptionsPanel.SetActive(true);
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x0007EB64 File Offset: 0x0007CD64
	private void HideConversationOptions()
	{
		this.timeSinceOptionsShown = 0f;
		DigitalCursor.main.Hide();
		this.conversationOptionsPanel.SetActive(false);
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x0007EB87 File Offset: 0x0007CD87
	public void GetDialogueOption(int index)
	{
		this.HideConversationOptions();
		this.dialogueController.SelectChoice(index);
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x0007EB9C File Offset: 0x0007CD9C
	public void ReleaseSpeaker()
	{
		if (this.shrinkAndDisableCoroutine != null)
		{
			base.StopCoroutine(this.shrinkAndDisableCoroutine);
		}
		this.shrinkAndDisableCoroutine = base.StartCoroutine(this.ShrinkAndDiableAfterDelay(2f));
		if (this.interactiveObject == Overworld_Purse.main.targetForInteraction)
		{
			Overworld_Purse.main.targetForInteraction = null;
		}
		if (this.interactiveObject == null)
		{
			return;
		}
		Overworld_NPC.ReleaseAllNPCs();
		Overworld_Manager.main.SetState(Overworld_Manager.State.MOVING);
	}

	// Token: 0x040009C8 RID: 2504
	public static Overworld_ConversationManager main;

	// Token: 0x040009C9 RID: 2505
	[SerializeField]
	public bool debugDisableKeys;

	// Token: 0x040009CA RID: 2506
	[SerializeField]
	public Transform interactiveObject;

	// Token: 0x040009CB RID: 2507
	private GameObject conversationPanel;

	// Token: 0x040009CC RID: 2508
	[SerializeField]
	private GameObject littleConversationPanel;

	// Token: 0x040009CD RID: 2509
	[SerializeField]
	private GameObject bigConversationPanel;

	// Token: 0x040009CE RID: 2510
	[SerializeField]
	private Animator bigConversationPanelAnimator;

	// Token: 0x040009CF RID: 2511
	[SerializeField]
	private CanvasGroup conversationPanelCanvasGroup;

	// Token: 0x040009D0 RID: 2512
	[SerializeField]
	private ActorDefinition defaultActor;

	// Token: 0x040009D1 RID: 2513
	[SerializeField]
	private TextMeshProUGUI conversationText;

	// Token: 0x040009D2 RID: 2514
	[SerializeField]
	private TextMeshProUGUI bigConversationText;

	// Token: 0x040009D3 RID: 2515
	[SerializeField]
	private GameObject leftPortait;

	// Token: 0x040009D4 RID: 2516
	[SerializeField]
	private GameObject rightPortait;

	// Token: 0x040009D5 RID: 2517
	[SerializeField]
	private Image rightPortaitImage;

	// Token: 0x040009D6 RID: 2518
	[SerializeField]
	private TextMeshProUGUI rightPortaitName;

	// Token: 0x040009D7 RID: 2519
	[SerializeField]
	private RectTransform conversationPanelDingTop;

	// Token: 0x040009D8 RID: 2520
	[SerializeField]
	private RectTransform conversationPanelDingBottom;

	// Token: 0x040009D9 RID: 2521
	[SerializeField]
	private Transform currentSpeaker;

	// Token: 0x040009DA RID: 2522
	[SerializeField]
	public Overworld_NPC currentNPCSpeaker;

	// Token: 0x040009DB RID: 2523
	public IActor currentActor;

	// Token: 0x040009DC RID: 2524
	[SerializeField]
	private GameObject conversationOptionsPanel;

	// Token: 0x040009DD RID: 2525
	[SerializeField]
	private GameObject conversationOptionButtonPrefab;

	// Token: 0x040009DE RID: 2526
	public DialogueController dialogueController;

	// Token: 0x040009DF RID: 2527
	public DialogueGraph dialogueGraph;

	// Token: 0x040009E0 RID: 2528
	public List<ReplaceTextAction.Rule> currentReplaceRules = new List<ReplaceTextAction.Rule>();

	// Token: 0x040009E1 RID: 2529
	private Coroutine showConversationCoroutine;

	// Token: 0x040009E2 RID: 2530
	[SerializeField]
	private InputHandler inputHandlerBig;

	// Token: 0x040009E3 RID: 2531
	private List<IChoice> choicesList;

	// Token: 0x040009E4 RID: 2532
	private float time;

	// Token: 0x040009E5 RID: 2533
	private Sprite gameSceneSprite;

	// Token: 0x040009E6 RID: 2534
	private string gameSceneName;

	// Token: 0x040009E7 RID: 2535
	private float gameScenePitch;

	// Token: 0x040009E8 RID: 2536
	private Coroutine expandOverTime;

	// Token: 0x040009E9 RID: 2537
	private Coroutine showingTextRoutine;

	// Token: 0x040009EA RID: 2538
	[SerializeField]
	private CanvasGroup overworldConversationOptionsCanvasGroup;

	// Token: 0x040009EB RID: 2539
	private float timeSinceOptionsShown;

	// Token: 0x040009EC RID: 2540
	private Coroutine shrinkAndDisableCoroutine;
}
