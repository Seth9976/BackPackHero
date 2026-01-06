using System;
using System.Collections;
using System.Reflection;

namespace System.Diagnostics
{
	/// <summary>Identifies a switch used in an assembly, class, or member.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000225 RID: 549
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public sealed class SwitchAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SwitchAttribute" /> class, specifying the name and the type of the switch. </summary>
		/// <param name="switchName">The display name of the switch.</param>
		/// <param name="switchType">The type of the switch.</param>
		// Token: 0x06000FF9 RID: 4089 RVA: 0x000469C0 File Offset: 0x00044BC0
		public SwitchAttribute(string switchName, Type switchType)
		{
			this.SwitchName = switchName;
			this.SwitchType = switchType;
		}

		/// <summary>Gets or sets the display name of the switch.</summary>
		/// <returns>The display name of the switch.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchName" /> is set to null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchName" /> is set to an empty string.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x000469D6 File Offset: 0x00044BD6
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x000469E0 File Offset: 0x00044BE0
		public string SwitchName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("Argument {0} cannot be null or zero-length.", new object[] { "value" }), "value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the type of the switch.</summary>
		/// <returns>The type of the switch.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchType" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00046A2D File Offset: 0x00044C2D
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x00046A35 File Offset: 0x00044C35
		public Type SwitchType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		/// <summary>Gets or sets the description of the switch.</summary>
		/// <returns>The description of the switch.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00046A52 File Offset: 0x00044C52
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x00046A5A File Offset: 0x00044C5A
		public string SwitchDescription
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Returns all switch attributes for the specified assembly.</summary>
		/// <returns>An array that contains all the switch attributes for the assembly.</returns>
		/// <param name="assembly">The assembly to check for switch attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001000 RID: 4096 RVA: 0x00046A64 File Offset: 0x00044C64
		public static SwitchAttribute[] GetAll(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			ArrayList arrayList = new ArrayList();
			object[] customAttributes = assembly.GetCustomAttributes(typeof(SwitchAttribute), false);
			arrayList.AddRange(customAttributes);
			Type[] types = assembly.GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				SwitchAttribute.GetAllRecursive(types[i], arrayList);
			}
			SwitchAttribute[] array = new SwitchAttribute[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00046ADC File Offset: 0x00044CDC
		private static void GetAllRecursive(Type type, ArrayList switchAttribs)
		{
			SwitchAttribute.GetAllRecursive(type, switchAttribs);
			MemberInfo[] members = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < members.Length; i++)
			{
				if (!(members[i] is Type))
				{
					SwitchAttribute.GetAllRecursive(members[i], switchAttribs);
				}
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00046B1C File Offset: 0x00044D1C
		private static void GetAllRecursive(MemberInfo member, ArrayList switchAttribs)
		{
			object[] customAttributes = member.GetCustomAttributes(typeof(SwitchAttribute), false);
			switchAttribs.AddRange(customAttributes);
		}

		// Token: 0x040009C0 RID: 2496
		private Type type;

		// Token: 0x040009C1 RID: 2497
		private string name;

		// Token: 0x040009C2 RID: 2498
		private string description;
	}
}
