namespace Agriculiture.BLL.Dtos;

public class AfforestationAgricultureAchievementReadDto
{
    public int Id { get; set; }
    public DateOnly DateOfPlanted { get; set; }
    public string TreeName { get; set; }
    public int TreeNameId { get; set; }
    
    public string LocationName { get; set; }

    public string UserName { get; set; }
    
   
    public int Number { get; set; }
}