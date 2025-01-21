using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnimalsNeedWater.Core.Models;
using StardewModdingAPI;

namespace AnimalsNeedWater.Core
{
    public class TroughPlacementProfiles
    {
        public static TroughPlacementProfile? DefaultProfile;
        public static List<TroughPlacementProfile>? LoadedProfiles;

        public static void LoadProfiles(IModHelper helper)
        {
            var profiles = new List<TroughPlacementProfile>();
            var availableFiles = Directory.GetFiles(Path.Combine(helper.DirectoryPath, "assets/profiles")).Where(filename => filename.Contains(".json"));
            foreach (var file in availableFiles)
            {
                var profile = helper.Data.ReadJsonFile<TroughPlacementProfile>("assets/profiles/" + Path.GetFileName(file));
                if (profile == null)
                    continue;
                
                if (Path.GetFileNameWithoutExtension(file).Equals("default"))
                {
                    DefaultProfile = profile;
                }
                else
                {
                    profiles.Add(profile);
                }
            }

            LoadedProfiles = profiles;
        }
        
        public static TroughPlacementProfile? GetProfileByUniqueId(string id)
        {
            TroughPlacementProfile? result = null;
            foreach (var profile in LoadedProfiles!)
            {
                if (profile.ModUniqueId!.Contains(id, StringComparison.CurrentCultureIgnoreCase))
                {
                    result = profile;
                }
            }
            
            return result;
        }
    }
}