namespace Engine.SellerReportController
{
    public class SellerApiReport
    {
        public static Ozon.API.Seller.Methods.Finance.Transaction.List GetTransactions(Ozon.API.Seller.Credentials credentials, DateTime from, DateTime to)
        {
            var transactionListRequest = new Ozon.API.Seller.Methods.Finance.Transaction.ListRequest();

            transactionListRequest.filter.date.from = from.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            transactionListRequest.filter.date.to = to.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var list = Ozon.API.Seller.RequestMethods.GetTransactionList(credentials, transactionListRequest);

            for (int page = 2; page <= list.result.page_count; page++)
            {
                transactionListRequest.page = page;
                var temp = Ozon.API.Seller.RequestMethods.GetTransactionList(credentials, transactionListRequest);
                list.result.operations.AddRange(temp.result.operations);
            }

            return list;
        }
    }
}