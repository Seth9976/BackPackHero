using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000440 RID: 1088
	public static class PlayableExtensions
	{
		// Token: 0x060025A4 RID: 9636 RVA: 0x0003F4F8 File Offset: 0x0003D6F8
		public static bool IsNull<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsNull();
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x0003F520 File Offset: 0x0003D720
		public static bool IsValid<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsValid();
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0003F548 File Offset: 0x0003D748
		public static void Destroy<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Destroy();
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0003F56C File Offset: 0x0003D76C
		public static PlayableGraph GetGraph<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetGraph();
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0003F594 File Offset: 0x0003D794
		[Obsolete("SetPlayState() has been deprecated. Use Play(), Pause() or SetDelay() instead", false)]
		public static void SetPlayState<U>(this U playable, PlayState value) where U : struct, IPlayable
		{
			bool flag = value == PlayState.Delayed;
			if (flag)
			{
				throw new ArgumentException("Can't set Delayed: use SetDelay() instead");
			}
			if (value != PlayState.Paused)
			{
				if (value == PlayState.Playing)
				{
					playable.GetHandle().Play();
				}
			}
			else
			{
				playable.GetHandle().Pause();
			}
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x0003F5F4 File Offset: 0x0003D7F4
		public static PlayState GetPlayState<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPlayState();
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x0003F61C File Offset: 0x0003D81C
		public static void Play<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Play();
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0003F640 File Offset: 0x0003D840
		public static void Pause<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Pause();
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0003F664 File Offset: 0x0003D864
		public static void SetSpeed<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetSpeed(value);
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x0003F68C File Offset: 0x0003D88C
		public static double GetSpeed<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetSpeed();
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x0003F6B4 File Offset: 0x0003D8B4
		public static void SetDuration<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetDuration(value);
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x0003F6DC File Offset: 0x0003D8DC
		public static double GetDuration<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetDuration();
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x0003F704 File Offset: 0x0003D904
		public static void SetTime<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTime(value);
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x0003F72C File Offset: 0x0003D92C
		public static double GetTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTime();
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0003F754 File Offset: 0x0003D954
		public static double GetPreviousTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPreviousTime();
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x0003F77C File Offset: 0x0003D97C
		public static void SetDone<U>(this U playable, bool value) where U : struct, IPlayable
		{
			playable.GetHandle().SetDone(value);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x0003F7A4 File Offset: 0x0003D9A4
		public static bool IsDone<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsDone();
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x0003F7CC File Offset: 0x0003D9CC
		public static void SetPropagateSetTime<U>(this U playable, bool value) where U : struct, IPlayable
		{
			playable.GetHandle().SetPropagateSetTime(value);
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		public static bool GetPropagateSetTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPropagateSetTime();
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0003F81C File Offset: 0x0003DA1C
		public static bool CanChangeInputs<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanChangeInputs();
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0003F844 File Offset: 0x0003DA44
		public static bool CanSetWeights<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanSetWeights();
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x0003F86C File Offset: 0x0003DA6C
		public static bool CanDestroy<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanDestroy();
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x0003F894 File Offset: 0x0003DA94
		public static void SetInputCount<U>(this U playable, int value) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputCount(value);
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x0003F8BC File Offset: 0x0003DABC
		public static int GetInputCount<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputCount();
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x0003F8E4 File Offset: 0x0003DAE4
		public static void SetOutputCount<U>(this U playable, int value) where U : struct, IPlayable
		{
			playable.GetHandle().SetOutputCount(value);
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x0003F90C File Offset: 0x0003DB0C
		public static int GetOutputCount<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetOutputCount();
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0003F934 File Offset: 0x0003DB34
		public static Playable GetInput<U>(this U playable, int inputPort) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInput(inputPort);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0003F95C File Offset: 0x0003DB5C
		public static Playable GetOutput<U>(this U playable, int outputPort) where U : struct, IPlayable
		{
			return playable.GetHandle().GetOutput(outputPort);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0003F984 File Offset: 0x0003DB84
		public static void SetInputWeight<U>(this U playable, int inputIndex, float weight) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(inputIndex, weight);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0003F9AC File Offset: 0x0003DBAC
		public static void SetInputWeight<U, V>(this U playable, V input, float weight) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(input.GetHandle(), weight);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x0003F9E0 File Offset: 0x0003DBE0
		public static float GetInputWeight<U>(this U playable, int inputIndex) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputWeight(inputIndex);
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0003FA08 File Offset: 0x0003DC08
		public static void ConnectInput<U, V>(this U playable, int inputIndex, V sourcePlayable, int sourceOutputIndex) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.ConnectInput(inputIndex, sourcePlayable, sourceOutputIndex, 0f);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0003FA1C File Offset: 0x0003DC1C
		public static void ConnectInput<U, V>(this U playable, int inputIndex, V sourcePlayable, int sourceOutputIndex, float weight) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.GetGraph<U>().Connect<V, U>(sourcePlayable, sourceOutputIndex, playable, inputIndex);
			playable.SetInputWeight(inputIndex, weight);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0003FA48 File Offset: 0x0003DC48
		public static void DisconnectInput<U>(this U playable, int inputPort) where U : struct, IPlayable
		{
			playable.GetGraph<U>().Disconnect<U>(playable, inputPort);
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0003FA68 File Offset: 0x0003DC68
		public static int AddInput<U, V>(this U playable, V sourcePlayable, int sourceOutputIndex, float weight = 0f) where U : struct, IPlayable where V : struct, IPlayable
		{
			int inputCount = playable.GetInputCount<U>();
			playable.SetInputCount(inputCount + 1);
			playable.ConnectInput(inputCount, sourcePlayable, sourceOutputIndex, weight);
			return inputCount;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0003FA98 File Offset: 0x0003DC98
		[Obsolete("SetDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static void SetDelay<U>(this U playable, double delay) where U : struct, IPlayable
		{
			playable.GetHandle().SetDelay(delay);
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0003FAC0 File Offset: 0x0003DCC0
		[Obsolete("GetDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static double GetDelay<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetDelay();
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0003FAE8 File Offset: 0x0003DCE8
		[Obsolete("IsDelayed is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static bool IsDelayed<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsDelayed();
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0003FB10 File Offset: 0x0003DD10
		public static void SetLeadTime<U>(this U playable, float value) where U : struct, IPlayable
		{
			playable.GetHandle().SetLeadTime(value);
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0003FB38 File Offset: 0x0003DD38
		public static float GetLeadTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetLeadTime();
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0003FB60 File Offset: 0x0003DD60
		public static PlayableTraversalMode GetTraversalMode<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTraversalMode();
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x0003FB88 File Offset: 0x0003DD88
		public static void SetTraversalMode<U>(this U playable, PlayableTraversalMode mode) where U : struct, IPlayable
		{
			playable.GetHandle().SetTraversalMode(mode);
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0003FBB0 File Offset: 0x0003DDB0
		internal static DirectorWrapMode GetTimeWrapMode<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTimeWrapMode();
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0003FBD8 File Offset: 0x0003DDD8
		internal static void SetTimeWrapMode<U>(this U playable, DirectorWrapMode value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTimeWrapMode(value);
		}
	}
}
