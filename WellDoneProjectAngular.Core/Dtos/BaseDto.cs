using WellDoneProjectAngular.Core.Interfaces.Entities;

namespace WellDoneProjectAngular.Core.Dtos
{
    public class BaseDto : IIdentified
    {
        public long? Id { get; set; }
    }
}
