using AutoMapper;
using MoviesReviewApp.Dto;
using MoviesReviewApp.Models;

namespace MoviesReviewApp.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDto>();
        }
    }
}
