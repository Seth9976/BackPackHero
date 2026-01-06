using System;
using System.ComponentModel;
using UnityEngine.Bindings;

namespace UnityEngine.Playables
{
	// Token: 0x0200043D RID: 1085
	public struct PlayableBinding
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x0003F3D8 File Offset: 0x0003D5D8
		// (set) Token: 0x06002595 RID: 9621 RVA: 0x0003F3F0 File Offset: 0x0003D5F0
		public string streamName
		{
			get
			{
				return this.m_StreamName;
			}
			set
			{
				this.m_StreamName = value;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x0003F3FC File Offset: 0x0003D5FC
		// (set) Token: 0x06002597 RID: 9623 RVA: 0x0003F414 File Offset: 0x0003D614
		public Object sourceObject
		{
			get
			{
				return this.m_SourceObject;
			}
			set
			{
				this.m_SourceObject = value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x0003F420 File Offset: 0x0003D620
		public Type outputTargetType
		{
			get
			{
				return this.m_SourceBindingType;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x0003F438 File Offset: 0x0003D638
		// (set) Token: 0x0600259A RID: 9626 RVA: 0x00004557 File Offset: 0x00002757
		[Obsolete("sourceBindingType is no longer supported on PlayableBinding. Use outputBindingType instead to get the required output target type, and the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) to create PlayableBindings", true)]
		[EditorBrowsable(1)]
		public Type sourceBindingType
		{
			get
			{
				return this.m_SourceBindingType;
			}
			set
			{
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x0003F450 File Offset: 0x0003D650
		// (set) Token: 0x0600259C RID: 9628 RVA: 0x00004557 File Offset: 0x00002757
		[Obsolete("streamType is no longer supported on PlayableBinding. Use the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) instead.", true)]
		[EditorBrowsable(1)]
		public DataStreamType streamType
		{
			get
			{
				return DataStreamType.None;
			}
			set
			{
			}
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0003F464 File Offset: 0x0003D664
		internal PlayableOutput CreateOutput(PlayableGraph graph)
		{
			bool flag = this.m_CreateOutputMethod != null;
			PlayableOutput playableOutput;
			if (flag)
			{
				playableOutput = this.m_CreateOutputMethod(graph, this.m_StreamName);
			}
			else
			{
				playableOutput = PlayableOutput.Null;
			}
			return playableOutput;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0003F4A0 File Offset: 0x0003D6A0
		[VisibleToOtherModules]
		internal static PlayableBinding CreateInternal(string name, Object sourceObject, Type sourceType, PlayableBinding.CreateOutputMethod createFunction)
		{
			return new PlayableBinding
			{
				m_StreamName = name,
				m_SourceObject = sourceObject,
				m_SourceBindingType = sourceType,
				m_CreateOutputMethod = createFunction
			};
		}

		// Token: 0x04000E0E RID: 3598
		private string m_StreamName;

		// Token: 0x04000E0F RID: 3599
		private Object m_SourceObject;

		// Token: 0x04000E10 RID: 3600
		private Type m_SourceBindingType;

		// Token: 0x04000E11 RID: 3601
		private PlayableBinding.CreateOutputMethod m_CreateOutputMethod;

		// Token: 0x04000E12 RID: 3602
		public static readonly PlayableBinding[] None = new PlayableBinding[0];

		// Token: 0x04000E13 RID: 3603
		public static readonly double DefaultDuration = double.PositiveInfinity;

		// Token: 0x0200043E RID: 1086
		// (Invoke) Token: 0x060025A1 RID: 9633
		[VisibleToOtherModules]
		internal delegate PlayableOutput CreateOutputMethod(PlayableGraph graph, string name);
	}
}
