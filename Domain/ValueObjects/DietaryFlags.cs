using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public enum DietaryType
    {
        Vegan,
        LactosFree 
    }

    public class DietaryFlags
    {
        public List<DietaryType> Dietaries { get; }
        public string Serialized => JsonSerializer.Serialize(Dietaries, (JsonSerializerOptions)null);

        public DietaryFlags(List<DietaryType> dietaries)
        {
            Dietaries = dietaries;
        }

        public DietaryFlags(string SerializedDietaries)
            : this(JsonSerializer.Deserialize<List<DietaryType>>(SerializedDietaries, 
                (JsonSerializerOptions)null))
        {
        }
    }
}
