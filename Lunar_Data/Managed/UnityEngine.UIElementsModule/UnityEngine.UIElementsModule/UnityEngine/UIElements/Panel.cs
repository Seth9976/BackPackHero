using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005D RID: 93
	internal class Panel : BaseVisualElementPanel
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00009420 File Offset: 0x00007620
		public sealed override VisualElement visualTree
		{
			get
			{
				return this.m_RootContainer;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00009438 File Offset: 0x00007638
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00009440 File Offset: 0x00007640
		public sealed override EventDispatcher dispatcher { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000944C File Offset: 0x0000764C
		public TimerEventScheduler timerEventScheduler
		{
			get
			{
				TimerEventScheduler timerEventScheduler;
				if ((timerEventScheduler = this.m_Scheduler) == null)
				{
					timerEventScheduler = (this.m_Scheduler = new TimerEventScheduler());
				}
				return timerEventScheduler;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00009478 File Offset: 0x00007678
		internal override IScheduler scheduler
		{
			get
			{
				return this.timerEventScheduler;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00009490 File Offset: 0x00007690
		internal VisualTreeUpdater visualTreeUpdater
		{
			get
			{
				return this.m_VisualTreeUpdater;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000094A8 File Offset: 0x000076A8
		// (set) Token: 0x06000268 RID: 616 RVA: 0x000094B0 File Offset: 0x000076B0
		internal override IStylePropertyAnimationSystem styleAnimationSystem
		{
			get
			{
				return this.m_StylePropertyAnimationSystem;
			}
			set
			{
				bool flag = this.m_StylePropertyAnimationSystem == value;
				if (!flag)
				{
					IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.m_StylePropertyAnimationSystem;
					if (stylePropertyAnimationSystem != null)
					{
						stylePropertyAnimationSystem.CancelAllAnimations();
					}
					this.m_StylePropertyAnimationSystem = value;
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000094E6 File Offset: 0x000076E6
		// (set) Token: 0x0600026A RID: 618 RVA: 0x000094EE File Offset: 0x000076EE
		public override ScriptableObject ownerObject { get; protected set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000094F7 File Offset: 0x000076F7
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000094FF File Offset: 0x000076FF
		public override ContextType contextType { get; protected set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009508 File Offset: 0x00007708
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00009510 File Offset: 0x00007710
		public override SavePersistentViewData saveViewData { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00009519 File Offset: 0x00007719
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00009521 File Offset: 0x00007721
		public override GetViewDataDictionary getViewDataDictionary { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000952A File Offset: 0x0000772A
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00009532 File Offset: 0x00007732
		public sealed override FocusController focusController { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000953B File Offset: 0x0000773B
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00009543 File Offset: 0x00007743
		public override EventInterests IMGUIEventInterests { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000954C File Offset: 0x0000774C
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00009553 File Offset: 0x00007753
		internal static LoadResourceFunction loadResourceFunc { private get; set; }

		// Token: 0x06000277 RID: 631 RVA: 0x0000955C File Offset: 0x0000775C
		internal static Object LoadResource(string pathName, Type type, float dpiScaling)
		{
			bool flag = Panel.loadResourceFunc != null;
			Object @object;
			if (flag)
			{
				@object = Panel.loadResourceFunc(pathName, type, dpiScaling);
			}
			else
			{
				@object = Resources.Load(pathName, type);
			}
			return @object;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009599 File Offset: 0x00007799
		internal void Focus()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.SetFocusToLastFocusedElement();
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000095AE File Offset: 0x000077AE
		internal void Blur()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.BlurLastFocusedElement();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000095C4 File Offset: 0x000077C4
		// (set) Token: 0x0600027B RID: 635 RVA: 0x000095DC File Offset: 0x000077DC
		internal string name
		{
			get
			{
				return this.m_PanelName;
			}
			set
			{
				this.m_PanelName = value;
				this.CreateMarkers();
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000095F0 File Offset: 0x000077F0
		private void CreateMarkers()
		{
			bool flag = !string.IsNullOrEmpty(this.m_PanelName);
			if (flag)
			{
				this.m_MarkerBeforeUpdate = new ProfilerMarker("Panel.BeforeUpdate." + this.m_PanelName);
				this.m_MarkerUpdate = new ProfilerMarker("Panel.Update." + this.m_PanelName);
				this.m_MarkerLayout = new ProfilerMarker("Panel.Layout." + this.m_PanelName);
				this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings." + this.m_PanelName);
				this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations." + this.m_PanelName);
			}
			else
			{
				this.m_MarkerBeforeUpdate = new ProfilerMarker("Panel.BeforeUpdate");
				this.m_MarkerUpdate = new ProfilerMarker("Panel.Update");
				this.m_MarkerLayout = new ProfilerMarker("Panel.Layout");
				this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings");
				this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000096F0 File Offset: 0x000078F0
		// (set) Token: 0x0600027E RID: 638 RVA: 0x000096F7 File Offset: 0x000078F7
		internal static TimeMsFunction TimeSinceStartup { private get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000096FF File Offset: 0x000078FF
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00009707 File Offset: 0x00007907
		public override int IMGUIContainersCount { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00009710 File Offset: 0x00007910
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00009718 File Offset: 0x00007918
		public override IMGUIContainer rootIMGUIContainer { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00009721 File Offset: 0x00007921
		internal override uint version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00009729 File Offset: 0x00007929
		internal override uint repaintVersion
		{
			get
			{
				return this.m_RepaintVersion;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009731 File Offset: 0x00007931
		internal override uint hierarchyVersion
		{
			get
			{
				return this.m_HierarchyVersion;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000973C File Offset: 0x0000793C
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00009754 File Offset: 0x00007954
		internal override Shader standardShader
		{
			get
			{
				return this.m_StandardShader;
			}
			set
			{
				bool flag = this.m_StandardShader != value;
				if (flag)
				{
					this.m_StandardShader = value;
					base.InvokeStandardShaderChanged();
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00009784 File Offset: 0x00007984
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000979C File Offset: 0x0000799C
		public override AtlasBase atlas
		{
			get
			{
				return this.m_Atlas;
			}
			set
			{
				bool flag = this.m_Atlas != value;
				if (flag)
				{
					AtlasBase atlas = this.m_Atlas;
					if (atlas != null)
					{
						atlas.InvokeRemovedFromPanel(this);
					}
					this.m_Atlas = value;
					base.InvokeAtlasChanged();
					AtlasBase atlas2 = this.m_Atlas;
					if (atlas2 != null)
					{
						atlas2.InvokeAssignedToPanel(this);
					}
				}
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000097F0 File Offset: 0x000079F0
		internal static Panel CreateEditorPanel(ScriptableObject ownerObject)
		{
			return new Panel(ownerObject, ContextType.Editor, EventDispatcher.CreateDefault());
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009810 File Offset: 0x00007A10
		public Panel(ScriptableObject ownerObject, ContextType contextType, EventDispatcher dispatcher)
		{
			this.ownerObject = ownerObject;
			this.contextType = contextType;
			this.dispatcher = dispatcher;
			this.repaintData = new RepaintData();
			this.cursorManager = new CursorManager();
			base.contextualMenuManager = null;
			this.m_VisualTreeUpdater = new VisualTreeUpdater(this);
			this.m_RootContainer = new VisualElement
			{
				name = VisualElementUtils.GetUniqueName("unity-panel-container"),
				viewDataKey = "PanelContainer",
				pickingMode = ((contextType == ContextType.Editor) ? PickingMode.Position : PickingMode.Ignore)
			};
			this.visualTree.SetPanel(this);
			this.focusController = new FocusController(new VisualElementFocusRing(this.visualTree, VisualElementFocusRing.DefaultFocusOrder.ChildOrder));
			this.styleAnimationSystem = new StylePropertyAnimationSystem();
			this.CreateMarkers();
			base.InvokeHierarchyChanged(this.visualTree, HierarchyChangeType.Add);
			this.atlas = new DynamicAtlas();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009910 File Offset: 0x00007B10
		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.atlas = null;
					this.m_VisualTreeUpdater.Dispose();
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009950 File Offset: 0x00007B50
		public static long TimeSinceStartupMs()
		{
			TimeMsFunction timeSinceStartup = Panel.TimeSinceStartup;
			return (timeSinceStartup != null) ? timeSinceStartup() : Panel.DefaultTimeSinceStartupMs();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009978 File Offset: 0x00007B78
		internal static long DefaultTimeSinceStartupMs()
		{
			return (long)(Time.realtimeSinceStartup * 1000f);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009998 File Offset: 0x00007B98
		internal static VisualElement PickAllWithoutValidatingLayout(VisualElement root, Vector2 point)
		{
			return Panel.PickAll(root, point, null);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000099B4 File Offset: 0x00007BB4
		private static VisualElement PickAll(VisualElement root, Vector2 point, List<VisualElement> picked = null)
		{
			Panel.s_MarkerPickAll.Begin();
			VisualElement visualElement = Panel.PerformPick(root, point, picked);
			Panel.s_MarkerPickAll.End();
			return visualElement;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000099E8 File Offset: 0x00007BE8
		private static VisualElement PerformPick(VisualElement root, Vector2 point, List<VisualElement> picked = null)
		{
			bool flag = root.resolvedStyle.display == DisplayStyle.None;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = null;
			}
			else
			{
				bool flag2 = root.pickingMode == PickingMode.Ignore && root.hierarchy.childCount == 0;
				if (flag2)
				{
					visualElement = null;
				}
				else
				{
					bool flag3 = !root.worldBoundingBox.Contains(point);
					if (flag3)
					{
						visualElement = null;
					}
					else
					{
						Vector2 vector = root.WorldToLocal(point);
						bool flag4 = root.ContainsPoint(vector);
						bool flag5 = !flag4 && root.ShouldClip();
						if (flag5)
						{
							visualElement = null;
						}
						else
						{
							VisualElement visualElement2 = null;
							int childCount = root.hierarchy.childCount;
							for (int i = childCount - 1; i >= 0; i--)
							{
								VisualElement visualElement3 = root.hierarchy[i];
								VisualElement visualElement4 = Panel.PerformPick(visualElement3, point, picked);
								bool flag6 = visualElement2 == null && visualElement4 != null;
								if (flag6)
								{
									bool flag7 = picked == null;
									if (flag7)
									{
										return visualElement4;
									}
									visualElement2 = visualElement4;
								}
							}
							bool flag8 = root.visible && root.pickingMode == PickingMode.Position && flag4;
							if (flag8)
							{
								if (picked != null)
								{
									picked.Add(root);
								}
								bool flag9 = visualElement2 == null;
								if (flag9)
								{
									visualElement2 = root;
								}
							}
							visualElement = visualElement2;
						}
					}
				}
			}
			return visualElement;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009B40 File Offset: 0x00007D40
		public override VisualElement PickAll(Vector2 point, List<VisualElement> picked)
		{
			this.ValidateLayout();
			bool flag = picked != null;
			if (flag)
			{
				picked.Clear();
			}
			return Panel.PickAll(this.visualTree, point, picked);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009B78 File Offset: 0x00007D78
		public override VisualElement Pick(Vector2 point)
		{
			this.ValidateLayout();
			Vector2 vector;
			bool flag;
			VisualElement topElementUnderPointer = this.m_TopElementUnderPointers.GetTopElementUnderPointer(PointerId.mousePointerId, out vector, out flag);
			bool flag2 = !flag && Panel.<Pick>g__PixelOf|99_0(vector) == Panel.<Pick>g__PixelOf|99_0(point);
			VisualElement visualElement;
			if (flag2)
			{
				visualElement = topElementUnderPointer;
			}
			else
			{
				visualElement = Panel.PickAll(this.visualTree, point, null);
			}
			return visualElement;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public override void ValidateLayout()
		{
			bool flag = !this.m_ValidatingLayout;
			if (flag)
			{
				this.m_ValidatingLayout = true;
				this.m_MarkerLayout.Begin();
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
				this.m_MarkerLayout.End();
				this.m_ValidatingLayout = false;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009C42 File Offset: 0x00007E42
		public override void UpdateAnimations()
		{
			this.m_MarkerAnimations.Begin();
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Animation);
			this.m_MarkerAnimations.End();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009C6A File Offset: 0x00007E6A
		public override void UpdateBindings()
		{
			this.m_MarkerBindings.Begin();
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Bindings);
			this.m_MarkerBindings.End();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009C92 File Offset: 0x00007E92
		public override void ApplyStyles()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009CA4 File Offset: 0x00007EA4
		private void UpdateForRepaint()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.ViewData);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Repaint);
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000299 RID: 665 RVA: 0x00009CF4 File Offset: 0x00007EF4
		// (remove) Token: 0x0600029A RID: 666 RVA: 0x00009D28 File Offset: 0x00007F28
		[field: DebuggerBrowsable(0)]
		internal static event Action<Panel> beforeAnyRepaint;

		// Token: 0x0600029B RID: 667 RVA: 0x00009D5C File Offset: 0x00007F5C
		public override void Repaint(Event e)
		{
			this.m_RepaintVersion = this.version;
			bool flag = this.contextType == ContextType.Editor;
			if (flag)
			{
				base.pixelsPerPoint = GUIUtility.pixelsPerPoint;
			}
			this.repaintData.repaintEvent = e;
			using (this.m_MarkerBeforeUpdate.Auto())
			{
				base.InvokeBeforeUpdate();
			}
			Action<Panel> action = Panel.beforeAnyRepaint;
			if (action != null)
			{
				action.Invoke(this);
			}
			using (this.m_MarkerUpdate.Auto())
			{
				this.UpdateForRepaint();
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009E18 File Offset: 0x00008018
		internal override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			this.m_Version += 1U;
			this.m_VisualTreeUpdater.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & VersionChangeType.Hierarchy) == VersionChangeType.Hierarchy;
			if (flag)
			{
				this.m_HierarchyVersion += 1U;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009E5A File Offset: 0x0000805A
		internal override void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase)
		{
			this.m_VisualTreeUpdater.SetUpdater(updater, phase);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009E6C File Offset: 0x0000806C
		internal override IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase)
		{
			return this.m_VisualTreeUpdater.GetUpdater(phase);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009E9B File Offset: 0x0000809B
		[CompilerGenerated]
		internal static Vector2Int <Pick>g__PixelOf|99_0(Vector2 p)
		{
			return Vector2Int.FloorToInt(p);
		}

		// Token: 0x0400011E RID: 286
		private VisualElement m_RootContainer;

		// Token: 0x0400011F RID: 287
		private VisualTreeUpdater m_VisualTreeUpdater;

		// Token: 0x04000120 RID: 288
		private IStylePropertyAnimationSystem m_StylePropertyAnimationSystem;

		// Token: 0x04000121 RID: 289
		private string m_PanelName;

		// Token: 0x04000122 RID: 290
		private uint m_Version = 0U;

		// Token: 0x04000123 RID: 291
		private uint m_RepaintVersion = 0U;

		// Token: 0x04000124 RID: 292
		private uint m_HierarchyVersion = 0U;

		// Token: 0x04000125 RID: 293
		private ProfilerMarker m_MarkerBeforeUpdate;

		// Token: 0x04000126 RID: 294
		private ProfilerMarker m_MarkerUpdate;

		// Token: 0x04000127 RID: 295
		private ProfilerMarker m_MarkerLayout;

		// Token: 0x04000128 RID: 296
		private ProfilerMarker m_MarkerBindings;

		// Token: 0x04000129 RID: 297
		private ProfilerMarker m_MarkerAnimations;

		// Token: 0x0400012A RID: 298
		private static ProfilerMarker s_MarkerPickAll = new ProfilerMarker("Panel.PickAll");

		// Token: 0x0400012C RID: 300
		private TimerEventScheduler m_Scheduler;

		// Token: 0x04000137 RID: 311
		private Shader m_StandardShader;

		// Token: 0x04000138 RID: 312
		private AtlasBase m_Atlas;

		// Token: 0x04000139 RID: 313
		private bool m_ValidatingLayout = false;
	}
}
