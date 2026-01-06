using System;

namespace System.ComponentModel
{
	/// <summary>Provides a simple list of delegates. This class cannot be inherited.</summary>
	// Token: 0x02000681 RID: 1665
	public sealed class EventHandlerList : IDisposable
	{
		// Token: 0x06003588 RID: 13704 RVA: 0x000BEF5A File Offset: 0x000BD15A
		internal EventHandlerList(Component parent)
		{
			this._parent = parent;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventHandlerList" /> class. </summary>
		// Token: 0x06003589 RID: 13705 RVA: 0x0000219B File Offset: 0x0000039B
		public EventHandlerList()
		{
		}

		/// <summary>Gets or sets the delegate for the specified object.</summary>
		/// <returns>The delegate for the specified key, or null if a delegate does not exist.</returns>
		/// <param name="key">An object to find in the list. </param>
		// Token: 0x17000C5B RID: 3163
		public Delegate this[object key]
		{
			get
			{
				EventHandlerList.ListEntry listEntry = null;
				if (this._parent == null || this._parent.CanRaiseEventsInternal)
				{
					listEntry = this.Find(key);
				}
				if (listEntry == null)
				{
					return null;
				}
				return listEntry._handler;
			}
			set
			{
				EventHandlerList.ListEntry listEntry = this.Find(key);
				if (listEntry != null)
				{
					listEntry._handler = value;
					return;
				}
				this._head = new EventHandlerList.ListEntry(key, value, this._head);
			}
		}

		/// <summary>Adds a delegate to the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to add to the list. </param>
		// Token: 0x0600358C RID: 13708 RVA: 0x000BEFD8 File Offset: 0x000BD1D8
		public void AddHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry._handler = Delegate.Combine(listEntry._handler, value);
				return;
			}
			this._head = new EventHandlerList.ListEntry(key, value, this._head);
		}

		/// <summary>Adds a list of delegates to the current list.</summary>
		/// <param name="listToAddFrom">The list to add.</param>
		// Token: 0x0600358D RID: 13709 RVA: 0x000BF018 File Offset: 0x000BD218
		public void AddHandlers(EventHandlerList listToAddFrom)
		{
			for (EventHandlerList.ListEntry listEntry = listToAddFrom._head; listEntry != null; listEntry = listEntry._next)
			{
				this.AddHandler(listEntry._key, listEntry._handler);
			}
		}

		/// <summary>Disposes the delegate list.</summary>
		// Token: 0x0600358E RID: 13710 RVA: 0x000BF04A File Offset: 0x000BD24A
		public void Dispose()
		{
			this._head = null;
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000BF054 File Offset: 0x000BD254
		private EventHandlerList.ListEntry Find(object key)
		{
			EventHandlerList.ListEntry listEntry = this._head;
			while (listEntry != null && listEntry._key != key)
			{
				listEntry = listEntry._next;
			}
			return listEntry;
		}

		/// <summary>Removes a delegate from the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to remove from the list. </param>
		// Token: 0x06003590 RID: 13712 RVA: 0x000BF080 File Offset: 0x000BD280
		public void RemoveHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry._handler = Delegate.Remove(listEntry._handler, value);
			}
		}

		// Token: 0x04002023 RID: 8227
		private EventHandlerList.ListEntry _head;

		// Token: 0x04002024 RID: 8228
		private Component _parent;

		// Token: 0x02000682 RID: 1666
		private sealed class ListEntry
		{
			// Token: 0x06003591 RID: 13713 RVA: 0x000BF0AA File Offset: 0x000BD2AA
			public ListEntry(object key, Delegate handler, EventHandlerList.ListEntry next)
			{
				this._next = next;
				this._key = key;
				this._handler = handler;
			}

			// Token: 0x04002025 RID: 8229
			internal EventHandlerList.ListEntry _next;

			// Token: 0x04002026 RID: 8230
			internal object _key;

			// Token: 0x04002027 RID: 8231
			internal Delegate _handler;
		}
	}
}
