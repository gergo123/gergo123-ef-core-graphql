using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper
{
    public abstract class StateChangeEntityBase<T> : IStateChangeEntityBase<T>
        where T : Enum
    {
        public long Id { get; set; }
        public T CurrentState { get; set; }
    }

    public interface IStateChangeEntityBase<T> where T : Enum
    {
        long Id { get; set; }
        T CurrentState { get; set; }
    }
}
