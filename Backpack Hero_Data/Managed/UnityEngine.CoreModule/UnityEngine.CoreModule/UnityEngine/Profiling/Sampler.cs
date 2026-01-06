using System;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x02000279 RID: 633
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[UsedByNativeCode]
	public class Sampler
	{
		// Token: 0x06001BA9 RID: 7081 RVA: 0x00008C2F File Offset: 0x00006E2F
		internal Sampler()
		{
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0002C517 File Offset: 0x0002A717
		internal Sampler(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0002C528 File Offset: 0x0002A728
		public bool isValid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0002C54C File Offset: 0x0002A74C
		public Recorder GetRecorder()
		{
			ProfilerRecorderHandle profilerRecorderHandle = new ProfilerRecorderHandle((ulong)this.m_Ptr.ToInt64());
			return new Recorder(profilerRecorderHandle);
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0002C578 File Offset: 0x0002A778
		public static Sampler Get(string name)
		{
			IntPtr marker = ProfilerUnsafeUtility.GetMarker(name);
			bool flag = marker == IntPtr.Zero;
			Sampler sampler;
			if (flag)
			{
				sampler = Sampler.s_InvalidSampler;
			}
			else
			{
				sampler = new Sampler(marker);
			}
			return sampler;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0002C5B0 File Offset: 0x0002A7B0
		public static int GetNames(List<string> names)
		{
			List<ProfilerRecorderHandle> list = new List<ProfilerRecorderHandle>();
			ProfilerRecorderHandle.GetAvailable(list);
			bool flag = names != null;
			if (flag)
			{
				bool flag2 = names.Count < list.Count;
				if (flag2)
				{
					names.Capacity = list.Count;
					for (int i = names.Count; i < list.Count; i++)
					{
						names.Add(null);
					}
				}
				int num = 0;
				foreach (ProfilerRecorderHandle profilerRecorderHandle in list)
				{
					names[num] = ProfilerRecorderHandle.GetDescription(profilerRecorderHandle).Name;
					num++;
				}
			}
			return list.Count;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0002C68C File Offset: 0x0002A88C
		public string name
		{
			get
			{
				return ProfilerUnsafeUtility.Internal_GetName(this.m_Ptr);
			}
		}

		// Token: 0x0400090A RID: 2314
		internal IntPtr m_Ptr;

		// Token: 0x0400090B RID: 2315
		internal static Sampler s_InvalidSampler = new Sampler();
	}
}
