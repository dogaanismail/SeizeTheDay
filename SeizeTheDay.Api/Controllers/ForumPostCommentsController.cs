using Microsoft.AspNet.Identity;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataDomain.Api;
using SeizeTheDay.DataDomain.DTOs;
using SeizeTheDay.DataDomain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xgteamc1XgTeamModel;
using ModelPostComment = Xgteamc1XgTeamModel.ForumPostComment;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/comments")]
    public class ForumPostCommentsController : BaseController
    {
        #region Ctor
        private readonly IForumPostCommentService _commentService;

        public ForumPostCommentsController(IForumPostCommentService commentService)
        {
            _commentService = commentService;
        }
        #endregion

        [HttpGet]
        [Route("getlistbypostid")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<PostCommentDto> GetListByPostId(int id)
        {
            List<PostCommentDto> comments = _commentService.GetCommentsByPostId(id).Select(x => new PostCommentDto()
            {
                CommentID = x.ForumPostCommentID,
                Text = x.Text,
                CreatedTime = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.CreatedTime.Value.Month) + " " +
                x.CreatedTime.Value.Day.ToString() + "," + x.CreatedTime.Value.Year.ToString(),
                CreatedBy = x.CreatedBy,
                ForumPostID = x.ForumPostID,
                CreatedByUserName = x.User.UserName,
                CreatedByUserID = x.User.Id,
                CreatedByPhotoPath = x.User.UserInfoe_Id.PhotoPath,
                CommentLikesCount = x.ForumCommentLikes.Count().ToString()
            }).ToList();

            return comments;
        }

        [HttpGet]
        [Route("getbyid")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public PostCommentDto GetById(int id)
        {
            ModelPostComment data = _commentService.GetByForumPostComment(id);
            if (data != null)
            {
                PostCommentDto commentInf = new PostCommentDto
                {
                    Text = data.Text
                };

                return commentInf;
            }
            return null;
        }

        [HttpGet]
        [Route("getcomments")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<PostCommentDto> GetComments()
        {
            List<PostCommentDto> comments = _commentService.GetListWithInclude().Select(x => new PostCommentDto()
            {
                CommentID = x.ForumPostCommentID,
                Text = x.Text,
                CreatedTime = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.CreatedTime.Value.Month) + " " +
                x.CreatedTime.Value.Day.ToString() + "," + x.CreatedTime.Value.Year.ToString(),
                CreatedBy = x.CreatedBy,
                ForumPostID = x.ForumPostID,
                CreatedByUserName = x.User.UserName,
                CreatedByUserID = x.User.Id,
                CreatedByPhotoPath = x.User.UserInfoe_Id.PhotoPath,
                CommentLikesCount = x.ForumCommentLikes.Count().ToString()
            }).ToList();

            return comments;
        }

        [Route("createcomment")]
        [HttpPost]
        public IHttpActionResult CreateForumComment([FromBody] ForumPostCommentApi model)
        {
            try
            {
                ModelPostComment newComment = new ModelPostComment
                {
                    Text = model.Text,
                    CreatedTime = DateTime.Now,
                    CreatedBy = User.Identity.GetUserId(),
                    ForumPostID = model.PostID
                };
                _commentService.Add(newComment);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletecomment")]
        [HttpPost]
        public IHttpActionResult DeleteForumComment([FromBody] ForumPostCommentApi model)
        {
            try
            {
                var getForumComment = _commentService.GetByForumPostComment(model.CommentID);
                _commentService.Delete(getForumComment);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletecomment")]
        [HttpPost]
        public IHttpActionResult DeleteForumComment(int id)
        {
            try
            {
                var getForumComment = _commentService.GetByForumPostComment(id);
                _commentService.Delete(getForumComment);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("updatecomment")]
        [HttpPost]
        public IHttpActionResult UpdateForumComment([FromBody] ForumPostCommentApi model)
        {
            try
            {
                ForumPostComment getCom = _commentService.GetByForumPostComment(Convert.ToInt32(model.CommentID));
                getCom.Text = model.Text;
                _commentService.Update(getCom);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
