using System;

namespace System.Diagnostics
{
	/// <summary>Specifies what messages to output for the <see cref="T:System.Diagnostics.Debug" />, <see cref="T:System.Diagnostics.Trace" /> and <see cref="T:System.Diagnostics.TraceSwitch" /> classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000230 RID: 560
	public enum TraceLevel
	{
		/// <summary>Output no tracing and debugging messages.</summary>
		// Token: 0x040009ED RID: 2541
		Off,
		/// <summary>Output error-handling messages.</summary>
		// Token: 0x040009EE RID: 2542
		Error,
		/// <summary>Output warnings and error-handling messages.</summary>
		// Token: 0x040009EF RID: 2543
		Warning,
		/// <summary>Output informational messages, warnings, and error-handling messages.</summary>
		// Token: 0x040009F0 RID: 2544
		Info,
		/// <summary>Output all debugging and tracing messages.</summary>
		// Token: 0x040009F1 RID: 2545
		Verbose
	}
}
