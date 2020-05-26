namespace PhungDKH.Ordering.Api.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PhungDKH.Ordering.Service.Categories;
    using PhungDKH.Core.Models.Common;

    [Route("api/orders")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        ///   Initializes a new instance of the <see cref="OrderController" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="mapper">The auto mapper.</param>
        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///   Create a new order for the user.
        /// </summary>
        /// <param name="request">The request body when create a new order for the user.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns.</returns>
        [HttpPost]
        public async Task<ResponseModel> PostAsync([FromBody] OrderCreateRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }
    }
}
