using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000B8 RID: 184
	[DisableAnnotation]
	public abstract class Macro<TGraph> : MacroScriptableObject, IMacro, IGraphRoot, IGraphParent, ISerializationDependency, ISerializationCallbackReceiver, IAotStubbable where TGraph : class, IGraph, new()
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000A359 File Offset: 0x00008559
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000A361 File Offset: 0x00008561
		[DoNotSerialize]
		public TGraph graph
		{
			get
			{
				return this._graph;
			}
			set
			{
				if (value == null)
				{
					throw new InvalidOperationException("Macros must have a graph.");
				}
				if (value == this.graph)
				{
					return;
				}
				this._graph = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000A391 File Offset: 0x00008591
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0000A39E File Offset: 0x0000859E
		[DoNotSerialize]
		IGraph IMacro.graph
		{
			get
			{
				return this.graph;
			}
			set
			{
				this.graph = (TGraph)((object)value);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000A3AC File Offset: 0x000085AC
		[DoNotSerialize]
		IGraph IGraphParent.childGraph
		{
			get
			{
				return this.graph;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000A3B9 File Offset: 0x000085B9
		public IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			return this.graph.GetAotStubs(visited);
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000A3CC File Offset: 0x000085CC
		[DoNotSerialize]
		bool IGraphParent.isSerializationRoot
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000A3CF File Offset: 0x000085CF
		[DoNotSerialize]
		Object IGraphParent.serializedObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000A3D2 File Offset: 0x000085D2
		[DoNotSerialize]
		protected GraphReference reference
		{
			get
			{
				if (!(this._reference == null))
				{
					return this._reference;
				}
				return GraphReference.New(this, false);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000A3F0 File Offset: 0x000085F0
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0000A3F3 File Offset: 0x000085F3
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

		// Token: 0x0600049E RID: 1182 RVA: 0x0000A3F5 File Offset: 0x000085F5
		protected override void OnBeforeDeserialize()
		{
			base.OnBeforeDeserialize();
			Serialization.NotifyDependencyDeserializing(this);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000A403 File Offset: 0x00008603
		protected override void OnAfterDeserialize()
		{
			base.OnAfterDeserialize();
			Serialization.NotifyDependencyDeserialized(this);
		}

		// Token: 0x060004A0 RID: 1184
		public abstract TGraph DefaultGraph();

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000A411 File Offset: 0x00008611
		IGraph IGraphParent.DefaultGraph()
		{
			return this.DefaultGraph();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000A41E File Offset: 0x0000861E
		protected virtual void OnEnable()
		{
			Serialization.NotifyDependencyAvailable(this);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000A426 File Offset: 0x00008626
		protected virtual void OnDisable()
		{
			Serialization.NotifyDependencyUnavailable(this);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000A42E File Offset: 0x0000862E
		public GraphPointer GetReference()
		{
			return this.reference;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000A436 File Offset: 0x00008636
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0000A43E File Offset: 0x0000863E
		bool ISerializationDependency.IsDeserialized { get; set; }

		// Token: 0x040000FE RID: 254
		[SerializeAs("graph")]
		private TGraph _graph = new TGraph();

		// Token: 0x040000FF RID: 255
		[DoNotSerialize]
		private GraphReference _reference;
	}
}
