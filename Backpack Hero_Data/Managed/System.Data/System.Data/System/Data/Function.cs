using System;

namespace System.Data
{
	// Token: 0x020000A3 RID: 163
	internal sealed class Function
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x000311D6 File Offset: 0x0002F3D6
		internal Function()
		{
			this._name = null;
			this._id = FunctionId.none;
			this._result = null;
			this._isValidateArguments = false;
			this._argumentCount = 0;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00031210 File Offset: 0x0002F410
		internal Function(string name, FunctionId id, Type result, bool IsValidateArguments, bool IsVariantArgumentList, int argumentCount, Type a1, Type a2, Type a3)
		{
			this._name = name;
			this._id = id;
			this._result = result;
			this._isValidateArguments = IsValidateArguments;
			this._isVariantArgumentList = IsVariantArgumentList;
			this._argumentCount = argumentCount;
			if (a1 != null)
			{
				this._parameters[0] = a1;
			}
			if (a2 != null)
			{
				this._parameters[1] = a2;
			}
			if (a3 != null)
			{
				this._parameters[2] = a3;
			}
		}

		// Token: 0x040006F6 RID: 1782
		internal readonly string _name;

		// Token: 0x040006F7 RID: 1783
		internal readonly FunctionId _id;

		// Token: 0x040006F8 RID: 1784
		internal readonly Type _result;

		// Token: 0x040006F9 RID: 1785
		internal readonly bool _isValidateArguments;

		// Token: 0x040006FA RID: 1786
		internal readonly bool _isVariantArgumentList;

		// Token: 0x040006FB RID: 1787
		internal readonly int _argumentCount;

		// Token: 0x040006FC RID: 1788
		internal readonly Type[] _parameters = new Type[3];

		// Token: 0x040006FD RID: 1789
		internal static string[] s_functionName = new string[]
		{
			"Unknown", "Ascii", "Char", "CharIndex", "Difference", "Len", "Lower", "LTrim", "Patindex", "Replicate",
			"Reverse", "Right", "RTrim", "Soundex", "Space", "Str", "Stuff", "Substring", "Upper", "IsNull",
			"Iif", "Convert", "cInt", "cBool", "cDate", "cDbl", "cStr", "Abs", "Acos", "In",
			"Trim", "Sum", "Avg", "Min", "Max", "Count", "StDev", "Var", "DateTimeOffset"
		};
	}
}
