using WellDoneProjectAngular.Core.Interfaces.Entities;

namespace WellDoneProjectAngular.Core.Dtos
{
    public class BaseAuditableDto : BaseDto, IAuditableUtc
    {
        
        public DateTime? CreatedAtUtc { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAtUtc { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
