using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Holds a message returned in response to a method call on a remote object.</summary>
	// Token: 0x02000635 RID: 1589
	[ComVisible(true)]
	public class ReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage, IInternalMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class with all the information returning to the caller after the method call.</summary>
		/// <param name="ret">The object returned by the invoked method from which the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> instance originated. </param>
		/// <param name="outArgs">The objects returned from the invoked method as out parameters. </param>
		/// <param name="outArgsCount">The number of out parameters returned from the invoked method. </param>
		/// <param name="callCtx">The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the method call. </param>
		/// <param name="mcm">The original method call to the invoked method. </param>
		// Token: 0x06003BF3 RID: 15347 RVA: 0x000D0A64 File Offset: 0x000CEC64
		public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
		{
			this._returnValue = ret;
			this._args = outArgs;
			this._callCtx = callCtx;
			if (mcm != null)
			{
				this._uri = mcm.Uri;
				this._methodBase = mcm.MethodBase;
			}
			if (this._args == null)
			{
				this._args = new object[outArgsCount];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class.</summary>
		/// <param name="e">The exception that was thrown during execution of the remotely called method. </param>
		/// <param name="mcm">An <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> with which to create an instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class. </param>
		// Token: 0x06003BF4 RID: 15348 RVA: 0x000D0ABF File Offset: 0x000CECBF
		public ReturnMessage(Exception e, IMethodCallMessage mcm)
		{
			this._exception = e;
			if (mcm != null)
			{
				this._methodBase = mcm.MethodBase;
				this._callCtx = mcm.LogicalCallContext;
			}
			this._args = new object[0];
		}

		/// <summary>Gets the number of arguments of the called method.</summary>
		/// <returns>The number of arguments of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06003BF5 RID: 15349 RVA: 0x000D0AF5 File Offset: 0x000CECF5
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this._args.Length;
			}
		}

		/// <summary>Gets a specified argument passed to the method called on the remote object.</summary>
		/// <returns>An argument passed to the method called on the remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000D0AFF File Offset: 0x000CECFF
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._args;
			}
		}

		/// <summary>Gets a value indicating whether the called method accepts a variable number of arguments.</summary>
		/// <returns>true if the called method accepts a variable number of arguments; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000D0B07 File Offset: 0x000CED07
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return !(this._methodBase == null) && (this._methodBase.CallingConvention | CallingConventions.VarArgs) > (CallingConventions)0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x000D0B29 File Offset: 0x000CED29
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				if (this._callCtx == null)
				{
					this._callCtx = new LogicalCallContext();
				}
				return this._callCtx;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x000D0B44 File Offset: 0x000CED44
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._methodBase;
			}
		}

		/// <summary>Gets the name of the called method.</summary>
		/// <returns>The name of the method that the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> originated from.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x000D0B4C File Offset: 0x000CED4C
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._methodBase != null && this._methodName == null)
				{
					this._methodName = this._methodBase.Name;
				}
				return this._methodName;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Type" /> objects containing the method signature.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects containing the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x000D0B7C File Offset: 0x000CED7C
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._methodBase != null && this._methodSignature == null)
				{
					ParameterInfo[] parameters = this._methodBase.GetParameters();
					this._methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this._methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this._methodSignature;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> of properties contained in the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of properties contained in the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003BFC RID: 15356 RVA: 0x000D0BDD File Offset: 0x000CEDDD
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnDictionary(this);
				}
				return this._properties;
			}
		}

		/// <summary>Gets the name of the type on which the remote method was called.</summary>
		/// <returns>The type name of the remote object on which the remote method was called.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000D0BF9 File Offset: 0x000CEDF9
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				if (this._methodBase != null && this._typeName == null)
				{
					this._typeName = this._methodBase.DeclaringType.AssemblyQualifiedName;
				}
				return this._typeName;
			}
		}

		/// <summary>Gets or sets the URI of the remote object on which the remote method was called.</summary>
		/// <returns>The URI of the remote object on which the remote method was called.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06003BFE RID: 15358 RVA: 0x000D0C2D File Offset: 0x000CEE2D
		// (set) Token: 0x06003BFF RID: 15359 RVA: 0x000D0C35 File Offset: 0x000CEE35
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x000D0C3E File Offset: 0x000CEE3E
		// (set) Token: 0x06003C01 RID: 15361 RVA: 0x000D0C46 File Offset: 0x000CEE46
		string IInternalMessage.Uri
		{
			get
			{
				return this.Uri;
			}
			set
			{
				this.Uri = value;
			}
		}

		/// <summary>Returns a specified argument passed to the remote method during the method call.</summary>
		/// <returns>An argument passed to the remote method during the method call.</returns>
		/// <param name="argNum">The zero-based index of the requested argument. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003C02 RID: 15362 RVA: 0x000D0C4F File Offset: 0x000CEE4F
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		/// <summary>Returns the name of a specified method argument.</summary>
		/// <returns>The name of a specified method argument.</returns>
		/// <param name="index">The zero-based index of the requested argument name. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003C03 RID: 15363 RVA: 0x000D0C59 File Offset: 0x000CEE59
		[SecurityCritical]
		public string GetArgName(int index)
		{
			return this._methodBase.GetParameters()[index].Name;
		}

		/// <summary>Gets the exception that was thrown during the remote method call.</summary>
		/// <returns>The exception thrown during the method call, or null if an exception did not occur during the call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06003C04 RID: 15364 RVA: 0x000D0C6D File Offset: 0x000CEE6D
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets the number of out or ref arguments on the called method.</summary>
		/// <returns>The number of out or ref arguments on the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06003C05 RID: 15365 RVA: 0x000D0C75 File Offset: 0x000CEE75
		public int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._args == null || this._args.Length == 0)
				{
					return 0;
				}
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
				}
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets a specified object passed as an out or ref parameter to the called method.</summary>
		/// <returns>An object passed as an out or ref parameter to the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003C06 RID: 15366 RVA: 0x000D0CB0 File Offset: 0x000CEEB0
		public object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._outArgs == null && this._args != null)
				{
					if (this._inArgInfo == null)
					{
						this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
					}
					this._outArgs = this._inArgInfo.GetInOutArgs(this._args);
				}
				return this._outArgs;
			}
		}

		/// <summary>Gets the object returned by the called method.</summary>
		/// <returns>The object returned by the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06003C07 RID: 15367 RVA: 0x000D0D04 File Offset: 0x000CEF04
		public virtual object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._returnValue;
			}
		}

		/// <summary>Returns the object passed as an out or ref parameter during the remote method call.</summary>
		/// <returns>The object passed as an out or ref parameter during the remote method call.</returns>
		/// <param name="argNum">The zero-based index of the requested out or ref parameter. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003C08 RID: 15368 RVA: 0x000D0D0C File Offset: 0x000CEF0C
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Returns the name of a specified out or ref parameter passed to the remote method.</summary>
		/// <returns>A string representing the name of the specified out or ref parameter, or null if the current method is not implemented.</returns>
		/// <param name="index">The zero-based index of the requested argument. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003C09 RID: 15369 RVA: 0x000D0D3B File Offset: 0x000CEF3B
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06003C0A RID: 15370 RVA: 0x000D0D63 File Offset: 0x000CEF63
		// (set) Token: 0x06003C0B RID: 15371 RVA: 0x000D0D6B File Offset: 0x000CEF6B
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this._targetIdentity;
			}
			set
			{
				this._targetIdentity = value;
			}
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x000D0D74 File Offset: 0x000CEF74
		bool IInternalMessage.HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x000D0D74 File Offset: 0x000CEF74
		internal bool HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x040026DC RID: 9948
		private object[] _outArgs;

		// Token: 0x040026DD RID: 9949
		private object[] _args;

		// Token: 0x040026DE RID: 9950
		private LogicalCallContext _callCtx;

		// Token: 0x040026DF RID: 9951
		private object _returnValue;

		// Token: 0x040026E0 RID: 9952
		private string _uri;

		// Token: 0x040026E1 RID: 9953
		private Exception _exception;

		// Token: 0x040026E2 RID: 9954
		private MethodBase _methodBase;

		// Token: 0x040026E3 RID: 9955
		private string _methodName;

		// Token: 0x040026E4 RID: 9956
		private Type[] _methodSignature;

		// Token: 0x040026E5 RID: 9957
		private string _typeName;

		// Token: 0x040026E6 RID: 9958
		private MethodReturnDictionary _properties;

		// Token: 0x040026E7 RID: 9959
		private Identity _targetIdentity;

		// Token: 0x040026E8 RID: 9960
		private ArgInfo _inArgInfo;
	}
}
