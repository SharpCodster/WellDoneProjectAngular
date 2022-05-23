namespace WellDoneProjectAngular.Core.Interfaces.Entities
{
    public interface IAuditableUtc : IIdentified
    {
        DateTime? CreatedAtUtc { get; set; }
        string? CreatedBy { get; set; }
        DateTime? UpdatedAtUtc { get; set; }
        string? UpdatedBy { get; set; }
        DateTime? DeletedAtUtc { get; set; }
        string? DeletedBy { get; set; }
        bool? IsDeleted { get; set; }
    }
}
