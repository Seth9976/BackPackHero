using System;
using System.Collections;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Graphs;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200014C RID: 332
public class Overworld_NPC : Overworld_InteractiveObject
{
	// Token: 0x06000D00 RID: 3328 RVA: 0x00083C30 File Offset: 0x00081E30
	public bool HasForcedConversation()
	{
		foreach (Overworld_NPC.DialogueGraphInstance dialogueGraphInstance in this.alternateDialogueGraphs)
		{
			if ((!dialogueGraphInstance.cannotBeReplayed || !MetaProgressSaveManager.main.dialoguesPerformed.Contains(dialogueGraphInstance.graph.name)) && MetaProgressSaveManager.ConditionsMet(dialogueGraphInstance.conditions))
			{
				if (dialogueGraphInstance.forcedConversation)
				{
					return true;
				}
				return false;
			}
		}
		return false;
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x00083CC0 File Offset: 0x00081EC0
	private void OnEnable()
	{
		Overworld_NPC.npcs.Add(this);
		if (OVerworld_NPCManager.main)
		{
			OVerworld_NPCManager.main.UseNPCTicket(base.name, 1);
		}
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x00083CEC File Offset: 0x00081EEC
	private void OnDisable()
	{
		Overworld_NPC.npcs.Remove(this);
		if (OVerworld_NPCManager.main && (!SceneLoader.main || !SceneLoader.main.IsLoading() || SceneLoader.main.GetSceneNameToLoad() == "Overworld"))
		{
			OVerworld_NPCManager.main.UseNPCTicket(base.name, -1);
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x00083D50 File Offset: 0x00081F50
	private void Start()
	{
		this.target.transform.position = base.transform.position + Random.insideUnitSphere * 10f;
		if (this.randomlySelectedDialogueGraphs.Count > 0)
		{
			int num = Random.Range(0, this.randomlySelectedDialogueGraphs.Count);
			this.defaultDialogueGraph = this.randomlySelectedDialogueGraphs[num];
		}
		SpriteRenderer componentInChildren = base.GetComponentInChildren<SpriteRenderer>();
		if (componentInChildren)
		{
			this.defaultSprite = componentInChildren.sprite;
		}
		this.highlightCircle.SetActive(false);
		this.interactable = base.GetComponentInChildren<Overworld_Interactable>();
		this.interactable.enabled = false;
		this.target.SetParent(base.transform.parent);
		this.animator = base.GetComponentInChildren<Animator>();
		this.overworld_NPC_CostumeSelector = base.GetComponentInChildren<Overworld_NPC_CostumeSelector>();
		if (this.overworld_NPC_CostumeSelector)
		{
			this.isRandomNPC = true;
		}
		this.overworld_FollowAStarPath = base.GetComponent<Overworld_FollowAStarPath>();
		if (this.phase == Overworld_NPC.Phase.LeaveBuilding)
		{
			this.StartEntranceSequence();
		}
		else if (this.phase == Overworld_NPC.Phase.Moving)
		{
			this.overworld_FollowAStarPath.UpdatePath();
		}
		else if (this.phase == Overworld_NPC.Phase.Idle)
		{
			this.overworld_FollowAStarPath.FaceTowards(new Vector3(0f, -999f, 0f));
			this.ChangePhase(Overworld_NPC.Phase.Idle);
		}
		if ((this.characterName == Character.CharacterName.Any || MetaProgressSaveManager.main.lastRun.character != this.characterName) && this.homeStructureType)
		{
			base.StartCoroutine(this.FindHomeAfterLoad());
		}
		this.ConsiderExclmation();
		this.baseSpeed = this.overworld_FollowAStarPath.speed;
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x00083EF0 File Offset: 0x000820F0
	private IEnumerator FaceTowardsRoutine(Vector3 pos, float delay)
	{
		yield return new WaitForSeconds(delay);
		this.overworld_FollowAStarPath.FaceTowards(pos);
		yield break;
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x00083F0D File Offset: 0x0008210D
	public void FaceTowards(Vector3 pos, float delay = 0f)
	{
		if (delay == 0f)
		{
			this.overworld_FollowAStarPath.FaceTowards(pos);
			return;
		}
		if (this.faceTowardsRoutine != null)
		{
			base.StopCoroutine(this.faceTowardsRoutine);
		}
		this.faceTowardsRoutine = base.StartCoroutine(this.FaceTowardsRoutine(pos, delay));
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x00083F4C File Offset: 0x0008214C
	private IEnumerator FindHomeAfterLoad()
	{
		yield return new WaitForEndOfFrame();
		yield return null;
		Overworld_SaveManager saveManager = Object.FindObjectOfType<Overworld_SaveManager>();
		while (saveManager && saveManager.isSavingOrLoading)
		{
			yield return null;
		}
		this.FindHome();
		if (this.canTeleport && this.home)
		{
			Vector3 vector = base.transform.position;
			OVerworld_NPCDestination componentInChildren = this.home.GetComponentInChildren<OVerworld_NPCDestination>();
			if (componentInChildren)
			{
				vector = componentInChildren.transform.position + Random.insideUnitSphere * 0.5f;
			}
			else
			{
				vector = this.home.position + Random.insideUnitSphere * 0.5f;
			}
			bool flag;
			do
			{
				flag = false;
				RaycastHit2D[] array = Physics2D.BoxCastAll(vector, Vector2.one, 0f, Vector2.zero);
				vector.x -= 1f;
				foreach (RaycastHit2D raycastHit2D in array)
				{
					if (raycastHit2D.collider.gameObject.CompareTag("Overworld_Water"))
					{
						flag = true;
						break;
					}
				}
			}
			while (flag);
			base.transform.position = vector;
		}
		yield break;
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x00083F5C File Offset: 0x0008215C
	public bool HasSomethingToSay()
	{
		if (this.FindAlternateDialogueGraph())
		{
			return true;
		}
		Overworld_BuildingInterfaceLauncher componentInChildren = base.GetComponentInChildren<Overworld_BuildingInterfaceLauncher>();
		return componentInChildren && MetaProgressSaveManager.main.researchBuildingsDiscovered.Contains(Item2.GetDisplayName(componentInChildren.name)) && componentInChildren.DetermineIfResearchIsAvailableBool();
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x00083FB0 File Offset: 0x000821B0
	private void ConsiderExclmation()
	{
		if (!this.exclamationMark)
		{
			return;
		}
		DialogueGraph dialogueGraph = this.FindAlternateDialogueGraph();
		if (dialogueGraph)
		{
			this.exclamationMark.gameObject.SetActive(true);
			SpriteRenderer component = this.exclamationMark.GetComponent<SpriteRenderer>();
			if (component)
			{
				component.sprite = this.exclamationMarkConversation;
			}
			if (MetaProgressSaveManager.main.dialoguesPerformed.Contains(dialogueGraph.name))
			{
				this.defaultColorForExclamation = new Color(0.4f, 0.4f, 0.4f, 0.6f);
				return;
			}
			this.defaultColorForExclamation = new Color(1f, 1f, 1f, 1f);
			return;
		}
		else
		{
			Overworld_BuildingInterfaceLauncher componentInChildren = base.GetComponentInChildren<Overworld_BuildingInterfaceLauncher>();
			if (componentInChildren && MetaProgressSaveManager.main.researchBuildingsDiscovered.Contains(Item2.GetDisplayName(componentInChildren.name)) && componentInChildren.DetermineIfResearchIsAvailableBool())
			{
				this.exclamationMark.gameObject.SetActive(true);
				SpriteRenderer component2 = this.exclamationMark.GetComponent<SpriteRenderer>();
				if (component2)
				{
					component2.sprite = this.exclamationMarkResearch;
				}
				this.defaultColorForExclamation = new Color(1f, 1f, 1f, 1f);
				return;
			}
			this.exclamationMark.gameObject.SetActive(false);
			return;
		}
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x000840FC File Offset: 0x000822FC
	public DialogueGraph FindAlternateDialogueGraph()
	{
		foreach (Overworld_NPC.DialogueGraphInstance dialogueGraphInstance in this.alternateDialogueGraphs)
		{
			if (MetaProgressSaveManager.ConditionsMet(dialogueGraphInstance.conditions) && (!dialogueGraphInstance.cannotBeReplayed || !MetaProgressSaveManager.main.dialoguesPerformed.Contains(dialogueGraphInstance.graph.name)))
			{
				return dialogueGraphInstance.graph;
			}
		}
		return null;
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x00084188 File Offset: 0x00082388
	public void FindHome()
	{
		if (!this.homeStructureType)
		{
			return;
		}
		foreach (Overworld_Structure overworld_Structure in Overworld_Structure.structures)
		{
			if (Item2.GetDisplayName(overworld_Structure.name) == Item2.GetDisplayName(this.homeStructureType.name))
			{
				this.home = overworld_Structure.transform;
				break;
			}
		}
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x00084214 File Offset: 0x00082414
	public void TeleportToIntroHome(Transform t)
	{
		if (!this.home)
		{
			if (t == null)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("IntroHome");
				if (!gameObject)
				{
					return;
				}
				t = gameObject.transform;
			}
			base.transform.position = t.position + Random.insideUnitSphere * 2f;
		}
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x00084278 File Offset: 0x00082478
	private void Update()
	{
		if (this.timeToDisplayCard <= 0f)
		{
			this.ConsiderHideCard();
		}
		if (Overworld_ConversationManager.main.ShowingConversation() && !Overworld_Manager.main.IsState(Overworld_Manager.State.MOVING))
		{
			this.timeToInteract = 0.75f;
			this.interactable.enabled = false;
		}
		else if (this.timeToInteract > 0f)
		{
			this.timeToInteract -= Time.deltaTime;
		}
		else
		{
			this.interactable.enabled = true;
		}
		if ((!(Overworld_Purse.main.targetForInteraction != null) || (!Overworld_Purse.main.targetForInteraction.IsChildOf(base.transform) && !(Overworld_Purse.main.targetForInteraction == base.transform))) && this.phase == Overworld_NPC.Phase.Waiting)
		{
			this.IdleForTime(3f + Random.Range(-0.2f, 0.2f));
		}
		if ((this.phase == Overworld_NPC.Phase.Talking && !Overworld_ConversationManager.main.ShowingConversation() && !SingleUI.IsViewingPopUp()) || (this.phase == Overworld_NPC.Phase.Talking && Overworld_ConversationManager.main.currentNPCSpeaker != this))
		{
			this.ReleaseNPC();
			this.IdleForTime(2f);
			this.ConsiderExclmation();
		}
		if (this.phase == Overworld_NPC.Phase.Idle && this.idleCoroutine == null && Overworld_Purse.main.GetTargetForInteraction() != base.transform && this.validPhases.Contains(Overworld_NPC.Phase.Moving))
		{
			this.FindNewPath();
		}
		if (this.phase == Overworld_NPC.Phase.Moving && this.overworld_FollowAStarPath.reachedDestination)
		{
			this.EndOfPath();
		}
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x00084400 File Offset: 0x00082600
	public void EndOfPath()
	{
		if (this.currentDestination == null || this.currentDestination.actionHere == Overworld_NPC.Phase.Moving || Vector2.Distance(base.transform.position, this.currentDestination.transform.position) > this.currentDestination.radius + 0.15f)
		{
			this.IdleForTime(4f + Random.Range(0f, 6f));
			return;
		}
		if (this.currentDestination.actionHere == Overworld_NPC.Phase.EnterBuilding && this.validPhases.Contains(Overworld_NPC.Phase.EnterBuilding))
		{
			Vector3 position = this.currentDestination.transform.position;
			base.transform.position = position + (base.transform.position - position).normalized * (this.currentDestination.radius - 0.25f);
			this.ChangePhase(Overworld_NPC.Phase.EnterBuilding);
			base.StartCoroutine(this.EnterBuilding());
			return;
		}
		if (this.currentDestination.actionHere == Overworld_NPC.Phase.ExitOffMap && this.validPhases.Contains(Overworld_NPC.Phase.ExitOffMap))
		{
			this.ChangePhase(Overworld_NPC.Phase.ExitOffMap);
			base.StartCoroutine(this.ExitOffMap());
			return;
		}
		if (this.currentDestination.actionHere == Overworld_NPC.Phase.Idle)
		{
			this.overworld_FollowAStarPath.FaceTowards(this.currentDestination.transform.parent.position);
			this.IdleForTime(5f);
		}
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0008456D File Offset: 0x0008276D
	public void SetHome(Transform newHome)
	{
		this.home = newHome;
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x00084576 File Offset: 0x00082776
	public void SetNPCType(string npcTypeName)
	{
		this.npcTypeName = npcTypeName;
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0008457F File Offset: 0x0008277F
	public void StartEntranceSequence()
	{
		base.StartCoroutine(this.LeaveBuilding());
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0008458E File Offset: 0x0008278E
	private IEnumerator LeaveBuilding()
	{
		Overworld_FollowAStarPath overworld_FollowAStarPath = base.GetComponent<Overworld_FollowAStarPath>();
		if (this.animator)
		{
			this.animator.Play("move_down");
		}
		else if (this.overworld_NPC_CostumeSelector)
		{
			this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Direction.Front, npcOutfit.Animation.Walk);
		}
		Collider2D collider = base.GetComponent<Collider2D>();
		collider.enabled = false;
		overworld_FollowAStarPath.enabled = false;
		SpriteRenderer[] spriteRenderers = base.GetComponentsInChildren<SpriteRenderer>();
		SpriteRenderer[] array = spriteRenderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = new Color(0f, 0f, 0f, 0f);
		}
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			array = spriteRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = Color.Lerp(new Color(0f, 0f, 0f, 0f), Color.white, time / 1f);
			}
			base.transform.position += Vector3.down * 1.2f * Time.deltaTime;
			yield return null;
		}
		collider.enabled = true;
		overworld_FollowAStarPath.enabled = true;
		this.ChangePhase(Overworld_NPC.Phase.Idle);
		yield break;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0008459D File Offset: 0x0008279D
	private IEnumerator ExitOffMap()
	{
		Overworld_FollowAStarPath component = base.GetComponent<Overworld_FollowAStarPath>();
		Animator componentInChildren = base.GetComponentInChildren<Animator>();
		Overworld_NPC_CostumeSelector componentInChildren2 = base.GetComponentInChildren<Overworld_NPC_CostumeSelector>();
		if (componentInChildren)
		{
			componentInChildren.Play("move_left");
		}
		else if (componentInChildren2)
		{
			componentInChildren2.SetMovement(npcOutfit.Direction.Left, npcOutfit.Animation.Walk);
		}
		base.GetComponent<Collider2D>().enabled = false;
		component.enabled = false;
		SpriteRenderer[] componentsInChildren = base.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].color = Color.white;
		}
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			base.transform.position += Vector3.left * 1.5f * Time.deltaTime;
			yield return null;
		}
		time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			base.transform.position += Vector3.left * 1.5f * Time.deltaTime;
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x000845AC File Offset: 0x000827AC
	private IEnumerator EnterBuilding()
	{
		Behaviour component = base.GetComponent<Overworld_FollowAStarPath>();
		Animator componentInChildren = base.GetComponentInChildren<Animator>();
		Overworld_NPC_CostumeSelector componentInChildren2 = base.GetComponentInChildren<Overworld_NPC_CostumeSelector>();
		if (componentInChildren)
		{
			componentInChildren.Play("move_up");
		}
		else if (componentInChildren2)
		{
			componentInChildren2.SetMovement(npcOutfit.Direction.Back, npcOutfit.Animation.Walk);
		}
		base.GetComponent<Collider2D>().enabled = false;
		component.enabled = false;
		SpriteRenderer[] spriteRenderers = base.GetComponentsInChildren<SpriteRenderer>();
		SpriteRenderer[] array = spriteRenderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = Color.white;
		}
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			base.transform.position += Vector3.up * 1.2f * 0.5f * Time.deltaTime;
			yield return null;
		}
		time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			array = spriteRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = Color.Lerp(Color.white, new Color(0f, 0f, 0f, 0f), time / 1f);
			}
			base.transform.position += Vector3.up * 1.2f * 0.5f * Time.deltaTime;
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x000845BB File Offset: 0x000827BB
	private void EndIdle()
	{
		if (this.idleCoroutine != null)
		{
			base.StopCoroutine(this.idleCoroutine);
			this.idleCoroutine = null;
		}
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x000845D8 File Offset: 0x000827D8
	private void StartIdle()
	{
		this.EndIdle();
		this.overworld_FollowAStarPath.IdleAnimation();
		this.ChangePhase(Overworld_NPC.Phase.Idle);
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x000845F2 File Offset: 0x000827F2
	private void IdleForTime(float time = 0f)
	{
		this.StartIdle();
		this.idleCoroutine = base.StartCoroutine(this.Idle(time + Random.Range(-0.1f, 0.3f)));
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0008461D File Offset: 0x0008281D
	private IEnumerator Idle(float time)
	{
		yield return new WaitForSeconds(time);
		this.ChangePhase(Overworld_NPC.Phase.Moving);
		this.FindNewPath();
		this.idleCoroutine = null;
		yield break;
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x00084634 File Offset: 0x00082834
	public void FindPathToTransform(Transform t)
	{
		if (!t)
		{
			return;
		}
		this.overworld_FollowAStarPath.speed = 3.5f;
		this.ReleaseNPC();
		OVerworld_NPCDestination component = t.GetComponent<OVerworld_NPCDestination>();
		if (component)
		{
			this.currentDestination = component;
		}
		this.target.position = new Vector3(Mathf.Clamp(t.position.x, -20.5f, 39f), Mathf.Clamp(t.position.y, -25f, 26f), this.target.position.z);
		this.ChangePhase(Overworld_NPC.Phase.Moving);
		if (!this.overworld_FollowAStarPath)
		{
			this.overworld_FollowAStarPath = base.GetComponent<Overworld_FollowAStarPath>();
		}
		this.overworld_FollowAStarPath.enabled = true;
		this.overworld_FollowAStarPath.UpdatePath();
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x00084704 File Offset: 0x00082904
	public void FindNewPath()
	{
		if (this.isWrecked)
		{
			return;
		}
		this.overworld_FollowAStarPath.speed = this.baseSpeed;
		this.ChangePhase(Overworld_NPC.Phase.Moving);
		int num = Random.Range(0, 100);
		List<OVerworld_NPCDestination> list = new List<OVerworld_NPCDestination>();
		if (num > 75)
		{
			list = OVerworld_NPCDestination.allDestinations;
		}
		if (this.home)
		{
			list = list.FindAll((OVerworld_NPCDestination x) => Vector3.Distance(x.transform.position, this.home.position) < this.radius);
		}
		list = list.FindAll((OVerworld_NPCDestination x) => this.validPhases.Contains(x.actionHere));
		if (this.currentDestination)
		{
			list.Remove(this.currentDestination);
		}
		if (list == null || list.Count == 0)
		{
			if (this.home)
			{
				this.target.transform.position = this.home.position + Random.insideUnitCircle * this.radius;
			}
			else
			{
				this.target.transform.position = base.transform.position + Random.insideUnitCircle * this.radius;
			}
		}
		else
		{
			int num2 = Random.Range(0, list.Count);
			this.currentDestination = list[num2];
			list.RemoveAt(num2);
			list.Insert(0, this.currentDestination);
			this.backupDestinations = list;
			this.target.transform.position = this.currentDestination.transform.position + Random.insideUnitCircle * this.currentDestination.radius;
		}
		this.target.position = new Vector3(Mathf.Clamp(this.target.position.x, -20.5f, 39f), Mathf.Clamp(this.target.position.y, -25f, 26f), this.target.position.z);
		if (!this.overworld_FollowAStarPath)
		{
			this.overworld_FollowAStarPath = base.GetComponent<Overworld_FollowAStarPath>();
		}
		this.overworld_FollowAStarPath.enabled = true;
		this.overworld_FollowAStarPath.UpdatePath();
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x00084934 File Offset: 0x00082B34
	public void ChangePhase(Overworld_NPC.Phase newPhase)
	{
		if (!this.validPhases.Contains(newPhase))
		{
			newPhase = Overworld_NPC.Phase.Idle;
		}
		this.phase = newPhase;
		if (this.phase != Overworld_NPC.Phase.Moving)
		{
			this.overworld_FollowAStarPath.enabled = false;
			return;
		}
		this.EndIdle();
		this.overworld_FollowAStarPath.enabled = true;
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x00084984 File Offset: 0x00082B84
	public override void WaitForInteraction()
	{
		if (this.phase == Overworld_NPC.Phase.EnterBuilding || this.phase == Overworld_NPC.Phase.LeaveBuilding || this.phase == Overworld_NPC.Phase.ExitOffMap)
		{
			return;
		}
		this.StartIdle();
		this.ChangePhase(Overworld_NPC.Phase.Waiting);
		this.highlightCircle.SetActive(true);
		Rigidbody2D component = base.GetComponent<Rigidbody2D>();
		if (component)
		{
			component.mass = 1000f;
		}
		this.overworld_FollowAStarPath.FaceTowards(Overworld_Purse.main.transform.position);
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x000849FC File Offset: 0x00082BFC
	public override void Interact()
	{
		if (this.phase == Overworld_NPC.Phase.EnterBuilding || this.phase == Overworld_NPC.Phase.LeaveBuilding || this.phase == Overworld_NPC.Phase.ExitOffMap)
		{
			return;
		}
		base.Interact();
		UnityEvent unityEvent = this.onTalk;
		if (unityEvent != null)
		{
			unityEvent.Invoke();
		}
		DialogueGraph dialogueGraph = this.FindAlternateDialogueGraph();
		DialogueGraph dialogueGraph2 = (dialogueGraph ? dialogueGraph : this.defaultDialogueGraph);
		this.lastPlayedDialogueGraph = dialogueGraph2;
		if (this.isRandomNPC && this.textChosenForRandomNPC != "")
		{
			base.StopAllCoroutines();
			this.ChangePhase(Overworld_NPC.Phase.Talking);
			Overworld_ConversationManager.main.ShowConversation(this.textChosenForRandomNPC, base.transform);
		}
		else if (dialogueGraph2 != null)
		{
			if (!MetaProgressSaveManager.main.dialoguesPerformed.Contains(dialogueGraph2.name))
			{
				MetaProgressSaveManager.main.dialoguesPerformed.Add(dialogueGraph2.name);
			}
			base.StopAllCoroutines();
			this.ChangePhase(Overworld_NPC.Phase.Talking);
			Overworld_ConversationManager.main.ShowConversation(dialogueGraph2, base.transform);
		}
		if (!this.isWrecked)
		{
			this.overworld_FollowAStarPath.FaceTowards(Overworld_Purse.main.transform.position);
		}
		Overworld_Purse.main.overworld_FollowAStarPath.FaceTowards(base.transform.position);
		if (this.exclamationMark)
		{
			this.exclamationMark.SetActive(false);
		}
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x00084B44 File Offset: 0x00082D44
	public static void ReleaseAllNPCs()
	{
		foreach (Overworld_NPC overworld_NPC in Overworld_NPC.npcs)
		{
			if (overworld_NPC)
			{
				overworld_NPC.ReleaseNPC();
			}
		}
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x00084BA0 File Offset: 0x00082DA0
	public void ReleaseNPC()
	{
		this.highlightCircle.SetActive(false);
		Rigidbody2D component = base.GetComponent<Rigidbody2D>();
		if (component)
		{
			component.mass = 1f;
		}
		if (Overworld_ConversationManager.main.currentNPCSpeaker == this)
		{
			UnityEvent unityEvent = this.onConversationEnd;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			Overworld_Purse.main.ConsiderForcedDialogues();
		}
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x00084C00 File Offset: 0x00082E00
	public void GetWrecked(int num)
	{
		if (this.isWrecked)
		{
			return;
		}
		this.isWrecked = true;
		base.GetComponent<Rigidbody2D>().velocity = (base.transform.position - Overworld_Purse.main.transform.position).normalized * 20f;
		SoundManager.main.PlaySFX("enemy_die");
		Object.Instantiate<GameObject>(this.townWreckSmashParticles, base.transform.position, Quaternion.identity);
		if (num == 0)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.defeatedKingsGuardDuringRaid1);
		}
		else if (num == 1)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.defeatedKingsGuardDuringRaid2);
		}
		else if (num == 2)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.defeatedKingsGuardDuringRaid3);
		}
		else if (num == 3)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.defeatedKingsGuardDuringRaid4);
		}
		else if (num == 4)
		{
			MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.defeatedKingsGuardDuringRaid5);
		}
		this.animator.Play("GetWrecked");
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x00084CFF File Offset: 0x00082EFF
	public void WalkAway()
	{
		this.overworld_FollowAStarPath.SetNewPath(new Vector2(-26f, 0f), 0);
		base.StartCoroutine(this.WalkAwayRoutine());
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x00084D29 File Offset: 0x00082F29
	private IEnumerator WalkAwayRoutine()
	{
		while (!this.overworld_FollowAStarPath.reachedDestination)
		{
			yield return null;
		}
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x00084D38 File Offset: 0x00082F38
	public void ShowShortText(string text)
	{
		Overworld_Manager.main.SetState(Overworld_Manager.State.MOVING);
		this.ChangePhase(Overworld_NPC.Phase.Idle);
		this.IdleForTime(3f);
		Overworld_ConversationManager.main.ShowConversationSmall(text, base.transform);
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x00084D68 File Offset: 0x00082F68
	public void SetCurrentDestination(OVerworld_NPCDestination destination)
	{
		this.currentDestination = destination;
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x00084D71 File Offset: 0x00082F71
	private void OnDrawGizmos()
	{
		if (this.home)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(this.home.transform.position, this.radius);
		}
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x00084DA8 File Offset: 0x00082FA8
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
		this.timeToDisplayCard += Time.deltaTime;
		this.startMousePosition = DigitalCursor.main.transform.position;
		if (this.timeToDisplayCard > 0.1f && !this.isShowingCard)
		{
			this.ShowCard();
		}
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x00084E0D File Offset: 0x0008300D
	public override void OnCursorEnd()
	{
		this.timeToDisplayCard = 0f;
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x00084E1A File Offset: 0x0008301A
	private void ShowCard()
	{
		if (!this.canViewCard)
		{
			return;
		}
		if (DigitalCursor.main.OverUI())
		{
			return;
		}
		this.isShowingCard = true;
		Overworld_CardManager.main.DisplayCard(this);
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x00084E48 File Offset: 0x00083048
	private void ConsiderHideCard()
	{
		if (!this.isShowingCard)
		{
			return;
		}
		if (Vector2.Distance(this.startMousePosition, DigitalCursor.main.transform.position) > 0.75f)
		{
			this.isShowingCard = false;
			Overworld_CardManager.main.RemoveCard();
		}
	}

	// Token: 0x04000A82 RID: 2690
	public DialogueGraph lastPlayedDialogueGraph;

	// Token: 0x04000A83 RID: 2691
	public string textChosenForRandomNPC = "";

	// Token: 0x04000A84 RID: 2692
	public bool isRandomNPC;

	// Token: 0x04000A85 RID: 2693
	[SerializeField]
	private UnityEvent onTalk;

	// Token: 0x04000A86 RID: 2694
	[SerializeField]
	private UnityEvent onConversationEnd;

	// Token: 0x04000A87 RID: 2695
	private float baseSpeed = 1.5f;

	// Token: 0x04000A88 RID: 2696
	[SerializeField]
	public bool canViewCard = true;

	// Token: 0x04000A89 RID: 2697
	public Sprite defaultSprite;

	// Token: 0x04000A8A RID: 2698
	[SerializeField]
	public float voicePitch;

	// Token: 0x04000A8B RID: 2699
	[SerializeField]
	public Sprite portraitSprite;

	// Token: 0x04000A8C RID: 2700
	[SerializeField]
	private Character.CharacterName characterName;

	// Token: 0x04000A8D RID: 2701
	[SerializeField]
	private List<Overworld_NPC.DialogueGraphInstance> alternateDialogueGraphs = new List<Overworld_NPC.DialogueGraphInstance>();

	// Token: 0x04000A8E RID: 2702
	[SerializeField]
	private List<DialogueGraph> randomlySelectedDialogueGraphs = new List<DialogueGraph>();

	// Token: 0x04000A8F RID: 2703
	[SerializeField]
	private DialogueGraph defaultDialogueGraph;

	// Token: 0x04000A90 RID: 2704
	[SerializeField]
	private Overworld_Structure homeStructureType;

	// Token: 0x04000A91 RID: 2705
	[SerializeField]
	private Transform home;

	// Token: 0x04000A92 RID: 2706
	[SerializeField]
	private float radius = 5f;

	// Token: 0x04000A93 RID: 2707
	public string npcTypeName;

	// Token: 0x04000A94 RID: 2708
	public Overworld_NPC.Interaction interaction;

	// Token: 0x04000A95 RID: 2709
	public List<Overworld_NPC.Phase> validPhases = new List<Overworld_NPC.Phase>
	{
		Overworld_NPC.Phase.Idle,
		Overworld_NPC.Phase.Moving,
		Overworld_NPC.Phase.LeaveBuilding,
		Overworld_NPC.Phase.EnterBuilding,
		Overworld_NPC.Phase.Talking,
		Overworld_NPC.Phase.Waiting
	};

	// Token: 0x04000A96 RID: 2710
	public Overworld_NPC.Phase phase;

	// Token: 0x04000A97 RID: 2711
	private Overworld_FollowAStarPath overworld_FollowAStarPath;

	// Token: 0x04000A98 RID: 2712
	[SerializeField]
	private Transform target;

	// Token: 0x04000A99 RID: 2713
	[SerializeField]
	private bool canTeleport;

	// Token: 0x04000A9A RID: 2714
	private OVerworld_NPCDestination currentDestination;

	// Token: 0x04000A9B RID: 2715
	private Animator animator;

	// Token: 0x04000A9C RID: 2716
	private Overworld_NPC_CostumeSelector overworld_NPC_CostumeSelector;

	// Token: 0x04000A9D RID: 2717
	private Coroutine idleCoroutine;

	// Token: 0x04000A9E RID: 2718
	private Overworld_Interactable interactable;

	// Token: 0x04000A9F RID: 2719
	[SerializeField]
	private GameObject highlightCircle;

	// Token: 0x04000AA0 RID: 2720
	[SerializeField]
	private GameObject exclamationMark;

	// Token: 0x04000AA1 RID: 2721
	[SerializeField]
	private Sprite exclamationMarkConversation;

	// Token: 0x04000AA2 RID: 2722
	[SerializeField]
	private Sprite exclamationMarkResearch;

	// Token: 0x04000AA3 RID: 2723
	[SerializeField]
	private GameObject townWreckSmashParticles;

	// Token: 0x04000AA4 RID: 2724
	public static List<Overworld_NPC> npcs = new List<Overworld_NPC>();

	// Token: 0x04000AA5 RID: 2725
	private Coroutine faceTowardsRoutine;

	// Token: 0x04000AA6 RID: 2726
	public Color defaultColorForExclamation;

	// Token: 0x04000AA7 RID: 2727
	private float timeToInteract;

	// Token: 0x04000AA8 RID: 2728
	public List<OVerworld_NPCDestination> backupDestinations = new List<OVerworld_NPCDestination>();

	// Token: 0x04000AA9 RID: 2729
	public bool isWrecked;

	// Token: 0x04000AAA RID: 2730
	private bool isShowingCard;

	// Token: 0x04000AAB RID: 2731
	private float timeToDisplayCard;

	// Token: 0x04000AAC RID: 2732
	private Vector2 startMousePosition;

	// Token: 0x020003FF RID: 1023
	[Serializable]
	private class DialogueGraphInstance
	{
		// Token: 0x0400178E RID: 6030
		public DialogueGraph graph;

		// Token: 0x0400178F RID: 6031
		public bool forcedConversation;

		// Token: 0x04001790 RID: 6032
		public bool cannotBeReplayed;

		// Token: 0x04001791 RID: 6033
		public List<MetaProgressSaveManager.MetaProgressCondition> conditions = new List<MetaProgressSaveManager.MetaProgressCondition>();
	}

	// Token: 0x02000400 RID: 1024
	public enum Interaction
	{
		// Token: 0x04001793 RID: 6035
		Talk,
		// Token: 0x04001794 RID: 6036
		Shop
	}

	// Token: 0x02000401 RID: 1025
	public enum Phase
	{
		// Token: 0x04001796 RID: 6038
		Idle,
		// Token: 0x04001797 RID: 6039
		Moving,
		// Token: 0x04001798 RID: 6040
		LeaveBuilding,
		// Token: 0x04001799 RID: 6041
		EnterBuilding,
		// Token: 0x0400179A RID: 6042
		Talking,
		// Token: 0x0400179B RID: 6043
		Waiting,
		// Token: 0x0400179C RID: 6044
		ExitOffMap
	}
}
