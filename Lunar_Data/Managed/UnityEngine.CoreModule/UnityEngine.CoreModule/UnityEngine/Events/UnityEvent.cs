using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x020002C3 RID: 707
	[Serializable]
	public class UnityEvent : UnityEventBase
	{
		// Token: 0x06001D8F RID: 7567 RVA: 0x0002FEDE File Offset: 0x0002E0DE
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0002FEEF File Offset: 0x0002E0EF
		public void AddListener(UnityAction call)
		{
			base.AddCall(UnityEvent.GetDelegate(call));
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0002FEFF File Offset: 0x0002E0FF
		public void RemoveListener(UnityAction call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0002FF18 File Offset: 0x0002E118
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[0]);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0002FF38 File Offset: 0x0002E138
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall(target, theFunction);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0002FF54 File Offset: 0x0002E154
		private static BaseInvokableCall GetDelegate(UnityAction action)
		{
			return new InvokableCall(action);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0002FF6C File Offset: 0x0002E16C
		public void Invoke()
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall invokableCall = list[i] as InvokableCall;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke();
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
							this.m_InvokeArray = new object[0];
						}
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x040009A9 RID: 2473
		private object[] m_InvokeArray = null;
	}
}
