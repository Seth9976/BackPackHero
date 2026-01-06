using System;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000003 RID: 3
	public sealed class InvalidConnection : UnitConnection<IUnitOutputPort, IUnitInputPort>, IUnitConnection, IConnection<IUnitOutputPort, IUnitInputPort>, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020ED File Offset: 0x000002ED
		[Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public InvalidConnection()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020F5 File Offset: 0x000002F5
		public InvalidConnection(IUnitOutputPort source, IUnitInputPort destination)
			: base(source, destination)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020FF File Offset: 0x000002FF
		public override void AfterRemove()
		{
			base.AfterRemove();
			this.source.unit.RemoveUnconnectedInvalidPorts();
			this.destination.unit.RemoveUnconnectedInvalidPorts();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002127 File Offset: 0x00000327
		public override IUnitOutputPort source
		{
			get
			{
				return base.sourceUnit.outputs.Single((IUnitOutputPort p) => p.key == base.sourceKey);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002145 File Offset: 0x00000345
		public override IUnitInputPort destination
		{
			get
			{
				return base.destinationUnit.inputs.Single((IUnitInputPort p) => p.key == base.destinationKey);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002163 File Offset: 0x00000363
		public IUnitOutputPort validSource
		{
			get
			{
				return base.sourceUnit.validOutputs.Single((IUnitOutputPort p) => p.key == base.sourceKey);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002181 File Offset: 0x00000381
		public IUnitInputPort validDestination
		{
			get
			{
				return base.destinationUnit.validInputs.Single((IUnitInputPort p) => p.key == base.destinationKey);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000219F File Offset: 0x0000039F
		public override bool sourceExists
		{
			get
			{
				return base.sourceUnit.outputs.Any((IUnitOutputPort p) => p.key == base.sourceKey);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021BD File Offset: 0x000003BD
		public override bool destinationExists
		{
			get
			{
				return base.destinationUnit.inputs.Any((IUnitInputPort p) => p.key == base.destinationKey);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021DB File Offset: 0x000003DB
		public bool validSourceExists
		{
			get
			{
				return base.sourceUnit.validOutputs.Any((IUnitOutputPort p) => p.key == base.sourceKey);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021F9 File Offset: 0x000003F9
		public bool validDestinationExists
		{
			get
			{
				return base.destinationUnit.validInputs.Any((IUnitInputPort p) => p.key == base.destinationKey);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002218 File Offset: 0x00000418
		public override bool HandleDependencies()
		{
			if (this.validSourceExists && this.validDestinationExists && this.validSource.CanValidlyConnectTo(this.validDestination))
			{
				this.validSource.ValidlyConnectTo(this.validDestination);
				return false;
			}
			if (!this.sourceExists)
			{
				base.sourceUnit.invalidOutputs.Add(new InvalidOutput(base.sourceKey));
			}
			if (!this.destinationExists)
			{
				base.destinationUnit.invalidInputs.Add(new InvalidInput(base.destinationKey));
			}
			return true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022A2 File Offset: 0x000004A2
		FlowGraph IUnitConnection.get_graph()
		{
			return base.graph;
		}
	}
}
