using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    public class DateTimePickerValidatingFixed : DateTimePicker
    {
        private bool pendingValidating = false;
        private bool pendingTextChanged = false;
        private bool pendingValueChanged = false;

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (pendingValidating)
            {
                if (pendingTextChanged || pendingValueChanged)
                {
                    CancelEventArgs cancelEventArgs = new CancelEventArgs();
                    OnValidating(cancelEventArgs);
                    if (cancelEventArgs.Cancel)
                    {
                        this.Select();
                        return;
                    }
                    OnValidated(EventArgs.Empty);
                }
                pendingValidating = false;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            pendingTextChanged = true;
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            base.OnValueChanged(eventargs);
            pendingValueChanged = true;
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            pendingTextChanged = false;
            pendingValueChanged = false;
            base.OnValidating(e);
            pendingValidating = true;
        }
    }
}