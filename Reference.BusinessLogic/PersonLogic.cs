using System.Collections.Generic;
using System.Linq;
using Reference.BusinessLogic.Contracts;
using Reference.Common.Contracts;
using Reference.Domain.Entities;

namespace Reference.BusinessLogic
{
    public class PersonLogic : IPersonLogic
    {
        public PersonLogic(IEntityContext context)
        {
            Context = context;
        }

        public IEntityContext Context { get; set; }

        public IReadOnlyList<Person> GetAll()
        {
            return Context.GetAll<Person>().ToList();
        }
    }
}