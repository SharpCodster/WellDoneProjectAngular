using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Core.Interfaces
{
    public interface IRequestContextProvider
    {
        Task<RequestContext> GetRequestContexAsync();
        T GetQueryStringValue<T>(string parameterName);
    }
}
