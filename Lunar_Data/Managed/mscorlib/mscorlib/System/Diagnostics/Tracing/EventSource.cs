using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides the ability to create events for event tracing for Windows (ETW).</summary>
	// Token: 0x020009F7 RID: 2551
	public class EventSource : IDisposable
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x06005AD2 RID: 23250 RVA: 0x00134519 File Offset: 0x00132719
		protected EventSource()
		{
			this.Name = base.GetType().Name;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class and specifies whether to throw an exception when an error occurs in the underlying Windows code.</summary>
		/// <param name="throwOnEventWriteErrors">true to throw an exception when an error occurs in the underlying Windows code; otherwise, false.</param>
		// Token: 0x06005AD3 RID: 23251 RVA: 0x0007B570 File Offset: 0x00079770
		protected EventSource(bool throwOnEventWriteErrors)
			: this()
		{
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x00134532 File Offset: 0x00132732
		protected EventSource(EventSourceSettings settings)
			: this()
		{
			this.Settings = settings;
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x00134541 File Offset: 0x00132741
		protected EventSource(EventSourceSettings settings, params string[] traits)
			: this(settings)
		{
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x0013454A File Offset: 0x0013274A
		public EventSource(string eventSourceName)
		{
			this.Name = eventSourceName;
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x00134559 File Offset: 0x00132759
		public EventSource(string eventSourceName, EventSourceSettings config)
			: this(eventSourceName)
		{
			this.Settings = config;
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x00134569 File Offset: 0x00132769
		public EventSource(string eventSourceName, EventSourceSettings config, params string[] traits)
			: this(eventSourceName, config)
		{
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x00134573 File Offset: 0x00132773
		internal EventSource(Guid eventSourceGuid, string eventSourceName)
			: this(eventSourceName)
		{
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x0013457C File Offset: 0x0013277C
		~EventSource()
		{
			this.Dispose(false);
		}

		/// <summary>Gets any exception that was thrown during the construction of the event source. </summary>
		/// <returns>The exception that was thrown during the construction of the event source, or null if no exception was thrown. </returns>
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005ADB RID: 23259 RVA: 0x0000AF5E File Offset: 0x0000915E
		public Exception ConstructionException
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the activity ID of the current thread. </summary>
		/// <returns>The activity ID of the current thread. </returns>
		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06005ADC RID: 23260 RVA: 0x001345AC File Offset: 0x001327AC
		public static Guid CurrentThreadActivityId
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>The unique identifier for the event source.</summary>
		/// <returns>A unique identifier for the event source.</returns>
		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06005ADD RID: 23261 RVA: 0x001345AC File Offset: 0x001327AC
		public Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>The friendly name of the class that is derived from the event source.</summary>
		/// <returns>The friendly name of the derived class.  The default is the simple name of the class.</returns>
		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06005ADE RID: 23262 RVA: 0x001345B3 File Offset: 0x001327B3
		// (set) Token: 0x06005ADF RID: 23263 RVA: 0x001345BB File Offset: 0x001327BB
		public string Name { get; private set; }

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06005AE0 RID: 23264 RVA: 0x001345C4 File Offset: 0x001327C4
		// (set) Token: 0x06005AE1 RID: 23265 RVA: 0x001345CC File Offset: 0x001327CC
		public EventSourceSettings Settings { get; private set; }

		/// <summary>Determines whether the current event source is enabled.</summary>
		/// <returns>true if the current event source is enabled; otherwise, false.</returns>
		// Token: 0x06005AE2 RID: 23266 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsEnabled()
		{
			return false;
		}

		/// <summary>Determines whether the current event source that has the specified level and keyword is enabled.</summary>
		/// <returns>true if the event source is enabled; otherwise, false.</returns>
		/// <param name="level">The level of the event source.</param>
		/// <param name="keywords">The keyword of the event source.</param>
		// Token: 0x06005AE3 RID: 23267 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsEnabled(EventLevel level, EventKeywords keywords)
		{
			return false;
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsEnabled(EventLevel level, EventKeywords keywords, EventChannel channel)
		{
			return false;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x06005AE5 RID: 23269 RVA: 0x001345D5 File Offset: 0x001327D5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x0000AF5E File Offset: 0x0000915E
		public string GetTrait(string key)
		{
			return null;
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write(string eventName)
		{
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write(string eventName, EventSourceOptions options)
		{
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write<T>(string eventName, T data)
		{
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[CLSCompliant(false)]
		public void Write<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write<T>(string eventName, ref EventSourceOptions options, ref Guid activityId, ref Guid relatedActivityId, ref T data)
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06005AED RID: 23277 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Called when the current event source is updated by the controller.</summary>
		/// <param name="command">The arguments for the event.</param>
		// Token: 0x06005AEE RID: 23278 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void OnEventCommand(EventCommandEventArgs command)
		{
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal void ReportOutOfBandMessage(string msg, bool flush)
		{
		}

		/// <summary>Writes an event by using the provided event identifier.</summary>
		/// <param name="eventId">The event identifier.</param>
		// Token: 0x06005AF0 RID: 23280 RVA: 0x001345E4 File Offset: 0x001327E4
		protected void WriteEvent(int eventId)
		{
			this.WriteEvent(eventId, new object[0]);
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x001345F3 File Offset: 0x001327F3
		protected void WriteEvent(int eventId, byte[] arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">An integer argument.</param>
		// Token: 0x06005AF2 RID: 23282 RVA: 0x00134606 File Offset: 0x00132806
		protected void WriteEvent(int eventId, int arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		/// <summary>Writes an event by using the provided event identifier and string argument.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		// Token: 0x06005AF3 RID: 23283 RVA: 0x001345F3 File Offset: 0x001327F3
		protected void WriteEvent(int eventId, string arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		// Token: 0x06005AF4 RID: 23284 RVA: 0x0013461E File Offset: 0x0013281E
		protected void WriteEvent(int eventId, int arg1, int arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		/// <param name="arg3">An integer argument.</param>
		// Token: 0x06005AF5 RID: 23285 RVA: 0x0013463F File Offset: 0x0013283F
		protected void WriteEvent(int eventId, int arg1, int arg2, int arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x0013466A File Offset: 0x0013286A
		protected void WriteEvent(int eventId, int arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		// Token: 0x06005AF7 RID: 23287 RVA: 0x00134686 File Offset: 0x00132886
		protected void WriteEvent(int eventId, long arg1)
		{
			this.WriteEvent(eventId, new object[] { arg1 });
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x0013469E File Offset: 0x0013289E
		protected void WriteEvent(int eventId, long arg1, byte[] arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		// Token: 0x06005AF9 RID: 23289 RVA: 0x001346BA File Offset: 0x001328BA
		protected void WriteEvent(int eventId, long arg1, long arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		/// <param name="arg3">A 64 bit integer argument.</param>
		// Token: 0x06005AFA RID: 23290 RVA: 0x001346DB File Offset: 0x001328DB
		protected void WriteEvent(int eventId, long arg1, long arg2, long arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		// Token: 0x06005AFB RID: 23291 RVA: 0x0013469E File Offset: 0x0013289E
		protected void WriteEvent(int eventId, long arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and array of arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="args">An array of objects.</param>
		// Token: 0x06005AFC RID: 23292 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected void WriteEvent(int eventId, params object[] args)
		{
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 32 bit integer argument.</param>
		// Token: 0x06005AFD RID: 23293 RVA: 0x00134706 File Offset: 0x00132906
		protected void WriteEvent(int eventId, string arg1, int arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 32 bit integer argument.</param>
		/// <param name="arg3">A 32 bit integer argument.</param>
		// Token: 0x06005AFE RID: 23294 RVA: 0x00134722 File Offset: 0x00132922
		protected void WriteEvent(int eventId, string arg1, int arg2, int arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		// Token: 0x06005AFF RID: 23295 RVA: 0x00134748 File Offset: 0x00132948
		protected void WriteEvent(int eventId, string arg1, long arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and string arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A string argument.</param>
		// Token: 0x06005B00 RID: 23296 RVA: 0x00134764 File Offset: 0x00132964
		protected void WriteEvent(int eventId, string arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2 });
		}

		/// <summary>Writes an event by using the provided event identifier and string arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A string argument.</param>
		/// <param name="arg3">A string argument.</param>
		// Token: 0x06005B01 RID: 23297 RVA: 0x0013477B File Offset: 0x0013297B
		protected void WriteEvent(int eventId, string arg1, string arg2, string arg3)
		{
			this.WriteEvent(eventId, new object[] { arg1, arg2, arg3 });
		}

		/// <summary>Creates a new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload by using the provided event identifier and event data.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="eventDataCount">The number of event data items.</param>
		/// <param name="data">The structure that contains the event data.</param>
		// Token: 0x06005B02 RID: 23298 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[CLSCompliant(false)]
		protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
		{
		}

		/// <summary>Writes an event that indicates that the current activity is related to another activity. </summary>
		/// <param name="eventId">An identifier that uniquely identifies this event within the <see cref="T:System.Diagnostics.Tracing.EventSource" />. </param>
		/// <param name="childActivityID">The related activity identifier. </param>
		/// <param name="args">An array of objects that contain data about the event, or null if only the <paramref name="childActivityID" /> is needed.</param>
		// Token: 0x06005B03 RID: 23299 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected void WriteEventWithRelatedActivityId(int eventId, Guid relatedActivityId, params object[] args)
		{
		}

		/// <summary>Writes an event that indicates that the current activity is related to another activity.</summary>
		/// <param name="eventId">An identifier that uniquely identifies this event within the <see cref="T:System.Diagnostics.Tracing.EventSource" />.</param>
		/// <param name="childActivityID">A pointer to the GUID of the child activity ID. </param>
		/// <param name="eventDataCount">The number of items in the <paramref name="data" /> field. </param>
		/// <param name="data">A pointer to the first item in the event data field. </param>
		// Token: 0x06005B04 RID: 23300 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[CLSCompliant(false)]
		protected unsafe void WriteEventWithRelatedActivityIdCore(int eventId, Guid* relatedActivityId, int eventDataCount, EventSource.EventData* data)
		{
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06005B05 RID: 23301 RVA: 0x000479FC File Offset: 0x00045BFC
		// (remove) Token: 0x06005B06 RID: 23302 RVA: 0x000479FC File Offset: 0x00045BFC
		public event EventHandler<EventCommandEventArgs> EventCommandExecuted
		{
			add
			{
				throw new NotImplementedException();
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns a string of the XML manifest that is associated with the current event source.</summary>
		/// <returns>The XML data string.</returns>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <param name="assemblyPathToIncludeInManifest">The path to the .dll file to include in the manifest. </param>
		// Token: 0x06005B07 RID: 23303 RVA: 0x000479FC File Offset: 0x00045BFC
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x000479FC File Offset: 0x00045BFC
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest, EventManifestOptions flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the unique identifier for this implementation of the event source.</summary>
		/// <returns>A unique identifier for this event source type.</returns>
		/// <param name="eventSourceType">The type of the event source.</param>
		// Token: 0x06005B09 RID: 23305 RVA: 0x000479FC File Offset: 0x00045BFC
		public static Guid GetGuid(Type eventSourceType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the friendly name of the event source.</summary>
		/// <returns>The friendly name of the event source. The default is the simple name of the class.</returns>
		/// <param name="eventSourceType">The type of the event source.</param>
		// Token: 0x06005B0A RID: 23306 RVA: 0x000479FC File Offset: 0x00045BFC
		public static string GetName(Type eventSourceType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a snapshot of all the event sources for the application domain.</summary>
		/// <returns>An enumeration of all the event sources in the application domain.</returns>
		// Token: 0x06005B0B RID: 23307 RVA: 0x000479FC File Offset: 0x00045BFC
		public static IEnumerable<EventSource> GetSources()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a command to a specified event source.</summary>
		/// <param name="eventSource">The event source to send the command to.</param>
		/// <param name="command">The event command to send.</param>
		/// <param name="commandArguments">The arguments for the event command.</param>
		// Token: 0x06005B0C RID: 23308 RVA: 0x000479FC File Offset: 0x00045BFC
		public static void SendCommand(EventSource eventSource, EventCommand command, IDictionary<string, string> commandArguments)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the activity ID on the current thread.</summary>
		/// <param name="activityId">The current thread's new activity ID, or <see cref="F:System.Guid.Empty" /> to indicate that work on the current thread is not associated with any activity. </param>
		// Token: 0x06005B0D RID: 23309 RVA: 0x000479FC File Offset: 0x00045BFC
		public static void SetCurrentThreadActivityId(Guid activityId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the activity ID on the current thread, and returns the previous activity ID.</summary>
		/// <param name="activityId">The current thread's new activity ID, or <see cref="F:System.Guid.Empty" /> to indicate that work on the current thread is not associated with any activity.</param>
		/// <param name="oldActivityThatWillContinue">When this method returns, contains the previous activity ID on the current thread. </param>
		// Token: 0x06005B0E RID: 23310 RVA: 0x000479FC File Offset: 0x00045BFC
		public static void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides the event data for creating fast <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overloads by using the <see cref="M:System.Diagnostics.Tracing.EventSource.WriteEventCore(System.Int32,System.Int32,System.Diagnostics.Tracing.EventSource.EventData*)" /> method.</summary>
		// Token: 0x020009F8 RID: 2552
		protected internal struct EventData
		{
			/// <summary>Gets or sets the pointer to the data for the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The pointer to the data.</returns>
			// Token: 0x17000F93 RID: 3987
			// (get) Token: 0x06005B0F RID: 23311 RVA: 0x00134797 File Offset: 0x00132997
			// (set) Token: 0x06005B10 RID: 23312 RVA: 0x0013479F File Offset: 0x0013299F
			public IntPtr DataPointer { readonly get; set; }

			/// <summary>Gets or sets the number of payload items in the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The number of payload items in the new overload.</returns>
			// Token: 0x17000F94 RID: 3988
			// (get) Token: 0x06005B11 RID: 23313 RVA: 0x001347A8 File Offset: 0x001329A8
			// (set) Token: 0x06005B12 RID: 23314 RVA: 0x001347B0 File Offset: 0x001329B0
			public int Size { readonly get; set; }

			// Token: 0x17000F95 RID: 3989
			// (get) Token: 0x06005B13 RID: 23315 RVA: 0x001347B9 File Offset: 0x001329B9
			// (set) Token: 0x06005B14 RID: 23316 RVA: 0x001347C1 File Offset: 0x001329C1
			internal int Reserved { readonly get; set; }
		}
	}
}
