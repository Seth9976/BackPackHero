using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E3 RID: 483
	internal static class EventDispatchUtilities
	{
		// Token: 0x06000EFF RID: 3839 RVA: 0x0003C340 File Offset: 0x0003A540
		public static void PropagateEvent(EventBase evt)
		{
			Debug.Assert(!evt.dispatch, "Event is being dispatched recursively.");
			evt.dispatch = true;
			bool flag = evt.path == null;
			if (flag)
			{
				CallbackEventHandler callbackEventHandler = evt.target as CallbackEventHandler;
				if (callbackEventHandler != null)
				{
					callbackEventHandler.HandleEventAtTargetPhase(evt);
				}
			}
			else
			{
				bool tricklesDown = evt.tricklesDown;
				if (tricklesDown)
				{
					evt.propagationPhase = PropagationPhase.TrickleDown;
					for (int i = evt.path.trickleDownPath.Count - 1; i >= 0; i--)
					{
						bool isPropagationStopped = evt.isPropagationStopped;
						if (isPropagationStopped)
						{
							break;
						}
						bool flag2 = evt.Skip(evt.path.trickleDownPath[i]);
						if (!flag2)
						{
							evt.currentTarget = evt.path.trickleDownPath[i];
							evt.currentTarget.HandleEvent(evt);
						}
					}
				}
				evt.propagationPhase = PropagationPhase.AtTarget;
				foreach (VisualElement visualElement in evt.path.targetElements)
				{
					bool flag3 = evt.Skip(visualElement);
					if (!flag3)
					{
						evt.target = visualElement;
						evt.currentTarget = evt.target;
						evt.currentTarget.HandleEvent(evt);
					}
				}
				evt.propagationPhase = PropagationPhase.DefaultActionAtTarget;
				foreach (VisualElement visualElement2 in evt.path.targetElements)
				{
					bool flag4 = evt.Skip(visualElement2);
					if (!flag4)
					{
						evt.target = visualElement2;
						evt.currentTarget = evt.target;
						evt.currentTarget.HandleEvent(evt);
					}
				}
				evt.target = evt.leafTarget;
				bool bubbles = evt.bubbles;
				if (bubbles)
				{
					evt.propagationPhase = PropagationPhase.BubbleUp;
					foreach (VisualElement visualElement3 in evt.path.bubbleUpPath)
					{
						bool flag5 = evt.Skip(visualElement3);
						if (!flag5)
						{
							evt.currentTarget = visualElement3;
							evt.currentTarget.HandleEvent(evt);
						}
					}
				}
			}
			evt.dispatch = false;
			evt.propagationPhase = PropagationPhase.None;
			evt.currentTarget = null;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003C5D8 File Offset: 0x0003A7D8
		internal static void PropagateToIMGUIContainer(VisualElement root, EventBase evt)
		{
			bool flag = evt.imguiEvent == null || root.elementPanel.contextType == ContextType.Player;
			if (!flag)
			{
				bool isIMGUIContainer = root.isIMGUIContainer;
				if (isIMGUIContainer)
				{
					IMGUIContainer imguicontainer = root as IMGUIContainer;
					bool flag2 = evt.Skip(imguicontainer);
					if (flag2)
					{
						return;
					}
					Focusable focusable = evt.target as Focusable;
					bool flag3 = focusable != null && focusable.focusable;
					bool flag4 = imguicontainer.SendEventToIMGUI(evt, !flag3, true);
					if (flag4)
					{
						evt.StopPropagation();
						evt.PreventDefault();
					}
					bool flag5 = evt.imguiEvent.rawType == EventType.Used;
					if (flag5)
					{
						Debug.Assert(evt.isPropagationStopped);
					}
				}
				bool flag6 = root.imguiContainerDescendantCount > 0;
				if (flag6)
				{
					int childCount = root.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						EventDispatchUtilities.PropagateToIMGUIContainer(root.hierarchy[i], evt);
						bool isPropagationStopped = evt.isPropagationStopped;
						if (isPropagationStopped)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003C6EC File Offset: 0x0003A8EC
		public static void ExecuteDefaultAction(EventBase evt, IPanel panel)
		{
			bool flag = evt.target == null && panel != null;
			if (flag)
			{
				evt.target = panel.visualTree;
			}
			bool flag2 = evt.target != null;
			if (flag2)
			{
				evt.dispatch = true;
				evt.currentTarget = evt.target;
				evt.propagationPhase = PropagationPhase.DefaultAction;
				evt.currentTarget.HandleEvent(evt);
				evt.propagationPhase = PropagationPhase.None;
				evt.currentTarget = null;
				evt.dispatch = false;
			}
		}
	}
}
