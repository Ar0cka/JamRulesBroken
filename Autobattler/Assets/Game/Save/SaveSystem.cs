using Game.SaveSystem;

namespace Game.Save
{
    public class SaveSystem
    {
        private readonly SaveRegister _register;
        
        public SaveSystem(SaveRegister register)
        {
            _register = register;
        }

        public void SaveAll()
        {
            var list = _register.GetAll();
            
            foreach (var item in list)
            {
                
            }
        }
    }
}