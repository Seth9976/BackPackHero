using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System
{
	/// <summary>Controls the system garbage collector, a service that automatically reclaims unused memory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FF RID: 511
	public static class GC
	{
		// Token: 0x060015EC RID: 5612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCollectionCount(int generation);

		// Token: 0x060015ED RID: 5613
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxGeneration();

		// Token: 0x060015EE RID: 5614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalCollect(int generation);

		// Token: 0x060015EF RID: 5615
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecordPressure(long bytesAllocated);

		// Token: 0x060015F0 RID: 5616
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void register_ephemeron_array(Ephemeron[] array);

		// Token: 0x060015F1 RID: 5617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object get_ephemeron_tombstone();

		// Token: 0x060015F2 RID: 5618 RVA: 0x00056B69 File Offset: 0x00054D69
		internal static void GetMemoryInfo(out uint highMemLoadThreshold, out ulong totalPhysicalMem, out uint lastRecordedMemLoad, out UIntPtr lastRecordedHeapSize, out UIntPtr lastRecordedFragmentation)
		{
			highMemLoadThreshold = 0U;
			totalPhysicalMem = ulong.MaxValue;
			lastRecordedMemLoad = 0U;
			lastRecordedHeapSize = UIntPtr.Zero;
			lastRecordedFragmentation = UIntPtr.Zero;
		}

		// Token: 0x060015F3 RID: 5619
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetAllocatedBytesForCurrentThread();

		/// <summary>Informs the runtime of a large allocation of unmanaged memory that should be taken into account when scheduling garbage collection.</summary>
		/// <param name="bytesAllocated">The incremental amount of unmanaged memory that has been allocated. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bytesAllocated" /> is less than or equal to 0.-or-On a 32-bit computer, <paramref name="bytesAllocated" /> is larger than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060015F4 RID: 5620 RVA: 0x00056B84 File Offset: 0x00054D84
		[SecurityCritical]
		public static void AddMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Positive number required."));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("pressure", Environment.GetResourceString("Value must be non-negative and less than or equal to Int32.MaxValue."));
			}
			GC.RecordPressure(bytesAllocated);
		}

		/// <summary>Informs the runtime that unmanaged memory has been released and no longer needs to be taken into account when scheduling garbage collection.</summary>
		/// <param name="bytesAllocated">The amount of unmanaged memory that has been released. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bytesAllocated" /> is less than or equal to 0. -or- On a 32-bit computer, <paramref name="bytesAllocated" /> is larger than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060015F5 RID: 5621 RVA: 0x00056BD8 File Offset: 0x00054DD8
		[SecurityCritical]
		public static void RemoveMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Positive number required."));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Value must be non-negative and less than or equal to Int32.MaxValue."));
			}
			GC.RecordPressure(-bytesAllocated);
		}

		/// <summary>Returns the current generation number of the specified object.</summary>
		/// <returns>The current generation number of <paramref name="obj" />.</returns>
		/// <param name="obj">The object that generation information is retrieved for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F6 RID: 5622
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetGeneration(object obj);

		/// <summary>Forces an immediate garbage collection from generation 0 through a specified generation.</summary>
		/// <param name="generation">The number of the oldest generation that garbage collection can be performed on. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F7 RID: 5623 RVA: 0x00056C2C File Offset: 0x00054E2C
		public static void Collect(int generation)
		{
			GC.Collect(generation, GCCollectionMode.Default);
		}

		/// <summary>Forces an immediate garbage collection of all generations. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F8 RID: 5624 RVA: 0x00056C35 File Offset: 0x00054E35
		[SecuritySafeCritical]
		public static void Collect()
		{
			GC.InternalCollect(GC.MaxGeneration);
		}

		/// <summary>Forces a garbage collection from generation 0 through a specified generation, at a time specified by a <see cref="T:System.GCCollectionMode" /> value.</summary>
		/// <param name="generation">The number of the oldest generation that garbage collection can be performed on. </param>
		/// <param name="mode">One of the enumeration values that specifies the behavior for the garbage collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is not valid.-or-<paramref name="mode" /> is not one of the <see cref="T:System.GCCollectionMode" /> values.</exception>
		// Token: 0x060015F9 RID: 5625 RVA: 0x00056C41 File Offset: 0x00054E41
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode)
		{
			GC.Collect(generation, mode, true);
		}

		/// <summary>Forces a garbage collection from generation 0 through a specified generation, at a time specified by a <see cref="T:System.GCCollectionMode" /> value, with a value specifying whether the collection should be blocking.</summary>
		/// <param name="generation">The number of the oldest generation that garbage collection can be performed on.</param>
		/// <param name="mode">One of the enumeration values that specifies whether the garbage collection is forced or optimized.</param>
		/// <param name="blocking">true to perform a blocking garbage collection; false to perform a background garbage collection where possible.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is not valid.-or-<paramref name="mode" /> is not one of the <see cref="T:System.GCCollectionMode" /> values.</exception>
		// Token: 0x060015FA RID: 5626 RVA: 0x00056C4B File Offset: 0x00054E4B
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking)
		{
			GC.Collect(generation, mode, blocking, false);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00056C58 File Offset: 0x00054E58
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking, bool compacting)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("Value must be positive."));
			}
			if (mode < GCCollectionMode.Default || mode > GCCollectionMode.Optimized)
			{
				throw new ArgumentOutOfRangeException("mode", Environment.GetResourceString("Enum value was out of legal range."));
			}
			int num = 0;
			if (mode == GCCollectionMode.Optimized)
			{
				num |= 4;
			}
			if (compacting)
			{
				num |= 8;
			}
			if (blocking)
			{
				num |= 2;
			}
			else if (!compacting)
			{
				num |= 1;
			}
			GC.InternalCollect(generation);
		}

		/// <summary>Returns the number of times garbage collection has occurred for the specified generation of objects.</summary>
		/// <returns>The number of times garbage collection has occurred for the specified generation since the process was started.</returns>
		/// <param name="generation">The generation of objects for which the garbage collection count is to be determined. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015FC RID: 5628 RVA: 0x00056CC2 File Offset: 0x00054EC2
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		public static int CollectionCount(int generation)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("Value must be positive."));
			}
			return GC.GetCollectionCount(generation);
		}

		/// <summary>References the specified object, which makes it ineligible for garbage collection from the start of the current routine to the point where this method is called.</summary>
		/// <param name="obj">The object to reference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015FD RID: 5629 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void KeepAlive(object obj)
		{
		}

		/// <summary>Returns the current generation number of the target of a specified weak reference.</summary>
		/// <returns>The current generation number of the target of <paramref name="wo" />.</returns>
		/// <param name="wo">A <see cref="T:System.WeakReference" /> that refers to the target object whose generation number is to be determined. </param>
		/// <exception cref="T:System.ArgumentException">Garbage collection has already been performed on <paramref name="wo" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015FE RID: 5630 RVA: 0x00056CE3 File Offset: 0x00054EE3
		[SecuritySafeCritical]
		public static int GetGeneration(WeakReference wo)
		{
			object target = wo.Target;
			if (target == null)
			{
				throw new ArgumentException();
			}
			return GC.GetGeneration(target);
		}

		/// <summary>Gets the maximum number of generations that the system currently supports.</summary>
		/// <returns>A value that ranges from zero to the maximum number of supported generations.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00056CF9 File Offset: 0x00054EF9
		public static int MaxGeneration
		{
			[SecuritySafeCritical]
			get
			{
				return GC.GetMaxGeneration();
			}
		}

		/// <summary>Suspends the current thread until the thread that is processing the queue of finalizers has emptied that queue.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001600 RID: 5632
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitForPendingFinalizers();

		// Token: 0x06001601 RID: 5633
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _SuppressFinalize(object o);

		/// <summary>Requests that the system not call the finalizer for the specified object.</summary>
		/// <param name="obj">The object that a finalizer must not be called for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001602 RID: 5634 RVA: 0x00056D00 File Offset: 0x00054F00
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		public static void SuppressFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._SuppressFinalize(obj);
		}

		// Token: 0x06001603 RID: 5635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ReRegisterForFinalize(object o);

		/// <summary>Requests that the system call the finalizer for the specified object for which <see cref="M:System.GC.SuppressFinalize(System.Object)" /> has previously been called.</summary>
		/// <param name="obj">The object that a finalizer must be called for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001604 RID: 5636 RVA: 0x00056D16 File Offset: 0x00054F16
		[SecuritySafeCritical]
		public static void ReRegisterForFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._ReRegisterForFinalize(obj);
		}

		/// <summary>Retrieves the number of bytes currently thought to be allocated. A parameter indicates whether this method can wait a short interval before returning, to allow the system to collect garbage and finalize objects.</summary>
		/// <returns>A number that is the best available approximation of the number of bytes currently allocated in managed memory.</returns>
		/// <param name="forceFullCollection">true to indicate that this method can wait for garbage collection to occur before returning; otherwise, false.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001605 RID: 5637
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalMemory(bool forceFullCollection);

		// Token: 0x06001606 RID: 5638 RVA: 0x000479FC File Offset: 0x00045BFC
		private static bool _RegisterForFullGCNotification(int maxGenerationPercentage, int largeObjectHeapPercentage)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000479FC File Offset: 0x00045BFC
		private static bool _CancelFullGCNotification()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000479FC File Offset: 0x00045BFC
		private static int _WaitForFullGCApproach(int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000479FC File Offset: 0x00045BFC
		private static int _WaitForFullGCComplete(int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		/// <summary>Specifies that a garbage collection notification should be raised when conditions favor full garbage collection and when the collection has been completed.</summary>
		/// <param name="maxGenerationThreshold">A number between 1 and 99 that specifies when the notification should be raised based on the objects surviving in generation 2. </param>
		/// <param name="largeObjectHeapThreshold">A number between 1 and 99 that specifies when the notification should be raised based on objects allocated in the large object heap. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxGenerationThreshold " />or <paramref name="largeObjectHeapThreshold " />is not between 1 and 99.</exception>
		// Token: 0x0600160A RID: 5642 RVA: 0x00056D2C File Offset: 0x00054F2C
		[SecurityCritical]
		public static void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold)
		{
			if (maxGenerationThreshold <= 0 || maxGenerationThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("maxGenerationThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument must be between {0} and {1}."), 1, 99));
			}
			if (largeObjectHeapThreshold <= 0 || largeObjectHeapThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("largeObjectHeapThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument must be between {0} and {1}."), 1, 99));
			}
			if (!GC._RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold))
			{
				throw new InvalidOperationException(Environment.GetResourceString("This API is not available when the concurrent GC is enabled."));
			}
		}

		/// <summary>Cancels the registration of a garbage collection notification.</summary>
		/// <exception cref="T:System.InvalidOperationException">This member is not available when concurrent garbage collection is enabled. See the &lt;gcConcurrent&gt; runtime setting for information about how to disable concurrent garbage collection.</exception>
		// Token: 0x0600160B RID: 5643 RVA: 0x00056DBC File Offset: 0x00054FBC
		[SecurityCritical]
		public static void CancelFullGCNotification()
		{
			if (!GC._CancelFullGCNotification())
			{
				throw new InvalidOperationException(Environment.GetResourceString("This API is not available when the concurrent GC is enabled."));
			}
		}

		/// <summary>Returns the status of a registered notification for determining whether a full, blocking garbage collection by the common langauge runtime is imminent.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		// Token: 0x0600160C RID: 5644 RVA: 0x00056DD5 File Offset: 0x00054FD5
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach()
		{
			return (GCNotificationStatus)GC._WaitForFullGCApproach(-1);
		}

		/// <summary>Returns, in a specified time-out period, the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime is imminent.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		/// <param name="millisecondsTimeout">The length of time to wait before a notification status can be obtained. Specify -1 to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> must be either non-negative or less than or equal to <see cref="F:System.Int32.MaxValue" /> or -1.</exception>
		// Token: 0x0600160D RID: 5645 RVA: 0x00056DDD File Offset: 0x00054FDD
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return (GCNotificationStatus)GC._WaitForFullGCApproach(millisecondsTimeout);
		}

		/// <summary>Returns the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime has completed.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		// Token: 0x0600160E RID: 5646 RVA: 0x00056DFE File Offset: 0x00054FFE
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete()
		{
			return (GCNotificationStatus)GC._WaitForFullGCComplete(-1);
		}

		/// <summary>Returns, in a specified time-out period, the status of a registered notification for determining whether a full, blocking garbage collection by common language the runtime has completed.</summary>
		/// <returns>The status of the registered garbage collection notification.</returns>
		/// <param name="millisecondsTimeout">The length of time to wait before a notification status can be obtained. Specify -1 to wait indefinitely.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="millisecondsTimeout" /> must be either non-negative or less than or equal to <see cref="F:System.Int32.MaxValue" /> or -1.</exception>
		// Token: 0x0600160F RID: 5647 RVA: 0x00056E06 File Offset: 0x00055006
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return (GCNotificationStatus)GC._WaitForFullGCComplete(millisecondsTimeout);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000479FC File Offset: 0x00045BFC
		[SecurityCritical]
		private static bool StartNoGCRegionWorker(long totalSize, bool hasLohSize, long lohSize, bool disallowFullBlockingGC)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00056E27 File Offset: 0x00055027
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, false);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00056E33 File Offset: 0x00055033
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, false);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00056E3E File Offset: 0x0005503E
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, disallowFullBlockingGC);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00056E4A File Offset: 0x0005504A
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, disallowFullBlockingGC);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000479FC File Offset: 0x00045BFC
		[SecurityCritical]
		private static GC.EndNoGCRegionStatus EndNoGCRegionWorker()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00056E55 File Offset: 0x00055055
		[SecurityCritical]
		public static void EndNoGCRegion()
		{
			GC.EndNoGCRegionWorker();
		}

		// Token: 0x04001545 RID: 5445
		internal static readonly object EPHEMERON_TOMBSTONE = GC.get_ephemeron_tombstone();

		// Token: 0x02000200 RID: 512
		private enum StartNoGCRegionStatus
		{
			// Token: 0x04001547 RID: 5447
			Succeeded,
			// Token: 0x04001548 RID: 5448
			NotEnoughMemory,
			// Token: 0x04001549 RID: 5449
			AmountTooLarge,
			// Token: 0x0400154A RID: 5450
			AlreadyInProgress
		}

		// Token: 0x02000201 RID: 513
		private enum EndNoGCRegionStatus
		{
			// Token: 0x0400154C RID: 5452
			Succeeded,
			// Token: 0x0400154D RID: 5453
			NotInProgress,
			// Token: 0x0400154E RID: 5454
			GCInduced,
			// Token: 0x0400154F RID: 5455
			AllocationExceeded
		}
	}
}
