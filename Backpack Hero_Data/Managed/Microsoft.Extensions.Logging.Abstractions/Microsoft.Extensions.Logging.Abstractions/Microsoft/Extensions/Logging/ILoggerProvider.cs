using System;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Represents a type that can create instances of <see cref="T:Microsoft.Extensions.Logging.ILogger" />.
	/// </summary>
	// Token: 0x0200000F RID: 15
	public interface ILoggerProvider : IDisposable
	{
		/// <summary>
		/// Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
		/// </summary>
		/// <param name="categoryName">The category name for messages produced by the logger.</param>
		/// <returns>The instance of <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that was created.</returns>
		// Token: 0x06000033 RID: 51
		ILogger CreateLogger(string categoryName);
	}
}
