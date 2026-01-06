using System;

namespace System.Net.Mail
{
	/// <summary>Describes the delivery notification options for e-mail.</summary>
	// Token: 0x02000635 RID: 1589
	[Flags]
	public enum DeliveryNotificationOptions
	{
		/// <summary>No notification information will be sent. The mail server will utilize its configured behavior to determine whether it should generate a delivery notification.</summary>
		// Token: 0x04001F0D RID: 7949
		None = 0,
		/// <summary>Notify if the delivery is successful.</summary>
		// Token: 0x04001F0E RID: 7950
		OnSuccess = 1,
		/// <summary>Notify if the delivery is unsuccessful.</summary>
		// Token: 0x04001F0F RID: 7951
		OnFailure = 2,
		/// <summary>Notify if the delivery is delayed.</summary>
		// Token: 0x04001F10 RID: 7952
		Delay = 4,
		/// <summary>A notification should not be generated under any circumstances.</summary>
		// Token: 0x04001F11 RID: 7953
		Never = 134217728
	}
}
