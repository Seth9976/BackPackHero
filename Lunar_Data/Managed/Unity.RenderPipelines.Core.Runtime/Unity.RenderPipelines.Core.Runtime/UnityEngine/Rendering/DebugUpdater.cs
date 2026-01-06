using System;
using System.Collections;
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.UI;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006E RID: 110
	internal class DebugUpdater : MonoBehaviour
	{
		// Token: 0x06000380 RID: 896 RVA: 0x000113A5 File Offset: 0x0000F5A5
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void RuntimeInit()
		{
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000113A7 File Offset: 0x0000F5A7
		internal static void SetEnabled(bool enabled)
		{
			if (enabled)
			{
				DebugUpdater.EnableRuntime();
				return;
			}
			DebugUpdater.DisableRuntime();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000113B8 File Offset: 0x0000F5B8
		private static void EnableRuntime()
		{
			if (DebugUpdater.s_Instance != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "[Debug Updater]";
			DebugUpdater.s_Instance = gameObject.AddComponent<DebugUpdater>();
			DebugUpdater.s_Instance.m_Orientation = Screen.orientation;
			Object.DontDestroyOnLoad(gameObject);
			DebugManager.instance.EnableInputActions();
			EnhancedTouchSupport.Enable();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011411 File Offset: 0x0000F611
		private static void DisableRuntime()
		{
			DebugManager instance = DebugManager.instance;
			instance.displayRuntimeUI = false;
			instance.displayPersistentRuntimeUI = false;
			if (DebugUpdater.s_Instance != null)
			{
				CoreUtils.Destroy(DebugUpdater.s_Instance.gameObject);
				DebugUpdater.s_Instance = null;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011447 File Offset: 0x0000F647
		internal static void HandleInternalEventSystemComponents(bool uiEnabled)
		{
			if (DebugUpdater.s_Instance == null)
			{
				return;
			}
			if (uiEnabled)
			{
				DebugUpdater.s_Instance.EnsureExactlyOneEventSystem();
				return;
			}
			DebugUpdater.s_Instance.DestroyDebugEventSystem();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00011470 File Offset: 0x0000F670
		private void EnsureExactlyOneEventSystem()
		{
			EventSystem[] array = Object.FindObjectsOfType<EventSystem>();
			EventSystem component = base.GetComponent<EventSystem>();
			if (array.Length > 1 && component != null)
			{
				Debug.Log("More than one EventSystem detected in scene. Destroying EventSystem owned by DebugUpdater.");
				this.DestroyDebugEventSystem();
				return;
			}
			if (array.Length == 0)
			{
				Debug.Log("No EventSystem available. Creating a new EventSystem to enable Rendering Debugger runtime UI.");
				this.CreateDebugEventSystem();
				return;
			}
			base.StartCoroutine(this.DoAfterInputModuleUpdated(new Action(this.CheckInputModuleExists)));
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000114D8 File Offset: 0x0000F6D8
		private IEnumerator DoAfterInputModuleUpdated(Action action)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			action();
			yield break;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000114E7 File Offset: 0x0000F6E7
		private void CheckInputModuleExists()
		{
			if (EventSystem.current != null && EventSystem.current.currentInputModule == null)
			{
				Debug.LogWarning("Found a game object with EventSystem component but no corresponding BaseInputModule component - Debug UI input might not work correctly.");
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011514 File Offset: 0x0000F714
		private void AssignDefaultActions()
		{
			if (EventSystem.current != null)
			{
				InputSystemUIInputModule inputSystemUIInputModule = EventSystem.current.currentInputModule as InputSystemUIInputModule;
				if (inputSystemUIInputModule != null)
				{
					MethodInfo method = inputSystemUIInputModule.GetType().GetMethod("AssignDefaultActions");
					if (method != null)
					{
						method.Invoke(inputSystemUIInputModule, null);
					}
				}
			}
			this.CheckInputModuleExists();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001156A File Offset: 0x0000F76A
		private void CreateDebugEventSystem()
		{
			base.gameObject.AddComponent<EventSystem>();
			base.gameObject.AddComponent<InputSystemUIInputModule>();
			base.StartCoroutine(this.DoAfterInputModuleUpdated(new Action(this.AssignDefaultActions)));
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000115A0 File Offset: 0x0000F7A0
		private void DestroyDebugEventSystem()
		{
			Object component = base.GetComponent<EventSystem>();
			InputSystemUIInputModule component2 = base.GetComponent<InputSystemUIInputModule>();
			if (component2)
			{
				CoreUtils.Destroy(component2);
				base.StartCoroutine(this.DoAfterInputModuleUpdated(new Action(this.AssignDefaultActions)));
			}
			CoreUtils.Destroy(component);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000115E8 File Offset: 0x0000F7E8
		private void Update()
		{
			DebugManager instance = DebugManager.instance;
			if (this.m_RuntimeUiWasVisibleLastFrame != instance.displayRuntimeUI)
			{
				DebugUpdater.HandleInternalEventSystemComponents(instance.displayRuntimeUI);
			}
			instance.UpdateActions();
			if (instance.GetAction(DebugAction.EnableDebugMenu) != 0f || instance.GetActionToggleDebugMenuWithTouch())
			{
				instance.displayRuntimeUI = !instance.displayRuntimeUI;
			}
			if (instance.displayRuntimeUI)
			{
				if (instance.GetAction(DebugAction.ResetAll) != 0f)
				{
					instance.Reset();
				}
				if (instance.GetActionReleaseScrollTarget())
				{
					instance.SetScrollTarget(null);
				}
			}
			if (this.m_Orientation != Screen.orientation)
			{
				base.StartCoroutine(DebugUpdater.RefreshRuntimeUINextFrame());
				this.m_Orientation = Screen.orientation;
			}
			this.m_RuntimeUiWasVisibleLastFrame = instance.displayRuntimeUI;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001169A File Offset: 0x0000F89A
		private static IEnumerator RefreshRuntimeUINextFrame()
		{
			yield return null;
			DebugManager.instance.ReDrawOnScreenDebug();
			yield break;
		}

		// Token: 0x04000241 RID: 577
		private static DebugUpdater s_Instance;

		// Token: 0x04000242 RID: 578
		private ScreenOrientation m_Orientation;

		// Token: 0x04000243 RID: 579
		private bool m_RuntimeUiWasVisibleLastFrame;
	}
}
