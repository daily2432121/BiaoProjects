using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiaoProject.Service.Voucher.DataAccess;

namespace BiaoProject.Service.Voucher.Services
{
    public interface IVoucherService
    {
        List<Models.Voucher> GetAllVouchers();
    }

    public class VoucherService : IVoucherService
    {
        private IVoucherRepository _repository;

        public VoucherService(IVoucherRepository repo)
        {
            _repository = repo;
        }
        public List<Models.Voucher> GetAllVouchers()
        {
            var result = _repository.GetAllVouchers();
            if (!result.Success)
            {
                throw new DataException(result.ErrorMessage);
            }
            return result.Item;
        }
    }
}
