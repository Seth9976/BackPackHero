using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003A RID: 58
	public class IMGUIContainer : VisualElement, IDisposable
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00006AA8 File Offset: 0x00004CA8
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public Action onGUIHandler
		{
			get
			{
				return this.m_OnGUIHandler;
			}
			set
			{
				bool flag = this.m_OnGUIHandler != value;
				if (flag)
				{
					this.m_OnGUIHandler = value;
					base.IncrementVersion(VersionChangeType.Layout);
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006AFC File Offset: 0x00004CFC
		internal ObjectGUIState guiState
		{
			get
			{
				Debug.Assert(!this.useOwnerObjectGUIState);
				bool flag = this.m_ObjectGUIState == null;
				if (flag)
				{
					this.m_ObjectGUIState = new ObjectGUIState();
				}
				return this.m_ObjectGUIState;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006B3D File Offset: 0x00004D3D
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00006B45 File Offset: 0x00004D45
		internal Rect lastWorldClip { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006B50 File Offset: 0x00004D50
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00006B68 File Offset: 0x00004D68
		public bool cullingEnabled
		{
			get
			{
				return this.m_CullingEnabled;
			}
			set
			{
				this.m_CullingEnabled = value;
				base.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006B80 File Offset: 0x00004D80
		private GUILayoutUtility.LayoutCache cache
		{
			get
			{
				bool flag = this.m_Cache == null;
				if (flag)
				{
					this.m_Cache = new GUILayoutUtility.LayoutCache(-1);
				}
				return this.m_Cache;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006BB4 File Offset: 0x00004DB4
		private float layoutMeasuredWidth
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxWidth);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00006BDC File Offset: 0x00004DDC
		private float layoutMeasuredHeight
		{
			get
			{
				return Mathf.Ceil(this.cache.topLevel.maxHeight);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006C03 File Offset: 0x00004E03
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00006C0B File Offset: 0x00004E0B
		public ContextType contextType { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006C14 File Offset: 0x00004E14
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00006C1C File Offset: 0x00004E1C
		internal bool focusOnlyIfHasFocusableControls { get; set; } = true;

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006C25 File Offset: 0x00004E25
		public override bool canGrabFocus
		{
			get
			{
				return this.focusOnlyIfHasFocusableControls ? (this.hasFocusableControls && base.canGrabFocus) : base.canGrabFocus;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006C48 File Offset: 0x00004E48
		static IMGUIContainer()
		{
			for (int i = 0; i <= Foldout.ussFoldoutMaxDepth; i++)
			{
				IMGUIContainer.ussFoldoutChildDepthClassNames.Add(IMGUIContainer.ussFoldoutChildDepthClassName + i.ToString());
			}
			IMGUIContainer.ussFoldoutChildDepthClassNames.Add(IMGUIContainer.ussFoldoutChildDepthClassName + "max");
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006D32 File Offset: 0x00004F32
		public IMGUIContainer()
			: this(null)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006D40 File Offset: 0x00004F40
		public IMGUIContainer(Action onGUIHandler)
		{
			this.isIMGUIContainer = true;
			base.AddToClassList(IMGUIContainer.ussClassName);
			this.onGUIHandler = onGUIHandler;
			this.contextType = ContextType.Editor;
			base.focusable = true;
			base.requireMeasureFunction = true;
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006E0B File Offset: 0x0000500B
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this.lastWorldClip = base.elementPanel.repaintData.currentWorldClip;
			mgc.painter.DrawImmediate(new Action(this.DoIMGUIRepaint), this.cullingEnabled);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006E44 File Offset: 0x00005044
		private void SaveGlobals()
		{
			this.m_GUIGlobals.matrix = GUI.matrix;
			this.m_GUIGlobals.color = GUI.color;
			this.m_GUIGlobals.contentColor = GUI.contentColor;
			this.m_GUIGlobals.backgroundColor = GUI.backgroundColor;
			this.m_GUIGlobals.enabled = GUI.enabled;
			this.m_GUIGlobals.changed = GUI.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				this.m_GUIGlobals.displayIndex = Event.current.displayIndex;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006ED8 File Offset: 0x000050D8
		private void RestoreGlobals()
		{
			GUI.matrix = this.m_GUIGlobals.matrix;
			GUI.color = this.m_GUIGlobals.color;
			GUI.contentColor = this.m_GUIGlobals.contentColor;
			GUI.backgroundColor = this.m_GUIGlobals.backgroundColor;
			GUI.enabled = this.m_GUIGlobals.enabled;
			GUI.changed = this.m_GUIGlobals.changed;
			bool flag = Event.current != null;
			if (flag)
			{
				Event.current.displayIndex = this.m_GUIGlobals.displayIndex;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006F70 File Offset: 0x00005170
		private void DoOnGUI(Event evt, Matrix4x4 parentTransform, Rect clippingRect, bool isComputingLayout, Rect layoutSize, Action onGUIHandler, bool canAffectFocus = true)
		{
			bool flag = onGUIHandler == null || base.panel == null;
			if (!flag)
			{
				int num = GUIClip.Internal_GetCount();
				this.SaveGlobals();
				float layoutMeasuredWidth = this.layoutMeasuredWidth;
				float layoutMeasuredHeight = this.layoutMeasuredHeight;
				UIElementsUtility.BeginContainerGUI(this.cache, evt, this);
				GUI.color = UIElementsUtility.editorPlayModeTintColor;
				bool flag2 = Event.current.type != EventType.Layout;
				if (flag2)
				{
					bool flag3 = this.lostFocus;
					if (flag3)
					{
						bool flag4 = this.focusController != null;
						if (flag4)
						{
							bool flag5 = GUIUtility.OwnsId(GUIUtility.keyboardControl);
							if (flag5)
							{
								GUIUtility.keyboardControl = 0;
								this.focusController.imguiKeyboardControl = 0;
							}
						}
						this.lostFocus = false;
					}
					bool flag6 = this.receivedFocus;
					if (flag6)
					{
						bool flag7 = this.hasFocusableControls;
						if (flag7)
						{
							bool flag8 = this.focusChangeDirection != FocusChangeDirection.unspecified && this.focusChangeDirection != FocusChangeDirection.none;
							if (flag8)
							{
								bool flag9 = this.focusChangeDirection == VisualElementFocusChangeDirection.left;
								if (flag9)
								{
									GUIUtility.SetKeyboardControlToLastControlId();
								}
								else
								{
									bool flag10 = this.focusChangeDirection == VisualElementFocusChangeDirection.right;
									if (flag10)
									{
										GUIUtility.SetKeyboardControlToFirstControlId();
									}
								}
							}
							else
							{
								bool flag11 = GUIUtility.keyboardControl == 0 && this.m_IsFocusDelegated;
								if (flag11)
								{
									GUIUtility.SetKeyboardControlToFirstControlId();
								}
							}
						}
						bool flag12 = this.focusController != null;
						if (flag12)
						{
							bool flag13 = this.focusController.imguiKeyboardControl != GUIUtility.keyboardControl && this.focusChangeDirection != FocusChangeDirection.unspecified;
							if (flag13)
							{
								this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
							}
							this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
						}
						this.receivedFocus = false;
						this.focusChangeDirection = FocusChangeDirection.unspecified;
					}
				}
				EventType type = Event.current.type;
				bool flag14 = false;
				try
				{
					using (new GUIClip.ParentClipScope(parentTransform, clippingRect))
					{
						using (IMGUIContainer.k_OnGUIMarker.Auto())
						{
							onGUIHandler.Invoke();
						}
					}
				}
				catch (Exception ex)
				{
					bool flag15 = type == EventType.Layout;
					if (!flag15)
					{
						throw;
					}
					flag14 = GUIUtility.IsExitGUIException(ex);
					bool flag16 = !flag14;
					if (flag16)
					{
						Debug.LogException(ex);
					}
				}
				finally
				{
					bool flag17 = Event.current.type != EventType.Layout && canAffectFocus;
					if (flag17)
					{
						int keyboardControl = GUIUtility.keyboardControl;
						int num2 = GUIUtility.CheckForTabEvent(Event.current);
						bool flag18 = this.focusController != null;
						if (flag18)
						{
							bool flag19 = num2 < 0;
							if (flag19)
							{
								Focusable leafFocusedElement = this.focusController.GetLeafFocusedElement();
								Focusable focusable = null;
								using (KeyDownEvent pooled = KeyboardEventBase<KeyDownEvent>.GetPooled('\t', KeyCode.Tab, (num2 == -1) ? EventModifiers.None : EventModifiers.Shift))
								{
									focusable = this.focusController.SwitchFocusOnEvent(pooled);
								}
								bool flag20 = leafFocusedElement == this;
								if (flag20)
								{
									bool flag21 = focusable == this;
									if (flag21)
									{
										bool flag22 = num2 == -2;
										if (flag22)
										{
											GUIUtility.SetKeyboardControlToLastControlId();
										}
										else
										{
											bool flag23 = num2 == -1;
											if (flag23)
											{
												GUIUtility.SetKeyboardControlToFirstControlId();
											}
										}
										this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
										this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									}
									else
									{
										GUIUtility.keyboardControl = 0;
										this.focusController.imguiKeyboardControl = 0;
									}
								}
							}
							else
							{
								bool flag24 = num2 > 0;
								if (flag24)
								{
									this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
									this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
								}
								else
								{
									bool flag25 = num2 == 0;
									if (flag25)
									{
										bool flag26 = type == EventType.MouseDown && !this.focusOnlyIfHasFocusableControls;
										if (flag26)
										{
											this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, true);
										}
										else
										{
											bool flag27 = keyboardControl != GUIUtility.keyboardControl || type == EventType.MouseDown;
											if (flag27)
											{
												this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
											}
											else
											{
												bool flag28 = GUIUtility.keyboardControl != this.focusController.imguiKeyboardControl;
												if (flag28)
												{
													this.newKeyboardFocusControlID = GUIUtility.keyboardControl;
													bool flag29 = this.focusController.GetLeafFocusedElement() == this;
													if (flag29)
													{
														this.focusController.imguiKeyboardControl = GUIUtility.keyboardControl;
													}
													else
													{
														this.focusController.SyncIMGUIFocus(GUIUtility.keyboardControl, this, false);
													}
												}
											}
										}
									}
								}
							}
						}
						this.hasFocusableControls = GUIUtility.HasFocusableControls();
					}
				}
				UIElementsUtility.EndContainerGUI(evt, layoutSize);
				this.RestoreGlobals();
				bool flag30 = evt.type == EventType.Layout && (!Mathf.Approximately(layoutMeasuredWidth, this.layoutMeasuredWidth) || !Mathf.Approximately(layoutMeasuredHeight, this.layoutMeasuredHeight));
				if (flag30)
				{
					bool flag31 = isComputingLayout && clippingRect == Rect.zero;
					if (flag31)
					{
						base.schedule.Execute(delegate
						{
							base.IncrementVersion(VersionChangeType.Layout);
						});
					}
					else
					{
						base.IncrementVersion(VersionChangeType.Layout);
					}
				}
				bool flag32 = !flag14;
				if (flag32)
				{
					bool flag33 = evt.type != EventType.Ignore && evt.type != EventType.Used;
					if (flag33)
					{
						int num3 = GUIClip.Internal_GetCount();
						bool flag34 = num3 > num;
						if (flag34)
						{
							Debug.LogError("GUI Error: You are pushing more GUIClips than you are popping. Make sure they are balanced.");
						}
						else
						{
							bool flag35 = num3 < num;
							if (flag35)
							{
								Debug.LogError("GUI Error: You are popping more GUIClips than you are pushing. Make sure they are balanced.");
							}
						}
					}
				}
				while (GUIClip.Internal_GetCount() > num)
				{
					GUIClip.Internal_Pop();
				}
				bool flag36 = evt.type == EventType.Used;
				if (flag36)
				{
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000758C File Offset: 0x0000578C
		public void MarkDirtyLayout()
		{
			this.m_RefreshCachedLayout = true;
			base.IncrementVersion(VersionChangeType.Layout);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000075A0 File Offset: 0x000057A0
		public override void HandleEvent(EventBase evt)
		{
			base.HandleEvent(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.propagationPhase != PropagationPhase.TrickleDown && evt.propagationPhase != PropagationPhase.AtTarget && evt.propagationPhase != PropagationPhase.BubbleUp;
				if (!flag2)
				{
					bool flag3 = evt.imguiEvent == null;
					if (!flag3)
					{
						bool isPropagationStopped = evt.isPropagationStopped;
						if (!isPropagationStopped)
						{
							bool flag4 = this.SendEventToIMGUI(evt, true, true);
							if (flag4)
							{
								evt.StopPropagation();
								evt.PreventDefault();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007628 File Offset: 0x00005828
		private void DoIMGUIRepaint()
		{
			using (IMGUIContainer.k_ImmediateCallbackMarker.Auto())
			{
				Matrix4x4 currentOffset = base.elementPanel.repaintData.currentOffset;
				this.m_CachedClippingRect = VisualElement.ComputeAAAlignedBound(base.worldClip, currentOffset);
				this.m_CachedTransform = currentOffset * base.worldTransform;
				this.HandleIMGUIEvent(base.elementPanel.repaintData.repaintEvent, this.m_CachedTransform, this.m_CachedClippingRect, this.onGUIHandler, true);
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000076C8 File Offset: 0x000058C8
		internal bool SendEventToIMGUI(EventBase evt, bool canAffectFocus = true, bool verifyBounds = true)
		{
			bool flag = evt is IPointerEvent;
			bool flag12;
			if (flag)
			{
				bool flag2 = evt.imguiEvent != null && evt.imguiEvent.isDirectManipulationDevice;
				if (flag2)
				{
					bool flag3 = false;
					EventType rawType = evt.imguiEvent.rawType;
					bool flag4 = evt is PointerDownEvent;
					if (flag4)
					{
						flag3 = true;
						evt.imguiEvent.type = EventType.TouchDown;
					}
					else
					{
						bool flag5 = evt is PointerUpEvent;
						if (flag5)
						{
							flag3 = true;
							evt.imguiEvent.type = EventType.TouchUp;
						}
						else
						{
							bool flag6 = evt is PointerMoveEvent && evt.imguiEvent.rawType == EventType.MouseDrag;
							if (flag6)
							{
								flag3 = true;
								evt.imguiEvent.type = EventType.TouchMove;
							}
							else
							{
								bool flag7 = evt is PointerLeaveEvent;
								if (flag7)
								{
									flag3 = true;
									evt.imguiEvent.type = EventType.TouchLeave;
								}
								else
								{
									bool flag8 = evt is PointerEnterEvent;
									if (flag8)
									{
										flag3 = true;
										evt.imguiEvent.type = EventType.TouchEnter;
									}
									else
									{
										bool flag9 = evt is PointerStationaryEvent;
										if (flag9)
										{
											flag3 = true;
											evt.imguiEvent.type = EventType.TouchStationary;
										}
									}
								}
							}
						}
					}
					bool flag10 = flag3;
					if (flag10)
					{
						bool flag11 = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
						evt.imguiEvent.type = rawType;
						return flag11;
					}
				}
				flag12 = false;
			}
			else
			{
				flag12 = this.SendEventToIMGUIRaw(evt, canAffectFocus, verifyBounds);
			}
			return flag12;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007834 File Offset: 0x00005A34
		private bool SendEventToIMGUIRaw(EventBase evt, bool canAffectFocus, bool verifyBounds)
		{
			bool flag = verifyBounds && !this.VerifyBounds(evt);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3;
				using (new EventDebuggerLogIMGUICall(evt))
				{
					flag3 = this.HandleIMGUIEvent(evt.imguiEvent, canAffectFocus);
				}
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007898 File Offset: 0x00005A98
		private bool VerifyBounds(EventBase evt)
		{
			return this.IsContainerCapturingTheMouse() || !this.IsLocalEvent(evt) || this.IsEventInsideLocalWindow(evt) || IMGUIContainer.IsDockAreaMouseUp(evt);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000078D0 File Offset: 0x00005AD0
		private bool IsContainerCapturingTheMouse()
		{
			IPanel panel = base.panel;
			IMGUIContainer imguicontainer;
			if (panel == null)
			{
				imguicontainer = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				imguicontainer = ((dispatcher != null) ? dispatcher.pointerState.GetCapturingElement(PointerId.mousePointerId) : null);
			}
			return this == imguicontainer;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007910 File Offset: 0x00005B10
		private bool IsLocalEvent(EventBase evt)
		{
			long eventTypeId = evt.eventTypeId;
			return eventTypeId == EventBase<MouseDownEvent>.TypeId() || eventTypeId == EventBase<MouseUpEvent>.TypeId() || eventTypeId == EventBase<MouseMoveEvent>.TypeId() || eventTypeId == EventBase<PointerDownEvent>.TypeId() || eventTypeId == EventBase<PointerUpEvent>.TypeId() || eventTypeId == EventBase<PointerMoveEvent>.TypeId();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000795C File Offset: 0x00005B5C
		private bool IsEventInsideLocalWindow(EventBase evt)
		{
			Rect currentClipRect = this.GetCurrentClipRect();
			IPointerEvent pointerEvent = evt as IPointerEvent;
			string text = ((pointerEvent != null) ? pointerEvent.pointerType : null);
			bool flag = text == PointerType.touch || text == PointerType.pen;
			return GUIUtility.HitTest(currentClipRect, evt.originalMousePosition, flag);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000079B4 File Offset: 0x00005BB4
		private static bool IsDockAreaMouseUp(EventBase evt)
		{
			bool flag;
			if (evt.eventTypeId == EventBase<MouseUpEvent>.TypeId())
			{
				IMGUIContainer target = evt.target;
				VisualElement visualElement = evt.target as VisualElement;
				flag = target == ((visualElement != null) ? visualElement.elementPanel.rootIMGUIContainer : null);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000079FC File Offset: 0x00005BFC
		private bool HandleIMGUIEvent(Event e, bool canAffectFocus)
		{
			return this.HandleIMGUIEvent(e, this.onGUIHandler, canAffectFocus);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007A1C File Offset: 0x00005C1C
		internal bool HandleIMGUIEvent(Event e, Action onGUIHandler, bool canAffectFocus)
		{
			IMGUIContainer.GetCurrentTransformAndClip(this, e, out this.m_CachedTransform, out this.m_CachedClippingRect);
			return this.HandleIMGUIEvent(e, this.m_CachedTransform, this.m_CachedClippingRect, onGUIHandler, canAffectFocus);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007A58 File Offset: 0x00005C58
		private bool HandleIMGUIEvent(Event e, Matrix4x4 worldTransform, Rect clippingRect, Action onGUIHandler, bool canAffectFocus)
		{
			bool flag = e == null || onGUIHandler == null || base.elementPanel == null || !base.elementPanel.IMGUIEventInterests.WantsEvent(e.rawType);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventType rawType = e.rawType;
				bool flag3 = rawType != EventType.Layout;
				if (flag3)
				{
					bool flag4 = this.m_RefreshCachedLayout || base.elementPanel.IMGUIEventInterests.WantsLayoutPass(e.rawType);
					if (flag4)
					{
						e.type = EventType.Layout;
						this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
						this.m_RefreshCachedLayout = false;
						e.type = rawType;
					}
					else
					{
						this.cache.ResetCursor();
					}
				}
				this.DoOnGUI(e, worldTransform, clippingRect, false, base.layout, onGUIHandler, canAffectFocus);
				bool flag5 = this.newKeyboardFocusControlID > 0;
				if (flag5)
				{
					this.newKeyboardFocusControlID = 0;
					Event @event = new Event
					{
						type = EventType.ExecuteCommand,
						commandName = "NewKeyboardFocus"
					};
					this.HandleIMGUIEvent(@event, true);
				}
				bool flag6 = e.rawType == EventType.Used;
				if (flag6)
				{
					flag2 = true;
				}
				else
				{
					bool flag7 = e.rawType == EventType.MouseUp && this.HasMouseCapture();
					if (flag7)
					{
						GUIUtility.hotControl = 0;
					}
					bool flag8 = base.elementPanel == null;
					if (flag8)
					{
						GUIUtility.ExitGUI();
					}
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007BC4 File Offset: 0x00005DC4
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.lostFocus = true;
					base.IncrementVersion(VersionChangeType.Repaint);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
					if (flag3)
					{
						FocusEvent focusEvent = evt as FocusEvent;
						this.receivedFocus = true;
						this.focusChangeDirection = focusEvent.direction;
						this.m_IsFocusDelegated = focusEvent.IsFocusDelegated;
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<DetachFromPanelEvent>.TypeId();
						if (flag4)
						{
							bool flag5 = base.elementPanel != null;
							if (flag5)
							{
								BaseVisualElementPanel elementPanel = base.elementPanel;
								int num = elementPanel.IMGUIContainersCount;
								elementPanel.IMGUIContainersCount = num - 1;
							}
						}
						else
						{
							bool flag6 = evt.eventTypeId == EventBase<AttachToPanelEvent>.TypeId();
							if (flag6)
							{
								bool flag7 = base.elementPanel != null;
								if (flag7)
								{
									BaseVisualElementPanel elementPanel2 = base.elementPanel;
									int num = elementPanel2.IMGUIContainersCount;
									elementPanel2.IMGUIContainersCount = num + 1;
									this.SetFoldoutDepthClass();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007CD0 File Offset: 0x00005ED0
		private void SetFoldoutDepthClass()
		{
			for (int i = 0; i < IMGUIContainer.ussFoldoutChildDepthClassNames.Count; i++)
			{
				base.RemoveFromClassList(IMGUIContainer.ussFoldoutChildDepthClassNames[i]);
			}
			int num = this.GetFoldoutDepth();
			bool flag = num == 0;
			if (!flag)
			{
				num = Mathf.Min(num, IMGUIContainer.ussFoldoutChildDepthClassNames.Count - 1);
				base.AddToClassList(IMGUIContainer.ussFoldoutChildDepthClassNames[num]);
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007D44 File Offset: 0x00005F44
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = false;
			bool flag2 = widthMode != VisualElement.MeasureMode.Exactly || heightMode != VisualElement.MeasureMode.Exactly;
			if (flag2)
			{
				bool flag3 = Event.current != null;
				if (flag3)
				{
					IMGUIContainer.s_CurrentEvent.CopyFrom(Event.current);
					flag = true;
				}
				IMGUIContainer.s_MeasureEvent.CopyFrom(IMGUIContainer.s_DefaultMeasureEvent);
				Rect layout = base.layout;
				if (widthMode == VisualElement.MeasureMode.Exactly)
				{
					layout.width = desiredWidth;
				}
				if (heightMode == VisualElement.MeasureMode.Exactly)
				{
					layout.height = desiredHeight;
				}
				this.DoOnGUI(IMGUIContainer.s_MeasureEvent, this.m_CachedTransform, this.m_CachedClippingRect, true, layout, this.onGUIHandler, true);
				num = this.layoutMeasuredWidth;
				num2 = this.layoutMeasuredHeight;
				bool flag4 = flag;
				if (flag4)
				{
					Event.current.CopyFrom(IMGUIContainer.s_CurrentEvent);
				}
			}
			if (widthMode != VisualElement.MeasureMode.Exactly)
			{
				if (widthMode == VisualElement.MeasureMode.AtMost)
				{
					num = Mathf.Min(num, desiredWidth);
				}
			}
			else
			{
				num = desiredWidth;
			}
			if (heightMode != VisualElement.MeasureMode.Exactly)
			{
				if (heightMode == VisualElement.MeasureMode.AtMost)
				{
					num2 = Mathf.Min(num2, desiredHeight);
				}
			}
			else
			{
				num2 = desiredHeight;
			}
			return new Vector2(num, num2);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007E80 File Offset: 0x00006080
		private Rect GetCurrentClipRect()
		{
			Rect rect = this.lastWorldClip;
			bool flag = rect.width == 0f || rect.height == 0f;
			if (flag)
			{
				rect = base.worldBound;
			}
			return rect;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007EC8 File Offset: 0x000060C8
		private static void GetCurrentTransformAndClip(IMGUIContainer container, Event evt, out Matrix4x4 transform, out Rect clipRect)
		{
			clipRect = container.GetCurrentClipRect();
			transform = container.worldTransform;
			bool flag = evt != null && evt.rawType == EventType.Repaint && container.elementPanel != null;
			if (flag)
			{
				transform = container.elementPanel.repaintData.currentOffset * container.worldTransform;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007F2D File Offset: 0x0000612D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007F40 File Offset: 0x00006140
		protected virtual void Dispose(bool disposeManaged)
		{
			if (disposeManaged)
			{
				ObjectGUIState objectGUIState = this.m_ObjectGUIState;
				if (objectGUIState != null)
				{
					objectGUIState.Dispose();
				}
			}
		}

		// Token: 0x04000092 RID: 146
		private Action m_OnGUIHandler;

		// Token: 0x04000093 RID: 147
		private ObjectGUIState m_ObjectGUIState;

		// Token: 0x04000094 RID: 148
		internal bool useOwnerObjectGUIState;

		// Token: 0x04000096 RID: 150
		private bool m_CullingEnabled = false;

		// Token: 0x04000097 RID: 151
		private bool m_IsFocusDelegated = false;

		// Token: 0x04000098 RID: 152
		private bool m_RefreshCachedLayout = true;

		// Token: 0x04000099 RID: 153
		private GUILayoutUtility.LayoutCache m_Cache = null;

		// Token: 0x0400009A RID: 154
		private Rect m_CachedClippingRect = Rect.zero;

		// Token: 0x0400009B RID: 155
		private Matrix4x4 m_CachedTransform = Matrix4x4.identity;

		// Token: 0x0400009D RID: 157
		private bool lostFocus = false;

		// Token: 0x0400009E RID: 158
		private bool receivedFocus = false;

		// Token: 0x0400009F RID: 159
		private FocusChangeDirection focusChangeDirection = FocusChangeDirection.unspecified;

		// Token: 0x040000A0 RID: 160
		private bool hasFocusableControls = false;

		// Token: 0x040000A1 RID: 161
		private int newKeyboardFocusControlID = 0;

		// Token: 0x040000A3 RID: 163
		public static readonly string ussClassName = "unity-imgui-container";

		// Token: 0x040000A4 RID: 164
		internal static readonly string ussFoldoutChildDepthClassName = Foldout.ussClassName + "__" + IMGUIContainer.ussClassName + "--depth-";

		// Token: 0x040000A5 RID: 165
		internal static readonly List<string> ussFoldoutChildDepthClassNames = new List<string>(Foldout.ussFoldoutMaxDepth + 1);

		// Token: 0x040000A6 RID: 166
		private IMGUIContainer.GUIGlobals m_GUIGlobals;

		// Token: 0x040000A7 RID: 167
		private static readonly ProfilerMarker k_OnGUIMarker = new ProfilerMarker("OnGUI");

		// Token: 0x040000A8 RID: 168
		private static readonly ProfilerMarker k_ImmediateCallbackMarker = new ProfilerMarker("IMGUIContainer");

		// Token: 0x040000A9 RID: 169
		private static Event s_DefaultMeasureEvent = new Event
		{
			type = EventType.Layout
		};

		// Token: 0x040000AA RID: 170
		private static Event s_MeasureEvent = new Event
		{
			type = EventType.Layout
		};

		// Token: 0x040000AB RID: 171
		private static Event s_CurrentEvent = new Event
		{
			type = EventType.Layout
		};

		// Token: 0x0200003B RID: 59
		public new class UxmlFactory : UxmlFactory<IMGUIContainer, IMGUIContainer.UxmlTraits>
		{
		}

		// Token: 0x0200003C RID: 60
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x0600018E RID: 398 RVA: 0x00007F7A File Offset: 0x0000617A
			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x0600018F RID: 399 RVA: 0x00007FA0 File Offset: 0x000061A0
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}
		}

		// Token: 0x0200003E RID: 62
		private struct GUIGlobals
		{
			// Token: 0x040000B0 RID: 176
			public Matrix4x4 matrix;

			// Token: 0x040000B1 RID: 177
			public Color color;

			// Token: 0x040000B2 RID: 178
			public Color contentColor;

			// Token: 0x040000B3 RID: 179
			public Color backgroundColor;

			// Token: 0x040000B4 RID: 180
			public bool enabled;

			// Token: 0x040000B5 RID: 181
			public bool changed;

			// Token: 0x040000B6 RID: 182
			public int displayIndex;
		}
	}
}
