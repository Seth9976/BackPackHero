using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C1 RID: 193
	internal static class UIEventRegistration
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x00017B58 File Offset: 0x00015D58
		static UIEventRegistration()
		{
			GUIUtility.takeCapture = (Action)Delegate.Combine(GUIUtility.takeCapture, delegate
			{
				UIEventRegistration.TakeCapture();
			});
			GUIUtility.releaseCapture = (Action)Delegate.Combine(GUIUtility.releaseCapture, delegate
			{
				UIEventRegistration.ReleaseCapture();
			});
			GUIUtility.processEvent = (Func<int, IntPtr, bool>)Delegate.Combine(GUIUtility.processEvent, (int i, IntPtr ptr) => UIEventRegistration.ProcessEvent(i, ptr));
			GUIUtility.cleanupRoots = (Action)Delegate.Combine(GUIUtility.cleanupRoots, delegate
			{
				UIEventRegistration.CleanupRoots();
			});
			GUIUtility.endContainerGUIFromException = (Func<Exception, bool>)Delegate.Combine(GUIUtility.endContainerGUIFromException, (Exception exception) => UIEventRegistration.EndContainerGUIFromException(exception));
			GUIUtility.guiChanged = (Action)Delegate.Combine(GUIUtility.guiChanged, delegate
			{
				UIEventRegistration.MakeCurrentIMGUIContainerDirty();
			});
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00017C48 File Offset: 0x00015E48
		internal static void RegisterUIElementSystem(IUIElementsUtility utility)
		{
			UIEventRegistration.s_Utilities.Insert(0, utility);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00017C58 File Offset: 0x00015E58
		private static void TakeCapture()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.TakeCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00017CB4 File Offset: 0x00015EB4
		private static void ReleaseCapture()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.ReleaseCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00017D10 File Offset: 0x00015F10
		private static bool EndContainerGUIFromException(Exception exception)
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.EndContainerGUIFromException(exception);
				if (flag)
				{
					return true;
				}
			}
			return GUIUtility.ShouldRethrowException(exception);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00017D7C File Offset: 0x00015F7C
		private static bool ProcessEvent(int instanceID, IntPtr nativeEventPtr)
		{
			bool flag = false;
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag2 = iuielementsUtility.ProcessEvent(instanceID, nativeEventPtr, ref flag);
				if (flag2)
				{
					return flag;
				}
			}
			return false;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00017DEC File Offset: 0x00015FEC
		private static void CleanupRoots()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.CleanupRoots();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00017E48 File Offset: 0x00016048
		internal static void MakeCurrentIMGUIContainerDirty()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = iuielementsUtility.MakeCurrentIMGUIContainerDirty();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00017EA4 File Offset: 0x000160A4
		internal static void UpdateSchedulers()
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				iuielementsUtility.UpdateSchedulers();
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00017EFC File Offset: 0x000160FC
		internal static void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback)
		{
			foreach (IUIElementsUtility iuielementsUtility in UIEventRegistration.s_Utilities)
			{
				iuielementsUtility.RequestRepaintForPanels(repaintCallback);
			}
		}

		// Token: 0x0400028A RID: 650
		private static List<IUIElementsUtility> s_Utilities = new List<IUIElementsUtility>();
	}
}
