using Newtonsoft.Json;
using Tutum.StaticValues;

namespace Tutum.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PreviewPath { get; set; }

        public bool IsPremiumOnly { get; set; }

        [JsonIgnore]
        public string PreviewUrl { get => $"{ApiStrings.BLOB}{PreviewPath}"; }
    }
}
