using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000019 RID: 25
	[AddComponentMenu("Layout/Aspect Ratio Fitter", 142)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class AspectRatioFitter : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000CD07 File Offset: 0x0000AF07
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000CD0F File Offset: 0x0000AF0F
		public AspectRatioFitter.AspectMode aspectMode
		{
			get
			{
				return this.m_AspectMode;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<AspectRatioFitter.AspectMode>(ref this.m_AspectMode, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000CD25 File Offset: 0x0000AF25
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000CD2D File Offset: 0x0000AF2D
		public float aspectRatio
		{
			get
			{
				return this.m_AspectRatio;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_AspectRatio, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000CD43 File Offset: 0x0000AF43
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000CD65 File Offset: 0x0000AF65
		protected AspectRatioFitter()
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000CD78 File Offset: 0x0000AF78
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_DoesParentExist = this.rectTransform.parent;
			this.SetDirty();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
		protected override void Start()
		{
			base.Start();
			if (!this.IsComponentValidOnObject() || !this.IsAspectModeValid())
			{
				base.enabled = false;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000CDC1 File Offset: 0x0000AFC1
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000CDDF File Offset: 0x0000AFDF
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_DoesParentExist = this.rectTransform.parent;
			this.SetDirty();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000CE09 File Offset: 0x0000B009
		protected virtual void Update()
		{
			if (this.m_DelayedSetDirty)
			{
				this.m_DelayedSetDirty = false;
				this.SetDirty();
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000CE20 File Offset: 0x0000B020
		protected override void OnRectTransformDimensionsChange()
		{
			this.UpdateRect();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000CE28 File Offset: 0x0000B028
		private void UpdateRect()
		{
			if (!this.IsActive() || !this.IsComponentValidOnObject())
			{
				return;
			}
			this.m_Tracker.Clear();
			switch (this.m_AspectMode)
			{
			case AspectRatioFitter.AspectMode.WidthControlsHeight:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.rectTransform.rect.width / this.m_AspectRatio);
				return;
			case AspectRatioFitter.AspectMode.HeightControlsWidth:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaX);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.rectTransform.rect.height * this.m_AspectRatio);
				return;
			case AspectRatioFitter.AspectMode.FitInParent:
			case AspectRatioFitter.AspectMode.EnvelopeParent:
				if (this.DoesParentExists())
				{
					this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					this.rectTransform.anchorMin = Vector2.zero;
					this.rectTransform.anchorMax = Vector2.one;
					this.rectTransform.anchoredPosition = Vector2.zero;
					Vector2 zero = Vector2.zero;
					Vector2 parentSize = this.GetParentSize();
					if ((parentSize.y * this.aspectRatio < parentSize.x) ^ (this.m_AspectMode == AspectRatioFitter.AspectMode.FitInParent))
					{
						zero.y = this.GetSizeDeltaToProduceSize(parentSize.x / this.aspectRatio, 1);
					}
					else
					{
						zero.x = this.GetSizeDeltaToProduceSize(parentSize.y * this.aspectRatio, 0);
					}
					this.rectTransform.sizeDelta = zero;
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		private float GetSizeDeltaToProduceSize(float size, int axis)
		{
			return size - this.GetParentSize()[axis] * (this.rectTransform.anchorMax[axis] - this.rectTransform.anchorMin[axis]);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = this.rectTransform.parent as RectTransform;
			if (rectTransform)
			{
				return rectTransform.rect.size;
			}
			return Vector2.zero;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000D036 File Offset: 0x0000B236
		public virtual void SetLayoutHorizontal()
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000D038 File Offset: 0x0000B238
		public virtual void SetLayoutVertical()
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000D03A File Offset: 0x0000B23A
		protected void SetDirty()
		{
			this.UpdateRect();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000D044 File Offset: 0x0000B244
		public bool IsComponentValidOnObject()
		{
			Canvas component = base.gameObject.GetComponent<Canvas>();
			return !component || !component.isRootCanvas || component.renderMode == RenderMode.WorldSpace;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000D079 File Offset: 0x0000B279
		public bool IsAspectModeValid()
		{
			return this.DoesParentExists() || (this.aspectMode != AspectRatioFitter.AspectMode.EnvelopeParent && this.aspectMode != AspectRatioFitter.AspectMode.FitInParent);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000D098 File Offset: 0x0000B298
		private bool DoesParentExists()
		{
			return this.m_DoesParentExist;
		}

		// Token: 0x040000BC RID: 188
		[SerializeField]
		private AspectRatioFitter.AspectMode m_AspectMode;

		// Token: 0x040000BD RID: 189
		[SerializeField]
		private float m_AspectRatio = 1f;

		// Token: 0x040000BE RID: 190
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040000BF RID: 191
		private bool m_DelayedSetDirty;

		// Token: 0x040000C0 RID: 192
		private bool m_DoesParentExist;

		// Token: 0x040000C1 RID: 193
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000096 RID: 150
		public enum AspectMode
		{
			// Token: 0x040002A5 RID: 677
			None,
			// Token: 0x040002A6 RID: 678
			WidthControlsHeight,
			// Token: 0x040002A7 RID: 679
			HeightControlsWidth,
			// Token: 0x040002A8 RID: 680
			FitInParent,
			// Token: 0x040002A9 RID: 681
			EnvelopeParent
		}
	}
}
