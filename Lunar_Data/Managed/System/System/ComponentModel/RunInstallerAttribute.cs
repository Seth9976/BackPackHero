using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed.</summary>
	// Token: 0x020006FE RID: 1790
	[AttributeUsage(AttributeTargets.Class)]
	public class RunInstallerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunInstallerAttribute" /> class.</summary>
		/// <param name="runInstaller">true if an installer should be invoked during installation of an assembly; otherwise, false. </param>
		// Token: 0x06003952 RID: 14674 RVA: 0x000C8386 File Offset: 0x000C6586
		public RunInstallerAttribute(bool runInstaller)
		{
			this.RunInstaller = runInstaller;
		}

		/// <summary>Gets a value indicating whether an installer should be invoked during installation of an assembly.</summary>
		/// <returns>true if an installer should be invoked during installation of an assembly; otherwise, false.</returns>
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06003953 RID: 14675 RVA: 0x000C8395 File Offset: 0x000C6595
		public bool RunInstaller { get; }

		/// <summary>Determines whether the value of the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equivalent to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equal to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x06003954 RID: 14676 RVA: 0x000C83A0 File Offset: 0x000C65A0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RunInstallerAttribute runInstallerAttribute = obj as RunInstallerAttribute;
			return runInstallerAttribute != null && runInstallerAttribute.RunInstaller == this.RunInstaller;
		}

		/// <summary>Generates a hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</returns>
		// Token: 0x06003955 RID: 14677 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06003956 RID: 14678 RVA: 0x000C83CD File Offset: 0x000C65CD
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RunInstallerAttribute.Default);
		}

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed. This static field is read-only.</summary>
		// Token: 0x0400214E RID: 8526
		public static readonly RunInstallerAttribute Yes = new RunInstallerAttribute(true);

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should not be invoked when the assembly is installed. This static field is read-only.</summary>
		// Token: 0x0400214F RID: 8527
		public static readonly RunInstallerAttribute No = new RunInstallerAttribute(false);

		/// <summary>Specifies the default visiblity, which is <see cref="F:System.ComponentModel.RunInstallerAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04002150 RID: 8528
		public static readonly RunInstallerAttribute Default = RunInstallerAttribute.No;
	}
}
