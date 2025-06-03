namespace ApplicationCore.Models;

public class PurchasedMovieCardModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PosterURL { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }
    public Guid PurchasedNumber { get; set; }
}