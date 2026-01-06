using System;

namespace TwitchLib.Client.Exceptions
{
	// Token: 0x0200001A RID: 26
	public class BadListenException : Exception
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00007DD3 File Offset: 0x00005FD3
		public BadListenException(string eventName, string additionalDetails = "")
			: base(string.IsNullOrEmpty(additionalDetails) ? ("You are listening to event '" + eventName + "', which is not currently allowed. See details: " + additionalDetails) : ("You are listening to event '" + eventName + "', which is not currently allowed."))
		{
		}
	}
}
