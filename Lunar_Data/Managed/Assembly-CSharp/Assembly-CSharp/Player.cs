using System;
using SaveSystem.States;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x0200006E RID: 110
public class Player : MonoBehaviour
{
	// Token: 0x06000311 RID: 785 RVA: 0x0000F930 File Offset: 0x0000DB30
	private void OnEnable()
	{
		if (Player.instance == null)
		{
			Player.instance = this;
		}
		this.inputActions = new InputActions();
		this.inputActions.Enable();
		Singleton.ControlType controlType = Singleton.instance.controlType;
		if (controlType == Singleton.ControlType.Xbox)
		{
			this.inputActions.Default.Movement.performed += delegate(InputAction.CallbackContext ctx)
			{
				this.movementVector = ctx.ReadValue<Vector2>();
			};
			this.inputActions.Default.Movement.canceled += delegate(InputAction.CallbackContext ctx)
			{
				this.movementVector = Vector2.zero;
			};
			this.inputActions.Default.Cancel.started += delegate(InputAction.CallbackContext ctx)
			{
				if (InputManager.instance.IsPlayerMovementMode() && InputManager.instance.IsGameInput())
				{
					this.startedWaiting = true;
				}
			};
			this.inputActions.Default.Cancel.performed += delegate(InputAction.CallbackContext ctx)
			{
				if (this.startedWaiting)
				{
					this.isWaiting = true;
				}
			};
			this.inputActions.Default.Cancel.canceled += delegate(InputAction.CallbackContext ctx)
			{
				this.isWaiting = false;
				this.startedWaiting = false;
			};
			this.inputActions.Default.ViewDecks.performed += delegate(InputAction.CallbackContext ctx)
			{
				this.isLockedViewingDeck = true;
			};
			this.inputActions.Default.ViewDecks.canceled += delegate(InputAction.CallbackContext ctx)
			{
				this.isLockedViewingDeck = false;
			};
			return;
		}
		if (controlType != Singleton.ControlType.Switch)
		{
			return;
		}
		this.inputActions.Switch.Movement.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = ctx.ReadValue<Vector2>();
		};
		this.inputActions.Switch.Movement.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = Vector2.zero;
		};
		this.inputActions.Switch.Cancel.started += delegate(InputAction.CallbackContext ctx)
		{
			if (InputManager.instance.IsPlayerMovementMode() && InputManager.instance.IsGameInput())
			{
				this.startedWaiting = true;
			}
		};
		this.inputActions.Switch.Cancel.performed += delegate(InputAction.CallbackContext ctx)
		{
			if (this.startedWaiting)
			{
				this.isWaiting = true;
			}
		};
		this.inputActions.Switch.Cancel.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.isWaiting = false;
			this.startedWaiting = false;
		};
		this.inputActions.Switch.ViewDecks.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.isLockedViewingDeck = true;
		};
		this.inputActions.Switch.ViewDecks.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.isLockedViewingDeck = false;
		};
	}

	// Token: 0x06000312 RID: 786 RVA: 0x0000FB78 File Offset: 0x0000DD78
	private void OnDisable()
	{
		if (Player.instance == this)
		{
			Player.instance = null;
		}
		this.movement.Disable();
		this.movement.performed -= delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = ctx.ReadValue<Vector2>();
		};
		this.movement.canceled -= delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = Vector2.zero;
		};
		this.waitAction.Disable();
		this.waitAction.performed -= delegate(InputAction.CallbackContext ctx)
		{
			this.isWaiting = true;
		};
		this.waitAction.canceled -= delegate(InputAction.CallbackContext ctx)
		{
			this.isWaiting = false;
		};
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0000FC0A File Offset: 0x0000DE0A
	private void Start()
	{
		this.animator.Play("playerIdle", 0, 0f);
		this.usingItemSpriteRenderer.enabled = false;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x0000FC30 File Offset: 0x0000DE30
	private void Update()
	{
		if (this.isDead || this.isWinning)
		{
			TimeManager.instance.setTimeScale = 1f;
			this.animator.speed = 1f;
			this.rb.velocity = Vector2.zero;
			return;
		}
		if (!InputManager.instance.IsPlayerMovementMode() || !InputManager.instance.IsGameInput())
		{
			TimeManager.instance.setTimeScale = 0f;
			this.animator.speed = 0f;
			this.rb.velocity = Vector2.zero;
			return;
		}
		this.HandleAnimation();
		this.facingDirection = Vector2.MoveTowards(this.facingDirection, this.nextFacingDirection, Time.deltaTime * TimeManager.instance.currentTimeScale * 10f);
		if (this.isWaiting && this.movementVector.magnitude == 0f)
		{
			TimeManager.instance.setTimeScale = 1f;
			this.animator.speed = 1f;
			this.rb.velocity = Vector2.zero;
			return;
		}
		if (this.movementVector.magnitude < 0.1f || this.isLockedViewingDeck)
		{
			this.movementVector = Vector2.zero;
		}
		else if (this.movementVector.magnitude < 0.5f)
		{
			this.movementVector = this.movementVector.normalized * 0.5f;
		}
		else if (this.movementVector.magnitude > 1f)
		{
			this.movementVector.Normalize();
		}
		if (this.movementVector != Vector2.zero)
		{
			this.nextFacingDirection = this.movementVector;
		}
		TimeManager.instance.setTimeScale = Mathf.Max(0f, this.movementVector.magnitude);
		this.animator.speed = this.movementVector.magnitude;
		this.rb.velocity = this.movementVector * this.GetSpeed();
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0000FE24 File Offset: 0x0000E024
	private void HandleAnimation()
	{
		if (this.movementVector.magnitude >= 0.1f && !this.isLockedViewingDeck)
		{
			this.animator.SetBool("isWalking", true);
			if (this.movementVector.x > 0.1f)
			{
				this.spriteRenderer.flipX = false;
				return;
			}
			if (this.movementVector.x < -0.1f)
			{
				this.spriteRenderer.flipX = true;
				return;
			}
		}
		else if (this.isWaiting)
		{
			this.animator.SetBool("isWalking", false);
		}
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0000FEB3 File Offset: 0x0000E0B3
	public void ShowConsiderUseItem(Sprite sprite)
	{
		this.spriteRenderer.flipX = false;
		this.usingItemSpriteRenderer.sprite = sprite;
		this.animator.SetTrigger("considerUsingItem");
		this.usingItemSpriteRenderer.enabled = true;
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0000FEE9 File Offset: 0x0000E0E9
	public void EndConsiderUseItem()
	{
		this.usingItemSpriteRenderer.enabled = false;
		this.animator.SetTrigger("default");
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0000FF08 File Offset: 0x0000E108
	public float GetSpeed()
	{
		float num = this.speed;
		float modifierPercentage = ModifierManager.instance.GetModifierPercentage(Modifier.ModifierEffect.Type.SpeedPercentBoostOnFire);
		if (GroundDetector.instance.OnFire() && modifierPercentage > 0f)
		{
			num *= modifierPercentage;
		}
		return num;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0000FF41 File Offset: 0x0000E141
	public void Die()
	{
		if (this.isDead || this.isWinning)
		{
			return;
		}
		Singleton.instance.AddAccomplishment(ProgressState.Accomplishment.TimesDied, 1);
		this.isDead = true;
		this.animator.SetTrigger("die");
		GameOverManager.instance.ShowGameOver();
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0000FF81 File Offset: 0x0000E181
	public Vector2 GetFacingDirection()
	{
		return this.facingDirection;
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0000FF89 File Offset: 0x0000E189
	public void Win()
	{
		if (this.isWinning)
		{
			return;
		}
		this.isWinning = true;
		this.animator.SetTrigger("win");
		SoundManager.instance.PlaySFX("winning", double.PositiveInfinity);
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
	public void StartNewLevel()
	{
		this.isDead = false;
		this.isWinning = false;
		this.animator.SetTrigger("default");
		if (RoomManager.instance.currentRoom.playerSpawnPosition)
		{
			base.transform.position = RoomManager.instance.currentRoom.playerSpawnPosition.position;
			return;
		}
		base.transform.position = RoomManager.instance.currentRoom.GetCenter();
	}

	// Token: 0x0600031D RID: 797 RVA: 0x00010044 File Offset: 0x0000E244
	public Sprite GetSprite()
	{
		return this.spriteRenderer.sprite;
	}

	// Token: 0x04000257 RID: 599
	public static Player instance;

	// Token: 0x04000258 RID: 600
	[SerializeField]
	private SpriteRenderer usingItemSpriteRenderer;

	// Token: 0x04000259 RID: 601
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400025A RID: 602
	[SerializeField]
	private Animator animator;

	// Token: 0x0400025B RID: 603
	[SerializeField]
	public Rigidbody2D rb;

	// Token: 0x0400025C RID: 604
	[SerializeField]
	private float speed = 5f;

	// Token: 0x0400025D RID: 605
	public bool isWinning;

	// Token: 0x0400025E RID: 606
	public bool isDead;

	// Token: 0x0400025F RID: 607
	public InputAction movement;

	// Token: 0x04000260 RID: 608
	public InputAction waitAction;

	// Token: 0x04000261 RID: 609
	private Vector2 movementVector;

	// Token: 0x04000262 RID: 610
	private Vector2 facingDirection;

	// Token: 0x04000263 RID: 611
	private Vector2 nextFacingDirection;

	// Token: 0x04000264 RID: 612
	public bool startedWaiting;

	// Token: 0x04000265 RID: 613
	public bool isWaiting;

	// Token: 0x04000266 RID: 614
	public bool isLockedViewingDeck;

	// Token: 0x04000267 RID: 615
	private InputActions inputActions;
}
