using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Represents a collection of <see cref="T:System.Net.Mail.AlternateView" /> objects.</summary>
	// Token: 0x02000630 RID: 1584
	public sealed class AlternateViewCollection : Collection<AlternateView>, IDisposable
	{
		// Token: 0x060032B5 RID: 12981 RVA: 0x000B64E8 File Offset: 0x000B46E8
		internal AlternateViewCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AlternateViewCollection" />.</summary>
		// Token: 0x060032B6 RID: 12982 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose()
		{
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000B64F0 File Offset: 0x000B46F0
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000B64F8 File Offset: 0x000B46F8
		protected override void InsertItem(int index, AlternateView item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000B6502 File Offset: 0x000B4702
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000B650B File Offset: 0x000B470B
		protected override void SetItem(int index, AlternateView item)
		{
			base.SetItem(index, item);
		}
	}
}
