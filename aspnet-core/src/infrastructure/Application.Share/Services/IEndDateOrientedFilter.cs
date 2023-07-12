using System;

namespace Application.Share.Services
{
    public interface IEndDateOrientedFilter
    {
        DateTime? EndDate { get; set; }
    }
}