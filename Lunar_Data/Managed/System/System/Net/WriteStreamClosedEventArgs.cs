using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.WriteStreamClosed" /> event.</summary>
	// Token: 0x020003D2 RID: 978
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class WriteStreamClosedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WriteStreamClosedEventArgs" /> class.</summary>
		// Token: 0x06002043 RID: 8259 RVA: 0x0000C6B5 File Offset: 0x0000A8B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public WriteStreamClosedEventArgs()
		{
		}

		/// <summary>Gets the error value when a write stream is closed.</summary>
		/// <returns>Returns <see cref="T:System.Exception" />.</returns>
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x00002F6A File Offset: 0x0000116A
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Exception Error
		{
			get
			{
				return null;
			}
		}
	}
}
