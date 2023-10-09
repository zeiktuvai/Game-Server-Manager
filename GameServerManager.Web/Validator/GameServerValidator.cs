using FluentValidation;
using FluentValidation.Validators;

namespace GameServerManager.Web.Validator
{
    public class GBServerValidator : AbstractValidator<GBServer>
    {
        public GBServerValidator()
        {
            Include(new GameServerValidator());
            RuleFor(s => s.MaxPlayers).LessThan(17).WithMessage("Maximum Max players is 16");
            RuleFor(s => s.RestartTime).GreaterThan(0).LessThan(25).WithMessage("Please enter a restart timer between 1-24 hours");
            RuleFor(s => s.ServerPassword).NotEmpty().When(s => s._IsPrivateServer == true).WithMessage("Please enter a server password");
        }
    }
    
    public class GameServerValidator : AbstractValidator<GameServer>
    {
        public GameServerValidator()
        {
            RuleFor(s => s.Port).NotEmpty().WithMessage("Please enter a port number");
            RuleFor(s => s.ServerName).NotEmpty().WithMessage("Please enter a server name");
            RuleFor(s => s.MaxPlayers).NotEmpty().WithMessage("Please enter max players");
            RuleFor(s => s.QueryPort).NotEmpty().WithMessage("Please enter a query Port");
        }
    }

}
