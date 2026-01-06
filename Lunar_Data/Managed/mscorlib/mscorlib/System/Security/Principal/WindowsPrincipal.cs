using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Permissions;
using Mono;
using Unity;

namespace System.Security.Principal
{
	/// <summary>Enables code to check the Windows group membership of a Windows user.</summary>
	// Token: 0x020004EE RID: 1262
	[ComVisible(true)]
	[Serializable]
	public class WindowsPrincipal : ClaimsPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsPrincipal" /> class by using the specified <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <param name="ntIdentity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.WindowsPrincipal" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ntIdentity" /> is null. </exception>
		// Token: 0x06003278 RID: 12920 RVA: 0x000B996B File Offset: 0x000B7B6B
		public WindowsPrincipal(WindowsIdentity ntIdentity)
		{
			if (ntIdentity == null)
			{
				throw new ArgumentNullException("ntIdentity");
			}
			this._identity = ntIdentity;
		}

		/// <summary>Gets the identity of the current principal.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.WindowsIdentity" /> object of the current principal.</returns>
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000B9988 File Offset: 0x000B7B88
		public override IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified relative identifier (RID).</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group, that is, in a particular role; otherwise, false.</returns>
		/// <param name="rid">The RID of the Windows user group in which to check for the principal’s membership status. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600327A RID: 12922 RVA: 0x000B9990 File Offset: 0x000B7B90
		public virtual bool IsInRole(int rid)
		{
			if (Environment.IsUnix)
			{
				return WindowsPrincipal.IsMemberOfGroupId(this.Token, (IntPtr)rid);
			}
			string text;
			switch (rid)
			{
			case 544:
				text = "BUILTIN\\Administrators";
				break;
			case 545:
				text = "BUILTIN\\Users";
				break;
			case 546:
				text = "BUILTIN\\Guests";
				break;
			case 547:
				text = "BUILTIN\\Power Users";
				break;
			case 548:
				text = "BUILTIN\\Account Operators";
				break;
			case 549:
				text = "BUILTIN\\System Operators";
				break;
			case 550:
				text = "BUILTIN\\Print Operators";
				break;
			case 551:
				text = "BUILTIN\\Backup Operators";
				break;
			case 552:
				text = "BUILTIN\\Replicator";
				break;
			default:
				return false;
			}
			return this.IsInRole(text);
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified name.</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group; otherwise, false.</returns>
		/// <param name="role">The name of the Windows user group for which to check membership. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600327B RID: 12923 RVA: 0x000B9A3C File Offset: 0x000B7C3C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public override bool IsInRole(string role)
		{
			if (role == null)
			{
				return false;
			}
			if (Environment.IsUnix)
			{
				using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(role))
				{
					return WindowsPrincipal.IsMemberOfGroupName(this.Token, safeStringMarshal.Value);
				}
			}
			if (this.m_roles == null)
			{
				this.m_roles = WindowsIdentity._GetRoles(this.Token);
			}
			role = role.ToUpperInvariant();
			foreach (string text in this.m_roles)
			{
				if (text != null && role == text.ToUpperInvariant())
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified <see cref="T:System.Security.Principal.WindowsBuiltInRole" />.</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group; otherwise, false.</returns>
		/// <param name="role">One of the <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="role" /> is not a valid <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> value.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600327C RID: 12924 RVA: 0x000B9AE4 File Offset: 0x000B7CE4
		public virtual bool IsInRole(WindowsBuiltInRole role)
		{
			if (!Environment.IsUnix)
			{
				return this.IsInRole((int)role);
			}
			if (role == WindowsBuiltInRole.Administrator)
			{
				string text = "root";
				return this.IsInRole(text);
			}
			return false;
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified security identifier (SID).</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group; otherwise, false.</returns>
		/// <param name="sid">A <see cref="T:System.Security.Principal.SecurityIdentifier" />  that uniquely identifies a Windows user group.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sid" /> is null.</exception>
		/// <exception cref="T:System.Security.SecurityException">Windows returned a Win32 error.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600327D RID: 12925 RVA: 0x000479FC File Offset: 0x00045BFC
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public virtual bool IsInRole(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000B9B1B File Offset: 0x000B7D1B
		private IntPtr Token
		{
			get
			{
				return this._identity.Token;
			}
		}

		// Token: 0x0600327F RID: 12927
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupId(IntPtr user, IntPtr group);

		// Token: 0x06003280 RID: 12928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupName(IntPtr user, IntPtr group);

		/// <summary>Gets all Windows device claims from this principal.</summary>
		/// <returns>A collection of all Windows device claims from this principal.</returns>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000B98D3 File Offset: 0x000B7AD3
		public virtual IEnumerable<Claim> DeviceClaims
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets all Windows user claims from this principal.</summary>
		/// <returns>A collection of all Windows user claims from this principal.</returns>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000B98D3 File Offset: 0x000B7AD3
		public virtual IEnumerable<Claim> UserClaims
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x04002346 RID: 9030
		private WindowsIdentity _identity;

		// Token: 0x04002347 RID: 9031
		private string[] m_roles;
	}
}
