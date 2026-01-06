using System;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Specifies when you can use a licensed object and provides a way of obtaining additional services needed to support licenses running within its domain.</summary>
	// Token: 0x020006DC RID: 1756
	public class LicenseContext : IServiceProvider
	{
		/// <summary>When overridden in a derived class, gets a value that specifies when you can use a license.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.LicenseUsageMode" /> values that specifies when you can use a license. The default is <see cref="F:System.ComponentModel.LicenseUsageMode.Runtime" />.</returns>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x00003062 File Offset: 0x00001262
		public virtual LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Runtime;
			}
		}

		/// <summary>When overridden in a derived class, returns a saved license key for the specified type, from the specified resource assembly.</summary>
		/// <returns>The <see cref="P:System.ComponentModel.License.LicenseKey" /> for the specified type. This method returns null unless you override it.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component. </param>
		/// <param name="resourceAssembly">An <see cref="T:System.Reflection.Assembly" /> with the license key. </param>
		// Token: 0x060037CB RID: 14283 RVA: 0x00002F6A File Offset: 0x0000116A
		public virtual string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		/// <summary>Gets the requested service, if it is available.</summary>
		/// <returns>An instance of the service, or null if the service cannot be found.</returns>
		/// <param name="type">The type of service to retrieve. </param>
		// Token: 0x060037CC RID: 14284 RVA: 0x00002F6A File Offset: 0x0000116A
		public virtual object GetService(Type type)
		{
			return null;
		}

		/// <summary>When overridden in a derived class, sets a license key for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component associated with the license key. </param>
		/// <param name="key">The <see cref="P:System.ComponentModel.License.LicenseKey" /> to save for the type of component. </param>
		// Token: 0x060037CD RID: 14285 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void SetSavedLicenseKey(Type type, string key)
		{
		}
	}
}
