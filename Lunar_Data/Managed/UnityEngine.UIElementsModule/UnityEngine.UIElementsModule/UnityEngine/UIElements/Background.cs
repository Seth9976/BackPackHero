using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000268 RID: 616
	public struct Background : IEquatable<Background>
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x000487D8 File Offset: 0x000469D8
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x000487F0 File Offset: 0x000469F0
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				bool flag = this.m_Texture == value;
				if (!flag)
				{
					this.m_Texture = value;
					this.m_Sprite = null;
					this.m_RenderTexture = null;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0004882C File Offset: 0x00046A2C
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x00048844 File Offset: 0x00046A44
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				bool flag = this.m_Sprite == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = value;
					this.m_RenderTexture = null;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00048880 File Offset: 0x00046A80
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x00048898 File Offset: 0x00046A98
		public RenderTexture renderTexture
		{
			get
			{
				return this.m_RenderTexture;
			}
			set
			{
				bool flag = this.m_RenderTexture == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = null;
					this.m_RenderTexture = value;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x000488D4 File Offset: 0x00046AD4
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x000488EC File Offset: 0x00046AEC
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = this.vectorImage == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = null;
					this.m_RenderTexture = null;
					this.m_VectorImage = value;
				}
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00048928 File Offset: 0x00046B28
		[Obsolete("Use Background.FromTexture2D instead")]
		public Background(Texture2D t)
		{
			this.m_Texture = t;
			this.m_Sprite = null;
			this.m_RenderTexture = null;
			this.m_VectorImage = null;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00048948 File Offset: 0x00046B48
		public static Background FromTexture2D(Texture2D t)
		{
			return new Background
			{
				texture = t
			};
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0004896C File Offset: 0x00046B6C
		public static Background FromRenderTexture(RenderTexture rt)
		{
			return new Background
			{
				renderTexture = rt
			};
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00048990 File Offset: 0x00046B90
		public static Background FromSprite(Sprite s)
		{
			return new Background
			{
				sprite = s
			};
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x000489B4 File Offset: 0x00046BB4
		public static Background FromVectorImage(VectorImage vi)
		{
			return new Background
			{
				vectorImage = vi
			};
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x000489D8 File Offset: 0x00046BD8
		internal static Background FromObject(object obj)
		{
			Texture2D texture2D = obj as Texture2D;
			bool flag = texture2D != null;
			Background background;
			if (flag)
			{
				background = Background.FromTexture2D(texture2D);
			}
			else
			{
				RenderTexture renderTexture = obj as RenderTexture;
				bool flag2 = renderTexture != null;
				if (flag2)
				{
					background = Background.FromRenderTexture(renderTexture);
				}
				else
				{
					Sprite sprite = obj as Sprite;
					bool flag3 = sprite != null;
					if (flag3)
					{
						background = Background.FromSprite(sprite);
					}
					else
					{
						VectorImage vectorImage = obj as VectorImage;
						bool flag4 = vectorImage != null;
						if (flag4)
						{
							background = Background.FromVectorImage(vectorImage);
						}
						else
						{
							background = default(Background);
						}
					}
				}
			}
			return background;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00048A70 File Offset: 0x00046C70
		public static bool operator ==(Background lhs, Background rhs)
		{
			return lhs.texture == rhs.texture && lhs.sprite == rhs.sprite && lhs.renderTexture == rhs.renderTexture && lhs.vectorImage == rhs.vectorImage;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00048AD8 File Offset: 0x00046CD8
		public static bool operator !=(Background lhs, Background rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00048AF4 File Offset: 0x00046CF4
		public bool Equals(Background other)
		{
			return other == this;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00048B14 File Offset: 0x00046D14
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Background);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Background background = (Background)obj;
				flag2 = background == this;
			}
			return flag2;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00048B50 File Offset: 0x00046D50
		public override int GetHashCode()
		{
			int num = 851985039;
			bool flag = this.texture != null;
			if (flag)
			{
				num = num * -1521134295 + this.texture.GetHashCode();
			}
			bool flag2 = this.sprite != null;
			if (flag2)
			{
				num = num * -1521134295 + this.sprite.GetHashCode();
			}
			bool flag3 = this.renderTexture != null;
			if (flag3)
			{
				num = num * -1521134295 + this.renderTexture.GetHashCode();
			}
			bool flag4 = this.vectorImage != null;
			if (flag4)
			{
				num = num * -1521134295 + this.vectorImage.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00048BF4 File Offset: 0x00046DF4
		public override string ToString()
		{
			bool flag = this.texture != null;
			string text;
			if (flag)
			{
				text = this.texture.ToString();
			}
			else
			{
				bool flag2 = this.sprite != null;
				if (flag2)
				{
					text = this.sprite.ToString();
				}
				else
				{
					bool flag3 = this.renderTexture != null;
					if (flag3)
					{
						text = this.renderTexture.ToString();
					}
					else
					{
						bool flag4 = this.vectorImage != null;
						if (flag4)
						{
							text = this.vectorImage.ToString();
						}
						else
						{
							text = "";
						}
					}
				}
			}
			return text;
		}

		// Token: 0x0400088B RID: 2187
		private Texture2D m_Texture;

		// Token: 0x0400088C RID: 2188
		private Sprite m_Sprite;

		// Token: 0x0400088D RID: 2189
		private RenderTexture m_RenderTexture;

		// Token: 0x0400088E RID: 2190
		private VectorImage m_VectorImage;
	}
}
