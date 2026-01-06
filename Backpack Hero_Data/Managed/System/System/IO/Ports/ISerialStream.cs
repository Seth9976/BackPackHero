using System;

namespace System.IO.Ports
{
	// Token: 0x02000840 RID: 2112
	internal interface ISerialStream : IDisposable
	{
		// Token: 0x06004310 RID: 17168
		int Read(byte[] buffer, int offset, int count);

		// Token: 0x06004311 RID: 17169
		void Write(byte[] buffer, int offset, int count);

		// Token: 0x06004312 RID: 17170
		void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits sb, Handshake hs);

		// Token: 0x06004313 RID: 17171
		void DiscardInBuffer();

		// Token: 0x06004314 RID: 17172
		void DiscardOutBuffer();

		// Token: 0x06004315 RID: 17173
		SerialSignal GetSignals();

		// Token: 0x06004316 RID: 17174
		void SetSignal(SerialSignal signal, bool value);

		// Token: 0x06004317 RID: 17175
		void SetBreakState(bool value);

		// Token: 0x06004318 RID: 17176
		void Close();

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06004319 RID: 17177
		int BytesToRead { get; }

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x0600431A RID: 17178
		int BytesToWrite { get; }

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x0600431B RID: 17179
		// (set) Token: 0x0600431C RID: 17180
		int ReadTimeout { get; set; }

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x0600431D RID: 17181
		// (set) Token: 0x0600431E RID: 17182
		int WriteTimeout { get; set; }
	}
}
