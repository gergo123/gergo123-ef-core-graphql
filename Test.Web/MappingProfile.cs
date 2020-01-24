using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Test.Db.Model.Placeholder;
using Test.Web.ViewModels;

namespace Test.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Since every property is the same, we dont need to explicitly specify mapping details
            CreateMap<PlaceholderEntity, PlaceHolderViewModel>();
            CreateMap<PlaceHolderViewModel, PlaceholderEntity>();

            CreateMap<SimplePlaceHolderEntity, SimplePlaceHolderViewModel>();
        }
    }
}
