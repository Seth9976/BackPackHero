using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000212 RID: 530
	[NativeClass(null)]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[RequiredByNativeCode]
	[ExtensionOfNativeClass]
	[StructLayout(0)]
	public class ScriptableObject : Object
	{
		// Token: 0x06001748 RID: 5960 RVA: 0x0002560E File Offset: 0x0002380E
		public ScriptableObject()
		{
			ScriptableObject.CreateScriptableObject(this);
		}

		// Token: 0x06001749 RID: 5961
		[Obsolete("Use EditorUtility.SetDirty instead")]
		[NativeConditional("ENABLE_MONO")]
		[MethodImpl(4096)]
		public extern void SetDirty();

		// Token: 0x0600174A RID: 5962 RVA: 0x00025620 File Offset: 0x00023820
		public static ScriptableObject CreateInstance(string className)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromName(className);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00025638 File Offset: 0x00023838
		public static ScriptableObject CreateInstance(Type type)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromType(type, true);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00025654 File Offset: 0x00023854
		public static T CreateInstance<T>() where T : ScriptableObject
		{
			return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0002567C File Offset: 0x0002387C
		[EditorBrowsable(1)]
		internal static ScriptableObject CreateInstance(Type type, Action<ScriptableObject> initialize)
		{
			bool flag = !typeof(ScriptableObject).IsAssignableFrom(type);
			if (flag)
			{
				throw new ArgumentException("Type must inherit ScriptableObject.", "type");
			}
			ScriptableObject scriptableObject = ScriptableObject.CreateScriptableObjectInstanceFromType(type, false);
			try
			{
				initialize.Invoke(scriptableObject);
			}
			finally
			{
				ScriptableObject.ResetAndApplyDefaultInstances(scriptableObject);
			}
			return scriptableObject;
		}

		// Token: 0x0600174E RID: 5966
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void CreateScriptableObject([Writable] ScriptableObject self);

		// Token: 0x0600174F RID: 5967
		[FreeFunction("Scripting::CreateScriptableObject")]
		[MethodImpl(4096)]
		private static extern ScriptableObject CreateScriptableObjectInstanceFromName(string className);

		// Token: 0x06001750 RID: 5968
		[FreeFunction("Scripting::CreateScriptableObjectWithType")]
		[MethodImpl(4096)]
		internal static extern ScriptableObject CreateScriptableObjectInstanceFromType(Type type, bool applyDefaultsAndReset);

		// Token: 0x06001751 RID: 5969
		[FreeFunction("Scripting::ResetAndApplyDefaultInstances")]
		[MethodImpl(4096)]
		internal static extern void ResetAndApplyDefaultInstances([NotNull("NullExceptionObject")] Object obj);
	}
}
