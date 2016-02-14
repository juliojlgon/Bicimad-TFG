


using System.Collections.Generic;
namespace Bicimad.Core.DomainObjects.Interfaces
{
    public class IEntityComparer<T> : IEqualityComparer<T> where T : IEntity
    {
        public bool Equals(T x, T y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(T obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
