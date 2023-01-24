using Microsoft.Build.Framework;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Juan.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile? Photo { get; set; }
        [MaxLength(100)]
        public string? SubTitle { get; set; }
        [Required ]
      
        public string? Title { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
