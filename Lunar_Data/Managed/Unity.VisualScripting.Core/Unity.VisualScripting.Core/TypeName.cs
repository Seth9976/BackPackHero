using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Unity.VisualScripting
{
	// Token: 0x02000127 RID: 295
	public class TypeName
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00023BC2 File Offset: 0x00021DC2
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x00023BCA File Offset: 0x00021DCA
		public string AssemblyDescription { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00023BD3 File Offset: 0x00021DD3
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x00023BDB File Offset: 0x00021DDB
		public string AssemblyName { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00023BE4 File Offset: 0x00021DE4
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x00023BEC File Offset: 0x00021DEC
		public string AssemblyVersion { get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00023BF5 File Offset: 0x00021DF5
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00023BFD File Offset: 0x00021DFD
		public string AssemblyCulture { get; private set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00023C06 File Offset: 0x00021E06
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00023C0E File Offset: 0x00021E0E
		public string AssemblyPublicKeyToken { get; private set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00023C17 File Offset: 0x00021E17
		public List<TypeName> GenericParameters { get; } = new List<TypeName>();

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00023C1F File Offset: 0x00021E1F
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00023C27 File Offset: 0x00021E27
		public string Name { get; private set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00023C30 File Offset: 0x00021E30
		public bool IsArray
		{
			get
			{
				return this.Name.EndsWith("[]");
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00023C42 File Offset: 0x00021E42
		public string LastName
		{
			get
			{
				return this.names[this.names.Count - 1];
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00023C5C File Offset: 0x00021E5C
		public static TypeName Parse(string s)
		{
			int num = 0;
			return new TypeName(s, ref num);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00023C74 File Offset: 0x00021E74
		private TypeName(string s, ref int index)
		{
			try
			{
				int num = index;
				int num2 = num;
				int? num3 = null;
				int? num4 = null;
				int? num5 = null;
				bool flag = false;
				TypeName.ParseState parseState = TypeName.ParseState.Name;
				while (index < s.Length)
				{
					char c = s[index];
					char? c2 = ((index + 1 < s.Length) ? new char?(s[index + 1]) : null);
					if (parseState == TypeName.ParseState.Name)
					{
						if (c == '[')
						{
							if (index == num)
							{
								flag = true;
								num2++;
							}
							else
							{
								char? c3 = c2;
								int? num6 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
								int i = 93;
								if (!((num6.GetValueOrDefault() == i) & (num6 != null)))
								{
									c3 = c2;
									num6 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
									i = 44;
									if (!((num6.GetValueOrDefault() == i) & (num6 != null)))
									{
										num3 = new int?(index);
										parseState = TypeName.ParseState.Generics;
										goto IL_01D0;
									}
								}
								parseState = TypeName.ParseState.Array;
							}
						}
						else if (c == ']')
						{
							if (flag)
							{
								break;
							}
						}
						else if (c == ',')
						{
							parseState = TypeName.ParseState.Assembly;
							num4 = new int?(index + 1);
							if (num3 == null)
							{
								num3 = new int?(index);
							}
						}
					}
					else if (parseState == TypeName.ParseState.Array)
					{
						if (c == ']')
						{
							parseState = TypeName.ParseState.Name;
						}
					}
					else if (parseState == TypeName.ParseState.Generics)
					{
						if (c == ']')
						{
							parseState = TypeName.ParseState.Name;
						}
						else if (c != ',' && c != ' ')
						{
							this.GenericParameters.Add(new TypeName(s, ref index));
						}
					}
					else if (parseState == TypeName.ParseState.Assembly && c == ']' && flag)
					{
						num5 = new int?(index);
						break;
					}
					IL_01D0:
					index++;
				}
				if (num3 == null)
				{
					num3 = new int?(s.Length);
				}
				if (num5 == null)
				{
					num5 = new int?(s.Length);
				}
				this.Name = s.Substring(num2, num3.Value - num2);
				if (this.Name.Contains('+'))
				{
					string[] array = this.Name.Split('+', StringSplitOptions.None);
					for (int i = 0; i < array.Length; i++)
					{
						string text;
						string text2;
						array[i].PartsAround('`', out text, out text2);
						this.names.Add(text);
						if (text2 != null)
						{
							this.genericarities.Add(int.Parse(text2));
						}
						else
						{
							this.genericarities.Add(0);
						}
					}
				}
				else
				{
					string text3;
					string text4;
					this.Name.PartsAround('`', out text3, out text4);
					this.names.Add(text3);
					if (text4 != null)
					{
						this.genericarities.Add(int.Parse(text4));
					}
					else
					{
						this.genericarities.Add(0);
					}
				}
				if (num4 != null)
				{
					this.AssemblyDescription = s.Substring(num4.Value, num5.Value - num4.Value);
					List<string> list = (from x in this.AssemblyDescription.Split(',', StringSplitOptions.None)
						select x.Trim()).ToList<string>();
					this.AssemblyVersion = TypeName.LookForPairThenRemove(list, "Version");
					this.AssemblyCulture = TypeName.LookForPairThenRemove(list, "Culture");
					this.AssemblyPublicKeyToken = TypeName.LookForPairThenRemove(list, "PublicKeyToken");
					if (list.Count > 0)
					{
						this.AssemblyName = list[0];
					}
				}
			}
			catch (Exception ex)
			{
				throw new FormatException("Failed to parse type name: " + s, ex);
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00024048 File Offset: 0x00022248
		private static string LookForPairThenRemove(List<string> strings, string Name)
		{
			for (int i = 0; i < strings.Count; i++)
			{
				string text = strings[i];
				if (text.IndexOf(Name) == 0)
				{
					int num = text.IndexOf('=');
					if (num > 0)
					{
						string text2 = text.Substring(num + 1);
						strings.RemoveAt(i);
						return text2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00024098 File Offset: 0x00022298
		public void ReplaceNamespace(string oldNamespace, string newNamespace)
		{
			if (this.names[0].StartsWith(oldNamespace + "."))
			{
				this.names[0] = newNamespace + "." + this.names[0].TrimStart(oldNamespace + ".");
			}
			foreach (TypeName typeName in this.GenericParameters)
			{
				typeName.ReplaceNamespace(oldNamespace, newNamespace);
			}
			this.UpdateName();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00024144 File Offset: 0x00022344
		public void ReplaceAssembly(string oldAssembly, string newAssembly)
		{
			if (this.AssemblyName != null && this.AssemblyName.StartsWith(oldAssembly))
			{
				this.AssemblyName = newAssembly + this.AssemblyName.TrimStart(oldAssembly);
			}
			foreach (TypeName typeName in this.GenericParameters)
			{
				typeName.ReplaceAssembly(oldAssembly, newAssembly);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000241C4 File Offset: 0x000223C4
		public void ReplaceName(string oldTypeName, Type newType)
		{
			string fullName = newType.FullName;
			Assembly assembly = newType.Assembly;
			this.ReplaceName(oldTypeName, fullName, (assembly != null) ? assembly.GetName() : null);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000241E8 File Offset: 0x000223E8
		public void ReplaceName(string oldTypeName, string newTypeName, AssemblyName newAssemblyName = null)
		{
			for (int i = 0; i < this.names.Count; i++)
			{
				if (TypeName.ToElementTypeName(this.names[i]) == oldTypeName)
				{
					this.names[i] = TypeName.ToArrayOrType(this.names[i], newTypeName);
					if (newAssemblyName != null)
					{
						this.SetAssemblyName(newAssemblyName);
					}
				}
			}
			foreach (TypeName typeName in this.GenericParameters)
			{
				typeName.ReplaceName(oldTypeName, newTypeName, newAssemblyName);
			}
			this.UpdateName();
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00024298 File Offset: 0x00022498
		private static string ToElementTypeName(string s)
		{
			if (!s.EndsWith("[]"))
			{
				return s;
			}
			return s.Replace("[]", string.Empty);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000242B9 File Offset: 0x000224B9
		private static string ToArrayOrType(string oldType, string newType)
		{
			if (oldType.EndsWith("[]"))
			{
				newType += "[]";
			}
			return newType;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000242D8 File Offset: 0x000224D8
		public void SetAssemblyName(AssemblyName newAssemblyName)
		{
			this.AssemblyDescription = newAssemblyName.ToString();
			this.AssemblyName = newAssemblyName.Name;
			this.AssemblyCulture = newAssemblyName.CultureName;
			this.AssemblyVersion = newAssemblyName.Version.ToString();
			byte[] publicKeyToken = newAssemblyName.GetPublicKeyToken();
			this.AssemblyPublicKeyToken = ((publicKeyToken != null) ? publicKeyToken.ToHexString() : null) ?? "null";
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002433C File Offset: 0x0002253C
		private void UpdateName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.names.Count; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append('+');
				}
				stringBuilder.Append(this.names[i]);
				if (this.genericarities[i] > 0)
				{
					stringBuilder.Append('`');
					stringBuilder.Append(this.genericarities[i]);
				}
			}
			this.Name = stringBuilder.ToString();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000243BC File Offset: 0x000225BC
		public string ToString(TypeNameDetail specification, TypeNameDetail genericsSpecification)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Name);
			if (this.GenericParameters.Count > 0)
			{
				stringBuilder.Append("[");
				bool flag = true;
				foreach (TypeName typeName in this.GenericParameters)
				{
					if (!flag)
					{
						stringBuilder.Append(",");
					}
					if (genericsSpecification != TypeNameDetail.Name)
					{
						stringBuilder.Append("[");
					}
					stringBuilder.Append(typeName.ToString(genericsSpecification, genericsSpecification));
					if (genericsSpecification != TypeNameDetail.Name)
					{
						stringBuilder.Append("]");
					}
					flag = false;
				}
				stringBuilder.Append("]");
			}
			if (specification == TypeNameDetail.Full)
			{
				if (!string.IsNullOrEmpty(this.AssemblyDescription))
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(this.AssemblyDescription);
				}
			}
			else if (specification == TypeNameDetail.NameAndAssembly && !string.IsNullOrEmpty(this.AssemblyName))
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.AssemblyName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000244E0 File Offset: 0x000226E0
		public override string ToString()
		{
			return this.ToString(TypeNameDetail.Name, TypeNameDetail.Full);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000244EA File Offset: 0x000226EA
		public string ToLooseString()
		{
			return this.ToString(TypeNameDetail.NameAndAssembly, TypeNameDetail.NameAndAssembly);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000244F4 File Offset: 0x000226F4
		public static string Simplify(string typeName)
		{
			return TypeName.Parse(typeName).ToLooseString();
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00024504 File Offset: 0x00022704
		public static string SimplifyFast(string typeName)
		{
			int num;
			for (;;)
			{
				num = typeName.IndexOf(", Version=", StringComparison.Ordinal);
				if (num < 0)
				{
					return typeName;
				}
				int num2 = typeName.IndexOf(']', num);
				if (num2 < 0)
				{
					break;
				}
				typeName = typeName.Remove(num, num2 - num);
			}
			typeName = typeName.Substring(0, num);
			return typeName;
		}

		// Token: 0x040001EF RID: 495
		private readonly List<string> names = new List<string>();

		// Token: 0x040001F0 RID: 496
		private readonly List<int> genericarities = new List<int>();

		// Token: 0x020001FA RID: 506
		private enum ParseState
		{
			// Token: 0x0400095A RID: 2394
			Name,
			// Token: 0x0400095B RID: 2395
			Array,
			// Token: 0x0400095C RID: 2396
			Generics,
			// Token: 0x0400095D RID: 2397
			Assembly
		}
	}
}
