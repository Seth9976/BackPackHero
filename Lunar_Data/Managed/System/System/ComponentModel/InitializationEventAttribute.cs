using System;

namespace System.ComponentModel
{
	/// <summary>Specifies which event is raised on initialization. This class cannot be inherited.</summary>
	// Token: 0x02000688 RID: 1672
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class InitializationEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InitializationEventAttribute" /> class.</summary>
		/// <param name="eventName">The name of the initialization event.</param>
		// Token: 0x060035A7 RID: 13735 RVA: 0x000BF15B File Offset: 0x000BD35B
		public InitializationEventAttribute(string eventName)
		{
			this.EventName = eventName;
		}

		/// <summary>Gets the name of the initialization event.</summary>
		/// <returns>The name of the initialization event.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x000BF16A File Offset: 0x000BD36A
		public string EventName { get; }
	}
}
