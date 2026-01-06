using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to objects without direct manipulation of Access Control Lists (ACLs). This class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> and <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> classes.</summary>
	// Token: 0x02000541 RID: 1345
	public abstract class ObjectSecurity
	{
		// Token: 0x06003509 RID: 13577 RVA: 0x0000259F File Offset: 0x0000079F
		protected ObjectSecurity()
		{
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000C0564 File Offset: 0x000BE764
		protected ObjectSecurity(CommonSecurityDescriptor securityDescriptor)
		{
			if (securityDescriptor == null)
			{
				throw new ArgumentNullException("securityDescriptor");
			}
			this.descriptor = securityDescriptor;
			this.rw_lock = new ReaderWriterLock();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</param>
		/// <param name="isDS">True if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</param>
		// Token: 0x0600350B RID: 13579 RVA: 0x000C058C File Offset: 0x000BE78C
		protected ObjectSecurity(bool isContainer, bool isDS)
			: this(new CommonSecurityDescriptor(isContainer, isDS, ControlFlags.None, null, null, null, new DiscretionaryAcl(isContainer, isDS, 0)))
		{
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>The type of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600350C RID: 13580
		public abstract Type AccessRightType { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600350D RID: 13581
		public abstract Type AccessRuleType { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600350E RID: 13582
		public abstract Type AuditRuleType { get; }

		/// <summary>Gets a Boolean value that specifies whether the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object are in canonical order.</summary>
		/// <returns>true if the access rules are in canonical order; otherwise, false.</returns>
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x000C05B4 File Offset: 0x000BE7B4
		public bool AreAccessRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isDiscretionaryAclCanonical;
				try
				{
					isDiscretionaryAclCanonical = this.descriptor.IsDiscretionaryAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isDiscretionaryAclCanonical;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>true if the DACL is protected; otherwise, false.</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x000C05F0 File Offset: 0x000BE7F0
		public bool AreAccessRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this.descriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object are in canonical order.</summary>
		/// <returns>true if the audit rules are in canonical order; otherwise, false.</returns>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000C0634 File Offset: 0x000BE834
		public bool AreAuditRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isSystemAclCanonical;
				try
				{
					isSystemAclCanonical = this.descriptor.IsSystemAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isSystemAclCanonical;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>true if the SACL is protected; otherwise, false.</returns>
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000C0670 File Offset: 0x000BE870
		public bool AreAuditRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this.descriptor.ControlFlags & ControlFlags.SystemAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000C06B4 File Offset: 0x000BE8B4
		// (set) Token: 0x06003514 RID: 13588 RVA: 0x000C06C2 File Offset: 0x000BE8C2
		internal AccessControlSections AccessControlSectionsModified
		{
			get
			{
				this.Reading();
				return this.sections_modified;
			}
			set
			{
				this.Writing();
				this.sections_modified = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified.</summary>
		/// <returns>true if the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified; otherwise, false.</returns>
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000C06D1 File Offset: 0x000BE8D1
		// (set) Token: 0x06003516 RID: 13590 RVA: 0x000C06DA File Offset: 0x000BE8DA
		protected bool AccessRulesModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Access);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Access, value);
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified.</summary>
		/// <returns>true if the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified; otherwise, false.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000C06E4 File Offset: 0x000BE8E4
		// (set) Token: 0x06003518 RID: 13592 RVA: 0x000C06ED File Offset: 0x000BE8ED
		protected bool AuditRulesModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Audit);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Audit, value);
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the group associated with the securable object has been modified. </summary>
		/// <returns>true if the group associated with the securable object has been modified; otherwise, false.</returns>
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000C06F7 File Offset: 0x000BE8F7
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x000C0700 File Offset: 0x000BE900
		protected bool GroupModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Group);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Group, value);
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</summary>
		/// <returns>true if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object; otherwise, false.</returns>
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000C070A File Offset: 0x000BE90A
		protected bool IsContainer
		{
			get
			{
				return this.descriptor.IsContainer;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</summary>
		/// <returns>true if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object; otherwise, false.</returns>
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x000C0717 File Offset: 0x000BE917
		protected bool IsDS
		{
			get
			{
				return this.descriptor.IsDS;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the owner of the securable object has been modified.</summary>
		/// <returns>true if the owner of the securable object has been modified; otherwise, false.</returns>
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000C0724 File Offset: 0x000BE924
		// (set) Token: 0x0600351E RID: 13598 RVA: 0x000C072D File Offset: 0x000BE92D
		protected bool OwnerModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Owner);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Owner, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">Specifies the valid access control type.</param>
		// Token: 0x0600351F RID: 13599
		public abstract AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity to which the audit rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited audit rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">Specifies the conditions for which the rule is audited.</param>
		// Token: 0x06003520 RID: 13600
		public abstract AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags);

		/// <summary>Gets the primary group associated with the specified owner.</summary>
		/// <returns>The primary group associated with the specified owner.</returns>
		/// <param name="targetType">The owner for which to get the primary group. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003521 RID: 13601 RVA: 0x000C0738 File Offset: 0x000BE938
		public IdentityReference GetGroup(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this.descriptor.Group == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this.descriptor.Group.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		/// <summary>Gets the owner associated with the specified primary group.</summary>
		/// <returns>The owner associated with the specified group.</returns>
		/// <param name="targetType">The primary group for which to get the owner.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003522 RID: 13602 RVA: 0x000C0790 File Offset: 0x000BE990
		public IdentityReference GetOwner(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this.descriptor.Owner == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this.descriptor.Owner.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		/// <summary>Returns an array of byte values that represents the security descriptor information for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>An array of byte values that represents the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. This method returns null if there is no security information in this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x06003523 RID: 13603 RVA: 0x000C07E8 File Offset: 0x000BE9E8
		public byte[] GetSecurityDescriptorBinaryForm()
		{
			this.ReadLock();
			byte[] array2;
			try
			{
				byte[] array = new byte[this.descriptor.BinaryLength];
				this.descriptor.GetBinaryForm(array, 0);
				array2 = array;
			}
			finally
			{
				this.ReadUnlock();
			}
			return array2;
		}

		/// <summary>Returns the Security Descriptor Definition Language (SDDL) representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>The SDDL representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		/// <param name="includeSections">Specifies which sections (access rules, audit rules, primary group, owner) of the security descriptor to get.</param>
		// Token: 0x06003524 RID: 13604 RVA: 0x000C0838 File Offset: 0x000BEA38
		public string GetSecurityDescriptorSddlForm(AccessControlSections includeSections)
		{
			this.ReadLock();
			string sddlForm;
			try
			{
				sddlForm = this.descriptor.GetSddlForm(includeSections);
			}
			finally
			{
				this.ReadUnlock();
			}
			return sddlForm;
		}

		/// <summary>Returns a Boolean value that specifies whether the security descriptor associated with this  <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object can be converted to the Security Descriptor Definition Language (SDDL) format.</summary>
		/// <returns>true if the security descriptor associated with this  <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object can be converted to the Security Descriptor Definition Language (SDDL) format; otherwise, false.</returns>
		// Token: 0x06003525 RID: 13605 RVA: 0x000C0874 File Offset: 0x000BEA74
		public static bool IsSddlConversionSupported()
		{
			return GenericSecurityDescriptor.IsSddlConversionSupported();
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the DACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if the DACL is successfully modified; otherwise, false.</param>
		// Token: 0x06003526 RID: 13606 RVA: 0x000C087B File Offset: 0x000BEA7B
		public virtual bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AccessRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException("rule");
			}
			return this.ModifyAccess(modification, rule, out modified);
		}

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the SACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if the SACL is successfully modified; otherwise, false.</param>
		// Token: 0x06003527 RID: 13607 RVA: 0x000C08B2 File Offset: 0x000BEAB2
		public virtual bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AuditRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException("rule");
			}
			return this.ModifyAudit(modification, rule, out modified);
		}

		/// <summary>Removes all access rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all access rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All access rules are not in canonical order.</exception>
		// Token: 0x06003528 RID: 13608 RVA: 0x000C08EC File Offset: 0x000BEAEC
		public virtual void PurgeAccessRules(IdentityReference identity)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this.descriptor.PurgeAccessControl(ObjectSecurity.SidFromIR(identity));
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Removes all audit rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all audit rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All audit rules are not in canonical order.</exception>
		// Token: 0x06003529 RID: 13609 RVA: 0x000C0940 File Offset: 0x000BEB40
		public virtual void PurgeAuditRules(IdentityReference identity)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this.descriptor.PurgeAudit(ObjectSecurity.SidFromIR(identity));
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets or removes protection of the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. Protected access rules cannot be modified by parent objects through inheritance.</summary>
		/// <param name="isProtected">true to protect the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from inheritance; false to allow inheritance.</param>
		/// <param name="preserveInheritance">true to preserve inherited access rules; false to remove inherited access rules. This parameter is ignored if <paramref name="isProtected" /> is false.</param>
		/// <exception cref="T:System.InvalidOperationException">This method attempts to remove inherited rules from a non-canonical Discretionary Access Control List (DACL).</exception>
		// Token: 0x0600352A RID: 13610 RVA: 0x000C0994 File Offset: 0x000BEB94
		public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this.descriptor.SetDiscretionaryAclProtection(isProtected, preserveInheritance);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets or removes protection of the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. Protected audit rules cannot be modified by parent objects through inheritance.</summary>
		/// <param name="isProtected">true to protect the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from inheritance; false to allow inheritance.</param>
		/// <param name="preserveInheritance">true to preserve inherited audit rules; false to remove inherited audit rules. This parameter is ignored if <paramref name="isProtected" /> is false.</param>
		/// <exception cref="T:System.InvalidOperationException">This method attempts to remove inherited rules from a non-canonical System Access Control List (SACL).</exception>
		// Token: 0x0600352B RID: 13611 RVA: 0x000C09D0 File Offset: 0x000BEBD0
		public void SetAuditRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this.descriptor.SetSystemAclProtection(isProtected, preserveInheritance);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets the primary group for the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="identity">The primary group to set.</param>
		// Token: 0x0600352C RID: 13612 RVA: 0x000C0A0C File Offset: 0x000BEC0C
		public void SetGroup(IdentityReference identity)
		{
			this.WriteLock();
			try
			{
				this.descriptor.Group = ObjectSecurity.SidFromIR(identity);
				this.GroupModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets the owner for the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="identity">The owner to set.</param>
		// Token: 0x0600352D RID: 13613 RVA: 0x000C0A50 File Offset: 0x000BEC50
		public void SetOwner(IdentityReference identity)
		{
			this.WriteLock();
			try
			{
				this.descriptor.Owner = ObjectSecurity.SidFromIR(identity);
				this.OwnerModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of bytes from which to set the security descriptor.</param>
		// Token: 0x0600352E RID: 13614 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm)
		{
			this.SetSecurityDescriptorBinaryForm(binaryForm, AccessControlSections.All);
		}

		/// <summary>Sets the specified sections of the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of bytes from which to set the security descriptor.</param>
		/// <param name="includeSections">The sections (access rules, audit rules, owner, primary group) of the security descriptor to set.</param>
		// Token: 0x0600352F RID: 13615 RVA: 0x000C0A9F File Offset: 0x000BEC9F
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
		{
			this.CopySddlForm(new CommonSecurityDescriptor(this.IsContainer, this.IsDS, binaryForm, 0), includeSections);
		}

		/// <summary>Sets the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to set the security descriptor.</param>
		// Token: 0x06003530 RID: 13616 RVA: 0x000C0ABB File Offset: 0x000BECBB
		public void SetSecurityDescriptorSddlForm(string sddlForm)
		{
			this.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.All);
		}

		/// <summary>Sets the specified sections of the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to set the security descriptor.</param>
		/// <param name="includeSections">The sections (access rules, audit rules, owner, primary group) of the security descriptor to set.</param>
		// Token: 0x06003531 RID: 13617 RVA: 0x000C0AC6 File Offset: 0x000BECC6
		public void SetSecurityDescriptorSddlForm(string sddlForm, AccessControlSections includeSections)
		{
			this.CopySddlForm(new CommonSecurityDescriptor(this.IsContainer, this.IsDS, sddlForm), includeSections);
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000C0AE4 File Offset: 0x000BECE4
		private void CopySddlForm(CommonSecurityDescriptor sourceDescriptor, AccessControlSections includeSections)
		{
			this.WriteLock();
			try
			{
				this.AccessControlSectionsModified |= includeSections;
				if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
				{
					this.descriptor.SystemAcl = sourceDescriptor.SystemAcl;
				}
				if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
				{
					this.descriptor.DiscretionaryAcl = sourceDescriptor.DiscretionaryAcl;
				}
				if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
				{
					this.descriptor.Owner = sourceDescriptor.Owner;
				}
				if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
				{
					this.descriptor.Group = sourceDescriptor.Group;
				}
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the DACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if the DACL is successfully modified; otherwise, false.</param>
		// Token: 0x06003533 RID: 13619
		protected abstract bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified);

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the SACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if the SACL is successfully modified; otherwise, false.</param>
		// Token: 0x06003534 RID: 13620
		protected abstract bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified);

		// Token: 0x06003535 RID: 13621 RVA: 0x0004D855 File Offset: 0x0004BA55
		private Exception GetNotImplementedException()
		{
			return new NotImplementedException();
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="handle">The handle used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x06003536 RID: 13622 RVA: 0x000C0B78 File Offset: 0x000BED78
		protected virtual void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			throw this.GetNotImplementedException();
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="name">The name used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x06003537 RID: 13623 RVA: 0x000C0B78 File Offset: 0x000BED78
		protected virtual void Persist(string name, AccessControlSections includeSections)
		{
			throw this.GetNotImplementedException();
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="enableOwnershipPrivilege">true to enable the privilege that allows the caller to take ownership of the object.</param>
		/// <param name="name">The name used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x06003538 RID: 13624 RVA: 0x000479FC File Offset: 0x00045BFC
		[MonoTODO]
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Persist(bool enableOwnershipPrivilege, string name, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000C0B80 File Offset: 0x000BED80
		private void Reading()
		{
			if (!this.rw_lock.IsReaderLockHeld && !this.rw_lock.IsWriterLockHeld)
			{
				throw new InvalidOperationException("Either a read or a write lock must be held.");
			}
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x0600353A RID: 13626 RVA: 0x000C0BA7 File Offset: 0x000BEDA7
		protected void ReadLock()
		{
			this.rw_lock.AcquireReaderLock(-1);
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x0600353B RID: 13627 RVA: 0x000C0BB5 File Offset: 0x000BEDB5
		protected void ReadUnlock()
		{
			this.rw_lock.ReleaseReaderLock();
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000C0BC2 File Offset: 0x000BEDC2
		private void Writing()
		{
			if (!this.rw_lock.IsWriterLockHeld)
			{
				throw new InvalidOperationException("Write lock must be held.");
			}
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x0600353D RID: 13629 RVA: 0x000C0BDC File Offset: 0x000BEDDC
		protected void WriteLock()
		{
			this.rw_lock.AcquireWriterLock(-1);
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x0600353E RID: 13630 RVA: 0x000C0BEA File Offset: 0x000BEDEA
		protected void WriteUnlock()
		{
			this.rw_lock.ReleaseWriterLock();
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000C0BF8 File Offset: 0x000BEDF8
		internal AuthorizationRuleCollection InternalGetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			List<AuthorizationRule> list = new List<AuthorizationRule>();
			this.ReadLock();
			try
			{
				foreach (GenericAce genericAce in this.descriptor.DiscretionaryAcl)
				{
					QualifiedAce qualifiedAce = genericAce as QualifiedAce;
					if (!(null == qualifiedAce) && (!qualifiedAce.IsInherited || includeInherited) && (qualifiedAce.IsInherited || includeExplicit))
					{
						AccessControlType accessControlType;
						if (qualifiedAce.AceQualifier == AceQualifier.AccessAllowed)
						{
							accessControlType = AccessControlType.Allow;
						}
						else
						{
							if (AceQualifier.AccessDenied != qualifiedAce.AceQualifier)
							{
								continue;
							}
							accessControlType = AccessControlType.Deny;
						}
						AccessRule accessRule = this.InternalAccessRuleFactory(qualifiedAce, targetType, accessControlType);
						list.Add(accessRule);
					}
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return new AuthorizationRuleCollection(list.ToArray());
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000C0CA8 File Offset: 0x000BEEA8
		internal virtual AccessRule InternalAccessRuleFactory(QualifiedAce ace, Type targetType, AccessControlType type)
		{
			return this.AccessRuleFactory(ace.SecurityIdentifier.Translate(targetType), ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, type);
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000C0CD8 File Offset: 0x000BEED8
		internal AuthorizationRuleCollection InternalGetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			List<AuthorizationRule> list = new List<AuthorizationRule>();
			this.ReadLock();
			try
			{
				if (this.descriptor.SystemAcl != null)
				{
					foreach (GenericAce genericAce in this.descriptor.SystemAcl)
					{
						QualifiedAce qualifiedAce = genericAce as QualifiedAce;
						if (!(null == qualifiedAce) && (!qualifiedAce.IsInherited || includeInherited) && (qualifiedAce.IsInherited || includeExplicit) && AceQualifier.SystemAudit == qualifiedAce.AceQualifier)
						{
							AuditRule auditRule = this.InternalAuditRuleFactory(qualifiedAce, targetType);
							list.Add(auditRule);
						}
					}
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return new AuthorizationRuleCollection(list.ToArray());
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000C0D84 File Offset: 0x000BEF84
		internal virtual AuditRule InternalAuditRuleFactory(QualifiedAce ace, Type targetType)
		{
			return this.AuditRuleFactory(ace.SecurityIdentifier.Translate(targetType), ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, ace.AuditFlags);
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000C0DB6 File Offset: 0x000BEFB6
		internal static SecurityIdentifier SidFromIR(IdentityReference identity)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			return (SecurityIdentifier)identity.Translate(typeof(SecurityIdentifier));
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000C0DE1 File Offset: 0x000BEFE1
		private bool AreAccessControlSectionsModified(AccessControlSections mask)
		{
			return (this.AccessControlSectionsModified & mask) > AccessControlSections.None;
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000C0DEE File Offset: 0x000BEFEE
		private void SetAccessControlSectionsModified(AccessControlSections mask, bool modified)
		{
			if (modified)
			{
				this.AccessControlSectionsModified |= mask;
				return;
			}
			this.AccessControlSectionsModified &= ~mask;
		}

		// Token: 0x040024CD RID: 9421
		internal CommonSecurityDescriptor descriptor;

		// Token: 0x040024CE RID: 9422
		private AccessControlSections sections_modified;

		// Token: 0x040024CF RID: 9423
		private ReaderWriterLock rw_lock;
	}
}
