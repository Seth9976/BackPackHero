using System;
using System.Runtime.CompilerServices;

namespace System.Configuration
{
	// Token: 0x020001A7 RID: 423
	internal class DefaultConfig : IConfigurationSystem
	{
		// Token: 0x06000B15 RID: 2837 RVA: 0x0000219B File Offset: 0x0000039B
		private DefaultConfig()
		{
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002F336 File Offset: 0x0002D536
		public static DefaultConfig GetInstance()
		{
			return DefaultConfig.instance;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002F33D File Offset: 0x0002D53D
		[Obsolete("This method is obsolete.  Please use System.Configuration.ConfigurationManager.GetConfig")]
		public object GetConfig(string sectionName)
		{
			this.Init();
			return this.config.GetConfig(sectionName);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002F354 File Offset: 0x0002D554
		public void Init()
		{
			lock (this)
			{
				if (this.config == null)
				{
					ConfigurationData configurationData = new ConfigurationData();
					if (!configurationData.LoadString(DefaultConfig.GetBundledMachineConfig()) && !configurationData.Load(DefaultConfig.GetMachineConfigPath()))
					{
						throw new ConfigurationException("Cannot find " + DefaultConfig.GetMachineConfigPath());
					}
					string appConfigPath = DefaultConfig.GetAppConfigPath();
					if (appConfigPath == null)
					{
						this.config = configurationData;
					}
					else
					{
						ConfigurationData configurationData2 = new ConfigurationData(configurationData);
						if (configurationData2.Load(appConfigPath))
						{
							this.config = configurationData2;
						}
						else
						{
							this.config = configurationData;
						}
					}
				}
			}
		}

		// Token: 0x06000B19 RID: 2841
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002F400 File Offset: 0x0002D600
		internal static string GetBundledMachineConfig()
		{
			return DefaultConfig.get_bundled_machine_config();
		}

		// Token: 0x06000B1B RID: 2843
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_machine_config_path();

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002F407 File Offset: 0x0002D607
		internal static string GetMachineConfigPath()
		{
			return DefaultConfig.get_machine_config_path();
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002F410 File Offset: 0x0002D610
		private static string GetAppConfigPath()
		{
			string configurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			if (configurationFile == null || configurationFile.Length == 0)
			{
				return null;
			}
			return configurationFile;
		}

		// Token: 0x04000743 RID: 1859
		private static readonly DefaultConfig instance = new DefaultConfig();

		// Token: 0x04000744 RID: 1860
		private ConfigurationData config;
	}
}
