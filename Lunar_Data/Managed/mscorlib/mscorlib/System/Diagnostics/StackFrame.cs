using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides information about a <see cref="T:System.Diagnostics.StackFrame" />, which represents a function call on the call stack for the current thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020009C1 RID: 2497
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with MS.NET")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class StackFrame
	{
		// Token: 0x060059D3 RID: 22995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_frame_info(int skip, bool needFileInfo, out MethodBase method, out int iloffset, out int native_offset, out string file, out int line, out int column);

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class.</summary>
		// Token: 0x060059D4 RID: 22996 RVA: 0x001332AC File Offset: 0x001314AC
		public StackFrame()
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number of the stack frame; otherwise, false. </param>
		// Token: 0x060059D5 RID: 22997 RVA: 0x001332FC File Offset: 0x001314FC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip. </param>
		// Token: 0x060059D6 RID: 22998 RVA: 0x0013334C File Offset: 0x0013154C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame, optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number of the stack frame; otherwise, false. </param>
		// Token: 0x060059D7 RID: 22999 RVA: 0x0013339C File Offset: 0x0013159C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames, bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name and line number.</summary>
		/// <param name="fileName">The file name. </param>
		/// <param name="lineNumber">The line number in the specified file. </param>
		// Token: 0x060059D8 RID: 23000 RVA: 0x001333EC File Offset: 0x001315EC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(string fileName, int lineNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name, line number, and column number.</summary>
		/// <param name="fileName">The file name. </param>
		/// <param name="lineNumber">The line number in the specified file. </param>
		/// <param name="colNumber">The column number in the specified file. </param>
		// Token: 0x060059D9 RID: 23001 RVA: 0x00133448 File Offset: 0x00131648
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(string fileName, int lineNumber, int colNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = colNumber;
		}

		/// <summary>Gets the line number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file line number, or 0 (zero) if the file line number cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060059DA RID: 23002 RVA: 0x001334A2 File Offset: 0x001316A2
		public virtual int GetFileLineNumber()
		{
			return this.lineNumber;
		}

		/// <summary>Gets the column number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file column number, or 0 (zero) if the file column number cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060059DB RID: 23003 RVA: 0x001334AA File Offset: 0x001316AA
		public virtual int GetFileColumnNumber()
		{
			return this.columnNumber;
		}

		/// <summary>Gets the file name that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file name, or null if the file name cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060059DC RID: 23004 RVA: 0x001334B2 File Offset: 0x001316B2
		public virtual string GetFileName()
		{
			return this.fileName;
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x001334BC File Offset: 0x001316BC
		internal string GetSecureFileName()
		{
			string text = "<filename unknown>";
			if (this.fileName == null)
			{
				return text;
			}
			try
			{
				text = this.GetFileName();
			}
			catch (SecurityException)
			{
			}
			return text;
		}

		/// <summary>Gets the offset from the start of the Microsoft intermediate language (MSIL) code for the method that is executing. This offset might be an approximation depending on whether or not the just-in-time (JIT) compiler is generating debugging code. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" />.</summary>
		/// <returns>The offset from the start of the MSIL code for the method that is executing.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060059DE RID: 23006 RVA: 0x001334F8 File Offset: 0x001316F8
		public virtual int GetILOffset()
		{
			return this.ilOffset;
		}

		/// <summary>Gets the method in which the frame is executing.</summary>
		/// <returns>The method in which the frame is executing.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060059DF RID: 23007 RVA: 0x00133500 File Offset: 0x00131700
		public virtual MethodBase GetMethod()
		{
			return this.methodBase;
		}

		/// <summary>Gets the offset from the start of the native just-in-time (JIT)-compiled code for the method that is being executed. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class.</summary>
		/// <returns>The offset from the start of the JIT-compiled code for the method that is being executed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060059E0 RID: 23008 RVA: 0x00133508 File Offset: 0x00131708
		public virtual int GetNativeOffset()
		{
			return this.nativeOffset;
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x00133510 File Offset: 0x00131710
		internal long GetMethodAddress()
		{
			return this.methodAddress;
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x00133518 File Offset: 0x00131718
		internal uint GetMethodIndex()
		{
			return this.methodIndex;
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x00133520 File Offset: 0x00131720
		internal string GetInternalMethodName()
		{
			return this.internalMethodName;
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060059E4 RID: 23012 RVA: 0x00133528 File Offset: 0x00131728
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.methodBase == null)
			{
				stringBuilder.Append(Locale.GetText("<unknown method>"));
			}
			else
			{
				stringBuilder.Append(this.methodBase.Name);
			}
			stringBuilder.Append(Locale.GetText(" at "));
			if (this.ilOffset == -1)
			{
				stringBuilder.Append(Locale.GetText("<unknown offset>"));
			}
			else
			{
				stringBuilder.Append(Locale.GetText("offset "));
				stringBuilder.Append(this.ilOffset);
			}
			stringBuilder.Append(Locale.GetText(" in file:line:column "));
			stringBuilder.Append(this.GetSecureFileName());
			stringBuilder.AppendFormat(":{0}:{1}", this.lineNumber, this.columnNumber);
			return stringBuilder.ToString();
		}

		/// <summary>Defines the value that is returned from the <see cref="M:System.Diagnostics.StackFrame.GetNativeOffset" /> or <see cref="M:System.Diagnostics.StackFrame.GetILOffset" /> method when the native or Microsoft intermediate language (MSIL) offset is unknown. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400378E RID: 14222
		public const int OFFSET_UNKNOWN = -1;

		// Token: 0x0400378F RID: 14223
		private int ilOffset = -1;

		// Token: 0x04003790 RID: 14224
		private int nativeOffset = -1;

		// Token: 0x04003791 RID: 14225
		private long methodAddress;

		// Token: 0x04003792 RID: 14226
		private uint methodIndex;

		// Token: 0x04003793 RID: 14227
		private MethodBase methodBase;

		// Token: 0x04003794 RID: 14228
		private string fileName;

		// Token: 0x04003795 RID: 14229
		private int lineNumber;

		// Token: 0x04003796 RID: 14230
		private int columnNumber;

		// Token: 0x04003797 RID: 14231
		private string internalMethodName;
	}
}
