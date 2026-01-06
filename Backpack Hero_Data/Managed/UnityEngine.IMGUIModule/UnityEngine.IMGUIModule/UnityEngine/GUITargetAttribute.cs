using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(64)]
	public class GUITargetAttribute : Attribute
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000C06F File Offset: 0x0000A26F
		public GUITargetAttribute()
		{
			this.displayMask = -1;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000C080 File Offset: 0x0000A280
		public GUITargetAttribute(int displayIndex)
		{
			this.displayMask = 1 << displayIndex;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C096 File Offset: 0x0000A296
		public GUITargetAttribute(int displayIndex, int displayIndex1)
		{
			this.displayMask = (1 << displayIndex) | (1 << displayIndex1);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
		public GUITargetAttribute(int displayIndex, int displayIndex1, params int[] displayIndexList)
		{
			this.displayMask = (1 << displayIndex) | (1 << displayIndex1);
			for (int i = 0; i < displayIndexList.Length; i++)
			{
				this.displayMask |= 1 << displayIndexList[i];
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C104 File Offset: 0x0000A304
		[RequiredByNativeCode]
		private static int GetGUITargetAttrValue(Type klass, string methodName)
		{
			MethodInfo method = klass.GetMethod(methodName, 52);
			bool flag = method != null;
			if (flag)
			{
				object[] customAttributes = method.GetCustomAttributes(true);
				bool flag2 = customAttributes != null;
				if (flag2)
				{
					for (int i = 0; i < customAttributes.Length; i++)
					{
						bool flag3 = customAttributes[i].GetType() != typeof(GUITargetAttribute);
						if (!flag3)
						{
							GUITargetAttribute guitargetAttribute = customAttributes[i] as GUITargetAttribute;
							return guitargetAttribute.displayMask;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x040000E3 RID: 227
		internal int displayMask;
	}
}
