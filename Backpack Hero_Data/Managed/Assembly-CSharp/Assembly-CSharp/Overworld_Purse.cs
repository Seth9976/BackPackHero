using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000152 RID: 338
public class Overworld_Purse : MonoBehaviour
{
	// Token: 0x06000D56 RID: 3414 RVA: 0x00085CF9 File Offset: 0x00083EF9
	private void Awake()
	{
		Overworld_Purse.main = this;
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x00085D01 File Offset: 0x00083F01
	private void OnDestroy()
	{
		if (Overworld_Purse.main == this)
		{
			Overworld_Purse.main = null;
		}
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x00085D16 File Offset: 0x00083F16
	public void EndMovement()
	{
		this.myHeldForce = Vector2.zero;
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00085D24 File Offset: 0x00083F24
	private void Start()
	{
		this.animator = base.GetComponentInChildren<Animator>();
		this.rigidbody2D = base.GetComponent<Rigidbody2D>();
		this.overworld_FollowAStarPath = base.GetComponent<Overworld_FollowAStarPath>();
		this.isLocked = true;
		this.consideredLeaving = true;
		this.isLocked = true;
		this.overworld_FollowAStarPath.enabled = true;
		this.overworld_FollowAStarPath.ResetEarlyEnd();
		this.overworld_FollowAStarPath.SetNewPath(this.startTarget.position, 0);
		this.overworld_FollowAStarPath.UpdatePath();
		this.moveToStartCoroutine = base.StartCoroutine(this.UnlockAfterMove());
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x00085DBA File Offset: 0x00083FBA
	private IEnumerator UnlockAfterMove()
	{
		yield return new WaitForSeconds(0.25f);
		while (this.overworld_FollowAStarPath.IsMoving())
		{
			yield return null;
		}
		this.ConsiderForcedDialogues();
		this.isLocked = false;
		yield break;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x00085DC9 File Offset: 0x00083FC9
	public void ConsiderForcedDialogues()
	{
		if (this.considerForcedDialoguesCoroutine != null)
		{
			base.StopCoroutine(this.considerForcedDialoguesCoroutine);
		}
		this.considerForcedDialoguesCoroutine = base.StartCoroutine(this.ConsiderForcedDialoguesRoutine());
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x00085DF1 File Offset: 0x00083FF1
	private IEnumerator ConsiderForcedDialoguesRoutine()
	{
		this.isLocked = true;
		yield return new WaitForSeconds(0.28f);
		List<Overworld_NPC> npcs = Overworld_NPC.npcs;
		Transform transform = null;
		foreach (Overworld_NPC overworld_NPC in npcs)
		{
			if (overworld_NPC.HasForcedConversation())
			{
				Debug.Log("Has forced conversation with " + overworld_NPC.name);
				if (this.moveToStartCoroutine != null)
				{
					base.StopCoroutine(this.moveToStartCoroutine);
				}
				this.SetTargetForInteraction(overworld_NPC.gameObject);
				transform = overworld_NPC.transform;
				break;
			}
		}
		if (transform == null)
		{
			this.isLocked = false;
			this.considerForcedDialoguesCoroutine = null;
			yield break;
		}
		this.isLocked = true;
		this.overworld_FollowAStarPath.enabled = true;
		this.overworld_FollowAStarPath.SetNewPath(transform.transform.position, 0);
		this.consideredLeaving = false;
		this.considerForcedDialoguesCoroutine = null;
		yield break;
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x00085E00 File Offset: 0x00084000
	public bool IsFreeToMove()
	{
		return Overworld_Manager.IsFreeToMove() && !this.isLocked;
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00085E18 File Offset: 0x00084018
	private void Update()
	{
		if (!this.IsFreeToMove() || !Overworld_Manager.main.IsState(Overworld_Manager.State.MOVING))
		{
			this.HideAllInteractionButtons();
			if (DigitalCursor.main.followObject && DigitalCursor.main.followObject.IsChildOf(base.transform))
			{
				DigitalCursor.main.ClearFollow();
			}
			this.myHeldForce = Vector2.zero;
			if (!this.isLocked)
			{
				this.SetAnimations(Vector2.zero);
			}
			return;
		}
		if (Vector2.Distance(base.transform.position, this.exitOffMap.position) < 3f && this.IsFreeToMove())
		{
			if (!this.consideredLeaving && Overworld_Manager.main.newRunButton.activeInHierarchy)
			{
				Overworld_RunManager overworld_RunManager = Object.FindObjectOfType<Overworld_RunManager>();
				if (overworld_RunManager)
				{
					overworld_RunManager.RunButton();
				}
				this.consideredLeaving = true;
				return;
			}
			this.consideredLeaving = true;
		}
		if (this.consideredLeaving)
		{
			this.consideredLeaving = false;
			this.overworld_FollowAStarPath.enabled = true;
			this.overworld_FollowAStarPath.ResetEarlyEnd();
			this.overworld_FollowAStarPath.SetNewPath(this.startTarget.position, 0);
			this.overworld_FollowAStarPath.UpdatePath();
			this.isLocked = true;
			return;
		}
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			DigitalCursor.main.ClearUIElement();
			DigitalCursor.main.FollowGameElement(this.controllerCursorPosition, false);
			DigitalCursor.main.Hide();
		}
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
		{
			if (Input.GetMouseButtonDown(0) && DigitalCursor.main.UIObjectSelected == null && DigitalCursor.main.NoUIElements() && !EventSystem.current.IsPointerOverGameObject())
			{
				Overworld_NPC.ReleaseAllNPCs();
				Overworld_Interactable closestInteractable = Overworld_Interactable.GetClosestInteractable(DigitalCursor.main.currentPosition, 0.5f);
				Transform transform = null;
				if (closestInteractable)
				{
					this.SetTargetForInteraction(closestInteractable.gameObject);
					using (IEnumerator enumerator = closestInteractable.transform.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							Transform transform2 = (Transform)obj;
							if (transform2.CompareTag("BuildingEntrance"))
							{
								transform = transform2.transform;
								break;
							}
						}
						goto IL_0238;
					}
				}
				this.SetTargetForInteraction(null);
				IL_0238:
				this.overworld_FollowAStarPath.enabled = true;
				this.zPositioner.enabled = true;
				this.overworld_FollowAStarPath.ResetEarlyEnd();
				if (transform)
				{
					this.overworld_FollowAStarPath.SetNewPath(transform.position, 0);
				}
				else
				{
					this.overworld_FollowAStarPath.UpdatePath();
				}
			}
			if (!Input.GetMouseButton(0))
			{
				if (this.holdingTime > 0.5f)
				{
					this.myHeldForce = Vector2.zero;
					this.SetAnimations(Vector2.zero);
					this.animator.SetBool("idle", true);
				}
				this.holdingTime = 0f;
				this.InteractWithKeyboard();
				return;
			}
			this.holdingTime += Time.deltaTime;
			if (this.holdingTime > 0.5f)
			{
				this.overworld_FollowAStarPath.enabled = false;
				this.FollowHeldMouse();
				return;
			}
		}
		else
		{
			if ((this.overworld_FollowAStarPath.enabled && !this.overworld_FollowAStarPath.reachedDestination && DigitalCursor.main.GetInputDown("confirm")) || DigitalCursor.main.GetInputDown("cancel"))
			{
				this.SetTargetForInteraction(null);
				this.overworld_FollowAStarPath.EndMove();
				return;
			}
			if (this.overworld_FollowAStarPath.enabled && this.overworld_FollowAStarPath.reachedDestination)
			{
				this.overworld_FollowAStarPath.enabled = false;
			}
			this.InteractWithController();
		}
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x000861BC File Offset: 0x000843BC
	public void MoveToAndInteractWithNPC(Overworld_NPC npc)
	{
		this.SetTargetForInteraction(npc.gameObject);
		this.overworld_FollowAStarPath.enabled = true;
		this.zPositioner.enabled = true;
		this.overworld_FollowAStarPath.ResetEarlyEnd();
		this.overworld_FollowAStarPath.SetNewPath(npc.transform.position, 0);
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x00086214 File Offset: 0x00084414
	private void InteractWithKeyboard()
	{
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
			this.overworld_FollowAStarPath.enabled = false;
			vector = vector.normalized * this.speed;
		}
		if (this.overworld_FollowAStarPath.enabled && this.overworld_FollowAStarPath.IsMoving())
		{
			return;
		}
		this.SetAnimations(vector);
		this.myHeldForce = vector;
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x00086300 File Offset: 0x00084500
	private void FollowHeldMouse()
	{
		Vector2 vector = DigitalCursor.main.transform.position;
		vector = new Vector2(Mathf.Clamp(vector.x, -23.5f, 40f), Mathf.Clamp(vector.y, -26f, 26f));
		if (Vector2.Distance(vector, base.transform.position) < 0.6f)
		{
			this.SetAnimations(Vector2.zero);
			return;
		}
		Vector2 vector2 = (vector - base.transform.position).normalized * this.speed;
		this.myHeldForce = vector2;
		this.SetAnimations(vector2);
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x000863B4 File Offset: 0x000845B4
	public void EndMove()
	{
		this.isLocked = false;
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x000863BD File Offset: 0x000845BD
	private void FixedUpdate()
	{
		if (this.myHeldForce != Vector2.zero && !this.overworld_FollowAStarPath.enabled)
		{
			this.rigidbody2D.velocity = this.myHeldForce;
		}
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x000863F0 File Offset: 0x000845F0
	public void SetTargetForInteraction(GameObject target)
	{
		if (this.targetForInteraction)
		{
			Overworld_NPC componentInParent = this.targetForInteraction.GetComponentInParent<Overworld_NPC>();
			if (componentInParent)
			{
				componentInParent.ReleaseNPC();
			}
		}
		if (!target)
		{
			this.overworld_FollowAStarPath.SetInteraction(null);
			this.targetForInteraction = null;
			return;
		}
		this.targetForInteraction = target.transform;
		Overworld_NPC componentInParent2 = target.GetComponentInParent<Overworld_NPC>();
		if (componentInParent2)
		{
			componentInParent2.WaitForInteraction();
			this.overworld_FollowAStarPath.SetInteraction(componentInParent2);
			return;
		}
		Overworld_Interactable componentInParent3 = target.GetComponentInParent<Overworld_Interactable>();
		this.overworld_FollowAStarPath.SetInteraction(componentInParent3);
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x00086481 File Offset: 0x00084681
	public Transform GetTargetForInteraction()
	{
		return this.targetForInteraction;
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x00086489 File Offset: 0x00084689
	public void StopMoving()
	{
		this.targetForInteraction = null;
		this.overworld_FollowAStarPath.SetInteraction(null);
		this.rigidbody2D.velocity = Vector2.zero;
		this.overworld_FollowAStarPath.EndMove();
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x000864B9 File Offset: 0x000846B9
	public void StopMoving(Vector2 lookAtPosition)
	{
		this.rigidbody2D.velocity = Vector2.zero;
		this.overworld_FollowAStarPath.EndMove();
		this.overworld_FollowAStarPath.FaceTowards(lookAtPosition);
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x000864E8 File Offset: 0x000846E8
	private void SetAnimations(Vector2 force)
	{
		this.timeSinceFootSteps += Time.deltaTime;
		if (force.magnitude < 0.05f)
		{
			force = Vector2.zero;
			this.animator.SetBool("idle", true);
		}
		else
		{
			this.animator.SetBool("idle", false);
		}
		if (Mathf.Abs(force.y) > Mathf.Abs(force.x))
		{
			if (force.y > 0f)
			{
				if (this.timeSinceFootSteps > 0.1f)
				{
					SoundManager.main.PlaySFXPitched("footstep", Random.Range(0.9f, 1.1f), 0.4f, false);
					this.timeSinceFootSteps = 0f;
				}
				this.animator.Play("move_up");
				this.controllerCursorPosition.localPosition = new Vector3(0f, 0.25f, 0f);
				return;
			}
			if (force.y < 0f)
			{
				if (this.timeSinceFootSteps > 0.1f)
				{
					SoundManager.main.PlaySFXPitched("footstep", Random.Range(0.9f, 1.1f), 0.4f, false);
					this.timeSinceFootSteps = 0f;
				}
				this.animator.Play("move_down");
				this.controllerCursorPosition.localPosition = new Vector3(0f, -0.5f, 0f);
				return;
			}
		}
		else
		{
			if (force.x > 0f)
			{
				if (this.timeSinceFootSteps > 0.1f)
				{
					SoundManager.main.PlaySFXPitched("footstep", Random.Range(0.9f, 1.1f), 0.4f, false);
					this.timeSinceFootSteps = 0f;
				}
				this.animator.Play("move_right");
				this.controllerCursorPosition.localPosition = new Vector3(0.5f, 0f, 0f);
				return;
			}
			if (force.x < 0f)
			{
				if (this.timeSinceFootSteps > 0.1f)
				{
					SoundManager.main.PlaySFXPitched("footstep", Random.Range(0.9f, 1.1f), 0.4f, false);
					this.timeSinceFootSteps = 0f;
				}
				this.animator.Play("move_left");
				this.controllerCursorPosition.localPosition = new Vector3(-0.5f, 0f, 0f);
			}
		}
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x00086744 File Offset: 0x00084944
	private void MovePurseWithController()
	{
		if (this.overworld_FollowAStarPath.enabled && this.overworld_FollowAStarPath.IsMoving())
		{
			return;
		}
		Vector2 vector = base.transform.position + DigitalCursor.main.moveLockVector;
		vector = new Vector2(Mathf.Clamp(vector.x, -23.5f, 40f), Mathf.Clamp(vector.y, -26f, 26f));
		this.myHeldForce = vector - base.transform.position;
		if (Mathf.Abs(this.myHeldForce.x) < 0.1f)
		{
			this.myHeldForce.x = 0f;
		}
		if (Mathf.Abs(this.myHeldForce.y) < 0.1f)
		{
			this.myHeldForce.y = 0f;
		}
		if (this.myHeldForce.magnitude > 0f)
		{
			this.myHeldForce.Normalize();
		}
		this.SetAnimations(this.myHeldForce);
		this.myHeldForce *= this.speed;
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x00086868 File Offset: 0x00084A68
	private Overworld_Purse.ButtonAndSprite GetButtonAndSprite(InputHandler.Key key)
	{
		foreach (Overworld_Purse.ButtonAndSprite buttonAndSprite in this.buttonAndSprites)
		{
			if (buttonAndSprite.key == key)
			{
				if (buttonAndSprite.interactionButton == null)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.interactionButtonPrefab, Vector3.zero, Quaternion.identity);
					buttonAndSprite.interactionButton = gameObject;
					buttonAndSprite.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
				}
				string keyName = InputHandler.GetKeyName(key);
				buttonAndSprite.spriteRenderer.sprite = DigitalCursor.main.GetSprite(keyName);
				return buttonAndSprite;
			}
		}
		return null;
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0008691C File Offset: 0x00084B1C
	private void HideAllInteractionButtons()
	{
		foreach (Overworld_Purse.ButtonAndSprite buttonAndSprite in this.buttonAndSprites)
		{
			if (buttonAndSprite.interactionButton != null)
			{
				buttonAndSprite.interactionButton.SetActive(false);
			}
		}
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x00086984 File Offset: 0x00084B84
	private void ShowInteractionButton(Transform trans, InputHandler.Key key)
	{
		Overworld_Purse.ButtonAndSprite buttonAndSprite = this.GetButtonAndSprite(key);
		buttonAndSprite.interactionButton.transform.position = trans.position + new Vector3(0f, 0.75f, 0f);
		buttonAndSprite.interactionButton.SetActive(true);
		if (buttonAndSprite.current == trans)
		{
			return;
		}
		buttonAndSprite.current = trans;
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x000869EC File Offset: 0x00084BEC
	private void InteractWithController()
	{
		this.HideAllInteractionButtons();
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.controller)
		{
			return;
		}
		if (this.isLocked)
		{
			return;
		}
		this.MovePurseWithController();
		foreach (Overworld_Interactable.ClosestOfType closestOfType in Overworld_Interactable.GetClosestInteractableOfEachType(base.transform.position, 1f))
		{
			Overworld_Interactable closestInteractable = closestOfType.closestInteractable;
			this.ShowInteractionButton(closestInteractable.transform, closestOfType.key);
			if (closestOfType.key == InputHandler.Key.A && DigitalCursor.main.GetInputDown("confirm"))
			{
				closestInteractable.Interact(InputHandler.Key.A);
				break;
			}
			if (closestOfType.key == InputHandler.Key.X && DigitalCursor.main.GetInputDown("contextMenu"))
			{
				closestInteractable.Interact(InputHandler.Key.X);
				break;
			}
		}
	}

	// Token: 0x04000AC9 RID: 2761
	public static Overworld_Purse main;

	// Token: 0x04000ACA RID: 2762
	[SerializeField]
	private List<Overworld_Purse.ButtonAndSprite> buttonAndSprites = new List<Overworld_Purse.ButtonAndSprite>();

	// Token: 0x04000ACB RID: 2763
	private Rigidbody2D rigidbody2D;

	// Token: 0x04000ACC RID: 2764
	public Overworld_FollowAStarPath overworld_FollowAStarPath;

	// Token: 0x04000ACD RID: 2765
	[SerializeField]
	private Overworld_Z_Positioner zPositioner;

	// Token: 0x04000ACE RID: 2766
	[SerializeField]
	private float speed = 5f;

	// Token: 0x04000ACF RID: 2767
	[SerializeField]
	private Transform exitOffMap;

	// Token: 0x04000AD0 RID: 2768
	[SerializeField]
	private Transform startTarget;

	// Token: 0x04000AD1 RID: 2769
	[SerializeField]
	private GameObject interactionButtonPrefab;

	// Token: 0x04000AD2 RID: 2770
	[SerializeField]
	public Transform targetForInteraction;

	// Token: 0x04000AD3 RID: 2771
	[SerializeField]
	public Transform controllerCursorPosition;

	// Token: 0x04000AD4 RID: 2772
	private float holdingTime;

	// Token: 0x04000AD5 RID: 2773
	private Animator animator;

	// Token: 0x04000AD6 RID: 2774
	public bool isLocked;

	// Token: 0x04000AD7 RID: 2775
	private Vector2 myHeldForce = Vector2.zero;

	// Token: 0x04000AD8 RID: 2776
	private Coroutine setZPositionCoroutine;

	// Token: 0x04000AD9 RID: 2777
	private Coroutine considerForcedDialoguesCoroutine;

	// Token: 0x04000ADA RID: 2778
	private Coroutine moveToStartCoroutine;

	// Token: 0x04000ADB RID: 2779
	private bool consideredLeaving = true;

	// Token: 0x04000ADC RID: 2780
	private float timeSinceFootSteps;

	// Token: 0x0200040B RID: 1035
	[Serializable]
	private class ButtonAndSprite
	{
		// Token: 0x040017C5 RID: 6085
		public InputHandler.Key key;

		// Token: 0x040017C6 RID: 6086
		[HideInInspector]
		public Transform current;

		// Token: 0x040017C7 RID: 6087
		[HideInInspector]
		public GameObject interactionButton;

		// Token: 0x040017C8 RID: 6088
		[HideInInspector]
		public SpriteRenderer spriteRenderer;
	}
}
