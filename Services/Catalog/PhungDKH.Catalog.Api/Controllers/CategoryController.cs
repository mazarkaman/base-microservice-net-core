﻿namespace PhungDKH.Catalog.Api.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PhungDKH.Catalog.Service.Categories;
    using PhungDKH.Core.Models.Common;

    [Route("api/categories")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CategoryController" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="mapper">The auto mapper.</param>
        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///   Get a page of category.
        /// </summary>
        /// <param name="request">The request for category, with paging.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns the page.</returns>
        [HttpGet]
        public async Task<PagedList<CategoryViewModel>> GetAsync([FromQuery] CategoryPageListRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }

        /// <summary>
        ///   Get a category.
        /// </summary>
        /// <param name="id">The id of the category.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns the category.</returns>
        [HttpGet("{id}")]
        public async Task<CategoryViewModel> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new CategoryGetByIdRequest { Id = id };
            return await this.mediator.Send(request, cancellationToken);
        }

        /// <summary>
        ///   Create a new category.
        /// </summary>
        /// <param name="request">The request body when create a new category.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns.</returns>
        [HttpPost]
        public async Task<ResponseModel> PostAsync([FromBody] CategoryCreateRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }

        /// <summary>
        ///   Create a new category.
        /// </summary>
        /// <param name="request">The request body when create a new category.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns.</returns>
        [HttpPut("{id}")]
        public async Task<ResponseModel> PutAsync(Guid id, [FromBody] CategoryEditRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            return await this.mediator.Send(request, cancellationToken);
        }

        /// <summary>
        ///   Delete a category.
        /// </summary>
        /// <param name="id">The id of the category.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns.</returns>
        [HttpDelete("{id}")]
        public async Task<ResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new CategoryDeleteRequest()
            {
                Id = id
            };
            return await this.mediator.Send(request, cancellationToken);
        }
    }
}
