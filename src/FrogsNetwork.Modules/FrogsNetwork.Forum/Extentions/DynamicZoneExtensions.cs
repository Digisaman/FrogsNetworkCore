using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsNetwork.Forum.Extentions;
public class DynamicZoneExtensions
{
    public static void RemoveItemFrom(dynamic zone, string itemToDelete)
    {
        var itemsToDelete = new List<object>();

        foreach (var item in zone.Items)
        {
            if (item.Metadata.Type == itemToDelete)
                itemsToDelete.Add(item);
        }

        foreach (var item in itemsToDelete)
        {
            zone.Items.Remove(item);
        }
    }
}
