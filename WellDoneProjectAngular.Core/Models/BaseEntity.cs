using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellDoneProjectAngular.Core.Interfaces.Entities;

namespace WellDoneProjectAngular.Core.Models
{
    public abstract class BaseEntity : IIdentified
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public long? Id { get; set; }
    }
}
