using System;
namespace DataAccsess
{
    public interface IEntity<TId>
        where TId : IEquatable<TId>
    {
        TId Id { get; }
    }
}
