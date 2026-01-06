using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000B6 RID: 182
	public abstract class Machine<TGraph, TMacro> : LudiqBehaviour, IMachine, IGraphRoot, IGraphParent, IGraphNester, IAotStubbable where TGraph : class, IGraph, new() where TMacro : Macro<TGraph>
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x0000A07D File Offset: 0x0000827D
		protected Machine()
		{
			this.nest.nester = this;
			this.nest.source = GraphSource.Macro;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000A0A8 File Offset: 0x000082A8
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0000A0B0 File Offset: 0x000082B0
		[Serialize]
		public GraphNest<TGraph, TMacro> nest { get; private set; } = new GraphNest<TGraph, TMacro>();

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000A0B9 File Offset: 0x000082B9
		[DoNotSerialize]
		IGraphNest IGraphNester.nest
		{
			get
			{
				return this.nest;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000A0C1 File Offset: 0x000082C1
		[DoNotSerialize]
		GameObject IMachine.threadSafeGameObject
		{
			get
			{
				return this.threadSafeGameObject;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000A0C9 File Offset: 0x000082C9
		[DoNotSerialize]
		protected GraphReference reference
		{
			get
			{
				if (!this.isReferenceCached)
				{
					return GraphReference.New(this, false);
				}
				return this._reference;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000A0E1 File Offset: 0x000082E1
		[DoNotSerialize]
		protected bool hasGraph
		{
			get
			{
				return this.reference != null;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000A0EF File Offset: 0x000082EF
		[DoNotSerialize]
		public TGraph graph
		{
			get
			{
				return this.nest.graph;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000A0FC File Offset: 0x000082FC
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000A104 File Offset: 0x00008304
		[DoNotSerialize]
		public IGraphData graphData { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000A10D File Offset: 0x0000830D
		[DoNotSerialize]
		bool IGraphParent.isSerializationRoot
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000A110 File Offset: 0x00008310
		[DoNotSerialize]
		Object IGraphParent.serializedObject
		{
			get
			{
				GraphSource source = this.nest.source;
				if (source == GraphSource.Embed)
				{
					return this;
				}
				if (source == GraphSource.Macro)
				{
					return this.nest.macro;
				}
				throw new UnexpectedEnumValueException<GraphSource>(this.nest.source);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000A153 File Offset: 0x00008353
		[DoNotSerialize]
		IGraph IGraphParent.childGraph
		{
			get
			{
				return this.graph;
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000A160 File Offset: 0x00008360
		public IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			return this.nest.GetAotStubs(visited);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000A16E File Offset: 0x0000836E
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000A171 File Offset: 0x00008371
		public bool isDescriptionValid
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000A174 File Offset: 0x00008374
		protected virtual void Awake()
		{
			this._alive = true;
			this.threadSafeGameObject = base.gameObject;
			this.nest.afterGraphChange += this.CacheReference;
			this.nest.beforeGraphChange += this.ClearCachedReference;
			this.CacheReference();
			if (this.graph != null)
			{
				this.graph.Prewarm();
				this.InstantiateNest();
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000A1EB File Offset: 0x000083EB
		protected virtual void OnEnable()
		{
			this._enabled = true;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000A1F4 File Offset: 0x000083F4
		protected virtual void OnInstantiateWhileEnabled()
		{
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000A1F6 File Offset: 0x000083F6
		protected virtual void OnUninstantiateWhileEnabled()
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000A1F8 File Offset: 0x000083F8
		protected virtual void OnDisable()
		{
			this._enabled = false;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000A201 File Offset: 0x00008401
		protected virtual void OnDestroy()
		{
			this.ClearCachedReference();
			if (this.graph != null)
			{
				this.UninstantiateNest();
			}
			this.threadSafeGameObject = null;
			this._alive = false;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000A22A File Offset: 0x0000842A
		protected virtual void OnValidate()
		{
			this.threadSafeGameObject = base.gameObject;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000A238 File Offset: 0x00008438
		public GraphPointer GetReference()
		{
			return this.reference;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000A240 File Offset: 0x00008440
		private void CacheReference()
		{
			this._reference = GraphReference.New(this, false);
			this.isReferenceCached = true;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000A256 File Offset: 0x00008456
		private void ClearCachedReference()
		{
			this._reference = null;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000A260 File Offset: 0x00008460
		public virtual void InstantiateNest()
		{
			if (this._alive)
			{
				GraphInstances.Instantiate(this.reference);
			}
			if (this._enabled)
			{
				if (UnityThread.allowsAPI)
				{
					this.OnInstantiateWhileEnabled();
					return;
				}
				Debug.LogWarning("Could not run instantiation events on " + this.ToSafeString() + " because the Unity API is not available.\nThis can happen when undoing / redoing a graph source change.", this);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public virtual void UninstantiateNest()
		{
			if (this._enabled)
			{
				if (UnityThread.allowsAPI)
				{
					this.OnUninstantiateWhileEnabled();
				}
				else
				{
					Debug.LogWarning("Could not run uninstantiation events on " + this.ToSafeString() + " because the Unity API is not available.\nThis can happen when undoing / redoing a graph source change.", this);
				}
			}
			if (this._alive)
			{
				HashSet<GraphReference> hashSet = GraphInstances.ChildrenOfPooled(this);
				foreach (GraphReference graphReference in hashSet)
				{
					GraphInstances.Uninstantiate(graphReference);
				}
				hashSet.Free<GraphReference>();
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000A348 File Offset: 0x00008548
		public virtual void TriggerAnimationEvent(AnimationEvent animationEvent)
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000A34A File Offset: 0x0000854A
		public virtual void TriggerUnityEvent(string name)
		{
		}

		// Token: 0x0600048F RID: 1167
		public abstract TGraph DefaultGraph();

		// Token: 0x06000490 RID: 1168 RVA: 0x0000A34C File Offset: 0x0000854C
		IGraph IGraphParent.DefaultGraph()
		{
			return this.DefaultGraph();
		}

		// Token: 0x040000F8 RID: 248
		[DoNotSerialize]
		private bool _alive;

		// Token: 0x040000F9 RID: 249
		[DoNotSerialize]
		private bool _enabled;

		// Token: 0x040000FA RID: 250
		[DoNotSerialize]
		private GameObject threadSafeGameObject;

		// Token: 0x040000FB RID: 251
		[DoNotSerialize]
		private bool isReferenceCached;

		// Token: 0x040000FC RID: 252
		[DoNotSerialize]
		private GraphReference _reference;
	}
}
