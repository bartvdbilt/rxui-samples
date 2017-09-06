using Hippocrate.ServiceObservation;
using System.Threading.Tasks;

namespace Hippocrate.DataAccess
{
    public class ServiceObservationManager : IServiceObservation
    {
        private ServiceObservationClient _client;
        public ServiceObservationManager()
        {
            this._client = new ServiceObservationClient();
        }

        public bool AddObservation(int idPatient, Observation obs)
        {
            return this._client.AddObservation(idPatient, obs);
        }

        public Task<bool> AddObservationAsync(int idPatient, Observation obs)
        {
            return this.AddObservationAsync(idPatient, obs);
        }
    }
}