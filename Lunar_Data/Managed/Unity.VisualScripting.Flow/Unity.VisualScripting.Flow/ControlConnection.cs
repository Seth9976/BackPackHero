using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000002 RID: 2
	public sealed class ControlConnection : UnitConnection<ControlOutput, ControlInput>, IUnitConnection, IConnection<IUnitOutputPort, IUnitInputPort>, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public ControlConnection()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public ControlConnection(ControlOutput source, ControlInput destination)
			: base(source, destination)
		{
			if (source.hasValidConnection)
			{
				throw new InvalidConnectionException("Control output ports do not support multiple connections.");
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002075 File Offset: 0x00000275
		public override ControlOutput source
		{
			get
			{
				return base.sourceUnit.controlOutputs[base.sourceKey];
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000208D File Offset: 0x0000028D
		public override ControlInput destination
		{
			get
			{
				return base.destinationUnit.controlInputs[base.destinationKey];
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020A5 File Offset: 0x000002A5
		IUnitOutputPort IConnection<IUnitOutputPort, IUnitInputPort>.source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020AD File Offset: 0x000002AD
		IUnitInputPort IConnection<IUnitOutputPort, IUnitInputPort>.destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020B5 File Offset: 0x000002B5
		public override bool sourceExists
		{
			get
			{
				return base.sourceUnit.controlOutputs.Contains(base.sourceKey);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020CD File Offset: 0x000002CD
		public override bool destinationExists
		{
			get
			{
				return base.destinationUnit.controlInputs.Contains(base.destinationKey);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E5 File Offset: 0x000002E5
		FlowGraph IUnitConnection.get_graph()
		{
			return base.graph;
		}
	}
}
