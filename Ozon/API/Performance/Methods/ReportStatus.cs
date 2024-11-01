namespace Ozon.API.Performance.Methods
{
    public class ReportStatus
    {
        public string? UUID {get; set;}
        public string? state {get; set;}
        public string? createdAt {get; set;}
        public string? updatedAt {get;set;}
        //public Responses.CampaignStaticstics? request {get; set;}
        public string? error {get; set;}
        public string? link {get; set;}
        public string? kind {get; set;}

        public class State
        {
            public static string NOT_STARTED {get; } = "NOT_STARTED";
            public static string IN_PROGRESS {get; } = "IN_PROGRESS";
            public static string ERROR {get; } = "ERROR";
            public static string OK {get; } = "OK";
        }

        public class Kind
        {
            public static string STATS {get; } = "STATS";
            public static string SEARCH_PHRASES {get; } = "SEARCH_PHRASES";
            public static string ATTRIBUTION {get; } = "ATTRIBUTION";
            public static string VIDEO {get; } = "VIDEO";
        }
    }
}