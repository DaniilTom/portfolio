using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Interfaces;

namespace WebStore.Domain.Implementations
{
    [Table("MCDescriptions")]
    public class MCDescription : IProductDescription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [NotMapped]
        public string[] DetailedDesriptionList {
            get => DetailedDesription.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
            set => DetailedDesription = String.Join(";", value);
        }

        private string DetailedDesription { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual ProductBase Product { get; set; }

        // нужен для приватного св-ва DetailedDescription
        public class MCDescriptionConfiguration : IEntityTypeConfiguration<MCDescription>
        {
            public void Configure(EntityTypeBuilder<MCDescription> builder)
            {
                builder.Property(nameof(DetailedDesription));
            }
        }
    }
}
