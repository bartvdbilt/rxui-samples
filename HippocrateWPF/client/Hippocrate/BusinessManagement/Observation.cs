using System.ServiceModel;
using System.Windows;

namespace Hippocrate.BusinessManagement
{
    public static class Observation
    {
        public static bool AddObservation(int idPatient, ServiceObservation.Observation o)
        {
            try
            {
                ServiceObservation.ServiceObservationClient c = new ServiceObservation.ServiceObservationClient();
                return c.AddObservation(idPatient, o);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Le serveur ne répond pas.", "Erreur");
                return false;
            }
        }
    }
}
