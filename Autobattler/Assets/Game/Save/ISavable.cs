namespace Game.SaveSystem
{
    public interface ISavable
    {
        public object Capture();
        public void Restore(object data);
    }
}