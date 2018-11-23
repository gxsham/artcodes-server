using System;

namespace LinkCraft.Models.Interfaces
{
    public interface IExperience: IBaseExperience
    {
        Guid Id { get; set; }
        string UserId { get; set; }
    }
}
