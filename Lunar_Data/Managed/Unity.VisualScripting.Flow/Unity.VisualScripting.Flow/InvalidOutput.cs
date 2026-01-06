using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000164 RID: 356
	public sealed class InvalidOutput : UnitPort<IUnitInputPort, IUnitInputPort, InvalidConnection>, IUnitInvalidPort, IUnitPort, IGraphItem, IUnitOutputPort
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x00010D37 File Offset: 0x0000EF37
		public InvalidOutput(string key)
			: base(key)
		{
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00010D40 File Offset: 0x0000EF40
		public override IEnumerable<InvalidConnection> validConnections
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

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00010D6F File Offset: 0x0000EF6F
		public override IEnumerable<InvalidConnection> invalidConnections
		{
			get
			{
				return Enumerable.Empty<InvalidConnection>();
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00010D76 File Offset: 0x0000EF76
		public override IEnumerable<IUnitInputPort> validConnectedPorts
		{
			get
			{
				return this.validConnections.Select((InvalidConnection c) => c.destination);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		public override IEnumerable<IUnitInputPort> invalidConnectedPorts
		{
			get
			{
				return this.invalidConnections.Select((InvalidConnection c) => c.destination);
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00010DCE File Offset: 0x0000EFCE
		public override bool CanConnectToValid(IUnitInputPort port)
		{
			return false;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00010DD1 File Offset: 0x0000EFD1
		public override void ConnectToValid(IUnitInputPort port)
		{
			base.ConnectInvalid(this, port);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00010DDB File Offset: 0x0000EFDB
		public override void ConnectToInvalid(IUnitInputPort port)
		{
			base.ConnectInvalid(this, port);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00010DE5 File Offset: 0x0000EFE5
		public override void DisconnectFromValid(IUnitInputPort port)
		{
			base.DisconnectInvalid(this, port);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00010DEF File Offset: 0x0000EFEF
		public override void DisconnectFromInvalid(IUnitInputPort port)
		{
			base.DisconnectInvalid(this, port);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00010DF9 File Offset: 0x0000EFF9
		public override IUnitPort CompatiblePort(IUnit unit)
		{
			return null;
		}
	}
}
