using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.PinChanged" /> event.</summary>
	// Token: 0x02000846 RID: 2118
	public class SerialPinChangedEventArgs : EventArgs
	{
		// Token: 0x06004322 RID: 17186 RVA: 0x000E9A44 File Offset: 0x000E7C44
		internal SerialPinChangedEventArgs(SerialPinChange eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialPinChange" /> values.</returns>
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x000E9A53 File Offset: 0x000E7C53
		public SerialPinChange EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x00013B26 File Offset: 0x00011D26
		internal SerialPinChangedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400288A RID: 10378
		private SerialPinChange eventType;
	}
}
