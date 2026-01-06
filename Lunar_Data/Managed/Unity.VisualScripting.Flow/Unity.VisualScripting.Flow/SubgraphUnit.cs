using System;
using System.ComponentModel;

namespace Unity.VisualScripting
{
	// Token: 0x0200017C RID: 380
	[TypeIcon(typeof(FlowGraph))]
	[UnitCategory("Nesting")]
	[UnitTitle("Subgraph")]
	[RenamedFrom("Bolt.SuperUnit")]
	[RenamedFrom("Unity.VisualScripting.SuperUnit")]
	[DisplayName("Subgraph Node")]
	public sealed class SubgraphUnit : NesterUnit<FlowGraph, ScriptGraphAsset>, IGraphEventListener, IGraphElementWithData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x00011ADD File Offset: 0x0000FCDD
		public IGraphElementData CreateData()
		{
			return new SubgraphUnit.Data();
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		public SubgraphUnit()
		{
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public SubgraphUnit(ScriptGraphAsset macro)
			: base(macro)
		{
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00011AF5 File Offset: 0x0000FCF5
		public static SubgraphUnit WithInputOutput()
		{
			return new SubgraphUnit
			{
				nest = 
				{
					source = GraphSource.Embed
				},
				nest = 
				{
					embed = FlowGraph.WithInputOutput()
				}
			};
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00011B18 File Offset: 0x0000FD18
		public static SubgraphUnit WithStartUpdate()
		{
			return new SubgraphUnit
			{
				nest = 
				{
					source = GraphSource.Embed
				},
				nest = 
				{
					embed = FlowGraph.WithStartUpdate()
				}
			};
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00011B3B File Offset: 0x0000FD3B
		public override FlowGraph DefaultGraph()
		{
			return FlowGraph.WithInputOutput();
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00011B44 File Offset: 0x0000FD44
		protected override void Definition()
		{
			this.isControlRoot = true;
			foreach (IUnitPortDefinition unitPortDefinition in base.nest.graph.validPortDefinitions)
			{
				if (unitPortDefinition is ControlInputDefinition)
				{
					ControlInputDefinition controlInputDefinition = (ControlInputDefinition)unitPortDefinition;
					string key4 = controlInputDefinition.key;
					base.ControlInput(key4, delegate(Flow flow)
					{
						foreach (IUnit unit in this.nest.graph.units)
						{
							if (unit is GraphInput)
							{
								Unit unit2 = (GraphInput)unit;
								flow.stack.EnterParentElement(this);
								return unit2.controlOutputs[key4];
							}
						}
						return null;
					});
				}
				else if (unitPortDefinition is ValueInputDefinition)
				{
					ValueInputDefinition valueInputDefinition = (ValueInputDefinition)unitPortDefinition;
					string key3 = valueInputDefinition.key;
					Type type = valueInputDefinition.type;
					bool hasDefaultValue = valueInputDefinition.hasDefaultValue;
					object defaultValue = valueInputDefinition.defaultValue;
					ValueInput valueInput = base.ValueInput(type, key3);
					if (hasDefaultValue)
					{
						valueInput.SetDefaultValue(defaultValue);
					}
				}
				else if (unitPortDefinition is ControlOutputDefinition)
				{
					string key2 = ((ControlOutputDefinition)unitPortDefinition).key;
					base.ControlOutput(key2);
				}
				else if (unitPortDefinition is ValueOutputDefinition)
				{
					ValueOutputDefinition valueOutputDefinition = (ValueOutputDefinition)unitPortDefinition;
					string key = valueOutputDefinition.key;
					Type type2 = valueOutputDefinition.type;
					base.ValueOutput(type2, key, delegate(Flow flow)
					{
						flow.stack.EnterParentElement(this);
						foreach (IUnit unit3 in this.nest.graph.units)
						{
							if (unit3 is GraphOutput)
							{
								GraphOutput graphOutput = (GraphOutput)unit3;
								object value = flow.GetValue(graphOutput.valueInputs[key]);
								flow.stack.ExitParentElement();
								return value;
							}
						}
						flow.stack.ExitParentElement();
						throw new InvalidOperationException("Missing output node when to get value.");
					});
				}
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00011CB0 File Offset: 0x0000FEB0
		public void StartListening(GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StartListening(stack);
				stack.ExitParentElement();
			}
			stack.GetElementData<SubgraphUnit.Data>(this).isListening = true;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00011CDF File Offset: 0x0000FEDF
		public void StopListening(GraphStack stack)
		{
			stack.GetElementData<SubgraphUnit.Data>(this).isListening = false;
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StopListening(stack);
				stack.ExitParentElement();
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00011D0E File Offset: 0x0000FF0E
		public bool IsListening(GraphPointer pointer)
		{
			return pointer.GetElementData<SubgraphUnit.Data>(this).isListening;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00011D1C File Offset: 0x0000FF1C
		public override void AfterAdd()
		{
			base.AfterAdd();
			base.nest.beforeGraphChange += this.StopWatchingPortDefinitions;
			base.nest.afterGraphChange += this.StartWatchingPortDefinitions;
			this.StartWatchingPortDefinitions();
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00011D58 File Offset: 0x0000FF58
		public override void BeforeRemove()
		{
			base.BeforeRemove();
			this.StopWatchingPortDefinitions();
			base.nest.beforeGraphChange -= this.StopWatchingPortDefinitions;
			base.nest.afterGraphChange -= this.StartWatchingPortDefinitions;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00011D94 File Offset: 0x0000FF94
		private void StopWatchingPortDefinitions()
		{
			if (base.nest.graph != null)
			{
				base.nest.graph.onPortDefinitionsChanged -= this.Define;
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		private void StartWatchingPortDefinitions()
		{
			if (base.nest.graph != null)
			{
				base.nest.graph.onPortDefinitionsChanged += this.Define;
			}
		}

		// Token: 0x020001DF RID: 479
		public sealed class Data : IGraphElementData
		{
			// Token: 0x04000410 RID: 1040
			public bool isListening;
		}
	}
}
