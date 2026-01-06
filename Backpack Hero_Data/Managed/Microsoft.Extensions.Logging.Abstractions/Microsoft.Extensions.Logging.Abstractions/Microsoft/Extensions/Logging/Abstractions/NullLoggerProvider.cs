using System;

namespace Microsoft.Extensions.Logging.Abstractions
{
	/// <summary>
	/// Provider for the <see cref="T:Microsoft.Extensions.Logging.Abstractions.NullLogger" />.
	/// </summary>
	// Token: 0x0200001E RID: 30
	public class NullLoggerProvider : ILoggerProvider, IDisposable
	{
		/// <summary>
		/// Returns an instance of <see cref="T:Microsoft.Extensions.Logging.Abstractions.NullLoggerProvider" />.
		/// </summary>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003311 File Offset: 0x00001511
		public static NullLoggerProvider Instance { get; } = new NullLoggerProvider();

		// Token: 0x06000098 RID: 152 RVA: 0x00003318 File Offset: 0x00001518
		private NullLoggerProvider()
		{
		}

		/// <inheritdoc />
		// Token: 0x06000099 RID: 153 RVA: 0x00003320 File Offset: 0x00001520
		public ILogger CreateLogger(string categoryName)
		{
			return NullLogger.Instance;
		}

		/// <inheritdoc />
		// Token: 0x0600009A RID: 154 RVA: 0x00003327 File Offset: 0x00001527
		public void Dispose()
		{
		}
	}
}
