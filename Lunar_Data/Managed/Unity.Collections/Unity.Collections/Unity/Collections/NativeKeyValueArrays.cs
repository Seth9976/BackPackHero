using System;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000B7 RID: 183
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeKeyValueArrays<TKey, TValue> : INativeDisposable, IDisposable where TKey : struct where TValue : struct
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x000160A4 File Offset: 0x000142A4
		public int Length
		{
			get
			{
				return this.Keys.Length;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000160B1 File Offset: 0x000142B1
		public NativeKeyValueArrays(int length, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options)
		{
			this.Keys = CollectionHelper.CreateNativeArray<TKey>(length, allocator, options);
			this.Values = CollectionHelper.CreateNativeArray<TValue>(length, allocator, options);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000160CF File Offset: 0x000142CF
		public void Dispose()
		{
			this.Keys.Dispose();
			this.Values.Dispose();
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000160E7 File Offset: 0x000142E7
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.Keys.Dispose(this.Values.Dispose(inputDeps));
		}

		// Token: 0x04000285 RID: 645
		public NativeArray<TKey> Keys;

		// Token: 0x04000286 RID: 646
		public NativeArray<TValue> Values;
	}
}
