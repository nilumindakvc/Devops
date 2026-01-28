using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace agent.TableInteraction.generic
{
    public class TableOperations<T> : ITableOperation<T> where T : class
    {
        private readonly agentDbContextSqlite _dbContext;
        private readonly DbSet<T> _table;

        public TableOperations(agentDbContextSqlite dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<T>();
        }

        public List<T> getAll()
        {
            return _table.ToList();
        }

        public T? getRecordByProperty(Expression<Func<T, bool>> filter)
        {
            var record = _table.Where(filter).FirstOrDefault();
            return record;
        }

        public List<T> getAllRecordsByProperty(Expression<Func<T, bool>> filter)
        {
            List<T> records = _table.Where(filter).ToList();
            return records;
        }

        public IEnumerable<T> addRecords(IEnumerable<T> records)
        {
            _table.AddRange(records);
            _dbContext.SaveChanges();
            return records;
        }

        public T addRecord(T record)
        {
            _table.Add(record);
            _dbContext.SaveChanges();
            return record;
        }

        public void updateRecord(T record)
        {
            _table.Update(record);
            _dbContext.SaveChanges();
        }

        public bool deleteRecord(T record)
        {
            _table.Remove(record);
            _dbContext.SaveChanges();
            return true;
        }

        public T? getRecordAsNoTrackingByProperty(Expression<Func<T, bool>> filter)
        {
            var record = _table.Where(filter).FirstOrDefault();
            return record;
        }
    }
}