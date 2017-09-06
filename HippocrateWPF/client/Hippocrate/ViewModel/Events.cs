using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hippocrate.ViewModel
{
    interface IUserConnectedChangedEventHandler
    {
        void UserConnectedChangedEventHandler(object sender, ServiceUser.User e);
    }

    interface IPatientSelected
    {
        void PatientSelectedEventHandler(ServicePatient.Patient e);
    }

    interface IUserSelected
    {
        void UserSelectedEventHandler(ServiceUser.User e);
    }
}
