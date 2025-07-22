using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgricultureFrontEnd.Models.Vm.AfforestationVM;

public class AfforestationSearchVM
{
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public int? SelectedUserId { get; set; }
    public int? SelectedLocationId { get; set; }
    public int? SelectedTreeId { get; set; }

    public List<SelectListItem> Users { get; set; }
    public List<SelectListItem> Locations { get; set; }
    public List<SelectListItem> Trees { get; set; }
}