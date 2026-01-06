using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000039 RID: 57
	internal class TransformAccessJob
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00006D28 File Offset: 0x00004F28
		public TransformAccessJob()
		{
			this.m_TransformMatrix = new NativeArray<float4x4>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_TransformData = new NativeHashMap<int, TransformAccessJob.TransformData>(1, Allocator.Persistent);
			this.m_Transform = new Transform[0];
			this.m_Dirty = false;
			this.m_JobHandle = default(JobHandle);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006D7C File Offset: 0x00004F7C
		public void Destroy()
		{
			this.m_JobHandle.Complete();
			if (this.m_TransformMatrix.IsCreated)
			{
				this.m_TransformMatrix.Dispose();
			}
			if (this.m_TransformAccessArray.isCreated)
			{
				this.m_TransformAccessArray.Dispose();
			}
			if (this.m_TransformData.IsCreated)
			{
				this.m_TransformData.Dispose();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006DDC File Offset: 0x00004FDC
		public NativeHashMap<int, TransformAccessJob.TransformData> transformData
		{
			get
			{
				return this.m_TransformData;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public NativeArray<float4x4> transformMatrix
		{
			get
			{
				return this.m_TransformMatrix;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006DEC File Offset: 0x00004FEC
		public void AddTransform(Transform t)
		{
			if (t == null || !this.m_TransformData.IsCreated)
			{
				return;
			}
			this.m_JobHandle.Complete();
			int instanceID = t.GetInstanceID();
			if (this.m_TransformData.ContainsKey(instanceID))
			{
				TransformAccessJob.TransformData transformData = this.m_TransformData[instanceID];
				transformData.refCount++;
				this.m_TransformData[instanceID] = transformData;
				return;
			}
			this.m_TransformData.TryAdd(instanceID, new TransformAccessJob.TransformData(-1));
			TransformAccessJob.ArrayAdd<Transform>(ref this.m_Transform, t);
			this.m_Dirty = true;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006E80 File Offset: 0x00005080
		private static void ArrayAdd<T>(ref T[] array, T item)
		{
			int num = array.Length;
			Array.Resize<T>(ref array, num + 1);
			array[num] = item;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006EA4 File Offset: 0x000050A4
		private static void ArrayRemove<T>(ref T[] array, T item)
		{
			List<T> list = new List<T>(array);
			list.Remove(item);
			array = list.ToArray();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006ECC File Offset: 0x000050CC
		public static void ArrayRemoveAt<T>(ref T[] array, int index)
		{
			List<T> list = new List<T>(array);
			list.RemoveAt(index);
			array = list.ToArray();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006EF0 File Offset: 0x000050F0
		public void RemoveTransform(Transform t)
		{
			if (t == null || !this.m_TransformData.IsCreated)
			{
				return;
			}
			this.m_JobHandle.Complete();
			int instanceID = t.GetInstanceID();
			if (this.m_TransformData.ContainsKey(instanceID))
			{
				TransformAccessJob.TransformData transformData = this.m_TransformData[instanceID];
				if (transformData.refCount == 1)
				{
					this.m_TransformData.Remove(instanceID);
					TransformAccessJob.ArrayRemove<Transform>(ref this.m_Transform, t);
					this.m_Dirty = true;
					return;
				}
				transformData.refCount--;
				this.m_TransformData[instanceID] = transformData;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006F84 File Offset: 0x00005184
		private void UpdateTransformIndex()
		{
			if (!this.m_Dirty)
			{
				return;
			}
			this.m_Dirty = false;
			NativeArrayHelpers.ResizeIfNeeded<float4x4>(ref this.m_TransformMatrix, this.m_Transform.Length, Allocator.Persistent);
			if (!this.m_TransformAccessArray.isCreated)
			{
				TransformAccessArray.Allocate(this.m_Transform.Length, -1, out this.m_TransformAccessArray);
			}
			else if (this.m_TransformAccessArray.capacity != this.m_Transform.Length)
			{
				this.m_TransformAccessArray.capacity = this.m_Transform.Length;
			}
			this.m_TransformAccessArray.SetTransforms(this.m_Transform);
			for (int i = 0; i < this.m_Transform.Length; i++)
			{
				if (this.m_Transform[i] != null)
				{
					int instanceID = this.m_Transform[i].GetInstanceID();
					TransformAccessJob.TransformData transformData = this.m_TransformData[instanceID];
					transformData.transformIndex = i;
					this.m_TransformData[instanceID] = transformData;
				}
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007064 File Offset: 0x00005264
		public JobHandle StartLocalToWorldJob()
		{
			if (this.m_Transform.Length != 0)
			{
				this.m_JobHandle.Complete();
				this.UpdateTransformIndex();
				LocalToWorldTransformAccessJob localToWorldTransformAccessJob = new LocalToWorldTransformAccessJob
				{
					outMatrix = this.transformMatrix
				};
				this.m_JobHandle = localToWorldTransformAccessJob.Schedule(this.m_TransformAccessArray, default(JobHandle));
				return this.m_JobHandle;
			}
			return default(JobHandle);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000070D0 File Offset: 0x000052D0
		public JobHandle StartWorldToLocalJob()
		{
			if (this.m_Transform.Length != 0)
			{
				this.m_JobHandle.Complete();
				this.UpdateTransformIndex();
				WorldToLocalTransformAccessJob worldToLocalTransformAccessJob = new WorldToLocalTransformAccessJob
				{
					outMatrix = this.transformMatrix
				};
				this.m_JobHandle = worldToLocalTransformAccessJob.Schedule(this.m_TransformAccessArray, default(JobHandle));
				return this.m_JobHandle;
			}
			return default(JobHandle);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000713C File Offset: 0x0000533C
		internal string GetDebugLog()
		{
			string text = "";
			text = text + "TransformData Count: " + this.m_TransformData.Count().ToString() + "\n";
			text = text + "Transform Count: " + this.m_Transform.Length.ToString() + "\n";
			foreach (Transform transform2 in this.m_Transform)
			{
				text += ((transform2 == null) ? "null" : (transform2.name + " " + transform2.GetInstanceID().ToString()));
				text += "\n";
				if (transform2 != null)
				{
					text = text + "RefCount: " + this.m_TransformData[transform2.GetInstanceID()].refCount.ToString() + "\n";
				}
				text += "\n";
			}
			return text;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000723C File Offset: 0x0000543C
		internal void RemoveTransformById(int transformId)
		{
			if (!this.m_TransformData.IsCreated)
			{
				return;
			}
			this.m_JobHandle.Complete();
			if (this.m_TransformData.ContainsKey(transformId))
			{
				TransformAccessJob.TransformData transformData = this.m_TransformData[transformId];
				if (transformData.refCount == 1)
				{
					this.m_TransformData.Remove(transformId);
					int num = Array.FindIndex<Transform>(this.m_Transform, (Transform t) => t.GetInstanceID() == transformId);
					if (num >= 0)
					{
						TransformAccessJob.ArrayRemoveAt<Transform>(ref this.m_Transform, num);
					}
					this.m_Dirty = true;
					return;
				}
				transformData.refCount--;
				this.m_TransformData[transformId] = transformData;
			}
		}

		// Token: 0x040000C8 RID: 200
		private Transform[] m_Transform;

		// Token: 0x040000C9 RID: 201
		private TransformAccessArray m_TransformAccessArray;

		// Token: 0x040000CA RID: 202
		private NativeHashMap<int, TransformAccessJob.TransformData> m_TransformData;

		// Token: 0x040000CB RID: 203
		private NativeArray<float4x4> m_TransformMatrix;

		// Token: 0x040000CC RID: 204
		private bool m_Dirty;

		// Token: 0x040000CD RID: 205
		private JobHandle m_JobHandle;

		// Token: 0x0200003A RID: 58
		internal struct TransformData
		{
			// Token: 0x06000148 RID: 328 RVA: 0x000072FD File Offset: 0x000054FD
			public TransformData(int index)
			{
				this.transformIndex = index;
				this.refCount = 1;
			}

			// Token: 0x040000CE RID: 206
			public int transformIndex;

			// Token: 0x040000CF RID: 207
			public int refCount;
		}
	}
}
