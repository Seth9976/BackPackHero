using System;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001BD RID: 445
	[UsedByNativeCode]
	[StructLayout(2)]
	public struct Color32 : IFormattable
	{
		// Token: 0x0600139A RID: 5018 RVA: 0x0001BECE File Offset: 0x0001A0CE
		public Color32(byte r, byte g, byte b, byte a)
		{
			this.rgba = 0;
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)Mathf.Round(Mathf.Clamp01(c.r) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.g) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.b) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.a) * 255f));
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0001BF6C File Offset: 0x0001A16C
		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
		public static Color32 Lerp(Color32 a, Color32 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0001C03C File Offset: 0x0001A23C
		public static Color32 LerpUnclamped(Color32 a, Color32 b, float t)
		{
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x17000402 RID: 1026
		public byte this[int index]
		{
			get
			{
				byte b;
				switch (index)
				{
				case 0:
					b = this.r;
					break;
				case 1:
					b = this.g;
					break;
				case 2:
					b = this.b;
					break;
				case 3:
					b = this.a;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index.ToString() + ")!");
				}
				return b;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.r = value;
					break;
				case 1:
					this.g = value;
					break;
				case 2:
					this.b = value;
					break;
				case 3:
					this.a = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index.ToString() + ")!");
				}
			}
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0001C190 File Offset: 0x0001A390
		[VisibleToOtherModules]
		internal bool InternalEquals(Color32 other)
		{
			return this.rgba == other.rgba;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0001C1CC File Offset: 0x0001A3CC
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r.ToString(format, formatProvider),
				this.g.ToString(format, formatProvider),
				this.b.ToString(format, formatProvider),
				this.a.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000738 RID: 1848
		[Ignore(DoesNotContributeToSize = true)]
		[FieldOffset(0)]
		private int rgba;

		// Token: 0x04000739 RID: 1849
		[FieldOffset(0)]
		public byte r;

		// Token: 0x0400073A RID: 1850
		[FieldOffset(1)]
		public byte g;

		// Token: 0x0400073B RID: 1851
		[FieldOffset(2)]
		public byte b;

		// Token: 0x0400073C RID: 1852
		[FieldOffset(3)]
		public byte a;
	}
}
