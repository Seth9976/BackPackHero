using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnding" /> event.</summary>
	// Token: 0x02000125 RID: 293
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SessionEndingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionEndingEventArgs" /> class using the specified value indicating the type of session close event that is occurring.</summary>
		/// <param name="reason">One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> that specifies how the session ends. </param>
		// Token: 0x060006D0 RID: 1744 RVA: 0x0001361E File Offset: 0x0001181E
		public SessionEndingEventArgs(SessionEndReasons reason)
		{
			this.myreason = reason;
		}

		/// <summary>Gets the reason the session is ending.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values that specifies how the session is ending.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001362D File Offset: 0x0001182D
		public SessionEndReasons Reason
		{
			get
			{
				return this.myreason;
			}
		}

		/// <summary>Gets or sets a value indicating whether to cancel the user request to end the session.</summary>
		/// <returns>true to cancel the user request to end the session; otherwise, false.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00013635 File Offset: 0x00011835
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0001363D File Offset: 0x0001183D
		public bool Cancel
		{
			get
			{
				return this.mycancel;
			}
			set
			{
				this.mycancel = value;
			}
		}

		// Token: 0x040004D5 RID: 1237
		private SessionEndReasons myreason;

		// Token: 0x040004D6 RID: 1238
		private bool mycancel;
	}
}
