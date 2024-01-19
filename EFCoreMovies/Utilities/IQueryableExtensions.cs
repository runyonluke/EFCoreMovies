namespace EFCoreMovies.Utilities
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int page = 1, int recordsToTake = 2)
        {
            return source.Skip((page - 1) * recordsToTake).Take(recordsToTake);
        }
    }
}
