using Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Output
{
    public interface IDataOutput
    {
        void OutputData(IEnumerable<ExpenseDataRow> data);
    }
}
