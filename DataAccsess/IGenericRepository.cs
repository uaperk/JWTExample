using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccsess
{
    public interface IGenericRepository<TEntity, TId>
        where TEntity : class
        where TId : IEquatable<TId>
    {

        string CollectionName { get; set; }

        bool CollectionExists { get; }

        bool CreateCollection();

        bool DropCollection();

        TEntity GetById(TId id);

        int Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

    }
}
