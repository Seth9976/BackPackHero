using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000053 RID: 83
	public abstract class EventMachine<TGraph, TMacro> : Machine<TGraph, TMacro>, IEventMachine, IMachine, IGraphRoot, IGraphParent, IGraphNester, IAotStubbable where TGraph : class, IGraph, new() where TMacro : Macro<TGraph>, new()
	{
		// Token: 0x06000268 RID: 616 RVA: 0x000061C0 File Offset: 0x000043C0
		protected void TriggerEvent(string name)
		{
			if (base.hasGraph)
			{
				this.TriggerRegisteredEvent<EmptyEventArgs>(new EventHook(name, this, null), default(EmptyEventArgs));
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000061EC File Offset: 0x000043EC
		protected void TriggerEvent<TArgs>(string name, TArgs args)
		{
			if (base.hasGraph)
			{
				this.TriggerRegisteredEvent<TArgs>(new EventHook(name, this, null), args);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00006208 File Offset: 0x00004408
		protected void TriggerUnregisteredEvent(string name)
		{
			if (base.hasGraph)
			{
				this.TriggerUnregisteredEvent<EmptyEventArgs>(name, default(EmptyEventArgs));
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00006232 File Offset: 0x00004432
		protected virtual void TriggerRegisteredEvent<TArgs>(EventHook hook, TArgs args)
		{
			EventBus.Trigger<TArgs>(hook, args);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000623C File Offset: 0x0000443C
		protected virtual void TriggerUnregisteredEvent<TArgs>(EventHook hook, TArgs args)
		{
			using (GraphStack graphStack = base.reference.ToStackPooled())
			{
				graphStack.TriggerEventHandler((EventHook _hook) => _hook == hook, args, (IGraphParentElement parent) => true, true);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000062B4 File Offset: 0x000044B4
		protected override void Awake()
		{
			base.Awake();
			GlobalMessageListener.Require();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000062C1 File Offset: 0x000044C1
		protected override void OnEnable()
		{
			base.OnEnable();
			this.TriggerEvent("OnEnable");
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000062D4 File Offset: 0x000044D4
		protected virtual void Start()
		{
			this.TriggerEvent("Start");
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000062E1 File Offset: 0x000044E1
		protected override void OnInstantiateWhileEnabled()
		{
			base.OnInstantiateWhileEnabled();
			this.TriggerEvent("OnEnable");
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000062F4 File Offset: 0x000044F4
		protected virtual void Update()
		{
			this.TriggerEvent("Update");
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00006301 File Offset: 0x00004501
		protected virtual void FixedUpdate()
		{
			this.TriggerEvent("FixedUpdate");
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000630E File Offset: 0x0000450E
		protected virtual void LateUpdate()
		{
			this.TriggerEvent("LateUpdate");
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000631B File Offset: 0x0000451B
		protected override void OnUninstantiateWhileEnabled()
		{
			this.TriggerEvent("OnDisable");
			base.OnUninstantiateWhileEnabled();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000632E File Offset: 0x0000452E
		protected override void OnDisable()
		{
			this.TriggerEvent("OnDisable");
			base.OnDisable();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00006344 File Offset: 0x00004544
		protected override void OnDestroy()
		{
			try
			{
				this.TriggerEvent("OnDestroy");
			}
			finally
			{
				base.OnDestroy();
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00006378 File Offset: 0x00004578
		public override void TriggerAnimationEvent(AnimationEvent animationEvent)
		{
			this.TriggerEvent<AnimationEvent>("AnimationEvent", animationEvent);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00006386 File Offset: 0x00004586
		public override void TriggerUnityEvent(string name)
		{
			this.TriggerEvent<string>("UnityEvent", name);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00006394 File Offset: 0x00004594
		protected virtual void OnDrawGizmos()
		{
			this.TriggerUnregisteredEvent("OnDrawGizmos");
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000063A1 File Offset: 0x000045A1
		protected virtual void OnDrawGizmosSelected()
		{
			this.TriggerUnregisteredEvent("OnDrawGizmosSelected");
		}
	}
}
