using System.Diagnostics;
using System.Text;

namespace Alphonse.Listener;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class PhoneNumber : IComparable<PhoneNumber>, IEquatable<PhoneNumber>
{
    public static PhoneNumber Parse(string value)
        => TryParse(value, out var result) ? result! : throw new FormatException();

    public static bool TryParse(string value, out PhoneNumber result)
    {
        if (value == "P")
        {
            // private / anonymous case
            result = new();
            return true;
        }

        var digits = value.Where(d => char.IsDigit(d) || d == '+').ToList();
        if (!digits.Any()
            || digits.Skip(1).Contains('+')
            || (digits.Count == 1 && digits[0] == '+'))
        {
            result = new();
            return false;
        }

        var number = string.Concat(digits);

        if (digits[0] == '+')
        {
            // already in good format - nice !
            result = new PhoneNumber(number);
        }
        else if (digits.Count > 2 && digits[0] == '0' && digits[1] == '0')
        {
            // case 0033 1 [...]
            result = new PhoneNumber("+" + number.Substring(2));
        }
        else if (digits.Count == 10)
        {
            // well... As I'm a frog eater, deal with it !
            result = new PhoneNumber("+33" + number.Substring(1));
        }
        else
        {
            result = new();
            return false;   // don't know how to deal with
        }

        return true;
    }

    public string? Number { get; }

    public PhoneNumber()
    {
        this.Number = null; // anonymous
    }

    public PhoneNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException($"'{nameof(number)}' cannot be null or whitespace.", nameof(number));

        this.Number = number;
    }

    public override string ToString() => this.ToString(false);

    public string ToString(bool niceFormat)
    {
        if(this.Number is null)
            return niceFormat ? "Anonymous" : "PRIVATE";

        if (!niceFormat || this.Number.Length < 3)
            return this.Number;

        var remainder = this.Number.Length % 2 == 0 ? 1 : 2;

        var batchSizes = new[] { 1, 2, remainder }.Concat(Enumerable.Repeat(2, this.Number.Length / 2));
        var batches = this.Number.VariableBatch(batchSizes);

        var result = string.Join(" ", batches.Select(b => string.Concat(b)));

        return result;
    }

    public override bool Equals(object? obj)
        => object.ReferenceEquals(this, obj) ||
            obj is PhoneNumber phoneNumber && string.Equals(this.Number, phoneNumber?.Number, StringComparison.Ordinal);

    public override int GetHashCode() => this.Number?.GetHashCode() ?? 0;

    int IComparable<PhoneNumber>.CompareTo(PhoneNumber? other)
        => string.CompareOrdinal(this.Number, other?.Number);

    bool IEquatable<PhoneNumber>.Equals(PhoneNumber? other)
        => object.ReferenceEquals(this, other) || string.Equals(this.Number, other?.Number, StringComparison.Ordinal);

    private string GetDebuggerDisplay() => this.ToString(true);

    public static bool operator ==(PhoneNumber? a, PhoneNumber? b) => b is not null && b.Equals(a);
    public static bool operator !=(PhoneNumber? a, PhoneNumber? b) => !(a == b);

    public static implicit  operator string(PhoneNumber number) => number.ToString();

}