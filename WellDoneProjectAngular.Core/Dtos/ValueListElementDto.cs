namespace WellDoneProjectAngular.Core.Dtos
{
    public class ValueListElementDto : BaseAuditableDto
    {
        public int EntityType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
