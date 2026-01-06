using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000177 RID: 375
	public sealed class ValueOutput : UnitPort<ValueInput, IUnitInputPort, ValueConnection>, IUnitValuePort, IUnitPort, IGraphItem, IUnitOutputPort
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x0001179F File Offset: 0x0000F99F
		public ValueOutput(string key, Type type, Func<Flow, object> getValue)
			: base(key)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			Ensure.That("getValue").IsNotNull<Func<Flow, object>>(getValue);
			this.type = type;
			this.getValue = getValue;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000117D6 File Offset: 0x0000F9D6
		public ValueOutput(string key, Type type)
			: base(key)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			this.type = type;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000117F6 File Offset: 0x0000F9F6
		public bool supportsPrediction
		{
			get
			{
				return this.canPredictValue != null;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00011801 File Offset: 0x0000FA01
		public bool supportsFetch
		{
			get
			{
				return this.getValue != null;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0001180C File Offset: 0x0000FA0C
		public Type type { get; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00011814 File Offset: 0x0000FA14
		public override IEnumerable<ValueConnection> validConnections
		{
			get
			{
				IUnit unit = base.unit;
				IEnumerable<ValueConnection> enumerable;
				if (unit == null)
				{
					enumerable = null;
				}
				else
				{
					FlowGraph graph = unit.graph;
					enumerable = ((graph != null) ? graph.valueConnections.WithSource(this) : null);
				}
				return enumerable ?? Enumerable.Empty<ValueConnection>();
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00011843 File Offset: 0x0000FA43
		public override IEnumerable<InvalidConnection> invalidConnections
		{
			get
			{
				IUnit unit = base.unit;
				IEnumerable<InvalidConnection> enumerable;
				if (unit == null)
				{
					enumerable = null;
				}
				else
				{
					FlowGraph graph = unit.graph;
					enumerable = ((graph != null) ? graph.invalidConnections.WithSource(this) : null);
				}
				return enumerable ?? Enumerable.Empty<InvalidConnection>();
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00011872 File Offset: 0x0000FA72
		public override IEnumerable<ValueInput> validConnectedPorts
		{
			get
			{
				return this.validConnections.Select((ValueConnection c) => c.destination);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0001189E File Offset: 0x0000FA9E
		public override IEnumerable<IUnitInputPort> invalidConnectedPorts
		{
			get
			{
				return this.invalidConnections.Select((InvalidConnection c) => c.destination);
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000118CC File Offset: 0x0000FACC
		public override bool CanConnectToValid(ValueInput port)
		{
			return this.type.IsConvertibleTo(port.type, false);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000118F0 File Offset: 0x0000FAF0
		public override void ConnectToValid(ValueInput port)
		{
			port.Disconnect();
			base.unit.graph.valueConnections.Add(new ValueConnection(this, port));
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00011923 File Offset: 0x0000FB23
		public override void ConnectToInvalid(IUnitInputPort port)
		{
			base.ConnectInvalid(this, port);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00011930 File Offset: 0x0000FB30
		public override void DisconnectFromValid(ValueInput port)
		{
			ValueConnection valueConnection = this.validConnections.SingleOrDefault((ValueConnection c) => c.destination == port);
			if (valueConnection != null)
			{
				base.unit.graph.valueConnections.Remove(valueConnection);
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001197C File Offset: 0x0000FB7C
		public override void DisconnectFromInvalid(IUnitInputPort port)
		{
			base.DisconnectInvalid(this, port);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00011986 File Offset: 0x0000FB86
		public ValueOutput PredictableIf(Func<Flow, bool> condition)
		{
			Ensure.That("condition").IsNotNull<Func<Flow, bool>>(condition);
			this.canPredictValue = condition;
			return this;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000119A0 File Offset: 0x0000FBA0
		public ValueOutput Predictable()
		{
			this.canPredictValue = (Flow flow) => true;
			return this;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000119C8 File Offset: 0x0000FBC8
		public override IUnitPort CompatiblePort(IUnit unit)
		{
			if (unit == base.unit)
			{
				return null;
			}
			return unit.CompatibleValueInput(this.type);
		}

		// Token: 0x0400020E RID: 526
		internal readonly Func<Flow, object> getValue;

		// Token: 0x0400020F RID: 527
		internal Func<Flow, bool> canPredictValue;
	}
}
