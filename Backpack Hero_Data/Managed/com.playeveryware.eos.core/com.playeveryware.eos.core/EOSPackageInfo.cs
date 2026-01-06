using System;

// Token: 0x02000004 RID: 4
public static class EOSPackageInfo
{
	// Token: 0x06000006 RID: 6 RVA: 0x0000210F File Offset: 0x0000030F
	public static string GetPackageName()
	{
		return "com.playeveryware.eos";
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002116 File Offset: 0x00000316
	public static string GetPackageVersion()
	{
		return "3.0.2";
	}

	// Token: 0x04000002 RID: 2
	public static readonly string ConfigFileName = "EpicOnlineServicesConfig.json";

	// Token: 0x04000003 RID: 3
	public const string UnknownVersion = "?.?.?";
}
