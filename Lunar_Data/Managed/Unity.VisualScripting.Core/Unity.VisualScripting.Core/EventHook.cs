using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000050 RID: 80
	public struct EventHook
	{
		// Token: 0x0600025E RID: 606 RVA: 0x000060DB File Offset: 0x000042DB
		public EventHook(string name, object target = null, object tag = null)
		{
			Ensure.That("name").IsNotNull(name);
			this.name = name;
			this.target = target;
			this.tag = tag;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00006104 File Offset: 0x00004304
		public override bool Equals(object obj)
		{
			if (obj is EventHook)
			{
				EventHook eventHook = (EventHook)obj;
				return this.Equals(eventHook);
			}
			return false;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000612B File Offset: 0x0000432B
		public bool Equals(EventHook other)
		{
			return this.name == other.name && object.Equals(this.target, other.target) && object.Equals(this.tag, other.tag);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00006166 File Offset: 0x00004366
		public override int GetHashCode()
		{
			return HashUtility.GetHashCode<string, object, object>(this.name, this.target, this.tag);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000617F File Offset: 0x0000437F
		public static bool operator ==(EventHook a, EventHook b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00006189 File Offset: 0x00004389
		public static bool operator !=(EventHook a, EventHook b)
		{
			return !(a == b);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00006195 File Offset: 0x00004395
		public static implicit operator EventHook(string name)
		{
			return new EventHook(name, null, null);
		}

		// Token: 0x0400006F RID: 111
		public readonly string name;

		// Token: 0x04000070 RID: 112
		public readonly object target;

		// Token: 0x04000071 RID: 113
		public readonly object tag;
	}
}
