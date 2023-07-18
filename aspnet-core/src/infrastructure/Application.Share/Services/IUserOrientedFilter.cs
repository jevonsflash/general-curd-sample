using System;

namespace Application.Share.Services
{
    public interface IUserOrientedFilter
    {
        public string EntityUserIdIdiom { get; }
        Guid? UserId { get; set; }
    }
}