using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000007 RID: 7
	public abstract class UnitConnection<TSourcePort, TDestinationPort> : GraphElement<FlowGraph>, IConnection<TSourcePort, TDestinationPort> where TSourcePort : class, IUnitOutputPort where TDestinationPort : class, IUnitInputPort
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002342 File Offset: 0x00000542
		[Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		protected UnitConnection()
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000234C File Offset: 0x0000054C
		protected UnitConnection(TSourcePort source, TDestinationPort destination)
		{
			Ensure.That("source").IsNotNull<TSourcePort>(source);
			Ensure.That("destination").IsNotNull<TDestinationPort>(destination);
			if (source.unit.graph != destination.unit.graph)
			{
				throw new NotSupportedException("Cannot create connections across graphs.");
			}
			if (source.unit == destination.unit)
			{
				throw new InvalidConnectionException("Cannot create connections on the same unit.");
			}
			this.sourceUnit = source.unit;
			this.sourceKey = source.key;
			this.destinationUnit = destination.unit;
			this.destinationKey = destination.key;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002413 File Offset: 0x00000613
		public virtual IGraphElementDebugData CreateDebugData()
		{
			return new UnitConnectionDebugData();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000241A File Offset: 0x0000061A
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002422 File Offset: 0x00000622
		[Serialize]
		private protected IUnit sourceUnit { protected get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000242B File Offset: 0x0000062B
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002433 File Offset: 0x00000633
		[Serialize]
		private protected string sourceKey { protected get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000243C File Offset: 0x0000063C
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002444 File Offset: 0x00000644
		[Serialize]
		private protected IUnit destinationUnit { protected get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000244D File Offset: 0x0000064D
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002455 File Offset: 0x00000655
		[Serialize]
		private protected string destinationKey { protected get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002F RID: 47
		[DoNotSerialize]
		public abstract TSourcePort source { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000030 RID: 48
		[DoNotSerialize]
		public abstract TDestinationPort destination { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000245E File Offset: 0x0000065E
		public override int dependencyOrder
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000032 RID: 50
		public abstract bool sourceExists { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000033 RID: 51
		public abstract bool destinationExists { get; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002461 File Offset: 0x00000661
		protected void CopyFrom(UnitConnection<TSourcePort, TDestinationPort> source)
		{
			base.CopyFrom(source);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000246C File Offset: 0x0000066C
		public override bool HandleDependencies()
		{
			bool flag = true;
			IUnitOutputPort unitOutputPort;
			if (!this.sourceExists)
			{
				if (!this.sourceUnit.invalidOutputs.Contains(this.sourceKey))
				{
					this.sourceUnit.invalidOutputs.Add(new InvalidOutput(this.sourceKey));
				}
				unitOutputPort = this.sourceUnit.invalidOutputs[this.sourceKey];
				flag = false;
			}
			else
			{
				unitOutputPort = this.source;
			}
			IUnitInputPort unitInputPort;
			if (!this.destinationExists)
			{
				if (!this.destinationUnit.invalidInputs.Contains(this.destinationKey))
				{
					this.destinationUnit.invalidInputs.Add(new InvalidInput(this.destinationKey));
				}
				unitInputPort = this.destinationUnit.invalidInputs[this.destinationKey];
				flag = false;
			}
			else
			{
				unitInputPort = this.destination;
			}
			if (!unitOutputPort.CanValidlyConnectTo(unitInputPort))
			{
				flag = false;
			}
			if (!flag && unitOutputPort.CanInvalidlyConnectTo(unitInputPort))
			{
				unitOutputPort.InvalidlyConnectTo(unitInputPort);
				if (unitOutputPort.unit.GetType() != typeof(MissingType) && unitInputPort.unit.GetType() != typeof(MissingType))
				{
					Debug.LogWarning(string.Format("Could not load connection between '{0}' of '{1}' and '{2}' of '{3}'.", new object[] { unitOutputPort.key, this.sourceUnit, unitInputPort.key, this.destinationUnit }));
				}
			}
			return flag;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025D2 File Offset: 0x000007D2
		public override AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			return null;
		}
	}
}
