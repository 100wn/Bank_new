using System;
using System.Collections.Generic;

namespace APIBank.Model;

public partial class Card
{
    public int CardId { get; set; }

    public int ClientId { get; set; }

    public int AccountId { get; set; }

    public string CardNumber { get; set; } = null!;

    public string? MonthlyCardMaintenance { get; set; }

    public DateOnly CardExpiryDate { get; set; }

    public string? PaymentSystem { get; set; }

    public string PinCod { get; set; } = null!;

    public string CvvCvcCod { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual User Client { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
