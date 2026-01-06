using System;
using System.Collections.Generic;
using System.Reflection;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x0200018A RID: 394
	internal struct JobDependencyAnalyzer<T> where T : struct
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x0003CA53 File Offset: 0x0003AC53
		private static void initReflectionData()
		{
			if (JobDependencyAnalyzer<T>.reflectionData.fieldOffsets == null)
			{
				JobDependencyAnalyzer<T>.reflectionData.Build();
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0003CA6C File Offset: 0x0003AC6C
		private static bool HasHash(int[] hashes, int hash, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (hashes[i] == hash)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0003CA90 File Offset: 0x0003AC90
		public static JobHandle GetDependencies(ref T data, JobDependencyTracker tracker)
		{
			return JobDependencyAnalyzer<T>.GetDependencies(ref data, tracker, default(JobHandle), false);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0003CAAE File Offset: 0x0003ACAE
		public static JobHandle GetDependencies(ref T data, JobDependencyTracker tracker, JobHandle additionalDependency)
		{
			return JobDependencyAnalyzer<T>.GetDependencies(ref data, tracker, additionalDependency, true);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0003CABC File Offset: 0x0003ACBC
		private unsafe static JobHandle GetDependencies(ref T data, JobDependencyTracker tracker, JobHandle additionalDependency, bool useAdditionalDependency)
		{
			if (!tracker.dependenciesScratchBuffer.IsCreated)
			{
				tracker.dependenciesScratchBuffer = new NativeArray<JobHandle>(16, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			NativeArray<JobHandle> dependenciesScratchBuffer = tracker.dependenciesScratchBuffer;
			List<JobDependencyTracker.NativeArraySlot> slots = tracker.slots;
			int[] tempJobDependencyHashes = JobDependencyAnalyzerAssociated.tempJobDependencyHashes;
			int num = 0;
			JobDependencyAnalyzer<T>.initReflectionData();
			byte* ptr = (byte*)UnsafeUtility.AddressOf<T>(ref data);
			int[] fieldOffsets = JobDependencyAnalyzer<T>.reflectionData.fieldOffsets;
			for (int i = 0; i < fieldOffsets.Length; i++)
			{
				long num2 = (long)((ulong)(*(IntPtr*)(ptr + fieldOffsets[i])));
				int j = 0;
				while (j <= slots.Count)
				{
					if (j == slots.Count)
					{
						slots.Add(new JobDependencyTracker.NativeArraySlot
						{
							hash = num2,
							lastWrite = default(JobDependencyTracker.JobInstance),
							lastReads = ListPool<JobDependencyTracker.JobInstance>.Claim(),
							initialized = true,
							hasWrite = false
						});
					}
					JobDependencyTracker.NativeArraySlot nativeArraySlot = slots[j];
					if (nativeArraySlot.hash == num2)
					{
						if (JobDependencyAnalyzer<T>.reflectionData.checkUninitializedRead[i] && !nativeArraySlot.initialized)
						{
							throw new InvalidOperationException(string.Concat(new string[]
							{
								"A job tries to read from the native array ",
								typeof(T).Name,
								".",
								JobDependencyAnalyzer<T>.reflectionData.fieldNames[i],
								" which contains uninitialized data"
							}));
						}
						if (nativeArraySlot.hasWrite && !JobDependencyAnalyzer<T>.HasHash(tempJobDependencyHashes, nativeArraySlot.lastWrite.hash, num))
						{
							dependenciesScratchBuffer[num] = nativeArraySlot.lastWrite.handle;
							tempJobDependencyHashes[num] = nativeArraySlot.lastWrite.hash;
							num++;
							if (num >= dependenciesScratchBuffer.Length)
							{
								throw new Exception("Too many dependencies for job");
							}
						}
						if (JobDependencyAnalyzer<T>.reflectionData.writes[i])
						{
							for (int k = 0; k < nativeArraySlot.lastReads.Count; k++)
							{
								if (!JobDependencyAnalyzer<T>.HasHash(tempJobDependencyHashes, nativeArraySlot.lastReads[k].hash, num))
								{
									dependenciesScratchBuffer[num] = nativeArraySlot.lastReads[k].handle;
									tempJobDependencyHashes[num] = nativeArraySlot.lastReads[k].hash;
									num++;
									if (num >= dependenciesScratchBuffer.Length)
									{
										throw new Exception("Too many dependencies for job");
									}
								}
							}
							break;
						}
						break;
					}
					else
					{
						j++;
					}
				}
			}
			if (useAdditionalDependency)
			{
				dependenciesScratchBuffer[num] = additionalDependency;
				num++;
			}
			if (num == 0)
			{
				return default(JobHandle);
			}
			if (num == 1)
			{
				return dependenciesScratchBuffer[0];
			}
			return JobHandle.CombineDependencies(dependenciesScratchBuffer.Slice(0, num));
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0003CD44 File Offset: 0x0003AF44
		internal unsafe static void Scheduled(ref T data, JobDependencyTracker tracker, JobHandle job)
		{
			int num = JobDependencyAnalyzerAssociated.jobCounter++;
			byte* ptr = (byte*)UnsafeUtility.AddressOf<T>(ref data);
			for (int i = 0; i < JobDependencyAnalyzer<T>.reflectionData.fieldOffsets.Length; i++)
			{
				long num2 = (long)((ulong)(*(IntPtr*)(ptr + JobDependencyAnalyzer<T>.reflectionData.fieldOffsets[i])));
				if (JobDependencyAnalyzer<T>.reflectionData.writes[i])
				{
					tracker.JobWritesTo(job, num2, num);
				}
				else
				{
					tracker.JobReadsFrom(job, num2, num);
				}
			}
		}

		// Token: 0x04000757 RID: 1879
		private static JobDependencyAnalyzer<T>.ReflectionData reflectionData;

		// Token: 0x04000758 RID: 1880
		private static readonly int BufferOffset = UnsafeUtility.GetFieldOffset(typeof(NativeArray<int>).GetField("m_Buffer", BindingFlags.Instance | BindingFlags.NonPublic));

		// Token: 0x04000759 RID: 1881
		private static readonly int SpanPtrOffset = UnsafeUtility.GetFieldOffset(typeof(UnsafeSpan<int>).GetField("ptr", BindingFlags.Instance | BindingFlags.NonPublic));

		// Token: 0x0200018B RID: 395
		private struct ReflectionData
		{
			// Token: 0x06000ACC RID: 2764 RVA: 0x0003CE00 File Offset: 0x0003B000
			public void Build()
			{
				List<int> list = new List<int>();
				List<bool> list2 = new List<bool>();
				List<bool> list3 = new List<bool>();
				List<string> list4 = new List<string>();
				this.Build(typeof(T), list, list2, list3, list4, 0, false, false, false);
				this.fieldOffsets = list.ToArray();
				this.writes = list2.ToArray();
				this.fieldNames = list4.ToArray();
				this.checkUninitializedRead = list3.ToArray();
			}

			// Token: 0x06000ACD RID: 2765 RVA: 0x0003CE70 File Offset: 0x0003B070
			private void Build(Type type, List<int> fields, List<bool> writes, List<bool> reads, List<string> names, int offset, bool forceReadOnly, bool forceWriteOnly, bool forceDisableUninitializedCheck)
			{
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(NativeArray<>))
					{
						fields.Add(offset + UnsafeUtility.GetFieldOffset(fieldInfo) + JobDependencyAnalyzer<T>.BufferOffset);
						writes.Add(!forceReadOnly && fieldInfo.GetCustomAttribute(typeof(ReadOnlyAttribute)) == null);
						reads.Add(!forceWriteOnly && !forceDisableUninitializedCheck && fieldInfo.GetCustomAttribute(typeof(WriteOnlyAttribute)) == null && fieldInfo.GetCustomAttribute(typeof(DisableUninitializedReadCheckAttribute)) == null);
						names.Add(fieldInfo.Name);
					}
					else if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(UnsafeSpan<>))
					{
						fields.Add(offset + UnsafeUtility.GetFieldOffset(fieldInfo) + JobDependencyAnalyzer<T>.SpanPtrOffset);
						writes.Add(!forceReadOnly && fieldInfo.GetCustomAttribute(typeof(ReadOnlyAttribute)) == null);
						reads.Add(!forceWriteOnly && !forceDisableUninitializedCheck && fieldInfo.GetCustomAttribute(typeof(WriteOnlyAttribute)) == null && fieldInfo.GetCustomAttribute(typeof(DisableUninitializedReadCheckAttribute)) == null);
						names.Add(fieldInfo.Name);
					}
					else if (!fieldInfo.FieldType.IsPrimitive && fieldInfo.FieldType.IsValueType && !fieldInfo.FieldType.IsEnum)
					{
						bool flag = fieldInfo.GetCustomAttribute(typeof(ReadOnlyAttribute)) != null;
						bool flag2 = fieldInfo.GetCustomAttribute(typeof(WriteOnlyAttribute)) != null;
						bool flag3 = fieldInfo.GetCustomAttribute(typeof(DisableUninitializedReadCheckAttribute)) != null;
						this.Build(fieldInfo.FieldType, fields, writes, reads, names, offset + UnsafeUtility.GetFieldOffset(fieldInfo), flag, flag2, flag3);
					}
				}
			}

			// Token: 0x0400075A RID: 1882
			public int[] fieldOffsets;

			// Token: 0x0400075B RID: 1883
			public bool[] writes;

			// Token: 0x0400075C RID: 1884
			public bool[] checkUninitializedRead;

			// Token: 0x0400075D RID: 1885
			public string[] fieldNames;
		}
	}
}
