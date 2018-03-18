using System;
using System.Collections.Generic;
using System.Linq;
using Sherlock.Core;

namespace Sherlock
{
    internal static class ItemHelper
    {
        public static int GetIndex(this IItem item)
        {
            return item.Parent.Items.IndexOf(item);
        }
    }
}
