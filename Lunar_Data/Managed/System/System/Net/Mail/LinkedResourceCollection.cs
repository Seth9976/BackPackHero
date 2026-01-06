using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores linked resources to be sent as part of an e-mail message.</summary>
	// Token: 0x02000637 RID: 1591
	public sealed class LinkedResourceCollection : Collection<LinkedResource>, IDisposable
	{
		// Token: 0x060032ED RID: 13037 RVA: 0x000B8C3F File Offset: 0x000B6E3F
		internal LinkedResourceCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.LinkedResourceCollection" />.</summary>
		// Token: 0x060032EE RID: 13038 RVA: 0x000B8C47 File Offset: 0x000B6E47
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x00003917 File Offset: 0x00001B17
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000B8C56 File Offset: 0x000B6E56
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000B8C5E File Offset: 0x000B6E5E
		protected override void InsertItem(int index, LinkedResource item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000B8C68 File Offset: 0x000B6E68
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000B8C71 File Offset: 0x000B6E71
		protected override void SetItem(int index, LinkedResource item)
		{
			base.SetItem(index, item);
		}
	}
}
