using System;
using System.Collections.Generic;
using System.Text;

namespace Matoapp.Infrastructure.DesignPattern
{
    public interface IPrototypeAppService<TPrototype, TEntity>
    {
        List<TEntity> CloneEntitys(List<TPrototype> input);
        TEntity CloneEntity(TPrototype input);
    }
}
