using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the parity bit for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000841 RID: 2113
	public enum Parity
	{
		/// <summary>No parity check occurs.</summary>
		// Token: 0x04002875 RID: 10357
		None,
		/// <summary>Sets the parity bit so that the count of bits set is an odd number.</summary>
		// Token: 0x04002876 RID: 10358
		Odd,
		/// <summary>Sets the parity bit so that the count of bits set is an even number.</summary>
		// Token: 0x04002877 RID: 10359
		Even,
		/// <summary>Leaves the parity bit set to 1.</summary>
		// Token: 0x04002878 RID: 10360
		Mark,
		/// <summary>Leaves the parity bit set to 0.</summary>
		// Token: 0x04002879 RID: 10361
		Space
	}
}
