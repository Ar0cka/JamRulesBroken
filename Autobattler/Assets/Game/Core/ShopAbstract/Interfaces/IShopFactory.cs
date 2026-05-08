namespace Game.Core.ShopAbstract.Interfaces
{
    public interface IShopFactory<TFactoryType>
    {
        public TFactoryType Create<TData>(TData createData);
    }
}