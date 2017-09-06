using Hippocrate.ServicePatient;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Hippocrate.DataAccess
{
    public class ServicePatientManager : IServicePatient
    {
        private ServicePatientClient _client;
        public ServicePatientManager()
        {
            this._client = new ServicePatientClient();
        }

        public bool AddPatient(Patient user)
        {
            return this._client.AddPatient(user);
        }

        public Task<bool> AddPatientAsync(Patient user)
        {
            return this._client.AddPatientAsync(user);
        }

        public bool DeletePatient(int id)
        {
            return this._client.DeletePatient(id);
        }

        public Task<bool> DeletePatientAsync(int id)
        {
            return this._client.DeletePatientAsync(id);
        }

        public Patient[] GetListPatient()
        {
            return this._client.GetListPatient();
        }

        public Task<Patient[]> GetListPatientAsync()
        {
            return this._client.GetListPatientAsync();
        }

        public Patient GetPatient(int id)
        {
            return this._client.GetPatient(id);
        }

        public Task<Patient> GetPatientAsync(int id)
        {
            return this._client.GetPatientAsync(id);
        }
    }
}