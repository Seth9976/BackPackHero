using System;
using System.Collections.Generic;

namespace System.Reflection
{
	/// <summary>Provides methods that retrieve information about types at run time.</summary>
	// Token: 0x020008DC RID: 2268
	public static class RuntimeReflectionExtensions
	{
		/// <summary>Retrieves a collection that represents all the fields defined on a specified type.</summary>
		/// <returns>A collection of fields for the specified type.</returns>
		/// <param name="type">The type that contains the fields.</param>
		// Token: 0x06004B84 RID: 19332 RVA: 0x000F08D5 File Offset: 0x000EEAD5
		public static IEnumerable<FieldInfo> GetRuntimeFields(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves a collection that represents all methods defined on a specified type.</summary>
		/// <returns>A collection of methods for the specified type.</returns>
		/// <param name="type">The type that contains the methods.</param>
		// Token: 0x06004B85 RID: 19333 RVA: 0x000F08F3 File Offset: 0x000EEAF3
		public static IEnumerable<MethodInfo> GetRuntimeMethods(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves a collection that represents all the properties defined on a specified type.</summary>
		/// <returns>A collection of properties for the specified type.</returns>
		/// <param name="type">The type that contains the properties.</param>
		// Token: 0x06004B86 RID: 19334 RVA: 0x000F0911 File Offset: 0x000EEB11
		public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves a collection that represents all the events defined on a specified type.</summary>
		/// <returns>A collection of events for the specified type.</returns>
		/// <param name="type">The type that contains the events.</param>
		// Token: 0x06004B87 RID: 19335 RVA: 0x000F092F File Offset: 0x000EEB2F
		public static IEnumerable<EventInfo> GetRuntimeEvents(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves an object that represents a specified field.</summary>
		/// <returns>An object that represents the specified field, or null if the field is not found.</returns>
		/// <param name="type">The type that contains the field.</param>
		/// <param name="name">The name of the field.</param>
		// Token: 0x06004B88 RID: 19336 RVA: 0x000F094D File Offset: 0x000EEB4D
		public static FieldInfo GetRuntimeField(this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetField(name);
		}

		/// <summary>Retrieves an object that represents a specified method.</summary>
		/// <returns>An object that represents the specified method, or null if the method is not found.</returns>
		/// <param name="type">The type that contains the method.</param>
		/// <param name="name">The name of the method.</param>
		/// <param name="parameters">An array that contains the method's parameters.</param>
		// Token: 0x06004B89 RID: 19337 RVA: 0x000F096A File Offset: 0x000EEB6A
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] parameters)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetMethod(name, parameters);
		}

		/// <summary>Retrieves an object that represents a specified property.</summary>
		/// <returns>An object that represents the specified property, or null if the property is not found.</returns>
		/// <param name="type">The type that contains the property.</param>
		/// <param name="name">The name of the property.</param>
		// Token: 0x06004B8A RID: 19338 RVA: 0x000F0988 File Offset: 0x000EEB88
		public static PropertyInfo GetRuntimeProperty(this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetProperty(name);
		}

		/// <summary>Retrieves an object that represents the specified event.</summary>
		/// <returns>An object that represents the specified event, or null if the event is not found.</returns>
		/// <param name="type">The type that contains the event.</param>
		/// <param name="name">The name of the event.</param>
		// Token: 0x06004B8B RID: 19339 RVA: 0x000F09A5 File Offset: 0x000EEBA5
		public static EventInfo GetRuntimeEvent(this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetEvent(name);
		}

		/// <summary>Retrieves an object that represents the specified method on the direct or indirect base class where the method was first declared.</summary>
		/// <returns>An object that represents the specified method's initial declaration on a base class.</returns>
		/// <param name="method">The method to retrieve information about.</param>
		// Token: 0x06004B8C RID: 19340 RVA: 0x000F09C2 File Offset: 0x000EEBC2
		public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return method.GetBaseDefinition();
		}

		/// <summary>Returns an interface mapping for the specified type and the specified interface.</summary>
		/// <returns>An object that represents the interface mapping for the specified interface and type.</returns>
		/// <param name="typeInfo">The type to retrieve a mapping for.</param>
		/// <param name="interfaceType">The interface to retrieve a mapping for.</param>
		// Token: 0x06004B8D RID: 19341 RVA: 0x000F09DE File Offset: 0x000EEBDE
		public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			return typeInfo.GetInterfaceMap(interfaceType);
		}

		/// <summary>Gets an object that represents the method represented by the specified delegate.</summary>
		/// <returns>An object that represents the method.</returns>
		/// <param name="del">The delegate to examine.</param>
		// Token: 0x06004B8E RID: 19342 RVA: 0x000F09FB File Offset: 0x000EEBFB
		public static MethodInfo GetMethodInfo(this Delegate del)
		{
			if (del == null)
			{
				throw new ArgumentNullException("del");
			}
			return del.Method;
		}

		// Token: 0x04002F66 RID: 12134
		private const BindingFlags Everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
