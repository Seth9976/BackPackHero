using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000129 RID: 297
	internal static class DelegateHelpers
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x0004E9AC File Offset: 0x0004CBAC
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

		// Token: 0x06001085 RID: 4229 RVA: 0x0004EA48 File Offset: 0x0004CC48
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

		// Token: 0x06001086 RID: 4230 RVA: 0x0004EAE8 File Offset: 0x0004CCE8
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

		// Token: 0x06001087 RID: 4231 RVA: 0x0004EB88 File Offset: 0x0004CD88
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

		// Token: 0x06001088 RID: 4232 RVA: 0x0004EC38 File Offset: 0x0004CE38
		public static void InvokeCallbacksSafe_AndInvokeReturnedActions<TValue>(ref CallbackArray<Func<TValue, Action>> callbacks, TValue argument, string callbackName, object context = null)
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
					Action action = callbacks[i](argument);
					if (action != null)
					{
						action();
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
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0004ECE0 File Offset: 0x0004CEE0
		public static bool InvokeCallbacksSafe_AnyCallbackReturnsObject<TValue, TReturn>(ref CallbackArray<Func<TValue, TReturn>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return false;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					if (callbacks[i](argument) != null)
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
