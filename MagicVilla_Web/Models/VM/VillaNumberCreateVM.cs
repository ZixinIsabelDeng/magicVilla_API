

using magicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.VM
{
    public class VillaNumberCreateVM
    {
        public VillaNumberCreateVM() 
        {
            VillaNumber=new VillaNumberCreatedDTO();
        }
        public VillaNumberCreatedDTO VillaNumber {  get; set; }
      
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList {  get; set; }


    }
}
