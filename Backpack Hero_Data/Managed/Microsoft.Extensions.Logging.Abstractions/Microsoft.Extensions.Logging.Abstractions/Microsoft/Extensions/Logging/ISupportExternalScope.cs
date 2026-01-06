using System;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Represents a <see cref="T:Microsoft.Extensions.Logging.ILoggerProvider" /> that is able to consume external scope information.
	/// </summary>
	// Token: 0x02000011 RID: 17
	public interface ISupportExternalScope
	{
		/// <summary>
		/// Sets external scope information source for logger provider.
		/// </summary>
		/// <param name="scopeProvider">The provider of scope data.</param>
		// Token: 0x06000034 RID: 52
		void SetScopeProvider(IExternalScopeProvider scopeProvider);
	}
}
