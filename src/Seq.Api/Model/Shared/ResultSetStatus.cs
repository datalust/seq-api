namespace Seq.Api.Model.Shared
{
    public enum ResultSetStatus
    {
        Unknown,

        // Still more to come, scanned as far as requested/short circuit
        Partial,

        // Have covered the whole range
        Complete,

        // You asked for 10, I got you 10
        Full
    }
}
