using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Application.Share.Services
{
    public interface IKeywordOrientedFilter
    {
        public string Keyword { get; set; }

        public string TargetFields { get; set; }
    }
}
