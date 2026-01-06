using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001E4 RID: 484
	[NativeHeader("Runtime/Export/Random/Random.bindings.h")]
	public static class Random
	{
		// Token: 0x060015ED RID: 5613
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		[NativeMethod("SetSeed")]
		[MethodImpl(4096)]
		public static extern void InitState(int seed);

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00023288 File Offset: 0x00021488
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x0002329D File Offset: 0x0002149D
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static Random.State state
		{
			get
			{
				Random.State state;
				Random.get_state_Injected(out state);
				return state;
			}
			set
			{
				Random.set_state_Injected(ref value);
			}
		}

		// Token: 0x060015F0 RID: 5616
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern float Range(float minInclusive, float maxInclusive);

		// Token: 0x060015F1 RID: 5617 RVA: 0x000232A8 File Offset: 0x000214A8
		public static int Range(int minInclusive, int maxExclusive)
		{
			return Random.RandomRangeInt(minInclusive, maxExclusive);
		}

		// Token: 0x060015F2 RID: 5618
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern int RandomRangeInt(int minInclusive, int maxExclusive);

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060015F3 RID: 5619
		public static extern float value
		{
			[FreeFunction]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000232C4 File Offset: 0x000214C4
		public static Vector3 insideUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 vector;
				Random.get_insideUnitSphere_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x060015F5 RID: 5621
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void GetRandomUnitCircle(out Vector2 output);

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x000232DC File Offset: 0x000214DC
		public static Vector2 insideUnitCircle
		{
			get
			{
				Vector2 vector;
				Random.GetRandomUnitCircle(out vector);
				return vector;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x000232F8 File Offset: 0x000214F8
		public static Vector3 onUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 vector;
				Random.get_onUnitSphere_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00023310 File Offset: 0x00021510
		public static Quaternion rotation
		{
			[FreeFunction]
			get
			{
				Quaternion quaternion;
				Random.get_rotation_Injected(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00023328 File Offset: 0x00021528
		public static Quaternion rotationUniform
		{
			[FreeFunction]
			get
			{
				Quaternion quaternion;
				Random.get_rotationUniform_Injected(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060015FA RID: 5626
		// (set) Token: 0x060015FB RID: 5627
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		[Obsolete("Deprecated. Use InitState() function or Random.state property instead.")]
		public static extern int seed
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00023340 File Offset: 0x00021540
		[Obsolete("Use Random.Range instead")]
		public static float RandomRange(float min, float max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0002335C File Offset: 0x0002155C
		[Obsolete("Use Random.Range instead")]
		public static int RandomRange(int min, int max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00023378 File Offset: 0x00021578
		public static Color ColorHSV()
		{
			return Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000233B8 File Offset: 0x000215B8
		public static Color ColorHSV(float hueMin, float hueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000233F0 File Offset: 0x000215F0
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00023420 File Offset: 0x00021620
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, 1f, 1f);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0002344C File Offset: 0x0002164C
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
		{
			float num = Mathf.Lerp(hueMin, hueMax, Random.value);
			float num2 = Mathf.Lerp(saturationMin, saturationMax, Random.value);
			float num3 = Mathf.Lerp(valueMin, valueMax, Random.value);
			Color color = Color.HSVToRGB(num, num2, num3, true);
			color.a = Mathf.Lerp(alphaMin, alphaMax, Random.value);
			return color;
		}

		// Token: 0x06001603 RID: 5635
		[MethodImpl(4096)]
		private static extern void get_state_Injected(out Random.State ret);

		// Token: 0x06001604 RID: 5636
		[MethodImpl(4096)]
		private static extern void set_state_Injected(ref Random.State value);

		// Token: 0x06001605 RID: 5637
		[MethodImpl(4096)]
		private static extern void get_insideUnitSphere_Injected(out Vector3 ret);

		// Token: 0x06001606 RID: 5638
		[MethodImpl(4096)]
		private static extern void get_onUnitSphere_Injected(out Vector3 ret);

		// Token: 0x06001607 RID: 5639
		[MethodImpl(4096)]
		private static extern void get_rotation_Injected(out Quaternion ret);

		// Token: 0x06001608 RID: 5640
		[MethodImpl(4096)]
		private static extern void get_rotationUniform_Injected(out Quaternion ret);

		// Token: 0x020001E5 RID: 485
		[Serializable]
		public struct State
		{
			// Token: 0x040007BD RID: 1981
			[SerializeField]
			private int s0;

			// Token: 0x040007BE RID: 1982
			[SerializeField]
			private int s1;

			// Token: 0x040007BF RID: 1983
			[SerializeField]
			private int s2;

			// Token: 0x040007C0 RID: 1984
			[SerializeField]
			private int s3;
		}
	}
}
