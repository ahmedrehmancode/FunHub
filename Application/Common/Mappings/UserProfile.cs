using Application.Feature.Profile.Commands.UpdateProfile;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<UpdateProfileCommand, User>();
           
        }
    }
}
