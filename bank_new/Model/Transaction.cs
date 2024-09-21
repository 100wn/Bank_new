using System;
using System.Collections.Generic;

namespace APIBank.Model;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int ClientId { get; set; }

    public int? SenderCardId { get; set; }

    public string? RecipientCardNumber { get; set; }

    public int AccountId { get; set; }

    public string? OptionAmountTransaction { get; set; }

    public DateTime DateTimeTransaction { get; set; }

    public string? OperationDescription { get; set; }

    public string? InfoCommission { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual User Client { get; set; } = null!;

    public virtual Card? SenderCard { get; set; }
}
