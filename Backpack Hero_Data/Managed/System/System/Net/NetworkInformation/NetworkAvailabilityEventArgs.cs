using System;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged" /> event.</summary>
	// Token: 0x02000502 RID: 1282
	public class NetworkAvailabilityEventArgs : EventArgs
	{
		// Token: 0x060029AB RID: 10667 RVA: 0x00099728 File Offset: 0x00097928
		internal NetworkAvailabilityEventArgs(bool isAvailable)
		{
			this.isAvailable = isAvailable;
		}

		/// <summary>Gets the current status of the network connection.</summary>
		/// <returns>true if the network is available; otherwise, false.</returns>
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x00099737 File Offset: 0x00097937
		public bool IsAvailable
		{
			get
			{
				return this.isAvailable;
			}
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x00013B26 File Offset: 0x00011D26
		internal NetworkAvailabilityEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400185F RID: 6239
		private bool isAvailable;
	}
}
