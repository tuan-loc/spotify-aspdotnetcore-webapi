using SpotifyWebApiDotNet.Core.Entities;
using System.Linq.Expressions;

namespace SpotifyWebApiDotNet.Core.DataAccess
{
	public interface IEntityRepository<T> where T : class, IEntity, new()
	{
		T Get(Expression<Func<T, bool>> filter);
		IList<T> GetList(Expression<Func<T, bool>> filter = null);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
