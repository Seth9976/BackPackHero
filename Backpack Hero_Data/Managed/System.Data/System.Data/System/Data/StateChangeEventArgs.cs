using System;

namespace System.Data
{
	/// <summary>Provides data for the state change event of a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E0 RID: 224
	public sealed class StateChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StateChangeEventArgs" /> class, when given the original state and the current state of the object.</summary>
		/// <param name="originalState">One of the <see cref="T:System.Data.ConnectionState" /> values. </param>
		/// <param name="currentState">One of the <see cref="T:System.Data.ConnectionState" /> values. </param>
		// Token: 0x06000CA6 RID: 3238 RVA: 0x00039FC6 File Offset: 0x000381C6
		public StateChangeEventArgs(ConnectionState originalState, ConnectionState currentState)
		{
			this._originalState = originalState;
			this._currentState = currentState;
		}

		/// <summary>Gets the new state of the connection. The connection object will be in the new state already when the event is fired.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00039FDC File Offset: 0x000381DC
		public ConnectionState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		/// <summary>Gets the original state of the connection.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00039FE4 File Offset: 0x000381E4
		public ConnectionState OriginalState
		{
			get
			{
				return this._originalState;
			}
		}

		// Token: 0x04000831 RID: 2097
		private ConnectionState _originalState;

		// Token: 0x04000832 RID: 2098
		private ConnectionState _currentState;
	}
}
