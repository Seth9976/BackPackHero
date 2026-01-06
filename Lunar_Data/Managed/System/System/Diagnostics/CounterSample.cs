using System;

namespace System.Diagnostics
{
	/// <summary>Defines a structure that holds the raw data for a performance counter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200024E RID: 590
	public struct CounterSample
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to 0 (zero).</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample. </param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters. </param>
		/// <param name="counterFrequency">The frequency with which the counter is read. </param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter. </param>
		/// <param name="timeStamp">The raw time stamp. </param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp. </param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot. </param>
		// Token: 0x0600122A RID: 4650 RVA: 0x0004E3F4 File Offset: 0x0004C5F4
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType)
		{
			this = new CounterSample(rawValue, baseValue, counterFrequency, systemFrequency, timeStamp, timeStamp100nSec, counterType, 0L);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to the value that is passed in.</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample. </param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters. </param>
		/// <param name="counterFrequency">The frequency with which the counter is read. </param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter. </param>
		/// <param name="timeStamp">The raw time stamp. </param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp. </param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot. </param>
		/// <param name="counterTimeStamp">The time at which the sample was taken. </param>
		// Token: 0x0600122B RID: 4651 RVA: 0x0004E414 File Offset: 0x0004C614
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType, long counterTimeStamp)
		{
			this.rawValue = rawValue;
			this.baseValue = baseValue;
			this.counterFrequency = counterFrequency;
			this.systemFrequency = systemFrequency;
			this.timeStamp = timeStamp;
			this.timeStamp100nSec = timeStamp100nSec;
			this.counterType = counterType;
			this.counterTimeStamp = counterTimeStamp;
		}

		/// <summary>Gets an optional, base raw value for the counter.</summary>
		/// <returns>The base raw value, which is used only if the sample is based on multiple counters.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0004E453 File Offset: 0x0004C653
		public long BaseValue
		{
			get
			{
				return this.baseValue;
			}
		}

		/// <summary>Gets the raw counter frequency.</summary>
		/// <returns>The frequency with which the counter is read.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0004E45B File Offset: 0x0004C65B
		public long CounterFrequency
		{
			get
			{
				return this.counterFrequency;
			}
		}

		/// <summary>Gets the counter's time stamp.</summary>
		/// <returns>The time at which the sample was taken.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004E463 File Offset: 0x0004C663
		public long CounterTimeStamp
		{
			get
			{
				return this.counterTimeStamp;
			}
		}

		/// <summary>Gets the performance counter type.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0004E46B File Offset: 0x0004C66B
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
		}

		/// <summary>Gets the raw value of the counter.</summary>
		/// <returns>The numeric value that is associated with the performance counter sample.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004E473 File Offset: 0x0004C673
		public long RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		/// <summary>Gets the raw system frequency.</summary>
		/// <returns>The frequency with which the system reads from the counter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004E47B File Offset: 0x0004C67B
		public long SystemFrequency
		{
			get
			{
				return this.systemFrequency;
			}
		}

		/// <summary>Gets the raw time stamp.</summary>
		/// <returns>The system time stamp.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0004E483 File Offset: 0x0004C683
		public long TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		/// <summary>Gets the raw, high-fidelity time stamp.</summary>
		/// <returns>The system time stamp, represented within 0.1 millisecond.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004E48B File Offset: 0x0004C68B
		public long TimeStamp100nSec
		{
			get
			{
				return this.timeStamp100nSec;
			}
		}

		/// <summary>Calculates the performance data of the counter, using a single sample point. This method is generally used for uncalculated performance counter types.</summary>
		/// <returns>The calculated performance value.</returns>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001234 RID: 4660 RVA: 0x0004E493 File Offset: 0x0004C693
		public static float Calculate(CounterSample counterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample);
		}

		/// <summary>Calculates the performance data of the counter, using two sample points. This method is generally used for calculated performance counter types, such as averages.</summary>
		/// <returns>The calculated performance value.</returns>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data. </param>
		/// <param name="nextCounterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as an ending point for calculating performance data. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001235 RID: 4661 RVA: 0x0004E49B File Offset: 0x0004C69B
		public static float Calculate(CounterSample counterSample, CounterSample nextCounterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample, nextCounterSample);
		}

		/// <summary>Indicates whether the specified structure is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current instance; otherwise, false. </returns>
		/// <param name="o">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with the current structure.</param>
		// Token: 0x06001236 RID: 4662 RVA: 0x0004E4A4 File Offset: 0x0004C6A4
		public override bool Equals(object o)
		{
			return o is CounterSample && this.Equals((CounterSample)o);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Diagnostics.CounterSample" /> structure is equal to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <returns>true if <paramref name="sample" /> is equal to the current instance; otherwise, false. </returns>
		/// <param name="sample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with this instance.</param>
		// Token: 0x06001237 RID: 4663 RVA: 0x0004E4BC File Offset: 0x0004C6BC
		public bool Equals(CounterSample sample)
		{
			return this.rawValue == sample.rawValue && this.baseValue == sample.counterFrequency && this.counterFrequency == sample.counterFrequency && this.systemFrequency == sample.systemFrequency && this.timeStamp == sample.timeStamp && this.timeStamp100nSec == sample.timeStamp100nSec && this.counterTimeStamp == sample.counterTimeStamp && this.counterType == sample.counterType;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		// Token: 0x06001238 RID: 4664 RVA: 0x0004E53B File Offset: 0x0004C73B
		public static bool operator ==(CounterSample a, CounterSample b)
		{
			return a.Equals(b);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, false</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		// Token: 0x06001239 RID: 4665 RVA: 0x0004E545 File Offset: 0x0004C745
		public static bool operator !=(CounterSample a, CounterSample b)
		{
			return !a.Equals(b);
		}

		/// <summary>Gets a hash code for the current counter sample.</summary>
		/// <returns>A hash code for the current counter sample.</returns>
		// Token: 0x0600123A RID: 4666 RVA: 0x0004E554 File Offset: 0x0004C754
		public override int GetHashCode()
		{
			return (int)((this.rawValue << 28) ^ ((this.baseValue << 24) ^ ((this.counterFrequency << 20) ^ ((this.systemFrequency << 16) ^ ((this.timeStamp << 8) ^ ((this.timeStamp100nSec << 4) ^ (this.counterTimeStamp ^ (long)this.counterType)))))));
		}

		// Token: 0x04000A8E RID: 2702
		private long rawValue;

		// Token: 0x04000A8F RID: 2703
		private long baseValue;

		// Token: 0x04000A90 RID: 2704
		private long counterFrequency;

		// Token: 0x04000A91 RID: 2705
		private long systemFrequency;

		// Token: 0x04000A92 RID: 2706
		private long timeStamp;

		// Token: 0x04000A93 RID: 2707
		private long timeStamp100nSec;

		// Token: 0x04000A94 RID: 2708
		private long counterTimeStamp;

		// Token: 0x04000A95 RID: 2709
		private PerformanceCounterType counterType;

		/// <summary>Defines an empty, uninitialized performance counter sample of type NumberOfItems32.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000A96 RID: 2710
		public static CounterSample Empty = new CounterSample(0L, 0L, 0L, 0L, 0L, 0L, PerformanceCounterType.NumberOfItems32, 0L);
	}
}
