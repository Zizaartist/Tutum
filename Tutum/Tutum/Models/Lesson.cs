using Newtonsoft.Json;
using Tutum.StaticValues;

namespace Tutum.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string VideoPath { get; set; }

        public string PreviewPath { get; set; }

        [JsonIgnore]
        public string VideoUrl { get => $"{ApiStrings.ENDPOINT}{VideoPath}"; }
        [JsonIgnore]
        public string PreviewUrl { get => $"{ApiStrings.ENDPOINT}{PreviewPath}"; }
    }
}
