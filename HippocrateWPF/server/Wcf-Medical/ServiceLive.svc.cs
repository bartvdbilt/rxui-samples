using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Configuration;

namespace Wcf_Medical
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServiceLive : IServiceLive
    {
        IDuplexCallback _callback;
        string _requestString = String.Empty;
        Timer _timerHeart = null;
        Timer _timerTemp = null;
        Random _Random = new Random();
        int _intervalHeart = 100;
        int _intervalTemp = 100;


        public ServiceLive()
        {
            _callback = OperationContext.Current.GetCallbackChannel<IDuplexCallback>();
        }


        #region IServiceLive Members
        /// <summary>
        /// permet de s'abonner pour récupérer les valeurs
        /// </summary>
        public void Subscribe()
        {

            _intervalHeart = Convert.ToInt32(ConfigurationManager.AppSettings["timeHeart"]);
            _intervalTemp = Convert.ToInt32(ConfigurationManager.AppSettings["timeTemp"]);

            _timerHeart = new Timer(new TimerCallback(TimerHeart_Elapsed), null, _intervalHeart, Timeout.Infinite);
            _timerTemp = new Timer(new TimerCallback(TimerTemp_Elapsed), null, _intervalTemp, Timeout.Infinite);
        }

        #endregion

        /// <summary>
        /// Retourne une valeur pour les pulsations du coeur
        /// </summary>
        /// <param name="data"></param>
        private void TimerHeart_Elapsed(object data)
        {

            int interval = _Random.Next(0, 360);
            try
            {
                if (_timerHeart != null)
                {
                    _callback.PushDataHeart(Math.Sin(interval));
                    _timerHeart.Change(_intervalHeart, Timeout.Infinite);
                }
                else
                {
                    Debug.WriteLine("timer heart null");
                }
            }
            catch (Exception ex)
            {
                _timerHeart = null;
            }

        }

        /// <summary>
        /// Retourne une valeur pour la température
        /// </summary>
        /// <param name="data"></param>
        private void TimerTemp_Elapsed(object data)
        {

            int interval = _Random.Next(360, 390);
            try
            {
                if (_timerTemp != null)
                {
                    _callback.PushDataTemp(interval / 10);
                    _timerTemp.Change(_intervalTemp, Timeout.Infinite);
                }
                else
                {
                    Debug.WriteLine("timer heart null");
                }
            }
            catch (Exception ex)
            {
                _timerTemp = null;
            }

        }
    }
}
