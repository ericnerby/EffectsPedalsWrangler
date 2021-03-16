example code to deserialize interface as concrete object:
```
public class Model
{
    [JsonConverter(typeof(ConcreteTypeConverter<Something>))]
    public ISomething TheThing { get; set; }
}
```