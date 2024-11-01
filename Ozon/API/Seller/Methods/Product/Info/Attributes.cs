namespace Ozon.API.Seller.Methods.Product.Info
{
    public class Attributes
    {
        public List<Result> result {get; set;} = new List<Result>();
        public string? last_id {get; set;}
        public int total {get; set;}

        public class Result
        {
            public List<Attribute> attributes {get; set;} = new List<Attribute>();
            public Int64 description_category_id {get; set;}
            public string? color_image {get; set;}
            public List<Attribute> complex_attributes {get; set;} = new List<Attribute>();
            public int depth {get; set;}
            public string? dimension_unit {get; set;}
            public int height {get; set;}
            public Int64 id {get; set;}
            public List<Image> images {get; set;} = new List<Image>();
            public List<Image> images360 {get; set;} = new List<Image>();
            public string? name {get; set;}
            public string? offer_id {get; set;}
            public List<PdfFile> pdf_list = new List<PdfFile>();
            public Int64 type_id {get; set;}
            public int weight {get; set;}
            public string? weight_unit {get; set;}
            public int width {get; set;}

            public class Attribute
            {
                public Int64 attribute_id {get; set;}
                public Int64 complex_id {get; set;}
                public List<Value> values {get; set;} = new List<Value>();

                public class Value
                {
                    public Int64 dictionary_value_id {get; set;}
                    public string? value {get; set;}
                }
            }

            public class File
            {
                public string? file_name {get; set;}
                public Int64 index {get; set;}
            }

            public class Image : File {}
            public class PdfFile : File
            {
                public string? name {get; set;}
            }
        }
    }

    public class AttributesRequest : GenericRequest
    {
        public override string path {get;} = "/v3/products/info/attributes";

        public Filter filter {get; set; } = new Filter();
        public string? visibility { get; set; }
        public string? last_id { get; set; }
        public Int64 limit { get; set; } = 1000;
        public string? sort_by { get; set; }
        public string? sort_dir { get; set; }

        public class Filter
        {
            public List<string> offer_id { get; set; } = new List<string>();
            public List<string> product_id { get; set; } = new List<string>();
            public string? visibility {get; set;}
        
            public class Visibility
            {
                public string ALL {get;} = "ALL";
                public string VISIBLE {get;} = "VISIBLE";
                public string INVISIBLE {get;} = "INVISIBLE";
                public string EMPTY_STOCK {get;} = "EMPTY_STOCK";
                public string NOT_MODERATED {get;} = "NOT_MODERATED";
                public string MODERATED {get;} = "MODERATED";
                public string DISABLED {get;} = "DISABLED";
                public string STATE_FAILED {get;} = "STATE_FAILED";
                public string READY_TO_SUPPLY {get;} = "READY_TO_SUPPLY";
                public string VALIDATION_STATE_PENDING {get;} = "VALIDATION_STATE_PENDING";
                public string VALIDATION_STATE_FAIL {get;} = "VALIDATION_STATE_FAIL";
                public string VALIDATION_STATE_SUCCESS {get;} = "VALIDATION_STATE_SUCCESS";
                public string TO_SUPPLY {get;} = "TO_SUPPLY";
                public string IN_SALE {get;} = "IN_SALE";
                public string REMOVED_FROM_SALE {get;} = "REMOVED_FROM_SALE";
                public string BANNED {get;} = "BANNED";
                public string OVERPRICED {get;} = "OVERPRICED";
                public string CRITICALLY_OVERPRICED {get;} = "CRITICALLY_OVERPRICED";
                public string EMPTY_BARCODE {get;} = "EMPTY_BARCODE";
                public string BARCODE_EXISTS {get;} = "BARCODE_EXISTS";
                public string QUARANTINE {get;} = "QUARANTINE";
                public string ARCHIVED {get;} = "ARCHIVED";
                public string OVERPRICED_WITH_STOCK {get;} = "OVERPRICED_WITH_STOCK";
                public string PARTIAL_APPROVED {get;} = "PARTIAL_APPROVED";
                public string IMAGE_ABSENT {get;} = "IMAGE_ABSENT";
                public string MODERATION_BLOCK {get;} = "MODERATION_BLOCK";
            }
        }
    }
}