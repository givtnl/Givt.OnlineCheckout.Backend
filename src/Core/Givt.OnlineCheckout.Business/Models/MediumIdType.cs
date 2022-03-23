using Givt.OnlineCheckout.Business.Exceptions;
using Newtonsoft.Json;
using Microsoft.Linq.Translations;

namespace Givt.OnlineCheckout.Business.Models;

[JsonConverter(typeof(MediumIdTypeSerializer))]
public struct MediumIdType : IComparable<MediumIdType>
{
    private readonly string _value;
    private MediumIdType(string value)
    {
        if (value.Contains('.'))
        {
            if (value[..value.IndexOf('.')].Length != 20)
                throw new InvalidMediumException(nameof(Namespace), value);
            if (value[(value.IndexOf('.') + 1)..].Length != 12)
                throw new InvalidMediumException(nameof(Instance), value);
        } else if (value.Length != 20)
            throw new InvalidMediumException(nameof(Namespace), value);

        _value = value;
    }

    private static readonly CompiledExpression<MediumIdType, string> NameSpaceExpression =
        DefaultTranslationOf<MediumIdType>.Property(m => m.Namespace)
        .Is(m => ((string)m).Substring(0, 20));

    private static readonly CompiledExpression<MediumIdType, string> InstanceExpression =
        DefaultTranslationOf<MediumIdType>.Property(m => m.Instance)
        .Is(m => ((string)m).Length > 20 ? ((string)m).Substring(21, 12) : null);
    
    public bool Equals(MediumIdType other)
    {
        return _value == other._value;
    }

    public override bool Equals(object obj)
    {
        return obj is MediumIdType other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (_value != null ? _value.GetHashCode() : 0);
    }

    public static bool operator ==(MediumIdType a, MediumIdType b)
    {
        return a.Equals(b);
    } 
    public static bool operator !=(MediumIdType a, MediumIdType b)
    {
        return !(a == b);
    }

    public static implicit operator MediumIdType(string value)
    {
        return new MediumIdType(value.ToLower());
    }

    public static implicit operator string(MediumIdType type)
    {
        return type._value.ToLower();
    }

    public override string ToString()
    {
        return _value;
    }

    public int CompareTo(MediumIdType other)
    {
        return string.Compare(_value, other._value, StringComparison.Ordinal);
    }

    public string Namespace => NameSpaceExpression.Evaluate(this);

    public string Instance => InstanceExpression.Evaluate(this);
}