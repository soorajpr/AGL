using System;

namespace GroupedPetsList.Shared
{
    public class ControllerBase<T> where T : class, new()
    {
        static volatile T instance;
        static readonly object SyncRoot = new Object();

        #region Singleton Instance

        public static T Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    if (instance == null)
                    {
                        instance = new T();

                    }
                }
                return instance;
            }
        }

        #endregion


    }
}
