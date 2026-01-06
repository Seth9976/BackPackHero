using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000047 RID: 71
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00016F5B File Offset: 0x0001515B
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00016F63 File Offset: 0x00015163
		public Color startColor
		{
			get
			{
				return this.m_StartColor;
			}
			set
			{
				this.m_StartColor = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00016F6C File Offset: 0x0001516C
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00016F74 File Offset: 0x00015174
		public Color targetColor
		{
			get
			{
				return this.m_TargetColor;
			}
			set
			{
				this.m_TargetColor = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00016F7D File Offset: 0x0001517D
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00016F85 File Offset: 0x00015185
		public ColorTween.ColorTweenMode tweenMode
		{
			get
			{
				return this.m_TweenMode;
			}
			set
			{
				this.m_TweenMode = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00016F8E File Offset: 0x0001518E
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00016F96 File Offset: 0x00015196
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.m_Duration = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00016F9F File Offset: 0x0001519F
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00016FA7 File Offset: 0x000151A7
		public bool ignoreTimeScale
		{
			get
			{
				return this.m_IgnoreTimeScale;
			}
			set
			{
				this.m_IgnoreTimeScale = value;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00016FB0 File Offset: 0x000151B0
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			Color color = Color.Lerp(this.m_StartColor, this.m_TargetColor, floatPercentage);
			if (this.m_TweenMode == ColorTween.ColorTweenMode.Alpha)
			{
				color.r = this.m_StartColor.r;
				color.g = this.m_StartColor.g;
				color.b = this.m_StartColor.b;
			}
			else if (this.m_TweenMode == ColorTween.ColorTweenMode.RGB)
			{
				color.a = this.m_StartColor.a;
			}
			this.m_Target.Invoke(color);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00017041 File Offset: 0x00015241
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00017062 File Offset: 0x00015262
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001706A File Offset: 0x0001526A
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00017072 File Offset: 0x00015272
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000198 RID: 408
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x04000199 RID: 409
		private Color m_StartColor;

		// Token: 0x0400019A RID: 410
		private Color m_TargetColor;

		// Token: 0x0400019B RID: 411
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x0400019C RID: 412
		private float m_Duration;

		// Token: 0x0400019D RID: 413
		private bool m_IgnoreTimeScale;

		// Token: 0x020000B9 RID: 185
		public enum ColorTweenMode
		{
			// Token: 0x04000319 RID: 793
			All,
			// Token: 0x0400031A RID: 794
			RGB,
			// Token: 0x0400031B RID: 795
			Alpha
		}

		// Token: 0x020000BA RID: 186
		public class ColorTweenCallback : UnityEvent<Color>
		{
		}
	}
}
