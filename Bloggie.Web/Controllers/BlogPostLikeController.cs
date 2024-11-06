using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostLikesController : ControllerBase
	{
		private readonly IBlogPostLikeRepository BlogPostLikesRepository;

		public BlogPostLikesController(IBlogPostLikeRepository BlogPostLikesRepository)
        {
			this.BlogPostLikesRepository = BlogPostLikesRepository;
		}

        [HttpPost]
		[Route("Add")]

		public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
		{
			var model = new BlogPostLike
			{
				BlogPostId = addLikeRequest.BlogPostId,
				UserId = addLikeRequest.UserId
			};

			await BlogPostLikesRepository.AddLikeForBlog(model);

			return Ok();

		}

		[HttpGet]
		[Route("{blogPostId:Guid}/totalLikes")]
		public async Task<IActionResult>GetTotalLikesForBlog([FromRoute] Guid blogPostId)
		{
			var totalLikes = await BlogPostLikesRepository.GetTotalLikes(blogPostId);

			return Ok(totalLikes);
		}

	}
}
