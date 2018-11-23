using LinkCraft.Models.Interfaces;

namespace LinkCraft.Models
{
    public class ResultBaseModel<T>: IResponseModel<T, string>
    {
        public bool Success { get;}

        public T Result { get; protected set; }

        public string Errors { get;}

        /// <summary>
        /// Successful model creation
        /// </summary>
        /// <param name="results"></param>
        /// <param name="isSuccess">In case T is string too</param>
        public ResultBaseModel(T results, bool isSuccess = true)
        {
            Success = true;
            Result = results;
        }

        public ResultBaseModel(string errors)
        {
            Success = false;
            Errors = errors;
        }
    }
}
