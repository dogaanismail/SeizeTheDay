using FluentValidation;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.ValidationRules.FluentValidation
{
    public class ForumPostValidator : AbstractValidator<ForumPost>
    {
        public ForumPostValidator()
        {
            //Examples for validation system
            //RuleFor(p => p.ForumPostTitle).NotEmpty();
            //RuleFor(p => p.ForumPostContent).NotEmpty();
        }
    }
}
