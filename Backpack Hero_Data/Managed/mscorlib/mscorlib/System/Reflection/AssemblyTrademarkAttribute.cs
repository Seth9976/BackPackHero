using System;

namespace System.Reflection
{
	/// <summary>Defines a trademark custom attribute for an assembly manifest.</summary>
	// Token: 0x02000890 RID: 2192
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTrademarkAttribute" /> class.</summary>
		/// <param name="trademark">The trademark information. </param>
		// Token: 0x06004876 RID: 18550 RVA: 0x000EE174 File Offset: 0x000EC374
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.Trademark = trademark;
		}

		/// <summary>Gets trademark information.</summary>
		/// <returns>A String containing trademark information.</returns>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x000EE183 File Offset: 0x000EC383
		public string Trademark { get; }
	}
}
