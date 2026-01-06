using System;
using Microsoft.Extensions.Internal;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// ILoggerFactory extension methods for common scenarios.
	/// </summary>
	// Token: 0x02000014 RID: 20
	public static class LoggerFactoryExtensions
	{
		/// <summary>
		/// Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance using the full name of the given type.
		/// </summary>
		/// <param name="factory">The factory.</param>
		/// <typeparam name="T">The type.</typeparam>
		/// <returns>The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that was created.</returns>
		// Token: 0x06000058 RID: 88 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public static ILogger<T> CreateLogger<T>(this ILoggerFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			return new Logger<T>(factory);
		}

		/// <summary>
		/// Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance using the full name of the given <paramref name="type" />.
		/// </summary>
		/// <param name="factory">The factory.</param>
		/// <param name="type">The type.</param>
		/// <returns>The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that was created.</returns>
		// Token: 0x06000059 RID: 89 RVA: 0x00002B0E File Offset: 0x00000D0E
		public static ILogger CreateLogger(this ILoggerFactory factory, Type type)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return factory.CreateLogger(TypeNameHelper.GetTypeDisplayName(type, true, false, false, '.'));
		}
	}
}
