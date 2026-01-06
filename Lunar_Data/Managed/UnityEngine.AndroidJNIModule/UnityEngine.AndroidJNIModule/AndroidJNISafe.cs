using System;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	internal class AndroidJNISafe
	{
		// Token: 0x06000122 RID: 290 RVA: 0x000064B8 File Offset: 0x000046B8
		public static void CheckException()
		{
			IntPtr intPtr = AndroidJNI.ExceptionOccurred();
			bool flag = intPtr != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.ExceptionClear();
				IntPtr intPtr2 = AndroidJNI.FindClass("java/lang/Throwable");
				IntPtr intPtr3 = AndroidJNI.FindClass("android/util/Log");
				try
				{
					IntPtr methodID = AndroidJNI.GetMethodID(intPtr2, "toString", "()Ljava/lang/String;");
					IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr3, "getStackTraceString", "(Ljava/lang/Throwable;)Ljava/lang/String;");
					string text = AndroidJNI.CallStringMethod(intPtr, methodID, new jvalue[0]);
					jvalue[] array = new jvalue[1];
					array[0].l = intPtr;
					string text2 = AndroidJNI.CallStaticStringMethod(intPtr3, staticMethodID, array);
					throw new AndroidJavaException(text, text2);
				}
				finally
				{
					AndroidJNISafe.DeleteLocalRef(intPtr);
					AndroidJNISafe.DeleteLocalRef(intPtr2);
					AndroidJNISafe.DeleteLocalRef(intPtr3);
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006584 File Offset: 0x00004784
		public static void DeleteGlobalRef(IntPtr globalref)
		{
			bool flag = globalref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteGlobalRef(globalref);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000065A8 File Offset: 0x000047A8
		public static void DeleteWeakGlobalRef(IntPtr globalref)
		{
			bool flag = globalref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteWeakGlobalRef(globalref);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000065CC File Offset: 0x000047CC
		public static void DeleteLocalRef(IntPtr localref)
		{
			bool flag = localref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteLocalRef(localref);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000065F0 File Offset: 0x000047F0
		public static IntPtr NewString(string chars)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.NewString(chars);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006624 File Offset: 0x00004824
		public static IntPtr NewStringUTF(string bytes)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.NewStringUTF(bytes);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006658 File Offset: 0x00004858
		public static string GetStringChars(IntPtr str)
		{
			string stringChars;
			try
			{
				stringChars = AndroidJNI.GetStringChars(str);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringChars;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000668C File Offset: 0x0000488C
		public static string GetStringUTFChars(IntPtr str)
		{
			string stringUTFChars;
			try
			{
				stringUTFChars = AndroidJNI.GetStringUTFChars(str);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringUTFChars;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000066C0 File Offset: 0x000048C0
		public static IntPtr GetObjectClass(IntPtr ptr)
		{
			IntPtr objectClass;
			try
			{
				objectClass = AndroidJNI.GetObjectClass(ptr);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectClass;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000066F4 File Offset: 0x000048F4
		public static IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticMethodID;
			try
			{
				staticMethodID = AndroidJNI.GetStaticMethodID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticMethodID;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006728 File Offset: 0x00004928
		public static IntPtr GetMethodID(IntPtr obj, string name, string sig)
		{
			IntPtr methodID;
			try
			{
				methodID = AndroidJNI.GetMethodID(obj, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return methodID;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000675C File Offset: 0x0000495C
		public static IntPtr GetFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr fieldID;
			try
			{
				fieldID = AndroidJNI.GetFieldID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return fieldID;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006790 File Offset: 0x00004990
		public static IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticFieldID;
			try
			{
				staticFieldID = AndroidJNI.GetStaticFieldID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticFieldID;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000067C4 File Offset: 0x000049C4
		public static IntPtr FromReflectedMethod(IntPtr refMethod)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.FromReflectedMethod(refMethod);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000067F8 File Offset: 0x000049F8
		public static IntPtr FromReflectedField(IntPtr refField)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.FromReflectedField(refField);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000682C File Offset: 0x00004A2C
		public static IntPtr FindClass(string name)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.FindClass(name);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006860 File Offset: 0x00004A60
		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.NewObject(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006894 File Offset: 0x00004A94
		public static void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetStaticObjectField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000068C8 File Offset: 0x00004AC8
		public static void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStaticStringField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000068FC File Offset: 0x00004AFC
		public static void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetStaticCharField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006930 File Offset: 0x00004B30
		public static void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetStaticDoubleField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006964 File Offset: 0x00004B64
		public static void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetStaticFloatField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006998 File Offset: 0x00004B98
		public static void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetStaticLongField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000069CC File Offset: 0x00004BCC
		public static void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetStaticShortField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006A00 File Offset: 0x00004C00
		public static void SetStaticSByteField(IntPtr clazz, IntPtr fieldID, sbyte val)
		{
			try
			{
				AndroidJNI.SetStaticSByteField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006A34 File Offset: 0x00004C34
		public static void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetStaticBooleanField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006A68 File Offset: 0x00004C68
		public static void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetStaticIntField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006A9C File Offset: 0x00004C9C
		public static IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID)
		{
			IntPtr staticObjectField;
			try
			{
				staticObjectField = AndroidJNI.GetStaticObjectField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticObjectField;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public static string GetStaticStringField(IntPtr clazz, IntPtr fieldID)
		{
			string staticStringField;
			try
			{
				staticStringField = AndroidJNI.GetStaticStringField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticStringField;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006B04 File Offset: 0x00004D04
		public static char GetStaticCharField(IntPtr clazz, IntPtr fieldID)
		{
			char staticCharField;
			try
			{
				staticCharField = AndroidJNI.GetStaticCharField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticCharField;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006B38 File Offset: 0x00004D38
		public static double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID)
		{
			double staticDoubleField;
			try
			{
				staticDoubleField = AndroidJNI.GetStaticDoubleField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticDoubleField;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006B6C File Offset: 0x00004D6C
		public static float GetStaticFloatField(IntPtr clazz, IntPtr fieldID)
		{
			float staticFloatField;
			try
			{
				staticFloatField = AndroidJNI.GetStaticFloatField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticFloatField;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public static long GetStaticLongField(IntPtr clazz, IntPtr fieldID)
		{
			long staticLongField;
			try
			{
				staticLongField = AndroidJNI.GetStaticLongField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticLongField;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006BD4 File Offset: 0x00004DD4
		public static short GetStaticShortField(IntPtr clazz, IntPtr fieldID)
		{
			short staticShortField;
			try
			{
				staticShortField = AndroidJNI.GetStaticShortField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticShortField;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006C08 File Offset: 0x00004E08
		public static sbyte GetStaticSByteField(IntPtr clazz, IntPtr fieldID)
		{
			sbyte staticSByteField;
			try
			{
				staticSByteField = AndroidJNI.GetStaticSByteField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticSByteField;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006C3C File Offset: 0x00004E3C
		public static bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID)
		{
			bool staticBooleanField;
			try
			{
				staticBooleanField = AndroidJNI.GetStaticBooleanField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticBooleanField;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006C70 File Offset: 0x00004E70
		public static int GetStaticIntField(IntPtr clazz, IntPtr fieldID)
		{
			int staticIntField;
			try
			{
				staticIntField = AndroidJNI.GetStaticIntField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticIntField;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			try
			{
				AndroidJNI.CallStaticVoidMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006CD8 File Offset: 0x00004ED8
		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.CallStaticObjectMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006D0C File Offset: 0x00004F0C
		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			string text;
			try
			{
				text = AndroidJNI.CallStaticStringMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return text;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006D40 File Offset: 0x00004F40
		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			char c;
			try
			{
				c = AndroidJNI.CallStaticCharMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return c;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006D74 File Offset: 0x00004F74
		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			double num;
			try
			{
				num = AndroidJNI.CallStaticDoubleMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			float num;
			try
			{
				num = AndroidJNI.CallStaticFloatMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006DDC File Offset: 0x00004FDC
		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			long num;
			try
			{
				num = AndroidJNI.CallStaticLongMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006E10 File Offset: 0x00005010
		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			short num;
			try
			{
				num = AndroidJNI.CallStaticShortMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006E44 File Offset: 0x00005044
		public static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			sbyte b;
			try
			{
				b = AndroidJNI.CallStaticSByteMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return b;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006E78 File Offset: 0x00005078
		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			bool flag;
			try
			{
				flag = AndroidJNI.CallStaticBooleanMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return flag;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006EAC File Offset: 0x000050AC
		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			int num;
			try
			{
				num = AndroidJNI.CallStaticIntMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006EE0 File Offset: 0x000050E0
		public static void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetObjectField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006F14 File Offset: 0x00005114
		public static void SetStringField(IntPtr obj, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStringField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006F48 File Offset: 0x00005148
		public static void SetCharField(IntPtr obj, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetCharField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006F7C File Offset: 0x0000517C
		public static void SetDoubleField(IntPtr obj, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetDoubleField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006FB0 File Offset: 0x000051B0
		public static void SetFloatField(IntPtr obj, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetFloatField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006FE4 File Offset: 0x000051E4
		public static void SetLongField(IntPtr obj, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetLongField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007018 File Offset: 0x00005218
		public static void SetShortField(IntPtr obj, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetShortField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000704C File Offset: 0x0000524C
		public static void SetSByteField(IntPtr obj, IntPtr fieldID, sbyte val)
		{
			try
			{
				AndroidJNI.SetSByteField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007080 File Offset: 0x00005280
		public static void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetBooleanField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000070B4 File Offset: 0x000052B4
		public static void SetIntField(IntPtr obj, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetIntField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000070E8 File Offset: 0x000052E8
		public static IntPtr GetObjectField(IntPtr obj, IntPtr fieldID)
		{
			IntPtr objectField;
			try
			{
				objectField = AndroidJNI.GetObjectField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectField;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000711C File Offset: 0x0000531C
		public static string GetStringField(IntPtr obj, IntPtr fieldID)
		{
			string stringField;
			try
			{
				stringField = AndroidJNI.GetStringField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringField;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007150 File Offset: 0x00005350
		public static char GetCharField(IntPtr obj, IntPtr fieldID)
		{
			char charField;
			try
			{
				charField = AndroidJNI.GetCharField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return charField;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007184 File Offset: 0x00005384
		public static double GetDoubleField(IntPtr obj, IntPtr fieldID)
		{
			double doubleField;
			try
			{
				doubleField = AndroidJNI.GetDoubleField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return doubleField;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000071B8 File Offset: 0x000053B8
		public static float GetFloatField(IntPtr obj, IntPtr fieldID)
		{
			float floatField;
			try
			{
				floatField = AndroidJNI.GetFloatField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return floatField;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000071EC File Offset: 0x000053EC
		public static long GetLongField(IntPtr obj, IntPtr fieldID)
		{
			long longField;
			try
			{
				longField = AndroidJNI.GetLongField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return longField;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007220 File Offset: 0x00005420
		public static short GetShortField(IntPtr obj, IntPtr fieldID)
		{
			short shortField;
			try
			{
				shortField = AndroidJNI.GetShortField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return shortField;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007254 File Offset: 0x00005454
		public static sbyte GetSByteField(IntPtr obj, IntPtr fieldID)
		{
			sbyte sbyteField;
			try
			{
				sbyteField = AndroidJNI.GetSByteField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return sbyteField;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007288 File Offset: 0x00005488
		public static bool GetBooleanField(IntPtr obj, IntPtr fieldID)
		{
			bool booleanField;
			try
			{
				booleanField = AndroidJNI.GetBooleanField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return booleanField;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000072BC File Offset: 0x000054BC
		public static int GetIntField(IntPtr obj, IntPtr fieldID)
		{
			int intField;
			try
			{
				intField = AndroidJNI.GetIntField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intField;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000072F0 File Offset: 0x000054F0
		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			try
			{
				AndroidJNI.CallVoidMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007324 File Offset: 0x00005524
		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.CallObjectMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007358 File Offset: 0x00005558
		public static string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			string text;
			try
			{
				text = AndroidJNI.CallStringMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return text;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000738C File Offset: 0x0000558C
		public static char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			char c;
			try
			{
				c = AndroidJNI.CallCharMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return c;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000073C0 File Offset: 0x000055C0
		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			double num;
			try
			{
				num = AndroidJNI.CallDoubleMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000073F4 File Offset: 0x000055F4
		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			float num;
			try
			{
				num = AndroidJNI.CallFloatMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007428 File Offset: 0x00005628
		public static long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			long num;
			try
			{
				num = AndroidJNI.CallLongMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000745C File Offset: 0x0000565C
		public static short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			short num;
			try
			{
				num = AndroidJNI.CallShortMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007490 File Offset: 0x00005690
		public static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			sbyte b;
			try
			{
				b = AndroidJNI.CallSByteMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return b;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000074C4 File Offset: 0x000056C4
		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			bool flag;
			try
			{
				flag = AndroidJNI.CallBooleanMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return flag;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000074F8 File Offset: 0x000056F8
		public static int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			int num;
			try
			{
				num = AndroidJNI.CallIntMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return num;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000752C File Offset: 0x0000572C
		public static IntPtr[] FromObjectArray(IntPtr array)
		{
			IntPtr[] array2;
			try
			{
				array2 = AndroidJNI.FromObjectArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007560 File Offset: 0x00005760
		public static char[] FromCharArray(IntPtr array)
		{
			char[] array2;
			try
			{
				array2 = AndroidJNI.FromCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007594 File Offset: 0x00005794
		public static double[] FromDoubleArray(IntPtr array)
		{
			double[] array2;
			try
			{
				array2 = AndroidJNI.FromDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000075C8 File Offset: 0x000057C8
		public static float[] FromFloatArray(IntPtr array)
		{
			float[] array2;
			try
			{
				array2 = AndroidJNI.FromFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000075FC File Offset: 0x000057FC
		public static long[] FromLongArray(IntPtr array)
		{
			long[] array2;
			try
			{
				array2 = AndroidJNI.FromLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007630 File Offset: 0x00005830
		public static short[] FromShortArray(IntPtr array)
		{
			short[] array2;
			try
			{
				array2 = AndroidJNI.FromShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007664 File Offset: 0x00005864
		public static byte[] FromByteArray(IntPtr array)
		{
			byte[] array2;
			try
			{
				array2 = AndroidJNI.FromByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007698 File Offset: 0x00005898
		public static sbyte[] FromSByteArray(IntPtr array)
		{
			sbyte[] array2;
			try
			{
				array2 = AndroidJNI.FromSByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000076CC File Offset: 0x000058CC
		public static bool[] FromBooleanArray(IntPtr array)
		{
			bool[] array2;
			try
			{
				array2 = AndroidJNI.FromBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007700 File Offset: 0x00005900
		public static int[] FromIntArray(IntPtr array)
		{
			int[] array2;
			try
			{
				array2 = AndroidJNI.FromIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return array2;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007734 File Offset: 0x00005934
		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToObjectArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007768 File Offset: 0x00005968
		public static IntPtr ToObjectArray(IntPtr[] array, IntPtr type)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToObjectArray(array, type);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000779C File Offset: 0x0000599C
		public static IntPtr ToCharArray(char[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000077D0 File Offset: 0x000059D0
		public static IntPtr ToDoubleArray(double[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007804 File Offset: 0x00005A04
		public static IntPtr ToFloatArray(float[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007838 File Offset: 0x00005A38
		public static IntPtr ToLongArray(long[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000786C File Offset: 0x00005A6C
		public static IntPtr ToShortArray(short[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000078A0 File Offset: 0x00005AA0
		public static IntPtr ToByteArray(byte[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000078D4 File Offset: 0x00005AD4
		public static IntPtr ToSByteArray(sbyte[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToSByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007908 File Offset: 0x00005B08
		public static IntPtr ToBooleanArray(bool[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000793C File Offset: 0x00005B3C
		public static IntPtr ToIntArray(int[] array)
		{
			IntPtr intPtr;
			try
			{
				intPtr = AndroidJNI.ToIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intPtr;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007970 File Offset: 0x00005B70
		public static IntPtr GetObjectArrayElement(IntPtr array, int index)
		{
			IntPtr objectArrayElement;
			try
			{
				objectArrayElement = AndroidJNI.GetObjectArrayElement(array, index);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectArrayElement;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000079A4 File Offset: 0x00005BA4
		public static int GetArrayLength(IntPtr array)
		{
			int arrayLength;
			try
			{
				arrayLength = AndroidJNI.GetArrayLength(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return arrayLength;
		}
	}
}
