using System;

namespace Steamworks
{
	// Token: 0x0200017B RID: 379
	internal class CallbackIdentities
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x0000CC14 File Offset: 0x0000AE14
		public static int GetCallbackIdentity(Type callbackStruct)
		{
			object[] customAttributes = callbackStruct.GetCustomAttributes(typeof(CallbackIdentityAttribute), false);
			int num = 0;
			if (num >= customAttributes.Length)
			{
				throw new Exception("Callback number not found for struct " + ((callbackStruct != null) ? callbackStruct.ToString() : null));
			}
			return ((CallbackIdentityAttribute)customAttributes[num]).Identity;
		}
	}
}
