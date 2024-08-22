using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Questao5.Domain.Enumerators
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum ETipoMovimento
    {
        C = 'C',
        D = 'D'
    }
}
