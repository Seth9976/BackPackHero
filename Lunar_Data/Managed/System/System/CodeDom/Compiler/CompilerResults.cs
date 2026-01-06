using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the results of compilation that are returned from a compiler.</summary>
	// Token: 0x0200034D RID: 845
	[Serializable]
	public class CompilerResults
	{
		/// <summary>Indicates the evidence object that represents the security policy permissions of the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object that represents the security policy permissions of the compiled assembly.</returns>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x000666C4 File Offset: 0x000648C4
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x000666D7 File Offset: 0x000648D7
		[Obsolete("CAS policy is obsolete and will be removed in a future release of the .NET Framework. Please see http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public Evidence Evidence
		{
			get
			{
				Evidence evidence = this._evidence;
				if (evidence == null)
				{
					return null;
				}
				return evidence.Clone();
			}
			set
			{
				this._evidence = ((value != null) ? value.Clone() : null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerResults" /> class that uses the specified temporary files.</summary>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		// Token: 0x06001BFC RID: 7164 RVA: 0x000666EB File Offset: 0x000648EB
		public CompilerResults(TempFileCollection tempFiles)
		{
			this._tempFiles = tempFiles;
		}

		/// <summary>Gets or sets the temporary file collection to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</returns>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00066710 File Offset: 0x00064910
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x00066718 File Offset: 0x00064918
		public TempFileCollection TempFiles
		{
			get
			{
				return this._tempFiles;
			}
			set
			{
				this._tempFiles = value;
			}
		}

		/// <summary>Gets or sets the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> that indicates the compiled assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00066721 File Offset: 0x00064921
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x0006675B File Offset: 0x0006495B
		public Assembly CompiledAssembly
		{
			get
			{
				if (this._compiledAssembly == null && this.PathToAssembly != null)
				{
					this._compiledAssembly = Assembly.Load(new AssemblyName
					{
						CodeBase = this.PathToAssembly
					});
				}
				return this._compiledAssembly;
			}
			set
			{
				this._compiledAssembly = value;
			}
		}

		/// <summary>Gets the collection of compiler errors and warnings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> that indicates the errors and warnings resulting from compilation, if any.</returns>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x00066764 File Offset: 0x00064964
		public CompilerErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Gets the compiler output messages.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the output messages.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0006676C File Offset: 0x0006496C
		public StringCollection Output
		{
			get
			{
				return this._output;
			}
		}

		/// <summary>Gets or sets the path of the compiled assembly.</summary>
		/// <returns>The path of the assembly, or null if the assembly was generated in memory.</returns>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00066774 File Offset: 0x00064974
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0006677C File Offset: 0x0006497C
		public string PathToAssembly { get; set; }

		/// <summary>Gets or sets the compiler's return value.</summary>
		/// <returns>The compiler's return value.</returns>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x00066785 File Offset: 0x00064985
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0006678D File Offset: 0x0006498D
		public int NativeCompilerReturnValue { get; set; }

		// Token: 0x04000E44 RID: 3652
		private Evidence _evidence;

		// Token: 0x04000E45 RID: 3653
		private readonly CompilerErrorCollection _errors = new CompilerErrorCollection();

		// Token: 0x04000E46 RID: 3654
		private readonly StringCollection _output = new StringCollection();

		// Token: 0x04000E47 RID: 3655
		private Assembly _compiledAssembly;

		// Token: 0x04000E48 RID: 3656
		private TempFileCollection _tempFiles;
	}
}
