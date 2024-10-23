using RapidPay.Application.Interfaces;

namespace RapidPay.Application.Services
{
    public class FeeService : IFeeService
    {
        private static readonly object LockObject = new object();
        private static FeeService _instance;
        private static decimal _currentFeeRate = 1.0m;
        private static Timer _timer;

        private FeeService()
        {
            _timer = new Timer(UpdateFeeRate, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }

        public static FeeService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new FeeService();
                        }
                    }
                }
                return _instance;
            }
        }

        private void UpdateFeeRate(object state)
        {
            lock (LockObject)
            {
                var random = new Random();
                var randomValue = (decimal)(random.NextDouble() * 2.0);
                _currentFeeRate *= randomValue;  
            }
        }

        public decimal GetCurrentFeeRate()
        {
            lock (LockObject)
            {
                return _currentFeeRate;
            }
        }
    }
}
