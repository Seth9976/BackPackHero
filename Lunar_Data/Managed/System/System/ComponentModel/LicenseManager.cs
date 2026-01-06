using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides properties and methods to add a license to a component and to manage a <see cref="T:System.ComponentModel.LicenseProvider" />. This class cannot be inherited.</summary>
	// Token: 0x020006DD RID: 1757
	public sealed class LicenseManager
	{
		// Token: 0x060037CF RID: 14287 RVA: 0x0000219B File Offset: 0x0000039B
		private LicenseManager()
		{
		}

		/// <summary>Gets or sets the current <see cref="T:System.ComponentModel.LicenseContext" />, which specifies when you can use the licensed object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" /> property is currently locked and cannot be changed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000C3B64 File Offset: 0x000C1D64
		// (set) Token: 0x060037D1 RID: 14289 RVA: 0x000C3BC4 File Offset: 0x000C1DC4
		public static LicenseContext CurrentContext
		{
			get
			{
				if (LicenseManager.s_context == null)
				{
					object obj = LicenseManager.s_internalSyncObject;
					lock (obj)
					{
						if (LicenseManager.s_context == null)
						{
							LicenseManager.s_context = new RuntimeLicenseContext();
						}
					}
				}
				return LicenseManager.s_context;
			}
			set
			{
				object obj = LicenseManager.s_internalSyncObject;
				lock (obj)
				{
					if (LicenseManager.s_contextLockHolder != null)
					{
						throw new InvalidOperationException("The CurrentContext property of the LicenseManager is currently locked and cannot be changed.");
					}
					LicenseManager.s_context = value;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.LicenseUsageMode" /> which specifies when you can use the licensed object for the <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.LicenseUsageMode" /> values, as specified in the <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" /> property.</returns>
		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x000C3C18 File Offset: 0x000C1E18
		public static LicenseUsageMode UsageMode
		{
			get
			{
				if (LicenseManager.s_context != null)
				{
					return LicenseManager.s_context.UsageMode;
				}
				return LicenseUsageMode.Runtime;
			}
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000C3C34 File Offset: 0x000C1E34
		private static void CacheProvider(Type type, LicenseProvider provider)
		{
			if (LicenseManager.s_providers == null)
			{
				LicenseManager.s_providers = new Hashtable();
			}
			LicenseManager.s_providers[type] = provider;
			if (provider != null)
			{
				if (LicenseManager.s_providerInstances == null)
				{
					LicenseManager.s_providerInstances = new Hashtable();
				}
				LicenseManager.s_providerInstances[provider.GetType()] = provider;
			}
		}

		/// <summary>Creates an instance of the specified type, given a context in which you can use the licensed instance.</summary>
		/// <returns>An instance of the specified type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create. </param>
		/// <param name="creationContext">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed instance. </param>
		// Token: 0x060037D4 RID: 14292 RVA: 0x000C3C8F File Offset: 0x000C1E8F
		public static object CreateWithContext(Type type, LicenseContext creationContext)
		{
			return LicenseManager.CreateWithContext(type, creationContext, Array.Empty<object>());
		}

		/// <summary>Creates an instance of the specified type with the specified arguments, given a context in which you can use the licensed instance.</summary>
		/// <returns>An instance of the specified type with the given array of arguments.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create. </param>
		/// <param name="creationContext">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed instance. </param>
		/// <param name="args">An array of type <see cref="T:System.Object" /> that represents the arguments for the type. </param>
		// Token: 0x060037D5 RID: 14293 RVA: 0x000C3CA0 File Offset: 0x000C1EA0
		public static object CreateWithContext(Type type, LicenseContext creationContext, object[] args)
		{
			object obj = null;
			object obj2 = LicenseManager.s_internalSyncObject;
			lock (obj2)
			{
				LicenseContext currentContext = LicenseManager.CurrentContext;
				try
				{
					LicenseManager.CurrentContext = creationContext;
					LicenseManager.LockContext(LicenseManager.s_selfLock);
					try
					{
						obj = SecurityUtils.SecureCreateInstance(type, args);
					}
					catch (TargetInvocationException ex)
					{
						throw ex.InnerException;
					}
				}
				finally
				{
					LicenseManager.UnlockContext(LicenseManager.s_selfLock);
					LicenseManager.CurrentContext = currentContext;
				}
			}
			return obj;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000C3D2C File Offset: 0x000C1F2C
		private static bool GetCachedNoLicenseProvider(Type type)
		{
			return LicenseManager.s_providers != null && LicenseManager.s_providers.ContainsKey(type);
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000C3D46 File Offset: 0x000C1F46
		private static LicenseProvider GetCachedProvider(Type type)
		{
			Hashtable hashtable = LicenseManager.s_providers;
			return (LicenseProvider)((hashtable != null) ? hashtable[type] : null);
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000C3D61 File Offset: 0x000C1F61
		private static LicenseProvider GetCachedProviderInstance(Type providerType)
		{
			Hashtable hashtable = LicenseManager.s_providerInstances;
			return (LicenseProvider)((hashtable != null) ? hashtable[providerType] : null);
		}

		/// <summary>Returns whether the given type has a valid license.</summary>
		/// <returns>true if the given type is licensed; otherwise, false.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to find a valid license for. </param>
		// Token: 0x060037D9 RID: 14297 RVA: 0x000C3D7C File Offset: 0x000C1F7C
		public static bool IsLicensed(Type type)
		{
			License license;
			bool flag = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return flag;
		}

		/// <summary>Determines whether a valid license can be granted for the specified type.</summary>
		/// <returns>true if a valid license can be granted; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the <see cref="T:System.ComponentModel.License" />. </param>
		// Token: 0x060037DA RID: 14298 RVA: 0x000C3DA0 File Offset: 0x000C1FA0
		public static bool IsValid(Type type)
		{
			License license;
			bool flag = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return flag;
		}

		/// <summary>Determines whether a valid license can be granted for the specified instance of the type. This method creates a valid <see cref="T:System.ComponentModel.License" />.</summary>
		/// <returns>true if a valid <see cref="T:System.ComponentModel.License" /> can be granted; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license. </param>
		/// <param name="instance">An object of the specified type or a type derived from the specified type. </param>
		/// <param name="license">A <see cref="T:System.ComponentModel.License" /> that is a valid license, or null if a valid license cannot be granted. </param>
		// Token: 0x060037DB RID: 14299 RVA: 0x000C3DC2 File Offset: 0x000C1FC2
		public static bool IsValid(Type type, object instance, out License license)
		{
			return LicenseManager.ValidateInternal(type, instance, false, out license);
		}

		/// <summary>Prevents changes being made to the current <see cref="T:System.ComponentModel.LicenseContext" /> of the given object.</summary>
		/// <param name="contextUser">The object whose current context you want to lock. </param>
		/// <exception cref="T:System.InvalidOperationException">The context is already locked.</exception>
		// Token: 0x060037DC RID: 14300 RVA: 0x000C3DD0 File Offset: 0x000C1FD0
		public static void LockContext(object contextUser)
		{
			object obj = LicenseManager.s_internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.s_contextLockHolder != null)
				{
					throw new InvalidOperationException("The CurrentContext property of the LicenseManager is already locked by another user.");
				}
				LicenseManager.s_contextLockHolder = contextUser;
			}
		}

		/// <summary>Allows changes to be made to the current <see cref="T:System.ComponentModel.LicenseContext" /> of the given object.</summary>
		/// <param name="contextUser">The object whose current context you want to unlock. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contextUser" /> represents a different user than the one specified in a previous call to <see cref="M:System.ComponentModel.LicenseManager.LockContext(System.Object)" />. </exception>
		// Token: 0x060037DD RID: 14301 RVA: 0x000C3E24 File Offset: 0x000C2024
		public static void UnlockContext(object contextUser)
		{
			object obj = LicenseManager.s_internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.s_contextLockHolder != contextUser)
				{
					throw new ArgumentException("The CurrentContext property of the LicenseManager can only be unlocked with the same contextUser.");
				}
				LicenseManager.s_contextLockHolder = null;
			}
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000C3E78 File Offset: 0x000C2078
		private static bool ValidateInternal(Type type, object instance, bool allowExceptions, out License license)
		{
			string text;
			return LicenseManager.ValidateInternalRecursive(LicenseManager.CurrentContext, type, instance, allowExceptions, out license, out text);
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000C3E98 File Offset: 0x000C2098
		private static bool ValidateInternalRecursive(LicenseContext context, Type type, object instance, bool allowExceptions, out License license, out string licenseKey)
		{
			LicenseProvider licenseProvider = LicenseManager.GetCachedProvider(type);
			if (licenseProvider == null && !LicenseManager.GetCachedNoLicenseProvider(type))
			{
				LicenseProviderAttribute licenseProviderAttribute = (LicenseProviderAttribute)Attribute.GetCustomAttribute(type, typeof(LicenseProviderAttribute), false);
				if (licenseProviderAttribute != null)
				{
					Type licenseProvider2 = licenseProviderAttribute.LicenseProvider;
					licenseProvider = LicenseManager.GetCachedProviderInstance(licenseProvider2) ?? ((LicenseProvider)SecurityUtils.SecureCreateInstance(licenseProvider2));
				}
				LicenseManager.CacheProvider(type, licenseProvider);
			}
			license = null;
			bool flag = true;
			licenseKey = null;
			if (licenseProvider != null)
			{
				license = licenseProvider.GetLicense(context, type, instance, allowExceptions);
				if (license == null)
				{
					flag = false;
				}
				else
				{
					licenseKey = license.LicenseKey;
				}
			}
			if (flag && instance == null)
			{
				Type baseType = type.BaseType;
				if (baseType != typeof(object) && baseType != null)
				{
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
					string text;
					flag = LicenseManager.ValidateInternalRecursive(context, baseType, null, allowExceptions, out license, out text);
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
				}
			}
			return flag;
		}

		/// <summary>Determines whether a license can be granted for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license. </param>
		/// <exception cref="T:System.ComponentModel.LicenseException">A <see cref="T:System.ComponentModel.License" /> cannot be granted. </exception>
		// Token: 0x060037E0 RID: 14304 RVA: 0x000C3F80 File Offset: 0x000C2180
		public static void Validate(Type type)
		{
			License license;
			if (!LicenseManager.ValidateInternal(type, null, true, out license))
			{
				throw new LicenseException(type);
			}
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
		}

		/// <summary>Determines whether a license can be granted for the instance of the specified type.</summary>
		/// <returns>A valid <see cref="T:System.ComponentModel.License" />.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license. </param>
		/// <param name="instance">An <see cref="T:System.Object" /> of the specified type or a type derived from the specified type. </param>
		/// <exception cref="T:System.ComponentModel.LicenseException">The type is licensed, but a <see cref="T:System.ComponentModel.License" /> cannot be granted. </exception>
		// Token: 0x060037E1 RID: 14305 RVA: 0x000C3FAC File Offset: 0x000C21AC
		public static License Validate(Type type, object instance)
		{
			License license;
			if (!LicenseManager.ValidateInternal(type, instance, true, out license))
			{
				throw new LicenseException(type, instance);
			}
			return license;
		}

		// Token: 0x040020B8 RID: 8376
		private static readonly object s_selfLock = new object();

		// Token: 0x040020B9 RID: 8377
		private static volatile LicenseContext s_context;

		// Token: 0x040020BA RID: 8378
		private static object s_contextLockHolder;

		// Token: 0x040020BB RID: 8379
		private static volatile Hashtable s_providers;

		// Token: 0x040020BC RID: 8380
		private static volatile Hashtable s_providerInstances;

		// Token: 0x040020BD RID: 8381
		private static readonly object s_internalSyncObject = new object();
	}
}
