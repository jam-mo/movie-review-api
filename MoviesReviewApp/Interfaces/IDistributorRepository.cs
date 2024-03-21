using MoviesReviewApp.Models;

namespace MoviesReviewApp.Interfaces
{
    public interface IDistributorRepository
    {
        ICollection<Distributor> GetDistributors();
        Distributor GetDistributor(int id);
        Distributor GetDistributorByDirector(int directorId); // 
        ICollection<Director> GetDirectorsFromADistributor(int id);
        bool DistributorExists(int id);
        bool CreateDistributor(Distributor distributor);
        bool UpdateDistributor(Distributor distributor);

        bool DeleteDistributor(Distributor distributor);
        bool Save();
    }
}
