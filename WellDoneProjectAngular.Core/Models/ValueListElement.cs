using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellDoneProjectAngular.Core.Models
{
    public class ValueListElement : BaseAuditableEntity
    {
        [Column(Order = 1)]
        public int EntityType { get; set; }

        [Column(Order = 2), MaxLength(200)]
        public string Name { get; set; }

        [Column(Order = 3)]
        public string Value { get; set; }
    }
}
