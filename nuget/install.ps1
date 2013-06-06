param($installPath, $toolsPath, $package, $project)

# find dll file in project and change it's CopyToOutputDirectory property to Copy If Newer
(($project.ProjectItems | ?{ $_.Name -eq "freetype6.dll" }).Properties | ?{ $_.Name -eq "CopyToOutputDirectory" }).Value = 2
# find config file in project and change it's CopyToOutputDirectory property to Copy If Newer
(($project.ProjectItems | ?{ $_.Name -eq "SharpFont.dll.config" }).Properties | ?{ $_.Name -eq "CopyToOutputDirectory" }).Value = 2