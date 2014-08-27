using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.Control.ListEditor
{
    internal class ListEditorElement : INotifyPropertyChanged
    {
        private readonly object _value;
        private string _text;

        public ListEditorElement(object value)
        {
            _value = value;
            Text = _value.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual String Text
        {
            get { return _text; }
            protected set
            {
                if (_text == value)
                    return;

                _text = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstListEditorElementEvent.ConstText));
            }
        }

        public object Value
        {
            get { return _value; }
        }

        public override bool Equals(object obj)
        {
            ListEditorElement element = obj as ListEditorElement;
            if (obj == null)
                return false;

            return Object.Equals(this.Value, element.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() | "ListEditorElement".GetHashCode();
        }

        public override string ToString()
        {
            return Text;
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }
    }
}