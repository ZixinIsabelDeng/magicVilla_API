// Import the necessary namespaces
using magicVilla_Web.Models.Dto;  // Importing DTO (Data Transfer Object) models
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;  // For model validation attributes
using Microsoft.AspNetCore.Mvc.Rendering;  // For SelectListItem


// Define the namespace
namespace MagicVilla_Web.Models.VM  // VM stands for ViewModel
{
    // Define the VillaNumberUpdateVM class, which serves as a ViewModel
    public class VillaNumberUpdateVM
    {
        // Default constructor
        public VillaNumberUpdateVM()
        {
            // Initialize VillaNumber property with a new instance of VillaNumberUpdateDTO
            // This ensures that VillaNumber is never null, avoiding NullReferenceException
            VillaNumber = new VillaNumberUpdateDTO();
        }

        // Property to hold the data for updating a villa number
        // This is of type VillaNumberUpdateDTO, which likely contains the fields needed for the update operation
        public VillaNumberUpdateDTO VillaNumber { get; set; }

        // Property to hold a list of villas for a dropdown or similar UI element
        // ValidateNever attribute indicates that this property should be skipped during model validation
        // This is useful because this list is likely populated in the controller, not by user input
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
