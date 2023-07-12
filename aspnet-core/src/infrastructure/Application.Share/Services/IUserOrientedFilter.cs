using System;

namespace Application.Share.Services
{
    public interface IUserOrientedFilter
    {
        Guid? UserId { get; set; }
    }
}