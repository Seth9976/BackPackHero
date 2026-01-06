using System;

namespace System.Diagnostics
{
	/// <summary>Holds instance data associated with a performance counter sample.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000269 RID: 617
	public class InstanceData
	{
		/// <summary>Initializes a new instance of the InstanceData class, using the specified sample and performance counter instance.</summary>
		/// <param name="instanceName">The name of an instance associated with the performance counter. </param>
		/// <param name="sample">A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <paramref name="instanceName" /> parameter. </param>
		// Token: 0x0600136B RID: 4971 RVA: 0x00051728 File Offset: 0x0004F928
		public InstanceData(string instanceName, CounterSample sample)
		{
			this.instanceName = instanceName;
			this.sample = sample;
		}

		/// <summary>Gets the instance name associated with this instance data.</summary>
		/// <returns>The name of an instance associated with the performance counter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0005173E File Offset: 0x0004F93E
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		/// <summary>Gets the raw data value associated with the performance counter sample.</summary>
		/// <returns>The raw value read by the performance counter sample associated with the <see cref="P:System.Diagnostics.InstanceData.Sample" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x00051746 File Offset: 0x0004F946
		public long RawValue
		{
			get
			{
				return this.sample.RawValue;
			}
		}

		/// <summary>Gets the performance counter sample that generated this data.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <see cref="P:System.Diagnostics.InstanceData.InstanceName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x00051753 File Offset: 0x0004F953
		public CounterSample Sample
		{
			get
			{
				return this.sample;
			}
		}

		// Token: 0x04000AFF RID: 2815
		private string instanceName;

		// Token: 0x04000B00 RID: 2816
		private CounterSample sample;
	}
}
