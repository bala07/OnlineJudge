using AutoMapper;

using Domain.Models;

using OnlineJudge.Web.ViewModels;

namespace OnlineJudge.Web.Initialization
{
    public class AutoMapperInitalizer
    {
        public static void Initialize()
        {
            Mapper.CreateMap<UserRegistrationViewModel, User>()
                .ForMember(model => model.Password, expression => expression.Ignore())
                .ForMember(model => model.PasswordSalt, expression => expression.Ignore());
        }
    }
}