namespace WellDoneProjectAngular.Core.Models
{
    public class CatalogItem : BaseAuditableEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string PictureUri { get; private set; }

        public int CatalogTypeId { get; private set; }
        public CatalogType CatalogType { get; private set; }

        public int CatalogBrandId { get; private set; }
        public CatalogBrand CatalogBrand { get; private set; }
    }
}
