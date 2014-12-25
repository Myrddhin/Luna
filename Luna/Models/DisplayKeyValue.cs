using System;
using System.ComponentModel;
using Loki.Common;
using Loki.UI;

namespace Luna.UI
{
    public class DisplayKeyValue<K> : DisplayElement
    {
        #region Key

        private PropertyChangedEventArgs keyChangedEventArgs = ObservableHelper.CreateChangedArgs<DisplayKeyValue<K>>(x => x.Key);

        private K key;

        public K Key
        {
            get
            {
                return key;
            }

            set
            {
                if (!Object.Equals(key, value))
                {
                    key = value;
                    NotifyChanged(keyChangedEventArgs);
                }
            }
        }

        #endregion Key

        #region Value

        private PropertyChangedEventArgs valueBackerChangedEventArgs = ObservableHelper.CreateChangedArgs<DisplayKeyValue<K>>(x => x.Value);

        private string valueBacker
            ;

        public string Value
        {
            get
            {
                return valueBacker;
            }

            set
            {
                if (!Object.Equals(valueBacker, value))
                {
                    valueBacker = value;
                    NotifyChanged(valueBackerChangedEventArgs);
                }
            }
        }

        #endregion Value
    }
}