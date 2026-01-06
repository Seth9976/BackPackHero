using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200020A RID: 522
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("BitField", "struct BitField;")]
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	[NativeHeader("Runtime/BaseClasses/BitField.h")]
	public struct LayerMask
	{
		// Token: 0x06001708 RID: 5896 RVA: 0x00024F4C File Offset: 0x0002314C
		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00024F64 File Offset: 0x00023164
		public static implicit operator LayerMask(int intVal)
		{
			LayerMask layerMask;
			layerMask.m_Mask = intVal;
			return layerMask;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x00024F80 File Offset: 0x00023180
		// (set) Token: 0x0600170B RID: 5899 RVA: 0x00024F98 File Offset: 0x00023198
		public int value
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x0600170C RID: 5900
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[NativeMethod("LayerToString")]
		[MethodImpl(4096)]
		public static extern string LayerToName(int layer);

		// Token: 0x0600170D RID: 5901
		[NativeMethod("StringToLayer")]
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		public static extern int NameToLayer(string layerName);

		// Token: 0x0600170E RID: 5902 RVA: 0x00024FA4 File Offset: 0x000231A4
		public static int GetMask(params string[] layerNames)
		{
			bool flag = layerNames == null;
			if (flag)
			{
				throw new ArgumentNullException("layerNames");
			}
			int num = 0;
			foreach (string text in layerNames)
			{
				int num2 = LayerMask.NameToLayer(text);
				bool flag2 = num2 != -1;
				if (flag2)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}

		// Token: 0x040007F2 RID: 2034
		[NativeName("m_Bits")]
		private int m_Mask;
	}
}
