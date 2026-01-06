using System;

namespace System.Net.WebSockets
{
	// Token: 0x020005F7 RID: 1527
	public readonly struct ValueWebSocketReceiveResult
	{
		// Token: 0x06003108 RID: 12552 RVA: 0x000AFB56 File Offset: 0x000ADD56
		public ValueWebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage)
		{
			if (count < 0)
			{
				ValueWebSocketReceiveResult.ThrowCountOutOfRange();
			}
			if (messageType > WebSocketMessageType.Close)
			{
				ValueWebSocketReceiveResult.ThrowMessageTypeOutOfRange();
			}
			this._countAndEndOfMessage = (uint)(count | (endOfMessage ? int.MinValue : 0));
			this._messageType = messageType;
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000AFB84 File Offset: 0x000ADD84
		public int Count
		{
			get
			{
				return (int)(this._countAndEndOfMessage & 2147483647U);
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000AFB92 File Offset: 0x000ADD92
		public bool EndOfMessage
		{
			get
			{
				return (this._countAndEndOfMessage & 2147483648U) == 2147483648U;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000AFBA7 File Offset: 0x000ADDA7
		public WebSocketMessageType MessageType
		{
			get
			{
				return this._messageType;
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000AFBAF File Offset: 0x000ADDAF
		private static void ThrowCountOutOfRange()
		{
			throw new ArgumentOutOfRangeException("count");
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000AFBBB File Offset: 0x000ADDBB
		private static void ThrowMessageTypeOutOfRange()
		{
			throw new ArgumentOutOfRangeException("messageType");
		}

		// Token: 0x04001DE4 RID: 7652
		private readonly uint _countAndEndOfMessage;

		// Token: 0x04001DE5 RID: 7653
		private readonly WebSocketMessageType _messageType;
	}
}
