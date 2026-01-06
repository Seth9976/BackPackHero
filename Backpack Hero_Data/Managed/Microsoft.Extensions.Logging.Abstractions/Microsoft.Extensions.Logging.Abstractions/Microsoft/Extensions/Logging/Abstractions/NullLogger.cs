using System;

namespace Microsoft.Extensions.Logging.Abstractions
{
	/// <summary>
	/// Minimalistic logger that does nothing.
	/// </summary>
	// Token: 0x0200001C RID: 28
	public class NullLogger : ILogger
	{
		/// <summary>
		/// Returns the shared instance of <see cref="T:Microsoft.Extensions.Logging.Abstractions.NullLogger" />.
		/// </summary>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000032CB File Offset: 0x000014CB
		public static NullLogger Instance { get; } = new NullLogger();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Microsoft.Extensions.Logging.Abstractions.NullLogger" /> class.
		/// </summary>
		// Token: 0x0600008D RID: 141 RVA: 0x000032D2 File Offset: 0x000014D2
		private NullLogger()
		{
		}

		/// <inheritdoc />
		// Token: 0x0600008E RID: 142 RVA: 0x000032DA File Offset: 0x000014DA
		public IDisposable BeginScope<TState>(TState state)
		{
			return NullScope.Instance;
		}

		/// <inheritdoc />
		// Token: 0x0600008F RID: 143 RVA: 0x000032E1 File Offset: 0x000014E1
		public bool IsEnabled(LogLevel logLevel)
		{
			return false;
		}

		/// <inheritdoc />
		// Token: 0x06000090 RID: 144 RVA: 0x000032E4 File Offset: 0x000014E4
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
		}
	}
}
