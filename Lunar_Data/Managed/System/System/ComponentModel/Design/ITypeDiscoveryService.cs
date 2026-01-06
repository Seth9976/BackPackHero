using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Discovers available types at design time.</summary>
	// Token: 0x02000782 RID: 1922
	public interface ITypeDiscoveryService
	{
		/// <summary>Retrieves the list of available types.</summary>
		/// <returns>A collection of types that match the criteria specified by <paramref name="baseType" /> and <paramref name="excludeGlobalTypes" />.</returns>
		/// <param name="baseType">The base type to match. Can be null.</param>
		/// <param name="excludeGlobalTypes">Indicates whether types from all referenced assemblies should be checked.</param>
		// Token: 0x06003D00 RID: 15616
		ICollection GetTypes(Type baseType, bool excludeGlobalTypes);
	}
}
