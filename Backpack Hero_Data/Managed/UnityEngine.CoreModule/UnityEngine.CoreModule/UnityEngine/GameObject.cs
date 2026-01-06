using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000209 RID: 521
	[NativeHeader("Runtime/Export/Scripting/GameObject.bindings.h")]
	[ExcludeFromPreset]
	[UsedByNativeCode]
	public sealed class GameObject : Object
	{
		// Token: 0x060016BB RID: 5819
		[FreeFunction("GameObjectBindings::CreatePrimitive")]
		[MethodImpl(4096)]
		public static extern GameObject CreatePrimitive(PrimitiveType type);

		// Token: 0x060016BC RID: 5820 RVA: 0x00024A30 File Offset: 0x00022C30
		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		// Token: 0x060016BD RID: 5821
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentFromType", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Component GetComponent(Type type);

		// Token: 0x060016BE RID: 5822
		[NativeWritableSelf]
		[FreeFunction(Name = "GameObjectBindings::GetComponentFastPath", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x060016BF RID: 5823
		[FreeFunction(Name = "Scripting::GetScriptingWrapperOfComponentOfGameObject", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern Component GetComponentByName(string type);

		// Token: 0x060016C0 RID: 5824 RVA: 0x00024A70 File Offset: 0x00022C70
		public Component GetComponent(string type)
		{
			return this.GetComponentByName(type);
		}

		// Token: 0x060016C1 RID: 5825
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentInChildren", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Component GetComponentInChildren(Type type, bool includeInactive);

		// Token: 0x060016C2 RID: 5826 RVA: 0x00024A8C File Offset: 0x00022C8C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type type)
		{
			return this.GetComponentInChildren(type, false);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00024AA8 File Offset: 0x00022CA8
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			bool flag = false;
			return this.GetComponentInChildren<T>(flag);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00024AC4 File Offset: 0x00022CC4
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x060016C5 RID: 5829
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentInParent", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Component GetComponentInParent(Type type, bool includeInactive);

		// Token: 0x060016C6 RID: 5830 RVA: 0x00024AEC File Offset: 0x00022CEC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type type)
		{
			return this.GetComponentInParent(type, false);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00024B08 File Offset: 0x00022D08
		[ExcludeFromDocs]
		public T GetComponentInParent<T>()
		{
			bool flag = false;
			return this.GetComponentInParent<T>(flag);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00024B24 File Offset: 0x00022D24
		public T GetComponentInParent<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInParent(typeof(T), includeInactive));
		}

		// Token: 0x060016C9 RID: 5833
		[FreeFunction(Name = "GameObjectBindings::GetComponentsInternal", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern Array GetComponentsInternal(Type type, bool useSearchTypeAsArrayReturnType, bool recursive, bool includeInactive, bool reverse, object resultList);

		// Token: 0x060016CA RID: 5834 RVA: 0x00024B4C File Offset: 0x00022D4C
		public Component[] GetComponents(Type type)
		{
			return (Component[])this.GetComponentsInternal(type, false, false, true, false, null);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00024B70 File Offset: 0x00022D70
		public T[] GetComponents<T>()
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, false, true, false, null);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00024B9C File Offset: 0x00022D9C
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsInternal(type, false, false, true, false, results);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00024BAC File Offset: 0x00022DAC
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, false, true, false, results);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00024BC8 File Offset: 0x00022DC8
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool flag = false;
			return this.GetComponentsInChildren(type, flag);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00024BE4 File Offset: 0x00022DE4
		public Component[] GetComponentsInChildren(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, false, null);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00024C08 File Offset: 0x00022E08
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, null);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00024C34 File Offset: 0x00022E34
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, results);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00024C50 File Offset: 0x00022E50
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00024C69 File Offset: 0x00022E69
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00024C78 File Offset: 0x00022E78
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool flag = false;
			return this.GetComponentsInParent(type, flag);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00024C94 File Offset: 0x00022E94
		public Component[] GetComponentsInParent(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, true, null);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00024CB7 File Offset: 0x00022EB7
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, results);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00024CD0 File Offset: 0x00022ED0
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, null);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00024CFC File Offset: 0x00022EFC
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00024D18 File Offset: 0x00022F18
		[SecuritySafeCritical]
		public unsafe bool TryGetComponent<T>(out T component)
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.TryGetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			component = castHelper.t;
			return castHelper.t != null;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00024D6C File Offset: 0x00022F6C
		public bool TryGetComponent(Type type, out Component component)
		{
			component = this.TryGetComponentInternal(type);
			return component != null;
		}

		// Token: 0x060016DB RID: 5851
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFromType", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal extern Component TryGetComponentInternal(Type type);

		// Token: 0x060016DC RID: 5852
		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFastPath", HasExplicitThis = true, ThrowsException = true)]
		[NativeWritableSelf]
		[MethodImpl(4096)]
		internal extern void TryGetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x060016DD RID: 5853 RVA: 0x00024D90 File Offset: 0x00022F90
		public static GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00024DA8 File Offset: 0x00022FA8
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00024DB5 File Offset: 0x00022FB5
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00024DC2 File Offset: 0x00022FC2
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x060016E1 RID: 5857
		[FreeFunction(Name = "MonoAddComponent", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern Component AddComponentInternal(string className);

		// Token: 0x060016E2 RID: 5858
		[FreeFunction(Name = "MonoAddComponentWithType", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Component Internal_AddComponentWithType(Type componentType);

		// Token: 0x060016E3 RID: 5859 RVA: 0x00024DD0 File Offset: 0x00022FD0
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return this.Internal_AddComponentWithType(componentType);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00024DEC File Offset: 0x00022FEC
		public T AddComponent<T>() where T : Component
		{
			return this.AddComponent(typeof(T)) as T;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060016E5 RID: 5861
		public extern Transform transform
		{
			[FreeFunction("GameObjectBindings::GetTransform", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060016E6 RID: 5862
		// (set) Token: 0x060016E7 RID: 5863
		public extern int layer
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060016E8 RID: 5864
		// (set) Token: 0x060016E9 RID: 5865
		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public extern bool active
		{
			[NativeMethod(Name = "IsActive")]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "SetSelfActive")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060016EA RID: 5866
		[NativeMethod(Name = "SetSelfActive")]
		[MethodImpl(4096)]
		public extern void SetActive(bool value);

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060016EB RID: 5867
		public extern bool activeSelf
		{
			[NativeMethod(Name = "IsSelfActive")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060016EC RID: 5868
		public extern bool activeInHierarchy
		{
			[NativeMethod(Name = "IsActive")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060016ED RID: 5869
		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children.")]
		[NativeMethod(Name = "SetActiveRecursivelyDeprecated")]
		[MethodImpl(4096)]
		public extern void SetActiveRecursively(bool state);

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060016EE RID: 5870
		// (set) Token: 0x060016EF RID: 5871
		public extern bool isStatic
		{
			[NativeMethod(Name = "GetIsStaticDeprecated")]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "SetIsStaticDeprecated")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060016F0 RID: 5872
		internal extern bool isStaticBatchable
		{
			[NativeMethod(Name = "IsStaticBatchable")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060016F1 RID: 5873
		// (set) Token: 0x060016F2 RID: 5874
		public extern string tag
		{
			[FreeFunction("GameObjectBindings::GetTag", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("GameObjectBindings::SetTag", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060016F3 RID: 5875
		[FreeFunction(Name = "GameObjectBindings::CompareTag", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool CompareTag(string tag);

		// Token: 0x060016F4 RID: 5876
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectWithTag", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern GameObject FindGameObjectWithTag(string tag);

		// Token: 0x060016F5 RID: 5877
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectsWithTag", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern GameObject[] FindGameObjectsWithTag(string tag);

		// Token: 0x060016F6 RID: 5878
		[FreeFunction(Name = "Scripting::SendScriptingMessageUpwards", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060016F7 RID: 5879 RVA: 0x00024E18 File Offset: 0x00023018
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, sendMessageOptions);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00024E34 File Offset: 0x00023034
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessageUpwards(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060016F9 RID: 5881
		[FreeFunction(Name = "Scripting::SendScriptingMessage", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060016FA RID: 5882 RVA: 0x00024E50 File Offset: 0x00023050
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, sendMessageOptions);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00024E6C File Offset: 0x0002306C
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060016FC RID: 5884
		[FreeFunction(Name = "Scripting::BroadcastScriptingMessage", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060016FD RID: 5885 RVA: 0x00024E88 File Offset: 0x00023088
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, sendMessageOptions);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00024EA4 File Offset: 0x000230A4
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.BroadcastMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00024EC0 File Offset: 0x000230C0
		public GameObject(string name)
		{
			GameObject.Internal_CreateGameObject(this, name);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00024ED2 File Offset: 0x000230D2
		public GameObject()
		{
			GameObject.Internal_CreateGameObject(this, null);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00024EE4 File Offset: 0x000230E4
		public GameObject(string name, params Type[] components)
		{
			GameObject.Internal_CreateGameObject(this, name);
			foreach (Type type in components)
			{
				this.AddComponent(type);
			}
		}

		// Token: 0x06001702 RID: 5890
		[FreeFunction(Name = "GameObjectBindings::Internal_CreateGameObject")]
		[MethodImpl(4096)]
		private static extern void Internal_CreateGameObject([Writable] GameObject self, string name);

		// Token: 0x06001703 RID: 5891
		[FreeFunction(Name = "GameObjectBindings::Find")]
		[MethodImpl(4096)]
		public static extern GameObject Find(string name);

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00024F20 File Offset: 0x00023120
		public Scene scene
		{
			[FreeFunction("GameObjectBindings::GetScene", HasExplicitThis = true)]
			get
			{
				Scene scene;
				this.get_scene_Injected(out scene);
				return scene;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001705 RID: 5893
		public extern ulong sceneCullingMask
		{
			[FreeFunction(Name = "GameObjectBindings::GetSceneCullingMask", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00024F38 File Offset: 0x00023138
		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001707 RID: 5895
		[MethodImpl(4096)]
		private extern void get_scene_Injected(out Scene ret);
	}
}
