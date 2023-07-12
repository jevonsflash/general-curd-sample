using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Matoapp.Common.DataEnumCategory
{
    public class DataEnumCategory : Category.Category
    {
        public ICollection<DataEnum.DataEnum> DataEnums { get; set; }

        [ForeignKey("ParentId")]
        public DataEnumCategory ParentDataEnumCategory { get; set; }
        public virtual ICollection<DataEnumCategory> Children { get; set; }
    }
}
