using System.Collections.Generic;
using System.Linq;
using Reference.BusinessLogic.Contracts;
using Reference.Common.Contracts;
using Reference.Domain.Entities;

namespace Reference.BusinessLogic
{
    public class PersonLogic : IPersonLogic
    {
        private readonly ILog log;

        public PersonLogic(IEntityContext context, ILog log)
        {
            this.log = log;
            Context = context;
        }

        public IEntityContext Context { get; set; }

        public IReadOnlyList<Person> GetAll()
        {
            log.Debug("GetAll called from {SourceContext}");
            return Context.GetAll<Person>().ToList();
        }
    }
}