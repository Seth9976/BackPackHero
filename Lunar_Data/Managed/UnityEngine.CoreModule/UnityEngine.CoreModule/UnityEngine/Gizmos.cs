using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200011C RID: 284
	[NativeHeader("Runtime/Export/Gizmos/Gizmos.bindings.h")]
	[StaticAccessor("GizmoBindings", StaticAccessorType.DoubleColon)]
	public sealed class Gizmos
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x0000B777 File Offset: 0x00009977
		[NativeThrows]
		public static void DrawLine(Vector3 from, Vector3 to)
		{
			Gizmos.DrawLine_Injected(ref from, ref to);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000B782 File Offset: 0x00009982
		[NativeThrows]
		public static void DrawWireSphere(Vector3 center, float radius)
		{
			Gizmos.DrawWireSphere_Injected(ref center, radius);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000B78C File Offset: 0x0000998C
		[NativeThrows]
		public static void DrawSphere(Vector3 center, float radius)
		{
			Gizmos.DrawSphere_Injected(ref center, radius);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000B796 File Offset: 0x00009996
		[NativeThrows]
		public static void DrawWireCube(Vector3 center, Vector3 size)
		{
			Gizmos.DrawWireCube_Injected(ref center, ref size);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000B7A1 File Offset: 0x000099A1
		[NativeThrows]
		public static void DrawCube(Vector3 center, Vector3 size)
		{
			Gizmos.DrawCube_Injected(ref center, ref size);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000B7AC File Offset: 0x000099AC
		[NativeThrows]
		public static void DrawMesh(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawMesh_Injected(mesh, submeshIndex, ref position, ref rotation, ref scale);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000B7BB File Offset: 0x000099BB
		[NativeThrows]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawWireMesh_Injected(mesh, submeshIndex, ref position, ref rotation, ref scale);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000B7CA File Offset: 0x000099CA
		[NativeThrows]
		public static void DrawIcon(Vector3 center, string name, [DefaultValue("true")] bool allowScaling)
		{
			Gizmos.DrawIcon(center, name, allowScaling, Color.white);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000B7DB File Offset: 0x000099DB
		[NativeThrows]
		public static void DrawIcon(Vector3 center, string name, [DefaultValue("true")] bool allowScaling, [DefaultValue("Color(255,255,255,255)")] Color tint)
		{
			Gizmos.DrawIcon_Injected(ref center, name, allowScaling, ref tint);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000B7E8 File Offset: 0x000099E8
		[NativeThrows]
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat)
		{
			Gizmos.DrawGUITexture_Injected(ref screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0000B7FC File Offset: 0x000099FC
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x0000B811 File Offset: 0x00009A11
		public static Color color
		{
			get
			{
				Color color;
				Gizmos.get_color_Injected(out color);
				return color;
			}
			set
			{
				Gizmos.set_color_Injected(ref value);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0000B81C File Offset: 0x00009A1C
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0000B831 File Offset: 0x00009A31
		public static Matrix4x4 matrix
		{
			get
			{
				Matrix4x4 matrix4x;
				Gizmos.get_matrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				Gizmos.set_matrix_Injected(ref value);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007A8 RID: 1960
		// (set) Token: 0x060007A9 RID: 1961
		public static extern Texture exposure
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007AA RID: 1962
		public static extern float probeSize
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000B83A File Offset: 0x00009A3A
		public static void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect)
		{
			Gizmos.DrawFrustum_Injected(ref center, fov, maxRange, minRange, aspect);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000B848 File Offset: 0x00009A48
		public static void DrawRay(Ray r)
		{
			Gizmos.DrawLine(r.origin, r.origin + r.direction);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000B86B File Offset: 0x00009A6B
		public static void DrawRay(Vector3 from, Vector3 direction)
		{
			Gizmos.DrawLine(from, from + direction);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000B87C File Offset: 0x00009A7C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawMesh(mesh, position, rotation, one);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0000B89C File Offset: 0x00009A9C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawMesh(mesh, position, identity, one);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawMesh(mesh, zero, identity, one);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000B8EA File Offset: 0x00009AEA
		public static void DrawMesh(Mesh mesh, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawMesh(mesh, -1, position, rotation, scale);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0000B8F8 File Offset: 0x00009AF8
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawMesh(mesh, submeshIndex, position, rotation, one);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0000B918 File Offset: 0x00009B18
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawMesh(mesh, submeshIndex, position, identity, one);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0000B940 File Offset: 0x00009B40
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawMesh(mesh, submeshIndex, zero, identity, one);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0000B96C File Offset: 0x00009B6C
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawWireMesh(mesh, position, rotation, one);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0000B98C File Offset: 0x00009B8C
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawWireMesh(mesh, position, identity, one);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawWireMesh(mesh, zero, identity, one);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0000B9DA File Offset: 0x00009BDA
		public static void DrawWireMesh(Mesh mesh, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawWireMesh(mesh, -1, position, rotation, scale);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawWireMesh(mesh, submeshIndex, position, rotation, one);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0000BA08 File Offset: 0x00009C08
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawWireMesh(mesh, submeshIndex, position, identity, one);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0000BA30 File Offset: 0x00009C30
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawWireMesh(mesh, submeshIndex, zero, identity, one);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0000BA5C File Offset: 0x00009C5C
		[ExcludeFromDocs]
		public static void DrawIcon(Vector3 center, string name)
		{
			bool flag = true;
			Gizmos.DrawIcon(center, name, flag);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0000BA78 File Offset: 0x00009C78
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture)
		{
			Material material = null;
			Gizmos.DrawGUITexture(screenRect, texture, material);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0000BA91 File Offset: 0x00009C91
		public static void DrawGUITexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat)
		{
			Gizmos.DrawGUITexture(screenRect, texture, 0, 0, 0, 0, mat);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material material = null;
			Gizmos.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, material);
		}

		// Token: 0x060007C1 RID: 1985
		[MethodImpl(4096)]
		private static extern void DrawLine_Injected(ref Vector3 from, ref Vector3 to);

		// Token: 0x060007C2 RID: 1986
		[MethodImpl(4096)]
		private static extern void DrawWireSphere_Injected(ref Vector3 center, float radius);

		// Token: 0x060007C3 RID: 1987
		[MethodImpl(4096)]
		private static extern void DrawSphere_Injected(ref Vector3 center, float radius);

		// Token: 0x060007C4 RID: 1988
		[MethodImpl(4096)]
		private static extern void DrawWireCube_Injected(ref Vector3 center, ref Vector3 size);

		// Token: 0x060007C5 RID: 1989
		[MethodImpl(4096)]
		private static extern void DrawCube_Injected(ref Vector3 center, ref Vector3 size);

		// Token: 0x060007C6 RID: 1990
		[MethodImpl(4096)]
		private static extern void DrawMesh_Injected(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] ref Vector3 position, [DefaultValue("Quaternion.identity")] ref Quaternion rotation, [DefaultValue("Vector3.one")] ref Vector3 scale);

		// Token: 0x060007C7 RID: 1991
		[MethodImpl(4096)]
		private static extern void DrawWireMesh_Injected(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] ref Vector3 position, [DefaultValue("Quaternion.identity")] ref Quaternion rotation, [DefaultValue("Vector3.one")] ref Vector3 scale);

		// Token: 0x060007C8 RID: 1992
		[MethodImpl(4096)]
		private static extern void DrawIcon_Injected(ref Vector3 center, string name, [DefaultValue("true")] bool allowScaling, [DefaultValue("Color(255,255,255,255)")] ref Color tint);

		// Token: 0x060007C9 RID: 1993
		[MethodImpl(4096)]
		private static extern void DrawGUITexture_Injected(ref Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat);

		// Token: 0x060007CA RID: 1994
		[MethodImpl(4096)]
		private static extern void get_color_Injected(out Color ret);

		// Token: 0x060007CB RID: 1995
		[MethodImpl(4096)]
		private static extern void set_color_Injected(ref Color value);

		// Token: 0x060007CC RID: 1996
		[MethodImpl(4096)]
		private static extern void get_matrix_Injected(out Matrix4x4 ret);

		// Token: 0x060007CD RID: 1997
		[MethodImpl(4096)]
		private static extern void set_matrix_Injected(ref Matrix4x4 value);

		// Token: 0x060007CE RID: 1998
		[MethodImpl(4096)]
		private static extern void DrawFrustum_Injected(ref Vector3 center, float fov, float maxRange, float minRange, float aspect);
	}
}
