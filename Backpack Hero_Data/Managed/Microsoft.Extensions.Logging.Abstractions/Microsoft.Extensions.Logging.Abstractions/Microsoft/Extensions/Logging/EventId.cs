using System;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Identifies a logging event. The primary identifier is the "Id" property, with the "Name" property providing a short description of this type of event.
	/// </summary>
	// Token: 0x0200000A RID: 10
	public readonly struct EventId
	{
		/// <summary>
		/// Implicitly creates an EventId from the given <see cref="T:System.Int32" />.
		/// </summary>
		/// <param name="i">The <see cref="T:System.Int32" /> to convert to an EventId.</param>
		// Token: 0x0600001A RID: 26 RVA: 0x00002695 File Offset: 0x00000895
		public static implicit operator EventId(int i)
		{
			return new EventId(i, null);
		}

		/// <summary>
		/// Checks if two specified <see cref="T:Microsoft.Extensions.Logging.EventId" /> instances have the same value. They are equal if they have the same Id.
		/// </summary>
		/// <param name="left">The first <see cref="T:Microsoft.Extensions.Logging.EventId" />.</param>
		/// <param name="right">The second <see cref="T:Microsoft.Extensions.Logging.EventId" />.</param>
		/// <returns><see langword="true" /> if the objects are equal.</returns>
		// Token: 0x0600001B RID: 27 RVA: 0x0000269E File Offset: 0x0000089E
		public static bool operator ==(EventId left, EventId right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Checks if two specified <see cref="T:Microsoft.Extensions.Logging.EventId" /> instances have different values.
		/// </summary>
		/// <param name="left">The first <see cref="T:Microsoft.Extensions.Logging.EventId" />.</param>
		/// <param name="right">The second <see cref="T:Microsoft.Extensions.Logging.EventId" />.</param>
		/// <returns><see langword="true" /> if the objects are not equal.</returns>
		// Token: 0x0600001C RID: 28 RVA: 0x000026A8 File Offset: 0x000008A8
		public static bool operator !=(EventId left, EventId right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Initializes an instance of the <see cref="T:Microsoft.Extensions.Logging.EventId" /> struct.
		/// </summary>
		/// <param name="id">The numeric identifier for this event.</param>
		/// <param name="name">The name of this event.</param>
		// Token: 0x0600001D RID: 29 RVA: 0x000026B5 File Offset: 0x000008B5
		public EventId(int id, string name = null)
		{
			this.Id = id;
			this.Name = name;
		}

		/// <summary>
		/// Gets the numeric identifier for this event.
		/// </summary>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000026C5 File Offset: 0x000008C5
		public int Id { get; }

		/// <summary>
		/// Gets the name of this event.
		/// </summary>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000026CD File Offset: 0x000008CD
		public string Name { get; }

		/// <inheritdoc />
		// Token: 0x06000020 RID: 32 RVA: 0x000026D8 File Offset: 0x000008D8
		public override string ToString()
		{
			return this.Name ?? this.Id.ToString();
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type. Two events are equal if they have the same id.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns><see langword="true" /> if the current object is equal to the other parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000021 RID: 33 RVA: 0x000026FD File Offset: 0x000008FD
		public bool Equals(EventId other)
		{
			return this.Id == other.Id;
		}

		/// <inheritdoc />
		// Token: 0x06000022 RID: 34 RVA: 0x00002710 File Offset: 0x00000910
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is EventId)
			{
				EventId eventId = (EventId)obj;
				return this.Equals(eventId);
			}
			return false;
		}

		/// <inheritdoc />
		// Token: 0x06000023 RID: 35 RVA: 0x0000273A File Offset: 0x0000093A
		public override int GetHashCode()
		{
			return this.Id;
		}
	}
}
