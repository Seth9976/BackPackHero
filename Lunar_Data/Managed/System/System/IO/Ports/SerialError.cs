using System;

namespace System.IO.Ports
{
	/// <summary>Specifies errors that occur on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000843 RID: 2115
	public enum SerialError
	{
		/// <summary>An input buffer overflow has occurred. There is either no room in the input buffer, or a character was received after the end-of-file (EOF) character.</summary>
		// Token: 0x0400287E RID: 10366
		RXOver = 1,
		/// <summary>A character-buffer overrun has occurred. The next character is lost.</summary>
		// Token: 0x0400287F RID: 10367
		Overrun,
		/// <summary>The hardware detected a parity error.</summary>
		// Token: 0x04002880 RID: 10368
		RXParity = 4,
		/// <summary>The hardware detected a framing error.</summary>
		// Token: 0x04002881 RID: 10369
		Frame = 8,
		/// <summary>The application tried to transmit a character, but the output buffer was full.</summary>
		// Token: 0x04002882 RID: 10370
		TXFull = 256
	}
}
