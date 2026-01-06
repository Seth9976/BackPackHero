using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000120 RID: 288
	[NativeHeader("Runtime/Graphics/Billboard/BillboardAsset.h")]
	[NativeHeader("Runtime/Export/Graphics/BillboardRenderer.bindings.h")]
	public sealed class BillboardAsset : Object
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x0000BD84 File Offset: 0x00009F84
		public BillboardAsset()
		{
			BillboardAsset.Internal_Create(this);
		}

		// Token: 0x060007D8 RID: 2008
		[FreeFunction(Name = "BillboardRenderer_Bindings::Internal_Create")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] BillboardAsset obj);

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007D9 RID: 2009
		// (set) Token: 0x060007DA RID: 2010
		public extern float width
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007DB RID: 2011
		// (set) Token: 0x060007DC RID: 2012
		public extern float height
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007DD RID: 2013
		// (set) Token: 0x060007DE RID: 2014
		public extern float bottom
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007DF RID: 2015
		public extern int imageCount
		{
			[NativeMethod("GetNumImages")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007E0 RID: 2016
		public extern int vertexCount
		{
			[NativeMethod("GetNumVertices")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007E1 RID: 2017
		public extern int indexCount
		{
			[NativeMethod("GetNumIndices")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007E2 RID: 2018
		// (set) Token: 0x060007E3 RID: 2019
		public extern Material material
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0000BD98 File Offset: 0x00009F98
		public void GetImageTexCoords(List<Vector4> imageTexCoords)
		{
			bool flag = imageTexCoords == null;
			if (flag)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			this.GetImageTexCoordsInternal(imageTexCoords);
		}

		// Token: 0x060007E5 RID: 2021
		[NativeMethod("GetBillboardDataReadonly().GetImageTexCoords")]
		[MethodImpl(4096)]
		public extern Vector4[] GetImageTexCoords();

		// Token: 0x060007E6 RID: 2022
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetImageTexCoordsInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void GetImageTexCoordsInternal(object list);

		// Token: 0x060007E7 RID: 2023 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public void SetImageTexCoords(List<Vector4> imageTexCoords)
		{
			bool flag = imageTexCoords == null;
			if (flag)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			this.SetImageTexCoordsInternalList(imageTexCoords);
		}

		// Token: 0x060007E8 RID: 2024
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetImageTexCoords([NotNull("ArgumentNullException")] Vector4[] imageTexCoords);

		// Token: 0x060007E9 RID: 2025
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoordsInternalList", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void SetImageTexCoordsInternalList(object list);

		// Token: 0x060007EA RID: 2026 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		public void GetVertices(List<Vector2> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices");
			}
			this.GetVerticesInternal(vertices);
		}

		// Token: 0x060007EB RID: 2027
		[NativeMethod("GetBillboardDataReadonly().GetVertices")]
		[MethodImpl(4096)]
		public extern Vector2[] GetVertices();

		// Token: 0x060007EC RID: 2028
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetVerticesInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void GetVerticesInternal(object list);

		// Token: 0x060007ED RID: 2029 RVA: 0x0000BE1C File Offset: 0x0000A01C
		public void SetVertices(List<Vector2> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices");
			}
			this.SetVerticesInternalList(vertices);
		}

		// Token: 0x060007EE RID: 2030
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVertices", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetVertices([NotNull("ArgumentNullException")] Vector2[] vertices);

		// Token: 0x060007EF RID: 2031
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVerticesInternalList", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void SetVerticesInternalList(object list);

		// Token: 0x060007F0 RID: 2032 RVA: 0x0000BE48 File Offset: 0x0000A048
		public void GetIndices(List<ushort> indices)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices");
			}
			this.GetIndicesInternal(indices);
		}

		// Token: 0x060007F1 RID: 2033
		[NativeMethod("GetBillboardDataReadonly().GetIndices")]
		[MethodImpl(4096)]
		public extern ushort[] GetIndices();

		// Token: 0x060007F2 RID: 2034
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetIndicesInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void GetIndicesInternal(object list);

		// Token: 0x060007F3 RID: 2035 RVA: 0x0000BE74 File Offset: 0x0000A074
		public void SetIndices(List<ushort> indices)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices");
			}
			this.SetIndicesInternalList(indices);
		}

		// Token: 0x060007F4 RID: 2036
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndices", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetIndices([NotNull("ArgumentNullException")] ushort[] indices);

		// Token: 0x060007F5 RID: 2037
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndicesInternalList", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void SetIndicesInternalList(object list);

		// Token: 0x060007F6 RID: 2038
		[FreeFunction(Name = "BillboardRenderer_Bindings::MakeMaterialProperties", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void MakeMaterialProperties(MaterialPropertyBlock properties, Camera camera);
	}
}
