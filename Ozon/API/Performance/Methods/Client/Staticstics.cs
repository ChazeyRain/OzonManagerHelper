namespace Ozon.API.Performance.Methods.Client
{
    public class Staticstics
    {
        public string[] campaigns { get; set; } = {};
        //RFC 3339
        public string from { get; set; } = "";
        //RFC 3339
        public string to { get; set; } = "";
        //YYYY-mm-DD
        public string dateFrom { get; set; } = "";
        //YYYY-mm-DD
        public string dateTo { get; set; } = "";
        public string groupBy { get; set; } = "";

        public Staticstics(IEnumerable<string> campaigns, DateTime dateFrom, DateTime dateTo, string groupBy)
        {

            this.campaigns = new string[campaigns.Count()];

            for(int i = 0; i < campaigns.Count(); i++)
            {
                this.campaigns[i] = campaigns.ElementAt(i);
            }

            this.from = dateFrom.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz");
            this.to = dateTo.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz");
            this.dateFrom = dateFrom.ToString("yyyy-MM-dd");
            this.dateTo = dateTo.ToString("yyyy-MM-dd");
            this.groupBy = groupBy;
        }

        public Staticstics(){}
        
        public class GroupBy
        {
            public static string DATE = "DATE";
            public static string START_OF_WEEK = "START_OF_WEEK";
            public static string START_OF_MONTH = "START_OF_MONTH";
            public static string NONE = "NO_GROUP_BY";
        }
    }
}