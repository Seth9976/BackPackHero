using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200017D RID: 381
	[SerializationVersion("A", new Type[] { })]
	public abstract class Unit : GraphElement<FlowGraph>, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x06000A00 RID: 2560 RVA: 0x00011DEC File Offset: 0x0000FFEC
		protected Unit()
		{
			this.controlInputs = new UnitPortCollection<ControlInput>(this);
			this.controlOutputs = new UnitPortCollection<ControlOutput>(this);
			this.valueInputs = new UnitPortCollection<ValueInput>(this);
			this.valueOutputs = new UnitPortCollection<ValueOutput>(this);
			this.invalidInputs = new UnitPortCollection<InvalidInput>(this);
			this.invalidOutputs = new UnitPortCollection<InvalidOutput>(this);
			this.relations = new ConnectionCollection<IUnitRelation, IUnitPort, IUnitPort>();
			this.defaultValues = new Dictionary<string, object>();
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00011E5D File Offset: 0x0001005D
		public virtual IGraphElementDebugData CreateDebugData()
		{
			return new Unit.DebugData();
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00011E64 File Offset: 0x00010064
		public override void AfterAdd()
		{
			this.Define();
			base.AfterAdd();
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00011E72 File Offset: 0x00010072
		public override void BeforeRemove()
		{
			base.BeforeRemove();
			this.Disconnect();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00011E80 File Offset: 0x00010080
		public override void Instantiate(GraphReference instance)
		{
			base.Instantiate(instance);
			IGraphEventListener graphEventListener = this as IGraphEventListener;
			if (graphEventListener != null && XGraphEventListener.IsHierarchyListening(instance))
			{
				graphEventListener.StartListening(instance);
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00011EB0 File Offset: 0x000100B0
		public override void Uninstantiate(GraphReference instance)
		{
			IGraphEventListener graphEventListener = this as IGraphEventListener;
			if (graphEventListener != null)
			{
				graphEventListener.StopListening(instance);
			}
			base.Uninstantiate(instance);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00011ED5 File Offset: 0x000100D5
		protected void CopyFrom(Unit source)
		{
			base.CopyFrom(source);
			this.defaultValues = source.defaultValues;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00011EEA File Offset: 0x000100EA
		[DoNotSerialize]
		public virtual bool canDefine
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00011EED File Offset: 0x000100ED
		[DoNotSerialize]
		public bool failedToDefine
		{
			get
			{
				return this.definitionException != null;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00011EF8 File Offset: 0x000100F8
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00011F00 File Offset: 0x00010100
		[DoNotSerialize]
		public bool isDefined { get; private set; }

		// Token: 0x06000A0B RID: 2571
		protected abstract void Definition();

		// Token: 0x06000A0C RID: 2572 RVA: 0x00011F09 File Offset: 0x00010109
		protected virtual void AfterDefine()
		{
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00011F0B File Offset: 0x0001010B
		protected virtual void BeforeUndefine()
		{
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00011F10 File Offset: 0x00010110
		private void Undefine()
		{
			if (this.isDefined)
			{
				this.BeforeUndefine();
			}
			this.Disconnect();
			this.defaultValues.Clear();
			this.controlInputs.Clear();
			this.controlOutputs.Clear();
			this.valueInputs.Clear();
			this.valueOutputs.Clear();
			this.invalidInputs.Clear();
			this.invalidOutputs.Clear();
			this.relations.Clear();
			this.isDefined = false;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00011F90 File Offset: 0x00010190
		public void EnsureDefined()
		{
			if (!this.isDefined)
			{
				this.Define();
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00011FA0 File Offset: 0x000101A0
		public void Define()
		{
			UnitPreservation unitPreservation = UnitPreservation.Preserve(this);
			this.Undefine();
			if (this.canDefine)
			{
				try
				{
					this.Definition();
					this.isDefined = true;
					this.definitionException = null;
					this.AfterDefine();
				}
				catch (Exception ex)
				{
					this.Undefine();
					this.definitionException = ex;
					Debug.LogWarning(string.Format("Failed to define {0}:\n{1}", this, ex));
				}
			}
			unitPreservation.RestoreTo(this);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00012018 File Offset: 0x00010218
		public void RemoveUnconnectedInvalidPorts()
		{
			foreach (InvalidInput invalidInput in this.invalidInputs.Where((InvalidInput p) => !p.hasAnyConnection).ToArray<InvalidInput>())
			{
				this.invalidInputs.Remove(invalidInput);
			}
			foreach (InvalidOutput invalidOutput in this.invalidOutputs.Where((InvalidOutput p) => !p.hasAnyConnection).ToArray<InvalidOutput>())
			{
				this.invalidOutputs.Remove(invalidOutput);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000120C5 File Offset: 0x000102C5
		[DoNotSerialize]
		public IUnitPortCollection<ControlInput> controlInputs { get; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x000120CD File Offset: 0x000102CD
		[DoNotSerialize]
		public IUnitPortCollection<ControlOutput> controlOutputs { get; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x000120D5 File Offset: 0x000102D5
		[DoNotSerialize]
		public IUnitPortCollection<ValueInput> valueInputs { get; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x000120DD File Offset: 0x000102DD
		[DoNotSerialize]
		public IUnitPortCollection<ValueOutput> valueOutputs { get; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x000120E5 File Offset: 0x000102E5
		[DoNotSerialize]
		public IUnitPortCollection<InvalidInput> invalidInputs { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000120ED File Offset: 0x000102ED
		[DoNotSerialize]
		public IUnitPortCollection<InvalidOutput> invalidOutputs { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000120F5 File Offset: 0x000102F5
		[DoNotSerialize]
		public IEnumerable<IUnitInputPort> inputs
		{
			get
			{
				return LinqUtility.Concat<IUnitInputPort>(new IEnumerable[] { this.controlInputs, this.valueInputs, this.invalidInputs });
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0001211D File Offset: 0x0001031D
		[DoNotSerialize]
		public IEnumerable<IUnitOutputPort> outputs
		{
			get
			{
				return LinqUtility.Concat<IUnitOutputPort>(new IEnumerable[] { this.controlOutputs, this.valueOutputs, this.invalidOutputs });
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00012145 File Offset: 0x00010345
		[DoNotSerialize]
		public IEnumerable<IUnitInputPort> validInputs
		{
			get
			{
				return LinqUtility.Concat<IUnitInputPort>(new IEnumerable[] { this.controlInputs, this.valueInputs });
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00012164 File Offset: 0x00010364
		[DoNotSerialize]
		public IEnumerable<IUnitOutputPort> validOutputs
		{
			get
			{
				return LinqUtility.Concat<IUnitOutputPort>(new IEnumerable[] { this.controlOutputs, this.valueOutputs });
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00012183 File Offset: 0x00010383
		[DoNotSerialize]
		public IEnumerable<IUnitPort> ports
		{
			get
			{
				return LinqUtility.Concat<IUnitPort>(new IEnumerable[] { this.inputs, this.outputs });
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x000121A2 File Offset: 0x000103A2
		[DoNotSerialize]
		public IEnumerable<IUnitPort> invalidPorts
		{
			get
			{
				return LinqUtility.Concat<IUnitPort>(new IEnumerable[] { this.invalidInputs, this.invalidOutputs });
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000121C1 File Offset: 0x000103C1
		[DoNotSerialize]
		public IEnumerable<IUnitPort> validPorts
		{
			get
			{
				return LinqUtility.Concat<IUnitPort>(new IEnumerable[] { this.validInputs, this.validOutputs });
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000A1F RID: 2591 RVA: 0x000121E0 File Offset: 0x000103E0
		// (remove) Token: 0x06000A20 RID: 2592 RVA: 0x00012218 File Offset: 0x00010418
		public event Action onPortsChanged;

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001224D File Offset: 0x0001044D
		public void PortsChanged()
		{
			Action action = this.onPortsChanged;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0001225F File Offset: 0x0001045F
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00012267 File Offset: 0x00010467
		[Serialize]
		public Dictionary<string, object> defaultValues { get; private set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00012270 File Offset: 0x00010470
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00012278 File Offset: 0x00010478
		[DoNotSerialize]
		public IConnectionCollection<IUnitRelation, IUnitPort, IUnitPort> relations { get; private set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00012281 File Offset: 0x00010481
		[DoNotSerialize]
		public IEnumerable<IUnitConnection> connections
		{
			get
			{
				return this.ports.SelectMany((IUnitPort p) => p.connections);
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000122B0 File Offset: 0x000104B0
		public void Disconnect()
		{
			for (;;)
			{
				if (!this.ports.Any((IUnitPort p) => p.hasAnyConnection))
				{
					break;
				}
				this.ports.First((IUnitPort p) => p.hasAnyConnection).Disconnect();
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0001231A File Offset: 0x0001051A
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x00012322 File Offset: 0x00010522
		[DoNotSerialize]
		public virtual bool isControlRoot { get; protected set; }

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001232C File Offset: 0x0001052C
		protected void EnsureUniqueInput(string key)
		{
			if (this.controlInputs.Contains(key) || this.valueInputs.Contains(key) || this.invalidInputs.Contains(key))
			{
				throw new ArgumentException(string.Format("Duplicate input for '{0}' in {1}.", key, base.GetType()));
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001237C File Offset: 0x0001057C
		protected void EnsureUniqueOutput(string key)
		{
			if (this.controlOutputs.Contains(key) || this.valueOutputs.Contains(key) || this.invalidOutputs.Contains(key))
			{
				throw new ArgumentException(string.Format("Duplicate output for '{0}' in {1}.", key, base.GetType()));
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000123CC File Offset: 0x000105CC
		protected ControlInput ControlInput(string key, Func<Flow, ControlOutput> action)
		{
			this.EnsureUniqueInput(key);
			ControlInput controlInput = new ControlInput(key, action);
			this.controlInputs.Add(controlInput);
			return controlInput;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000123F8 File Offset: 0x000105F8
		protected ControlInput ControlInputCoroutine(string key, Func<Flow, IEnumerator> coroutineAction)
		{
			this.EnsureUniqueInput(key);
			ControlInput controlInput = new ControlInput(key, coroutineAction);
			this.controlInputs.Add(controlInput);
			return controlInput;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00012424 File Offset: 0x00010624
		protected ControlInput ControlInputCoroutine(string key, Func<Flow, ControlOutput> action, Func<Flow, IEnumerator> coroutineAction)
		{
			this.EnsureUniqueInput(key);
			ControlInput controlInput = new ControlInput(key, action, coroutineAction);
			this.controlInputs.Add(controlInput);
			return controlInput;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00012450 File Offset: 0x00010650
		protected ControlOutput ControlOutput(string key)
		{
			this.EnsureUniqueOutput(key);
			ControlOutput controlOutput = new ControlOutput(key);
			this.controlOutputs.Add(controlOutput);
			return controlOutput;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00012478 File Offset: 0x00010678
		protected ValueInput ValueInput(Type type, string key)
		{
			this.EnsureUniqueInput(key);
			ValueInput valueInput = new ValueInput(key, type);
			this.valueInputs.Add(valueInput);
			return valueInput;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x000124A1 File Offset: 0x000106A1
		protected ValueInput ValueInput<T>(string key)
		{
			return this.ValueInput(typeof(T), key);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000124B4 File Offset: 0x000106B4
		protected ValueInput ValueInput<T>(string key, T @default)
		{
			ValueInput valueInput = this.ValueInput<T>(key);
			valueInput.SetDefaultValue(@default);
			return valueInput;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000124CC File Offset: 0x000106CC
		protected ValueOutput ValueOutput(Type type, string key)
		{
			this.EnsureUniqueOutput(key);
			ValueOutput valueOutput = new ValueOutput(key, type);
			this.valueOutputs.Add(valueOutput);
			return valueOutput;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000124F8 File Offset: 0x000106F8
		protected ValueOutput ValueOutput(Type type, string key, Func<Flow, object> getValue)
		{
			this.EnsureUniqueOutput(key);
			ValueOutput valueOutput = new ValueOutput(key, type, getValue);
			this.valueOutputs.Add(valueOutput);
			return valueOutput;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00012522 File Offset: 0x00010722
		protected ValueOutput ValueOutput<T>(string key)
		{
			return this.ValueOutput(typeof(T), key);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00012538 File Offset: 0x00010738
		protected ValueOutput ValueOutput<T>(string key, Func<Flow, T> getValue)
		{
			return this.ValueOutput(typeof(T), key, (Flow recursion) => getValue(recursion));
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001256F File Offset: 0x0001076F
		private void Relation(IUnitPort source, IUnitPort destination)
		{
			this.relations.Add(new UnitRelation(source, destination));
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00012583 File Offset: 0x00010783
		protected void Requirement(ValueInput source, ControlInput destination)
		{
			this.Relation(source, destination);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001258D File Offset: 0x0001078D
		protected void Requirement(ValueInput source, ValueOutput destination)
		{
			this.Relation(source, destination);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00012597 File Offset: 0x00010797
		protected void Assignment(ControlInput source, ValueOutput destination)
		{
			this.Relation(source, destination);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000125A1 File Offset: 0x000107A1
		protected void Succession(ControlInput source, ControlOutput destination)
		{
			this.Relation(source, destination);
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x000125AB File Offset: 0x000107AB
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x000125B3 File Offset: 0x000107B3
		[Serialize]
		public Vector2 position { get; set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000125BC File Offset: 0x000107BC
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x000125C4 File Offset: 0x000107C4
		[DoNotSerialize]
		public Exception definitionException { get; protected set; }

		// Token: 0x06000A40 RID: 2624 RVA: 0x000125CD File Offset: 0x000107CD
		public override AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			AnalyticsIdentifier analyticsIdentifier = new AnalyticsIdentifier();
			analyticsIdentifier.Identifier = base.GetType().FullName;
			analyticsIdentifier.Namespace = base.GetType().Namespace;
			analyticsIdentifier.Hashcode = analyticsIdentifier.Identifier.GetHashCode();
			return analyticsIdentifier;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00012607 File Offset: 0x00010807
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}

		// Token: 0x020001E2 RID: 482
		public class DebugData : IUnitDebugData, IGraphElementDebugData
		{
			// Token: 0x170003E0 RID: 992
			// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0001BFF8 File Offset: 0x0001A1F8
			// (set) Token: 0x06000C63 RID: 3171 RVA: 0x0001C000 File Offset: 0x0001A200
			public int lastInvokeFrame { get; set; }

			// Token: 0x170003E1 RID: 993
			// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0001C009 File Offset: 0x0001A209
			// (set) Token: 0x06000C65 RID: 3173 RVA: 0x0001C011 File Offset: 0x0001A211
			public float lastInvokeTime { get; set; }

			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0001C01A File Offset: 0x0001A21A
			// (set) Token: 0x06000C67 RID: 3175 RVA: 0x0001C022 File Offset: 0x0001A222
			public Exception runtimeException { get; set; }
		}
	}
}
