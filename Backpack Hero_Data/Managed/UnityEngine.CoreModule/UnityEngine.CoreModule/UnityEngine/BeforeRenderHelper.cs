using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine
{
	// Token: 0x0200011E RID: 286
	internal static class BeforeRenderHelper
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		private static int GetUpdateOrder(UnityAction callback)
		{
			object[] customAttributes = callback.Method.GetCustomAttributes(typeof(BeforeRenderOrderAttribute), true);
			BeforeRenderOrderAttribute beforeRenderOrderAttribute = ((customAttributes != null && customAttributes.Length != 0) ? (customAttributes[0] as BeforeRenderOrderAttribute) : null);
			return (beforeRenderOrderAttribute != null) ? beforeRenderOrderAttribute.order : 0;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0000BB30 File Offset: 0x00009D30
		public static void RegisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag)
					{
						BeforeRenderHelper.OrderBlock orderBlock = BeforeRenderHelper.s_OrderBlocks[num];
						orderBlock.callback = (UnityAction)Delegate.Combine(orderBlock.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = orderBlock;
						return;
					}
					num++;
				}
				BeforeRenderHelper.OrderBlock orderBlock2 = default(BeforeRenderHelper.OrderBlock);
				orderBlock2.order = updateOrder;
				orderBlock2.callback = (UnityAction)Delegate.Combine(orderBlock2.callback, callback);
				BeforeRenderHelper.s_OrderBlocks.Insert(num, orderBlock2);
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000BC24 File Offset: 0x00009E24
		public static void UnregisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag)
					{
						BeforeRenderHelper.OrderBlock orderBlock = BeforeRenderHelper.s_OrderBlocks[num];
						orderBlock.callback = (UnityAction)Delegate.Remove(orderBlock.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = orderBlock;
						bool flag2 = orderBlock.callback == null;
						if (flag2)
						{
							BeforeRenderHelper.s_OrderBlocks.RemoveAt(num);
						}
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000BD04 File Offset: 0x00009F04
		public static void Invoke()
		{
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				for (int i = 0; i < BeforeRenderHelper.s_OrderBlocks.Count; i++)
				{
					UnityAction callback = BeforeRenderHelper.s_OrderBlocks[i].callback;
					bool flag = callback != null;
					if (flag)
					{
						callback();
					}
				}
			}
		}

		// Token: 0x040003A0 RID: 928
		private static List<BeforeRenderHelper.OrderBlock> s_OrderBlocks = new List<BeforeRenderHelper.OrderBlock>();

		// Token: 0x0200011F RID: 287
		private struct OrderBlock
		{
			// Token: 0x040003A1 RID: 929
			internal int order;

			// Token: 0x040003A2 RID: 930
			internal UnityAction callback;
		}
	}
}
