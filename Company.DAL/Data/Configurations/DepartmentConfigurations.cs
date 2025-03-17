using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(D => D.Id).UseIdentityColumn(10, 10);

            //builder.HasMany(D => D.Employees)
            //    .WithOne(E => E.Department)
            //    .HasForeignKey(E => E.DepartmentId)
            //    .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(d => d.Employees)       // Department has many Employees
                .WithOne(e => e.Department)         // Employee belongs to one Department
                .HasForeignKey(e => e.DepartmentId) // Foreign Key in Employee
                .OnDelete(DeleteBehavior.SetNull);  // Optional: Set NULL on delete

        }
    }
}
