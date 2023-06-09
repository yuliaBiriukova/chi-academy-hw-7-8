﻿using HW_7_8.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HW_7_8.DAL.Configurations
{
    public class ExpenseEntityTypeConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(c => c.Cost).IsRequired();

            builder.Property(c => c.DateCreated).IsRequired();

            builder.Property(c => c.Comment).HasMaxLength(256);
        }
    }
}