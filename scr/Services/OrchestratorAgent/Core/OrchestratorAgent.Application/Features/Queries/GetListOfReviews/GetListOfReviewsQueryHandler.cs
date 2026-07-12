using BuildingBlocks.CQRS;

namespace OrchestratorAgent.Application.Features.Queries.GetListOfReviews
{
    public class GetListOfReviewsQueryHandler : IQueryHandler<GetListOfReviewsQuery, GetListOfReviewsResponse>
    {
        public Task<GetListOfReviewsResponse> Handle(GetListOfReviewsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
