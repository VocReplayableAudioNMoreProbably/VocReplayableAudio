using System.Reflection;
using MelonLoader;

[assembly: MelonInfo(typeof(VoCReplayableAudio.ReplayableAudioMod), VoCReplayableAudio.BuildInfo.Name, VoCReplayableAudio.BuildInfo.Version, VoCReplayableAudio.BuildInfo.Author, VoCReplayableAudio.BuildInfo.DownloadLink)]
[assembly: MelonGame("Voice of Cards: Replayable Audio", null)]

[assembly: AssemblyTitle(VoCReplayableAudio.BuildInfo.Description)]
[assembly: AssemblyDescription(VoCReplayableAudio.BuildInfo.Description)]
[assembly: AssemblyCompany(VoCReplayableAudio.BuildInfo.Company)]
[assembly: AssemblyProduct(VoCReplayableAudio.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + VoCReplayableAudio.BuildInfo.Author)]
[assembly: AssemblyTrademark(VoCReplayableAudio.BuildInfo.Company)]
[assembly: AssemblyVersion(VoCReplayableAudio.BuildInfo.Version)]
[assembly: AssemblyFileVersion(VoCReplayableAudio.BuildInfo.Version)]
[assembly: MelonColor()]
