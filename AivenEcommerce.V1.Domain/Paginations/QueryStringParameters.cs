using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Paginations
{
    public abstract class QueryStringParameters
	{
		public int PageNumber { get; set; } = 1;
		public string SortColumn { get; set; }
		public SortDirection SortDirection { get; set; }
		public int? PageSize { get; set; }
	}
}
