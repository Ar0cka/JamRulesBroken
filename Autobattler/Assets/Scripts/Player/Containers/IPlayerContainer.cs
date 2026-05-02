using System.Collections.Generic;

namespace Player.Containers
{
    public interface IPlayerContainer
    {
        public List<TContainer> GetContainer<TContainer>();
    }
}