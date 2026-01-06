using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Creates delegates that can be later cached to log messages in a performant way.
	/// </summary>
	// Token: 0x02000015 RID: 21
	public static class LoggerMessage
	{
		/// <summary>
		/// Creates a delegate that can be invoked to create a log scope.
		/// </summary>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log scope.</returns>
		// Token: 0x0600005A RID: 90 RVA: 0x00002B44 File Offset: 0x00000D44
		public static Func<ILogger, IDisposable> DefineScope(string formatString)
		{
			LogValuesFormatter logValuesFormatter = LoggerMessage.CreateLogValuesFormatter(formatString, 0);
			LoggerMessage.LogValues logValues = new LoggerMessage.LogValues(logValuesFormatter);
			return (ILogger logger) => logger.BeginScope<LoggerMessage.LogValues>(logValues);
		}

		/// <summary>
		/// Creates a delegate that can be invoked to create a log scope.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log scope.</returns>
		// Token: 0x0600005B RID: 91 RVA: 0x00002B78 File Offset: 0x00000D78
		public static Func<ILogger, T1, IDisposable> DefineScope<T1>(string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 1);
			return (ILogger logger, T1 arg1) => logger.BeginScope<LoggerMessage.LogValues<T1>>(new LoggerMessage.LogValues<T1>(formatter, arg1));
		}

		/// <summary>
		/// Creates a delegate that can be invoked to create a log scope.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log scope.</returns>
		// Token: 0x0600005C RID: 92 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public static Func<ILogger, T1, T2, IDisposable> DefineScope<T1, T2>(string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 2);
			return (ILogger logger, T1 arg1, T2 arg2) => logger.BeginScope<LoggerMessage.LogValues<T1, T2>>(new LoggerMessage.LogValues<T1, T2>(formatter, arg1, arg2));
		}

		/// <summary>
		/// Creates a delegate that can be invoked to create a log scope.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <typeparam name="T3">The type of the third parameter passed to the named format string.</typeparam>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log scope.</returns>
		// Token: 0x0600005D RID: 93 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public static Func<ILogger, T1, T2, T3, IDisposable> DefineScope<T1, T2, T3>(string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 3);
			return (ILogger logger, T1 arg1, T2 arg2, T3 arg3) => logger.BeginScope<LoggerMessage.LogValues<T1, T2, T3>>(new LoggerMessage.LogValues<T1, T2, T3>(formatter, arg1, arg2, arg3));
		}

		/// <summary>
		/// Creates a delegate that can be invoked to create a log scope.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <typeparam name="T3">The type of the third parameter passed to the named format string.</typeparam>
		/// <typeparam name="T4">The type of the fourth parameter passed to the named format string.</typeparam>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log scope.</returns>
		// Token: 0x0600005E RID: 94 RVA: 0x00002BFC File Offset: 0x00000DFC
		public static Func<ILogger, T1, T2, T3, T4, IDisposable> DefineScope<T1, T2, T3, T4>(string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 4);
			return (ILogger logger, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => logger.BeginScope<LoggerMessage.LogValues<T1, T2, T3, T4>>(new LoggerMessage.LogValues<T1, T2, T3, T4>(formatter, arg1, arg2, arg3, arg4));
		}

		/// <summary>
		/// Creates a delegate that can be invoked to create a log scope.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <typeparam name="T3">The type of the third parameter passed to the named format string.</typeparam>
		/// <typeparam name="T4">The type of the fourth parameter passed to the named format string.</typeparam>
		/// <typeparam name="T5">The type of the fifth parameter passed to the named format string.</typeparam>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log scope.</returns>
		// Token: 0x0600005F RID: 95 RVA: 0x00002C28 File Offset: 0x00000E28
		public static Func<ILogger, T1, T2, T3, T4, T5, IDisposable> DefineScope<T1, T2, T3, T4, T5>(string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 5);
			return (ILogger logger, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => logger.BeginScope<LoggerMessage.LogValues<T1, T2, T3, T4, T5>>(new LoggerMessage.LogValues<T1, T2, T3, T4, T5>(formatter, arg1, arg2, arg3, arg4, arg5));
		}

		/// <summary>
		/// Creates a delegate that can be invoked to create a log scope.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <typeparam name="T3">The type of the third parameter passed to the named format string.</typeparam>
		/// <typeparam name="T4">The type of the fourth parameter passed to the named format string.</typeparam>
		/// <typeparam name="T5">The type of the fifth parameter passed to the named format string.</typeparam>
		/// <typeparam name="T6">The type of the sixth parameter passed to the named format string.</typeparam>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log scope.</returns>
		// Token: 0x06000060 RID: 96 RVA: 0x00002C54 File Offset: 0x00000E54
		public static Func<ILogger, T1, T2, T3, T4, T5, T6, IDisposable> DefineScope<T1, T2, T3, T4, T5, T6>(string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 6);
			return (ILogger logger, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => logger.BeginScope<LoggerMessage.LogValues<T1, T2, T3, T4, T5, T6>>(new LoggerMessage.LogValues<T1, T2, T3, T4, T5, T6>(formatter, arg1, arg2, arg3, arg4, arg5, arg6));
		}

		/// <summary>
		/// Creates a delegate that can be invoked for logging a message.
		/// </summary>
		/// <param name="logLevel">The <see cref="T:Microsoft.Extensions.Logging.LogLevel" />.</param>
		/// <param name="eventId">The event ID.</param>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log message.</returns>
		// Token: 0x06000061 RID: 97 RVA: 0x00002C80 File Offset: 0x00000E80
		public static Action<ILogger, Exception> Define(LogLevel logLevel, EventId eventId, string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 0);
			return delegate(ILogger logger, Exception exception)
			{
				if (logger.IsEnabled(logLevel))
				{
					logger.Log<LoggerMessage.LogValues>(logLevel, eventId, new LoggerMessage.LogValues(formatter), exception, LoggerMessage.LogValues.Callback);
				}
			};
		}

		/// <summary>
		/// Creates a delegate that can be invoked for logging a message.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <param name="logLevel">The <see cref="T:Microsoft.Extensions.Logging.LogLevel" />.</param>
		/// <param name="eventId">The event ID.</param>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log message.</returns>
		// Token: 0x06000062 RID: 98 RVA: 0x00002CBC File Offset: 0x00000EBC
		public static Action<ILogger, T1, Exception> Define<T1>(LogLevel logLevel, EventId eventId, string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 1);
			return delegate(ILogger logger, T1 arg1, Exception exception)
			{
				if (logger.IsEnabled(logLevel))
				{
					base.<Define>g__Log|0(logger, arg1, exception);
				}
			};
		}

		/// <summary>
		/// Creates a delegate that can be invoked for logging a message.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <param name="logLevel">The <see cref="T:Microsoft.Extensions.Logging.LogLevel" />.</param>
		/// <param name="eventId">The event ID.</param>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log message.</returns>
		// Token: 0x06000063 RID: 99 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public static Action<ILogger, T1, T2, Exception> Define<T1, T2>(LogLevel logLevel, EventId eventId, string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 2);
			return delegate(ILogger logger, T1 arg1, T2 arg2, Exception exception)
			{
				if (logger.IsEnabled(logLevel))
				{
					base.<Define>g__Log|0(logger, arg1, arg2, exception);
				}
			};
		}

		/// <summary>
		/// Creates a delegate that can be invoked for logging a message.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <typeparam name="T3">The type of the third parameter passed to the named format string.</typeparam>
		/// <param name="logLevel">The <see cref="T:Microsoft.Extensions.Logging.LogLevel" />.</param>
		/// <param name="eventId">The event ID.</param>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log message.</returns>
		// Token: 0x06000064 RID: 100 RVA: 0x00002D34 File Offset: 0x00000F34
		public static Action<ILogger, T1, T2, T3, Exception> Define<T1, T2, T3>(LogLevel logLevel, EventId eventId, string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 3);
			return delegate(ILogger logger, T1 arg1, T2 arg2, T3 arg3, Exception exception)
			{
				if (logger.IsEnabled(logLevel))
				{
					base.<Define>g__Log|0(logger, arg1, arg2, arg3, exception);
				}
			};
		}

		/// <summary>
		/// Creates a delegate that can be invoked for logging a message.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <typeparam name="T3">The type of the third parameter passed to the named format string.</typeparam>
		/// <typeparam name="T4">The type of the fourth parameter passed to the named format string.</typeparam>
		/// <param name="logLevel">The <see cref="T:Microsoft.Extensions.Logging.LogLevel" />.</param>
		/// <param name="eventId">The event ID.</param>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log message.</returns>
		// Token: 0x06000065 RID: 101 RVA: 0x00002D70 File Offset: 0x00000F70
		public static Action<ILogger, T1, T2, T3, T4, Exception> Define<T1, T2, T3, T4>(LogLevel logLevel, EventId eventId, string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 4);
			return delegate(ILogger logger, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Exception exception)
			{
				if (logger.IsEnabled(logLevel))
				{
					base.<Define>g__Log|0(logger, arg1, arg2, arg3, arg4, exception);
				}
			};
		}

		/// <summary>
		/// Creates a delegate that can be invoked for logging a message.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <typeparam name="T3">The type of the third parameter passed to the named format string.</typeparam>
		/// <typeparam name="T4">The type of the fourth parameter passed to the named format string.</typeparam>
		/// <typeparam name="T5">The type of the fifth parameter passed to the named format string.</typeparam>
		/// <param name="logLevel">The <see cref="T:Microsoft.Extensions.Logging.LogLevel" />.</param>
		/// <param name="eventId">The event ID.</param>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log message.</returns>
		// Token: 0x06000066 RID: 102 RVA: 0x00002DAC File Offset: 0x00000FAC
		public static Action<ILogger, T1, T2, T3, T4, T5, Exception> Define<T1, T2, T3, T4, T5>(LogLevel logLevel, EventId eventId, string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 5);
			return delegate(ILogger logger, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Exception exception)
			{
				if (logger.IsEnabled(logLevel))
				{
					logger.Log<LoggerMessage.LogValues<T1, T2, T3, T4, T5>>(logLevel, eventId, new LoggerMessage.LogValues<T1, T2, T3, T4, T5>(formatter, arg1, arg2, arg3, arg4, arg5), exception, LoggerMessage.LogValues<T1, T2, T3, T4, T5>.Callback);
				}
			};
		}

		/// <summary>
		/// Creates a delegate that can be invoked for logging a message.
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter passed to the named format string.</typeparam>
		/// <typeparam name="T2">The type of the second parameter passed to the named format string.</typeparam>
		/// <typeparam name="T3">The type of the third parameter passed to the named format string.</typeparam>
		/// <typeparam name="T4">The type of the fourth parameter passed to the named format string.</typeparam>
		/// <typeparam name="T5">The type of the fifth parameter passed to the named format string.</typeparam>
		/// <typeparam name="T6">The type of the sixth parameter passed to the named format string.</typeparam>
		/// <param name="logLevel">The <see cref="T:Microsoft.Extensions.Logging.LogLevel" />.</param>
		/// <param name="eventId">The event ID.</param>
		/// <param name="formatString">The named format string.</param>
		/// <returns>A delegate that, when invoked, creates a log message.</returns>
		// Token: 0x06000067 RID: 103 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public static Action<ILogger, T1, T2, T3, T4, T5, T6, Exception> Define<T1, T2, T3, T4, T5, T6>(LogLevel logLevel, EventId eventId, string formatString)
		{
			LogValuesFormatter formatter = LoggerMessage.CreateLogValuesFormatter(formatString, 6);
			return delegate(ILogger logger, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Exception exception)
			{
				if (logger.IsEnabled(logLevel))
				{
					logger.Log<LoggerMessage.LogValues<T1, T2, T3, T4, T5, T6>>(logLevel, eventId, new LoggerMessage.LogValues<T1, T2, T3, T4, T5, T6>(formatter, arg1, arg2, arg3, arg4, arg5, arg6), exception, LoggerMessage.LogValues<T1, T2, T3, T4, T5, T6>.Callback);
				}
			};
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E24 File Offset: 0x00001024
		private static LogValuesFormatter CreateLogValuesFormatter(string formatString, int expectedNamedParameterCount)
		{
			LogValuesFormatter logValuesFormatter = new LogValuesFormatter(formatString);
			int count = logValuesFormatter.ValueNames.Count;
			if (count != expectedNamedParameterCount)
			{
				throw new ArgumentException(SR.Format(SR.UnexpectedNumberOfNamedParameters, formatString, expectedNamedParameterCount, count));
			}
			return logValuesFormatter;
		}

		// Token: 0x02000025 RID: 37
		private readonly struct LogValues : IReadOnlyList<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyCollection<KeyValuePair<string, object>>
		{
			// Token: 0x060000B4 RID: 180 RVA: 0x000034C7 File Offset: 0x000016C7
			public LogValues(LogValuesFormatter formatter)
			{
				this._formatter = formatter;
			}

			// Token: 0x1700001C RID: 28
			public KeyValuePair<string, object> this[int index]
			{
				get
				{
					if (index == 0)
					{
						return new KeyValuePair<string, object>("{OriginalFormat}", this._formatter.OriginalFormat);
					}
					throw new IndexOutOfRangeException("index");
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x000034F5 File Offset: 0x000016F5
			public int Count
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x000034F8 File Offset: 0x000016F8
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				yield return this[0];
				yield break;
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x0000350C File Offset: 0x0000170C
			public override string ToString()
			{
				return this._formatter.Format();
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00003519 File Offset: 0x00001719
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400003D RID: 61
			public static readonly Func<LoggerMessage.LogValues, Exception, string> Callback = (LoggerMessage.LogValues state, Exception exception) => state.ToString();

			// Token: 0x0400003E RID: 62
			private readonly LogValuesFormatter _formatter;
		}

		// Token: 0x02000026 RID: 38
		private readonly struct LogValues<T0> : IReadOnlyList<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyCollection<KeyValuePair<string, object>>
		{
			// Token: 0x060000BB RID: 187 RVA: 0x00003538 File Offset: 0x00001738
			public LogValues(LogValuesFormatter formatter, T0 value0)
			{
				this._formatter = formatter;
				this._value0 = value0;
			}

			// Token: 0x1700001E RID: 30
			public KeyValuePair<string, object> this[int index]
			{
				get
				{
					if (index == 0)
					{
						return new KeyValuePair<string, object>(this._formatter.ValueNames[0], this._value0);
					}
					if (index != 1)
					{
						throw new IndexOutOfRangeException("index");
					}
					return new KeyValuePair<string, object>("{OriginalFormat}", this._formatter.OriginalFormat);
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000BD RID: 189 RVA: 0x000035A0 File Offset: 0x000017A0
			public int Count
			{
				get
				{
					return 2;
				}
			}

			// Token: 0x060000BE RID: 190 RVA: 0x000035A3 File Offset: 0x000017A3
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num)
				{
					yield return this[i];
					num = i + 1;
				}
				yield break;
			}

			// Token: 0x060000BF RID: 191 RVA: 0x000035B7 File Offset: 0x000017B7
			public override string ToString()
			{
				return this._formatter.Format(this._value0);
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x000035CF File Offset: 0x000017CF
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400003F RID: 63
			public static readonly Func<LoggerMessage.LogValues<T0>, Exception, string> Callback = (LoggerMessage.LogValues<T0> state, Exception exception) => state.ToString();

			// Token: 0x04000040 RID: 64
			private readonly LogValuesFormatter _formatter;

			// Token: 0x04000041 RID: 65
			private readonly T0 _value0;
		}

		// Token: 0x02000027 RID: 39
		private readonly struct LogValues<T0, T1> : IReadOnlyList<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyCollection<KeyValuePair<string, object>>
		{
			// Token: 0x060000C2 RID: 194 RVA: 0x000035EE File Offset: 0x000017EE
			public LogValues(LogValuesFormatter formatter, T0 value0, T1 value1)
			{
				this._formatter = formatter;
				this._value0 = value0;
				this._value1 = value1;
			}

			// Token: 0x17000020 RID: 32
			public KeyValuePair<string, object> this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[0], this._value0);
					case 1:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[1], this._value1);
					case 2:
						return new KeyValuePair<string, object>("{OriginalFormat}", this._formatter.OriginalFormat);
					default:
						throw new IndexOutOfRangeException("index");
					}
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000368D File Offset: 0x0000188D
			public int Count
			{
				get
				{
					return 3;
				}
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00003690 File Offset: 0x00001890
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num)
				{
					yield return this[i];
					num = i + 1;
				}
				yield break;
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x000036A4 File Offset: 0x000018A4
			public override string ToString()
			{
				return this._formatter.Format(this._value0, this._value1);
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x000036C7 File Offset: 0x000018C7
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000042 RID: 66
			public static readonly Func<LoggerMessage.LogValues<T0, T1>, Exception, string> Callback = (LoggerMessage.LogValues<T0, T1> state, Exception exception) => state.ToString();

			// Token: 0x04000043 RID: 67
			private readonly LogValuesFormatter _formatter;

			// Token: 0x04000044 RID: 68
			private readonly T0 _value0;

			// Token: 0x04000045 RID: 69
			private readonly T1 _value1;
		}

		// Token: 0x02000028 RID: 40
		private readonly struct LogValues<T0, T1, T2> : IReadOnlyList<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyCollection<KeyValuePair<string, object>>
		{
			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x000036E6 File Offset: 0x000018E6
			public int Count
			{
				get
				{
					return 4;
				}
			}

			// Token: 0x17000023 RID: 35
			public KeyValuePair<string, object> this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[0], this._value0);
					case 1:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[1], this._value1);
					case 2:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[2], this._value2);
					case 3:
						return new KeyValuePair<string, object>("{OriginalFormat}", this._formatter.OriginalFormat);
					default:
						throw new IndexOutOfRangeException("index");
					}
				}
			}

			// Token: 0x060000CB RID: 203 RVA: 0x00003797 File Offset: 0x00001997
			public LogValues(LogValuesFormatter formatter, T0 value0, T1 value1, T2 value2)
			{
				this._formatter = formatter;
				this._value0 = value0;
				this._value1 = value1;
				this._value2 = value2;
			}

			// Token: 0x060000CC RID: 204 RVA: 0x000037B6 File Offset: 0x000019B6
			public override string ToString()
			{
				return this._formatter.Format(this._value0, this._value1, this._value2);
			}

			// Token: 0x060000CD RID: 205 RVA: 0x000037E4 File Offset: 0x000019E4
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num)
				{
					yield return this[i];
					num = i + 1;
				}
				yield break;
			}

			// Token: 0x060000CE RID: 206 RVA: 0x000037F8 File Offset: 0x000019F8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000046 RID: 70
			public static readonly Func<LoggerMessage.LogValues<T0, T1, T2>, Exception, string> Callback = (LoggerMessage.LogValues<T0, T1, T2> state, Exception exception) => state.ToString();

			// Token: 0x04000047 RID: 71
			private readonly LogValuesFormatter _formatter;

			// Token: 0x04000048 RID: 72
			private readonly T0 _value0;

			// Token: 0x04000049 RID: 73
			private readonly T1 _value1;

			// Token: 0x0400004A RID: 74
			private readonly T2 _value2;
		}

		// Token: 0x02000029 RID: 41
		private readonly struct LogValues<T0, T1, T2, T3> : IReadOnlyList<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyCollection<KeyValuePair<string, object>>
		{
			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003817 File Offset: 0x00001A17
			public int Count
			{
				get
				{
					return 5;
				}
			}

			// Token: 0x17000025 RID: 37
			public KeyValuePair<string, object> this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[0], this._value0);
					case 1:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[1], this._value1);
					case 2:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[2], this._value2);
					case 3:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[3], this._value3);
					case 4:
						return new KeyValuePair<string, object>("{OriginalFormat}", this._formatter.OriginalFormat);
					default:
						throw new IndexOutOfRangeException("index");
					}
				}
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x000038F0 File Offset: 0x00001AF0
			public LogValues(LogValuesFormatter formatter, T0 value0, T1 value1, T2 value2, T3 value3)
			{
				this._formatter = formatter;
				this._value0 = value0;
				this._value1 = value1;
				this._value2 = value2;
				this._value3 = value3;
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00003917 File Offset: 0x00001B17
			private object[] ToArray()
			{
				return new object[] { this._value0, this._value1, this._value2, this._value3 };
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00003957 File Offset: 0x00001B57
			public override string ToString()
			{
				return this._formatter.Format(this.ToArray());
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x0000396A File Offset: 0x00001B6A
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num)
				{
					yield return this[i];
					num = i + 1;
				}
				yield break;
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x0000397E File Offset: 0x00001B7E
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400004B RID: 75
			public static readonly Func<LoggerMessage.LogValues<T0, T1, T2, T3>, Exception, string> Callback = (LoggerMessage.LogValues<T0, T1, T2, T3> state, Exception exception) => state.ToString();

			// Token: 0x0400004C RID: 76
			private readonly LogValuesFormatter _formatter;

			// Token: 0x0400004D RID: 77
			private readonly T0 _value0;

			// Token: 0x0400004E RID: 78
			private readonly T1 _value1;

			// Token: 0x0400004F RID: 79
			private readonly T2 _value2;

			// Token: 0x04000050 RID: 80
			private readonly T3 _value3;
		}

		// Token: 0x0200002A RID: 42
		private readonly struct LogValues<T0, T1, T2, T3, T4> : IReadOnlyList<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyCollection<KeyValuePair<string, object>>
		{
			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000399D File Offset: 0x00001B9D
			public int Count
			{
				get
				{
					return 6;
				}
			}

			// Token: 0x17000027 RID: 39
			public KeyValuePair<string, object> this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[0], this._value0);
					case 1:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[1], this._value1);
					case 2:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[2], this._value2);
					case 3:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[3], this._value3);
					case 4:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[4], this._value4);
					case 5:
						return new KeyValuePair<string, object>("{OriginalFormat}", this._formatter.OriginalFormat);
					default:
						throw new IndexOutOfRangeException("index");
					}
				}
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00003A9A File Offset: 0x00001C9A
			public LogValues(LogValuesFormatter formatter, T0 value0, T1 value1, T2 value2, T3 value3, T4 value4)
			{
				this._formatter = formatter;
				this._value0 = value0;
				this._value1 = value1;
				this._value2 = value2;
				this._value3 = value3;
				this._value4 = value4;
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00003ACC File Offset: 0x00001CCC
			private object[] ToArray()
			{
				return new object[] { this._value0, this._value1, this._value2, this._value3, this._value4 };
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00003B25 File Offset: 0x00001D25
			public override string ToString()
			{
				return this._formatter.Format(this.ToArray());
			}

			// Token: 0x060000DD RID: 221 RVA: 0x00003B38 File Offset: 0x00001D38
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num)
				{
					yield return this[i];
					num = i + 1;
				}
				yield break;
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00003B4C File Offset: 0x00001D4C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000051 RID: 81
			public static readonly Func<LoggerMessage.LogValues<T0, T1, T2, T3, T4>, Exception, string> Callback = (LoggerMessage.LogValues<T0, T1, T2, T3, T4> state, Exception exception) => state.ToString();

			// Token: 0x04000052 RID: 82
			private readonly LogValuesFormatter _formatter;

			// Token: 0x04000053 RID: 83
			private readonly T0 _value0;

			// Token: 0x04000054 RID: 84
			private readonly T1 _value1;

			// Token: 0x04000055 RID: 85
			private readonly T2 _value2;

			// Token: 0x04000056 RID: 86
			private readonly T3 _value3;

			// Token: 0x04000057 RID: 87
			private readonly T4 _value4;
		}

		// Token: 0x0200002B RID: 43
		private readonly struct LogValues<T0, T1, T2, T3, T4, T5> : IReadOnlyList<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyCollection<KeyValuePair<string, object>>
		{
			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003B6B File Offset: 0x00001D6B
			public int Count
			{
				get
				{
					return 7;
				}
			}

			// Token: 0x17000029 RID: 41
			public KeyValuePair<string, object> this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[0], this._value0);
					case 1:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[1], this._value1);
					case 2:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[2], this._value2);
					case 3:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[3], this._value3);
					case 4:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[4], this._value4);
					case 5:
						return new KeyValuePair<string, object>(this._formatter.ValueNames[5], this._value5);
					case 6:
						return new KeyValuePair<string, object>("{OriginalFormat}", this._formatter.OriginalFormat);
					default:
						throw new IndexOutOfRangeException("index");
					}
				}
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00003C90 File Offset: 0x00001E90
			public LogValues(LogValuesFormatter formatter, T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
			{
				this._formatter = formatter;
				this._value0 = value0;
				this._value1 = value1;
				this._value2 = value2;
				this._value3 = value3;
				this._value4 = value4;
				this._value5 = value5;
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x00003CC8 File Offset: 0x00001EC8
			private object[] ToArray()
			{
				return new object[] { this._value0, this._value1, this._value2, this._value3, this._value4, this._value5 };
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x00003D2F File Offset: 0x00001F2F
			public override string ToString()
			{
				return this._formatter.Format(this.ToArray());
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x00003D42 File Offset: 0x00001F42
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num)
				{
					yield return this[i];
					num = i + 1;
				}
				yield break;
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00003D56 File Offset: 0x00001F56
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000058 RID: 88
			public static readonly Func<LoggerMessage.LogValues<T0, T1, T2, T3, T4, T5>, Exception, string> Callback = (LoggerMessage.LogValues<T0, T1, T2, T3, T4, T5> state, Exception exception) => state.ToString();

			// Token: 0x04000059 RID: 89
			private readonly LogValuesFormatter _formatter;

			// Token: 0x0400005A RID: 90
			private readonly T0 _value0;

			// Token: 0x0400005B RID: 91
			private readonly T1 _value1;

			// Token: 0x0400005C RID: 92
			private readonly T2 _value2;

			// Token: 0x0400005D RID: 93
			private readonly T3 _value3;

			// Token: 0x0400005E RID: 94
			private readonly T4 _value4;

			// Token: 0x0400005F RID: 95
			private readonly T5 _value5;
		}
	}
}
