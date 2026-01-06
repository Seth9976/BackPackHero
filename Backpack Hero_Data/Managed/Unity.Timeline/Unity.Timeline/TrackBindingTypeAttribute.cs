using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.Class)]
	public class TrackBindingTypeAttribute : Attribute
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x000099BB File Offset: 0x00007BBB
		public TrackBindingTypeAttribute(Type type)
		{
			this.type = type;
			this.flags = TrackBindingFlags.AllowCreateComponent;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000099D1 File Offset: 0x00007BD1
		public TrackBindingTypeAttribute(Type type, TrackBindingFlags flags)
		{
			this.type = type;
			this.flags = flags;
		}

		// Token: 0x040000EE RID: 238
		public readonly Type type;

		// Token: 0x040000EF RID: 239
		public readonly TrackBindingFlags flags;
	}
}
