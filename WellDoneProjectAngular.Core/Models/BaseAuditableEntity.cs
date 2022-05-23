using System.ComponentModel.DataAnnotations.Schema;
using WellDoneProjectAngular.Core.Interfaces.Entities;

namespace WellDoneProjectAngular.Core.Models
{
    public class BaseAuditableEntity : BaseEntity, IAuditableUtc
    {
        [Column(Order = 10000)]
        public DateTime? CreatedAtUtc { get; set; }
        [Column(Order = 10001)]
        public string? CreatedBy { get; set; }
        [Column(Order = 10002)]
        public DateTime? UpdatedAtUtc { get; set; }
        [Column(Order = 10003)]
        public string? UpdatedBy { get; set; }    
        [Column(Order = 10004)]
        public DateTime? DeletedAtUtc { get; set; }
        [Column(Order = 10005)]
        public string? DeletedBy { get; set; }
        [Column(Order = 10006)]
        public bool? IsDeleted { get; set; }
    }
}
