using System.Collections.Generic;

namespace Game.SaveSystem
{
    public class SaveRegister
    {
        private readonly List<ISavable> _register = new();

        public void Register(ISavable savable) => _register.Add(savable);
        public void Unregister(ISavable savable) => _register.Remove(savable);
        
        public IReadOnlyList<ISavable> GetAll() => _register;
    }
}