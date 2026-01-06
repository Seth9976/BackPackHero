using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000119 RID: 281
	[NativeHeader("Modules/IMGUI/GUIStyle.h")]
	[UsedByNativeCode]
	[Serializable]
	[StructLayout(0)]
	public class RectOffset : IFormattable
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x0000B58D File Offset: 0x0000978D
		public RectOffset()
		{
			this.m_Ptr = RectOffset.InternalCreate();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000B5A2 File Offset: 0x000097A2
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal RectOffset(object sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000B5BC File Offset: 0x000097BC
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Destroy();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000B5FC File Offset: 0x000097FC
		public RectOffset(int left, int right, int top, int bottom)
		{
			this.m_Ptr = RectOffset.InternalCreate();
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000B634 File Offset: 0x00009834
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000B650 File Offset: 0x00009850
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000B66C File Offset: 0x0000986C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", new object[]
			{
				this.left.ToString(format, formatProvider),
				this.right.ToString(format, formatProvider),
				this.top.ToString(format, formatProvider),
				this.bottom.ToString(format, formatProvider)
			});
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000B6F0 File Offset: 0x000098F0
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				RectOffset.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x0600076D RID: 1901
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private static extern IntPtr InternalCreate();

		// Token: 0x0600076E RID: 1902
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600076F RID: 1903
		// (set) Token: 0x06000770 RID: 1904
		[NativeProperty("left", false, TargetType.Field)]
		public extern int left
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000771 RID: 1905
		// (set) Token: 0x06000772 RID: 1906
		[NativeProperty("right", false, TargetType.Field)]
		public extern int right
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000773 RID: 1907
		// (set) Token: 0x06000774 RID: 1908
		[NativeProperty("top", false, TargetType.Field)]
		public extern int top
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000775 RID: 1909
		// (set) Token: 0x06000776 RID: 1910
		[NativeProperty("bottom", false, TargetType.Field)]
		public extern int bottom
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000777 RID: 1911
		public extern int horizontal
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000778 RID: 1912
		public extern int vertical
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000B72C File Offset: 0x0000992C
		public Rect Add(Rect rect)
		{
			Rect rect2;
			this.Add_Injected(ref rect, out rect2);
			return rect2;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000B744 File Offset: 0x00009944
		public Rect Remove(Rect rect)
		{
			Rect rect2;
			this.Remove_Injected(ref rect, out rect2);
			return rect2;
		}

		// Token: 0x0600077B RID: 1915
		[MethodImpl(4096)]
		private extern void Add_Injected(ref Rect rect, out Rect ret);

		// Token: 0x0600077C RID: 1916
		[MethodImpl(4096)]
		private extern void Remove_Injected(ref Rect rect, out Rect ret);

		// Token: 0x0400039D RID: 925
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0400039E RID: 926
		private readonly object m_SourceStyle;
	}
}
