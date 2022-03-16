[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [ValidateNotNull()]
    [System.Uri]
    $Url,
    [Parameter(Mandatory = $true)]
    [int]
    [ValidateRange(100, 500)]
    $ExpectedStatusCode
)

function FailTest([string] $message){
    Write-Output "::error title=Smoke test failed::$message"
    Exit 1
}

try {
    Write-Output "Sending HTTP GET request to '$($Url.AbsoluteUri)'..."
    $response = Invoke-WebRequest -Uri $Url -TimeoutSec 30
    if ($response.StatusCode -eq $ExpectedStatusCode){
        Write-Output "Smoke test passed."
    }else{
        FailTest("Expected status code '$ExpectedStatusCode' but got '$($response.StatusCode)' instead")
    }
}
catch {
    FailTest($_.Exception.Message)
}