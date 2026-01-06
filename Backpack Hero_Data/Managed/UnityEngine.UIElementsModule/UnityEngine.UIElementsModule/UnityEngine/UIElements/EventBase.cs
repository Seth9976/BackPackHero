using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CA RID: 458
	public abstract class EventBase : IDisposable
	{
		// Token: 0x06000E46 RID: 3654 RVA: 0x0003A874 File Offset: 0x00038A74
		protected static long RegisterEventType()
		{
			return EventBase.s_LastTypeId += 1L;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0003A894 File Offset: 0x00038A94
		public virtual long eventTypeId
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0003A898 File Offset: 0x00038A98
		// (set) Token: 0x06000E49 RID: 3657 RVA: 0x0003A8A0 File Offset: 0x00038AA0
		public long timestamp { get; private set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0003A8A9 File Offset: 0x00038AA9
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x0003A8B1 File Offset: 0x00038AB1
		internal ulong eventId { get; private set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0003A8BA File Offset: 0x00038ABA
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x0003A8C2 File Offset: 0x00038AC2
		internal ulong triggerEventId { get; private set; }

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003A8CB File Offset: 0x00038ACB
		internal void SetTriggerEventId(ulong id)
		{
			this.triggerEventId = id;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0003A8D6 File Offset: 0x00038AD6
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x0003A8DE File Offset: 0x00038ADE
		internal EventBase.EventPropagation propagation { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0003A8E8 File Offset: 0x00038AE8
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x0003A950 File Offset: 0x00038B50
		internal PropagationPaths path
		{
			get
			{
				bool flag = this.m_Path == null;
				if (flag)
				{
					PropagationPaths.Type type = (this.tricklesDown ? PropagationPaths.Type.TrickleDown : PropagationPaths.Type.None);
					type |= (this.bubbles ? PropagationPaths.Type.BubbleUp : PropagationPaths.Type.None);
					this.m_Path = PropagationPaths.Build(this.leafTarget as VisualElement, this, type);
					EventDebugger.LogPropagationPaths(this, this.m_Path);
				}
				return this.m_Path;
			}
			set
			{
				bool flag = value != null;
				if (flag)
				{
					this.m_Path = PropagationPaths.Copy(value);
				}
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0003A972 File Offset: 0x00038B72
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x0003A97A File Offset: 0x00038B7A
		private EventBase.LifeCycleStatus lifeCycleStatus { get; set; }

		// Token: 0x06000E55 RID: 3669 RVA: 0x000020E6 File Offset: 0x000002E6
		[Obsolete("Override PreDispatch(IPanel panel) instead.")]
		protected virtual void PreDispatch()
		{
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003A983 File Offset: 0x00038B83
		protected internal virtual void PreDispatch(IPanel panel)
		{
			this.PreDispatch();
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000020E6 File Offset: 0x000002E6
		[Obsolete("Override PostDispatch(IPanel panel) instead.")]
		protected virtual void PostDispatch()
		{
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003A98D File Offset: 0x00038B8D
		protected internal virtual void PostDispatch(IPanel panel)
		{
			this.PostDispatch();
			this.processed = true;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x0003A9C0 File Offset: 0x00038BC0
		public bool bubbles
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.Bubbles) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.Bubbles;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.Bubbles;
				}
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0003A9F8 File Offset: 0x00038BF8
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x0003AA18 File Offset: 0x00038C18
		public bool tricklesDown
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.TricklesDown) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.TricklesDown;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.TricklesDown;
				}
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0003AA50 File Offset: 0x00038C50
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x0003AA70 File Offset: 0x00038C70
		internal bool skipDisabledElements
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.SkipDisabledElements) > EventBase.EventPropagation.None;
			}
			set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.SkipDisabledElements;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.SkipDisabledElements;
				}
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0003AAA8 File Offset: 0x00038CA8
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0003AAC8 File Offset: 0x00038CC8
		internal bool ignoreCompositeRoots
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.IgnoreCompositeRoots) > EventBase.EventPropagation.None;
			}
			set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.IgnoreCompositeRoots;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.IgnoreCompositeRoots;
				}
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0003AB01 File Offset: 0x00038D01
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0003AB09 File Offset: 0x00038D09
		internal IEventHandler leafTarget { get; private set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0003AB14 File Offset: 0x00038D14
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x0003AB2C File Offset: 0x00038D2C
		public IEventHandler target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				this.m_Target = value;
				bool flag = this.leafTarget == null;
				if (flag)
				{
					this.leafTarget = value;
				}
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0003AB58 File Offset: 0x00038D58
		internal List<IEventHandler> skipElements { get; } = new List<IEventHandler>();

		// Token: 0x06000E66 RID: 3686 RVA: 0x0003AB60 File Offset: 0x00038D60
		internal bool Skip(IEventHandler h)
		{
			return this.skipElements.Contains(h);
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0003AB80 File Offset: 0x00038D80
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0003ABA0 File Offset: 0x00038DA0
		public bool isPropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagationStopped;
				}
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003ABD8 File Offset: 0x00038DD8
		public void StopPropagation()
		{
			this.isPropagationStopped = true;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0003ABE4 File Offset: 0x00038DE4
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0003AC04 File Offset: 0x00038E04
		public bool isImmediatePropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ImmediatePropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0003AC3C File Offset: 0x00038E3C
		public void StopImmediatePropagation()
		{
			this.isPropagationStopped = true;
			this.isImmediatePropagationStopped = true;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0003AC50 File Offset: 0x00038E50
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x0003AC70 File Offset: 0x00038E70
		public bool isDefaultPrevented
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.DefaultPrevented) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.DefaultPrevented;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.DefaultPrevented;
				}
			}
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003ACA8 File Offset: 0x00038EA8
		public void PreventDefault()
		{
			bool flag = (this.propagation & EventBase.EventPropagation.Cancellable) == EventBase.EventPropagation.Cancellable;
			if (flag)
			{
				this.isDefaultPrevented = true;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0003ACCF File Offset: 0x00038ECF
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0003ACD7 File Offset: 0x00038ED7
		public PropagationPhase propagationPhase { get; internal set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0003ACE0 File Offset: 0x00038EE0
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x0003ACF8 File Offset: 0x00038EF8
		public virtual IEventHandler currentTarget
		{
			get
			{
				return this.m_CurrentTarget;
			}
			internal set
			{
				this.m_CurrentTarget = value;
				bool flag = this.imguiEvent != null;
				if (flag)
				{
					VisualElement visualElement = this.currentTarget as VisualElement;
					bool flag2 = visualElement != null;
					if (flag2)
					{
						this.imguiEvent.mousePosition = visualElement.WorldToLocal(this.originalMousePosition);
					}
					else
					{
						this.imguiEvent.mousePosition = this.originalMousePosition;
					}
				}
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0003AD60 File Offset: 0x00038F60
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x0003AD80 File Offset: 0x00038F80
		public bool dispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatching) > EventBase.LifeCycleStatus.None;
			}
			internal set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatching;
					this.dispatched = true;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatching;
				}
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003ADC0 File Offset: 0x00038FC0
		internal void MarkReceivedByDispatcher()
		{
			Debug.Assert(!this.dispatched, "Events cannot be dispatched more than once.");
			this.dispatched = true;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0003ADE0 File Offset: 0x00038FE0
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x0003AE04 File Offset: 0x00039004
		private bool dispatched
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatched) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatched;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatched;
				}
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0003AE44 File Offset: 0x00039044
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x0003AE68 File Offset: 0x00039068
		internal bool processed
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Processed) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Processed;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Processed;
				}
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0003AEA8 File Offset: 0x000390A8
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x0003AECC File Offset: 0x000390CC
		internal bool processedByFocusController
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ProcessedByFocusController) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0003AF0C File Offset: 0x0003910C
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x0003AF2C File Offset: 0x0003912C
		internal bool stopDispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.StopDispatch) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.StopDispatch;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.StopDispatch;
				}
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0003AF68 File Offset: 0x00039168
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x0003AF8C File Offset: 0x0003918C
		internal bool propagateToIMGUI
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagateToIMGUI) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0003AFCC File Offset: 0x000391CC
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0003AFEC File Offset: 0x000391EC
		private bool imguiEventIsValid
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.IMGUIEventIsValid) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0003B028 File Offset: 0x00039228
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x0003B04C File Offset: 0x0003924C
		public Event imguiEvent
		{
			get
			{
				return this.imguiEventIsValid ? this.m_ImguiEvent : null;
			}
			protected set
			{
				bool flag = this.m_ImguiEvent == null;
				if (flag)
				{
					this.m_ImguiEvent = new Event();
				}
				bool flag2 = value != null;
				if (flag2)
				{
					this.m_ImguiEvent.CopyFrom(value);
					this.imguiEventIsValid = true;
					this.originalMousePosition = value.mousePosition;
				}
				else
				{
					this.imguiEventIsValid = false;
				}
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0003B0AC File Offset: 0x000392AC
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0003B0B4 File Offset: 0x000392B4
		public Vector2 originalMousePosition { get; private set; }

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003B0BD File Offset: 0x000392BD
		protected virtual void Init()
		{
			this.LocalInit();
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003B0C8 File Offset: 0x000392C8
		private void LocalInit()
		{
			this.timestamp = Panel.TimeSinceStartupMs();
			this.triggerEventId = 0UL;
			ulong num = EventBase.s_NextEventId;
			EventBase.s_NextEventId = num + 1UL;
			this.eventId = num;
			this.propagation = EventBase.EventPropagation.None;
			PropagationPaths path = this.m_Path;
			if (path != null)
			{
				path.Release();
			}
			this.m_Path = null;
			this.leafTarget = null;
			this.target = null;
			this.skipElements.Clear();
			this.isPropagationStopped = false;
			this.isImmediatePropagationStopped = false;
			this.isDefaultPrevented = false;
			this.propagationPhase = PropagationPhase.None;
			this.originalMousePosition = Vector2.zero;
			this.m_CurrentTarget = null;
			this.dispatch = false;
			this.stopDispatch = false;
			this.propagateToIMGUI = true;
			this.dispatched = false;
			this.processed = false;
			this.processedByFocusController = false;
			this.imguiEventIsValid = false;
			this.pooled = false;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003B1B0 File Offset: 0x000393B0
		protected EventBase()
		{
			this.m_ImguiEvent = null;
			this.LocalInit();
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0003B1D4 File Offset: 0x000393D4
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x0003B1F4 File Offset: 0x000393F4
		protected bool pooled
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Pooled) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Pooled;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Pooled;
				}
			}
		}

		// Token: 0x06000E8C RID: 3724
		internal abstract void Acquire();

		// Token: 0x06000E8D RID: 3725
		public abstract void Dispose();

		// Token: 0x04000685 RID: 1669
		private static long s_LastTypeId;

		// Token: 0x04000686 RID: 1670
		private static ulong s_NextEventId;

		// Token: 0x0400068B RID: 1675
		private PropagationPaths m_Path;

		// Token: 0x0400068E RID: 1678
		private IEventHandler m_Target;

		// Token: 0x04000691 RID: 1681
		private IEventHandler m_CurrentTarget;

		// Token: 0x04000692 RID: 1682
		private Event m_ImguiEvent;

		// Token: 0x020001CB RID: 459
		[Flags]
		internal enum EventPropagation
		{
			// Token: 0x04000695 RID: 1685
			None = 0,
			// Token: 0x04000696 RID: 1686
			Bubbles = 1,
			// Token: 0x04000697 RID: 1687
			TricklesDown = 2,
			// Token: 0x04000698 RID: 1688
			Cancellable = 4,
			// Token: 0x04000699 RID: 1689
			SkipDisabledElements = 8,
			// Token: 0x0400069A RID: 1690
			IgnoreCompositeRoots = 16
		}

		// Token: 0x020001CC RID: 460
		[Flags]
		private enum LifeCycleStatus
		{
			// Token: 0x0400069C RID: 1692
			None = 0,
			// Token: 0x0400069D RID: 1693
			PropagationStopped = 1,
			// Token: 0x0400069E RID: 1694
			ImmediatePropagationStopped = 2,
			// Token: 0x0400069F RID: 1695
			DefaultPrevented = 4,
			// Token: 0x040006A0 RID: 1696
			Dispatching = 8,
			// Token: 0x040006A1 RID: 1697
			Pooled = 16,
			// Token: 0x040006A2 RID: 1698
			IMGUIEventIsValid = 32,
			// Token: 0x040006A3 RID: 1699
			StopDispatch = 64,
			// Token: 0x040006A4 RID: 1700
			PropagateToIMGUI = 128,
			// Token: 0x040006A5 RID: 1701
			Dispatched = 512,
			// Token: 0x040006A6 RID: 1702
			Processed = 1024,
			// Token: 0x040006A7 RID: 1703
			ProcessedByFocusController = 2048
		}
	}
}
