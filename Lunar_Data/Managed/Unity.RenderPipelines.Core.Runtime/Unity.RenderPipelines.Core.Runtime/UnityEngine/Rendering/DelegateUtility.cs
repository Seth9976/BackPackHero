using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A8 RID: 168
	public static class DelegateUtility
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x0001B660 File Offset: 0x00019860
		public static Delegate Cast(Delegate source, Type type)
		{
			if (source == null)
			{
				return null;
			}
			Delegate[] invocationList = source.GetInvocationList();
			if (invocationList.Length == 1)
			{
				return Delegate.CreateDelegate(type, invocationList[0].Target, invocationList[0].Method);
			}
			Delegate[] array = new Delegate[invocationList.Length];
			for (int i = 0; i < invocationList.Length; i++)
			{
				array[i] = Delegate.CreateDelegate(type, invocationList[i].Target, invocationList[i].Method);
			}
			return Delegate.Combine(array);
		}
	}
}
