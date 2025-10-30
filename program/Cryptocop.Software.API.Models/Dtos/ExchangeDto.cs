namespace Cryptocop.Software.API.Models.Dtos;

public class ExchangeDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public required string AssetSymbol { get; set; }
    public float? PriceInUsd { get; set; }
    public DateTime? LastTrade { get; set; }
}