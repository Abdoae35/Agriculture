namespace Agriculiture.BLL.Dtos;

public class AfforestationUpdateDto
{
    public int Id { get; set; }
    public DateTime DateOfPlanted { get; set; }
    public string TreeTypeName { get; set; }
    public string LocationName { get; set; }
    public string LocationTypeName { get; set; }
    public int Number { get; set; }
}