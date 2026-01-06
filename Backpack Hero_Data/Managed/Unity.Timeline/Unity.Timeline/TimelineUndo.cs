using System;
using System.Diagnostics;

namespace UnityEngine.Timeline
{
	// Token: 0x02000051 RID: 81
	internal static class TimelineUndo
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000A839 File Offset: 0x00008A39
		public static void PushDestroyUndo(TimelineAsset timeline, Object thingToDirty, Object objectToDestroy)
		{
			if (objectToDestroy != null)
			{
				Object.Destroy(objectToDestroy);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000A84A File Offset: 0x00008A4A
		[Conditional("UNITY_EDITOR")]
		public static void PushUndo(Object[] thingsToDirty, string operation)
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000A84C File Offset: 0x00008A4C
		[Conditional("UNITY_EDITOR")]
		public static void PushUndo(Object thingToDirty, string operation)
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000A84E File Offset: 0x00008A4E
		[Conditional("UNITY_EDITOR")]
		public static void RegisterCreatedObjectUndo(Object thingCreated, string operation)
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A850 File Offset: 0x00008A50
		private static string UndoName(string name)
		{
			return "Timeline " + name;
		}
	}
}
