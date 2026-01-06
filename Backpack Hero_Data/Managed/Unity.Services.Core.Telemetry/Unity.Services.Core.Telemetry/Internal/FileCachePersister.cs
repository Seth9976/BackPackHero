using System;
using UnityEngine;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000003 RID: 3
	internal abstract class FileCachePersister
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000207F File Offset: 0x0000027F
		internal static bool IsAvailableFor(RuntimePlatform platform)
		{
			return !string.IsNullOrEmpty(FileCachePersister.GetPersistentDataPathFor(platform));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000208F File Offset: 0x0000028F
		internal static string GetPersistentDataPathFor(RuntimePlatform platform)
		{
			if (platform == RuntimePlatform.Switch)
			{
				return string.Empty;
			}
			return Application.persistentDataPath;
		}
	}
}
