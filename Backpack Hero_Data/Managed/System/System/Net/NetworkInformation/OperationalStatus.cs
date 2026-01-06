using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the operational state of a network interface.</summary>
	// Token: 0x0200050B RID: 1291
	public enum OperationalStatus
	{
		/// <summary>The network interface is up; it can transmit data packets.</summary>
		// Token: 0x0400186C RID: 6252
		Up = 1,
		/// <summary>The network interface is unable to transmit data packets.</summary>
		// Token: 0x0400186D RID: 6253
		Down,
		/// <summary>The network interface is running tests.</summary>
		// Token: 0x0400186E RID: 6254
		Testing,
		/// <summary>The network interface status is not known.</summary>
		// Token: 0x0400186F RID: 6255
		Unknown,
		/// <summary>The network interface is not in a condition to transmit data packets; it is waiting for an external event.</summary>
		// Token: 0x04001870 RID: 6256
		Dormant,
		/// <summary>The network interface is unable to transmit data packets because of a missing component, typically a hardware component.</summary>
		// Token: 0x04001871 RID: 6257
		NotPresent,
		/// <summary>The network interface is unable to transmit data packets because it runs on top of one or more other interfaces, and at least one of these "lower layer" interfaces is down.</summary>
		// Token: 0x04001872 RID: 6258
		LowerLayerDown
	}
}
