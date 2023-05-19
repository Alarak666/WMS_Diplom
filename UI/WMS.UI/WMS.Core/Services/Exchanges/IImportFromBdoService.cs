using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Core.Services.Exchanges
{
    public interface IImportFromBdoService
    {
        public delegate Task UpdateProgress(int current, int total, string table);
        event UpdateProgress OnUpdateProgress;
        Task ImportWarehouses();
        Task ImportProducts();
        Task ImportCustomers();
        Task ImportCompanies();
        Task ImportBanks();
        Task ImportBankAccounts();
        Task ImportCurrencies();
        Task ImportOpenBalanceOfGood();
        Task ImportCustomerInvoices();
        Task ImportContracts();
        Task ImportFakeData();
        Task ClearAllData();
    }
}
