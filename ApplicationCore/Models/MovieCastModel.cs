namespace ApplicationCore.Models;

public class MovieCastModel
{
    public int CastId { get; set; }
    public string CastName { get; set; }
    public string CharacterName { get; set; }
    public string AvatarUrl { get; set; }
    public string TmdbUrl { get; set; }
}