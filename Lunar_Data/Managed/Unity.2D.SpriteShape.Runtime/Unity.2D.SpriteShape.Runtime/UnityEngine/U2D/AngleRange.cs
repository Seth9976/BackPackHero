using System;
using System.Collections.Generic;

namespace UnityEngine.U2D
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class AngleRange : ICloneable
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000728A File Offset: 0x0000548A
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00007292 File Offset: 0x00005492
		public float start
		{
			get
			{
				return this.m_Start;
			}
			set
			{
				this.m_Start = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000729B File Offset: 0x0000549B
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000072A3 File Offset: 0x000054A3
		public float end
		{
			get
			{
				return this.m_End;
			}
			set
			{
				this.m_End = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000072AC File Offset: 0x000054AC
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000072B4 File Offset: 0x000054B4
		public int order
		{
			get
			{
				return this.m_Order;
			}
			set
			{
				this.m_Order = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000072BD File Offset: 0x000054BD
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000072C5 File Offset: 0x000054C5
		public List<Sprite> sprites
		{
			get
			{
				return this.m_Sprites;
			}
			set
			{
				this.m_Sprites = value;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000072CE File Offset: 0x000054CE
		public object Clone()
		{
			AngleRange angleRange = base.MemberwiseClone() as AngleRange;
			angleRange.sprites = new List<Sprite>(angleRange.sprites);
			return angleRange;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000072EC File Offset: 0x000054EC
		public override bool Equals(object obj)
		{
			AngleRange angleRange = obj as AngleRange;
			if (angleRange == null)
			{
				return false;
			}
			if (!this.start.Equals(angleRange.start) || !this.end.Equals(angleRange.end) || !this.order.Equals(angleRange.order))
			{
				return false;
			}
			if (this.sprites.Count != angleRange.sprites.Count)
			{
				return false;
			}
			for (int i = 0; i < this.sprites.Count; i++)
			{
				if (this.sprites[i] != angleRange.sprites[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000073A0 File Offset: 0x000055A0
		public override int GetHashCode()
		{
			int num = this.start.GetHashCode() ^ this.end.GetHashCode() ^ this.order.GetHashCode();
			if (this.sprites != null)
			{
				for (int i = 0; i < this.sprites.Count; i++)
				{
					Sprite sprite = this.sprites[i];
					if (sprite)
					{
						num = (num * 16777619) ^ (sprite.GetHashCode() + i);
					}
				}
			}
			return num;
		}

		// Token: 0x0400007A RID: 122
		[SerializeField]
		private float m_Start;

		// Token: 0x0400007B RID: 123
		[SerializeField]
		private float m_End;

		// Token: 0x0400007C RID: 124
		[SerializeField]
		private int m_Order;

		// Token: 0x0400007D RID: 125
		[SerializeField]
		private List<Sprite> m_Sprites = new List<Sprite>();
	}
}
