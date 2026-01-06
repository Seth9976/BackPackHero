using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Controls access to Simple Mail Transport Protocol (SMTP) servers.</summary>
	// Token: 0x02000649 RID: 1609
	[Serializable]
	public sealed class SmtpPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class with the specified state.</summary>
		/// <param name="unrestricted">true if the new permission is unrestricted; otherwise, false.</param>
		// Token: 0x06003398 RID: 13208 RVA: 0x000BB321 File Offset: 0x000B9521
		public SmtpPermission(bool unrestricted)
		{
			this.unrestricted = unrestricted;
			this.access = (unrestricted ? SmtpAccess.ConnectToUnrestrictedPort : SmtpAccess.None);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class using the specified permission state value.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06003399 RID: 13209 RVA: 0x000BB33D File Offset: 0x000B953D
		public SmtpPermission(PermissionState state)
		{
			this.unrestricted = state == PermissionState.Unrestricted;
			this.access = (this.unrestricted ? SmtpAccess.ConnectToUnrestrictedPort : SmtpAccess.None);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class using the specified access level.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</param>
		// Token: 0x0600339A RID: 13210 RVA: 0x000BB361 File Offset: 0x000B9561
		public SmtpPermission(SmtpAccess access)
		{
			this.access = access;
		}

		/// <summary>Gets the level of access to SMTP servers controlled by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values. </returns>
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000BB370 File Offset: 0x000B9570
		public SmtpAccess Access
		{
			get
			{
				return this.access;
			}
		}

		/// <summary>Adds the specified access level value to the permission. </summary>
		/// <param name="access">One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</param>
		// Token: 0x0600339C RID: 13212 RVA: 0x000BB378 File Offset: 0x000B9578
		public void AddPermission(SmtpAccess access)
		{
			if (!this.unrestricted && access > this.access)
			{
				this.access = access;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission. </summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> that is identical to the current permission.</returns>
		// Token: 0x0600339D RID: 13213 RVA: 0x000BB392 File Offset: 0x000B9592
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.access);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> that represents the intersection of the current permission and the specified permission. Returns null if the intersection is empty or <paramref name="target" /> is null.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x0600339E RID: 13214 RVA: 0x000BB3B0 File Offset: 0x000B95B0
		public override IPermission Intersect(IPermission target)
		{
			SmtpPermission smtpPermission = this.Cast(target);
			if (smtpPermission == null)
			{
				return null;
			}
			if (this.unrestricted && smtpPermission.unrestricted)
			{
				return new SmtpPermission(true);
			}
			if (this.access > smtpPermission.access)
			{
				return new SmtpPermission(smtpPermission.access);
			}
			return new SmtpPermission(this.access);
		}

		/// <summary>Returns a value indicating whether the current permission is a subset of the specified permission. </summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x0600339F RID: 13215 RVA: 0x000BB408 File Offset: 0x000B9608
		public override bool IsSubsetOf(IPermission target)
		{
			SmtpPermission smtpPermission = this.Cast(target);
			if (smtpPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.unrestricted)
			{
				return smtpPermission.unrestricted;
			}
			return this.access <= smtpPermission.access;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x060033A0 RID: 13216 RVA: 0x000BB447 File Offset: 0x000B9647
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		/// <summary>Creates an XML encoding of the state of the permission. </summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the current permission.</returns>
		// Token: 0x060033A1 RID: 13217 RVA: 0x000BB450 File Offset: 0x000B9650
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(SmtpPermission), 1);
			if (this.unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				SmtpAccess smtpAccess = this.access;
				if (smtpAccess != SmtpAccess.Connect)
				{
					if (smtpAccess == SmtpAccess.ConnectToUnrestrictedPort)
					{
						securityElement.AddAttribute("Access", "ConnectToUnrestrictedPort");
					}
				}
				else
				{
					securityElement.AddAttribute("Access", "Connect");
				}
			}
			return securityElement;
		}

		/// <summary>Sets the state of the permission using the specified XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to set the state of the current permission.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> does not describe an <see cref="T:System.Net.Mail.SmtpPermission" /> object.-or-<paramref name="securityElement" /> does not contain the required state information to reconstruct the permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null.</exception>
		// Token: 0x060033A2 RID: 13218 RVA: 0x000BB4BC File Offset: 0x000B96BC
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			if (PermissionHelper.IsUnrestricted(securityElement))
			{
				this.access = SmtpAccess.Connect;
				return;
			}
			this.access = SmtpAccess.None;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission. </summary>
		/// <returns>A new <see cref="T:System.Net.Mail.SmtpPermission" /> permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to combine with the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x060033A3 RID: 13219 RVA: 0x000BB50C File Offset: 0x000B970C
		public override IPermission Union(IPermission target)
		{
			SmtpPermission smtpPermission = this.Cast(target);
			if (smtpPermission == null)
			{
				return this.Copy();
			}
			if (this.unrestricted || smtpPermission.unrestricted)
			{
				return new SmtpPermission(true);
			}
			if (this.access > smtpPermission.access)
			{
				return new SmtpPermission(this.access);
			}
			return new SmtpPermission(smtpPermission.access);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000BB567 File Offset: 0x000B9767
		private bool IsEmpty()
		{
			return !this.unrestricted && this.access == SmtpAccess.None;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000BB57C File Offset: 0x000B977C
		private SmtpPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SmtpPermission smtpPermission = target as SmtpPermission;
			if (smtpPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(SmtpPermission));
			}
			return smtpPermission;
		}

		// Token: 0x04001F68 RID: 8040
		private const int version = 1;

		// Token: 0x04001F69 RID: 8041
		private bool unrestricted;

		// Token: 0x04001F6A RID: 8042
		private SmtpAccess access;
	}
}
