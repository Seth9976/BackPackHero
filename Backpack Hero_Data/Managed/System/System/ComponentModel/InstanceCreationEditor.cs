using System;

namespace System.ComponentModel
{
	/// <summary>Creates an instance of a particular type of property from a drop-down box within the <see cref="T:System.Windows.Forms.PropertyGrid" />. </summary>
	// Token: 0x020006D4 RID: 1748
	public abstract class InstanceCreationEditor
	{
		/// <summary>Gets the specified text.</summary>
		/// <returns>The specified text.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000C3928 File Offset: 0x000C1B28
		public virtual string Text
		{
			get
			{
				return "(New...)";
			}
		}

		/// <summary>When overridden in a derived class, returns an instance of the specified type.</summary>
		/// <returns>An instance of the specified type or null.</returns>
		/// <param name="context">The context information.</param>
		/// <param name="instanceType">The specified type.</param>
		// Token: 0x060037AB RID: 14251
		public abstract object CreateInstance(ITypeDescriptorContext context, Type instanceType);
	}
}
