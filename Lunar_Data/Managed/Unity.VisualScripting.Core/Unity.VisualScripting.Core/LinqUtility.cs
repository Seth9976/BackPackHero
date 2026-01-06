using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200015A RID: 346
	public static class LinqUtility
	{
		// Token: 0x06000920 RID: 2336 RVA: 0x00027BF8 File Offset: 0x00025DF8
		public static IEnumerable<T> Concat<T>(params IEnumerable[] enumerables)
		{
			foreach (IEnumerable enumerable in enumerables.NotNull<IEnumerable>())
			{
				foreach (T t in enumerable.OfType<T>())
				{
					yield return t;
				}
				IEnumerator<T> enumerator2 = null;
			}
			IEnumerator<IEnumerable> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00027C08 File Offset: 0x00025E08
		public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
		{
			return from x in items.GroupBy(property)
				select x.First<T>();
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00027C35 File Offset: 0x00025E35
		public static IEnumerable<T> NotNull<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.Where((T i) => i != null);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00027C5C File Offset: 0x00025E5C
		public static IEnumerable<T> Yield<T>(this T t)
		{
			yield return t;
			yield break;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00027C6C File Offset: 0x00025E6C
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
		{
			return new HashSet<T>(enumerable);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00027C74 File Offset: 0x00025E74
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				collection.Add(t);
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00027CBC File Offset: 0x00025EBC
		public static void AddRange(this IList list, IEnumerable items)
		{
			foreach (object obj in items)
			{
				list.Add(obj);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00027D0C File Offset: 0x00025F0C
		public static ICollection<T> AsReadOnlyCollection<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable is ICollection<T>)
			{
				return (ICollection<T>)enumerable;
			}
			return enumerable.ToList<T>().AsReadOnly();
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00027D28 File Offset: 0x00025F28
		public static IList<T> AsReadOnlyList<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable is IList<T>)
			{
				return (IList<T>)enumerable;
			}
			return enumerable.ToList<T>().AsReadOnly();
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00027D44 File Offset: 0x00025F44
		public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector)
		{
			IEnumerable<T> enumerable = source;
			foreach (T t in source)
			{
				enumerable = enumerable.Concat(childrenSelector(t).Flatten(childrenSelector));
			}
			return enumerable;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00027D9C File Offset: 0x00025F9C
		public static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> groups)
		{
			HashSet<T> hashSet = null;
			foreach (IEnumerable<T> enumerable in groups)
			{
				if (hashSet == null)
				{
					hashSet = new HashSet<T>(enumerable);
				}
				else
				{
					hashSet.IntersectWith(enumerable);
				}
			}
			if (hashSet != null)
			{
				return hashSet.AsEnumerable<T>();
			}
			return Enumerable.Empty<T>();
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00027E04 File Offset: 0x00026004
		public static IEnumerable<T> OrderByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, bool throwOnCycle = true)
		{
			List<T> list = new List<T>();
			HashSet<T> hashSet = HashSetPool<T>.New();
			foreach (T t in source)
			{
				LinqUtility.OrderByDependenciesVisit<T>(t, hashSet, list, getDependencies, throwOnCycle);
			}
			HashSetPool<T>.Free(hashSet);
			return list;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00027E60 File Offset: 0x00026060
		private static void OrderByDependenciesVisit<T>(T item, HashSet<T> visited, List<T> sorted, Func<T, IEnumerable<T>> getDependencies, bool throwOnCycle)
		{
			if (!visited.Contains(item))
			{
				visited.Add(item);
				foreach (T t in getDependencies(item))
				{
					LinqUtility.OrderByDependenciesVisit<T>(t, visited, sorted, getDependencies, throwOnCycle);
				}
				sorted.Add(item);
				return;
			}
			if (throwOnCycle && !sorted.Contains(item))
			{
				throw new InvalidOperationException("Cyclic dependency.");
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00027EE4 File Offset: 0x000260E4
		public static IEnumerable<T> OrderByDependers<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependers, bool throwOnCycle = true)
		{
			Dictionary<T, HashSet<T>> dependencies = new Dictionary<T, HashSet<T>>();
			foreach (T t in source)
			{
				foreach (T t2 in getDependers(t))
				{
					if (!dependencies.ContainsKey(t2))
					{
						dependencies.Add(t2, new HashSet<T>());
					}
					dependencies[t2].Add(t);
				}
			}
			return source.OrderByDependencies(delegate(T depender)
			{
				if (dependencies.ContainsKey(depender))
				{
					return dependencies[depender];
				}
				return Enumerable.Empty<T>();
			}, throwOnCycle);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00027FB8 File Offset: 0x000261B8
		public static IEnumerable<T> Catch<T>(this IEnumerable<T> source, Action<Exception> @catch)
		{
			Ensure.That("source").IsNotNull<IEnumerable<T>>(source);
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				bool success;
				do
				{
					try
					{
						success = enumerator.MoveNext();
					}
					catch (OperationCanceledException)
					{
						yield break;
					}
					catch (Exception ex)
					{
						if (@catch != null)
						{
							@catch(ex);
						}
						success = false;
					}
					if (success)
					{
						yield return enumerator.Current;
					}
				}
				while (success);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00027FCF File Offset: 0x000261CF
		public static IEnumerable<T> Catch<T>(this IEnumerable<T> source, ICollection<Exception> exceptions)
		{
			Ensure.That("exceptions").IsNotNull<ICollection<Exception>>(exceptions);
			return source.Catch(new Action<Exception>(exceptions.Add));
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00027FF4 File Offset: 0x000261F4
		public static IEnumerable<T> CatchAsLogError<T>(this IEnumerable<T> source, string message)
		{
			return source.Catch(delegate(Exception ex)
			{
				Debug.LogError(message + "\n" + ex.ToString());
			});
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00028020 File Offset: 0x00026220
		public static IEnumerable<T> CatchAsLogWarning<T>(this IEnumerable<T> source, string message)
		{
			return source.Catch(delegate(Exception ex)
			{
				Debug.LogWarning(message + "\n" + ex.ToString());
			});
		}
	}
}
