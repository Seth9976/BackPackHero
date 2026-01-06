using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000129 RID: 297
	internal static class DelegateHelpers
	{
		// Token: 0x0600107F RID: 4223 RVA: 0x0004E960 File Offset: 0x0004CB60
		public static void InvokeCallbacksSafe(ref CallbackArray<Action> callbacks, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i]();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0004E9FC File Offset: 0x0004CBFC
		public static void InvokeCallbacksSafe<TValue>(ref CallbackArray<Action<TValue>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i](argument);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0004EA9C File Offset: 0x0004CC9C
		public static void InvokeCallbacksSafe<TValue1, TValue2>(ref CallbackArray<Action<TValue1, TValue2>> callbacks, TValue1 argument1, TValue2 argument2, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i](argument1, argument2);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0004EB3C File Offset: 0x0004CD3C
		public static bool InvokeCallbacksSafe_AnyCallbackReturnsTrue<TValue1, TValue2>(ref CallbackArray<Func<TValue1, TValue2, bool>> callbacks, TValue1 argument1, TValue2 argument2, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return true;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					if (callbacks[i](argument1, argument2))
					{
						callbacks.UnlockForChanges();
						return true;
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
			return false;
		}
	}
}
