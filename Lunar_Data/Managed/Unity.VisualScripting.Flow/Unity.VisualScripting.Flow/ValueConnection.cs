using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200000A RID: 10
	public sealed class ValueConnection : UnitConnection<ValueOutput, ValueInput>, IUnitConnection, IConnection<IUnitOutputPort, IUnitInputPort>, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x06000041 RID: 65 RVA: 0x0000267A File Offset: 0x0000087A
		public override IGraphElementDebugData CreateDebugData()
		{
			return new ValueConnection.DebugData();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002681 File Offset: 0x00000881
		[Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public ValueConnection()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000268C File Offset: 0x0000088C
		public ValueConnection(ValueOutput source, ValueInput destination)
			: base(source, destination)
		{
			if (destination.hasValidConnection)
			{
				throw new InvalidConnectionException("Value input ports do not support multiple connections.");
			}
			if (!source.type.IsConvertibleTo(destination.type, false))
			{
				throw new InvalidConnectionException(string.Format("Cannot convert from '{0}' to '{1}'.", source.type, destination.type));
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000026E4 File Offset: 0x000008E4
		public override ValueOutput source
		{
			get
			{
				return base.sourceUnit.valueOutputs[base.sourceKey];
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000026FC File Offset: 0x000008FC
		public override ValueInput destination
		{
			get
			{
				return base.destinationUnit.valueInputs[base.destinationKey];
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002714 File Offset: 0x00000914
		IUnitOutputPort IConnection<IUnitOutputPort, IUnitInputPort>.source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000271C File Offset: 0x0000091C
		IUnitInputPort IConnection<IUnitOutputPort, IUnitInputPort>.destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002724 File Offset: 0x00000924
		public override bool sourceExists
		{
			get
			{
				return base.sourceUnit.valueOutputs.Contains(base.sourceKey);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000273C File Offset: 0x0000093C
		public override bool destinationExists
		{
			get
			{
				return base.destinationUnit.valueInputs.Contains(base.destinationKey);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002754 File Offset: 0x00000954
		FlowGraph IUnitConnection.get_graph()
		{
			return base.graph;
		}

		// Token: 0x0200019F RID: 415
		public class DebugData : UnitConnectionDebugData
		{
			// Token: 0x170003BD RID: 957
			// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0001A39C File Offset: 0x0001859C
			// (set) Token: 0x06000B70 RID: 2928 RVA: 0x0001A3A4 File Offset: 0x000185A4
			public object lastValue { get; set; }

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0001A3AD File Offset: 0x000185AD
			// (set) Token: 0x06000B72 RID: 2930 RVA: 0x0001A3B5 File Offset: 0x000185B5
			public bool assignedLastValue { get; set; }
		}
	}
}
