using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020007EF RID: 2031
	[Serializable]
	internal sealed class TreeSet<T> : SortedSet<T>
	{
		// Token: 0x060040C2 RID: 16578 RVA: 0x000E175E File Offset: 0x000DF95E
		public TreeSet()
		{
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x000E1766 File Offset: 0x000DF966
		public TreeSet(IComparer<T> comparer)
			: base(comparer)
		{
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x000E176F File Offset: 0x000DF96F
		public TreeSet(SerializationInfo siInfo, StreamingContext context)
			: base(siInfo, context)
		{
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x000E1779 File Offset: 0x000DF979
		internal override bool AddIfNotPresent(T item)
		{
			bool flag = base.AddIfNotPresent(item);
			if (!flag)
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", item));
			}
			return flag;
		}
	}
}
