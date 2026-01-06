using System;

namespace System.IO.Ports
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.Ports.SerialPort.PinChanged" /> event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	/// <param name="sender">The source of the event, which is the <see cref="T:System.IO.Ports.SerialPort" /> object. </param>
	/// <param name="e">A <see cref="T:System.IO.Ports.SerialPinChangedEventArgs" /> object that contains the event data. </param>
	// Token: 0x02000849 RID: 2121
	// (Invoke) Token: 0x0600437A RID: 17274
	public delegate void SerialPinChangedEventHandler(object sender, SerialPinChangedEventArgs e);
}
