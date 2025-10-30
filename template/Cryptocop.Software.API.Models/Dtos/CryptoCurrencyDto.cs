namespace Cryptocop.Software.API.Models.Dtos;

public class CryptoCurrencyDto
{
    public int Id { get; set; }
    public required string Symbol { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public float PriceInUsd { get; set; }
    public required string ProjectDetails { get; set; }
}