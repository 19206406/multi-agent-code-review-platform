using BuildingBlocks.CQRS;

namespace OrchestratorAgent.Application.Features.Queries.GetListOfReviews
{
    public record GetListOfReviewsQuery() : IQuery<GetListOfReviewsResponse>; 
}
