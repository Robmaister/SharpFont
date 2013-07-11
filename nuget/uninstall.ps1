param($installPath, $toolsPath, $package, $project)

$project.Objects.References | Where-Object { $_.Name -eq 'SharpFont' } | ForEach-Object { $_.Remove() }

$files = $project.ProjectItems | Where-Object { $_.Name -eq "SharpFont.dll.config" -or $_.Name -eq "freetype6.dll" }
if ($files)
{
	$files | ForEach-Object { $_.Delete() }
}