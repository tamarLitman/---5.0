namespace DTO.Classes;

public partial class StockDto
{
    public int ProdId { get; set; }

    public string? ProdName { get; set; }

    public decimal? Price { get; set; }
    public int? MinAmount { get; set; }
    public int? SupplierId { get; set; }


}
