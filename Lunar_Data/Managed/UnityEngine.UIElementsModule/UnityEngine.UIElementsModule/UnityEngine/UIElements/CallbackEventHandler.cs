using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DA RID: 474
	public abstract class CallbackEventHandler : IEventHandler
	{
		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003BE34 File Offset: 0x0003A034
		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown, InvokePolicy.Default);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003BE6C File Offset: 0x0003A06C
		public void RegisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TUserArgsType userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType, TUserArgsType>(callback, userArgs, useTrickleDown, InvokePolicy.Default);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003BEA4 File Offset: 0x0003A0A4
		internal void RegisterCallback<TEventType>(EventCallback<TEventType> callback, InvokePolicy invokePolicy, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown, invokePolicy);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003BEDC File Offset: 0x0003A0DC
		public void UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry != null;
			if (flag)
			{
				this.m_CallbackRegistry.UnregisterCallback<TEventType>(callback, useTrickleDown);
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003BF08 File Offset: 0x0003A108
		public void UnregisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry != null;
			if (flag)
			{
				this.m_CallbackRegistry.UnregisterCallback<TEventType, TUserArgsType>(callback, useTrickleDown);
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003BF34 File Offset: 0x0003A134
		internal bool TryGetUserArgs<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown, out TCallbackArgs userData) where TEventType : EventBase<TEventType>, new()
		{
			userData = default(TCallbackArgs);
			bool flag = this.m_CallbackRegistry != null;
			return flag && this.m_CallbackRegistry.TryGetUserArgs<TEventType, TCallbackArgs>(callback, useTrickleDown, out userData);
		}

		// Token: 0x06000EDA RID: 3802
		public abstract void SendEvent(EventBase e);

		// Token: 0x06000EDB RID: 3803
		internal abstract void SendEvent(EventBase e, DispatchMode dispatchMode);

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003BF6D File Offset: 0x0003A16D
		internal void HandleEventAtTargetPhase(EventBase evt)
		{
			evt.currentTarget = evt.target;
			evt.propagationPhase = PropagationPhase.AtTarget;
			this.HandleEvent(evt);
			evt.propagationPhase = PropagationPhase.DefaultActionAtTarget;
			this.HandleEvent(evt);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003BFA0 File Offset: 0x0003A1A0
		public virtual void HandleEvent(EventBase evt)
		{
			bool flag = evt == null;
			if (!flag)
			{
				switch (evt.propagationPhase)
				{
				case PropagationPhase.TrickleDown:
				case PropagationPhase.BubbleUp:
				{
					bool flag2 = !evt.isPropagationStopped;
					if (flag2)
					{
						EventCallbackRegistry callbackRegistry = this.m_CallbackRegistry;
						if (callbackRegistry != null)
						{
							callbackRegistry.InvokeCallbacks(evt, evt.propagationPhase);
						}
					}
					break;
				}
				case PropagationPhase.AtTarget:
				{
					bool flag3 = !evt.isPropagationStopped;
					if (flag3)
					{
						EventCallbackRegistry callbackRegistry2 = this.m_CallbackRegistry;
						if (callbackRegistry2 != null)
						{
							callbackRegistry2.InvokeCallbacks(evt, PropagationPhase.TrickleDown);
						}
					}
					bool flag4 = !evt.isPropagationStopped;
					if (flag4)
					{
						EventCallbackRegistry callbackRegistry3 = this.m_CallbackRegistry;
						if (callbackRegistry3 != null)
						{
							callbackRegistry3.InvokeCallbacks(evt, PropagationPhase.BubbleUp);
						}
					}
					break;
				}
				case PropagationPhase.DefaultAction:
				{
					bool flag5 = !evt.isDefaultPrevented;
					if (flag5)
					{
						using (new EventDebuggerLogExecuteDefaultAction(evt))
						{
							bool flag6;
							if (evt.skipDisabledElements)
							{
								VisualElement visualElement = this as VisualElement;
								if (visualElement != null)
								{
									flag6 = !visualElement.enabledInHierarchy;
									goto IL_015A;
								}
							}
							flag6 = false;
							IL_015A:
							bool flag7 = flag6;
							if (flag7)
							{
								this.ExecuteDefaultActionDisabled(evt);
							}
							else
							{
								this.ExecuteDefaultAction(evt);
							}
						}
					}
					break;
				}
				case PropagationPhase.DefaultActionAtTarget:
				{
					bool flag8 = !evt.isDefaultPrevented;
					if (flag8)
					{
						using (new EventDebuggerLogExecuteDefaultAction(evt))
						{
							bool flag9;
							if (evt.skipDisabledElements)
							{
								VisualElement visualElement2 = this as VisualElement;
								if (visualElement2 != null)
								{
									flag9 = !visualElement2.enabledInHierarchy;
									goto IL_00F2;
								}
							}
							flag9 = false;
							IL_00F2:
							bool flag10 = flag9;
							if (flag10)
							{
								this.ExecuteDefaultActionDisabledAtTarget(evt);
							}
							else
							{
								this.ExecuteDefaultActionAtTarget(evt);
							}
						}
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003C150 File Offset: 0x0003A350
		public bool HasTrickleDownHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasTrickleDownHandlers();
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003C178 File Offset: 0x0003A378
		public bool HasBubbleUpHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasBubbleHandlers();
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000020E6 File Offset: 0x000002E6
		protected virtual void ExecuteDefaultActionAtTarget(EventBase evt)
		{
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000020E6 File Offset: 0x000002E6
		protected virtual void ExecuteDefaultAction(EventBase evt)
		{
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000020E6 File Offset: 0x000002E6
		internal virtual void ExecuteDefaultActionDisabledAtTarget(EventBase evt)
		{
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000020E6 File Offset: 0x000002E6
		internal virtual void ExecuteDefaultActionDisabled(EventBase evt)
		{
		}

		// Token: 0x040006C3 RID: 1731
		private EventCallbackRegistry m_CallbackRegistry;
	}
}
