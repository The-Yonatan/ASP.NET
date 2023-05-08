using System.ComponentModel.DataAnnotations;

namespace Assignment_1.Models;



public enum Genre
{
    Action, Driving, FPS
}


public class Game
{
    [Key]
    public int GameId { get; set; }
    [Required]
    public string Title { get; set; } = "";
    [Required]
    public string Developer { get; set; } = "";
    [EnumDataType(typeof(Genre))]
    [Required]
    public string Genre { get; set; } = "";
    [Required]

    [DateSeenValidation(ErrorMessage = "Purcase daet cant be in the future")]
    public int ReleaseYear { get; set; }

    [DataType(DataType.Date), Display(Name = "Purchase date")]

    [DateSeenValidation(ErrorMessage ="Purcase daet cant be in the future") ]
    public DateTime PurchaseDate { get; set; }
    [Range(1,10)]
    public int? Rating { get; set; }

}
