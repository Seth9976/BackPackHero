using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000134 RID: 308
	[NativeHeader("Runtime/Export/Graphics/Graphics.bindings.h")]
	[StructLayout(0)]
	public sealed class LightProbes : Object
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		private LightProbes()
		{
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060009A8 RID: 2472 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		// (remove) Token: 0x060009A9 RID: 2473 RVA: 0x0000E814 File Offset: 0x0000CA14
		[field: DebuggerBrowsable(0)]
		public static event Action tetrahedralizationCompleted;

		// Token: 0x060009AA RID: 2474 RVA: 0x0000E848 File Offset: 0x0000CA48
		[RequiredByNativeCode]
		private static void Internal_CallTetrahedralizationCompletedFunction()
		{
			bool flag = LightProbes.tetrahedralizationCompleted != null;
			if (flag)
			{
				LightProbes.tetrahedralizationCompleted.Invoke();
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060009AB RID: 2475 RVA: 0x0000E870 File Offset: 0x0000CA70
		// (remove) Token: 0x060009AC RID: 2476 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		[field: DebuggerBrowsable(0)]
		public static event Action needsRetetrahedralization;

		// Token: 0x060009AD RID: 2477 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		[RequiredByNativeCode]
		private static void Internal_CallNeedsRetetrahedralizationFunction()
		{
			bool flag = LightProbes.needsRetetrahedralization != null;
			if (flag)
			{
				LightProbes.needsRetetrahedralization.Invoke();
			}
		}

		// Token: 0x060009AE RID: 2478
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void Tetrahedralize();

		// Token: 0x060009AF RID: 2479
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void TetrahedralizeAsync();

		// Token: 0x060009B0 RID: 2480 RVA: 0x0000E8FD File Offset: 0x0000CAFD
		[FreeFunction]
		public static void GetInterpolatedProbe(Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe)
		{
			LightProbes.GetInterpolatedProbe_Injected(ref position, renderer, out probe);
		}

		// Token: 0x060009B1 RID: 2481
		[FreeFunction]
		[MethodImpl(4096)]
		internal static extern bool AreLightProbesAllowed(Renderer renderer);

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000E908 File Offset: 0x0000CB08
		public static void CalculateInterpolatedLightAndOcclusionProbes(Vector3[] positions, SphericalHarmonicsL2[] lightProbes, Vector4[] occlusionProbes)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = lightProbes == null && occlusionProbes == null;
			if (flag2)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			bool flag3 = lightProbes != null && lightProbes.Length < positions.Length;
			if (flag3)
			{
				throw new ArgumentException("lightProbes", "Argument lightProbes has less elements than positions");
			}
			bool flag4 = occlusionProbes != null && occlusionProbes.Length < positions.Length;
			if (flag4)
			{
				throw new ArgumentException("occlusionProbes", "Argument occlusionProbes has less elements than positions");
			}
			LightProbes.CalculateInterpolatedLightAndOcclusionProbes_Internal(positions, positions.Length, lightProbes, occlusionProbes);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0000E994 File Offset: 0x0000CB94
		public static void CalculateInterpolatedLightAndOcclusionProbes(List<Vector3> positions, List<SphericalHarmonicsL2> lightProbes, List<Vector4> occlusionProbes)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = lightProbes == null && occlusionProbes == null;
			if (flag2)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			bool flag3 = lightProbes != null;
			if (flag3)
			{
				bool flag4 = lightProbes.Capacity < positions.Count;
				if (flag4)
				{
					lightProbes.Capacity = positions.Count;
				}
				bool flag5 = lightProbes.Count < positions.Count;
				if (flag5)
				{
					NoAllocHelpers.ResizeList<SphericalHarmonicsL2>(lightProbes, positions.Count);
				}
			}
			bool flag6 = occlusionProbes != null;
			if (flag6)
			{
				bool flag7 = occlusionProbes.Capacity < positions.Count;
				if (flag7)
				{
					occlusionProbes.Capacity = positions.Count;
				}
				bool flag8 = occlusionProbes.Count < positions.Count;
				if (flag8)
				{
					NoAllocHelpers.ResizeList<Vector4>(occlusionProbes, positions.Count);
				}
			}
			LightProbes.CalculateInterpolatedLightAndOcclusionProbes_Internal(NoAllocHelpers.ExtractArrayFromListT<Vector3>(positions), positions.Count, NoAllocHelpers.ExtractArrayFromListT<SphericalHarmonicsL2>(lightProbes), NoAllocHelpers.ExtractArrayFromListT<Vector4>(occlusionProbes));
		}

		// Token: 0x060009B4 RID: 2484
		[FreeFunction]
		[NativeName("CalculateInterpolatedLightAndOcclusionProbes")]
		[MethodImpl(4096)]
		internal static extern void CalculateInterpolatedLightAndOcclusionProbes_Internal(Vector3[] positions, int positionsCount, SphericalHarmonicsL2[] lightProbes, Vector4[] occlusionProbes);

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009B5 RID: 2485
		public extern Vector3[] positions
		{
			[NativeName("GetLightProbePositions")]
			[FreeFunction(HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009B6 RID: 2486
		// (set) Token: 0x060009B7 RID: 2487
		public extern SphericalHarmonicsL2[] bakedProbes
		{
			[FreeFunction(HasExplicitThis = true)]
			[NativeName("GetBakedCoefficients")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetBakedCoefficients")]
			[FreeFunction(HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009B8 RID: 2488
		public extern int count
		{
			[NativeName("GetLightProbeCount")]
			[FreeFunction(HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009B9 RID: 2489
		public extern int cellCount
		{
			[NativeName("GetTetrahedraSize")]
			[FreeFunction(HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060009BA RID: 2490
		[FreeFunction]
		[NativeName("GetLightProbeCount")]
		[MethodImpl(4096)]
		internal static extern int GetCount();

		// Token: 0x060009BB RID: 2491 RVA: 0x00004557 File Offset: 0x00002757
		[EditorBrowsable(1)]
		[Obsolete("Use GetInterpolatedProbe instead.", true)]
		public void GetInterpolatedLightProbe(Vector3 position, Renderer renderer, float[] coefficients)
		{
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0000EA84 File Offset: 0x0000CC84
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x00004557 File Offset: 0x00002757
		[Obsolete("Use bakedProbes instead.", true)]
		[EditorBrowsable(1)]
		public float[] coefficients
		{
			get
			{
				return new float[0];
			}
			set
			{
			}
		}

		// Token: 0x060009BE RID: 2494
		[MethodImpl(4096)]
		private static extern void GetInterpolatedProbe_Injected(ref Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe);
	}
}
