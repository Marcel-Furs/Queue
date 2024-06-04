using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaypalIdDto
    {
        public string PaypalId { get; set; } = null!;

        public static PaypalIdDto Of(string paypalId)
        {
            return new PaypalIdDto { PaypalId = paypalId };
        }
    }
}
