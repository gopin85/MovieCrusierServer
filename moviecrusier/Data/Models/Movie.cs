using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace moviecrusier.Data.Models
{
    public class Movie
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "budget")]
        public int Budget { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty(PropertyName = "vote_count")]
        public int VoteCount { get; set; }

        public string UserId { get; set; }

    }
}
