using Domain.Model;

namespace Application.Service
{
    public interface IMasterRefRepository : IGenericRepository<MasterRef>
    {
        MasterRef GetRef(string refName);
    }
}
