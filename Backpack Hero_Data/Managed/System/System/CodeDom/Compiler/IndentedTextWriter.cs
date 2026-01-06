using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides a text writer that can indent new lines by a tab string token.</summary>
	// Token: 0x02000355 RID: 853
	public class IndentedTextWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.IndentedTextWriter" /> class using the specified text writer and default tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to use for output. </param>
		// Token: 0x06001C1E RID: 7198 RVA: 0x000668B8 File Offset: 0x00064AB8
		public IndentedTextWriter(TextWriter writer)
			: this(writer, "    ")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.IndentedTextWriter" /> class using the specified text writer and tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to use for output. </param>
		/// <param name="tabString">The tab string to use for indentation. </param>
		// Token: 0x06001C1F RID: 7199 RVA: 0x000668C6 File Offset: 0x00064AC6
		public IndentedTextWriter(TextWriter writer, string tabString)
			: base(CultureInfo.InvariantCulture)
		{
			this._writer = writer;
			this._tabString = tabString;
			this._indentLevel = 0;
			this._tabsPending = false;
		}

		/// <summary>Gets the encoding for the text writer to use.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that indicates the encoding for the text writer to use.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x000668EF File Offset: 0x00064AEF
		public override Encoding Encoding
		{
			get
			{
				return this._writer.Encoding;
			}
		}

		/// <summary>Gets or sets the new line character to use.</summary>
		/// <returns>The new line character to use.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000668FC File Offset: 0x00064AFC
		// (set) Token: 0x06001C22 RID: 7202 RVA: 0x00066909 File Offset: 0x00064B09
		public override string NewLine
		{
			get
			{
				return this._writer.NewLine;
			}
			set
			{
				this._writer.NewLine = value;
			}
		}

		/// <summary>Gets or sets the number of spaces to indent.</summary>
		/// <returns>The number of spaces to indent.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00066917 File Offset: 0x00064B17
		// (set) Token: 0x06001C24 RID: 7204 RVA: 0x0006691F File Offset: 0x00064B1F
		public int Indent
		{
			get
			{
				return this._indentLevel;
			}
			set
			{
				this._indentLevel = Math.Max(value, 0);
			}
		}

		/// <summary>Gets the <see cref="T:System.IO.TextWriter" /> to use.</summary>
		/// <returns>The <see cref="T:System.IO.TextWriter" /> to use.</returns>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0006692E File Offset: 0x00064B2E
		public TextWriter InnerWriter
		{
			get
			{
				return this._writer;
			}
		}

		/// <summary>Closes the document being written to.</summary>
		// Token: 0x06001C26 RID: 7206 RVA: 0x00066936 File Offset: 0x00064B36
		public override void Close()
		{
			this._writer.Close();
		}

		/// <summary>Flushes the stream.</summary>
		// Token: 0x06001C27 RID: 7207 RVA: 0x00066943 File Offset: 0x00064B43
		public override void Flush()
		{
			this._writer.Flush();
		}

		/// <summary>Outputs the tab string once for each level of indentation according to the <see cref="P:System.CodeDom.Compiler.IndentedTextWriter.Indent" /> property.</summary>
		// Token: 0x06001C28 RID: 7208 RVA: 0x00066950 File Offset: 0x00064B50
		protected virtual void OutputTabs()
		{
			if (this._tabsPending)
			{
				for (int i = 0; i < this._indentLevel; i++)
				{
					this._writer.Write(this._tabString);
				}
				this._tabsPending = false;
			}
		}

		/// <summary>Writes the specified string to the text stream.</summary>
		/// <param name="s">The string to write. </param>
		// Token: 0x06001C29 RID: 7209 RVA: 0x0006698E File Offset: 0x00064B8E
		public override void Write(string s)
		{
			this.OutputTabs();
			this._writer.Write(s);
		}

		/// <summary>Writes the text representation of a Boolean value to the text stream.</summary>
		/// <param name="value">The Boolean value to write. </param>
		// Token: 0x06001C2A RID: 7210 RVA: 0x000669A2 File Offset: 0x00064BA2
		public override void Write(bool value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes a character to the text stream.</summary>
		/// <param name="value">The character to write. </param>
		// Token: 0x06001C2B RID: 7211 RVA: 0x000669B6 File Offset: 0x00064BB6
		public override void Write(char value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes a character array to the text stream.</summary>
		/// <param name="buffer">The character array to write. </param>
		// Token: 0x06001C2C RID: 7212 RVA: 0x000669CA File Offset: 0x00064BCA
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this._writer.Write(buffer);
		}

		/// <summary>Writes a subarray of characters to the text stream.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">Starting index in the buffer. </param>
		/// <param name="count">The number of characters to write. </param>
		// Token: 0x06001C2D RID: 7213 RVA: 0x000669DE File Offset: 0x00064BDE
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.Write(buffer, index, count);
		}

		/// <summary>Writes the text representation of a Double to the text stream.</summary>
		/// <param name="value">The double to write. </param>
		// Token: 0x06001C2E RID: 7214 RVA: 0x000669F4 File Offset: 0x00064BF4
		public override void Write(double value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of a Single to the text stream.</summary>
		/// <param name="value">The single to write. </param>
		// Token: 0x06001C2F RID: 7215 RVA: 0x00066A08 File Offset: 0x00064C08
		public override void Write(float value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of an integer to the text stream.</summary>
		/// <param name="value">The integer to write. </param>
		// Token: 0x06001C30 RID: 7216 RVA: 0x00066A1C File Offset: 0x00064C1C
		public override void Write(int value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of an 8-byte integer to the text stream.</summary>
		/// <param name="value">The 8-byte integer to write. </param>
		// Token: 0x06001C31 RID: 7217 RVA: 0x00066A30 File Offset: 0x00064C30
		public override void Write(long value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of an object to the text stream.</summary>
		/// <param name="value">The object to write. </param>
		// Token: 0x06001C32 RID: 7218 RVA: 0x00066A44 File Offset: 0x00064C44
		public override void Write(object value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">The object to write into the formatted string. </param>
		// Token: 0x06001C33 RID: 7219 RVA: 0x00066A58 File Offset: 0x00064C58
		public override void Write(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg0">The first object to write into the formatted string. </param>
		/// <param name="arg1">The second object to write into the formatted string. </param>
		// Token: 0x06001C34 RID: 7220 RVA: 0x00066A6D File Offset: 0x00064C6D
		public override void Write(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0, arg1);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg">The argument array to output. </param>
		// Token: 0x06001C35 RID: 7221 RVA: 0x00066A83 File Offset: 0x00064C83
		public override void Write(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.Write(format, arg);
		}

		/// <summary>Writes the specified string to a line without tabs.</summary>
		/// <param name="s">The string to write. </param>
		// Token: 0x06001C36 RID: 7222 RVA: 0x00066A98 File Offset: 0x00064C98
		public void WriteLineNoTabs(string s)
		{
			this._writer.WriteLine(s);
		}

		/// <summary>Writes the specified string, followed by a line terminator, to the text stream.</summary>
		/// <param name="s">The string to write. </param>
		// Token: 0x06001C37 RID: 7223 RVA: 0x00066AA6 File Offset: 0x00064CA6
		public override void WriteLine(string s)
		{
			this.OutputTabs();
			this._writer.WriteLine(s);
			this._tabsPending = true;
		}

		/// <summary>Writes a line terminator.</summary>
		// Token: 0x06001C38 RID: 7224 RVA: 0x00066AC1 File Offset: 0x00064CC1
		public override void WriteLine()
		{
			this.OutputTabs();
			this._writer.WriteLine();
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a Boolean, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The Boolean to write. </param>
		// Token: 0x06001C39 RID: 7225 RVA: 0x00066ADB File Offset: 0x00064CDB
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes a character, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The character to write. </param>
		// Token: 0x06001C3A RID: 7226 RVA: 0x00066AF6 File Offset: 0x00064CF6
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes a character array, followed by a line terminator, to the text stream.</summary>
		/// <param name="buffer">The character array to write. </param>
		// Token: 0x06001C3B RID: 7227 RVA: 0x00066B11 File Offset: 0x00064D11
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer);
			this._tabsPending = true;
		}

		/// <summary>Writes a subarray of characters, followed by a line terminator, to the text stream.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">Starting index in the buffer. </param>
		/// <param name="count">The number of characters to write. </param>
		// Token: 0x06001C3C RID: 7228 RVA: 0x00066B2C File Offset: 0x00064D2C
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer, index, count);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a Double, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The double to write. </param>
		// Token: 0x06001C3D RID: 7229 RVA: 0x00066B49 File Offset: 0x00064D49
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a Single, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The single to write. </param>
		// Token: 0x06001C3E RID: 7230 RVA: 0x00066B64 File Offset: 0x00064D64
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of an integer, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The integer to write. </param>
		// Token: 0x06001C3F RID: 7231 RVA: 0x00066B7F File Offset: 0x00064D7F
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of an 8-byte integer, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The 8-byte integer to write. </param>
		// Token: 0x06001C40 RID: 7232 RVA: 0x00066B9A File Offset: 0x00064D9A
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of an object, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The object to write. </param>
		// Token: 0x06001C41 RID: 7233 RVA: 0x00066BB5 File Offset: 0x00064DB5
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">The object to write into the formatted string. </param>
		// Token: 0x06001C42 RID: 7234 RVA: 0x00066BD0 File Offset: 0x00064DD0
		public override void WriteLine(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0);
			this._tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg0">The first object to write into the formatted string. </param>
		/// <param name="arg1">The second object to write into the formatted string. </param>
		// Token: 0x06001C43 RID: 7235 RVA: 0x00066BEC File Offset: 0x00064DEC
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0, arg1);
			this._tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg">The argument array to output. </param>
		// Token: 0x06001C44 RID: 7236 RVA: 0x00066C09 File Offset: 0x00064E09
		public override void WriteLine(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a UInt32, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">A UInt32 to output. </param>
		// Token: 0x06001C45 RID: 7237 RVA: 0x00066C25 File Offset: 0x00064E25
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x04000E6D RID: 3693
		private readonly TextWriter _writer;

		// Token: 0x04000E6E RID: 3694
		private readonly string _tabString;

		// Token: 0x04000E6F RID: 3695
		private int _indentLevel;

		// Token: 0x04000E70 RID: 3696
		private bool _tabsPending;

		/// <summary>Specifies the default tab string. This field is constant. </summary>
		// Token: 0x04000E71 RID: 3697
		public const string DefaultTabString = "    ";
	}
}
