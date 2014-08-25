using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Xml.Entity;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Xml
{
    public class XmlSession : IDisposable
    {
        #region Common

        private Dictionary<XmlId, object> _instanceByXmlIds = new Dictionary<XmlId, object> { };

        private bool? _isSerialiseMode;
        private Dictionary<XmlId, IXmlObjectId> _xmlInstanceByXmlIds = new Dictionary<XmlId, IXmlObjectId> { };

        public object GetInstance(XmlId xmlId)
        {
            object result = null;
            _instanceByXmlIds.TryGetValue(xmlId, out result);

            return result;
        }

        public IXmlObjectId GetXmlObjectId(XmlId xmlId)
        {
            IXmlObjectId result = null;
            _xmlInstanceByXmlIds.TryGetValue(xmlId, out result);

            return result;
        }

        public TIXmlObjectId GetXmlObjectId<TIXmlObjectId>(XmlId xmlId)
            where TIXmlObjectId : class, IXmlObjectId
        {
            TIXmlObjectId result = GetXmlObjectId(xmlId) as TIXmlObjectId;

            return result;
        }

        public void Register(XmlId xmlId, object instance)
        {
            object current = null;
            bool exists = _instanceByXmlIds.TryGetValue(xmlId, out current);
            if (exists)
            {
                if (Object.ReferenceEquals(instance, current))
                    return; // alreay registered

                throw new FastBuildGenException("Duplicated id");
            }
            _instanceByXmlIds.Add(xmlId, instance);
        }

        public void Register(IXmlObjectId xmlObjectId)
        {
            XmlId xmlId = xmlObjectId.XmlId;

            IXmlObjectId current = null;
            bool exists = _xmlInstanceByXmlIds.TryGetValue(xmlId, out current);
            if (exists)
            {
                if (Object.ReferenceEquals(xmlObjectId, current))
                    return; // alreay registered

                throw new FastBuildGenException("Duplicated id");
            }
            _xmlInstanceByXmlIds.Add(xmlId, xmlObjectId);
        }

        private void CheckSerializeMode(bool isSerializeMode)
        {
            if (_isSerialiseMode == null)
            {
                _isSerialiseMode = isSerializeMode;
            }
            else
            {
                if (_isSerialiseMode != isSerializeMode)
                {
                    throw new FastBuildGenException("Mixed mode not allowed");
                }
            }
        }

        #endregion Common

        #region Serialization

        public XmlFastBuild GetOrCreateXmlFastBuild(IFastBuildModel model)
        {
            CheckSerializeMode(true);
            return GetOrCreateXml<XmlFastBuild, IFastBuildModel>(model);
        }

        public XmlFastBuildInternalVar GetOrCreateXmlFastBuildInternalVar(IFastBuildInternalVarModel model)
        {
            CheckSerializeMode(true);
            return GetOrCreateXml<XmlFastBuildInternalVar, IFastBuildInternalVarModel>(model);
        }

        public XmlFastBuildParam GetOrCreateXmlFastBuildParam(IFastBuildParamModel model)
        {
            CheckSerializeMode(true);
            return GetOrCreateXml<XmlFastBuildParam, IFastBuildParamModel>(model);
        }

        public XmlParamDescriptionHeoModule GetOrCreateXmlParamDescriptionHeoModule(IParamDescriptionHeoModule module)
        {
            CheckSerializeMode(true);
            return GetOrCreateXml<XmlParamDescriptionHeoModule, IParamDescriptionHeoModule>(module);
        }

        public XmlParamDescriptionHeoTarget GetOrCreateXmlParamDescriptionHeoTarget(IParamDescriptionHeoTarget target)
        {
            CheckSerializeMode(true);
            return GetOrCreateXml<XmlParamDescriptionHeoTarget, IParamDescriptionHeoTarget>(target);
        }

        #region Core

        private int _lastXmlId = -1;
        private Dictionary<object, object> _xmlInstanceByInstances = new Dictionary<object, object> { };

        private TXmlObjectId CreateXml<TXmlObjectId, TInstance>(TInstance instance)
            where TInstance : class
            where TXmlObjectId : class, IXmlObjectId<TInstance>, new()
        {
            TXmlObjectId result;

            // new xml object (empty)
            result = new TXmlObjectId();
            // set XmlId
            XmlId id = GetNextFreeId();
            result.XmlId = id;
            // memorize the xml object
            _xmlInstanceByInstances[instance] = result;
            Register(result);
            Register(id, instance);
            // serialize info
            result.Serialize(instance, this);

            return result;
        }

        private XmlId GetNextFreeId()
        {
            int id = _lastXmlId + 1;    // optim
            while (_xmlInstanceByXmlIds.ContainsKey((XmlId)id))
            {
                id++;
            }

            _lastXmlId = id;
            return (XmlId)id;
        }

        private TXmlObjectId GetOrCreateXml<TXmlObjectId, TInstance>(TInstance instance)
            where TInstance : class
            where TXmlObjectId : class, IXmlObjectId<TInstance>, new()
        {
            TXmlObjectId result;

            object xmlObjectId = null;
            bool success = _xmlInstanceByInstances.TryGetValue(instance, out xmlObjectId);
            if (success)
            {
                result = xmlObjectId as TXmlObjectId;
            }
            else
            {
                result = CreateXml<TXmlObjectId, TInstance>(instance);
            }

            return result;
        }

        #endregion Core

        #endregion Serialization

        #region Deserialization

        public void CopyTo<TInstance, TXmlObjectId>(TInstance instanceDest, TXmlObjectId xmlObjectIdSource)
            where TInstance : class
            where TXmlObjectId : class, IXmlObjectId<TInstance>
        {
            CheckSerializeMode(false);
            // deserialize first
            Deserialize(xmlObjectIdSource);
            // copy
            xmlObjectIdSource.CopyTo(instanceDest);
        }

        public void Deserialize<TInstance>(IXmlObjectId<TInstance> xmlObjectIdSource)
            where TInstance : class
        {
            CheckSerializeMode(false);
            xmlObjectIdSource.Deserialize(this);
        }

        #endregion Deserialization

        #region Disposable

        // Track whether Dispose has been called.
        private bool _disposed = false;

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~XmlSession()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this._disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this._instanceByXmlIds.Clear();
                    this._xmlInstanceByInstances.Clear();
                    this._xmlInstanceByXmlIds.Clear();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                // Note : nothing !

                // Note disposing has been done.
                _disposed = true;
            }
        }

        // Use interop to call the method necessary
        // to clean up the unmanaged resource.
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        #endregion Disposable
    }
}