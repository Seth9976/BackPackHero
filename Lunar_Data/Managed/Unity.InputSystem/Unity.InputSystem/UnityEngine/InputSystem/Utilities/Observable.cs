using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000137 RID: 311
	public static class Observable
	{
		// Token: 0x06001112 RID: 4370 RVA: 0x00051534 File Offset: 0x0004F734
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

		// Token: 0x06001113 RID: 4371 RVA: 0x00051559 File Offset: 0x0004F759
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

		// Token: 0x06001114 RID: 4372 RVA: 0x0005157E File Offset: 0x0004F77E
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

		// Token: 0x06001115 RID: 4373 RVA: 0x000515A3 File Offset: 0x0004F7A3
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

		// Token: 0x06001116 RID: 4374 RVA: 0x000515C9 File Offset: 0x0004F7C9
		public static IObservable<InputEventPtr> ForDevice(this IObservable<InputEventPtr> source, InputDevice device)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ForDeviceEventObservable(source, null, device);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000515E1 File Offset: 0x0004F7E1
		public static IObservable<InputEventPtr> ForDevice<TDevice>(this IObservable<InputEventPtr> source) where TDevice : InputDevice
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ForDeviceEventObservable(source, typeof(TDevice), null);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00051604 File Offset: 0x0004F804
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

		// Token: 0x06001119 RID: 4377 RVA: 0x00051664 File Offset: 0x0004F864
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
