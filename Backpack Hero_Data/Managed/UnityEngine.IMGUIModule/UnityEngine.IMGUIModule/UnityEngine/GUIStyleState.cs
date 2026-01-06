using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIStyleState
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002D5 RID: 725
		// (set) Token: 0x060002D6 RID: 726
		[NativeProperty("Background", false, TargetType.Function)]
		public extern Texture2D background
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B4F4 File Offset: 0x000096F4
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000B50A File Offset: 0x0000970A
		[NativeProperty("textColor", false, TargetType.Field)]
		public Color textColor
		{
			get
			{
				Color color;
				this.get_textColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_textColor_Injected(ref value);
			}
		}

		// Token: 0x060002D9 RID: 729
		[FreeFunction(Name = "GUIStyleState_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Init();

		// Token: 0x060002DA RID: 730
		[FreeFunction(Name = "GUIStyleState_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x060002DB RID: 731 RVA: 0x0000B514 File Offset: 0x00009714
		public GUIStyleState()
		{
			this.m_Ptr = GUIStyleState.Init();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B529 File Offset: 0x00009729
		private GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B544 File Offset: 0x00009744
		internal static GUIStyleState ProduceGUIStyleStateFromDeserialization(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B560 File Offset: 0x00009760
		internal static GUIStyleState GetGUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B57C File Offset: 0x0000977C
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Cleanup();
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060002E0 RID: 736
		[MethodImpl(4096)]
		private extern void get_textColor_Injected(out Color ret);

		// Token: 0x060002E1 RID: 737
		[MethodImpl(4096)]
		private extern void set_textColor_Injected(ref Color value);

		// Token: 0x040000C9 RID: 201
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040000CA RID: 202
		private readonly GUIStyle m_SourceStyle;
	}
}
