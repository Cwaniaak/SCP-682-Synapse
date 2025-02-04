﻿using System;
using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;

namespace SCP_682_Synapse
{
    [PluginInformation(
Author = "Naku",
Description = "",
LoadPriority = 1,
Name = "SCP-682",
SynapseMajor = 2,
SynapseMinor = 5,
SynapsePatch = 3,
Version = "1.2.1"
)]
    public class SCP682 : AbstractPlugin
    {
        [Config(section = "Scp682")]
        public static PluginConfig Config;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation;
        public override void Load()
        {
            Server.Get.RoleManager.RegisterCustomRole<Scp682PlayerScript>();
            PluginTranslation.AddTranslation(new PluginTranslation());
            new EventHandlers();
        }
    }
}
