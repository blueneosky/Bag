using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.Assets.Converters
{
    public abstract class DataConverter<T1, T2> : IDataConverter<T1, T2>
    {
        private static Lazy<IDataConverter<T1, T2>> _defaultCache
            = new Lazy<IDataConverter<T1, T2>>(
                () => typeof(T1) == typeof(T2)
                    ? (new NoDataConverter<T1>() as IDataConverter<T1, T2>)
                    : new DefaultDataConverter());

        public static IDataConverter<T1, T2> Default => _defaultCache.Value;


        protected DataConverter() { }

        public abstract T2 Convert(T1 data);
        public abstract T1 Invert(T2 data);

        object IDataConverter.Convert(object data) => Convert((T1)data);
        object IDataConverter.Invert(object data) => Invert((T2)data);

        #region Class DefaultDataConverter<,>

        private sealed class DefaultDataConverter : DataConverter<T1, T2>
        {
            public DefaultDataConverter() { }
            public override T2 Convert(T1 data) => (T2)System.Convert.ChangeType(data, typeof(T2));
            public override T1 Invert(T2 data) => (T1)System.Convert.ChangeType(data, typeof(T1));
        }

        #endregion

        #region Class NoDataConverter<,>

        private sealed class NoDataConverter<T> : DataConverter<T, T>
        {
            public NoDataConverter() { }
            public override T Convert(T data) => data;
            public override T Invert(T data) => data;
        }

        #endregion
    }
}
