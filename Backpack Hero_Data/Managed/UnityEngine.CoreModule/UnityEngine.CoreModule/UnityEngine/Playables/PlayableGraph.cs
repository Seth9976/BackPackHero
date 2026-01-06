using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000442 RID: 1090
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Director/PlayableGraph.bindings.h")]
	public struct PlayableGraph
	{
		// Token: 0x060025D0 RID: 9680 RVA: 0x0003FC00 File Offset: 0x0003DE00
		public Playable GetRootPlayable(int index)
		{
			PlayableHandle rootPlayableInternal = this.GetRootPlayableInternal(index);
			return new Playable(rootPlayableInternal);
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x0003FC20 File Offset: 0x0003DE20
		public bool Connect<U, V>(U source, int sourceOutputPort, V destination, int destinationInputPort) where U : struct, IPlayable where V : struct, IPlayable
		{
			return this.ConnectInternal(source.GetHandle(), sourceOutputPort, destination.GetHandle(), destinationInputPort);
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0003FC55 File Offset: 0x0003DE55
		public void Disconnect<U>(U input, int inputPort) where U : struct, IPlayable
		{
			this.DisconnectInternal(input.GetHandle(), inputPort);
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0003FC6D File Offset: 0x0003DE6D
		public void DestroyPlayable<U>(U playable) where U : struct, IPlayable
		{
			this.DestroyPlayableInternal(playable.GetHandle());
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x0003FC84 File Offset: 0x0003DE84
		public void DestroySubgraph<U>(U playable) where U : struct, IPlayable
		{
			this.DestroySubgraphInternal(playable.GetHandle());
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x0003FC9B File Offset: 0x0003DE9B
		public void DestroyOutput<U>(U output) where U : struct, IPlayableOutput
		{
			this.DestroyOutputInternal(output.GetHandle());
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0003FCB4 File Offset: 0x0003DEB4
		public int GetOutputCountByType<T>() where T : struct, IPlayableOutput
		{
			return this.GetOutputCountByTypeInternal(typeof(T));
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x0003FCD8 File Offset: 0x0003DED8
		public PlayableOutput GetOutput(int index)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !this.GetOutputInternal(index, out playableOutputHandle);
			PlayableOutput playableOutput;
			if (flag)
			{
				playableOutput = PlayableOutput.Null;
			}
			else
			{
				playableOutput = new PlayableOutput(playableOutputHandle);
			}
			return playableOutput;
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0003FD08 File Offset: 0x0003DF08
		public PlayableOutput GetOutputByType<T>(int index) where T : struct, IPlayableOutput
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !this.GetOutputByTypeInternal(typeof(T), index, out playableOutputHandle);
			PlayableOutput playableOutput;
			if (flag)
			{
				playableOutput = PlayableOutput.Null;
			}
			else
			{
				playableOutput = new PlayableOutput(playableOutputHandle);
			}
			return playableOutput;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0003FD42 File Offset: 0x0003DF42
		public void Evaluate()
		{
			this.Evaluate(0f);
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0003FD54 File Offset: 0x0003DF54
		public static PlayableGraph Create()
		{
			return PlayableGraph.Create(null);
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0003FD6C File Offset: 0x0003DF6C
		public static PlayableGraph Create(string name)
		{
			PlayableGraph playableGraph;
			PlayableGraph.Create_Injected(name, out playableGraph);
			return playableGraph;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0003FD82 File Offset: 0x0003DF82
		[FreeFunction("PlayableGraphBindings::Destroy", HasExplicitThis = true, ThrowsException = true)]
		public void Destroy()
		{
			PlayableGraph.Destroy_Injected(ref this);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0003FD8A File Offset: 0x0003DF8A
		public bool IsValid()
		{
			return PlayableGraph.IsValid_Injected(ref this);
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0003FD92 File Offset: 0x0003DF92
		[FreeFunction("PlayableGraphBindings::IsPlaying", HasExplicitThis = true, ThrowsException = true)]
		public bool IsPlaying()
		{
			return PlayableGraph.IsPlaying_Injected(ref this);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0003FD9A File Offset: 0x0003DF9A
		[FreeFunction("PlayableGraphBindings::IsDone", HasExplicitThis = true, ThrowsException = true)]
		public bool IsDone()
		{
			return PlayableGraph.IsDone_Injected(ref this);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0003FDA2 File Offset: 0x0003DFA2
		[FreeFunction("PlayableGraphBindings::Play", HasExplicitThis = true, ThrowsException = true)]
		public void Play()
		{
			PlayableGraph.Play_Injected(ref this);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0003FDAA File Offset: 0x0003DFAA
		[FreeFunction("PlayableGraphBindings::Stop", HasExplicitThis = true, ThrowsException = true)]
		public void Stop()
		{
			PlayableGraph.Stop_Injected(ref this);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0003FDB2 File Offset: 0x0003DFB2
		[FreeFunction("PlayableGraphBindings::Evaluate", HasExplicitThis = true, ThrowsException = true)]
		public void Evaluate([DefaultValue("0")] float deltaTime)
		{
			PlayableGraph.Evaluate_Injected(ref this, deltaTime);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0003FDBB File Offset: 0x0003DFBB
		[FreeFunction("PlayableGraphBindings::GetTimeUpdateMode", HasExplicitThis = true, ThrowsException = true)]
		public DirectorUpdateMode GetTimeUpdateMode()
		{
			return PlayableGraph.GetTimeUpdateMode_Injected(ref this);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0003FDC3 File Offset: 0x0003DFC3
		[FreeFunction("PlayableGraphBindings::SetTimeUpdateMode", HasExplicitThis = true, ThrowsException = true)]
		public void SetTimeUpdateMode(DirectorUpdateMode value)
		{
			PlayableGraph.SetTimeUpdateMode_Injected(ref this, value);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0003FDCC File Offset: 0x0003DFCC
		[FreeFunction("PlayableGraphBindings::GetResolver", HasExplicitThis = true, ThrowsException = true)]
		public IExposedPropertyTable GetResolver()
		{
			return PlayableGraph.GetResolver_Injected(ref this);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0003FDD4 File Offset: 0x0003DFD4
		[FreeFunction("PlayableGraphBindings::SetResolver", HasExplicitThis = true, ThrowsException = true)]
		public void SetResolver(IExposedPropertyTable value)
		{
			PlayableGraph.SetResolver_Injected(ref this, value);
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0003FDDD File Offset: 0x0003DFDD
		[FreeFunction("PlayableGraphBindings::GetPlayableCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetPlayableCount()
		{
			return PlayableGraph.GetPlayableCount_Injected(ref this);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0003FDE5 File Offset: 0x0003DFE5
		[FreeFunction("PlayableGraphBindings::GetRootPlayableCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetRootPlayableCount()
		{
			return PlayableGraph.GetRootPlayableCount_Injected(ref this);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x0003FDED File Offset: 0x0003DFED
		[FreeFunction("PlayableGraphBindings::SynchronizeEvaluation", HasExplicitThis = true, ThrowsException = true)]
		internal void SynchronizeEvaluation(PlayableGraph playable)
		{
			PlayableGraph.SynchronizeEvaluation_Injected(ref this, ref playable);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x0003FDF7 File Offset: 0x0003DFF7
		[FreeFunction("PlayableGraphBindings::GetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetOutputCount()
		{
			return PlayableGraph.GetOutputCount_Injected(ref this);
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x0003FE00 File Offset: 0x0003E000
		[FreeFunction("PlayableGraphBindings::CreatePlayableHandle", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle CreatePlayableHandle()
		{
			PlayableHandle playableHandle;
			PlayableGraph.CreatePlayableHandle_Injected(ref this, out playableHandle);
			return playableHandle;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x0003FE16 File Offset: 0x0003E016
		[FreeFunction("PlayableGraphBindings::CreateScriptOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		internal bool CreateScriptOutputInternal(string name, out PlayableOutputHandle handle)
		{
			return PlayableGraph.CreateScriptOutputInternal_Injected(ref this, name, out handle);
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0003FE20 File Offset: 0x0003E020
		[FreeFunction("PlayableGraphBindings::GetRootPlayableInternal", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle GetRootPlayableInternal(int index)
		{
			PlayableHandle playableHandle;
			PlayableGraph.GetRootPlayableInternal_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0003FE37 File Offset: 0x0003E037
		[FreeFunction("PlayableGraphBindings::DestroyOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		internal void DestroyOutputInternal(PlayableOutputHandle handle)
		{
			PlayableGraph.DestroyOutputInternal_Injected(ref this, ref handle);
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0003FE41 File Offset: 0x0003E041
		[FreeFunction("PlayableGraphBindings::IsMatchFrameRateEnabled", HasExplicitThis = true, ThrowsException = true)]
		internal bool IsMatchFrameRateEnabled()
		{
			return PlayableGraph.IsMatchFrameRateEnabled_Injected(ref this);
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x0003FE49 File Offset: 0x0003E049
		[FreeFunction("PlayableGraphBindings::EnableMatchFrameRate", HasExplicitThis = true, ThrowsException = true)]
		internal void EnableMatchFrameRate(FrameRate frameRate)
		{
			PlayableGraph.EnableMatchFrameRate_Injected(ref this, ref frameRate);
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x0003FE53 File Offset: 0x0003E053
		[FreeFunction("PlayableGraphBindings::DisableMatchFrameRate", HasExplicitThis = true, ThrowsException = true)]
		internal void DisableMatchFrameRate()
		{
			PlayableGraph.DisableMatchFrameRate_Injected(ref this);
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x0003FE5C File Offset: 0x0003E05C
		[FreeFunction("PlayableGraphBindings::GetFrameRate", HasExplicitThis = true, ThrowsException = true)]
		internal FrameRate GetFrameRate()
		{
			FrameRate frameRate;
			PlayableGraph.GetFrameRate_Injected(ref this, out frameRate);
			return frameRate;
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0003FE72 File Offset: 0x0003E072
		[FreeFunction("PlayableGraphBindings::GetOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool GetOutputInternal(int index, out PlayableOutputHandle handle)
		{
			return PlayableGraph.GetOutputInternal_Injected(ref this, index, out handle);
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0003FE7C File Offset: 0x0003E07C
		[FreeFunction("PlayableGraphBindings::GetOutputCountByTypeInternal", HasExplicitThis = true, ThrowsException = true)]
		private int GetOutputCountByTypeInternal(Type outputType)
		{
			return PlayableGraph.GetOutputCountByTypeInternal_Injected(ref this, outputType);
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x0003FE85 File Offset: 0x0003E085
		[FreeFunction("PlayableGraphBindings::GetOutputByTypeInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool GetOutputByTypeInternal(Type outputType, int index, out PlayableOutputHandle handle)
		{
			return PlayableGraph.GetOutputByTypeInternal_Injected(ref this, outputType, index, out handle);
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x0003FE90 File Offset: 0x0003E090
		[FreeFunction("PlayableGraphBindings::ConnectInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool ConnectInternal(PlayableHandle source, int sourceOutputPort, PlayableHandle destination, int destinationInputPort)
		{
			return PlayableGraph.ConnectInternal_Injected(ref this, ref source, sourceOutputPort, ref destination, destinationInputPort);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x0003FE9F File Offset: 0x0003E09F
		[FreeFunction("PlayableGraphBindings::DisconnectInternal", HasExplicitThis = true, ThrowsException = true)]
		private void DisconnectInternal(PlayableHandle playable, int inputPort)
		{
			PlayableGraph.DisconnectInternal_Injected(ref this, ref playable, inputPort);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0003FEAA File Offset: 0x0003E0AA
		[FreeFunction("PlayableGraphBindings::DestroyPlayableInternal", HasExplicitThis = true, ThrowsException = true)]
		private void DestroyPlayableInternal(PlayableHandle playable)
		{
			PlayableGraph.DestroyPlayableInternal_Injected(ref this, ref playable);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x0003FEB4 File Offset: 0x0003E0B4
		[FreeFunction("PlayableGraphBindings::DestroySubgraphInternal", HasExplicitThis = true, ThrowsException = true)]
		private void DestroySubgraphInternal(PlayableHandle playable)
		{
			PlayableGraph.DestroySubgraphInternal_Injected(ref this, ref playable);
		}

		// Token: 0x060025FA RID: 9722
		[MethodImpl(4096)]
		private static extern void Create_Injected(string name, out PlayableGraph ret);

		// Token: 0x060025FB RID: 9723
		[MethodImpl(4096)]
		private static extern void Destroy_Injected(ref PlayableGraph _unity_self);

		// Token: 0x060025FC RID: 9724
		[MethodImpl(4096)]
		private static extern bool IsValid_Injected(ref PlayableGraph _unity_self);

		// Token: 0x060025FD RID: 9725
		[MethodImpl(4096)]
		private static extern bool IsPlaying_Injected(ref PlayableGraph _unity_self);

		// Token: 0x060025FE RID: 9726
		[MethodImpl(4096)]
		private static extern bool IsDone_Injected(ref PlayableGraph _unity_self);

		// Token: 0x060025FF RID: 9727
		[MethodImpl(4096)]
		private static extern void Play_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002600 RID: 9728
		[MethodImpl(4096)]
		private static extern void Stop_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002601 RID: 9729
		[MethodImpl(4096)]
		private static extern void Evaluate_Injected(ref PlayableGraph _unity_self, [DefaultValue("0")] float deltaTime);

		// Token: 0x06002602 RID: 9730
		[MethodImpl(4096)]
		private static extern DirectorUpdateMode GetTimeUpdateMode_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002603 RID: 9731
		[MethodImpl(4096)]
		private static extern void SetTimeUpdateMode_Injected(ref PlayableGraph _unity_self, DirectorUpdateMode value);

		// Token: 0x06002604 RID: 9732
		[MethodImpl(4096)]
		private static extern IExposedPropertyTable GetResolver_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002605 RID: 9733
		[MethodImpl(4096)]
		private static extern void SetResolver_Injected(ref PlayableGraph _unity_self, IExposedPropertyTable value);

		// Token: 0x06002606 RID: 9734
		[MethodImpl(4096)]
		private static extern int GetPlayableCount_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002607 RID: 9735
		[MethodImpl(4096)]
		private static extern int GetRootPlayableCount_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002608 RID: 9736
		[MethodImpl(4096)]
		private static extern void SynchronizeEvaluation_Injected(ref PlayableGraph _unity_self, ref PlayableGraph playable);

		// Token: 0x06002609 RID: 9737
		[MethodImpl(4096)]
		private static extern int GetOutputCount_Injected(ref PlayableGraph _unity_self);

		// Token: 0x0600260A RID: 9738
		[MethodImpl(4096)]
		private static extern void CreatePlayableHandle_Injected(ref PlayableGraph _unity_self, out PlayableHandle ret);

		// Token: 0x0600260B RID: 9739
		[MethodImpl(4096)]
		private static extern bool CreateScriptOutputInternal_Injected(ref PlayableGraph _unity_self, string name, out PlayableOutputHandle handle);

		// Token: 0x0600260C RID: 9740
		[MethodImpl(4096)]
		private static extern void GetRootPlayableInternal_Injected(ref PlayableGraph _unity_self, int index, out PlayableHandle ret);

		// Token: 0x0600260D RID: 9741
		[MethodImpl(4096)]
		private static extern void DestroyOutputInternal_Injected(ref PlayableGraph _unity_self, ref PlayableOutputHandle handle);

		// Token: 0x0600260E RID: 9742
		[MethodImpl(4096)]
		private static extern bool IsMatchFrameRateEnabled_Injected(ref PlayableGraph _unity_self);

		// Token: 0x0600260F RID: 9743
		[MethodImpl(4096)]
		private static extern void EnableMatchFrameRate_Injected(ref PlayableGraph _unity_self, ref FrameRate frameRate);

		// Token: 0x06002610 RID: 9744
		[MethodImpl(4096)]
		private static extern void DisableMatchFrameRate_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002611 RID: 9745
		[MethodImpl(4096)]
		private static extern void GetFrameRate_Injected(ref PlayableGraph _unity_self, out FrameRate ret);

		// Token: 0x06002612 RID: 9746
		[MethodImpl(4096)]
		private static extern bool GetOutputInternal_Injected(ref PlayableGraph _unity_self, int index, out PlayableOutputHandle handle);

		// Token: 0x06002613 RID: 9747
		[MethodImpl(4096)]
		private static extern int GetOutputCountByTypeInternal_Injected(ref PlayableGraph _unity_self, Type outputType);

		// Token: 0x06002614 RID: 9748
		[MethodImpl(4096)]
		private static extern bool GetOutputByTypeInternal_Injected(ref PlayableGraph _unity_self, Type outputType, int index, out PlayableOutputHandle handle);

		// Token: 0x06002615 RID: 9749
		[MethodImpl(4096)]
		private static extern bool ConnectInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle source, int sourceOutputPort, ref PlayableHandle destination, int destinationInputPort);

		// Token: 0x06002616 RID: 9750
		[MethodImpl(4096)]
		private static extern void DisconnectInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle playable, int inputPort);

		// Token: 0x06002617 RID: 9751
		[MethodImpl(4096)]
		private static extern void DestroyPlayableInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle playable);

		// Token: 0x06002618 RID: 9752
		[MethodImpl(4096)]
		private static extern void DestroySubgraphInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle playable);

		// Token: 0x04000E1C RID: 3612
		internal IntPtr m_Handle;

		// Token: 0x04000E1D RID: 3613
		internal uint m_Version;
	}
}
