using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity.VisualScripting
{
	// Token: 0x02000175 RID: 373
	public sealed class ValueInput : UnitPort<ValueOutput, IUnitOutputPort, ValueConnection>, IUnitValuePort, IUnitPort, IGraphItem, IUnitInputPort
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public ValueInput(string key, Type type)
			: base(key)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			this.type = type;
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x000112F8 File Offset: 0x0000F4F8
		public Type type { get; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00011300 File Offset: 0x0000F500
		public bool hasDefaultValue
		{
			get
			{
				return base.unit.defaultValues.ContainsKey(base.key);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00011318 File Offset: 0x0000F518
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
					enumerable = ((graph != null) ? graph.valueConnections.WithDestination(this) : null);
				}
				return enumerable ?? Enumerable.Empty<ValueConnection>();
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00011347 File Offset: 0x0000F547
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
					enumerable = ((graph != null) ? graph.invalidConnections.WithDestination(this) : null);
				}
				return enumerable ?? Enumerable.Empty<InvalidConnection>();
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00011376 File Offset: 0x0000F576
		public override IEnumerable<ValueOutput> validConnectedPorts
		{
			get
			{
				return this.validConnections.Select((ValueConnection c) => c.source);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000113A2 File Offset: 0x0000F5A2
		public override IEnumerable<IUnitOutputPort> invalidConnectedPorts
		{
			get
			{
				return this.invalidConnections.Select((InvalidConnection c) => c.source);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000113CE File Offset: 0x0000F5CE
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x000113E6 File Offset: 0x0000F5E6
		[DoNotSerialize]
		internal object _defaultValue
		{
			get
			{
				return base.unit.defaultValues[base.key];
			}
			set
			{
				base.unit.defaultValues[base.key] = value;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x000113FF File Offset: 0x0000F5FF
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00011407 File Offset: 0x0000F607
		public bool nullMeansSelf { get; private set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00011410 File Offset: 0x0000F610
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00011418 File Offset: 0x0000F618
		public bool allowsNull { get; private set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00011421 File Offset: 0x0000F621
		public ValueConnection connection
		{
			get
			{
				FlowGraph graph = base.unit.graph;
				if (graph == null)
				{
					return null;
				}
				return graph.valueConnections.SingleOrDefaultWithDestination(this);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0001143F File Offset: 0x0000F63F
		public override bool hasValidConnection
		{
			get
			{
				return this.connection != null;
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001144C File Offset: 0x0000F64C
		public void SetDefaultValue(object value)
		{
			Ensure.That("value").IsOfType<object>(value, this.type);
			if (!ValueInput.SupportsDefaultValue(this.type))
			{
				return;
			}
			if (base.unit.defaultValues.ContainsKey(base.key))
			{
				base.unit.defaultValues[base.key] = value;
				return;
			}
			base.unit.defaultValues.Add(base.key, value);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000114C4 File Offset: 0x0000F6C4
		public override bool CanConnectToValid(ValueOutput port)
		{
			return port.type.IsConvertibleTo(this.type, false);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000114E8 File Offset: 0x0000F6E8
		public override void ConnectToValid(ValueOutput port)
		{
			this.Disconnect();
			base.unit.graph.valueConnections.Add(new ValueConnection(port, this));
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001151B File Offset: 0x0000F71B
		public override void ConnectToInvalid(IUnitOutputPort port)
		{
			base.ConnectInvalid(port, this);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00011528 File Offset: 0x0000F728
		public override void DisconnectFromValid(ValueOutput port)
		{
			ValueConnection valueConnection = this.validConnections.SingleOrDefault((ValueConnection c) => c.source == port);
			if (valueConnection != null)
			{
				base.unit.graph.valueConnections.Remove(valueConnection);
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00011574 File Offset: 0x0000F774
		public override void DisconnectFromInvalid(IUnitOutputPort port)
		{
			base.DisconnectInvalid(port, this);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001157E File Offset: 0x0000F77E
		public ValueInput NullMeansSelf()
		{
			if (ComponentHolderProtocol.IsComponentHolderType(this.type))
			{
				this.nullMeansSelf = true;
			}
			return this;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00011595 File Offset: 0x0000F795
		public ValueInput AllowsNull()
		{
			if (this.type.IsNullable())
			{
				this.allowsNull = true;
			}
			return this;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000115AC File Offset: 0x0000F7AC
		public static bool SupportsDefaultValue(Type type)
		{
			return ValueInput.typesWithDefaultValues.Contains(type) || ValueInput.typesWithDefaultValues.Contains(Nullable.GetUnderlyingType(type)) || type.IsBasic() || typeof(Object).IsAssignableFrom(type);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000115E7 File Offset: 0x0000F7E7
		public override IUnitPort CompatiblePort(IUnit unit)
		{
			if (unit == base.unit)
			{
				return null;
			}
			return unit.CompatibleValueOutput(this.type);
		}

		// Token: 0x0400020B RID: 523
		private static readonly HashSet<Type> typesWithDefaultValues = new HashSet<Type>
		{
			typeof(Vector2),
			typeof(Vector3),
			typeof(Vector4),
			typeof(Color),
			typeof(AnimationCurve),
			typeof(Rect),
			typeof(Ray),
			typeof(Ray2D),
			typeof(Type),
			typeof(InputAction)
		};
	}
}
