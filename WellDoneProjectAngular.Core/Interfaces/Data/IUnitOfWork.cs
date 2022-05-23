namespace WellDoneProjectAngular.Core.Interfaces.Data
{
    public interface IUnitOfWork
    {
        IUnitOfWorkScope BeginScope();
    }

    public interface IUnitOfWorkScope : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
