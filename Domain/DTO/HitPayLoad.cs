using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class HitpayPayload
    {
        public int amount { get; set; }
        public string currency { get; set; }
        public string[] payment_methods { get; set; }
        public bool generate_qr { get; set; }
    }
}
