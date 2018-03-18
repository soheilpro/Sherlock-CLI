using System;
using System.Collections.Generic;
using System.Linq;
using Sherlock.Core;

namespace Sherlock
{
    internal static class FolderHelper
    {
        public static bool IsRoot(this IFolder folder)
        {
            return folder.Parent == null;
        }

        public static IFolder GetRoot(this IFolder folder)
        {
            if (folder.IsRoot())
                return folder;

            return GetRoot(folder.Parent);
        }

        public static string GetFullPath(this IFolder folder)
        {
            if (folder.IsRoot())
                return "/";

            return "/" + string.Join("/", folder.GetHierarchy().Select(theFolder => theFolder.Name));
        }

        public static IEnumerable<IFolder> GetHierarchy(this IFolder folder)
        {
            if (folder.Parent == null)
                yield break;

            foreach (var parent in folder.Parent.GetHierarchy())
                yield return parent;

            yield return folder;
        }

        public static int GetIndex(this IFolder folder)
        {
            return folder.Parent.Folders.IndexOf(folder);
        }

        public static IEnumerable<IFolder> FindFolders(this IFolder folder, string spec)
        {
            if (spec == "/")
            {
                yield return folder.GetRoot();
            }
            else if (spec == ".")
            {
                yield return folder;
            }
            else if (spec == "..")
            {
                yield return folder.Parent ?? folder;
            }
            else if (int.TryParse(spec, out var index))
            {
                if (index < 1 || index > folder.Folders.Count)
                    yield break;

                yield return folder.Folders[index - 1];
            }
            else
            {
                foreach (var subFolder in folder.Folders.Where(subFolder => subFolder.Name.Contains(spec, StringComparison.OrdinalIgnoreCase)))
                    yield return subFolder;
            }
        }

        public static IEnumerable<IItem> FindItems(this IFolder folder, string spec)
        {
            if (int.TryParse(spec, out var index))
            {
                if (index < 1 || index > folder.Items.Count)
                    yield break;

                yield return folder.Items[index - 1];
            }
            else
            {
                foreach (var item in folder.Items.Where(item => item.Name.Contains(spec, StringComparison.OrdinalIgnoreCase)))
                    yield return item;
            }
        }
    }
}
