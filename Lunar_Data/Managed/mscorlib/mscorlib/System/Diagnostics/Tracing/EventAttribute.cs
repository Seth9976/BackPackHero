using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies additional event schema information for an event.</summary>
	// Token: 0x020009EC RID: 2540
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class EventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> class with the specified event identifier.</summary>
		/// <param name="eventId">The event identifier for the event.</param>
		// Token: 0x06005A9F RID: 23199 RVA: 0x0013436F File Offset: 0x0013256F
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
		}

		/// <summary>Gets or sets the identifier for the event.</summary>
		/// <returns>The event identifier.</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005AA0 RID: 23200 RVA: 0x0013437E File Offset: 0x0013257E
		// (set) Token: 0x06005AA1 RID: 23201 RVA: 0x00134386 File Offset: 0x00132586
		public int EventId { get; private set; }

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06005AA2 RID: 23202 RVA: 0x0013438F File Offset: 0x0013258F
		// (set) Token: 0x06005AA3 RID: 23203 RVA: 0x00134397 File Offset: 0x00132597
		public EventActivityOptions ActivityOptions { get; set; }

		/// <summary>Gets or sets the level for the event.</summary>
		/// <returns>One of the enumeration values that specifies the level for the event.</returns>
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005AA4 RID: 23204 RVA: 0x001343A0 File Offset: 0x001325A0
		// (set) Token: 0x06005AA5 RID: 23205 RVA: 0x001343A8 File Offset: 0x001325A8
		public EventLevel Level { get; set; }

		/// <summary>Gets or sets the keywords for the event.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06005AA6 RID: 23206 RVA: 0x001343B1 File Offset: 0x001325B1
		// (set) Token: 0x06005AA7 RID: 23207 RVA: 0x001343B9 File Offset: 0x001325B9
		public EventKeywords Keywords { get; set; }

		/// <summary>Gets or sets the operation code for the event.</summary>
		/// <returns>One of the enumeration values that specifies the operation code.</returns>
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06005AA8 RID: 23208 RVA: 0x001343C2 File Offset: 0x001325C2
		// (set) Token: 0x06005AA9 RID: 23209 RVA: 0x001343CA File Offset: 0x001325CA
		public EventOpcode Opcode { get; set; }

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06005AAA RID: 23210 RVA: 0x001343D3 File Offset: 0x001325D3
		// (set) Token: 0x06005AAB RID: 23211 RVA: 0x001343DB File Offset: 0x001325DB
		public EventChannel Channel { get; set; }

		/// <summary>Gets or sets the message for the event.</summary>
		/// <returns>The message for the event.</returns>
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06005AAC RID: 23212 RVA: 0x001343E4 File Offset: 0x001325E4
		// (set) Token: 0x06005AAD RID: 23213 RVA: 0x001343EC File Offset: 0x001325EC
		public string Message { get; set; }

		/// <summary>Gets or sets the task for the event.</summary>
		/// <returns>The task for the event.</returns>
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06005AAE RID: 23214 RVA: 0x001343F5 File Offset: 0x001325F5
		// (set) Token: 0x06005AAF RID: 23215 RVA: 0x001343FD File Offset: 0x001325FD
		public EventTask Task { get; set; }

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06005AB0 RID: 23216 RVA: 0x00134406 File Offset: 0x00132606
		// (set) Token: 0x06005AB1 RID: 23217 RVA: 0x0013440E File Offset: 0x0013260E
		public EventTags Tags { get; set; }

		/// <summary>Gets or sets the version of the event.</summary>
		/// <returns>The version of the event.</returns>
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005AB2 RID: 23218 RVA: 0x00134417 File Offset: 0x00132617
		// (set) Token: 0x06005AB3 RID: 23219 RVA: 0x0013441F File Offset: 0x0013261F
		public byte Version { get; set; }
	}
}
