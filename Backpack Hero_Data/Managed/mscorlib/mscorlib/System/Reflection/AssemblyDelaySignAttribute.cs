using System;

namespace System.Reflection
{
	/// <summary>Specifies that the assembly is not fully signed when created.</summary>
	// Token: 0x02000884 RID: 2180
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDelaySignAttribute" /> class.</summary>
		/// <param name="delaySign">true if the feature this attribute represents is activated; otherwise, false. </param>
		// Token: 0x0600485B RID: 18523 RVA: 0x000EE04B File Offset: 0x000EC24B
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.DelaySign = delaySign;
		}

		/// <summary>Gets a value indicating the state of the attribute.</summary>
		/// <returns>true if this assembly has been built as delay-signed; otherwise, false.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x000EE05A File Offset: 0x000EC25A
		public bool DelaySign { get; }
	}
}
