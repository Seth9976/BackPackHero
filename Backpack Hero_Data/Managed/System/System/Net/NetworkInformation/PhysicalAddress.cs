using System;
using System.Text;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides the Media Access Control (MAC) address for a network interface (adapter).</summary>
	// Token: 0x0200050C RID: 1292
	public class PhysicalAddress
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> class. </summary>
		/// <param name="address">A <see cref="T:System.Byte" /> array containing the address.</param>
		// Token: 0x060029DD RID: 10717 RVA: 0x00099BF3 File Offset: 0x00097DF3
		public PhysicalAddress(byte[] address)
		{
			this.address = address;
		}

		/// <summary>Returns the hash value of a physical address.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x060029DE RID: 10718 RVA: 0x00099C0C File Offset: 0x00097E0C
		public override int GetHashCode()
		{
			if (this.changed)
			{
				this.changed = false;
				this.hash = 0;
				int num = this.address.Length & -4;
				int i;
				for (i = 0; i < num; i += 4)
				{
					this.hash ^= (int)this.address[i] | ((int)this.address[i + 1] << 8) | ((int)this.address[i + 2] << 16) | ((int)this.address[i + 3] << 24);
				}
				if ((this.address.Length & 3) != 0)
				{
					int num2 = 0;
					int num3 = 0;
					while (i < this.address.Length)
					{
						num2 |= (int)this.address[i] << num3;
						num3 += 8;
						i++;
					}
					this.hash ^= num2;
				}
			}
			return this.hash;
		}

		/// <summary>Compares two <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> instances.</summary>
		/// <returns>true if this instance and the specified instance contain the same address; otherwise false.</returns>
		/// <param name="comparand">The <see cref="T:System.Net.NetworkInformation.PhysicalAddress" />  to compare to the current instance.</param>
		// Token: 0x060029DF RID: 10719 RVA: 0x00099CD4 File Offset: 0x00097ED4
		public override bool Equals(object comparand)
		{
			PhysicalAddress physicalAddress = comparand as PhysicalAddress;
			if (physicalAddress == null)
			{
				return false;
			}
			if (this.address.Length != physicalAddress.address.Length)
			{
				return false;
			}
			for (int i = 0; i < physicalAddress.address.Length; i++)
			{
				if (this.address[i] != physicalAddress.address[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the <see cref="T:System.String" /> representation of the address of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the address contained in this instance.</returns>
		// Token: 0x060029E0 RID: 10720 RVA: 0x00099D2C File Offset: 0x00097F2C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in this.address)
			{
				int num = (b >> 4) & 15;
				for (int j = 0; j < 2; j++)
				{
					if (num < 10)
					{
						stringBuilder.Append((char)(num + 48));
					}
					else
					{
						stringBuilder.Append((char)(num + 55));
					}
					num = (int)(b & 15);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns the address of the current instance.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the address.</returns>
		// Token: 0x060029E1 RID: 10721 RVA: 0x00099DA0 File Offset: 0x00097FA0
		public byte[] GetAddressBytes()
		{
			byte[] array = new byte[this.address.Length];
			Buffer.BlockCopy(this.address, 0, array, 0, this.address.Length);
			return array;
		}

		/// <summary>Parses the specified <see cref="T:System.String" /> and stores its contents as the address bytes of the <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> returned by this method.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> instance with the specified address.</returns>
		/// <param name="address">A <see cref="T:System.String" /> containing the address that will be used to initialize the <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> instance returned by this method.</param>
		/// <exception cref="T:System.FormatException">The <paramref name="address" /> parameter contains an illegal hardware address. This exception also occurs if the <paramref name="address" /> parameter contains a string in the incorrect format.</exception>
		// Token: 0x060029E2 RID: 10722 RVA: 0x00099DD4 File Offset: 0x00097FD4
		public static PhysicalAddress Parse(string address)
		{
			int num = 0;
			bool flag = false;
			if (address == null)
			{
				return PhysicalAddress.None;
			}
			byte[] array;
			if (address.IndexOf('-') >= 0)
			{
				flag = true;
				array = new byte[(address.Length + 1) / 3];
			}
			else
			{
				if (address.Length % 2 > 0)
				{
					throw new FormatException(SR.GetString("An invalid physical address was specified."));
				}
				array = new byte[address.Length / 2];
			}
			int num2 = 0;
			int i = 0;
			while (i < address.Length)
			{
				int num3 = (int)address[i];
				if (num3 >= 48 && num3 <= 57)
				{
					num3 -= 48;
					goto IL_00C3;
				}
				if (num3 >= 65 && num3 <= 70)
				{
					num3 -= 55;
					goto IL_00C3;
				}
				if (num3 != 45)
				{
					throw new FormatException(SR.GetString("An invalid physical address was specified."));
				}
				if (num != 2)
				{
					throw new FormatException(SR.GetString("An invalid physical address was specified."));
				}
				num = 0;
				IL_0100:
				i++;
				continue;
				IL_00C3:
				if (flag && num >= 2)
				{
					throw new FormatException(SR.GetString("An invalid physical address was specified."));
				}
				if (num % 2 == 0)
				{
					array[num2] = (byte)(num3 << 4);
				}
				else
				{
					byte[] array2 = array;
					int num4 = num2++;
					array2[num4] |= (byte)num3;
				}
				num++;
				goto IL_0100;
			}
			if (num < 2)
			{
				throw new FormatException(SR.GetString("An invalid physical address was specified."));
			}
			return new PhysicalAddress(array);
		}

		// Token: 0x04001873 RID: 6259
		private byte[] address;

		// Token: 0x04001874 RID: 6260
		private bool changed = true;

		// Token: 0x04001875 RID: 6261
		private int hash;

		/// <summary>Returns a new <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> instance with a zero length address. This field is read-only.</summary>
		// Token: 0x04001876 RID: 6262
		public static readonly PhysicalAddress None = new PhysicalAddress(new byte[0]);
	}
}
