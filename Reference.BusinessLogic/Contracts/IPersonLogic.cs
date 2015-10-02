using System.Collections.Generic;
using Reference.Domain.Entities;

namespace Reference.BusinessLogic.Contracts
{
    public interface IPersonLogic {
        IReadOnlyList<Person> GetAll();
    }
}