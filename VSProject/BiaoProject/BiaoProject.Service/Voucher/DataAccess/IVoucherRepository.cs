using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaoProject.Service.Voucher.DataAccess
{
    public interface IVoucherRepository
    {
        RepositoryResult<string, List<Models.Voucher>> GetAllVouchers();
        RepositoryResult<Tuple<long, DateTime>, Models.Voucher> GetAllVoucherByPatNumberAndVoucherServiceDate(long patNumber, DateTime serviceDate);
    }
}
