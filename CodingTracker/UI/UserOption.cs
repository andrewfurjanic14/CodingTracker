using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.UI
{
    enum UIOptionKeyword
    {
        View,
        Log,
        Delete,
        Update,
        Exit,

        ChooseFromAll,
        ChooseFromDate
    }
    internal class UserOption
    {
        public string Label { get; set; }
        public UIOptionKeyword Id { get; set; }
    }
}
