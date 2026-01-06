using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000017 RID: 23
	[SerializationVersion("A", new Type[] { })]
	[DisplayName("Script Graph")]
	public sealed class FlowGraph : Graph, IGraphWithVariables, IGraph, IDisposable, IPrewarmable, IAotStubbable, ISerializationDepender, ISerializationCallbackReceiver, IGraphEventListener
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000034BC File Offset: 0x000016BC
		public FlowGraph()
		{
			this.units = new GraphElementCollection<IUnit>(this);
			this.controlConnections = new GraphConnectionCollection<ControlConnection, ControlOutput, ControlInput>(this);
			this.valueConnections = new GraphConnectionCollection<ValueConnection, ValueOutput, ValueInput>(this);
			this.invalidConnections = new GraphConnectionCollection<InvalidConnection, IUnitOutputPort, IUnitInputPort>(this);
			this.groups = new GraphElementCollection<GraphGroup>(this);
			this.sticky = new GraphElementCollection<StickyNote>(this);
			base.elements.Include<IUnit>(this.units);
			base.elements.Include<ControlConnection>(this.controlConnections);
			base.elements.Include<ValueConnection>(this.valueConnections);
			base.elements.Include<InvalidConnection>(this.invalidConnections);
			base.elements.Include<GraphGroup>(this.groups);
			base.elements.Include<StickyNote>(this.sticky);
			this.controlInputDefinitions = new UnitPortDefinitionCollection<ControlInputDefinition>();
			this.controlOutputDefinitions = new UnitPortDefinitionCollection<ControlOutputDefinition>();
			this.valueInputDefinitions = new UnitPortDefinitionCollection<ValueInputDefinition>();
			this.valueOutputDefinitions = new UnitPortDefinitionCollection<ValueOutputDefinition>();
			this.variables = new VariableDeclarations();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000035B4 File Offset: 0x000017B4
		public override IGraphData CreateData()
		{
			return new FlowGraphData(this);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000035BC File Offset: 0x000017BC
		public void StartListening(GraphStack stack)
		{
			stack.GetGraphData<FlowGraphData>().isListening = true;
			foreach (IUnit unit in this.units)
			{
				IGraphEventListener graphEventListener = unit as IGraphEventListener;
				if (graphEventListener != null)
				{
					graphEventListener.StartListening(stack);
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003624 File Offset: 0x00001824
		public void StopListening(GraphStack stack)
		{
			foreach (IUnit unit in this.units)
			{
				IGraphEventListener graphEventListener = unit as IGraphEventListener;
				if (graphEventListener != null)
				{
					graphEventListener.StopListening(stack);
				}
			}
			stack.GetGraphData<FlowGraphData>().isListening = false;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000368C File Offset: 0x0000188C
		public bool IsListening(GraphPointer pointer)
		{
			return pointer.GetGraphData<FlowGraphData>().isListening;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003699 File Offset: 0x00001899
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000036A1 File Offset: 0x000018A1
		[Serialize]
		public VariableDeclarations variables { get; private set; }

		// Token: 0x060000AE RID: 174 RVA: 0x000036AC File Offset: 0x000018AC
		public IEnumerable<string> GetDynamicVariableNames(VariableKind kind, GraphReference reference)
		{
			return from name in (from v in this.units.OfType<IUnifiedVariableUnit>()
					where v.kind == kind && Flow.CanPredict(v.name, reference)
					select Flow.Predict<string>(v.name, reference) into name
					where !StringUtility.IsNullOrWhiteSpace(name)
					select name).Distinct<string>()
				orderby name
				select name;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003747 File Offset: 0x00001947
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000374F File Offset: 0x0000194F
		[DoNotSerialize]
		public GraphElementCollection<IUnit> units { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003758 File Offset: 0x00001958
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003760 File Offset: 0x00001960
		[DoNotSerialize]
		public GraphConnectionCollection<ControlConnection, ControlOutput, ControlInput> controlConnections { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003769 File Offset: 0x00001969
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003771 File Offset: 0x00001971
		[DoNotSerialize]
		public GraphConnectionCollection<ValueConnection, ValueOutput, ValueInput> valueConnections { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000377A File Offset: 0x0000197A
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003782 File Offset: 0x00001982
		[DoNotSerialize]
		public GraphConnectionCollection<InvalidConnection, IUnitOutputPort, IUnitInputPort> invalidConnections { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000378B File Offset: 0x0000198B
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003793 File Offset: 0x00001993
		[DoNotSerialize]
		public GraphElementCollection<GraphGroup> groups { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000379C File Offset: 0x0000199C
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000037A4 File Offset: 0x000019A4
		[DoNotSerialize]
		public GraphElementCollection<StickyNote> sticky { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000037AD File Offset: 0x000019AD
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000037B5 File Offset: 0x000019B5
		[Serialize]
		[InspectorLabel("Trigger Inputs")]
		[InspectorWide(true)]
		[WarnBeforeRemoving("Remove Port Definition", "Removing this definition will break any existing connection to this port. Are you sure you want to continue?")]
		public UnitPortDefinitionCollection<ControlInputDefinition> controlInputDefinitions { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000037BE File Offset: 0x000019BE
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000037C6 File Offset: 0x000019C6
		[Serialize]
		[InspectorLabel("Trigger Outputs")]
		[InspectorWide(true)]
		[WarnBeforeRemoving("Remove Port Definition", "Removing this definition will break any existing connection to this port. Are you sure you want to continue?")]
		public UnitPortDefinitionCollection<ControlOutputDefinition> controlOutputDefinitions { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000037CF File Offset: 0x000019CF
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000037D7 File Offset: 0x000019D7
		[Serialize]
		[InspectorLabel("Data Inputs")]
		[InspectorWide(true)]
		[WarnBeforeRemoving("Remove Port Definition", "Removing this definition will break any existing connection to this port. Are you sure you want to continue?")]
		public UnitPortDefinitionCollection<ValueInputDefinition> valueInputDefinitions { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000037E0 File Offset: 0x000019E0
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000037E8 File Offset: 0x000019E8
		[Serialize]
		[InspectorLabel("Data Outputs")]
		[InspectorWide(true)]
		[WarnBeforeRemoving("Remove Port Definition", "Removing this definition will break any existing connection to this port. Are you sure you want to continue?")]
		public UnitPortDefinitionCollection<ValueOutputDefinition> valueOutputDefinitions { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000037F4 File Offset: 0x000019F4
		public IEnumerable<IUnitPortDefinition> validPortDefinitions
		{
			get
			{
				return (from upd in LinqUtility.Concat<IUnitPortDefinition>(new IEnumerable[] { this.controlInputDefinitions, this.controlOutputDefinitions, this.valueInputDefinitions, this.valueOutputDefinitions })
					where upd.isValid
					select upd).DistinctBy((IUnitPortDefinition upd) => upd.key);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000C4 RID: 196 RVA: 0x00003878 File Offset: 0x00001A78
		// (remove) Token: 0x060000C5 RID: 197 RVA: 0x000038B0 File Offset: 0x00001AB0
		public event Action onPortDefinitionsChanged;

		// Token: 0x060000C6 RID: 198 RVA: 0x000038E5 File Offset: 0x00001AE5
		public void PortDefinitionsChanged()
		{
			Action action = this.onPortDefinitionsChanged;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000038F8 File Offset: 0x00001AF8
		public static FlowGraph WithInputOutput()
		{
			return new FlowGraph
			{
				units = 
				{
					new GraphInput
					{
						position = new Vector2(-250f, -30f)
					},
					new GraphOutput
					{
						position = new Vector2(105f, -30f)
					}
				}
			};
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003954 File Offset: 0x00001B54
		public static FlowGraph WithStartUpdate()
		{
			return new FlowGraph
			{
				units = 
				{
					new Start
					{
						position = new Vector2(-204f, -144f)
					},
					new Update
					{
						position = new Vector2(-204f, 60f)
					}
				}
			};
		}

		// Token: 0x0400002D RID: 45
		private const string DefinitionRemoveWarningTitle = "Remove Port Definition";

		// Token: 0x0400002E RID: 46
		private const string DefinitionRemoveWarningMessage = "Removing this definition will break any existing connection to this port. Are you sure you want to continue?";
	}
}
