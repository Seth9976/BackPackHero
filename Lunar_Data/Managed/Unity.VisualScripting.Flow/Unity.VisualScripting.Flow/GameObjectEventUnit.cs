using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200005C RID: 92
	public abstract class GameObjectEventUnit<TArgs> : EventUnit<TArgs>, IGameObjectEventUnit, IEventUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphEventListener
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00008CB1 File Offset: 0x00006EB1
		protected sealed override bool register
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000377 RID: 887
		public abstract Type MessageListenerType { get; }

		// Token: 0x06000378 RID: 888 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public override IGraphElementData CreateData()
		{
			return new GameObjectEventUnit<TArgs>.Data();
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00008CBB File Offset: 0x00006EBB
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00008CC3 File Offset: 0x00006EC3
		[DoNotSerialize]
		[NullMeansSelf]
		[PortLabel("Target")]
		[PortLabelHidden]
		public ValueInput target { get; private set; }

		// Token: 0x0600037B RID: 891 RVA: 0x00008CCC File Offset: 0x00006ECC
		protected override void Definition()
		{
			base.Definition();
			this.target = base.ValueInput<GameObject>("target", null).NullMeansSelf();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00008CEC File Offset: 0x00006EEC
		public override EventHook GetHook(GraphReference reference)
		{
			if (!reference.hasData)
			{
				return this.hookName;
			}
			GameObjectEventUnit<TArgs>.Data elementData = reference.GetElementData<GameObjectEventUnit<TArgs>.Data>(this);
			return new EventHook(this.hookName, elementData.target, null);
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00008D27 File Offset: 0x00006F27
		protected virtual string hookName
		{
			get
			{
				throw new InvalidImplementationException(string.Format("Missing event hook for '{0}'.", this));
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00008D3C File Offset: 0x00006F3C
		private void UpdateTarget(GraphStack stack)
		{
			GameObjectEventUnit<TArgs>.Data elementData = stack.GetElementData<GameObjectEventUnit<TArgs>.Data>(this);
			bool isListening = elementData.isListening;
			GameObject gameObject = Flow.FetchValue<GameObject>(this.target, stack.ToReference());
			if (gameObject != elementData.target)
			{
				if (isListening)
				{
					this.StopListening(stack);
				}
				elementData.target = gameObject;
				if (isListening)
				{
					this.StartListening(stack, false);
				}
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00008D94 File Offset: 0x00006F94
		protected void StartListening(GraphStack stack, bool updateTarget)
		{
			if (updateTarget)
			{
				this.UpdateTarget(stack);
			}
			GameObjectEventUnit<TArgs>.Data elementData = stack.GetElementData<GameObjectEventUnit<TArgs>.Data>(this);
			if (elementData.target == null)
			{
				return;
			}
			if (UnityThread.allowsAPI && this.MessageListenerType != null)
			{
				MessageListener.AddTo(this.MessageListenerType, elementData.target);
			}
			base.StartListening(stack);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00008DEF File Offset: 0x00006FEF
		public override void StartListening(GraphStack stack)
		{
			this.StartListening(stack, true);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00008E01 File Offset: 0x00007001
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}

		// Token: 0x020001B6 RID: 438
		public new class Data : EventUnit<TArgs>.Data
		{
			// Token: 0x040003A8 RID: 936
			public GameObject target;
		}
	}
}
