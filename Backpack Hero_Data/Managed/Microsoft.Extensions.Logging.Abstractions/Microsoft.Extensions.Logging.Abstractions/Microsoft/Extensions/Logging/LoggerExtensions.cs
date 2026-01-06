using System;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// ILogger extension methods for common scenarios.
	/// </summary>
	// Token: 0x02000012 RID: 18
	public static class LoggerExtensions
	{
		/// <summary>
		/// Formats and writes a debug log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogDebug(0, exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000035 RID: 53 RVA: 0x0000289B File Offset: 0x00000A9B
		public static void LogDebug(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Debug, eventId, exception, message, args);
		}

		/// <summary>
		/// Formats and writes a debug log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogDebug(0, "Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000036 RID: 54 RVA: 0x000028A9 File Offset: 0x00000AA9
		public static void LogDebug(this ILogger logger, EventId eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Debug, eventId, message, args);
		}

		/// <summary>
		/// Formats and writes a debug log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogDebug(exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000037 RID: 55 RVA: 0x000028B5 File Offset: 0x00000AB5
		public static void LogDebug(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Debug, exception, message, args);
		}

		/// <summary>
		/// Formats and writes a debug log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogDebug("Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000038 RID: 56 RVA: 0x000028C1 File Offset: 0x00000AC1
		public static void LogDebug(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Debug, message, args);
		}

		/// <summary>
		/// Formats and writes a trace log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogTrace(0, exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000039 RID: 57 RVA: 0x000028CC File Offset: 0x00000ACC
		public static void LogTrace(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Trace, eventId, exception, message, args);
		}

		/// <summary>
		/// Formats and writes a trace log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogTrace(0, "Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x0600003A RID: 58 RVA: 0x000028DA File Offset: 0x00000ADA
		public static void LogTrace(this ILogger logger, EventId eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Trace, eventId, message, args);
		}

		/// <summary>
		/// Formats and writes a trace log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogTrace(exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x0600003B RID: 59 RVA: 0x000028E6 File Offset: 0x00000AE6
		public static void LogTrace(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Trace, exception, message, args);
		}

		/// <summary>
		/// Formats and writes a trace log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogTrace("Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x0600003C RID: 60 RVA: 0x000028F2 File Offset: 0x00000AF2
		public static void LogTrace(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Trace, message, args);
		}

		/// <summary>
		/// Formats and writes an informational log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogInformation(0, exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x0600003D RID: 61 RVA: 0x000028FD File Offset: 0x00000AFD
		public static void LogInformation(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Information, eventId, exception, message, args);
		}

		/// <summary>
		/// Formats and writes an informational log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogInformation(0, "Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x0600003E RID: 62 RVA: 0x0000290B File Offset: 0x00000B0B
		public static void LogInformation(this ILogger logger, EventId eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Information, eventId, message, args);
		}

		/// <summary>
		/// Formats and writes an informational log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogInformation(exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x0600003F RID: 63 RVA: 0x00002917 File Offset: 0x00000B17
		public static void LogInformation(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Information, exception, message, args);
		}

		/// <summary>
		/// Formats and writes an informational log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogInformation("Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000040 RID: 64 RVA: 0x00002923 File Offset: 0x00000B23
		public static void LogInformation(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Information, message, args);
		}

		/// <summary>
		/// Formats and writes a warning log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogWarning(0, exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000041 RID: 65 RVA: 0x0000292E File Offset: 0x00000B2E
		public static void LogWarning(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Warning, eventId, exception, message, args);
		}

		/// <summary>
		/// Formats and writes a warning log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogWarning(0, "Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000042 RID: 66 RVA: 0x0000293C File Offset: 0x00000B3C
		public static void LogWarning(this ILogger logger, EventId eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Warning, eventId, message, args);
		}

		/// <summary>
		/// Formats and writes a warning log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogWarning(exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000043 RID: 67 RVA: 0x00002948 File Offset: 0x00000B48
		public static void LogWarning(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Warning, exception, message, args);
		}

		/// <summary>
		/// Formats and writes a warning log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogWarning("Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000044 RID: 68 RVA: 0x00002954 File Offset: 0x00000B54
		public static void LogWarning(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Warning, message, args);
		}

		/// <summary>
		/// Formats and writes an error log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogError(0, exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000045 RID: 69 RVA: 0x0000295F File Offset: 0x00000B5F
		public static void LogError(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Error, eventId, exception, message, args);
		}

		/// <summary>
		/// Formats and writes an error log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogError(0, "Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000046 RID: 70 RVA: 0x0000296D File Offset: 0x00000B6D
		public static void LogError(this ILogger logger, EventId eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Error, eventId, message, args);
		}

		/// <summary>
		/// Formats and writes an error log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogError(exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000047 RID: 71 RVA: 0x00002979 File Offset: 0x00000B79
		public static void LogError(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Error, exception, message, args);
		}

		/// <summary>
		/// Formats and writes an error log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogError("Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000048 RID: 72 RVA: 0x00002985 File Offset: 0x00000B85
		public static void LogError(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Error, message, args);
		}

		/// <summary>
		/// Formats and writes a critical log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogCritical(0, exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x06000049 RID: 73 RVA: 0x00002990 File Offset: 0x00000B90
		public static void LogCritical(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Critical, eventId, exception, message, args);
		}

		/// <summary>
		/// Formats and writes a critical log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogCritical(0, "Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x0600004A RID: 74 RVA: 0x0000299E File Offset: 0x00000B9E
		public static void LogCritical(this ILogger logger, EventId eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Critical, eventId, message, args);
		}

		/// <summary>
		/// Formats and writes a critical log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogCritical(exception, "Error while processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x0600004B RID: 75 RVA: 0x000029AA File Offset: 0x00000BAA
		public static void LogCritical(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Critical, exception, message, args);
		}

		/// <summary>
		/// Formats and writes a critical log message.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <example>
		/// <code language="csharp">
		/// logger.LogCritical("Processing request from {Address}", address)
		/// </code>
		/// </example>
		// Token: 0x0600004C RID: 76 RVA: 0x000029B6 File Offset: 0x00000BB6
		public static void LogCritical(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Critical, message, args);
		}

		/// <summary>
		/// Formats and writes a log message at the specified log level.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="logLevel">Entry will be written on this level.</param>
		/// <param name="message">Format string of the log message.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		// Token: 0x0600004D RID: 77 RVA: 0x000029C1 File Offset: 0x00000BC1
		public static void Log(this ILogger logger, LogLevel logLevel, string message, params object[] args)
		{
			logger.Log(logLevel, 0, null, message, args);
		}

		/// <summary>
		/// Formats and writes a log message at the specified log level.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="logLevel">Entry will be written on this level.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="message">Format string of the log message.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		// Token: 0x0600004E RID: 78 RVA: 0x000029D3 File Offset: 0x00000BD3
		public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, string message, params object[] args)
		{
			logger.Log(logLevel, eventId, null, message, args);
		}

		/// <summary>
		/// Formats and writes a log message at the specified log level.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="logLevel">Entry will be written on this level.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		// Token: 0x0600004F RID: 79 RVA: 0x000029E1 File Offset: 0x00000BE1
		public static void Log(this ILogger logger, LogLevel logLevel, Exception exception, string message, params object[] args)
		{
			logger.Log(logLevel, 0, exception, message, args);
		}

		/// <summary>
		/// Formats and writes a log message at the specified log level.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to write to.</param>
		/// <param name="logLevel">Entry will be written on this level.</param>
		/// <param name="eventId">The event id associated with the log.</param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">Format string of the log message.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		// Token: 0x06000050 RID: 80 RVA: 0x000029F4 File Offset: 0x00000BF4
		public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger.Log<FormattedLogValues>(logLevel, eventId, new FormattedLogValues(message, args), exception, LoggerExtensions._messageFormatter);
		}

		/// <summary>
		/// Formats the message and creates a scope.
		/// </summary>
		/// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> to create the scope in.</param>
		/// <param name="messageFormat">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c>.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		/// <returns>A disposable scope object. Can be null.</returns>
		/// <example>
		/// <code language="csharp">
		/// using(logger.BeginScope("Processing request from {Address}", address)) { }
		/// </code>
		/// </example>
		// Token: 0x06000051 RID: 81 RVA: 0x00002A1B File Offset: 0x00000C1B
		public static IDisposable BeginScope(this ILogger logger, string messageFormat, params object[] args)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			return logger.BeginScope<FormattedLogValues>(new FormattedLogValues(messageFormat, args));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A38 File Offset: 0x00000C38
		private static string MessageFormatter(FormattedLogValues state, Exception error)
		{
			return state.ToString();
		}

		// Token: 0x04000011 RID: 17
		private static readonly Func<FormattedLogValues, Exception, string> _messageFormatter = new Func<FormattedLogValues, Exception, string>(LoggerExtensions.MessageFormatter);
	}
}
