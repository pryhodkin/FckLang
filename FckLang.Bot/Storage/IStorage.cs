using System.Collections;
using System.Collections.Generic;

namespace FckLang.Bot.Storage
{
    public interface IStorage<T> : IEnumerable<T>, IEnumerable
    {
    }
}
