using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x0200001C RID: 28
	public class InvalidCredentialException : Exception
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003661 File Offset: 0x00001861
		public InvalidCredentialException(string data)
			: base(data)
		{
		}
	}
}
