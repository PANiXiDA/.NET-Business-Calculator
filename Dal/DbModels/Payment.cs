using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Payment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentMethod { get; set; }

    public string Status { get; set; }

    public string Description { get; set; }
}
