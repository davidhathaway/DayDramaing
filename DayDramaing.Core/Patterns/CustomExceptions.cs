using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public class EntityIdNotFoundException : Exception
    {
        public EntityIdNotFoundException(int id, Type type)
            : base("Entity Id Not Found: " + id + " For " + type.ToString())
        {
        }
    }

    public class EntityKeyNotFoundException : Exception
    {
        public EntityKeyNotFoundException(Type type)
            : base("Entity Key Not Found For : " + type.ToString())
        {

        }
    }

    public class EntityObjectSetNotFoundException : Exception
    {
        public EntityObjectSetNotFoundException(Type type)
            : base("Entity ObjectSet Not Found For : " + type.ToString())
        {

        }

        public EntityObjectSetNotFoundException(Type type, Exception innerException)
            : base("Entity ObjectSet Not Found For : " + type.ToString(), innerException)
        {

        }
    }
}
