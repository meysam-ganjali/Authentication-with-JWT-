namespace AuthJWT.Entities;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int WriterId { get; set; }
    public Writer Writer { get; set; }
}