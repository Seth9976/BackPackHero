using System;

namespace TwitchLib.Client.Exceptions
{
	// Token: 0x0200001F RID: 31
	public class EventNotHandled : Exception
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00007E42 File Offset: 0x00006042
		public EventNotHandled(string eventName, string additionalDetails = "")
			: base(string.IsNullOrEmpty(additionalDetails) ? ("To use this call, you must handle/subscribe to event: " + eventName) : ("To use this call, you must handle/subscribe to event: " + eventName + ", additional details: " + additionalDetails))
		{
		}
	}
}
