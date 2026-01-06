using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Store e-mail addresses that are associated with an e-mail message.</summary>
	// Token: 0x0200062B RID: 1579
	public class MailAddressCollection : Collection<MailAddress>
	{
		/// <summary>Add a list of e-mail addresses to the collection.</summary>
		/// <param name="addresses">The e-mail addresses to add to the <see cref="T:System.Net.Mail.MailAddressCollection" />. Multiple e-mail addresses must be separated with a comma character (","). </param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" addresses" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The<paramref name=" addresses" /> parameter is an empty string.</exception>
		/// <exception cref="T:System.FormatException">The<paramref name=" addresses" /> parameter contains an e-mail address that is invalid or not supported. </exception>
		// Token: 0x0600329A RID: 12954 RVA: 0x000B5D30 File Offset: 0x000B3F30
		public void Add(string addresses)
		{
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (addresses == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "addresses"), "addresses");
			}
			this.ParseValue(addresses);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The index of the e-mail address element to be replaced.</param>
		/// <param name="item">An e-mail address that will replace the element in the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" item" /> parameter is null.</exception>
		// Token: 0x0600329B RID: 12955 RVA: 0x000B5D6E File Offset: 0x000B3F6E
		protected override void SetItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		/// <summary>Inserts an e-mail address into the <see cref="T:System.Net.Mail.MailAddressCollection" />, at the specified location.</summary>
		/// <param name="index">The location at which to insert the e-mail address that is specified by <paramref name="item" />.</param>
		/// <param name="item">The e-mail address to be inserted into the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" item" /> parameter is null.</exception>
		// Token: 0x0600329C RID: 12956 RVA: 0x000B5D86 File Offset: 0x000B3F86
		protected override void InsertItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000B5DA0 File Offset: 0x000B3FA0
		internal void ParseValue(string addresses)
		{
			IList<MailAddress> list = MailAddressParser.ParseMultipleAddresses(addresses);
			for (int i = 0; i < list.Count; i++)
			{
				base.Add(list[i]);
			}
		}

		/// <summary>Returns a string representation of the e-mail addresses in this <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the e-mail addresses in this collection.</returns>
		// Token: 0x0600329E RID: 12958 RVA: 0x000B5DD4 File Offset: 0x000B3FD4
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MailAddress mailAddress in this)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(mailAddress.ToString());
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000B5E44 File Offset: 0x000B4044
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			foreach (MailAddress mailAddress in this)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = mailAddress.Encode(charsConsumed, allowUnicode);
				}
				else
				{
					text = text + ", " + mailAddress.Encode(1, allowUnicode);
				}
			}
			return text;
		}
	}
}
