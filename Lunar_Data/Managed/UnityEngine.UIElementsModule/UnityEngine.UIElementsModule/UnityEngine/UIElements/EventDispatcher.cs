using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000029 RID: 41
	public sealed class EventDispatcher
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000054F5 File Offset: 0x000036F5
		internal PointerDispatchState pointerState { get; } = new PointerDispatchState();

		// Token: 0x060000F9 RID: 249 RVA: 0x00005500 File Offset: 0x00003700
		internal static EventDispatcher CreateDefault()
		{
			return new EventDispatcher(EventDispatcher.s_EditorStrategies);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000551C File Offset: 0x0000371C
		internal static EventDispatcher CreateForRuntime(IList<IEventDispatchingStrategy> strategies)
		{
			return new EventDispatcher(strategies);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005534 File Offset: 0x00003734
		[Obsolete("Please use EventDispatcher.CreateDefault().")]
		internal EventDispatcher()
			: this(EventDispatcher.s_EditorStrategies)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005544 File Offset: 0x00003744
		private EventDispatcher(IList<IEventDispatchingStrategy> strategies)
		{
			this.m_DispatchingStrategies = new List<IEventDispatchingStrategy>();
			this.m_DispatchingStrategies.AddRange(strategies);
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000055AC File Offset: 0x000037AC
		private bool dispatchImmediately
		{
			get
			{
				return this.m_Immediate || this.m_GateCount == 0U;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000055D2 File Offset: 0x000037D2
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000055DA File Offset: 0x000037DA
		internal bool processingEvents { get; private set; }

		// Token: 0x06000100 RID: 256 RVA: 0x000055E4 File Offset: 0x000037E4
		internal void Dispatch(EventBase evt, IPanel panel, DispatchMode dispatchMode)
		{
			evt.MarkReceivedByDispatcher();
			bool flag = evt.eventTypeId == EventBase<IMGUIEvent>.TypeId();
			if (flag)
			{
				Event imguiEvent = evt.imguiEvent;
				bool flag2 = imguiEvent.rawType == EventType.Repaint;
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.dispatchImmediately || dispatchMode == DispatchMode.Immediate;
			if (flag3)
			{
				this.ProcessEvent(evt, panel);
			}
			else
			{
				evt.Acquire();
				this.m_Queue.Enqueue(new EventDispatcher.EventRecord
				{
					m_Event = evt,
					m_Panel = panel
				});
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005674 File Offset: 0x00003874
		internal void PushDispatcherContext()
		{
			this.ProcessEventQueue();
			this.m_DispatchContexts.Push(new EventDispatcher.DispatchContext
			{
				m_GateCount = this.m_GateCount,
				m_Queue = this.m_Queue
			});
			this.m_GateCount = 0U;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000056D0 File Offset: 0x000038D0
		internal void PopDispatcherContext()
		{
			Debug.Assert(this.m_GateCount == 0U, "All gates should have been opened before popping dispatch context.");
			Debug.Assert(this.m_Queue.Count == 0, "Queue should be empty when popping dispatch context.");
			EventDispatcher.k_EventQueuePool.Release(this.m_Queue);
			this.m_GateCount = this.m_DispatchContexts.Peek().m_GateCount;
			this.m_Queue = this.m_DispatchContexts.Peek().m_Queue;
			this.m_DispatchContexts.Pop();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005754 File Offset: 0x00003954
		internal void CloseGate()
		{
			this.m_GateCount += 1U;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005768 File Offset: 0x00003968
		internal void OpenGate()
		{
			Debug.Assert(this.m_GateCount > 0U);
			bool flag = this.m_GateCount > 0U;
			if (flag)
			{
				this.m_GateCount -= 1U;
			}
			bool flag2 = this.m_GateCount == 0U;
			if (flag2)
			{
				this.ProcessEventQueue();
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000057B8 File Offset: 0x000039B8
		private void ProcessEventQueue()
		{
			Queue<EventDispatcher.EventRecord> queue = this.m_Queue;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
			ExitGUIException ex = null;
			try
			{
				this.processingEvents = true;
				while (queue.Count > 0)
				{
					EventDispatcher.EventRecord eventRecord = queue.Dequeue();
					EventBase @event = eventRecord.m_Event;
					IPanel panel = eventRecord.m_Panel;
					try
					{
						this.ProcessEvent(@event, panel);
					}
					catch (ExitGUIException ex2)
					{
						Debug.Assert(ex == null);
						ex = ex2;
					}
					finally
					{
						@event.Dispose();
					}
				}
			}
			finally
			{
				this.processingEvents = false;
				EventDispatcher.k_EventQueuePool.Release(queue);
			}
			bool flag = ex != null;
			if (flag)
			{
				throw ex;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005890 File Offset: 0x00003A90
		private void ProcessEvent(EventBase evt, IPanel panel)
		{
			Event imguiEvent = evt.imguiEvent;
			bool flag = imguiEvent != null && imguiEvent.rawType == EventType.Used;
			using (new EventDispatcherGate(this))
			{
				evt.PreDispatch(panel);
				bool flag2 = !evt.stopDispatch && !evt.isPropagationStopped;
				if (flag2)
				{
					this.ApplyDispatchingStrategies(evt, panel, flag);
				}
				bool flag3 = evt.path != null;
				if (flag3)
				{
					foreach (VisualElement visualElement in evt.path.targetElements)
					{
						evt.target = visualElement;
						EventDispatchUtilities.ExecuteDefaultAction(evt, panel);
					}
					evt.target = evt.leafTarget;
				}
				else
				{
					EventDispatchUtilities.ExecuteDefaultAction(evt, panel);
				}
				evt.PostDispatch(panel);
				this.m_ClickDetector.ProcessEvent(evt);
				Debug.Assert(flag || evt.isPropagationStopped || imguiEvent == null || imguiEvent.rawType != EventType.Used, "Event is used but not stopped.");
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000059CC File Offset: 0x00003BCC
		private void ApplyDispatchingStrategies(EventBase evt, IPanel panel, bool imguiEventIsInitiallyUsed)
		{
			foreach (IEventDispatchingStrategy eventDispatchingStrategy in this.m_DispatchingStrategies)
			{
				bool flag = eventDispatchingStrategy.CanDispatchEvent(evt);
				if (flag)
				{
					eventDispatchingStrategy.DispatchEvent(evt, panel);
					Debug.Assert(imguiEventIsInitiallyUsed || evt.isPropagationStopped || evt.imguiEvent == null || evt.imguiEvent.rawType != EventType.Used, "Unexpected condition: !evt.isPropagationStopped && evt.imguiEvent.rawType == EventType.Used.");
					bool flag2 = evt.stopDispatch || evt.isPropagationStopped;
					if (flag2)
					{
						break;
					}
				}
			}
		}

		// Token: 0x0400006E RID: 110
		internal ClickDetector m_ClickDetector = new ClickDetector();

		// Token: 0x0400006F RID: 111
		private List<IEventDispatchingStrategy> m_DispatchingStrategies;

		// Token: 0x04000070 RID: 112
		private static readonly ObjectPool<Queue<EventDispatcher.EventRecord>> k_EventQueuePool = new ObjectPool<Queue<EventDispatcher.EventRecord>>(100);

		// Token: 0x04000071 RID: 113
		private Queue<EventDispatcher.EventRecord> m_Queue;

		// Token: 0x04000073 RID: 115
		private uint m_GateCount;

		// Token: 0x04000074 RID: 116
		private Stack<EventDispatcher.DispatchContext> m_DispatchContexts = new Stack<EventDispatcher.DispatchContext>();

		// Token: 0x04000075 RID: 117
		private static readonly IEventDispatchingStrategy[] s_EditorStrategies = new IEventDispatchingStrategy[]
		{
			new PointerCaptureDispatchingStrategy(),
			new MouseCaptureDispatchingStrategy(),
			new KeyboardEventDispatchingStrategy(),
			new PointerEventDispatchingStrategy(),
			new MouseEventDispatchingStrategy(),
			new CommandEventDispatchingStrategy(),
			new IMGUIEventDispatchingStrategy(),
			new DefaultDispatchingStrategy()
		};

		// Token: 0x04000076 RID: 118
		private bool m_Immediate = false;

		// Token: 0x0200002A RID: 42
		private struct EventRecord
		{
			// Token: 0x04000078 RID: 120
			public EventBase m_Event;

			// Token: 0x04000079 RID: 121
			public IPanel m_Panel;
		}

		// Token: 0x0200002B RID: 43
		private struct DispatchContext
		{
			// Token: 0x0400007A RID: 122
			public uint m_GateCount;

			// Token: 0x0400007B RID: 123
			public Queue<EventDispatcher.EventRecord> m_Queue;
		}
	}
}
