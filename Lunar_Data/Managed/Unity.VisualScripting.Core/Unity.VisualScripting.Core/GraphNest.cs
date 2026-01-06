using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000061 RID: 97
	public sealed class GraphNest<TGraph, TMacro> : IGraphNest, IAotStubbable where TGraph : class, IGraph, new() where TMacro : Macro<TGraph>
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00007680 File Offset: 0x00005880
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00007688 File Offset: 0x00005888
		[DoNotSerialize]
		public IGraphNester nester { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00007691 File Offset: 0x00005891
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00007699 File Offset: 0x00005899
		[Serialize]
		public GraphSource source
		{
			get
			{
				return this._source;
			}
			set
			{
				if (value == this.source)
				{
					return;
				}
				this.BeforeGraphChange();
				this._source = value;
				this.AfterGraphChange();
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000076B8 File Offset: 0x000058B8
		// (set) Token: 0x060002EE RID: 750 RVA: 0x000076C0 File Offset: 0x000058C0
		[Serialize]
		public TMacro macro
		{
			get
			{
				return this._macro;
			}
			set
			{
				if (value == this.macro)
				{
					return;
				}
				this.BeforeGraphChange();
				this._macro = value;
				this.AfterGraphChange();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000076EE File Offset: 0x000058EE
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x000076F6 File Offset: 0x000058F6
		[Serialize]
		public TGraph embed
		{
			get
			{
				return this._embed;
			}
			set
			{
				if (value == this.embed)
				{
					return;
				}
				this.BeforeGraphChange();
				this._embed = value;
				this.AfterGraphChange();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00007720 File Offset: 0x00005920
		[DoNotSerialize]
		public TGraph graph
		{
			get
			{
				GraphSource source = this.source;
				if (source == GraphSource.Embed)
				{
					return this.embed;
				}
				if (source != GraphSource.Macro)
				{
					throw new UnexpectedEnumValueException<GraphSource>(this.source);
				}
				TMacro tmacro = this.macro;
				if (tmacro == null)
				{
					return default(TGraph);
				}
				return tmacro.graph;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000776E File Offset: 0x0000596E
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000777B File Offset: 0x0000597B
		IMacro IGraphNest.macro
		{
			get
			{
				return this.macro;
			}
			set
			{
				this.macro = (TMacro)((object)value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00007789 File Offset: 0x00005989
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00007796 File Offset: 0x00005996
		IGraph IGraphNest.embed
		{
			get
			{
				return this.embed;
			}
			set
			{
				this.embed = (TGraph)((object)value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000077A4 File Offset: 0x000059A4
		IGraph IGraphNest.graph
		{
			get
			{
				return this.graph;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000077B1 File Offset: 0x000059B1
		Type IGraphNest.graphType
		{
			get
			{
				return typeof(TGraph);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000077BD File Offset: 0x000059BD
		Type IGraphNest.macroType
		{
			get
			{
				return typeof(TMacro);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000077CC File Offset: 0x000059CC
		public void SwitchToEmbed(TGraph embed)
		{
			if (this.source == GraphSource.Embed && this.embed == embed)
			{
				return;
			}
			this.BeforeGraphChange();
			this._source = GraphSource.Embed;
			this._embed = embed;
			this._macro = default(TMacro);
			this.AfterGraphChange();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000781C File Offset: 0x00005A1C
		public void SwitchToMacro(TMacro macro)
		{
			if (this.source == GraphSource.Macro && this.macro == macro)
			{
				return;
			}
			this.BeforeGraphChange();
			this._source = GraphSource.Macro;
			this._embed = default(TGraph);
			this._macro = macro;
			this.AfterGraphChange();
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060002FB RID: 763 RVA: 0x00007874 File Offset: 0x00005A74
		// (remove) Token: 0x060002FC RID: 764 RVA: 0x000078AC File Offset: 0x00005AAC
		public event Action beforeGraphChange;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060002FD RID: 765 RVA: 0x000078E4 File Offset: 0x00005AE4
		// (remove) Token: 0x060002FE RID: 766 RVA: 0x0000791C File Offset: 0x00005B1C
		public event Action afterGraphChange;

		// Token: 0x060002FF RID: 767 RVA: 0x00007951 File Offset: 0x00005B51
		private void BeforeGraphChange()
		{
			if (this.graph != null)
			{
				this.nester.UninstantiateNest();
			}
			Action action = this.beforeGraphChange;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000797B File Offset: 0x00005B7B
		private void AfterGraphChange()
		{
			Action action = this.afterGraphChange;
			if (action != null)
			{
				action();
			}
			if (this.graph != null)
			{
				this.nester.InstantiateNest();
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000079A6 File Offset: 0x00005BA6
		public IEnumerable<ISerializationDependency> deserializationDependencies
		{
			get
			{
				if (this.macro != null)
				{
					yield return this.macro;
				}
				yield break;
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000079B6 File Offset: 0x00005BB6
		public IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			IEnumerable[] array = new IEnumerable[1];
			int num = 0;
			TGraph tgraph = this.graph;
			array[num] = ((tgraph != null) ? tgraph.GetAotStubs(visited) : null);
			return LinqUtility.Concat<object>(array);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000303 RID: 771 RVA: 0x000079DE File Offset: 0x00005BDE
		[DoNotSerialize]
		public bool hasBackgroundEmbed
		{
			get
			{
				return this.source == GraphSource.Macro && this.embed != null;
			}
		}

		// Token: 0x040000D2 RID: 210
		[DoNotSerialize]
		private GraphSource _source = GraphSource.Macro;

		// Token: 0x040000D3 RID: 211
		[DoNotSerialize]
		private TMacro _macro;

		// Token: 0x040000D4 RID: 212
		[DoNotSerialize]
		private TGraph _embed;
	}
}
