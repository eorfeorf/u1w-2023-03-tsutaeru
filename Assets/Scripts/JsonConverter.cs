using System;
using LitJson;
using UnityEngine;

public class JsonConverter
{
    static JsonConverter() => JsonMapper.RegisterImporter<long, ulong>((ImporterFunc<long, ulong>) (input => Convert.ToUInt64(input)));

    public T FromJsonToInstance<T>(string json) => JsonMapper.ToObject<T>(json);

    public string FromInstanceToJson<T>(T instance) => JsonMapper.ToJson((object) instance);
}
