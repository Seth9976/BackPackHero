using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x020001A3 RID: 419
	public abstract class fsObjectProcessor
	{
		// Token: 0x06000B01 RID: 2817 RVA: 0x0002E843 File Offset: 0x0002CA43
		public virtual bool CanProcess(Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002E84A File Offset: 0x0002CA4A
		public virtual void OnBeforeSerialize(Type storageType, object instance)
		{
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002E84C File Offset: 0x0002CA4C
		public virtual void OnAfterSerialize(Type storageType, object instance, ref fsData data)
		{
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002E84E File Offset: 0x0002CA4E
		public virtual void OnBeforeDeserialize(Type storageType, ref fsData data)
		{
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002E850 File Offset: 0x0002CA50
		public virtual void OnBeforeDeserializeAfterInstanceCreation(Type storageType, object instance, ref fsData data)
		{
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002E852 File Offset: 0x0002CA52
		public virtual void OnAfterDeserialize(Type storageType, object instance)
		{
		}
	}
}
