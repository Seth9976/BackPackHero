using System;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000159 RID: 345
	public static class XUnit
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x000103C0 File Offset: 0x0000E5C0
		public static ValueInput CompatibleValueInput(this IUnit unit, Type outputType)
		{
			Ensure.That("outputType").IsNotNull<Type>(outputType);
			return unit.valueInputs.Where((ValueInput valueInput) => ConversionUtility.CanConvert(outputType, valueInput.type, false)).OrderBy(delegate(ValueInput valueInput)
			{
				bool flag = outputType == valueInput.type;
				bool flag2 = !valueInput.hasValidConnection;
				if (flag2 && flag)
				{
					return 1;
				}
				if (flag2)
				{
					return 2;
				}
				if (flag)
				{
					return 3;
				}
				return 4;
			}).FirstOrDefault<ValueInput>();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001041C File Offset: 0x0000E61C
		public static ValueOutput CompatibleValueOutput(this IUnit unit, Type inputType)
		{
			Ensure.That("inputType").IsNotNull<Type>(inputType);
			return unit.valueOutputs.Where((ValueOutput valueOutput) => ConversionUtility.CanConvert(valueOutput.type, inputType, false)).OrderBy(delegate(ValueOutput valueOutput)
			{
				bool flag = inputType == valueOutput.type;
				bool flag2 = !valueOutput.hasValidConnection;
				if (flag2 && flag)
				{
					return 1;
				}
				if (flag2)
				{
					return 2;
				}
				if (flag)
				{
					return 3;
				}
				return 4;
			}).FirstOrDefault<ValueOutput>();
		}
	}
}
