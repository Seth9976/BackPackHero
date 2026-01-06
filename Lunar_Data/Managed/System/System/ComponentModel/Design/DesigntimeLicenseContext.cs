using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a design-time license context that can support a license provider at design time.</summary>
	// Token: 0x02000762 RID: 1890
	public class DesigntimeLicenseContext : LicenseContext
	{
		/// <summary>Gets the license usage mode.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseUsageMode" /> indicating the licensing mode for the context.</returns>
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x0000390E File Offset: 0x00001B0E
		public override LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Designtime;
			}
		}

		/// <summary>Gets a saved license key.</summary>
		/// <returns>The saved license key that matches the specified type.</returns>
		/// <param name="type">The type of the license key. </param>
		/// <param name="resourceAssembly">The assembly to get the key from. </param>
		// Token: 0x06003C5A RID: 15450 RVA: 0x00002F6A File Offset: 0x0000116A
		public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		/// <summary>Sets a saved license key.</summary>
		/// <param name="type">The type of the license key. </param>
		/// <param name="key">The license key. </param>
		// Token: 0x06003C5B RID: 15451 RVA: 0x000D8060 File Offset: 0x000D6260
		public override void SetSavedLicenseKey(Type type, string key)
		{
			this.savedLicenseKeys[type.AssemblyQualifiedName] = key;
		}

		// Token: 0x0400222B RID: 8747
		internal Hashtable savedLicenseKeys = new Hashtable();
	}
}
