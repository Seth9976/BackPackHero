using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a Windows menu or toolbar command item.</summary>
	// Token: 0x02000784 RID: 1924
	public class MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommand" /> class.</summary>
		/// <param name="handler">The event to raise when the user selects the menu item or toolbar button. </param>
		/// <param name="command">The unique command ID that links this menu command to the environment's menu. </param>
		// Token: 0x06003D08 RID: 15624 RVA: 0x000D84C6 File Offset: 0x000D66C6
		public MenuCommand(EventHandler handler, CommandID command)
		{
			this._execHandler = handler;
			this.CommandID = command;
			this._status = 3;
		}

		/// <summary>Gets or sets a value indicating whether this menu item is checked.</summary>
		/// <returns>true if the item is checked; otherwise, false.</returns>
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x000D84E3 File Offset: 0x000D66E3
		// (set) Token: 0x06003D0A RID: 15626 RVA: 0x000D84F0 File Offset: 0x000D66F0
		public virtual bool Checked
		{
			get
			{
				return (this._status & 4) != 0;
			}
			set
			{
				this.SetStatus(4, value);
			}
		}

		/// <summary>Gets a value indicating whether this menu item is available.</summary>
		/// <returns>true if the item is enabled; otherwise, false.</returns>
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x000D84FA File Offset: 0x000D66FA
		// (set) Token: 0x06003D0C RID: 15628 RVA: 0x000D8507 File Offset: 0x000D6707
		public virtual bool Enabled
		{
			get
			{
				return (this._status & 2) != 0;
			}
			set
			{
				this.SetStatus(2, value);
			}
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x000D8514 File Offset: 0x000D6714
		private void SetStatus(int mask, bool value)
		{
			int num = this._status;
			if (value)
			{
				num |= mask;
			}
			else
			{
				num &= ~mask;
			}
			if (num != this._status)
			{
				this._status = num;
				this.OnCommandChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the public properties associated with the <see cref="T:System.ComponentModel.Design.MenuCommand" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the public properties of the <see cref="T:System.ComponentModel.Design.MenuCommand" />. </returns>
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x000D8554 File Offset: 0x000D6754
		public virtual IDictionary Properties
		{
			get
			{
				IDictionary dictionary;
				if ((dictionary = this._properties) == null)
				{
					dictionary = (this._properties = new HybridDictionary());
				}
				return dictionary;
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is supported.</summary>
		/// <returns>true if the item is supported, which is the default; otherwise, false.</returns>
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x000D8579 File Offset: 0x000D6779
		// (set) Token: 0x06003D10 RID: 15632 RVA: 0x000D8586 File Offset: 0x000D6786
		public virtual bool Supported
		{
			get
			{
				return (this._status & 1) != 0;
			}
			set
			{
				this.SetStatus(1, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is visible.</summary>
		/// <returns>true if the item is visible; otherwise, false.</returns>
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06003D11 RID: 15633 RVA: 0x000D8590 File Offset: 0x000D6790
		// (set) Token: 0x06003D12 RID: 15634 RVA: 0x000D859E File Offset: 0x000D679E
		public virtual bool Visible
		{
			get
			{
				return (this._status & 16) == 0;
			}
			set
			{
				this.SetStatus(16, !value);
			}
		}

		/// <summary>Occurs when the menu command changes.</summary>
		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06003D13 RID: 15635 RVA: 0x000D85AC File Offset: 0x000D67AC
		// (remove) Token: 0x06003D14 RID: 15636 RVA: 0x000D85E4 File Offset: 0x000D67E4
		public event EventHandler CommandChanged;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> associated with this menu command.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.CommandID" /> associated with the menu command.</returns>
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x000D8619 File Offset: 0x000D6819
		public virtual CommandID CommandID { get; }

		/// <summary>Invokes the command.</summary>
		// Token: 0x06003D16 RID: 15638 RVA: 0x000D8624 File Offset: 0x000D6824
		public virtual void Invoke()
		{
			if (this._execHandler != null)
			{
				try
				{
					this._execHandler(this, EventArgs.Empty);
				}
				catch (CheckoutException ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						throw;
					}
				}
			}
		}

		/// <summary>Invokes the command with the given parameter.</summary>
		/// <param name="arg">An optional argument for use by the command.</param>
		// Token: 0x06003D17 RID: 15639 RVA: 0x000D8668 File Offset: 0x000D6868
		public virtual void Invoke(object arg)
		{
			this.Invoke();
		}

		/// <summary>Gets the OLE command status code for this menu item.</summary>
		/// <returns>An integer containing a mixture of status flags that reflect the state of this menu item.</returns>
		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000D8670 File Offset: 0x000D6870
		public virtual int OleStatus
		{
			get
			{
				return this._status;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.MenuCommand.CommandChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003D19 RID: 15641 RVA: 0x000D8678 File Offset: 0x000D6878
		protected virtual void OnCommandChanged(EventArgs e)
		{
			EventHandler commandChanged = this.CommandChanged;
			if (commandChanged == null)
			{
				return;
			}
			commandChanged(this, e);
		}

		/// <summary>Returns a string representation of this menu command.</summary>
		/// <returns>A string containing the value of the <see cref="P:System.ComponentModel.Design.MenuCommand.CommandID" /> property appended with the names of any flags that are set, separated by pipe bars (|). These flag properties include <see cref="P:System.ComponentModel.Design.MenuCommand.Checked" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Enabled" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Supported" />, and <see cref="P:System.ComponentModel.Design.MenuCommand.Visible" />.</returns>
		// Token: 0x06003D1A RID: 15642 RVA: 0x000D868C File Offset: 0x000D688C
		public override string ToString()
		{
			string text = this.CommandID.ToString() + " : ";
			if ((this._status & 1) != 0)
			{
				text += "Supported";
			}
			if ((this._status & 2) != 0)
			{
				text += "|Enabled";
			}
			if ((this._status & 16) == 0)
			{
				text += "|Visible";
			}
			if ((this._status & 4) != 0)
			{
				text += "|Checked";
			}
			return text;
		}

		// Token: 0x0400223C RID: 8764
		private EventHandler _execHandler;

		// Token: 0x0400223D RID: 8765
		private int _status;

		// Token: 0x0400223E RID: 8766
		private IDictionary _properties;

		// Token: 0x0400223F RID: 8767
		private const int ENABLED = 2;

		// Token: 0x04002240 RID: 8768
		private const int INVISIBLE = 16;

		// Token: 0x04002241 RID: 8769
		private const int CHECKED = 4;

		// Token: 0x04002242 RID: 8770
		private const int SUPPORTED = 1;
	}
}
