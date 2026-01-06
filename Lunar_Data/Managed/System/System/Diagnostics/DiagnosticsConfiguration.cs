using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000218 RID: 536
	internal static class DiagnosticsConfiguration
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00045320 File Offset: 0x00043520
		internal static SwitchElementsCollection SwitchSettings
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.Switches;
				}
				return null;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00045348 File Offset: 0x00043548
		internal static bool AssertUIEnabled
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Assert == null || systemDiagnosticsSection.Assert.AssertUIEnabled;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0004537C File Offset: 0x0004357C
		internal static string ConfigFilePath
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.ElementInformation.Source;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x000453AC File Offset: 0x000435AC
		internal static string LogFileName
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Assert != null)
				{
					return systemDiagnosticsSection.Assert.LogFileName;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x000453E4 File Offset: 0x000435E4
		internal static bool AutoFlush
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null && systemDiagnosticsSection.Trace.AutoFlush;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00045418 File Offset: 0x00043618
		internal static bool UseGlobalLock
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Trace == null || systemDiagnosticsSection.Trace.UseGlobalLock;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0004544C File Offset: 0x0004364C
		internal static int IndentSize
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null)
				{
					return systemDiagnosticsSection.Trace.IndentSize;
				}
				return 4;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00045480 File Offset: 0x00043680
		internal static ListenerElementsCollection SharedListeners
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.SharedListeners;
				}
				return null;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x000454A8 File Offset: 0x000436A8
		internal static SourceElementsCollection Sources
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Sources != null)
				{
					return systemDiagnosticsSection.Sources;
				}
				return null;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x000454D5 File Offset: 0x000436D5
		internal static SystemDiagnosticsSection SystemDiagnosticsSection
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				return DiagnosticsConfiguration.configSection;
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000454E4 File Offset: 0x000436E4
		private static SystemDiagnosticsSection GetConfigSection()
		{
			object section = PrivilegedConfigurationManager.GetSection("system.diagnostics");
			if (section is SystemDiagnosticsSection)
			{
				return (SystemDiagnosticsSection)section;
			}
			return null;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0004550C File Offset: 0x0004370C
		internal static bool IsInitializing()
		{
			return DiagnosticsConfiguration.initState == InitState.Initializing;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00045518 File Offset: 0x00043718
		internal static bool IsInitialized()
		{
			return DiagnosticsConfiguration.initState == InitState.Initialized;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00045524 File Offset: 0x00043724
		internal static bool CanInitialize()
		{
			return DiagnosticsConfiguration.initState != InitState.Initializing && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0004553C File Offset: 0x0004373C
		internal static void Initialize()
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				if (DiagnosticsConfiguration.initState == InitState.NotInitialized && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress)
				{
					DiagnosticsConfiguration.initState = InitState.Initializing;
					try
					{
						DiagnosticsConfiguration.configSection = DiagnosticsConfiguration.GetConfigSection();
					}
					finally
					{
						DiagnosticsConfiguration.initState = InitState.Initialized;
					}
				}
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000455B0 File Offset: 0x000437B0
		internal static void Refresh()
		{
			ConfigurationManager.RefreshSection("system.diagnostics");
			SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
			if (systemDiagnosticsSection != null)
			{
				if (systemDiagnosticsSection.Switches != null)
				{
					foreach (object obj in systemDiagnosticsSection.Switches)
					{
						((SwitchElement)obj).ResetProperties();
					}
				}
				if (systemDiagnosticsSection.SharedListeners != null)
				{
					foreach (object obj2 in systemDiagnosticsSection.SharedListeners)
					{
						((ListenerElement)obj2).ResetProperties();
					}
				}
				if (systemDiagnosticsSection.Sources != null)
				{
					foreach (object obj3 in systemDiagnosticsSection.Sources)
					{
						((SourceElement)obj3).ResetProperties();
					}
				}
			}
			DiagnosticsConfiguration.configSection = null;
			DiagnosticsConfiguration.initState = InitState.NotInitialized;
			DiagnosticsConfiguration.Initialize();
		}

		// Token: 0x04000997 RID: 2455
		private static volatile SystemDiagnosticsSection configSection;

		// Token: 0x04000998 RID: 2456
		private static volatile InitState initState;
	}
}
