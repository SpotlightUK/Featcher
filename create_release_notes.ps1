#parse parameters supplied by TeamCity script step
param (
    [string]$pivotal_token = "",
    [string]$octopus_key = "",
    [string]$octopus_project_name = ""
)

### Find latest release based on Octopus
$prod_environment_name = "prodaws"

# Find project by name
Add-Type -AssemblyName System.Web
$encoded_proejct_name = [System.Web.HttpUtility]::UrlEncode($octopus_project_name)
Write-Host $encoded_proejct_name
$r = Invoke-WebRequest -Uri "http://octopus/api/projects?name=$encoded_proejct_name" -Headers @{"X-Octopus-ApiKey" = $octopus_key}
Write-Host "Get Octo Project"
$projects = ConvertFrom-Json $r.Content
$project_id = $projects.Items[0].Id

# Find environment by name
$r = Invoke-WebRequest -Uri "http://octopus/api/environments/all" -Headers @{"X-Octopus-ApiKey" = $octopus_key}
Write-Host "Finding Environment"
$environments = ConvertFrom-Json $r.Content
$prod_env = $environments | ? { $_.Name -eq $prod_environment_name } | Select-Object -First 1
$prod_env_id = $prod_env.Id

# Get latest deployment and release

$r = Invoke-WebRequest -Uri "http://octopus/api/deployments?take=1&projects=$project_id&environments=$prod_env_id&taskState=Success" -Headers @{"X-Octopus-ApiKey" = $octopus_key}
Write-Host "Get Deployments"
$deployments = (ConvertFrom-Json $r.Content).Items
$latest_release_link = $deployments[0].Links.Release

# Get version of last release
$r = Invoke-WebRequest -Uri "http://octopus/$latest_release_link" -Headers @{"X-Octopus-ApiKey" = $octopus_key}
Write-Host "Get Latest Release Links"
$latest_octopus_release = (ConvertFrom-Json $r.Content).version

$latest_octopus_release -match "\d*\.\d*\.\d*"
$latest_release = "v" + $Matches[0]

Write-Host "Latest release is $latest_release"

# Obtain a static list of project id's. Pivotal should order them based on last story activity, but don't quote me on that
$projects_response = Invoke-WebRequest -Uri "https://www.pivotaltracker.com/services/v5/projects/" -Headers @{"X-TrackerToken" = "$pivotal_token" }
$projects = ConvertFrom-Json $projects_response.Content | % {$_.id}

function Get-StoryLine {
    param ([string] $story_id)
    Write-Host $story_id
    foreach ($p in $projects) {
        try { $response = Invoke-WebRequest -Uri "https://www.pivotaltracker.com/services/v5/projects/$p/stories/$story_id" -Headers @{"X-TrackerToken" = "$pivotal_token"  } }
            catch {
            # Write-Host "Trying $story_id in project $p and got " $_.Exception.Response.StatusCode.Value__
            }
        if ($response.StatusCode -eq 200){
            # Write-Host "Found story $story_id in project $p"
            $story_name = ConvertFrom-Json $response.Content | % {$_.name}
            return "* [$story_name](https://www.pivotaltracker.com/story/show/$story_id)"
        }
    }
}

# Get unique list of associated ticket numbers from commit starts
# Initialised to arrays to ensure return type. 
$commit_ticket_list = @()
(git log "$latest_release..HEAD" --format="%s") | select-string -Pattern "\[#(.+)\]" |  % { $_.Matches } | % { $_.Value } | sort-object -Unique | % { $_.Replace('[#','').Replace(']','')} | ForEach-Object {$commit_ticket_list += $_}
$merge_branch_list = @()
(git log "$latest_release..HEAD" --merges --format="%s") | select-string -Pattern "SpotlightUK\/\d{9}" |  % { $_.Matches } | % { $_.Value } | % { $_.Replace('SpotlightUK/','')} | ForEach-Object{ $merge_branch_list += $_}
$ticket_list = $commit_ticket_list + $merge_branch_list | sort-object -Unique
Write-Host "Found unique ticket numbers: "
Write-Host $ticket_list

# Get names of deployed stories
$deployed_stories = $ticket_list | % {Get-StoryLine $_}


Write-Host "Creating release notes: "
Write-Host $deployed_stories

$deployed_stories > release_notes.md

Write-Host "Finished gererating release notes"
