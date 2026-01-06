using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Win32;

namespace System.IO.Ports
{
	/// <summary>Represents a serial port resource.</summary>
	// Token: 0x02000847 RID: 2119
	[MonitoringDescription("")]
	public class SerialPort : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class.</summary>
		// Token: 0x06004325 RID: 17189 RVA: 0x000E9A5B File Offset: 0x000E7C5B
		public SerialPort()
			: this(SerialPort.GetDefaultPortName(), 9600, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified <see cref="T:System.ComponentModel.IContainer" /> object.</summary>
		/// <param name="container">An interface to a container. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06004326 RID: 17190 RVA: 0x000E9A70 File Offset: 0x000E7C70
		public SerialPort(IContainer container)
			: this()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06004327 RID: 17191 RVA: 0x000E9A78 File Offset: 0x000E7C78
		public SerialPort(string portName)
			: this(portName, 9600, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name and baud rate.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06004328 RID: 17192 RVA: 0x000E9A89 File Offset: 0x000E7C89
		public SerialPort(string portName, int baudRate)
			: this(portName, baudRate, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, and parity bit.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06004329 RID: 17193 RVA: 0x000E9A96 File Offset: 0x000E7C96
		public SerialPort(string portName, int baudRate, Parity parity)
			: this(portName, baudRate, parity, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, parity bit, and data bits.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values. </param>
		/// <param name="dataBits">The data bits value. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x0600432A RID: 17194 RVA: 0x000E9AA3 File Offset: 0x000E7CA3
		public SerialPort(string portName, int baudRate, Parity parity, int dataBits)
			: this(portName, baudRate, parity, dataBits, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, parity bit, data bits, and stop bit.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values. </param>
		/// <param name="dataBits">The data bits value. </param>
		/// <param name="stopBits">One of the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> values. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x0600432B RID: 17195 RVA: 0x000E9AB4 File Offset: 0x000E7CB4
		public SerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
		{
			this.port_name = portName;
			this.baud_rate = baudRate;
			this.data_bits = dataBits;
			this.stop_bits = stopBits;
			this.parity = parity;
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000E9B48 File Offset: 0x000E7D48
		private static string GetDefaultPortName()
		{
			string[] portNames = SerialPort.GetPortNames();
			if (portNames.Length != 0)
			{
				return portNames[0];
			}
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128 || platform == 6)
			{
				return "ttyS0";
			}
			return "COM1";
		}

		/// <summary>Gets the underlying <see cref="T:System.IO.Stream" /> object for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream is in a .NET Compact Framework application and one of the following methods was called:<see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /><see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /><see cref="M:System.IO.Stream.EndRead(System.IAsyncResult)" /><see cref="M:System.IO.Stream.EndWrite(System.IAsyncResult)" />The .NET Compact Framework does not support the asynchronous model with base streams.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x0600432D RID: 17197 RVA: 0x000E9B89 File Offset: 0x000E7D89
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Stream BaseStream
		{
			get
			{
				this.CheckOpen();
				return (Stream)this.stream;
			}
		}

		/// <summary>Gets or sets the serial baud rate.</summary>
		/// <returns>The baud rate.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The baud rate specified is less than or equal to zero, or is greater than the maximum allowable baud rate for the device. </exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state. - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x0600432E RID: 17198 RVA: 0x000E9B9C File Offset: 0x000E7D9C
		// (set) Token: 0x0600432F RID: 17199 RVA: 0x000E9BA4 File Offset: 0x000E7DA4
		[DefaultValue(9600)]
		[Browsable(true)]
		[MonitoringDescription("")]
		public int BaudRate
		{
			get
			{
				return this.baud_rate;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(value, this.parity, this.data_bits, this.stop_bits, this.handshake);
				}
				this.baud_rate = value;
			}
		}

		/// <summary>Gets or sets the break signal state.</summary>
		/// <returns>true if the port is in a break state; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x000E9BF3 File Offset: 0x000E7DF3
		// (set) Token: 0x06004331 RID: 17201 RVA: 0x000E9BFB File Offset: 0x000E7DFB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool BreakState
		{
			get
			{
				return this.break_state;
			}
			set
			{
				this.CheckOpen();
				if (value == this.break_state)
				{
					return;
				}
				this.stream.SetBreakState(value);
				this.break_state = value;
			}
		}

		/// <summary>Gets the number of bytes of data in the receive buffer.</summary>
		/// <returns>The number of bytes of data in the receive buffer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The port is not open.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06004332 RID: 17202 RVA: 0x000E9C20 File Offset: 0x000E7E20
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int BytesToRead
		{
			get
			{
				this.CheckOpen();
				return this.stream.BytesToRead;
			}
		}

		/// <summary>Gets the number of bytes of data in the send buffer.</summary>
		/// <returns>The number of bytes of data in the send buffer.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x000E9C33 File Offset: 0x000E7E33
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int BytesToWrite
		{
			get
			{
				this.CheckOpen();
				return this.stream.BytesToWrite;
			}
		}

		/// <summary>Gets the state of the Carrier Detect line for the port.</summary>
		/// <returns>true if the carrier is detected; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06004334 RID: 17204 RVA: 0x000E9C46 File Offset: 0x000E7E46
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CDHolding
		{
			get
			{
				this.CheckOpen();
				return (this.stream.GetSignals() & SerialSignal.Cd) > SerialSignal.None;
			}
		}

		/// <summary>Gets the state of the Clear-to-Send line.</summary>
		/// <returns>true if the Clear-to-Send line is detected; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x000E9C5E File Offset: 0x000E7E5E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CtsHolding
		{
			get
			{
				this.CheckOpen();
				return (this.stream.GetSignals() & SerialSignal.Cts) > SerialSignal.None;
			}
		}

		/// <summary>Gets or sets the standard length of data bits per byte.</summary>
		/// <returns>The data bits length.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The data bits value is less than 5 or more than 8. </exception>
		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x000E9C76 File Offset: 0x000E7E76
		// (set) Token: 0x06004337 RID: 17207 RVA: 0x000E9C80 File Offset: 0x000E7E80
		[MonitoringDescription("")]
		[Browsable(true)]
		[DefaultValue(8)]
		public int DataBits
		{
			get
			{
				return this.data_bits;
			}
			set
			{
				if (value < 5 || value > 8)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, this.parity, value, this.stop_bits, this.handshake);
				}
				this.data_bits = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether null bytes are ignored when transmitted between the port and the receive buffer.</summary>
		/// <returns>true if null bytes are ignored; otherwise false. The default is false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x06004339 RID: 17209 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO("Not implemented")]
		[Browsable(true)]
		[MonitoringDescription("")]
		[DefaultValue(false)]
		public bool DiscardNull
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the state of the Data Set Ready (DSR) signal.</summary>
		/// <returns>true if a Data Set Ready signal has been sent to the port; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x000E9CD3 File Offset: 0x000E7ED3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool DsrHolding
		{
			get
			{
				this.CheckOpen();
				return (this.stream.GetSignals() & SerialSignal.Dsr) > SerialSignal.None;
			}
		}

		/// <summary>Gets or sets a value that enables the Data Terminal Ready (DTR) signal during serial communication.</summary>
		/// <returns>true to enable Data Terminal Ready (DTR); otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x000E9CEB File Offset: 0x000E7EEB
		// (set) Token: 0x0600433C RID: 17212 RVA: 0x000E9CF3 File Offset: 0x000E7EF3
		[MonitoringDescription("")]
		[DefaultValue(false)]
		[Browsable(true)]
		public bool DtrEnable
		{
			get
			{
				return this.dtr_enable;
			}
			set
			{
				if (value == this.dtr_enable)
				{
					return;
				}
				if (this.is_open)
				{
					this.stream.SetSignal(SerialSignal.Dtr, value);
				}
				this.dtr_enable = value;
			}
		}

		/// <summary>Gets or sets the byte encoding for pre- and post-transmission conversion of text.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object. The default is <see cref="T:System.Text.ASCIIEncoding" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.Encoding" /> property was set to null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.Encoding" /> property was set to an encoding that is not <see cref="T:System.Text.ASCIIEncoding" />, <see cref="T:System.Text.UTF8Encoding" />, <see cref="T:System.Text.UTF32Encoding" />, <see cref="T:System.Text.UnicodeEncoding" />, one of the Windows single byte encodings, or one of the Windows double byte encodings.</exception>
		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x000E9D1B File Offset: 0x000E7F1B
		// (set) Token: 0x0600433E RID: 17214 RVA: 0x000E9D23 File Offset: 0x000E7F23
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("")]
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets the handshaking protocol for serial port transmission of data.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.Handshake" /> values. The default is None.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value passed is not a valid value in the <see cref="T:System.IO.Ports.Handshake" /> enumeration.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x000E9D3A File Offset: 0x000E7F3A
		// (set) Token: 0x06004340 RID: 17216 RVA: 0x000E9D44 File Offset: 0x000E7F44
		[MonitoringDescription("")]
		[Browsable(true)]
		[DefaultValue(Handshake.None)]
		public Handshake Handshake
		{
			get
			{
				return this.handshake;
			}
			set
			{
				if (value < Handshake.None || value > Handshake.RequestToSendXOnXOff)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, this.parity, this.data_bits, this.stop_bits, value);
				}
				this.handshake = value;
			}
		}

		/// <summary>Gets a value indicating the open or closed status of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>true if the serial port is open; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> value passed is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> value passed is an empty string ("").</exception>
		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x000E9D97 File Offset: 0x000E7F97
		[Browsable(false)]
		public bool IsOpen
		{
			get
			{
				return this.is_open;
			}
		}

		/// <summary>Gets or sets the value used to interpret the end of a call to the <see cref="M:System.IO.Ports.SerialPort.ReadLine" /> and <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)" /> methods.</summary>
		/// <returns>A value that represents the end of a line. The default is a line feed, (<see cref="P:System.Environment.NewLine" />).</returns>
		/// <exception cref="T:System.ArgumentException">The property value is empty.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property value is null.</exception>
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x000E9D9F File Offset: 0x000E7F9F
		// (set) Token: 0x06004343 RID: 17219 RVA: 0x000E9DA7 File Offset: 0x000E7FA7
		[DefaultValue("\n")]
		[Browsable(false)]
		[MonitoringDescription("")]
		public string NewLine
		{
			get
			{
				return this.new_line;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("NewLine cannot be null or empty.", "value");
				}
				this.new_line = value;
			}
		}

		/// <summary>Gets or sets the parity-checking protocol.</summary>
		/// <returns>One of the enumeration values that represents the parity-checking protocol. The default is <see cref="F:System.IO.Ports.Parity.None" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.Parity" /> value passed is not a valid value in the <see cref="T:System.IO.Ports.Parity" /> enumeration.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06004344 RID: 17220 RVA: 0x000E9DD6 File Offset: 0x000E7FD6
		// (set) Token: 0x06004345 RID: 17221 RVA: 0x000E9DE0 File Offset: 0x000E7FE0
		[MonitoringDescription("")]
		[DefaultValue(Parity.None)]
		[Browsable(true)]
		public Parity Parity
		{
			get
			{
				return this.parity;
			}
			set
			{
				if (value < Parity.None || value > Parity.Space)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, value, this.data_bits, this.stop_bits, this.handshake);
				}
				this.parity = value;
			}
		}

		/// <summary>Gets or sets the byte that replaces invalid bytes in a data stream when a parity error occurs.</summary>
		/// <returns>A byte that replaces invalid bytes.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06004346 RID: 17222 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x06004347 RID: 17223 RVA: 0x0000822E File Offset: 0x0000642E
		[DefaultValue(63)]
		[Browsable(true)]
		[MonoTODO("Not implemented")]
		[MonitoringDescription("")]
		public byte ParityReplace
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the port for communications, including but not limited to all available COM ports.</summary>
		/// <returns>The communications port. The default is COM1.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to a value with a length of zero.-or-The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to a value that starts with "\\".-or-The port name was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is open. </exception>
		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x000E9E33 File Offset: 0x000E8033
		// (set) Token: 0x06004349 RID: 17225 RVA: 0x000E9E3C File Offset: 0x000E803C
		[Browsable(true)]
		[MonitoringDescription("")]
		[DefaultValue("COM1")]
		public string PortName
		{
			get
			{
				return this.port_name;
			}
			set
			{
				if (this.is_open)
				{
					throw new InvalidOperationException("Port name cannot be set while port is open.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0 || value.StartsWith("\\\\"))
				{
					throw new ArgumentException("value");
				}
				this.port_name = value;
			}
		}

		/// <summary>Gets or sets the size of the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The buffer size, in bytes. The default value is 4096.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> value set is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> property was set while the stream was open.</exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> property was set to an odd integer value. </exception>
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x0600434A RID: 17226 RVA: 0x000E9E91 File Offset: 0x000E8091
		// (set) Token: 0x0600434B RID: 17227 RVA: 0x000E9E99 File Offset: 0x000E8099
		[Browsable(true)]
		[DefaultValue(4096)]
		[MonitoringDescription("")]
		public int ReadBufferSize
		{
			get
			{
				return this.readBufferSize;
			}
			set
			{
				if (this.is_open)
				{
					throw new InvalidOperationException();
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value <= 4096)
				{
					return;
				}
				this.readBufferSize = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish.</summary>
		/// <returns>The number of milliseconds before a time-out occurs when a read operation does not finish.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The read time-out value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />. </exception>
		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x000E9EC8 File Offset: 0x000E80C8
		// (set) Token: 0x0600434D RID: 17229 RVA: 0x000E9ED0 File Offset: 0x000E80D0
		[DefaultValue(-1)]
		[Browsable(true)]
		[MonitoringDescription("")]
		public int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.ReadTimeout = value;
				}
				this.read_timeout = value;
			}
		}

		/// <summary>Gets or sets the number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event occurs.</summary>
		/// <returns>The number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event is fired. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReceivedBytesThreshold" /> value is less than or equal to zero. </exception>
		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x0600434E RID: 17230 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x0600434F RID: 17231 RVA: 0x000E9F00 File Offset: 0x000E8100
		[MonitoringDescription("")]
		[Browsable(true)]
		[DefaultValue(1)]
		[MonoTODO("Not implemented")]
		public int ReceivedBytesThreshold
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the Request to Send (RTS) signal is enabled during serial communication.</summary>
		/// <returns>true to enable Request to Transmit (RTS); otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.IO.Ports.SerialPort.RtsEnable" /> property was set or retrieved while the <see cref="P:System.IO.Ports.SerialPort.Handshake" /> property is set to the <see cref="F:System.IO.Ports.Handshake.RequestToSend" /> value or the <see cref="F:System.IO.Ports.Handshake.RequestToSendXOnXOff" /> value.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x000E9F16 File Offset: 0x000E8116
		// (set) Token: 0x06004351 RID: 17233 RVA: 0x000E9F1E File Offset: 0x000E811E
		[Browsable(true)]
		[DefaultValue(false)]
		[MonitoringDescription("")]
		public bool RtsEnable
		{
			get
			{
				return this.rts_enable;
			}
			set
			{
				if (value == this.rts_enable)
				{
					return;
				}
				if (this.is_open)
				{
					this.stream.SetSignal(SerialSignal.Rts, value);
				}
				this.rts_enable = value;
			}
		}

		/// <summary>Gets or sets the standard number of stopbits per byte.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.StopBits" /> values.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.StopBits" /> value is  <see cref="F:System.IO.Ports.StopBits.None" />.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06004352 RID: 17234 RVA: 0x000E9F47 File Offset: 0x000E8147
		// (set) Token: 0x06004353 RID: 17235 RVA: 0x000E9F50 File Offset: 0x000E8150
		[DefaultValue(StopBits.One)]
		[MonitoringDescription("")]
		[Browsable(true)]
		public StopBits StopBits
		{
			get
			{
				return this.stop_bits;
			}
			set
			{
				if (value < StopBits.One || value > StopBits.OnePointFive)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, this.parity, this.data_bits, value, this.handshake);
				}
				this.stop_bits = value;
			}
		}

		/// <summary>Gets or sets the size of the serial port output buffer. </summary>
		/// <returns>The size of the output buffer. The default is 2048.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> value is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> property was set while the stream was open.</exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> property was set to an odd integer value. </exception>
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x000E9FA3 File Offset: 0x000E81A3
		// (set) Token: 0x06004355 RID: 17237 RVA: 0x000E9FAB File Offset: 0x000E81AB
		[Browsable(true)]
		[MonitoringDescription("")]
		[DefaultValue(2048)]
		public int WriteBufferSize
		{
			get
			{
				return this.writeBufferSize;
			}
			set
			{
				if (this.is_open)
				{
					throw new InvalidOperationException();
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value <= 2048)
				{
					return;
				}
				this.writeBufferSize = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish.</summary>
		/// <returns>The number of milliseconds before a time-out occurs. The default is <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteTimeout" /> value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />. </exception>
		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x000E9FDA File Offset: 0x000E81DA
		// (set) Token: 0x06004357 RID: 17239 RVA: 0x000E9FE2 File Offset: 0x000E81E2
		[DefaultValue(-1)]
		[MonitoringDescription("")]
		[Browsable(true)]
		public int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.WriteTimeout = value;
				}
				this.write_timeout = value;
			}
		}

		/// <summary>Closes the port connection, sets the <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> property to false, and disposes of the internal <see cref="T:System.IO.Stream" /> object.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.- or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06004358 RID: 17240 RVA: 0x000EA012 File Offset: 0x000E8212
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Ports.SerialPort" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x06004359 RID: 17241 RVA: 0x000EA01B File Offset: 0x000E821B
		protected override void Dispose(bool disposing)
		{
			if (!this.is_open)
			{
				return;
			}
			this.is_open = false;
			if (disposing)
			{
				this.stream.Close();
			}
			this.stream = null;
		}

		/// <summary>Discards data from the serial driver's receive buffer.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600435A RID: 17242 RVA: 0x000EA042 File Offset: 0x000E8242
		public void DiscardInBuffer()
		{
			this.CheckOpen();
			this.stream.DiscardInBuffer();
		}

		/// <summary>Discards data from the serial driver's transmit buffer.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600435B RID: 17243 RVA: 0x000EA055 File Offset: 0x000E8255
		public void DiscardOutBuffer()
		{
			this.CheckOpen();
			this.stream.DiscardOutBuffer();
		}

		/// <summary>Gets an array of serial port names for the current computer.</summary>
		/// <returns>An array of serial port names for the current computer.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The serial port names could not be queried.</exception>
		// Token: 0x0600435C RID: 17244 RVA: 0x000EA068 File Offset: 0x000E8268
		public static string[] GetPortNames()
		{
			int platform = (int)Environment.OSVersion.Platform;
			List<string> list = new List<string>();
			if (platform == 4 || platform == 128 || platform == 6)
			{
				string[] files = Directory.GetFiles("/dev/", "tty*");
				bool flag = false;
				foreach (string text in files)
				{
					if (text.StartsWith("/dev/ttyS") || text.StartsWith("/dev/ttyUSB") || text.StartsWith("/dev/ttyACM"))
					{
						flag = true;
						break;
					}
				}
				foreach (string text2 in files)
				{
					if (flag)
					{
						if (text2.StartsWith("/dev/ttyS") || text2.StartsWith("/dev/ttyUSB") || text2.StartsWith("/dev/ttyACM"))
						{
							list.Add(text2);
						}
					}
					else if (text2 != "/dev/tty" && text2.StartsWith("/dev/tty") && !text2.StartsWith("/dev/ttyC"))
					{
						list.Add(text2);
					}
				}
			}
			else
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM"))
				{
					if (registryKey != null)
					{
						foreach (string text3 in registryKey.GetValueNames())
						{
							string text4 = registryKey.GetValue(text3, "").ToString();
							if (text4 != "")
							{
								list.Add(text4);
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x000EA208 File Offset: 0x000E8408
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT;
			}
		}

		/// <summary>Opens a new serial port connection.</summary>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied to the port.- or -The current process, or another process on the system, already has the specified COM port open either by a <see cref="T:System.IO.Ports.SerialPort" /> instance or in unmanaged code.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the properties for this instance are invalid. For example, the <see cref="P:System.IO.Ports.SerialPort.Parity" />, <see cref="P:System.IO.Ports.SerialPort.DataBits" />, or <see cref="P:System.IO.Ports.SerialPort.Handshake" /> properties are not valid values; the <see cref="P:System.IO.Ports.SerialPort.BaudRate" /> is less than or equal to zero; the <see cref="P:System.IO.Ports.SerialPort.ReadTimeout" /> or <see cref="P:System.IO.Ports.SerialPort.WriteTimeout" /> property is less than zero and is not <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />. </exception>
		/// <exception cref="T:System.ArgumentException">The port name does not begin with "COM". - or -The file type of the port is not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port on the current instance of the <see cref="T:System.IO.Ports.SerialPort" /> is already open.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600435E RID: 17246 RVA: 0x000EA22C File Offset: 0x000E842C
		public void Open()
		{
			if (this.is_open)
			{
				throw new InvalidOperationException("Port is already open");
			}
			if (SerialPort.IsWindows)
			{
				this.stream = new WinSerialStream(this.port_name, this.baud_rate, this.data_bits, this.parity, this.stop_bits, this.dtr_enable, this.rts_enable, this.handshake, this.read_timeout, this.write_timeout, this.readBufferSize, this.writeBufferSize);
			}
			else
			{
				this.stream = new SerialPortStream(this.port_name, this.baud_rate, this.data_bits, this.parity, this.stop_bits, this.dtr_enable, this.rts_enable, this.handshake, this.read_timeout, this.write_timeout, this.readBufferSize, this.writeBufferSize);
			}
			this.is_open = true;
		}

		/// <summary>Reads a number of bytes from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer and writes those bytes into a byte array at the specified offset.</summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="buffer">The byte array to write the input to. </param>
		/// <param name="offset">The offset in the buffer array to begin writing. </param>
		/// <param name="count">The number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentNullException">The buffer passed is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.TimeoutException">No bytes were available to read.</exception>
		// Token: 0x0600435F RID: 17247 RVA: 0x000EA304 File Offset: 0x000E8504
		public int Read(byte[] buffer, int offset, int count)
		{
			this.CheckOpen();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			return this.stream.Read(buffer, offset, count);
		}

		/// <summary>Reads a number of characters from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer and writes them into an array of characters at a given offset.</summary>
		/// <returns>The number of characters read.</returns>
		/// <param name="buffer">The character array to write the input to. </param>
		/// <param name="offset">The offset in the buffer array to begin writing. </param>
		/// <param name="count">The number of characters to read. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the buffer.- or -<paramref name="count" /> is 1 and there is a surrogate character in the buffer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">No characters were available to read.</exception>
		// Token: 0x06004360 RID: 17248 RVA: 0x000EA360 File Offset: 0x000E8560
		public int Read(char[] buffer, int offset, int count)
		{
			this.CheckOpen();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num = 0;
			int num2;
			while (num < count && (num2 = this.ReadChar()) != -1)
			{
				buffer[offset + num] = (char)num2;
				num++;
			}
			return num;
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x000EA3CC File Offset: 0x000E85CC
		internal int read_byte()
		{
			byte[] array = new byte[1];
			if (this.stream.Read(array, 0, 1) > 0)
			{
				return (int)array[0];
			}
			return -1;
		}

		/// <summary>Synchronously reads one byte from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The byte, cast to an <see cref="T:System.Int32" />, or -1 if the end of the stream has been read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.- or -No byte was read.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06004362 RID: 17250 RVA: 0x000EA3F6 File Offset: 0x000E85F6
		public int ReadByte()
		{
			this.CheckOpen();
			return this.read_byte();
		}

		/// <summary>Synchronously reads one character from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The character that was read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.- or -No character was available in the allotted time-out period.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06004363 RID: 17251 RVA: 0x000EA404 File Offset: 0x000E8604
		public int ReadChar()
		{
			this.CheckOpen();
			byte[] array = new byte[16];
			int num = 0;
			char[] chars;
			for (;;)
			{
				int num2 = this.read_byte();
				if (num2 == -1)
				{
					break;
				}
				array[num++] = (byte)num2;
				chars = this.encoding.GetChars(array, 0, 1);
				if (chars.Length != 0)
				{
					goto Block_2;
				}
				if (num >= array.Length)
				{
					return -1;
				}
			}
			return -1;
			Block_2:
			return (int)chars[0];
		}

		/// <summary>Reads all immediately available bytes, based on the encoding, in both the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>The contents of the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06004364 RID: 17252 RVA: 0x000EA458 File Offset: 0x000E8658
		public string ReadExisting()
		{
			this.CheckOpen();
			int bytesToRead = this.BytesToRead;
			byte[] array = new byte[bytesToRead];
			int num = this.stream.Read(array, 0, bytesToRead);
			return new string(this.encoding.GetChars(array, 0, num));
		}

		/// <summary>Reads up to the <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value in the input buffer.</summary>
		/// <returns>The contents of the input buffer up to the first occurrence of a <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended.- or -No bytes were read.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06004365 RID: 17253 RVA: 0x000EA49B File Offset: 0x000E869B
		public string ReadLine()
		{
			return this.ReadTo(this.new_line);
		}

		/// <summary>Reads a string up to the specified <paramref name="value" /> in the input buffer.</summary>
		/// <returns>The contents of the input buffer up to the specified <paramref name="value" />.</returns>
		/// <param name="value">A value that indicates where the read operation stops. </param>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="value" /> parameter is 0.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x06004366 RID: 17254 RVA: 0x000EA4AC File Offset: 0x000E86AC
		public string ReadTo(string value)
		{
			this.CheckOpen();
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("value");
			}
			byte[] bytes = this.encoding.GetBytes(value);
			int num = 0;
			List<byte> list = new List<byte>();
			for (;;)
			{
				int num2 = this.read_byte();
				if (num2 == -1)
				{
					goto IL_0089;
				}
				list.Add((byte)num2);
				if (num2 == (int)bytes[num])
				{
					num++;
					if (num == bytes.Length)
					{
						break;
					}
				}
				else
				{
					num = (((int)bytes[0] == num2) ? 1 : 0);
				}
			}
			return this.encoding.GetString(list.ToArray(), 0, list.Count - bytes.Length);
			IL_0089:
			return this.encoding.GetString(list.ToArray());
		}

		/// <summary>Writes the specified string to the serial port.</summary>
		/// <param name="text">The string for output. </param>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x06004367 RID: 17255 RVA: 0x000EA554 File Offset: 0x000E8754
		public void Write(string text)
		{
			this.CheckOpen();
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			byte[] bytes = this.encoding.GetBytes(text);
			this.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes a specified number of bytes to the serial port using data from a buffer.</summary>
		/// <param name="buffer">The byte array that contains the data to write to the port. </param>
		/// <param name="offset">The zero-based byte offset in the <paramref name="buffer" /> parameter at which to begin copying bytes to the port. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x06004368 RID: 17256 RVA: 0x000EA590 File Offset: 0x000E8790
		public void Write(byte[] buffer, int offset, int count)
		{
			this.CheckOpen();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			this.stream.Write(buffer, offset, count);
		}

		/// <summary>Writes a specified number of characters to the serial port using data from a buffer.</summary>
		/// <param name="buffer">The character array that contains the data to write to the port. </param>
		/// <param name="offset">The zero-based byte offset in the <paramref name="buffer" /> parameter at which to begin copying bytes to the port. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x06004369 RID: 17257 RVA: 0x000EA5E8 File Offset: 0x000E87E8
		public void Write(char[] buffer, int offset, int count)
		{
			this.CheckOpen();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			byte[] bytes = this.encoding.GetBytes(buffer, offset, count);
			this.stream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes the specified string and the <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value to the output buffer.</summary>
		/// <param name="text">The string to write to the output buffer. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">The <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)" /> method could not write to the stream.  </exception>
		// Token: 0x0600436A RID: 17258 RVA: 0x000EA64E File Offset: 0x000E884E
		public void WriteLine(string text)
		{
			this.Write(text + this.new_line);
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x000EA662 File Offset: 0x000E8862
		private void CheckOpen()
		{
			if (!this.is_open)
			{
				throw new InvalidOperationException("Specified port is not open.");
			}
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x000EA678 File Offset: 0x000E8878
		internal void OnErrorReceived(SerialErrorReceivedEventArgs args)
		{
			SerialErrorReceivedEventHandler serialErrorReceivedEventHandler = (SerialErrorReceivedEventHandler)base.Events[this.error_received];
			if (serialErrorReceivedEventHandler != null)
			{
				serialErrorReceivedEventHandler(this, args);
			}
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x000EA6A8 File Offset: 0x000E88A8
		internal void OnDataReceived(SerialDataReceivedEventArgs args)
		{
			SerialDataReceivedEventHandler serialDataReceivedEventHandler = (SerialDataReceivedEventHandler)base.Events[this.data_received];
			if (serialDataReceivedEventHandler != null)
			{
				serialDataReceivedEventHandler(this, args);
			}
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000EA6D8 File Offset: 0x000E88D8
		internal void OnDataReceived(SerialPinChangedEventArgs args)
		{
			SerialPinChangedEventHandler serialPinChangedEventHandler = (SerialPinChangedEventHandler)base.Events[this.pin_changed];
			if (serialPinChangedEventHandler != null)
			{
				serialPinChangedEventHandler(this, args);
			}
		}

		/// <summary>Represents the method that handles the error event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x0600436F RID: 17263 RVA: 0x000EA707 File Offset: 0x000E8907
		// (remove) Token: 0x06004370 RID: 17264 RVA: 0x000EA71B File Offset: 0x000E891B
		[MonitoringDescription("")]
		public event SerialErrorReceivedEventHandler ErrorReceived
		{
			add
			{
				base.Events.AddHandler(this.error_received, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.error_received, value);
			}
		}

		/// <summary>Represents the method that will handle the serial pin changed event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06004371 RID: 17265 RVA: 0x000EA72F File Offset: 0x000E892F
		// (remove) Token: 0x06004372 RID: 17266 RVA: 0x000EA743 File Offset: 0x000E8943
		[MonitoringDescription("")]
		public event SerialPinChangedEventHandler PinChanged
		{
			add
			{
				base.Events.AddHandler(this.pin_changed, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.pin_changed, value);
			}
		}

		/// <summary>Represents the method that will handle the data received event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06004373 RID: 17267 RVA: 0x000EA757 File Offset: 0x000E8957
		// (remove) Token: 0x06004374 RID: 17268 RVA: 0x000EA76B File Offset: 0x000E896B
		[MonitoringDescription("")]
		public event SerialDataReceivedEventHandler DataReceived
		{
			add
			{
				base.Events.AddHandler(this.data_received, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.data_received, value);
			}
		}

		/// <summary>Indicates that no time-out should occur.</summary>
		// Token: 0x0400288B RID: 10379
		public const int InfiniteTimeout = -1;

		// Token: 0x0400288C RID: 10380
		private const int DefaultReadBufferSize = 4096;

		// Token: 0x0400288D RID: 10381
		private const int DefaultWriteBufferSize = 2048;

		// Token: 0x0400288E RID: 10382
		private const int DefaultBaudRate = 9600;

		// Token: 0x0400288F RID: 10383
		private const int DefaultDataBits = 8;

		// Token: 0x04002890 RID: 10384
		private const Parity DefaultParity = Parity.None;

		// Token: 0x04002891 RID: 10385
		private const StopBits DefaultStopBits = StopBits.One;

		// Token: 0x04002892 RID: 10386
		private bool is_open;

		// Token: 0x04002893 RID: 10387
		private int baud_rate;

		// Token: 0x04002894 RID: 10388
		private Parity parity;

		// Token: 0x04002895 RID: 10389
		private StopBits stop_bits;

		// Token: 0x04002896 RID: 10390
		private Handshake handshake;

		// Token: 0x04002897 RID: 10391
		private int data_bits;

		// Token: 0x04002898 RID: 10392
		private bool break_state;

		// Token: 0x04002899 RID: 10393
		private bool dtr_enable;

		// Token: 0x0400289A RID: 10394
		private bool rts_enable;

		// Token: 0x0400289B RID: 10395
		private ISerialStream stream;

		// Token: 0x0400289C RID: 10396
		private Encoding encoding = Encoding.ASCII;

		// Token: 0x0400289D RID: 10397
		private string new_line = Environment.NewLine;

		// Token: 0x0400289E RID: 10398
		private string port_name;

		// Token: 0x0400289F RID: 10399
		private int read_timeout = -1;

		// Token: 0x040028A0 RID: 10400
		private int write_timeout = -1;

		// Token: 0x040028A1 RID: 10401
		private int readBufferSize = 4096;

		// Token: 0x040028A2 RID: 10402
		private int writeBufferSize = 2048;

		// Token: 0x040028A3 RID: 10403
		private object error_received = new object();

		// Token: 0x040028A4 RID: 10404
		private object data_received = new object();

		// Token: 0x040028A5 RID: 10405
		private object pin_changed = new object();
	}
}
