using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200041F RID: 1055
	[NativeHeader("Runtime/Shaders/PassIdentifier.h")]
	[UsedByNativeCode]
	public readonly struct PassIdentifier : IEquatable<PassIdentifier>
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x0003E418 File Offset: 0x0003C618
		public uint SubshaderIndex
		{
			get
			{
				return this.m_SubShaderIndex;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x0003E430 File Offset: 0x0003C630
		public uint PassIndex
		{
			get
			{
				return this.m_PassIndex;
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0003E448 File Offset: 0x0003C648
		public override bool Equals(object o)
		{
			bool flag;
			if (o is PassIdentifier)
			{
				PassIdentifier passIdentifier = (PassIdentifier)o;
				flag = this.Equals(passIdentifier);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0003E474 File Offset: 0x0003C674
		public bool Equals(PassIdentifier rhs)
		{
			return this.m_SubShaderIndex == rhs.m_SubShaderIndex && this.m_PassIndex == rhs.m_PassIndex;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x0003E4A8 File Offset: 0x0003C6A8
		public static bool operator ==(PassIdentifier lhs, PassIdentifier rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x0003E4C4 File Offset: 0x0003C6C4
		public static bool operator !=(PassIdentifier lhs, PassIdentifier rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0003E4E0 File Offset: 0x0003C6E0
		public override int GetHashCode()
		{
			return this.m_SubShaderIndex.GetHashCode() ^ this.m_PassIndex.GetHashCode();
		}

		// Token: 0x04000DA1 RID: 3489
		internal readonly uint m_SubShaderIndex;

		// Token: 0x04000DA2 RID: 3490
		internal readonly uint m_PassIndex;
	}
}
