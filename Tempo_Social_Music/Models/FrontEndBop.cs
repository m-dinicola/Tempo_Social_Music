using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempo_Social_Music.Models
{
    public class FrontEndBop
    {
        public int RecommendationId { get; set; }
        public int UserFrom { get; set; }
        public int RecSongId { get; set; }
        public int UserTo { get; set; }

        public FrontEndBop(Recommendation recommendation)
        {
            RecommendationId = recommendation.RecommendationId;
            UserFrom = recommendation.UserFrom;
            RecSongId = recommendation.RecSongId;
            UserTo = recommendation.UserTo;
        }
    }
}
