

namespace AgricultureFrontEnd.Models.Vm.AfforestationVM;

public class AfforestationFormVM
{
    
    public int TreeNameId { get; set; }
    public int LocationNameId { get; set; }
    public int Number { get; set; }
    public DateTime DateOfPlanted { get; set; }

    public List<TreeReadVM> TreeNames { get; set; }
    public List<LocationNameReadVM> LocationNames { get; set; }
}