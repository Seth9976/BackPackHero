using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the number of stop bits used on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200084E RID: 2126
	public enum StopBits
	{
		/// <summary>No stop bits are used. This value is not supported by the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> property. </summary>
		// Token: 0x040028B3 RID: 10419
		None,
		/// <summary>One stop bit is used.</summary>
		// Token: 0x040028B4 RID: 10420
		One,
		/// <summary>Two stop bits are used.</summary>
		// Token: 0x040028B5 RID: 10421
		Two,
		/// <summary>1.5 stop bits are used.</summary>
		// Token: 0x040028B6 RID: 10422
		OnePointFive
	}
}
