using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;
using WebBlog.Web.Models;

namespace WebBlog.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleViewModel, Role>().ReverseMap();
            CreateMap<AddUserViewModel, User>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<CommentModel, Comment>().ReverseMap();
            CreateMap<PostsViewModel, Post>().ReverseMap();
            CreateMap<PostEditViewModel, Post>().ReverseMap();
            CreateMap<TagViewModel, Tag>().ReverseMap();

        }
    }

}
