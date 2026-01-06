using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E3 RID: 227
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Math/AnimationCurve.bindings.h")]
	[StructLayout(0)]
	public class AnimationCurve : IEquatable<AnimationCurve>
	{
		// Token: 0x0600039B RID: 923
		[FreeFunction("AnimationCurveBindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600039C RID: 924
		[FreeFunction("AnimationCurveBindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Create(Keyframe[] keys);

		// Token: 0x0600039D RID: 925
		[FreeFunction("AnimationCurveBindings::Internal_Equals", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern bool Internal_Equals(IntPtr other);

		// Token: 0x0600039E RID: 926 RVA: 0x000060A8 File Offset: 0x000042A8
		~AnimationCurve()
		{
			AnimationCurve.Internal_Destroy(this.m_Ptr);
		}

		// Token: 0x0600039F RID: 927
		[ThreadSafe]
		[MethodImpl(4096)]
		public extern float Evaluate(float time);

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000060E0 File Offset: 0x000042E0
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x000060F8 File Offset: 0x000042F8
		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		// Token: 0x060003A2 RID: 930
		[FreeFunction("AnimationCurveBindings::AddKeySmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		public extern int AddKey(float time, float value);

		// Token: 0x060003A3 RID: 931 RVA: 0x00006104 File Offset: 0x00004304
		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000611D File Offset: 0x0000431D
		[NativeMethod("AddKey", IsThreadSafe = true)]
		private int AddKey_Internal(Keyframe key)
		{
			return this.AddKey_Internal_Injected(ref key);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00006127 File Offset: 0x00004327
		[FreeFunction("AnimationCurveBindings::MoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		public int MoveKey(int index, Keyframe key)
		{
			return this.MoveKey_Injected(index, ref key);
		}

		// Token: 0x060003A6 RID: 934
		[NativeThrows]
		[FreeFunction("AnimationCurveBindings::RemoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		public extern void RemoveKey(int index);

		// Token: 0x170000A0 RID: 160
		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey(index);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003A8 RID: 936
		public extern int length
		{
			[NativeMethod("GetKeyCount", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003A9 RID: 937
		[FreeFunction("AnimationCurveBindings::SetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void SetKeys(Keyframe[] keys);

		// Token: 0x060003AA RID: 938 RVA: 0x00006150 File Offset: 0x00004350
		[FreeFunction("AnimationCurveBindings::GetKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		private Keyframe GetKey(int index)
		{
			Keyframe keyframe;
			this.GetKey_Injected(index, out keyframe);
			return keyframe;
		}

		// Token: 0x060003AB RID: 939
		[FreeFunction("AnimationCurveBindings::GetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern Keyframe[] GetKeys();

		// Token: 0x060003AC RID: 940
		[FreeFunction("AnimationCurveBindings::SmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		[MethodImpl(4096)]
		public extern void SmoothTangents(int index, float weight);

		// Token: 0x060003AD RID: 941 RVA: 0x00006168 File Offset: 0x00004368
		public static AnimationCurve Constant(float timeStart, float timeEnd, float value)
		{
			return AnimationCurve.Linear(timeStart, value, timeEnd, value);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00006184 File Offset: 0x00004384
		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve animationCurve;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				animationCurve = new AnimationCurve(new Keyframe[] { keyframe });
			}
			else
			{
				float num = (valueEnd - valueStart) / (timeEnd - timeStart);
				Keyframe[] array = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, num),
					new Keyframe(timeEnd, valueEnd, num, 0f)
				};
				animationCurve = new AnimationCurve(array);
			}
			return animationCurve;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00006200 File Offset: 0x00004400
		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve animationCurve;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				animationCurve = new AnimationCurve(new Keyframe[] { keyframe });
			}
			else
			{
				Keyframe[] array = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, 0f),
					new Keyframe(timeEnd, valueEnd, 0f, 0f)
				};
				animationCurve = new AnimationCurve(array);
			}
			return animationCurve;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003B0 RID: 944
		// (set) Token: 0x060003B1 RID: 945
		public extern WrapMode preWrapMode
		{
			[NativeMethod("GetPreInfinity", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetPreInfinity", IsThreadSafe = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003B2 RID: 946
		// (set) Token: 0x060003B3 RID: 947
		public extern WrapMode postWrapMode
		{
			[NativeMethod("GetPostInfinity", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetPostInfinity", IsThreadSafe = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00006277 File Offset: 0x00004477
		public AnimationCurve(params Keyframe[] keys)
		{
			this.m_Ptr = AnimationCurve.Internal_Create(keys);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000628D File Offset: 0x0000448D
		[RequiredByNativeCode]
		public AnimationCurve()
		{
			this.m_Ptr = AnimationCurve.Internal_Create(null);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000062A4 File Offset: 0x000044A4
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
					flag2 = !flag4 && this.Equals((AnimationCurve)o);
				}
			}
			return flag2;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000062F8 File Offset: 0x000044F8
		public bool Equals(AnimationCurve other)
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

		// Token: 0x060003B8 RID: 952 RVA: 0x00006350 File Offset: 0x00004550
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x060003B9 RID: 953
		[MethodImpl(4096)]
		private extern int AddKey_Internal_Injected(ref Keyframe key);

		// Token: 0x060003BA RID: 954
		[MethodImpl(4096)]
		private extern int MoveKey_Injected(int index, ref Keyframe key);

		// Token: 0x060003BB RID: 955
		[MethodImpl(4096)]
		private extern void GetKey_Injected(int index, out Keyframe ret);

		// Token: 0x040002F3 RID: 755
		internal IntPtr m_Ptr;
	}
}
