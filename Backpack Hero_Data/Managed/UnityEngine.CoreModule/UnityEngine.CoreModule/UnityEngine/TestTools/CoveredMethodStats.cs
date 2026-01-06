using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	// Token: 0x0200048C RID: 1164
	[NativeType(CodegenOptions.Custom, "ManagedCoveredMethodStats", Header = "Runtime/Scripting/ScriptingCoverage.bindings.h")]
	public struct CoveredMethodStats
	{
		// Token: 0x0600292F RID: 10543 RVA: 0x00044004 File Offset: 0x00042204
		private string GetTypeDisplayName(Type t)
		{
			bool flag = t == typeof(int);
			string text;
			if (flag)
			{
				text = "int";
			}
			else
			{
				bool flag2 = t == typeof(bool);
				if (flag2)
				{
					text = "bool";
				}
				else
				{
					bool flag3 = t == typeof(float);
					if (flag3)
					{
						text = "float";
					}
					else
					{
						bool flag4 = t == typeof(double);
						if (flag4)
						{
							text = "double";
						}
						else
						{
							bool flag5 = t == typeof(void);
							if (flag5)
							{
								text = "void";
							}
							else
							{
								bool flag6 = t == typeof(string);
								if (flag6)
								{
									text = "string";
								}
								else
								{
									bool flag7 = t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List);
									if (flag7)
									{
										text = "System.Collections.Generic.List<" + this.GetTypeDisplayName(t.GetGenericArguments()[0]) + ">";
									}
									else
									{
										bool flag8 = t.IsArray && t.GetArrayRank() == 1;
										if (flag8)
										{
											text = this.GetTypeDisplayName(t.GetElementType()) + "[]";
										}
										else
										{
											text = t.FullName;
										}
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x00044144 File Offset: 0x00042344
		public override string ToString()
		{
			bool flag = this.method == null;
			string text;
			if (flag)
			{
				text = "<no method>";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.GetTypeDisplayName(this.method.DeclaringType));
				stringBuilder.Append(".");
				stringBuilder.Append(this.method.Name);
				stringBuilder.Append("(");
				bool flag2 = false;
				foreach (ParameterInfo parameterInfo in this.method.GetParameters())
				{
					bool flag3 = flag2;
					if (flag3)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(this.GetTypeDisplayName(parameterInfo.ParameterType));
					stringBuilder.Append(" ");
					stringBuilder.Append(parameterInfo.Name);
					flag2 = true;
				}
				stringBuilder.Append(")");
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x04000FA4 RID: 4004
		public MethodBase method;

		// Token: 0x04000FA5 RID: 4005
		public int totalSequencePoints;

		// Token: 0x04000FA6 RID: 4006
		public int uncoveredSequencePoints;
	}
}
