using SeizeTheDay.Business.Abstract.MySQL;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using Xgteamc1XgTeamModel;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using SeizeTheDay.DataDomain.ViewModels;
using SeizeTheDay.Entities.EntityClasses.MySQL;
using SeizeTheDay.Entities.Identity.Entities;
using SeizeTheDay.Business.Concrete.IdentityManagers;
using SeizeTheDay.FilterAttributes;

namespace SeizeTheDay.Web.Controllers
{
    public class UsersController : Controller
    {
        // GET: User
        #region DefinitionsOfServices

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly IUserService _userService;
        private readonly ICountryService _countryService;
        private readonly IFriendRequestService _friendRequestService;
        private readonly IFriendService _friendService;
        private readonly IChatBoxService _chatBoxService;
        private readonly IChatService _chatService;
        private readonly IUserInfoService _userInfoService;
        private readonly GeneralHelper hepler = new GeneralHelper();

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            ApplicationRoleManager roleManager, IUserService userService, ICountryService countryService, 
            IFriendRequestService friendRequestService, IFriendService friendService, 
            IChatBoxService chatBoxService, IChatService chatService, IUserInfoService userInfoService)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _countryService = countryService;
            _friendRequestService = friendRequestService;
            _friendService = friendService;
            _chatBoxService = chatBoxService;
            _chatService = chatService;
            _userInfoService = userInfoService;

        }

        #endregion

        #region ProfileManagement

        [AllowAnonymous]
        [Visitor]
        public new ActionResult Profile(string id)
        {
            Xgteamc1XgTeamModel.User GetUser = _userService.SingleStringIncludeWithExp(id);
            if (GetUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.request = _friendRequestService.GetList();
            return View(GetUser);
        }


        [Authorize(Roles = "User,Admin")]
        public ActionResult EditProfile(string id)
        {
            var getUser = _userManager.FindById(id);

            if (getUser.Id == User.Identity.GetUserId())
            {
                var selected = _countryService.FirstOrDefault();
                SelectList list = new SelectList(_countryService.GetList(), "CountryID", "CountryName", selectedValue: selected.CountryID);
                ViewData["country"] = list;
                Xgteamc1XgTeamModel.UserInfoe getUserr = _userInfoService.GetFirstOrDefaultInclude(id);
                return View(getUserr);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "User,Admin")]
        public ActionResult EditUser(HttpPostedFileBase avatarphoto, HttpPostedFileBase coverphoto, Xgteamc1XgTeamModel.UserInfoe editUser, FormCollection form)
        {

            string dosyaYolu = "", dosyaYolu2 = "", yuklemeYeri = "", yuklemeYeri2 = "";
            Xgteamc1XgTeamModel.UserInfoe getUser = _userInfoService.GetFirstOrDefaultInclude(editUser.Id);
            if (avatarphoto == null & coverphoto == null)
            {
                getUser.CoverPhotoPath = editUser.CoverPhotoPath;
                getUser.PhotoPath = editUser.PhotoPath;
            }
            else if (avatarphoto == null & coverphoto != null)
            {
                dosyaYolu = Path.GetFileName(coverphoto.FileName);
                yuklemeYeri = Path.Combine(Server.MapPath("~/FileUpload/CoverPhoto"), dosyaYolu);
                coverphoto.SaveAs(yuklemeYeri);
                getUser.CoverPhotoPath = dosyaYolu;
            }
            else if (avatarphoto != null & coverphoto == null)
            {
                dosyaYolu2 = Path.GetFileName(avatarphoto.FileName);
                yuklemeYeri2 = Path.Combine(Server.MapPath("~/FileUpload/UserProfile"), dosyaYolu2);
                avatarphoto.SaveAs(yuklemeYeri2);
                getUser.PhotoPath = dosyaYolu2;
            }
            else if (avatarphoto != null & coverphoto != null)
            {
                dosyaYolu = Path.GetFileName(coverphoto.FileName);
                yuklemeYeri = Path.Combine(Server.MapPath("~/FileUpload/CoverPhoto"), dosyaYolu);
                coverphoto.SaveAs(yuklemeYeri);
                getUser.CoverPhotoPath = dosyaYolu;

                dosyaYolu2 = Path.GetFileName(avatarphoto.FileName);
                yuklemeYeri2 = Path.Combine(Server.MapPath("~/FileUpload/UserProfile"), dosyaYolu2);
                avatarphoto.SaveAs(yuklemeYeri2);
                getUser.PhotoPath = dosyaYolu2;
            }

            if (form["year"] != "" && form["day"] != null && form["month"] != null)
            {
                var year = Convert.ToInt16(form["year"]);
                var day = Convert.ToInt16(form["day"]);
                var month = Convert.ToInt16(form["month"]);

                DateTime birthday = new DateTime(year, month, day);
                getUser.BirthDate = birthday.Date;
            }
            else
                getUser.BirthDate = editUser.BirthDate;

            getUser.CountryID = editUser.CountryID;
            getUser.UserCity = editUser.UserCity;
            getUser.FacebookLink = editUser.FacebookLink;
            getUser.TwitterLink = editUser.TwitterLink;
            getUser.SkypeID = editUser.SkypeID;
            _userInfoService.Update(getUser);

            return RedirectToAction("Profile", "Users", new { id = editUser.Id });
        }

        #endregion

        #region FriendshipManagement

        [Authorize(Roles = "User,Admin")]
        public ActionResult AddFriend(string id)
        {
            Xgteamc1XgTeamModel.User futureFriend = _userService.GetByUserID(id);  //futureFriend
            FriendRequest request = new FriendRequest
            {
                UserID = User.Identity.GetUserId(),
                FutureFriendID = futureFriend.Id,
                SendingDate = DateTime.Now,
                IsPending = true
            };
            _friendRequestService.Add(request);
            ViewBag.request = _friendRequestService.GetList();
            return RedirectToAction("Profile", "Users", new { id });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult FriendRequests(string id)
        {
            if (User.Identity.GetUserId() == id)
            {
                Xgteamc1XgTeamModel.User GetUser = _userService.GetFirstOrDefaultInclude(id);
                if (GetUser == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.request = _friendRequestService.GetList();
                return View(GetUser);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult AcceptRequest(int id)
        {
            FriendRequest request = _friendRequestService.GetByrequest(id);
            string FutureID = request.FutureFriendID;
            string UserID = request.UserID;
            Friend friend = new Friend
            {
                FutureFriendID = FutureID,
                UserID = UserID,
                BecameFriendDate = DateTime.Now
            };
            _friendService.Add(friend);

            request.IsPending = false;
            request.IsAccepted = true;
            _friendRequestService.Update(request);
            return RedirectToAction("FriendRequests", "Users", new { id = User.Identity.GetUserId() });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult RemoveRequest(int id)
        {
            FriendRequest request = _friendRequestService.GetByrequest(id);
            _friendRequestService.Delete(request);
            return RedirectToAction("FriendRequests", "Users", new { id = User.Identity.GetUserId() });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult RemoveMyRequest(int id)
        {
            FriendRequest request = _friendRequestService.GetByrequest(id);
            string futureFR = request.FutureFriendID;
            _friendRequestService.Delete(request);
            return RedirectToAction("Profile", "Users", new { id = futureFR });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult RemoveAFriend(int id)
        {
            FriendRequest req = _friendRequestService.GetByrequest(id);
            string user_ID = req.UserID;
            Friend fd = _friendService.GetByUserandFuture(req.UserID, req.FutureFriendID);
            _friendService.Delete(fd);
            _friendRequestService.Delete(req);
            return RedirectToAction("Profile", "Users", new { id = user_ID });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult RemoveMYFriend(int id)
        {
            FriendRequest req = _friendRequestService.GetByrequest(id);
            string userID = req.FutureFriendID;
            string FutureFriendID = req.UserID;
            Friend fd = _friendService.GetByUserandFuture(userID, FutureFriendID);
            _friendService.Delete(fd);
            _friendRequestService.Delete(req);
            return RedirectToAction("Profile", "Users", new { id = FutureFriendID });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult AddSenderFriend(int id)
        {
            FriendRequest request = _friendRequestService.GetByrequest(id);
            request.IsPending = false;
            request.IsAccepted = true;
            _friendRequestService.Update(request);

            Friend fd = new Friend
            {
                FutureFriendID = request.FutureFriendID,
                UserID = request.UserID //sender
            };
            _friendService.Add(fd);
            return RedirectToAction("Profile", "Users", new { id = fd.UserID });
        }

        [AllowAnonymous]
        public ActionResult UserFriends(string id)
        {
            Xgteamc1XgTeamModel.User GetUser = _userService.SingleStringIncludeWithExp(id);
            if (GetUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.request = _friendRequestService.GetList();
            return View(GetUser);
        }

        #endregion

        #region InboxManagement

        [Authorize(Roles = "User,Admin")]
        public ActionResult Messenger(string id)
        {
            return View();
        }
        #endregion

        #region PasswordManagement
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> ChangePassword(ChangingPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userService.GetByUserID(User.Identity.GetUserId());
            Entities.Identity.Entities.User checkPass = _userManager.FindById(User.Identity.GetUserId());
            bool result = await _userManager.CheckPasswordAsync(checkPass, model.OldPassword);
            if (result)
            {
                string passwordHasher = _userManager.PasswordHasher.HashPassword(model.NewPassword);
                user.PasswordHash = passwordHasher.ToString();
                _userService.Update(user);
                ViewBag.success = "Password has been changed !";
            }
            else
            {
                IdentityResult errorResult = new IdentityResult("Check yor password !");
                AddErrors(errorResult);
            }
            return View(model);
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}