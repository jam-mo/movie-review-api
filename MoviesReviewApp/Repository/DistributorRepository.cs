using AutoMapper;
using MoviesReviewApp.Data;
using MoviesReviewApp.Interfaces;
using MoviesReviewApp.Models;

namespace MoviesReviewApp.Repository
{
    public class DistributorRepository : IDistributorRepository
    {
        private readonly DataContext _context;
       
        public DistributorRepository(DataContext context)
        {
            _context = context;
         
        }

        public bool CreateDistributor(Distributor distributor)
        {
            _context.Add(distributor);
            return Save();
        }

        public bool DeleteDistributor(Distributor distributor)
        {
            _context.Remove(distributor);
            return Save();
        }

        public bool DistributorExists(int id)
        {
            return _context.Distributors.Any(x => x.Id == id);
        }

        public ICollection<Director> GetDirectorsFromADistributor(int id)
        {
            return _context.Directors.Where(x => x.Distributor.Id == id).ToList();
        }

        public Distributor GetDistributor(int id)
        {
            return _context.Distributors.Where(x => x.Id == id).FirstOrDefault();
        }

        public Distributor GetDistributorByDirector(int directorId)
        {
            return _context.Directors.Where(d => d.Id == directorId).Select(x => x.Distributor).FirstOrDefault();
        }

        public ICollection<Distributor> GetDistributors()
        {
            return _context.Distributors.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDistributor(Distributor distributor)
        {
            _context.Update(distributor);
            return Save();
        }
    }
}
