using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.NetworkInformation
{
	/// <summary>Controls access to network information and traffic statistics for the local computer. This class cannot be inherited. </summary>
	// Token: 0x02000508 RID: 1288
	[Serializable]
	public sealed class NetworkInformationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x060029BF RID: 10687 RVA: 0x000997FE File Offset: 0x000979FE
		public NetworkInformationPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.access = NetworkInformationAccess.Read | NetworkInformationAccess.Ping;
				this.unrestricted = true;
				return;
			}
			this.access = NetworkInformationAccess.None;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00099820 File Offset: 0x00097A20
		internal NetworkInformationPermission(bool unrestricted)
		{
			if (unrestricted)
			{
				this.access = NetworkInformationAccess.Read | NetworkInformationAccess.Ping;
				unrestricted = true;
				return;
			}
			this.access = NetworkInformationAccess.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> class using the specified <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> value.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</param>
		// Token: 0x060029C1 RID: 10689 RVA: 0x0009983D File Offset: 0x00097A3D
		public NetworkInformationPermission(NetworkInformationAccess access)
		{
			this.access = access;
		}

		/// <summary>Gets the level of access to network information controlled by this permission. </summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</returns>
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x0009984C File Offset: 0x00097A4C
		public NetworkInformationAccess Access
		{
			get
			{
				return this.access;
			}
		}

		/// <summary>Adds the specified value to this permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</param>
		// Token: 0x060029C3 RID: 10691 RVA: 0x00099854 File Offset: 0x00097A54
		public void AddPermission(NetworkInformationAccess access)
		{
			this.access |= access;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x060029C4 RID: 10692 RVA: 0x00099864 File Offset: 0x00097A64
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		/// <summary>Creates and returns an identical copy of this permission.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> that is identical to the current permission</returns>
		// Token: 0x060029C5 RID: 10693 RVA: 0x0009986C File Offset: 0x00097A6C
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access);
		}

		/// <summary>Creates a permission that is the union of this permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" />  permission to combine with the current permission. </param>
		// Token: 0x060029C6 RID: 10694 RVA: 0x00099888 File Offset: 0x00097A88
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			NetworkInformationPermission networkInformationPermission = target as NetworkInformationPermission;
			if (networkInformationPermission == null)
			{
				throw new ArgumentException(SR.GetString("Cannot cast target permission type."), "target");
			}
			if (this.unrestricted || networkInformationPermission.IsUnrestricted())
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access | networkInformationPermission.access);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty or <paramref name="target" /> is null.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" />.</exception>
		// Token: 0x060029C7 RID: 10695 RVA: 0x000998E8 File Offset: 0x00097AE8
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			NetworkInformationPermission networkInformationPermission = target as NetworkInformationPermission;
			if (networkInformationPermission == null)
			{
				throw new ArgumentException(SR.GetString("Cannot cast target permission type."), "target");
			}
			if (this.unrestricted && networkInformationPermission.IsUnrestricted())
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access & networkInformationPermission.access);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> that is to be tested for the subset relationship. This permission must be of the same type as the current permission</param>
		// Token: 0x060029C8 RID: 10696 RVA: 0x00099944 File Offset: 0x00097B44
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.access == NetworkInformationAccess.None;
			}
			NetworkInformationPermission networkInformationPermission = target as NetworkInformationPermission;
			if (networkInformationPermission == null)
			{
				throw new ArgumentException(SR.GetString("Cannot cast target permission type."), "target");
			}
			return (!this.unrestricted || networkInformationPermission.IsUnrestricted()) && (this.access & networkInformationPermission.access) == this.access;
		}

		/// <summary>Sets the state of this permission using the specified XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to set the state of the current permission</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a permission encoding.-or-<paramref name="securityElement" /> is not an encoding of a <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" />. -or-<paramref name="securityElement" /> has invalid <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</exception>
		// Token: 0x060029C9 RID: 10697 RVA: 0x000999A8 File Offset: 0x00097BA8
		public override void FromXml(SecurityElement securityElement)
		{
			this.access = NetworkInformationAccess.None;
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("Specified value does not contain 'IPermission' as its tag."), "securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("Specified value does not contain a 'class' attribute."), "securityElement");
			}
			if (text.IndexOf(base.GetType().FullName) < 0)
			{
				throw new ArgumentException(SR.GetString("The value class attribute is not valid."), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = NetworkInformationAccess.Read | NetworkInformationAccess.Ping;
				this.unrestricted = true;
				return;
			}
			if (securityElement.Children != null)
			{
				foreach (object obj in securityElement.Children)
				{
					text2 = ((SecurityElement)obj).Attribute("Access");
					if (string.Compare(text2, "Read", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.access |= NetworkInformationAccess.Read;
					}
					else if (string.Compare(text2, "Ping", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.access |= NetworkInformationAccess.Ping;
					}
				}
			}
		}

		/// <summary>Creates an XML encoding of the state of this permission.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the current permission.</returns>
		// Token: 0x060029CA RID: 10698 RVA: 0x00099AFC File Offset: 0x00097CFC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
				return securityElement;
			}
			if ((this.access & NetworkInformationAccess.Read) > NetworkInformationAccess.None)
			{
				SecurityElement securityElement2 = new SecurityElement("NetworkInformationAccess");
				securityElement2.AddAttribute("Access", "Read");
				securityElement.AddChild(securityElement2);
			}
			if ((this.access & NetworkInformationAccess.Ping) > NetworkInformationAccess.None)
			{
				SecurityElement securityElement3 = new SecurityElement("NetworkInformationAccess");
				securityElement3.AddAttribute("Access", "Ping");
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		// Token: 0x04001866 RID: 6246
		private NetworkInformationAccess access;

		// Token: 0x04001867 RID: 6247
		private bool unrestricted;
	}
}
