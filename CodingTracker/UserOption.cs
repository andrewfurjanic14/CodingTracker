using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker
{
    enum UIOptionKeyword
    {
        View,
        Log,
        Delete,
        Update,
        Exit
    }
    internal class UserOption
    {
        public string Label { get; set; }
        public UIOptionKeyword Id { get; set; }
    }
}
