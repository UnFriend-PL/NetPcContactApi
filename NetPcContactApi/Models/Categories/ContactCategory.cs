namespace NetPcContactApi.Models.Categories
{
    public class ContactCategory
    {
        public int ContactCategoryId { get; set; }
        public string Name { get; set; }
    }
    public class ContactSubCategory
    {
        public int ContactSubCategoryId { get; set; }
        public int ContactCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ContactSubCategoryDto
    {
        public int ContactCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
