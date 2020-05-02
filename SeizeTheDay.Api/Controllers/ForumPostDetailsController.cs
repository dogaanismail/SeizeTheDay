using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataDomain.Api;
using SeizeTheDay.DataDomain.DTOs;
using SeizeTheDay.DataDomain.Enumerations;
using System;
using System.Linq;
using System.Web.Http;
using ModelForumpost = Xgteamc1XgTeamModel.ForumPost;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/postdetail")]
    public class ForumPostDetailsController : BaseController
    {
        #region Ctor
        private readonly IForumPostService _forumPostService;

        public ForumPostDetailsController(IForumPostService forumPostService)
        {
            _forumPostService = forumPostService;
        }

        #endregion

        [HttpGet]
        [Route("getdetailsbyid")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public TopicDetailDto GetPostDetailById(int id)
        {
            ModelForumpost getPost = _forumPostService.SingleInclude(id);

            var postInf = new TopicDetailDto
            {
                ForumPostID = getPost.ForumPostID,
                ForumPostTitle = getPost.ForumPostTitle,
                ForumPostContent = getPost.ForumPostContent,
                CreatedTime = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(getPost.CreatedTime.Value.Month) + " " +
                getPost.CreatedTime.Value.Day.ToString() + "," + getPost.CreatedTime.Value.Year.ToString(),
                CreatedBy = getPost.CreatedBy,
                ForumTopicID = getPost.ForumTopicID,
                ForumID = getPost.ForumID,
                ShowInPortal = getPost.ShowInPortal,
                PostLocked = getPost.PostLocked,
                ReviewCount = getPost.ReviewCount,
                IsDefault = getPost.IsDefault,
                CreatedByUserName = getPost.User.UserName,
                CreatedByUserID = getPost.User.Id,
                CreatedByPhotoPath = getPost.User.UserInfoe_Id.PhotoPath,
                ForumName = getPost.Forum.ForumName,
                ForumTopicName = getPost.ForumTopic.ForumTopicName,
                CommentCount = getPost.ForumPostComments.Count().ToString(),
                PostLikesCount = getPost.ForumPostLikes.Count().ToString()
            };
            return postInf;
        }

        [Route("editpostdetail")]
        [HttpPost]
        public IHttpActionResult EditPostDetail([FromBody] PostEditApi model)
        {
            try
            {
                ModelForumpost getPost = _forumPostService.GetByForumPost(model.PostID);
                getPost.ForumPostContent = model.Content;
                getPost.ForumPostTitle = model.Title;
                _forumPostService.Update(getPost);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletepost")]
        [HttpPost]
        public IHttpActionResult DeletePost(int id)
        {
            try
            {
                ModelForumpost getPost = _forumPostService.GetByForumPost(id);
                _forumPostService.Delete(getPost);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
