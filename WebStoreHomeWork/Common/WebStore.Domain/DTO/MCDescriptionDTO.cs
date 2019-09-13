using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.DTO
{
    public class MCDescriptionDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        /*public string[] DetailedDesriptionList
        {
            get => DetailedDesription.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            set => DetailedDesription = String.Join(";", value);
        }*/

        public string DetailedDesription { get; set; }

        public string ProductName { get; set; }
    }
}
