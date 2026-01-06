using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000482 RID: 1154
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/RayTracingShader.h")]
	[NativeHeader("Runtime/Shaders/RayTracingAccelerationStructure.h")]
	public sealed class RayTracingShader : Object
	{
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002891 RID: 10385
		public extern float maxRecursionDepth
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06002892 RID: 10386
		[FreeFunction(Name = "RayTracingShaderScripting::SetFloat", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetFloat(int nameID, float val);

		// Token: 0x06002893 RID: 10387
		[FreeFunction(Name = "RayTracingShaderScripting::SetInt", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetInt(int nameID, int val);

		// Token: 0x06002894 RID: 10388 RVA: 0x0004305D File Offset: 0x0004125D
		[FreeFunction(Name = "RayTracingShaderScripting::SetVector", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			this.SetVector_Injected(nameID, ref val);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x00043068 File Offset: 0x00041268
		[FreeFunction(Name = "RayTracingShaderScripting::SetMatrix", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			this.SetMatrix_Injected(nameID, ref val);
		}

		// Token: 0x06002896 RID: 10390
		[FreeFunction(Name = "RayTracingShaderScripting::SetFloatArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetFloatArray(int nameID, float[] values);

		// Token: 0x06002897 RID: 10391
		[FreeFunction(Name = "RayTracingShaderScripting::SetIntArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetIntArray(int nameID, int[] values);

		// Token: 0x06002898 RID: 10392
		[FreeFunction(Name = "RayTracingShaderScripting::SetVectorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetVectorArray(int nameID, Vector4[] values);

		// Token: 0x06002899 RID: 10393
		[FreeFunction(Name = "RayTracingShaderScripting::SetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetMatrixArray(int nameID, Matrix4x4[] values);

		// Token: 0x0600289A RID: 10394
		[NativeMethod(Name = "RayTracingShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		public extern void SetTexture(int nameID, [NotNull("ArgumentNullException")] Texture texture);

		// Token: 0x0600289B RID: 10395
		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		public extern void SetBuffer(int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer);

		// Token: 0x0600289C RID: 10396
		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		private extern void SetGraphicsBuffer(int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer);

		// Token: 0x0600289D RID: 10397
		[FreeFunction(Name = "RayTracingShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetConstantComputeBuffer(int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer, int offset, int size);

		// Token: 0x0600289E RID: 10398
		[FreeFunction(Name = "RayTracingShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetConstantGraphicsBuffer(int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer, int offset, int size);

		// Token: 0x0600289F RID: 10399
		[NativeMethod(Name = "RayTracingShaderScripting::SetAccelerationStructure", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		public extern void SetAccelerationStructure(int nameID, [NotNull("ArgumentNullException")] RayTracingAccelerationStructure accelerationStructure);

		// Token: 0x060028A0 RID: 10400
		[MethodImpl(4096)]
		public extern void SetShaderPass(string passName);

		// Token: 0x060028A1 RID: 10401
		[NativeMethod(Name = "RayTracingShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		public extern void SetTextureFromGlobal(int nameID, int globalTextureNameID);

		// Token: 0x060028A2 RID: 10402
		[NativeName("DispatchRays")]
		[MethodImpl(4096)]
		public extern void Dispatch(string rayGenFunctionName, int width, int height, int depth, Camera camera = null);

		// Token: 0x060028A3 RID: 10403 RVA: 0x00043073 File Offset: 0x00041273
		public void SetBuffer(int nameID, GraphicsBuffer buffer)
		{
			this.SetGraphicsBuffer(nameID, buffer);
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		private RayTracingShader()
		{
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x0004307F File Offset: 0x0004127F
		public void SetFloat(string name, float val)
		{
			this.SetFloat(Shader.PropertyToID(name), val);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x00043090 File Offset: 0x00041290
		public void SetInt(string name, int val)
		{
			this.SetInt(Shader.PropertyToID(name), val);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x000430A1 File Offset: 0x000412A1
		public void SetVector(string name, Vector4 val)
		{
			this.SetVector(Shader.PropertyToID(name), val);
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000430B2 File Offset: 0x000412B2
		public void SetMatrix(string name, Matrix4x4 val)
		{
			this.SetMatrix(Shader.PropertyToID(name), val);
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000430C3 File Offset: 0x000412C3
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000430D4 File Offset: 0x000412D4
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000430E5 File Offset: 0x000412E5
		public void SetFloats(string name, params float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000430F6 File Offset: 0x000412F6
		public void SetFloats(int nameID, params float[] values)
		{
			this.SetFloatArray(nameID, values);
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x00043102 File Offset: 0x00041302
		public void SetInts(string name, params int[] values)
		{
			this.SetIntArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00043113 File Offset: 0x00041313
		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x0004311F File Offset: 0x0004131F
		public void SetBool(string name, bool val)
		{
			this.SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x00043136 File Offset: 0x00041336
		public void SetBool(int nameID, bool val)
		{
			this.SetInt(nameID, val ? 1 : 0);
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x00043148 File Offset: 0x00041348
		public void SetTexture(string name, Texture texture)
		{
			this.SetTexture(Shader.PropertyToID(name), texture);
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x00043159 File Offset: 0x00041359
		public void SetBuffer(string name, ComputeBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(name), buffer);
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x0004316A File Offset: 0x0004136A
		public void SetBuffer(string name, GraphicsBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(name), buffer);
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x0004317B File Offset: 0x0004137B
		public void SetConstantBuffer(int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x0004318A File Offset: 0x0004138A
		public void SetConstantBuffer(string name, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x0004319E File Offset: 0x0004139E
		public void SetConstantBuffer(int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000431AD File Offset: 0x000413AD
		public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000431C1 File Offset: 0x000413C1
		public void SetAccelerationStructure(string name, RayTracingAccelerationStructure accelerationStructure)
		{
			this.SetAccelerationStructure(Shader.PropertyToID(name), accelerationStructure);
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000431D2 File Offset: 0x000413D2
		public void SetTextureFromGlobal(string name, string globalTextureName)
		{
			this.SetTextureFromGlobal(Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		// Token: 0x060028BA RID: 10426
		[MethodImpl(4096)]
		private extern void SetVector_Injected(int nameID, ref Vector4 val);

		// Token: 0x060028BB RID: 10427
		[MethodImpl(4096)]
		private extern void SetMatrix_Injected(int nameID, ref Matrix4x4 val);
	}
}
