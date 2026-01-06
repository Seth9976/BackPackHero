using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200007F RID: 127
	public static class XGraphEventListener
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x00009360 File Offset: 0x00007560
		public static void StartListening(this IGraphEventListener listener, GraphReference reference)
		{
			using (GraphStack graphStack = reference.ToStackPooled())
			{
				listener.StartListening(graphStack);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00009398 File Offset: 0x00007598
		public static void StopListening(this IGraphEventListener listener, GraphReference reference)
		{
			using (GraphStack graphStack = reference.ToStackPooled())
			{
				listener.StopListening(graphStack);
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000093D0 File Offset: 0x000075D0
		public static bool IsHierarchyListening(GraphReference reference)
		{
			bool flag;
			using (GraphStack graphStack = reference.ToStackPooled())
			{
				while (graphStack.isChild)
				{
					IGraphParent parent = graphStack.parent;
					graphStack.ExitParentElement();
					IGraphEventListener graphEventListener = parent as IGraphEventListener;
					if (graphEventListener != null && !graphEventListener.IsListening(graphStack))
					{
						return false;
					}
				}
				IGraphEventListener graphEventListener2 = graphStack.graph as IGraphEventListener;
				if (graphEventListener2 != null && !graphEventListener2.IsListening(graphStack))
				{
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			return flag;
		}
	}
}
