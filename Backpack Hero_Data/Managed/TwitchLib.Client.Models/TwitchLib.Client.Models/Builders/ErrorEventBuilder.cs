using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000032 RID: 50
	public sealed class ErrorEventBuilder : IBuilder<ErrorEvent>
	{
		// Token: 0x060001BE RID: 446 RVA: 0x000080A3 File Offset: 0x000062A3
		private ErrorEventBuilder()
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000080AB File Offset: 0x000062AB
		public ErrorEventBuilder WithMessage(string message)
		{
			this._message = message;
			return this;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000080B5 File Offset: 0x000062B5
		public static ErrorEventBuilder Create()
		{
			return new ErrorEventBuilder();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000080BC File Offset: 0x000062BC
		public ErrorEvent Build()
		{
			return new ErrorEvent
			{
				Message = this._message
			};
		}

		// Token: 0x040001D1 RID: 465
		private string _message;
	}
}
