using System;

namespace Microsoft.Extensions.Logging.Abstractions
{
	/// <summary>
	/// An <see cref="T:Microsoft.Extensions.Logging.ILoggerFactory" /> used to create an instance of
	/// <see cref="T:Microsoft.Extensions.Logging.Abstractions.NullLogger" /> that logs nothing.
	/// </summary>
	// Token: 0x0200001D RID: 29
	public class NullLoggerFactory : ILoggerFactory, IDisposable
	{
		/// <inheritdoc />
		/// <remarks>
		/// This returns a <see cref="T:Microsoft.Extensions.Logging.Abstractions.NullLogger" /> instance that logs nothing.
		/// </remarks>
		// Token: 0x06000093 RID: 147 RVA: 0x000032FA File Offset: 0x000014FA
		public ILogger CreateLogger(string name)
		{
			return NullLogger.Instance;
		}

		/// <inheritdoc />
		/// <remarks>
		/// This method ignores the parameter and does nothing.
		/// </remarks>
		// Token: 0x06000094 RID: 148 RVA: 0x00003301 File Offset: 0x00001501
		public void AddProvider(ILoggerProvider provider)
		{
		}

		/// <inheritdoc />
		// Token: 0x06000095 RID: 149 RVA: 0x00003303 File Offset: 0x00001503
		public void Dispose()
		{
		}

		/// <summary>
		/// Returns the shared instance of <see cref="T:Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory" />.
		/// </summary>
		// Token: 0x0400002A RID: 42
		public static readonly NullLoggerFactory Instance = new NullLoggerFactory();
	}
}
