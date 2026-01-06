using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000237 RID: 567
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public sealed class ComputeShader : Object
	{
		// Token: 0x0600181E RID: 6174
		[NativeMethod(Name = "ComputeShaderScripting::FindKernel", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[RequiredByNativeCode]
		[MethodImpl(4096)]
		public extern int FindKernel(string name);

		// Token: 0x0600181F RID: 6175
		[FreeFunction(Name = "ComputeShaderScripting::HasKernel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasKernel(string name);

		// Token: 0x06001820 RID: 6176
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<float>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetFloat(int nameID, float val);

		// Token: 0x06001821 RID: 6177
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<int>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetInt(int nameID, int val);

		// Token: 0x06001822 RID: 6178 RVA: 0x0002742B File Offset: 0x0002562B
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Vector4f>", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			this.SetVector_Injected(nameID, ref val);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00027436 File Offset: 0x00025636
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			this.SetMatrix_Injected(nameID, ref val);
		}

		// Token: 0x06001824 RID: 6180
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<float>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetFloatArray(int nameID, float[] values);

		// Token: 0x06001825 RID: 6181
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<int>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetIntArray(int nameID, int[] values);

		// Token: 0x06001826 RID: 6182
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Vector4f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetVectorArray(int nameID, Vector4[] values);

		// Token: 0x06001827 RID: 6183
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Matrix4x4f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetMatrixArray(int nameID, Matrix4x4[] values);

		// Token: 0x06001828 RID: 6184
		[NativeMethod(Name = "ComputeShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetTexture(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] Texture texture, int mipLevel);

		// Token: 0x06001829 RID: 6185
		[NativeMethod(Name = "ComputeShaderScripting::SetRenderTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetRenderTexture(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] RenderTexture texture, int mipLevel, RenderTextureSubElement element);

		// Token: 0x0600182A RID: 6186
		[NativeMethod(Name = "ComputeShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetTextureFromGlobal(int kernelIndex, int nameID, int globalTextureNameID);

		// Token: 0x0600182B RID: 6187
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetBuffer(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer);

		// Token: 0x0600182C RID: 6188
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetGraphicsBuffer(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer);

		// Token: 0x0600182D RID: 6189 RVA: 0x00027441 File Offset: 0x00025641
		public void SetBuffer(int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0002744E File Offset: 0x0002564E
		public void SetBuffer(int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetGraphicsBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x0600182F RID: 6191
		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetConstantComputeBuffer(int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer, int offset, int size);

		// Token: 0x06001830 RID: 6192
		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetConstantGraphicsBuffer(int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer, int offset, int size);

		// Token: 0x06001831 RID: 6193
		[NativeMethod(Name = "ComputeShaderScripting::GetKernelThreadGroupSizes", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void GetKernelThreadGroupSizes(int kernelIndex, out uint x, out uint y, out uint z);

		// Token: 0x06001832 RID: 6194
		[NativeName("DispatchComputeShader")]
		[MethodImpl(4096)]
		public extern void Dispatch(int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		// Token: 0x06001833 RID: 6195
		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_DispatchIndirect(int kernelIndex, [NotNull("ArgumentNullException")] ComputeBuffer argsBuffer, uint argsOffset);

		// Token: 0x06001834 RID: 6196
		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_DispatchIndirectGraphicsBuffer(int kernelIndex, [NotNull("ArgumentNullException")] GraphicsBuffer argsBuffer, uint argsOffset);

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0002745C File Offset: 0x0002565C
		public LocalKeywordSpace keywordSpace
		{
			get
			{
				LocalKeywordSpace localKeywordSpace;
				this.get_keywordSpace_Injected(out localKeywordSpace);
				return localKeywordSpace;
			}
		}

		// Token: 0x06001836 RID: 6198
		[FreeFunction("ComputeShaderScripting::EnableKeyword", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void EnableKeyword(string keyword);

		// Token: 0x06001837 RID: 6199
		[FreeFunction("ComputeShaderScripting::DisableKeyword", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void DisableKeyword(string keyword);

		// Token: 0x06001838 RID: 6200
		[FreeFunction("ComputeShaderScripting::IsKeywordEnabled", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsKeywordEnabled(string keyword);

		// Token: 0x06001839 RID: 6201 RVA: 0x00027472 File Offset: 0x00025672
		[FreeFunction("ComputeShaderScripting::EnableKeyword", HasExplicitThis = true)]
		private void EnableLocalKeyword(LocalKeyword keyword)
		{
			this.EnableLocalKeyword_Injected(ref keyword);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0002747C File Offset: 0x0002567C
		[FreeFunction("ComputeShaderScripting::DisableKeyword", HasExplicitThis = true)]
		private void DisableLocalKeyword(LocalKeyword keyword)
		{
			this.DisableLocalKeyword_Injected(ref keyword);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00027486 File Offset: 0x00025686
		[FreeFunction("ComputeShaderScripting::SetKeyword", HasExplicitThis = true)]
		private void SetLocalKeyword(LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword_Injected(ref keyword, value);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00027491 File Offset: 0x00025691
		[FreeFunction("ComputeShaderScripting::IsKeywordEnabled", HasExplicitThis = true)]
		private bool IsLocalKeywordEnabled(LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled_Injected(ref keyword);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0002749B File Offset: 0x0002569B
		public void EnableKeyword(in LocalKeyword keyword)
		{
			this.EnableLocalKeyword(keyword);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000274AB File Offset: 0x000256AB
		public void DisableKeyword(in LocalKeyword keyword)
		{
			this.DisableLocalKeyword(keyword);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000274BB File Offset: 0x000256BB
		public void SetKeyword(in LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword(keyword, value);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x000274CC File Offset: 0x000256CC
		public bool IsKeywordEnabled(in LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled(keyword);
		}

		// Token: 0x06001841 RID: 6209
		[FreeFunction("ComputeShaderScripting::IsSupported", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsSupported(int kernelIndex);

		// Token: 0x06001842 RID: 6210
		[FreeFunction("ComputeShaderScripting::GetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern string[] GetShaderKeywords();

		// Token: 0x06001843 RID: 6211
		[FreeFunction("ComputeShaderScripting::SetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetShaderKeywords(string[] names);

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x000274EC File Offset: 0x000256EC
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x00027504 File Offset: 0x00025704
		public string[] shaderKeywords
		{
			get
			{
				return this.GetShaderKeywords();
			}
			set
			{
				this.SetShaderKeywords(value);
			}
		}

		// Token: 0x06001846 RID: 6214
		[FreeFunction("ComputeShaderScripting::GetEnabledKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern LocalKeyword[] GetEnabledKeywords();

		// Token: 0x06001847 RID: 6215
		[FreeFunction("ComputeShaderScripting::SetEnabledKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetEnabledKeywords(LocalKeyword[] keywords);

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00027510 File Offset: 0x00025710
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x00027528 File Offset: 0x00025728
		public LocalKeyword[] enabledKeywords
		{
			get
			{
				return this.GetEnabledKeywords();
			}
			set
			{
				this.SetEnabledKeywords(value);
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		private ComputeShader()
		{
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00027533 File Offset: 0x00025733
		public void SetFloat(string name, float val)
		{
			this.SetFloat(Shader.PropertyToID(name), val);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00027544 File Offset: 0x00025744
		public void SetInt(string name, int val)
		{
			this.SetInt(Shader.PropertyToID(name), val);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00027555 File Offset: 0x00025755
		public void SetVector(string name, Vector4 val)
		{
			this.SetVector(Shader.PropertyToID(name), val);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00027566 File Offset: 0x00025766
		public void SetMatrix(string name, Matrix4x4 val)
		{
			this.SetMatrix(Shader.PropertyToID(name), val);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00027577 File Offset: 0x00025777
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00027588 File Offset: 0x00025788
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00027599 File Offset: 0x00025799
		public void SetFloats(string name, params float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x000275AA File Offset: 0x000257AA
		public void SetFloats(int nameID, params float[] values)
		{
			this.SetFloatArray(nameID, values);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x000275B6 File Offset: 0x000257B6
		public void SetInts(string name, params int[] values)
		{
			this.SetIntArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x000275C7 File Offset: 0x000257C7
		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000275D3 File Offset: 0x000257D3
		public void SetBool(string name, bool val)
		{
			this.SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000275EA File Offset: 0x000257EA
		public void SetBool(int nameID, bool val)
		{
			this.SetInt(nameID, val ? 1 : 0);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000275FC File Offset: 0x000257FC
		public void SetTexture(int kernelIndex, int nameID, Texture texture)
		{
			this.SetTexture(kernelIndex, nameID, texture, 0);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0002760A File Offset: 0x0002580A
		public void SetTexture(int kernelIndex, string name, Texture texture)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, 0);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0002761D File Offset: 0x0002581D
		public void SetTexture(int kernelIndex, string name, Texture texture, int mipLevel)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00027631 File Offset: 0x00025831
		public void SetTexture(int kernelIndex, int nameID, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, nameID, texture, mipLevel, element);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00027642 File Offset: 0x00025842
		public void SetTexture(int kernelIndex, string name, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel, element);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00027658 File Offset: 0x00025858
		public void SetTextureFromGlobal(int kernelIndex, string name, string globalTextureName)
		{
			this.SetTextureFromGlobal(kernelIndex, Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0002766F File Offset: 0x0002586F
		public void SetBuffer(int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00027681 File Offset: 0x00025881
		public void SetBuffer(int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00027693 File Offset: 0x00025893
		public void SetConstantBuffer(int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x000276A2 File Offset: 0x000258A2
		public void SetConstantBuffer(string name, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000276B6 File Offset: 0x000258B6
		public void SetConstantBuffer(int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000276C5 File Offset: 0x000258C5
		public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000276DC File Offset: 0x000258DC
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			this.Internal_DispatchIndirect(kernelIndex, argsBuffer, argsOffset);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00027726 File Offset: 0x00025926
		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0U);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00027734 File Offset: 0x00025934
		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			this.Internal_DispatchIndirectGraphicsBuffer(kernelIndex, argsBuffer, argsOffset);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0002777E File Offset: 0x0002597E
		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0U);
		}

		// Token: 0x06001867 RID: 6247
		[MethodImpl(4096)]
		private extern void SetVector_Injected(int nameID, ref Vector4 val);

		// Token: 0x06001868 RID: 6248
		[MethodImpl(4096)]
		private extern void SetMatrix_Injected(int nameID, ref Matrix4x4 val);

		// Token: 0x06001869 RID: 6249
		[MethodImpl(4096)]
		private extern void get_keywordSpace_Injected(out LocalKeywordSpace ret);

		// Token: 0x0600186A RID: 6250
		[MethodImpl(4096)]
		private extern void EnableLocalKeyword_Injected(ref LocalKeyword keyword);

		// Token: 0x0600186B RID: 6251
		[MethodImpl(4096)]
		private extern void DisableLocalKeyword_Injected(ref LocalKeyword keyword);

		// Token: 0x0600186C RID: 6252
		[MethodImpl(4096)]
		private extern void SetLocalKeyword_Injected(ref LocalKeyword keyword, bool value);

		// Token: 0x0600186D RID: 6253
		[MethodImpl(4096)]
		private extern bool IsLocalKeywordEnabled_Injected(ref LocalKeyword keyword);
	}
}
