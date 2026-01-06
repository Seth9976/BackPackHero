using System;

namespace System.Net
{
	/// <summary>Provides the base interface to load and execute scripts for automatic proxy detection.</summary>
	// Token: 0x020004B0 RID: 1200
	public interface IWebProxyScript
	{
		/// <summary>Closes a script.</summary>
		// Token: 0x060026BB RID: 9915
		void Close();

		/// <summary>Loads a script.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> indicating whether the script was successfully loaded.</returns>
		/// <param name="scriptLocation">Internal only.</param>
		/// <param name="script">Internal only.</param>
		/// <param name="helperType">Internal only.</param>
		// Token: 0x060026BC RID: 9916
		bool Load(Uri scriptLocation, string script, Type helperType);

		/// <summary>Runs a script.</summary>
		/// <returns>A <see cref="T:System.String" />.An internal-only value returned.</returns>
		/// <param name="url">Internal only.</param>
		/// <param name="host">Internal only.</param>
		// Token: 0x060026BD RID: 9917
		string Run(string url, string host);
	}
}
