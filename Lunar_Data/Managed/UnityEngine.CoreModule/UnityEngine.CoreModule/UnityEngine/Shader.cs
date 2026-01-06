using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000149 RID: 329
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/ShaderNameRegistry.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/GpuPrograms/ShaderVariantCollection.h")]
	[NativeHeader("Runtime/Shaders/Shader.h")]
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManager.h")]
	public sealed class Shader : Object
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00010758 File Offset: 0x0000E958
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x0001076F File Offset: 0x0000E96F
		[Obsolete("Use Graphics.activeTier instead (UnityUpgradable) -> UnityEngine.Graphics.activeTier", false)]
		public static ShaderHardwareTier globalShaderHardwareTier
		{
			get
			{
				return (ShaderHardwareTier)Graphics.activeTier;
			}
			set
			{
				Graphics.activeTier = (GraphicsTier)value;
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00010779 File Offset: 0x0000E979
		public static Shader Find(string name)
		{
			return ResourcesAPI.ActiveAPI.FindShaderByName(name);
		}

		// Token: 0x06000C4C RID: 3148
		[FreeFunction("GetBuiltinResource<Shader>")]
		[MethodImpl(4096)]
		internal static extern Shader FindBuiltin(string name);

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000C4D RID: 3149
		// (set) Token: 0x06000C4E RID: 3150
		[NativeProperty("MaxChunksRuntimeOverride")]
		public static extern int maximumChunksOverride
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000C4F RID: 3151
		// (set) Token: 0x06000C50 RID: 3152
		[NativeProperty("MaximumShaderLOD")]
		public extern int maximumLOD
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000C51 RID: 3153
		// (set) Token: 0x06000C52 RID: 3154
		[NativeProperty("GlobalMaximumShaderLOD")]
		public static extern int globalMaximumLOD
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000C53 RID: 3155
		public extern bool isSupported
		{
			[NativeMethod("IsSupported")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000C54 RID: 3156
		// (set) Token: 0x06000C55 RID: 3157
		public static extern string globalRenderPipeline
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00010788 File Offset: 0x0000E988
		public static GlobalKeyword[] enabledGlobalKeywords
		{
			get
			{
				return Shader.GetEnabledGlobalKeywords();
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000107A0 File Offset: 0x0000E9A0
		public static GlobalKeyword[] globalKeywords
		{
			get
			{
				return Shader.GetAllGlobalKeywords();
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x000107B8 File Offset: 0x0000E9B8
		public LocalKeywordSpace keywordSpace
		{
			get
			{
				LocalKeywordSpace localKeywordSpace;
				this.get_keywordSpace_Injected(out localKeywordSpace);
				return localKeywordSpace;
			}
		}

		// Token: 0x06000C59 RID: 3161
		[FreeFunction("keywords::GetEnabledGlobalKeywords")]
		[MethodImpl(4096)]
		internal static extern GlobalKeyword[] GetEnabledGlobalKeywords();

		// Token: 0x06000C5A RID: 3162
		[FreeFunction("keywords::GetAllGlobalKeywords")]
		[MethodImpl(4096)]
		internal static extern GlobalKeyword[] GetAllGlobalKeywords();

		// Token: 0x06000C5B RID: 3163
		[FreeFunction("ShaderScripting::EnableKeyword")]
		[MethodImpl(4096)]
		public static extern void EnableKeyword(string keyword);

		// Token: 0x06000C5C RID: 3164
		[FreeFunction("ShaderScripting::DisableKeyword")]
		[MethodImpl(4096)]
		public static extern void DisableKeyword(string keyword);

		// Token: 0x06000C5D RID: 3165
		[FreeFunction("ShaderScripting::IsKeywordEnabled")]
		[MethodImpl(4096)]
		public static extern bool IsKeywordEnabled(string keyword);

		// Token: 0x06000C5E RID: 3166 RVA: 0x000107CE File Offset: 0x0000E9CE
		[FreeFunction("ShaderScripting::EnableKeyword")]
		internal static void EnableKeywordFast(GlobalKeyword keyword)
		{
			Shader.EnableKeywordFast_Injected(ref keyword);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000107D7 File Offset: 0x0000E9D7
		[FreeFunction("ShaderScripting::DisableKeyword")]
		internal static void DisableKeywordFast(GlobalKeyword keyword)
		{
			Shader.DisableKeywordFast_Injected(ref keyword);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000107E0 File Offset: 0x0000E9E0
		[FreeFunction("ShaderScripting::SetKeyword")]
		internal static void SetKeywordFast(GlobalKeyword keyword, bool value)
		{
			Shader.SetKeywordFast_Injected(ref keyword, value);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000107EA File Offset: 0x0000E9EA
		[FreeFunction("ShaderScripting::IsKeywordEnabled")]
		internal static bool IsKeywordEnabledFast(GlobalKeyword keyword)
		{
			return Shader.IsKeywordEnabledFast_Injected(ref keyword);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000107F3 File Offset: 0x0000E9F3
		public static void EnableKeyword(in GlobalKeyword keyword)
		{
			Shader.EnableKeywordFast(keyword);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00010802 File Offset: 0x0000EA02
		public static void DisableKeyword(in GlobalKeyword keyword)
		{
			Shader.DisableKeywordFast(keyword);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00010811 File Offset: 0x0000EA11
		public static void SetKeyword(in GlobalKeyword keyword, bool value)
		{
			Shader.SetKeywordFast(keyword, value);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00010824 File Offset: 0x0000EA24
		public static bool IsKeywordEnabled(in GlobalKeyword keyword)
		{
			return Shader.IsKeywordEnabledFast(keyword);
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000C66 RID: 3174
		public extern int renderQueue
		{
			[FreeFunction("ShaderScripting::GetRenderQueue", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000C67 RID: 3175
		internal extern DisableBatchingType disableBatching
		{
			[FreeFunction("ShaderScripting::GetDisableBatchingType", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000C68 RID: 3176
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void WarmupAllShaders();

		// Token: 0x06000C69 RID: 3177
		[FreeFunction("ShaderScripting::TagToID")]
		[MethodImpl(4096)]
		internal static extern int TagToID(string name);

		// Token: 0x06000C6A RID: 3178
		[FreeFunction("ShaderScripting::IDToTag")]
		[MethodImpl(4096)]
		internal static extern string IDToTag(int name);

		// Token: 0x06000C6B RID: 3179
		[FreeFunction(Name = "ShaderScripting::PropertyToID", IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern int PropertyToID(string name);

		// Token: 0x06000C6C RID: 3180
		[MethodImpl(4096)]
		public extern Shader GetDependency(string name);

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000C6D RID: 3181
		public extern int passCount
		{
			[FreeFunction(Name = "ShaderScripting::GetPassCount", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000C6E RID: 3182
		public extern int subshaderCount
		{
			[FreeFunction(Name = "ShaderScripting::GetSubshaderCount", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000C6F RID: 3183
		[FreeFunction(Name = "ShaderScripting::GetPassCountInSubshader", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetPassCountInSubshader(int subshaderIndex);

		// Token: 0x06000C70 RID: 3184 RVA: 0x00010844 File Offset: 0x0000EA44
		public ShaderTagId FindPassTagValue(int passIndex, ShaderTagId tagName)
		{
			bool flag = passIndex < 0 || passIndex >= this.passCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("passIndex");
			}
			int num = this.Internal_FindPassTagValue(passIndex, tagName.id);
			return new ShaderTagId
			{
				id = num
			};
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0001089C File Offset: 0x0000EA9C
		public ShaderTagId FindPassTagValue(int subshaderIndex, int passIndex, ShaderTagId tagName)
		{
			bool flag = subshaderIndex < 0 || subshaderIndex >= this.subshaderCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("subshaderIndex");
			}
			bool flag2 = passIndex < 0 || passIndex >= this.GetPassCountInSubshader(subshaderIndex);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("passIndex");
			}
			int num = this.Internal_FindPassTagValueInSubShader(subshaderIndex, passIndex, tagName.id);
			return new ShaderTagId
			{
				id = num
			};
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00010918 File Offset: 0x0000EB18
		public ShaderTagId FindSubshaderTagValue(int subshaderIndex, ShaderTagId tagName)
		{
			bool flag = subshaderIndex < 0 || subshaderIndex >= this.subshaderCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Invalid subshaderIndex {0}. Value must be in the range [0, {1})", subshaderIndex, this.subshaderCount));
			}
			int num = this.Internal_FindSubshaderTagValue(subshaderIndex, tagName.id);
			return new ShaderTagId
			{
				id = num
			};
		}

		// Token: 0x06000C73 RID: 3187
		[FreeFunction(Name = "ShaderScripting::FindPassTagValue", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int Internal_FindPassTagValue(int passIndex, int tagName);

		// Token: 0x06000C74 RID: 3188
		[FreeFunction(Name = "ShaderScripting::FindPassTagValue", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int Internal_FindPassTagValueInSubShader(int subShaderIndex, int passIndex, int tagName);

		// Token: 0x06000C75 RID: 3189
		[FreeFunction(Name = "ShaderScripting::FindSubshaderTagValue", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int Internal_FindSubshaderTagValue(int subShaderIndex, int tagName);

		// Token: 0x06000C76 RID: 3190
		[FreeFunction("ShaderScripting::SetGlobalInt")]
		[MethodImpl(4096)]
		private static extern void SetGlobalIntImpl(int name, int value);

		// Token: 0x06000C77 RID: 3191
		[FreeFunction("ShaderScripting::SetGlobalFloat")]
		[MethodImpl(4096)]
		private static extern void SetGlobalFloatImpl(int name, float value);

		// Token: 0x06000C78 RID: 3192 RVA: 0x00010983 File Offset: 0x0000EB83
		[FreeFunction("ShaderScripting::SetGlobalVector")]
		private static void SetGlobalVectorImpl(int name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl_Injected(name, ref value);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0001098D File Offset: 0x0000EB8D
		[FreeFunction("ShaderScripting::SetGlobalMatrix")]
		private static void SetGlobalMatrixImpl(int name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl_Injected(name, ref value);
		}

		// Token: 0x06000C7A RID: 3194
		[FreeFunction("ShaderScripting::SetGlobalTexture")]
		[MethodImpl(4096)]
		private static extern void SetGlobalTextureImpl(int name, Texture value);

		// Token: 0x06000C7B RID: 3195
		[FreeFunction("ShaderScripting::SetGlobalRenderTexture")]
		[MethodImpl(4096)]
		private static extern void SetGlobalRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element);

		// Token: 0x06000C7C RID: 3196
		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		[MethodImpl(4096)]
		private static extern void SetGlobalBufferImpl(int name, ComputeBuffer value);

		// Token: 0x06000C7D RID: 3197
		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		[MethodImpl(4096)]
		private static extern void SetGlobalGraphicsBufferImpl(int name, GraphicsBuffer value);

		// Token: 0x06000C7E RID: 3198
		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		[MethodImpl(4096)]
		private static extern void SetGlobalConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		// Token: 0x06000C7F RID: 3199
		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		[MethodImpl(4096)]
		private static extern void SetGlobalConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		// Token: 0x06000C80 RID: 3200
		[FreeFunction("ShaderScripting::GetGlobalInt")]
		[MethodImpl(4096)]
		private static extern int GetGlobalIntImpl(int name);

		// Token: 0x06000C81 RID: 3201
		[FreeFunction("ShaderScripting::GetGlobalFloat")]
		[MethodImpl(4096)]
		private static extern float GetGlobalFloatImpl(int name);

		// Token: 0x06000C82 RID: 3202 RVA: 0x00010998 File Offset: 0x0000EB98
		[FreeFunction("ShaderScripting::GetGlobalVector")]
		private static Vector4 GetGlobalVectorImpl(int name)
		{
			Vector4 vector;
			Shader.GetGlobalVectorImpl_Injected(name, out vector);
			return vector;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000109B0 File Offset: 0x0000EBB0
		[FreeFunction("ShaderScripting::GetGlobalMatrix")]
		private static Matrix4x4 GetGlobalMatrixImpl(int name)
		{
			Matrix4x4 matrix4x;
			Shader.GetGlobalMatrixImpl_Injected(name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000C84 RID: 3204
		[FreeFunction("ShaderScripting::GetGlobalTexture")]
		[MethodImpl(4096)]
		private static extern Texture GetGlobalTextureImpl(int name);

		// Token: 0x06000C85 RID: 3205
		[FreeFunction("ShaderScripting::SetGlobalFloatArray")]
		[MethodImpl(4096)]
		private static extern void SetGlobalFloatArrayImpl(int name, float[] values, int count);

		// Token: 0x06000C86 RID: 3206
		[FreeFunction("ShaderScripting::SetGlobalVectorArray")]
		[MethodImpl(4096)]
		private static extern void SetGlobalVectorArrayImpl(int name, Vector4[] values, int count);

		// Token: 0x06000C87 RID: 3207
		[FreeFunction("ShaderScripting::SetGlobalMatrixArray")]
		[MethodImpl(4096)]
		private static extern void SetGlobalMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		// Token: 0x06000C88 RID: 3208
		[FreeFunction("ShaderScripting::GetGlobalFloatArray")]
		[MethodImpl(4096)]
		private static extern float[] GetGlobalFloatArrayImpl(int name);

		// Token: 0x06000C89 RID: 3209
		[FreeFunction("ShaderScripting::GetGlobalVectorArray")]
		[MethodImpl(4096)]
		private static extern Vector4[] GetGlobalVectorArrayImpl(int name);

		// Token: 0x06000C8A RID: 3210
		[FreeFunction("ShaderScripting::GetGlobalMatrixArray")]
		[MethodImpl(4096)]
		private static extern Matrix4x4[] GetGlobalMatrixArrayImpl(int name);

		// Token: 0x06000C8B RID: 3211
		[FreeFunction("ShaderScripting::GetGlobalFloatArrayCount")]
		[MethodImpl(4096)]
		private static extern int GetGlobalFloatArrayCountImpl(int name);

		// Token: 0x06000C8C RID: 3212
		[FreeFunction("ShaderScripting::GetGlobalVectorArrayCount")]
		[MethodImpl(4096)]
		private static extern int GetGlobalVectorArrayCountImpl(int name);

		// Token: 0x06000C8D RID: 3213
		[FreeFunction("ShaderScripting::GetGlobalMatrixArrayCount")]
		[MethodImpl(4096)]
		private static extern int GetGlobalMatrixArrayCountImpl(int name);

		// Token: 0x06000C8E RID: 3214
		[FreeFunction("ShaderScripting::ExtractGlobalFloatArray")]
		[MethodImpl(4096)]
		private static extern void ExtractGlobalFloatArrayImpl(int name, [Out] float[] val);

		// Token: 0x06000C8F RID: 3215
		[FreeFunction("ShaderScripting::ExtractGlobalVectorArray")]
		[MethodImpl(4096)]
		private static extern void ExtractGlobalVectorArrayImpl(int name, [Out] Vector4[] val);

		// Token: 0x06000C90 RID: 3216
		[FreeFunction("ShaderScripting::ExtractGlobalMatrixArray")]
		[MethodImpl(4096)]
		private static extern void ExtractGlobalMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		// Token: 0x06000C91 RID: 3217 RVA: 0x000109C8 File Offset: 0x0000EBC8
		private static void SetGlobalFloatArray(int name, float[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			Shader.SetGlobalFloatArrayImpl(name, values, count);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00010A1C File Offset: 0x0000EC1C
		private static void SetGlobalVectorArray(int name, Vector4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			Shader.SetGlobalVectorArrayImpl(name, values, count);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00010A70 File Offset: 0x0000EC70
		private static void SetGlobalMatrixArray(int name, Matrix4x4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			Shader.SetGlobalMatrixArrayImpl(name, values, count);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		private static void ExtractGlobalFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalFloatArrayCountImpl = Shader.GetGlobalFloatArrayCountImpl(name);
			bool flag2 = globalFloatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, globalFloatArrayCountImpl);
				Shader.ExtractGlobalFloatArrayImpl(name, (float[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00010B18 File Offset: 0x0000ED18
		private static void ExtractGlobalVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalVectorArrayCountImpl = Shader.GetGlobalVectorArrayCountImpl(name);
			bool flag2 = globalVectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, globalVectorArrayCountImpl);
				Shader.ExtractGlobalVectorArrayImpl(name, (Vector4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00010B6C File Offset: 0x0000ED6C
		private static void ExtractGlobalMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalMatrixArrayCountImpl = Shader.GetGlobalMatrixArrayCountImpl(name);
			bool flag2 = globalMatrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, globalMatrixArrayCountImpl);
				Shader.ExtractGlobalMatrixArrayImpl(name, (Matrix4x4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00010BBF File Offset: 0x0000EDBF
		public static void SetGlobalInt(string name, int value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		public static void SetGlobalInt(int nameID, int value)
		{
			Shader.SetGlobalFloatImpl(nameID, (float)value);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00010BDC File Offset: 0x0000EDDC
		public static void SetGlobalFloat(string name, float value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00010BEC File Offset: 0x0000EDEC
		public static void SetGlobalFloat(int nameID, float value)
		{
			Shader.SetGlobalFloatImpl(nameID, value);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00010BF7 File Offset: 0x0000EDF7
		public static void SetGlobalInteger(string name, int value)
		{
			Shader.SetGlobalIntImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00010C07 File Offset: 0x0000EE07
		public static void SetGlobalInteger(int nameID, int value)
		{
			Shader.SetGlobalIntImpl(nameID, value);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00010C12 File Offset: 0x0000EE12
		public static void SetGlobalVector(string name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00010C22 File Offset: 0x0000EE22
		public static void SetGlobalVector(int nameID, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00010C2D File Offset: 0x0000EE2D
		public static void SetGlobalColor(string name, Color value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00010C42 File Offset: 0x0000EE42
		public static void SetGlobalColor(int nameID, Color value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00010C52 File Offset: 0x0000EE52
		public static void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00010C62 File Offset: 0x0000EE62
		public static void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(nameID, value);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00010C6D File Offset: 0x0000EE6D
		public static void SetGlobalTexture(string name, Texture value)
		{
			Shader.SetGlobalTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00010C7D File Offset: 0x0000EE7D
		public static void SetGlobalTexture(int nameID, Texture value)
		{
			Shader.SetGlobalTextureImpl(nameID, value);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00010C88 File Offset: 0x0000EE88
		public static void SetGlobalTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00010C99 File Offset: 0x0000EE99
		public static void SetGlobalTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00010CA5 File Offset: 0x0000EEA5
		public static void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00010CB5 File Offset: 0x0000EEB5
		public static void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(nameID, value);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		public static void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00010CD0 File Offset: 0x0000EED0
		public static void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00010CDB File Offset: 0x0000EEDB
		public static void SetGlobalConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00010CED File Offset: 0x0000EEED
		public static void SetGlobalConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00010CFA File Offset: 0x0000EEFA
		public static void SetGlobalConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00010D0C File Offset: 0x0000EF0C
		public static void SetGlobalConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00010D19 File Offset: 0x0000EF19
		public static void SetGlobalFloatArray(string name, List<float> values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00010D34 File Offset: 0x0000EF34
		public static void SetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.SetGlobalFloatArray(nameID, NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00010D4A File Offset: 0x0000EF4A
		public static void SetGlobalFloatArray(string name, float[] values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00010D5D File Offset: 0x0000EF5D
		public static void SetGlobalFloatArray(int nameID, float[] values)
		{
			Shader.SetGlobalFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00010D6B File Offset: 0x0000EF6B
		public static void SetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00010D86 File Offset: 0x0000EF86
		public static void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00010D9C File Offset: 0x0000EF9C
		public static void SetGlobalVectorArray(string name, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00010DAF File Offset: 0x0000EFAF
		public static void SetGlobalVectorArray(int nameID, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00010DBD File Offset: 0x0000EFBD
		public static void SetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		public static void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00010DEE File Offset: 0x0000EFEE
		public static void SetGlobalMatrixArray(string name, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00010E01 File Offset: 0x0000F001
		public static void SetGlobalMatrixArray(int nameID, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00010E10 File Offset: 0x0000F010
		public static int GetGlobalInt(string name)
		{
			return (int)Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00010E30 File Offset: 0x0000F030
		public static int GetGlobalInt(int nameID)
		{
			return (int)Shader.GetGlobalFloatImpl(nameID);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00010E4C File Offset: 0x0000F04C
		public static float GetGlobalFloat(string name)
		{
			return Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00010E6C File Offset: 0x0000F06C
		public static float GetGlobalFloat(int nameID)
		{
			return Shader.GetGlobalFloatImpl(nameID);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00010E84 File Offset: 0x0000F084
		public static int GetGlobalInteger(string name)
		{
			return Shader.GetGlobalIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		public static int GetGlobalInteger(int nameID)
		{
			return Shader.GetGlobalIntImpl(nameID);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00010EBC File Offset: 0x0000F0BC
		public static Vector4 GetGlobalVector(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00010EDC File Offset: 0x0000F0DC
		public static Vector4 GetGlobalVector(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		public static Color GetGlobalColor(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00010F18 File Offset: 0x0000F118
		public static Color GetGlobalColor(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00010F38 File Offset: 0x0000F138
		public static Matrix4x4 GetGlobalMatrix(string name)
		{
			return Shader.GetGlobalMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00010F58 File Offset: 0x0000F158
		public static Matrix4x4 GetGlobalMatrix(int nameID)
		{
			return Shader.GetGlobalMatrixImpl(nameID);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00010F70 File Offset: 0x0000F170
		public static Texture GetGlobalTexture(string name)
		{
			return Shader.GetGlobalTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00010F90 File Offset: 0x0000F190
		public static Texture GetGlobalTexture(int nameID)
		{
			return Shader.GetGlobalTextureImpl(nameID);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		public static float[] GetGlobalFloatArray(string name)
		{
			return Shader.GetGlobalFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		public static float[] GetGlobalFloatArray(int nameID)
		{
			return (Shader.GetGlobalFloatArrayCountImpl(nameID) != 0) ? Shader.GetGlobalFloatArrayImpl(nameID) : null;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00010FEC File Offset: 0x0000F1EC
		public static Vector4[] GetGlobalVectorArray(string name)
		{
			return Shader.GetGlobalVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0001100C File Offset: 0x0000F20C
		public static Vector4[] GetGlobalVectorArray(int nameID)
		{
			return (Shader.GetGlobalVectorArrayCountImpl(nameID) != 0) ? Shader.GetGlobalVectorArrayImpl(nameID) : null;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00011030 File Offset: 0x0000F230
		public static Matrix4x4[] GetGlobalMatrixArray(string name)
		{
			return Shader.GetGlobalMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00011050 File Offset: 0x0000F250
		public static Matrix4x4[] GetGlobalMatrixArray(int nameID)
		{
			return (Shader.GetGlobalMatrixArrayCountImpl(nameID) != 0) ? Shader.GetGlobalMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00011073 File Offset: 0x0000F273
		public static void GetGlobalFloatArray(string name, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00011083 File Offset: 0x0000F283
		public static void GetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(nameID, values);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0001108E File Offset: 0x0000F28E
		public static void GetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0001109E File Offset: 0x0000F29E
		public static void GetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(nameID, values);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x000110A9 File Offset: 0x0000F2A9
		public static void GetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000110B9 File Offset: 0x0000F2B9
		public static void GetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(nameID, values);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		private Shader()
		{
		}

		// Token: 0x06000CD6 RID: 3286
		[FreeFunction("ShaderScripting::GetPropertyName")]
		[MethodImpl(4096)]
		private static extern string GetPropertyName([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		// Token: 0x06000CD7 RID: 3287
		[FreeFunction("ShaderScripting::GetPropertyNameId")]
		[MethodImpl(4096)]
		private static extern int GetPropertyNameId([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		// Token: 0x06000CD8 RID: 3288
		[FreeFunction("ShaderScripting::GetPropertyType")]
		[MethodImpl(4096)]
		private static extern ShaderPropertyType GetPropertyType([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		// Token: 0x06000CD9 RID: 3289
		[FreeFunction("ShaderScripting::GetPropertyDescription")]
		[MethodImpl(4096)]
		private static extern string GetPropertyDescription([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		// Token: 0x06000CDA RID: 3290
		[FreeFunction("ShaderScripting::GetPropertyFlags")]
		[MethodImpl(4096)]
		private static extern ShaderPropertyFlags GetPropertyFlags([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		// Token: 0x06000CDB RID: 3291
		[FreeFunction("ShaderScripting::GetPropertyAttributes")]
		[MethodImpl(4096)]
		private static extern string[] GetPropertyAttributes([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		// Token: 0x06000CDC RID: 3292
		[FreeFunction("ShaderScripting::GetPropertyDefaultIntValue")]
		[MethodImpl(4096)]
		private static extern int GetPropertyDefaultIntValue([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		// Token: 0x06000CDD RID: 3293 RVA: 0x000110C4 File Offset: 0x0000F2C4
		[FreeFunction("ShaderScripting::GetPropertyDefaultValue")]
		private static Vector4 GetPropertyDefaultValue([NotNull("ArgumentNullException")] Shader shader, int propertyIndex)
		{
			Vector4 vector;
			Shader.GetPropertyDefaultValue_Injected(shader, propertyIndex, out vector);
			return vector;
		}

		// Token: 0x06000CDE RID: 3294
		[FreeFunction("ShaderScripting::GetPropertyTextureDimension")]
		[MethodImpl(4096)]
		private static extern TextureDimension GetPropertyTextureDimension([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		// Token: 0x06000CDF RID: 3295
		[FreeFunction("ShaderScripting::GetPropertyTextureDefaultName")]
		[MethodImpl(4096)]
		private static extern string GetPropertyTextureDefaultName([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		// Token: 0x06000CE0 RID: 3296
		[FreeFunction("ShaderScripting::FindTextureStack")]
		[MethodImpl(4096)]
		private static extern bool FindTextureStackImpl([NotNull("ArgumentNullException")] Shader s, int propertyIdx, out string stackName, out int layerIndex);

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000110DC File Offset: 0x0000F2DC
		private static void CheckPropertyIndex(Shader s, int propertyIndex)
		{
			bool flag = propertyIndex < 0 || propertyIndex >= s.GetPropertyCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("propertyIndex");
			}
		}

		// Token: 0x06000CE2 RID: 3298
		[MethodImpl(4096)]
		public extern int GetPropertyCount();

		// Token: 0x06000CE3 RID: 3299
		[MethodImpl(4096)]
		public extern int FindPropertyIndex(string propertyName);

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0001110C File Offset: 0x0000F30C
		public string GetPropertyName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyName(this, propertyIndex);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00011130 File Offset: 0x0000F330
		public int GetPropertyNameId(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyNameId(this, propertyIndex);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00011154 File Offset: 0x0000F354
		public ShaderPropertyType GetPropertyType(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyType(this, propertyIndex);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00011178 File Offset: 0x0000F378
		public string GetPropertyDescription(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyDescription(this, propertyIndex);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0001119C File Offset: 0x0000F39C
		public ShaderPropertyFlags GetPropertyFlags(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyFlags(this, propertyIndex);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000111C0 File Offset: 0x0000F3C0
		public string[] GetPropertyAttributes(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyAttributes(this, propertyIndex);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000111E4 File Offset: 0x0000F3E4
		public float GetPropertyDefaultFloatValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Float && propertyType != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Float or Range.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex)[0];
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00011234 File Offset: 0x0000F434
		public Vector4 GetPropertyDefaultVectorValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Color && propertyType != ShaderPropertyType.Vector;
			if (flag)
			{
				throw new ArgumentException("Property type is not Color or Vector.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0001127C File Offset: 0x0000F47C
		public Vector2 GetPropertyRangeLimits(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Range.");
			}
			Vector4 propertyDefaultValue = Shader.GetPropertyDefaultValue(this, propertyIndex);
			return new Vector2(propertyDefaultValue[1], propertyDefaultValue[2]);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public TextureDimension GetPropertyTextureDimension(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not TexEnv.");
			}
			return Shader.GetPropertyTextureDimension(this, propertyIndex);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00011310 File Offset: 0x0000F510
		public string GetPropertyTextureDefaultName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.GetPropertyTextureDefaultName(this, propertyIndex);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00011350 File Offset: 0x0000F550
		public bool FindTextureStack(int propertyIndex, out string stackName, out int layerIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.FindTextureStackImpl(this, propertyIndex, out stackName, out layerIndex);
		}

		// Token: 0x06000CF0 RID: 3312
		[MethodImpl(4096)]
		private extern void get_keywordSpace_Injected(out LocalKeywordSpace ret);

		// Token: 0x06000CF1 RID: 3313
		[MethodImpl(4096)]
		private static extern void EnableKeywordFast_Injected(ref GlobalKeyword keyword);

		// Token: 0x06000CF2 RID: 3314
		[MethodImpl(4096)]
		private static extern void DisableKeywordFast_Injected(ref GlobalKeyword keyword);

		// Token: 0x06000CF3 RID: 3315
		[MethodImpl(4096)]
		private static extern void SetKeywordFast_Injected(ref GlobalKeyword keyword, bool value);

		// Token: 0x06000CF4 RID: 3316
		[MethodImpl(4096)]
		private static extern bool IsKeywordEnabledFast_Injected(ref GlobalKeyword keyword);

		// Token: 0x06000CF5 RID: 3317
		[MethodImpl(4096)]
		private static extern void SetGlobalVectorImpl_Injected(int name, ref Vector4 value);

		// Token: 0x06000CF6 RID: 3318
		[MethodImpl(4096)]
		private static extern void SetGlobalMatrixImpl_Injected(int name, ref Matrix4x4 value);

		// Token: 0x06000CF7 RID: 3319
		[MethodImpl(4096)]
		private static extern void GetGlobalVectorImpl_Injected(int name, out Vector4 ret);

		// Token: 0x06000CF8 RID: 3320
		[MethodImpl(4096)]
		private static extern void GetGlobalMatrixImpl_Injected(int name, out Matrix4x4 ret);

		// Token: 0x06000CF9 RID: 3321
		[MethodImpl(4096)]
		private static extern void GetPropertyDefaultValue_Injected(Shader shader, int propertyIndex, out Vector4 ret);
	}
}
