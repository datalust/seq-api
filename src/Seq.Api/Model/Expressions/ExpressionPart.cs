namespace Seq.Api.Model.Expressions
{
    public class ExpressionPart
    {
        public string StrictExpression { get; set; }
        public bool MatchedAsText { get; set; }
        public string ReasonIfMatchedAsText { get; set; }
    }
}
