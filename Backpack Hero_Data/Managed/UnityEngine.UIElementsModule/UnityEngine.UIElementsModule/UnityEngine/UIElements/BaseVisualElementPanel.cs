using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x02000058 RID: 88
	internal abstract class BaseVisualElementPanel : IPanel, IDisposable, IGroupBox
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001F6 RID: 502
		// (set) Token: 0x060001F7 RID: 503
		public abstract EventInterests IMGUIEventInterests { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001F8 RID: 504
		// (set) Token: 0x060001F9 RID: 505
		public abstract ScriptableObject ownerObject { get; protected set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001FA RID: 506
		// (set) Token: 0x060001FB RID: 507
		public abstract SavePersistentViewData saveViewData { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001FC RID: 508
		// (set) Token: 0x060001FD RID: 509
		public abstract GetViewDataDictionary getViewDataDictionary { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001FE RID: 510
		// (set) Token: 0x060001FF RID: 511
		public abstract int IMGUIContainersCount { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000200 RID: 512
		// (set) Token: 0x06000201 RID: 513
		public abstract FocusController focusController { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000202 RID: 514
		// (set) Token: 0x06000203 RID: 515
		public abstract IMGUIContainer rootIMGUIContainer { get; set; }

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000204 RID: 516 RVA: 0x00008BAC File Offset: 0x00006DAC
		// (remove) Token: 0x06000205 RID: 517 RVA: 0x00008BE4 File Offset: 0x00006DE4
		[field: DebuggerBrowsable(0)]
		internal event Action<BaseVisualElementPanel> panelDisposed;

		// Token: 0x06000206 RID: 518 RVA: 0x00008C1C File Offset: 0x00006E1C
		protected BaseVisualElementPanel()
		{
			this.yogaConfig = new YogaConfig();
			this.yogaConfig.UseWebDefaults = YogaConfig.Default.UseWebDefaults;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008C9E File Offset: 0x00006E9E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008CB0 File Offset: 0x00006EB0
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.ownerObject != null;
					if (flag)
					{
						UIElementsUtility.RemoveCachedPanel(this.ownerObject.GetInstanceID());
					}
					PointerDeviceState.RemovePanelData(this);
				}
				Action<BaseVisualElementPanel> action = this.panelDisposed;
				if (action != null)
				{
					action.Invoke(this);
				}
				this.yogaConfig = null;
				this.disposed = true;
			}
		}

		// Token: 0x06000209 RID: 521
		public abstract void Repaint(Event e);

		// Token: 0x0600020A RID: 522
		public abstract void ValidateLayout();

		// Token: 0x0600020B RID: 523
		public abstract void UpdateAnimations();

		// Token: 0x0600020C RID: 524
		public abstract void UpdateBindings();

		// Token: 0x0600020D RID: 525
		public abstract void ApplyStyles();

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00008D20 File Offset: 0x00006F20
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00008D38 File Offset: 0x00006F38
		internal float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_Scale, value);
				if (flag)
				{
					this.m_Scale = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					this.yogaConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00008D90 File Offset: 0x00006F90
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00008DA8 File Offset: 0x00006FA8
		internal float pixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_PixelsPerPoint, value);
				if (flag)
				{
					this.m_PixelsPerPoint = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					this.yogaConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008E00 File Offset: 0x00007000
		public float scaledPixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint * this.m_Scale;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00008E20 File Offset: 0x00007020
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00008E60 File Offset: 0x00007060
		public PanelClearFlags clearFlags
		{
			get
			{
				PanelClearFlags panelClearFlags = PanelClearFlags.None;
				bool clearColor = this.clearSettings.clearColor;
				if (clearColor)
				{
					panelClearFlags |= PanelClearFlags.Color;
				}
				bool clearDepthStencil = this.clearSettings.clearDepthStencil;
				if (clearDepthStencil)
				{
					panelClearFlags |= PanelClearFlags.Depth;
				}
				return panelClearFlags;
			}
			set
			{
				PanelClearSettings clearSettings = this.clearSettings;
				clearSettings.clearColor = (value & PanelClearFlags.Color) == PanelClearFlags.Color;
				clearSettings.clearDepthStencil = (value & PanelClearFlags.Depth) == PanelClearFlags.Depth;
				this.clearSettings = clearSettings;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008E97 File Offset: 0x00007097
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00008E9F File Offset: 0x0000709F
		internal PanelClearSettings clearSettings { get; set; } = new PanelClearSettings
		{
			clearDepthStencil = true,
			clearColor = true,
			color = Color.clear
		};

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00008EA8 File Offset: 0x000070A8
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00008EB0 File Offset: 0x000070B0
		internal bool duringLayoutPhase { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00008EBC File Offset: 0x000070BC
		internal bool isDirty
		{
			get
			{
				return this.version != this.repaintVersion;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600021A RID: 538
		internal abstract uint version { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600021B RID: 539
		internal abstract uint repaintVersion { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600021C RID: 540
		internal abstract uint hierarchyVersion { get; }

		// Token: 0x0600021D RID: 541
		internal abstract void OnVersionChanged(VisualElement ele, VersionChangeType changeTypeFlag);

		// Token: 0x0600021E RID: 542
		internal abstract void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase);

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008EDF File Offset: 0x000070DF
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00008EE7 File Offset: 0x000070E7
		internal virtual RepaintData repaintData { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008EF0 File Offset: 0x000070F0
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00008EF8 File Offset: 0x000070F8
		internal virtual ICursorManager cursorManager { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00008F01 File Offset: 0x00007101
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00008F09 File Offset: 0x00007109
		public ContextualMenuManager contextualMenuManager { get; internal set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000225 RID: 549
		public abstract VisualElement visualTree { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000226 RID: 550
		// (set) Token: 0x06000227 RID: 551
		public abstract EventDispatcher dispatcher { get; set; }

		// Token: 0x06000228 RID: 552 RVA: 0x00008F12 File Offset: 0x00007112
		internal void SendEvent(EventBase e, DispatchMode dispatchMode = DispatchMode.Default)
		{
			Debug.Assert(this.dispatcher != null);
			EventDispatcher dispatcher = this.dispatcher;
			if (dispatcher != null)
			{
				dispatcher.Dispatch(e, this, dispatchMode);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000229 RID: 553
		internal abstract IScheduler scheduler { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600022A RID: 554
		// (set) Token: 0x0600022B RID: 555
		internal abstract IStylePropertyAnimationSystem styleAnimationSystem { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600022C RID: 556
		// (set) Token: 0x0600022D RID: 557
		public abstract ContextType contextType { get; protected set; }

		// Token: 0x0600022E RID: 558
		public abstract VisualElement Pick(Vector2 point);

		// Token: 0x0600022F RID: 559
		public abstract VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00008F39 File Offset: 0x00007139
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00008F41 File Offset: 0x00007141
		internal bool disposed { get; private set; }

		// Token: 0x06000232 RID: 562
		internal abstract IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase);

		// Token: 0x06000233 RID: 563 RVA: 0x00008F4C File Offset: 0x0000714C
		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_TopElementUnderPointers.GetTopElementUnderPointer(pointerId);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008F6C File Offset: 0x0000716C
		internal VisualElement RecomputeTopElementUnderPointer(int pointerId, Vector2 pointerPos, EventBase triggerEvent)
		{
			VisualElement visualElement = null;
			bool flag = PointerDeviceState.GetPanel(pointerId, this.contextType) == this && !PointerDeviceState.HasLocationFlag(pointerId, this.contextType, PointerDeviceState.LocationFlag.OutsidePanel);
			if (flag)
			{
				visualElement = this.Pick(pointerPos);
			}
			this.m_TopElementUnderPointers.SetElementUnderPointer(visualElement, pointerId, triggerEvent);
			return visualElement;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008FC0 File Offset: 0x000071C0
		internal void ClearCachedElementUnderPointer(int pointerId, EventBase triggerEvent)
		{
			this.m_TopElementUnderPointers.SetTemporaryElementUnderPointer(null, pointerId, triggerEvent);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008FD2 File Offset: 0x000071D2
		internal void CommitElementUnderPointers()
		{
			this.m_TopElementUnderPointers.CommitElementUnderPointers(this.dispatcher, this.contextType);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000237 RID: 567
		// (set) Token: 0x06000238 RID: 568
		internal abstract Shader standardShader { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008FF0 File Offset: 0x000071F0
		// (set) Token: 0x0600023A RID: 570 RVA: 0x000020E6 File Offset: 0x000002E6
		internal virtual Shader standardWorldSpaceShader
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600023B RID: 571 RVA: 0x00009004 File Offset: 0x00007204
		// (remove) Token: 0x0600023C RID: 572 RVA: 0x0000903C File Offset: 0x0000723C
		[field: DebuggerBrowsable(0)]
		internal event Action standardShaderChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600023D RID: 573 RVA: 0x00009074 File Offset: 0x00007274
		// (remove) Token: 0x0600023E RID: 574 RVA: 0x000090AC File Offset: 0x000072AC
		[field: DebuggerBrowsable(0)]
		internal event Action standardWorldSpaceShaderChanged;

		// Token: 0x0600023F RID: 575 RVA: 0x000090E4 File Offset: 0x000072E4
		protected void InvokeStandardShaderChanged()
		{
			bool flag = this.standardShaderChanged != null;
			if (flag)
			{
				this.standardShaderChanged.Invoke();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000910C File Offset: 0x0000730C
		protected void InvokeStandardWorldSpaceShaderChanged()
		{
			bool flag = this.standardWorldSpaceShaderChanged != null;
			if (flag)
			{
				this.standardWorldSpaceShaderChanged.Invoke();
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000241 RID: 577 RVA: 0x00009134 File Offset: 0x00007334
		// (remove) Token: 0x06000242 RID: 578 RVA: 0x0000916C File Offset: 0x0000736C
		[field: DebuggerBrowsable(0)]
		internal event Action atlasChanged;

		// Token: 0x06000243 RID: 579 RVA: 0x000091A1 File Offset: 0x000073A1
		protected void InvokeAtlasChanged()
		{
			Action action = this.atlasChanged;
			if (action != null)
			{
				action.Invoke();
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000244 RID: 580
		// (set) Token: 0x06000245 RID: 581
		public abstract AtlasBase atlas { get; set; }

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000246 RID: 582 RVA: 0x000091B8 File Offset: 0x000073B8
		// (remove) Token: 0x06000247 RID: 583 RVA: 0x000091F0 File Offset: 0x000073F0
		[field: DebuggerBrowsable(0)]
		internal event Action<Material> updateMaterial;

		// Token: 0x06000248 RID: 584 RVA: 0x00009225 File Offset: 0x00007425
		internal void InvokeUpdateMaterial(Material mat)
		{
			Action<Material> action = this.updateMaterial;
			if (action != null)
			{
				action.Invoke(mat);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000249 RID: 585 RVA: 0x0000923C File Offset: 0x0000743C
		// (remove) Token: 0x0600024A RID: 586 RVA: 0x00009274 File Offset: 0x00007474
		[field: DebuggerBrowsable(0)]
		internal event HierarchyEvent hierarchyChanged;

		// Token: 0x0600024B RID: 587 RVA: 0x000092AC File Offset: 0x000074AC
		internal void InvokeHierarchyChanged(VisualElement ve, HierarchyChangeType changeType)
		{
			bool flag = this.hierarchyChanged != null;
			if (flag)
			{
				this.hierarchyChanged(ve, changeType);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600024C RID: 588 RVA: 0x000092D8 File Offset: 0x000074D8
		// (remove) Token: 0x0600024D RID: 589 RVA: 0x00009310 File Offset: 0x00007510
		[field: DebuggerBrowsable(0)]
		internal event Action<IPanel> beforeUpdate;

		// Token: 0x0600024E RID: 590 RVA: 0x00009345 File Offset: 0x00007545
		internal void InvokeBeforeUpdate()
		{
			Action<IPanel> action = this.beforeUpdate;
			if (action != null)
			{
				action.Invoke(this);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000935C File Offset: 0x0000755C
		internal void UpdateElementUnderPointers()
		{
			foreach (int num in PointerId.hoveringPointers)
			{
				bool flag = PointerDeviceState.GetPanel(num, this.contextType) != this || PointerDeviceState.HasLocationFlag(num, this.contextType, PointerDeviceState.LocationFlag.OutsidePanel);
				if (flag)
				{
					this.m_TopElementUnderPointers.SetElementUnderPointer(null, num, new Vector2(float.MinValue, float.MinValue));
				}
				else
				{
					Vector2 pointerPosition = PointerDeviceState.GetPointerPosition(num, this.contextType);
					VisualElement visualElement = this.PickAll(pointerPosition, null);
					this.m_TopElementUnderPointers.SetElementUnderPointer(visualElement, num, pointerPosition);
				}
			}
			this.CommitElementUnderPointers();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000093FC File Offset: 0x000075FC
		public virtual void Update()
		{
			this.scheduler.UpdateScheduledEvents();
			this.ValidateLayout();
			this.UpdateAnimations();
			this.UpdateBindings();
		}

		// Token: 0x0400010E RID: 270
		private float m_Scale = 1f;

		// Token: 0x0400010F RID: 271
		internal YogaConfig yogaConfig;

		// Token: 0x04000110 RID: 272
		private float m_PixelsPerPoint = 1f;

		// Token: 0x04000117 RID: 279
		internal ElementUnderPointer m_TopElementUnderPointers = new ElementUnderPointer();
	}
}
