using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class TransactionDetailDTO
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long TransactionId { get; set; }
        public long ProductId { get; set; }
        public string CustomerName { get; set; }
        public int Qty { get; set; }
        public int TotalQtyPrice { get; set; }
        public virtual Domain.Model.Product Product { get; set; }
    }
}
