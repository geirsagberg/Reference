using System.Collections.Generic;
using System.Linq;
using Reference.BusinessLogic.Contracts;
using Reference.Common.Contracts;
using Reference.Domain.Entities;

namespace Reference.BusinessLogic.Logic
{
    public class PersonLogic : LogicBase, IPersonLogic
    {
        private readonly ILog log;

        public PersonLogic(IEntityContext context, ILog log) : base(context)
        {
            this.log = log;
        }

        public IReadOnlyList<Person> GetAll()
        {
            log.Debug("GetAll called from {SourceContext}");
            return Context.GetAll<Person>().ToList();
        }
    }
}