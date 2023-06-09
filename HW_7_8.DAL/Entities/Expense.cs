﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW_7_8.DAL.Entities
{
    public class Expense
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("cost")]
        public int Cost { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("comment")]
        public string? Comment { get; set; }

        [ForeignKey("category_id")]
        public Category ExpenseCategory { get; set; }

        [ForeignKey("user_id")]
        public IdentityUser User { get; set; }
    }
}