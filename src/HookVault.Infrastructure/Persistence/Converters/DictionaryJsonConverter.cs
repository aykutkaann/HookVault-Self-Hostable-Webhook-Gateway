using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace HookVault.Infrastructure.Persistence.Converters
{

    public sealed class DictionaryJsonConverter : ValueConverter<IReadOnlyDictionary<string, string>, string>
    {
        public DictionaryJsonConverter() : base(
            // Write: Dictionary -> JSON String
            dict => JsonSerializer.Serialize(dict, (JsonSerializerOptions?)null),

            // Read: JSON String -> Dictionary
            json => JsonSerializer.Deserialize<Dictionary<string, string>>(json, (JsonSerializerOptions?)null)! ?? new Dictionary<string, string>()
        )
        {
        }
    }


}
