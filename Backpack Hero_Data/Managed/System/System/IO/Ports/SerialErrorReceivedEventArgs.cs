using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Prepares data for the <see cref="E:System.IO.Ports.SerialPort.ErrorReceived" /> event.</summary>
	// Token: 0x02000844 RID: 2116
	public class SerialErrorReceivedEventArgs : EventArgs
	{
		// Token: 0x0600431F RID: 17183 RVA: 0x000E9A2D File Offset: 0x000E7C2D
		internal SerialErrorReceivedEventArgs(SerialError eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialError" /> values.</returns>
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06004320 RID: 17184 RVA: 0x000E9A3C File Offset: 0x000E7C3C
		public SerialError EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x00013B26 File Offset: 0x00011D26
		internal SerialErrorReceivedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002883 RID: 10371
		private SerialError eventType;
	}
}
