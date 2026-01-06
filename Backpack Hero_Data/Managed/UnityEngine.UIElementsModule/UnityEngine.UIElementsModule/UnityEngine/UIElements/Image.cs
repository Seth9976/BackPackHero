using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000143 RID: 323
	public class Image : VisualElement
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00028DB4 File Offset: 0x00026FB4
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x00028DCC File Offset: 0x00026FCC
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				bool flag = value != null && (this.m_Sprite != null || this.m_VectorImage != null);
				if (flag)
				{
					string text = ((this.m_Sprite != null) ? "sprite" : "vector image");
					Debug.LogWarning("Image object already has a background, removing " + text);
					this.m_Sprite = null;
					this.m_VectorImage = null;
				}
				this.m_ImageIsInline = value != null;
				bool flag2 = this.m_Image != value;
				if (flag2)
				{
					this.m_Image = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					bool flag3 = this.m_Image == null;
					if (flag3)
					{
						this.m_UV = new Rect(0f, 0f, 1f, 1f);
					}
				}
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00028EA8 File Offset: 0x000270A8
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00028EC0 File Offset: 0x000270C0
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				bool flag = value != null && (this.m_Image != null || this.m_VectorImage != null);
				if (flag)
				{
					string text = ((this.m_Image != null) ? "texture" : "vector image");
					Debug.LogWarning("Image object already has a background, removing " + text);
					this.m_Image = null;
					this.m_VectorImage = null;
				}
				this.m_ImageIsInline = value != null;
				bool flag2 = this.m_Sprite != value;
				if (flag2)
				{
					this.m_Sprite = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00028F6C File Offset: 0x0002716C
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00028F84 File Offset: 0x00027184
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = value != null && (this.m_Image != null || this.m_Sprite != null);
				if (flag)
				{
					string text = ((this.m_Image != null) ? "texture" : "sprite");
					Debug.LogWarning("Image object already has a background, removing " + text);
					this.m_Image = null;
					this.m_Sprite = null;
				}
				this.m_ImageIsInline = value != null;
				bool flag2 = this.m_VectorImage != value;
				if (flag2)
				{
					this.m_VectorImage = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					bool flag3 = this.m_VectorImage == null;
					if (flag3)
					{
						this.m_UV = new Rect(0f, 0f, 1f, 1f);
					}
				}
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00029060 File Offset: 0x00027260
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x00029078 File Offset: 0x00027278
		public Rect sourceRect
		{
			get
			{
				return this.GetSourceRect();
			}
			set
			{
				bool flag = this.sprite != null;
				if (flag)
				{
					Debug.LogError("Cannot set sourceRect on a sprite image");
				}
				else
				{
					this.CalculateUV(value);
				}
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x000290AC File Offset: 0x000272AC
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x000290C4 File Offset: 0x000272C4
		public Rect uv
		{
			get
			{
				return this.m_UV;
			}
			set
			{
				this.m_UV = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x000290D0 File Offset: 0x000272D0
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x000290E8 File Offset: 0x000272E8
		public ScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				this.m_ScaleModeIsInline = true;
				this.SetScaleMode(value);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x000290FC File Offset: 0x000272FC
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00029114 File Offset: 0x00027314
		public Color tintColor
		{
			get
			{
				return this.m_TintColor;
			}
			set
			{
				this.m_TintColorIsInline = true;
				bool flag = this.m_TintColor != value;
				if (flag)
				{
					this.m_TintColor = value;
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00029150 File Offset: 0x00027350
		public Image()
		{
			base.AddToClassList(Image.ussClassName);
			this.m_ScaleMode = ScaleMode.ScaleToFit;
			this.m_TintColor = Color.white;
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			base.requireMeasureFunction = true;
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000291E4 File Offset: 0x000273E4
		private Vector2 GetTextureDisplaySize(Texture texture)
		{
			Vector2 zero = Vector2.zero;
			bool flag = texture != null;
			if (flag)
			{
				zero = new Vector2((float)texture.width, (float)texture.height);
			}
			return zero;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00029220 File Offset: 0x00027420
		private Vector2 GetTextureDisplaySize(Sprite sprite)
		{
			Vector2 vector = Vector2.zero;
			bool flag = sprite != null;
			if (flag)
			{
				vector = sprite.bounds.size * sprite.pixelsPerUnit;
			}
			return vector;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00029264 File Offset: 0x00027464
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = this.image == null && this.sprite == null && this.vectorImage == null;
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(num, num2);
			}
			else
			{
				Vector2 vector2 = Vector2.zero;
				bool flag2 = this.image != null;
				if (flag2)
				{
					vector2 = this.GetTextureDisplaySize(this.image);
				}
				else
				{
					bool flag3 = this.sprite != null;
					if (flag3)
					{
						vector2 = this.GetTextureDisplaySize(this.sprite);
					}
					else
					{
						vector2 = this.vectorImage.size;
					}
				}
				Rect sourceRect = this.sourceRect;
				bool flag4 = sourceRect != Rect.zero;
				num = (flag4 ? Mathf.Abs(sourceRect.width) : vector2.x);
				num2 = (flag4 ? Mathf.Abs(sourceRect.height) : vector2.y);
				bool flag5 = widthMode == VisualElement.MeasureMode.AtMost;
				if (flag5)
				{
					num = Mathf.Min(num, desiredWidth);
				}
				bool flag6 = heightMode == VisualElement.MeasureMode.AtMost;
				if (flag6)
				{
					num2 = Mathf.Min(num2, desiredHeight);
				}
				vector = new Vector2(num, num2);
			}
			return vector;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00029390 File Offset: 0x00027590
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.image == null && this.sprite == null && this.vectorImage == null;
			if (!flag)
			{
				MeshGenerationContextUtils.RectangleParams rectangleParams = default(MeshGenerationContextUtils.RectangleParams);
				bool flag2 = this.image != null;
				if (flag2)
				{
					rectangleParams = MeshGenerationContextUtils.RectangleParams.MakeTextured(base.contentRect, this.uv, this.image, this.scaleMode, base.panel.contextType);
				}
				else
				{
					bool flag3 = this.sprite != null;
					if (flag3)
					{
						Vector4 zero = Vector4.zero;
						rectangleParams = MeshGenerationContextUtils.RectangleParams.MakeSprite(base.contentRect, this.uv, this.sprite, this.scaleMode, base.panel.contextType, false, ref zero);
					}
					else
					{
						bool flag4 = this.vectorImage != null;
						if (flag4)
						{
							rectangleParams = MeshGenerationContextUtils.RectangleParams.MakeVectorTextured(base.contentRect, this.uv, this.vectorImage, this.scaleMode, base.panel.contextType);
						}
					}
				}
				rectangleParams.color = this.tintColor;
				mgc.Rectangle(rectangleParams);
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000294B0 File Offset: 0x000276B0
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			Texture2D texture2D = null;
			Sprite sprite = null;
			VectorImage vectorImage = null;
			Color white = Color.white;
			ICustomStyle customStyle = e.customStyle;
			bool flag = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_ImageProperty, out texture2D);
			if (flag)
			{
				this.m_Image = texture2D;
				this.m_Sprite = null;
				this.m_VectorImage = null;
			}
			bool flag2 = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_SpriteProperty, out sprite);
			if (flag2)
			{
				this.m_Image = null;
				this.m_Sprite = sprite;
				this.m_VectorImage = null;
			}
			bool flag3 = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_VectorImageProperty, out vectorImage);
			if (flag3)
			{
				this.m_Image = null;
				this.m_Sprite = null;
				this.m_VectorImage = vectorImage;
			}
			string text;
			bool flag4 = !this.m_ScaleModeIsInline && customStyle.TryGetValue(Image.s_ScaleModeProperty, out text);
			if (flag4)
			{
				int num;
				StylePropertyUtil.TryGetEnumIntValue(StyleEnumType.ScaleMode, text, out num);
				this.SetScaleMode((ScaleMode)num);
			}
			bool flag5 = !this.m_TintColorIsInline && customStyle.TryGetValue(Image.s_TintColorProperty, out white);
			if (flag5)
			{
				this.m_TintColor = white;
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000295D4 File Offset: 0x000277D4
		private void SetScaleMode(ScaleMode mode)
		{
			bool flag = this.m_ScaleMode != mode;
			if (flag)
			{
				this.m_ScaleMode = mode;
				base.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00029608 File Offset: 0x00027808
		private void CalculateUV(Rect srcRect)
		{
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				this.m_UV.x = srcRect.x / vector.x;
				this.m_UV.width = srcRect.width / vector.x;
				this.m_UV.height = srcRect.height / vector.y;
				this.m_UV.y = 1f - this.m_UV.height - srcRect.y / vector.y;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00029700 File Offset: 0x00027900
		private Rect GetSourceRect()
		{
			Rect zero = Rect.zero;
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				zero.x = this.uv.x * vector.x;
				zero.width = this.uv.width * vector.x;
				zero.y = (1f - this.uv.y - this.uv.height) * vector.y;
				zero.height = this.uv.height * vector.y;
			}
			return zero;
		}

		// Token: 0x04000480 RID: 1152
		private ScaleMode m_ScaleMode;

		// Token: 0x04000481 RID: 1153
		private Texture m_Image;

		// Token: 0x04000482 RID: 1154
		private Sprite m_Sprite;

		// Token: 0x04000483 RID: 1155
		private VectorImage m_VectorImage;

		// Token: 0x04000484 RID: 1156
		private Rect m_UV;

		// Token: 0x04000485 RID: 1157
		private Color m_TintColor;

		// Token: 0x04000486 RID: 1158
		private bool m_ImageIsInline;

		// Token: 0x04000487 RID: 1159
		private bool m_ScaleModeIsInline;

		// Token: 0x04000488 RID: 1160
		private bool m_TintColorIsInline;

		// Token: 0x04000489 RID: 1161
		public static readonly string ussClassName = "unity-image";

		// Token: 0x0400048A RID: 1162
		private static CustomStyleProperty<Texture2D> s_ImageProperty = new CustomStyleProperty<Texture2D>("--unity-image");

		// Token: 0x0400048B RID: 1163
		private static CustomStyleProperty<Sprite> s_SpriteProperty = new CustomStyleProperty<Sprite>("--unity-image");

		// Token: 0x0400048C RID: 1164
		private static CustomStyleProperty<VectorImage> s_VectorImageProperty = new CustomStyleProperty<VectorImage>("--unity-image");

		// Token: 0x0400048D RID: 1165
		private static CustomStyleProperty<string> s_ScaleModeProperty = new CustomStyleProperty<string>("--unity-image-size");

		// Token: 0x0400048E RID: 1166
		private static CustomStyleProperty<Color> s_TintColorProperty = new CustomStyleProperty<Color>("--unity-image-tint-color");

		// Token: 0x02000144 RID: 324
		public new class UxmlFactory : UxmlFactory<Image, Image.UxmlTraits>
		{
		}

		// Token: 0x02000145 RID: 325
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00029868 File Offset: 0x00027A68
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}
		}
	}
}
