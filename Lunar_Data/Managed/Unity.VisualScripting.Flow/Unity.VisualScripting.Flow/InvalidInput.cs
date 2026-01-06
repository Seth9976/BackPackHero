using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000163 RID: 355
	public sealed class InvalidInput : UnitPort<IUnitOutputPort, IUnitOutputPort, InvalidConnection>, IUnitInvalidPort, IUnitPort, IGraphItem, IUnitInputPort
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x00010C72 File Offset: 0x0000EE72
		public InvalidInput(string key)
			: base(key)
		{
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00010C7B File Offset: 0x0000EE7B
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
					enumerable = ((graph != null) ? graph.invalidConnections.WithDestination(this) : null);
				}
				return enumerable ?? Enumerable.Empty<InvalidConnection>();
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00010CAA File Offset: 0x0000EEAA
		public override IEnumerable<InvalidConnection> invalidConnections
		{
			get
			{
				return Enumerable.Empty<InvalidConnection>();
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00010CB1 File Offset: 0x0000EEB1
		public override IEnumerable<IUnitOutputPort> validConnectedPorts
		{
			get
			{
				return this.validConnections.Select((InvalidConnection c) => c.source);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00010CDD File Offset: 0x0000EEDD
		public override IEnumerable<IUnitOutputPort> invalidConnectedPorts
		{
			get
			{
				return this.invalidConnections.Select((InvalidConnection c) => c.source);
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00010D09 File Offset: 0x0000EF09
		public override bool CanConnectToValid(IUnitOutputPort port)
		{
			return false;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00010D0C File Offset: 0x0000EF0C
		public override void ConnectToValid(IUnitOutputPort port)
		{
			base.ConnectInvalid(port, this);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00010D16 File Offset: 0x0000EF16
		public override void ConnectToInvalid(IUnitOutputPort port)
		{
			base.ConnectInvalid(port, this);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00010D20 File Offset: 0x0000EF20
		public override void DisconnectFromValid(IUnitOutputPort port)
		{
			base.DisconnectInvalid(port, this);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00010D2A File Offset: 0x0000EF2A
		public override void DisconnectFromInvalid(IUnitOutputPort port)
		{
			base.DisconnectInvalid(port, this);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00010D34 File Offset: 0x0000EF34
		public override IUnitPort CompatiblePort(IUnit unit)
		{
			return null;
		}
	}
}
