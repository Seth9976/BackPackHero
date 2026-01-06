using System;

namespace Microsoft.Extensions.Logging.Abstractions
{
	/// <summary>
	/// Holds the information for a single log entry.
	/// </summary>
	// Token: 0x0200001B RID: 27
	public readonly struct LogEntry<TState>
	{
		/// <summary>
		/// Initializes an instance of the LogEntry struct.
		/// </summary>
		/// <param name="logLevel">The log level.</param>
		/// <param name="category">The category name for the log.</param>
		/// <param name="eventId">The log event Id.</param>
		/// <param name="state">The state for which log is being written.</param>
		/// <param name="exception">The log exception.</param>
		/// <param name="formatter">The formatter.</param>
		// Token: 0x06000085 RID: 133 RVA: 0x0000326C File Offset: 0x0000146C
		public LogEntry(LogLevel logLevel, string category, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			this.LogLevel = logLevel;
			this.Category = category;
			this.EventId = eventId;
			this.State = state;
			this.Exception = exception;
			this.Formatter = formatter;
		}

		/// <summary>
		/// Gets the log level.
		/// </summary>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000329B File Offset: 0x0000149B
		public LogLevel LogLevel { get; }

		/// <summary>
		/// Gets the log category.
		/// </summary>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000032A3 File Offset: 0x000014A3
		public string Category { get; }

		/// <summary>
		/// Gets the log event ID.
		/// </summary>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000032AB File Offset: 0x000014AB
		public EventId EventId { get; }

		/// <summary>
		/// Gets the state.
		/// </summary>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000032B3 File Offset: 0x000014B3
		public TState State { get; }

		/// <summary>
		/// Gets the log exception.
		/// </summary>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000032BB File Offset: 0x000014BB
		public Exception Exception { get; }

		/// <summary>
		/// Gets the formatter.
		/// </summary>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000032C3 File Offset: 0x000014C3
		public Func<TState, Exception, string> Formatter { get; }
	}
}
