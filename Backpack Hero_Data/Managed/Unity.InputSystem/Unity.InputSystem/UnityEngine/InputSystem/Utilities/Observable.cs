using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000137 RID: 311
	public static class Observable
	{
		// Token: 0x06001119 RID: 4377 RVA: 0x00051748 File Offset: 0x0004F948
		public static IObservable<TValue> Where<TValue>(this IObservable<TValue> source, Func<TValue, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new WhereObservable<TValue>(source, predicate);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0005176D File Offset: 0x0004F96D
		public static IObservable<TResult> Select<TSource, TResult>(this IObservable<TSource> source, Func<TSource, TResult> filter)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			return new SelectObservable<TSource, TResult>(source, filter);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00051792 File Offset: 0x0004F992
		public static IObservable<TResult> SelectMany<TSource, TResult>(this IObservable<TSource> source, Func<TSource, IEnumerable<TResult>> filter)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			return new SelectManyObservable<TSource, TResult>(source, filter);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000517B7 File Offset: 0x0004F9B7
		public static IObservable<TValue> Take<TValue>(this IObservable<TValue> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new TakeNObservable<TValue>(source, count);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000517DD File Offset: 0x0004F9DD
		public static IObservable<InputEventPtr> ForDevice(this IObservable<InputEventPtr> source, InputDevice device)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ForDeviceEventObservable(source, null, device);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000517F5 File Offset: 0x0004F9F5
		public static IObservable<InputEventPtr> ForDevice<TDevice>(this IObservable<InputEventPtr> source) where TDevice : InputDevice
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ForDeviceEventObservable(source, typeof(TDevice), null);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00051818 File Offset: 0x0004FA18
		public static IDisposable CallOnce<TValue>(this IObservable<TValue> source, Action<TValue> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			IDisposable subscription = null;
			subscription = source.Take(1).Subscribe(new Observer<TValue>(action, delegate
			{
				IDisposable subscription2 = subscription;
				if (subscription2 == null)
				{
					return;
				}
				subscription2.Dispose();
			}));
			return subscription;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00051878 File Offset: 0x0004FA78
		public static IDisposable Call<TValue>(this IObservable<TValue> source, Action<TValue> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return source.Subscribe(new Observer<TValue>(action, null));
		}
	}
}
