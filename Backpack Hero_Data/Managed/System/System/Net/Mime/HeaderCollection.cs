using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200060B RID: 1547
	internal class HeaderCollection : NameValueCollection
	{
		// Token: 0x060031BD RID: 12733 RVA: 0x000B2338 File Offset: 0x000B0538
		internal HeaderCollection()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000B2348 File Offset: 0x000B0548
		public override void Remove(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType = null;
			}
			else if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition = null;
			}
			base.Remove(name);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000B23D4 File Offset: 0x000B05D4
		public override string Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.Get(name);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000B246C File Offset: 0x000B066C
		public override string[] GetValues(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.GetValues(name);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000B2504 File Offset: 0x000B0704
		internal void InternalRemove(string name)
		{
			base.Remove(name);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000B250D File Offset: 0x000B070D
		internal void InternalSet(string name, string value)
		{
			base.Set(name, value);
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000B2517 File Offset: 0x000B0717
		internal void InternalAdd(string name, string value)
		{
			if (MailHeaderInfo.IsSingleton(name))
			{
				base.Set(name, value);
				return;
			}
			base.Add(name, value);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000B2534 File Offset: 0x000B0734
		public override void Set(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "value"), "value");
			}
			if (!MimeBasePart.IsAscii(name, false))
			{
				throw new FormatException(SR.Format("An invalid character was found in header name.", Array.Empty<object>()));
			}
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			base.Set(name, value);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000B2648 File Offset: 0x000B0848
		public override void Add(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "name"), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "value"), "value");
			}
			MailBnfHelper.ValidateHeaderName(name);
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
			if (id == MailHeaderID.ContentType && this._part != null)
			{
				this._part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this._part is MimePart)
			{
				((MimePart)this._part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			this.InternalAdd(name, value);
		}

		// Token: 0x04001E5D RID: 7773
		private MimeBasePart _part;
	}
}
