namespace MetricsManagerService.Repositories
{
    public interface IMetricsRepository<T> where T : class
    {
        public IList<T> GetAll();
        public IList<T> GetAllByRange(TimeSpan fromTime, TimeSpan toTime);
        public Task Create(T item);
        public IList<T> GetByRange(int agentId, TimeSpan fromTime, TimeSpan toTime);
        public double GetLastTime();
    }
}
