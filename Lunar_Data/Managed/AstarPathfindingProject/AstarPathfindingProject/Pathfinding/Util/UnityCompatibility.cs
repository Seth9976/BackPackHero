using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200025F RID: 607
	public static class UnityCompatibility
	{
		// Token: 0x06000E6E RID: 3694 RVA: 0x0005A2D6 File Offset: 0x000584D6
		public static T[] FindObjectsByTypeSorted<T>() where T : Object
		{
			return Object.FindObjectsByType<T>(FindObjectsSortMode.InstanceID);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0005A2DE File Offset: 0x000584DE
		public static T[] FindObjectsByTypeUnsorted<T>() where T : Object
		{
			return Object.FindObjectsByType<T>(FindObjectsSortMode.None);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0005A2E6 File Offset: 0x000584E6
		public static T[] FindObjectsByTypeUnsortedWithInactive<T>() where T : Object
		{
			return Object.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0005A2EF File Offset: 0x000584EF
		public static T FindAnyObjectByType<T>() where T : Object
		{
			return Object.FindAnyObjectByType<T>();
		}
	}
}
