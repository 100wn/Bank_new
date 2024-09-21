using System;
using System.Collections.Generic;

namespace APIBank.Model;

public partial class Account
{
    public int AccountId { get; set; }

    public int ClientId { get; set; }

    public decimal Balance { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual User Client { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
