using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Represents a single relationship between an object and a member.</summary>
	// Token: 0x020007A4 RID: 1956
	public readonly struct MemberRelationship
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> class. </summary>
		/// <param name="owner">The object that owns <paramref name="member" />.</param>
		/// <param name="member">The member which is to be related to <paramref name="owner" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> or <paramref name="member" /> is null.</exception>
		// Token: 0x06003DC7 RID: 15815 RVA: 0x000D9D9E File Offset: 0x000D7F9E
		public MemberRelationship(object owner, MemberDescriptor member)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this.Owner = owner;
			this.Member = member;
		}

		/// <summary>Gets a value indicating whether this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship. </summary>
		/// <returns>true if this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship; otherwise, false.</returns>
		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000D9DCA File Offset: 0x000D7FCA
		public bool IsEmpty
		{
			get
			{
				return this.Owner == null;
			}
		}

		/// <summary>Gets the related member.</summary>
		/// <returns>The member that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x000D9DD5 File Offset: 0x000D7FD5
		public MemberDescriptor Member { get; }

		/// <summary>Gets the owning object.</summary>
		/// <returns>The owning object that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000D9DDD File Offset: 0x000D7FDD
		public object Owner { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> is equal to the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> to compare with the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</param>
		// Token: 0x06003DCB RID: 15819 RVA: 0x000D9DE8 File Offset: 0x000D7FE8
		public override bool Equals(object obj)
		{
			if (!(obj is MemberRelationship))
			{
				return false;
			}
			MemberRelationship memberRelationship = (MemberRelationship)obj;
			return memberRelationship.Owner == this.Owner && memberRelationship.Member == this.Member;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</returns>
		// Token: 0x06003DCC RID: 15820 RVA: 0x000D9E26 File Offset: 0x000D8026
		public override int GetHashCode()
		{
			if (this.Owner == null)
			{
				return base.GetHashCode();
			}
			return this.Owner.GetHashCode() ^ this.Member.GetHashCode();
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equivalent.</summary>
		/// <returns>This operator returns true if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the equality operator.</param>
		// Token: 0x06003DCD RID: 15821 RVA: 0x000D9E58 File Offset: 0x000D8058
		public static bool operator ==(MemberRelationship left, MemberRelationship right)
		{
			return left.Owner == right.Owner && left.Member == right.Member;
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different.</summary>
		/// <returns>This operator returns true if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the inequality operator.</param>
		// Token: 0x06003DCE RID: 15822 RVA: 0x000D9E7C File Offset: 0x000D807C
		public static bool operator !=(MemberRelationship left, MemberRelationship right)
		{
			return !(left == right);
		}

		/// <summary>Represents the empty member relationship. This field is read-only.</summary>
		// Token: 0x04002609 RID: 9737
		public static readonly MemberRelationship Empty;
	}
}
