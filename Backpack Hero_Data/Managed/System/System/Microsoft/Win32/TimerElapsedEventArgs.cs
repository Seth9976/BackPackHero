using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.TimerElapsed" /> event.</summary>
	// Token: 0x0200012B RID: 299
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class TimerElapsedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.TimerElapsedEventArgs" /> class.</summary>
		/// <param name="timerId">The ID number for the timer. </param>
		// Token: 0x06000700 RID: 1792 RVA: 0x000137A3 File Offset: 0x000119A3
		public TimerElapsedEventArgs(IntPtr timerId)
		{
			this.mytimerId = timerId;
		}

		/// <summary>Gets the ID number for the timer.</summary>
		/// <returns>The ID number for the timer.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x000137B2 File Offset: 0x000119B2
		public IntPtr TimerId
		{
			get
			{
				return this.mytimerId;
			}
		}

		// Token: 0x040004E4 RID: 1252
		private IntPtr mytimerId;
	}
}
