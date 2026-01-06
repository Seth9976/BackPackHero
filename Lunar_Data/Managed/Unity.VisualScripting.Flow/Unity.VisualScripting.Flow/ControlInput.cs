using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x0200015E RID: 350
	public sealed class ControlInput : UnitPort<ControlOutput, IUnitOutputPort, ControlConnection>, IUnitControlPort, IUnitPort, IGraphItem, IUnitInputPort
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x00010678 File Offset: 0x0000E878
		public ControlInput(string key, Func<Flow, ControlOutput> action)
			: base(key)
		{
			Ensure.That("action").IsNotNull<Func<Flow, ControlOutput>>(action);
			this.action = action;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00010698 File Offset: 0x0000E898
		public ControlInput(string key, Func<Flow, IEnumerator> coroutineAction)
			: base(key)
		{
			Ensure.That("coroutineAction").IsNotNull<Func<Flow, IEnumerator>>(coroutineAction);
			this.coroutineAction = coroutineAction;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000106B8 File Offset: 0x0000E8B8
		public ControlInput(string key, Func<Flow, ControlOutput> action, Func<Flow, IEnumerator> coroutineAction)
			: base(key)
		{
			Ensure.That("action").IsNotNull<Func<Flow, ControlOutput>>(action);
			Ensure.That("coroutineAction").IsNotNull<Func<Flow, IEnumerator>>(coroutineAction);
			this.action = action;
			this.coroutineAction = coroutineAction;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x000106EF File Offset: 0x0000E8EF
		public bool supportsCoroutine
		{
			get
			{
				return this.coroutineAction != null;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x000106FA File Offset: 0x0000E8FA
		public bool requiresCoroutine
		{
			get
			{
				return this.action == null;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00010705 File Offset: 0x0000E905
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
					enumerable = ((graph != null) ? graph.controlConnections.WithDestination(this) : null);
				}
				return enumerable ?? Enumerable.Empty<ControlConnection>();
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00010734 File Offset: 0x0000E934
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

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00010763 File Offset: 0x0000E963
		public override IEnumerable<ControlOutput> validConnectedPorts
		{
			get
			{
				return this.validConnections.Select((ControlConnection c) => c.source);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001078F File Offset: 0x0000E98F
		public override IEnumerable<IUnitOutputPort> invalidConnectedPorts
		{
			get
			{
				return this.invalidConnections.Select((InvalidConnection c) => c.source);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000107BC File Offset: 0x0000E9BC
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

		// Token: 0x06000929 RID: 2345 RVA: 0x000107F8 File Offset: 0x0000E9F8
		public bool IsPredictable(Recursion recursion)
		{
			if (!this.hasValidConnection)
			{
				return true;
			}
			Recursion recursion2 = recursion;
			if (recursion2 != null && !recursion2.TryEnter(this))
			{
				return false;
			}
			bool flag = this.validConnectedPorts.All((ControlOutput cop) => cop.IsPredictable(recursion));
			Recursion recursion3 = recursion;
			if (recursion3 == null)
			{
				return flag;
			}
			recursion3.Exit(this);
			return flag;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00010860 File Offset: 0x0000EA60
		public bool couldBeEntered
		{
			get
			{
				if (!this.isPredictable)
				{
					throw new NotSupportedException();
				}
				if (!this.hasValidConnection)
				{
					return false;
				}
				return this.validConnectedPorts.Any((ControlOutput cop) => cop.couldBeEntered);
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000108AF File Offset: 0x0000EAAF
		public override bool CanConnectToValid(ControlOutput port)
		{
			return true;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000108B4 File Offset: 0x0000EAB4
		public override void ConnectToValid(ControlOutput port)
		{
			port.Disconnect();
			base.unit.graph.controlConnections.Add(new ControlConnection(port, this));
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000108E7 File Offset: 0x0000EAE7
		public override void ConnectToInvalid(IUnitOutputPort port)
		{
			base.ConnectInvalid(port, this);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000108F4 File Offset: 0x0000EAF4
		public override void DisconnectFromValid(ControlOutput port)
		{
			ControlConnection controlConnection = this.validConnections.SingleOrDefault((ControlConnection c) => c.source == port);
			if (controlConnection != null)
			{
				base.unit.graph.controlConnections.Remove(controlConnection);
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00010940 File Offset: 0x0000EB40
		public override void DisconnectFromInvalid(IUnitOutputPort port)
		{
			base.DisconnectInvalid(port, this);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001094A File Offset: 0x0000EB4A
		public override IUnitPort CompatiblePort(IUnit unit)
		{
			if (unit == base.unit)
			{
				return null;
			}
			return unit.controlOutputs.FirstOrDefault<ControlOutput>();
		}

		// Token: 0x040001FF RID: 511
		internal readonly Func<Flow, ControlOutput> action;

		// Token: 0x04000200 RID: 512
		internal readonly Func<Flow, IEnumerator> coroutineAction;
	}
}
