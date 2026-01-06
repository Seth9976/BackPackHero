using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x020002C5 RID: 709
	[Serializable]
	public class UnityEvent<T0> : UnityEventBase
	{
		// Token: 0x06001D9A RID: 7578 RVA: 0x00030014 File Offset: 0x0002E214
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00030025 File Offset: 0x0002E225
		public void AddListener(UnityAction<T0> call)
		{
			base.AddCall(UnityEvent<T0>.GetDelegate(call));
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0002FEFF File Offset: 0x0002E0FF
		public void RemoveListener(UnityAction<T0> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00030038 File Offset: 0x0002E238
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[] { typeof(T0) });
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00030064 File Offset: 0x0002E264
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0>(target, theFunction);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00030080 File Offset: 0x0002E280
		private static BaseInvokableCall GetDelegate(UnityAction<T0> action)
		{
			return new InvokableCall<T0>(action);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00030098 File Offset: 0x0002E298
		public void Invoke(T0 arg0)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0> invokableCall = list[i] as InvokableCall<T0>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0);
				}
				else
				{
					InvokableCall invokableCall2 = list[i] as InvokableCall;
					bool flag2 = invokableCall2 != null;
					if (flag2)
					{
						invokableCall2.Invoke();
					}
					else
					{
						BaseInvokableCall baseInvokableCall = list[i];
						bool flag3 = this.m_InvokeArray == null;
						if (flag3)
						{
							this.m_InvokeArray = new object[1];
						}
						this.m_InvokeArray[0] = arg0;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x040009AA RID: 2474
		private object[] m_InvokeArray = null;
	}
}
