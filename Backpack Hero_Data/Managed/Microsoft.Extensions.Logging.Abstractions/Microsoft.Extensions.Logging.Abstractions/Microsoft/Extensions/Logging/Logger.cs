using System;
using Microsoft.Extensions.Internal;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Delegates to a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance using the full name of the given type, created by the
	/// provided <see cref="T:Microsoft.Extensions.Logging.ILoggerFactory" />.
	/// </summary>
	/// <typeparam name="T">The type.</typeparam>
	// Token: 0x02000016 RID: 22
	public class Logger<T> : ILogger<T>, ILogger
	{
		/// <summary>
		/// Creates a new <see cref="T:Microsoft.Extensions.Logging.Logger`1" />.
		/// </summary>
		/// <param name="factory">The factory.</param>
		// Token: 0x06000069 RID: 105 RVA: 0x00002E66 File Offset: 0x00001066
		public Logger(ILoggerFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			this._logger = factory.CreateLogger(TypeNameHelper.GetTypeDisplayName(typeof(T), true, false, false, '.'));
		}

		/// <inheritdoc />
		// Token: 0x0600006A RID: 106 RVA: 0x00002E9C File Offset: 0x0000109C
		IDisposable ILogger.BeginScope<TState>(TState state)
		{
			return this._logger.BeginScope<TState>(state);
		}

		/// <inheritdoc />
		// Token: 0x0600006B RID: 107 RVA: 0x00002EAA File Offset: 0x000010AA
		bool ILogger.IsEnabled(LogLevel logLevel)
		{
			return this._logger.IsEnabled(logLevel);
		}

		/// <inheritdoc />
		// Token: 0x0600006C RID: 108 RVA: 0x00002EB8 File Offset: 0x000010B8
		void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			this._logger.Log<TState>(logLevel, eventId, state, exception, formatter);
		}

		// Token: 0x04000013 RID: 19
		private readonly ILogger _logger;
	}
}
