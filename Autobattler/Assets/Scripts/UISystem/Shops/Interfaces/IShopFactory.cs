namespace UISystem
{
    public interface IShopFactory<TFactoryType>
    {
        public TFactoryType Create<TData>(TData createData);
    }
}