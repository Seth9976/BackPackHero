using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores attachments to be sent as part of an e-mail message.</summary>
	// Token: 0x02000634 RID: 1588
	public sealed class AttachmentCollection : Collection<Attachment>, IDisposable
	{
		// Token: 0x060032DC RID: 13020 RVA: 0x000B8AF2 File Offset: 0x000B6CF2
		internal AttachmentCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AttachmentCollection" />. </summary>
		// Token: 0x060032DD RID: 13021 RVA: 0x000B8AFC File Offset: 0x000B6CFC
		public void Dispose()
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Dispose();
			}
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000B8B26 File Offset: 0x000B6D26
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000B8B2E File Offset: 0x000B6D2E
		protected override void InsertItem(int index, Attachment item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000B8B38 File Offset: 0x000B6D38
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000B8B41 File Offset: 0x000B6D41
		protected override void SetItem(int index, Attachment item)
		{
			base.SetItem(index, item);
		}
	}
}
