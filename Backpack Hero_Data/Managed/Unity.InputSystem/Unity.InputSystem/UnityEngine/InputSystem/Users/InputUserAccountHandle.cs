using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x0200007E RID: 126
	public struct InputUserAccountHandle : IEquatable<InputUserAccountHandle>
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00038C53 File Offset: 0x00036E53
		public string apiName
		{
			get
			{
				return this.m_ApiName;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00038C5B File Offset: 0x00036E5B
		public ulong handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00038C63 File Offset: 0x00036E63
		public InputUserAccountHandle(string apiName, ulong handle)
		{
			if (string.IsNullOrEmpty(apiName))
			{
				throw new ArgumentNullException("apiName");
			}
			this.m_ApiName = apiName;
			this.m_Handle = handle;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00038C86 File Offset: 0x00036E86
		public override string ToString()
		{
			if (this.m_ApiName == null)
			{
				return base.ToString();
			}
			return string.Format("{0}({1})", this.m_ApiName, this.m_Handle);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00038CBC File Offset: 0x00036EBC
		public bool Equals(InputUserAccountHandle other)
		{
			return string.Equals(this.apiName, other.apiName) && object.Equals(this.handle, other.handle);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00038CF0 File Offset: 0x00036EF0
		public override bool Equals(object obj)
		{
			return obj != null && obj is InputUserAccountHandle && this.Equals((InputUserAccountHandle)obj);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00038D0D File Offset: 0x00036F0D
		public static bool operator ==(InputUserAccountHandle left, InputUserAccountHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00038D17 File Offset: 0x00036F17
		public static bool operator !=(InputUserAccountHandle left, InputUserAccountHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00038D24 File Offset: 0x00036F24
		public override int GetHashCode()
		{
			return (((this.apiName != null) ? this.apiName.GetHashCode() : 0) * 397) ^ this.handle.GetHashCode();
		}

		// Token: 0x04000382 RID: 898
		private string m_ApiName;

		// Token: 0x04000383 RID: 899
		private ulong m_Handle;
	}
}
