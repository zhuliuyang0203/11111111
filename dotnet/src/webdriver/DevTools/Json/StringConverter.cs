using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable enable

namespace OpenQA.Selenium.DevTools.Json;

internal sealed class StringConverter : JsonConverter<string>
{
    public override bool HandleNull => true;

    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return reader.GetString();
        }
        catch (InvalidOperationException)
        {
            // Fallback to read the value as bytes instead of string.
            // System.Text.Json library throws exception when CDP remote end sends non-encoded string as binary data.
            // Using JavaScriptEncoder.UnsafeRelaxedJsonEscaping doesn't help because the string actually is byte[].
            // https://chromedevtools.github.io/devtools-protocol/tot/Network/#type-Request - here "postData" property
            // is a string, which we cannot deserialize properly. This property is marked as deprecated, and new "postDataEntries"
            // is suggested for using, where most likely it is base64 encoded.

            var bytes = reader.ValueSpan;
            var sb = new StringBuilder(bytes.Length);
            foreach (byte b in bytes)
            {
                sb.Append(Convert.ToChar(b));
            }

            return sb.ToString();
        }
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value);
}