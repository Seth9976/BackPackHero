using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event.</summary>
	// Token: 0x0200084C RID: 2124
	public class SerialDataReceivedEventArgs : EventArgs
	{
		// Token: 0x060043AE RID: 17326 RVA: 0x000EAAD6 File Offset: 0x000E8CD6
		internal SerialDataReceivedEventArgs(SerialData eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialData" /> values.</returns>
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x000EAAE5 File Offset: 0x000E8CE5
		public SerialData EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x00013B26 File Offset: 0x00011D26
		internal SerialDataReceivedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040028AA RID: 10410
		private SerialData eventType;
	}
}
