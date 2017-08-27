namespace ContosoInc.Presentation.ViewModels
{
    public interface IHeaderInfoProvider<T>
    {
        T HeaderInfo { get; }
    }
}
