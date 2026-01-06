using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Unity.Profiling;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.UIR;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007F RID: 127
	public class VisualElement : Focusable, IStylePropertyAnimations, ITransform, ITransitionAnimations, IExperimentalFeatures, IVisualElementScheduler, IResolvedStyle
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000B95A File Offset: 0x00009B5A
		internal bool hasRunningAnimations
		{
			get
			{
				return this.styleAnimation.runningAnimationCount > 0;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000B96A File Offset: 0x00009B6A
		internal bool hasCompletedAnimations
		{
			get
			{
				return this.styleAnimation.completedAnimationCount > 0;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000B97A File Offset: 0x00009B7A
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000B982 File Offset: 0x00009B82
		int IStylePropertyAnimations.runningAnimationCount { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000B98B File Offset: 0x00009B8B
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000B993 File Offset: 0x00009B93
		int IStylePropertyAnimations.completedAnimationCount { get; set; }

		// Token: 0x0600033B RID: 827 RVA: 0x0000B99C File Offset: 0x00009B9C
		private IStylePropertyAnimationSystem GetStylePropertyAnimationSystem()
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			return (elementPanel != null) ? elementPanel.styleAnimationSystem : null;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		internal IStylePropertyAnimations styleAnimation
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000B9C4 File Offset: 0x00009BC4
		bool IStylePropertyAnimations.Start(StylePropertyId id, float from, float to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000B9EC File Offset: 0x00009BEC
		bool IStylePropertyAnimations.Start(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BA14 File Offset: 0x00009C14
		bool IStylePropertyAnimations.Start(StylePropertyId id, Length from, Length to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			bool flag = !this.TryConvertLengthUnits(id, ref from, ref to);
			return !flag && this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BA54 File Offset: 0x00009C54
		bool IStylePropertyAnimations.Start(StylePropertyId id, Color from, Color to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000BA7C File Offset: 0x00009C7C
		bool IStylePropertyAnimations.StartEnum(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		bool IStylePropertyAnimations.Start(StylePropertyId id, Background from, Background to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000BACC File Offset: 0x00009CCC
		bool IStylePropertyAnimations.Start(StylePropertyId id, FontDefinition from, FontDefinition to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		bool IStylePropertyAnimations.Start(StylePropertyId id, Font from, Font to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000BB1C File Offset: 0x00009D1C
		bool IStylePropertyAnimations.Start(StylePropertyId id, TextShadow from, TextShadow to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BB44 File Offset: 0x00009D44
		bool IStylePropertyAnimations.Start(StylePropertyId id, Scale from, Scale to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000BB6C File Offset: 0x00009D6C
		bool IStylePropertyAnimations.Start(StylePropertyId id, Translate from, Translate to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			bool flag = !this.TryConvertTranslateUnits(ref from, ref to);
			return !flag && this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		bool IStylePropertyAnimations.Start(StylePropertyId id, Rotate from, Rotate to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		bool IStylePropertyAnimations.Start(StylePropertyId id, TransformOrigin from, TransformOrigin to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			bool flag = !this.TryConvertTransformOriginUnits(ref from, ref to);
			return !flag && this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BC0C File Offset: 0x00009E0C
		void IStylePropertyAnimations.CancelAnimation(StylePropertyId id)
		{
			IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.GetStylePropertyAnimationSystem();
			if (stylePropertyAnimationSystem != null)
			{
				stylePropertyAnimationSystem.CancelAnimation(this, id);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BC24 File Offset: 0x00009E24
		void IStylePropertyAnimations.CancelAllAnimations()
		{
			bool flag = this.hasRunningAnimations || this.hasCompletedAnimations;
			if (flag)
			{
				IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.GetStylePropertyAnimationSystem();
				if (stylePropertyAnimationSystem != null)
				{
					stylePropertyAnimationSystem.CancelAllAnimations(this);
				}
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BC5C File Offset: 0x00009E5C
		bool IStylePropertyAnimations.HasRunningAnimation(StylePropertyId id)
		{
			return this.hasRunningAnimations && this.GetStylePropertyAnimationSystem().HasRunningAnimation(this, id);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BC86 File Offset: 0x00009E86
		void IStylePropertyAnimations.UpdateAnimation(StylePropertyId id)
		{
			this.GetStylePropertyAnimationSystem().UpdateAnimation(this, id);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BC98 File Offset: 0x00009E98
		void IStylePropertyAnimations.GetAllAnimations(List<StylePropertyId> outPropertyIds)
		{
			bool flag = this.hasRunningAnimations || this.hasCompletedAnimations;
			if (flag)
			{
				this.GetStylePropertyAnimationSystem().GetAllAnimations(this, outPropertyIds);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BCCC File Offset: 0x00009ECC
		private bool TryConvertLengthUnits(StylePropertyId id, ref Length from, ref Length to)
		{
			bool flag = from.IsAuto() || from.IsNone() || to.IsAuto() || to.IsNone();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = Mathf.Approximately(from.value, 0f);
				if (flag3)
				{
					from.unit = to.unit;
				}
				else
				{
					bool flag4 = from.unit != to.unit;
					if (flag4)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000BD44 File Offset: 0x00009F44
		private bool TryConvertTransformOriginUnits(ref TransformOrigin from, ref TransformOrigin to)
		{
			bool flag = from.x.unit != to.x.unit || from.y.unit != to.y.unit;
			return !flag;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		private bool TryConvertTranslateUnits(ref Translate from, ref Translate to)
		{
			bool flag = from.x.unit != to.x.unit || from.y.unit != to.y.unit;
			return !flag;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000BE01 File Offset: 0x0000A001
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000BE10 File Offset: 0x0000A010
		internal bool isCompositeRoot
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.CompositeRoot) == VisualElementFlags.CompositeRoot;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.CompositeRoot) : (this.m_Flags & ~VisualElementFlags.CompositeRoot));
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000BE2F File Offset: 0x0000A02F
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000BE44 File Offset: 0x0000A044
		internal bool isHierarchyDisplayed
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.HierarchyDisplayed) == VisualElementFlags.HierarchyDisplayed;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.HierarchyDisplayed) : (this.m_Flags & ~VisualElementFlags.HierarchyDisplayed));
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000BE6C File Offset: 0x0000A06C
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000BE84 File Offset: 0x0000A084
		public string viewDataKey
		{
			get
			{
				return this.m_ViewDataKey;
			}
			set
			{
				bool flag = this.m_ViewDataKey != value;
				if (flag)
				{
					this.m_ViewDataKey = value;
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.IncrementVersion(VersionChangeType.ViewData);
					}
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000BED5 File Offset: 0x0000A0D5
		internal bool enableViewDataPersistence
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.EnableViewDataPersistence) == VisualElementFlags.EnableViewDataPersistence;
			}
			private set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.EnableViewDataPersistence) : (this.m_Flags & ~VisualElementFlags.EnableViewDataPersistence));
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000BF1D File Offset: 0x0000A11D
		public object userData
		{
			get
			{
				object obj;
				this.TryGetPropertyInternal(VisualElement.userDataPropertyKey, out obj);
				return obj;
			}
			set
			{
				this.SetPropertyInternal(VisualElement.userDataPropertyKey, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000BF30 File Offset: 0x0000A130
		public override bool canGrabFocus
		{
			get
			{
				bool flag = false;
				for (VisualElement visualElement = this.hierarchy.parent; visualElement != null; visualElement = visualElement.parent)
				{
					bool isCompositeRoot = visualElement.isCompositeRoot;
					if (isCompositeRoot)
					{
						flag |= !visualElement.canGrabFocus;
						break;
					}
				}
				return !flag && this.visible && this.resolvedStyle.display != DisplayStyle.None && this.enabledInHierarchy && base.canGrabFocus;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		public override FocusController focusController
		{
			get
			{
				IPanel panel = this.panel;
				return (panel != null) ? panel.focusController : null;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000C024 File Offset: 0x0000A224
		public UsageHints usageHints
		{
			get
			{
				return (((this.renderHints & RenderHints.GroupTransform) != RenderHints.None) ? UsageHints.GroupTransform : UsageHints.None) | (((this.renderHints & RenderHints.BoneTransform) != RenderHints.None) ? UsageHints.DynamicTransform : UsageHints.None) | (((this.renderHints & RenderHints.MaskContainer) != RenderHints.None) ? UsageHints.MaskContainer : UsageHints.None) | (((this.renderHints & RenderHints.DynamicColor) != RenderHints.None) ? UsageHints.DynamicColor : UsageHints.None);
			}
			set
			{
				bool flag = (value & UsageHints.GroupTransform) > UsageHints.None;
				if (flag)
				{
					this.renderHints |= RenderHints.GroupTransform;
				}
				else
				{
					this.renderHints &= ~RenderHints.GroupTransform;
				}
				bool flag2 = (value & UsageHints.DynamicTransform) > UsageHints.None;
				if (flag2)
				{
					this.renderHints |= RenderHints.BoneTransform;
				}
				else
				{
					this.renderHints &= ~RenderHints.BoneTransform;
				}
				bool flag3 = (value & UsageHints.MaskContainer) > UsageHints.None;
				if (flag3)
				{
					this.renderHints |= RenderHints.MaskContainer;
				}
				else
				{
					this.renderHints &= ~RenderHints.MaskContainer;
				}
				bool flag4 = (value & UsageHints.DynamicColor) > UsageHints.None;
				if (flag4)
				{
					this.renderHints |= RenderHints.DynamicColor;
				}
				else
				{
					this.renderHints &= ~RenderHints.DynamicColor;
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		internal RenderHints renderHints
		{
			get
			{
				return this.m_RenderHints;
			}
			set
			{
				RenderHints renderHints = this.m_RenderHints & ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer);
				RenderHints renderHints2 = value & ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer);
				RenderHints renderHints3 = renderHints ^ renderHints2;
				bool flag = renderHints3 > RenderHints.None;
				if (flag)
				{
					RenderHints renderHints4 = this.m_RenderHints & RenderHints.DirtyAll;
					RenderHints renderHints5 = renderHints3 << 5;
					this.m_RenderHints = renderHints2 | renderHints4 | renderHints5;
					this.IncrementVersion(VersionChangeType.RenderHints);
				}
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C155 File Offset: 0x0000A355
		internal void MarkRenderHintsClean()
		{
			this.m_RenderHints &= ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000C16C File Offset: 0x0000A36C
		public ITransform transform
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000C180 File Offset: 0x0000A380
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000C19D File Offset: 0x0000A39D
		Vector3 ITransform.position
		{
			get
			{
				return this.resolvedStyle.translate;
			}
			set
			{
				this.style.translate = new Translate(value.x, value.y, value.z);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		Quaternion ITransform.rotation
		{
			get
			{
				return this.resolvedStyle.rotate.ToQuaternion();
			}
			set
			{
				float num;
				Vector3 vector;
				value.ToAngleAxis(out num, out vector);
				this.style.rotate = new Rotate(num, vector);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000C234 File Offset: 0x0000A434
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000C259 File Offset: 0x0000A459
		Vector3 ITransform.scale
		{
			get
			{
				return this.resolvedStyle.scale.value;
			}
			set
			{
				this.style.scale = new Scale(value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000C280 File Offset: 0x0000A480
		Matrix4x4 ITransform.matrix
		{
			get
			{
				return Matrix4x4.TRS(this.resolvedStyle.translate, this.resolvedStyle.rotate.ToQuaternion(), this.resolvedStyle.scale.value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000C2D7 File Offset: 0x0000A4D7
		internal bool isLayoutManual
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.LayoutManual) == VisualElementFlags.LayoutManual;
			}
			private set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.LayoutManual) : (this.m_Flags & ~VisualElementFlags.LayoutManual));
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000C2F6 File Offset: 0x0000A4F6
		internal float scaledPixelsPerPoint
		{
			get
			{
				BaseVisualElementPanel elementPanel = this.elementPanel;
				return (elementPanel != null) ? elementPanel.scaledPixelsPerPoint : GUIUtility.pixelsPerPoint;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000C310 File Offset: 0x0000A510
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000C390 File Offset: 0x0000A590
		public Rect layout
		{
			get
			{
				Rect layout = this.m_Layout;
				bool flag = this.yogaNode != null && !this.isLayoutManual;
				if (flag)
				{
					layout.x = this.yogaNode.LayoutX;
					layout.y = this.yogaNode.LayoutY;
					layout.width = this.yogaNode.LayoutWidth;
					layout.height = this.yogaNode.LayoutHeight;
				}
				return layout;
			}
			internal set
			{
				bool flag = this.yogaNode == null;
				if (flag)
				{
					this.yogaNode = new YogaNode(null);
				}
				bool flag2 = this.isLayoutManual && this.m_Layout == value;
				if (!flag2)
				{
					Rect layout = this.layout;
					VersionChangeType versionChangeType = (VersionChangeType)0;
					bool flag3 = !Mathf.Approximately(layout.x, value.x) || !Mathf.Approximately(layout.y, value.y);
					if (flag3)
					{
						versionChangeType |= VersionChangeType.Transform;
					}
					bool flag4 = !Mathf.Approximately(layout.width, value.width) || !Mathf.Approximately(layout.height, value.height);
					if (flag4)
					{
						versionChangeType |= VersionChangeType.Size;
					}
					this.m_Layout = value;
					this.isLayoutManual = true;
					IStyle style = this.style;
					style.position = Position.Absolute;
					style.marginLeft = 0f;
					style.marginRight = 0f;
					style.marginBottom = 0f;
					style.marginTop = 0f;
					style.left = value.x;
					style.top = value.y;
					style.right = float.NaN;
					style.bottom = float.NaN;
					style.width = value.width;
					style.height = value.height;
					bool flag5 = versionChangeType > (VersionChangeType)0;
					if (flag5)
					{
						this.IncrementVersion(versionChangeType);
					}
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000C544 File Offset: 0x0000A744
		public Rect contentRect
		{
			get
			{
				Spacing spacing = new Spacing(this.resolvedStyle.paddingLeft, this.resolvedStyle.paddingTop, this.resolvedStyle.paddingRight, this.resolvedStyle.paddingBottom);
				return this.paddingRect - spacing;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000C598 File Offset: 0x0000A798
		protected Rect paddingRect
		{
			get
			{
				Spacing spacing = new Spacing(this.resolvedStyle.borderLeftWidth, this.resolvedStyle.borderTopWidth, this.resolvedStyle.borderRightWidth, this.resolvedStyle.borderBottomWidth);
				return this.rect - spacing;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000C5E9 File Offset: 0x0000A7E9
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000C5F6 File Offset: 0x0000A7F6
		internal bool isBoundingBoxDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.BoundingBoxDirty) == VisualElementFlags.BoundingBoxDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.BoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.BoundingBoxDirty));
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000C614 File Offset: 0x0000A814
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000C623 File Offset: 0x0000A823
		internal bool isWorldBoundingBoxDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldBoundingBoxDirty) == VisualElementFlags.WorldBoundingBoxDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldBoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.WorldBoundingBoxDirty));
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000C644 File Offset: 0x0000A844
		internal Rect boundingBox
		{
			get
			{
				bool isBoundingBoxDirty = this.isBoundingBoxDirty;
				if (isBoundingBoxDirty)
				{
					this.UpdateBoundingBox();
					this.isBoundingBoxDirty = false;
				}
				return this.m_BoundingBox;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000C678 File Offset: 0x0000A878
		internal Rect worldBoundingBox
		{
			get
			{
				bool flag = this.isWorldBoundingBoxDirty || this.isBoundingBoxDirty;
				if (flag)
				{
					this.UpdateWorldBoundingBox();
					this.isWorldBoundingBoxDirty = false;
				}
				return this.m_WorldBoundingBox;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		private Rect boundingBoxInParentSpace
		{
			get
			{
				Rect boundingBox = this.boundingBox;
				this.TransformAlignedRectToParentSpace(ref boundingBox);
				return boundingBox;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		internal void UpdateBoundingBox()
		{
			bool flag = float.IsNaN(this.rect.x) || float.IsNaN(this.rect.y) || float.IsNaN(this.rect.width) || float.IsNaN(this.rect.height);
			if (flag)
			{
				this.m_BoundingBox = Rect.zero;
			}
			else
			{
				this.m_BoundingBox = this.rect;
				bool flag2 = !this.ShouldClip();
				if (flag2)
				{
					int count = this.m_Children.Count;
					for (int i = 0; i < count; i++)
					{
						Rect boundingBoxInParentSpace = this.m_Children[i].boundingBoxInParentSpace;
						this.m_BoundingBox.xMin = Math.Min(this.m_BoundingBox.xMin, boundingBoxInParentSpace.xMin);
						this.m_BoundingBox.xMax = Math.Max(this.m_BoundingBox.xMax, boundingBoxInParentSpace.xMax);
						this.m_BoundingBox.yMin = Math.Min(this.m_BoundingBox.yMin, boundingBoxInParentSpace.yMin);
						this.m_BoundingBox.yMax = Math.Max(this.m_BoundingBox.yMax, boundingBoxInParentSpace.yMax);
					}
				}
			}
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000C847 File Offset: 0x0000AA47
		internal void UpdateWorldBoundingBox()
		{
			this.m_WorldBoundingBox = this.boundingBox;
			VisualElement.TransformAlignedRect(this.worldTransformRef, ref this.m_WorldBoundingBox);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000C868 File Offset: 0x0000AA68
		public Rect worldBound
		{
			get
			{
				Rect rect = this.rect;
				VisualElement.TransformAlignedRect(this.worldTransformRef, ref rect);
				return rect;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000C890 File Offset: 0x0000AA90
		public Rect localBound
		{
			get
			{
				Rect rect = this.rect;
				this.TransformAlignedRectToParentSpace(ref rect);
				return rect;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		internal Rect rect
		{
			get
			{
				Rect layout = this.layout;
				return new Rect(0f, 0f, layout.width, layout.height);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000C8EA File Offset: 0x0000AAEA
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000C8F7 File Offset: 0x0000AAF7
		internal bool isWorldTransformDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldTransformDirty) == VisualElementFlags.WorldTransformDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformDirty));
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000C915 File Offset: 0x0000AB15
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000C922 File Offset: 0x0000AB22
		internal bool isWorldTransformInverseDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldTransformInverseDirty) == VisualElementFlags.WorldTransformInverseDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformInverseDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformInverseDirty));
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000C940 File Offset: 0x0000AB40
		public Matrix4x4 worldTransform
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return this.m_WorldTransformCache;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000C96C File Offset: 0x0000AB6C
		internal ref Matrix4x4 worldTransformRef
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return ref this.m_WorldTransformCache;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000C998 File Offset: 0x0000AB98
		internal ref Matrix4x4 worldTransformInverse
		{
			get
			{
				bool flag = this.isWorldTransformDirty || this.isWorldTransformInverseDirty;
				if (flag)
				{
					this.UpdateWorldTransformInverse();
				}
				return ref this.m_WorldTransformInverseCache;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		internal void UpdateWorldTransform()
		{
			bool flag = this.elementPanel != null && !this.elementPanel.duringLayoutPhase;
			if (flag)
			{
				this.isWorldTransformDirty = false;
			}
			bool flag2 = this.hierarchy.parent != null;
			if (flag2)
			{
				bool hasDefaultRotationAndScale = this.hasDefaultRotationAndScale;
				if (hasDefaultRotationAndScale)
				{
					VisualElement.TranslateMatrix34(this.hierarchy.parent.worldTransformRef, this.positionWithLayout, out this.m_WorldTransformCache);
				}
				else
				{
					Matrix4x4 matrix4x;
					this.GetPivotedMatrixWithLayout(out matrix4x);
					VisualElement.MultiplyMatrix34(this.hierarchy.parent.worldTransformRef, ref matrix4x, out this.m_WorldTransformCache);
				}
			}
			else
			{
				this.GetPivotedMatrixWithLayout(out this.m_WorldTransformCache);
			}
			this.isWorldTransformInverseDirty = true;
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000CA98 File Offset: 0x0000AC98
		internal void UpdateWorldTransformInverse()
		{
			Matrix4x4.Inverse3DAffine(this.worldTransform, ref this.m_WorldTransformInverseCache);
			this.isWorldTransformInverseDirty = false;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000CAB5 File Offset: 0x0000ACB5
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000CAC2 File Offset: 0x0000ACC2
		internal bool isWorldClipDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldClipDirty) == VisualElementFlags.WorldClipDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldClipDirty) : (this.m_Flags & ~VisualElementFlags.WorldClipDirty));
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		internal Rect worldClip
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClip;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000CB14 File Offset: 0x0000AD14
		internal Rect worldClipMinusGroup
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipMinusGroup;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000CB48 File Offset: 0x0000AD48
		internal bool worldClipIsInfinite
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipIsInfinite;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		internal void EnsureWorldTransformAndClipUpToDate()
		{
			bool isWorldTransformDirty = this.isWorldTransformDirty;
			if (isWorldTransformDirty)
			{
				this.UpdateWorldTransform();
			}
			bool isWorldClipDirty = this.isWorldClipDirty;
			if (isWorldClipDirty)
			{
				this.UpdateWorldClip();
				this.isWorldClipDirty = false;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		private void UpdateWorldClip()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.m_WorldClip = this.hierarchy.parent.worldClip;
				bool flag2 = this.hierarchy.parent.worldClipIsInfinite;
				bool flag3 = this.hierarchy.parent != this.renderChainData.groupTransformAncestor;
				if (flag3)
				{
					this.m_WorldClipMinusGroup = this.hierarchy.parent.worldClipMinusGroup;
				}
				else
				{
					flag2 = true;
					this.m_WorldClipMinusGroup = VisualElement.s_InfiniteRect;
				}
				bool flag4 = this.ShouldClip();
				if (flag4)
				{
					Rect rect = this.SubstractBorderPadding(this.worldBound);
					this.m_WorldClip = this.CombineClipRects(rect, this.m_WorldClip);
					this.m_WorldClipMinusGroup = (flag2 ? rect : this.CombineClipRects(rect, this.m_WorldClipMinusGroup));
					this.m_WorldClipIsInfinite = false;
				}
				else
				{
					this.m_WorldClipIsInfinite = flag2;
				}
			}
			else
			{
				this.m_WorldClipMinusGroup = (this.m_WorldClip = ((this.panel != null) ? this.panel.visualTree.rect : VisualElement.s_InfiniteRect));
				this.m_WorldClipIsInfinite = true;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		private Rect CombineClipRects(Rect rect, Rect parentRect)
		{
			float num = Mathf.Max(rect.xMin, parentRect.xMin);
			float num2 = Mathf.Min(rect.xMax, parentRect.xMax);
			float num3 = Mathf.Max(rect.yMin, parentRect.yMin);
			float num4 = Mathf.Min(rect.yMax, parentRect.yMax);
			float num5 = Mathf.Max(num2 - num, 0f);
			float num6 = Mathf.Max(num4 - num3, 0f);
			return new Rect(num, num3, num5, num6);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000CD84 File Offset: 0x0000AF84
		private Rect SubstractBorderPadding(Rect worldRect)
		{
			float m = this.worldTransform.m00;
			float m2 = this.worldTransform.m11;
			worldRect.x += this.resolvedStyle.borderLeftWidth * m;
			worldRect.y += this.resolvedStyle.borderTopWidth * m2;
			worldRect.width -= (this.resolvedStyle.borderLeftWidth + this.resolvedStyle.borderRightWidth) * m;
			worldRect.height -= (this.resolvedStyle.borderTopWidth + this.resolvedStyle.borderBottomWidth) * m2;
			bool flag = this.computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
			if (flag)
			{
				worldRect.x += this.resolvedStyle.paddingLeft * m;
				worldRect.y += this.resolvedStyle.paddingTop * m2;
				worldRect.width -= (this.resolvedStyle.paddingLeft + this.resolvedStyle.paddingRight) * m;
				worldRect.height -= (this.resolvedStyle.paddingTop + this.resolvedStyle.paddingBottom) * m2;
			}
			return worldRect;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		internal static Rect ComputeAAAlignedBound(Rect position, Matrix4x4 mat)
		{
			Rect rect = position;
			Vector3 vector = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y, 0f));
			Vector3 vector2 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y, 0f));
			Vector3 vector3 = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y + rect.height, 0f));
			Vector3 vector4 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y + rect.height, 0f));
			return Rect.MinMaxRect(Mathf.Min(vector.x, Mathf.Min(vector2.x, Mathf.Min(vector3.x, vector4.x))), Mathf.Min(vector.y, Mathf.Min(vector2.y, Mathf.Min(vector3.y, vector4.y))), Mathf.Max(vector.x, Mathf.Max(vector2.x, Mathf.Max(vector3.x, vector4.x))), Mathf.Max(vector.y, Mathf.Max(vector2.y, Mathf.Max(vector3.y, vector4.y))));
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000D034 File Offset: 0x0000B234
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000D04C File Offset: 0x0000B24C
		internal PseudoStates pseudoStates
		{
			get
			{
				return this.m_PseudoStates;
			}
			set
			{
				PseudoStates pseudoStates = this.m_PseudoStates ^ value;
				bool flag = pseudoStates > (PseudoStates)0;
				if (flag)
				{
					bool flag2 = (value & PseudoStates.Root) == PseudoStates.Root;
					if (flag2)
					{
						this.isRootVisualContainer = true;
					}
					bool flag3 = pseudoStates != PseudoStates.Root;
					if (flag3)
					{
						PseudoStates pseudoStates2 = pseudoStates & value;
						PseudoStates pseudoStates3 = pseudoStates & this.m_PseudoStates;
						bool flag4 = (this.triggerPseudoMask & pseudoStates2) != (PseudoStates)0 || (this.dependencyPseudoMask & pseudoStates3) > (PseudoStates)0;
						if (flag4)
						{
							this.IncrementVersion(VersionChangeType.StyleSheet);
						}
					}
					this.m_PseudoStates = value;
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000D0D9 File Offset: 0x0000B2D9
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000D0E1 File Offset: 0x0000B2E1
		internal int containedPointerIds { get; private set; }

		// Token: 0x06000395 RID: 917 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		private void UpdateHoverPseudoState()
		{
			bool flag = this.containedPointerIds == 0;
			if (flag)
			{
				this.pseudoStates &= ~PseudoStates.Hover;
			}
			else
			{
				bool flag2 = false;
				for (int i = 0; i < PointerId.maxPointers; i++)
				{
					bool flag3 = (this.containedPointerIds & (1 << i)) != 0;
					if (flag3)
					{
						IPanel panel = this.panel;
						IEventHandler eventHandler = ((panel != null) ? panel.GetCapturingElement(i) : null);
						bool flag4 = eventHandler == null || eventHandler == this;
						if (flag4)
						{
							flag2 = true;
							break;
						}
					}
				}
				bool flag5 = flag2;
				if (flag5)
				{
					this.pseudoStates |= PseudoStates.Hover;
				}
				else
				{
					this.pseudoStates &= ~PseudoStates.Hover;
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000D1A1 File Offset: 0x0000B3A1
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000D1A9 File Offset: 0x0000B3A9
		public PickingMode pickingMode { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000D1CC File Offset: 0x0000B3CC
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = this.m_Name == value;
				if (!flag)
				{
					this.m_Name = value;
					this.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		internal List<string> classList
		{
			get
			{
				bool flag = this.m_ClassList == VisualElement.s_EmptyClassList;
				if (flag)
				{
					this.m_ClassList = ObjectListPool<string>.Get();
				}
				return this.m_ClassList;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000D232 File Offset: 0x0000B432
		internal string fullTypeName
		{
			get
			{
				return this.typeData.fullTypeName;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000D23F File Offset: 0x0000B43F
		internal string typeName
		{
			get
			{
				return this.typeData.typeName;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000D24C File Offset: 0x0000B44C
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000D254 File Offset: 0x0000B454
		internal YogaNode yogaNode { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000D25D File Offset: 0x0000B45D
		internal ref ComputedStyle computedStyle
		{
			get
			{
				return ref this.m_Style;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000D265 File Offset: 0x0000B465
		internal bool hasInlineStyle
		{
			get
			{
				return this.inlineStyleAccess != null;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000D270 File Offset: 0x0000B470
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000D285 File Offset: 0x0000B485
		internal bool styleInitialized
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.StyleInitialized) == VisualElementFlags.StyleInitialized;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.StyleInitialized) : (this.m_Flags & ~VisualElementFlags.StyleInitialized));
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000D2C9 File Offset: 0x0000B4C9
		internal float opacity
		{
			get
			{
				return this.resolvedStyle.opacity;
			}
			set
			{
				this.style.opacity = value;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		private void ChangeIMGUIContainerCount(int delta)
		{
			for (VisualElement visualElement = this; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				visualElement.imguiContainerDescendantCount += delta;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000D31C File Offset: 0x0000B51C
		public VisualElement()
		{
			UIElementsRuntimeUtilityNative.VisualElementCreation();
			this.m_Children = VisualElement.s_EmptyList;
			this.controlid = (VisualElement.s_NextId += 1U);
			this.hierarchy = new VisualElement.Hierarchy(this);
			this.m_ClassList = VisualElement.s_EmptyClassList;
			this.m_Flags = VisualElementFlags.Init;
			this.SetEnabled(true);
			base.focusable = false;
			this.name = string.Empty;
			this.yogaNode = new YogaNode(null);
			this.renderHints = RenderHints.None;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000D420 File Offset: 0x0000B620
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<MouseOverEvent>.TypeId() || evt.eventTypeId == EventBase<MouseOutEvent>.TypeId();
				if (flag2)
				{
					this.UpdateCursorStyle(evt.eventTypeId);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<PointerEnterEvent>.TypeId();
					if (flag3)
					{
						this.containedPointerIds |= 1 << ((IPointerEvent)evt).pointerId;
						this.UpdateHoverPseudoState();
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<PointerLeaveEvent>.TypeId();
						if (flag4)
						{
							this.containedPointerIds &= ~(1 << ((IPointerEvent)evt).pointerId);
							this.UpdateHoverPseudoState();
						}
						else
						{
							bool flag5 = evt.eventTypeId == EventBase<PointerCaptureEvent>.TypeId() || evt.eventTypeId == EventBase<PointerCaptureOutEvent>.TypeId();
							if (flag5)
							{
								this.UpdateHoverPseudoState();
								BaseVisualElementPanel elementPanel = this.elementPanel;
								VisualElement visualElement = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(((IPointerCaptureEventInternal)evt).pointerId) : null);
								VisualElement visualElement2 = visualElement;
								while (visualElement2 != null && visualElement2 != this)
								{
									visualElement2.UpdateHoverPseudoState();
									visualElement2 = visualElement2.parent;
								}
							}
							else
							{
								bool flag6 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
								if (flag6)
								{
									this.pseudoStates &= ~PseudoStates.Focus;
								}
								else
								{
									bool flag7 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
									if (flag7)
									{
										this.pseudoStates |= PseudoStates.Focus;
									}
									else
									{
										bool flag8 = evt.eventTypeId == EventBase<TooltipEvent>.TypeId();
										if (flag8)
										{
											this.SetTooltip((TooltipEvent)evt);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		internal virtual Rect GetTooltipRect()
		{
			return this.worldBound;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		private void SetTooltip(TooltipEvent e)
		{
			VisualElement visualElement = e.currentTarget as VisualElement;
			bool flag = visualElement != null && !string.IsNullOrEmpty(visualElement.tooltip);
			if (flag)
			{
				e.rect = visualElement.GetTooltipRect();
				e.tooltip = visualElement.tooltip;
				e.StopImmediatePropagation();
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000D650 File Offset: 0x0000B850
		public sealed override void Focus()
		{
			bool flag = !this.canGrabFocus && this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.Focus();
			}
			else
			{
				base.Focus();
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		internal void SetPanel(BaseVisualElementPanel p)
		{
			bool flag = this.panel == p;
			if (!flag)
			{
				List<VisualElement> list = VisualElementListPool.Get(0);
				try
				{
					list.Add(this);
					this.GatherAllChildren(list);
					EventDispatcherGate? eventDispatcherGate = default(EventDispatcherGate?);
					bool flag2 = ((p != null) ? p.dispatcher : null) != null;
					if (flag2)
					{
						eventDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(p.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate2 = default(EventDispatcherGate?);
					IPanel panel = this.panel;
					bool flag3 = ((panel != null) ? panel.dispatcher : null) != null && this.panel.dispatcher != ((p != null) ? p.dispatcher : null);
					if (flag3)
					{
						eventDispatcherGate2 = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					BaseVisualElementPanel elementPanel = this.elementPanel;
					uint num = ((elementPanel != null) ? elementPanel.hierarchyVersion : 0U);
					EventDispatcherGate? eventDispatcherGate3 = eventDispatcherGate;
					try
					{
						EventDispatcherGate? eventDispatcherGate4 = eventDispatcherGate2;
						try
						{
							foreach (VisualElement visualElement in list)
							{
								visualElement.WillChangePanel(p);
							}
							uint num2 = ((elementPanel != null) ? elementPanel.hierarchyVersion : 0U);
							bool flag4 = num != num2;
							if (flag4)
							{
								list.Clear();
								list.Add(this);
								this.GatherAllChildren(list);
							}
							VisualElementFlags visualElementFlags = ((p != null) ? VisualElementFlags.NeedsAttachToPanelEvent : ((VisualElementFlags)0));
							foreach (VisualElement visualElement2 in list)
							{
								visualElement2.elementPanel = p;
								visualElement2.m_Flags |= visualElementFlags;
							}
							foreach (VisualElement visualElement3 in list)
							{
								visualElement3.HasChangedPanel(elementPanel);
							}
						}
						finally
						{
							if (eventDispatcherGate4 != null)
							{
								eventDispatcherGate4.GetValueOrDefault().Dispose();
							}
						}
					}
					finally
					{
						if (eventDispatcherGate3 != null)
						{
							eventDispatcherGate3.GetValueOrDefault().Dispose();
						}
					}
				}
				finally
				{
					VisualElementListPool.Release(list);
				}
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000D96C File Offset: 0x0000BB6C
		private void WillChangePanel(BaseVisualElementPanel destinationPanel)
		{
			bool flag = this.panel != null;
			if (flag)
			{
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == (VisualElementFlags)0;
				if (flag2)
				{
					using (DetachFromPanelEvent pooled = PanelChangedEventBase<DetachFromPanelEvent>.GetPooled(this.panel, destinationPanel))
					{
						pooled.target = this;
						this.elementPanel.SendEvent(pooled, DispatchMode.Immediate);
					}
					this.panel.dispatcher.m_ClickDetector.Cleanup(this);
				}
				this.UnregisterRunningAnimations();
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000DA00 File Offset: 0x0000BC00
		private void HasChangedPanel(BaseVisualElementPanel prevPanel)
		{
			bool flag = this.panel != null;
			if (flag)
			{
				this.yogaNode.Config = this.elementPanel.yogaConfig;
				this.RegisterRunningAnimations();
				this.pseudoStates &= ~(PseudoStates.Active | PseudoStates.Hover | PseudoStates.Focus);
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == VisualElementFlags.NeedsAttachToPanelEvent;
				if (flag2)
				{
					using (AttachToPanelEvent pooled = PanelChangedEventBase<AttachToPanelEvent>.GetPooled(prevPanel, this.panel))
					{
						pooled.target = this;
						this.elementPanel.SendEvent(pooled, DispatchMode.Immediate);
					}
					this.m_Flags &= ~VisualElementFlags.NeedsAttachToPanelEvent;
				}
			}
			else
			{
				this.yogaNode.Config = YogaConfig.Default;
			}
			this.styleInitialized = false;
			this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Transform);
			bool flag3 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag3)
			{
				this.IncrementVersion(VersionChangeType.ViewData);
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000DB00 File Offset: 0x0000BD00
		public sealed override void SendEvent(EventBase e)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.SendEvent(e, DispatchMode.Default);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000DB17 File Offset: 0x0000BD17
		internal sealed override void SendEvent(EventBase e, DispatchMode dispatchMode)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.SendEvent(e, dispatchMode);
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000DB2E File Offset: 0x0000BD2E
		internal void IncrementVersion(VersionChangeType changeType)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.OnVersionChanged(this, changeType);
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000DB45 File Offset: 0x0000BD45
		internal void InvokeHierarchyChanged(HierarchyChangeType changeType)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.InvokeHierarchyChanged(this, changeType);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		[Obsolete("SetEnabledFromHierarchy is deprecated and will be removed in a future release. Please use SetEnabled instead.")]
		protected internal bool SetEnabledFromHierarchy(bool state)
		{
			return this.SetEnabledFromHierarchyPrivate(state);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000DB78 File Offset: 0x0000BD78
		private bool SetEnabledFromHierarchyPrivate(bool state)
		{
			bool enabledInHierarchy = this.enabledInHierarchy;
			bool flag = false;
			if (state)
			{
				bool isParentEnabledInHierarchy = this.isParentEnabledInHierarchy;
				if (isParentEnabledInHierarchy)
				{
					bool enabledSelf = this.enabledSelf;
					if (enabledSelf)
					{
						this.RemoveFromClassList(VisualElement.disabledUssClassName);
					}
					else
					{
						flag = true;
						this.AddToClassList(VisualElement.disabledUssClassName);
					}
				}
				else
				{
					flag = true;
					this.RemoveFromClassList(VisualElement.disabledUssClassName);
				}
			}
			else
			{
				flag = true;
				this.EnableInClassList(VisualElement.disabledUssClassName, this.isParentEnabledInHierarchy);
			}
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this.focusController != null && this.focusController.IsFocused(this);
				if (flag3)
				{
					EventDispatcherGate? eventDispatcherGate = default(EventDispatcherGate?);
					IPanel panel = this.panel;
					bool flag4 = ((panel != null) ? panel.dispatcher : null) != null;
					if (flag4)
					{
						eventDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate2 = eventDispatcherGate;
					try
					{
						base.BlurImmediately();
					}
					finally
					{
						if (eventDispatcherGate2 != null)
						{
							eventDispatcherGate2.GetValueOrDefault().Dispose();
						}
					}
				}
				this.pseudoStates |= PseudoStates.Disabled;
			}
			else
			{
				this.pseudoStates &= ~PseudoStates.Disabled;
			}
			return enabledInHierarchy != this.enabledInHierarchy;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		private bool isParentEnabledInHierarchy
		{
			get
			{
				return this.hierarchy.parent == null || this.hierarchy.parent.enabledInHierarchy;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000DD10 File Offset: 0x0000BF10
		public bool enabledInHierarchy
		{
			get
			{
				return (this.pseudoStates & PseudoStates.Disabled) != PseudoStates.Disabled;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000DD32 File Offset: 0x0000BF32
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000DD3A File Offset: 0x0000BF3A
		public bool enabledSelf { get; private set; }

		// Token: 0x060003B8 RID: 952 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public void SetEnabled(bool value)
		{
			bool flag = this.enabledSelf == value;
			if (!flag)
			{
				this.enabledSelf = value;
				this.PropagateEnabledToChildren(value);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000DD74 File Offset: 0x0000BF74
		private void PropagateEnabledToChildren(bool value)
		{
			bool flag = this.SetEnabledFromHierarchyPrivate(value);
			if (flag)
			{
				int count = this.m_Children.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_Children[i].PropagateEnabledToChildren(value);
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		public bool visible
		{
			get
			{
				return this.resolvedStyle.visibility == Visibility.Visible;
			}
			set
			{
				this.style.visibility = (value ? Visibility.Visible : Visibility.Hidden);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000DDFB File Offset: 0x0000BFFB
		public void MarkDirtyRepaint()
		{
			this.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000DE0A File Offset: 0x0000C00A
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000DE12 File Offset: 0x0000C012
		public Action<MeshGenerationContext> generateVisualContent { get; set; }

		// Token: 0x060003BF RID: 959 RVA: 0x0000DE1C File Offset: 0x0000C01C
		internal void InvokeGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.generateVisualContent != null;
			if (flag)
			{
				try
				{
					using (this.k_GenerateVisualContentMarker.Auto())
					{
						this.generateVisualContent.Invoke(mgc);
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000DE90 File Offset: 0x0000C090
		internal void GetFullHierarchicalViewDataKey(StringBuilder key)
		{
			bool flag = this.parent != null;
			if (flag)
			{
				this.parent.GetFullHierarchicalViewDataKey(key);
			}
			bool flag2 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag2)
			{
				key.Append("__");
				key.Append(this.viewDataKey);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		internal string GetFullHierarchicalViewDataKey()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.GetFullHierarchicalViewDataKey(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000DF10 File Offset: 0x0000C110
		internal T GetOrCreateViewData<T>(object existing, string key) where T : class, new()
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load persistent data.");
			ISerializableJsonDictionary serializableJsonDictionary = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T t;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					t = existing as T;
				}
				else
				{
					t = new T();
				}
			}
			else
			{
				string text = "__";
				Type typeFromHandle = typeof(T);
				string text2 = key + text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(text2);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(text2, new T());
				}
				t = serializableJsonDictionary.Get<T>(text2);
			}
			return t;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		internal T GetOrCreateViewData<T>(ScriptableObject existing, string key) where T : ScriptableObject
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T t;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					t = existing as T;
				}
				else
				{
					t = ScriptableObject.CreateInstance<T>();
				}
			}
			else
			{
				string text = "__";
				Type typeFromHandle = typeof(T);
				string text2 = key + text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(text2);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(text2, ScriptableObject.CreateInstance<T>());
				}
				t = serializableJsonDictionary.GetScriptable<T>(text2);
			}
			return t;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		internal void OverwriteFromViewData(object obj, string key)
		{
			bool flag = obj == null;
			if (flag)
			{
				throw new ArgumentNullException("obj");
			}
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag2 = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			if (!flag2)
			{
				string text = "__";
				Type type = obj.GetType();
				string text2 = key + text + ((type != null) ? type.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(text2);
				if (flag3)
				{
					serializableJsonDictionary.Set<object>(text2, obj);
				}
				else
				{
					serializableJsonDictionary.Overwrite(obj, text2);
				}
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E18C File Offset: 0x0000C38C
		internal void SaveViewData()
		{
			bool flag = this.elementPanel != null && this.elementPanel.saveViewData != null && !string.IsNullOrEmpty(this.viewDataKey) && this.enableViewDataPersistence;
			if (flag)
			{
				this.elementPanel.saveViewData();
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		internal bool IsViewDataPersitenceSupportedOnChildren(bool existingState)
		{
			bool flag = existingState;
			bool flag2 = string.IsNullOrEmpty(this.viewDataKey) && this != this.contentContainer;
			if (flag2)
			{
				flag = false;
			}
			bool flag3 = this.parent != null && this == this.parent.contentContainer;
			if (flag3)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E232 File Offset: 0x0000C432
		internal void OnViewDataReady(bool enablePersistence)
		{
			this.enableViewDataPersistence = enablePersistence;
			this.OnViewDataReady();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000020E6 File Offset: 0x000002E6
		internal virtual void OnViewDataReady()
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E244 File Offset: 0x0000C444
		public virtual bool ContainsPoint(Vector2 localPoint)
		{
			return this.rect.Contains(localPoint);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E268 File Offset: 0x0000C468
		public virtual bool Overlaps(Rect rectangle)
		{
			return this.rect.Overlaps(rectangle, true);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000E28A File Offset: 0x0000C48A
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		internal bool requireMeasureFunction
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.RequireMeasureFunction) == VisualElementFlags.RequireMeasureFunction;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.RequireMeasureFunction) : (this.m_Flags & ~VisualElementFlags.RequireMeasureFunction));
				bool flag = value && !this.yogaNode.IsMeasureDefined;
				if (flag)
				{
					this.AssignMeasureFunction();
				}
				else
				{
					bool flag2 = !value && this.yogaNode.IsMeasureDefined;
					if (flag2)
					{
						this.RemoveMeasureFunction();
					}
				}
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E312 File Offset: 0x0000C512
		private void AssignMeasureFunction()
		{
			this.yogaNode.SetMeasureFunction((YogaNode node, float f, YogaMeasureMode mode, float f1, YogaMeasureMode heightMode) => this.Measure(node, f, mode, f1, heightMode));
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000E32D File Offset: 0x0000C52D
		private void RemoveMeasureFunction()
		{
			this.yogaNode.SetMeasureFunction(null);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000E340 File Offset: 0x0000C540
		protected internal virtual Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			return new Vector2(float.NaN, float.NaN);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000E364 File Offset: 0x0000C564
		internal YogaSize Measure(YogaNode node, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode)
		{
			Debug.Assert(node == this.yogaNode, "YogaNode instance mismatch");
			Vector2 vector = this.DoMeasure(width, (VisualElement.MeasureMode)widthMode, height, (VisualElement.MeasureMode)heightMode);
			float scaledPixelsPerPoint = this.scaledPixelsPerPoint;
			return MeasureOutput.Make(AlignmentUtils.RoundToPixelGrid(vector.x, scaledPixelsPerPoint, 0.02f), AlignmentUtils.RoundToPixelGrid(vector.y, scaledPixelsPerPoint, 0.02f));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		internal void SetSize(Vector2 size)
		{
			Rect layout = this.layout;
			layout.width = size.x;
			layout.height = size.y;
			this.layout = layout;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000E404 File Offset: 0x0000C604
		private void FinalizeLayout()
		{
			bool flag = this.hasInlineStyle || this.hasRunningAnimations;
			if (flag)
			{
				this.computedStyle.SyncWithLayout(this.yogaNode);
			}
			else
			{
				this.yogaNode.CopyStyle(this.computedStyle.yogaNode);
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E458 File Offset: 0x0000C658
		internal void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			bool flag = this.inlineStyleAccess == null;
			if (flag)
			{
				this.inlineStyleAccess = new InlineStyleAccess(this);
			}
			this.inlineStyleAccess.SetInlineRule(sheet, rule);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E490 File Offset: 0x0000C690
		internal unsafe void UpdateInlineRule(StyleSheet sheet, StyleRule rule)
		{
			ComputedStyle computedStyle = this.computedStyle.Acquire();
			long matchingRulesHash = this.computedStyle.matchingRulesHash;
			ComputedStyle computedStyle2;
			bool flag = !StyleCache.TryGetValue(matchingRulesHash, out computedStyle2);
			if (flag)
			{
				computedStyle2 = *InitialStyle.Get();
			}
			this.m_Style.CopyFrom(ref computedStyle2);
			this.SetInlineRule(sheet, rule);
			this.FinalizeLayout();
			VersionChangeType versionChangeType = ComputedStyle.CompareChanges(ref computedStyle, this.computedStyle);
			computedStyle.Release();
			this.IncrementVersion(versionChangeType);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000E510 File Offset: 0x0000C710
		internal void SetComputedStyle(ref ComputedStyle newStyle)
		{
			bool flag = this.m_Style.matchingRulesHash == newStyle.matchingRulesHash;
			if (!flag)
			{
				VersionChangeType versionChangeType = ComputedStyle.CompareChanges(ref this.m_Style, ref newStyle);
				this.m_Style.CopyFrom(ref newStyle);
				this.FinalizeLayout();
				BaseVisualElementPanel elementPanel = this.elementPanel;
				bool flag2 = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) : null) == this;
				if (flag2)
				{
					this.elementPanel.cursorManager.SetCursor(this.m_Style.cursor);
				}
				this.IncrementVersion(versionChangeType);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000E59C File Offset: 0x0000C79C
		internal void ResetPositionProperties()
		{
			bool flag = !this.hasInlineStyle;
			if (!flag)
			{
				this.style.position = StyleKeyword.Null;
				this.style.marginLeft = StyleKeyword.Null;
				this.style.marginRight = StyleKeyword.Null;
				this.style.marginBottom = StyleKeyword.Null;
				this.style.marginTop = StyleKeyword.Null;
				this.style.left = StyleKeyword.Null;
				this.style.top = StyleKeyword.Null;
				this.style.right = StyleKeyword.Null;
				this.style.bottom = StyleKeyword.Null;
				this.style.width = StyleKeyword.Null;
				this.style.height = StyleKeyword.Null;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000E684 File Offset: 0x0000C884
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				" ",
				this.name,
				" ",
				this.layout.ToString(),
				" world rect: ",
				this.worldBound.ToString()
			});
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E700 File Offset: 0x0000C900
		public IEnumerable<string> GetClasses()
		{
			return this.m_ClassList;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E718 File Offset: 0x0000C918
		internal List<string> GetClassesForIteration()
		{
			return this.m_ClassList;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E730 File Offset: 0x0000C930
		public void ClearClassList()
		{
			bool flag = this.m_ClassList.Count > 0;
			if (flag)
			{
				ObjectListPool<string>.Release(this.m_ClassList);
				this.m_ClassList = VisualElement.s_EmptyClassList;
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E774 File Offset: 0x0000C974
		public void AddToClassList(string className)
		{
			bool flag = this.m_ClassList == VisualElement.s_EmptyClassList;
			if (flag)
			{
				this.m_ClassList = ObjectListPool<string>.Get();
			}
			else
			{
				bool flag2 = this.m_ClassList.Contains(className);
				if (flag2)
				{
					return;
				}
				bool flag3 = this.m_ClassList.Capacity == this.m_ClassList.Count;
				if (flag3)
				{
					this.m_ClassList.Capacity++;
				}
			}
			this.m_ClassList.Add(className);
			this.IncrementVersion(VersionChangeType.StyleSheet);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000E800 File Offset: 0x0000CA00
		public void RemoveFromClassList(string className)
		{
			bool flag = this.m_ClassList.Remove(className);
			if (flag)
			{
				bool flag2 = this.m_ClassList.Count == 0;
				if (flag2)
				{
					ObjectListPool<string>.Release(this.m_ClassList);
					this.m_ClassList = VisualElement.s_EmptyClassList;
				}
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000E854 File Offset: 0x0000CA54
		public void ToggleInClassList(string className)
		{
			bool flag = this.ClassListContains(className);
			if (flag)
			{
				this.RemoveFromClassList(className);
			}
			else
			{
				this.AddToClassList(className);
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E880 File Offset: 0x0000CA80
		public void EnableInClassList(string className, bool enable)
		{
			if (enable)
			{
				this.AddToClassList(className);
			}
			else
			{
				this.RemoveFromClassList(className);
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		public bool ClassListContains(string cls)
		{
			for (int i = 0; i < this.m_ClassList.Count; i++)
			{
				bool flag = this.m_ClassList[i] == cls;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		public object FindAncestorUserData()
		{
			for (VisualElement visualElement = this.parent; visualElement != null; visualElement = visualElement.parent)
			{
				bool flag = visualElement.userData != null;
				if (flag)
				{
					return visualElement.userData;
				}
			}
			return null;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E938 File Offset: 0x0000CB38
		internal object GetProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			object obj;
			this.TryGetPropertyInternal(key, out obj);
			return obj;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000E95C File Offset: 0x0000CB5C
		internal void SetProperty(PropertyName key, object value)
		{
			VisualElement.CheckUserKeyArgument(key);
			this.SetPropertyInternal(key, value);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000E970 File Offset: 0x0000CB70
		internal bool HasProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			object obj;
			return this.TryGetPropertyInternal(key, out obj);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000E994 File Offset: 0x0000CB94
		private bool TryGetPropertyInternal(PropertyName key, out object value)
		{
			value = null;
			bool flag = this.m_PropertyBag != null;
			if (flag)
			{
				for (int i = 0; i < this.m_PropertyBag.Count; i++)
				{
					bool flag2 = this.m_PropertyBag[i].Key == key;
					if (flag2)
					{
						value = this.m_PropertyBag[i].Value;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000EA14 File Offset: 0x0000CC14
		private static void CheckUserKeyArgument(PropertyName key)
		{
			bool flag = PropertyName.IsNullOrEmpty(key);
			if (flag)
			{
				throw new ArgumentNullException("key");
			}
			bool flag2 = key == VisualElement.userDataPropertyKey;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The {0} key is reserved by the system", VisualElement.userDataPropertyKey));
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000EA60 File Offset: 0x0000CC60
		private void SetPropertyInternal(PropertyName key, object value)
		{
			KeyValuePair<PropertyName, object> keyValuePair = new KeyValuePair<PropertyName, object>(key, value);
			bool flag = this.m_PropertyBag == null;
			if (flag)
			{
				this.m_PropertyBag = new List<KeyValuePair<PropertyName, object>>(1);
				this.m_PropertyBag.Add(keyValuePair);
			}
			else
			{
				for (int i = 0; i < this.m_PropertyBag.Count; i++)
				{
					bool flag2 = this.m_PropertyBag[i].Key == key;
					if (flag2)
					{
						this.m_PropertyBag[i] = keyValuePair;
						return;
					}
				}
				bool flag3 = this.m_PropertyBag.Capacity == this.m_PropertyBag.Count;
				if (flag3)
				{
					this.m_PropertyBag.Capacity++;
				}
				this.m_PropertyBag.Add(keyValuePair);
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000EB38 File Offset: 0x0000CD38
		private void UpdateCursorStyle(long eventType)
		{
			bool flag = this.elementPanel != null;
			if (flag)
			{
				bool flag2 = eventType == EventBase<MouseOverEvent>.TypeId() && this.elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) == this;
				if (flag2)
				{
					this.elementPanel.cursorManager.SetCursor(this.computedStyle.cursor);
				}
				else
				{
					bool flag3 = eventType == EventBase<MouseOutEvent>.TypeId();
					if (flag3)
					{
						this.elementPanel.cursorManager.ResetCursor();
					}
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		internal VisualElement.RenderTargetMode subRenderTargetMode
		{
			get
			{
				return this.m_SubRenderTargetMode;
			}
			set
			{
				bool flag = this.m_SubRenderTargetMode == value;
				if (!flag)
				{
					Debug.Assert(Application.isEditor, "subRenderTargetMode is not supported on runtime yet");
					this.m_SubRenderTargetMode = value;
					this.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000EC10 File Offset: 0x0000CE10
		private Material getRuntimeMaterial()
		{
			bool flag = VisualElement.s_runtimeMaterial != null;
			Material material;
			if (flag)
			{
				material = VisualElement.s_runtimeMaterial;
			}
			else
			{
				Shader shader = Shader.Find(UIRUtility.k_DefaultShaderName);
				Debug.Assert(shader != null, "Failed to load UIElements default shader");
				bool flag2 = shader != null;
				if (flag2)
				{
					shader.hideFlags |= HideFlags.DontSaveInEditor;
					Material material2 = new Material(shader);
					material2.hideFlags |= HideFlags.DontSaveInEditor;
					material = (VisualElement.s_runtimeMaterial = material2);
				}
				else
				{
					material = null;
				}
			}
			return material;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000EC98 File Offset: 0x0000CE98
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		internal Material defaultMaterial
		{
			get
			{
				return this.m_defaultMaterial;
			}
			private set
			{
				bool flag = this.m_defaultMaterial == value;
				if (!flag)
				{
					this.m_defaultMaterial = value;
					this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		private VisualElementAnimationSystem GetAnimationSystem()
		{
			bool flag = this.elementPanel != null;
			VisualElementAnimationSystem visualElementAnimationSystem;
			if (flag)
			{
				visualElementAnimationSystem = this.elementPanel.GetUpdater(VisualTreeUpdatePhase.Animation) as VisualElementAnimationSystem;
			}
			else
			{
				visualElementAnimationSystem = null;
			}
			return visualElementAnimationSystem;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		internal void RegisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations == null;
			if (flag)
			{
				this.m_RunningAnimations = new List<IValueAnimationUpdate>();
			}
			this.m_RunningAnimations.Add(anim);
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.RegisterAnimation(anim);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		internal void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations != null;
			if (flag)
			{
				this.m_RunningAnimations.Remove(anim);
			}
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.UnregisterAnimation(anim);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		private void UnregisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.UnregisterAnimations(this.m_RunningAnimations);
				}
			}
			this.styleAnimation.CancelAllAnimations();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000EE08 File Offset: 0x0000D008
		private void RegisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.RegisterAnimations(this.m_RunningAnimations);
				}
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000EE54 File Offset: 0x0000D054
		ValueAnimation<float> ITransitionAnimations.Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000EE94 File Offset: 0x0000D094
		ValueAnimation<Rect> ITransitionAnimations.Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		ValueAnimation<Color> ITransitionAnimations.Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000EF14 File Offset: 0x0000D114
		ValueAnimation<Vector3> ITransitionAnimations.Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000EF54 File Offset: 0x0000D154
		ValueAnimation<Vector2> ITransitionAnimations.Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000EF94 File Offset: 0x0000D194
		ValueAnimation<Quaternion> ITransitionAnimations.Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues from, StyleValues to, int durationMs)
		{
			return this.Start((VisualElement e) => from, to, durationMs);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000F008 File Offset: 0x0000D208
		ValueAnimation<float> ITransitionAnimations.Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return VisualElement.StartAnimation<float>(ValueAnimation<float>.Create(this, new Func<float, float, float, float>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000F038 File Offset: 0x0000D238
		ValueAnimation<Rect> ITransitionAnimations.Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return VisualElement.StartAnimation<Rect>(ValueAnimation<Rect>.Create(this, new Func<Rect, Rect, float, Rect>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000F068 File Offset: 0x0000D268
		ValueAnimation<Color> ITransitionAnimations.Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return VisualElement.StartAnimation<Color>(ValueAnimation<Color>.Create(this, new Func<Color, Color, float, Color>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000F098 File Offset: 0x0000D298
		ValueAnimation<Vector3> ITransitionAnimations.Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector3>(ValueAnimation<Vector3>.Create(this, new Func<Vector3, Vector3, float, Vector3>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000F0C8 File Offset: 0x0000D2C8
		ValueAnimation<Vector2> ITransitionAnimations.Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector2>(ValueAnimation<Vector2>.Create(this, new Func<Vector2, Vector2, float, Vector2>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		ValueAnimation<Quaternion> ITransitionAnimations.Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return VisualElement.StartAnimation<Quaternion>(ValueAnimation<Quaternion>.Create(this, new Func<Quaternion, Quaternion, float, Quaternion>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000F128 File Offset: 0x0000D328
		private static ValueAnimation<T> StartAnimation<T>(ValueAnimation<T> anim, Func<VisualElement, T> fromValueGetter, T to, int durationMs, Action<VisualElement, T> onValueChanged)
		{
			anim.initialValue = fromValueGetter;
			anim.to = to;
			anim.durationMs = durationMs;
			anim.valueUpdated = onValueChanged;
			anim.Start();
			return anim;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000F164 File Offset: 0x0000D364
		private static void AssignStyleValues(VisualElement ve, StyleValues src)
		{
			IStyle style = ve.style;
			foreach (StyleValue styleValue in src.m_StyleValues.m_Values)
			{
				StylePropertyId id = styleValue.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.FontSize)
				{
					if (stylePropertyId != StylePropertyId.Unknown)
					{
						if (stylePropertyId != StylePropertyId.Color)
						{
							if (stylePropertyId == StylePropertyId.FontSize)
							{
								style.fontSize = styleValue.number;
							}
						}
						else
						{
							style.color = styleValue.color;
						}
					}
				}
				else if (stylePropertyId <= StylePropertyId.UnityBackgroundImageTintColor)
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.BorderBottomWidth:
						style.borderBottomWidth = styleValue.number;
						break;
					case StylePropertyId.BorderLeftWidth:
						style.borderLeftWidth = styleValue.number;
						break;
					case StylePropertyId.BorderRightWidth:
						style.borderRightWidth = styleValue.number;
						break;
					case StylePropertyId.BorderTopWidth:
						style.borderTopWidth = styleValue.number;
						break;
					case StylePropertyId.Bottom:
						style.bottom = styleValue.number;
						break;
					case StylePropertyId.Display:
					case StylePropertyId.FlexBasis:
					case StylePropertyId.FlexDirection:
					case StylePropertyId.FlexWrap:
					case StylePropertyId.JustifyContent:
					case StylePropertyId.MaxHeight:
					case StylePropertyId.MaxWidth:
					case StylePropertyId.MinHeight:
					case StylePropertyId.MinWidth:
					case StylePropertyId.Position:
						break;
					case StylePropertyId.FlexGrow:
						style.flexGrow = styleValue.number;
						break;
					case StylePropertyId.FlexShrink:
						style.flexShrink = styleValue.number;
						break;
					case StylePropertyId.Height:
						style.height = styleValue.number;
						break;
					case StylePropertyId.Left:
						style.left = styleValue.number;
						break;
					case StylePropertyId.MarginBottom:
						style.marginBottom = styleValue.number;
						break;
					case StylePropertyId.MarginLeft:
						style.marginLeft = styleValue.number;
						break;
					case StylePropertyId.MarginRight:
						style.marginRight = styleValue.number;
						break;
					case StylePropertyId.MarginTop:
						style.marginTop = styleValue.number;
						break;
					case StylePropertyId.PaddingBottom:
						style.paddingBottom = styleValue.number;
						break;
					case StylePropertyId.PaddingLeft:
						style.paddingLeft = styleValue.number;
						break;
					case StylePropertyId.PaddingRight:
						style.paddingRight = styleValue.number;
						break;
					case StylePropertyId.PaddingTop:
						style.paddingTop = styleValue.number;
						break;
					case StylePropertyId.Right:
						style.right = styleValue.number;
						break;
					case StylePropertyId.Top:
						style.top = styleValue.number;
						break;
					case StylePropertyId.Width:
						style.width = styleValue.number;
						break;
					default:
						if (stylePropertyId == StylePropertyId.UnityBackgroundImageTintColor)
						{
							style.unityBackgroundImageTintColor = styleValue.color;
						}
						break;
					}
				}
				else if (stylePropertyId != StylePropertyId.BorderColor)
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.BackgroundColor:
						style.backgroundColor = styleValue.color;
						break;
					case StylePropertyId.BorderBottomLeftRadius:
						style.borderBottomLeftRadius = styleValue.number;
						break;
					case StylePropertyId.BorderBottomRightRadius:
						style.borderBottomRightRadius = styleValue.number;
						break;
					case StylePropertyId.BorderTopLeftRadius:
						style.borderTopLeftRadius = styleValue.number;
						break;
					case StylePropertyId.BorderTopRightRadius:
						style.borderTopRightRadius = styleValue.number;
						break;
					case StylePropertyId.Opacity:
						style.opacity = styleValue.number;
						break;
					}
				}
				else
				{
					style.borderLeftColor = styleValue.color;
					style.borderTopColor = styleValue.color;
					style.borderRightColor = styleValue.color;
					style.borderBottomColor = styleValue.color;
				}
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000F5DC File Offset: 0x0000D7DC
		private StyleValues ReadCurrentValues(VisualElement ve, StyleValues targetValuesToRead)
		{
			StyleValues styleValues = default(StyleValues);
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			foreach (StyleValue styleValue in targetValuesToRead.m_StyleValues.m_Values)
			{
				StylePropertyId id = styleValue.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.Width)
				{
					if (stylePropertyId != StylePropertyId.Unknown)
					{
						if (stylePropertyId != StylePropertyId.Color)
						{
							switch (stylePropertyId)
							{
							case StylePropertyId.BorderBottomWidth:
								styleValues.borderBottomWidth = resolvedStyle.borderBottomWidth;
								break;
							case StylePropertyId.BorderLeftWidth:
								styleValues.borderLeftWidth = resolvedStyle.borderLeftWidth;
								break;
							case StylePropertyId.BorderRightWidth:
								styleValues.borderRightWidth = resolvedStyle.borderRightWidth;
								break;
							case StylePropertyId.BorderTopWidth:
								styleValues.borderTopWidth = resolvedStyle.borderTopWidth;
								break;
							case StylePropertyId.Bottom:
								styleValues.bottom = resolvedStyle.bottom;
								break;
							case StylePropertyId.FlexGrow:
								styleValues.flexGrow = resolvedStyle.flexGrow;
								break;
							case StylePropertyId.FlexShrink:
								styleValues.flexShrink = resolvedStyle.flexShrink;
								break;
							case StylePropertyId.Height:
								styleValues.height = resolvedStyle.height;
								break;
							case StylePropertyId.Left:
								styleValues.left = resolvedStyle.left;
								break;
							case StylePropertyId.MarginBottom:
								styleValues.marginBottom = resolvedStyle.marginBottom;
								break;
							case StylePropertyId.MarginLeft:
								styleValues.marginLeft = resolvedStyle.marginLeft;
								break;
							case StylePropertyId.MarginRight:
								styleValues.marginRight = resolvedStyle.marginRight;
								break;
							case StylePropertyId.MarginTop:
								styleValues.marginTop = resolvedStyle.marginTop;
								break;
							case StylePropertyId.PaddingBottom:
								styleValues.paddingBottom = resolvedStyle.paddingBottom;
								break;
							case StylePropertyId.PaddingLeft:
								styleValues.paddingLeft = resolvedStyle.paddingLeft;
								break;
							case StylePropertyId.PaddingRight:
								styleValues.paddingRight = resolvedStyle.paddingRight;
								break;
							case StylePropertyId.PaddingTop:
								styleValues.paddingTop = resolvedStyle.paddingTop;
								break;
							case StylePropertyId.Right:
								styleValues.right = resolvedStyle.right;
								break;
							case StylePropertyId.Top:
								styleValues.top = resolvedStyle.top;
								break;
							case StylePropertyId.Width:
								styleValues.width = resolvedStyle.width;
								break;
							}
						}
						else
						{
							styleValues.color = resolvedStyle.color;
						}
					}
				}
				else if (stylePropertyId != StylePropertyId.UnityBackgroundImageTintColor)
				{
					if (stylePropertyId != StylePropertyId.BorderColor)
					{
						switch (stylePropertyId)
						{
						case StylePropertyId.BackgroundColor:
							styleValues.backgroundColor = resolvedStyle.backgroundColor;
							break;
						case StylePropertyId.BorderBottomLeftRadius:
							styleValues.borderBottomLeftRadius = resolvedStyle.borderBottomLeftRadius;
							break;
						case StylePropertyId.BorderBottomRightRadius:
							styleValues.borderBottomRightRadius = resolvedStyle.borderBottomRightRadius;
							break;
						case StylePropertyId.BorderTopLeftRadius:
							styleValues.borderTopLeftRadius = resolvedStyle.borderTopLeftRadius;
							break;
						case StylePropertyId.BorderTopRightRadius:
							styleValues.borderTopRightRadius = resolvedStyle.borderTopRightRadius;
							break;
						case StylePropertyId.Opacity:
							styleValues.opacity = resolvedStyle.opacity;
							break;
						}
					}
					else
					{
						styleValues.borderColor = resolvedStyle.borderLeftColor;
					}
				}
				else
				{
					styleValues.unityBackgroundImageTintColor = resolvedStyle.unityBackgroundImageTintColor;
				}
			}
			return styleValues;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000F984 File Offset: 0x0000DB84
		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues to, int durationMs)
		{
			return this.Start((VisualElement e) => this.ReadCurrentValues(e, to), to, durationMs);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		private ValueAnimation<StyleValues> Start(Func<VisualElement, StyleValues> fromValueGetter, StyleValues to, int durationMs)
		{
			return VisualElement.StartAnimation<StyleValues>(ValueAnimation<StyleValues>.Create(this, new Func<StyleValues, StyleValues, float, StyleValues>(Lerp.Interpolate)), fromValueGetter, to, durationMs, new Action<VisualElement, StyleValues>(VisualElement.AssignStyleValues));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		ValueAnimation<Rect> ITransitionAnimations.Layout(Rect to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => new Rect(e.resolvedStyle.left, e.resolvedStyle.top, e.resolvedStyle.width, e.resolvedStyle.height), to, durationMs, delegate(VisualElement e, Rect c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
				e.style.width = c.width;
				e.style.height = c.height;
			});
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000FA60 File Offset: 0x0000DC60
		ValueAnimation<Vector2> ITransitionAnimations.TopLeft(Vector2 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => new Vector2(e.resolvedStyle.left, e.resolvedStyle.top), to, durationMs, delegate(VisualElement e, Vector2 c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
			});
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
		ValueAnimation<Vector2> ITransitionAnimations.Size(Vector2 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.layout.size, to, durationMs, delegate(VisualElement e, Vector2 c)
			{
				e.style.width = c.x;
				e.style.height = c.y;
			});
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000FB28 File Offset: 0x0000DD28
		ValueAnimation<float> ITransitionAnimations.Scale(float to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.scale.x, to, durationMs, delegate(VisualElement e, float c)
			{
				e.transform.scale = new Vector3(c, c, c);
			});
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000FB8C File Offset: 0x0000DD8C
		ValueAnimation<Vector3> ITransitionAnimations.Position(Vector3 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.position, to, durationMs, delegate(VisualElement e, Vector3 c)
			{
				e.transform.position = c;
			});
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		ValueAnimation<Quaternion> ITransitionAnimations.Rotation(Quaternion to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.rotation, to, durationMs, delegate(VisualElement e, Quaternion c)
			{
				e.transform.rotation = c;
			});
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000FC54 File Offset: 0x0000DE54
		public IExperimentalFeatures experimental
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000FC68 File Offset: 0x0000DE68
		ITransitionAnimations IExperimentalFeatures.animation
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000FC7B File Offset: 0x0000DE7B
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000FC83 File Offset: 0x0000DE83
		public VisualElement.Hierarchy hierarchy { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000FC94 File Offset: 0x0000DE94
		internal bool isRootVisualContainer { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000FC9D File Offset: 0x0000DE9D
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000FCA5 File Offset: 0x0000DEA5
		[Obsolete("VisualElement.cacheAsBitmap is deprecated and has no effect")]
		public bool cacheAsBitmap { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000FCAE File Offset: 0x0000DEAE
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000FCC3 File Offset: 0x0000DEC3
		internal bool disableClipping
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.DisableClipping) == VisualElementFlags.DisableClipping;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.DisableClipping) : (this.m_Flags & ~VisualElementFlags.DisableClipping));
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		internal bool ShouldClip()
		{
			return this.computedStyle.overflow != OverflowInternal.Visible && !this.disableClipping;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000FD14 File Offset: 0x0000DF14
		public VisualElement parent
		{
			get
			{
				return this.m_LogicalParent;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000FD34 File Offset: 0x0000DF34
		internal BaseVisualElementPanel elementPanel { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public IPanel panel
		{
			get
			{
				return this.elementPanel;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000FD58 File Offset: 0x0000DF58
		public virtual VisualElement contentContainer
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000FD6B File Offset: 0x0000DF6B
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000FD73 File Offset: 0x0000DF73
		public VisualTreeAsset visualTreeAssetSource
		{
			get
			{
				return this.m_VisualTreeAssetSource;
			}
			internal set
			{
				this.m_VisualTreeAssetSource = value;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		public void Add(VisualElement child)
		{
			bool flag = child == null;
			if (!flag)
			{
				VisualElement contentContainer = this.contentContainer;
				bool flag2 = contentContainer == null;
				if (flag2)
				{
					throw new InvalidOperationException("You can't add directly to this VisualElement. Use hierarchy.Add() if you know what you're doing.");
				}
				bool flag3 = contentContainer == this;
				if (flag3)
				{
					this.hierarchy.Add(child);
				}
				else if (contentContainer != null)
				{
					contentContainer.Add(child);
				}
				child.m_LogicalParent = this;
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public void Insert(int index, VisualElement element)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = this.contentContainer == this;
				if (flag2)
				{
					this.hierarchy.Insert(index, element);
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					if (contentContainer != null)
					{
						contentContainer.Insert(index, element);
					}
				}
				element.m_LogicalParent = this;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000FE3C File Offset: 0x0000E03C
		public void Remove(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Remove(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Remove(element);
				}
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000FE80 File Offset: 0x0000E080
		public void RemoveAt(int index)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.RemoveAt(index);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.RemoveAt(index);
				}
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		public void Clear()
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Clear();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Clear();
				}
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000FF08 File Offset: 0x0000E108
		public VisualElement ElementAt(int index)
		{
			return this[index];
		}

		// Token: 0x170000EE RID: 238
		public VisualElement this[int key]
		{
			get
			{
				bool flag = this.contentContainer == this;
				VisualElement visualElement;
				if (flag)
				{
					visualElement = this.hierarchy[key];
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					visualElement = ((contentContainer != null) ? contentContainer[key] : null);
				}
				return visualElement;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000FF6C File Offset: 0x0000E16C
		public int childCount
		{
			get
			{
				bool flag = this.contentContainer == this;
				int num;
				if (flag)
				{
					num = this.hierarchy.childCount;
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					num = ((contentContainer != null) ? contentContainer.childCount : 0);
				}
				return num;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		public int IndexOf(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			int num;
			if (flag)
			{
				num = this.hierarchy.IndexOf(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				num = ((contentContainer != null) ? contentContainer.IndexOf(element) : (-1));
			}
			return num;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000FFF8 File Offset: 0x0000E1F8
		internal VisualElement ElementAtTreePath(List<int> childIndexes)
		{
			VisualElement visualElement = this;
			foreach (int num in childIndexes)
			{
				bool flag = num >= 0 && num < visualElement.hierarchy.childCount;
				if (!flag)
				{
					return null;
				}
				visualElement = visualElement.hierarchy[num];
			}
			return visualElement;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00010088 File Offset: 0x0000E288
		internal bool FindElementInTree(VisualElement element, List<int> outChildIndexes)
		{
			VisualElement visualElement = element;
			for (VisualElement visualElement2 = visualElement.hierarchy.parent; visualElement2 != null; visualElement2 = visualElement2.hierarchy.parent)
			{
				outChildIndexes.Insert(0, visualElement2.hierarchy.IndexOf(visualElement));
				bool flag = visualElement2 == this;
				if (flag)
				{
					return true;
				}
				visualElement = visualElement2;
			}
			outChildIndexes.Clear();
			return false;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000100FC File Offset: 0x0000E2FC
		public IEnumerable<VisualElement> Children()
		{
			bool flag = this.contentContainer == this;
			IEnumerable<VisualElement> enumerable;
			if (flag)
			{
				enumerable = this.hierarchy.Children();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				enumerable = ((contentContainer != null) ? contentContainer.Children() : null) ?? VisualElement.s_EmptyList;
			}
			return enumerable;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00010148 File Offset: 0x0000E348
		public void Sort(Comparison<VisualElement> comp)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Sort(comp);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Sort(comp);
				}
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001018C File Offset: 0x0000E38C
		public void BringToFront()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.BringToFront(this);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000101D0 File Offset: 0x0000E3D0
		public void SendToBack()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.SendToBack(this);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00010214 File Offset: 0x0000E414
		public void PlaceBehind(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceBehind(this, sibling);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00010298 File Offset: 0x0000E498
		public void PlaceInFront(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceInFront(this, sibling);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001031C File Offset: 0x0000E51C
		public void RemoveFromHierarchy()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.hierarchy.Remove(this);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00010360 File Offset: 0x0000E560
		public T GetFirstOfType<T>() where T : class
		{
			T t = this as T;
			bool flag = t != null;
			T t2;
			if (flag)
			{
				t2 = t;
			}
			else
			{
				t2 = this.GetFirstAncestorOfType<T>();
			}
			return t2;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00010398 File Offset: 0x0000E598
		public T GetFirstAncestorOfType<T>() where T : class
		{
			for (VisualElement visualElement = this.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				T t = visualElement as T;
				bool flag = t != null;
				if (flag)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00010404 File Offset: 0x0000E604
		public bool Contains(VisualElement child)
		{
			while (child != null)
			{
				bool flag = child.hierarchy.parent == this;
				if (flag)
				{
					return true;
				}
				child = child.hierarchy.parent;
			}
			return false;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00010450 File Offset: 0x0000E650
		private void GatherAllChildren(List<VisualElement> elements)
		{
			bool flag = this.m_Children.Count > 0;
			if (flag)
			{
				int i = elements.Count;
				elements.AddRange(this.m_Children);
				while (i < elements.Count)
				{
					VisualElement visualElement = elements[i];
					elements.AddRange(visualElement.m_Children);
					i++;
				}
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000104B0 File Offset: 0x0000E6B0
		public VisualElement FindCommonAncestor(VisualElement other)
		{
			bool flag = other == null;
			if (flag)
			{
				throw new ArgumentNullException("other");
			}
			bool flag2 = this.panel != other.panel;
			VisualElement visualElement;
			if (flag2)
			{
				visualElement = null;
			}
			else
			{
				VisualElement visualElement2 = this;
				int i = 0;
				while (visualElement2 != null)
				{
					i++;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				VisualElement visualElement3 = other;
				int j = 0;
				while (visualElement3 != null)
				{
					j++;
					visualElement3 = visualElement3.hierarchy.parent;
				}
				visualElement2 = this;
				visualElement3 = other;
				while (i > j)
				{
					i--;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				while (j > i)
				{
					j--;
					visualElement3 = visualElement3.hierarchy.parent;
				}
				while (visualElement2 != visualElement3)
				{
					visualElement2 = visualElement2.hierarchy.parent;
					visualElement3 = visualElement3.hierarchy.parent;
				}
				visualElement = visualElement2;
			}
			return visualElement;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000105C0 File Offset: 0x0000E7C0
		internal VisualElement GetRoot()
		{
			bool flag = this.panel != null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.panel.visualTree;
			}
			else
			{
				VisualElement visualElement2 = this;
				while (visualElement2.m_PhysicalParent != null)
				{
					visualElement2 = visualElement2.m_PhysicalParent;
				}
				visualElement = visualElement2;
			}
			return visualElement;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001060C File Offset: 0x0000E80C
		internal VisualElement GetRootVisualContainer()
		{
			VisualElement visualElement = null;
			for (VisualElement visualElement2 = this; visualElement2 != null; visualElement2 = visualElement2.hierarchy.parent)
			{
				bool isRootVisualContainer = visualElement2.isRootVisualContainer;
				if (isRootVisualContainer)
				{
					visualElement = visualElement2;
				}
			}
			return visualElement;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00010650 File Offset: 0x0000E850
		internal VisualElement GetNextElementDepthFirst()
		{
			bool flag = this.m_Children.Count > 0;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.m_Children[0];
			}
			else
			{
				VisualElement visualElement2 = this.m_PhysicalParent;
				VisualElement visualElement3 = this;
				while (visualElement2 != null)
				{
					int i;
					for (i = 0; i < visualElement2.m_Children.Count; i++)
					{
						bool flag2 = visualElement2.m_Children[i] == visualElement3;
						if (flag2)
						{
							break;
						}
					}
					bool flag3 = i < visualElement2.m_Children.Count - 1;
					if (flag3)
					{
						return visualElement2.m_Children[i + 1];
					}
					visualElement3 = visualElement2;
					visualElement2 = visualElement2.m_PhysicalParent;
				}
				visualElement = null;
			}
			return visualElement;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00010710 File Offset: 0x0000E910
		internal VisualElement GetPreviousElementDepthFirst()
		{
			bool flag = this.m_PhysicalParent != null;
			VisualElement visualElement2;
			if (flag)
			{
				int i;
				for (i = 0; i < this.m_PhysicalParent.m_Children.Count; i++)
				{
					bool flag2 = this.m_PhysicalParent.m_Children[i] == this;
					if (flag2)
					{
						break;
					}
				}
				bool flag3 = i > 0;
				if (flag3)
				{
					VisualElement visualElement = this.m_PhysicalParent.m_Children[i - 1];
					while (visualElement.m_Children.Count > 0)
					{
						visualElement = visualElement.m_Children[visualElement.m_Children.Count - 1];
					}
					visualElement2 = visualElement;
				}
				else
				{
					visualElement2 = this.m_PhysicalParent;
				}
			}
			else
			{
				visualElement2 = null;
			}
			return visualElement2;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000107D8 File Offset: 0x0000E9D8
		internal VisualElement RetargetElement(VisualElement retargetAgainst)
		{
			bool flag = retargetAgainst == null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this;
			}
			else
			{
				VisualElement visualElement2 = retargetAgainst.m_PhysicalParent ?? retargetAgainst;
				while (visualElement2.m_PhysicalParent != null && !visualElement2.isCompositeRoot)
				{
					visualElement2 = visualElement2.m_PhysicalParent;
				}
				VisualElement visualElement3 = this;
				VisualElement visualElement4 = this.m_PhysicalParent;
				while (visualElement4 != null)
				{
					visualElement4 = visualElement4.m_PhysicalParent;
					bool flag2 = visualElement4 == visualElement2;
					if (flag2)
					{
						return visualElement3;
					}
					bool flag3 = visualElement4 != null && visualElement4.isCompositeRoot;
					if (flag3)
					{
						visualElement3 = visualElement4;
					}
				}
				visualElement = this;
			}
			return visualElement;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00010870 File Offset: 0x0000EA70
		private Vector3 positionWithLayout
		{
			get
			{
				return this.ResolveTranslate() + this.layout.min;
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000108A0 File Offset: 0x0000EAA0
		internal void GetPivotedMatrixWithLayout(out Matrix4x4 result)
		{
			Vector3 vector = this.ResolveTransformOrigin();
			result = Matrix4x4.TRS(this.positionWithLayout + vector, this.ResolveRotation(), this.ResolveScale());
			VisualElement.TranslateMatrix34InPlace(ref result, -vector);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000108E8 File Offset: 0x0000EAE8
		internal bool hasDefaultRotationAndScale
		{
			[MethodImpl(256)]
			get
			{
				return this.computedStyle.rotate.angle.value == 0f && this.computedStyle.scale.value == Vector3.one;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001093C File Offset: 0x0000EB3C
		[MethodImpl(256)]
		internal static float Min(float a, float b, float c, float d)
		{
			return Mathf.Min(Mathf.Min(a, b), Mathf.Min(c, d));
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00010964 File Offset: 0x0000EB64
		[MethodImpl(256)]
		internal static float Max(float a, float b, float c, float d)
		{
			return Mathf.Max(Mathf.Max(a, b), Mathf.Max(c, d));
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001098C File Offset: 0x0000EB8C
		[MethodImpl(256)]
		private void TransformAlignedRectToParentSpace(ref Rect rect)
		{
			bool hasDefaultRotationAndScale = this.hasDefaultRotationAndScale;
			if (hasDefaultRotationAndScale)
			{
				rect.position += this.positionWithLayout;
			}
			else
			{
				Matrix4x4 matrix4x;
				this.GetPivotedMatrixWithLayout(out matrix4x);
				rect = VisualElement.CalculateConservativeRect(ref matrix4x, rect);
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000109E4 File Offset: 0x0000EBE4
		internal static Rect CalculateConservativeRect(ref Matrix4x4 matrix, Rect rect)
		{
			bool flag = float.IsNaN(rect.height) | float.IsNaN(rect.width) | float.IsNaN(rect.x) | float.IsNaN(rect.y);
			Rect rect2;
			if (flag)
			{
				rect = new Rect(VisualElement.MultiplyMatrix44Point2(ref matrix, rect.position), VisualElement.MultiplyVector2(ref matrix, rect.size));
				VisualElement.OrderMinMaxRect(ref rect);
				rect2 = rect;
			}
			else
			{
				Vector2 vector = new Vector2(rect.xMin, rect.yMin);
				Vector2 vector2 = new Vector2(rect.xMax, rect.yMax);
				Vector2 vector3 = new Vector2(rect.xMax, rect.yMin);
				Vector2 vector4 = new Vector2(rect.xMin, rect.yMax);
				Vector3 vector5 = matrix.MultiplyPoint3x4(vector);
				Vector3 vector6 = matrix.MultiplyPoint3x4(vector2);
				Vector3 vector7 = matrix.MultiplyPoint3x4(vector3);
				Vector3 vector8 = matrix.MultiplyPoint3x4(vector4);
				Vector2 vector9 = new Vector2(VisualElement.Min(vector5.x, vector6.x, vector7.x, vector8.x), VisualElement.Min(vector5.y, vector6.y, vector7.y, vector8.y));
				Vector2 vector10 = new Vector2(VisualElement.Max(vector5.x, vector6.x, vector7.x, vector8.x), VisualElement.Max(vector5.y, vector6.y, vector7.y, vector8.y));
				rect2 = new Rect(vector9.x, vector9.y, vector10.x - vector9.x, vector10.y - vector9.y);
			}
			return rect2;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00010BB2 File Offset: 0x0000EDB2
		[MethodImpl(256)]
		internal static void TransformAlignedRect(ref Matrix4x4 matrix, ref Rect rect)
		{
			rect = VisualElement.CalculateConservativeRect(ref matrix, rect);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		internal static void OrderMinMaxRect(ref Rect rect)
		{
			bool flag = rect.width < 0f;
			if (flag)
			{
				rect.x += rect.width;
				rect.width = -rect.width;
			}
			bool flag2 = rect.height < 0f;
			if (flag2)
			{
				rect.y += rect.height;
				rect.height = -rect.height;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00010C40 File Offset: 0x0000EE40
		[MethodImpl(256)]
		internal static Vector2 MultiplyMatrix44Point2(ref Matrix4x4 lhs, Vector2 point)
		{
			Vector2 vector;
			vector.x = lhs.m00 * point.x + lhs.m01 * point.y + lhs.m03;
			vector.y = lhs.m10 * point.x + lhs.m11 * point.y + lhs.m13;
			return vector;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		[MethodImpl(256)]
		internal static Vector2 MultiplyVector2(ref Matrix4x4 lhs, Vector2 vector)
		{
			Vector2 vector2;
			vector2.x = lhs.m00 * vector.x + lhs.m01 * vector.y;
			vector2.y = lhs.m10 * vector.x + lhs.m11 * vector.y;
			return vector2;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00010D00 File Offset: 0x0000EF00
		[MethodImpl(256)]
		internal static Rect MultiplyMatrix44Rect2(ref Matrix4x4 lhs, Rect r)
		{
			r.position = VisualElement.MultiplyMatrix44Point2(ref lhs, r.position);
			r.size = VisualElement.MultiplyVector2(ref lhs, r.size);
			return r;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00010D40 File Offset: 0x0000EF40
		internal static void MultiplyMatrix34(ref Matrix4x4 lhs, ref Matrix4x4 rhs, out Matrix4x4 res)
		{
			res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20;
			res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21;
			res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22;
			res.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03;
			res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20;
			res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21;
			res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22;
			res.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13;
			res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20;
			res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21;
			res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22;
			res.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23;
			res.m30 = 0f;
			res.m31 = 0f;
			res.m32 = 0f;
			res.m33 = 1f;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00010FC3 File Offset: 0x0000F1C3
		[MethodImpl(256)]
		private static void TranslateMatrix34(ref Matrix4x4 lhs, Vector3 rhs, out Matrix4x4 res)
		{
			res = lhs;
			VisualElement.TranslateMatrix34InPlace(ref res, rhs);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00010FDC File Offset: 0x0000F1DC
		[MethodImpl(256)]
		private static void TranslateMatrix34InPlace(ref Matrix4x4 lhs, Vector3 rhs)
		{
			lhs.m03 += lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z;
			lhs.m13 += lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z;
			lhs.m23 += lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00011084 File Offset: 0x0000F284
		public IVisualElementScheduler schedule
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00011098 File Offset: 0x0000F298
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action<TimerState> timerUpdateEvent)
		{
			VisualElement.TimerStateScheduledItem timerStateScheduledItem = new VisualElement.TimerStateScheduledItem(this, timerUpdateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			timerStateScheduledItem.Resume();
			return timerStateScheduledItem;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000110C8 File Offset: 0x0000F2C8
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action updateEvent)
		{
			VisualElement.SimpleScheduledItem simpleScheduledItem = new VisualElement.SimpleScheduledItem(this, updateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			simpleScheduledItem.Resume();
			return simpleScheduledItem;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x000110F8 File Offset: 0x0000F2F8
		public IStyle style
		{
			get
			{
				bool flag = this.inlineStyleAccess == null;
				if (flag)
				{
					this.inlineStyleAccess = new InlineStyleAccess(this);
				}
				return this.inlineStyleAccess;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001112C File Offset: 0x0000F32C
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement.s_CustomStyleAccess.SetContext(this.computedStyle.customProperties, this.computedStyle.dpiScaling);
				return VisualElement.s_CustomStyleAccess;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00011164 File Offset: 0x0000F364
		public VisualElementStyleSheetSet styleSheets
		{
			get
			{
				return new VisualElementStyleSheetSet(this);
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001116C File Offset: 0x0000F36C
		internal void AddStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				bool flag2 = !VisualElement.s_InternalStyleSheetPath.IsMatch(sheetPath);
				if (flag2)
				{
					Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				}
			}
			else
			{
				this.styleSheets.Add(styleSheet);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000111DC File Offset: 0x0000F3DC
		internal bool HasStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			bool flag2;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				flag2 = false;
			}
			else
			{
				flag2 = this.styleSheets.Contains(styleSheet);
			}
			return flag2;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001123C File Offset: 0x0000F43C
		internal void RemoveStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
			}
			else
			{
				this.styleSheets.Remove(styleSheet);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00011298 File Offset: 0x0000F498
		private StyleFloat ResolveLengthValue(Length length, bool isRow)
		{
			bool flag = length.IsAuto();
			StyleFloat styleFloat;
			if (flag)
			{
				styleFloat = new StyleFloat(StyleKeyword.Auto);
			}
			else
			{
				bool flag2 = length.IsNone();
				if (flag2)
				{
					styleFloat = new StyleFloat(StyleKeyword.None);
				}
				else
				{
					bool flag3 = length.unit != LengthUnit.Percent;
					if (flag3)
					{
						styleFloat = new StyleFloat(length.value);
					}
					else
					{
						VisualElement parent = this.hierarchy.parent;
						bool flag4 = parent == null;
						if (flag4)
						{
							styleFloat = 0f;
						}
						else
						{
							float num = (isRow ? parent.resolvedStyle.width : parent.resolvedStyle.height);
							styleFloat = length.value * num / 100f;
						}
					}
				}
			}
			return styleFloat;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00011350 File Offset: 0x0000F550
		private Vector3 ResolveTranslate()
		{
			Translate translate = this.computedStyle.translate;
			Length x = translate.x;
			bool flag = x.unit == LengthUnit.Percent;
			float num;
			if (flag)
			{
				float width = this.resolvedStyle.width;
				num = (float.IsNaN(width) ? 0f : (width * x.value / 100f));
			}
			else
			{
				num = x.value;
			}
			Length y = translate.y;
			bool flag2 = y.unit == LengthUnit.Percent;
			float num2;
			if (flag2)
			{
				float height = this.resolvedStyle.height;
				num2 = (float.IsNaN(height) ? 0f : (height * y.value / 100f));
			}
			else
			{
				num2 = y.value;
			}
			float z = translate.z;
			return new Vector3(num, num2, z);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001142C File Offset: 0x0000F62C
		private Vector3 ResolveTransformOrigin()
		{
			TransformOrigin transformOrigin = this.computedStyle.transformOrigin;
			Length x = transformOrigin.x;
			bool flag = x.IsNone();
			float num;
			if (flag)
			{
				float width = this.resolvedStyle.width;
				num = (float.IsNaN(width) ? 0f : (width / 2f));
			}
			else
			{
				bool flag2 = x.unit == LengthUnit.Percent;
				if (flag2)
				{
					float width2 = this.resolvedStyle.width;
					num = (float.IsNaN(width2) ? 0f : (width2 * x.value / 100f));
				}
				else
				{
					num = x.value;
				}
			}
			Length y = transformOrigin.y;
			bool flag3 = y.IsNone();
			float num2;
			if (flag3)
			{
				float height = this.resolvedStyle.height;
				num2 = (float.IsNaN(height) ? 0f : (height / 2f));
			}
			else
			{
				bool flag4 = y.unit == LengthUnit.Percent;
				if (flag4)
				{
					float height2 = this.resolvedStyle.height;
					num2 = (float.IsNaN(height2) ? 0f : (height2 * y.value / 100f));
				}
				else
				{
					num2 = y.value;
				}
			}
			float z = transformOrigin.z;
			return new Vector3(num, num2, z);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00011584 File Offset: 0x0000F784
		private Quaternion ResolveRotation()
		{
			return this.computedStyle.rotate.ToQuaternion();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000115A4 File Offset: 0x0000F7A4
		private Vector3 ResolveScale()
		{
			return this.computedStyle.scale.value;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x000115C4 File Offset: 0x0000F7C4
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x000115F4 File Offset: 0x0000F7F4
		public string tooltip
		{
			get
			{
				string text = this.GetProperty(VisualElement.tooltipPropertyKey) as string;
				return text ?? string.Empty;
			}
			set
			{
				bool flag = !this.HasProperty(VisualElement.tooltipPropertyKey);
				if (flag)
				{
					base.RegisterCallback<TooltipEvent>(new EventCallback<TooltipEvent>(this.SetTooltip), TrickleDown.NoTrickleDown);
				}
				this.SetProperty(VisualElement.tooltipPropertyKey, value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00011638 File Offset: 0x0000F838
		private VisualElement.TypeData typeData
		{
			get
			{
				bool flag = this.m_TypeData == null;
				if (flag)
				{
					Type type = base.GetType();
					bool flag2 = !VisualElement.s_TypeData.TryGetValue(type, ref this.m_TypeData);
					if (flag2)
					{
						this.m_TypeData = new VisualElement.TypeData(type);
						VisualElement.s_TypeData.Add(type, this.m_TypeData);
					}
				}
				return this.m_TypeData;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		public IResolvedStyle resolvedStyle
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0001169E File Offset: 0x0000F89E
		Align IResolvedStyle.alignContent
		{
			get
			{
				return this.computedStyle.alignContent;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x000116AB File Offset: 0x0000F8AB
		Align IResolvedStyle.alignItems
		{
			get
			{
				return this.computedStyle.alignItems;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x000116B8 File Offset: 0x0000F8B8
		Align IResolvedStyle.alignSelf
		{
			get
			{
				return this.computedStyle.alignSelf;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x000116C5 File Offset: 0x0000F8C5
		Color IResolvedStyle.backgroundColor
		{
			get
			{
				return this.computedStyle.backgroundColor;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x000116D2 File Offset: 0x0000F8D2
		Background IResolvedStyle.backgroundImage
		{
			get
			{
				return this.computedStyle.backgroundImage;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x000116DF File Offset: 0x0000F8DF
		Color IResolvedStyle.borderBottomColor
		{
			get
			{
				return this.computedStyle.borderBottomColor;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x000116EC File Offset: 0x0000F8EC
		float IResolvedStyle.borderBottomLeftRadius
		{
			get
			{
				return this.computedStyle.borderBottomLeftRadius.value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001170C File Offset: 0x0000F90C
		float IResolvedStyle.borderBottomRightRadius
		{
			get
			{
				return this.computedStyle.borderBottomRightRadius.value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001172C File Offset: 0x0000F92C
		float IResolvedStyle.borderBottomWidth
		{
			get
			{
				return this.yogaNode.LayoutBorderBottom;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00011739 File Offset: 0x0000F939
		Color IResolvedStyle.borderLeftColor
		{
			get
			{
				return this.computedStyle.borderLeftColor;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00011746 File Offset: 0x0000F946
		float IResolvedStyle.borderLeftWidth
		{
			get
			{
				return this.yogaNode.LayoutBorderLeft;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00011753 File Offset: 0x0000F953
		Color IResolvedStyle.borderRightColor
		{
			get
			{
				return this.computedStyle.borderRightColor;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00011760 File Offset: 0x0000F960
		float IResolvedStyle.borderRightWidth
		{
			get
			{
				return this.yogaNode.LayoutBorderRight;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001176D File Offset: 0x0000F96D
		Color IResolvedStyle.borderTopColor
		{
			get
			{
				return this.computedStyle.borderTopColor;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001177C File Offset: 0x0000F97C
		float IResolvedStyle.borderTopLeftRadius
		{
			get
			{
				return this.computedStyle.borderTopLeftRadius.value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001179C File Offset: 0x0000F99C
		float IResolvedStyle.borderTopRightRadius
		{
			get
			{
				return this.computedStyle.borderTopRightRadius.value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000117BC File Offset: 0x0000F9BC
		float IResolvedStyle.borderTopWidth
		{
			get
			{
				return this.yogaNode.LayoutBorderTop;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x000117C9 File Offset: 0x0000F9C9
		float IResolvedStyle.bottom
		{
			get
			{
				return this.yogaNode.LayoutBottom;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x000117D6 File Offset: 0x0000F9D6
		Color IResolvedStyle.color
		{
			get
			{
				return this.computedStyle.color;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000117E3 File Offset: 0x0000F9E3
		DisplayStyle IResolvedStyle.display
		{
			get
			{
				return this.computedStyle.display;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000117F0 File Offset: 0x0000F9F0
		StyleFloat IResolvedStyle.flexBasis
		{
			get
			{
				return new StyleFloat(this.yogaNode.ComputedFlexBasis);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00011802 File Offset: 0x0000FA02
		FlexDirection IResolvedStyle.flexDirection
		{
			get
			{
				return this.computedStyle.flexDirection;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001180F File Offset: 0x0000FA0F
		float IResolvedStyle.flexGrow
		{
			get
			{
				return this.computedStyle.flexGrow;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001181C File Offset: 0x0000FA1C
		float IResolvedStyle.flexShrink
		{
			get
			{
				return this.computedStyle.flexShrink;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00011829 File Offset: 0x0000FA29
		Wrap IResolvedStyle.flexWrap
		{
			get
			{
				return this.computedStyle.flexWrap;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00011838 File Offset: 0x0000FA38
		float IResolvedStyle.fontSize
		{
			get
			{
				return this.computedStyle.fontSize.value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00011858 File Offset: 0x0000FA58
		float IResolvedStyle.height
		{
			get
			{
				return this.yogaNode.LayoutHeight;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00011865 File Offset: 0x0000FA65
		Justify IResolvedStyle.justifyContent
		{
			get
			{
				return this.computedStyle.justifyContent;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00011872 File Offset: 0x0000FA72
		float IResolvedStyle.left
		{
			get
			{
				return this.yogaNode.LayoutX;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00011880 File Offset: 0x0000FA80
		float IResolvedStyle.letterSpacing
		{
			get
			{
				return this.computedStyle.letterSpacing.value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000118A0 File Offset: 0x0000FAA0
		float IResolvedStyle.marginBottom
		{
			get
			{
				return this.yogaNode.LayoutMarginBottom;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000118AD File Offset: 0x0000FAAD
		float IResolvedStyle.marginLeft
		{
			get
			{
				return this.yogaNode.LayoutMarginLeft;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000118BA File Offset: 0x0000FABA
		float IResolvedStyle.marginRight
		{
			get
			{
				return this.yogaNode.LayoutMarginRight;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000118C7 File Offset: 0x0000FAC7
		float IResolvedStyle.marginTop
		{
			get
			{
				return this.yogaNode.LayoutMarginTop;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x000118D4 File Offset: 0x0000FAD4
		StyleFloat IResolvedStyle.maxHeight
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.maxHeight, false);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000118E8 File Offset: 0x0000FAE8
		StyleFloat IResolvedStyle.maxWidth
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.maxWidth, true);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000118FC File Offset: 0x0000FAFC
		StyleFloat IResolvedStyle.minHeight
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.minHeight, false);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00011910 File Offset: 0x0000FB10
		StyleFloat IResolvedStyle.minWidth
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.minWidth, true);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00011924 File Offset: 0x0000FB24
		float IResolvedStyle.opacity
		{
			get
			{
				return this.computedStyle.opacity;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00011931 File Offset: 0x0000FB31
		float IResolvedStyle.paddingBottom
		{
			get
			{
				return this.yogaNode.LayoutPaddingBottom;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001193E File Offset: 0x0000FB3E
		float IResolvedStyle.paddingLeft
		{
			get
			{
				return this.yogaNode.LayoutPaddingLeft;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001194B File Offset: 0x0000FB4B
		float IResolvedStyle.paddingRight
		{
			get
			{
				return this.yogaNode.LayoutPaddingRight;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00011958 File Offset: 0x0000FB58
		float IResolvedStyle.paddingTop
		{
			get
			{
				return this.yogaNode.LayoutPaddingTop;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00011965 File Offset: 0x0000FB65
		Position IResolvedStyle.position
		{
			get
			{
				return this.computedStyle.position;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00011972 File Offset: 0x0000FB72
		float IResolvedStyle.right
		{
			get
			{
				return this.yogaNode.LayoutRight;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001197F File Offset: 0x0000FB7F
		Rotate IResolvedStyle.rotate
		{
			get
			{
				return this.computedStyle.rotate;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001198C File Offset: 0x0000FB8C
		Scale IResolvedStyle.scale
		{
			get
			{
				return this.computedStyle.scale;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00011999 File Offset: 0x0000FB99
		TextOverflow IResolvedStyle.textOverflow
		{
			get
			{
				return this.computedStyle.textOverflow;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000119A6 File Offset: 0x0000FBA6
		float IResolvedStyle.top
		{
			get
			{
				return this.yogaNode.LayoutY;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000119B3 File Offset: 0x0000FBB3
		Vector3 IResolvedStyle.transformOrigin
		{
			get
			{
				return this.ResolveTransformOrigin();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000119BB File Offset: 0x0000FBBB
		IEnumerable<TimeValue> IResolvedStyle.transitionDelay
		{
			get
			{
				return this.computedStyle.transitionDelay;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000119C8 File Offset: 0x0000FBC8
		IEnumerable<TimeValue> IResolvedStyle.transitionDuration
		{
			get
			{
				return this.computedStyle.transitionDuration;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000119D5 File Offset: 0x0000FBD5
		IEnumerable<StylePropertyName> IResolvedStyle.transitionProperty
		{
			get
			{
				return this.computedStyle.transitionProperty;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000119E2 File Offset: 0x0000FBE2
		IEnumerable<EasingFunction> IResolvedStyle.transitionTimingFunction
		{
			get
			{
				return this.computedStyle.transitionTimingFunction;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x000119EF File Offset: 0x0000FBEF
		Vector3 IResolvedStyle.translate
		{
			get
			{
				return this.ResolveTranslate();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000119F7 File Offset: 0x0000FBF7
		Color IResolvedStyle.unityBackgroundImageTintColor
		{
			get
			{
				return this.computedStyle.unityBackgroundImageTintColor;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00011A04 File Offset: 0x0000FC04
		ScaleMode IResolvedStyle.unityBackgroundScaleMode
		{
			get
			{
				return this.computedStyle.unityBackgroundScaleMode;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00011A11 File Offset: 0x0000FC11
		Font IResolvedStyle.unityFont
		{
			get
			{
				return this.computedStyle.unityFont;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00011A1E File Offset: 0x0000FC1E
		FontDefinition IResolvedStyle.unityFontDefinition
		{
			get
			{
				return this.computedStyle.unityFontDefinition;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00011A2B File Offset: 0x0000FC2B
		FontStyle IResolvedStyle.unityFontStyleAndWeight
		{
			get
			{
				return this.computedStyle.unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00011A38 File Offset: 0x0000FC38
		float IResolvedStyle.unityParagraphSpacing
		{
			get
			{
				return this.computedStyle.unityParagraphSpacing.value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00011A58 File Offset: 0x0000FC58
		int IResolvedStyle.unitySliceBottom
		{
			get
			{
				return this.computedStyle.unitySliceBottom;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00011A65 File Offset: 0x0000FC65
		int IResolvedStyle.unitySliceLeft
		{
			get
			{
				return this.computedStyle.unitySliceLeft;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00011A72 File Offset: 0x0000FC72
		int IResolvedStyle.unitySliceRight
		{
			get
			{
				return this.computedStyle.unitySliceRight;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00011A7F File Offset: 0x0000FC7F
		int IResolvedStyle.unitySliceTop
		{
			get
			{
				return this.computedStyle.unitySliceTop;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00011A8C File Offset: 0x0000FC8C
		TextAnchor IResolvedStyle.unityTextAlign
		{
			get
			{
				return this.computedStyle.unityTextAlign;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00011A99 File Offset: 0x0000FC99
		Color IResolvedStyle.unityTextOutlineColor
		{
			get
			{
				return this.computedStyle.unityTextOutlineColor;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00011AA6 File Offset: 0x0000FCA6
		float IResolvedStyle.unityTextOutlineWidth
		{
			get
			{
				return this.computedStyle.unityTextOutlineWidth;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		TextOverflowPosition IResolvedStyle.unityTextOverflowPosition
		{
			get
			{
				return this.computedStyle.unityTextOverflowPosition;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00011AC0 File Offset: 0x0000FCC0
		Visibility IResolvedStyle.visibility
		{
			get
			{
				return this.computedStyle.visibility;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00011ACD File Offset: 0x0000FCCD
		WhiteSpace IResolvedStyle.whiteSpace
		{
			get
			{
				return this.computedStyle.whiteSpace;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00011ADA File Offset: 0x0000FCDA
		float IResolvedStyle.width
		{
			get
			{
				return this.yogaNode.LayoutWidth;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00011AE8 File Offset: 0x0000FCE8
		float IResolvedStyle.wordSpacing
		{
			get
			{
				return this.computedStyle.wordSpacing.value;
			}
		}

		// Token: 0x040001A6 RID: 422
		private static uint s_NextId;

		// Token: 0x040001A7 RID: 423
		private static List<string> s_EmptyClassList = new List<string>(0);

		// Token: 0x040001A8 RID: 424
		internal static readonly PropertyName userDataPropertyKey = new PropertyName("--unity-user-data");

		// Token: 0x040001A9 RID: 425
		public static readonly string disabledUssClassName = "unity-disabled";

		// Token: 0x040001AA RID: 426
		private string m_Name;

		// Token: 0x040001AB RID: 427
		private List<string> m_ClassList;

		// Token: 0x040001AC RID: 428
		private List<KeyValuePair<PropertyName, object>> m_PropertyBag;

		// Token: 0x040001AD RID: 429
		private VisualElementFlags m_Flags;

		// Token: 0x040001AE RID: 430
		private string m_ViewDataKey;

		// Token: 0x040001AF RID: 431
		private RenderHints m_RenderHints;

		// Token: 0x040001B0 RID: 432
		internal Rect lastLayout;

		// Token: 0x040001B1 RID: 433
		internal Rect lastPseudoPadding;

		// Token: 0x040001B2 RID: 434
		internal RenderChainVEData renderChainData;

		// Token: 0x040001B3 RID: 435
		private Rect m_Layout;

		// Token: 0x040001B4 RID: 436
		private Rect m_BoundingBox;

		// Token: 0x040001B5 RID: 437
		private Rect m_WorldBoundingBox;

		// Token: 0x040001B6 RID: 438
		private Matrix4x4 m_WorldTransformCache = Matrix4x4.identity;

		// Token: 0x040001B7 RID: 439
		private Matrix4x4 m_WorldTransformInverseCache = Matrix4x4.identity;

		// Token: 0x040001B8 RID: 440
		private Rect m_WorldClip = Rect.zero;

		// Token: 0x040001B9 RID: 441
		private Rect m_WorldClipMinusGroup = Rect.zero;

		// Token: 0x040001BA RID: 442
		private bool m_WorldClipIsInfinite = false;

		// Token: 0x040001BB RID: 443
		internal static readonly Rect s_InfiniteRect = new Rect(-10000f, -10000f, 40000f, 40000f);

		// Token: 0x040001BC RID: 444
		internal PseudoStates triggerPseudoMask;

		// Token: 0x040001BD RID: 445
		internal PseudoStates dependencyPseudoMask;

		// Token: 0x040001BE RID: 446
		private PseudoStates m_PseudoStates;

		// Token: 0x040001C2 RID: 450
		internal ComputedStyle m_Style = InitialStyle.Acquire();

		// Token: 0x040001C3 RID: 451
		internal StyleVariableContext variableContext = StyleVariableContext.none;

		// Token: 0x040001C4 RID: 452
		internal int inheritedStylesHash = 0;

		// Token: 0x040001C5 RID: 453
		internal readonly uint controlid;

		// Token: 0x040001C6 RID: 454
		internal int imguiContainerDescendantCount = 0;

		// Token: 0x040001C9 RID: 457
		private ProfilerMarker k_GenerateVisualContentMarker = new ProfilerMarker("GenerateVisualContent");

		// Token: 0x040001CA RID: 458
		private VisualElement.RenderTargetMode m_SubRenderTargetMode = VisualElement.RenderTargetMode.None;

		// Token: 0x040001CB RID: 459
		private static Material s_runtimeMaterial;

		// Token: 0x040001CC RID: 460
		private Material m_defaultMaterial;

		// Token: 0x040001CD RID: 461
		private List<IValueAnimationUpdate> m_RunningAnimations;

		// Token: 0x040001CE RID: 462
		internal const string k_RootVisualContainerName = "rootVisualContainer";

		// Token: 0x040001D2 RID: 466
		private VisualElement m_PhysicalParent;

		// Token: 0x040001D3 RID: 467
		private VisualElement m_LogicalParent;

		// Token: 0x040001D4 RID: 468
		private static readonly List<VisualElement> s_EmptyList = new List<VisualElement>();

		// Token: 0x040001D5 RID: 469
		private List<VisualElement> m_Children;

		// Token: 0x040001D7 RID: 471
		private VisualTreeAsset m_VisualTreeAssetSource = null;

		// Token: 0x040001D8 RID: 472
		internal static VisualElement.CustomStyleAccess s_CustomStyleAccess = new VisualElement.CustomStyleAccess();

		// Token: 0x040001D9 RID: 473
		internal InlineStyleAccess inlineStyleAccess;

		// Token: 0x040001DA RID: 474
		internal List<StyleSheet> styleSheetList;

		// Token: 0x040001DB RID: 475
		private static readonly Regex s_InternalStyleSheetPath = new Regex("^instanceId:[-0-9]+$", 8);

		// Token: 0x040001DC RID: 476
		internal static readonly PropertyName tooltipPropertyKey = new PropertyName("--unity-tooltip");

		// Token: 0x040001DD RID: 477
		private static readonly Dictionary<Type, VisualElement.TypeData> s_TypeData = new Dictionary<Type, VisualElement.TypeData>();

		// Token: 0x040001DE RID: 478
		private VisualElement.TypeData m_TypeData;

		// Token: 0x02000080 RID: 128
		public class UxmlFactory : UxmlFactory<VisualElement, VisualElement.UxmlTraits>
		{
		}

		// Token: 0x02000081 RID: 129
		public class UxmlTraits : UnityEngine.UIElements.UxmlTraits
		{
			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00011BAC File Offset: 0x0000FDAC
			// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00011BB4 File Offset: 0x0000FDB4
			protected UxmlIntAttributeDescription focusIndex { get; set; } = new UxmlIntAttributeDescription
			{
				name = null,
				obsoleteNames = new string[] { "focus-index", "focusIndex" },
				defaultValue = -1
			};

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00011BBD File Offset: 0x0000FDBD
			// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00011BC5 File Offset: 0x0000FDC5
			protected UxmlBoolAttributeDescription focusable { get; set; } = new UxmlBoolAttributeDescription
			{
				name = "focusable",
				defaultValue = false
			};

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00011BD0 File Offset: 0x0000FDD0
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x00011BF0 File Offset: 0x0000FDF0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				bool flag = ve == null;
				if (flag)
				{
					throw new ArgumentNullException("ve");
				}
				ve.name = this.m_Name.GetValueFromBag(bag, cc);
				ve.viewDataKey = this.m_ViewDataKey.GetValueFromBag(bag, cc);
				ve.pickingMode = this.m_PickingMode.GetValueFromBag(bag, cc);
				ve.usageHints = this.m_UsageHints.GetValueFromBag(bag, cc);
				int num = 0;
				bool flag2 = this.focusIndex.TryGetValueFromBag(bag, cc, ref num);
				if (flag2)
				{
					ve.tabIndex = ((num >= 0) ? num : 0);
					ve.focusable = num >= 0;
				}
				bool flag3 = this.m_TabIndex.TryGetValueFromBag(bag, cc, ref num);
				if (flag3)
				{
					ve.tabIndex = num;
				}
				bool flag4 = false;
				bool flag5 = this.focusable.TryGetValueFromBag(bag, cc, ref flag4);
				if (flag5)
				{
					ve.focusable = flag4;
				}
				ve.tooltip = this.m_Tooltip.GetValueFromBag(bag, cc);
			}

			// Token: 0x040001DF RID: 479
			protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
			{
				name = "name"
			};

			// Token: 0x040001E0 RID: 480
			private UxmlStringAttributeDescription m_ViewDataKey = new UxmlStringAttributeDescription
			{
				name = "view-data-key"
			};

			// Token: 0x040001E1 RID: 481
			protected UxmlEnumAttributeDescription<PickingMode> m_PickingMode = new UxmlEnumAttributeDescription<PickingMode>
			{
				name = "picking-mode",
				obsoleteNames = new string[] { "pickingMode" }
			};

			// Token: 0x040001E2 RID: 482
			private UxmlStringAttributeDescription m_Tooltip = new UxmlStringAttributeDescription
			{
				name = "tooltip"
			};

			// Token: 0x040001E3 RID: 483
			private UxmlEnumAttributeDescription<UsageHints> m_UsageHints = new UxmlEnumAttributeDescription<UsageHints>
			{
				name = "usage-hints"
			};

			// Token: 0x040001E5 RID: 485
			private UxmlIntAttributeDescription m_TabIndex = new UxmlIntAttributeDescription
			{
				name = "tabindex",
				defaultValue = 0
			};

			// Token: 0x040001E7 RID: 487
			private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
			{
				name = "class"
			};

			// Token: 0x040001E8 RID: 488
			private UxmlStringAttributeDescription m_ContentContainer = new UxmlStringAttributeDescription
			{
				name = "content-container",
				obsoleteNames = new string[] { "contentContainer" }
			};

			// Token: 0x040001E9 RID: 489
			private UxmlStringAttributeDescription m_Style = new UxmlStringAttributeDescription
			{
				name = "style"
			};
		}

		// Token: 0x02000083 RID: 131
		public enum MeasureMode
		{
			// Token: 0x040001EF RID: 495
			Undefined,
			// Token: 0x040001F0 RID: 496
			Exactly,
			// Token: 0x040001F1 RID: 497
			AtMost
		}

		// Token: 0x02000084 RID: 132
		internal enum RenderTargetMode
		{
			// Token: 0x040001F3 RID: 499
			None,
			// Token: 0x040001F4 RID: 500
			NoColorConversion,
			// Token: 0x040001F5 RID: 501
			LinearToGamma,
			// Token: 0x040001F6 RID: 502
			GammaToLinear
		}

		// Token: 0x02000085 RID: 133
		public struct Hierarchy
		{
			// Token: 0x17000147 RID: 327
			// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00011F34 File Offset: 0x00010134
			public VisualElement parent
			{
				get
				{
					return this.m_Owner.m_PhysicalParent;
				}
			}

			// Token: 0x060004B5 RID: 1205 RVA: 0x00011F51 File Offset: 0x00010151
			internal Hierarchy(VisualElement element)
			{
				this.m_Owner = element;
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x00011F5C File Offset: 0x0001015C
			public void Add(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot add null child");
				}
				this.Insert(this.childCount, child);
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x00011F8C File Offset: 0x0001018C
			public void Insert(int index, VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot insert null child");
				}
				bool flag2 = index > this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				bool flag3 = child == this.m_Owner;
				if (flag3)
				{
					throw new ArgumentException("Cannot insert element as its own child");
				}
				bool flag4 = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag4)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				child.RemoveFromHierarchy();
				bool flag5 = this.m_Owner.m_Children == VisualElement.s_EmptyList;
				if (flag5)
				{
					this.m_Owner.m_Children = VisualElementListPool.Get(0);
				}
				bool isMeasureDefined = this.m_Owner.yogaNode.IsMeasureDefined;
				if (isMeasureDefined)
				{
					this.m_Owner.RemoveMeasureFunction();
				}
				this.PutChildAtIndex(child, index);
				int num = child.imguiContainerDescendantCount + (child.isIMGUIContainer ? 1 : 0);
				bool flag6 = num > 0;
				if (flag6)
				{
					this.m_Owner.ChangeIMGUIContainerCount(num);
				}
				child.hierarchy.SetParent(this.m_Owner);
				child.PropagateEnabledToChildren(this.m_Owner.enabledInHierarchy);
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				child.IncrementVersion(VersionChangeType.Hierarchy);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x000120EC File Offset: 0x000102EC
			public void Remove(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot remove null child");
				}
				bool flag2 = child.hierarchy.parent != this.m_Owner;
				if (flag2)
				{
					throw new ArgumentException("This VisualElement is not my child");
				}
				int num = this.m_Owner.m_Children.IndexOf(child);
				this.RemoveAt(num);
			}

			// Token: 0x060004B9 RID: 1209 RVA: 0x00012150 File Offset: 0x00010350
			public void RemoveAt(int index)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = index < 0 || index >= this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				VisualElement visualElement = this.m_Owner.m_Children[index];
				visualElement.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(index);
				int num = visualElement.imguiContainerDescendantCount + (visualElement.isIMGUIContainer ? 1 : 0);
				bool flag3 = num > 0;
				if (flag3)
				{
					this.m_Owner.ChangeIMGUIContainerCount(-num);
				}
				visualElement.hierarchy.SetParent(null);
				bool flag4 = this.childCount == 0;
				if (flag4)
				{
					this.ReleaseChildList();
					bool requireMeasureFunction = this.m_Owner.requireMeasureFunction;
					if (requireMeasureFunction)
					{
						this.m_Owner.AssignMeasureFunction();
					}
				}
				BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
				if (elementPanel != null)
				{
					elementPanel.OnVersionChanged(visualElement, VersionChangeType.Hierarchy);
				}
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x060004BA RID: 1210 RVA: 0x00012278 File Offset: 0x00010478
			public void Clear()
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = this.childCount > 0;
				if (flag2)
				{
					List<VisualElement> list = VisualElementListPool.Copy(this.m_Owner.m_Children);
					this.ReleaseChildList();
					this.m_Owner.yogaNode.Clear();
					bool requireMeasureFunction = this.m_Owner.requireMeasureFunction;
					if (requireMeasureFunction)
					{
						this.m_Owner.AssignMeasureFunction();
					}
					foreach (VisualElement visualElement in list)
					{
						visualElement.InvokeHierarchyChanged(HierarchyChangeType.Remove);
						visualElement.hierarchy.SetParent(null);
						visualElement.m_LogicalParent = null;
						BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
						if (elementPanel != null)
						{
							elementPanel.OnVersionChanged(visualElement, VersionChangeType.Hierarchy);
						}
					}
					bool flag3 = this.m_Owner.imguiContainerDescendantCount > 0;
					if (flag3)
					{
						int num = this.m_Owner.imguiContainerDescendantCount;
						bool isIMGUIContainer = this.m_Owner.isIMGUIContainer;
						if (isIMGUIContainer)
						{
							num--;
						}
						this.m_Owner.ChangeIMGUIContainerCount(-num);
					}
					VisualElementListPool.Release(list);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x000123E8 File Offset: 0x000105E8
			internal void BringToFront(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num >= 0 && num < this.childCount - 1;
					if (flag2)
					{
						this.MoveChildElement(child, num, this.childCount);
					}
				}
			}

			// Token: 0x060004BC RID: 1212 RVA: 0x00012440 File Offset: 0x00010640
			internal void SendToBack(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num > 0;
					if (flag2)
					{
						this.MoveChildElement(child, num, 0);
					}
				}
			}

			// Token: 0x060004BD RID: 1213 RVA: 0x00012484 File Offset: 0x00010684
			internal void PlaceBehind(VisualElement child, VisualElement over)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(over);
						bool flag3 = num2 > 0 && num < num2;
						if (flag3)
						{
							num2--;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			// Token: 0x060004BE RID: 1214 RVA: 0x000124F0 File Offset: 0x000106F0
			internal void PlaceInFront(VisualElement child, VisualElement under)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(under);
						bool flag3 = num > num2;
						if (flag3)
						{
							num2++;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			// Token: 0x060004BF RID: 1215 RVA: 0x00012558 File Offset: 0x00010758
			private void MoveChildElement(VisualElement child, int currentIndex, int nextIndex)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				child.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(currentIndex);
				this.PutChildAtIndex(child, nextIndex);
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000125C4 File Offset: 0x000107C4
			public int childCount
			{
				get
				{
					return this.m_Owner.m_Children.Count;
				}
			}

			// Token: 0x17000149 RID: 329
			public VisualElement this[int key]
			{
				get
				{
					return this.m_Owner.m_Children[key];
				}
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x0001260C File Offset: 0x0001080C
			public int IndexOf(VisualElement element)
			{
				return this.m_Owner.m_Children.IndexOf(element);
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x00012630 File Offset: 0x00010830
			public VisualElement ElementAt(int index)
			{
				return this[index];
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x0001264C File Offset: 0x0001084C
			public IEnumerable<VisualElement> Children()
			{
				return this.m_Owner.m_Children;
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x0001266C File Offset: 0x0001086C
			private void SetParent(VisualElement value)
			{
				this.m_Owner.m_PhysicalParent = value;
				this.m_Owner.m_LogicalParent = value;
				bool flag = value != null;
				if (flag)
				{
					this.m_Owner.SetPanel(this.m_Owner.m_PhysicalParent.elementPanel);
				}
				else
				{
					this.m_Owner.SetPanel(null);
				}
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x000126CC File Offset: 0x000108CC
			public void Sort(Comparison<VisualElement> comp)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = this.childCount > 1;
				if (flag2)
				{
					this.m_Owner.m_Children.Sort(comp);
					this.m_Owner.yogaNode.Clear();
					for (int i = 0; i < this.m_Owner.m_Children.Count; i++)
					{
						this.m_Owner.yogaNode.Insert(i, this.m_Owner.m_Children[i].yogaNode);
					}
					this.m_Owner.InvokeHierarchyChanged(HierarchyChangeType.Move);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x000127A4 File Offset: 0x000109A4
			private void PutChildAtIndex(VisualElement child, int index)
			{
				bool flag = index >= this.childCount;
				if (flag)
				{
					this.m_Owner.m_Children.Add(child);
					this.m_Owner.yogaNode.Insert(this.m_Owner.yogaNode.Count, child.yogaNode);
				}
				else
				{
					this.m_Owner.m_Children.Insert(index, child);
					this.m_Owner.yogaNode.Insert(index, child.yogaNode);
				}
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x0001282C File Offset: 0x00010A2C
			private void RemoveChildAtIndex(int index)
			{
				this.m_Owner.m_Children.RemoveAt(index);
				this.m_Owner.yogaNode.RemoveAt(index);
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x00012854 File Offset: 0x00010A54
			private void ReleaseChildList()
			{
				bool flag = this.m_Owner.m_Children != VisualElement.s_EmptyList;
				if (flag)
				{
					List<VisualElement> children = this.m_Owner.m_Children;
					this.m_Owner.m_Children = VisualElement.s_EmptyList;
					VisualElementListPool.Release(children);
				}
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x000128A0 File Offset: 0x00010AA0
			public bool Equals(VisualElement.Hierarchy other)
			{
				return other == this;
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x000128C0 File Offset: 0x00010AC0
			public override bool Equals(object obj)
			{
				bool flag = obj == null;
				return !flag && obj is VisualElement.Hierarchy && this.Equals((VisualElement.Hierarchy)obj);
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x000128F8 File Offset: 0x00010AF8
			public override int GetHashCode()
			{
				return (this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0;
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x00012920 File Offset: 0x00010B20
			public static bool operator ==(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return x.m_Owner == y.m_Owner;
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x00012940 File Offset: 0x00010B40
			public static bool operator !=(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return !(x == y);
			}

			// Token: 0x040001F7 RID: 503
			private const string k_InvalidHierarchyChangeMsg = "Cannot modify VisualElement hierarchy during layout calculation";

			// Token: 0x040001F8 RID: 504
			private readonly VisualElement m_Owner;
		}

		// Token: 0x02000086 RID: 134
		private abstract class BaseVisualElementScheduledItem : ScheduledItem, IVisualElementScheduledItem, IVisualElementPanelActivatable
		{
			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001295C File Offset: 0x00010B5C
			// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00012964 File Offset: 0x00010B64
			public VisualElement element { get; private set; }

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00012970 File Offset: 0x00010B70
			public bool isActive
			{
				get
				{
					return this.m_Activator.isActive;
				}
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x0001298D File Offset: 0x00010B8D
			protected BaseVisualElementScheduledItem(VisualElement handler)
			{
				this.element = handler;
				this.m_Activator = new VisualElementPanelActivator(this);
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x000129B4 File Offset: 0x00010BB4
			public IVisualElementScheduledItem StartingIn(long delayMs)
			{
				base.delayMs = delayMs;
				return this;
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x000129D0 File Offset: 0x00010BD0
			public IVisualElementScheduledItem Until(Func<bool> stopCondition)
			{
				bool flag = stopCondition == null;
				if (flag)
				{
					stopCondition = ScheduledItem.ForeverCondition;
				}
				this.timerUpdateStopCondition = stopCondition;
				return this;
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x000129FC File Offset: 0x00010BFC
			public IVisualElementScheduledItem ForDuration(long durationMs)
			{
				base.SetDuration(durationMs);
				return this;
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x00012A18 File Offset: 0x00010C18
			public IVisualElementScheduledItem Every(long intervalMs)
			{
				base.intervalMs = intervalMs;
				bool flag = this.timerUpdateStopCondition == ScheduledItem.OnceCondition;
				if (flag)
				{
					this.timerUpdateStopCondition = ScheduledItem.ForeverCondition;
				}
				return this;
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x00012A54 File Offset: 0x00010C54
			internal override void OnItemUnscheduled()
			{
				base.OnItemUnscheduled();
				this.isScheduled = false;
				bool flag = !this.m_Activator.isDetaching;
				if (flag)
				{
					this.m_Activator.SetActive(false);
				}
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x00012A91 File Offset: 0x00010C91
			public void Resume()
			{
				this.m_Activator.SetActive(true);
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x00012AA1 File Offset: 0x00010CA1
			public void Pause()
			{
				this.m_Activator.SetActive(false);
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x00012AB4 File Offset: 0x00010CB4
			public void ExecuteLater(long delayMs)
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.Resume();
				}
				base.ResetStartTime();
				this.StartingIn(delayMs);
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x00012AE8 File Offset: 0x00010CE8
			public void OnPanelActivate()
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.isScheduled = true;
					base.ResetStartTime();
					this.element.elementPanel.scheduler.Schedule(this);
				}
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x00012B2C File Offset: 0x00010D2C
			public void OnPanelDeactivate()
			{
				bool flag = this.isScheduled;
				if (flag)
				{
					this.isScheduled = false;
					this.element.elementPanel.scheduler.Unschedule(this);
				}
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x00012B64 File Offset: 0x00010D64
			public bool CanBeActivated()
			{
				return this.element != null && this.element.elementPanel != null && this.element.elementPanel.scheduler != null;
			}

			// Token: 0x040001FA RID: 506
			public bool isScheduled = false;

			// Token: 0x040001FB RID: 507
			private VisualElementPanelActivator m_Activator;
		}

		// Token: 0x02000087 RID: 135
		private abstract class VisualElementScheduledItem<ActionType> : VisualElement.BaseVisualElementScheduledItem
		{
			// Token: 0x060004DE RID: 1246 RVA: 0x00012BA1 File Offset: 0x00010DA1
			public VisualElementScheduledItem(VisualElement handler, ActionType upEvent)
				: base(handler)
			{
				this.updateEvent = upEvent;
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x00012BB4 File Offset: 0x00010DB4
			public static bool Matches(ScheduledItem item, ActionType updateEvent)
			{
				VisualElement.VisualElementScheduledItem<ActionType> visualElementScheduledItem = item as VisualElement.VisualElementScheduledItem<ActionType>;
				bool flag = visualElementScheduledItem != null;
				return flag && EqualityComparer<ActionType>.Default.Equals(visualElementScheduledItem.updateEvent, updateEvent);
			}

			// Token: 0x040001FC RID: 508
			public ActionType updateEvent;
		}

		// Token: 0x02000088 RID: 136
		private class TimerStateScheduledItem : VisualElement.VisualElementScheduledItem<Action<TimerState>>
		{
			// Token: 0x060004E0 RID: 1248 RVA: 0x00012BEB File Offset: 0x00010DEB
			public TimerStateScheduledItem(VisualElement handler, Action<TimerState> updateEvent)
				: base(handler, updateEvent)
			{
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x00012BF8 File Offset: 0x00010DF8
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent.Invoke(state);
				}
			}
		}

		// Token: 0x02000089 RID: 137
		private class SimpleScheduledItem : VisualElement.VisualElementScheduledItem<Action>
		{
			// Token: 0x060004E2 RID: 1250 RVA: 0x00012C1F File Offset: 0x00010E1F
			public SimpleScheduledItem(VisualElement handler, Action updateEvent)
				: base(handler, updateEvent)
			{
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x00012C2C File Offset: 0x00010E2C
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent.Invoke();
				}
			}
		}

		// Token: 0x0200008A RID: 138
		internal class CustomStyleAccess : ICustomStyle
		{
			// Token: 0x060004E4 RID: 1252 RVA: 0x00012C52 File Offset: 0x00010E52
			public void SetContext(Dictionary<string, StylePropertyValue> customProperties, float dpiScaling)
			{
				this.m_CustomProperties = customProperties;
				this.m_DpiScaling = dpiScaling;
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x00012C64 File Offset: 0x00010E64
			public bool TryGetValue(CustomStyleProperty<float> property, out float value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
				if (flag)
				{
					bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out value);
					if (flag2)
					{
						return true;
					}
				}
				value = 0f;
				return false;
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x00012CB0 File Offset: 0x00010EB0
			public bool TryGetValue(CustomStyleProperty<int> property, out int value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
				if (flag)
				{
					float num;
					bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out num);
					if (flag2)
					{
						value = (int)num;
						return true;
					}
				}
				value = 0;
				return false;
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x00012D00 File Offset: 0x00010F00
			public bool TryGetValue(CustomStyleProperty<bool> property, out bool value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
				bool flag2;
				if (flag)
				{
					value = stylePropertyValue.sheet.ReadKeyword(stylePropertyValue.handle) == StyleValueKeyword.True;
					flag2 = true;
				}
				else
				{
					value = false;
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x00012D54 File Offset: 0x00010F54
			public bool TryGetValue(CustomStyleProperty<Color> property, out Color value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
				if (flag)
				{
					StyleValueHandle handle = stylePropertyValue.handle;
					StyleValueType valueType = handle.valueType;
					StyleValueType styleValueType = valueType;
					if (styleValueType != StyleValueType.Color)
					{
						if (styleValueType == StyleValueType.Enum)
						{
							string text = stylePropertyValue.sheet.ReadAsString(handle);
							return StyleSheetColor.TryGetColor(text.ToLower(), out value);
						}
						VisualElement.CustomStyleAccess.LogCustomPropertyWarning(property.name, StyleValueType.Color, stylePropertyValue);
					}
					else
					{
						bool flag2 = stylePropertyValue.sheet.TryReadColor(stylePropertyValue.handle, out value);
						if (flag2)
						{
							return true;
						}
					}
				}
				value = Color.clear;
				return false;
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x00012E0C File Offset: 0x0001100C
			public bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
				if (flag)
				{
					ImageSource imageSource = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.m_DpiScaling, out imageSource) && imageSource.texture != null;
					if (flag2)
					{
						value = imageSource.texture;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x00012E84 File Offset: 0x00011084
			public bool TryGetValue(CustomStyleProperty<Sprite> property, out Sprite value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
				if (flag)
				{
					ImageSource imageSource = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.m_DpiScaling, out imageSource) && imageSource.sprite != null;
					if (flag2)
					{
						value = imageSource.sprite;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x00012EFC File Offset: 0x000110FC
			public bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
				if (flag)
				{
					ImageSource imageSource = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.m_DpiScaling, out imageSource) && imageSource.vectorImage != null;
					if (flag2)
					{
						value = imageSource.vectorImage;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x00012F74 File Offset: 0x00011174
			public bool TryGetValue<T>(CustomStyleProperty<T> property, out T value) where T : Object
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
				if (flag)
				{
					Object @object;
					bool flag2 = stylePropertyValue.sheet.TryReadAssetReference(stylePropertyValue.handle, out @object);
					if (flag2)
					{
						value = @object as T;
						return value != null;
					}
				}
				value = default(T);
				return false;
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x00012FF4 File Offset: 0x000111F4
			public bool TryGetValue(CustomStyleProperty<string> property, out string value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, ref stylePropertyValue);
				bool flag2;
				if (flag)
				{
					value = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
					flag2 = true;
				}
				else
				{
					value = string.Empty;
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x0001304C File Offset: 0x0001124C
			private bool TryGetValue(string propertyName, StyleValueType valueType, out StylePropertyValue customProp)
			{
				customProp = default(StylePropertyValue);
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(propertyName, ref customProp);
				bool flag3;
				if (flag)
				{
					StyleValueHandle handle = customProp.handle;
					bool flag2 = handle.valueType != valueType;
					if (flag2)
					{
						VisualElement.CustomStyleAccess.LogCustomPropertyWarning(propertyName, valueType, customProp);
						flag3 = false;
					}
					else
					{
						flag3 = true;
					}
				}
				else
				{
					flag3 = false;
				}
				return flag3;
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x000130B2 File Offset: 0x000112B2
			private static void LogCustomPropertyWarning(string propertyName, StyleValueType valueType, StylePropertyValue customProp)
			{
				Debug.LogWarning(string.Format("Trying to read custom property {0} value as {1} while parsed type is {2}", propertyName, valueType, customProp.handle.valueType));
			}

			// Token: 0x040001FD RID: 509
			private Dictionary<string, StylePropertyValue> m_CustomProperties;

			// Token: 0x040001FE RID: 510
			private float m_DpiScaling;
		}

		// Token: 0x0200008B RID: 139
		private class TypeData
		{
			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000130DD File Offset: 0x000112DD
			public Type type { get; }

			// Token: 0x060004F2 RID: 1266 RVA: 0x000130E5 File Offset: 0x000112E5
			public TypeData(Type type)
			{
				this.type = type;
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001310C File Offset: 0x0001130C
			public string fullTypeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_FullTypeName);
					if (flag)
					{
						this.m_FullTypeName = this.type.FullName;
					}
					return this.m_FullTypeName;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00013144 File Offset: 0x00011344
			public string typeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_TypeName);
					if (flag)
					{
						bool isGenericType = this.type.IsGenericType;
						this.m_TypeName = this.type.Name;
						bool flag2 = isGenericType;
						if (flag2)
						{
							int num = this.m_TypeName.IndexOf('`');
							bool flag3 = num >= 0;
							if (flag3)
							{
								this.m_TypeName = this.m_TypeName.Remove(num);
							}
						}
					}
					return this.m_TypeName;
				}
			}

			// Token: 0x04000200 RID: 512
			private string m_FullTypeName = string.Empty;

			// Token: 0x04000201 RID: 513
			private string m_TypeName = string.Empty;
		}
	}
}
