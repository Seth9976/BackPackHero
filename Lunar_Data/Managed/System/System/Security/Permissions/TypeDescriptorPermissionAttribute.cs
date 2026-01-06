using System;
using Unity;

namespace System.Security.Permissions
{
	/// <summary>Determines the permission flags that apply to a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x02000878 RID: 2168
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class TypeDescriptorPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />. </summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x060044BD RID: 17597 RVA: 0x00003917 File Offset: 0x00001B17
		public TypeDescriptorPermissionAttribute(SecurityAction action)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />. </summary>
		/// <returns>The <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />.</returns>
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x000ECFD4 File Offset: 0x000EB1D4
		// (set) Token: 0x060044BF RID: 17599 RVA: 0x00013B26 File Offset: 0x00011D26
		public TypeDescriptorPermissionFlags Flags
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return TypeDescriptorPermissionFlags.NoFlags;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether the type descriptor can be accessed from partial trust. </summary>
		/// <returns>true if the type descriptor can be accessed from partial trust; otherwise, false. </returns>
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x060044C0 RID: 17600 RVA: 0x000ECFF0 File Offset: 0x000EB1F0
		// (set) Token: 0x060044C1 RID: 17601 RVA: 0x00013B26 File Offset: 0x00011D26
		public bool RestrictedRegistrationAccess
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <returns>A serializable permission object.</returns>
		// Token: 0x060044C2 RID: 17602 RVA: 0x000327E0 File Offset: 0x000309E0
		public override IPermission CreatePermission()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
