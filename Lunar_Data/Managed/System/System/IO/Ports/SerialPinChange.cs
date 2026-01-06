using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the type of change that occurred on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000845 RID: 2117
	public enum SerialPinChange
	{
		/// <summary>The Clear to Send (CTS) signal changed state. This signal is used to indicate whether data can be sent over the serial port.</summary>
		// Token: 0x04002885 RID: 10373
		CtsChanged = 8,
		/// <summary>The Data Set Ready (DSR) signal changed state. This signal is used to indicate whether the device on the serial port is ready to operate.</summary>
		// Token: 0x04002886 RID: 10374
		DsrChanged = 16,
		/// <summary>The Carrier Detect (CD) signal changed state. This signal is used to indicate whether a modem is connected to a working phone line and a data carrier signal is detected.</summary>
		// Token: 0x04002887 RID: 10375
		CDChanged = 32,
		/// <summary>A break was detected on input.</summary>
		// Token: 0x04002888 RID: 10376
		Break = 64,
		/// <summary>A ring indicator was detected.</summary>
		// Token: 0x04002889 RID: 10377
		Ring = 256
	}
}
