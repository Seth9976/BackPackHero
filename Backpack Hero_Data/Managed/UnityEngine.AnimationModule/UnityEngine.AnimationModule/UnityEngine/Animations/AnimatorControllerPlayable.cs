using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200005F RID: 95
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animator.bindings.h")]
	[RequiredByNativeCode]
	[StaticAccessor("AnimatorControllerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	[NativeHeader("Modules/Animation/Director/AnimatorControllerPlayable.h")]
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	public struct AnimatorControllerPlayable : IPlayable, IEquatable<AnimatorControllerPlayable>
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000071E4 File Offset: 0x000053E4
		public static AnimatorControllerPlayable Null
		{
			get
			{
				return AnimatorControllerPlayable.m_NullPlayable;
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000071FC File Offset: 0x000053FC
		public static AnimatorControllerPlayable Create(PlayableGraph graph, RuntimeAnimatorController controller)
		{
			PlayableHandle playableHandle = AnimatorControllerPlayable.CreateHandle(graph, controller);
			return new AnimatorControllerPlayable(playableHandle);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000721C File Offset: 0x0000541C
		private static PlayableHandle CreateHandle(PlayableGraph graph, RuntimeAnimatorController controller)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimatorControllerPlayable.CreateHandleInternal(graph, controller, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000724D File Offset: 0x0000544D
		internal AnimatorControllerPlayable(PlayableHandle handle)
		{
			this.m_Handle = PlayableHandle.Null;
			this.SetHandle(handle);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00007264 File Offset: 0x00005464
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000727C File Offset: 0x0000547C
		public void SetHandle(PlayableHandle handle)
		{
			bool flag = this.m_Handle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Cannot call IPlayable.SetHandle on an instance that already contains a valid handle.");
			}
			bool flag2 = handle.IsValid();
			if (flag2)
			{
				bool flag3 = !handle.IsPlayableOfType<AnimatorControllerPlayable>();
				if (flag3)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimatorControllerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000072D4 File Offset: 0x000054D4
		public static implicit operator Playable(AnimatorControllerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000072F4 File Offset: 0x000054F4
		public static explicit operator AnimatorControllerPlayable(Playable playable)
		{
			return new AnimatorControllerPlayable(playable.GetHandle());
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00007314 File Offset: 0x00005514
		public bool Equals(AnimatorControllerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00007338 File Offset: 0x00005538
		public float GetFloat(string name)
		{
			return AnimatorControllerPlayable.GetFloatString(ref this.m_Handle, name);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00007358 File Offset: 0x00005558
		public float GetFloat(int id)
		{
			return AnimatorControllerPlayable.GetFloatID(ref this.m_Handle, id);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00007376 File Offset: 0x00005576
		public void SetFloat(string name, float value)
		{
			AnimatorControllerPlayable.SetFloatString(ref this.m_Handle, name, value);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00007387 File Offset: 0x00005587
		public void SetFloat(int id, float value)
		{
			AnimatorControllerPlayable.SetFloatID(ref this.m_Handle, id, value);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00007398 File Offset: 0x00005598
		public bool GetBool(string name)
		{
			return AnimatorControllerPlayable.GetBoolString(ref this.m_Handle, name);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000073B8 File Offset: 0x000055B8
		public bool GetBool(int id)
		{
			return AnimatorControllerPlayable.GetBoolID(ref this.m_Handle, id);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000073D6 File Offset: 0x000055D6
		public void SetBool(string name, bool value)
		{
			AnimatorControllerPlayable.SetBoolString(ref this.m_Handle, name, value);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000073E7 File Offset: 0x000055E7
		public void SetBool(int id, bool value)
		{
			AnimatorControllerPlayable.SetBoolID(ref this.m_Handle, id, value);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000073F8 File Offset: 0x000055F8
		public int GetInteger(string name)
		{
			return AnimatorControllerPlayable.GetIntegerString(ref this.m_Handle, name);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00007418 File Offset: 0x00005618
		public int GetInteger(int id)
		{
			return AnimatorControllerPlayable.GetIntegerID(ref this.m_Handle, id);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00007436 File Offset: 0x00005636
		public void SetInteger(string name, int value)
		{
			AnimatorControllerPlayable.SetIntegerString(ref this.m_Handle, name, value);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00007447 File Offset: 0x00005647
		public void SetInteger(int id, int value)
		{
			AnimatorControllerPlayable.SetIntegerID(ref this.m_Handle, id, value);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00007458 File Offset: 0x00005658
		public void SetTrigger(string name)
		{
			AnimatorControllerPlayable.SetTriggerString(ref this.m_Handle, name);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00007468 File Offset: 0x00005668
		public void SetTrigger(int id)
		{
			AnimatorControllerPlayable.SetTriggerID(ref this.m_Handle, id);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00007478 File Offset: 0x00005678
		public void ResetTrigger(string name)
		{
			AnimatorControllerPlayable.ResetTriggerString(ref this.m_Handle, name);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00007488 File Offset: 0x00005688
		public void ResetTrigger(int id)
		{
			AnimatorControllerPlayable.ResetTriggerID(ref this.m_Handle, id);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00007498 File Offset: 0x00005698
		public bool IsParameterControlledByCurve(string name)
		{
			return AnimatorControllerPlayable.IsParameterControlledByCurveString(ref this.m_Handle, name);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000074B8 File Offset: 0x000056B8
		public bool IsParameterControlledByCurve(int id)
		{
			return AnimatorControllerPlayable.IsParameterControlledByCurveID(ref this.m_Handle, id);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000074D8 File Offset: 0x000056D8
		public int GetLayerCount()
		{
			return AnimatorControllerPlayable.GetLayerCountInternal(ref this.m_Handle);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000074F8 File Offset: 0x000056F8
		public string GetLayerName(int layerIndex)
		{
			return AnimatorControllerPlayable.GetLayerNameInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00007518 File Offset: 0x00005718
		public int GetLayerIndex(string layerName)
		{
			return AnimatorControllerPlayable.GetLayerIndexInternal(ref this.m_Handle, layerName);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00007538 File Offset: 0x00005738
		public float GetLayerWeight(int layerIndex)
		{
			return AnimatorControllerPlayable.GetLayerWeightInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00007556 File Offset: 0x00005756
		public void SetLayerWeight(int layerIndex, float weight)
		{
			AnimatorControllerPlayable.SetLayerWeightInternal(ref this.m_Handle, layerIndex, weight);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00007568 File Offset: 0x00005768
		public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetCurrentAnimatorStateInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00007588 File Offset: 0x00005788
		public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetNextAnimatorStateInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000075A8 File Offset: 0x000057A8
		public AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetAnimatorTransitionInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000075C8 File Offset: 0x000057C8
		public AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetCurrentAnimatorClipInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000075E8 File Offset: 0x000057E8
		public void GetCurrentAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			AnimatorControllerPlayable.GetAnimatorClipInfoInternal(ref this.m_Handle, layerIndex, true, clips);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00007618 File Offset: 0x00005818
		public void GetNextAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			AnimatorControllerPlayable.GetAnimatorClipInfoInternal(ref this.m_Handle, layerIndex, false, clips);
		}

		// Token: 0x0600050A RID: 1290
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void GetAnimatorClipInfoInternal(ref PlayableHandle handle, int layerIndex, bool isCurrent, object clips);

		// Token: 0x0600050B RID: 1291 RVA: 0x00007648 File Offset: 0x00005848
		public int GetCurrentAnimatorClipInfoCount(int layerIndex)
		{
			return AnimatorControllerPlayable.GetAnimatorClipInfoCountInternal(ref this.m_Handle, layerIndex, true);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00007668 File Offset: 0x00005868
		public int GetNextAnimatorClipInfoCount(int layerIndex)
		{
			return AnimatorControllerPlayable.GetAnimatorClipInfoCountInternal(ref this.m_Handle, layerIndex, false);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00007688 File Offset: 0x00005888
		public AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetNextAnimatorClipInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000076A8 File Offset: 0x000058A8
		public bool IsInTransition(int layerIndex)
		{
			return AnimatorControllerPlayable.IsInTransitionInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000076C8 File Offset: 0x000058C8
		public int GetParameterCount()
		{
			return AnimatorControllerPlayable.GetParameterCountInternal(ref this.m_Handle);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000076E8 File Offset: 0x000058E8
		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter[] parametersArrayInternal = AnimatorControllerPlayable.GetParametersArrayInternal(ref this.m_Handle);
			bool flag = index < 0 || index >= parametersArrayInternal.Length;
			if (flag)
			{
				throw new IndexOutOfRangeException("Invalid parameter index.");
			}
			return parametersArrayInternal[index];
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00007728 File Offset: 0x00005928
		public void CrossFadeInFixedTime(string stateName, float transitionDuration)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, -1, 0f);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00007744 File Offset: 0x00005944
		public void CrossFadeInFixedTime(string stateName, float transitionDuration, int layer)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, 0f);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00007760 File Offset: 0x00005960
		public void CrossFadeInFixedTime(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTime)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, fixedTime);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00007779 File Offset: 0x00005979
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, stateNameHash, transitionDuration, -1, 0f);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00007790 File Offset: 0x00005990
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, int layer)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, stateNameHash, transitionDuration, layer, 0f);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000077A7 File Offset: 0x000059A7
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTime)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, stateNameHash, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000077BB File Offset: 0x000059BB
		public void CrossFade(string stateName, float transitionDuration)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, -1, float.NegativeInfinity);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000077D7 File Offset: 0x000059D7
		public void CrossFade(string stateName, float transitionDuration, int layer)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, float.NegativeInfinity);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000077F3 File Offset: 0x000059F3
		public void CrossFade(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, normalizedTime);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000780C File Offset: 0x00005A0C
		public void CrossFade(int stateNameHash, float transitionDuration)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, stateNameHash, transitionDuration, -1, float.NegativeInfinity);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00007823 File Offset: 0x00005A23
		public void CrossFade(int stateNameHash, float transitionDuration, int layer)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, stateNameHash, transitionDuration, layer, float.NegativeInfinity);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000783A File Offset: 0x00005A3A
		public void CrossFade(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, stateNameHash, transitionDuration, layer, normalizedTime);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000784E File Offset: 0x00005A4E
		public void PlayInFixedTime(string stateName)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), -1, float.NegativeInfinity);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00007869 File Offset: 0x00005A69
		public void PlayInFixedTime(string stateName, int layer)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), layer, float.NegativeInfinity);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00007884 File Offset: 0x00005A84
		public void PlayInFixedTime(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), layer, fixedTime);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000789B File Offset: 0x00005A9B
		public void PlayInFixedTime(int stateNameHash)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, stateNameHash, -1, float.NegativeInfinity);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000078B1 File Offset: 0x00005AB1
		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, stateNameHash, layer, float.NegativeInfinity);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000078C7 File Offset: 0x00005AC7
		public void PlayInFixedTime(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, stateNameHash, layer, fixedTime);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000078D9 File Offset: 0x00005AD9
		public void Play(string stateName)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), -1, float.NegativeInfinity);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000078F4 File Offset: 0x00005AF4
		public void Play(string stateName, int layer)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), layer, float.NegativeInfinity);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000790F File Offset: 0x00005B0F
		public void Play(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), layer, normalizedTime);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00007926 File Offset: 0x00005B26
		public void Play(int stateNameHash)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, stateNameHash, -1, float.NegativeInfinity);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000793C File Offset: 0x00005B3C
		public void Play(int stateNameHash, int layer)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, stateNameHash, layer, float.NegativeInfinity);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00007952 File Offset: 0x00005B52
		public void Play(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, stateNameHash, layer, normalizedTime);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00007964 File Offset: 0x00005B64
		public bool HasState(int layerIndex, int stateID)
		{
			return AnimatorControllerPlayable.HasStateInternal(ref this.m_Handle, layerIndex, stateID);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00007984 File Offset: 0x00005B84
		internal string ResolveHash(int hash)
		{
			return AnimatorControllerPlayable.ResolveHashInternal(ref this.m_Handle, hash);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000079A2 File Offset: 0x00005BA2
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, RuntimeAnimatorController controller, ref PlayableHandle handle)
		{
			return AnimatorControllerPlayable.CreateHandleInternal_Injected(ref graph, controller, ref handle);
		}

		// Token: 0x0600052C RID: 1324
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern RuntimeAnimatorController GetAnimatorControllerInternal(ref PlayableHandle handle);

		// Token: 0x0600052D RID: 1325
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int GetLayerCountInternal(ref PlayableHandle handle);

		// Token: 0x0600052E RID: 1326
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern string GetLayerNameInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x0600052F RID: 1327
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int GetLayerIndexInternal(ref PlayableHandle handle, string layerName);

		// Token: 0x06000530 RID: 1328
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern float GetLayerWeightInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x06000531 RID: 1329
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetLayerWeightInternal(ref PlayableHandle handle, int layerIndex, float weight);

		// Token: 0x06000532 RID: 1330 RVA: 0x000079B0 File Offset: 0x00005BB0
		[NativeThrows]
		private static AnimatorStateInfo GetCurrentAnimatorStateInfoInternal(ref PlayableHandle handle, int layerIndex)
		{
			AnimatorStateInfo animatorStateInfo;
			AnimatorControllerPlayable.GetCurrentAnimatorStateInfoInternal_Injected(ref handle, layerIndex, out animatorStateInfo);
			return animatorStateInfo;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000079C8 File Offset: 0x00005BC8
		[NativeThrows]
		private static AnimatorStateInfo GetNextAnimatorStateInfoInternal(ref PlayableHandle handle, int layerIndex)
		{
			AnimatorStateInfo animatorStateInfo;
			AnimatorControllerPlayable.GetNextAnimatorStateInfoInternal_Injected(ref handle, layerIndex, out animatorStateInfo);
			return animatorStateInfo;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000079E0 File Offset: 0x00005BE0
		[NativeThrows]
		private static AnimatorTransitionInfo GetAnimatorTransitionInfoInternal(ref PlayableHandle handle, int layerIndex)
		{
			AnimatorTransitionInfo animatorTransitionInfo;
			AnimatorControllerPlayable.GetAnimatorTransitionInfoInternal_Injected(ref handle, layerIndex, out animatorTransitionInfo);
			return animatorTransitionInfo;
		}

		// Token: 0x06000535 RID: 1333
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern AnimatorClipInfo[] GetCurrentAnimatorClipInfoInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x06000536 RID: 1334
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int GetAnimatorClipInfoCountInternal(ref PlayableHandle handle, int layerIndex, bool current);

		// Token: 0x06000537 RID: 1335
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern AnimatorClipInfo[] GetNextAnimatorClipInfoInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x06000538 RID: 1336
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern string ResolveHashInternal(ref PlayableHandle handle, int hash);

		// Token: 0x06000539 RID: 1337
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool IsInTransitionInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x0600053A RID: 1338
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern AnimatorControllerParameter[] GetParametersArrayInternal(ref PlayableHandle handle);

		// Token: 0x0600053B RID: 1339
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int GetParameterCountInternal(ref PlayableHandle handle);

		// Token: 0x0600053C RID: 1340
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern int StringToHash(string name);

		// Token: 0x0600053D RID: 1341
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void CrossFadeInFixedTimeInternal(ref PlayableHandle handle, int stateNameHash, float transitionDuration, int layer, float fixedTime);

		// Token: 0x0600053E RID: 1342
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void CrossFadeInternal(ref PlayableHandle handle, int stateNameHash, float transitionDuration, int layer, float normalizedTime);

		// Token: 0x0600053F RID: 1343
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void PlayInFixedTimeInternal(ref PlayableHandle handle, int stateNameHash, int layer, float fixedTime);

		// Token: 0x06000540 RID: 1344
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void PlayInternal(ref PlayableHandle handle, int stateNameHash, int layer, float normalizedTime);

		// Token: 0x06000541 RID: 1345
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool HasStateInternal(ref PlayableHandle handle, int layerIndex, int stateID);

		// Token: 0x06000542 RID: 1346
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetFloatString(ref PlayableHandle handle, string name, float value);

		// Token: 0x06000543 RID: 1347
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetFloatID(ref PlayableHandle handle, int id, float value);

		// Token: 0x06000544 RID: 1348
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern float GetFloatString(ref PlayableHandle handle, string name);

		// Token: 0x06000545 RID: 1349
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern float GetFloatID(ref PlayableHandle handle, int id);

		// Token: 0x06000546 RID: 1350
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetBoolString(ref PlayableHandle handle, string name, bool value);

		// Token: 0x06000547 RID: 1351
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetBoolID(ref PlayableHandle handle, int id, bool value);

		// Token: 0x06000548 RID: 1352
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetBoolString(ref PlayableHandle handle, string name);

		// Token: 0x06000549 RID: 1353
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetBoolID(ref PlayableHandle handle, int id);

		// Token: 0x0600054A RID: 1354
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetIntegerString(ref PlayableHandle handle, string name, int value);

		// Token: 0x0600054B RID: 1355
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetIntegerID(ref PlayableHandle handle, int id, int value);

		// Token: 0x0600054C RID: 1356
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int GetIntegerString(ref PlayableHandle handle, string name);

		// Token: 0x0600054D RID: 1357
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int GetIntegerID(ref PlayableHandle handle, int id);

		// Token: 0x0600054E RID: 1358
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetTriggerString(ref PlayableHandle handle, string name);

		// Token: 0x0600054F RID: 1359
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetTriggerID(ref PlayableHandle handle, int id);

		// Token: 0x06000550 RID: 1360
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void ResetTriggerString(ref PlayableHandle handle, string name);

		// Token: 0x06000551 RID: 1361
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void ResetTriggerID(ref PlayableHandle handle, int id);

		// Token: 0x06000552 RID: 1362
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool IsParameterControlledByCurveString(ref PlayableHandle handle, string name);

		// Token: 0x06000553 RID: 1363
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool IsParameterControlledByCurveID(ref PlayableHandle handle, int id);

		// Token: 0x06000555 RID: 1365
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, RuntimeAnimatorController controller, ref PlayableHandle handle);

		// Token: 0x06000556 RID: 1366
		[MethodImpl(4096)]
		private static extern void GetCurrentAnimatorStateInfoInternal_Injected(ref PlayableHandle handle, int layerIndex, out AnimatorStateInfo ret);

		// Token: 0x06000557 RID: 1367
		[MethodImpl(4096)]
		private static extern void GetNextAnimatorStateInfoInternal_Injected(ref PlayableHandle handle, int layerIndex, out AnimatorStateInfo ret);

		// Token: 0x06000558 RID: 1368
		[MethodImpl(4096)]
		private static extern void GetAnimatorTransitionInfoInternal_Injected(ref PlayableHandle handle, int layerIndex, out AnimatorTransitionInfo ret);

		// Token: 0x04000175 RID: 373
		private PlayableHandle m_Handle;

		// Token: 0x04000176 RID: 374
		private static readonly AnimatorControllerPlayable m_NullPlayable = new AnimatorControllerPlayable(PlayableHandle.Null);
	}
}
