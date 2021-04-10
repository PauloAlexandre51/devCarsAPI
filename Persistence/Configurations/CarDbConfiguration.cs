using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.API.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                 .Property(c => c.Brand)
                .HasMaxLength(100)
                //.HasColumnName("Marca")
                .HasDefaultValue("PADRÃO")
                .HasColumnType("VARCHAR(100)");

            builder
                .Property(c => c.ProductionDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
