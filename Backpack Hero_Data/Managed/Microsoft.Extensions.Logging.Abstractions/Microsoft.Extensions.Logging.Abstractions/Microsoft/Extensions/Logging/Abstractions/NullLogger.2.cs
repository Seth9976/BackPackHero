using System;

namespace Microsoft.Extensions.Logging.Abstractions
{
	/// <summary>
	/// Minimalistic logger that does nothing.
	/// </summary>
	// Token: 0x0200001F RID: 31
	public class NullLogger<T> : ILogger<T>, ILogger
	{
		/// <inheritdoc />
		// Token: 0x0600009C RID: 156 RVA: 0x00003335 File Offset: 0x00001535
		public IDisposable BeginScope<TState>(TState state)
		{
			return NullScope.Instance;
		}

		/// <inheritdoc />
		/// <remarks>
		/// This method ignores the parameters and does nothing.
		/// </remarks>
		// Token: 0x0600009D RID: 157 RVA: 0x0000333C File Offset: 0x0000153C
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
		}

		/// <inheritdoc />
		// Token: 0x0600009E RID: 158 RVA: 0x0000333E File Offset: 0x0000153E
		public bool IsEnabled(LogLevel logLevel)
		{
			return false;
		}

		/// <summary>
		/// Returns an instance of <see cref="T:Microsoft.Extensions.Logging.Abstractions.NullLogger`1" />.
		/// </summary>
		/// <returns>An instance of <see cref="T:Microsoft.Extensions.Logging.Abstractions.NullLogger`1" />.</returns>
		// Token: 0x0400002C RID: 44
		public static readonly NullLogger<T> Instance = new NullLogger<T>();
	}
}
