using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.CustomAttribute;

namespace Warehouse.Core
{
    public class TestViewModel
    {
        public DateTime FirstDate { get; set; }
        [IsBifore(nameof(FirstDate),errorMessage:"Boom")]
        public DateTime SecondDate { get; set; }


    }
}
