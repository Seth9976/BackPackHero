using System;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Defines logging severity levels.
	/// </summary>
	// Token: 0x02000017 RID: 23
	public enum LogLevel
	{
		/// <summary>
		/// Logs that contain the most detailed messages. These messages may contain sensitive application data.
		/// These messages are disabled by default and should never be enabled in a production environment.
		/// </summary>
		// Token: 0x04000015 RID: 21
		Trace,
		/// <summary>
		/// Logs that are used for interactive investigation during development.  These logs should primarily contain
		/// information useful for debugging and have no long-term value.
		/// </summary>
		// Token: 0x04000016 RID: 22
		Debug,
		/// <summary>
		/// Logs that track the general flow of the application. These logs should have long-term value.
		/// </summary>
		// Token: 0x04000017 RID: 23
		Information,
		/// <summary>
		/// Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the
		/// application execution to stop.
		/// </summary>
		// Token: 0x04000018 RID: 24
		Warning,
		/// <summary>
		/// Logs that highlight when the current flow of execution is stopped due to a failure. These should indicate a
		/// failure in the current activity, not an application-wide failure.
		/// </summary>
		// Token: 0x04000019 RID: 25
		Error,
		/// <summary>
		/// Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires
		/// immediate attention.
		/// </summary>
		// Token: 0x0400001A RID: 26
		Critical,
		/// <summary>
		/// Not used for writing log messages. Specifies that a logging category should not write any messages.
		/// </summary>
		// Token: 0x0400001B RID: 27
		None
	}
}
