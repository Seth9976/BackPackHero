using System;

namespace System.Reflection
{
	/// <summary>Provides a remotable version of the AssemblyName.</summary>
	// Token: 0x020008DB RID: 2267
	public class AssemblyNameProxy : MarshalByRefObject
	{
		/// <summary>Gets the AssemblyName for a given file.</summary>
		/// <returns>An AssemblyName object representing the given file.</returns>
		/// <param name="assemblyFile">The assembly file for which to get the AssemblyName. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is empty. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. </exception>
		// Token: 0x06004B82 RID: 19330 RVA: 0x000F08CD File Offset: 0x000EEACD
		public AssemblyName GetAssemblyName(string assemblyFile)
		{
			return AssemblyName.GetAssemblyName(assemblyFile);
		}
	}
}
