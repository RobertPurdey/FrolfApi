using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Contracts
{
    public interface IOwnable
    {
        Guid CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
