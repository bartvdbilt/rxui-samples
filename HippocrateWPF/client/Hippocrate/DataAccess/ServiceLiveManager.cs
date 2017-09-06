using Hippocrate.ServiceLive;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Hippocrate.DataAccess
{
    public class ServiceLiveManager
    {
        private InstanceContext _ic;
        private ServiceLiveClient _client;
        public ServiceLiveManager(IServiceLiveCallback i)
        {
            this._ic = new InstanceContext(i);
            this._client = new ServiceLiveClient(this._ic);
        }

        public void Subscribe()
        {
           this._client.SubscribeAsync();
        }

        public bool IsSubscribed()
        {
            return this._client.State == CommunicationState.Opened;
        }
    }
}