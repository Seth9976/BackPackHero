using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.Rendering
{
	// Token: 0x02000040 RID: 64
	public class FreeCamera : MonoBehaviour
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0000C8B2 File Offset: 0x0000AAB2
		private void OnEnable()
		{
			this.RegisterInputs();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C8BC File Offset: 0x0000AABC
		private void RegisterInputs()
		{
			InputActionMap inputActionMap = new InputActionMap("Free Camera");
			this.lookAction = inputActionMap.AddAction("look", InputActionType.Value, "<Mouse>/delta", null, null, null, null);
			this.moveAction = inputActionMap.AddAction("move", InputActionType.Value, "<Gamepad>/leftStick", null, null, null, null);
			this.speedAction = inputActionMap.AddAction("speed", InputActionType.Value, "<Gamepad>/dpad", null, null, null, null);
			this.yMoveAction = inputActionMap.AddAction("yMove", InputActionType.Value, null, null, null, null, null);
			this.lookAction.AddBinding("<Gamepad>/rightStick", null, null, null).WithProcessor("scaleVector2(x=15, y=15)");
			this.moveAction.AddCompositeBinding("Dpad", null, null).With("Up", "<Keyboard>/w", null, null).With("Up", "<Keyboard>/upArrow", null, null)
				.With("Down", "<Keyboard>/s", null, null)
				.With("Down", "<Keyboard>/downArrow", null, null)
				.With("Left", "<Keyboard>/a", null, null)
				.With("Left", "<Keyboard>/leftArrow", null, null)
				.With("Right", "<Keyboard>/d", null, null)
				.With("Right", "<Keyboard>/rightArrow", null, null);
			this.speedAction.AddCompositeBinding("Dpad", null, null).With("Up", "<Keyboard>/home", null, null).With("Down", "<Keyboard>/end", null, null);
			this.yMoveAction.AddCompositeBinding("Dpad", null, null).With("Up", "<Keyboard>/pageUp", null, null).With("Down", "<Keyboard>/pageDown", null, null)
				.With("Up", "<Keyboard>/e", null, null)
				.With("Down", "<Keyboard>/q", null, null)
				.With("Up", "<Gamepad>/rightshoulder", null, null)
				.With("Down", "<Gamepad>/leftshoulder", null, null);
			this.moveAction.Enable();
			this.lookAction.Enable();
			this.speedAction.Enable();
			this.yMoveAction.Enable();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000CB04 File Offset: 0x0000AD04
		private void UpdateInputs()
		{
			this.inputRotateAxisX = 0f;
			this.inputRotateAxisY = 0f;
			this.leftShiftBoost = false;
			this.fire1 = false;
			Vector2 vector = this.lookAction.ReadValue<Vector2>();
			this.inputRotateAxisX = vector.x * this.m_LookSpeedMouse * 0.01f;
			this.inputRotateAxisY = vector.y * this.m_LookSpeedMouse * 0.01f;
			Keyboard current = Keyboard.current;
			bool? flag;
			if (current == null)
			{
				flag = null;
			}
			else
			{
				KeyControl leftShiftKey = current.leftShiftKey;
				flag = ((leftShiftKey != null) ? new bool?(leftShiftKey.isPressed) : null);
			}
			bool? flag2 = flag;
			this.leftShift = flag2.GetValueOrDefault();
			Mouse current2 = Mouse.current;
			bool flag3;
			if (current2 == null)
			{
				flag3 = false;
			}
			else
			{
				ButtonControl leftButton = current2.leftButton;
				flag2 = ((leftButton != null) ? new bool?(leftButton.isPressed) : null);
				bool flag4 = true;
				flag3 = (flag2.GetValueOrDefault() == flag4) & (flag2 != null);
			}
			bool flag5;
			if (!flag3)
			{
				Gamepad current3 = Gamepad.current;
				if (current3 == null)
				{
					flag5 = false;
				}
				else
				{
					ButtonControl xButton = current3.xButton;
					flag2 = ((xButton != null) ? new bool?(xButton.isPressed) : null);
					bool flag4 = true;
					flag5 = (flag2.GetValueOrDefault() == flag4) & (flag2 != null);
				}
			}
			else
			{
				flag5 = true;
			}
			this.fire1 = flag5;
			this.inputChangeSpeed = this.speedAction.ReadValue<Vector2>().y;
			Vector2 vector2 = this.moveAction.ReadValue<Vector2>();
			this.inputVertical = vector2.y;
			this.inputHorizontal = vector2.x;
			this.inputYAxis = this.yMoveAction.ReadValue<Vector2>().y;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000CC90 File Offset: 0x0000AE90
		private void Update()
		{
			if (DebugManager.instance.displayRuntimeUI)
			{
				return;
			}
			this.UpdateInputs();
			if (this.inputChangeSpeed != 0f)
			{
				this.m_MoveSpeed += this.inputChangeSpeed * this.m_MoveSpeedIncrement;
				if (this.m_MoveSpeed < this.m_MoveSpeedIncrement)
				{
					this.m_MoveSpeed = this.m_MoveSpeedIncrement;
				}
			}
			if (this.inputRotateAxisX != 0f || this.inputRotateAxisY != 0f || this.inputVertical != 0f || this.inputHorizontal != 0f || this.inputYAxis != 0f)
			{
				float x = base.transform.localEulerAngles.x;
				float num = base.transform.localEulerAngles.y + this.inputRotateAxisX;
				float num2 = x - this.inputRotateAxisY;
				if (x <= 90f && num2 >= 0f)
				{
					num2 = Mathf.Clamp(num2, 0f, 90f);
				}
				if (x >= 270f)
				{
					num2 = Mathf.Clamp(num2, 270f, 360f);
				}
				base.transform.localRotation = Quaternion.Euler(num2, num, base.transform.localEulerAngles.z);
				float num3 = Time.deltaTime * this.m_MoveSpeed;
				if (this.fire1 || (this.leftShiftBoost && this.leftShift))
				{
					num3 *= this.m_Turbo;
				}
				base.transform.position += base.transform.forward * num3 * this.inputVertical;
				base.transform.position += base.transform.right * num3 * this.inputHorizontal;
				base.transform.position += Vector3.up * num3 * this.inputYAxis;
			}
		}

		// Token: 0x0400018D RID: 397
		private const float k_MouseSensitivityMultiplier = 0.01f;

		// Token: 0x0400018E RID: 398
		public float m_LookSpeedController = 120f;

		// Token: 0x0400018F RID: 399
		public float m_LookSpeedMouse = 4f;

		// Token: 0x04000190 RID: 400
		public float m_MoveSpeed = 10f;

		// Token: 0x04000191 RID: 401
		public float m_MoveSpeedIncrement = 2.5f;

		// Token: 0x04000192 RID: 402
		public float m_Turbo = 10f;

		// Token: 0x04000193 RID: 403
		private InputAction lookAction;

		// Token: 0x04000194 RID: 404
		private InputAction moveAction;

		// Token: 0x04000195 RID: 405
		private InputAction speedAction;

		// Token: 0x04000196 RID: 406
		private InputAction yMoveAction;

		// Token: 0x04000197 RID: 407
		private float inputRotateAxisX;

		// Token: 0x04000198 RID: 408
		private float inputRotateAxisY;

		// Token: 0x04000199 RID: 409
		private float inputChangeSpeed;

		// Token: 0x0400019A RID: 410
		private float inputVertical;

		// Token: 0x0400019B RID: 411
		private float inputHorizontal;

		// Token: 0x0400019C RID: 412
		private float inputYAxis;

		// Token: 0x0400019D RID: 413
		private bool leftShiftBoost;

		// Token: 0x0400019E RID: 414
		private bool leftShift;

		// Token: 0x0400019F RID: 415
		private bool fire1;
	}
}
