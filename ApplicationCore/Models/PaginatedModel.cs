namespace ApplicationCore.Models;

public class PaginatedModel<T> where T : class
{
    public List<T> Items =  new List<T>();
    public int CurrentIndex;
    public int TotalPages;
    public int Genre = -1;
}