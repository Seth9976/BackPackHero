using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D8 RID: 472
	internal class EventCallbackRegistry
	{
		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003B9AC File Offset: 0x00039BAC
		private static EventCallbackList GetCallbackList(EventCallbackList initializer = null)
		{
			return EventCallbackRegistry.s_ListPool.Get(initializer);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003B9C9 File Offset: 0x00039BC9
		private static void ReleaseCallbackList(EventCallbackList toRelease)
		{
			EventCallbackRegistry.s_ListPool.Release(toRelease);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003B9D8 File Offset: 0x00039BD8
		public EventCallbackRegistry()
		{
			this.m_IsInvoking = 0;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003B9EC File Offset: 0x00039BEC
		private EventCallbackList GetCallbackListForWriting()
		{
			bool flag = this.m_IsInvoking > 0;
			EventCallbackList eventCallbackList;
			if (flag)
			{
				bool flag2 = this.m_TemporaryCallbacks == null;
				if (flag2)
				{
					bool flag3 = this.m_Callbacks != null;
					if (flag3)
					{
						this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(this.m_Callbacks);
					}
					else
					{
						this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(null);
					}
				}
				eventCallbackList = this.m_TemporaryCallbacks;
			}
			else
			{
				bool flag4 = this.m_Callbacks == null;
				if (flag4)
				{
					this.m_Callbacks = EventCallbackRegistry.GetCallbackList(null);
				}
				eventCallbackList = this.m_Callbacks;
			}
			return eventCallbackList;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003BA78 File Offset: 0x00039C78
		private EventCallbackList GetCallbackListForReading()
		{
			bool flag = this.m_TemporaryCallbacks != null;
			EventCallbackList eventCallbackList;
			if (flag)
			{
				eventCallbackList = this.m_TemporaryCallbacks;
			}
			else
			{
				eventCallbackList = this.m_Callbacks;
			}
			return eventCallbackList;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003BAA8 File Offset: 0x00039CA8
		private bool ShouldRegisterCallback(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			bool flag = callback == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventCallbackList callbackListForReading = this.GetCallbackListForReading();
				bool flag3 = callbackListForReading != null;
				flag2 = !flag3 || !callbackListForReading.Contains(eventTypeId, callback, phase);
			}
			return flag2;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003BAE8 File Offset: 0x00039CE8
		private bool UnregisterCallback(long eventTypeId, Delegate callback, TrickleDown useTrickleDown)
		{
			bool flag = callback == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventCallbackList callbackListForWriting = this.GetCallbackListForWriting();
				CallbackPhase callbackPhase = ((useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp);
				flag2 = callbackListForWriting.Remove(eventTypeId, callback, callbackPhase);
			}
			return flag2;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003BB20 File Offset: 0x00039D20
		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown, InvokePolicy invokePolicy = InvokePolicy.Default) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			long num = EventBase<TEventType>.TypeId();
			CallbackPhase callbackPhase = ((useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp);
			EventCallbackList eventCallbackList = this.GetCallbackListForReading();
			bool flag2 = eventCallbackList == null || !eventCallbackList.Contains(num, callback, callbackPhase);
			if (flag2)
			{
				eventCallbackList = this.GetCallbackListForWriting();
				eventCallbackList.Add(new EventCallbackFunctor<TEventType>(callback, callbackPhase, invokePolicy));
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003BB88 File Offset: 0x00039D88
		public void RegisterCallback<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown, InvokePolicy invokePolicy = InvokePolicy.Default) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			long num = EventBase<TEventType>.TypeId();
			CallbackPhase callbackPhase = ((useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp);
			EventCallbackList eventCallbackList = this.GetCallbackListForReading();
			bool flag2 = eventCallbackList != null;
			if (flag2)
			{
				EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = eventCallbackList.Find(num, callback, callbackPhase) as EventCallbackFunctor<TEventType, TCallbackArgs>;
				bool flag3 = eventCallbackFunctor != null;
				if (flag3)
				{
					eventCallbackFunctor.userArgs = userArgs;
					return;
				}
			}
			eventCallbackList = this.GetCallbackListForWriting();
			eventCallbackList.Add(new EventCallbackFunctor<TEventType, TCallbackArgs>(callback, userArgs, callbackPhase, invokePolicy));
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003BC0C File Offset: 0x00039E0C
		public bool UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			long num = EventBase<TEventType>.TypeId();
			return this.UnregisterCallback(num, callback, useTrickleDown);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0003BC30 File Offset: 0x00039E30
		public bool UnregisterCallback<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			long num = EventBase<TEventType>.TypeId();
			return this.UnregisterCallback(num, callback, useTrickleDown);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0003BC54 File Offset: 0x00039E54
		internal bool TryGetUserArgs<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown, out TCallbackArgs userArgs) where TEventType : EventBase<TEventType>, new()
		{
			userArgs = default(TCallbackArgs);
			bool flag = callback == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventCallbackList callbackListForReading = this.GetCallbackListForReading();
				long num = EventBase<TEventType>.TypeId();
				CallbackPhase callbackPhase = ((useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp);
				EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = callbackListForReading.Find(num, callback, callbackPhase) as EventCallbackFunctor<TEventType, TCallbackArgs>;
				bool flag3 = eventCallbackFunctor == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					userArgs = eventCallbackFunctor.userArgs;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003BCC0 File Offset: 0x00039EC0
		public void InvokeCallbacks(EventBase evt, PropagationPhase propagationPhase)
		{
			bool flag = this.m_Callbacks == null;
			if (!flag)
			{
				this.m_IsInvoking++;
				int i = 0;
				while (i < this.m_Callbacks.Count)
				{
					bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
					if (isImmediatePropagationStopped)
					{
						break;
					}
					if (!evt.skipDisabledElements)
					{
						goto IL_006B;
					}
					VisualElement visualElement = evt.currentTarget as VisualElement;
					if (visualElement == null || visualElement.enabledInHierarchy)
					{
						goto IL_006B;
					}
					bool flag2 = this.m_Callbacks[i].invokePolicy != InvokePolicy.IncludeDisabled;
					IL_006C:
					bool flag3 = flag2;
					if (!flag3)
					{
						this.m_Callbacks[i].Invoke(evt, propagationPhase);
					}
					i++;
					continue;
					IL_006B:
					flag2 = false;
					goto IL_006C;
				}
				this.m_IsInvoking--;
				bool flag4 = this.m_IsInvoking == 0;
				if (flag4)
				{
					bool flag5 = this.m_TemporaryCallbacks != null;
					if (flag5)
					{
						EventCallbackRegistry.ReleaseCallbackList(this.m_Callbacks);
						this.m_Callbacks = EventCallbackRegistry.GetCallbackList(this.m_TemporaryCallbacks);
						EventCallbackRegistry.ReleaseCallbackList(this.m_TemporaryCallbacks);
						this.m_TemporaryCallbacks = null;
					}
				}
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003BDD0 File Offset: 0x00039FD0
		public bool HasTrickleDownHandlers()
		{
			return this.m_Callbacks != null && this.m_Callbacks.trickleDownCallbackCount > 0;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003BDFC File Offset: 0x00039FFC
		public bool HasBubbleHandlers()
		{
			return this.m_Callbacks != null && this.m_Callbacks.bubbleUpCallbackCount > 0;
		}

		// Token: 0x040006BF RID: 1727
		private static readonly EventCallbackListPool s_ListPool = new EventCallbackListPool();

		// Token: 0x040006C0 RID: 1728
		private EventCallbackList m_Callbacks;

		// Token: 0x040006C1 RID: 1729
		private EventCallbackList m_TemporaryCallbacks;

		// Token: 0x040006C2 RID: 1730
		private int m_IsInvoking;
	}
}
