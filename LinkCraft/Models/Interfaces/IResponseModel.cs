namespace LinkCraft.Models.Interfaces
{
    public interface IResponseModel<T, E>
    {
        bool Success { get;}
        T Result { get;}
        E Errors { get;}
    }
}
