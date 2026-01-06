using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the reason a thread is waiting.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000283 RID: 643
	public enum ThreadWaitReason
	{
		/// <summary>The thread is waiting for event pair high.</summary>
		// Token: 0x04000B78 RID: 2936
		EventPairHigh = 7,
		/// <summary>The thread is waiting for event pair low.</summary>
		// Token: 0x04000B79 RID: 2937
		EventPairLow,
		/// <summary>Thread execution is delayed.</summary>
		// Token: 0x04000B7A RID: 2938
		ExecutionDelay = 4,
		/// <summary>The thread is waiting for the scheduler.</summary>
		// Token: 0x04000B7B RID: 2939
		Executive = 0,
		/// <summary>The thread is waiting for a free virtual memory page.</summary>
		// Token: 0x04000B7C RID: 2940
		FreePage,
		/// <summary>The thread is waiting for a local procedure call to arrive.</summary>
		// Token: 0x04000B7D RID: 2941
		LpcReceive = 9,
		/// <summary>The thread is waiting for reply to a local procedure call to arrive.</summary>
		// Token: 0x04000B7E RID: 2942
		LpcReply,
		/// <summary>The thread is waiting for a virtual memory page to arrive in memory.</summary>
		// Token: 0x04000B7F RID: 2943
		PageIn = 2,
		/// <summary>The thread is waiting for a virtual memory page to be written to disk.</summary>
		// Token: 0x04000B80 RID: 2944
		PageOut = 12,
		/// <summary>Thread execution is suspended.</summary>
		// Token: 0x04000B81 RID: 2945
		Suspended = 5,
		/// <summary>The thread is waiting for system allocation.</summary>
		// Token: 0x04000B82 RID: 2946
		SystemAllocation = 3,
		/// <summary>The thread is waiting for an unknown reason.</summary>
		// Token: 0x04000B83 RID: 2947
		Unknown = 13,
		/// <summary>The thread is waiting for a user request.</summary>
		// Token: 0x04000B84 RID: 2948
		UserRequest = 6,
		/// <summary>The thread is waiting for the system to allocate virtual memory.</summary>
		// Token: 0x04000B85 RID: 2949
		VirtualMemory = 11
	}
}
