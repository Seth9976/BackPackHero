using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000057 RID: 87
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStream.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	[RequiredByNativeCode]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct AnimationStream
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00005F9C File Offset: 0x0000419C
		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00005FB4 File Offset: 0x000041B4
		public bool isValid
		{
			get
			{
				return this.m_AnimatorBindingsVersion >= 2U && this.constant != IntPtr.Zero && this.input != IntPtr.Zero && this.output != IntPtr.Zero && this.workspace != IntPtr.Zero && this.animationHandleBinder != IntPtr.Zero;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000602C File Offset: 0x0000422C
		internal void CheckIsValid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new InvalidOperationException("The AnimationStream is invalid.");
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00006054 File Offset: 0x00004254
		public float deltaTime
		{
			get
			{
				this.CheckIsValid();
				return this.GetDeltaTime();
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00006074 File Offset: 0x00004274
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00006093 File Offset: 0x00004293
		public Vector3 velocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetVelocity(value);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x000060A8 File Offset: 0x000042A8
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x000060C7 File Offset: 0x000042C7
		public Vector3 angularVelocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetAngularVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetAngularVelocity(value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000060DC File Offset: 0x000042DC
		public Vector3 rootMotionPosition
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionPosition();
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000060FC File Offset: 0x000042FC
		public Quaternion rootMotionRotation
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionRotation();
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000611C File Offset: 0x0000431C
		public bool isHumanStream
		{
			get
			{
				this.CheckIsValid();
				return this.GetIsHumanStream();
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000613C File Offset: 0x0000433C
		public AnimationHumanStream AsHuman()
		{
			this.CheckIsValid();
			bool flag = !this.GetIsHumanStream();
			if (flag)
			{
				throw new InvalidOperationException("Cannot create an AnimationHumanStream for a generic rig.");
			}
			return this.GetHumanStream();
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00006174 File Offset: 0x00004374
		public int inputStreamCount
		{
			get
			{
				this.CheckIsValid();
				return this.GetInputStreamCount();
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00006194 File Offset: 0x00004394
		public AnimationStream GetInputStream(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputStream(index);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000061B4 File Offset: 0x000043B4
		public float GetInputWeight(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputWeight(index);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000061D4 File Offset: 0x000043D4
		public void CopyAnimationStreamMotion(AnimationStream animationStream)
		{
			this.CheckIsValid();
			animationStream.CheckIsValid();
			this.CopyAnimationStreamMotionInternal(animationStream);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000061EE File Offset: 0x000043EE
		private void ReadSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalReadSceneTransforms();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000061FF File Offset: 0x000043FF
		private void WriteSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalWriteSceneTransforms();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00006210 File Offset: 0x00004410
		[NativeMethod(Name = "AnimationStreamBindings::CopyAnimationStreamMotion", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void CopyAnimationStreamMotionInternal(AnimationStream animationStream)
		{
			AnimationStream.CopyAnimationStreamMotionInternal_Injected(ref this, ref animationStream);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000621A File Offset: 0x0000441A
		[NativeMethod(IsThreadSafe = true)]
		private float GetDeltaTime()
		{
			return AnimationStream.GetDeltaTime_Injected(ref this);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00006222 File Offset: 0x00004422
		[NativeMethod(IsThreadSafe = true)]
		private bool GetIsHumanStream()
		{
			return AnimationStream.GetIsHumanStream_Injected(ref this);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000622C File Offset: 0x0000442C
		[NativeMethod(Name = "AnimationStreamBindings::GetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetVelocity()
		{
			Vector3 vector;
			AnimationStream.GetVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00006242 File Offset: 0x00004442
		[NativeMethod(Name = "AnimationStreamBindings::SetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetVelocity(Vector3 velocity)
		{
			AnimationStream.SetVelocity_Injected(ref this, ref velocity);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000624C File Offset: 0x0000444C
		[NativeMethod(Name = "AnimationStreamBindings::GetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetAngularVelocity()
		{
			Vector3 vector;
			AnimationStream.GetAngularVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00006262 File Offset: 0x00004462
		[NativeMethod(Name = "AnimationStreamBindings::SetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetAngularVelocity(Vector3 velocity)
		{
			AnimationStream.SetAngularVelocity_Injected(ref this, ref velocity);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000626C File Offset: 0x0000446C
		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRootMotionPosition()
		{
			Vector3 vector;
			AnimationStream.GetRootMotionPosition_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00006284 File Offset: 0x00004484
		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRootMotionRotation()
		{
			Quaternion quaternion;
			AnimationStream.GetRootMotionRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000629A File Offset: 0x0000449A
		[NativeMethod(IsThreadSafe = true)]
		private int GetInputStreamCount()
		{
			return AnimationStream.GetInputStreamCount_Injected(ref this);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000062A4 File Offset: 0x000044A4
		[NativeMethod(Name = "GetInputStream", IsThreadSafe = true)]
		private AnimationStream InternalGetInputStream(int index)
		{
			AnimationStream animationStream;
			AnimationStream.InternalGetInputStream_Injected(ref this, index, out animationStream);
			return animationStream;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000062BB File Offset: 0x000044BB
		[NativeMethod(Name = "GetInputWeight", IsThreadSafe = true)]
		private float InternalGetInputWeight(int index)
		{
			return AnimationStream.InternalGetInputWeight_Injected(ref this, index);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000062C4 File Offset: 0x000044C4
		[NativeMethod(IsThreadSafe = true)]
		private AnimationHumanStream GetHumanStream()
		{
			AnimationHumanStream animationHumanStream;
			AnimationStream.GetHumanStream_Injected(ref this, out animationHumanStream);
			return animationHumanStream;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000062DA File Offset: 0x000044DA
		[NativeMethod(Name = "ReadSceneTransforms", IsThreadSafe = true)]
		private void InternalReadSceneTransforms()
		{
			AnimationStream.InternalReadSceneTransforms_Injected(ref this);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000062E2 File Offset: 0x000044E2
		[NativeMethod(Name = "WriteSceneTransforms", IsThreadSafe = true)]
		private void InternalWriteSceneTransforms()
		{
			AnimationStream.InternalWriteSceneTransforms_Injected(ref this);
		}

		// Token: 0x0600042A RID: 1066
		[MethodImpl(4096)]
		private static extern void CopyAnimationStreamMotionInternal_Injected(ref AnimationStream _unity_self, ref AnimationStream animationStream);

		// Token: 0x0600042B RID: 1067
		[MethodImpl(4096)]
		private static extern float GetDeltaTime_Injected(ref AnimationStream _unity_self);

		// Token: 0x0600042C RID: 1068
		[MethodImpl(4096)]
		private static extern bool GetIsHumanStream_Injected(ref AnimationStream _unity_self);

		// Token: 0x0600042D RID: 1069
		[MethodImpl(4096)]
		private static extern void GetVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x0600042E RID: 1070
		[MethodImpl(4096)]
		private static extern void SetVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		// Token: 0x0600042F RID: 1071
		[MethodImpl(4096)]
		private static extern void GetAngularVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x06000430 RID: 1072
		[MethodImpl(4096)]
		private static extern void SetAngularVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		// Token: 0x06000431 RID: 1073
		[MethodImpl(4096)]
		private static extern void GetRootMotionPosition_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x06000432 RID: 1074
		[MethodImpl(4096)]
		private static extern void GetRootMotionRotation_Injected(ref AnimationStream _unity_self, out Quaternion ret);

		// Token: 0x06000433 RID: 1075
		[MethodImpl(4096)]
		private static extern int GetInputStreamCount_Injected(ref AnimationStream _unity_self);

		// Token: 0x06000434 RID: 1076
		[MethodImpl(4096)]
		private static extern void InternalGetInputStream_Injected(ref AnimationStream _unity_self, int index, out AnimationStream ret);

		// Token: 0x06000435 RID: 1077
		[MethodImpl(4096)]
		private static extern float InternalGetInputWeight_Injected(ref AnimationStream _unity_self, int index);

		// Token: 0x06000436 RID: 1078
		[MethodImpl(4096)]
		private static extern void GetHumanStream_Injected(ref AnimationStream _unity_self, out AnimationHumanStream ret);

		// Token: 0x06000437 RID: 1079
		[MethodImpl(4096)]
		private static extern void InternalReadSceneTransforms_Injected(ref AnimationStream _unity_self);

		// Token: 0x06000438 RID: 1080
		[MethodImpl(4096)]
		private static extern void InternalWriteSceneTransforms_Injected(ref AnimationStream _unity_self);

		// Token: 0x0400015A RID: 346
		private uint m_AnimatorBindingsVersion;

		// Token: 0x0400015B RID: 347
		private IntPtr constant;

		// Token: 0x0400015C RID: 348
		private IntPtr input;

		// Token: 0x0400015D RID: 349
		private IntPtr output;

		// Token: 0x0400015E RID: 350
		private IntPtr workspace;

		// Token: 0x0400015F RID: 351
		private IntPtr inputStreamAccessor;

		// Token: 0x04000160 RID: 352
		private IntPtr animationHandleBinder;

		// Token: 0x04000161 RID: 353
		internal const int InvalidIndex = -1;
	}
}
