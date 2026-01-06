using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000160 RID: 352
	public sealed class ControlOutput : UnitPort<ControlInput, IUnitInputPort, ControlConnection>, IUnitControlPort, IUnitPort, IGraphItem, IUnitOutputPort
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x0001096A File Offset: 0x0000EB6A
		public ControlOutput(string key)
			: base(key)
		{
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00010973 File Offset: 0x0000EB73
		public override IEnumerable<ControlConnection> validConnections
		{
			get
			{
				IUnit unit = base.unit;
				IEnumerable<ControlConnection> enumerable;
				if (unit == null)
				{
					enumerable = null;
				}
				else
				{
					FlowGraph graph = unit.graph;
					enumerable = ((graph != null) ? graph.controlConnections.WithSource(this) : null);
				}
				return enumerable ?? Enumerable.Empty<ControlConnection>();
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000109A2 File Offset: 0x0000EBA2
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

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000109D1 File Offset: 0x0000EBD1
		public override IEnumerable<ControlInput> validConnectedPorts
		{
			get
			{
				return this.validConnections.Select((ControlConnection c) => c.destination);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000109FD File Offset: 0x0000EBFD
		public override IEnumerable<IUnitInputPort> invalidConnectedPorts
		{
			get
			{
				return this.invalidConnections.Select((InvalidConnection c) => c.destination);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00010A2C File Offset: 0x0000EC2C
		public bool isPredictable
		{
			get
			{
				bool flag;
				using (Recursion recursion = Recursion.New(1))
				{
					flag = this.IsPredictable(recursion);
				}
				return flag;
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00010A68 File Offset: 0x0000EC68
		public bool IsPredictable(Recursion recursion)
		{
			if (base.unit.isControlRoot)
			{
				return true;
			}
			Recursion recursion2 = recursion;
			if (recursion2 != null && !recursion2.TryEnter(this))
			{
				return false;
			}
			bool flag = (from r in base.unit.relations.WithDestination(this)
				where r.source is ControlInput
				select r).All((IUnitRelation r) => ((ControlInput)r.source).IsPredictable(recursion));
			Recursion recursion3 = recursion;
			if (recursion3 == null)
			{
				return flag;
			}
			recursion3.Exit(this);
			return flag;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00010B04 File Offset: 0x0000ED04
		public bool couldBeEntered
		{
			get
			{
				if (!this.isPredictable)
				{
					throw new NotSupportedException();
				}
				if (base.unit.isControlRoot)
				{
					return true;
				}
				return (from r in base.unit.relations.WithDestination(this)
					where r.source is ControlInput
					select r).Any((IUnitRelation r) => ((ControlInput)r.source).couldBeEntered);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00010B87 File Offset: 0x0000ED87
		public ControlConnection connection
		{
			get
			{
				FlowGraph graph = base.unit.graph;
				if (graph == null)
				{
					return null;
				}
				return graph.controlConnections.SingleOrDefaultWithSource(this);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00010BA5 File Offset: 0x0000EDA5
		public override bool hasValidConnection
		{
			get
			{
				return this.connection != null;
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		public override bool CanConnectToValid(ControlInput port)
		{
			return true;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		public override void ConnectToValid(ControlInput port)
		{
			this.Disconnect();
			base.unit.graph.controlConnections.Add(new ControlConnection(this, port));
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00010BE7 File Offset: 0x0000EDE7
		public override void ConnectToInvalid(IUnitInputPort port)
		{
			base.ConnectInvalid(this, port);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		public override void DisconnectFromValid(ControlInput port)
		{
			ControlConnection controlConnection = this.validConnections.SingleOrDefault((ControlConnection c) => c.destination == port);
			if (controlConnection != null)
			{
				base.unit.graph.controlConnections.Remove(controlConnection);
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00010C40 File Offset: 0x0000EE40
		public override void DisconnectFromInvalid(IUnitInputPort port)
		{
			base.DisconnectInvalid(this, port);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override IUnitPort CompatiblePort(IUnit unit)
		{
			if (unit == base.unit)
			{
				return null;
			}
			return unit.controlInputs.FirstOrDefault<ControlInput>();
		}
	}
}
