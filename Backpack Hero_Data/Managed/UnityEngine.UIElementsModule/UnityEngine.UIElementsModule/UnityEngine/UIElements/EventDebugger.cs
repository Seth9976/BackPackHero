using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x02000231 RID: 561
	internal class EventDebugger
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0004047F File Offset: 0x0003E67F
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x00040487 File Offset: 0x0003E687
		public IPanel panel { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x00040490 File Offset: 0x0003E690
		// (set) Token: 0x060010A2 RID: 4258 RVA: 0x00040498 File Offset: 0x0003E698
		public bool isReplaying { get; private set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x000404A1 File Offset: 0x0003E6A1
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x000404A9 File Offset: 0x0003E6A9
		public float playbackSpeed { get; set; } = 1f;

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x000404B2 File Offset: 0x0003E6B2
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x000404BA File Offset: 0x0003E6BA
		public bool isPlaybackPaused { get; set; }

		// Token: 0x060010A7 RID: 4263 RVA: 0x000404C4 File Offset: 0x0003E6C4
		public void UpdateModificationCount()
		{
			bool flag = this.panel == null;
			if (!flag)
			{
				long num;
				bool flag2 = !this.m_ModificationCount.TryGetValue(this.panel, ref num);
				if (flag2)
				{
					num = 0L;
				}
				num += 1L;
				this.m_ModificationCount[this.panel] = num;
			}
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00040518 File Offset: 0x0003E718
		public void BeginProcessEvent(EventBase evt, IEventHandler mouseCapture)
		{
			this.AddBeginProcessEvent(evt, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0004052B File Offset: 0x0003E72B
		public void EndProcessEvent(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.AddEndProcessEvent(evt, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00040540 File Offset: 0x0003E740
		public void LogCall(int cbHashCode, string cbName, EventBase evt, bool propagationHasStopped, bool immediatePropagationHasStopped, bool defaultHasBeenPrevented, long duration, IEventHandler mouseCapture)
		{
			this.AddCallObject(cbHashCode, cbName, evt, propagationHasStopped, immediatePropagationHasStopped, defaultHasBeenPrevented, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00040569 File Offset: 0x0003E769
		public void LogIMGUICall(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.AddIMGUICall(evt, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0004057D File Offset: 0x0003E77D
		public void LogExecuteDefaultAction(EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
		{
			this.AddExecuteDefaultAction(evt, phase, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000020E6 File Offset: 0x000002E6
		public static void LogPropagationPaths(EventBase evt, PropagationPaths paths)
		{
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00040594 File Offset: 0x0003E794
		private void LogPropagationPathsInternal(EventBase evt, PropagationPaths paths)
		{
			PropagationPaths propagationPaths = ((paths == null) ? new PropagationPaths() : new PropagationPaths(paths));
			this.AddPropagationPaths(evt, propagationPaths);
			this.UpdateModificationCount();
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000405C4 File Offset: 0x0003E7C4
		public List<EventDebuggerCallTrace> GetCalls(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerCallTrace> list;
			bool flag = !this.m_EventCalledObjects.TryGetValue(panel, ref list);
			List<EventDebuggerCallTrace> list2;
			if (flag)
			{
				list2 = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerCallTrace> list3 = new List<EventDebuggerCallTrace>();
					foreach (EventDebuggerCallTrace eventDebuggerCallTrace in list)
					{
						bool flag3 = eventDebuggerCallTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list3.Add(eventDebuggerCallTrace);
						}
					}
					list = list3;
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00040674 File Offset: 0x0003E874
		public List<EventDebuggerDefaultActionTrace> GetDefaultActions(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerDefaultActionTrace> list;
			bool flag = !this.m_EventDefaultActionObjects.TryGetValue(panel, ref list);
			List<EventDebuggerDefaultActionTrace> list2;
			if (flag)
			{
				list2 = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerDefaultActionTrace> list3 = new List<EventDebuggerDefaultActionTrace>();
					foreach (EventDebuggerDefaultActionTrace eventDebuggerDefaultActionTrace in list)
					{
						bool flag3 = eventDebuggerDefaultActionTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list3.Add(eventDebuggerDefaultActionTrace);
						}
					}
					list = list3;
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00040724 File Offset: 0x0003E924
		public List<EventDebuggerPathTrace> GetPropagationPaths(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerPathTrace> list;
			bool flag = !this.m_EventPathObjects.TryGetValue(panel, ref list);
			List<EventDebuggerPathTrace> list2;
			if (flag)
			{
				list2 = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerPathTrace> list3 = new List<EventDebuggerPathTrace>();
					foreach (EventDebuggerPathTrace eventDebuggerPathTrace in list)
					{
						bool flag3 = eventDebuggerPathTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list3.Add(eventDebuggerPathTrace);
						}
					}
					list = list3;
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000407D4 File Offset: 0x0003E9D4
		public List<EventDebuggerTrace> GetBeginEndProcessedEvents(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerTrace> list;
			bool flag = !this.m_EventProcessedEvents.TryGetValue(panel, ref list);
			List<EventDebuggerTrace> list2;
			if (flag)
			{
				list2 = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerTrace> list3 = new List<EventDebuggerTrace>();
					foreach (EventDebuggerTrace eventDebuggerTrace in list)
					{
						bool flag3 = eventDebuggerTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list3.Add(eventDebuggerTrace);
						}
					}
					list = list3;
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00040884 File Offset: 0x0003EA84
		public long GetModificationCount(IPanel panel)
		{
			bool flag = panel == null;
			long num;
			if (flag)
			{
				num = -1L;
			}
			else
			{
				long num2;
				bool flag2 = !this.m_ModificationCount.TryGetValue(panel, ref num2);
				if (flag2)
				{
					num2 = -1L;
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000408C0 File Offset: 0x0003EAC0
		public void ClearLogs()
		{
			this.UpdateModificationCount();
			bool flag = this.panel == null;
			if (flag)
			{
				this.m_EventCalledObjects.Clear();
				this.m_EventDefaultActionObjects.Clear();
				this.m_EventPathObjects.Clear();
				this.m_EventProcessedEvents.Clear();
				this.m_StackOfProcessedEvent.Clear();
				this.m_EventTypeProcessedCount.Clear();
			}
			else
			{
				this.m_EventCalledObjects.Remove(this.panel);
				this.m_EventDefaultActionObjects.Remove(this.panel);
				this.m_EventPathObjects.Remove(this.panel);
				this.m_EventProcessedEvents.Remove(this.panel);
				this.m_StackOfProcessedEvent.Remove(this.panel);
				Dictionary<long, int> dictionary;
				bool flag2 = this.m_EventTypeProcessedCount.TryGetValue(this.panel, ref dictionary);
				if (flag2)
				{
					dictionary.Clear();
				}
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000409A8 File Offset: 0x0003EBA8
		public void SaveReplaySessionFromSelection(string path, List<EventDebuggerEventRecord> eventList)
		{
			bool flag = string.IsNullOrEmpty(path);
			if (!flag)
			{
				EventDebuggerRecordList eventDebuggerRecordList = new EventDebuggerRecordList
				{
					eventList = eventList
				};
				string text = JsonUtility.ToJson(eventDebuggerRecordList);
				File.WriteAllText(path, text);
				Debug.Log("Saved under: " + path);
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000409F0 File Offset: 0x0003EBF0
		public EventDebuggerRecordList LoadReplaySession(string path)
		{
			bool flag = string.IsNullOrEmpty(path);
			EventDebuggerRecordList eventDebuggerRecordList;
			if (flag)
			{
				eventDebuggerRecordList = null;
			}
			else
			{
				string text = File.ReadAllText(path);
				eventDebuggerRecordList = JsonUtility.FromJson<EventDebuggerRecordList>(text);
			}
			return eventDebuggerRecordList;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00040A1D File Offset: 0x0003EC1D
		public IEnumerator ReplayEvents(IEnumerable<EventDebuggerEventRecord> eventBases, Action<int, int> refreshList)
		{
			bool flag = eventBases == null;
			if (flag)
			{
				yield break;
			}
			this.isReplaying = true;
			IEnumerator doReplay = this.DoReplayEvents(eventBases, refreshList);
			while (doReplay.MoveNext())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00040A3A File Offset: 0x0003EC3A
		public void StopPlayback()
		{
			this.isReplaying = false;
			this.isPlaybackPaused = false;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00040A4D File Offset: 0x0003EC4D
		private IEnumerator DoReplayEvents(IEnumerable<EventDebuggerEventRecord> eventBases, Action<int, int> refreshList)
		{
			EventDebugger.<>c__DisplayClass34_0 CS$<>8__locals1 = new EventDebugger.<>c__DisplayClass34_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.sortedEvents = Enumerable.ToList<EventDebuggerEventRecord>(Enumerable.OrderBy<EventDebuggerEventRecord, long>(eventBases, (EventDebuggerEventRecord e) => e.timestamp));
			int sortedEventsCount = CS$<>8__locals1.sortedEvents.Count;
			int i = 0;
			while (i < sortedEventsCount)
			{
				bool flag = !this.isReplaying;
				if (flag)
				{
					break;
				}
				EventDebuggerEventRecord eventBase = CS$<>8__locals1.sortedEvents[i];
				Event newEvent = new Event
				{
					button = eventBase.button,
					clickCount = eventBase.clickCount,
					modifiers = eventBase.modifiers,
					mousePosition = eventBase.mousePosition
				};
				bool flag2 = eventBase.eventTypeId == EventBase<MouseMoveEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag2)
				{
					newEvent.type = EventType.MouseMove;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseMove), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag3 = eventBase.eventTypeId == EventBase<MouseDownEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag3)
				{
					newEvent.type = EventType.MouseDown;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseDown), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag4 = eventBase.eventTypeId == EventBase<MouseUpEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag4)
				{
					newEvent.type = EventType.MouseUp;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseUp), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag5 = eventBase.eventTypeId == EventBase<ContextClickEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag5)
				{
					newEvent.type = EventType.ContextClick;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ContextClick), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag6 = eventBase.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag6)
				{
					newEvent.type = EventType.MouseEnterWindow;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseEnterWindow), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag7 = eventBase.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag7)
				{
					newEvent.type = EventType.MouseLeaveWindow;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseLeaveWindow), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag8 = eventBase.eventTypeId == EventBase<PointerMoveEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag8)
				{
					newEvent.type = EventType.MouseMove;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseMove), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag9 = eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag9)
				{
					newEvent.type = EventType.MouseDown;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseDown), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag10 = eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag10)
				{
					newEvent.type = EventType.MouseUp;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseUp), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag11 = eventBase.eventTypeId == EventBase<WheelEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag11)
				{
					newEvent.type = EventType.ScrollWheel;
					newEvent.delta = eventBase.delta;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ScrollWheel), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag12 = eventBase.eventTypeId == EventBase<KeyDownEvent>.TypeId();
				if (flag12)
				{
					newEvent.type = EventType.KeyDown;
					newEvent.character = eventBase.character;
					newEvent.keyCode = eventBase.keyCode;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.KeyDown), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag13 = eventBase.eventTypeId == EventBase<KeyUpEvent>.TypeId();
				if (flag13)
				{
					newEvent.type = EventType.KeyUp;
					newEvent.character = eventBase.character;
					newEvent.keyCode = eventBase.keyCode;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.KeyUp), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag14 = eventBase.eventTypeId == EventBase<ValidateCommandEvent>.TypeId();
				if (flag14)
				{
					newEvent.type = EventType.ValidateCommand;
					newEvent.commandName = eventBase.commandName;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ValidateCommand), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag15 = eventBase.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
				if (flag15)
				{
					newEvent.type = EventType.ExecuteCommand;
					newEvent.commandName = eventBase.commandName;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ExecuteCommand), this.panel, DispatchMode.Default);
					goto IL_0868;
				}
				bool flag16 = eventBase.eventTypeId == EventBase<IMGUIEvent>.TypeId();
				if (flag16)
				{
					string text = "Skipped IMGUI event (";
					string eventBaseName = eventBase.eventBaseName;
					string text2 = "): ";
					EventDebuggerEventRecord eventDebuggerEventRecord = eventBase;
					Debug.Log(text + eventBaseName + text2 + ((eventDebuggerEventRecord != null) ? eventDebuggerEventRecord.ToString() : null));
					IEnumerator awaitSkipped = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
					while (awaitSkipped.MoveNext())
					{
						yield return null;
					}
				}
				else
				{
					string text3 = "Skipped event (";
					string eventBaseName2 = eventBase.eventBaseName;
					string text4 = "): ";
					EventDebuggerEventRecord eventDebuggerEventRecord2 = eventBase;
					Debug.Log(text3 + eventBaseName2 + text4 + ((eventDebuggerEventRecord2 != null) ? eventDebuggerEventRecord2.ToString() : null));
					IEnumerator awaitSkipped2 = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
					while (awaitSkipped2.MoveNext())
					{
						yield return null;
					}
				}
				IL_0912:
				int num = i;
				i = num + 1;
				continue;
				IL_0868:
				if (refreshList != null)
				{
					refreshList.Invoke(i, sortedEventsCount);
				}
				Debug.Log(string.Format("Replayed event {0} ({1}): {2}", eventBase.eventId.ToString(), eventBase.eventBaseName, newEvent));
				IEnumerator await = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
				while (await.MoveNext())
				{
					yield return null;
				}
				eventBase = null;
				newEvent = null;
				await = null;
				goto IL_0912;
			}
			this.isReplaying = false;
			yield break;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00040A6C File Offset: 0x0003EC6C
		public Dictionary<string, EventDebugger.HistogramRecord> ComputeHistogram(List<EventDebuggerEventRecord> eventBases)
		{
			List<EventDebuggerTrace> list;
			bool flag = this.panel == null || !this.m_EventProcessedEvents.TryGetValue(this.panel, ref list);
			Dictionary<string, EventDebugger.HistogramRecord> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				bool flag2 = list == null;
				if (flag2)
				{
					dictionary = null;
				}
				else
				{
					Dictionary<string, EventDebugger.HistogramRecord> dictionary2 = new Dictionary<string, EventDebugger.HistogramRecord>();
					foreach (EventDebuggerTrace eventDebuggerTrace in list)
					{
						bool flag3 = eventBases == null || eventBases.Count == 0 || eventBases.Contains(eventDebuggerTrace.eventBase);
						if (flag3)
						{
							string eventBaseName = eventDebuggerTrace.eventBase.eventBaseName;
							long num = eventDebuggerTrace.duration;
							long num2 = 1L;
							EventDebugger.HistogramRecord histogramRecord;
							bool flag4 = dictionary2.TryGetValue(eventBaseName, ref histogramRecord);
							if (flag4)
							{
								num += histogramRecord.duration;
								num2 += histogramRecord.count;
							}
							dictionary2[eventBaseName] = new EventDebugger.HistogramRecord
							{
								count = num2,
								duration = num
							};
						}
					}
					dictionary = dictionary2;
				}
			}
			return dictionary;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00040B98 File Offset: 0x0003ED98
		public Dictionary<long, int> eventTypeProcessedCount
		{
			get
			{
				Dictionary<long, int> dictionary;
				return this.m_EventTypeProcessedCount.TryGetValue(this.panel, ref dictionary) ? dictionary : null;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x00040BBE File Offset: 0x0003EDBE
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x00040BC6 File Offset: 0x0003EDC6
		public bool suspended { get; set; }

		// Token: 0x060010BE RID: 4286 RVA: 0x00040BD0 File Offset: 0x0003EDD0
		public EventDebugger()
		{
			this.m_EventCalledObjects = new Dictionary<IPanel, List<EventDebuggerCallTrace>>();
			this.m_EventDefaultActionObjects = new Dictionary<IPanel, List<EventDebuggerDefaultActionTrace>>();
			this.m_EventPathObjects = new Dictionary<IPanel, List<EventDebuggerPathTrace>>();
			this.m_StackOfProcessedEvent = new Dictionary<IPanel, Stack<EventDebuggerTrace>>();
			this.m_EventProcessedEvents = new Dictionary<IPanel, List<EventDebuggerTrace>>();
			this.m_EventTypeProcessedCount = new Dictionary<IPanel, Dictionary<long, int>>();
			this.m_ModificationCount = new Dictionary<IPanel, long>();
			this.m_Log = true;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00040C44 File Offset: 0x0003EE44
		private void AddCallObject(int cbHashCode, string cbName, EventBase evt, bool propagationHasStopped, bool immediatePropagationHasStopped, bool defaultHasBeenPrevented, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerCallTrace eventDebuggerCallTrace = new EventDebuggerCallTrace(this.panel, evt, cbHashCode, cbName, propagationHasStopped, immediatePropagationHasStopped, defaultHasBeenPrevented, duration, mouseCapture);
					List<EventDebuggerCallTrace> list;
					bool flag = !this.m_EventCalledObjects.TryGetValue(this.panel, ref list);
					if (flag)
					{
						list = new List<EventDebuggerCallTrace>();
						this.m_EventCalledObjects.Add(this.panel, list);
					}
					list.Add(eventDebuggerCallTrace);
				}
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00040CC4 File Offset: 0x0003EEC4
		private void AddExecuteDefaultAction(EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerDefaultActionTrace eventDebuggerDefaultActionTrace = new EventDebuggerDefaultActionTrace(this.panel, evt, phase, duration, mouseCapture);
					List<EventDebuggerDefaultActionTrace> list;
					bool flag = !this.m_EventDefaultActionObjects.TryGetValue(this.panel, ref list);
					if (flag)
					{
						list = new List<EventDebuggerDefaultActionTrace>();
						this.m_EventDefaultActionObjects.Add(this.panel, list);
					}
					list.Add(eventDebuggerDefaultActionTrace);
				}
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00040D3C File Offset: 0x0003EF3C
		private void AddPropagationPaths(EventBase evt, PropagationPaths paths)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerPathTrace eventDebuggerPathTrace = new EventDebuggerPathTrace(this.panel, evt, paths);
					List<EventDebuggerPathTrace> list;
					bool flag = !this.m_EventPathObjects.TryGetValue(this.panel, ref list);
					if (flag)
					{
						list = new List<EventDebuggerPathTrace>();
						this.m_EventPathObjects.Add(this.panel, list);
					}
					list.Add(eventDebuggerPathTrace);
				}
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00040DB0 File Offset: 0x0003EFB0
		private void AddIMGUICall(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerCallTrace eventDebuggerCallTrace = new EventDebuggerCallTrace(this.panel, evt, 0, "OnGUI", false, false, false, duration, mouseCapture);
					List<EventDebuggerCallTrace> list;
					bool flag = !this.m_EventCalledObjects.TryGetValue(this.panel, ref list);
					if (flag)
					{
						list = new List<EventDebuggerCallTrace>();
						this.m_EventCalledObjects.Add(this.panel, list);
					}
					list.Add(eventDebuggerCallTrace);
				}
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00040E30 File Offset: 0x0003F030
		private void AddBeginProcessEvent(EventBase evt, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				EventDebuggerTrace eventDebuggerTrace = new EventDebuggerTrace(this.panel, evt, -1L, mouseCapture);
				Stack<EventDebuggerTrace> stack;
				bool flag = !this.m_StackOfProcessedEvent.TryGetValue(this.panel, ref stack);
				if (flag)
				{
					stack = new Stack<EventDebuggerTrace>();
					this.m_StackOfProcessedEvent.Add(this.panel, stack);
				}
				List<EventDebuggerTrace> list;
				bool flag2 = !this.m_EventProcessedEvents.TryGetValue(this.panel, ref list);
				if (flag2)
				{
					list = new List<EventDebuggerTrace>();
					this.m_EventProcessedEvents.Add(this.panel, list);
				}
				list.Add(eventDebuggerTrace);
				stack.Push(eventDebuggerTrace);
				Dictionary<long, int> dictionary;
				bool flag3 = !this.m_EventTypeProcessedCount.TryGetValue(this.panel, ref dictionary);
				if (!flag3)
				{
					int num;
					bool flag4 = !dictionary.TryGetValue(eventDebuggerTrace.eventBase.eventTypeId, ref num);
					if (flag4)
					{
						num = 0;
					}
					dictionary[eventDebuggerTrace.eventBase.eventTypeId] = num + 1;
				}
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00040F30 File Offset: 0x0003F130
		private void AddEndProcessEvent(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool flag = false;
				Stack<EventDebuggerTrace> stack;
				bool flag2 = this.m_StackOfProcessedEvent.TryGetValue(this.panel, ref stack);
				if (flag2)
				{
					bool flag3 = stack.Count > 0;
					if (flag3)
					{
						EventDebuggerTrace eventDebuggerTrace = stack.Peek();
						bool flag4 = eventDebuggerTrace.eventBase.eventId == evt.eventId;
						if (flag4)
						{
							stack.Pop();
							eventDebuggerTrace.duration = duration;
							bool flag5 = eventDebuggerTrace.eventBase.target == null;
							if (flag5)
							{
								eventDebuggerTrace.eventBase.target = evt.target;
							}
							flag = true;
						}
					}
				}
				bool flag6 = !flag;
				if (flag6)
				{
					EventDebuggerTrace eventDebuggerTrace2 = new EventDebuggerTrace(this.panel, evt, duration, mouseCapture);
					List<EventDebuggerTrace> list;
					bool flag7 = !this.m_EventProcessedEvents.TryGetValue(this.panel, ref list);
					if (flag7)
					{
						list = new List<EventDebuggerTrace>();
						this.m_EventProcessedEvents.Add(this.panel, list);
					}
					list.Add(eventDebuggerTrace2);
					Dictionary<long, int> dictionary;
					bool flag8 = !this.m_EventTypeProcessedCount.TryGetValue(this.panel, ref dictionary);
					if (!flag8)
					{
						int num;
						bool flag9 = !dictionary.TryGetValue(eventDebuggerTrace2.eventBase.eventTypeId, ref num);
						if (flag9)
						{
							num = 0;
						}
						dictionary[eventDebuggerTrace2.eventBase.eventTypeId] = num + 1;
					}
				}
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00041094 File Offset: 0x0003F294
		public static string GetObjectDisplayName(object obj, bool withHashCode = true)
		{
			bool flag = obj == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				Type type = obj.GetType();
				string text2 = EventDebugger.GetTypeDisplayName(type);
				bool flag2 = obj is VisualElement;
				if (flag2)
				{
					VisualElement visualElement = obj as VisualElement;
					bool flag3 = !string.IsNullOrEmpty(visualElement.name);
					if (flag3)
					{
						text2 = text2 + "#" + visualElement.name;
					}
				}
				if (withHashCode)
				{
					text2 = text2 + " (" + obj.GetHashCode().ToString("x8") + ")";
				}
				text = text2;
			}
			return text;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0004113C File Offset: 0x0003F33C
		public static string GetTypeDisplayName(Type type)
		{
			return type.IsGenericType ? (type.Name.TrimEnd(new char[] { '`', '1' }) + "<" + type.GetGenericArguments()[0].Name + ">") : type.Name;
		}

		// Token: 0x04000736 RID: 1846
		private Dictionary<IPanel, List<EventDebuggerCallTrace>> m_EventCalledObjects;

		// Token: 0x04000737 RID: 1847
		private Dictionary<IPanel, List<EventDebuggerDefaultActionTrace>> m_EventDefaultActionObjects;

		// Token: 0x04000738 RID: 1848
		private Dictionary<IPanel, List<EventDebuggerPathTrace>> m_EventPathObjects;

		// Token: 0x04000739 RID: 1849
		private Dictionary<IPanel, List<EventDebuggerTrace>> m_EventProcessedEvents;

		// Token: 0x0400073A RID: 1850
		private Dictionary<IPanel, Stack<EventDebuggerTrace>> m_StackOfProcessedEvent;

		// Token: 0x0400073B RID: 1851
		private Dictionary<IPanel, Dictionary<long, int>> m_EventTypeProcessedCount;

		// Token: 0x0400073C RID: 1852
		private readonly Dictionary<IPanel, long> m_ModificationCount;

		// Token: 0x0400073D RID: 1853
		private readonly bool m_Log;

		// Token: 0x02000232 RID: 562
		internal struct HistogramRecord
		{
			// Token: 0x0400073F RID: 1855
			public long count;

			// Token: 0x04000740 RID: 1856
			public long duration;
		}
	}
}
