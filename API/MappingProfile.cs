using API.Models.Comments;
using API.Models.Posts;
using API.Models.Roles;
using API.Models.Tags;
using API.Models.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;


namespace WebBlog.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ViewRole, Role>().ReverseMap();
            CreateMap<AddRoleRequest, Role>().ReverseMap();
            CreateMap<EditRoleRequest, Role>().ReverseMap();
            CreateMap<ViewTag, Tag>().ReverseMap();
            CreateMap<AddTagRequest, Tag>().ReverseMap();
            CreateMap<EditTagRequest, Tag>().ReverseMap();
            CreateMap<ViewUser, User>().ReverseMap();
            CreateMap<ViewUserProfile, User>().ReverseMap();
            CreateMap<AddUserRequest, User>().ReverseMap();
            CreateMap<RegisterUserRequest, User>().ReverseMap();
            CreateMap<EditUserRequest, User>().ReverseMap();
            CreateMap<EditUserProfileRequest, User>().ReverseMap();
            CreateMap<ViewComment, Comment>().ReverseMap();
            CreateMap<AddCommentRequest, Comment>().ReverseMap();
            CreateMap<EditCommentRequest, Comment>().ReverseMap();
            CreateMap<ViewPost, Post>().ReverseMap();
            CreateMap<AddPostRequest, Post>().ReverseMap();
            CreateMap<EditPostRequest, Post>().ReverseMap();
        }
    }
}
