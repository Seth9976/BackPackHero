using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x020002BE RID: 702
	[Serializable]
	internal class PersistentCall : ISerializationCallbackReceiver
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0002F100 File Offset: 0x0002D300
		public Object target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0002F118 File Offset: 0x0002D318
		public string targetAssemblyTypeName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.m_TargetAssemblyTypeName) && this.m_Target != null;
				if (flag)
				{
					this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_Target.GetType().AssemblyQualifiedName);
				}
				return this.m_TargetAssemblyTypeName;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0002F170 File Offset: 0x0002D370
		public string methodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x0002F188 File Offset: 0x0002D388
		// (set) Token: 0x06001D4D RID: 7501 RVA: 0x0002F1A0 File Offset: 0x0002D3A0
		public PersistentListenerMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0002F1AC File Offset: 0x0002D3AC
		public ArgumentCache arguments
		{
			get
			{
				return this.m_Arguments;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0002F1C4 File Offset: 0x0002D3C4
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0002F1DC File Offset: 0x0002D3DC
		public UnityEventCallState callState
		{
			get
			{
				return this.m_CallState;
			}
			set
			{
				this.m_CallState = value;
			}
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0002F1E8 File Offset: 0x0002D3E8
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.targetAssemblyTypeName) && !string.IsNullOrEmpty(this.methodName);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0002F218 File Offset: 0x0002D418
		public BaseInvokableCall GetRuntimeCall(UnityEventBase theEvent)
		{
			bool flag = this.m_CallState == UnityEventCallState.Off || theEvent == null;
			BaseInvokableCall baseInvokableCall;
			if (flag)
			{
				baseInvokableCall = null;
			}
			else
			{
				MethodInfo methodInfo = theEvent.FindMethod(this);
				bool flag2 = methodInfo == null;
				if (flag2)
				{
					baseInvokableCall = null;
				}
				else
				{
					bool flag3 = !methodInfo.IsStatic && this.target == null;
					if (flag3)
					{
						baseInvokableCall = null;
					}
					else
					{
						Object @object = (methodInfo.IsStatic ? null : this.target);
						switch (this.m_Mode)
						{
						case PersistentListenerMode.EventDefined:
							baseInvokableCall = theEvent.GetDelegate(@object, methodInfo);
							break;
						case PersistentListenerMode.Void:
							baseInvokableCall = new InvokableCall(@object, methodInfo);
							break;
						case PersistentListenerMode.Object:
							baseInvokableCall = PersistentCall.GetObjectCall(@object, methodInfo, this.m_Arguments);
							break;
						case PersistentListenerMode.Int:
							baseInvokableCall = new CachedInvokableCall<int>(@object, methodInfo, this.m_Arguments.intArgument);
							break;
						case PersistentListenerMode.Float:
							baseInvokableCall = new CachedInvokableCall<float>(@object, methodInfo, this.m_Arguments.floatArgument);
							break;
						case PersistentListenerMode.String:
							baseInvokableCall = new CachedInvokableCall<string>(@object, methodInfo, this.m_Arguments.stringArgument);
							break;
						case PersistentListenerMode.Bool:
							baseInvokableCall = new CachedInvokableCall<bool>(@object, methodInfo, this.m_Arguments.boolArgument);
							break;
						default:
							baseInvokableCall = null;
							break;
						}
					}
				}
			}
			return baseInvokableCall;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0002F340 File Offset: 0x0002D540
		private static BaseInvokableCall GetObjectCall(Object target, MethodInfo method, ArgumentCache arguments)
		{
			Type type = typeof(Object);
			bool flag = !string.IsNullOrEmpty(arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				type = Type.GetType(arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object);
			}
			Type typeFromHandle = typeof(CachedInvokableCall<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[] { type });
			ConstructorInfo constructor = type2.GetConstructor(new Type[]
			{
				typeof(Object),
				typeof(MethodInfo),
				type
			});
			Object @object = arguments.unityObjectArgument;
			bool flag2 = @object != null && !type.IsAssignableFrom(@object.GetType());
			if (flag2)
			{
				@object = null;
			}
			return constructor.Invoke(new object[] { target, method, @object }) as BaseInvokableCall;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0002F421 File Offset: 0x0002D621
		public void RegisterPersistentListener(Object ttarget, Type targetType, string mmethodName)
		{
			this.m_Target = ttarget;
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(targetType.AssemblyQualifiedName);
			this.m_MethodName = mmethodName;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0002F443 File Offset: 0x0002D643
		public void UnregisterPersistentListener()
		{
			this.m_MethodName = string.Empty;
			this.m_Target = null;
			this.m_TargetAssemblyTypeName = string.Empty;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0002F463 File Offset: 0x0002D663
		public void OnBeforeSerialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0002F463 File Offset: 0x0002D663
		public void OnAfterDeserialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x0400099B RID: 2459
		[SerializeField]
		[FormerlySerializedAs("instance")]
		private Object m_Target;

		// Token: 0x0400099C RID: 2460
		[SerializeField]
		private string m_TargetAssemblyTypeName;

		// Token: 0x0400099D RID: 2461
		[FormerlySerializedAs("methodName")]
		[SerializeField]
		private string m_MethodName;

		// Token: 0x0400099E RID: 2462
		[SerializeField]
		[FormerlySerializedAs("mode")]
		private PersistentListenerMode m_Mode = PersistentListenerMode.EventDefined;

		// Token: 0x0400099F RID: 2463
		[FormerlySerializedAs("arguments")]
		[SerializeField]
		private ArgumentCache m_Arguments = new ArgumentCache();

		// Token: 0x040009A0 RID: 2464
		[FormerlySerializedAs("m_Enabled")]
		[SerializeField]
		[FormerlySerializedAs("enabled")]
		private UnityEventCallState m_CallState = UnityEventCallState.RuntimeOnly;
	}
}
