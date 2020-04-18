using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using SatisfactoryModeler.ViewModels.Editors;

namespace System
{
    public static class SomeExtensions
    {
        public static T As<T>(this object src) where T : class
            => src as T;

        public static T CastTo<T>(this object src)
            => (T)src;

        public static void SetValue<T>(this Endpoint endpoint, T newValue)
            => endpoint?.Editor?.SetValue(newValue);

        public static void SetValue<T>(this NodeEndpointEditorViewModel editor, T newValue)
            => editor.CastTo<IModifiableValueEditorViewModel>().Value = newValue;

        public static void AddNodeType(this NodeListViewModel nodeListViewModel, Type nodeViewModelType)
            => Activator.CreateInstance(typeof(NodeTypeAdder<>).MakeGenericType(nodeViewModelType))
                .CastTo<INodeTypeAdder>()
                .AddNodeType(nodeListViewModel);

        private interface INodeTypeAdder
        {
            void AddNodeType(NodeListViewModel nodeListViewModel);
        }

        private class NodeTypeAdder<TNVM> : INodeTypeAdder
            where TNVM : NodeViewModel, new()
        {
            public void AddNodeType(NodeListViewModel nodeListViewModel)
                => nodeListViewModel.AddNodeType(() => new TNVM());
        }
    }
}
