using Microsoft.EntityFrameworkCore;
using ReactCRUDSupport_v1.Database;
using ReactCRUDSupport_v1.Models.Domain;

namespace ReactCRUDSupport_v1.Repositories
{
    public interface IValuesRepository
    {
        public Task<AddDomain?> UpdateValue(Guid id, AddDomain domain);
        public Task<AddDomain?> GetTotal();
        public Task<AddDomain?> AddValue(AddDomain domain);
    }

    public class ValuesRepository : IValuesRepository
    {
        private readonly ValuesDbContext _valuesDbContext;

        public ValuesRepository(ValuesDbContext valuesDbContext)
        {
            _valuesDbContext = valuesDbContext;
        }

        public async Task<AddDomain?> AddValue(AddDomain domain)
        {
            var valueDomain = await _valuesDbContext.AddOne.FirstOrDefaultAsync();

            if (valueDomain == null)
                return null;

            valueDomain.Value = valueDomain.Value + domain.Value;
            await _valuesDbContext.SaveChangesAsync();

            return valueDomain;
        }

        public async Task<AddDomain?> UpdateValue(Guid id, AddDomain domain)
        {
            var valueDomain = await _valuesDbContext.AddOne.FirstOrDefaultAsync(x => x.Id == id);

            if (valueDomain == null)
                return null;

            valueDomain.Value = domain.Value;
            await _valuesDbContext.SaveChangesAsync();

            return valueDomain;
        }

        public async Task<AddDomain?> GetTotal()
        {
            var total =  await _valuesDbContext.AddOne.FirstOrDefaultAsync();
            if (total == null)
                return null;

            return total;
        }
    }
}
