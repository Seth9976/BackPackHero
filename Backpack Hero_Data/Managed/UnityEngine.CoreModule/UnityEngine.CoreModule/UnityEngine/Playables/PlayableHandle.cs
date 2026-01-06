using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000444 RID: 1092
	[UsedByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Export/Director/PlayableHandle.bindings.h")]
	public struct PlayableHandle : IEquatable<PlayableHandle>
	{
		// Token: 0x06002619 RID: 9753 RVA: 0x0003FEC0 File Offset: 0x0003E0C0
		internal T GetObject<T>() where T : class, IPlayableBehaviour
		{
			bool flag = !this.IsValid();
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				object scriptInstance = this.GetScriptInstance();
				bool flag2 = scriptInstance == null;
				if (flag2)
				{
					t = default(T);
				}
				else
				{
					t = (T)((object)scriptInstance);
				}
			}
			return t;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x0003FF10 File Offset: 0x0003E110
		[VisibleToOtherModules]
		internal bool IsPlayableOfType<T>()
		{
			return this.GetPlayableType() == typeof(T);
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x0003FF34 File Offset: 0x0003E134
		public static PlayableHandle Null
		{
			get
			{
				return PlayableHandle.m_Null;
			}
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x0003FF4C File Offset: 0x0003E14C
		internal Playable GetInput(int inputPort)
		{
			return new Playable(this.GetInputHandle(inputPort));
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x0003FF6C File Offset: 0x0003E16C
		internal Playable GetOutput(int outputPort)
		{
			return new Playable(this.GetOutputHandle(outputPort));
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x0003FF8C File Offset: 0x0003E18C
		internal bool SetInputWeight(int inputIndex, float weight)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			bool flag2;
			if (flag)
			{
				this.SetInputWeightFromIndex(inputIndex, weight);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x0003FFB8 File Offset: 0x0003E1B8
		internal float GetInputWeight(int inputIndex)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			float num;
			if (flag)
			{
				num = this.GetInputWeightFromIndex(inputIndex);
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
		internal void Destroy()
		{
			this.GetGraph().DestroyPlayable<Playable>(new Playable(this));
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x00040010 File Offset: 0x0003E210
		public static bool operator ==(PlayableHandle x, PlayableHandle y)
		{
			return PlayableHandle.CompareVersion(x, y);
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x0004002C File Offset: 0x0003E22C
		public static bool operator !=(PlayableHandle x, PlayableHandle y)
		{
			return !PlayableHandle.CompareVersion(x, y);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00040048 File Offset: 0x0003E248
		public override bool Equals(object p)
		{
			return p is PlayableHandle && this.Equals((PlayableHandle)p);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00040074 File Offset: 0x0003E274
		public bool Equals(PlayableHandle other)
		{
			return PlayableHandle.CompareVersion(this, other);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x00040094 File Offset: 0x0003E294
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000400C0 File Offset: 0x0003E2C0
		internal static bool CompareVersion(PlayableHandle lhs, PlayableHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000400F8 File Offset: 0x0003E2F8
		internal bool CheckInputBounds(int inputIndex)
		{
			return this.CheckInputBounds(inputIndex, false);
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x00040114 File Offset: 0x0003E314
		internal bool CheckInputBounds(int inputIndex, bool acceptAny)
		{
			bool flag = inputIndex == -1 && acceptAny;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = inputIndex < 0;
				if (flag3)
				{
					throw new IndexOutOfRangeException("Index must be greater than 0");
				}
				bool flag4 = this.GetInputCount() <= inputIndex;
				if (flag4)
				{
					throw new IndexOutOfRangeException(string.Concat(new string[]
					{
						"inputIndex ",
						inputIndex.ToString(),
						" is greater than the number of available inputs (",
						this.GetInputCount().ToString(),
						")."
					}));
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0004019F File Offset: 0x0003E39F
		[VisibleToOtherModules]
		internal bool IsNull()
		{
			return PlayableHandle.IsNull_Injected(ref this);
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000401A7 File Offset: 0x0003E3A7
		[VisibleToOtherModules]
		internal bool IsValid()
		{
			return PlayableHandle.IsValid_Injected(ref this);
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000401AF File Offset: 0x0003E3AF
		[FreeFunction("PlayableHandleBindings::GetPlayableType", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal Type GetPlayableType()
		{
			return PlayableHandle.GetPlayableType_Injected(ref this);
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000401B7 File Offset: 0x0003E3B7
		[FreeFunction("PlayableHandleBindings::GetJobType", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal Type GetJobType()
		{
			return PlayableHandle.GetJobType_Injected(ref this);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000401BF File Offset: 0x0003E3BF
		[FreeFunction("PlayableHandleBindings::SetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetScriptInstance(object scriptInstance)
		{
			PlayableHandle.SetScriptInstance_Injected(ref this, scriptInstance);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000401C8 File Offset: 0x0003E3C8
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::CanChangeInputs", HasExplicitThis = true, ThrowsException = true)]
		internal bool CanChangeInputs()
		{
			return PlayableHandle.CanChangeInputs_Injected(ref this);
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000401D0 File Offset: 0x0003E3D0
		[FreeFunction("PlayableHandleBindings::CanSetWeights", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool CanSetWeights()
		{
			return PlayableHandle.CanSetWeights_Injected(ref this);
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000401D8 File Offset: 0x0003E3D8
		[FreeFunction("PlayableHandleBindings::CanDestroy", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool CanDestroy()
		{
			return PlayableHandle.CanDestroy_Injected(ref this);
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000401E0 File Offset: 0x0003E3E0
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetPlayState", HasExplicitThis = true, ThrowsException = true)]
		internal PlayState GetPlayState()
		{
			return PlayableHandle.GetPlayState_Injected(ref this);
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000401E8 File Offset: 0x0003E3E8
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::Play", HasExplicitThis = true, ThrowsException = true)]
		internal void Play()
		{
			PlayableHandle.Play_Injected(ref this);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000401F0 File Offset: 0x0003E3F0
		[FreeFunction("PlayableHandleBindings::Pause", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void Pause()
		{
			PlayableHandle.Pause_Injected(ref this);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x000401F8 File Offset: 0x0003E3F8
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetSpeed", HasExplicitThis = true, ThrowsException = true)]
		internal double GetSpeed()
		{
			return PlayableHandle.GetSpeed_Injected(ref this);
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x00040200 File Offset: 0x0003E400
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetSpeed", HasExplicitThis = true, ThrowsException = true)]
		internal void SetSpeed(double value)
		{
			PlayableHandle.SetSpeed_Injected(ref this, value);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00040209 File Offset: 0x0003E409
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetTime", HasExplicitThis = true, ThrowsException = true)]
		internal double GetTime()
		{
			return PlayableHandle.GetTime_Injected(ref this);
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00040211 File Offset: 0x0003E411
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetTime", HasExplicitThis = true, ThrowsException = true)]
		internal void SetTime(double value)
		{
			PlayableHandle.SetTime_Injected(ref this, value);
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x0004021A File Offset: 0x0003E41A
		[FreeFunction("PlayableHandleBindings::IsDone", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool IsDone()
		{
			return PlayableHandle.IsDone_Injected(ref this);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00040222 File Offset: 0x0003E422
		[FreeFunction("PlayableHandleBindings::SetDone", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetDone(bool value)
		{
			PlayableHandle.SetDone_Injected(ref this, value);
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x0004022B File Offset: 0x0003E42B
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetDuration", HasExplicitThis = true, ThrowsException = true)]
		internal double GetDuration()
		{
			return PlayableHandle.GetDuration_Injected(ref this);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00040233 File Offset: 0x0003E433
		[FreeFunction("PlayableHandleBindings::SetDuration", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetDuration(double value)
		{
			PlayableHandle.SetDuration_Injected(ref this, value);
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x0004023C File Offset: 0x0003E43C
		[FreeFunction("PlayableHandleBindings::GetPropagateSetTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool GetPropagateSetTime()
		{
			return PlayableHandle.GetPropagateSetTime_Injected(ref this);
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00040244 File Offset: 0x0003E444
		[FreeFunction("PlayableHandleBindings::SetPropagateSetTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetPropagateSetTime(bool value)
		{
			PlayableHandle.SetPropagateSetTime_Injected(ref this, value);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x00040250 File Offset: 0x0003E450
		[FreeFunction("PlayableHandleBindings::GetGraph", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal PlayableGraph GetGraph()
		{
			PlayableGraph playableGraph;
			PlayableHandle.GetGraph_Injected(ref this, out playableGraph);
			return playableGraph;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00040266 File Offset: 0x0003E466
		[FreeFunction("PlayableHandleBindings::GetInputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal int GetInputCount()
		{
			return PlayableHandle.GetInputCount_Injected(ref this);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x0004026E File Offset: 0x0003E46E
		[FreeFunction("PlayableHandleBindings::SetInputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetInputCount(int value)
		{
			PlayableHandle.SetInputCount_Injected(ref this, value);
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00040277 File Offset: 0x0003E477
		[FreeFunction("PlayableHandleBindings::GetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal int GetOutputCount()
		{
			return PlayableHandle.GetOutputCount_Injected(ref this);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x0004027F File Offset: 0x0003E47F
		[FreeFunction("PlayableHandleBindings::SetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetOutputCount(int value)
		{
			PlayableHandle.SetOutputCount_Injected(ref this, value);
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00040288 File Offset: 0x0003E488
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetInputWeight", HasExplicitThis = true, ThrowsException = true)]
		internal void SetInputWeight(PlayableHandle input, float weight)
		{
			PlayableHandle.SetInputWeight_Injected(ref this, ref input, weight);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00040293 File Offset: 0x0003E493
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetDelay", HasExplicitThis = true, ThrowsException = true)]
		internal void SetDelay(double delay)
		{
			PlayableHandle.SetDelay_Injected(ref this, delay);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0004029C File Offset: 0x0003E49C
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetDelay", HasExplicitThis = true, ThrowsException = true)]
		internal double GetDelay()
		{
			return PlayableHandle.GetDelay_Injected(ref this);
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000402A4 File Offset: 0x0003E4A4
		[FreeFunction("PlayableHandleBindings::IsDelayed", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool IsDelayed()
		{
			return PlayableHandle.IsDelayed_Injected(ref this);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000402AC File Offset: 0x0003E4AC
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetPreviousTime", HasExplicitThis = true, ThrowsException = true)]
		internal double GetPreviousTime()
		{
			return PlayableHandle.GetPreviousTime_Injected(ref this);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000402B4 File Offset: 0x0003E4B4
		[FreeFunction("PlayableHandleBindings::SetLeadTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetLeadTime(float value)
		{
			PlayableHandle.SetLeadTime_Injected(ref this, value);
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000402BD File Offset: 0x0003E4BD
		[FreeFunction("PlayableHandleBindings::GetLeadTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal float GetLeadTime()
		{
			return PlayableHandle.GetLeadTime_Injected(ref this);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000402C5 File Offset: 0x0003E4C5
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetTraversalMode", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableTraversalMode GetTraversalMode()
		{
			return PlayableHandle.GetTraversalMode_Injected(ref this);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000402CD File Offset: 0x0003E4CD
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetTraversalMode", HasExplicitThis = true, ThrowsException = true)]
		internal void SetTraversalMode(PlayableTraversalMode mode)
		{
			PlayableHandle.SetTraversalMode_Injected(ref this, mode);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000402D6 File Offset: 0x0003E4D6
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetJobData", HasExplicitThis = true, ThrowsException = true)]
		internal IntPtr GetJobData()
		{
			return PlayableHandle.GetJobData_Injected(ref this);
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000402DE File Offset: 0x0003E4DE
		[FreeFunction("PlayableHandleBindings::GetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal DirectorWrapMode GetTimeWrapMode()
		{
			return PlayableHandle.GetTimeWrapMode_Injected(ref this);
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000402E6 File Offset: 0x0003E4E6
		[FreeFunction("PlayableHandleBindings::SetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetTimeWrapMode(DirectorWrapMode mode)
		{
			PlayableHandle.SetTimeWrapMode_Injected(ref this, mode);
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000402EF File Offset: 0x0003E4EF
		[FreeFunction("PlayableHandleBindings::GetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		private object GetScriptInstance()
		{
			return PlayableHandle.GetScriptInstance_Injected(ref this);
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000402F8 File Offset: 0x0003E4F8
		[FreeFunction("PlayableHandleBindings::GetInputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetInputHandle(int index)
		{
			PlayableHandle playableHandle;
			PlayableHandle.GetInputHandle_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x00040310 File Offset: 0x0003E510
		[FreeFunction("PlayableHandleBindings::GetOutputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetOutputHandle(int index)
		{
			PlayableHandle playableHandle;
			PlayableHandle.GetOutputHandle_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x00040327 File Offset: 0x0003E527
		[FreeFunction("PlayableHandleBindings::SetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		private void SetInputWeightFromIndex(int index, float weight)
		{
			PlayableHandle.SetInputWeightFromIndex_Injected(ref this, index, weight);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x00040331 File Offset: 0x0003E531
		[FreeFunction("PlayableHandleBindings::GetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		private float GetInputWeightFromIndex(int index)
		{
			return PlayableHandle.GetInputWeightFromIndex_Injected(ref this, index);
		}

		// Token: 0x06002655 RID: 9813
		[MethodImpl(4096)]
		private static extern bool IsNull_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002656 RID: 9814
		[MethodImpl(4096)]
		private static extern bool IsValid_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002657 RID: 9815
		[MethodImpl(4096)]
		private static extern Type GetPlayableType_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002658 RID: 9816
		[MethodImpl(4096)]
		private static extern Type GetJobType_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002659 RID: 9817
		[MethodImpl(4096)]
		private static extern void SetScriptInstance_Injected(ref PlayableHandle _unity_self, object scriptInstance);

		// Token: 0x0600265A RID: 9818
		[MethodImpl(4096)]
		private static extern bool CanChangeInputs_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600265B RID: 9819
		[MethodImpl(4096)]
		private static extern bool CanSetWeights_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600265C RID: 9820
		[MethodImpl(4096)]
		private static extern bool CanDestroy_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600265D RID: 9821
		[MethodImpl(4096)]
		private static extern PlayState GetPlayState_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600265E RID: 9822
		[MethodImpl(4096)]
		private static extern void Play_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600265F RID: 9823
		[MethodImpl(4096)]
		private static extern void Pause_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002660 RID: 9824
		[MethodImpl(4096)]
		private static extern double GetSpeed_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002661 RID: 9825
		[MethodImpl(4096)]
		private static extern void SetSpeed_Injected(ref PlayableHandle _unity_self, double value);

		// Token: 0x06002662 RID: 9826
		[MethodImpl(4096)]
		private static extern double GetTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002663 RID: 9827
		[MethodImpl(4096)]
		private static extern void SetTime_Injected(ref PlayableHandle _unity_self, double value);

		// Token: 0x06002664 RID: 9828
		[MethodImpl(4096)]
		private static extern bool IsDone_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002665 RID: 9829
		[MethodImpl(4096)]
		private static extern void SetDone_Injected(ref PlayableHandle _unity_self, bool value);

		// Token: 0x06002666 RID: 9830
		[MethodImpl(4096)]
		private static extern double GetDuration_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002667 RID: 9831
		[MethodImpl(4096)]
		private static extern void SetDuration_Injected(ref PlayableHandle _unity_self, double value);

		// Token: 0x06002668 RID: 9832
		[MethodImpl(4096)]
		private static extern bool GetPropagateSetTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002669 RID: 9833
		[MethodImpl(4096)]
		private static extern void SetPropagateSetTime_Injected(ref PlayableHandle _unity_self, bool value);

		// Token: 0x0600266A RID: 9834
		[MethodImpl(4096)]
		private static extern void GetGraph_Injected(ref PlayableHandle _unity_self, out PlayableGraph ret);

		// Token: 0x0600266B RID: 9835
		[MethodImpl(4096)]
		private static extern int GetInputCount_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600266C RID: 9836
		[MethodImpl(4096)]
		private static extern void SetInputCount_Injected(ref PlayableHandle _unity_self, int value);

		// Token: 0x0600266D RID: 9837
		[MethodImpl(4096)]
		private static extern int GetOutputCount_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600266E RID: 9838
		[MethodImpl(4096)]
		private static extern void SetOutputCount_Injected(ref PlayableHandle _unity_self, int value);

		// Token: 0x0600266F RID: 9839
		[MethodImpl(4096)]
		private static extern void SetInputWeight_Injected(ref PlayableHandle _unity_self, ref PlayableHandle input, float weight);

		// Token: 0x06002670 RID: 9840
		[MethodImpl(4096)]
		private static extern void SetDelay_Injected(ref PlayableHandle _unity_self, double delay);

		// Token: 0x06002671 RID: 9841
		[MethodImpl(4096)]
		private static extern double GetDelay_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002672 RID: 9842
		[MethodImpl(4096)]
		private static extern bool IsDelayed_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002673 RID: 9843
		[MethodImpl(4096)]
		private static extern double GetPreviousTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002674 RID: 9844
		[MethodImpl(4096)]
		private static extern void SetLeadTime_Injected(ref PlayableHandle _unity_self, float value);

		// Token: 0x06002675 RID: 9845
		[MethodImpl(4096)]
		private static extern float GetLeadTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002676 RID: 9846
		[MethodImpl(4096)]
		private static extern PlayableTraversalMode GetTraversalMode_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002677 RID: 9847
		[MethodImpl(4096)]
		private static extern void SetTraversalMode_Injected(ref PlayableHandle _unity_self, PlayableTraversalMode mode);

		// Token: 0x06002678 RID: 9848
		[MethodImpl(4096)]
		private static extern IntPtr GetJobData_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002679 RID: 9849
		[MethodImpl(4096)]
		private static extern DirectorWrapMode GetTimeWrapMode_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600267A RID: 9850
		[MethodImpl(4096)]
		private static extern void SetTimeWrapMode_Injected(ref PlayableHandle _unity_self, DirectorWrapMode mode);

		// Token: 0x0600267B RID: 9851
		[MethodImpl(4096)]
		private static extern object GetScriptInstance_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600267C RID: 9852
		[MethodImpl(4096)]
		private static extern void GetInputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		// Token: 0x0600267D RID: 9853
		[MethodImpl(4096)]
		private static extern void GetOutputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		// Token: 0x0600267E RID: 9854
		[MethodImpl(4096)]
		private static extern void SetInputWeightFromIndex_Injected(ref PlayableHandle _unity_self, int index, float weight);

		// Token: 0x0600267F RID: 9855
		[MethodImpl(4096)]
		private static extern float GetInputWeightFromIndex_Injected(ref PlayableHandle _unity_self, int index);

		// Token: 0x04000E22 RID: 3618
		internal IntPtr m_Handle;

		// Token: 0x04000E23 RID: 3619
		internal uint m_Version;

		// Token: 0x04000E24 RID: 3620
		private static readonly PlayableHandle m_Null = default(PlayableHandle);
	}
}
