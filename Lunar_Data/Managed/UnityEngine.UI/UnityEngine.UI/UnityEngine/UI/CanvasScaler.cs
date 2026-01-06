using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200001A RID: 26
	[RequireComponent(typeof(Canvas))]
	[ExecuteAlways]
	[AddComponentMenu("Layout/Canvas Scaler", 101)]
	[DisallowMultipleComponent]
	public class CanvasScaler : UIBehaviour
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		public CanvasScaler.ScaleMode uiScaleMode
		{
			get
			{
				return this.m_UiScaleMode;
			}
			set
			{
				this.m_UiScaleMode = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000D0B1 File Offset: 0x0000B2B1
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		public float referencePixelsPerUnit
		{
			get
			{
				return this.m_ReferencePixelsPerUnit;
			}
			set
			{
				this.m_ReferencePixelsPerUnit = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000D0C2 File Offset: 0x0000B2C2
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000D0CA File Offset: 0x0000B2CA
		public float scaleFactor
		{
			get
			{
				return this.m_ScaleFactor;
			}
			set
			{
				this.m_ScaleFactor = Mathf.Max(0.01f, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000D0DD File Offset: 0x0000B2DD
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		public Vector2 referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
				if (this.m_ReferenceResolution.x > -1E-05f && this.m_ReferenceResolution.x < 1E-05f)
				{
					this.m_ReferenceResolution.x = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.x);
				}
				if (this.m_ReferenceResolution.y > -1E-05f && this.m_ReferenceResolution.y < 1E-05f)
				{
					this.m_ReferenceResolution.y = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.y);
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000D186 File Offset: 0x0000B386
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000D18E File Offset: 0x0000B38E
		public CanvasScaler.ScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000D197 File Offset: 0x0000B397
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000D19F File Offset: 0x0000B39F
		public float matchWidthOrHeight
		{
			get
			{
				return this.m_MatchWidthOrHeight;
			}
			set
			{
				this.m_MatchWidthOrHeight = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		public CanvasScaler.Unit physicalUnit
		{
			get
			{
				return this.m_PhysicalUnit;
			}
			set
			{
				this.m_PhysicalUnit = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000D1B9 File Offset: 0x0000B3B9
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000D1C1 File Offset: 0x0000B3C1
		public float fallbackScreenDPI
		{
			get
			{
				return this.m_FallbackScreenDPI;
			}
			set
			{
				this.m_FallbackScreenDPI = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000D1CA File Offset: 0x0000B3CA
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000D1D2 File Offset: 0x0000B3D2
		public float defaultSpriteDPI
		{
			get
			{
				return this.m_DefaultSpriteDPI;
			}
			set
			{
				this.m_DefaultSpriteDPI = Mathf.Max(1f, value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000D1E5 File Offset: 0x0000B3E5
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000D1ED File Offset: 0x0000B3ED
		public float dynamicPixelsPerUnit
		{
			get
			{
				return this.m_DynamicPixelsPerUnit;
			}
			set
			{
				this.m_DynamicPixelsPerUnit = value;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		protected CanvasScaler()
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D274 File Offset: 0x0000B474
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_Canvas = base.GetComponent<Canvas>();
			this.Handle();
			Canvas.preWillRenderCanvases += this.Canvas_preWillRenderCanvases;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000D29F File Offset: 0x0000B49F
		private void Canvas_preWillRenderCanvases()
		{
			this.Handle();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000D2A7 File Offset: 0x0000B4A7
		protected override void OnDisable()
		{
			this.SetScaleFactor(1f);
			this.SetReferencePixelsPerUnit(100f);
			Canvas.preWillRenderCanvases -= this.Canvas_preWillRenderCanvases;
			base.OnDisable();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		protected virtual void Handle()
		{
			if (this.m_Canvas == null || !this.m_Canvas.isRootCanvas)
			{
				return;
			}
			if (this.m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				this.HandleWorldCanvas();
				return;
			}
			switch (this.m_UiScaleMode)
			{
			case CanvasScaler.ScaleMode.ConstantPixelSize:
				this.HandleConstantPixelSize();
				return;
			case CanvasScaler.ScaleMode.ScaleWithScreenSize:
				this.HandleScaleWithScreenSize();
				return;
			case CanvasScaler.ScaleMode.ConstantPhysicalSize:
				this.HandleConstantPhysicalSize();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D344 File Offset: 0x0000B544
		protected virtual void HandleWorldCanvas()
		{
			this.SetScaleFactor(this.m_DynamicPixelsPerUnit);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D35E File Offset: 0x0000B55E
		protected virtual void HandleConstantPixelSize()
		{
			this.SetScaleFactor(this.m_ScaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D378 File Offset: 0x0000B578
		protected virtual void HandleScaleWithScreenSize()
		{
			Vector2 renderingDisplaySize = this.m_Canvas.renderingDisplaySize;
			int targetDisplay = this.m_Canvas.targetDisplay;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				Display display = Display.displays[targetDisplay];
				renderingDisplaySize = new Vector2((float)display.renderingWidth, (float)display.renderingHeight);
			}
			float num = 0f;
			switch (this.m_ScreenMatchMode)
			{
			case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
			{
				float num2 = Mathf.Log(renderingDisplaySize.x / this.m_ReferenceResolution.x, 2f);
				float num3 = Mathf.Log(renderingDisplaySize.y / this.m_ReferenceResolution.y, 2f);
				float num4 = Mathf.Lerp(num2, num3, this.m_MatchWidthOrHeight);
				num = Mathf.Pow(2f, num4);
				break;
			}
			case CanvasScaler.ScreenMatchMode.Expand:
				num = Mathf.Min(renderingDisplaySize.x / this.m_ReferenceResolution.x, renderingDisplaySize.y / this.m_ReferenceResolution.y);
				break;
			case CanvasScaler.ScreenMatchMode.Shrink:
				num = Mathf.Max(renderingDisplaySize.x / this.m_ReferenceResolution.x, renderingDisplaySize.y / this.m_ReferenceResolution.y);
				break;
			}
			this.SetScaleFactor(num);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		protected virtual void HandleConstantPhysicalSize()
		{
			float dpi = Screen.dpi;
			float num = ((dpi == 0f) ? this.m_FallbackScreenDPI : dpi);
			float num2 = 1f;
			switch (this.m_PhysicalUnit)
			{
			case CanvasScaler.Unit.Centimeters:
				num2 = 2.54f;
				break;
			case CanvasScaler.Unit.Millimeters:
				num2 = 25.4f;
				break;
			case CanvasScaler.Unit.Inches:
				num2 = 1f;
				break;
			case CanvasScaler.Unit.Points:
				num2 = 72f;
				break;
			case CanvasScaler.Unit.Picas:
				num2 = 6f;
				break;
			}
			this.SetScaleFactor(num / num2);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit * num2 / this.m_DefaultSpriteDPI);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D542 File Offset: 0x0000B742
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == this.m_PrevScaleFactor)
			{
				return;
			}
			this.m_Canvas.scaleFactor = scaleFactor;
			this.m_PrevScaleFactor = scaleFactor;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D561 File Offset: 0x0000B761
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == this.m_PrevReferencePixelsPerUnit)
			{
				return;
			}
			this.m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			this.m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}

		// Token: 0x040000C2 RID: 194
		[Tooltip("Determines how UI elements in the Canvas are scaled.")]
		[SerializeField]
		private CanvasScaler.ScaleMode m_UiScaleMode;

		// Token: 0x040000C3 RID: 195
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		[SerializeField]
		protected float m_ReferencePixelsPerUnit = 100f;

		// Token: 0x040000C4 RID: 196
		[Tooltip("Scales all UI elements in the Canvas by this factor.")]
		[SerializeField]
		protected float m_ScaleFactor = 1f;

		// Token: 0x040000C5 RID: 197
		[Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
		[SerializeField]
		protected Vector2 m_ReferenceResolution = new Vector2(800f, 600f);

		// Token: 0x040000C6 RID: 198
		[Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
		[SerializeField]
		protected CanvasScaler.ScreenMatchMode m_ScreenMatchMode;

		// Token: 0x040000C7 RID: 199
		[Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
		[Range(0f, 1f)]
		[SerializeField]
		protected float m_MatchWidthOrHeight;

		// Token: 0x040000C8 RID: 200
		private const float kLogBase = 2f;

		// Token: 0x040000C9 RID: 201
		[Tooltip("The physical unit to specify positions and sizes in.")]
		[SerializeField]
		protected CanvasScaler.Unit m_PhysicalUnit = CanvasScaler.Unit.Points;

		// Token: 0x040000CA RID: 202
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		[SerializeField]
		protected float m_FallbackScreenDPI = 96f;

		// Token: 0x040000CB RID: 203
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[SerializeField]
		protected float m_DefaultSpriteDPI = 96f;

		// Token: 0x040000CC RID: 204
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		[SerializeField]
		protected float m_DynamicPixelsPerUnit = 1f;

		// Token: 0x040000CD RID: 205
		private Canvas m_Canvas;

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		private float m_PrevScaleFactor = 1f;

		// Token: 0x040000CF RID: 207
		[NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		protected bool m_PresetInfoIsWorld;

		// Token: 0x02000097 RID: 151
		public enum ScaleMode
		{
			// Token: 0x040002AB RID: 683
			ConstantPixelSize,
			// Token: 0x040002AC RID: 684
			ScaleWithScreenSize,
			// Token: 0x040002AD RID: 685
			ConstantPhysicalSize
		}

		// Token: 0x02000098 RID: 152
		public enum ScreenMatchMode
		{
			// Token: 0x040002AF RID: 687
			MatchWidthOrHeight,
			// Token: 0x040002B0 RID: 688
			Expand,
			// Token: 0x040002B1 RID: 689
			Shrink
		}

		// Token: 0x02000099 RID: 153
		public enum Unit
		{
			// Token: 0x040002B3 RID: 691
			Centimeters,
			// Token: 0x040002B4 RID: 692
			Millimeters,
			// Token: 0x040002B5 RID: 693
			Inches,
			// Token: 0x040002B6 RID: 694
			Points,
			// Token: 0x040002B7 RID: 695
			Picas
		}
	}
}
