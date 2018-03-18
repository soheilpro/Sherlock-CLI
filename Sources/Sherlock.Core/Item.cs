using System;

namespace Sherlock.Core
{
    public class Item : IItem
    {
        public IFolder Parent
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public bool IsSecret
        {
            get;
            set;
        }

        public int CompareTo(IItem other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
