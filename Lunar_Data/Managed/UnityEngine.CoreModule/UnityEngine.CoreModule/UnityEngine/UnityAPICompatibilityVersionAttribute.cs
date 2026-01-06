using System;

namespace UnityEngine
{
	// Token: 0x02000222 RID: 546
	[AttributeUsage(1, AllowMultiple = false)]
	public class UnityAPICompatibilityVersionAttribute : Attribute
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x00026096 File Offset: 0x00024296
		[Obsolete("This overload of the attribute has been deprecated. Use the constructor that takes the version and a boolean", true)]
		public UnityAPICompatibilityVersionAttribute(string version)
		{
			this._version = version;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x000260A8 File Offset: 0x000242A8
		public UnityAPICompatibilityVersionAttribute(string version, bool checkOnlyUnityVersion)
		{
			bool flag = !checkOnlyUnityVersion;
			if (flag)
			{
				throw new ArgumentException("You must pass 'true' to checkOnlyUnityVersion parameter.");
			}
			this._version = version;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000260D7 File Offset: 0x000242D7
		public UnityAPICompatibilityVersionAttribute(string version, string[] configurationAssembliesHashes)
		{
			this._version = version;
			this._configurationAssembliesHashes = configurationAssembliesHashes;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x000260F0 File Offset: 0x000242F0
		public string version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x00026108 File Offset: 0x00024308
		internal string[] configurationAssembliesHashes
		{
			get
			{
				return this._configurationAssembliesHashes;
			}
		}

		// Token: 0x04000810 RID: 2064
		private string _version;

		// Token: 0x04000811 RID: 2065
		private string[] _configurationAssembliesHashes;
	}
}
