using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000031 RID: 49
	internal static class GroupBoxUtility
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00006689 File Offset: 0x00004889
		public static void RegisterGroupBoxOptionCallbacks<T>(this T option) where T : VisualElement, IGroupBoxOption
		{
			option.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(GroupBoxUtility.OnOptionAttachToPanel), TrickleDown.NoTrickleDown);
			option.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(GroupBoxUtility.OnOptionDetachFromPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000066C0 File Offset: 0x000048C0
		public static void OnOptionSelected<T>(this T selectedOption) where T : VisualElement, IGroupBoxOption
		{
			bool flag = !GroupBoxUtility.s_GroupOptionManagerCache.ContainsKey(selectedOption);
			if (!flag)
			{
				GroupBoxUtility.s_GroupOptionManagerCache[selectedOption].OnOptionSelectionChanged(selectedOption);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006704 File Offset: 0x00004904
		public static IGroupBoxOption GetSelectedOption(this IGroupBox groupBox)
		{
			return (!GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox)) ? null : GroupBoxUtility.s_GroupManagers[groupBox].GetSelectedOption();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006738 File Offset: 0x00004938
		public static IGroupManager GetGroupManager(this IGroupBox groupBox)
		{
			return GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox) ? GroupBoxUtility.s_GroupManagers[groupBox] : null;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006768 File Offset: 0x00004968
		private static void OnOptionAttachToPanel(AttachToPanelEvent evt)
		{
			VisualElement visualElement = evt.currentTarget as VisualElement;
			IGroupBoxOption groupBoxOption = evt.currentTarget as IGroupBoxOption;
			IGroupManager groupManager = null;
			for (VisualElement visualElement2 = visualElement.hierarchy.parent; visualElement2 != null; visualElement2 = visualElement2.hierarchy.parent)
			{
				IGroupBox groupBox = visualElement2 as IGroupBox;
				bool flag = groupBox != null;
				if (flag)
				{
					groupManager = GroupBoxUtility.FindOrCreateGroupManager(groupBox);
					break;
				}
			}
			bool flag2 = groupManager == null;
			if (flag2)
			{
				groupManager = GroupBoxUtility.FindOrCreateGroupManager(visualElement.elementPanel);
			}
			groupManager.RegisterOption(groupBoxOption);
			GroupBoxUtility.s_GroupOptionManagerCache[groupBoxOption] = groupManager;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000680C File Offset: 0x00004A0C
		private static void OnOptionDetachFromPanel(DetachFromPanelEvent evt)
		{
			IGroupBoxOption groupBoxOption = evt.currentTarget as IGroupBoxOption;
			bool flag = !GroupBoxUtility.s_GroupOptionManagerCache.ContainsKey(groupBoxOption);
			if (!flag)
			{
				GroupBoxUtility.s_GroupOptionManagerCache[groupBoxOption].UnregisterOption(groupBoxOption);
				GroupBoxUtility.s_GroupOptionManagerCache.Remove(groupBoxOption);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006858 File Offset: 0x00004A58
		private static IGroupManager FindOrCreateGroupManager(IGroupBox groupBox)
		{
			bool flag = GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox);
			IGroupManager groupManager;
			if (flag)
			{
				groupManager = GroupBoxUtility.s_GroupManagers[groupBox];
			}
			else
			{
				Type type = null;
				foreach (Type type2 in groupBox.GetType().GetInterfaces())
				{
					bool flag2 = type2.IsGenericType && GroupBoxUtility.k_GenericGroupBoxType.IsAssignableFrom(type2.GetGenericTypeDefinition());
					if (flag2)
					{
						type = type2.GetGenericArguments()[0];
						break;
					}
				}
				IGroupManager groupManager3;
				if (type == null)
				{
					IGroupManager groupManager2 = new DefaultGroupManager();
					groupManager3 = groupManager2;
				}
				else
				{
					groupManager3 = (IGroupManager)Activator.CreateInstance(type);
				}
				IGroupManager groupManager4 = groupManager3;
				BaseVisualElementPanel baseVisualElementPanel = groupBox as BaseVisualElementPanel;
				bool flag3 = baseVisualElementPanel != null;
				if (flag3)
				{
					baseVisualElementPanel.panelDisposed += new Action<BaseVisualElementPanel>(GroupBoxUtility.OnPanelDestroyed);
				}
				else
				{
					VisualElement visualElement = groupBox as VisualElement;
					bool flag4 = visualElement != null;
					if (flag4)
					{
						visualElement.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(GroupBoxUtility.OnGroupBoxDetachedFromPanel), TrickleDown.NoTrickleDown);
					}
				}
				GroupBoxUtility.s_GroupManagers[groupBox] = groupManager4;
				groupManager = groupManager4;
			}
			return groupManager;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006964 File Offset: 0x00004B64
		private static void OnGroupBoxDetachedFromPanel(DetachFromPanelEvent evt)
		{
			GroupBoxUtility.s_GroupManagers.Remove(evt.currentTarget as IGroupBox);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000697D File Offset: 0x00004B7D
		private static void OnPanelDestroyed(BaseVisualElementPanel panel)
		{
			GroupBoxUtility.s_GroupManagers.Remove(panel);
			panel.panelDisposed -= new Action<BaseVisualElementPanel>(GroupBoxUtility.OnPanelDestroyed);
		}

		// Token: 0x0400008D RID: 141
		private static Dictionary<IGroupBox, IGroupManager> s_GroupManagers = new Dictionary<IGroupBox, IGroupManager>();

		// Token: 0x0400008E RID: 142
		private static Dictionary<IGroupBoxOption, IGroupManager> s_GroupOptionManagerCache = new Dictionary<IGroupBoxOption, IGroupManager>();

		// Token: 0x0400008F RID: 143
		private static readonly Type k_GenericGroupBoxType = typeof(IGroupBox<>);
	}
}
