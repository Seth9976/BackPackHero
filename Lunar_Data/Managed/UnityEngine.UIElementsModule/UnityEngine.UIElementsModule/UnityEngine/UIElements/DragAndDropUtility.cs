using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A3 RID: 419
	internal static class DragAndDropUtility
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00038254 File Offset: 0x00036454
		public static IDragAndDrop dragAndDrop
		{
			get
			{
				bool flag = DragAndDropUtility.s_DragAndDrop == null;
				if (flag)
				{
					bool flag2 = DragAndDropUtility.s_MakeClientFunc != null;
					if (flag2)
					{
						DragAndDropUtility.s_DragAndDrop = DragAndDropUtility.s_MakeClientFunc.Invoke();
					}
					else
					{
						DragAndDropUtility.s_DragAndDrop = new DefaultDragAndDropClient();
					}
				}
				return DragAndDropUtility.s_DragAndDrop;
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000382A0 File Offset: 0x000364A0
		internal static void RegisterMakeClientFunc(Func<IDragAndDrop> makeClient)
		{
			bool flag = DragAndDropUtility.s_MakeClientFunc != null;
			if (flag)
			{
				throw new UnityException("The MakeClientFunc has already been registered. Registration denied.");
			}
			DragAndDropUtility.s_MakeClientFunc = makeClient;
		}

		// Token: 0x04000649 RID: 1609
		private static Func<IDragAndDrop> s_MakeClientFunc;

		// Token: 0x0400064A RID: 1610
		private static IDragAndDrop s_DragAndDrop;
	}
}
