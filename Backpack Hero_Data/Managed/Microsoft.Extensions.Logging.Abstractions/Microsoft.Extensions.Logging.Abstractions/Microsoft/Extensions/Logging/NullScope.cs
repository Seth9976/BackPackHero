using System;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// An empty scope without any logic
	/// </summary>
	// Token: 0x0200001A RID: 26
	internal class NullScope : IDisposable
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000324F File Offset: 0x0000144F
		public static NullScope Instance { get; } = new NullScope();

		// Token: 0x06000082 RID: 130 RVA: 0x00003256 File Offset: 0x00001456
		private NullScope()
		{
		}

		/// <inheritdoc />
		// Token: 0x06000083 RID: 131 RVA: 0x0000325E File Offset: 0x0000145E
		public void Dispose()
		{
		}
	}
}
