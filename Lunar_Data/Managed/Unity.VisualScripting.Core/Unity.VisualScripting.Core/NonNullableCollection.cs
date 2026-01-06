using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Unity.VisualScripting
{
	// Token: 0x0200001E RID: 30
	public abstract class NonNullableCollection<T> : Collection<T>
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x0000397C File Offset: 0x00001B7C
		protected override void InsertItem(int index, T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003999 File Offset: 0x00001B99
		protected override void SetItem(int index, T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000039B8 File Offset: 0x00001BB8
		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				base.Add(t);
			}
		}
	}
}
