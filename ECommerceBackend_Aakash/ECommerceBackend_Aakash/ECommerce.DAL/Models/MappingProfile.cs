using AutoMapper;
using ECommerce.DAL.Models.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<ProductDTO, Product>();
        }
    }
}
