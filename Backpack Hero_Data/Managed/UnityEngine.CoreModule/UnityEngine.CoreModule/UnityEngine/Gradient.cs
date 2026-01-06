using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C2 RID: 450
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Math/Gradient.bindings.h")]
	[StructLayout(0)]
	public class Gradient : IEquatable<Gradient>
	{
		// Token: 0x060013AC RID: 5036
		[FreeFunction(Name = "Gradient_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Init();

		// Token: 0x060013AD RID: 5037
		[FreeFunction(Name = "Gradient_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x060013AE RID: 5038
		[FreeFunction("Gradient_Bindings::Internal_Equals", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool Internal_Equals(IntPtr other);

		// Token: 0x060013AF RID: 5039 RVA: 0x0001C42B File Offset: 0x0001A62B
		[RequiredByNativeCode]
		public Gradient()
		{
			this.m_Ptr = Gradient.Init();
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0001C440 File Offset: 0x0001A640
		~Gradient()
		{
			this.Cleanup();
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0001C470 File Offset: 0x0001A670
		[FreeFunction(Name = "Gradient_Bindings::Evaluate", IsThreadSafe = true, HasExplicitThis = true)]
		public Color Evaluate(float time)
		{
			Color color;
			this.Evaluate_Injected(time, out color);
			return color;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060013B2 RID: 5042
		// (set) Token: 0x060013B3 RID: 5043
		public extern GradientColorKey[] colorKeys
		{
			[FreeFunction("Gradient_Bindings::GetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("Gradient_Bindings::SetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060013B4 RID: 5044
		// (set) Token: 0x060013B5 RID: 5045
		public extern GradientAlphaKey[] alphaKeys
		{
			[FreeFunction("Gradient_Bindings::GetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("Gradient_Bindings::SetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060013B6 RID: 5046
		// (set) Token: 0x060013B7 RID: 5047
		[NativeProperty(IsThreadSafe = true)]
		public extern GradientMode mode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060013B8 RID: 5048
		[FreeFunction(Name = "Gradient_Bindings::SetKeys", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys);

		// Token: 0x060013B9 RID: 5049 RVA: 0x0001C488 File Offset: 0x0001A688
		public override bool Equals(object o)
		{
			bool flag = o == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == o;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = o.GetType() != base.GetType();
					flag2 = !flag4 && this.Equals((Gradient)o);
				}
			}
			return flag2;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0001C4DC File Offset: 0x0001A6DC
		public bool Equals(Gradient other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == other;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = this.m_Ptr.Equals(other.m_Ptr);
					flag2 = flag4 || this.Internal_Equals(other.m_Ptr);
				}
			}
			return flag2;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0001C534 File Offset: 0x0001A734
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x060013BC RID: 5052
		[MethodImpl(4096)]
		private extern void Evaluate_Injected(float time, out Color ret);

		// Token: 0x04000744 RID: 1860
		internal IntPtr m_Ptr;
	}
}
