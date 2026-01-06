using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000385 RID: 901
	public sealed class ValueAnimation<T> : IValueAnimationUpdate, IValueAnimation
	{
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00085F78 File Offset: 0x00084178
		// (set) Token: 0x06001CDD RID: 7389 RVA: 0x00085F90 File Offset: 0x00084190
		public int durationMs
		{
			get
			{
				return this.m_DurationMs;
			}
			set
			{
				bool flag = value < 1;
				if (flag)
				{
					value = 1;
				}
				this.m_DurationMs = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x00085FB2 File Offset: 0x000841B2
		// (set) Token: 0x06001CDF RID: 7391 RVA: 0x00085FBA File Offset: 0x000841BA
		public Func<float, float> easingCurve { get; set; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00085FC3 File Offset: 0x000841C3
		// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x00085FCB File Offset: 0x000841CB
		public bool isRunning { get; private set; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00085FD4 File Offset: 0x000841D4
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x00085FDC File Offset: 0x000841DC
		public Action onAnimationCompleted { get; set; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00085FE5 File Offset: 0x000841E5
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x00085FED File Offset: 0x000841ED
		public bool autoRecycle { get; set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x00085FF6 File Offset: 0x000841F6
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x00085FFE File Offset: 0x000841FE
		private bool recycled { get; set; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00086007 File Offset: 0x00084207
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x0008600F File Offset: 0x0008420F
		private VisualElement owner { get; set; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00086018 File Offset: 0x00084218
		// (set) Token: 0x06001CEB RID: 7403 RVA: 0x00086020 File Offset: 0x00084220
		public Action<VisualElement, T> valueUpdated { get; set; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x00086029 File Offset: 0x00084229
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x00086031 File Offset: 0x00084231
		public Func<VisualElement, T> initialValue { get; set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0008603A File Offset: 0x0008423A
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x00086042 File Offset: 0x00084242
		public Func<T, T, float, T> interpolator { get; set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0008604C File Offset: 0x0008424C
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0008609A File Offset: 0x0008429A
		public T from
		{
			get
			{
				bool flag = !this.fromValueSet;
				if (flag)
				{
					bool flag2 = this.initialValue != null;
					if (flag2)
					{
						this.from = this.initialValue.Invoke(this.owner);
					}
				}
				return this._from;
			}
			set
			{
				this.fromValueSet = true;
				this._from = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x000860AB File Offset: 0x000842AB
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x000860B3 File Offset: 0x000842B3
		public T to { get; set; }

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000860BC File Offset: 0x000842BC
		public ValueAnimation()
		{
			this.SetDefaultValues();
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000860D4 File Offset: 0x000842D4
		public void Start()
		{
			this.CheckNotRecycled();
			bool flag = this.owner != null;
			if (flag)
			{
				this.m_StartTimeMs = Panel.TimeSinceStartupMs();
				this.Register();
				this.isRunning = true;
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x00086114 File Offset: 0x00084314
		public void Stop()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
				this.isRunning = false;
				Action onAnimationCompleted = this.onAnimationCompleted;
				if (onAnimationCompleted != null)
				{
					onAnimationCompleted.Invoke();
				}
				bool autoRecycle = this.autoRecycle;
				if (autoRecycle)
				{
					bool flag = !this.recycled;
					if (flag)
					{
						this.Recycle();
					}
				}
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x00086178 File Offset: 0x00084378
		public void Recycle()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				bool flag = !this.autoRecycle;
				if (!flag)
				{
					this.Stop();
					return;
				}
				this.Stop();
			}
			this.SetDefaultValues();
			this.recycled = true;
			ValueAnimation<T>.sObjectPool.Release(this);
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000861D8 File Offset: 0x000843D8
		void IValueAnimationUpdate.Tick(long currentTimeMs)
		{
			this.CheckNotRecycled();
			long num = currentTimeMs - this.m_StartTimeMs;
			float num2 = (float)num / (float)this.durationMs;
			bool flag = false;
			bool flag2 = num2 >= 1f;
			if (flag2)
			{
				num2 = 1f;
				flag = true;
			}
			Func<float, float> easingCurve = this.easingCurve;
			num2 = ((easingCurve != null) ? easingCurve.Invoke(num2) : num2);
			bool flag3 = this.interpolator != null;
			if (flag3)
			{
				T t = this.interpolator.Invoke(this.from, this.to, num2);
				Action<VisualElement, T> valueUpdated = this.valueUpdated;
				if (valueUpdated != null)
				{
					valueUpdated.Invoke(this.owner, t);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				this.Stop();
			}
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x00086288 File Offset: 0x00084488
		private void SetDefaultValues()
		{
			this.m_DurationMs = 400;
			this.autoRecycle = true;
			this.owner = null;
			this.m_StartTimeMs = 0L;
			this.onAnimationCompleted = null;
			this.valueUpdated = null;
			this.initialValue = null;
			this.interpolator = null;
			this.to = default(T);
			this.from = default(T);
			this.fromValueSet = false;
			this.easingCurve = new Func<float, float>(Easing.OutQuad);
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x00086314 File Offset: 0x00084514
		private void Unregister()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.UnregisterAnimation(this);
			}
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00086340 File Offset: 0x00084540
		private void Register()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.RegisterAnimation(this);
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0008636C File Offset: 0x0008456C
		internal void SetOwner(VisualElement e)
		{
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
			}
			this.owner = e;
			bool isRunning2 = this.isRunning;
			if (isRunning2)
			{
				this.Register();
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000863A8 File Offset: 0x000845A8
		private void CheckNotRecycled()
		{
			bool recycled = this.recycled;
			if (recycled)
			{
				throw new InvalidOperationException("Animation object has been recycled. Use KeepAlive() to keep a reference to an animation after it has been stopped.");
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000863CC File Offset: 0x000845CC
		public static ValueAnimation<T> Create(VisualElement e, Func<T, T, float, T> interpolator)
		{
			ValueAnimation<T> valueAnimation = ValueAnimation<T>.sObjectPool.Get();
			valueAnimation.recycled = false;
			valueAnimation.SetOwner(e);
			valueAnimation.interpolator = interpolator;
			return valueAnimation;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00086404 File Offset: 0x00084604
		public ValueAnimation<T> Ease(Func<float, float> easing)
		{
			this.easingCurve = easing;
			return this;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00086420 File Offset: 0x00084620
		public ValueAnimation<T> OnCompleted(Action callback)
		{
			this.onAnimationCompleted = callback;
			return this;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0008643C File Offset: 0x0008463C
		public ValueAnimation<T> KeepAlive()
		{
			this.autoRecycle = false;
			return this;
		}

		// Token: 0x04000E5B RID: 3675
		private const int k_DefaultDurationMs = 400;

		// Token: 0x04000E5C RID: 3676
		private const int k_DefaultMaxPoolSize = 100;

		// Token: 0x04000E5D RID: 3677
		private long m_StartTimeMs;

		// Token: 0x04000E5E RID: 3678
		private int m_DurationMs;

		// Token: 0x04000E64 RID: 3684
		private static ObjectPool<ValueAnimation<T>> sObjectPool = new ObjectPool<ValueAnimation<T>>(100);

		// Token: 0x04000E69 RID: 3689
		private T _from;

		// Token: 0x04000E6A RID: 3690
		private bool fromValueSet = false;
	}
}
