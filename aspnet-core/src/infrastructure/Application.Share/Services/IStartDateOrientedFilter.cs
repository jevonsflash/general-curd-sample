using System;

namespace Application.Share.Services
{
    public interface IStartDateOrientedFilter
    {
        DateTime? StartDate { get; set; }
    }
}