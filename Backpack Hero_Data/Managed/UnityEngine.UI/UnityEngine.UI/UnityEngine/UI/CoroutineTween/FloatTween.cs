using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000048 RID: 72
	internal struct FloatTween : ITweenValue
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001707D File Offset: 0x0001527D
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00017085 File Offset: 0x00015285
		public float startValue
		{
			get
			{
				return this.m_StartValue;
			}
			set
			{
				this.m_StartValue = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001708E File Offset: 0x0001528E
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00017096 File Offset: 0x00015296
		public float targetValue
		{
			get
			{
				return this.m_TargetValue;
			}
			set
			{
				this.m_TargetValue = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001709F File Offset: 0x0001529F
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x000170A7 File Offset: 0x000152A7
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000170B0 File Offset: 0x000152B0
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x000170B8 File Offset: 0x000152B8
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

		// Token: 0x060004ED RID: 1261 RVA: 0x000170C4 File Offset: 0x000152C4
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			float num = Mathf.Lerp(this.m_StartValue, this.m_TargetValue, floatPercentage);
			this.m_Target.Invoke(num);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000170F9 File Offset: 0x000152F9
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001711A File Offset: 0x0001531A
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00017122 File Offset: 0x00015322
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001712A File Offset: 0x0001532A
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x0400019E RID: 414
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x0400019F RID: 415
		private float m_StartValue;

		// Token: 0x040001A0 RID: 416
		private float m_TargetValue;

		// Token: 0x040001A1 RID: 417
		private float m_Duration;

		// Token: 0x040001A2 RID: 418
		private bool m_IgnoreTimeScale;

		// Token: 0x020000BB RID: 187
		public class FloatTweenCallback : UnityEvent<float>
		{
		}
	}
}
