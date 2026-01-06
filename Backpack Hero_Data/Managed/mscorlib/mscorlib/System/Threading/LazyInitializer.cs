using System;

namespace System.Threading
{
	/// <summary>Provides lazy initialization routines.</summary>
	// Token: 0x0200028E RID: 654
	public static class LazyInitializer
	{
		/// <summary>Initializes a target reference type with the type's default constructor if it hasn't already been initialized.</summary>
		/// <returns>The initialized reference of type <paramref name="T" />.</returns>
		/// <param name="target">A reference of type <paramref name="T" /> to initialize if it has not already been initialized.</param>
		/// <typeparam name="T">The type of the reference to be initialized.</typeparam>
		/// <exception cref="T:System.MemberAccessException">Permissions to access the constructor of type <paramref name="T" /> were missing.</exception>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		// Token: 0x06001D7B RID: 7547 RVA: 0x0006E412 File Offset: 0x0006C612
		public static T EnsureInitialized<T>(ref T target) where T : class
		{
			T t;
			if ((t = Volatile.Read<T>(ref target)) == null)
			{
				t = LazyInitializer.EnsureInitializedCore<T>(ref target);
			}
			return t;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0006E42C File Offset: 0x0006C62C
		private static T EnsureInitializedCore<T>(ref T target) where T : class
		{
			try
			{
				Interlocked.CompareExchange<T>(ref target, Activator.CreateInstance<T>(), default(T));
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException("The lazily-initialized type does not have a public, parameterless constructor.");
			}
			return target;
		}

		/// <summary>Initializes a target reference type by using a specified function if it hasn't already been initialized.</summary>
		/// <returns>The initialized value of type <paramref name="T" />.</returns>
		/// <param name="target">The reference of type <paramref name="T" /> to initialize if it hasn't already been initialized.</param>
		/// <param name="valueFactory">The function that is called to initialize the reference.</param>
		/// <typeparam name="T">The reference type of the reference to be initialized.</typeparam>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="valueFactory" /> returned null (Nothing in Visual Basic).</exception>
		// Token: 0x06001D7D RID: 7549 RVA: 0x0006E474 File Offset: 0x0006C674
		public static T EnsureInitialized<T>(ref T target, Func<T> valueFactory) where T : class
		{
			T t;
			if ((t = Volatile.Read<T>(ref target)) == null)
			{
				t = LazyInitializer.EnsureInitializedCore<T>(ref target, valueFactory);
			}
			return t;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0006E48C File Offset: 0x0006C68C
		private static T EnsureInitializedCore<T>(ref T target, Func<T> valueFactory) where T : class
		{
			T t = valueFactory();
			if (t == null)
			{
				throw new InvalidOperationException("ValueFactory returned null.");
			}
			Interlocked.CompareExchange<T>(ref target, t, default(T));
			return target;
		}

		/// <summary>Initializes a target reference or value type with its default constructor if it hasn't already been initialized.</summary>
		/// <returns>The initialized value of type <paramref name="T" />.</returns>
		/// <param name="target">A reference or value of type <paramref name="T" /> to initialize if it hasn't already been initialized.</param>
		/// <param name="initialized">A reference to a Boolean value that determines whether the target has already been initialized.</param>
		/// <param name="syncLock">A reference to an object used as the mutually exclusive lock for initializing <paramref name="target" />. If <paramref name="syncLock" /> is null, a new object will be instantiated.</param>
		/// <typeparam name="T">The type of the reference to be initialized.</typeparam>
		/// <exception cref="T:System.MemberAccessException">Permissions to access the constructor of type <paramref name="T" /> were missing.</exception>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		// Token: 0x06001D7F RID: 7551 RVA: 0x0006E4CA File Offset: 0x0006C6CA
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0006E4E4 File Offset: 0x0006C6E4
		private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock)
		{
			object obj = LazyInitializer.EnsureLockInitialized(ref syncLock);
			lock (obj)
			{
				if (!Volatile.Read(ref initialized))
				{
					try
					{
						target = Activator.CreateInstance<T>();
					}
					catch (MissingMethodException)
					{
						throw new MissingMemberException("The lazily-initialized type does not have a public, parameterless constructor.");
					}
					Volatile.Write(ref initialized, true);
				}
			}
			return target;
		}

		/// <summary>Initializes a target reference or value type by using a specified function if it hasn't already been initialized.</summary>
		/// <returns>The initialized value of type <paramref name="T" />.</returns>
		/// <param name="target">A reference or value of type <paramref name="T" /> to initialize if it hasn't already been initialized.</param>
		/// <param name="initialized">A reference to a Boolean value that determines whether the target has already been initialized.</param>
		/// <param name="syncLock">A reference to an object used as the mutually exclusive lock for initializing <paramref name="target" />. If <paramref name="syncLock" /> is null, a new object will be instantiated.</param>
		/// <param name="valueFactory">The function that is called to initialize the reference or value.</param>
		/// <typeparam name="T">The type of the reference to be initialized.</typeparam>
		/// <exception cref="T:System.MemberAccessException">Permissions to access the constructor of type <paramref name="T" /> were missing.</exception>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		// Token: 0x06001D81 RID: 7553 RVA: 0x0006E558 File Offset: 0x0006C758
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, valueFactory);
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0006E574 File Offset: 0x0006C774
		private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			object obj = LazyInitializer.EnsureLockInitialized(ref syncLock);
			lock (obj)
			{
				if (!Volatile.Read(ref initialized))
				{
					target = valueFactory();
					Volatile.Write(ref initialized, true);
				}
			}
			return target;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0006E5D0 File Offset: 0x0006C7D0
		public static T EnsureInitialized<T>(ref T target, ref object syncLock, Func<T> valueFactory) where T : class
		{
			T t;
			if ((t = Volatile.Read<T>(ref target)) == null)
			{
				t = LazyInitializer.EnsureInitializedCore<T>(ref target, ref syncLock, valueFactory);
			}
			return t;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0006E5EC File Offset: 0x0006C7EC
		private static T EnsureInitializedCore<T>(ref T target, ref object syncLock, Func<T> valueFactory) where T : class
		{
			object obj = LazyInitializer.EnsureLockInitialized(ref syncLock);
			lock (obj)
			{
				if (Volatile.Read<T>(ref target) == null)
				{
					Volatile.Write<T>(ref target, valueFactory());
					if (target == null)
					{
						throw new InvalidOperationException("ValueFactory returned null.");
					}
				}
			}
			return target;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0006E660 File Offset: 0x0006C860
		private static object EnsureLockInitialized(ref object syncLock)
		{
			object obj;
			if ((obj = syncLock) == null)
			{
				obj = Interlocked.CompareExchange(ref syncLock, new object(), null) ?? syncLock;
			}
			return obj;
		}
	}
}
