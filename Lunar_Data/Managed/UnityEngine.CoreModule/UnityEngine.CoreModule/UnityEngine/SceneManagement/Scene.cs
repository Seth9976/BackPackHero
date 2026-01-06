using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002DF RID: 735
	[NativeHeader("Runtime/Export/SceneManager/Scene.bindings.h")]
	[Serializable]
	public struct Scene
	{
		// Token: 0x06001DFB RID: 7675
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern bool IsValidInternal(int sceneHandle);

		// Token: 0x06001DFC RID: 7676
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern string GetPathInternal(int sceneHandle);

		// Token: 0x06001DFD RID: 7677
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern string GetNameInternal(int sceneHandle);

		// Token: 0x06001DFE RID: 7678
		[NativeThrows]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern void SetNameInternal(int sceneHandle, string name);

		// Token: 0x06001DFF RID: 7679
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern string GetGUIDInternal(int sceneHandle);

		// Token: 0x06001E00 RID: 7680
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern bool IsSubScene(int sceneHandle);

		// Token: 0x06001E01 RID: 7681
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern void SetIsSubScene(int sceneHandle, bool value);

		// Token: 0x06001E02 RID: 7682
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern bool GetIsLoadedInternal(int sceneHandle);

		// Token: 0x06001E03 RID: 7683
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern Scene.LoadingState GetLoadingStateInternal(int sceneHandle);

		// Token: 0x06001E04 RID: 7684
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern bool GetIsDirtyInternal(int sceneHandle);

		// Token: 0x06001E05 RID: 7685
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern int GetDirtyID(int sceneHandle);

		// Token: 0x06001E06 RID: 7686
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern int GetBuildIndexInternal(int sceneHandle);

		// Token: 0x06001E07 RID: 7687
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern int GetRootCountInternal(int sceneHandle);

		// Token: 0x06001E08 RID: 7688
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern void GetRootGameObjectsInternal(int sceneHandle, object resultRootList);

		// Token: 0x06001E09 RID: 7689 RVA: 0x00030B65 File Offset: 0x0002ED65
		internal Scene(int handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x00030B70 File Offset: 0x0002ED70
		public int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x00030B88 File Offset: 0x0002ED88
		internal Scene.LoadingState loadingState
		{
			get
			{
				return Scene.GetLoadingStateInternal(this.handle);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x00030BA8 File Offset: 0x0002EDA8
		internal string guid
		{
			get
			{
				return Scene.GetGUIDInternal(this.handle);
			}
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00030BC8 File Offset: 0x0002EDC8
		public bool IsValid()
		{
			return Scene.IsValidInternal(this.handle);
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x00030BE8 File Offset: 0x0002EDE8
		public string path
		{
			get
			{
				return Scene.GetPathInternal(this.handle);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00030C08 File Offset: 0x0002EE08
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x00030C25 File Offset: 0x0002EE25
		public string name
		{
			get
			{
				return Scene.GetNameInternal(this.handle);
			}
			set
			{
				Scene.SetNameInternal(this.handle, value);
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00030C38 File Offset: 0x0002EE38
		public bool isLoaded
		{
			get
			{
				return Scene.GetIsLoadedInternal(this.handle);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x00030C58 File Offset: 0x0002EE58
		public int buildIndex
		{
			get
			{
				return Scene.GetBuildIndexInternal(this.handle);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x00030C78 File Offset: 0x0002EE78
		public bool isDirty
		{
			get
			{
				return Scene.GetIsDirtyInternal(this.handle);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x00030C98 File Offset: 0x0002EE98
		internal int dirtyID
		{
			get
			{
				return Scene.GetDirtyID(this.handle);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x00030CB8 File Offset: 0x0002EEB8
		public int rootCount
		{
			get
			{
				return Scene.GetRootCountInternal(this.handle);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x00030CD8 File Offset: 0x0002EED8
		// (set) Token: 0x06001E17 RID: 7703 RVA: 0x00030CF5 File Offset: 0x0002EEF5
		public bool isSubScene
		{
			get
			{
				return Scene.IsSubScene(this.handle);
			}
			set
			{
				Scene.SetIsSubScene(this.handle, value);
			}
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00030D08 File Offset: 0x0002EF08
		public GameObject[] GetRootGameObjects()
		{
			List<GameObject> list = new List<GameObject>(this.rootCount);
			this.GetRootGameObjects(list);
			return list.ToArray();
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00030D34 File Offset: 0x0002EF34
		public void GetRootGameObjects(List<GameObject> rootGameObjects)
		{
			bool flag = rootGameObjects.Capacity < this.rootCount;
			if (flag)
			{
				rootGameObjects.Capacity = this.rootCount;
			}
			rootGameObjects.Clear();
			bool flag2 = !this.IsValid();
			if (flag2)
			{
				throw new ArgumentException("The scene is invalid.");
			}
			bool flag3 = !Application.isPlaying && !this.isLoaded;
			if (flag3)
			{
				throw new ArgumentException("The scene is not loaded.");
			}
			bool flag4 = this.rootCount == 0;
			if (!flag4)
			{
				Scene.GetRootGameObjectsInternal(this.handle, rootGameObjects);
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00030DC0 File Offset: 0x0002EFC0
		public static bool operator ==(Scene lhs, Scene rhs)
		{
			return lhs.handle == rhs.handle;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00030DE4 File Offset: 0x0002EFE4
		public static bool operator !=(Scene lhs, Scene rhs)
		{
			return lhs.handle != rhs.handle;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00030E0C File Offset: 0x0002F00C
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00030E24 File Offset: 0x0002F024
		public override bool Equals(object other)
		{
			bool flag = !(other is Scene);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Scene scene = (Scene)other;
				flag2 = this.handle == scene.handle;
			}
			return flag2;
		}

		// Token: 0x040009D8 RID: 2520
		[HideInInspector]
		[SerializeField]
		private int m_Handle;

		// Token: 0x020002E0 RID: 736
		internal enum LoadingState
		{
			// Token: 0x040009DA RID: 2522
			NotLoaded,
			// Token: 0x040009DB RID: 2523
			Loading,
			// Token: 0x040009DC RID: 2524
			Loaded,
			// Token: 0x040009DD RID: 2525
			Unloading
		}
	}
}
