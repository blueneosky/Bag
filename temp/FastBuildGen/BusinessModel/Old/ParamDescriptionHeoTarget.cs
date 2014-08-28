using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel.Old
{
    internal class ParamDescriptionHeoTarget : ParamDescription, IParamDescriptionHeoTarget
    {
        #region Members

        private ObservableCollection<IParamDescriptionHeoModule> _dependencies;

        #endregion Members

        #region ctor

        public ParamDescriptionHeoTarget(string name)
            : base(name)
        {
            _dependencies = new ObservableCollection<IParamDescriptionHeoModule>();
            _dependencies.CollectionChanged += _dependencies_CollectionChanged;
        }

        #endregion ctor

        #region Properties

        public IEnumerable<IParamDescriptionHeoModule> Dependencies
        {
            get { return _dependencies; }
        }

        #endregion Properties

        #region Methodes

        public void AddDependencies(IEnumerable<IParamDescriptionHeoModule> dependencies)
        {
            dependencies = dependencies.Execute();  // enumerate more than once

            HashSet<string> names = dependencies
                .Select(m => m.Name)
                .ToHashSet();
            IParamDescriptionHeoModule existingModule = _dependencies
                .Where(m => names.Contains(m.Name))
                .FirstOrDefault();
            if (existingModule != null)
                throw new FastBuildGenException("Module '" + existingModule.Name + "' already in target '" + this.Name + "'.");

            _dependencies.AddRange(dependencies);
        }

        public void AddDependency(IParamDescriptionHeoModule dependency)
        {
            string name = dependency.Name;
            bool exist = _dependencies
                .Where(m => m.Name == name)
                .Any();
            if (exist)
                throw new FastBuildGenException("Module '" + name + "' already in target '" + this.Name + "'.");

            _dependencies.Add(dependency);
        }

        public void ClearDependencies()
        {
            _dependencies.Clear();
        }

        public bool RemoveDependency(string name)
        {
            bool result = false;
            IParamDescriptionHeoModule module = _dependencies
                .Where(m => m.Name == name)
                .FirstOrDefault();
            if (module != null)
                result = _dependencies.Remove(module);

            return result;
        }

        #endregion Methodes

        #region Model events

        private void _dependencies_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnDependenciesChanged(this, e);
        }

        #endregion Model events

        #region Events

        public event NotifyCollectionChangedEventHandler DependenciesChanged;

        protected virtual void OnDependenciesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DependenciesChanged.Notify(sender, e);
        }

        #endregion Events
    }
}