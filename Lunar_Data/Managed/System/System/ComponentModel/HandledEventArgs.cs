using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for events that can be handled completely in an event handler. </summary>
	// Token: 0x020006C2 RID: 1730
	public class HandledEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with a default <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property value of false.</summary>
		// Token: 0x06003757 RID: 14167 RVA: 0x000C389E File Offset: 0x000C1A9E
		public HandledEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with the specified default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</summary>
		/// <param name="defaultHandledValue">The default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</param>
		// Token: 0x06003758 RID: 14168 RVA: 0x000C38A7 File Offset: 0x000C1AA7
		public HandledEventArgs(bool defaultHandledValue)
		{
			this.Handled = defaultHandledValue;
		}

		/// <summary>Gets or sets a value that indicates whether the event handler has completely handled the event or whether the system should continue its own processing.</summary>
		/// <returns>true if the event has been completely handled; otherwise, false.</returns>
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000C38B6 File Offset: 0x000C1AB6
		// (set) Token: 0x0600375A RID: 14170 RVA: 0x000C38BE File Offset: 0x000C1ABE
		public bool Handled { get; set; }
	}
}
