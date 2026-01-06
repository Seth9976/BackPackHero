using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000415 RID: 1045
	public struct StencilState : IEquatable<StencilState>
	{
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x0003CE70 File Offset: 0x0003B070
		public static StencilState defaultValue
		{
			get
			{
				return new StencilState(true, byte.MaxValue, byte.MaxValue, CompareFunction.Always, StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x0003CE98 File Offset: 0x0003B098
		public StencilState(bool enabled = true, byte readMask = 255, byte writeMask = 255, CompareFunction compareFunction = CompareFunction.Always, StencilOp passOperation = StencilOp.Keep, StencilOp failOperation = StencilOp.Keep, StencilOp zFailOperation = StencilOp.Keep)
		{
			this = new StencilState(enabled, readMask, writeMask, compareFunction, passOperation, failOperation, zFailOperation, compareFunction, passOperation, failOperation, zFailOperation);
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x0003CEC0 File Offset: 0x0003B0C0
		public StencilState(bool enabled, byte readMask, byte writeMask, CompareFunction compareFunctionFront, StencilOp passOperationFront, StencilOp failOperationFront, StencilOp zFailOperationFront, CompareFunction compareFunctionBack, StencilOp passOperationBack, StencilOp failOperationBack, StencilOp zFailOperationBack)
		{
			this.m_Enabled = Convert.ToByte(enabled);
			this.m_ReadMask = readMask;
			this.m_WriteMask = writeMask;
			this.m_Padding = 0;
			this.m_CompareFunctionFront = (byte)compareFunctionFront;
			this.m_PassOperationFront = (byte)passOperationFront;
			this.m_FailOperationFront = (byte)failOperationFront;
			this.m_ZFailOperationFront = (byte)zFailOperationFront;
			this.m_CompareFunctionBack = (byte)compareFunctionBack;
			this.m_PassOperationBack = (byte)passOperationBack;
			this.m_FailOperationBack = (byte)failOperationBack;
			this.m_ZFailOperationBack = (byte)zFailOperationBack;
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x0003CF38 File Offset: 0x0003B138
		// (set) Token: 0x0600240C RID: 9228 RVA: 0x0003CF55 File Offset: 0x0003B155
		public bool enabled
		{
			get
			{
				return Convert.ToBoolean(this.m_Enabled);
			}
			set
			{
				this.m_Enabled = Convert.ToByte(value);
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x0003CF64 File Offset: 0x0003B164
		// (set) Token: 0x0600240E RID: 9230 RVA: 0x0003CF7C File Offset: 0x0003B17C
		public byte readMask
		{
			get
			{
				return this.m_ReadMask;
			}
			set
			{
				this.m_ReadMask = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x0003CF88 File Offset: 0x0003B188
		// (set) Token: 0x06002410 RID: 9232 RVA: 0x0003CFA0 File Offset: 0x0003B1A0
		public byte writeMask
		{
			get
			{
				return this.m_WriteMask;
			}
			set
			{
				this.m_WriteMask = value;
			}
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x0003CFAA File Offset: 0x0003B1AA
		public void SetCompareFunction(CompareFunction value)
		{
			this.compareFunctionFront = value;
			this.compareFunctionBack = value;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x0003CFBD File Offset: 0x0003B1BD
		public void SetPassOperation(StencilOp value)
		{
			this.passOperationFront = value;
			this.passOperationBack = value;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0003CFD0 File Offset: 0x0003B1D0
		public void SetFailOperation(StencilOp value)
		{
			this.failOperationFront = value;
			this.failOperationBack = value;
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x0003CFE3 File Offset: 0x0003B1E3
		public void SetZFailOperation(StencilOp value)
		{
			this.zFailOperationFront = value;
			this.zFailOperationBack = value;
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x0003CFF8 File Offset: 0x0003B1F8
		// (set) Token: 0x06002416 RID: 9238 RVA: 0x0003D010 File Offset: 0x0003B210
		public CompareFunction compareFunctionFront
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionFront;
			}
			set
			{
				this.m_CompareFunctionFront = (byte)value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x0003D01C File Offset: 0x0003B21C
		// (set) Token: 0x06002418 RID: 9240 RVA: 0x0003D034 File Offset: 0x0003B234
		public StencilOp passOperationFront
		{
			get
			{
				return (StencilOp)this.m_PassOperationFront;
			}
			set
			{
				this.m_PassOperationFront = (byte)value;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x0003D040 File Offset: 0x0003B240
		// (set) Token: 0x0600241A RID: 9242 RVA: 0x0003D058 File Offset: 0x0003B258
		public StencilOp failOperationFront
		{
			get
			{
				return (StencilOp)this.m_FailOperationFront;
			}
			set
			{
				this.m_FailOperationFront = (byte)value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x0003D064 File Offset: 0x0003B264
		// (set) Token: 0x0600241C RID: 9244 RVA: 0x0003D07C File Offset: 0x0003B27C
		public StencilOp zFailOperationFront
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationFront;
			}
			set
			{
				this.m_ZFailOperationFront = (byte)value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x0003D088 File Offset: 0x0003B288
		// (set) Token: 0x0600241E RID: 9246 RVA: 0x0003D0A0 File Offset: 0x0003B2A0
		public CompareFunction compareFunctionBack
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionBack;
			}
			set
			{
				this.m_CompareFunctionBack = (byte)value;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x0003D0AC File Offset: 0x0003B2AC
		// (set) Token: 0x06002420 RID: 9248 RVA: 0x0003D0C4 File Offset: 0x0003B2C4
		public StencilOp passOperationBack
		{
			get
			{
				return (StencilOp)this.m_PassOperationBack;
			}
			set
			{
				this.m_PassOperationBack = (byte)value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x0003D0D0 File Offset: 0x0003B2D0
		// (set) Token: 0x06002422 RID: 9250 RVA: 0x0003D0E8 File Offset: 0x0003B2E8
		public StencilOp failOperationBack
		{
			get
			{
				return (StencilOp)this.m_FailOperationBack;
			}
			set
			{
				this.m_FailOperationBack = (byte)value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x0003D0F4 File Offset: 0x0003B2F4
		// (set) Token: 0x06002424 RID: 9252 RVA: 0x0003D10C File Offset: 0x0003B30C
		public StencilOp zFailOperationBack
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationBack;
			}
			set
			{
				this.m_ZFailOperationBack = (byte)value;
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x0003D118 File Offset: 0x0003B318
		public bool Equals(StencilState other)
		{
			return this.m_Enabled == other.m_Enabled && this.m_ReadMask == other.m_ReadMask && this.m_WriteMask == other.m_WriteMask && this.m_CompareFunctionFront == other.m_CompareFunctionFront && this.m_PassOperationFront == other.m_PassOperationFront && this.m_FailOperationFront == other.m_FailOperationFront && this.m_ZFailOperationFront == other.m_ZFailOperationFront && this.m_CompareFunctionBack == other.m_CompareFunctionBack && this.m_PassOperationBack == other.m_PassOperationBack && this.m_FailOperationBack == other.m_FailOperationBack && this.m_ZFailOperationBack == other.m_ZFailOperationBack;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is StencilState && this.Equals((StencilState)obj);
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x0003D208 File Offset: 0x0003B408
		public override int GetHashCode()
		{
			int num = this.m_Enabled.GetHashCode();
			num = (num * 397) ^ this.m_ReadMask.GetHashCode();
			num = (num * 397) ^ this.m_WriteMask.GetHashCode();
			num = (num * 397) ^ this.m_CompareFunctionFront.GetHashCode();
			num = (num * 397) ^ this.m_PassOperationFront.GetHashCode();
			num = (num * 397) ^ this.m_FailOperationFront.GetHashCode();
			num = (num * 397) ^ this.m_ZFailOperationFront.GetHashCode();
			num = (num * 397) ^ this.m_CompareFunctionBack.GetHashCode();
			num = (num * 397) ^ this.m_PassOperationBack.GetHashCode();
			num = (num * 397) ^ this.m_FailOperationBack.GetHashCode();
			return (num * 397) ^ this.m_ZFailOperationBack.GetHashCode();
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x0003D2F0 File Offset: 0x0003B4F0
		public static bool operator ==(StencilState left, StencilState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0003D30C File Offset: 0x0003B50C
		public static bool operator !=(StencilState left, StencilState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D57 RID: 3415
		private byte m_Enabled;

		// Token: 0x04000D58 RID: 3416
		private byte m_ReadMask;

		// Token: 0x04000D59 RID: 3417
		private byte m_WriteMask;

		// Token: 0x04000D5A RID: 3418
		private byte m_Padding;

		// Token: 0x04000D5B RID: 3419
		private byte m_CompareFunctionFront;

		// Token: 0x04000D5C RID: 3420
		private byte m_PassOperationFront;

		// Token: 0x04000D5D RID: 3421
		private byte m_FailOperationFront;

		// Token: 0x04000D5E RID: 3422
		private byte m_ZFailOperationFront;

		// Token: 0x04000D5F RID: 3423
		private byte m_CompareFunctionBack;

		// Token: 0x04000D60 RID: 3424
		private byte m_PassOperationBack;

		// Token: 0x04000D61 RID: 3425
		private byte m_FailOperationBack;

		// Token: 0x04000D62 RID: 3426
		private byte m_ZFailOperationBack;
	}
}
