using System;
using System.Collections.Generic;
using System.Security;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	internal class GUIStateObjects
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000B45C File Offset: 0x0000965C
		[SecuritySafeCritical]
		internal static object GetStateObject(Type t, int controlID)
		{
			object obj;
			bool flag = !GUIStateObjects.s_StateCache.TryGetValue(controlID, ref obj) || obj.GetType() != t;
			if (flag)
			{
				obj = Activator.CreateInstance(t);
				GUIStateObjects.s_StateCache[controlID] = obj;
			}
			return obj;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000B4A8 File Offset: 0x000096A8
		internal static object QueryStateObject(Type t, int controlID)
		{
			object obj = GUIStateObjects.s_StateCache[controlID];
			bool flag = t.IsInstanceOfType(obj);
			object obj2;
			if (flag)
			{
				obj2 = obj;
			}
			else
			{
				obj2 = null;
			}
			return obj2;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B4D7 File Offset: 0x000096D7
		internal static void Tests_ClearObjects()
		{
			GUIStateObjects.s_StateCache.Clear();
		}

		// Token: 0x040000C8 RID: 200
		private static Dictionary<int, object> s_StateCache = new Dictionary<int, object>();
	}
}
