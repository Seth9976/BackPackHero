using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Threading.Internal
{
	// Token: 0x02000002 RID: 2
	internal class UnityThreadUtilsInternal : IUnityThreadUtils, IServiceComponent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static Task PostAsync(Action action)
		{
			return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, UnityThreadUtils.UnityThreadScheduler);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public static Task PostAsync(Action<object> action, object state)
		{
			return Task.Factory.StartNew(action, state, CancellationToken.None, TaskCreationOptions.None, UnityThreadUtils.UnityThreadScheduler);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002081 File Offset: 0x00000281
		public static Task<T> PostAsync<T>(Func<T> action)
		{
			return Task<T>.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, UnityThreadUtils.UnityThreadScheduler);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002099 File Offset: 0x00000299
		public static Task<T> PostAsync<T>(Func<object, T> action, object state)
		{
			return Task<T>.Factory.StartNew(action, state, CancellationToken.None, TaskCreationOptions.None, UnityThreadUtils.UnityThreadScheduler);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B2 File Offset: 0x000002B2
		public static void Send(Action action)
		{
			if (UnityThreadUtils.IsRunningOnUnityThread)
			{
				action();
				return;
			}
			UnityThreadUtilsInternal.PostAsync(action).Wait();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020CD File Offset: 0x000002CD
		public static void Send(Action<object> action, object state)
		{
			if (UnityThreadUtils.IsRunningOnUnityThread)
			{
				action(state);
				return;
			}
			UnityThreadUtilsInternal.PostAsync(action, state).Wait();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EA File Offset: 0x000002EA
		public static T Send<T>(Func<T> action)
		{
			if (UnityThreadUtils.IsRunningOnUnityThread)
			{
				return action();
			}
			Task<T> task = UnityThreadUtilsInternal.PostAsync<T>(action);
			task.Wait();
			return task.Result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000210B File Offset: 0x0000030B
		public static T Send<T>(Func<object, T> action, object state)
		{
			if (UnityThreadUtils.IsRunningOnUnityThread)
			{
				return action(state);
			}
			Task<T> task = UnityThreadUtilsInternal.PostAsync<T>(action, state);
			task.Wait();
			return task.Result;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212E File Offset: 0x0000032E
		bool IUnityThreadUtils.IsRunningOnUnityThread
		{
			get
			{
				return UnityThreadUtils.IsRunningOnUnityThread;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002135 File Offset: 0x00000335
		Task IUnityThreadUtils.PostAsync(Action action)
		{
			return UnityThreadUtilsInternal.PostAsync(action);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000213D File Offset: 0x0000033D
		Task IUnityThreadUtils.PostAsync(Action<object> action, object state)
		{
			return UnityThreadUtilsInternal.PostAsync(action, state);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002146 File Offset: 0x00000346
		Task<T> IUnityThreadUtils.PostAsync<T>(Func<T> action)
		{
			return UnityThreadUtilsInternal.PostAsync<T>(action);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000214E File Offset: 0x0000034E
		Task<T> IUnityThreadUtils.PostAsync<T>(Func<object, T> action, object state)
		{
			return UnityThreadUtilsInternal.PostAsync<T>(action, state);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002157 File Offset: 0x00000357
		void IUnityThreadUtils.Send(Action action)
		{
			UnityThreadUtilsInternal.Send(action);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000215F File Offset: 0x0000035F
		void IUnityThreadUtils.Send(Action<object> action, object state)
		{
			UnityThreadUtilsInternal.Send(action, state);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002168 File Offset: 0x00000368
		T IUnityThreadUtils.Send<T>(Func<T> action)
		{
			return UnityThreadUtilsInternal.Send<T>(action);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002170 File Offset: 0x00000370
		T IUnityThreadUtils.Send<T>(Func<object, T> action, object state)
		{
			return UnityThreadUtilsInternal.Send<T>(action, state);
		}
	}
}
