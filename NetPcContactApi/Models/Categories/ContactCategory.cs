namespace NetPcContactApi.Models.Categories
{
    public class ContactCategory
    {
        public int ContactCategoryId { get; set; }
        public string Name { get; set; }
    }
    public class SubContactCategory
    {
        public int SubContactCategoryId { get; set; }
        public int ContactCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
