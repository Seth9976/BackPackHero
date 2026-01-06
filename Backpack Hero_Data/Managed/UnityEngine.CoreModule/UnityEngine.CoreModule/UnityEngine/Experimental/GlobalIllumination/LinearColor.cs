using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000457 RID: 1111
	public struct LinearColor
	{
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x00041434 File Offset: 0x0003F634
		// (set) Token: 0x060027AA RID: 10154 RVA: 0x0004144C File Offset: 0x0003F64C
		public float red
		{
			get
			{
				return this.m_red;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Red color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_red = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00041494 File Offset: 0x0003F694
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x000414AC File Offset: 0x0003F6AC
		public float green
		{
			get
			{
				return this.m_green;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Green color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_green = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x000414F4 File Offset: 0x0003F6F4
		// (set) Token: 0x060027AE RID: 10158 RVA: 0x0004150C File Offset: 0x0003F70C
		public float blue
		{
			get
			{
				return this.m_blue;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Blue color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_blue = value;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x00041554 File Offset: 0x0003F754
		// (set) Token: 0x060027B0 RID: 10160 RVA: 0x0004156C File Offset: 0x0003F76C
		public float intensity
		{
			get
			{
				return this.m_intensity;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Intensity (" + value.ToString() + ") must be positive.");
				}
				this.m_intensity = value;
			}
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000415AC File Offset: 0x0003F7AC
		public static LinearColor Convert(Color color, float intensity)
		{
			Color color2 = (GraphicsSettings.lightsUseLinearIntensity ? color.linear.RGBMultiplied(intensity) : color.RGBMultiplied(intensity).linear);
			float maxColorComponent = color2.maxColorComponent;
			bool flag = color2.r < 0f || color2.g < 0f || color2.b < 0f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Concat(new string[]
				{
					"The input color to be converted must not contain negative values (red: ",
					color2.r.ToString(),
					", green: ",
					color2.g.ToString(),
					", blue: ",
					color2.b.ToString(),
					")."
				}));
			}
			bool flag2 = maxColorComponent <= 1E-20f;
			LinearColor linearColor;
			if (flag2)
			{
				linearColor = LinearColor.Black();
			}
			else
			{
				float num = 1f / color2.maxColorComponent;
				LinearColor linearColor2;
				linearColor2.m_red = color2.r * num;
				linearColor2.m_green = color2.g * num;
				linearColor2.m_blue = color2.b * num;
				linearColor2.m_intensity = maxColorComponent;
				linearColor = linearColor2;
			}
			return linearColor;
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000416E0 File Offset: 0x0003F8E0
		public static LinearColor Black()
		{
			LinearColor linearColor;
			linearColor.m_red = (linearColor.m_green = (linearColor.m_blue = (linearColor.m_intensity = 0f)));
			return linearColor;
		}

		// Token: 0x04000E59 RID: 3673
		private float m_red;

		// Token: 0x04000E5A RID: 3674
		private float m_green;

		// Token: 0x04000E5B RID: 3675
		private float m_blue;

		// Token: 0x04000E5C RID: 3676
		private float m_intensity;
	}
}
