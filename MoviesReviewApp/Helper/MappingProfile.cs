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
            CreateMap<MovieDto, Movie>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<Distributor, DistributorDto>();
            CreateMap<DistributorDto, Distributor>();
            CreateMap<Director, DirectorDto>();
            CreateMap<DirectorDto, Director>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
