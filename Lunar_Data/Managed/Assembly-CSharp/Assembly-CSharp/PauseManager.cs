using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x02000065 RID: 101
public class PauseManager : MonoBehaviour
{
	// Token: 0x060002E0 RID: 736 RVA: 0x0000ED48 File Offset: 0x0000CF48
	private void OnEnable()
	{
		this.inputActions = new InputActions();
		this.inputActions.Enable();
		Singleton.ControlType controlType = Singleton.instance.controlType;
		if (controlType == Singleton.ControlType.Xbox)
		{
			this.inputActions.Default.Pause.performed += delegate(InputAction.CallbackContext ctx)
			{
				this.Pause();
			};
			return;
		}
		if (controlType != Singleton.ControlType.Switch)
		{
			return;
		}
		this.inputActions.Switch.Pause.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.Pause();
		};
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0000EDC7 File Offset: 0x0000CFC7
	private void Pause()
	{
		if (InputManager.instance && !InputManager.instance.IsGameInput())
		{
			return;
		}
		Object.Instantiate<GameObject>(this.pausePanel, CanvasManager.instance.masterContentScaler);
	}

	// Token: 0x0400022F RID: 559
	[SerializeField]
	private GameObject pausePanel;

	// Token: 0x04000230 RID: 560
	private InputActions inputActions;
}
