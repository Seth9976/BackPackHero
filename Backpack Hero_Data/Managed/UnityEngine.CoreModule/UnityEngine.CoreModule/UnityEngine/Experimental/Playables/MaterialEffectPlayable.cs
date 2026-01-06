using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x02000467 RID: 1127
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("MaterialEffectPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Shaders/Director/MaterialEffectPlayable.h")]
	[NativeHeader("Runtime/Export/Director/MaterialEffectPlayable.bindings.h")]
	public struct MaterialEffectPlayable : IPlayable, IEquatable<MaterialEffectPlayable>
	{
		// Token: 0x060027EA RID: 10218 RVA: 0x000426A0 File Offset: 0x000408A0
		public static MaterialEffectPlayable Create(PlayableGraph graph, Material material, int pass = -1)
		{
			PlayableHandle playableHandle = MaterialEffectPlayable.CreateHandle(graph, material, pass);
			return new MaterialEffectPlayable(playableHandle);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000426C4 File Offset: 0x000408C4
		private static PlayableHandle CreateHandle(PlayableGraph graph, Material material, int pass)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !MaterialEffectPlayable.InternalCreateMaterialEffectPlayable(ref graph, material, pass, ref @null);
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

		// Token: 0x060027EC RID: 10220 RVA: 0x000426F8 File Offset: 0x000408F8
		internal MaterialEffectPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<MaterialEffectPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an MaterialEffectPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x00042734 File Offset: 0x00040934
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x0004274C File Offset: 0x0004094C
		public static implicit operator Playable(MaterialEffectPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x0004276C File Offset: 0x0004096C
		public static explicit operator MaterialEffectPlayable(Playable playable)
		{
			return new MaterialEffectPlayable(playable.GetHandle());
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x0004278C File Offset: 0x0004098C
		public bool Equals(MaterialEffectPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000427B0 File Offset: 0x000409B0
		public Material GetMaterial()
		{
			return MaterialEffectPlayable.GetMaterialInternal(ref this.m_Handle);
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000427CD File Offset: 0x000409CD
		public void SetMaterial(Material value)
		{
			MaterialEffectPlayable.SetMaterialInternal(ref this.m_Handle, value);
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000427E0 File Offset: 0x000409E0
		public int GetPass()
		{
			return MaterialEffectPlayable.GetPassInternal(ref this.m_Handle);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000427FD File Offset: 0x000409FD
		public void SetPass(int value)
		{
			MaterialEffectPlayable.SetPassInternal(ref this.m_Handle, value);
		}

		// Token: 0x060027F5 RID: 10229
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern Material GetMaterialInternal(ref PlayableHandle hdl);

		// Token: 0x060027F6 RID: 10230
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetMaterialInternal(ref PlayableHandle hdl, Material material);

		// Token: 0x060027F7 RID: 10231
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int GetPassInternal(ref PlayableHandle hdl);

		// Token: 0x060027F8 RID: 10232
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetPassInternal(ref PlayableHandle hdl, int pass);

		// Token: 0x060027F9 RID: 10233
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool InternalCreateMaterialEffectPlayable(ref PlayableGraph graph, Material material, int pass, ref PlayableHandle handle);

		// Token: 0x060027FA RID: 10234
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x04000EBE RID: 3774
		private PlayableHandle m_Handle;
	}
}
