using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000228 RID: 552
	public abstract class TransitionEventBase<T> : EventBase<T>, ITransitionEvent where T : TransitionEventBase<T>, new()
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0004033B File Offset: 0x0003E53B
		public StylePropertyNameCollection stylePropertyNames { get; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00040343 File Offset: 0x0003E543
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x0004034B File Offset: 0x0003E54B
		public double elapsedTime { get; protected set; }

		// Token: 0x0600108C RID: 4236 RVA: 0x00040354 File Offset: 0x0003E554
		protected TransitionEventBase()
		{
			this.stylePropertyNames = new StylePropertyNameCollection(new List<StylePropertyName>());
			this.LocalInit();
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00040375 File Offset: 0x0003E575
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00040386 File Offset: 0x0003E586
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles;
			base.propagateToIMGUI = false;
			this.stylePropertyNames.propertiesList.Clear();
			this.elapsedTime = 0.0;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000403BC File Offset: 0x0003E5BC
		public static T GetPooled(StylePropertyName stylePropertyName, double elapsedTime)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.stylePropertyNames.propertiesList.Add(stylePropertyName);
			pooled.elapsedTime = elapsedTime;
			return pooled;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x000403FC File Offset: 0x0003E5FC
		public bool AffectsProperty(StylePropertyName stylePropertyName)
		{
			return this.stylePropertyNames.Contains(stylePropertyName);
		}
	}
}
