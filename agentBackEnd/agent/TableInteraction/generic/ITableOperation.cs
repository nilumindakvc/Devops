using System.Linq.Expressions;

namespace agent.TableInteraction.generic
{
    //this is an generic interface definition,that define common operations on any table
    public interface ITableOperation<T>
    {
        public List<T> getAll();

        public T? getRecordByProperty(Expression<Func<T, bool>> filter);

        public List<T> getAllRecordsByProperty(Expression<Func<T, bool>> filter);

        public T addRecord(T record);

        public IEnumerable<T> addRecords(IEnumerable<T> records);

        public void updateRecord(T record);

        public bool deleteRecord(T record);

        public T? getRecordAsNoTrackingByProperty(Expression<Func<T, bool>> filter);
    }
}