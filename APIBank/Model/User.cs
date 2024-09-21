using System;
using System.Collections.Generic;

namespace APIBank.Model;

public partial class User
{
    public int UserId { get; set; }

    public string UserNumberPhone { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserFio { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public DateOnly UserBirthday { get; set; }

    public int UserRoleId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual Role UserRole { get; set; } = null!;
}
