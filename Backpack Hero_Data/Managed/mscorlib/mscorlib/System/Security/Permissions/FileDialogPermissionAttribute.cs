using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.FileDialogPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000437 RID: 1079
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileDialogPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06002BC0 RID: 11200 RVA: 0x0009DCC8 File Offset: 0x0009BEC8
		public FileDialogPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a value indicating whether permission to open files through the file dialog is declared.</summary>
		/// <returns>true if permission to open files through the file dialog is declared; otherwise, false.</returns>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x0009DF88 File Offset: 0x0009C188
		// (set) Token: 0x06002BC2 RID: 11202 RVA: 0x0009DF90 File Offset: 0x0009C190
		public bool Open
		{
			get
			{
				return this.canOpen;
			}
			set
			{
				this.canOpen = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to save files through the file dialog is declared.</summary>
		/// <returns>true if permission to save files through the file dialog is declared; otherwise, false.</returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x0009DF99 File Offset: 0x0009C199
		// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x0009DFA1 File Offset: 0x0009C1A1
		public bool Save
		{
			get
			{
				return this.canSave;
			}
			set
			{
				this.canSave = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.FileDialogPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.FileDialogPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002BC5 RID: 11205 RVA: 0x0009DFAC File Offset: 0x0009C1AC
		public override IPermission CreatePermission()
		{
			FileDialogPermission fileDialogPermission;
			if (base.Unrestricted)
			{
				fileDialogPermission = new FileDialogPermission(PermissionState.Unrestricted);
			}
			else
			{
				FileDialogPermissionAccess fileDialogPermissionAccess = FileDialogPermissionAccess.None;
				if (this.canOpen)
				{
					fileDialogPermissionAccess |= FileDialogPermissionAccess.Open;
				}
				if (this.canSave)
				{
					fileDialogPermissionAccess |= FileDialogPermissionAccess.Save;
				}
				fileDialogPermission = new FileDialogPermission(fileDialogPermissionAccess);
			}
			return fileDialogPermission;
		}

		// Token: 0x04002011 RID: 8209
		private bool canOpen;

		// Token: 0x04002012 RID: 8210
		private bool canSave;
	}
}
