using System;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// A generic interface for logging where the category name is derived from the specified
	/// <typeparamref name="TCategoryName" /> type name.
	/// Generally used to enable activation of a named <see cref="T:Microsoft.Extensions.Logging.ILogger" /> from dependency injection.
	/// </summary>
	/// <typeparam name="TCategoryName">The type whose name is used for the logger category name.</typeparam>
	// Token: 0x02000010 RID: 16
	public interface ILogger<out TCategoryName> : ILogger
	{
	}
}
