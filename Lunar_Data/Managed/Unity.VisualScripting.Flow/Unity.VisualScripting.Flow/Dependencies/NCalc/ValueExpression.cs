using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x0200019C RID: 412
	public class ValueExpression : LogicalExpression
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x0001A1E4 File Offset: 0x000183E4
		public ValueExpression(object value, ValueType type)
		{
			this.Value = value;
			this.Type = type;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001A1FC File Offset: 0x000183FC
		public ValueExpression(object value)
		{
			switch (global::System.Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				this.Type = ValueType.Boolean;
				goto IL_00A5;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				this.Type = ValueType.Integer;
				goto IL_00A5;
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				this.Type = ValueType.Float;
				goto IL_00A5;
			case TypeCode.DateTime:
				this.Type = ValueType.DateTime;
				goto IL_00A5;
			case TypeCode.String:
				this.Type = ValueType.String;
				goto IL_00A5;
			}
			throw new EvaluationException("This value could not be handled: " + ((value != null) ? value.ToString() : null));
			IL_00A5:
			this.Value = value;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001A2B5 File Offset: 0x000184B5
		public ValueExpression(string value)
		{
			this.Value = value;
			this.Type = ValueType.String;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0001A2CB File Offset: 0x000184CB
		public ValueExpression(int value)
		{
			this.Value = value;
			this.Type = ValueType.Integer;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001A2E6 File Offset: 0x000184E6
		public ValueExpression(float value)
		{
			this.Value = value;
			this.Type = ValueType.Float;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001A301 File Offset: 0x00018501
		public ValueExpression(DateTime value)
		{
			this.Value = value;
			this.Type = ValueType.DateTime;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001A31C File Offset: 0x0001851C
		public ValueExpression(bool value)
		{
			this.Value = value;
			this.Type = ValueType.Boolean;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0001A337 File Offset: 0x00018537
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x0001A33F File Offset: 0x0001853F
		public object Value { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0001A348 File Offset: 0x00018548
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x0001A350 File Offset: 0x00018550
		public ValueType Type { get; set; }

		// Token: 0x06000B6D RID: 2925 RVA: 0x0001A359 File Offset: 0x00018559
		public override void Accept(LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
