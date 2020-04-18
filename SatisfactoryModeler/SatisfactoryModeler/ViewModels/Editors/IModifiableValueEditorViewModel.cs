using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.ViewModels.Editors
{
    public interface IModifiableValueEditorViewModel
    {
        object Value { get; set; }
    }
}
